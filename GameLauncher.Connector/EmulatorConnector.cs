using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Models.EpicGame;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector;
public class EmulatorConnector
{
    private readonly RestClient _client;

    public EmulatorConnector(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    public async Task<IEnumerable<LUEmulateur>> GetLocalEmusAsync()
    {
        var request = new RestRequest("/api/Emulateur/GetLocalEmu", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<LUEmulateur>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<LUEmulateur>();
        }
    }
    public async Task<IEnumerable<LUProfile>> GetLocalProfilesAsync()
    {
        var request = new RestRequest("/api/Emulateur/GetLocalProfile", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<LUProfile>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<LUProfile>();
        }
    }
    public async Task<IEnumerable<LUEmulateur>> ScanFolder(string folder)
    {
        var request = new RestRequest($"api/Emulateur/ScanFolder", Method.Post);
        FolderRequest folderRequest = new FolderRequest { Folder = folder };
        request.AddJsonBody(folderRequest);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<LUEmulateur>>(response.Content);
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new NotificationMessage { Type = Models.APIObject.MsgCategory.Error, MessageTitle = response.ErrorException.ToString(), MessageCorps = response.ErrorMessage });
            return new List<LUEmulateur>();
        }
    }
    public async Task ScanWithProfile(ScanProfile profile)
    {
        var request = new RestRequest($"api/Emulateur/ScanProfile", Method.Post);
        request.AddJsonBody(profile);
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            WeakReferenceMessenger.Default.Send(new NotificationMessage { Type = Models.APIObject.MsgCategory.Error, MessageTitle = response.ErrorException.ToString(), MessageCorps = response.ErrorMessage });
            Console.WriteLine("Error: " + response.ErrorMessage);
        }
    }
}
