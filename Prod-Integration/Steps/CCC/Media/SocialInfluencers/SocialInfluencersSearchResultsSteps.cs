using BoDi;
using CCC_Infrastructure.Utils;
using NUnit.Framework;
using Prod_Integration.Pages.CCC.Media.SocialInfluencers;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.SocialInfluencers
{
    [Binding]
    public class SocialInfluencersSearchResultsSteps : UISteps
    {
        private readonly SocialInfluencersSearchResultsPage _page;

        public SocialInfluencersSearchResultsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new SocialInfluencersSearchResultsPage(Browser);
        }

        [Then(@"I should see relevant results returned")]
        public void ThenIShouldSeeRelevantResultsReturned()
        {
            Browser.WaitUntil(() => _page.SocialInfluencersMDVNames().Count() > 0, "Results were not displayed");
            Assert.That(_page.SocialInfluencersMDVNames().Count() > 0, "No results returned");

            int rowNumber = 0;
            Browser.TryUntil(
                () => { _page.SocialInfluencersMDVNames().ElementAt(rowNumber).Click(); ++rowNumber; },
                () => _page.RecenTweets().Count() > 0,
                TimeSpan.FromSeconds(5));
            
            Assert.That(_page.RecenTweets().Count() > 0, "Recents tweets were not displayed");
            Assert.That(_page.SubjectsChart().Exists(), "Subjects chart is not displayed");
        }

        [Then(@"transparency text should have '(.*)' in the content")]
        public void ThenTransparencyTextShouldHaveInTheContent(string subject)
        {
            Assert.That(_page.TransparencyText().Text.ToLower().Contains(subject.ToLower()), "transparency text is not showing the correct content");
        }


    }
}
