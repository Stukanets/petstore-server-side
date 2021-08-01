using Bogus;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Generators;

namespace Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet
{
    public static class PetTagsStorage
    {
        public static Tag Default =>
            new Faker<Tag>()
                .RuleFor(u => u.Name, RandomGenerator.RandomString())
                .RuleFor(u => u.Id, RandomGenerator.GetRandomPositiveNumber());
    }
}
