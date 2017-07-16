using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.ValueObjects;

namespace ModernStore2.Domain.Tests
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer customer = new Customer(new Name("Alan", "Lira"), new DateTime(1992, 07, 06), 
            new Email("alan.lira@insidesistemas.com.br"), new User("alanlira", "alanlira", "alanlira"), new Document("08381614996"));

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnOutOfStockProductItShouldReturnError()
        {
            var mouse = new Product("Mouse", 299, "mouse.jpg", 0);

            var order = new Order(customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsFalse(order.IsValid());
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnInStockProductItShouldUpdateQuantity()
        {
            var mouse = new Product("Mouse", 299, "mouse.jpg", 20);

            var order = new Order(customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsTrue(mouse.QuantityOnHand == 18);
        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAValidOrderTheTotalShouldBe757()
        {
            var mouse = new Product("Mouse", 300, "mouse.jpg", 20);
            var memoria = new Product("Memória 8gb", 230, "memoria_8gb.jpg", 4);

            var order = new Order(customer, 12, 15);
            order.AddItem(new OrderItem(mouse, 1));
            order.AddItem(new OrderItem(memoria, 2));

            Assert.IsTrue(order.Total() == 757);
        }
    }
}
