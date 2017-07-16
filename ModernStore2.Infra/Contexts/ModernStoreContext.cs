using ModernStore2.Domain.Entities;
using ModernStore2.Infra.Map;
using System.Data.Entity;

namespace ModernStore2.Infra.Contexts
{
    public class ModernStoreContext:DbContext
    {
        public ModernStoreContext():
            base("Server=(localdb)\\mssqllocaldb;Database=ModernStore;Trusted_Connection=True;")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
        }
    }
}
