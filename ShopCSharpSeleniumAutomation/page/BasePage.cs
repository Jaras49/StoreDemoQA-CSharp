using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using System;
using ShopCSharpSeleniumAutomation.annotations;

namespace ShopCSharpSeleniumAutomation.page
{
    public abstract class BasePage<T> where T : BasePage<T>
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;

        protected abstract T This { get; }
        protected abstract ILog Logger { get; }

        protected BasePage(IWebDriver driver, WebDriverWait wait, Actions actions)
        {
            this.driver = driver;
            this.wait = wait;
            this.actions = actions;
        }

        public T AssertEquals<M>(M expected, M actual)
        {
            Assert.AreEqual(expected, actual);
            return This;
        }

        protected IWebElement WaitForElementToBeVisible(IWebElement element, string elementName)
        {
            Logger.Info($"Waiting for element - {elementName} - to become visible");
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        protected IWebElement WaitForElementToBeInvisible(IWebElement element, string elementName)
        {
            Logger.Info($"Waiting for element - {elementName} to become invisible");
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
            Logger.Info($"Waiting for element - {elementName} text to be {textToBe}");
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, textToBe));
            return This;
        }

        protected void WaitUntilPageLoads()
        {
            var elements = GetFieldsAnnotatedWithWaitForVisible();
            wait.Until((d) =>
            {
                foreach(IWebElement element in elements)
                {
                    if(!element.Displayed)
                    {
                        return false;
                    }
                }
                return true;
            });
        }

        private IList<IWebElement> GetFieldsAnnotatedWithWaitForVisible()
        {
            IList<IWebElement> result = new List<IWebElement>();
            var fields = This.GetType().GetRuntimeFields();
            foreach(FieldInfo field in fields)
            {
                if (field.FieldType == typeof(IWebElement) && Attribute.IsDefined(field, typeof(WaitUntilVisible)))
                {
                    result.Add((IWebElement) field.GetValue(this));
                }
            }
            return result;
        }
    }
}