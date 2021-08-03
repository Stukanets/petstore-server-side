using System;
using Newtonsoft.Json;

namespace Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment
{
    public class Assignment : AssignmentRequest
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public bool Completed { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
