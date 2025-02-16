public class Reservation
{
    public int Id { get; set; }
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    public int VinylRecordId { get; set; }
    public VinylRecord VinylRecord { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
