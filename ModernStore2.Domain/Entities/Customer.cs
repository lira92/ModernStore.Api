using ModernStore.Shared.Entities;
using ModernStore2.Domain.ValueObjects;
using System;

namespace ModernStore2.Domain.Entities
{
    public class Customer:Entity
    {
        protected Customer()
        {

        }

        public Customer(Name name, 
            DateTime birthDate, 
            Email email,
            User user,
            Document document)
        {
            Name = name;
            BirthDate = birthDate;
            User = user;
            Email = email;
            Document = document;

            AddNotifications(Name.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);
            AddNotifications(user.Notifications);
        }

        public Name Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public User User { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
    }
}
