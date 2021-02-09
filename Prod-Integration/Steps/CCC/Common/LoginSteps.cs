using BoDi;
using CCC_Infrastructure.UserSupport;
using Prod_Integration.Pages.CCC.Common;
using TechTalk.SpecFlow;
using Utils;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Common
{
    [Binding]
    public class LoginSteps : UISteps
    {

        private LoginPage _page;

        public LoginSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new LoginPage(Browser);
        }

        [Given(@"I go to CCC")]
        public void GivenIGoToCCC()
        {
            Browser.Visit(TestSettings.GetConfigValue("CCCBaseURL"));
        }

        [When(@"I login as '(.*)'")]
        public void WhenILoginAs(string key)
        {
            var user = UserList.GetUser(key);
            PropertyBucket.Remember("scenario user", user);
            _page.Login(user);
        }

    }
}
