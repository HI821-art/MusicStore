public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<VinylRecord> VinylRecords { get; set; } = new List<VinylRecord>();
}
