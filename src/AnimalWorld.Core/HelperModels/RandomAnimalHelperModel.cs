using System.Text.Json.Serialization;

namespace AnimalWorld.Core.HelperModels
{
    public class RandomAnimalHelperModel
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("fact")]
        public string Fact { get; set; }
    }
}
