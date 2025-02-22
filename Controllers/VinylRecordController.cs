using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;    

namespace MusicStore.Controllers
{
    public class VinylRecordController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public VinylRecordController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddVinylRecord(VinylRecordDto vinylRecordCreateDto)
        {
            if (string.IsNullOrWhiteSpace(vinylRecordCreateDto.Name))
            {
                Console.WriteLine("Error: Vinyl record name cannot be empty.");
                return;
            }

            try
            {
                var artist = _context.Artists.Find(vinylRecordCreateDto.ArtistId);
                var publisher = _context.Publishers.Find(vinylRecordCreateDto.PublisherId);
                var genre = _context.Genres.Find(vinylRecordCreateDto.GenreId);

                if (artist == null || publisher == null || genre == null)
                {
                    Console.WriteLine("Error: Invalid artist, publisher, or genre.");
                    return;
                }

                var vinylRecord = _mapper.Map<VinylRecord>(vinylRecordCreateDto);  

                vinylRecord.Artist = artist;
                vinylRecord.Publisher = publisher;
                vinylRecord.Genre = genre;

                _context.VinylRecords.Add(vinylRecord);
                _context.SaveChanges();

                Console.WriteLine($"Vinyl record '{vinylRecord.Name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding vinyl record: {ex.Message}");
            }
        }

        public void ListVinylRecords()
        {
            var vinylRecords = _context.VinylRecords.Include(v => v.Artist).Include(v => v.Genre).Include(v => v.Publisher).ToList();
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
            var vinylRecord = _context.VinylRecords.Include(v => v.Artist).Include(v => v.Genre).Include(v => v.Publisher).FirstOrDefault(v => v.Id == id);
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

        public void UpdateVinylRecord(int id, VinylRecordDto vinylRecordDto)
        {
            try
            {
                var vinylRecord = _context.VinylRecords.Find(id);
                if (vinylRecord == null)
                {
                    Console.WriteLine($"Error: Vinyl record with ID {id} not found.");
                    return;
                }

                var artist = _context.Artists.Find(vinylRecordDto.ArtistId);
                var publisher = _context.Publishers.Find(vinylRecordDto.PublisherId);
                var genre = _context.Genres.Find(vinylRecordDto.GenreId);

                if (artist == null || publisher == null || genre == null)
                {
                    Console.WriteLine("Error: Invalid artist, publisher or genre.");
                    return;
                }

                _mapper.Map(vinylRecordDto, vinylRecord);  

                _context.SaveChanges();

                Console.WriteLine($"Vinyl record '{vinylRecordDto.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating vinyl record: {ex.Message}");
            }
        }
    }
}
