using System;
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
    public class CheckoutPage : WebElementManipulator<CheckoutPage>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckoutPage).Name);

        public MenuPage Menu { get; }

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_9")]
        private IWebElement emailInput;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_2")]
        private IWebElement firstnameInput;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_3")]
        private IWebElement lastnameInput;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_4")]
        private IWebElement addressInput;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_5")]
        private IWebElement cityInput;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_6")]
        private IWebElement stateInput;

        [FindsBy(How = How.CssSelector, Using = "#current_country")]
        private IWebElement shippingCountrySelect;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_7")]
        private IWebElement countrySelect;

        [FindsBy(How = How.CssSelector, Using = "#wpsc_checkout_form_18")]
        private IWebElement phoneInput;

        [FindsBy(How = How.CssSelector, Using = "#change_country input[type='submit']")]
        private IWebElement calculateShippingButton;

        [FindsBy(How = How.CssSelector, Using = "#shippingSameBilling")]
        private IWebElement sameAsBillingAddressCheckBox;

        [FindsBy(How = How.CssSelector, Using = ".table-2 tr:first-of-type")]
        private IWebElement shippingAddressContent;

        [FindsBy(How = How.CssSelector, Using = ".table-4")]
        private IWebElement priceSummaryTable;

        [FindsBy(How = How.CssSelector, Using = "//*[contains(text(), 'Total Shipping')]/following-sibling::*")]
        private IWebElement totalShippingCost;

        [FindsBy(How = How.CssSelector, Using = "//*[contains(text(), 'Item Cost')]/following-sibling::*")]
        private IWebElement itemCost;

        [FindsBy(How = How.CssSelector, Using = "//*[contains(text(), 'Total Price')]/following-sibling::*")]
        private IWebElement totalPrice;

        [FindsBy(How = How.CssSelector, Using = "input[value='Purchase']")]
        private IWebElement purchaseButton;

        public CheckoutPage(IWebDriver driver, WebDriverWait wait, Actions actions, MenuPage menu)
            : base(driver, wait, actions)
        {
            Menu = menu;
            PageFactory.InitElements(driver, this);
        }

        public TransactionSummaryPage ClickPurchaseButton()
        {
            ClickElement(purchaseButton, nameof(purchaseButton));
            return PageObjectFactory.CreateTransactionSummaryPage(driver);
        }

        public CheckoutPage AssertCosts(Order expectedOrder) => AssertEquals(expectedOrder.GetOrderPrice(), GetItemCost())
                .AssertEquals(expectedOrder.GetOrderPriceWithShipping(), GetTotalPrice());

        public CheckoutPage FillFormWithUserDetails(User user)
        {
            return SelectDropdownByVisibleText(shippingCountrySelect, nameof(shippingCountrySelect), user.Country)
                .ClickElementAndWaitToBeVisible(calculateShippingButton, nameof(calculateShippingButton), priceSummaryTable, nameof(priceSummaryTable))
                .SendKeys(emailInput, nameof(emailInput), user.Email)
                .SendKeys(firstnameInput, nameof(firstnameInput), user.Firstname)
                .SendKeys(lastnameInput, nameof(lastnameInput), user.Lastname)
                .SendKeys(addressInput, nameof(addressInput), user.Address)
                .SendKeys(cityInput, nameof(cityInput), user.City)
                .SendKeys(stateInput, nameof(stateInput), user.State)
                .SelectDropdownByVisibleText(countrySelect, nameof(countrySelect), user.Country)
                .SendKeys(phoneInput, nameof(phoneInput), user.Phone)
                .ClickElementAndWaitToBeInvisible(sameAsBillingAddressCheckBox, nameof(sameAsBillingAddressCheckBox), shippingAddressContent, nameof(shippingAddressContent));
        }

        public decimal GetShippingCost() => Convert.ToDecimal(Regex.Replace(totalShippingCost.Text, "[$,]", ""));

        private decimal GetItemCost() => Convert.ToDecimal(itemCost);

        private decimal GetTotalPrice() => Convert.ToDecimal(Regex.Replace(totalPrice.Text, "[$,]", ""));

        protected override ILog Logger => log;
        protected override CheckoutPage This => this;
    }
}
