using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace GameLauncher.Models;
[Keyless]
public class LUEmulateur
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]

    [JsonProperty("Id")]
    public string Id
    {
        get; set;
    }

    [JsonProperty("Name")]
    public string Name
    {
        get; set;
    }

    [JsonProperty("Website")]
    public string Website
    {
        get; set;
    }
    public bool IsLocal
    {
        get; set;
    }

    [JsonProperty("Profiles")]
    public List<Guid>? Profiles
    {
        get; set;
    }
}
