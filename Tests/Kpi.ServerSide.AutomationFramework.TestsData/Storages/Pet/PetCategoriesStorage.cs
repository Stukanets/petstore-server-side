using Bogus;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Generators;

namespace Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet
{
    public static class PetCategoriesStorage
    {
        public static Category DefaultCategory =>
            new Faker<Category>()
                .RuleFor(u => u.Id, RandomGenerator.GetRandomPositiveNumber())
                .RuleFor(u => u.Name, RandomGenerator.RandomString());
    }
}
