using ModernStore2.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore2.Infra.Map
{
    public class CustomerMap:EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");
            HasKey(x => x.Id);
            Property(x => x.Name.FirstName).IsRequired().HasMaxLength(60);
            Property(x => x.Name.LastName).IsRequired().HasMaxLength(60);
            Property(x => x.Document.Number).IsRequired().HasMaxLength(11).IsFixedLength();
            Property(x => x.Email.Address).IsRequired().HasMaxLength(100);
            Property(x => x.BirthDate);
            HasRequired(x => x.User);
        }
    }
}
