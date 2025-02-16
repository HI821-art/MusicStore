using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(g => g.VinylRecords)
            .WithOne(v => v.Genre)
            .HasForeignKey(v => v.GenreId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}