using CCC_Pages.Common.Pages;
using Coypu;
using System;

namespace Prod_Integration.Pages.CCC.News
{
    public class SaveSearchPage : C3BasePage<SaveSearchPage>
    {
        public SaveSearchPage(BrowserSession browser) : base(browser) { }

        public ElementScope AlertEnabledCheckbox() => Browser.FindCss("ci-checkbox[name='isAlertEnabled'] label[class='checkbox-icon active']");
        public ElementScope AlertEnabledCheckboxUnChecked() => Browser.FindCss("ci-checkbox[name='isAlertEnabled'] label[class='checkbox-icon']");
        public ElementScope NextButton() => Browser.FindCss("button.btn.btn-primary.btn-sm.qa-wizard-next");
        public ElementScope SearchNameTextbox() => Browser.FindCss("input[name='searchName']");
        public ElementScope SubmitButton() => Browser.FindCss(".btn.btn-primary.qa-wizard-submit");

        /// <summary>
        /// Saves a search without configuring an alert
        /// </summary>
        /// <param name="searchName">Name of the search.</param>
        /// <param name="alertEnabled">if set to <c>true</c> [alert enabled].</param>
        public void SaveSearchWithoutAlert(string searchName)
        {
            SearchNameTextbox().FillInWith(searchName);
            if (AlertEnabledCheckboxUnChecked().Missing())
            {
                Browser.TryUntil(() => AlertEnabledCheckbox().Click(), () => AlertEnabledCheckboxUnChecked().Exists(), TimeSpan.FromSeconds(10));
            }
            SubmitButton().Click();
        }
    }
}
