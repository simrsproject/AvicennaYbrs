/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/26/2015 2:00:44 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esMenuSettingCollection : esEntityCollectionWAuditLog
    {
        public esMenuSettingCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "MenuSettingCollection";
        }

        #region Query Logic
        protected void InitQuery(esMenuSettingQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntityCollection)this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            this.PopulateCollection(table);
            return (this.RowCount > 0) ? true : false;
        }

        protected override void HookupQuery(esDynamicQuery query)
        {
            this.InitQuery(query as esMenuSettingQuery);
        }
        #endregion

        virtual public MenuSetting DetachEntity(MenuSetting entity)
        {
            return base.DetachEntity(entity) as MenuSetting;
        }

        virtual public MenuSetting AttachEntity(MenuSetting entity)
        {
            return base.AttachEntity(entity) as MenuSetting;
        }

        virtual public void Combine(MenuSettingCollection collection)
        {
            base.Combine(collection);
        }

        new public MenuSetting this[int index]
        {
            get
            {
                return base[index] as MenuSetting;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MenuSetting);
        }
    }



    [Serializable]
    abstract public class esMenuSetting : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMenuSettingQuery GetDynamicQuery()
        {
            return null;
        }

        public esMenuSetting()
        {

        }

        public esMenuSetting(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.DateTime startingDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(startingDate);
            else
                return LoadByPrimaryKeyStoredProcedure(startingDate);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime startingDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(startingDate);
            else
                return LoadByPrimaryKeyStoredProcedure(startingDate);
        }

        private bool LoadByPrimaryKeyDynamic(System.DateTime startingDate)
        {
            esMenuSettingQuery query = this.GetDynamicQuery();
            query.Where(query.StartingDate == startingDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.DateTime startingDate)
        {
            esParameters parms = new esParameters();
            parms.Add("StartingDate", startingDate);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
        #endregion



        #region Properties


        public override void SetProperties(IDictionary values)
        {
            foreach (string propertyName in values.Keys)
            {
                this.SetProperty(propertyName, values[propertyName]);
            }
        }

        public override void SetProperty(string name, object value)
        {
            if (this.Row == null) this.AddNew();

            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
            if (col != null)
            {
                if (value == null || value is System.String)
                {
                    // Use the strongly typed property
                    switch (name)
                    {
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "VersionID": this.str.VersionID = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "IsExtra": this.str.IsExtra = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
                            break;

                        case "IsExtra":

                            if (value == null || value is System.Boolean)
                                this.IsExtra = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;


                        default:
                            break;
                    }
                }
            }
            else if (this.Row.Table.Columns.Contains(name))
            {
                this.Row[name] = value;
            }
            else
            {
                throw new Exception("SetProperty Error: '" + name + "' not found");
            }
        }


        /// <summary>
        /// Maps to MenuSetting.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(MenuSettingMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(MenuSettingMetadata.ColumnNames.StartingDate, value);
            }
        }

        /// <summary>
        /// Maps to MenuSetting.VersionID
        /// </summary>
        virtual public System.String VersionID
        {
            get
            {
                return base.GetSystemString(MenuSettingMetadata.ColumnNames.VersionID);
            }

            set
            {
                base.SetSystemString(MenuSettingMetadata.ColumnNames.VersionID, value);
            }
        }

        /// <summary>
        /// Maps to MenuSetting.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(MenuSettingMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(MenuSettingMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to MenuSetting.IsExtra
        /// </summary>
        virtual public System.Boolean? IsExtra
        {
            get
            {
                return base.GetSystemBoolean(MenuSettingMetadata.ColumnNames.IsExtra);
            }

            set
            {
                base.SetSystemBoolean(MenuSettingMetadata.ColumnNames.IsExtra, value);
            }
        }

        /// <summary>
        /// Maps to MenuSetting.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MenuSettingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MenuSettingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to MenuSetting.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MenuSettingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MenuSettingMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        #endregion

        #region String Properties


        [BrowsableAttribute(false)]
        public esStrings str
        {
            get
            {
                if (esstrings == null)
                {
                    esstrings = new esStrings(this);
                }
                return esstrings;
            }
        }


        [Serializable]
        sealed public class esStrings
        {
            public esStrings(esMenuSetting entity)
            {
                this.entity = entity;
            }


            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
                }
            }

            public System.String VersionID
            {
                get
                {
                    System.String data = entity.VersionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VersionID = null;
                    else entity.VersionID = Convert.ToString(value);
                }
            }

            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
                }
            }

            public System.String IsExtra
            {
                get
                {
                    System.Boolean? data = entity.IsExtra;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsExtra = null;
                    else entity.IsExtra = Convert.ToBoolean(value);
                }
            }

            public System.String LastUpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastUpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String LastUpdateByUserID
            {
                get
                {
                    System.String data = entity.LastUpdateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
                    else entity.LastUpdateByUserID = Convert.ToString(value);
                }
            }


            private esMenuSetting entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMenuSettingQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntity)this).Connection;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected bool OnQueryLoaded(DataTable table)
        {
            bool dataFound = this.PopulateEntity(table);

            if (this.RowCount > 1)
            {
                throw new Exception("esMenuSetting can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class MenuSetting : esMenuSetting
    {


        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }

    }



    [Serializable]
    abstract public class esMenuSettingQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return MenuSettingMetadata.Meta();
            }
        }


        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem VersionID
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.VersionID, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem IsExtra
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.IsExtra, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MenuSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MenuSettingCollection")]
    public partial class MenuSettingCollection : esMenuSettingCollection, IEnumerable<MenuSetting>
    {
        public MenuSettingCollection()
        {

        }

        public static implicit operator List<MenuSetting>(MenuSettingCollection coll)
        {
            List<MenuSetting> list = new List<MenuSetting>();

            foreach (MenuSetting emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MenuSettingMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MenuSetting(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MenuSetting();
        }


        #endregion


        [BrowsableAttribute(false)]
        public MenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(MenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public MenuSetting AddNew()
        {
            MenuSetting entity = base.AddNewEntity() as MenuSetting;

            return entity;
        }

        public MenuSetting FindByPrimaryKey(System.DateTime startingDate)
        {
            return base.FindByPrimaryKey(startingDate) as MenuSetting;
        }


        #region IEnumerable<MenuSetting> Members

        IEnumerator<MenuSetting> IEnumerable<MenuSetting>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MenuSetting;
            }
        }

        #endregion

        private MenuSettingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MenuSetting' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("MenuSetting ({StartingDate})")]
    [Serializable]
    public partial class MenuSetting : esMenuSetting
    {
        public MenuSetting()
        {

        }

        public MenuSetting(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MenuSettingMetadata.Meta();
            }
        }



        override protected esMenuSettingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public MenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MenuSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(MenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MenuSettingQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class MenuSettingQuery : esMenuSettingQuery
    {
        public MenuSettingQuery()
        {

        }

        public MenuSettingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MenuSettingQuery";
        }


    }


    [Serializable]
    public partial class MenuSettingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MenuSettingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.StartingDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MenuSettingMetadata.PropertyNames.StartingDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.VersionID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuSettingMetadata.PropertyNames.VersionID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.SeqNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuSettingMetadata.PropertyNames.SeqNo;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.IsExtra, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MenuSettingMetadata.PropertyNames.IsExtra;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MenuSettingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MenuSettingMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MenuSettingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public MenuSettingMetadata Meta()
        {
            return meta;
        }

        public Guid DataID
        {
            get { return base._dataID; }
        }

        public bool MultiProviderMode
        {
            get { return false; }
        }

        public esColumnMetadataCollection Columns
        {
            get { return base._columns; }
        }

        #region ColumnNames
        public class ColumnNames
        {
            public const string StartingDate = "StartingDate";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string IsExtra = "IsExtra";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string StartingDate = "StartingDate";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string IsExtra = "IsExtra";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
        {
            MapToMeta mapMethod = mapDelegates[mapName];

            if (mapMethod != null)
                return mapMethod(mapName);
            else
                return null;
        }

        #region MAP esDefault

        static private int RegisterDelegateesDefault()
        {
            // This is only executed once per the life of the application
            lock (typeof(MenuSettingMetadata))
            {
                if (MenuSettingMetadata.mapDelegates == null)
                {
                    MenuSettingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MenuSettingMetadata.meta == null)
                {
                    MenuSettingMetadata.meta = new MenuSettingMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
            return 0;
        }

        private esProviderSpecificMetadata esDefault(string mapName)
        {
            if (!_providerMetadataMaps.ContainsKey(mapName))
            {
                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();


                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VersionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsExtra", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "MenuSetting";
                meta.Destination = "MenuSetting";

                meta.spInsert = "proc_MenuSettingInsert";
                meta.spUpdate = "proc_MenuSettingUpdate";
                meta.spDelete = "proc_MenuSettingDelete";
                meta.spLoadAll = "proc_MenuSettingLoadAll";
                meta.spLoadByPrimaryKey = "proc_MenuSettingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MenuSettingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
