using System.ComponentModel.DataAnnotations.Schema;

namespace GameLauncher.Models;

public class LUProfile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    
    public string LUEmulateurId
    {
        get; set;
    }
    public string StartupArguments
    {
        get; set;
    }
    public List<LUPlatformes> Platformes
    {
        get; set;
    }
    public string ImageExtensions
    {
        get; set;
    }
    public string ProfileFiles
    {
        get; set;
    }
    public string StartupExecutable
    {
        get; set;
    }
}