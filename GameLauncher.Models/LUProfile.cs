using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameLauncher.Models;
[Keyless]
public class LUProfile
{

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id
    {
        get; set;
    }
    [JsonProperty("Name")]
    public string Name
    {
        get; set;
    }

    [JsonProperty("StartupArguments")]
    public string StartupArguments
    {
        get; set;
    }

    [JsonProperty("Platforms")]
    public string[]? Platforms
    {
        get; set;
    }

    [JsonProperty("ImageExtensions")]
    public string[]? ImageExtensions
    {
        get; set;
    }

    [JsonProperty("ProfileFiles")]
    public string[]? ProfileFiles
    {
        get; set;
    }

    [JsonProperty("StartupExecutable")]
    public string? StartupExecutable
    {
        get; set;
    }
    public string LUEmulateurId
    {
        get; set;
    }
    public bool IsLocal
    {
        get; set;
    }
}