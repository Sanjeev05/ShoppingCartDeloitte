using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using ShoppingCartTest.Model;
using ShoppingCart.Model;


namespace ShoppingCart.Base
{
    [Binding]
    class Initialize : Constants
    {
        public Setting _driver;
        public Initialize(Setting driver) => _driver = driver;


        [BeforeScenario]
        public void InitializeBrowser()
        {            
            _driver.Driver = new ChromeDriver();
            _driver.Driver.Manage().Window.Maximize();
            _driver.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        } 

        [AfterScenario]
        public void TearDown()
        {
            _driver.Driver.Quit();
        } 
    }
}
