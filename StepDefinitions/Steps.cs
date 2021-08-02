using ShoppingCart.Pages;
using ShoppingCart.Model;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ShoppingCartTest.Model;
using BrowserStack;
using OpenQA.Selenium;

namespace ShoppingCartTest.StepDefinitions
{
    [Binding]
    class Steps : Constants
    {
        private Setting _driver;
        StorePage _storePage;
        CartPage _cartPage;
        readonly BrowserStackDriver _bsDriver;
        private IWebDriver webDriver;
        public Steps(Setting driver)
        {
            _driver = driver;
            _storePage = new StorePage(_driver.Driver);
            _cartPage = new CartPage(_driver.Driver);
            _bsDriver = (BrowserStackDriver)ScenarioContext.Current["bsDriver"];
        }

        [Given(@"The user is on the store page")]
        public void GivenTheUserIsOnTheStorePage()
        {
            webDriver = _bsDriver.Init();
            _driver.Driver.Navigate().GoToUrl(url);
        }

        [When(@"(.*) items are added to the cart")]
        public void WhenItemsAreAddedToTheCart(int numberOfItem)
        {
            if (numberOfItem == 1)
            {
                _storePage.selectOneItem();
            }
            else
            {
                _storePage.selectOneItem();
                _storePage.selectSecondItem();
            }
        }

        [When(@"I navigate to Cart")]
        public void WhenINavigateToCart()
        {
            _storePage.clickOnCart();
        }

        [When(@"Cart is validated")]
        public void WhenCartIsValidated()
        {
            Assert.IsTrue(_cartPage.verifyCart());
        }

        [When(@"The total value of items (.*) and payment (.*) is verified")]
        public void WhenTheTotalValueOfItemsAndPaymentIsVerified(int totalItem, string totalPayment)
        {
            _cartPage.verifyTotalItem(totalItem);            
            _cartPage.verifyTotalPayment(totalPayment);
        }

        [When(@"The total value of items and payment is verified")]
        public void WhenTheTotalValueOfItemsAndPaymentIsVerified()
        {
            _cartPage.verifyTotalItem(1);
            string itemValue = _storePage.getItem1Cost();//.Replace("$", "").Trim();
            _cartPage.verifyTotalPayment(itemValue);
        }

        [When(@"The delete button appears for the added item")]
        public void WhenTheDeleteButtonAppearsForTheAddedItem()
        {
            Assert.IsTrue(_cartPage.verifyDelete());
        }

        [When(@"The clear button is clicked")]
        public void WhenTheClearButtonIsClicked()
        {
            _cartPage.clickClearBtn();
        }

        [Then(@"The cart is cleared")]
        public void ThenTheCartIsCleared()
        {
            _cartPage.verifyCartEmpty();
        }

        [When(@"The qty is increased to (.*) for first item")]
        public void WhenTheQtyIsIncreasedToForFirstItem(int quantity)
        {
            _cartPage.increaseItem(quantity);
        }

        [When(@"Reduce and delete button for both the items are verified")]
        public void WhenReduceAndDeleteButtonForBothTheItemsAreVerified()
        {
            Assert.IsTrue(_cartPage.verifyDecreaseBtn());
            Assert.IsTrue(_cartPage.verifyDelete());
        }

        [When(@"Item (.*) is reduced by (.*) unit")]
        public void WhenItemIsReducedByUnit(int p0, int p1)
        {
            _cartPage.DecreaseItem();
        }

        [When(@"Item (.*) is deleted and validated")]
        public void WhenItemIsDeletedAndValidated(int p0)
        {
            _cartPage.clickDeleteItem();
        }

        [Then(@"The checkout functionality is verified")]
        public void ThenTheCheckoutFunctionalityIsVerified()
        {
            _cartPage.clickCheckout();
            _cartPage.verifyCheckoutSuccessfully();
        }

        [When(@"Cart is validated with both Item\.")]
        public void WhenCartIsValidatedWithBothItem_()
        {
            _cartPage.verifyCartWithBothItem();
        }

        //[When(@"The total value of items and payment is verified with both item")]
        //public void WhenTheTotalValueOfItemsAndPaymentIsVerifiedWithBothItem()
        //{
        //    _cartPage.verifyTotalItem(3);
        //    double item1 = double.Parse(_storePage.getItem1Cost().Replace("$", "").Trim());
        //    double item2 = double.Parse(_storePage.getItem2Cost().Replace("$", "").Trim());
        //    _cartPage.verifyTotalPayment((item1 + item1 + item2).ToString());
        //}


    }
}
