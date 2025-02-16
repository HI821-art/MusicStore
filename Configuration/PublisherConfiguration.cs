using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(p => p.VinylRecords)
            .WithOne(v => v.Publisher)
            .HasForeignKey(v => v.PublisherId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}