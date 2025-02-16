using Microsoft.Extensions.Logging;

namespace MusicStore.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IRepository<Discount> discountRepository, ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all discounts.");
                return await _discountRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all discounts.");
                throw new Exception("Error fetching all discounts.", ex);
            }
        }

        public async Task<Discount> GetDiscountByIdAsync(int id)
        {
            try
            {
                var discount = await _discountRepository.GetByIdAsync(id);
                if (discount == null)
                {
                    _logger.LogWarning($"Discount with ID {id} not found.");
                    throw new KeyNotFoundException($"Discount with ID {id} not found.");
                }
                return discount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching discount with ID {id}.");
                throw new Exception("Error fetching discount by ID.", ex);
            }
        }

        public async Task AddDiscountAsync(Discount discount)
        {
            try
            {
                _logger.LogInformation("Adding new discount.");
                await _discountRepository.AddAsync(discount);
                _logger.LogInformation("Discount added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new discount.");
                throw new Exception("Error adding discount.", ex);
            }
        }

        public async Task UpdateDiscountAsync(Discount discount)
        {
            try
            {
                _logger.LogInformation($"Updating discount with ID {discount.Id}.");
                await _discountRepository.UpdateAsync(discount);
                _logger.LogInformation("Discount updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating discount with ID {discount.Id}.");
                throw new Exception("Error updating discount.", ex);
            }
        }

        public async Task DeleteDiscountAsync(int id)
        {
            try
            {
                var discount = await _discountRepository.GetByIdAsync(id);
                if (discount == null)
                {
                    _logger.LogWarning($"Discount with ID {id} not found for deletion.");
                    throw new KeyNotFoundException($"Discount with ID {id} not found.");
                }

                _logger.LogInformation($"Deleting discount with ID {id}.");
                await _discountRepository.DeleteAsync(id);
                _logger.LogInformation("Discount deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting discount with ID {id}.");
                throw new Exception("Error deleting discount.", ex);
            }
        }

        public IEnumerable<Discount> GetAllDiscounts()
        {
            throw new NotImplementedException();
        }

        public Discount GetDiscountById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
