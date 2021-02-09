using BoDi;
using NUnit.Framework;
using Prod_Integration.Pages.CCC.News;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.News
{
    [Binding]
    public class MyCoverageSteps : UISteps
    {

        private readonly MyCoveragePage _page;

        public MyCoverageSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new MyCoveragePage(Browser);
        }

        [When(@"I select all coverage items")]
        public void WhenISelectAllCoverageItems()
        {
            _page.SelectAllCheckbox().Click();
        }

        [When(@"I click the Analyze button")]
        public void WhenIClickTheAnalyzeButton()
        {
            _page.ClickBulkAction("analyze");
        }

        [When(@"I click the Save Search Button")]
        public void WhenIClickTheSaveSearchButton()
        {
            _page.LoadingDone();
            _page.SavedSearchButton().Click();
        }
        
        [Then(@"I should see the My Coverage page")]
        public void ThenIShouldSeeTheMyCoveragePage()
        {
            _page.AssertCurrentPage();
        }

        [Then(@"I should see the search on My Coverage Page")]
        public void ThenIShouldSeeTheSearchOnMyCoveragePage()
        {
            _page.LoadingDone();
            var name = PropertyBucket.GetProperty<string>("Save Search Name");
            Browser.Refresh();
            Browser.WaitUntil(() => _page.SavedSearches().Count() > 0, "Save Searches failed to load");
            _page.LoadingDone();
            Assert.That(_page.SavedSearches().Any(s => s.Title.Equals(name)), Is.True);
            _page.DeleteSavedSearch(name);
            Browser.WaitUntil(() => !_page.SavedSearches().Any(s => s.Title.Equals(name)), "Saved Search not deleted");
            _page.LoadingDone();
        }

        [Then(@"I should see the news item")]
        public void ThenIShouldSeeTheNewsItem()
        {
            Assert.True(_page.IsLoaded(), "News items failed to load");
        }

    }
}
