using System.ComponentModel.DataAnnotations.Schema;

public class VinylRecordPopularity
{
    public int Id { get; set; }
    public string Name { get; set; }

    
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }

    [NotMapped]
    public string ArtistName => Artist?.Name ?? "Unknown";

    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    [NotMapped]
    public string GenreName => Genre?.Name ?? "Unknown";

    public int Sales { get; set; }
    public decimal SellingPrice { get; set; }
}
