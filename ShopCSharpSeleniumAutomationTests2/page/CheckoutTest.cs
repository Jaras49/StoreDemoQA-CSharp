using NUnit.Framework;
using System;
using System.Collections.Generic;
using ShopCSharpSeleniumAutomationTests2;
using ShopCSharpSeleniumAutomation.factory;
using ShopCSharpSeleniumAutomation.model;
using ShopCSharpSeleniumAutomation.page.category;
using ShopCSharpSeleniumAutomation.page.cart;
using System.Diagnostics;

namespace ShopCSharpSeleniumAutomation.page.Tests
{
    public class CheckoutTest : BaseTest
    {
        private Order expectedOrder;

        [Test]
        public void MenuPageTest()
        {
            expectedOrder = new Order(new List<Product>());

            for (var i = 0; i < 4; i++)
            {
                AddRandomProduct();
            }
            CheckoutPage checkoutPage = menu.GoToCartPage()
                .AssertCart(expectedOrder)
                .ClickContinueButton()
                .FillFormWithUserDetails(UserFactory.CreateRandomUser());

            expectedOrder.ShippingPrice = checkoutPage.GetShippingCost();
            checkoutPage.AssertCosts(expectedOrder)
                .ClickPurchaseButton()
                .AssertTransactionSummaryPage(expectedOrder);
        }

        private ProductPage AddRandomProduct()
        {
            var random = new Random().Next(1, 5);
            Debug.WriteLine($"Adding {random} random products");
            menu = PageObjectFactory.CreateMenuPage(driver);
            ProductPage productPage = menu.GoToRandomCategory()
                .GoToRandomProductPageAndAssertItSwitchedCorrectly();
            AddToExpectedOrder(productPage.GetProductName(), productPage.GetProductPrice(), random);

            return productPage.AddProductXtimes(random);

        }

        private void AddToExpectedOrder(string productName, decimal price, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                expectedOrder.AddProduct(new Product(productName, price));
            }
        }
    }
}