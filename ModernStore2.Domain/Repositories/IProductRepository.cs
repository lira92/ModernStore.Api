using ModernStore2.Domain.CommandResults;
using ModernStore2.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ModernStore2.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);
        List<GetProductsCommandResult> Get();
    }
}
