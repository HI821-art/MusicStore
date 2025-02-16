using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.TotalSpent)
            .HasColumnType("decimal(18,2)");

        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Reservations)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}