/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/19/2017 2:33:31 PM
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
    abstract public class esSupplierLocationCollection : esEntityCollectionWAuditLog
    {
        public esSupplierLocationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SupplierLocationCollection";
        }

        #region Query Logic
        protected void InitQuery(esSupplierLocationQuery query)
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
            this.InitQuery(query as esSupplierLocationQuery);
        }
        #endregion

        virtual public SupplierLocation DetachEntity(SupplierLocation entity)
        {
            return base.DetachEntity(entity) as SupplierLocation;
        }

        virtual public SupplierLocation AttachEntity(SupplierLocation entity)
        {
            return base.AttachEntity(entity) as SupplierLocation;
        }

        virtual public void Combine(SupplierLocationCollection collection)
        {
            base.Combine(collection);
        }

        new public SupplierLocation this[int index]
        {
            get
            {
                return base[index] as SupplierLocation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SupplierLocation);
        }
    }

    [Serializable]
    abstract public class esSupplierLocation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSupplierLocationQuery GetDynamicQuery()
        {
            return null;
        }

        public esSupplierLocation()
        {
        }

        public esSupplierLocation(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String supplierID, String locationID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID, locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID, locationID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String supplierID, String locationID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID, locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID, locationID);
        }

        private bool LoadByPrimaryKeyDynamic(String supplierID, String locationID)
        {
            esSupplierLocationQuery query = this.GetDynamicQuery();
            query.Where(query.SupplierID == supplierID, query.LocationID == locationID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String supplierID, String locationID)
        {
            esParameters parms = new esParameters();
            parms.Add("SupplierID", supplierID);
            parms.Add("LocationID", locationID);
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
                        case "SupplierID": this.str.SupplierID = (string)value; break;
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to SupplierLocation.SupplierID
        /// </summary>
        virtual public System.String SupplierID
        {
            get
            {
                return base.GetSystemString(SupplierLocationMetadata.ColumnNames.SupplierID);
            }

            set
            {
                base.SetSystemString(SupplierLocationMetadata.ColumnNames.SupplierID, value);
            }
        }
        /// <summary>
        /// Maps to SupplierLocation.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(SupplierLocationMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(SupplierLocationMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to SupplierLocation.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SupplierLocationMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SupplierLocationMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to SupplierLocation.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SupplierLocationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SupplierLocationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to SupplierLocation.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SupplierLocationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SupplierLocationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esSupplierLocation entity)
            {
                this.entity = entity;
            }
            public System.String SupplierID
            {
                get
                {
                    System.String data = entity.SupplierID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierID = null;
                    else entity.SupplierID = Convert.ToString(value);
                }
            }
            public System.String LocationID
            {
                get
                {
                    System.String data = entity.LocationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LocationID = null;
                    else entity.LocationID = Convert.ToString(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esSupplierLocation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSupplierLocationQuery query)
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
                throw new Exception("esSupplierLocation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class SupplierLocation : esSupplierLocation
    {
    }

    [Serializable]
    abstract public class esSupplierLocationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SupplierLocationMetadata.Meta();
            }
        }

        public esQueryItem SupplierID
        {
            get
            {
                return new esQueryItem(this, SupplierLocationMetadata.ColumnNames.SupplierID, esSystemType.String);
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, SupplierLocationMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SupplierLocationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SupplierLocationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SupplierLocationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SupplierLocationCollection")]
    public partial class SupplierLocationCollection : esSupplierLocationCollection, IEnumerable<SupplierLocation>
    {
        public SupplierLocationCollection()
        {

        }

        public static implicit operator List<SupplierLocation>(SupplierLocationCollection coll)
        {
            List<SupplierLocation> list = new List<SupplierLocation>();

            foreach (SupplierLocation emp in coll)
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
                return SupplierLocationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierLocationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SupplierLocation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SupplierLocation();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SupplierLocationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierLocationQuery();
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
        public bool Load(SupplierLocationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public SupplierLocation AddNew()
        {
            SupplierLocation entity = base.AddNewEntity() as SupplierLocation;

            return entity;
        }
        public SupplierLocation FindByPrimaryKey(String supplierID, String locationID)
        {
            return base.FindByPrimaryKey(supplierID, locationID) as SupplierLocation;
        }

        #region IEnumerable< SupplierLocation> Members

        IEnumerator<SupplierLocation> IEnumerable<SupplierLocation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SupplierLocation;
            }
        }

        #endregion

        private SupplierLocationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SupplierLocation' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SupplierLocation ({SupplierID, LocationID})")]
    [Serializable]
    public partial class SupplierLocation : esSupplierLocation
    {
        public SupplierLocation()
        {
        }

        public SupplierLocation(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SupplierLocationMetadata.Meta();
            }
        }

        override protected esSupplierLocationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierLocationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SupplierLocationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierLocationQuery();
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
        public bool Load(SupplierLocationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SupplierLocationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SupplierLocationQuery : esSupplierLocationQuery
    {
        public SupplierLocationQuery()
        {

        }

        public SupplierLocationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SupplierLocationQuery";
        }
    }

    [Serializable]
    public partial class SupplierLocationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SupplierLocationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SupplierLocationMetadata.ColumnNames.SupplierID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierLocationMetadata.PropertyNames.SupplierID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierLocationMetadata.ColumnNames.LocationID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierLocationMetadata.PropertyNames.LocationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierLocationMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierLocationMetadata.PropertyNames.IsActive;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierLocationMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierLocationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierLocationMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierLocationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public SupplierLocationMetadata Meta()
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
            public const string SupplierID = "SupplierID";
            public const string LocationID = "LocationID";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SupplierID = "SupplierID";
            public const string LocationID = "LocationID";
            public const string IsActive = "IsActive";
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
            lock (typeof(SupplierLocationMetadata))
            {
                if (SupplierLocationMetadata.mapDelegates == null)
                {
                    SupplierLocationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SupplierLocationMetadata.meta == null)
                {
                    SupplierLocationMetadata.meta = new SupplierLocationMetadata();
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

                meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "SupplierLocation";
                meta.Destination = "SupplierLocation";
                meta.spInsert = "proc_SupplierLocationInsert";
                meta.spUpdate = "proc_SupplierLocationUpdate";
                meta.spDelete = "proc_SupplierLocationDelete";
                meta.spLoadAll = "proc_SupplierLocationLoadAll";
                meta.spLoadByPrimaryKey = "proc_SupplierLocationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SupplierLocationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
