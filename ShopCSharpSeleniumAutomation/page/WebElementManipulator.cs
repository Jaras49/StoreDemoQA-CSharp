using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ShopCSharpSeleniumAutomation.page
{
    public abstract class WebElementManipulator<T>: BasePage<T> where T: WebElementManipulator<T>
    {
        protected WebElementManipulator(IWebDriver driver, WebDriverWait wait, Actions actions)
            :base(driver, wait, actions) { }

        protected T HoverOverElement(IWebElement element, string elementName)
        {
            Logger.Info($"Hovering to element - {elementName}");
            actions.MoveToElement(element).Perform();
            return This;
        }

        protected T SendKeys(IWebElement element, string elementName, string keysToSend)
        {
            Logger.Info($"Sending keys: {keysToSend} to element - {elementName}");
            element.SendKeys(keysToSend);
            return This;
        }

        protected T SelectDropdownByVisibleText(IWebElement selectElement, string elementName, string text)
        {
            Logger.Info($"Selecting dropdown by visible text: {text} in element - {elementName}");
            new SelectElement(selectElement).SelectByText(text);
            return This;
        }

        protected T ClickElementAndWaitToBeVisible(IWebElement toClick, string toClickElementName, IWebElement toBeVisible, string toBeVisibleElementName)
        {
            Logger.Info($"Clicking element - {toClickElementName}");
            toClick.Click();
            WaitForElementToBeVisible(toBeVisible, toBeVisibleElementName);
            return This;
        }

        protected T ClickElementAndWaitToBeInvisible(IWebElement toClick, string toClickElementName, IWebElement toBeInvisible, string toBeInvisibleElementName)
        {
            Logger.Info($"Clicking element - {toClickElementName}");
            toClick.Click();
            WaitForElementToBeInvisible(toBeInvisible, toBeInvisibleElementName);
            return This;
        }

        protected T ClickElement(IWebElement element, string elementName)
        {
            Logger.Info($"Clicking element - {elementName}");
            element.Click();
            return This;
        }
    }
}
