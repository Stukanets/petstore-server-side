using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Task;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.User;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Assignment Creation")]
    public class PostAssignmentDefinition
    {
        private readonly AssignmentRequest _newAssignment;
        private readonly IAssignmentContext _assignmentContext;
        private readonly IUserContext _userContext;
        private string _userToken;
        private int _assignmentCount;

        public PostAssignmentDefinition(
            IUserContext userContext,
            IAssignmentContext taskContext)
        {
            _userContext = userContext;
            _assignmentContext = taskContext;
            _newAssignment = TaskStorage.TaskRequests["RandomTask"];
        }

        [Given(@"I have logged user")]
        public async Task GivenIHaveLoggedUser()
        {
            _userToken = (await _userContext.CreateUserTokenByCredentialsAsync(
                    UserStorage.UserRequests["ValidUser"]))
                .Token;
        }

        [Given(@"I have current Assignment count")]
        public async Task GivenIHaveCurrentAssignmentCount()
        {
            _assignmentCount = (await _assignmentContext.GetAssignmentCountAsync(_userToken)).Count;
        }

        [When(@"I send the assignment creation request with provided model")]
        public async Task WhenISendTheAssignmentCreationRequestWithProvidedModel()
        {
            await _assignmentContext.CreateAssignmentResponseAsync(
                _newAssignment, 
                _userToken);
        }

        [Then(@"I see increased Assignment count")]
        public async Task ThenISeeIncreasedAssignmentCount()
        {
            var currentCount = 
                (await _assignmentContext.GetAssignmentCountAsync(_userToken)).Count;
            currentCount.Should().BeGreaterThan(
                _assignmentCount);
        }
    }
}
