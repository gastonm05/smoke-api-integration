using BoDi;
using Coypu;
using NUnit.Framework;
using Prod_Integration.Pages.CCC.Media.Contacts;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.Contacts
{
    [Binding]
    public class ContactProfileSteps : UISteps
    {

        private readonly ContactProfilePage _page;

        public ContactProfileSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new ContactProfilePage(Browser);
        }

        [Then(@"the contact profile should display with a twitter stream")]
        public void ThenTheContactProfileShouldDisplayWithATwitterStream()
        {
            var contact = PropertyBucket.GetProperty<string>("contact");
            Browser.WaitUntil(() => _page.LoadingDots.Missing(), $"Profile for '{contact}' was still loading", new Options() { Timeout = TimeSpan.FromMinutes(2) });
            Browser.WaitUntil(() => _page.Tweets().Count() > 0, $"Profile for '{contact}' had no tweets");
            Assert.That(_page.Tweets().Count(), Is.GreaterThan(0), $"No tweets found for '{contact}'");
        }

        [Then(@"the contact's profile displays")]
        public void ThenTheContactSProfileDisplays()
        {
            var contact = PropertyBucket.GetProperty<string>("contact");
            Browser.WaitUntil(() => _page.HistoryPanelTabHeaders().Count() > 0, $"Profile page for '{contact}' did not load");
            Assert.False(string.IsNullOrWhiteSpace(_page.ContactName().Text), $"Contact name for '{contact}' not displayed");
            Assert.False(string.IsNullOrWhiteSpace(_page.HistoryPanel().Text), $"History Panel for '{contact}' not displayed");
            Assert.True(_page.PitchingProfile().Exists(), $"Pitching Profile for '{contact}' not displayed");
        }

    }
}
