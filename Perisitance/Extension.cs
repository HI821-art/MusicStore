using Microsoft.EntityFrameworkCore;


public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Seeding Genre data
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Rock" },
            new Genre { Id = 2, Name = "Pop" },
            new Genre { Id = 3, Name = "Jazz" },
            new Genre { Id = 4, Name = "Classical" },
            new Genre { Id = 5, Name = "Hip-Hop" },
            new Genre { Id = 6, Name = "Electronic" },
            new Genre { Id = 7, Name = "Country" },
            new Genre { Id = 8, Name = "Blues" },
            new Genre { Id = 9, Name = "Reggae" },
            new Genre { Id = 10, Name = "Indie" }
        );

        // Seeding Artist data
        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "The Beatles", Bio = "English rock band formed in Liverpool in 1960." },
            new Artist { Id = 2, Name = "Michael Jackson", Bio = "American singer, songwriter, and dancer." },
            new Artist { Id = 3, Name = "Miles Davis", Bio = "American jazz trumpeter, bandleader, and composer." },
            new Artist { Id = 4, Name = "Beyoncé", Bio = "American singer, songwriter, and actress." },
            new Artist { Id = 5, Name = "Taylor Swift", Bio = "American singer-songwriter known for narrative songwriting." },
            new Artist { Id = 6, Name = "Pink Floyd", Bio = "English rock band known for progressive rock." },
            new Artist { Id = 7, Name = "Bob Marley", Bio = "Jamaican singer, songwriter, and musician." },
            new Artist { Id = 8, Name = "David Bowie", Bio = "English singer, songwriter, and actor." },
            new Artist { Id = 9, Name = "Madonna", Bio = "American singer, songwriter, and actress." },
            new Artist { Id = 10, Name = "Adele", Bio = "English singer-songwriter." }
        );

        // Seeding Publisher data
        modelBuilder.Entity<Publisher>().HasData(
            new Publisher { Id = 1, Name = "Universal Music" },
            new Publisher { Id = 2, Name = "Sony Music" },
            new Publisher { Id = 3, Name = "Warner Music" },
            new Publisher { Id = 4, Name = "EMI" },
            new Publisher { Id = 5, Name = "Atlantic Records" }
        );

        // Seeding Customer data
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Phone = "123-456-7890", TotalSpent = 500.00m },
            new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com", Phone = "234-567-8901", TotalSpent = 1000.00m },
            new Customer { Id = 3, FirstName = "Robert", LastName = "Johnson", Email = "robertjohnson@example.com", Phone = "345-678-9012", TotalSpent = 150.00m },
            new Customer { Id = 4, FirstName = "Emily", LastName = "Davis", Email = "emilydavis@example.com", Phone = "456-789-0123", TotalSpent = 250.00m },
            new Customer { Id = 5, FirstName = "Michael", LastName = "Miller", Email = "michaelmiller@example.com", Phone = "567-890-1234", TotalSpent = 300.00m },
            new Customer { Id = 6, FirstName = "Sophia", LastName = "Williams", Email = "sophiawilliams@example.com", Phone = "678-901-2345", TotalSpent = 450.00m },
            new Customer { Id = 7, FirstName = "Liam", LastName = "Brown", Email = "liambrown@example.com", Phone = "789-012-3456", TotalSpent = 1200.00m },
            new Customer { Id = 8, FirstName = "Olivia", LastName = "Jones", Email = "oliviajones@example.com", Phone = "890-123-4567", TotalSpent = 700.00m },
            new Customer { Id = 9, FirstName = "Ethan", LastName = "Garcia", Email = "ethangarcia@example.com", Phone = "901-234-5678", TotalSpent = 600.00m },
            new Customer { Id = 10, FirstName = "Ava", LastName = "Martinez", Email = "avamartinez@example.com", Phone = "012-345-6789", TotalSpent = 800.00m }
        );

        // Seeding Promotion data
        modelBuilder.Entity<Promotion>().HasData(
            new Promotion { Id = 1, Name = "Jazz Week", DiscountPercentage = 10.00m, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10) },
            new Promotion { Id = 2, Name = "Rock Fest", DiscountPercentage = 15.00m, StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(5) },
            new Promotion { Id = 3, Name = "Pop Mania", DiscountPercentage = 20.00m, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(20) }
        );

        // Seeding Discount data
        modelBuilder.Entity<Discount>().HasData(
            new Discount { Id = 1, Name = "Black Friday", Percentage = 50.00m, StartDate = DateTime.Now.AddDays(-15), EndDate = DateTime.Now.AddDays(-10) },
            new Discount { Id = 2, Name = "New Year Sale", Percentage = 30.00m, StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(5) },
            new Discount { Id = 3, Name = "Summer Clearance", Percentage = 25.00m, StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(30) },
            new Discount { Id = 4, Name = "Winter Deals", Percentage = 40.00m, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-10) },
            new Discount { Id = 5, Name = "Easter Special", Percentage = 15.00m, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(40) }
        );

        // Seeding VinylRecord data
        modelBuilder.Entity<VinylRecord>().HasData(
            new VinylRecord { Id = 1, Name = "Abbey Road", Year = 1969, Tracks = 17, SalePrice = 19.99m, SellingPrice = 20.99m, ReleaseDate = new DateTime(1969, 9, 26), Stock = 100, Sales = 500, ArtistId = 1, PublisherId = 1, GenreId = 1 },
            new VinylRecord { Id = 2, Name = "Thriller", Year = 1982, Tracks = 9, SalePrice = 24.99m, SellingPrice = 25.99m, ReleaseDate = new DateTime(1982, 11, 30), Stock = 150, Sales = 700, ArtistId = 2, PublisherId = 2, GenreId = 2 },
            new VinylRecord { Id = 3, Name = "Kind of Blue", Year = 1959, Tracks = 5, SalePrice = 29.99m, SellingPrice = 30.99m, ReleaseDate = new DateTime(1959, 8, 17), Stock = 80, Sales = 400, ArtistId = 3, PublisherId = 3, GenreId = 3 },
            new VinylRecord { Id = 4, Name = "Lemonade", Year = 2016, Tracks = 12, SalePrice = 21.99m, SellingPrice = 22.99m, ReleaseDate = new DateTime(2016, 4, 23), Stock = 200, Sales = 600, ArtistId = 4, PublisherId = 1, GenreId = 2 },
            new VinylRecord { Id = 5, Name = "1989", Year = 2014, Tracks = 13, SalePrice = 17.99m, SellingPrice = 18.99m, ReleaseDate = new DateTime(2014, 10, 27), Stock = 250, Sales = 800, ArtistId = 5, PublisherId = 2, GenreId = 2 }
        );
    }
}
