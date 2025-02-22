using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class PromotionController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public PromotionController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddPromotion(AddPromotionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.DiscountPercentage <= 0 || dto.DiscountPercentage > 100)
            {
                Console.WriteLine("Error: Invalid promotion details.");
                return;
            }

            try
            {
                var vinylRecords = _context.VinylRecords.Where(vr => dto.VinylRecordIds.Contains(vr.Id)).ToList();
                if (vinylRecords.Count != dto.VinylRecordIds.Count)
                {
                    Console.WriteLine("Error: One or more vinyl records not found.");
                    return;
                }

                var promotion = _mapper.Map<Promotion>(dto);
                promotion.VinylRecords = vinylRecords;

                _context.Promotions.Add(promotion);
                _context.SaveChanges();

                Console.WriteLine($"Promotion '{dto.Name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding promotion: {ex.Message}");
            }
        }

        public void UpdatePromotion(int id, UpdatePromotionDto dto)
        {
            try
            {
                var promotion = _context.Promotions
                    .Include(p => p.VinylRecords)
                    .FirstOrDefault(p => p.Id == id);

                if (promotion == null)
                {
                    Console.WriteLine($"Error: Promotion with ID {id} not found.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(dto.Name) || dto.DiscountPercentage <= 0 || dto.DiscountPercentage > 100)
                {
                    Console.WriteLine("Error: Invalid promotion details.");
                    return;
                }

                var vinylRecords = _context.VinylRecords.Where(vr => dto.VinylRecordIds.Contains(vr.Id)).ToList();
                if (vinylRecords.Count != dto.VinylRecordIds.Count)
                {
                    Console.WriteLine("Error: One or more vinyl records not found.");
                    return;
                }

                _mapper.Map(dto, promotion); // Мапимо оновлення в об'єкт акції
                promotion.VinylRecords = vinylRecords;

                _context.Promotions.Update(promotion);
                _context.SaveChanges();

                Console.WriteLine($"Promotion '{promotion.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating promotion: {ex.Message}");
            }
        }
    }
}
