using System;
using System.Globalization;
using System.Threading;
using System.Xml;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using ShopCSharpSeleniumAutomation.factory;
using ShopCSharpSeleniumAutomation.page;
using ShopCSharpSeleniumAutomationTests.factory;

namespace ShopCSharpSeleniumAutomationTests2
{
    //TODO remove ImplicitWait, ADD custom annotation waitFor to PageObjects, add takeScreenshot function
    [TestFixture]
    public class BaseTest
    {
        private static readonly string storeUrl = "http://store.demoqa.com/";
        private static readonly string testPropertiesFileName = "test.properties";

        private XmlDocument properties;

        protected IWebDriver driver;
        protected MenuPage menu;

        [OneTimeSetUp]
        public void ReadProperties()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            properties = new XmlDocument();
            properties.Load($"{TestContext.CurrentContext.TestDirectory}\\{testPropertiesFileName}");
        }

        [SetUp]
        public void Init()
        {
            var browserName = properties.SelectSingleNode("//browser").InnerXml;

            driver = DriverFactory.GetDriver(browserName);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Url = storeUrl;

            menu = PageObjectFactory.CreateMenuPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}