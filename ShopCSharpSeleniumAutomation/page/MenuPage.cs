using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.annotations;
using ShopCSharpSeleniumAutomation.factory;
using ShopCSharpSeleniumAutomation.page.cart;
using ShopCSharpSeleniumAutomation.page.category;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopCSharpSeleniumAutomation.page
{
    public class MenuPage : WebElementManipulator<MenuPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MenuPage).Name);

        [FindsBy(How = How.CssSelector, Using = "#menu-item-33 .menu-item")]
        private IList<IWebElement> productCategories;

        [WaitUntilVisible]
        [FindsBy(How = How.CssSelector, Using = "#menu-item-33")]
        private IWebElement productCategoryButton;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-34")]
        private IWebElement accessoriesCategoryButton;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-35")]
        private IWebElement iMacsCategoryButton;

        [WaitUntilVisible]
        [FindsBy(How = How.CssSelector, Using = "#header_cart")]
        private IWebElement cart;

        [WaitUntilVisible]
        [FindsBy(How = How.CssSelector, Using = "#header_cart .count")]
        private IWebElement numberOfCartItems;

        public MenuPage(IWebDriver driver, WebDriverWait wait, Actions actions)
            : base(driver, wait, actions)
        {
            PageFactory.InitElements(driver, this);
            WaitUntilPageLoads();
        }

        public CartPage GoToCartPage()
        {
            ClickElement(cart, nameof(cart));
            return PageObjectFactory.CreateCartPage(driver);
        }

        public CategoryPage GoToRandomCategory()
        {
            var size = productCategories.Count;
            var random = new Random().Next(0, size);
            HoverOverElement(productCategoryButton, nameof(productCategoryButton))
                .ClickElement(productCategories.ElementAt(random), nameof(productCategories));
            return PageObjectFactory.CreateCategoryPage(driver);
        }

        public int GetNumberOfCartItems() => Convert.ToInt32(numberOfCartItems.Text);

        public IWebElement GetNumberOfCartItemsElement() => numberOfCartItems;

        protected override MenuPage This => this;

        protected override ILog Logger => log;
    }
}
