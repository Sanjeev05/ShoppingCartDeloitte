using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using BrowserStack;

namespace ShoppingCart.Pages
{
    [Binding]
    public sealed class BrowserStack
    {
        private BrowserStackDriver bsDriver;
        private string[] tags;

        [BeforeScenario]
        public void BeforeScenario()
        {

            bsDriver = new BrowserStackDriver(ScenarioContext.Current);
            ScenarioContext.Current["bsDriver"] = bsDriver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            bsDriver.Cleanup();
        }
    }

}
