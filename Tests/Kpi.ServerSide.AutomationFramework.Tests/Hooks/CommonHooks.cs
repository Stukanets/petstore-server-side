using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Serilog;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Hooks
{
    [Binding]
    public sealed class CommonHooks
    {
        [ThreadStatic]
        private static ExtentTest _featureName;
        [ThreadStatic]
        private static ExtentTest _scenario;
        private static AventStack.ExtentReports.ExtentReports _extent;
        private static string _featureTitle;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        private readonly ILogger _logger;

        public CommonHooks(
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            ILogger logger)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _logger = logger;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            // Initialize Extent report before test starts
            var reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            // Attach report to reporter
            _extent = new AventStack.ExtentReports.ExtentReports();

            _extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extent.Flush();
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                _logger.Information("_scenarioContext.TestError = null");
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message);
                }
            }
            else if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                        .Skip("Step Definition Pending");
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                        .Skip("Step Definition Pending");
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                        .Skip("Step Definition Pending");
                }
            }
        }

        [BeforeScenario]
        public void Initialize()
        {
            if (string.IsNullOrEmpty(_featureTitle) || _featureTitle != _featureContext.FeatureInfo.Title)
            {
                _featureTitle = _featureContext.FeatureInfo.Title;
                _featureName = _extent.CreateTest<Feature>(_featureTitle);
            }

            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }
    }
}
