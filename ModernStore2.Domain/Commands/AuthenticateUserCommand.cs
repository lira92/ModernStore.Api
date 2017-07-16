using ModernStore.Shared.Commands;

namespace ModernStore2.Domain.Commands
{
    public class AuthenticateUserCommand:ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
