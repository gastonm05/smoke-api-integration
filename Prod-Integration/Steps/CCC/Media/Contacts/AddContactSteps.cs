using BoDi;
using Prod_Integration.Pages.CCC.Media.Contacts;
using Prod_Integration.Utils;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.Contacts
{
    [Binding]
    public class AddContactSteps : UISteps
    {
        private readonly AddContactPage _page;

        public AddContactSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new AddContactPage(Browser);
        }

        [When(@"I Create a Private Contact with the twitter handle '(.*)'")]
        public void WhenICreateAPrivateContactWithTheTwitterHandle(string twitter)
        {
            var last = $"Tester{StringUtils.RandomAlphaNumericString(5)}";
            var first = "QA";     
            _page.CreatePrivateContact(first, last, "Chicago Online", "United States", twitter);
            PropertyBucket.Remember("contact", $"{first} {last}");
            Browser.WaitUntil(() => _page.SaveButton().Missing(), "Add Contact Modal still displayed");
        }
    }
}
