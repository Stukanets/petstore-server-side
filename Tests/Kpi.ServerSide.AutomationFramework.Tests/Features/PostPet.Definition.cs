using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Pet Creation")]
    public class PostUserDefinition
    {
        private readonly PetRequest _newPet;
        private readonly IPetContext _petContext;

        public PostUserDefinition(
            IPetContext petContext)
        {
            _petContext = petContext;
            _newPet = PetStorage.PetRequests["RandomPet"];
        }

        [Given(@"I have free API with swagger")]
        public void GivenIHaveFreeApiWithSwagger()
        {
        }

        [When(@"I send the pet creation request with provided model")]
        public async Task WhenISendThePetCreationRequestWithProvidedModel()
        {
            await _petContext.CreatePetResponseAsync(
                _newPet);
        }

        [Then(@"I see created pet in the get response")]
        public async Task ThenISeeCreatedPetInTheGetResponse()
        {
            var createdPet = await _petContext.GetPetByIdAsync(
                _newPet.Id);
            createdPet.Name.Should().Be(
                _newPet.Name);
        }
    }
}
