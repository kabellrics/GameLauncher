namespace GameLauncher.Models;

public interface ISearchResult
{
    int id
    {
        get;
        set;
    }
    string name
    {
        get;
        set;
    }

    string ToString();
}