using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Task;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.User;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Get Assignment by Id")]
    public class GetAssignmentDefinition
    {
        private readonly AssignmentRequest _newAssignment;
        private readonly IAssignmentContext _assignmentContext;
        private readonly IUserContext _userContext;
        private AssignmentResponse _createdAssignment;
        private string _userToken;

        public GetAssignmentDefinition(
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

        [When(@"I create Assignment by post request")]
        public async Task WhenICreateAssignmentByPostRequest()
        {
            _createdAssignment =
                await _assignmentContext.CreateAssignmentAsync(_newAssignment, _userToken);
        }

        [Then(@"I see returned Assignment details which are equal with created")]
        public async Task ThenISeeReturnedAssignmentDetailsWhichAreEqualWithCreated()
        {
            var createdAssignment = await _assignmentContext.GetAssignmentByIdAsync(
                _createdAssignment.Data.Id,
                _userToken);
            createdAssignment.Data.Description.Should().Be(
                _newAssignment.Description);
        }
    }
}
