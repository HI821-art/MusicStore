using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Services;
using MusicStore.Data;

namespace MusicStore
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MusicStoreDbContext>(options =>
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging(false)
    .LogTo(_ => { }, LogLevel.None));

            services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();
            services.AddScoped<IRepository<VinylRecord>, Repository<VinylRecord>>();

          
            services.AddScoped<IPopularityService, PopularityService>();
        }
    }
}
