using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameLauncher.Models;
public class LUEmulateur
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string ID
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public Uri Website
    {
        get; set;
    }
    public List<LUProfile> Profiles
    {
        get; set;
    }
}
