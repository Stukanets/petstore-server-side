using System.Linq;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Hooks
{
    [Binding]
    public class StepsTransformation
    {
        [StepArgumentTransformation]
        public string[] TransformToListOfString(string commaSeparatedList) =>
           commaSeparatedList.Split(";")
               .Select(e => e.Trim()).ToArray();
    }
}
