using BoDi;
using Prod_Integration.Pages.CCC.Media.Contacts;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.Contacts
{
    [Binding]
    public class ContactSearchSteps : UISteps
    {
        private readonly ContactSearchPage _page;

        public ContactSearchSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new ContactSearchPage(Browser);
        }

        [When(@"I search for the contact")]
        public void WhenISearchForTheContact()
        {
            var contact = PropertyBucket.GetProperty<string>("contact");
            _page.NameSearchTextbox().SendKeys(contact);
            Browser.WaitUntil(() => _page.NameSearchOptions().Count() > 0, "Options failed to load");
            var split = contact.Split(' ');
            var name = $"{split.GetValue(1)}, {split.GetValue(0)}";
            _page.NameSearchOptions().First(o => o.Text.StartsWith(name)).Click();
            _page.SearchButton().Click();
        }

        [When(@"I search for Influencers with the term '(.*)'")]
        public void WhenISearchForInfluencersWithTheTerm(string term)
        {
            var optionGroup = _page.CriteriaDropdownOptionGroups().First(e => e.Text.Contains("Talking About"));
            optionGroup.SelectOption("influencerKeyword");
            _page.TalkingAboutSearchTextbox().FillInWith(term);
            _page.SearchButton().Click();
        }
    }
}
