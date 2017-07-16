using ModernStore2.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore2.Infra.Map
{
    public class OrderItemMap:EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("OrderItems");
            HasKey(x => x.Id);
            Property(x => x.Price).HasColumnType("money");
            Property(x => x.Quantity);
            HasRequired(x => x.Product);
        }
    }
}
