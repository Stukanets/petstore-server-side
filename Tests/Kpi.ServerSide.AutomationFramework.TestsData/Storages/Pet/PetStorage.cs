using System.Collections.Generic;
using Bogus;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Generators;

namespace Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet
{
    public static class PetStorage
    {
        public static Dictionary<string, PetRequest> PetRequests =>
            new Dictionary<string, PetRequest>
            {
                { "RandomPet", RandomPet }
            };

        private static PetRequest RandomPet =>
        new Faker<PetRequest>()
            .RuleFor(u => u.Id, RandomGenerator.GetRandomPositiveNumber())
            .RuleFor(u => u.Category, PetCategoriesStorage.DefaultCategory)
            .RuleFor(u => u.Name, RandomGenerator.RandomString())
            .RuleFor(u => u.Status, RandomGenerator.RandomString())
            .RuleFor(u => u.PhotoUrls, value: new[] { RandomGenerator.RandomString() })
            .RuleFor(u => u.Tags, new[] { PetTagsStorage.Default });
    }
}
