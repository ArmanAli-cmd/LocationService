using LocationService.Domain.Aggregates.LocationAggregate;
using Microsoft.EntityFrameworkCore;

namespace LocationService.Infrastructure.Data.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    
    }
}
