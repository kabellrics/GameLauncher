using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class StoreProvider : IStoreProvider
{
    private readonly StoreConnector storeconnector;
    public StoreProvider()
    {
        storeconnector = new StoreConnector("https://localhost:7197");
    }
    public async Task GetSteamGameAsync()
    {
        await storeconnector.GetSteamGameAsync();
    }
    public async Task GetEAOriginGameAsync()
    {
        await storeconnector.GetEAOriginGameAsync();
    }
    public async Task GetEpicGameAsync()
    {
        await storeconnector.GetEpicGameAsync();
    }
    public async Task CleaningSteamGameAsync()
    {
        await storeconnector.CleaningSteamGameAsync();
    }
    public async Task CleaningEpicGameAsync()
    {
        await storeconnector.CleaningEpicGameAsync();
    }
    public async Task CleaningEAGameAsync()
    {
        await storeconnector.CleaningEAGameAsync();
    }
}
