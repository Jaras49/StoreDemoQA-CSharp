using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopCSharpSeleniumAutomation.page
{
    public class MenuPage : WebElementManipulator<MenuPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MenuPage).Name);

        [FindsBy(How = How.CssSelector, Using = "#menu-item-33 .menu-item")]
        private IList<IWebElement> productCategories;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-33")]
        private IWebElement productCategoryButton;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-34")]
        private IWebElement accessoriesCategoryButton;

        [FindsBy(How = How.CssSelector, Using = "#menu-item-35")]
        private IWebElement iMacsCategoryButton;

        [FindsBy(How = How.CssSelector, Using = "#header_cart")]
        private IWebElement cart;

        [FindsBy(How = How.CssSelector, Using = "#header_cart .count")]
        private IWebElement numberOfCartItems;

        public MenuPage(IWebDriver driver, WebDriverWait wait, Actions actions)
            : base(driver, wait, actions)
        {
            PageFactory.InitElements(driver, this);
        }

        public void GoToCartPage()
        {
            ClickElement(cart, nameof(cart));
        }
        //TODO
        public void ExampleMethod()
        {
            HoverOverElement(productCategoryButton, nameof(productCategoryButton));
            //WaitForElementToBeInvisible(accessoriesCategoryButton, nameof(accessoriesCategoryButton));
            accessoriesCategoryButton.Click();
        }

        protected override MenuPage GetThis()
        {
            return this;
        }

        protected override ILog GetLogger()
        {
            return log;
        }
    }
}
