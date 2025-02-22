public class VinylRecordDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public int Tracks { get; set; }
    public decimal SalePrice { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Stock { get; set; }
    public int ArtistId { get; set; }
    public int PublisherId { get; set; }
    public int GenreId { get; set; }
    public Artist Artist { get; set; }
    public Publisher Publisher { get; set; }
    public Genre Genre { get; set; }
}
