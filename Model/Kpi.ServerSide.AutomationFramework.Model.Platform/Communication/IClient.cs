using System.Threading.Tasks;
using RestSharp;

namespace Kpi.ServerSide.AutomationFramework.Model.Platform.Communication
{
    public interface IClient
    {
        Task<IRestResponse> ExecuteAsync(IRestRequest request);

        void SetBaseUri(string baseUri);
    }
}
