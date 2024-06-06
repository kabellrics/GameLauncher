using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface IItemProvider
{
    IAsyncEnumerable<ObservableItem> GetAllItems();
}