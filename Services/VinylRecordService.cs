using Microsoft.EntityFrameworkCore;
using MusicStore.Services;

public class VinylRecordService : IVinylRecordService
{
    private readonly IRepository<VinylRecord> _vinylRecordRepository;
    private readonly IRepository<Artist> _artistRepository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly IRepository<Promotion> _promotionRepository;
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderDetail> _orderDetailRepository;
    private readonly IRepository<Sale> _saleRepository;
    private readonly DbContext _context; // Add this line

    public VinylRecordService(
        IRepository<VinylRecord> vinylRecordRepository,
        IRepository<Artist> artistRepository,
        IRepository<Genre> genreRepository,
        IRepository<Promotion> promotionRepository,
        IRepository<Reservation> reservationRepository,
        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IRepository<Sale> saleRepository,
        DbContext context) // Add this parameter
    {
        _vinylRecordRepository = vinylRecordRepository;
        _artistRepository = artistRepository;
        _genreRepository = genreRepository;
        _promotionRepository = promotionRepository;
        _reservationRepository = reservationRepository;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _saleRepository = saleRepository;
        _context = context; // Initialize the context
    }

    public IEnumerable<VinylRecord> GetAllVinylRecords()
    {
        return _context.Set<VinylRecord>()
                      .Include(v => v.Artist)
                       .Include(v => v.Genre)
                       .ToList();
    }

    public VinylRecord GetVinylRecordById(int id)
    {
        return _vinylRecordRepository.GetByIdAsync(id).Result;
    }

    public void AddVinylRecord(VinylRecord vinylRecord)
    {
        _vinylRecordRepository.AddAsync(vinylRecord).Wait();
    }

    public void UpdateVinylRecord(VinylRecord vinylRecord)
    {
        _vinylRecordRepository.UpdateAsync(vinylRecord).Wait();
    }

    public void DeleteVinylRecord(int id)
    {
        _vinylRecordRepository.DeleteAsync(id).Wait();
    }

    public void SellVinylRecord(int vinylId, int quantity, int customerId)
    {
        var vinyl = _vinylRecordRepository.GetByIdAsync(vinylId).Result;

        if (vinyl == null || vinyl.Stock < quantity)
            throw new InvalidOperationException("Недостатньо товару на складі!");

        vinyl.Stock -= quantity;
        vinyl.Sales += quantity;

        var sale = new Sale
        {
            VinylRecordId = vinylId,
            SaleDate = DateTime.UtcNow,
            Quantity = quantity,
            CustomerId = customerId
        };

        _saleRepository.AddAsync(sale).Wait();
    }

    public void WriteOffVinylRecord(int vinylId)
    {
        var record = _vinylRecordRepository.GetByIdAsync(vinylId).Result;

        if (record != null)
        {
            record.Stock = 0;
            _vinylRecordRepository.UpdateAsync(record).Wait();
        }
    }

    public void AddVinylRecordToPromotion(int vinylId, int promotionId)
    {
        var promotion = _promotionRepository.GetByIdAsync(promotionId).Result;
        if (promotion != null)
        {
            var vinylRecord = _vinylRecordRepository.GetByIdAsync(vinylId).Result;
            promotion.VinylRecords.Add(vinylRecord);
            _promotionRepository.UpdateAsync(promotion).Wait();
        }
    }

    public void ReserveVinylRecord(int vinylId, int customerId)
    {
        var reservation = new Reservation { VinylRecordId = vinylId, CustomerId = customerId, ReservedAt = DateTime.UtcNow };
        _reservationRepository.AddAsync(reservation).Wait();
    }

    public IEnumerable<VinylRecord> SearchVinylRecords(string name, string artist, string genre)
    {
        var query = _vinylRecordRepository.Query();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(v => v.Name.Contains(name));

        if (!string.IsNullOrEmpty(artist))
            query = query.Where(v => v.Artist.Name.Contains(artist));

        if (!string.IsNullOrEmpty(genre))
            query = query.Where(v => v.Genre.Name.Contains(genre));

        return query.ToList();
    }

    public Artist GetOrCreateArtist(string artistName)
    {
        var artist = _artistRepository.Query().FirstOrDefault(a => a.Name == artistName);
        if (artist == null)
        {
            artist = new Artist { Name = artistName };
            _artistRepository.AddAsync(artist).Wait();
        }
        return artist;
    }

    public Genre GetOrCreateGenre(string genreName)
    {
        var genre = _genreRepository.Query().FirstOrDefault(g => g.Name == genreName);
        if (genre == null)
        {
            genre = new Genre { Name = genreName };
            _genreRepository.AddAsync(genre).Wait();
        }
        return genre;
    }
}
