using System;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.Repositories;
using ModernStore2.Infra.Contexts;

namespace ModernStore2.Infra.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ModernStoreContext _context;

        public OrderRepository(ModernStoreContext context)
        {
            _context = context;
        }

        public void Save(Order order)
        {
            _context.Orders.Add(order);
        }
    }
}
