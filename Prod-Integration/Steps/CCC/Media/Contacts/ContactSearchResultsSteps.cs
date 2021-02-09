using BoDi;
using Prod_Integration.Pages.CCC.Media.Contacts;
using Prod_Integration.Utils;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.Contacts
{
    [Binding]
    public class ContactSearchResultsSteps : UISteps
    {

        private readonly ContactSearchResultsPage _page;

        public ContactSearchResultsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new ContactSearchResultsPage(Browser);
        }

        [When(@"I open the first contact's profile")]
        public void WhenIOpenTheFirstContactSProfile()
        {
            _page.ViewContactButton().Click();
        }

        [When(@"I select the first contact")]
        public void WhenISelectTheFirstContact()
        {
            _page.WaitToLoad();
            _page.LoadingDone();
            _page.SelectCheckBoxesByIndex(1);
        }

        [When(@"I create a new list '(.*)'")]
        public void WhenICreateANewList(string name)
        {
            var listName = $"{name}{StringUtils.RandomAlphaNumericString(5)}";
            PropertyBucket.Remember("list name", listName);
            _page.CreateNewList(listName);
            Browser.WaitUntil(() => _page.CreateListModal().Missing(), "Create List modal still displayed");
        }

        [Then(@"I should see Influencer results")]
        public void ThenIShouldSeeInfluencerResults()
        {
            Browser.WaitUntil(() => _page.MasterDetailListItems().Count() > 0, "Influencer results failed to load");
        }

        [Then(@"I should see contacts on the list")]
        public void ThenIShouldSeeContactsOnTheList()
        {
            Browser.WaitUntil(() => _page.MasterDetailListItems().Count() > 0, "List contents not displayed");
        }

    }
}
