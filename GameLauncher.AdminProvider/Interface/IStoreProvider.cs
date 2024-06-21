namespace GameLauncher.AdminProvider.Interface;

public interface IStoreProvider
{
    Task CleaningEAGameAsync();
    Task CleaningEpicGameAsync();
    Task CleaningSteamGameAsync();
    Task GetEAOriginGameAsync();
    Task GetEpicGameAsync();
    Task GetSteamGameAsync();
}