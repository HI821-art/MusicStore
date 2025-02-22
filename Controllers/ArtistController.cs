using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class ArtistController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public ArtistController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddArtist(AddArtistDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                Console.WriteLine("Error: Artist name cannot be empty.");
                return;
            }

            try
            {
                var genre = _context.Genres.FirstOrDefault(g => g.Name == dto.GenreName);
                if (genre == null)
                {
                    Console.WriteLine($"Error: Genre '{dto.GenreName}' not found.");
                    return;
                }

                var artist = _mapper.Map<Artist>(dto);
                _context.Artists.Add(artist);
                _context.SaveChanges();

                Console.WriteLine($"Artist '{dto.Name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artist: {ex.Message}");
            }
        }

        public void ListArtists()
        {
            var artists = _context.Artists.ToList();
            if (!artists.Any())
            {
                Console.WriteLine("No artists found.");
                return;
            }

            Console.WriteLine("Artists List:");
            foreach (var artist in artists)
            {
                Console.WriteLine($"Id: {artist.Id}, Name: {artist.Name}");
            }
        }

        public void GetArtistById(int id)
        {
            var artist = _context.Artists.Find(id);
            if (artist == null)
            {
                Console.WriteLine($"Artist with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {artist.Id}, Name: {artist.Name}, Bio: {artist.Bio}");
        }

        public void DeleteArtist(int id)
        {
            try
            {
                var artist = _context.Artists.Find(id);
                if (artist == null)
                {
                    Console.WriteLine($"Error: Artist with ID {id} not found.");
                    return;
                }

                _context.Artists.Remove(artist);
                _context.SaveChanges();
                Console.WriteLine($"Artist '{artist.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting artist: {ex.Message}");
            }
        }

        public void UpdateArtist(int id, UpdateArtistDto dto)
        {
            try
            {
                var artist = _context.Artists.Find(id);
                if (artist == null)
                {
                    Console.WriteLine($"Error: Artist with ID {id} not found.");
                    return;
                }

                _mapper.Map(dto, artist);

                _context.Artists.Update(artist);
                _context.SaveChanges();

                Console.WriteLine($"Artist '{artist.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating artist: {ex.Message}");
            }
        }
    }
}
