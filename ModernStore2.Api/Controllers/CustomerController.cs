using Microsoft.AspNetCore.Mvc;
using ModernStore2.Domain.CommandHandler;
using ModernStore2.Domain.Commands;
using ModernStore2.Domain.Repositories;
using ModernStore2.Domain.Services;
using ModernStore2.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore2.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;
        private readonly CustomerCommandHandler _handler;

        public CustomerController(CustomerCommandHandler customerCommandHandler, ICustomerRepository customerRepository, 
            IEmailService emailService,
            IUow uow):base(uow)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
            _handler = customerCommandHandler;
        }

        [HttpPost]
        [Route("v1/Customers")]
        public async Task<IActionResult> Create([FromBody] RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command);

            return await Response(result, _handler.Notifications);
        }
    }
}
