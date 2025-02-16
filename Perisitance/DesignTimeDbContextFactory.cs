using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MusicStore.Data;
using System.Configuration;

namespace MusicStore.Perisitance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MusicStoreDbContext>
    {
        public MusicStoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicStoreDbContext>();

          
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);

            return new MusicStoreDbContext(optionsBuilder.Options);
        }
    }
}
