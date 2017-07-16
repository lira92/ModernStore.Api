using System;
using FluentValidator;
using ModernStore.Shared.Commands;
using ModernStore2.Domain.Commands;
using ModernStore2.Domain.Repositories;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.CommandResults;

namespace ModernStore2.Domain.CommandHandler
{
    public class OrderCommandHandler : Notifiable, 
        ICommandHandler<RegisterOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            var customer = _customerRepository.Get(command.Customer);

            var order = new Order(customer, command.DeliveryFee, command.Discount);
            foreach (var item in command.Items)
            {
                var product = _productRepository.Get(item.Product);
                order.AddItem(new OrderItem(product, item.Quantity));
            }

            AddNotifications(order.Notifications);

            if (!IsValid())
                return null;

            _orderRepository.Save(order);

            return new RegisterOrderCommandResult()
            {
                Number = order.Number
            };
        }
    }
}
