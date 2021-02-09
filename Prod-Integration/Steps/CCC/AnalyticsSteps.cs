using BoDi;
using Prod_Integration.Pages.CCC.Analytics;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC
{
    [Binding]
    public class AnalyticsSteps : UISteps
    {
        private AnalyticsPage _page;

        public AnalyticsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new AnalyticsPage(Browser);
        }

        [Then(@"I should see the Analytics page")]
        public void ThenIShouldSeeTheAnalyticsPage()
        {
            Browser.WaitUntil(() => _page.ChartSections().Count() > 0, "Analytics charts did not display");
        }

    }
}
