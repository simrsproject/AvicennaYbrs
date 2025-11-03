/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/27/2015 9:33:59 AM
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
    abstract public class esServiceUnitClassMenuExtraSettingCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitClassMenuExtraSettingCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ServiceUnitClassMenuExtraSettingCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitClassMenuExtraSettingQuery query)
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
            this.InitQuery(query as esServiceUnitClassMenuExtraSettingQuery);
        }
        #endregion

        virtual public ServiceUnitClassMenuExtraSetting DetachEntity(ServiceUnitClassMenuExtraSetting entity)
        {
            return base.DetachEntity(entity) as ServiceUnitClassMenuExtraSetting;
        }

        virtual public ServiceUnitClassMenuExtraSetting AttachEntity(ServiceUnitClassMenuExtraSetting entity)
        {
            return base.AttachEntity(entity) as ServiceUnitClassMenuExtraSetting;
        }

        virtual public void Combine(ServiceUnitClassMenuExtraSettingCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitClassMenuExtraSetting this[int index]
        {
            get
            {
                return base[index] as ServiceUnitClassMenuExtraSetting;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitClassMenuExtraSetting);
        }
    }



    [Serializable]
    abstract public class esServiceUnitClassMenuExtraSetting : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitClassMenuExtraSettingQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitClassMenuExtraSetting()
        {

        }

        public esServiceUnitClassMenuExtraSetting(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String serviceUnitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID)
        {
            esServiceUnitClassMenuExtraSettingQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "MenuID": this.str.MenuID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to ServiceUnitClassMenuExtraSetting.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuExtraSetting.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuExtraSetting.MenuID
        /// </summary>
        virtual public System.String MenuID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.MenuID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.MenuID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuExtraSetting.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuExtraSetting.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esServiceUnitClassMenuExtraSetting entity)
            {
                this.entity = entity;
            }


            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }

            public System.String MenuID
            {
                get
                {
                    System.String data = entity.MenuID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuID = null;
                    else entity.MenuID = Convert.ToString(value);
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


            private esServiceUnitClassMenuExtraSetting entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitClassMenuExtraSettingQuery query)
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
                throw new Exception("esServiceUnitClassMenuExtraSetting can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ServiceUnitClassMenuExtraSetting : esServiceUnitClassMenuExtraSetting
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
    abstract public class esServiceUnitClassMenuExtraSettingQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitClassMenuExtraSettingMetadata.Meta();
            }
        }


        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem MenuID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.MenuID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitClassMenuExtraSettingCollection")]
    public partial class ServiceUnitClassMenuExtraSettingCollection : esServiceUnitClassMenuExtraSettingCollection, IEnumerable<ServiceUnitClassMenuExtraSetting>
    {
        public ServiceUnitClassMenuExtraSettingCollection()
        {

        }

        public static implicit operator List<ServiceUnitClassMenuExtraSetting>(ServiceUnitClassMenuExtraSettingCollection coll)
        {
            List<ServiceUnitClassMenuExtraSetting> list = new List<ServiceUnitClassMenuExtraSetting>();

            foreach (ServiceUnitClassMenuExtraSetting emp in coll)
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
                return ServiceUnitClassMenuExtraSettingMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitClassMenuExtraSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitClassMenuExtraSetting(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitClassMenuExtraSetting();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ServiceUnitClassMenuExtraSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitClassMenuExtraSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ServiceUnitClassMenuExtraSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ServiceUnitClassMenuExtraSetting AddNew()
        {
            ServiceUnitClassMenuExtraSetting entity = base.AddNewEntity() as ServiceUnitClassMenuExtraSetting;

            return entity;
        }

        public ServiceUnitClassMenuExtraSetting FindByPrimaryKey(System.String serviceUnitID)
        {
            return base.FindByPrimaryKey(serviceUnitID) as ServiceUnitClassMenuExtraSetting;
        }


        #region IEnumerable<ServiceUnitClassMenuExtraSetting> Members

        IEnumerator<ServiceUnitClassMenuExtraSetting> IEnumerable<ServiceUnitClassMenuExtraSetting>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitClassMenuExtraSetting;
            }
        }

        #endregion

        private ServiceUnitClassMenuExtraSettingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitClassMenuExtraSetting' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitClassMenuExtraSetting ({ServiceUnitID})")]
    [Serializable]
    public partial class ServiceUnitClassMenuExtraSetting : esServiceUnitClassMenuExtraSetting
    {
        public ServiceUnitClassMenuExtraSetting()
        {

        }

        public ServiceUnitClassMenuExtraSetting(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitClassMenuExtraSettingMetadata.Meta();
            }
        }



        override protected esServiceUnitClassMenuExtraSettingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitClassMenuExtraSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ServiceUnitClassMenuExtraSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitClassMenuExtraSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ServiceUnitClassMenuExtraSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitClassMenuExtraSettingQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ServiceUnitClassMenuExtraSettingQuery : esServiceUnitClassMenuExtraSettingQuery
    {
        public ServiceUnitClassMenuExtraSettingQuery()
        {

        }

        public ServiceUnitClassMenuExtraSettingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitClassMenuExtraSettingQuery";
        }


    }


    [Serializable]
    public partial class ServiceUnitClassMenuExtraSettingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitClassMenuExtraSettingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuExtraSettingMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuExtraSettingMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.MenuID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuExtraSettingMetadata.PropertyNames.MenuID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitClassMenuExtraSettingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuExtraSettingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuExtraSettingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ServiceUnitClassMenuExtraSettingMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string MenuID = "MenuID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string MenuID = "MenuID";
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
            lock (typeof(ServiceUnitClassMenuExtraSettingMetadata))
            {
                if (ServiceUnitClassMenuExtraSettingMetadata.mapDelegates == null)
                {
                    ServiceUnitClassMenuExtraSettingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitClassMenuExtraSettingMetadata.meta == null)
                {
                    ServiceUnitClassMenuExtraSettingMetadata.meta = new ServiceUnitClassMenuExtraSettingMetadata();
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


                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ServiceUnitClassMenuExtraSetting";
                meta.Destination = "ServiceUnitClassMenuExtraSetting";

                meta.spInsert = "proc_ServiceUnitClassMenuExtraSettingInsert";
                meta.spUpdate = "proc_ServiceUnitClassMenuExtraSettingUpdate";
                meta.spDelete = "proc_ServiceUnitClassMenuExtraSettingDelete";
                meta.spLoadAll = "proc_ServiceUnitClassMenuExtraSettingLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitClassMenuExtraSettingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitClassMenuExtraSettingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
