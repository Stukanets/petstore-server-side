using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Update Pet by Id")]
    public class PutUserDefinition
    {
        private readonly IPetContext _petContext;
        private readonly PetRequest _newPet;
        private readonly PetRequest _newPetInfo;

        public PutUserDefinition(
            IPetContext petContext)
        {
            _petContext = petContext;
            _newPet = PetStorage.PetRequests["RandomPet"];
            _newPetInfo = PetStorage.PetRequests["RandomPet"];
        }

        [Given(@"I have free API with swagger")]
        public void GivenIHaveFreeApiWithSwagger()
        {
        }

        [When(@"I create Pet by post request")]
        public async Task WhenICreatePetByPostRequest()
        {
            await _petContext.CreatePetResponseAsync(
                _newPet);
        }

        [When(@"I send the Pet update request with same Id and new body")]
        public async Task WhenISendThePetUpdateRequestWithSameIdAndNewBody()
        {
            _newPetInfo.Id = _newPet.Id;
            await _petContext.UpdatePetResponseAsync(
                _newPetInfo);
        }

        [Then(@"I see updated Pet")]
        public async Task ThenISeeUpdatedPet()
        {
            var actual = await _petContext.GetPetByIdAsync(
                _newPetInfo.Id);
            actual.Name.Should().BeEquivalentTo(_newPetInfo.Name);
        }
    }
}
