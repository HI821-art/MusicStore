
namespace MusicStore.Services
{
    public interface IVinylRecordService
    {
        IEnumerable<VinylRecord> GetAllVinylRecords();
        VinylRecord GetVinylRecordById(int id);
        void AddVinylRecord(VinylRecord vinylRecord);
        void UpdateVinylRecord(VinylRecord vinylRecord);
        void DeleteVinylRecord(int id);
        void SellVinylRecord(int vinylId, int quantity, int customerId);

        void WriteOffVinylRecord(int vinylId);
        void AddVinylRecordToPromotion(int vinylId, int promotionId);
        void ReserveVinylRecord(int vinylId, int customerId);
        IEnumerable<VinylRecord> SearchVinylRecords(string name, string artist, string genre);

        
        Artist GetOrCreateArtist(string artistName);
        Genre GetOrCreateGenre(string genreName);
       
          
        

    }
}
