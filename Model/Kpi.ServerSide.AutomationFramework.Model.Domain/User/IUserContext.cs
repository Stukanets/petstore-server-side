using System.Threading.Tasks;

namespace Kpi.ServerSide.AutomationFramework.Model.Domain.User
{
    public interface IUserContext
    {
        Task<UserLoginResponse> GetUserTokenByCredentialsAsync(
            UserLoginRequest userLoginRequest);
    }
}
