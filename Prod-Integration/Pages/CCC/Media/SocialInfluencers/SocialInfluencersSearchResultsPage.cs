using CCC_Pages.Common.Pages;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod_Integration.Pages.CCC.Media.SocialInfluencers
{
    public class SocialInfluencersSearchResultsPage : C3BasePage<SocialInfluencersSearchResultsPage>
    {
        public SocialInfluencersSearchResultsPage(BrowserSession browser) : base(browser) { }
        public override bool IsLoaded() => HasNoLoadingDots();

        public ElementScope TransparencyText() => _.FindCss(".search-receipt.secondary-text>span>span>span");
        public ElementScope SubjectsChart() => _.FindCss(".subjects-chart");
        public IEnumerable<ElementScope> RecenTweets() => _.FindAllCss(".stream.loading-container>div>div");
        public IEnumerable<ElementScope> SocialInfluencersMDVNames() => _.FindAllCss("div.list-row-main > div.list-row-content > div");

    }
}
