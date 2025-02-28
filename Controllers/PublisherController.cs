﻿using AutoMapper;
using MusicStore.Data;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class PublisherController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public PublisherController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddPublisher(AddPublisherDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                Console.WriteLine("Error: Publisher name cannot be empty.");
                return;
            }

            try
            {
                var publisher = _mapper.Map<Publisher>(dto);

                _context.Publishers.Add(publisher);
                _context.SaveChanges();

                Console.WriteLine($"Publisher '{dto.Name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding publisher: {ex.Message}");
            }
        }

        public void ListPublishers()
        {
            var publishers = _context.Publishers.ToList();
            if (!publishers.Any())
            {
                Console.WriteLine("No publishers found.");
                return;
            }

            Console.WriteLine("Publishers List:");
            foreach (var publisher in publishers)
            {
                Console.WriteLine($"Id: {publisher.Id}, Name: {publisher.Name}");
            }
        }

        public void GetPublisherById(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher == null)
            {
                Console.WriteLine($"Publisher with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {publisher.Id}, Name: {publisher.Name}");
        }

        public void DeletePublisher(int id)
        {
            try
            {
                var publisher = _context.Publishers.Find(id);
                if (publisher == null)
                {
                    Console.WriteLine($"Error: Publisher with ID {id} not found.");
                    return;
                }

                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
                Console.WriteLine($"Publisher '{publisher.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting publisher: {ex.Message}");
            }
        }

        public void UpdatePublisher(int id, UpdatePublisherDto dto)
        {
            try
            {
                var publisher = _context.Publishers.Find(id);
                if (publisher == null)
                {
                    Console.WriteLine($"Error: Publisher with ID {id} not found.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    Console.WriteLine("Error: Publisher name cannot be empty.");
                    return;
                }

                _mapper.Map(dto, publisher);

                _context.Publishers.Update(publisher);
                _context.SaveChanges();

                Console.WriteLine($"Publisher '{publisher.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating publisher: {ex.Message}");
            }
        }
    }
}
