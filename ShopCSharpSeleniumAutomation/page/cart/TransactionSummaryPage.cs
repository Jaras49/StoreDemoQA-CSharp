using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.model;

namespace ShopCSharpSeleniumAutomation.page.cart
{
    public class TransactionSummaryPage : WebElementManipulator<TransactionSummaryPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TransactionSummaryPage).Name);

        private static readonly string replaceDashesRegex = "\\p{Pd}";
        private static readonly string productNameSelector = "td:first-of-type";
        private static readonly string productPriceSelector = "td:nth-of-type(2)";
        private static readonly string productQuantitySelector = "td:nth-of-type(3)";
        private static readonly string productTotalPriceSelector = "td:nth-of-type(4)";

        public MenuPage Menu { get; }

        [FindsBy(How = How.CssSelector, Using = ".wpsc-purchase-log-transaction-results > tbody > tr")]
        private IList<IWebElement> productsTableRows;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(), 'Total Shipping')]")]
        private IWebElement totalShippingPrice;

        public TransactionSummaryPage(IWebDriver driver, WebDriverWait wait, Actions actions, MenuPage menu)
            : base(driver, wait, actions)
        {
            Menu = menu;
            PageFactory.InitElements(driver, this);
        }

        public TransactionSummaryPage AssertTransactionSummaryPage(Order expectedOrder)
        {
            Order actualOrder = MapSummaryTableToObjectAndAssertTotalPrices();
            actualOrder.ShippingPrice = GetTotalShippingPrice();

            return AssertEquals(expectedOrder, actualOrder)
                .AssertEquals(expectedOrder.ShippingPrice, actualOrder.ShippingPrice)
                .AssertEquals(expectedOrder.GetOrderPriceWithShipping(), GetTotalPrice());
        }

        private Order MapSummaryTableToObjectAndAssertTotalPrices()
        {
            IList<Product> p = new List<Product>();
            foreach (IWebElement row in productsTableRows)
            {
                var productName = Regex.Replace(row.FindElement(By.CssSelector(productNameSelector)).Text, replaceDashesRegex, "");
                var price = GetProductPrice(row);
                var quantity = Convert.ToInt32(row.FindElement(By.CssSelector(productQuantitySelector)).Text);

                AssertTotalPrice(row, price, quantity);

                for (var i = 0; i < quantity; i++)
                {
                    p.Add(new Product(productName, price));
                }
            }
            return new Order(p);
        }

        private decimal GetTotalProductPrice(IWebElement element) =>
            Convert.ToDecimal(Regex.Replace(element.FindElement(By.CssSelector(productTotalPriceSelector)).Text, "[$,]", ""));

        private decimal GetProductPrice(IWebElement element) =>
            Convert.ToDecimal(Regex.Replace(element.FindElement(By.CssSelector(productPriceSelector)).Text, "[$,]", ""));

        private void AssertTotalPrice(IWebElement product, decimal price, int quantity) =>
            AssertEquals(Decimal.Multiply(price, quantity), GetTotalProductPrice(product));

        private decimal GetTotalShippingPrice()
        {
            var totalShipping = Regex.Match(totalShippingPrice.Text, "Total Shipping: ((.)+)\\n").Groups[1].Value;
            return Convert.ToDecimal(Regex.Replace(totalShipping, "[$,]", "").Trim());
        }

        private decimal GetTotalPrice()
        {
            var totalPrice = Regex.Match(totalShippingPrice.Text, "Total: ((.)+)$").Groups[1].Value;
            return Convert.ToDecimal(Regex.Replace(totalPrice, "[$,]", ""));
        }

        protected override ILog Logger => log;
        protected override TransactionSummaryPage This => this;
    }
}
