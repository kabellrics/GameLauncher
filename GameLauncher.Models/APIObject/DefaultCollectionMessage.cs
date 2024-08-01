using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class DefaultCollectionMessage
{
    public bool CollecAllGames
    {
        get;set;
    }
    public bool CollecFavorite
    {
        get; set;
    }
    public bool CollecNeverPlayed
    {
        get; set;
    }
    public bool CollecLastPlayed
    {
        get; set;
    }
    public bool CollecEmulator
    {
        get; set;
    }
}
