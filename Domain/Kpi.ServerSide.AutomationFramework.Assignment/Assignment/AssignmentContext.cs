using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;

namespace Kpi.ServerSide.AutomationFramework.Assignment.Assignment
{
    public class AssignmentContext : IAssignmentContext
    {
        private readonly IAssignmentApiClient _taskApiClient;

        public AssignmentContext(
            IAssignmentApiClient taskApiClient)
        {
            _taskApiClient = taskApiClient;
        }

        public async Task<AssignmentCountResponse> GetAssignmentCountAsync(
            string accessToken)
        {
            return await _taskApiClient.GetAssignmentCountAsync(Token.BearerTokenGenerator(accessToken));
        }

        public async Task<AssignmentResponse> GetAssignmentByIdAsync(
            string assignmentId, 
            string accessToken)
        {
            return await _taskApiClient.GetAssignmentByIdAsync(assignmentId, Token.BearerTokenGenerator(accessToken));
        }

        public async Task<ResponseMessage> CreateAssignmentResponseAsync(
            AssignmentRequest assignmentRequest, 
            string accessToken)
        {
            return await _taskApiClient.CreateAssignmentResponseAsync(assignmentRequest, Token.BearerTokenGenerator(accessToken));
        }

        public async Task<AssignmentResponse> CreateAssignmentAsync(
            AssignmentRequest assignmentRequest, 
            string accessToken)
        {
            return await _taskApiClient.CreateAssignmentAsync(assignmentRequest, Token.BearerTokenGenerator(accessToken));
        }

        public async Task<ResponseMessage> DeleteAssignmentResponseAsync(
            string assignmentId, 
            string accessToken)
        {
            return await _taskApiClient.DeleteAssignmentResponseAsync(assignmentId, Token.BearerTokenGenerator(accessToken));
        }

        public async Task<ResponseMessage> UpdateAssignmentResponseAsync(
            AssignmentRequest assignmentRequest, 
            string assignmentId, 
            string accessToken)
        {
            return await _taskApiClient.UpdateAssignmentResponseAsync(assignmentRequest, assignmentId, Token.BearerTokenGenerator(accessToken));
        }
    }
}
