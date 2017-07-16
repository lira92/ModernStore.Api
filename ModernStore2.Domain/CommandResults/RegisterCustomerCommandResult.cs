using ModernStore.Shared.Commands;
using System;

namespace ModernStore2.Domain.CommandResults
{
    public class RegisterCustomerCommandResult:ICommandResult
    {
        public RegisterCustomerCommandResult()
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
