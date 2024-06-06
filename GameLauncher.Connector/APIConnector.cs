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

        // Méthode pour obtenir un jeu Steam
        public async Task GetSteamGameAsync()
        {
            var request = new RestRequest("/GetSteamGame", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Steam Game: " + response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
            }
        }

        // Méthode pour obtenir un jeu EA Origin
        public async Task GetEAOriginGameAsync()
        {
            var request = new RestRequest("/GetEAOriginGame", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("EA Origin Game: " + response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
            }
        }

        // Méthode pour obtenir un jeu Epic
        public async Task GetEpicGameAsync()
        {
            var request = new RestRequest("/GetEpicGame", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Epic Game: " + response.Content);
            }
            else
            {
                Console.WriteLine("Error: " + response.ErrorMessage);
            }
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

    }
}
