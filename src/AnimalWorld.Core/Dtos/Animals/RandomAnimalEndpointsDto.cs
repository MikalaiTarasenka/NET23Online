using System.Text.Json.Serialization;

namespace AnimalWorld.Core.Dtos.Animals
{
    public class RandomAnimalEndpointsDto
    {
        [JsonPropertyName("endpoints")]
        public List<string> Endpoints { get; set; }
    }
}
