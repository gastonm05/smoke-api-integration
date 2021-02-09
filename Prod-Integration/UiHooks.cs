using BoDi;
using Coypu;
using System;
using System.IO;
using TechTalk.SpecFlow;
using Zukini;
using Zukini.UI;

[Binding]
public class UiHooks : Zukini.Hooks
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UIHooks" /> class.
    /// </summary>
    /// <param name="objectContainer">The object container (Injected with DI).</param>
    /// <param name="scenarioContext">The current ScenarioContext (Injected with DI).</param>
    /// <param name="featureContext">The current FeatureContext (Injected with DI).</param>
    public UiHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext,
        FeatureContext featureContext)
        : base(objectContainer, scenarioContext, featureContext) { }

    /// <summary>
    /// Global After Scenario hook used to take a screenshot (if there is an error) 
    /// and shuts down the driver.
    /// </summary>
    [AfterScenario]
    protected virtual void AfterUiScenario()
    {
        var browser = ObjectContainer.Resolve<BrowserSession>();
        if (browser != null)
        {
            if (this.ScenarioContext.TestError != null)
            {
                TakeScreenshot(browser);
            }

            browser.Dispose();
        }
    }

    /// <summary>
    /// Returns the ZukiniConfiguration if one was registered, otherwise returns 
    /// a new ZukiniConfiguration with default settings.
    /// </summary>
    private ZukiniUIConfiguration ZukiniConfig =>
        ObjectContainer.Resolve<ZukiniUIConfiguration>() ?? new ZukiniUIConfiguration();

    /// <summary>
    /// Helper method to take a screenshot of the browser and save out to the TestResults folder.
    /// </summary>
    /// <param name="browser">BrowserSession to use for taking screenshot.</param>
    protected virtual void TakeScreenshot(BrowserSession browser)
    {
        try
        {
            var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), ZukiniConfig.ScreenshotDirectory);
            if (!Directory.Exists(artifactDirectory))
            {
                Directory.CreateDirectory(artifactDirectory);
            }

            var screenshotFilePath = Path.Combine(artifactDirectory, GetScreenshotName());
            browser.SaveScreenshot(screenshotFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            // TODO: Add to the report transform to interpret this as a link (XSLT - yuck)
            Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while taking screenshot: {0}", ex);
        }
    }

    /// <summary>
    /// Constructs the name of the screenshot based on the feature title, scenario title
    /// and test id.
    /// </summary>
    protected virtual string GetScreenshotName()
    {
        var feature = this.FeatureContext.FeatureInfo.Title.Replace(" ", "");
        var title = this.ScenarioContext.ScenarioInfo.Title.Replace(" ", "");
        var propertyBucket = ObjectContainer.Resolve<PropertyBucket>();

        var name = $"{feature}_{title}_{propertyBucket.TestId}.png";
        // Replace bad chars with
        var finalName = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
        return finalName;
    }
}