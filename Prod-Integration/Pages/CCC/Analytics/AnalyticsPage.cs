using CCC_Pages.Common.Pages;
using Coypu;
using System.Collections.Generic;

namespace Prod_Integration.Pages.CCC.Analytics
{
    public class AnalyticsPage : C3BasePage<AnalyticsPage>
    {
        public AnalyticsPage(BrowserSession browser) : base(browser) { }

        public IEnumerable<ElementScope> ChartSections() => Browser.FindAllCss("dashboard-section");
    }
}
