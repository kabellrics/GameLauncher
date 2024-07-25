using System.Threading.Channels;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface;
public interface INotificationService
{
    Task SendMessage(NotificationMessage message);
    Task SendJsonMessage(String message);
}