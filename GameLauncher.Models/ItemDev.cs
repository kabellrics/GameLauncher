using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameLauncher.Models;
public class ItemDev
{
    public Guid ID
    {
        get; set;
    }
    public Guid DevelloppeurID
    {
        get; set;
    }
    public Guid ItemID
    {
        get; set;
    }
    [JsonIgnore]
    public Item Item
    {
        get; set;
    }
    [JsonIgnore]
    public Develloppeur Develloppeur
    {
        get;set;
    }
}
