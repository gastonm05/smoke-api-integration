using System;
using Coypu;
using Coypu.Drivers;
using TechTalk.SpecFlow;
using Prod_Integration.Utils;
using BoDi;
using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using System.Collections.Generic;
using System.Reflection;
using Zukini;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;
    private readonly SessionConfiguration _sessionConfiguration;

    public Hooks(SessionConfiguration sessionConfig, IObjectContainer objectContainer)
    {
        _sessionConfiguration = sessionConfig;
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _sessionConfiguration.Browser = Browser.Chrome;
        _sessionConfiguration.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(TestSettings.GetConfigValue("Timeout")));
        _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(Convert.ToDouble(TestSettings.GetConfigValue("RetryInterval")));

        // grid or local browser session
        BrowserSession browser = BrowserFactory.GetBrowserSession(_sessionConfiguration, TestSettings.IsOnGrid);

        if (!TestSettings.IsOnGrid)
        {
            browser.MaximiseWindow();
        }

        // register with the DI container.
        _objectContainer.RegisterInstanceAs<BrowserSession>(browser);
    }

    /// <summary>
    /// Method to execute before the test suite executes. Populates the UserList with
    /// users from the project's Users.json file
    /// </summary>
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        UserList.PopulateUserList(TestData.GetDeserializedJsonData<List<User>>("Users.json", Assembly.GetExecutingAssembly()));
    }

    /// <summary>
    /// Method to execute after each scenario. Releases the scenario user from the UserList [InUse = false].
    /// </summary>
    [AfterScenario]
    public void AfterScenario()
    {
        var propertyBucket = _objectContainer.Resolve<PropertyBucket>();

        if (propertyBucket.ContainsKey("scenario user"))
        {
            var scenarioUser = propertyBucket.GetProperty<User>("scenario user");
            UserList.ReleaseListUser(scenarioUser);
        }
        else
        {
            // Nothing to do here, intentionally left empty. If we get here there is no user in the property bucket
            // We should not fail the test but continue to execute and let Assertion determine pass/fail
        }
    }
}
