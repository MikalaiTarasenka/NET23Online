using System.Text.Json.Serialization;

namespace AnimalWorld.Core.Dtos.Animals
{
    public class RandomAnimalDto
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("fact")]
        public string Fact { get; set; }
    }
}
