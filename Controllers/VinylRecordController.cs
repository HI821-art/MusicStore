using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class VinylRecordController
    {
        private readonly MusicStoreDbContext _context;

        public VinylRecordController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddVinylRecord(string name, int year, int tracks, decimal salePrice, DateTime releaseDate, int stock, int artistId, int publisherId, int genreId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Vinyl record name cannot be empty.");
                return;
            }

            try
            {
                var artist = _context.Artists.Find(artistId);
                var publisher = _context.Publishers.Find(publisherId);
                var genre = _context.Genres.Find(genreId);

                if (artist == null || publisher == null || genre == null)
                {
                    Console.WriteLine("Error: Invalid artist, publisher or genre.");
                    return;
                }

                var vinylRecord = new VinylRecord
                {
                    Name = name,
                    Year = year,
                    Tracks = tracks,
                    SalePrice = salePrice,
                    ReleaseDate = releaseDate,
                    Stock = stock,
                    ArtistId = artistId,
                    PublisherId = publisherId,
                    GenreId = genreId,
                    Artist = artist,
                    Publisher = publisher,
                    Genre = genre
                };

                _context.VinylRecords.Add(vinylRecord);
                _context.SaveChanges();

                Console.WriteLine($"Vinyl record '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding vinyl record: {ex.Message}");
            }
        }

        public void ListVinylRecords()
        {
            var vinylRecords = _context.VinylRecords.ToList();
            if (vinylRecords.Count == 0)
            {
                Console.WriteLine("No vinyl records found.");
                return;
            }

            Console.WriteLine("Vinyl Records List:");
            foreach (var vinylRecord in vinylRecords)
            {
                Console.WriteLine($"Id: {vinylRecord.Id}, Name: {vinylRecord.Name}, Artist: {vinylRecord.Artist.Name}, Genre: {vinylRecord.Genre.Name}, Price: {vinylRecord.SalePrice:C}");
            }
        }

        public void GetVinylRecordById(int id)
        {
            var vinylRecord = _context.VinylRecords.Find(id);
            if (vinylRecord == null)
            {
                Console.WriteLine($"Vinyl record with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {vinylRecord.Id}, Name: {vinylRecord.Name}, Artist: {vinylRecord.Artist.Name}, Genre: {vinylRecord.Genre.Name}, Price: {vinylRecord.SalePrice:C}, Year: {vinylRecord.Year}, Stock: {vinylRecord.Stock}");
        }

        public void DeleteVinylRecord(int id)
        {
            try
            {
                var vinylRecord = _context.VinylRecords.Find(id);
                if (vinylRecord == null)
                {
                    Console.WriteLine($"Error: Vinyl record with ID {id} not found.");
                    return;
                }

                _context.VinylRecords.Remove(vinylRecord);
                _context.SaveChanges();
                Console.WriteLine($"Vinyl record '{vinylRecord.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting vinyl record: {ex.Message}");
            }
        }

        public void UpdateVinylRecord(int id, string name, int year, int tracks, decimal salePrice, DateTime releaseDate, int stock, int artistId, int publisherId, int genreId)
        {
            try
            {
                var vinylRecord = _context.VinylRecords.Find(id);
                if (vinylRecord == null)
                {
                    Console.WriteLine($"Error: Vinyl record with ID {id} not found.");
                    return;
                }

                var artist = _context.Artists.Find(artistId);
                var publisher = _context.Publishers.Find(publisherId);
                var genre = _context.Genres.Find(genreId);

                if (artist == null || publisher == null || genre == null)
                {
                    Console.WriteLine("Error: Invalid artist, publisher or genre.");
                    return;
                }

                vinylRecord.Name = name;
                vinylRecord.Year = year;
                vinylRecord.Tracks = tracks;
                vinylRecord.SalePrice = salePrice;
                vinylRecord.ReleaseDate = releaseDate;
                vinylRecord.Stock = stock;
                vinylRecord.ArtistId = artistId;
                vinylRecord.PublisherId = publisherId;
                vinylRecord.GenreId = genreId;
                vinylRecord.Artist = artist;
                vinylRecord.Publisher = publisher;
                vinylRecord.Genre = genre;

                _context.SaveChanges();

                Console.WriteLine($"Vinyl record '{name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating vinyl record: {ex.Message}");
            }
        }
    }
}
