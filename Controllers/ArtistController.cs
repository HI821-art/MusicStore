using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class ArtistController
    {
        private readonly MusicStoreDbContext _context;

        public ArtistController(MusicStoreDbContext context)
        {
            _context = context;
        }

        // Метод для додавання артиста
        public void AddArtist(string name, string bio, string genreName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Artist name cannot be empty.");
                return;
            }

            try
            {
                // Знайти жанр за назвою
                var genre = _context.Genres.FirstOrDefault(g => g.Name == genreName);
                if (genre == null)
                {
                    Console.WriteLine($"Error: Genre '{genreName}' not found.");
                    return;
                }

                // Створити нового артиста
                var artist = new Artist
                {
                    Name = name,
                    Bio = bio,
                   
                };

                // Додаємо артиста до контексту
                _context.Artists.Add(artist);
                _context.SaveChanges();

                Console.WriteLine($"Artist '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artist: {ex.Message}");
            }
        }

        // Метод для переліку всіх артистів
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

        // Метод для видалення артиста
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
    }
}
