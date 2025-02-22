public partial class MappingProfile
{
    public class AddPromotionDto
    {
        public string Name { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> VinylRecordIds { get; set; }
    }
    
}
