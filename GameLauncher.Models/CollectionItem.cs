﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameLauncher.Models;
public class CollectionItem
{
    public Guid ID
    {
        get; set;
    }
    public Guid CollectionID
    {
        get; set;
    }
    public Guid ItemID
    {
        get; set;
    }
    public int Order
    {
        get;set;
    }
    //[JsonIgnore]
    //public Collection? Collection { get; set; }

    //[JsonIgnore]
    //public Item? Item { get; set; }
}
