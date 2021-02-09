using BoDi;
using CCC_Infrastructure.Utils;
using CCC_Pages.Common.Pages;
using Coypu;
using Prod_Integration.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Zukini.UI;

namespace Prod_Integration.Pages.CCC.Activities
{
    public class MyActivitiesPage : C3BasePage<MyActivitiesPage>
    {
        public MyActivitiesPage(BrowserSession browser) : base(browser) { }

        public IEnumerable<ElementScope> ActivityItems() => Browser.FindAllCss("datatable>div.datatable-wrapper>div.datatable-row");
        public ElementScope CreateNewActivityCreateButton() => Browser.FindCss("button[ng-click*=save");
        public ElementScope CreateActivityTitleTextbox() => Browser.FindCss("input[name*=title]");
        public ElementScope CreateNewActivityButton => Browser.FindCss("card [ng-if*='HasCustomActivitiesEnabled']");
        public ElementScope CreateNewActivityTypeMenu() => Browser.FindCss("select[name='type']");
        private IEnumerable<ElementScope> GridColumnValues(int index) => Browser.FindAllCss($".datatable-row>div:nth-child({index})");
        public IEnumerable<ElementScope> GridHeaders() => Browser.FindAllCss("div.datatable-header.bold>div>div");
        public IEnumerable<ElementScope> FilterAccordians => Browser.FindAllCss("h4 > a > span > span.accordion-title");
        private IEnumerable<ElementScope> StatusCheckBoxes => Browser.FindAllCss(".sidebar-panel-inner div[role='tablist'] div[ng-repeat]:nth-child(3) .checkbox-icon");
        private IEnumerable<ElementScope> TypeCheckBoxes => Browser.FindAllCss(".sidebar-panel-inner div[role='tablist'] [ng-switch-when=ACTIVITIES_TYPE] .checkbox-icon");
        public ElementScope CreateNewActivityTimeZone => Browser.FindCss("select[ng-model*='vm.timeZone']");
        public ElementScope AccordionDate => _.FindCss("[header-title='Date'] .panel");
        public ElementScope AccordionType => _.FindCss("[header-title='Type'] .panel");
        public ElementScope AccordionStatus => _.FindCss("[header-title='Status'] .panel");

        public void ExpandAccordion(ElementScope accordion)
        {
            if (accordion.FindCss("i.fa-plus").Exists())
            {
                accordion.Click();
            }
        }

        /// <summary>
        /// Selects type filter
        /// </summary>
        /// <param name="type">type, like email</param>
        public void SelectType(string type)
        {
            if (TypeCheckBoxes.Count() == 0)
            {
                FilterAccordians.First(f => f.Text.ToLower().Contains("type")).Click();
                Browser.WaitUntil(() => TypeCheckBoxes.Count() > 0, "Filter accordian had no children checkboxes");
            }
            TypeCheckBoxes.Where(checkbox => checkbox.Text.Contains(type)).First().FindCss("i").Click();
            LoadingDone();
        }

        /// <summary>
        /// Selects status filter
        /// </summary>
        /// <param name="status">status like sent</param>
        public void SelectState(string status)
        {
            if (StatusCheckBoxes.Count() == 0)
            {
                FilterAccordians.First(f => f.Text.ToLower().Contains("status")).Click();
                Browser.WaitUntil(() => StatusCheckBoxes.Count() > 0, "Filter accordian had no children checkboxes");
            }
            StatusCheckBoxes.Where(checkbox => checkbox.Text.Contains(status)).First().FindCss("i").Click();
            LoadingDone();
        }

        /// <summary>
        /// Creates the custom activity. 
        /// IMPORTANT: DO NOT MODIFY THIS METHOD TO LINK TO AN OUTLET OR CONTACT
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="type">The type.</param>
        public void CreateCustomActivity(string title, string type, string id, IObjectContainer objContainer)
        {
            CreateNewActivityButton.Click();
            CreateActivityTitleTextbox().FillInWith(title);
            CreateNewActivityTypeMenu().SelectOption(type);
            Screenshot.TakeScreenshot($"BrowserTimezoneTest1 {id}", objContainer, Browser);
            var browserTime = Browser.ExecuteScript($"return new Date().getTime();");
            Console.WriteLine($"Browser Time: {browserTime}");
            var browserTimeRelativeToMe = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Convert.ToDouble(browserTime));
            Console.WriteLine($"Browser Time Relative To Me: {browserTimeRelativeToMe.ToString()}");
            var hoursDelta = (DateTime.Now - browserTimeRelativeToMe).Hours;
            Console.WriteLine($"Browser Time Hours Delta: {hoursDelta}");
            if (hoursDelta < 0)
            {
                var button = Browser.FindCss("td.uib-decrement.hours > a");
                for (int i = 0; i < Math.Abs(hoursDelta) + 1; i++)
                {
                    button.Click(new Options() { WaitBeforeClick = TimeSpan.FromMilliseconds(500) });
                }
            }
            Screenshot.TakeScreenshot($"BrowserTimezoneTest2 {id}", objContainer, Browser);
            CreateNewActivityTimeZone.SelectOption(TimeUtils.GetLocalTimeZone()); // select timezone
            Screenshot.TakeScreenshot($"BrowserTimezoneTest3 {id}", objContainer, Browser);
            CreateNewActivityCreateButton().Click(); // closes modal
            Browser.WaitUntil(() => CreateNewActivityCreateButton().Missing(), "Activity Modal is still displayed");
        }

        /// <summary>
        /// Gets the index of the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private int GetColumnIndex(string columnName)
        {
            var headers = GridHeaders().Where(h => !string.IsNullOrEmpty(h.Text)).ToList(); // ignore blank column headers
            var index = headers.FindIndex(h => h.Text.ToUpper().Equals(columnName.ToUpper()));
            if (index == -1)
            {
                throw new Exception($"Column '{columnName}' not found");
            }
            return index + 2; //Grid dom is offset due to checkbox column and 1 base [list was 0 base]
        }

        /// <summary>
        /// Gets the values of the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>All column values</returns>
        public IEnumerable<ElementScope> GetColumnValues(string columnName)
        {
            var index = GetColumnIndex(columnName);
            return GridColumnValues(index);
        }
    }
}
