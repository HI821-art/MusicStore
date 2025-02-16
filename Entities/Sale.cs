public class Sale
{
    public int Id { get; set; }
    public int VinylRecordId { get; set; }
    public VinylRecord VinylRecord { get; set; }
    
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set;  }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
}