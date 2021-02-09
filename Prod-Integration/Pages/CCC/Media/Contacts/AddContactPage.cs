using CCC_Pages.Common.Pages;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using Zukini.UI;

namespace Prod_Integration.Pages.CCC.Media.Contacts
{
    public class AddContactPage : C3BasePage<AddContactPage>
    {
        public AddContactPage(BrowserSession browser) : base(browser) { }

        public IEnumerable<ElementScope> CountryOptions() => Browser.FindAllCss("ci-typeahead-input#CountryId>ul>li");
        public ElementScope CountryTextbox() => Browser.FindCss("#CountryId>input");
        public ElementScope FirstNameTextbox() => Browser.FindId("FirstName");
        public ElementScope LastNameTextbox() => Browser.FindId("LastName");
        public ElementScope SaveButton() => _.FindCss("button.btn-primary");
        public ElementScope TwitterHandleTextbox() => Browser.FindId("TwitterHandle");
        public IEnumerable<ElementScope> Cards() => _.FindAllCss(".cards>card>div");
        public ElementScope AdditionalInfo() => _.FindCss("ci-accordion-group[header-title='Additional Information']>div>div>h4>a>span>i");
        public ElementScope AddressLine() => _.FindId("AddressLine1");
        public ElementScope OutletName() => _.FindId("FullName");
        public IEnumerable<ElementScope> Suggestions() => _.FindAllCss(".dropdown-menu.ci-typeahead-list>li");
        public ElementScope OuletInput() => _.FindCss("#outlet>input");
        public ElementScope Email() => _.FindId("Email");

        /// <summary>
        /// Adds the private contact.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <param name="twitter">The twitter.</param>
        public void AddPrivateContact(string first, string last, string twitter)
        {
            FirstNameTextbox().FillInWith(first);
            LastNameTextbox().FillInWith(last);
            SelectCountry("United States");
            TwitterHandleTextbox().FillInWith(twitter);
            SaveButton().Click();
        }

        /// <summary>
        /// Selects the country.
        /// </summary>
        /// <param name="country">The country.</param>
        private void SelectCountry(string country)
        {
            CountryTextbox().FillInWith(country);
            Browser.WaitUntil(() => CountryOptions().Count() > 0, "Country options did not load");
            CountryOptions().First(o => o.Text.Equals(country)).Click();
        }

        /// <summary>
        /// Select an outlet from the autosuggestion list
        /// </summary>
        /// <param name="outlet"></param>
        public void SelectOutlet(string outlet)
        {
            OuletInput().SendKeys(outlet);
            Browser.TryUntil(() => OuletInput().Hover(), () => Suggestions().Count() > 0, TimeSpan.FromSeconds(1), new Options() { Timeout = TimeSpan.FromSeconds(5) });
            Suggestions().FirstOrDefault().Click();
        }

        /// <summary>
        /// Create a random private contact
        /// </summary>
        /// <param name="name"></param>
        /// <param name="outlet"></param>
        /// <param name="country"></param>
        public void CreatePrivateContact(string first, string last, string outlet, string country, string twitter)
        {
            FirstNameTextbox().SendKeys(first);
            LastNameTextbox().SendKeys(last);
            SelectOutlet(outlet);           
            SelectCountry(country);
            AdditionalInfo().Click();
            AddressLine().SendKeys("123 test st");
            TwitterHandleTextbox().SendKeys(twitter);
            SaveButton().Click();
        }

    }
}
