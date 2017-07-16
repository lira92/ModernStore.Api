using ModernStore2.Domain.CommandHandler;
using ModernStore2.Domain.Commands;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.Repositories;
using ModernStore2.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ModernStore2.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new RegisterOrderCommand()
            {
                Customer = Guid.NewGuid(),
                DeliveryFee = 9,
                Discount = 30,
                Items = new List<RegisterOrderItemCommand>
                {
                    new RegisterOrderItemCommand
                    {
                        Product = Guid.NewGuid(),
                        Quantity = 3
                    }
                }
            };

            GenerateOrder(new FakeCustomerRepository(),
                new FakeProductRepository(),
                new FakeOrderRepository(),
                command);

            System.Console.ReadKey();
        }

        public static void GenerateOrder(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            RegisterOrderCommand command)
        {
            var handler = new OrderCommandHandler(customerRepository, productRepository, orderRepository);
            handler.Handle(command);

            if (handler.IsValid())
                System.Console.WriteLine("Customer registrado com sucesso!");
        }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(Guid id)
        {
            return null;
        }

        public Customer GetByUserId(Guid id)
        {
            return new Customer(new Name("Alan", "Lira"), 
                new DateTime(1992, 07, 06),
                new Email("alan.lira@insidesistemas.com.br"), 
                new User("alanlira", "alanlira","alanlira"),
                new Document("08381614996"));
        }

        public void Save(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeProductRepository : IProductRepository
    {
        public Product Get(Guid id)
        {
            return new Product("Mouse", 299, "mouse.jpg", 20);
        }
    }

    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            
        }
    }


}
