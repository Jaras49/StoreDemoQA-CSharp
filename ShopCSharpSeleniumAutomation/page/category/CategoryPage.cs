using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.factory;

namespace ShopCSharpSeleniumAutomation.page.category
{
    public class CategoryPage : WebElementManipulator<CategoryPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryPage).Name);
        private static readonly string removeDashesRegex = "\\p{Pd}";

        public MenuPage Menu { get; }

        [FindsBy(How = How.CssSelector, Using = "#content[role='main']")]
        private IWebElement content;

        [FindsBy(How = How.CssSelector, Using = "#content  h2 > a")]
        private IList<IWebElement> products;

        public CategoryPage(IWebDriver driver, WebDriverWait wait, Actions actions, MenuPage menu)
            : base(driver, wait, actions)
        {
            Menu = menu;
            PageFactory.InitElements(driver, this);
        }

        public ProductPage GoToRandomProductPageAndAssertItSwitchedCorrectly()
        {
            var size = products.Count;
            var random = new Random().Next(0, size);

            IWebElement element = products.ElementAt(random);
            string productName = Regex.Replace(element.Text, removeDashesRegex, "");

            ClickElement(element, nameof(element));
            ProductPage productPage = PageObjectFactory.CreateProductPage(driver);

            AssertEquals(productName, productPage.GetProductName());
            return productPage;
        }

        protected override ILog Logger => log;
        protected override CategoryPage This => this;
    }
}
