/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/19/2014 12:02:02 PM
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
    abstract public class esServiceUnitTransactionCodeCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitTransactionCodeCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ServiceUnitTransactionCodeCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitTransactionCodeQuery query)
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
            this.InitQuery(query as esServiceUnitTransactionCodeQuery);
        }
        #endregion

        virtual public ServiceUnitTransactionCode DetachEntity(ServiceUnitTransactionCode entity)
        {
            return base.DetachEntity(entity) as ServiceUnitTransactionCode;
        }

        virtual public ServiceUnitTransactionCode AttachEntity(ServiceUnitTransactionCode entity)
        {
            return base.AttachEntity(entity) as ServiceUnitTransactionCode;
        }

        virtual public void Combine(ServiceUnitTransactionCodeCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitTransactionCode this[int index]
        {
            get
            {
                return base[index] as ServiceUnitTransactionCode;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitTransactionCode);
        }
    }



    [Serializable]
    abstract public class esServiceUnitTransactionCode : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitTransactionCodeQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitTransactionCode()
        {

        }

        public esServiceUnitTransactionCode(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String sRTransactionCode)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, sRTransactionCode);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, sRTransactionCode);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String sRTransactionCode)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, sRTransactionCode);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, sRTransactionCode);
        }

        private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String sRTransactionCode)
        {
            esServiceUnitTransactionCodeQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID, query.SRTransactionCode == sRTransactionCode);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String sRTransactionCode)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID); parms.Add("SRTransactionCode", sRTransactionCode);
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
                        case "SRTransactionCode": this.str.SRTransactionCode = (string)value; break;
                        case "IsItemProductMedic": this.str.IsItemProductMedic = (string)value; break;
                        case "IsItemProductNonMedic": this.str.IsItemProductNonMedic = (string)value; break;
                        case "IsItemKitchen": this.str.IsItemKitchen = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsItemProductMedic":

                            if (value == null || value is System.Boolean)
                                this.IsItemProductMedic = (System.Boolean?)value;
                            break;

                        case "IsItemProductNonMedic":

                            if (value == null || value is System.Boolean)
                                this.IsItemProductNonMedic = (System.Boolean?)value;
                            break;

                        case "IsItemKitchen":

                            if (value == null || value is System.Boolean)
                                this.IsItemKitchen = (System.Boolean?)value;
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
        /// Maps to ServiceUnitTransactionCode.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.SRTransactionCode
        /// </summary>
        virtual public System.String SRTransactionCode
        {
            get
            {
                return base.GetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.SRTransactionCode);
            }

            set
            {
                base.SetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.SRTransactionCode, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.IsItemProductMedic
        /// </summary>
        virtual public System.Boolean? IsItemProductMedic
        {
            get
            {
                return base.GetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductMedic);
            }

            set
            {
                base.SetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductMedic, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.IsItemProductNonMedic
        /// </summary>
        virtual public System.Boolean? IsItemProductNonMedic
        {
            get
            {
                return base.GetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductNonMedic);
            }

            set
            {
                base.SetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductNonMedic, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.IsItemKitchen
        /// </summary>
        virtual public System.Boolean? IsItemKitchen
        {
            get
            {
                return base.GetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemKitchen);
            }

            set
            {
                base.SetSystemBoolean(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemKitchen, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ServiceUnitTransactionCode.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esServiceUnitTransactionCode entity)
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

            public System.String SRTransactionCode
            {
                get
                {
                    System.String data = entity.SRTransactionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTransactionCode = null;
                    else entity.SRTransactionCode = Convert.ToString(value);
                }
            }

            public System.String IsItemProductMedic
            {
                get
                {
                    System.Boolean? data = entity.IsItemProductMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsItemProductMedic = null;
                    else entity.IsItemProductMedic = Convert.ToBoolean(value);
                }
            }

            public System.String IsItemProductNonMedic
            {
                get
                {
                    System.Boolean? data = entity.IsItemProductNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsItemProductNonMedic = null;
                    else entity.IsItemProductNonMedic = Convert.ToBoolean(value);
                }
            }

            public System.String IsItemKitchen
            {
                get
                {
                    System.Boolean? data = entity.IsItemKitchen;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsItemKitchen = null;
                    else entity.IsItemKitchen = Convert.ToBoolean(value);
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


            private esServiceUnitTransactionCode entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitTransactionCodeQuery query)
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
                throw new Exception("esServiceUnitTransactionCode can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ServiceUnitTransactionCode : esServiceUnitTransactionCode
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
    abstract public class esServiceUnitTransactionCodeQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitTransactionCodeMetadata.Meta();
            }
        }


        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem SRTransactionCode
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.SRTransactionCode, esSystemType.String);
            }
        }

        public esQueryItem IsItemProductMedic
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductMedic, esSystemType.Boolean);
            }
        }

        public esQueryItem IsItemProductNonMedic
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductNonMedic, esSystemType.Boolean);
            }
        }

        public esQueryItem IsItemKitchen
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemKitchen, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitTransactionCodeCollection")]
    public partial class ServiceUnitTransactionCodeCollection : esServiceUnitTransactionCodeCollection, IEnumerable<ServiceUnitTransactionCode>
    {
        public ServiceUnitTransactionCodeCollection()
        {

        }

        public static implicit operator List<ServiceUnitTransactionCode>(ServiceUnitTransactionCodeCollection coll)
        {
            List<ServiceUnitTransactionCode> list = new List<ServiceUnitTransactionCode>();

            foreach (ServiceUnitTransactionCode emp in coll)
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
                return ServiceUnitTransactionCodeMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitTransactionCodeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitTransactionCode(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitTransactionCode();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ServiceUnitTransactionCodeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitTransactionCodeQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ServiceUnitTransactionCodeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ServiceUnitTransactionCode AddNew()
        {
            ServiceUnitTransactionCode entity = base.AddNewEntity() as ServiceUnitTransactionCode;

            return entity;
        }

        public ServiceUnitTransactionCode FindByPrimaryKey(System.String serviceUnitID, System.String sRTransactionCode)
        {
            return base.FindByPrimaryKey(serviceUnitID, sRTransactionCode) as ServiceUnitTransactionCode;
        }


        #region IEnumerable<ServiceUnitTransactionCode> Members

        IEnumerator<ServiceUnitTransactionCode> IEnumerable<ServiceUnitTransactionCode>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitTransactionCode;
            }
        }

        #endregion

        private ServiceUnitTransactionCodeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitTransactionCode' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitTransactionCode ({ServiceUnitID},{SRTransactionCode})")]
    [Serializable]
    public partial class ServiceUnitTransactionCode : esServiceUnitTransactionCode
    {
        public ServiceUnitTransactionCode()
        {

        }

        public ServiceUnitTransactionCode(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitTransactionCodeMetadata.Meta();
            }
        }



        override protected esServiceUnitTransactionCodeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitTransactionCodeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ServiceUnitTransactionCodeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitTransactionCodeQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ServiceUnitTransactionCodeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitTransactionCodeQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ServiceUnitTransactionCodeQuery : esServiceUnitTransactionCodeQuery
    {
        public ServiceUnitTransactionCodeQuery()
        {

        }

        public ServiceUnitTransactionCodeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitTransactionCodeQuery";
        }


    }


    [Serializable]
    public partial class ServiceUnitTransactionCodeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitTransactionCodeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.SRTransactionCode, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.SRTransactionCode;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductMedic, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.IsItemProductMedic;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemProductNonMedic, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.IsItemProductNonMedic;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.IsItemKitchen, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.IsItemKitchen;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitTransactionCodeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ServiceUnitTransactionCodeMetadata Meta()
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
            public const string SRTransactionCode = "SRTransactionCode";
            public const string IsItemProductMedic = "IsItemProductMedic";
            public const string IsItemProductNonMedic = "IsItemProductNonMedic";
            public const string IsItemKitchen = "IsItemKitchen";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string SRTransactionCode = "SRTransactionCode";
            public const string IsItemProductMedic = "IsItemProductMedic";
            public const string IsItemProductNonMedic = "IsItemProductNonMedic";
            public const string IsItemKitchen = "IsItemKitchen";
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
            lock (typeof(ServiceUnitTransactionCodeMetadata))
            {
                if (ServiceUnitTransactionCodeMetadata.mapDelegates == null)
                {
                    ServiceUnitTransactionCodeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitTransactionCodeMetadata.meta == null)
                {
                    ServiceUnitTransactionCodeMetadata.meta = new ServiceUnitTransactionCodeMetadata();
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
                meta.AddTypeMap("SRTransactionCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsItemProductMedic", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsItemProductNonMedic", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsItemKitchen", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ServiceUnitTransactionCode";
                meta.Destination = "ServiceUnitTransactionCode";

                meta.spInsert = "proc_ServiceUnitTransactionCodeInsert";
                meta.spUpdate = "proc_ServiceUnitTransactionCodeUpdate";
                meta.spDelete = "proc_ServiceUnitTransactionCodeDelete";
                meta.spLoadAll = "proc_ServiceUnitTransactionCodeLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitTransactionCodeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitTransactionCodeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
