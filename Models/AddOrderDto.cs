public partial class MappingProfile
{
    public class AddOrderDto
    {
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
    
}
