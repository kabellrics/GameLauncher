namespace GameLauncher.Services.Interface.Front;

public interface IStartingService
{
    string RemoveFirstAndLastCharacter(string input);
    Task StartITem(Guid itemid);
}