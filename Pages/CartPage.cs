using ShoppingCart.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace ShoppingCart.Pages
{
    class CartPage : Constants
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool _result = false;
        IWebElement _checkItem => driver.FindElement(By.XPath("//*[text()='" + item1 + "']"));

        IWebElement _checkItem2 => driver.FindElement(By.XPath("//*[text()='" + item2 + "']"));

        IWebElement _deleteBtn => driver.FindElement(By.XPath("//*[contains(@d,'M9 2a1 1 0 00-.894.553L7.382')]"));

        IWebElement _btnClear => driver.FindElement(By.XPath("//*[text()='CLEAR']"));

        IWebElement _cartEmpty => driver.FindElement(By.XPath("//*[text()='Your cart is empty']"));

        IWebElement _totalItem => driver.FindElement(By.XPath("//*[@class=' mb-3 txt-right']"));
        
        IWebElement _totalPayment => driver.FindElement(By.XPath("//*[@class='m-0 txt-right']"));

        IWebElement _increaseItem => driver.FindElement(By.CssSelector(".border-0 .btn-primary"));

        IWebElement _decreaseItem => driver.FindElement(By.XPath("//*[contains(@d,'M10 18a8 8 0 100-16 8 8 0 000 16zM7 9a1')]"));

        IWebElement _checkoutBtn => driver.FindElement(By.XPath("//*[text()='CHECKOUT']"));

        IWebElement _checkoutMessage => driver.FindElement(By.XPath("//*[text()='Checkout successfull']"));

        public bool verifyCart() => _checkItem.Displayed;

        public bool verifyDelete() => _deleteBtn.Displayed;

        public void clickClearBtn() => _btnClear.Click();

        public void clickDeleteItem() => _deleteBtn.Click();

        public void clickCheckout() => _checkoutBtn.Click();

        public bool verifyDecreaseBtn() => _decreaseItem.Displayed;

        public void DecreaseItem() {
            Actions a = new Actions(driver);
            a.MoveToElement(_decreaseItem).Click(_decreaseItem).Perform();
        } 

        public void verifyCartEmpty()
        {
            Assert.AreEqual(yourCartEmpty.ToLower(), _cartEmpty.Text.ToLower());
        }

        public void verifyTotalItem(int totalItem)
        {
            Assert.AreEqual(totalItem.ToString(), _totalItem.Text);
        }

        public void verifyTotalPayment(string totalPayment)
        {
            Assert.AreEqual(totalPayment, _totalPayment.Text);
        }

        public void increaseItem(int number)
        {
            for (int i = 1; i < number; i++) {
                Actions a = new Actions(driver);
                a.MoveToElement(_increaseItem).Click(_increaseItem).Perform();
            }
        }        

        public void verifyCheckoutSuccessfully()
        {
            Assert.AreEqual(checkoutMessage.ToLower(), _checkoutMessage.Text.ToLower());
        }

        public void verifyCartWithBothItem()
        {
            Assert.IsTrue(_checkItem.Displayed);
            Assert.IsTrue(_checkItem2.Displayed);
        }
    }
}
