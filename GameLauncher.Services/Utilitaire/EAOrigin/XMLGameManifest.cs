using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameLauncher.Services.Utilitaire.EAOrigin;

public class XMLGameManifet
{
    //public DiPManifest DiPManifest
    //{
    //    get; set;
    //}
}
//public partial class DiPManifest
//{
//    [JsonProperty("buildMetaData")]
//    public BuildMetaData BuildMetaData
//    {
//        get; set;
//    }

//    [JsonProperty("contentIDs")]
//    public ContentIDs ContentIDs
//    {
//        get; set;
//    }

//    [JsonProperty("gameTitles")]
//    public GameTitles GameTitles
//    {
//        get; set;
//    }

//    [JsonProperty("uninstall")]
//    public Uninstall Uninstall
//    {
//        get; set;
//    }

//    [JsonProperty("runtime")]
//    public Runtime Runtime
//    {
//        get; set;
//    }

//    [JsonProperty("installMetaData")]
//    public InstallMetaData InstallMetaData
//    {
//        get; set;
//    }

//    [JsonProperty("touchup")]
//    public Touchup Touchup
//    {
//        get; set;
//    }

//    [JsonProperty("installManifest")]
//    public InstallManifest InstallManifest
//    {
//        get; set;
//    }

//    [JsonProperty("_version")]
//    public string Version
//    {
//        get; set;
//    }
//}

//public partial class BuildMetaData
//{
//    [JsonProperty("featureFlags")]
//    public FeatureFlags FeatureFlags
//    {
//        get; set;
//    }

//    [JsonProperty("gameVersion")]
//    public GameVersion GameVersion
//    {
//        get; set;
//    }

//    [JsonProperty("requirements")]
//    public Requirements Requirements
//    {
//        get; set;
//    }
//}

//public partial class FeatureFlags
//{
//    [JsonProperty("_allowMultipleInstances")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long AllowMultipleInstances
//    {
//        get; set;
//    }

//    [JsonProperty("_autoUpdateEnabled")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long AutoUpdateEnabled
//    {
//        get; set;
//    }

//    [JsonProperty("_dynamicContentSupportEnabled")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long DynamicContentSupportEnabled
//    {
//        get; set;
//    }

//    [JsonProperty("_enableDifferentialUpdate")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long EnableDifferentialUpdate
//    {
//        get; set;
//    }

//    [JsonProperty("_enableOriginInGameAPI")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long EnableOriginInGameApi
//    {
//        get; set;
//    }

//    [JsonProperty("_forceTouchupInstallerAfterUpdate")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long ForceTouchupInstallerAfterUpdate
//    {
//        get; set;
//    }

//    [JsonProperty("_languageChangeSupportEnabled")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long LanguageChangeSupportEnabled
//    {
//        get; set;
//    }

//    [JsonProperty("_treatUpdatesAsMandatory")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long TreatUpdatesAsMandatory
//    {
//        get; set;
//    }

//    [JsonProperty("_useGameVersionFromManifest")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long UseGameVersionFromManifest
//    {
//        get; set;
//    }
//}

//public partial class GameVersion
//{
//    [JsonProperty("_version")]
//    public string Version
//    {
//        get; set;
//    }
//}

//public partial class Requirements
//{
//    [JsonProperty("_osMinVersion")]
//    public string OsMinVersion
//    {
//        get; set;
//    }

//    [JsonProperty("_osReqs64Bit")]
//    public string OsReqs64Bit
//    {
//        get; set;
//    }
//}

//public partial class ContentIDs
//{
//    [JsonProperty("contentID")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long ContentId
//    {
//        get; set;
//    }
//}

//public partial class GameTitles
//{
//    [JsonProperty("gameTitle")]
//    public GameTitle[] GameTitle
//    {
//        get; set;
//    }
//}

//public partial class GameTitle
//{
//    [JsonProperty("_locale")]
//    public string Locale
//    {
//        get; set;
//    }

//    [JsonProperty("__text")]
//    public string Text
//    {
//        get; set;
//    }
//}

//public partial class InstallManifest
//{
//    [JsonProperty("filePath")]
//    public string FilePath
//    {
//        get; set;
//    }
//}

//public partial class InstallMetaData
//{
//    [JsonProperty("progressive")]
//    public Progressive Progressive
//    {
//        get; set;
//    }

//    [JsonProperty("patching")]
//    public string Patching
//    {
//        get; set;
//    }

//    [JsonProperty("locales")]
//    public string Locales
//    {
//        get; set;
//    }

//    [JsonProperty("localeFilters")]
//    public LocaleFilters LocaleFilters
//    {
//        get; set;
//    }

//    [JsonProperty("eulas")]
//    public InstallMetaDataEula[] Eulas
//    {
//        get; set;
//    }
//}

//public partial class InstallMetaDataEula
//{
//    [JsonProperty("eula")]
//    public EulaEula[] Eula
//    {
//        get; set;
//    }

//    [JsonProperty("_locale")]
//    public string Locale
//    {
//        get; set;
//    }
//}

//public partial class EulaEula
//{
//    [JsonProperty("_name")]
//    public string Name
//    {
//        get; set;
//    }

//    [JsonProperty("__text")]
//    public string Text
//    {
//        get; set;
//    }

//    [JsonProperty("_flag", NullValueHandling = NullValueHandling.Ignore)]
//    public Flag? Flag
//    {
//        get; set;
//    }

//    [JsonProperty("_installName", NullValueHandling = NullValueHandling.Ignore)]
//    public string InstallName
//    {
//        get; set;
//    }

//    [JsonProperty("_installedSize", NullValueHandling = NullValueHandling.Ignore)]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long? InstalledSize
//    {
//        get; set;
//    }

