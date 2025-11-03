/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/21/2015 3:53:36 PM
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
    abstract public class esIncidentTypeItemCollection : esEntityCollectionWAuditLog
    {
        public esIncidentTypeItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "IncidentTypeItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esIncidentTypeItemQuery query)
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
            this.InitQuery(query as esIncidentTypeItemQuery);
        }
        #endregion

        virtual public IncidentTypeItem DetachEntity(IncidentTypeItem entity)
        {
            return base.DetachEntity(entity) as IncidentTypeItem;
        }

        virtual public IncidentTypeItem AttachEntity(IncidentTypeItem entity)
        {
            return base.AttachEntity(entity) as IncidentTypeItem;
        }

        virtual public void Combine(IncidentTypeItemCollection collection)
        {
            base.Combine(collection);
        }

        new public IncidentTypeItem this[int index]
        {
            get
            {
                return base[index] as IncidentTypeItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(IncidentTypeItem);
        }
    }



    [Serializable]
    abstract public class esIncidentTypeItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esIncidentTypeItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esIncidentTypeItem()
        {

        }

        public esIncidentTypeItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String sRIncidentType, System.String componentID, System.String subComponentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRIncidentType, componentID, subComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRIncidentType, componentID, subComponentID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRIncidentType, System.String componentID, System.String subComponentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRIncidentType, componentID, subComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRIncidentType, componentID, subComponentID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String sRIncidentType, System.String componentID, System.String subComponentID)
        {
            esIncidentTypeItemQuery query = this.GetDynamicQuery();
            query.Where(query.SRIncidentType == sRIncidentType, query.ComponentID == componentID, query.SubComponentID == subComponentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String sRIncidentType, System.String componentID, System.String subComponentID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRIncidentType", sRIncidentType); parms.Add("ComponentID", componentID); parms.Add("SubComponentID", subComponentID);
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
                        case "SRIncidentType": this.str.SRIncidentType = (string)value; break;
                        case "ComponentID": this.str.ComponentID = (string)value; break;
                        case "SubComponentID": this.str.SubComponentID = (string)value; break;
                        case "SubComponentName": this.str.SubComponentName = (string)value; break;
                        case "IsAllowEdit": this.str.IsAllowEdit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAllowEdit":

                            if (value == null || value is System.Boolean)
                                this.IsAllowEdit = (System.Boolean?)value;
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
        /// Maps to IncidentTypeItem.SRIncidentType
        /// </summary>
        virtual public System.String SRIncidentType
        {
            get
            {
                return base.GetSystemString(IncidentTypeItemMetadata.ColumnNames.SRIncidentType);
            }

            set
            {
                base.SetSystemString(IncidentTypeItemMetadata.ColumnNames.SRIncidentType, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.ComponentID
        /// </summary>
        virtual public System.String ComponentID
        {
            get
            {
                return base.GetSystemString(IncidentTypeItemMetadata.ColumnNames.ComponentID);
            }

            set
            {
                base.SetSystemString(IncidentTypeItemMetadata.ColumnNames.ComponentID, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.SubComponentID
        /// </summary>
        virtual public System.String SubComponentID
        {
            get
            {
                return base.GetSystemString(IncidentTypeItemMetadata.ColumnNames.SubComponentID);
            }

            set
            {
                base.SetSystemString(IncidentTypeItemMetadata.ColumnNames.SubComponentID, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.SubComponentName
        /// </summary>
        virtual public System.String SubComponentName
        {
            get
            {
                return base.GetSystemString(IncidentTypeItemMetadata.ColumnNames.SubComponentName);
            }

            set
            {
                base.SetSystemString(IncidentTypeItemMetadata.ColumnNames.SubComponentName, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.IsAllowEdit
        /// </summary>
        virtual public System.Boolean? IsAllowEdit
        {
            get
            {
                return base.GetSystemBoolean(IncidentTypeItemMetadata.ColumnNames.IsAllowEdit);
            }

            set
            {
                base.SetSystemBoolean(IncidentTypeItemMetadata.ColumnNames.IsAllowEdit, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(IncidentTypeItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(IncidentTypeItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to IncidentTypeItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(IncidentTypeItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(IncidentTypeItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esIncidentTypeItem entity)
            {
                this.entity = entity;
            }


            public System.String SRIncidentType
            {
                get
                {
                    System.String data = entity.SRIncidentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRIncidentType = null;
                    else entity.SRIncidentType = Convert.ToString(value);
                }
            }

            public System.String ComponentID
            {
                get
                {
                    System.String data = entity.ComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ComponentID = null;
                    else entity.ComponentID = Convert.ToString(value);
                }
            }

            public System.String SubComponentID
            {
                get
                {
                    System.String data = entity.SubComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubComponentID = null;
                    else entity.SubComponentID = Convert.ToString(value);
                }
            }

            public System.String SubComponentName
            {
                get
                {
                    System.String data = entity.SubComponentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubComponentName = null;
                    else entity.SubComponentName = Convert.ToString(value);
                }
            }

            public System.String IsAllowEdit
            {
                get
                {
                    System.Boolean? data = entity.IsAllowEdit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowEdit = null;
                    else entity.IsAllowEdit = Convert.ToBoolean(value);
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


            private esIncidentTypeItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esIncidentTypeItemQuery query)
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
                throw new Exception("esIncidentTypeItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class IncidentTypeItem : esIncidentTypeItem
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
    abstract public class esIncidentTypeItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return IncidentTypeItemMetadata.Meta();
            }
        }


        public esQueryItem SRIncidentType
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.SRIncidentType, esSystemType.String);
            }
        }

        public esQueryItem ComponentID
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.ComponentID, esSystemType.String);
            }
        }

        public esQueryItem SubComponentID
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.SubComponentID, esSystemType.String);
            }
        }

        public esQueryItem SubComponentName
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.SubComponentName, esSystemType.String);
            }
        }

        public esQueryItem IsAllowEdit
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.IsAllowEdit, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, IncidentTypeItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("IncidentTypeItemCollection")]
    public partial class IncidentTypeItemCollection : esIncidentTypeItemCollection, IEnumerable<IncidentTypeItem>
    {
        public IncidentTypeItemCollection()
        {

        }

        public static implicit operator List<IncidentTypeItem>(IncidentTypeItemCollection coll)
        {
            List<IncidentTypeItem> list = new List<IncidentTypeItem>();

            foreach (IncidentTypeItem emp in coll)
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
                return IncidentTypeItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new IncidentTypeItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new IncidentTypeItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new IncidentTypeItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public IncidentTypeItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new IncidentTypeItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(IncidentTypeItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public IncidentTypeItem AddNew()
        {
            IncidentTypeItem entity = base.AddNewEntity() as IncidentTypeItem;

            return entity;
        }

        public IncidentTypeItem FindByPrimaryKey(System.String sRIncidentType, System.String componentID, System.String subComponentID)
        {
            return base.FindByPrimaryKey(sRIncidentType, componentID, subComponentID) as IncidentTypeItem;
        }


        #region IEnumerable<IncidentTypeItem> Members

        IEnumerator<IncidentTypeItem> IEnumerable<IncidentTypeItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as IncidentTypeItem;
            }
        }

        #endregion

        private IncidentTypeItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'IncidentTypeItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("IncidentTypeItem ({SRIncidentType},{ComponentID},{SubComponentID})")]
    [Serializable]
    public partial class IncidentTypeItem : esIncidentTypeItem
    {
        public IncidentTypeItem()
        {

        }

        public IncidentTypeItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return IncidentTypeItemMetadata.Meta();
            }
        }



        override protected esIncidentTypeItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new IncidentTypeItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public IncidentTypeItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new IncidentTypeItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(IncidentTypeItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private IncidentTypeItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class IncidentTypeItemQuery : esIncidentTypeItemQuery
    {
        public IncidentTypeItemQuery()
        {

        }

        public IncidentTypeItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "IncidentTypeItemQuery";
        }


    }


    [Serializable]
    public partial class IncidentTypeItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected IncidentTypeItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.SRIncidentType, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.SRIncidentType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.ComponentID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.ComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.SubComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.SubComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.SubComponentName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.SubComponentName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.IsAllowEdit, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.IsAllowEdit;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(IncidentTypeItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = IncidentTypeItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public IncidentTypeItemMetadata Meta()
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
            public const string SRIncidentType = "SRIncidentType";
            public const string ComponentID = "ComponentID";
            public const string SubComponentID = "SubComponentID";
            public const string SubComponentName = "SubComponentName";
            public const string IsAllowEdit = "IsAllowEdit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRIncidentType = "SRIncidentType";
            public const string ComponentID = "ComponentID";
            public const string SubComponentID = "SubComponentID";
            public const string SubComponentName = "SubComponentName";
            public const string IsAllowEdit = "IsAllowEdit";
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
            lock (typeof(IncidentTypeItemMetadata))
            {
                if (IncidentTypeItemMetadata.mapDelegates == null)
                {
                    IncidentTypeItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (IncidentTypeItemMetadata.meta == null)
                {
                    IncidentTypeItemMetadata.meta = new IncidentTypeItemMetadata();
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


                meta.AddTypeMap("SRIncidentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SubComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SubComponentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAllowEdit", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "IncidentTypeItem";
                meta.Destination = "IncidentTypeItem";

                meta.spInsert = "proc_IncidentTypeItemInsert";
                meta.spUpdate = "proc_IncidentTypeItemUpdate";
                meta.spDelete = "proc_IncidentTypeItemDelete";
                meta.spLoadAll = "proc_IncidentTypeItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_IncidentTypeItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private IncidentTypeItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
