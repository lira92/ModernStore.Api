using ModernStore2.Domain.Repositories;
using System;
using System.Linq;
using ModernStore2.Domain.Entities;
using ModernStore2.Infra.Contexts;
using ModernStore2.Domain.CommandResults;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace ModernStore2.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreContext _context;

        public ProductRepository(ModernStoreContext context)
        {
            _context = context;
        }

        public List<GetProductsCommandResult> Get()
        {
            return _context.Products.Select(x => new GetProductsCommandResult()
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price
            }).ToList();

            /*var query = "SELECT * FROM [Products]";
            using (var conn = new SqlConnection("Server=(localDb)\\mssqllocaldb;Database=ModernStore"))
            {
                conn.Open();
                return conn.Query<GetProductsCommandResult>(query).ToList();
            }*/
        }

        public Product Get(Guid id)
        {
            return _context
                .Products
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
