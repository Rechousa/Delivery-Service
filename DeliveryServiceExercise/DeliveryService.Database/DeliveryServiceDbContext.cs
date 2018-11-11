using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Database
{
    public class DeliveryServiceDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DeliveryServiceDbContext(DbContextOptions<DeliveryServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().HasKey(table => new {
                table.LocationA,
                table.LocationB
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=DeliveryServiceExercise;Trusted_Connection=True;");
            }
        }
    }
}
