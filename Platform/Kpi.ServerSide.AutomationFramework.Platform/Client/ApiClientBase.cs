using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Client;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Communication.Http;
using RestSharp;
using Serilog;

namespace Kpi.ServerSide.AutomationFramework.Platform.Client
{
    public abstract class ApiClientBase : IApiClient
    {
        protected readonly ILogger Logger;

        private readonly IClient _client;

        protected ApiClientBase(
            IClient client,
            ILogger logger)
        {
            _client = client;
            Logger = logger;
        }

        public async Task<T1> ExecutePostAsync<T1, T2>(string uri, T2 body, string accessToken = null)
            where T1 : class
            where T2 : class
        {
            var request = CreateRequest(uri, Method.POST, body, accessToken);
            var response = await _client.ExecuteAsync(request);
            return response.GetModel<T1>();
        }

        public async Task<IRestResponse> ExecutePostAsync<T1>(string uri, T1 body, string accessToken = null)
            where T1 : class
        {
            var request = CreateRequest(uri, Method.POST, body, accessToken);
            var response = await _client.ExecuteAsync(request);
            return response;
        }

        public async Task<T1> ExecuteGetAsync<T1>(string uri, string accessToken = null)
            where T1 : class
        {
            var request = CreateRequest<IRestRequest>(uri, Method.GET, null, accessToken);
            var response = await _client.ExecuteAsync(request);
            return response.GetModel<T1>();
        }

        protected async Task<IRestResponse> ExecutePostResponseAsync(string uri, string accessToken)
        {
            var request = new RestRequest(uri, Method.POST)
                .AddAuthorizationHeader(accessToken);
            return await _client.ExecuteAsync(request);
        }

        protected async Task<IRestResponse> ExecuteGetAsync(string uri, string accessToken = null)
        {
            var request = new RestRequest(uri, Method.GET)
                .AddAuthorizationHeader(accessToken);
            return await _client.ExecuteAsync(request);
        }

        protected async Task<IRestResponse> ExecuteDeleteAsync(string uri, string accessToken = null)
        {
            var request = new RestRequest(uri, Method.DELETE)
                .AddAuthorizationHeader(accessToken);
            return await _client.ExecuteAsync(request);
        }

        protected async Task<IRestResponse> ExecutePatchAsync<T>(string uri, T body, string accessToken)
            where T : class
        {
            return await _client.ExecuteAsync(CreateRequest(uri, Method.PATCH, body, accessToken));
        }

        protected async Task<T> ExecutePostAsync<T>(string uri, string accessToken)
        {
            var request = CreateRequest(uri, Method.POST, accessToken);
            var response = await _client.ExecuteAsync(request);
            return response.GetModel<T>();
        }

        protected async Task<IRestResponse> ExecutePutAsync<T2>(string uri, T2 body, string accessToken = null)
            where T2 : class
        {
            var request = CreateRequest(uri, Method.PUT, body, accessToken);
            var response = await _client.ExecuteAsync(request);
            return response;
        }

        protected virtual IRestRequest CreateRequest<T>(string uri, Method method, T body, string accessToken = null)
        {
            var restRequest = new RestRequest(uri, method);
            if (body != null)
            {
                restRequest.AddJsonBody(body);
            }

            restRequest.AddAuthorizationHeader(accessToken);
            restRequest.AddHeader("ApplicationIdentifier", "KycProxyAutomation");
            return restRequest;
        }
    }
}
