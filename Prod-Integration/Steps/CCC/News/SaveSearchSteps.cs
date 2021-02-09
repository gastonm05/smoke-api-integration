using BoDi;
using Prod_Integration.Utils;
using Prod_Integration.Pages.CCC.News;
using TechTalk.SpecFlow;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.News
{
    [Binding]
    public class SaveSearchSteps : UISteps
    {
        private readonly SaveSearchPage _page;

        public SaveSearchSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new SaveSearchPage(Browser);
        }

        [When(@"I save the search")]
        public void WhenISaveTheSearch()
        {
            var name = $"Test Save Search {StringUtils.RandomAlphaNumericString(5)}";
            _page.SaveSearchWithoutAlert(name);
            PropertyBucket.Remember("Save Search Name", name);
        }

    }
}
