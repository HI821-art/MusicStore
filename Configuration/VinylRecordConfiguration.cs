using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class VinylRecordConfiguration : IEntityTypeConfiguration<VinylRecord>
{
    public void Configure(EntityTypeBuilder<VinylRecord> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.SellingPrice)
            .IsRequired();

        builder.Property(v => v.ReleaseDate)
            .IsRequired();

        builder.Property(v => v.Stock)
            .IsRequired();

        builder.HasMany(v => v.Promotions)
            .WithMany(p => p.VinylRecords)
            .UsingEntity(j => j.ToTable("VinylRecordPromotions"));

        builder.HasOne(v => v.Artist)
            .WithMany(a => a.VinylRecords)
            .HasForeignKey(v => v.ArtistId);

        builder.HasOne(v => v.Publisher)
            .WithMany(p => p.VinylRecords)
            .HasForeignKey(v => v.PublisherId);

        builder.HasOne(v => v.Genre)
            .WithMany(g => g.VinylRecords)
            .HasForeignKey(v => v.GenreId);
    }
}