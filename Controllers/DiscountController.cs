using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class DiscountController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public DiscountController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddDiscount(AddDiscountDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                Console.WriteLine("Error: Discount name cannot be empty.");
                return;
            }

            if (dto.Percentage <= 0 || dto.Percentage > 100)
            {
                Console.WriteLine("Error: Discount percentage must be between 0 and 100.");
                return;
            }

            if (dto.StartDate >= dto.EndDate)
            {
                Console.WriteLine("Error: Start date must be earlier than end date.");
                return;
            }

            try
            {
                if (_context.Discounts.Any(d => d.Name == dto.Name))
                {
                    Console.WriteLine($"Error: Discount with the name '{dto.Name}' already exists.");
                    return;
                }

                var discount = _mapper.Map<Discount>(dto);

                _context.Discounts.Add(discount);
                _context.SaveChanges();

                Console.WriteLine($"Discount '{dto.Name}' added successfully.");
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

        public void UpdateDiscount(int id, UpdateDiscountDto dto)
        {
            try
            {
                var discount = _context.Discounts.Find(id);
                if (discount == null)
                {
                    Console.WriteLine($"Error: Discount with ID {id} not found.");
                    return;
                }

                _mapper.Map(dto, discount);

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
