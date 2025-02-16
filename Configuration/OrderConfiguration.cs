using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);
    }
}