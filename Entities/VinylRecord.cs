public class VinylRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; } 
    public int Tracks { get; set; } 
    public decimal SalePrice { get; set; }
    public decimal SellingPrice { get; set; } 
    public DateTime ReleaseDate { get; set; }
    public int Stock { get; set; } 
    public int Sales { get; set; } 

    public int ArtistId { get; set; }
    public Artist Artist { get; set; }

    public int? PublisherId { get; set; }
    public Publisher Publisher { get; set; }

    public int? GenreId { get; set; }
    public Genre Genre { get; set; }

    public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
