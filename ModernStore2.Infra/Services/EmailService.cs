using ModernStore2.Domain.Services;

namespace ModernStore2.Infra.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string ToName, string ToEmail, string subject, string body)
        {
            // fazer nada
        }
    }
}
