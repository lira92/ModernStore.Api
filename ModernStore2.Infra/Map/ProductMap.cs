using ModernStore2.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore2.Infra.Map
{
    public class ProductMap:EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Products");
            HasKey(x => x.Id);
            Property(x => x.Title).IsRequired().HasMaxLength(80);
            Property(x => x.Price).IsRequired().HasColumnType("money");
            Property(x => x.QuantityOnHand);
            Property(x => x.Image).IsRequired().HasMaxLength(1024);
        }
    }
}
