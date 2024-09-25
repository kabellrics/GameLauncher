using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Services.Interface;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class StoreProvider : IStoreProvider
{
    private readonly StoreConnector storeconnector;
    private readonly IEpicGameFinderService epicService;
    private readonly IEAOriginGameFinderService EAOriginService;
    private readonly ISteamGameFinderService steamService;
    public StoreProvider(IEpicGameFinderService epic, IEAOriginGameFinderService EAOrigin, ISteamGameFinderService steam)
    {
        //storeconnector = new StoreConnector("https://localhost:7197");
        epicService = epic;
        EAOriginService = EAOrigin;
        steamService = steam;
    }
    public async Task GetSteamGameAsync()
    {
        await steamService.GetGameAsync();
    }
    public async Task GetEAOriginGameAsync()
    {
        await EAOriginService.GetGameAsync();
    }
    public async Task GetEpicGameAsync()
    {
        await epicService.GetGameAsync();
    }
    public async Task CleaningSteamGameAsync()
    {
        await steamService.CleaningGame();
    }
    public async Task CleaningEpicGameAsync()
    {
        await epicService.CleaningGame();
    }
    public async Task CleaningEAGameAsync()
    {
        await EAOriginService.CleaningGame();
    }
}
