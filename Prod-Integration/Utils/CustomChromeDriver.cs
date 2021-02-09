using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Chrome;
namespace CCC_Utils.CustomDrivers
{
    public class CustomChromeDriver : SeleniumWebDriver
    {
        public CustomChromeDriver(ChromeOptions options)
            : base(CustomProfileDriver(options), Browser.Chrome)
        {
        }

        public static ChromeDriver CustomProfileDriver(ChromeOptions options)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            return new ChromeDriver(chromeOptions);
        }
    }
}