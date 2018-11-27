using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace ShopCSharpSeleniumAutomation.page
{
    public abstract class BasePage<T> where T : BasePage<T>
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

        public T AssertEquals<M>(M expected, M actual)
        {
            Assert.AreEqual(expected, actual);
            return GetThis();
        }

        protected IWebElement WaitForElementToBeVisible(IWebElement element, string elementName)
        {
            GetLogger().Info($"Waiting for element - {elementName} - to become visible");
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        protected IWebElement WaitForElementToBeInvisible(IWebElement element, string elementName)
        {
            GetLogger().Info($"Waiting for element - {elementName} to become invisible");
            return wait.Until<IWebElement>((d) =>
            {
                if (!element.Displayed)
                {
                    return element;
                }
                return null;
            });
        }

        protected T WaitForElementTextUpdate(IWebElement element, string textToBe, string elementName)
        {
            GetLogger().Info($"Waiting for element - {elementName} text to be {textToBe}");
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, textToBe));
            return GetThis();
        }

        protected abstract T GetThis();

        protected abstract ILog GetLogger();
    }
}
