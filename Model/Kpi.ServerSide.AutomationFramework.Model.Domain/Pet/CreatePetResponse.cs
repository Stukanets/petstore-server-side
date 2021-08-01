namespace Kpi.ServerSide.AutomationFramework.Model.Domain.Pet
{
    public class CreatePetResponse
    {
        public long Id { get; set; }

        public string[] PhotoUrls { get; set; }

        public Tag[] Tags { get; set; }
    }
}
