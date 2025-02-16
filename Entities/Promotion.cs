public class Promotion
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<VinylRecord> VinylRecords { get; set; } = new List<VinylRecord>();
}