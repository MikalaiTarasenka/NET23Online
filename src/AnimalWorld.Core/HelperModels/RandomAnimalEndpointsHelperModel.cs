using System.Text.Json.Serialization;

namespace AnimalWorld.Core.HelperModels
{
    public class RandomAnimalEndpointsHelperModel
    {
        [JsonPropertyName("endpoints")]
        public List<string> Endpoints { get; set; }
    }
}
