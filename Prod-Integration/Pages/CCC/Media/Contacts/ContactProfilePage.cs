using CCC_Pages.Common.Pages;
using Coypu;
using System.Collections.Generic;

namespace Prod_Integration.Pages.CCC.Media.Contacts
{
    public class ContactProfilePage : C3BasePage<ContactProfilePage>
    {
        public ContactProfilePage(BrowserSession browser) : base(browser) { }

        public ElementScope ContactName() => Browser.FindCss("div.media-details>div>div>h3");
        public ElementScope HistoryPanel() => Browser.FindCss("div[body-class='panel-history']");
        public IEnumerable<ElementScope> HistoryPanelTabHeaders() => Browser.FindAllCss("div.timeline.ci-tabs-inline.responsive-tabs>div>div>ul>li>a");
        public ElementScope PitchingProfile() => Browser.FindCss("div[body-class='media-profile-panel']>div>div.media-profile");
        public IEnumerable<ElementScope> Tweets() => Browser.FindAllCss("stream>div>div>div",null, new Options { ConsiderInvisibleElements = true });
    }
}
