using FluentValidator;
using ModernStore.Shared.Entities;

namespace ModernStore2.Domain.Entities
{
    public class OrderItem:Entity
    {
        protected OrderItem()
        {

        }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = Product.Price;

            new ValidationContract<OrderItem>(this)
                .IsGreaterThan(x => x.Quantity, 0)
                .IsGreaterThan(x => x.Product.QuantityOnHand, Quantity + 1, $"Não temos tantos {Product.Title}(s) em estoque");

            if((Product.QuantityOnHand - Quantity) > 0)
                Product.DecreaseQuantity(quantity);
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}
