using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector
{
    public class GameLauncherClient
    {
        private readonly RestClient _client;

        public GameLauncherClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }
        // Méthode pour obtenir des items
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var request = new RestRequest("/api/Items", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Item>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Item>();
            }
        }
        public async Task<IEnumerable<Develloppeur>> GetDevsAsync()
        {
            var request = new RestRequest("/api/Dev", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Develloppeur>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Develloppeur>();
            }
        }
        public async Task<IEnumerable<Editeur>> GetEditeursAsync()
        {
            var request = new RestRequest("/api/Editeurs", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Editeur>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Editeur>();
            }
        }
        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            var request = new RestRequest("/api/Genres", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Genre>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Genre>();
            }
        }
        public async Task<IEnumerable<LUPlatformes>> GetPlatformesAsync()
        {
            var request = new RestRequest("/api/Platforme", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<LUPlatformes>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<LUPlatformes>();
            }
        }
        public async Task<IEnumerable<Develloppeur>> GetDevsByItemAsync(Guid id)
        {
            var request = new RestRequest($"/api/Dev/ByItem/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Develloppeur>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Develloppeur>();
            }
        }
        public async Task<IEnumerable<Editeur>> GetEditeursByItemAsync(Guid id)
        {
            var request = new RestRequest($"/api/Editeurs/ByItem/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Editeur>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Editeur>();
            }
        }
        public async Task<IEnumerable<Genre>> GetGenresByItemAsync(Guid id)
        {
            var request = new RestRequest($"/api/Genres/ByItem/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Items: " + response.Content);
                return JsonConvert.DeserializeObject<IEnumerable<Genre>>(response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
                return new List<Genre>();
            }
        }
    }
}
