using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Services;

public partial class PopularityService : IPopularityService
{
    private readonly MusicStoreDbContext _context;

    public PopularityService(MusicStoreDbContext context)
    {
        _context = context;
    }


    public List<VinylRecordPopularity> GetMostPopularVinylRecords(DateTime startDate, DateTime endDate)
    {
        return _context.Sales
            .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
            .Join(_context.VinylRecords,
                sale => sale.VinylRecordId,
                record => record.Id,
                (sale, record) => new { sale, record })
            .Join(_context.Artists,
                saleRecord => saleRecord.record.ArtistId,
                artist => artist.Id,
                (saleRecord, artist) => new { saleRecord.sale, saleRecord.record, artist })
            .Join(_context.Genres,  // Додаємо з'єднання з Genres
                saleRecord => saleRecord.record.GenreId,
                genre => genre.Id,
                (saleRecord, genre) => new { saleRecord.sale, saleRecord.record, saleRecord.artist, genre })
            .GroupBy(s => new
            {
                VinylRecordId = s.record.Id,
                Name = s.record.Name,
                ArtistId = s.artist.Id,
                ArtistName = s.artist.Name,
                GenreId = s.genre.Id,
                GenreName = s.genre.Name,
                SellingPrice = s.record.SellingPrice
            })
            .Select(g => new VinylRecordPopularity
            {
                Id = g.Key.VinylRecordId,
                Name = g.Key.Name,
                ArtistId = g.Key.ArtistId,
                Artist = new Artist { Id = g.Key.ArtistId, Name = g.Key.ArtistName },
                GenreId = g.Key.GenreId,
                Genre = new Genre { Id = g.Key.GenreId, Name = g.Key.GenreName },
                SellingPrice = g.Key.SellingPrice,
                Sales = g.Sum(s => s.sale.Quantity)
            })
            .OrderByDescending(vr => vr.Sales)
            .ToList();
    }



    // Get the most popular genres in a given date range
    public List<GenrePopularity> GetMostPopularGenres(DateTime startDate, DateTime endDate)
    {
        return _context.Sales
            .Include(s => s.VinylRecord)
            .ThenInclude(vr => vr.Genre)
            .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
            .GroupBy(s => s.VinylRecord.Genre.Name)
            .Select(group => new GenrePopularity
            {
                GenreName = group.Key,
                TotalSales = group.Sum(s => s.Quantity)
            })
            .OrderByDescending(genre => genre.TotalSales)
            .Take(10)
            .ToList();
    }

    // Get the most popular artists in a given date range
    public List<ArtistPopularity> GetMostPopularArtists(DateTime startDate, DateTime endDate)
    {
        return _context.Sales
            .Join(_context.VinylRecords,
                sale => sale.VinylRecordId,
                record => record.Id,
                (sale, record) => new { sale, record })
            .Join(_context.Artists,
                saleRecord => saleRecord.record.ArtistId,
                artist => artist.Id,
                (saleRecord, artist) => new { saleRecord.sale, artist })
            .GroupBy(s => s.artist.Name)
            .Select(g => new ArtistPopularity
            {
                ArtistName = g.Key,
                TotalSales = g.Sum(s => s.sale.Quantity)
            })
            .OrderByDescending(artist => artist.TotalSales)
            .ToList();
    }

    // Get the latest vinyl records (released after a specific date)
    public List<VinylRecord> GetNewVinylRecords(DateTime startDate)
    {
        return _context.VinylRecords
            .Where(vr => vr.ReleaseDate >= startDate)
            .OrderByDescending(vr => vr.ReleaseDate)
            .ToList();
    }

    List<VinylRecord> IPopularityService.GetMostPopularVinylRecords(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}
