using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface ISteamGameFinderService
{
    Task GetGameAsync();
    Task CleaningGame();
}