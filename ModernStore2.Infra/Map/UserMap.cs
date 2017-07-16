using ModernStore2.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore2.Infra.Map
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x => x.Username).IsRequired().HasMaxLength(20);
            Property(x => x.Password).IsRequired().HasMaxLength(32).IsFixedLength();
            Property(x => x.Active).IsRequired();
        }
    }
}
