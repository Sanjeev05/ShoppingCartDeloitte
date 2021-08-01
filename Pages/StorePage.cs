using ShoppingCart.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Pages
{
    class StorePage : Constants
    {
        private IWebDriver driver;

        public StorePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement _btnItem1 => driver.FindElement(By.XPath("//*[text()='" + item1 + "']//following::button"));

        IWebElement _btnItem2 => driver.FindElement(By.XPath("//*[text()='" + item2 + "']//following::button"));

        IWebElement _item1Cost => driver.FindElement(By.XPath("//*[text()='" + item1 + "']//following::h3"));

        IWebElement _item2Cost => driver.FindElement(By.XPath("//*[text()='" + item2 + "']//following::h3"));

        IWebElement _btnCart => driver.FindElement(By.XPath("//*[text()=' Cart (']"));

        public void selectOneItem() => _btnItem1.Click();

        public void selectSecondItem() => _btnItem2.Click();

        public void clickOnCart() => _btnCart.Click();

        public string getItem1Cost() => _item1Cost.Text;

        public string getItem2Cost() => _item2Cost.Text;
    }
}
