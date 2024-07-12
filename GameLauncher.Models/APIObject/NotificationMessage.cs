using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class NotificationMessage
{
    public string Message
    {
        get;set;
    }
    public MsgType Type
    {
        get;set;
    }
}
public enum MsgType
{
    NeedUpdate,
    Info
}
