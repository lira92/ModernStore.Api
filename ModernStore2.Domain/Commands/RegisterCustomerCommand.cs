using ModernStore.Shared.Commands;
using System;

namespace ModernStore2.Domain.Commands
{
    public class RegisterCustomerCommand:ICommand
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Usename { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
