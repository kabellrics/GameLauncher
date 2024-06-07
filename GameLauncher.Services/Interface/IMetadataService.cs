using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IMetadataService
{
    IEnumerable<MetadataGenre> GetAll();
    IEnumerable<MetadataGenre> GetAllForItem(Guid id);
}