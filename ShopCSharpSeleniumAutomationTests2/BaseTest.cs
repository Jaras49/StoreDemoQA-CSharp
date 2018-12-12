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
            driver.Url = storeUrl;

            menu = PageObjectFactory.CreateMenuPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            TakeScreenshot();
            driver.Quit();
        }

        private void TakeScreenshot()
        {
            var screenshotsDirPath = properties.SelectSingleNode("//dir/screenshots").InnerXml;
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile($"{screenshotsDirPath}{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.jpeg");
        }
    }
}