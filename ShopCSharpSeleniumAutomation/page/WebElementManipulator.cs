using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace ShopCSharpSeleniumAutomation.page
{
    public abstract class WebElementManipulator<T>: BasePage<T> where T: WebElementManipulator<T>
    {
        protected WebElementManipulator(IWebDriver driver, WebDriverWait wait, Actions actions)
            :base(driver, wait, actions) { }

        protected T HoverOverElement(IWebElement element)
        {
            actions.MoveToElement(element);
            return GetThis();
        }
    }
}
