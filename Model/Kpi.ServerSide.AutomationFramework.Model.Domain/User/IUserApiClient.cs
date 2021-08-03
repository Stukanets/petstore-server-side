using System.Threading.Tasks;

namespace Kpi.ServerSide.AutomationFramework.Model.Domain.User
{
    public interface IUserApiClient
    {
        Task<UserLoginResponse> CreateUserTokenByCredentialsAsync(
            UserLoginRequest userLoginRequest);
    }
}
