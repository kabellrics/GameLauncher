namespace GameLauncher.Front.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
