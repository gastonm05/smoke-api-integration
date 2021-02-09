using BoDi;
using Coypu;
using Prod_Integration.Pages.CCC.Common;
using System;
using TechTalk.SpecFlow;
using Utils;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Common
{
    [Binding]
    public class NavigationSteps : UISteps
    {

        private readonly SessionConfiguration _sessionConfig;

        public NavigationSteps(IObjectContainer objectContainer, SessionConfiguration sessionConfig) : base(objectContainer)
        {
            _sessionConfig = sessionConfig;
        }

        /// <summary>
        /// Navigates to the specified URI by url manipulation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        private void Navigate(string uri)
        {
            var url = $"{TestSettings.GetConfigValue("CCCBaseUrl")}{uri}";
            Browser.WaitForNavigation(_sessionConfig, url);
        }

        [When(@"I search for news with a start date of '(.*)'")]
        public void WhenISearchForNewsWithAStartDateOf(DateTime date)
        {
            var newDate = date.ToString("yyyy-MM-dd");
            Navigate($"news/search?startDate={newDate}");
        }

        [When(@"I search the archive for news with the keyword '(.*)' and a start date of '(.*)' and an end date of '(.*)'")]
        public void WhenISearchTheArchiveForNewsWithTheKeywordAndAStartDateOfAndAnEndDateOf(string keyword, DateTime start, DateTime end)
        {
            var startDate = start.ToString("yyyy-MM-dd");
            var endDate = end.ToString("yyyy-MM-dd");
            Navigate($"news/archivesearch?keywords={keyword}&startDate={startDate}&endDate={endDate}");
        }

        [When(@"I go to My Coverage Page")]
        public void WhenIGoToMyCoveragePage()
        {
            new CisionHeader(Browser).NavigateTo("NEWS", "MY COVERAGE");
        }

        [When(@"I select '(.*)' from the '(.*)' navigation menu")]
        public void WhenISelectFromTheNavigationMenu(string sub, string main)
        {
            new CisionHeader(Browser).NavigateTo(main, sub);
        }

        [When(@"I navigate to contact search page")]
        public void WhenINavigateToContactSearchPage()
        {
            Navigate("media/search/contact?reset=0");
        }

        [When(@"I navigate to the activities page")]
        public void WhenINavigateToTheActivitiesPage()
        {
            Navigate("activities");
        }

        [When(@"I navigate to contact search results with the key '(.*)' and the value '(.*)'")]
        public void WhenINavigateToContactSearchResultsWithTheKeyAndTheValue(string key, string value)
        {
            //Url encoding needed for UI = [{"key":"key","value":"value"}]
            string url = $"%5B%7B%22key%22:%22{key}%22,%22value%22:%22{value}%22%7D%5D";
            Navigate("media/searchresults/contact/" + url);
        }

        [When(@"I navigate to the list page")]
        public void WhenINavigateToTheListPage()
        {
            var value = PropertyBucket.GetProperty("list name");
            //Url encoding needed for UI = [{"key":"key","value":"value"}]
            string url = $"%5B%7B%22key%22:%22listname%22,%22value%22:%22{value}%22%7D%5D";
            Navigate("media/searchresults/contact/" + url);
        }

        [When(@"I navigate to the Social Influencers search page")]
        public void WhenINavigateToTheSocialInfluencersSearchPage()
        {
            Navigate("media/search/contact");
        }


    }
}
