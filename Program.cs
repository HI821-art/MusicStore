using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Data;
using MusicStore.Services;
using System.Configuration;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        var serviceProvider = new ServiceCollection()
            .AddDbContext<MusicStoreDbContext>(options => options.UseSqlServer(connectionString))
            .AddSingleton<IRepository<VinylRecord>, Repository<VinylRecord>>()
            .AddSingleton<IRepository<Artist>, Repository<Artist>>()
            .AddSingleton<IRepository<Genre>, Repository<Genre>>()
            .AddSingleton<IRepository<Promotion>, Repository<Promotion>>()
            .AddSingleton<IRepository<Reservation>, Repository<Reservation>>()
            .AddSingleton<IRepository<Order>, Repository<Order>>()
            .AddSingleton<IRepository<OrderDetail>, Repository<OrderDetail>>()
            .AddSingleton<IRepository<Sale>, Repository<Sale>>()
            .AddSingleton<IVinylRecordService, VinylRecordService>()
            .AddScoped<DbContext, MusicStoreDbContext>() // Add this line
            .BuildServiceProvider();

        var vinylRecordService = serviceProvider.GetService<IVinylRecordService>();

        if (vinylRecordService == null)
        {
            Console.WriteLine("Failed to initialize VinylRecordService.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n===== Музичний Магазин =====");
            Console.WriteLine("1. Переглянути всі платівки");
            Console.WriteLine("2. Додати платівку");
            Console.WriteLine("3. Видалити платівку");
            Console.WriteLine("4. Редагувати платівку");
            Console.WriteLine("5. Продавати платівку");
            Console.WriteLine("6. Списати платівку");
            Console.WriteLine("7. Внести платівку в акцію");
            Console.WriteLine("8. Відкласти платівку для покупця");
            Console.WriteLine("9. Пошук платівок");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllVinylRecords(vinylRecordService);
                    break;
                case "2":
                    AddVinylRecord(vinylRecordService);
                    break;
                case "3":
                    DeleteVinylRecord(vinylRecordService);
                    break;
                case "4":
                    UpdateVinylRecord(vinylRecordService);
                    break;
                case "5":
                    SellVinylRecord(vinylRecordService);
                    break;
                case "6":
                    WriteOffVinylRecord(vinylRecordService);
                    break;
                case "7":
                    AddVinylRecordToPromotion(vinylRecordService);
                    break;
                case "8":
                    ReserveVinylRecord(vinylRecordService);
                    break;
                case "9":
                    SearchVinylRecords(vinylRecordService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невідома команда.");
                    break;
            }
        }
    }

    static void ViewAllVinylRecords(IVinylRecordService service)
    {
        var records = service.GetAllVinylRecords();
        if (!records.Any())
        {
            Console.WriteLine("Платівки відсутні.");
            return;
        }

        foreach (var record in records)
        {
            Console.WriteLine($"{record.Id}. {record.Name} - {record.Artist.Name} ({record.Genre.Name}) - {record.SellingPrice} грн");
        }
    }

    static void AddVinylRecord(IVinylRecordService service)
    {
        Console.Write("Назва платівки: ");
        string? name = Console.ReadLine();

        Console.Write("Виконавець: ");
        string? artistName = Console.ReadLine();
        var artist = service.GetOrCreateArtist(artistName ?? string.Empty);

        Console.Write("Жанр: ");
        string? genreName = Console.ReadLine();
        var genre = service.GetOrCreateGenre(genreName ?? string.Empty);

        Console.Write("Кількість треків: ");
        if (!int.TryParse(Console.ReadLine(), out int trackCount))
        {
            Console.WriteLine("Невірний формат кількості треків.");
            return;
        }

        Console.Write("Рік виходу: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Невірний формат року виходу.");
            return;
        }

        Console.Write("Собівартість: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal cost))
        {
            Console.WriteLine("Невірний формат собівартості.");
            return;
        }

        Console.Write("Ціна для продажу: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Невірний формат ціни для продажу.");
            return;
        }

        var record = new VinylRecord
        {
            Name = name ?? string.Empty,
            ArtistId = artist.Id,
            GenreId = genre.Id,
            Tracks = trackCount,
            Year = year,
            SalePrice = cost,
            SellingPrice = price,
            Stock = 10
        };

        service.AddVinylRecord(record);
        Console.WriteLine("Платівку додано.");
    }

    static void DeleteVinylRecord(IVinylRecordService service)
    {
        Console.Write("Введіть ID платівки для видалення: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний формат ID.");
            return;
        }

        service.DeleteVinylRecord(id);
        Console.WriteLine("Платівку видалено.");
    }

    static void UpdateVinylRecord(IVinylRecordService service)
    {
        Console.Write("Введіть ID платівки для редагування: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний формат ID.");
            return;
        }

        var record = service.GetVinylRecordById(id);
        if (record == null)
        {
            Console.WriteLine("Платівку не знайдено.");
            return;
        }

        Console.Write($"Нова назва ({record.Name}): ");
        string? name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name)) record.Name = name;

        Console.Write($"Нова ціна ({record.SellingPrice}): ");
        string? priceInput = Console.ReadLine();
        if (decimal.TryParse(priceInput, out decimal newPrice)) record.SellingPrice = newPrice;

        service.UpdateVinylRecord(record);
        Console.WriteLine("Платівку оновлено.");
    }

    static void SellVinylRecord(IVinylRecordService service)
    {
        Console.Write("ID платівки: ");
        if (!int.TryParse(Console.ReadLine(), out int vinylId))
        {
            Console.WriteLine("Невірний формат ID платівки.");
            return;
        }

        Console.Write("Кількість: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Невірний формат кількості.");
            return;
        }

        Console.Write("ID покупця: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Невірний формат ID покупця.");
            return;
        }

        service.SellVinylRecord(vinylId, quantity, customerId);
        Console.WriteLine("Продаж здійснено.");
    }

    static void WriteOffVinylRecord(IVinylRecordService service)
    {
        Console.Write("ID платівки для списання: ");
        if (!int.TryParse(Console.ReadLine(), out int vinylId))
        {
            Console.WriteLine("Невірний формат ID платівки.");
            return;
        }

        service.WriteOffVinylRecord(vinylId);
        Console.WriteLine("Платівку списано.");
    }

    static void AddVinylRecordToPromotion(IVinylRecordService service)
    {
        Console.Write("ID платівки: ");
        if (!int.TryParse(Console.ReadLine(), out int vinylId))
        {
            Console.WriteLine("Невірний формат ID платівки.");
            return;
        }

        Console.Write("ID акції: ");
        if (!int.TryParse(Console.ReadLine(), out int promotionId))
        {
            Console.WriteLine("Невірний формат ID акції.");
            return;
        }

        service.AddVinylRecordToPromotion(vinylId, promotionId);
        Console.WriteLine("Платівку додано в акцію.");
    }

    static void ReserveVinylRecord(IVinylRecordService service)
    {
        Console.Write("ID платівки: ");
        if (!int.TryParse(Console.ReadLine(), out int vinylId))
        {
            Console.WriteLine("Невірний формат ID платівки.");
            return;
        }

        Console.Write("ID покупця: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Невірний формат ID покупця.");
            return;
        }

        service.ReserveVinylRecord(vinylId, customerId);
        Console.WriteLine("Платівку зарезервовано.");
    }

    static void SearchVinylRecords(IVinylRecordService service)
    {
        Console.Write("Назва платівки (або Enter, щоб пропустити): ");
        string? name = Console.ReadLine();

        Console.Write("Виконавець (або Enter, щоб пропустити): ");
        string? artist = Console.ReadLine();

        Console.Write("Жанр (або Enter, щоб пропустити): ");
        string? genre = Console.ReadLine();

        var results = service.SearchVinylRecords(name ?? string.Empty, artist ?? string.Empty, genre ?? string.Empty);
        if (results == null || !results.Any())
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        foreach (var record in results)
        {
            Console.WriteLine($"{record.Id}. {record.Name} - {record.Artist?.Name ?? "Unknown Artist"} ({record.Genre?.Name ?? "Unknown Genre"}) - {record.SellingPrice} грн");
        }
    }
}