using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Task;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.User;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Delete Assignment by Id")]
    public class DeleteAssignmentDefinition
    {
        private readonly AssignmentRequest _newAssignment;
        private readonly IAssignmentContext _assignmentContext;
        private readonly IUserContext _userContext;
        private AssignmentResponse _createdAssignment;
        private ResponseMessage _responseMessage;
        private string _userToken;

        public DeleteAssignmentDefinition(
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
            _userToken = (await _userContext.GetUserTokenByCredentialsAsync(
                    UserStorage.UserRequests["ValidUser"]))
                .Token;
        }

        [When(@"I create Assignment by post request")]
        public async Task WhenICreateAssignmentByPostRequest()
        {
            _createdAssignment =
                await _assignmentContext.CreateAssignmentAsync(_newAssignment, _userToken);
        }

        [When(@"I send delete request with created Assignment id")]
        public async Task WhenISendDeleteRequestWithCreatedAssignmentId()
        {
            _responseMessage = 
                await _assignmentContext.DeleteAssignmentResponseAsync(
                    _createdAssignment.Data.Id, 
                    _userToken);
        }

        [Then(@"I see (.*) status code")]
        public void ThenISeeStatusCode(string statusCode)
        {
            _responseMessage.StatusCode.Should().Be(
                statusCode);
        }
    }
}
