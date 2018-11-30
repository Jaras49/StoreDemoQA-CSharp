using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopCSharpSeleniumAutomation.model
{
    public sealed class Order
    {
        public IList<Product> Products { get; }
        public decimal ShippingPrice { get; set; }

        public Order(IList<Product> products)
        {
            Products = new List<Product>();
            ShippingPrice = Decimal.Zero;
            foreach (Product product in products)
            {
                AddProduct(product);
            }
        }

        public void AddProduct(Product product)
        {
            var products = IsInProducts(product.ProductName);
            if (products.Any())
            {
                products.First().AddProduct();
            }
            else
            {
                Products.Add(product);
            }
        }

        public decimal GetOrderPrice() => Products.Select(n => n.TotalPrice).Sum();

        private IEnumerable<Product> IsInProducts(string name) => Products
                .Select(n => n)
                .Where(n => n.ProductName.Equals(name));

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   EqualityComparer<IList<Product>>.Default.Equals(Products, order.Products) &&
                   ShippingPrice == order.ShippingPrice;
        }

        public override int GetHashCode()
        {
            var hashCode = 182108865;
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<Product>>.Default.GetHashCode(Products);
            hashCode = hashCode * -1521134295 + ShippingPrice.GetHashCode();
            return hashCode;
        }

        public override string ToString() => "Order{" +
                "products=" + Products +
                ", shippingPrice=" + ShippingPrice +
                '}';
    }
}
