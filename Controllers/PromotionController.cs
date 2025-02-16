using Microsoft.EntityFrameworkCore;
using MusicStore.Data;


namespace MusicStore.Controllers
{
    public class PromotionController
    {
        private readonly MusicStoreDbContext _context;

        public PromotionController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddPromotion(string name, decimal discountPercentage, DateTime startDate, DateTime endDate, List<int> vinylRecordIds)
        {
            if (string.IsNullOrWhiteSpace(name) || discountPercentage <= 0 || discountPercentage > 100)
            {
                Console.WriteLine("Error: Invalid promotion details.");
                return;
            }

            try
            {
                var vinylRecords = _context.VinylRecords.Where(vr => vinylRecordIds.Contains(vr.Id)).ToList();
                if (vinylRecords.Count != vinylRecordIds.Count)
                {
                    Console.WriteLine("Error: One or more vinyl records not found.");
                    return;
                }

                var promotion = new Promotion
                {
                    Name = name,
                    DiscountPercentage = discountPercentage,
                    StartDate = startDate,
                    EndDate = endDate,
                    VinylRecords = vinylRecords
                };

                _context.Promotions.Add(promotion);
                _context.SaveChanges();

                Console.WriteLine($"Promotion '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding promotion: {ex.Message}");
            }
        }

        public void ListPromotions()
        {
            var promotions = _context.Promotions.ToList();
            if (!promotions.Any())
            {
                Console.WriteLine("No promotions found.");
                return;
            }

            Console.WriteLine("Promotions List:");
            foreach (var promotion in promotions)
            {
                Console.WriteLine($"Id: {promotion.Id}, Name: {promotion.Name}, Discount: {promotion.DiscountPercentage}%, Start Date: {promotion.StartDate.ToShortDateString()}, End Date: {promotion.EndDate.ToShortDateString()}");
            }
        }

        public void GetPromotionById(int id)
        {
            var promotion = _context.Promotions
                .Include(p => p.VinylRecords)
                .FirstOrDefault(p => p.Id == id);

            if (promotion == null)
            {
                Console.WriteLine($"Promotion with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {promotion.Id}, Name: {promotion.Name}, Discount: {promotion.DiscountPercentage}%, Start Date: {promotion.StartDate.ToShortDateString()}, End Date: {promotion.EndDate.ToShortDateString()}");
            Console.WriteLine("Vinyl Records included in the promotion:");
            foreach (var vinylRecord in promotion.VinylRecords)
            {
                Console.WriteLine($"Vinyl Record: {vinylRecord.Name}");
            }
        }

        public void DeletePromotion(int id)
        {
            try
            {
                var promotion = _context.Promotions.Find(id);
                if (promotion == null)
                {
                    Console.WriteLine($"Error: Promotion with ID {id} not found.");
                    return;
                }

                _context.Promotions.Remove(promotion);
                _context.SaveChanges();
                Console.WriteLine($"Promotion '{promotion.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting promotion: {ex.Message}");
            }
        }

        public void UpdatePromotion(int id, string name, decimal discountPercentage, DateTime startDate, DateTime endDate, List<int> vinylRecordIds)
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

                if (string.IsNullOrWhiteSpace(name) || discountPercentage <= 0 || discountPercentage > 100)
                {
                    Console.WriteLine("Error: Invalid promotion details.");
                    return;
                }

                var vinylRecords = _context.VinylRecords.Where(vr => vinylRecordIds.Contains(vr.Id)).ToList();
                if (vinylRecords.Count != vinylRecordIds.Count)
                {
                    Console.WriteLine("Error: One or more vinyl records not found.");
                    return;
                }

                promotion.Name = name;
                promotion.DiscountPercentage = discountPercentage;
                promotion.StartDate = startDate;
                promotion.EndDate = endDate;
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
