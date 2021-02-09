using CCC_Pages.Common.Pages;
using Coypu;

namespace Prod_Integration.Steps.CCC.Common.Modals
{
    public class DeleteSavedSearchModal : C3BasePage<DeleteSavedSearchModal>
    {
        public DeleteSavedSearchModal(BrowserSession browser) : base(browser) { }

        public ElementScope CancelButton() => Browser.FindCss("button[ng-click='$dismiss();']");
        public ElementScope DeleteButton() => Browser.FindCss("button[ng-click='$close();']");
    }
}
