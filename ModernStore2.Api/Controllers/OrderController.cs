using Microsoft.AspNetCore.Mvc;
using ModernStore2.Domain.CommandHandler;
using ModernStore2.Domain.Commands;
using ModernStore2.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore2.Api.Controllers
{
    public class OrderController:BaseController
    {
        private readonly OrderCommandHandler _handler;
        public OrderController(IUow uow, OrderCommandHandler handler):base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/orders")]
        public async Task<IActionResult> Post([FromBody]RegisterOrderCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}
