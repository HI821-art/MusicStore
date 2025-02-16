using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReservedAt)
            .IsRequired();

        builder.HasOne(r => r.VinylRecord)
            .WithMany()
            .HasForeignKey(r => r.VinylRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}