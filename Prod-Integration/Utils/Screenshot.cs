using BoDi;
using Coypu;
using System;
using System.IO;
using Zukini.UI;

namespace Prod_Integration.Utils
{
    public class Screenshot
    {
        public static void TakeScreenshot(string fileName, IObjectContainer objContainer, BrowserSession browser)
        {
            try
            {
                var ZukiniConfig = objContainer.Resolve<ZukiniUIConfiguration>() ?? new ZukiniUIConfiguration();
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), ZukiniConfig.ScreenshotDirectory);
                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                var screenshotFilePath = Path.Combine(artifactDirectory, fileName);
                browser.SaveScreenshot(screenshotFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                // TODO: Add to the report transform to interpret this as a link (XSLT - yuck)
                Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }
    }
}
