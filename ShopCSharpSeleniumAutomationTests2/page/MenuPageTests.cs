using NUnit.Framework;
using ShopCSharpSeleniumAutomation.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ShopCSharpSeleniumAutomationTests2;
using System.Threading;
using System.Diagnostics;

namespace ShopCSharpSeleniumAutomation.page.Tests
{
    public class MenuPageTests: BaseTest
    {
        [Test]
        public void MenuPageTest()
        {
            menu.GoToCartPage();
            Thread.Sleep(15);
            Assert.Pass();
        }
    }
}