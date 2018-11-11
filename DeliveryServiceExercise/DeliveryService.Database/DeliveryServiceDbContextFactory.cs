using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryService.Database
{
    public class DeliveryServiceDbContextFactory : IDesignTimeDbContextFactory<DeliveryServiceDbContext>
    {
        public DeliveryServiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeliveryServiceDbContext>();
            return new DeliveryServiceDbContext(optionsBuilder.Options);
        }
    }
}
