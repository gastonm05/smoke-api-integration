using BoDi;
using Prod_Integration.Pages.CCC.Media.SocialInfluencers;
using TechTalk.SpecFlow;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Media.SocialInfluencers
{
    [Binding]
    public class SocialInfluencerSearchSteps : UISteps
    {

        private readonly SocialInfluencerSearchPage _page;

        public SocialInfluencerSearchSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new SocialInfluencerSearchPage(Browser);
        }

        [When(@"I perform a search by subject : '(.*)'")]
        public void WhenIPerformASearchBySubject(string term)
        {
            _page.SearchMediaSocialBySubject(term);
        }


    }
}
