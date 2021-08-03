using System;
using Newtonsoft.Json;
using RestSharp;

namespace Kpi.ServerSide.AutomationFramework.Platform.Communication.Http
{
    public static class RestSharpExtensions
    {
        public static T GetModel<T>(this IRestResponse response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception($"Not be able to DeserializeObject. Content {response.Content} with exception: {e}.");
            }
        }

        public static IRestRequest AddAuthorizationHeader(this IRestRequest request, string accessToken) =>
            request.AddHeader("Authorization", accessToken);
    }
}
