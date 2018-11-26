using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCSharpSeleniumAutomation.page
{
    public class MenuPage : WebElementManipulator<MenuPage>
    {
        public MenuPage(IWebDriver driver, WebDriverWait wait, Actions actions)
            : base(driver, wait, actions) { }

        protected override MenuPage GetThis()
        {
            throw new NotImplementedException();
        }
    }
}
