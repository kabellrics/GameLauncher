
namespace GameLauncher.ObservableObjet;

public interface IObservableBaseGenre
{
    Guid Id
    {
        get;
    }
    string Name
    {
        get;
        set;
    }
}