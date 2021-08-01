using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Get Pet by Id")]
    public class GetUserDefinition
    {
        private readonly IPetContext _petContext;
        private readonly PetRequest _newPet;
        private PetResponse _petResponse;

        public GetUserDefinition(
            IPetContext petContext)
        {
            _petContext = petContext;
            _newPet = PetStorage.PetRequests["RandomPet"];
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

        [When(@"I receive get Pet by id response")]
        public async Task WhenIReceiveGetPetByIdResponse()
        {
            _petResponse = await _petContext.GetPetByIdAsync(
                _newPet.Id);
        }

        [Then(@"I see returned Pet details which are equal with created")]
        public void ThenISeeReturnedPetDetailsWhichAreEqualWithCreated()
        {
            _petResponse.Name.Should().Be(
                _newPet.Name);
        }
    }
}
