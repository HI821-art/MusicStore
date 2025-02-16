using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MusicStore.Configurations;


namespace MusicStore.Data
{
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            : base(options)
        {
        }
        public MusicStoreDbContext() 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicStore;Integrated Security=True");
            }
        }

       

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<VinylRecord> VinylRecords { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new VinylRecordConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Sale>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var vinylRecord = await VinylRecords
                        .FirstOrDefaultAsync(vr => vr.Id == entry.Entity.VinylRecordId, cancellationToken);

                    if (vinylRecord != null)
                    {
                        vinylRecord.Sales += entry.Entity.Quantity;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
