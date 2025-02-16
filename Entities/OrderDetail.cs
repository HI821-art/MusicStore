public class OrderDetail
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int VinylRecordId { get; set; }
    public VinylRecord VinylRecord { get; set; }
}