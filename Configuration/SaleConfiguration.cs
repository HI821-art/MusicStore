using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SaleDate)
            .IsRequired();

        builder.Property(s => s.SalePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(s => s.VinylRecord)
            .WithMany()
            .HasForeignKey(s => s.VinylRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Customer)
        .WithMany()
        .HasForeignKey(s => s.CustomerId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}