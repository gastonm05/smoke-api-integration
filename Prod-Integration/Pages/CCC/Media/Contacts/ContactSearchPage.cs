using CCC_Pages.Common.Pages;
using Coypu;
using System.Collections.Generic;

namespace Prod_Integration.Pages.CCC.Media.Contacts
{
    public class ContactSearchPage : C3BasePage<ContactSearchPage>
    {
        public ContactSearchPage(BrowserSession browser) : base(browser) { }

        public ElementScope ActiveTabLink() => Browser.FindCss("li.uib-tab.nav-item.active");
        public ElementScope CriteriaDropdown() => Browser.FindCss("div.tab-pane.active>div>search-form>form>div>div>div>select");
        public IEnumerable<ElementScope> CriteriaDropdownOptionGroups() => Browser.FindAllCss("div.tab-pane.active>div>search-form>form>div>div>div>select>optgroup");
        public IEnumerable<ElementScope> NameSearchOptions() => Browser.FindAllCss("ci-fuzzy-multi-select>ul>li");
        public ElementScope NameSearchTextbox() => Browser.FindCss("search-form[on-search*='Contacts'] input");
        public ElementScope SearchButton() => Browser.FindCss("div.tab-pane.active>div>search-form>form>div>div>button[type='submit']");
        public ElementScope TalkingAboutSearchTextbox() => Browser.FindCss("input[name='influencerKeyword']");
    }
}
