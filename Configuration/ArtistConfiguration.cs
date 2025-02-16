using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Bio)
            .HasMaxLength(500);

        builder.HasMany(a => a.VinylRecords)
            .WithOne(v => v.Artist)
            .HasForeignKey(v => v.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}