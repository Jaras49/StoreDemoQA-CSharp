using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.page;
using ShopCSharpSeleniumAutomation.page.cart;
using ShopCSharpSeleniumAutomation.page.category;
using System;

namespace ShopCSharpSeleniumAutomation.factory
{
    public sealed class PageObjectFactory
    {
        private static readonly long defaultWait = 15;

        private PageObjectFactory() { }

        public static TransactionSummaryPage CreateTransactionSummaryPage(IWebDriver driver) => new TransactionSummaryPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver), CreateMenuPage(driver));

        public static ProductPage CreateProductPage(IWebDriver driver) => new ProductPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver), CreateMenuPage(driver));

        public static CategoryPage CreateCategoryPage(IWebDriver driver) => new CategoryPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver), CreateMenuPage(driver));

        public static CartPage CreateCartPage(IWebDriver driver) => new CartPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver), CreateMenuPage(driver));

        public static CheckoutPage CreatecheckoutPage(IWebDriver driver) => new CheckoutPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver), CreateMenuPage(driver));

        public static MenuPage CreateMenuPage(IWebDriver driver) => new MenuPage
                (driver, new WebDriverWait(driver, TimeSpan.FromSeconds(defaultWait)), new Actions(driver));
    }
}
