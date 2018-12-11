using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;

namespace ShopCSharpSeleniumAutomationTests.factory
{
    public sealed class DriverFactory
    {
        public static IWebDriver GetDriver(string browserName)
        {
            DriverType driverType = (DriverType)Enum.Parse(typeof(DriverType), browserName);
            switch (driverType)
            {
                case DriverType.chrome:
                    return new ChromeDriver();

                case DriverType.firefox:
                    return new FirefoxDriver();

                case DriverType.edge:
                    return new EdgeDriver();
            }
            throw new DriverNotFoundException(DriverNotFoundException.noDriverType);
        }
    }
    enum DriverType
    {
        chrome,
        firefox,
        edge
    }
}
