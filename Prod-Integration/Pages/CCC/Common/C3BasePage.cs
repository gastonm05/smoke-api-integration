using Coypu;
using System.Linq;
using CCC_Infrastructure.ViewFactory;
using Zukini.UI;

namespace CCC_Pages.Common.Pages
{
    public class C3BasePage<TPage> : BasePage<TPage> where TPage : BasePage<TPage>
    {
        public C3BasePage(BrowserSession browser) : base(browser)
        {
        }

        public ElementScope LoadingDots => _.FindCss(".loading-dots");

        /// <summary>
        /// Returns true if there are no loading indicators.
        /// </summary>
        public bool HasNoLoadingDots()
        {
            return _.FindAllCss(".loading-dots").Count() == 0;
        }

        /// <summary>
        /// Waits until loading is done (loading dots are missing)
        /// </summary>
        public void LoadingDone()
        {
            Browser.WaitUntil(() => HasNoLoadingDots(), "Page was still loading");
        }
    }
}
