using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.Platform.Waiters;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public async Task<UserLoginResponse> CreateUserTokenByCredentialsAsync(
            UserLoginRequest userLoginRequest)
        {
            var exceptions = new List<Exception>();
            for (var attempted = 0; attempted < 5; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(500);
                    }

                    return await _userApiClient.CreateUserTokenByCredentialsAsync(userLoginRequest);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
