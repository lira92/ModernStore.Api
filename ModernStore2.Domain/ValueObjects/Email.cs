using FluentValidator;

namespace ModernStore2.Domain.ValueObjects
{
    public class Email:Notifiable
    {
        protected Email()
        {

        }

        public Email(string address)
        {
            Address = address;

            new ValidationContract<Email>(this)
                .IsRequired(x => x.Address)
                .IsEmail(x => x.Address);
        }

        public string Address { get; private set; }
    }
}
