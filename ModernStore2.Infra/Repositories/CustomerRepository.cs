using ModernStore2.Domain.Repositories;
using System;
using ModernStore2.Domain.Entities;
using ModernStore2.Infra.Contexts;
using System.Linq;
using System.Data.Entity;

namespace ModernStore2.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreContext _context;

        public CustomerRepository(ModernStoreContext context)
        {
            _context = context;
        }

        public Customer Get(Guid id)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public Customer GetByUserId(Guid id)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.Id == id);
        }

        public Customer GetByUsername(string username)
        {
            return _context
                .Customers
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault(x => x.User.Username == username);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }
    }
}
