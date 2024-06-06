using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.EAOrigin;
//internal class Manifest
//{
    // REMARQUE : Le code généré peut nécessiter au moins .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class DiPManifest
    {

        private DiPManifestBuildMetaData buildMetaDataField;

        private DiPManifestContentIDs contentIDsField;

        private DiPManifestGameTitle[] gameTitlesField;

        private DiPManifestUninstall uninstallField;

        private DiPManifestLauncher[] runtimeField;

        private DiPManifestInstallMetaData installMetaDataField;

        private DiPManifestTouchup touchupField;

        private DiPManifestInstallManifest installManifestField;

        private decimal versionField;

        /// <remarks/>
        public DiPManifestBuildMetaData buildMetaData
        {
            get
            {
                return this.buildMetaDataField;
            }
            set
            {
                this.buildMetaDataField = value;
            }
        }

        /// <remarks/>
        public DiPManifestContentIDs contentIDs
        {
            get
            {
                return this.contentIDsField;
            }
            set
            {
                this.contentIDsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("gameTitle", IsNullable = false)]
        public DiPManifestGameTitle[] gameTitles
        {
            get
            {
                return this.gameTitlesField;
            }
            set
            {
                this.gameTitlesField = value;
            }
        }

        /// <remarks/>
        public DiPManifestUninstall uninstall
        {
            get
            {
                return this.uninstallField;
            }
            set
            {
                this.uninstallField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("launcher", IsNullable = false)]
        public DiPManifestLauncher[] runtime
        {
            get
            {
                return this.runtimeField;
            }
            set
            {
                this.runtimeField = value;
            }
        }

        /// <remarks/>
        public DiPManifestInstallMetaData installMetaData
        {
            get
            {
                return this.installMetaDataField;
            }
            set
            {
                this.installMetaDataField = value;
            }
        }

        /// <remarks/>
        public DiPManifestTouchup touchup
        {
            get
            {
                return this.touchupField;
            }
            set
            {
                this.touchupField = value;
            }
        }

        /// <remarks/>
        public DiPManifestInstallManifest installManifest
        {
            get
            {
                return this.installManifestField;
            }
            set
            {
                this.installManifestField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestBuildMetaData
    {

        private DiPManifestBuildMetaDataFeatureFlags featureFlagsField;

        private DiPManifestBuildMetaDataGameVersion gameVersionField;

        private DiPManifestBuildMetaDataRequirements requirementsField;

        /// <remarks/>
        public DiPManifestBuildMetaDataFeatureFlags featureFlags
        {
            get
            {
                return this.featureFlagsField;
            }
            set
            {
                this.featureFlagsField = value;
            }
        }

        /// <remarks/>
        public DiPManifestBuildMetaDataGameVersion gameVersion
        {
            get
            {
                return this.gameVersionField;
            }
            set
            {
                this.gameVersionField = value;
            }
        }

        /// <remarks/>
        public DiPManifestBuildMetaDataRequirements requirements
        {
            get
            {
                return this.requirementsField;
            }
            set
            {
                this.requirementsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestBuildMetaDataFeatureFlags
    {

        private byte allowMultipleInstancesField;

        private byte autoUpdateEnabledField;

        private byte dynamicContentSupportEnabledField;

        private byte enableDifferentialUpdateField;

        private byte enableOriginInGameAPIField;

        private byte forceTouchupInstallerAfterUpdateField;

        private byte languageChangeSupportEnabledField;

        private byte treatUpdatesAsMandatoryField;

        private byte useGameVersionFromManifestField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte allowMultipleInstances
        {
            get
            {
                return this.allowMultipleInstancesField;
            }
            set
            {
                this.allowMultipleInstancesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte autoUpdateEnabled
        {
            get
            {
                return this.autoUpdateEnabledField;
            }
            set
            {
                this.autoUpdateEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte dynamicContentSupportEnabled
        {
            get
            {
                return this.dynamicContentSupportEnabledField;
            }
            set
            {
                this.dynamicContentSupportEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte enableDifferentialUpdate
        {
            get
            {
                return this.enableDifferentialUpdateField;
            }
            set
            {
                this.enableDifferentialUpdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte enableOriginInGameAPI
        {
            get
            {
                return this.enableOriginInGameAPIField;
            }
            set
            {
                this.enableOriginInGameAPIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte forceTouchupInstallerAfterUpdate
        {
            get
            {
                return this.forceTouchupInstallerAfterUpdateField;
            }
            set
            {
                this.forceTouchupInstallerAfterUpdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte languageChangeSupportEnabled
        {
            get
            {
                return this.languageChangeSupportEnabledField;
            }
            set
            {
                this.languageChangeSupportEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte treatUpdatesAsMandatory
        {
            get
            {
                return this.treatUpdatesAsMandatoryField;
            }
            set
            {
                this.treatUpdatesAsMandatoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte useGameVersionFromManifest
        {
            get
            {
                return this.useGameVersionFromManifestField;
            }
            set
            {
                this.useGameVersionFromManifestField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestBuildMetaDataGameVersion
    {

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestBuildMetaDataRequirements
    {

        private decimal osMinVersionField;

        private string osReqs64BitField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal osMinVersion
        {
            get
            {
                return this.osMinVersionField;
            }
            set
            {
                this.osMinVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string osReqs64Bit
        {
            get
            {
                return this.osReqs64BitField;
            }
            set
            {
                this.osReqs64BitField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestContentIDs
    {

        private uint contentIDField;

        /// <remarks/>
        public uint contentID
        {
            get
            {
                return this.contentIDField;
            }
            set
            {
                this.contentIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestGameTitle
    {

        private string localeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestUninstall
    {

        private string pathField;

        /// <remarks/>
        public string path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestLauncher
    {

        private DiPManifestLauncherName[] nameField;

        private string filePathField;

        private object parametersField;

        private byte executeElevatedField;

        private byte requires64BitOSField;

        private byte trialField;

        private string uidField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("name")]
        public DiPManifestLauncherName[] name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string filePath
        {
            get
            {
                return this.filePathField;
            }
            set
            {
                this.filePathField = value;
            }
        }

        /// <remarks/>
        public object parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        public byte executeElevated
        {
            get
            {
                return this.executeElevatedField;
            }
            set
            {
                this.executeElevatedField = value;
            }
        }

        /// <remarks/>
        public byte requires64BitOS
        {
            get
            {
                return this.requires64BitOSField;
            }
            set
            {
                this.requires64BitOSField = value;
            }
        }

        /// <remarks/>
        public byte trial
        {
            get
            {
                return this.trialField;
            }
            set
            {
                this.trialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uid
        {
            get
            {
                return this.uidField;
            }
            set
            {
                this.uidField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestLauncherName
    {

        private string localeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallMetaData
    {

        private DiPManifestInstallMetaDataChunk[] progressiveField;

        private object patchingField;

        private string localesField;

        private DiPManifestInstallMetaDataIncludes[] localeFiltersField;

        private DiPManifestInstallMetaDataEulas[] eulasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("chunk", IsNullable = false)]
        public DiPManifestInstallMetaDataChunk[] progressive
        {
            get
            {
                return this.progressiveField;
            }
            set
            {
                this.progressiveField = value;
            }
        }

        /// <remarks/>
        public object patching
        {
            get
            {
                return this.patchingField;
            }
            set
            {
                this.patchingField = value;
            }
        }

        /// <remarks/>
        public string locales
        {
            get
            {
                return this.localesField;
            }
            set
            {
                this.localesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("includes", IsNullable = false)]
        public DiPManifestInstallMetaDataIncludes[] localeFilters
        {
            get
            {
                return this.localeFiltersField;
            }
            set
            {
                this.localeFiltersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("eulas")]
        public DiPManifestInstallMetaDataEulas[] eulas
        {
            get
            {
                return this.eulasField;
            }
            set
            {
                this.eulasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallMetaDataChunk
    {

        private string[] includeField;

        private byte indexField;

        private string nameField;

        private string requiredField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("include")]
        public string[] include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string required
        {
            get
            {
                return this.requiredField;
            }
            set
            {
                this.requiredField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallMetaDataIncludes
    {

        private string[] includeField;

        private string localeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("include")]
        public string[] include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallMetaDataEulas
    {

        private DiPManifestInstallMetaDataEulasEula[] eulaField;

        private string localeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("eula")]
        public DiPManifestInstallMetaDataEulasEula[] eula
        {
            get
            {
                return this.eulaField;
            }
            set
            {
                this.eulaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallMetaDataEulasEula
    {

        private string nameField;

        private string flagField;

        private string installNameField;

        private uint installedSizeField;

        private bool installedSizeFieldSpecified;

        private string toolTipField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string flag
        {
            get
            {
                return this.flagField;
            }
            set
            {
                this.flagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string installName
        {
            get
            {
                return this.installNameField;
            }
            set
            {
                this.installNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint installedSize
        {
            get
            {
                return this.installedSizeField;
            }
            set
            {
                this.installedSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool installedSizeSpecified
        {
            get
            {
                return this.installedSizeFieldSpecified;
            }
            set
            {
                this.installedSizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string toolTip
        {
            get
            {
                return this.toolTipField;
            }
            set
            {
                this.toolTipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestTouchup
    {

        private string filePathField;

        private string parametersField;

        private string updateParametersField;

        private string repairParametersField;

        /// <remarks/>
        public string filePath
        {
            get
            {
                return this.filePathField;
            }
            set
            {
                this.filePathField = value;
            }
        }

        /// <remarks/>
        public string parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        public string updateParameters
        {
            get
            {
                return this.updateParametersField;
            }
            set
            {
                this.updateParametersField = value;
            }
        }

        /// <remarks/>
        public string repairParameters
        {
            get
            {
                return this.repairParametersField;
            }
            set
            {
                this.repairParametersField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DiPManifestInstallManifest
    {

        private string filePathField;

        /// <remarks/>
        public string filePath
        {
            get
            {
                return this.filePathField;
            }
            set
            {
                this.filePathField = value;
            }
        }
    }


//}
