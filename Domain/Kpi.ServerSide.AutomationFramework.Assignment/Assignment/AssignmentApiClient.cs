using System.Reflection;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Client;
using Kpi.ServerSide.AutomationFramework.Platform.Communication.Http;
using Kpi.ServerSide.AutomationFramework.Platform.Configuration.Environment;
using Serilog;

namespace Kpi.ServerSide.AutomationFramework.Assignment.Assignment
{
    public class AssignmentApiClient : ApiClientBase, IAssignmentApiClient
    {
        public AssignmentApiClient(
            IClient client,
            ILogger logger,
            IEnvironmentConfiguration environmentConfiguration)
            : base(client, logger)
        {
            client.SetBaseUri(environmentConfiguration.EnvironmentUri);
            Logger.Information($"TodoList: Set base uri: {environmentConfiguration.EnvironmentUri}");
        }

        public async Task<AssignmentCountResponse> GetAssignmentCountAsync(
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                accessToken);

            var restResponse = await ExecuteGetAsync(
                $"/task", accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return restResponse.GetModel<AssignmentCountResponse>();
        }

        public async Task<AssignmentResponse> GetAssignmentByIdAsync(
            string assignmentId, 
            string accessToken)
        {
            var restResponse = await ExecuteGetAsync(
                $"/task/{assignmentId}", accessToken);

            return restResponse.GetModel<AssignmentResponse>();
        }

        public async Task<ResponseMessage> GetAssignmentByIdResponseAsync(
            string assignmentId,
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@assignmentId} as task id and {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                assignmentId,
                accessToken);

            var restResponse = await ExecuteGetAsync(
                $"/task/{assignmentId}", accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<ResponseMessage> CreateAssignmentResponseAsync(
            AssignmentRequest assignmentRequest,
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@assignmentRequest} and {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                assignmentRequest,
                accessToken);

            var restResponse = await ExecutePostAsync(
                "/task",
                assignmentRequest,
                accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<AssignmentResponse> CreateAssignmentAsync(
            AssignmentRequest assignmentRequest,
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@assignmentRequest} and {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                assignmentRequest,
                accessToken);

            var restResponse = await ExecutePostAsync(
                "/task",
                assignmentRequest,
                accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return restResponse.GetModel<AssignmentResponse>();
        }

        public async Task<ResponseMessage> UpdateAssignmentResponseAsync(
            AssignmentRequest assignmentRequest,
            string assignmentId,
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@assignmentRequest} as new task description, " +
                "{@assignmentId} as task id and {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                assignmentRequest,
                accessToken,
                assignmentId);

            var restResponse = await ExecutePutAsync(
                $"/task/{assignmentId}",
                assignmentRequest,
                accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<ResponseMessage> DeleteAssignmentResponseAsync(
            string assignmentId,
            string accessToken)
        {
            Logger.Information(
                "Start '{@Method}' with {@assignmentId} as task id and {@accessToken} as access token",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                assignmentId,
                accessToken);

            var restResponse = await ExecuteDeleteAsync(
                $"/task/{assignmentId}",
                accessToken);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }
    }
}
