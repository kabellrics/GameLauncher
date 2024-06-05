﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class Develloppeur
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
    public List<Item> Items
    {
        get; set;
    }
}