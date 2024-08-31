using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Uri = System.Uri;

using JokeUI.Models;
using JokeUI.Configuration;

namespace JokeUI.Services
{
    public class JokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.Value.BaseUrl);
        }

        public async Task<IEnumerable<Joke>> GetAllJokes()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Joke>>("api/jokes");
        }

        public async Task<Joke> GetJokeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Joke>($"api/jokes/{id}");
        }

        public async Task<Joke> AddJoke(Joke joke)
        {
            var response = await _httpClient.PostAsJsonAsync("api/jokes", joke);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Joke>();
        }

        public async Task<Joke> GetRandomJoke()
        {
            return await _httpClient.GetFromJsonAsync<Joke>("api/jokes/random");
        }
    }
}