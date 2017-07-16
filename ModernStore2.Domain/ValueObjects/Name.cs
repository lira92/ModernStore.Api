using FluentValidator;

namespace ModernStore2.Domain.ValueObjects
{
    public class Name:Notifiable
    {
        protected Name()
        {

        }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            new ValidationContract<Name>(this)
                .IsRequired(x => x.FirstName, "Nome é obrigatório")
                .HasMinLenght(x => x.FirstName, 3, "Nome deve ter no mínimo 3 caracteres")
                .HasMaxLenght(x => x.FirstName, 60)
                .IsRequired(x => x.LastName)
                .HasMinLenght(x => x.LastName, 3)
                .HasMaxLenght(x => x.LastName, 60);
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
