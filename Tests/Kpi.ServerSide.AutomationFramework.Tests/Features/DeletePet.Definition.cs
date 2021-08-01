using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Delete Pet by Id")]
    public class DeleteUserDefinition
    {
        private readonly PetRequest _newPet;
        private readonly IPetContext _petContext;
        private ResponseMessage _responseMessage;

        public DeleteUserDefinition(
            IPetContext petContext)
        {
            _petContext = petContext;
            _newPet = PetStorage.PetRequests["RandomPet"];
        }

        [Given(@"I have free API with swagger")]
        public void GivenIHaveFreeApiWithSwagger()
        {
        }

        [When(@"I create pet by post request")]
        public async Task WhenICreatePetByPostRequest()
        {
            await _petContext.CreatePetResponseAsync(
                _newPet);
        }

        [When(@"I send delete request with created pet id")]
        public async Task WhenISendDeleteRequestWithCreatedPetId()
        {
            _responseMessage = await _petContext.DeletePetResponseAsync(
                _newPet.Id);
        }

        [Then(@"I see (.*) status code")]
        public void ThenISeeOkStatusCode(string statusCode)
        {
            _responseMessage.StatusCode.Should().Be(
                statusCode);
        }

        [Then(@"I see (.*) on get request")]
        public async Task ThenISeeNotFoundOnGetRequest(string status)
        {
            var actual = await _petContext.GetPetByIdResponseAsync(
                _newPet.Id);
            actual.StatusCode.Should().BeEquivalentTo(status);
        }

        [When(@"I send delete request with invalid pet id")]
        public async Task WhenISendDeleteRequestWithInvalidPetId()
        {
            _responseMessage = await _petContext.DeletePetResponseAsync(
                0);
        }
    }
}
