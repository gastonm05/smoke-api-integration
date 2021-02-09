using CCC_Utils.CustomDrivers;
using Coypu;
using Coypu.Drivers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace Prod_Integration.Utils
{
    public class BrowserFactory
    {
        /// <summary>
        /// Helper method to retrieve the browser based off of the string that is passed in.
        /// </summary>
        /// <param name="browserName">Name of the browser.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Browser GetBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    return Browser.Firefox;
                case "chrome":
                    return Browser.Chrome;
                case "ie":
                case "internetexplorer":
                    return Browser.InternetExplorer;
                default:
                    throw new ArgumentException(String.Format("Specified browserName '{0}' is not valid.", browserName));
            }
        }

        /// <summary>
        /// Creates a browser session on either the grid or locally depending on where the test is being run
        /// </summary>
        /// <param name="sessionConfiguration">The session configuration.</param>
        /// <param name="executeOnGrid">If true, create a remote browser on the grid.</param>
        /// <returns></returns>
        public static BrowserSession GetBrowserSession(SessionConfiguration sessionConfiguration, bool executeOnGrid)
        {
            return executeOnGrid ?
                   RegisterCustomRemoteChromeBrowser(sessionConfiguration) :
                   RegisterCustomChromeBrowser(sessionConfiguration);
        }

        /// <summary>
        /// Registers the custom chrome browser.
        /// </summary>
        private static BrowserSession RegisterCustomChromeBrowser(SessionConfiguration sessionConfiguration)
        {
            // Create chrome options and add any/all arguments
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--start-maximized");

            // Pass options to a new remote chrome browser and pass into the BrowserSession
            var customRemoteChromeDriver = new CustomChromeDriver(options);
            return new BrowserSession(sessionConfiguration, customRemoteChromeDriver);
        }

        /// <summary>
        /// Configures a custom remote Chrome driver        
        /// </summary>
        private static BrowserSession RegisterCustomRemoteChromeBrowser(SessionConfiguration sessionConfiguration)
        {
            // Create chrome options and add any/all arguments
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--start-maximized");

            // Must cast options to DesiredCapabilities due to issue with .net and driver
            // https://github.com/seleniumhq/selenium-google-code-issue-archive/issues/7043
            var capabilities = (DesiredCapabilities)options.ToCapabilities();
            capabilities.SetCapability("browserName", "chrome");

            // Pass options to a new remote chrome browser and pass into the BrowserSession
            var customRemoteChromeDriver = new CustomRemoteChromeSeleniumDriver(capabilities);
            return new BrowserSession(sessionConfiguration, customRemoteChromeDriver);
        }
    }
}
