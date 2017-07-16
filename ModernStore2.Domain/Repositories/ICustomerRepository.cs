using ModernStore2.Domain.Entities;
using System;

namespace ModernStore2.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(Guid id);
        Customer GetByUserId(Guid id);
        Customer GetByUsername(string username);
        void Save(Customer customer);
    }
}
