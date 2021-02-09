using CCC_Infrastructure.Utils;
using CCC_Pages.Common.Pages;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;


namespace Prod_Integration.Pages.CCC.Media.SocialInfluencers
{
    public class SocialInfluencerSearchPage : C3BasePage<SocialInfluencerSearchPage>
    {
        public SocialInfluencerSearchPage(BrowserSession browser) : base(browser) { }

        public ElementScope SocialSubjectSearchTextbox() => Browser.FindCss("div.tab-pane.active>div>search-form>form>div>div>search-form-fields>div>pilled-multiselect-range>multiselect-typeaheaddropdown>div>input");
        public IEnumerable<ElementScope> SocialSubjectDropdownItems() => Browser.FindAllCss("div[role='menu']>div>ul>li");
        public ElementScope ActiveTabLink() => Browser.FindCss("li.uib-tab.nav-item.active");
        public IEnumerable<ElementScope> MediaTabs() => Browser.FindAllCss("div.ci-tabs-inline>ul>li");
        public ElementScope SocialInfluencersTab() => Browser.FindCss("div.ci-tabs-inline>ul>li:last-child>a");
        public ElementScope CriteriaDropDownMultiple() => Browser.FindCss(".tab-pane.active>div>search-form>form>div>div>div>select[ng-if='field.allowChange']");
        public IEnumerable<ElementScope> SocialInfluencersDropDown() => Browser.FindAllCss("div.tab-pane.active>div>search-form>form>div>div>div>select> option");
        public ElementScope SearchButton() => Browser.FindCss("div.tab-pane.active>div>search-form>form>div>div>button[type='submit']");
        
        /// <summary>
        /// Searches social Influencers by single Subject
        /// </summary>
        /// <param name="value">The value.</param>
        public void SearchMediaSocialBySubject(string subject)
        {
            Browser.WaitUntil(() => MediaTabs().Count() > 2, "The social influencer tab was not loaded");
            SocialInfluencersTab().Click();
            var selectElement = (SelectElement)new SelectElement((IWebElement)CriteriaDropDownMultiple().Native);
            Browser.WaitUntil(() => SocialInfluencersDropDown().Count() > 0, "Criteria options failed to populate");
            selectElement.SelectByValue("subjectIds");
            SocialSubjectSearchTextbox().SendKeys(subject);
            Browser.WaitUntil(() => SocialSubjectDropdownItems().Count() > 0, "Subject options failed to load within session timeout");
            SocialSubjectDropdownItems().FirstOrDefault().Click();
            ActiveTabLink().Click();
            SearchButton().Click();
        }



    }
}
