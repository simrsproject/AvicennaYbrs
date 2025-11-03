/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2014 11:05:57 AM
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
    abstract public class esServiceUnitClassMenuSettingCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitClassMenuSettingCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ServiceUnitClassMenuSettingCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitClassMenuSettingQuery query)
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
            this.InitQuery(query as esServiceUnitClassMenuSettingQuery);
        }
        #endregion

        virtual public ServiceUnitClassMenuSetting DetachEntity(ServiceUnitClassMenuSetting entity)
        {
            return base.DetachEntity(entity) as ServiceUnitClassMenuSetting;
        }

        virtual public ServiceUnitClassMenuSetting AttachEntity(ServiceUnitClassMenuSetting entity)
        {
            return base.AttachEntity(entity) as ServiceUnitClassMenuSetting;
        }

        virtual public void Combine(ServiceUnitClassMenuSettingCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitClassMenuSetting this[int index]
        {
            get
            {
                return base[index] as ServiceUnitClassMenuSetting;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitClassMenuSetting);
        }
    }



    [Serializable]
    abstract public class esServiceUnitClassMenuSetting : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitClassMenuSettingQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitClassMenuSetting()
        {

        }

        public esServiceUnitClassMenuSetting(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String classID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, classID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String classID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, classID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, classID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String classID)
        {
            esServiceUnitClassMenuSettingQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID, query.ClassID == classID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String classID)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID); parms.Add("ClassID", classID);
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
                        case "IsOptional": this.str.IsOptional = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsOptional":

                            if (value == null || value is System.Boolean)
                                this.IsOptional = (System.Boolean?)value;
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
        /// Maps to ServiceUnitClassMenuSetting.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuSetting.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuSetting.IsOptional
        /// </summary>
        virtual public System.Boolean? IsOptional
        {
            get
            {
                return base.GetSystemBoolean(ServiceUnitClassMenuSettingMetadata.ColumnNames.IsOptional);
            }

            set
            {
                base.SetSystemBoolean(ServiceUnitClassMenuSettingMetadata.ColumnNames.IsOptional, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuSetting.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitClassMenuSetting.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esServiceUnitClassMenuSetting entity)
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

            public System.String IsOptional
            {
                get
                {
                    System.Boolean? data = entity.IsOptional;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOptional = null;
                    else entity.IsOptional = Convert.ToBoolean(value);
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


            private esServiceUnitClassMenuSetting entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitClassMenuSettingQuery query)
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
                throw new Exception("esServiceUnitClassMenuSetting can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ServiceUnitClassMenuSetting : esServiceUnitClassMenuSetting
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
    abstract public class esServiceUnitClassMenuSettingQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitClassMenuSettingMetadata.Meta();
            }
        }


        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem IsOptional
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuSettingMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitClassMenuSettingCollection")]
    public partial class ServiceUnitClassMenuSettingCollection : esServiceUnitClassMenuSettingCollection, IEnumerable<ServiceUnitClassMenuSetting>
    {
        public ServiceUnitClassMenuSettingCollection()
        {

        }

        public static implicit operator List<ServiceUnitClassMenuSetting>(ServiceUnitClassMenuSettingCollection coll)
        {
            List<ServiceUnitClassMenuSetting> list = new List<ServiceUnitClassMenuSetting>();

            foreach (ServiceUnitClassMenuSetting emp in coll)
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
                return ServiceUnitClassMenuSettingMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitClassMenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitClassMenuSetting(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitClassMenuSetting();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ServiceUnitClassMenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitClassMenuSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ServiceUnitClassMenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ServiceUnitClassMenuSetting AddNew()
        {
            ServiceUnitClassMenuSetting entity = base.AddNewEntity() as ServiceUnitClassMenuSetting;

            return entity;
        }

        public ServiceUnitClassMenuSetting FindByPrimaryKey(System.String serviceUnitID, System.String classID)
        {
            return base.FindByPrimaryKey(serviceUnitID, classID) as ServiceUnitClassMenuSetting;
        }


        #region IEnumerable<ServiceUnitClassMenuSetting> Members

        IEnumerator<ServiceUnitClassMenuSetting> IEnumerable<ServiceUnitClassMenuSetting>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitClassMenuSetting;
            }
        }

        #endregion

        private ServiceUnitClassMenuSettingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitClassMenuSetting' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitClassMenuSetting ({ServiceUnitID},{ClassID})")]
    [Serializable]
    public partial class ServiceUnitClassMenuSetting : esServiceUnitClassMenuSetting
    {
        public ServiceUnitClassMenuSetting()
        {

        }

        public ServiceUnitClassMenuSetting(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitClassMenuSettingMetadata.Meta();
            }
        }



        override protected esServiceUnitClassMenuSettingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitClassMenuSettingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ServiceUnitClassMenuSettingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitClassMenuSettingQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ServiceUnitClassMenuSettingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitClassMenuSettingQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ServiceUnitClassMenuSettingQuery : esServiceUnitClassMenuSettingQuery
    {
        public ServiceUnitClassMenuSettingQuery()
        {

        }

        public ServiceUnitClassMenuSettingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitClassMenuSettingQuery";
        }


    }


    [Serializable]
    public partial class ServiceUnitClassMenuSettingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitClassMenuSettingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitClassMenuSettingMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuSettingMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuSettingMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuSettingMetadata.ColumnNames.IsOptional, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ServiceUnitClassMenuSettingMetadata.PropertyNames.IsOptional;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitClassMenuSettingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitClassMenuSettingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitClassMenuSettingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ServiceUnitClassMenuSettingMetadata Meta()
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
            public const string IsOptional = "IsOptional";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string IsOptional = "IsOptional";
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
            lock (typeof(ServiceUnitClassMenuSettingMetadata))
            {
                if (ServiceUnitClassMenuSettingMetadata.mapDelegates == null)
                {
                    ServiceUnitClassMenuSettingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitClassMenuSettingMetadata.meta == null)
                {
                    ServiceUnitClassMenuSettingMetadata.meta = new ServiceUnitClassMenuSettingMetadata();
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
                meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ServiceUnitClassMenuSetting";
                meta.Destination = "ServiceUnitClassMenuSetting";

                meta.spInsert = "proc_ServiceUnitClassMenuSettingInsert";
                meta.spUpdate = "proc_ServiceUnitClassMenuSettingUpdate";
                meta.spDelete = "proc_ServiceUnitClassMenuSettingDelete";
                meta.spLoadAll = "proc_ServiceUnitClassMenuSettingLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitClassMenuSettingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitClassMenuSettingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
