using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CalculatorSelenium.Specs.Hooks
{
    /// <summary>
    /// Calculator related hooks
    /// </summary>

    [Binding]
    public class Hook : TechTalk.SpecFlow.Steps
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        [BeforeTestRun]
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter(@"Your location here");

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext sc)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(sc, null);
            if (sc.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            if (sc.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
            }
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featurecontext)
        {
            featureName = extent.CreateTest(featurecontext.FeatureInfo.Title);
        }
    }
}