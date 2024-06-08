using GameLauncher.Models.IGDB;

namespace GameLauncher.Services.Interface;
public interface IIGDBService
{
    string GetArtWorkLink(string hash);
    //IEnumerable<Artwork> GetArtworksByGameId(int id);
    string GetCoverLink(string hash);
    IGDBGame GetDetailsGame(int id);
    IEnumerable<Company> GetCompaniesDetail(IEnumerable<string> involvedComps);
    //IEnumerable<Company> GetDevByGameId(IEnumerable<InvolvedCompany> involvedComps);
    IEnumerable<SearchResult> GetGameByName(string name);
    //IEnumerable<Genre> GetGenresByGameId(int id);
    //IEnumerable<InvolvedCompany> GetInvolvedCompanyByGameId(int id);
    //IEnumerable<Company> GetPublishersByGameId(IEnumerable<InvolvedCompany> involvedComps);
    IEnumerable<Video> GetVideosByGameId(int id);
}