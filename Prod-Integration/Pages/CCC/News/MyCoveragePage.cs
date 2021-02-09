using CCC_Pages.Common.Pages;
using Coypu;
using Prod_Integration.Steps.CCC.Common.Modals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prod_Integration.Pages.CCC.News
{
    public class MyCoveragePage : C3BasePage<MyCoveragePage>
    {
        public MyCoveragePage(BrowserSession browser) : base(browser) { }

        //Bulk Actions
        public IEnumerable<ElementScope> BulkActionOptions() => Browser.FindAllCss("div.dropdown.open>ul.dropdown-menu>li");
        public ElementScope DownloadClipsButton() => Browser.FindXPath("//button[i[@class='fa fa-download']]");
        public ElementScope EditActionButton() => Browser.FindXPath("//div[@class='btn-group dropdown']/button[i[@class='fa fa-pencil']]");
        public ElementScope MoreActionsButton() => Browser.FindXPath("//div[@class='btn-group dropdown']/button[i[@class='fa icon-ellipse']]");
        public ElementScope ShareButton() => Browser.FindXPath("//button[i[@class='fa fa-share-alt']]");

        public IEnumerable<ElementScope> Headlines() => Browser.FindAllCss("md-list>md-row>div>div>div>div.bold.primary-text>span");
        public ElementScope PageHeader() => Browser.FindCss("div.page-header-main>h1");
        public IEnumerable<ElementScope> SavedSearches() => Browser.FindAllCss("div.sidebar-panel-inner>ul>li>span:first-child");
        public ElementScope SavedSearchButton() => Browser.FindCss("button[ng-click='news.showSaveSearchWizard()']");
        public ElementScope SavedSearchDeleteButton() => Browser.FindCss("li > span > i[class='fa fa-trash-o']");
        public ElementScope SelectAllCheckbox() => Browser.FindCss("news-search-action-row>div>div>div>label>i");


        public void AssertCurrentPage()
        {
            AssertCurrentPage(GetType().Name, PageHeader().Text == "My Coverage");
        }

        /// <summary>
        /// Deletes a saved search.
        /// </summary>
        /// <param name="name">The name.</param>
        public void DeleteSavedSearch(string name)
        {
            if (SavedSearches().Any(s => s.Title.Equals(name)))
            {
                var search = SavedSearches().First(s => s.Title.Equals(name)).Click();
                SavedSearchDeleteButton().Click();
                var modal = new DeleteSavedSearchModal(Browser);
                modal.DeleteButton().Click();          
            }
        }

        /// <summary>
        /// Clicks a bulk action option.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void ClickBulkAction(string action)
        {
            switch (action.ToLower())
            {
                case "add tags":
                case "modify custom fields":
                case "modify tone":
                    EditActionButton().Click();
                    break;

                case "clip report":
                case "export clips":
                    DownloadClipsButton().Click();
                    break;

                case "analyze":
                case "delete":
                    MoreActionsButton().Click();
                    break;

                case "forward":
                    ShareButton().Click();
                    break;

                default:
                    throw new ArgumentException($"'{action}' is not a valid bulk action option");
            }
            BulkActionOptions().First(o => o.Text.ToLower().Equals(action.ToLower())).Click();
        }
    }
}
