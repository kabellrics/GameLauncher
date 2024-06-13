using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class LUPlatformes
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
    public string CodeName
    {
        get; set;
    }
    public long? IgdbId
    {
        get; set;
    }
    public string Databases
    {
        get; set;
    }
    public List<LUEmulateur>? Emulators
    {
        get; set;
    }
}
