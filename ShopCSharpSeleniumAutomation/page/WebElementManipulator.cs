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
            GetLogger().Info($"Hovering to element - {elementName}");
            actions.MoveToElement(element).Perform();
            return GetThis();
        }

        protected T SendKeys(IWebElement element, string elementName, string keysToSend)
        {
            GetLogger().Info($"Sending keys: {keysToSend} to element - {elementName}");
            element.SendKeys(keysToSend);
            return GetThis();
        }

        protected T SelectDropdownByVisibleText(IWebElement selectElement, string elementName, string text)
        {
            GetLogger().Info($"Selecting dropdown by visible text: {text} in element - {elementName}");
            new SelectElement(selectElement).SelectByText(text);
            return GetThis();
        }

        protected T ClickElementAndWaitToBeVisible(IWebElement toClick, string toClickElementName, IWebElement toBeVisible, string toBeVisibleElementName)
        {
            GetLogger().Info($"Clicking element - {toClickElementName}");
            toClick.Click();
            WaitForElementToBeVisible(toBeVisible, toBeVisibleElementName);
            return GetThis();
        }

        protected T ClickElementAndWaitToBeInvisible(IWebElement toClick, string toClickElementName, IWebElement toBeInvisible, string toBeInvisibleElementName)
        {
            GetLogger().Info($"Clicking element - {toClickElementName}");
            toClick.Click();
            WaitForElementToBeInvisible(toBeInvisible, toBeInvisibleElementName);
            return GetThis();
        }

        protected T ClickElement(IWebElement element, string elementName)
        {
            GetLogger().Info($"Clicking element - {elementName}");
            element.Click();
            return GetThis();
        }
    }
}
