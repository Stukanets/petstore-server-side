using System;
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
    [Binding, Scope(Feature = "Update Assignment by Id")]
    public class PutAssignmentDefinition
    {
        private readonly IAssignmentContext _assignmentContext;
        private readonly IUserContext _userContext;
        private AssignmentRequest _newAssignment;
        private AssignmentResponse _createdAssignment;
        private ResponseMessage _responseMessage;
        private string _userToken;

        public PutAssignmentDefinition(
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

        [When(@"I send the Assignment update request with new description")]
        public async Task WhenISendTheAssignmentUpdateRequestWithNewDescription()
        {
            _newAssignment = TaskStorage.TaskRequests["RandomTask"];
            _responseMessage = await _assignmentContext.UpdateAssignmentResponseAsync(
                _newAssignment,
                _createdAssignment.Data.Id, 
                _userToken);
        }

        [When(@"I send the Assignment update request with invalid Assignment id")]
        public async Task WhenISendTheAssignmentUpdateRequestWithInvalidAssignmentId()
        {
            _responseMessage = await _assignmentContext.UpdateAssignmentResponseAsync(
                null,
                Guid.NewGuid().ToString(),
                _userToken);
        }

        [Then(@"I see (.*) status code")]
        public void ThenISeeStatusCode(string statusCode)
        {
            _responseMessage.StatusCode.Should().Be(
                statusCode);
        }

        [Then(@"I see updated Assignment")]
        public async Task ThenISeeUpdatedAssignment()
        {
            var createdAssignment = await _assignmentContext.GetAssignmentByIdAsync(
                _createdAssignment.Data.Id,
                _userToken);
            createdAssignment.Data.Description.Should().Be(
                _newAssignment.Description);
        }
    }
}
