using BrowserStack;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using BrowserStack;

namespace ShoppingCart.Pages
{
    public class BrowserStackDriver
    {
        private IWebDriver driver;
        private Local browserStackLocal;
        private string profile;
        private string environment;
        private ScenarioContext context;

        public BrowserStackDriver(ScenarioContext context)
        {
            this.context = context;
        }
        public void changeSessionStatus()
        {
            string reqString = "{\"status\":\"completed\", \"reason\":\"\"}";

            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            Uri myUri = new Uri(string.Format("https://www.browserstack.com/automate/sessions/<session-id>.json"));
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myWebRequest.ContentType = "application/json";
            myWebRequest.Method = "PUT";
            myWebRequest.ContentLength = requestData.Length;
            using (Stream st = myWebRequest.GetRequestStream()) st.Write(requestData, 0, requestData.Length);
            string username = ConfigurationManager.AppSettings.Get("user");
            string userkey = ConfigurationManager.AppSettings.Get("key");

            NetworkCredential myNetworkCredential = new NetworkCredential(username, userkey);
            CredentialCache myCredentialCache = new CredentialCache();
            myCredentialCache.Add(myUri, "Basic", myNetworkCredential);
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Credentials = myCredentialCache;

            myWebRequest.GetResponse().Close();
        }
        public IWebDriver Init()
        {
            NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;

            DesiredCapabilities capability = new DesiredCapabilities();

            if (caps != null)
            {


                foreach (string key in caps.AllKeys)
                {
                    capability.SetCapability(key, caps[key]);
                }
            }
            if (settings != null)
            {
                foreach (string key in settings.AllKeys)
                {
                    capability.SetCapability(key, settings[key]);
                }
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            capability.SetCapability("browserstack.user", username);
            capability.SetCapability("browserstack.key", accesskey);
            capability.SetCapability("name", "Bstack-[SpecFlow] Sample Test");

            File.AppendAllText("C:\\Users\\savenkatraman\\Desktop\\sf.log", "Starting local");

            if (capability.GetCapability("browserstack.local") != null && capability.GetCapability("browserstack.local").ToString() == "true")
            {
                browserStackLocal = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
        new KeyValuePair<string, string>("key", accesskey)
      };
                browserStackLocal.start(bsLocalArgs);
            }
            changeSessionStatus();

            File.AppendAllText("C:\\Users\\Admin\\Desktop\\sf.log", "Starting driver");
            driver = new RemoteWebDriver(new Uri("https://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capability);
            return driver;
        }

        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                if (browserStackLocal != null)
                {
                    browserStackLocal.stop();
                }
            }
        }
    }

}
