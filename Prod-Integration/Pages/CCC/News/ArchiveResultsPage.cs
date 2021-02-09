using CCC_Pages.Common.Pages;
using Coypu;
using System.Collections.Generic;

namespace Prod_Integration.Pages.CCC.News
{
    public class ArchiveResultsPage : C3BasePage<ArchiveResultsPage>
    {
        public ArchiveResultsPage(BrowserSession browser) : base(browser) { }

        public ElementScope AddToCoverageButton() => Browser.FindCss("div.btn-group>button");
        public ElementScope AddToCoverageModalOkButton() => Browser.FindCss("ci-modal-footer button[ng-click*='close']");
        public IEnumerable<ElementScope> NewsItems() => Browser.FindAllCss("md-list>md-row");
    }
}
