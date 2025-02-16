namespace MusicStore.Services
{
    public interface IPopularityService
    {
        List<VinylRecord> GetMostPopularVinylRecords(DateTime startDate, DateTime endDate);
        List<GenrePopularity> GetMostPopularGenres(DateTime startDate, DateTime endDate);
        List<ArtistPopularity> GetMostPopularArtists(DateTime startDate, DateTime endDate);
    }
}