//    [JsonProperty("_toolTip", NullValueHandling = NullValueHandling.Ignore)]
//    public string ToolTip
//    {
//        get; set;
//    }
//}

//public partial class LocaleFilters
//{
//    [JsonProperty("includes")]
//    public Include[] Includes
//    {
//        get; set;
//    }
//}

//public partial class Include
//{
//    [JsonProperty("include")]
//    public string[] IncludeInclude
//    {
//        get; set;
//    }

//    [JsonProperty("_locale")]
//    public string Locale
//    {
//        get; set;
//    }
//}

//public partial class Progressive
//{
//    [JsonProperty("chunk")]
//    public Chunk[] Chunk
//    {
//        get; set;
//    }
//}

//public partial class Chunk
//{
//    [JsonProperty("include", NullValueHandling = NullValueHandling.Ignore)]
//    public string[] Include
//    {
//        get; set;
//    }

//    [JsonProperty("_index")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long Index
//    {
//        get; set;
//    }

//    [JsonProperty("_name")]
//    public string Name
//    {
//        get; set;
//    }

//    [JsonProperty("_required")]
//    public RequiredEnum ChunkRequired
//    {
//        get; set;
//    }
//}

//public partial class Runtime
//{
//    [JsonProperty("launcher")]
//    public Launcher[] Launcher
//    {
//        get; set;
//    }
//}

//public partial class Launcher
//{
//    [JsonProperty("name")]
//    public GameTitle[] Name
//    {
//        get; set;
//    }

//    [JsonProperty("filePath")]
//    public string FilePath
//    {
//        get; set;
//    }

//    [JsonProperty("parameters")]
//    public string Parameters
//    {
//        get; set;
//    }

//    [JsonProperty("executeElevated")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long ExecuteElevated
//    {
//        get; set;
//    }

//    [JsonProperty("requires64BitOS")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long Requires64BitOs
//    {
//        get; set;
//    }

//    [JsonProperty("trial")]
//    [JsonConverter(typeof(ParseStringConverter))]
//    public long Trial
//    {
//        get; set;
//    }

//    [JsonProperty("_uid")]
//    public string Uid
//    {
//        get; set;
//    }
//}

//public partial class Touchup
//{
//    [JsonProperty("filePath")]
//    public string FilePath
//    {
//        get; set;
//    }

//    [JsonProperty("parameters")]
//    public string Parameters
//    {
//        get; set;
//    }

//    [JsonProperty("updateParameters")]
//    public string UpdateParameters
//    {
//        get; set;
//    }

//    [JsonProperty("repairParameters")]
//    public string RepairParameters
//    {
//        get; set;
//    }
//}

//public partial class Uninstall
//{
//    [JsonProperty("path")]
//    public string Path
//    {
//        get; set;
//    }
//}

//public enum Flag { ReqVc2015 };

//public enum RequiredEnum { Recommended, Required };

//internal static class Converter
//{
//    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//    {
//        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//        DateParseHandling = DateParseHandling.None,
//        Converters =
//            {
//                FlagConverter.Singleton,
//                RequiredEnumConverter.Singleton,
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//    };
//}

//internal class ParseStringConverter : JsonConverter
//{
//    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//        if (reader.TokenType == JsonToken.Null) return null;
//        var value = serializer.Deserialize<string>(reader);
//        long l;
//        if (Int64.TryParse(value, out l))
//        {
//            return l;
//        }
//        throw new Exception("Cannot unmarshal type long");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//        if (untypedValue == null)
//        {
//            serializer.Serialize(writer, null);
//            return;
//        }
//        var value = (long)untypedValue;
//        serializer.Serialize(writer, value.ToString());
//        return;
//    }

//    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
//}

//internal class FlagConverter : JsonConverter
//{
//    public override bool CanConvert(Type t) => t == typeof(Flag) || t == typeof(Flag?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//        if (reader.TokenType == JsonToken.Null) return null;
//        var value = serializer.Deserialize<string>(reader);
//        if (value == "-Req:vc2015")
//        {
//            return Flag.ReqVc2015;
//        }
//        throw new Exception("Cannot unmarshal type Flag");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//        if (untypedValue == null)
//        {
//            serializer.Serialize(writer, null);
//            return;
//        }
//        var value = (Flag)untypedValue;
//        if (value == Flag.ReqVc2015)
//        {
//            serializer.Serialize(writer, "-Req:vc2015");
//            return;
//        }
//        throw new Exception("Cannot marshal type Flag");
//    }

//    public static readonly FlagConverter Singleton = new FlagConverter();
//}

//internal class RequiredEnumConverter : JsonConverter
//{
//    public override bool CanConvert(Type t) => t == typeof(RequiredEnum) || t == typeof(RequiredEnum?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//        if (reader.TokenType == JsonToken.Null) return null;
//        var value = serializer.Deserialize<string>(reader);
//        switch (value)
//        {
//            case "recommended":
//                return RequiredEnum.Recommended;
//            case "required":
//                return RequiredEnum.Required;
//        }
//        throw new Exception("Cannot unmarshal type RequiredEnum");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//        if (untypedValue == null)
//        {
//            serializer.Serialize(writer, null);
//            return;
//        }
//        var value = (RequiredEnum)untypedValue;
//        switch (value)
//        {
//            case RequiredEnum.Recommended:
//                serializer.Serialize(writer, "recommended");
//                return;
//            case RequiredEnum.Required:
//                serializer.Serialize(writer, "required");
//                return;
//        }
//        throw new Exception("Cannot marshal type RequiredEnum");
//    }

//    public static readonly RequiredEnumConverter Singleton = new RequiredEnumConverter();
//}