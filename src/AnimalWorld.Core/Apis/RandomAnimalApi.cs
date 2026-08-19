using AnimalWorld.Core.Dtos.Animals;
using System.Net.Http.Json;

namespace AnimalWorld.Core.Apis
{
    public class RandomAnimalApi
    {
        private HttpClient _httpClient;

        public RandomAnimalApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetAnimalSpecies()
        {
            var endpoints = await _httpClient.GetFromJsonAsync<RandomAnimalEndpointsDto>("/animal");
            return endpoints.Endpoints;
        }

        public async Task<RandomAnimalDto> GetRandomAnimal(string type)
        {
            return await _httpClient.GetFromJsonAsync<RandomAnimalDto>(type);
        }
    }
}
