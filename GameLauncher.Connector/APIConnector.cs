using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Models.EpicGame;
using Newtonsoft.Json;
using RestSharp;
using Item = GameLauncher.Models.Item;

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

        public async Task UpdateItemAsync(Item item)
        {
            var request = new RestRequest($"/api/Items/{item.ID}", Method.Put);
            request.AddParameter("id", item.ID, ParameterType.UrlSegment);
            request.AddJsonBody(item);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Update Failed");
            }
        }

        public async Task UpdateGenreForItem(Item item, List<Genre> newGenres)
        {
            var request = new RestRequest($"/api/Genres/ChangeGenreForItem", Method.Post);
            var itemmessage = new UpdateGenreMessage() { newGenres = newGenres,Item = item };
            request.AddJsonBody(itemmessage); var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                //throw new Exception("Update Failed");
            }
        }
        public async Task UpdateDevForItem(Item item, List<Develloppeur> newDevs)
        {
            var request = new RestRequest($"/api/Dev/ChangeDevForItem", Method.Post);
            var itemmessage = new UpdateDevMessage() { newDevs = newDevs, Item = item };
            request.AddJsonBody(itemmessage); var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                //throw new Exception("Update Failed");
            }
        }
        public async Task UpdateEditForItem(Item item, List<Editeur> newEdits)
        {
            var request = new RestRequest($"/api/Editeurs/ChangeEditeurForItem", Method.Post);
            var itemmessage = new UpdateEditeurMessage() { newEditeurs = newEdits, Item = item };
            request.AddJsonBody(itemmessage); var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                //throw new Exception("Update Failed");
            }
        }
    }
}
