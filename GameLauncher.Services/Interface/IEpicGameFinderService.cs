namespace GameLauncher.Services.Interface;

public interface IEpicGameFinderService
{
    Task GetGameAsync();
    Task CleaningGame();
}