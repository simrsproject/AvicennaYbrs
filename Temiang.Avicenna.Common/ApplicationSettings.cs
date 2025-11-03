using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common
{
    public sealed class ApplicationSettings : ConfigurationSection
    {
        private static ApplicationSettings _applicationInfo;
        private static ApplicationElement _defaultApplication;

        private ApplicationSettings()
        {
        }

        [ConfigurationProperty("applications", IsDefaultCollection = false, IsRequired = false)]
        [ConfigurationCollection(typeof(ApplicationsCollection), AddItemName = "add", ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ApplicationsCollection Applications
        {
            get { return (ApplicationsCollection)base["applications"]; }
        }

        [ConfigurationProperty("default", IsRequired = false)]
        public string Default
        {
            get { return (string)base["default"]; }
            set
            {
                var s = (string)base["default"];
                if (s != value)
                {
                    base["default"] = value;
                    _defaultApplication = null;
                }
            }
        }

        public static ApplicationSettings ApplicationInfo
        {
            get
            {
                if (_applicationInfo == null)
                {
                    _applicationInfo = (ApplicationSettings)ConfigurationManager.GetSection("Application/applicationInfo");
                    if (_applicationInfo == null)
                        _applicationInfo = new ApplicationSettings();
                }
                return _applicationInfo;
            }
        }

        public static ApplicationElement DefaultApplication
        {
            get
            {
                if (_defaultApplication == null)
                {
                    var configSettings = ApplicationInfo;
                    foreach (ApplicationElement applicationElement in configSettings.Applications)
                    {
                        if (applicationElement.Name == configSettings.Default)
                        {
                            _defaultApplication = applicationElement;
                            break;
                        }
                    }
                }
                return _defaultApplication;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    } 

    public class ApplicationsCollection : ConfigurationElementCollection
    {
        public ApplicationElement this[int index]
        {
            get { return (ApplicationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                base.BaseAdd(index, value);
            }
        }

        public new ApplicationElement this[string name]
        {
            get { return (ApplicationElement)BaseGet(name); }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public void Add(ApplicationElement application)
        {
            base.BaseAdd(application);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(ApplicationElement application)
        {
            return BaseIndexOf(application);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApplicationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationElement)element).Name;
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    } 


    public class ApplicationElement : ConfigurationElement
    {
        public ApplicationElement()
        {
        }
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("baseUrl", IsRequired = false)]
        public string BaseUrl
        {
            get
            {
                var s = (string)base["baseUrl"];
                return !string.IsNullOrEmpty(s) ? s : null;
            }
            set { base["baseUrl"] = value; }
        }

        [ConfigurationProperty("binFolderLocation", IsRequired = false)]
        public string BinFolderLocation
        {
            get
            {
                var s = (string)base["binFolderLocation"];
                return !string.IsNullOrEmpty(s) ? s : null;
            }
            set { base["binFolderLocation"] = value; }
        }
    }


}
