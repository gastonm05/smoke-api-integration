using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using CCC_Pages.Common.Pages;
using Coypu;
using NUnit.Framework;
using Prod_Integration.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Zukini.UI;

namespace Prod_Integration.Pages.CCC.Common
{
    public class LoginPage : C3BasePage<LoginPage>
    {
        public LoginPage(BrowserSession browser) : base(browser) { }

        public ReconnectElementScope LanguageDropDown => new ReconnectElementScope(Browser.FindXPath("//button[@ng-model='login.currentLanguage']"));
        public IEnumerable<ElementScope> LanguageOptions => Browser.FindAllXPath("//ul//li");

        public ReconnectElementScope CompanyTextbox => new ReconnectElementScope(Browser.FindId("company"));
        public ElementScope InvalidCredentialAlert => Browser.FindCss("form>div.alert.alert-danger>span");
        public ReconnectElementScope PasswordTextbox => new ReconnectElementScope(Browser.FindId("password"));
        public ReconnectElementScope SignInButton => new ReconnectElementScope(Browser.FindButton("btnLogin"));
        public ElementScope SuccessfulLogoutMessage => _.FindCss("div.alert.alert-success>span");
        public ReconnectElementScope UserTextbox => new ReconnectElementScope(Browser.FindId("user"));

        public override bool IsLoaded() => SignInButton.Exists();

        /// <summary>
        /// Log in using the CCC login page and wait for login confirmation
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="datagroupName">The name of the datagroup to select, use null to not select a datagroup.</param>
        public void Login(User user, string datagroupName = null)
        {
            var timeout = Convert.ToDouble(TestSettings.GetConfigValue("Timeout"));
            var retry = 4;
            Browser.WaitUntil(() => SignInButton.Exists(), "Sign In button did not render");
            Browser.RetryUntilTimeout(() =>
                {
                    if (SignInButton.Disabled)
                    {
                        Browser.Refresh(); // if disabled, the previous attempt is hung
                    }
                    if (SignInButton.Exists(new ExtendedOptions() { Timeout = TimeSpan.FromSeconds(3) }))
                    {
                        HandleLanguage(user.Language);
                        CompanyTextbox.FillInWith(user.CompanyID);
                        UserTextbox.FillInWith(user.Username);
                        PasswordTextbox.FillInWith(user.Password);
                        SignInButton.Click(new ExtendedOptions() { RefreshToRecover = true, WaitBeforeClick = TimeSpan.FromSeconds(1) });
                    } // else we got logged in despite getting hung
                    if (!InvalidCredentialAlert.Exists(new ExtendedOptions() { Timeout = TimeSpan.FromSeconds(3) } ))
                    {
                        Browser.WaitUntil(() => SignInButton.Missing(new ExtendedOptions() { Timeout = TimeSpan.FromSeconds(timeout) }),
                                          $"Took too long to login to C3: {TestSettings.GetConfigValue("CCCBaseUrl")}");
                    }
                },
                new Options() { Timeout = TimeSpan.FromSeconds(timeout * retry) }
            );
            HandleDataGroup(datagroupName);
        }

        /// <summary>
        /// Log in using the CCC login page without waiting for login confirmation (failed logins okay)
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="datagroupName">The name of the datagroup to select, use null to not select a datagroup.</param>
        public void TryToLogin(User user, string datagroupName = null)
        {
            Browser.WaitUntil(() => SignInButton.Exists(), "Sign In button did not render");

            HandleLanguage(user.Language);
            CompanyTextbox.FillInWith(user.CompanyID);
            UserTextbox.SendKeys(user.Username);
            PasswordTextbox.SendKeys(user.Password);
            SignInButton.Click(new ExtendedOptions() { RefreshToRecover = true });

            HandleDataGroup(datagroupName);
        }

        /// <summary>
        /// Select a languge from the language dropdown
        /// </summary>
        /// <param name="language">language to select from dropdown</param>
        /// <exception cref="Exception">Language not in dropdown</exception>
        /// <remarks>
        /// Language string value is based on the name of the "flag" img src
        /// if omitted, language will not be selected on login page
        /// one of United-States, United-Kingdom, Canada
        /// </remarks>
        public void HandleLanguage(string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                return;
            }

            var langToken = language.Length == 5 && language.Contains("-")
                ? ConvertLangIdToLanguageToken(language) : language;

            Browser.WaitUntil(() => LanguageDropDown.Exists(), "Language dropdown did not render");
            try
            {
                Browser.TryUntil(() => LanguageDropDown.Click(), () => LanguageOptions.Any());
            }
            catch (Exception e)
            {
                throw new Exception("Language dropdown did not populate", e);
            }

            LanguageOptions
                .Where(option => option.OuterHTML.Contains(langToken))
                .First().Click();

            Assert.IsTrue(LanguageDropDown.InnerHTML.Contains(langToken),
                $"Language '{language}' not selected in language dropdown");
        }

        /// <summary>
        /// Converts language id to language dropdown token. 
        /// </summary>
        /// <param name="langId"></param>
        /// <returns>string</returns> Example: "en-US" to "United-States-of-America"
        private string ConvertLangIdToLanguageToken(string langId)
        {
            switch (langId.ToLower())
            {
                case "en-us":
                    return "United-States-of-America";
                case "en-gb":
                    return "United-Kingdom";
                case "en-ca":
                    return "Canada";
                case "fr-fr":
                    return "France";
                case "de-de":
                    return "Germany";
                case "nl-nl":
                    return "Netherlands";
                default:
                    throw new ArgumentException($"{langId} is not supported");
            }
        }

        /// <summary>
        /// Handles the data group associated with a User
        /// </summary>
        /// <param name="user">The user login object, typically deserialized from a test data json file</param>
        private void HandleDataGroup(string datagroupName)
        {
            if (!string.IsNullOrEmpty(datagroupName))
            {
                var cisionHeader = new CisionHeader(Browser);
                Browser.RetryUntilTimeout(() =>
                {
                    Browser.WaitUntil(() => cisionHeader.DataGroupDropDown.Exists(new Options() { Timeout = TimeSpan.FromSeconds(10) }), "Datagroup dropdown did not render");
                    cisionHeader.SelectDataGroup(datagroupName);
                }, new Options() { Timeout = TimeSpan.FromSeconds(120) });
            }
        }
    }
}