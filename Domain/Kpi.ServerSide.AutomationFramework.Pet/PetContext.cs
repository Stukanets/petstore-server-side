using System.Threading;
using System.Threading.Tasks;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;

namespace Kpi.ServerSide.AutomationFramework.Pet
{
    public class PetContext : IPetContext
    {
        private readonly IPetApiClient _petApiClient;

        public PetContext(
            IPetApiClient petApiClient)
        {
            _petApiClient = petApiClient;
        }

        public async Task<PetResponse> GetPetByIdAsync(
            long petId)
        {
            var checksDone = 0;
            var result = await _petApiClient.GetPetByIdAsync(petId);
            while (checksDone != 5)
            {
                if (result.Name != null)
                {
                    break;
                }

                Thread.Sleep(1000);
                result = await _petApiClient.GetPetByIdAsync(petId);
                checksDone++;
            }

            return result;
        }

        public async Task<ResponseMessage> GetPetByIdResponseAsync(
            long petId)
        {
            var checksDone = 0;
            var result = await _petApiClient.GetPetByIdResponseAsync(petId);
            while (checksDone != 5)
            {
                if (result.StatusCode == "NotFound")
                {
                    break;
                }

                Thread.Sleep(1000);
                result = await _petApiClient.GetPetByIdResponseAsync(petId);
                checksDone++;
            }

            return result;
        }

        public async Task<ResponseMessage> CreatePetResponseAsync(
            PetRequest petRequest)
        {
            return await _petApiClient.CreatePetResponseAsync(petRequest);
        }

        public async Task<CreatePetResponse> CreatePetAsync(
            PetRequest petRequest)
        {
            return await _petApiClient.CreatePetAsync(petRequest);
        }

        public async Task<ResponseMessage> DeletePetResponseAsync(
            long petId)
        {
            return await _petApiClient.DeletePetResponseAsync(petId);
        }

        public async Task<ResponseMessage> UpdatePetResponseAsync(
            PetRequest petRequest)
        {
            return await _petApiClient.UpdatePetResponseAsync(petRequest);
        }
    }
}
