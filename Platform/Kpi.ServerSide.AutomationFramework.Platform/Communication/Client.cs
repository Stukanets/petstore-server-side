using System;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using RestSharp;

namespace Kpi.ServerSide.AutomationFramework.Platform.Communication
{
    public class Client : IClient
    {
        private readonly IRestClient _restClient;

        public Client(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            if (string.IsNullOrEmpty(_restClient.BaseUrl?.ToString()))
            {
                throw new Exception("Base uri was not set.");
            }

            var response = await _restClient.ExecuteAsync<IRestResponse>(request);
            Console.WriteLine($"Log rest request. ResponseUri: {response.ResponseUri}, StatusCode: {response.StatusCode}, Method: {response.Request.Method}, ErrorMessage: {response.ErrorMessage}, ErrorException: {response.ErrorException}");
            return response;
        }

        public void SetBaseUri(string baseUri) => _restClient.BaseUrl = new Uri(baseUri);
    }
}
