using FluentValidator;
using ModernStore.Shared.Commands;
using ModernStore2.Domain.CommandResults;
using ModernStore2.Domain.Commands;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.Repositories;
using ModernStore2.Domain.Services;
using ModernStore2.Domain.ValueObjects;
using System;

namespace ModernStore2.Domain.CommandHandler
{
    public class CustomerCommandHandler : Notifiable,
        ICommandHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailServices)
        {
            _customerRepository = customerRepository;
            _emailService = emailServices;
        }

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            var customer = new Customer(
                new Name(command.Firstname, command.LastName), 
                command.BirthDate,
                new Email(command.Email), 
                new User(command.Usename, command.Password, command.ConfirmPassword),
                new Document(command.Document));

            AddNotifications(customer.Notifications);

            if (IsValid())
                _customerRepository.Save(customer);

            _emailService.Send(
                customer.Name.ToString(), 
                customer.Email.Address, 
                "Bem-Vindo ao Clube", 
                $"<h1>Olá, <strong>{customer.Name.ToString()}</strong></h1>");

            return new RegisterCustomerCommandResult()
            {
                Id = customer.Id,
                Name = customer.Name.ToString()
            };
        }
    }
}
