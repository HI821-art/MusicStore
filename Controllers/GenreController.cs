using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class GenreController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddGenre(AddGenreDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                Console.WriteLine("Error: Genre name cannot be empty.");
                return;
            }

            try
            {
                if (_context.Genres.Any(g => g.Name == dto.Name))
                {
                    Console.WriteLine($"Error: Genre '{dto.Name}' already exists.");
                    return;
                }

                var genre = _mapper.Map<Genre>(dto);

                _context.Genres.Add(genre);
                _context.SaveChanges();

                Console.WriteLine($"Genre '{dto.Name}' added successfully.");
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

        public void UpdateGenre(int id, UpdateGenreDto dto)
        {
            try
            {
                var genre = _context.Genres.Find(id);
                if (genre == null)
                {
                    Console.WriteLine($"Error: Genre with ID {id} not found.");
                    return;
                }

                _mapper.Map(dto, genre);

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
