using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class NotificationMessage
{
    public string MessageTitle
    {
        get;set;
    }
    public string MessageCorps
    {
        get;set;
    }
    public MsgCategory Type
    {
        get;set;
    }
    public string Icon
    {
        get
        {
            switch (Type)
            {
                case MsgCategory.Create:return "\uE710";
                    break;
                case MsgCategory.Update:
                    return "\uE777";
                    break;
                case MsgCategory.Delete:
                    return "\uE74D";
                    break;
                case MsgCategory.StartTask:
                    return "\uE768";
                    break;
                case MsgCategory.EndTask:
                    return "\uE930";
                    break;
                case MsgCategory.Error:
                    return "\uE783";
                    break;
                default:
                    return "\uE946";
                    break;
            }
        }
    }
}
public enum MsgCategory
{
    Create,
    Update,
    Delete,
    StartTask,
    EndTask,
    Error,
    Info
}
//public class MsgCategory
//{
//    private MsgCategory(string value)
//    {
//        Value = value;
//    }
//    private MsgCategory()
//    {
//    }

//    public string Value
//    {
//        get; private set;
//    }

//    public static MsgCategory Create
//    {
//        get
//        {
//            return new MsgCategory("\uE710");
//        }
//    }
//    public static MsgCategory Update
//    {
//        get
//        {
//            return new MsgCategory("\uE777");
//        }
//    }
//    public static MsgCategory Delete
//    {
//        get
//        {
//            return new MsgCategory("\uE74D");
//        }
//    }
//    public static MsgCategory StartTask
//    {
//        get
//        {
//            return new MsgCategory("\uE768");
//        }
//    }
//    public static MsgCategory EndTask
//    {
//        get
//        {
//            return new MsgCategory("\uE15B");
//        }
//    }
//    public static MsgCategory Error
//    {
//        get
//        {
//            return new MsgCategory("\uE783");
//        }
//    }

//    public override string ToString()
//    {
//        return Value;
//    }
//}
