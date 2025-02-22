using AutoMapper;
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
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public VinylRecordService(
        IRepository<VinylRecord> vinylRecordRepository,
        IRepository<Artist> artistRepository,
        IRepository<Genre> genreRepository,
        IRepository<Promotion> promotionRepository,
        IRepository<Reservation> reservationRepository,
        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IRepository<Sale> saleRepository,
        DbContext context,
        IMapper mapper)
    {
        _vinylRecordRepository = vinylRecordRepository;
        _artistRepository = artistRepository;
        _genreRepository = genreRepository;
        _promotionRepository = promotionRepository;
        _reservationRepository = reservationRepository;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _saleRepository = saleRepository;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VinylRecord>> GetAllVinylRecords()
    {
        return await _context.Set<VinylRecord>()
                             .Include(v => v.Artist)
                             .Include(v => v.Genre)
                             .ToListAsync();
    }

    public async Task<VinylRecord> GetVinylRecordById(int id)
    {
        return await _vinylRecordRepository.GetByIdAsync(id);
    }

    

    public async Task UpdateVinylRecord(VinylRecordDto vinylRecordDto)
    {
        var vinylRecord = await _vinylRecordRepository.GetByIdAsync(vinylRecordDto.Id);
        if (vinylRecord != null)
        {
            _mapper.Map(vinylRecordDto, vinylRecord);
            await _vinylRecordRepository.UpdateAsync(vinylRecord);
        }
    }

    public async Task DeleteVinylRecord(int id)
    {
        await _vinylRecordRepository.DeleteAsync(id);
    }

    public async Task SellVinylRecord(int vinylId, int quantity, int customerId)
    {
        var vinyl = await _vinylRecordRepository.GetByIdAsync(vinylId);

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

        await _saleRepository.AddAsync(sale);
    }

    public async Task WriteOffVinylRecord(int vinylId)
    {
        var record = await _vinylRecordRepository.GetByIdAsync(vinylId);

        if (record != null)
        {
            record.Stock = 0;
            await _vinylRecordRepository.UpdateAsync(record);
        }
    }

    public async Task AddVinylRecordToPromotion(int vinylId, int promotionId)
    {
        var promotion = await _promotionRepository.GetByIdAsync(promotionId);
        if (promotion != null)
        {
            var vinylRecord = await _vinylRecordRepository.GetByIdAsync(vinylId);
            promotion.VinylRecords.Add(vinylRecord);
            await _promotionRepository.UpdateAsync(promotion);
        }
    }

    public async Task ReserveVinylRecord(int vinylId, int customerId)
    {
        var reservation = new Reservation { VinylRecordId = vinylId, CustomerId = customerId, ReservedAt = DateTime.UtcNow };
        await _reservationRepository.AddAsync(reservation);
    }

    public async Task<IEnumerable<VinylRecord>> SearchVinylRecords(string name, string artist, string genre)
    {
        var query = _vinylRecordRepository.Query();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(v => v.Name.Contains(name));

        if (!string.IsNullOrEmpty(artist))
            query = query.Where(v => v.Artist.Name.Contains(artist));

        if (!string.IsNullOrEmpty(genre))
            query = query.Where(v => v.Genre.Name.Contains(genre));

        return await query.ToListAsync();
    }

    public async Task<Artist> GetOrCreateArtist(string artistName)
    {
        var artist = await _artistRepository.Query().FirstOrDefaultAsync(a => a.Name == artistName);
        if (artist == null)
        {
            artist = new Artist { Name = artistName };
            await _artistRepository.AddAsync(artist);
        }
        return artist;
    }

    public async Task<Genre> GetOrCreateGenre(string genreName)
    {
        var genre = await _genreRepository.Query().FirstOrDefaultAsync(g => g.Name == genreName);
        if (genre == null)
        {
            genre = new Genre { Name = genreName };
            await _genreRepository.AddAsync(genre);
        }
        return genre;
    }

    public Task AddVinylRecord(VinylRecord vinylRecord)
    {
        throw new NotImplementedException();
    }

    public Task UpdateVinylRecord(VinylRecord vinylRecord)
    {
        throw new NotImplementedException();
    }
}
