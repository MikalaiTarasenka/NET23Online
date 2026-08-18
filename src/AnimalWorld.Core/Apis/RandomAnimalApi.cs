using AnimalWorld.Core.HelperModels;
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
            var endpoints = await _httpClient.GetFromJsonAsync<RandomAnimalEndpointsHelperModel>("/animal");
            return endpoints.Endpoints;
        }

        public async Task<RandomAnimalHelperModel> GetRandomAnimal(string type)
        {
            return await _httpClient.GetFromJsonAsync<RandomAnimalHelperModel>(type);
        }
    }
}
