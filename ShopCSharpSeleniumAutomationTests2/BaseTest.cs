using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomation.page;

namespace ShopCSharpSeleniumAutomationTests2
{
    [TestFixture]
    public class BaseTest
    {
        private static readonly string STORE_URL = "http://store.demoqa.com/";

        protected IWebDriver driver;
        protected MenuPage menu;

        [SetUp]
        public void Init()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = STORE_URL;
            menu = new MenuPage(driver, new WebDriverWait(driver, new TimeSpan(0, 0, 15)), new Actions(driver));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
