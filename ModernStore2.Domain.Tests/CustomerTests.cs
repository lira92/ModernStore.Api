using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.ValueObjects;

namespace ModernStore2.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private readonly User user = new User("alanlira", "alanlira", "alanlira");

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidFirstNameShouldReturnANotification()
        {
            var name = new Name("", "Lira");
            var email = new Email("alan.lira@insidesistemas.com.br");
            var document = new Document("08381614996");
            var customer = new Customer(name, new DateTime(1992, 07, 06), email, user, document);

            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidLastNameNameShouldReturnANotification()
        {
            var name = new Name("Alan", "");
            var email = new Email("alan.lira@insidesistemas.com.br");
            var document = new Document("08381614996");
            var customer = new Customer(name, new DateTime(1992, 07, 06), email, user, document);

            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidEmailShouldReturnANotification()
        {
            var name = new Name("Alan", "Lira");
            var email = new Email("");
            var document = new Document("08381614996");
            var customer = new Customer(name, new DateTime(1992, 07, 06), email, user, document);

            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnValidCustomer()
        {
            var name = new Name("Alan", "Lira");
            var email = new Email("alan.lira@insidesistemas.com.br");
            var document = new Document("08381614996");
            var customer = new Customer(name, new DateTime(1992, 07, 06), email, user, document);

            Assert.IsTrue(customer.IsValid());
        }
    }
}
