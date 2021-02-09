using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Remote;
using System;
using Utils;

namespace Prod_Integration.Utils
{
    public class CustomRemoteChromeSeleniumDriver : SeleniumWebDriver
    {
        public CustomRemoteChromeSeleniumDriver(DesiredCapabilities capabilities)
            : base(CustomProfileDriver(capabilities), Browser.Chrome)
        {
        }

        public static RemoteWebDriver CustomProfileDriver(DesiredCapabilities capabilities)
        {
            Uri uri = new Uri(TestSettings.GetConfigValue("GridUrl"));
            try
            {
                return new RemoteWebDriver(uri, capabilities);
            }
            catch (OpenQA.Selenium.WebDriverException)
            {
                // sometimes RemoteWebDriver calls fail on the Grid with the following error message:
                // OpenQA.Selenium.WebDriverException : The HTTP request to the remote WebDriver server for URL http://qaselhub1.na1.ad.group:5555/wd/hub/session timed out after 60 seconds.
                // as a workaround, try try again
                return new RemoteWebDriver(uri, capabilities);
            }
        }
    }
}
