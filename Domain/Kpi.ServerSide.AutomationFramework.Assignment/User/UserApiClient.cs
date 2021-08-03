using System.Reflection;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Client;
using Kpi.ServerSide.AutomationFramework.Platform.Communication.Http;
using Kpi.ServerSide.AutomationFramework.Platform.Configuration.Environment;
using Serilog;

namespace Kpi.ServerSide.AutomationFramework.Assignment.User
{
    public class UserApiClient : ApiClientBase, IUserApiClient
    {
        public UserApiClient(
            IClient client,
            ILogger logger,
            IEnvironmentConfiguration environmentConfiguration)
            : base(client, logger)
        {
            client.SetBaseUri(environmentConfiguration.EnvironmentUri);
        }

        public async Task<UserLoginResponse> GetUserTokenByCredentialsAsync(
            UserLoginRequest userLoginRequest)
        {
            Logger.Information(
                "Start '{@Method}' with {@userLoginRequest} as user credentials",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                userLoginRequest);

            var restResponse = await ExecutePostAsync(
                "/user/login", userLoginRequest);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return restResponse.GetModel<UserLoginResponse>();
        }
    }
}
