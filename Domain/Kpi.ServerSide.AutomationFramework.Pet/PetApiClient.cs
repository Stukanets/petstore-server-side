using System.Reflection;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Client;
using Kpi.ServerSide.AutomationFramework.Platform.Communication.Http;
using Kpi.ServerSide.AutomationFramework.Platform.Configuration.Environment;
using Serilog;

namespace Kpi.ServerSide.AutomationFramework.Pet
{
    public class PetApiClient : ApiClientBase, IPetApiClient
    {
        public PetApiClient(
            IClient client,
            ILogger logger,
            IEnvironmentConfiguration environmentConfiguration)
            : base(client, logger)
        {
            client.SetBaseUri(environmentConfiguration.EnvironmentUri);
            Logger.Information($"PetStore: Set base uri: {environmentConfiguration.EnvironmentUri}");
        }

        public async Task<PetResponse> GetPetByIdAsync(
            long petId)
        {
            var restResponse = await ExecuteGetAsync(
                $"/v2/pet/{petId}");

            return restResponse.GetModel<PetResponse>();
        }

        public async Task<ResponseMessage> GetPetByIdResponseAsync(
            long petId)
        {
            Logger.Information(
                "Start '{@Method}' with {@petId}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                petId);

            var restResponse = await ExecuteGetAsync(
                $"/v2/pet/{petId}");

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<ResponseMessage> CreatePetResponseAsync(
            PetRequest petRequest)
        {
            Logger.Information(
                "Start '{@Method}' with {@petRequest}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                petRequest);

            var restResponse = await ExecutePostAsync(
                "/v2/pet",
                petRequest);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<CreatePetResponse> CreatePetAsync(
            PetRequest petRequest)
        {
            Logger.Information(
                "Start '{@Method}' with {@petRequest}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                petRequest);

            var restResponse = await ExecutePostAsync(
                "/v2/pet",
                petRequest);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);
            return restResponse.GetModel<CreatePetResponse>();
        }

        public async Task<ResponseMessage> DeletePetResponseAsync(
            long petId)
        {
            Logger.Information(
                "Start '{@Method}' with {@petId} pet id",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                petId);

            var restResponse = await ExecuteDeleteAsync(
                $"/v2/pet/{petId}");

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }

        public async Task<ResponseMessage> UpdatePetResponseAsync(
            PetRequest petRequest)
        {
            Logger.Information(
                "Start '{@Method}' with {@petRequest} as new pet body",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                petRequest);

            var restResponse = await ExecutePutAsync(
                "/v2/pet/",
                petRequest);

            Logger.Information(
                "Finished '{@Method}' with {@restResponse}",
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                restResponse);

            return new ResponseMessage
            {
                Content = restResponse.Content,
                StatusCode = restResponse.StatusCode.ToString()
            };
        }
    }
}
