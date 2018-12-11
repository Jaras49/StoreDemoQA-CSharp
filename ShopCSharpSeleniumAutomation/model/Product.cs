using System;
using System.Collections.Generic;

namespace ShopCSharpSeleniumAutomation.model
{
    public sealed class Product
    {
        public string ProductName { get; }
        public decimal Price { get; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Product(string productName, decimal price)
        {
            ProductName = productName;
            Price = price;
            Quantity++;
            TotalPrice = price;
        }

        public void AddProduct()
        {
            Quantity++;
            TotalPrice = Decimal.Add(TotalPrice, Price);
        }

        public override string ToString() => "Product{" +
               "productName='" + ProductName + '\'' +
               ", price=" + Price +
               ", quantity=" + Quantity +
               ", totalPrice=" + TotalPrice +
               '}';

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   ProductName == product.ProductName &&
                   Price == product.Price &&
                   Quantity == product.Quantity &&
                   TotalPrice == product.TotalPrice;
        }

        public override int GetHashCode()
        {
            var hashCode = 639599475;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            return hashCode;
        }
    }
}
