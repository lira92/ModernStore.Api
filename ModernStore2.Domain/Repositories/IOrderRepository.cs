using ModernStore2.Domain.Entities;

namespace ModernStore2.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}