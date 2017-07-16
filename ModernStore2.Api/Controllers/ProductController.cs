using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernStore2.Domain.Repositories;
using ModernStore2.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore2.Api.Controllers
{
    public class ProductController:BaseController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IUow uow, IProductRepository productRepository):base(uow)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("v1/Products")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(_productRepository.Get());
        }
    }
}
