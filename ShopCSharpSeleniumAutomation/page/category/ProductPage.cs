using System;
using System.Text.RegularExpressions;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ShopCSharpSeleniumAutomation.page.category
{
    public class ProductPage : WebElementManipulator<ProductPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductPage).Name);
        private static readonly string removeDashesRegex = "\\p{Pd}";

        public MenuPage Menu { get; }

        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        private IWebElement productTitle;

        [FindsBy(How = How.CssSelector, Using = ".currentprice")]
        private IWebElement productPrice;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private IWebElement addToCartButton;

        [FindsBy(How = How.CssSelector, Using = ".alert.addtocart")]
        private IWebElement productAddedAlert;

        public ProductPage(IWebDriver driver, WebDriverWait wait, Actions actions, MenuPage menu)
            : base(driver, wait, actions)
        {
            Menu = menu;
            PageFactory.InitElements(driver, this);
        }

        public ProductPage AddProductXtimes(int timesToAddProduct)
        {
            for (int i = 0; i < timesToAddProduct; i++)
            {
                AddProduct();
            }
            return this;
        }

        public string GetProductName() => Regex.Replace(productTitle.Text, removeDashesRegex, "");

        public decimal GetProductPrice()
        {
            string productPriceString = Regex.Replace(productPrice.Text, "[$,]", "");
            return Convert.ToDecimal(productPriceString);
        }

        private void AddProduct()
        {
            ClickElementAndWaitToBeVisible(addToCartButton, nameof(addToCartButton), productAddedAlert, nameof(productAddedAlert))
                .WaitForElementTextUpdate(Menu.GetNumberOfCartItemsElement(), ExpectedCartText(), nameof(Menu.GetNumberOfCartItemsElement));
        }

        private string ExpectedCartText()
        {
            var currentCartProducts = Menu.GetNumberOfCartItems();
            var expectedCartProducts = currentCartProducts + 1;
            return expectedCartProducts.ToString();
        }

        protected override ILog Logger => log;
        protected override ProductPage This => this;
    }
}
