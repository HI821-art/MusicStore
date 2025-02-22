public partial class MappingProfile
{
    public class AddOrderDetailDto
    {
        public int OrderId { get; set; }
        public int VinylRecordId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    
}
