namespace MusicStore.Services
{
    public interface IVinylRecordService
    {
  
       
      Task SellVinylRecord(int vinylId, int quantity, int customerId);
        Task WriteOffVinylRecord(int vinylId);
      Task AddVinylRecordToPromotion(int vinylId, int promotionId);
        Task ReserveVinylRecord(int vinylId, int customerId);
        Task<IEnumerable<VinylRecord>> GetAllVinylRecords();
        Task<VinylRecord> GetVinylRecordById(int id);
        Task AddVinylRecord(VinylRecord record);
        Task DeleteVinylRecord(int id);
        Task UpdateVinylRecord(VinylRecord record);
        Task<IEnumerable<VinylRecord>> SearchVinylRecords(string name, string artist, string genre);
        Task<Artist> GetOrCreateArtist(string artistName);
        Task<Genre> GetOrCreateGenre(string genreName);
    }
}
