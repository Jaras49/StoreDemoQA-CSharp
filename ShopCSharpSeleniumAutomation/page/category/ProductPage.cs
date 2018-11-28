using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ShopCSharpSeleniumAutomation.page.category
{
    //TODO implement me
    public class ProductPage : WebElementManipulator<ProductPage>
    {
        public ProductPage(IWebDriver driver, WebDriverWait wait, Actions actions)
            : base(driver, wait, actions)
        {
        }

        protected override ILog GetLogger()
        {
            throw new NotImplementedException();
        }

        protected override ProductPage GetThis()
        {
            throw new NotImplementedException();
        }
    }
}
