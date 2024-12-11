using Microsoft.EntityFrameworkCore;
using Trace.Education.Order.API.Feature.Orders;



namespace Trace.Education.Order.API.Repositories {
    public class AppDbContext :DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Feature.Orders.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
