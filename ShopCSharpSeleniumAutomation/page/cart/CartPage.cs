using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.factory;
using ShopCSharpSeleniumAutomation.model;

namespace ShopCSharpSeleniumAutomation.page.cart
{
    public class CartPage : WebElementManipulator<CartPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CartPage).Name);

        private static readonly string removeDashesRegex = "\\p{Pd}";
        private static readonly string productQuantitySelector = ".wpsc_product_quantity input[name='quantity']";
        private static readonly string productPriceSelector = ".wpsc_product_quantity + td";
        private static readonly string productNameSelector = ".wpsc_product_name";
        private static readonly string productTotalPriceSelector = ".wpsc_product_price";

        public MenuPage Menu { get; }

        [FindsBy(How = How.CssSelector, Using = ".yourtotal .pricedisplay")]
        private IWebElement totalCartPrice;

        [FindsBy(How = How.CssSelector, Using = ".checkout_cart tr.product_row")]
        private IList<IWebElement> products;

        [FindsBy(How = How.CssSelector, Using = ".step2")]
        private IWebElement continueButton;

        public CartPage(IWebDriver driver, WebDriverWait wait, Actions actions, MenuPage menu)
            : base(driver, wait, actions)
        {
            Menu = menu;
            PageFactory.InitElements(driver, this);
        }

        public CheckoutPage ClickContinueButton()
        {
            ClickElement(continueButton, nameof(continueButton));
            return PageObjectFactory.CreateCheckoutPage(driver);
        }

        public CartPage AssertCart(Order expectedOrder)
        {
            var actualOrder = MapTableRowsToObjects();
            return AssertEquals(expectedOrder, actualOrder)
                .AssertProductTotalPrices()
                .AssertEquals(expectedOrder.GetOrderPrice(), GetTotalPrice());
        }

        private CartPage AssertProductTotalPrices()
        {
            foreach (IWebElement product in products)
            {
                var productQuantity = GetProductQuantity(product);
                var productPrice = GetProductPrice(product);
                AssertEquals(Decimal.Multiply(productQuantity, productPrice), GetProductTotalPrice(product));
            }
            return this;
        }

        private decimal GetTotalPrice() => Convert.ToDecimal(Regex.Replace(totalCartPrice.Text, "[$,]", ""));

        private decimal GetProductTotalPrice(IWebElement product)
        {
            var price = product.FindElement(By.CssSelector(productTotalPriceSelector)).Text;
            return Convert.ToDecimal(Regex.Replace(price, "[$,]", ""));
        }

        private Order MapTableRowsToObjects()
        {
            IList<Product> p = new List<Product>();
            foreach (IWebElement product in products)
            {
                var productName = GetProductName(product);
                var price = GetProductPrice(product);
                var quantity = GetProductQuantity(product);

                for (var i = 0; i < quantity; i++)
                {
                    p.Add(new Product(productName, price));
                }
            }
            return new Order(p);
        }

        private int GetProductQuantity(IWebElement product)
        {
            var quantityString = product.FindElement(By.CssSelector(productQuantitySelector)).GetAttribute("value");
            return Convert.ToInt32(quantityString);
        }

        private decimal GetProductPrice(IWebElement product)
        {
            var priceText = product.FindElement(By.CssSelector(productPriceSelector)).Text;
            return Convert.ToDecimal(Regex.Replace(priceText, "[$,]", ""));
        }

        private string GetProductName(IWebElement product)
        {
            var text = product.FindElement(By.CssSelector(productNameSelector)).Text;
            return Regex.Replace(text, removeDashesRegex, "");
        }

        protected override ILog Logger => log;
        protected override CartPage This => this;
    }
}
