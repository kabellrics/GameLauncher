using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Models;
[Keyless]
public partial class LUPlatformes
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

    [JsonProperty("Id")]
    public string Codename
    {
        get; set;
    }

    [JsonProperty("IgdbId", NullValueHandling = NullValueHandling.Ignore)]
    public long? IgdbId
    {
        get; set;
    }

    [JsonProperty("Databases", NullValueHandling = NullValueHandling.Ignore)]
    public string[]? Databases
    {
        get; set;
    }

    [JsonProperty("Emulators", NullValueHandling = NullValueHandling.Ignore)]
    public string[]? Emulators
    {
        get; set;
    }
}
