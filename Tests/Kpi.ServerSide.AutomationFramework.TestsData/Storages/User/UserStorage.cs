using System.Collections.Generic;
using Bogus;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;

namespace Kpi.ServerSide.AutomationFramework.TestsData.Storages.User
{
    public static class UserStorage
    {
        public static Dictionary<string, UserLoginRequest> UserRequests =>
            new Dictionary<string, UserLoginRequest>
            {
                { "ValidUser", ValidUser }
            };

        private static UserLoginRequest ValidUser =>
            new Faker<UserLoginRequest>()
                .RuleFor(u => u.Email, "stukaniets2005@gmail.com")
                .RuleFor(u => u.Password, "egumas20052");
    }
}
