using System.Threading;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;

namespace Kpi.ServerSide.AutomationFramework.Assignment.User
{
    public class UserContext : IUserContext
    {
        private readonly IUserApiClient _userApiClient;

        public UserContext(
            IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        public async Task<UserLoginResponse> GetUserTokenByCredentialsAsync(
            UserLoginRequest userLoginRequest)
        {
            var checksDone = 0;
            while (true)
            {
                try
                {
                    var result = await _userApiClient.GetUserTokenByCredentialsAsync(userLoginRequest);
                    return result;
                }
                catch
                {
                    Thread.Sleep(500);
                    checksDone++;
                    if (checksDone == 5)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
