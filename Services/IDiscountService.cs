namespace MusicStore.Services
{
    public interface IDiscountService
    {
        IEnumerable<Discount> GetAllDiscounts();
        Discount GetDiscountById(int id);
        void AddDiscount(Discount discount);
        void UpdateDiscount(Discount discount);
        void DeleteDiscount(int id);
    }
}
