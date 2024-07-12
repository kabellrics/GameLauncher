using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface;
public interface INotificationService
{
    Task SendMessage(NotificationMessage message);
}