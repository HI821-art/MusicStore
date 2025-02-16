public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<VinylRecord> VinylRecords { get; set; } = new List<VinylRecord>();
}
public class GenrePopularity
{
    public string GenreName { get; set; }
    public int TotalSales { get; set; }
}