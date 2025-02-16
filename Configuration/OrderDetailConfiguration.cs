using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => od.Id);

        builder.Property(od => od.Quantity)
            .IsRequired();

        builder.Property(od => od.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(od => od.VinylRecord)
            .WithMany()
            .HasForeignKey(od => od.VinylRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
