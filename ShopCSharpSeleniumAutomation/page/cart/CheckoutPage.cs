using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ShopCSharpSeleniumAutomation.page.cart
{
    //TODO implement me
    public class CheckoutPage : WebElementManipulator<CheckoutPage>
    {
        public CheckoutPage(IWebDriver driver, WebDriverWait wait, Actions actions)
            : base(driver, wait, actions)
        {
        }

        protected override ILog GetLogger()
        {
            throw new NotImplementedException();
        }

        protected override CheckoutPage GetThis()
        {
            throw new NotImplementedException();
        }
    }
}
