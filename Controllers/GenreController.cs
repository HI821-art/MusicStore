using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class GenreController
    {
        private readonly MusicStoreDbContext _context;

        public GenreController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddGenre(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Genre name cannot be empty.");
                return;
            }

            try
            {
                if (_context.Genres.Any(g => g.Name == name))
                {
                    Console.WriteLine($"Error: Genre '{name}' already exists.");
                    return;
                }

                var genre = new Genre
                {
                    Name = name
                };

                _context.Genres.Add(genre);
                _context.SaveChanges();

                Console.WriteLine($"Genre '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding genre: {ex.Message}");
            }
        }

        public void ListGenres()
        {
            var genres = _context.Genres.ToList();
            if (!genres.Any())
            {
                Console.WriteLine("No genres found.");
                return;
            }

            Console.WriteLine("Genres List:");
            foreach (var genre in genres)
            {
                Console.WriteLine($"Id: {genre.Id}, Name: {genre.Name}");
            }
        }

        public void GetGenreById(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre == null)
            {
                Console.WriteLine($"Genre with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {genre.Id}, Name: {genre.Name}");
        }

        public void DeleteGenre(int id)
        {
            try
            {
                var genre = _context.Genres.Find(id);
                if (genre == null)
                {
                    Console.WriteLine($"Error: Genre with ID {id} not found.");
                    return;
                }

                _context.Genres.Remove(genre);
                _context.SaveChanges();
                Console.WriteLine($"Genre '{genre.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting genre: {ex.Message}");
            }
        }

        public void UpdateGenre(int id, string name)
        {
            try
            {
                var genre = _context.Genres.Find(id);
                if (genre == null)
                {
                    Console.WriteLine($"Error: Genre with ID {id} not found.");
                    return;
                }

                genre.Name = name;

                _context.Genres.Update(genre);
                _context.SaveChanges();

                Console.WriteLine($"Genre '{genre.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating genre: {ex.Message}");
            }
        }
    }
}
