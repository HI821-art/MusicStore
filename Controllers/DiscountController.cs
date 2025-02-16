using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class DiscountController
    {
        private readonly MusicStoreDbContext _context;

        public DiscountController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddDiscount(string name, decimal percentage, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Discount name cannot be empty.");
                return;
            }

            if (percentage <= 0 || percentage > 100)
            {
                Console.WriteLine("Error: Discount percentage must be between 0 and 100.");
                return;
            }

            if (startDate >= endDate)
            {
                Console.WriteLine("Error: Start date must be earlier than end date.");
                return;
            }

            try
            {
                if (_context.Discounts.Any(d => d.Name == name))
                {
                    Console.WriteLine($"Error: Discount with the name '{name}' already exists.");
                    return;
                }

                var discount = new Discount
                {
                    Name = name,
                    Percentage = percentage,
                    StartDate = startDate,
                    EndDate = endDate
                };

                _context.Discounts.Add(discount);
                _context.SaveChanges();

                Console.WriteLine($"Discount '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding discount: {ex.Message}");
            }
        }

        public void ListDiscounts()
        {
            var discounts = _context.Discounts.ToList();
            if (!discounts.Any())
            {
                Console.WriteLine("No discounts found.");
                return;
            }

            Console.WriteLine("Discounts List:");
            foreach (var discount in discounts)
            {
                Console.WriteLine($"Id: {discount.Id}, Name: {discount.Name}, Percentage: {discount.Percentage}%, Start Date: {discount.StartDate.ToShortDateString()}, End Date: {discount.EndDate.ToShortDateString()}");
            }
        }

        public void GetDiscountById(int id)
        {
            var discount = _context.Discounts.Find(id);
            if (discount == null)
            {
                Console.WriteLine($"Discount with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {discount.Id}, Name: {discount.Name}, Percentage: {discount.Percentage}%, Start Date: {discount.StartDate.ToShortDateString()}, End Date: {discount.EndDate.ToShortDateString()}");
        }

        public void DeleteDiscount(int id)
        {
            try
            {
                var discount = _context.Discounts.Find(id);
                if (discount == null)
                {
                    Console.WriteLine($"Error: Discount with ID {id} not found.");
                    return;
                }

                _context.Discounts.Remove(discount);
                _context.SaveChanges();
                Console.WriteLine($"Discount '{discount.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting discount: {ex.Message}");
            }
        }

        public void UpdateDiscount(int id, string name, decimal percentage, DateTime startDate, DateTime endDate)
        {
            try
            {
                var discount = _context.Discounts.Find(id);
                if (discount == null)
                {
                    Console.WriteLine($"Error: Discount with ID {id} not found.");
                    return;
                }

                discount.Name = name;
                discount.Percentage = percentage;
                discount.StartDate = startDate;
                discount.EndDate = endDate;

                _context.Discounts.Update(discount);
                _context.SaveChanges();

                Console.WriteLine($"Discount '{discount.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating discount: {ex.Message}");
            }
        }
    }
}
