using CCC_Pages.Common.Pages;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using Zukini.UI;

namespace Prod_Integration.Pages.CCC.Common
{
    public class CisionHeader : C3BasePage<CisionHeader>
    {
        public CisionHeader(BrowserSession browser) : base(browser) { }

        public IEnumerable<ElementScope> NavigationLinks => Browser.FindAllCss("navbar > nav > div.nav-menu-container > a");
        public ElementScope DataGroupDropDown => _.FindCss("[data-qa='datagroup dropdown']");
        public IEnumerable<ElementScope> DataGroupMenuItems => _.FindAllCss("[data-qa='datagroup menu item']");
        public IEnumerable<ElementScope> NavigationMenu => Browser.FindAllCss("navbar > nav > div.nav-menu-container");

        /// <summary>
        /// Allows navigation to the specified nav item using the specified main and sub nav text.
        /// </summary>
        /// <param name="mainNavText">Text of the main nav item.</param>
        /// <param name="subNavText">Text of the sub nav item.</param>
        /// <example>
        /// cisionHeader.NavigateTo(""DISTRIBUTE\r\nVia PR Newswire"","SEND NEWS RELEASE");
        /// </example>
        public void NavigateTo(string mainNavText, string subNavText)
        {
            // make sure we are loaded
            WaitToLoad();
            ElementScope mainNav = null;

            // Loop until we have an item or we timeout
            Browser.TryUntil(
                () => mainNav = NavigationMenu.FirstOrDefault(m => m.Text.Equals(mainNavText, StringComparison.CurrentCultureIgnoreCase)),
                () => mainNav != null);
            if (mainNav == null)
            {
                throw new Exception($"Could not find the specified top level nav item '{mainNavText}'");
            }
            Browser.TryUntil(() => mainNav.Click(),
                             () => mainNav.FindAllCss("a").Count() > 0,
                             TimeSpan.FromSeconds(2), new Options() { Timeout = TimeSpan.FromSeconds(10) });
            var subNav = mainNav.FindAllCss("a").FirstOrDefault(a => a.Text.ToLower().Contains(subNavText.ToLower()));
            if (subNav == null)
            {
                throw new Exception($"Could not find the specified sub nav item '{subNavText}' under top level item '{mainNavText}'");
            }
            subNav.Click();
        }

        /// <summary>
        /// Finds the data group menu item by name and returns the ElementScope
        /// </summary>
        /// <param name="name">Name of data group menu item</param>
        /// <returns>Data group menu item as ElementScope</returns>
        public ElementScope FindDataGroupMenuItem(string name)
        {
            var DATAGROUP_XPATH = "//header/navbar/nav/div[@class='nav-full']/ul[contains(@class, 'navbar-right')]/li[@class='user-menu']/div";
            var DATAGROUP_MENU_ITEMS_XPATH = DATAGROUP_XPATH + "/div[contains(@class, 'subnav-panel')]//ul/li";

            return Browser.FindXPath($"{DATAGROUP_MENU_ITEMS_XPATH}/a[text()='{name}']");
        }

        /// <summary>
        /// Select a datagroup from the datagroup dropdown.
        /// If the datagroup is already selected, this method does nothing.
        /// The caller should verify that the datagroup dropdown exists prior to selecting a datagroup.
        /// </summary>
        /// <param name="dataGroupName">datagroup to select from dropdown</param>
        public void SelectDataGroup(string dataGroupName)
        {
            // only change the datagroup if it isn't already selected
            if (DataGroupDropDown.Text != dataGroupName)
            {
                Browser.WaitUntil(() => DataGroupDropDown.Exists(), "Datagroup dropdown did not render");
                DataGroupDropDown.Click();
                Browser.WaitUntil(() => DataGroupMenuItems.Count() > 0, "Datagroup dropdown options did not populate");
                FindDataGroupMenuItem(dataGroupName).Click();
                Browser.WaitUntil(() => DataGroupDropDown.Text == dataGroupName, $"Expected datagroup '{dataGroupName}' but '{DataGroupDropDown.Text}' was selected");
            }
        }
    }
}
