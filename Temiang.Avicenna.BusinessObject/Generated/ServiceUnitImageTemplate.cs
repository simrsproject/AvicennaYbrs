/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2016 8:38:36 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esServiceUnitImageTemplateCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitImageTemplateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ServiceUnitImageTemplateCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitImageTemplateQuery query)
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
            this.InitQuery(query as esServiceUnitImageTemplateQuery);
        }
        #endregion

        virtual public ServiceUnitImageTemplate DetachEntity(ServiceUnitImageTemplate entity)
        {
            return base.DetachEntity(entity) as ServiceUnitImageTemplate;
        }

        virtual public ServiceUnitImageTemplate AttachEntity(ServiceUnitImageTemplate entity)
        {
            return base.AttachEntity(entity) as ServiceUnitImageTemplate;
        }

        virtual public void Combine(ServiceUnitImageTemplateCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitImageTemplate this[int index]
        {
            get
            {
                return base[index] as ServiceUnitImageTemplate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitImageTemplate);
        }
    }

    [Serializable]
    abstract public class esServiceUnitImageTemplate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitImageTemplateQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitImageTemplate()
        {
        }

        public esServiceUnitImageTemplate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String serviceUnitID, String imageTemplateID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, imageTemplateID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, imageTemplateID);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String imageTemplateID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, imageTemplateID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, imageTemplateID);
        }

        private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String imageTemplateID)
        {
            esServiceUnitImageTemplateQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID, query.ImageTemplateID == imageTemplateID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String imageTemplateID)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID);
            parms.Add("ImageTemplateID", imageTemplateID);
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
                        case "ImageTemplateID": this.str.ImageTemplateID = (string)value; break;
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
        /// Maps to ServiceUnitImageTemplate.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitImageTemplate.ImageTemplateID
        /// </summary>
        virtual public System.String ImageTemplateID
        {
            get
            {
                return base.GetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.ImageTemplateID);
            }

            set
            {
                base.SetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.ImageTemplateID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitImageTemplate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitImageTemplate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
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
            public esStrings(esServiceUnitImageTemplate entity)
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
            public System.String ImageTemplateID
            {
                get
                {
                    System.String data = entity.ImageTemplateID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImageTemplateID = null;
                    else entity.ImageTemplateID = Convert.ToString(value);
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
            private esServiceUnitImageTemplate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitImageTemplateQuery query)
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
                throw new Exception("esServiceUnitImageTemplate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ServiceUnitImageTemplate : esServiceUnitImageTemplate
    {
    }

    [Serializable]
    abstract public class esServiceUnitImageTemplateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitImageTemplateMetadata.Meta();
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitImageTemplateMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ImageTemplateID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitImageTemplateMetadata.ColumnNames.ImageTemplateID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitImageTemplateCollection")]
    public partial class ServiceUnitImageTemplateCollection : esServiceUnitImageTemplateCollection, IEnumerable<ServiceUnitImageTemplate>
    {
        public ServiceUnitImageTemplateCollection()
        {

        }

        public static implicit operator List<ServiceUnitImageTemplate>(ServiceUnitImageTemplateCollection coll)
        {
            List<ServiceUnitImageTemplate> list = new List<ServiceUnitImageTemplate>();

            foreach (ServiceUnitImageTemplate emp in coll)
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
                return ServiceUnitImageTemplateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitImageTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitImageTemplate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitImageTemplate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitImageTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitImageTemplateQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(ServiceUnitImageTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ServiceUnitImageTemplate AddNew()
        {
            ServiceUnitImageTemplate entity = base.AddNewEntity() as ServiceUnitImageTemplate;

            return entity;
        }
        public ServiceUnitImageTemplate FindByPrimaryKey(String serviceUnitID, String imageTemplateID)
        {
            return base.FindByPrimaryKey(serviceUnitID, imageTemplateID) as ServiceUnitImageTemplate;
        }

        #region IEnumerable< ServiceUnitImageTemplate> Members

        IEnumerator<ServiceUnitImageTemplate> IEnumerable<ServiceUnitImageTemplate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitImageTemplate;
            }
        }

        #endregion

        private ServiceUnitImageTemplateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitImageTemplate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ServiceUnitImageTemplate ({ServiceUnitID, ImageTemplateID})")]
    [Serializable]
    public partial class ServiceUnitImageTemplate : esServiceUnitImageTemplate
    {
        public ServiceUnitImageTemplate()
        {
        }

        public ServiceUnitImageTemplate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitImageTemplateMetadata.Meta();
            }
        }

        override protected esServiceUnitImageTemplateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitImageTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitImageTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitImageTemplateQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(ServiceUnitImageTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitImageTemplateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ServiceUnitImageTemplateQuery : esServiceUnitImageTemplateQuery
    {
        public ServiceUnitImageTemplateQuery()
        {

        }

        public ServiceUnitImageTemplateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitImageTemplateQuery";
        }
    }

    [Serializable]
    public partial class ServiceUnitImageTemplateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitImageTemplateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitImageTemplateMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitImageTemplateMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitImageTemplateMetadata.ColumnNames.ImageTemplateID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitImageTemplateMetadata.PropertyNames.ImageTemplateID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitImageTemplateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitImageTemplateMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitImageTemplateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ServiceUnitImageTemplateMetadata Meta()
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
            public const string ImageTemplateID = "ImageTemplateID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ImageTemplateID = "ImageTemplateID";
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
            lock (typeof(ServiceUnitImageTemplateMetadata))
            {
                if (ServiceUnitImageTemplateMetadata.mapDelegates == null)
                {
                    ServiceUnitImageTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitImageTemplateMetadata.meta == null)
                {
                    ServiceUnitImageTemplateMetadata.meta = new ServiceUnitImageTemplateMetadata();
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
                meta.AddTypeMap("ImageTemplateID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ServiceUnitImageTemplate";
                meta.Destination = "ServiceUnitImageTemplate";
                meta.spInsert = "proc_ServiceUnitImageTemplateInsert";
                meta.spUpdate = "proc_ServiceUnitImageTemplateUpdate";
                meta.spDelete = "proc_ServiceUnitImageTemplateDelete";
                meta.spLoadAll = "proc_ServiceUnitImageTemplateLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitImageTemplateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitImageTemplateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
