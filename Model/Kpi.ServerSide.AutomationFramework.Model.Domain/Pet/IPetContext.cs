using System.Threading.Tasks;

namespace Kpi.ServerSide.AutomationFramework.Model.Domain.Pet
{
    public interface IPetContext
    {
        Task<PetResponse> GetPetByIdAsync(
            long petId);

        Task<ResponseMessage> GetPetByIdResponseAsync(
            long petId);

        Task<ResponseMessage> CreatePetResponseAsync(
            PetRequest petRequest);

        Task<CreatePetResponse> CreatePetAsync(
            PetRequest petRequest);

        Task<ResponseMessage> DeletePetResponseAsync(
            long petId);

        Task<ResponseMessage> UpdatePetResponseAsync(
            PetRequest petRequest);
    }
}
