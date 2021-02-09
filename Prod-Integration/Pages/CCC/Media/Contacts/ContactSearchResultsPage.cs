using CCC_Pages.Common.Pages;
using Coypu;
using System.Collections.Generic;
using Zukini.UI;

namespace Prod_Integration.Pages.CCC.Media.Contacts
{
    public class ContactSearchResultsPage : C3BasePage<ContactSearchResultsPage>
    {
        public ContactSearchResultsPage(BrowserSession browser) : base(browser) { }

        public ElementScope AddToListButton() => Browser.FindCss("div>add-to-list>div>multiselect-dropdown>div>ng-transclude>button");
        public ElementScope CreateListModal() => Browser.FindCss("div.modal-content");
        public ElementScope ListNameTextInputBox() => Browser.FindCss("div>add-to-list>div>multiselect-dropdown>div>div>form>ci-search-box>div>input[name='query']");
        public ElementScope CreateListButton() => Browser.FindCss("a.static-dropdown-item");
        public IEnumerable<ElementScope> MasterDetailListItems() => Browser.FindAllCss("ul.list-group.master>li");
        public ElementScope ToastSuccess() => Browser.FindCss(".ci-toast.toast-success");
        public ElementScope ViewContactButton() => Browser.FindCss("ul.list-inline>li>a");
        public ElementScope CheckBox() => Browser.FindCss(".list-group");

        /// <summary>
        /// This is a helper method that selects all contacts at the indexes passed in.
        /// </summary>
        /// <param name="indexes"> The amount of check boxes to select. Should come from feature file. </param>
        public void SelectCheckBoxesByIndex(params int[] indexes)
        {

            Browser.WaitUntil(() => CheckBox().Exists(), "Checkbox Failed to appear");

            foreach (int index in indexes)
            {
                var path = Browser.FindXPath($"//ul[contains(@class,'list-group')]/li[{index}]/div/div/div//label/i");
                path.Click();
            }            
        }

        /// <summary>
        /// This is a helper method that clicks the add to list button and creates a new list.
        /// </summary>
        /// <param name="List"> The name of the list that is being created. This should come from the feature file. </param>
        public void CreateNewList(string list)
        {
            AddToListButton().Click();
            ListNameTextInputBox().Click();
            ListNameTextInputBox().SendKeys(list);           
            CreateListButton().Click();
            Browser.WaitUntil(() => ToastSuccess().Exists(), "Toast Failed to appear");
            Browser.WaitUntil(() => ToastSuccess().Missing(), "Toast Failed to disappear");
        }
    }
}
