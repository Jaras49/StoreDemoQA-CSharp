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
    public abstract class BasePage<T> where T: BasePage<T>
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;

        protected BasePage(IWebDriver driver, WebDriverWait wait, Actions actions)
        {
            this.driver = driver;
            this.wait = wait;
            this.actions = actions;
        }
        protected abstract T GetThis();
    }
}
