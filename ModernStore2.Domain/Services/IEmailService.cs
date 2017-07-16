namespace ModernStore2.Domain.Services
{
    public interface IEmailService
    {
        void Send(string ToName, string ToEmail, string subject, string body);
    }
}
