namespace Kpi.ServerSide.AutomationFramework.Assignment
{
    internal static class Token
    {
        internal static string BearerTokenGenerator(string accessToken)
        {
            return $"Bearer {accessToken}";
        }
    }
}
