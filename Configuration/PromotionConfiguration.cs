using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.DiscountPercentage)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.EndDate)
            .IsRequired();

        builder.HasMany(p => p.VinylRecords)
            .WithMany(v => v.Promotions)
            .UsingEntity(j => j.ToTable("VinylRecordPromotions"));
    }
}
