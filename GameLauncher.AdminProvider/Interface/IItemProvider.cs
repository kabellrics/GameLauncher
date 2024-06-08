using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface IItemProvider
{
    IAsyncEnumerable<ObservableItem> GetAllItemsAsyncEnumerable();
    Task<IEnumerable<ObservableItem>> GetAllItemsAsync();
    Task<IEnumerable<ObservableGroupItem>> GetAllItemsGrouped();
}