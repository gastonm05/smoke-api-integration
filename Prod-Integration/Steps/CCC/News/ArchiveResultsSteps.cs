using BoDi;
using Coypu;
using Prod_Integration.Pages.CCC.News;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.News
{
    [Binding]
    public class ArchiveResultsSteps : UISteps
    {
        private readonly ArchiveResultsPage _page;

        public ArchiveResultsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new ArchiveResultsPage(Browser);
        }

        [When(@"I select the first news item")]
        public void WhenISelectTheFirstNewsItem()
        {
            Browser.WaitUntil(() => _page.NewsItems().Count() > 0, "News Items failed to load");
            var headline = Browser.FindCss("h2").Text;
            PropertyBucket.Remember("headline", headline);
            var element = _page.NewsItems().ElementAt(0);
            element.FindCss("div>div>ci-icon-checkbox>label>i").Click();
        }

        [When(@"I click the Add To My Coverage button")]
        public void WhenIClickTheAddToMyCoverageButton()
        {
            _page.AddToCoverageButton().Click();
        }

        [When(@"I click Ok on the Add to My Coverage popup")]
        public void WhenIClickOkOnTheAddToMyCoveragePopup()
        {
            Browser.WaitUntil(() => _page.AddToCoverageModalOkButton().Exists(), "Add To Coverage Modal was not displayed");
            _page.AddToCoverageModalOkButton().Click();
            Browser.WaitUntil(() => _page.AddToCoverageModalOkButton().Missing(),"Add To Coverage Modal still displayed");
        }

    }
}
