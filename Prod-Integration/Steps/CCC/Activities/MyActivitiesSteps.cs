using BoDi;
using NUnit.Framework;
using Prod_Integration.Pages.CCC.Activities;
using Prod_Integration.Utils;
using System.Linq;
using TechTalk.SpecFlow;
using Zukini.UI;
using Zukini.UI.Steps;

namespace Prod_Integration.Steps.CCC.Activities
{
    [Binding]
    public class MyActivitiesSteps : UISteps
    {

        private MyActivitiesPage _page;
        private IObjectContainer _objectContainer;

        public MyActivitiesSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _page = new MyActivitiesPage(Browser);
            _objectContainer = objectContainer;
        }

        [When(@"I filter by Type '(.*)'")]
        public void WhenIFilterByType(string type)
        {
            _page.LoadingDone();
            _page.ExpandAccordion(_page.AccordionType);
            _page.LoadingDone();
            _page.SelectType(type);
        }

        [When(@"I filter by Status '(.*)'")]
        public void WhenIFilterByStatus(string status)
        {
            _page.LoadingDone();
            _page.ExpandAccordion(_page.AccordionStatus);
            _page.LoadingDone();
            _page.SelectState(status);
        }

        [When(@"Create an activity")]
        public void WhenCreateAnActivity()
        {
            _page.LoadingDone();
            var name = $"Internal Test {StringUtils.RandomAlphaNumericString(5)}";
            PropertyBucket.Remember("activity name", name);
            _page.CreateCustomActivity(name, "Other", PropertyBucket.TestId, _objectContainer);
        }

        [Then(@"all activities displayed should be of Type '(.*)'")]
        public void ThenAllActivitiesDisplayedShouldBeOfType(string type)
        {
            Browser.WaitUntil(() => _page.ActivityItems().Count() > 0, "Activities failed to load");
            Browser.WaitUntil(() => _page.GetColumnValues("Type").All(a => a.Text.Equals(type)), $"Filter failed for '{type}'");
            Assert.True(_page.GetColumnValues("Type").All(a => a.Text.Equals(type)), $"Not all actitivies are of Type '{type}'");
        }

        [Then(@"all activities displayed should be of State '(.*)'")]
        public void ThenAllActivitiesDisplayedShouldBeOfState(string status)
        {
            Browser.WaitUntil(() => _page.ActivityItems().Count() > 0, "Activities failed to load");
            Assert.True(_page.GetColumnValues("Status").All(a => a.Text.Equals(status)), $"Not all actitivies are of State '{status}'");
        }

        [Then(@"I should see the activity with a status of '(.*)'")]
        public void ThenIShouldSeeTheActivityWithAStatusOf(string status)
        {            
            Browser.WaitUntil(() => _page.ActivityItems().Count() > 0, "Activities failed to load");
            _page.ExpandAccordion(_page.AccordionStatus);
            _page.LoadingDone();
            _page.SelectState(status);
            var name = PropertyBucket.GetProperty<string>("activity name");          
            Assert.True(_page.GetColumnValues("Title").Any(a => a.Text.Equals(name)), "Created activity not displayed in grid");
        }
    }
}
