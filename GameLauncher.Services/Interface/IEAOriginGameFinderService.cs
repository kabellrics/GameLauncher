namespace GameLauncher.Services.Interface;

public interface IEAOriginGameFinderService
{
    Task GetGameAsync();
    Task CleaningGame();
}