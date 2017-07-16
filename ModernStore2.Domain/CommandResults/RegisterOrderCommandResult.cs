using ModernStore.Shared.Commands;

namespace ModernStore2.Domain.CommandResults
{
    public class RegisterOrderCommandResult:ICommandResult
    {
        public RegisterOrderCommandResult()
        {

        }

        public string Number { get; set; }
    }
}
