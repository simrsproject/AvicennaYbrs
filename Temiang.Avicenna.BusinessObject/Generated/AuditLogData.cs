/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/30/2016 2:59:26 PM
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
    abstract public class esAuditLogDataCollection : esEntityCollectionWAuditLog
    {
        public esAuditLogDataCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "AuditLogDataCollection";
        }

        #region Query Logic
        protected void InitQuery(esAuditLogDataQuery query)
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
            this.InitQuery(query as esAuditLogDataQuery);
        }
        #endregion

        virtual public AuditLogData DetachEntity(AuditLogData entity)
        {
            return base.DetachEntity(entity) as AuditLogData;
        }

        virtual public AuditLogData AttachEntity(AuditLogData entity)
        {
            return base.AttachEntity(entity) as AuditLogData;
        }

        virtual public void Combine(AuditLogDataCollection collection)
        {
            base.Combine(collection);
        }

        new public AuditLogData this[int index]
        {
            get
            {
                return base[index] as AuditLogData;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AuditLogData);
        }
    }

    [Serializable]
    abstract public class esAuditLogData : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAuditLogDataQuery GetDynamicQuery()
        {
            return null;
        }

        public esAuditLogData()
        {
        }

        public esAuditLogData(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 auditLogID, String columnName)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(auditLogID, columnName);
            else
                return LoadByPrimaryKeyStoredProcedure(auditLogID, columnName);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 auditLogID, String columnName)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(auditLogID, columnName);
            else
                return LoadByPrimaryKeyStoredProcedure(auditLogID, columnName);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 auditLogID, String columnName)
        {
            esAuditLogDataQuery query = this.GetDynamicQuery();
            query.Where(query.AuditLogID == auditLogID, query.ColumnName == columnName);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 auditLogID, String columnName)
        {
            esParameters parms = new esParameters();
            parms.Add("AuditLogID", auditLogID);
            parms.Add("ColumnName", columnName);
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
                        case "AuditLogID": this.str.AuditLogID = (string)value; break;
                        case "ColumnName": this.str.ColumnName = (string)value; break;
                        case "OldValue": this.str.OldValue = (string)value; break;
                        case "NewValue": this.str.NewValue = (string)value; break;
                        case "IsInPrimaryKey": this.str.IsInPrimaryKey = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AuditLogID":

                            if (value == null || value is System.Int32)
                                this.AuditLogID = (System.Int32?)value;
                            break;
                        case "IsInPrimaryKey":

                            if (value == null || value is System.Boolean)
                                this.IsInPrimaryKey = (System.Boolean?)value;
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
        /// Maps to AuditLogData.AuditLogID
        /// </summary>
        virtual public System.Int32? AuditLogID
        {
            get
            {
                return base.GetSystemInt32(AuditLogDataMetadata.ColumnNames.AuditLogID);
            }

            set
            {
                base.SetSystemInt32(AuditLogDataMetadata.ColumnNames.AuditLogID, value);
            }
        }
        /// <summary>
        /// Maps to AuditLogData.ColumnName
        /// </summary>
        virtual public System.String ColumnName
        {
            get
            {
                return base.GetSystemString(AuditLogDataMetadata.ColumnNames.ColumnName);
            }

            set
            {
                base.SetSystemString(AuditLogDataMetadata.ColumnNames.ColumnName, value);
            }
        }
        /// <summary>
        /// Maps to AuditLogData.OldValue
        /// </summary>
        virtual public System.String OldValue
        {
            get
            {
                return base.GetSystemString(AuditLogDataMetadata.ColumnNames.OldValue);
            }

            set
            {
                base.SetSystemString(AuditLogDataMetadata.ColumnNames.OldValue, value);
            }
        }
        /// <summary>
        /// Maps to AuditLogData.NewValue
        /// </summary>
        virtual public System.String NewValue
        {
            get
            {
                return base.GetSystemString(AuditLogDataMetadata.ColumnNames.NewValue);
            }

            set
            {
                base.SetSystemString(AuditLogDataMetadata.ColumnNames.NewValue, value);
            }
        }
        /// <summary>
        /// Maps to AuditLogData.IsInPrimaryKey
        /// </summary>
        virtual public System.Boolean? IsInPrimaryKey
        {
            get
            {
                return base.GetSystemBoolean(AuditLogDataMetadata.ColumnNames.IsInPrimaryKey);
            }

            set
            {
                base.SetSystemBoolean(AuditLogDataMetadata.ColumnNames.IsInPrimaryKey, value);
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
            public esStrings(esAuditLogData entity)
            {
                this.entity = entity;
            }
            public System.String AuditLogID
            {
                get
                {
                    System.Int32? data = entity.AuditLogID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AuditLogID = null;
                    else entity.AuditLogID = Convert.ToInt32(value);
                }
            }
            public System.String ColumnName
            {
                get
                {
                    System.String data = entity.ColumnName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ColumnName = null;
                    else entity.ColumnName = Convert.ToString(value);
                }
            }
            public System.String OldValue
            {
                get
                {
                    System.String data = entity.OldValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OldValue = null;
                    else entity.OldValue = Convert.ToString(value);
                }
            }
            public System.String NewValue
            {
                get
                {
                    System.String data = entity.NewValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NewValue = null;
                    else entity.NewValue = Convert.ToString(value);
                }
            }
            public System.String IsInPrimaryKey
            {
                get
                {
                    System.Boolean? data = entity.IsInPrimaryKey;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInPrimaryKey = null;
                    else entity.IsInPrimaryKey = Convert.ToBoolean(value);
                }
            }
            private esAuditLogData entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAuditLogDataQuery query)
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
                throw new Exception("esAuditLogData can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AuditLogData : esAuditLogData
    {
    }

    [Serializable]
    abstract public class esAuditLogDataQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return AuditLogDataMetadata.Meta();
            }
        }

        public esQueryItem AuditLogID
        {
            get
            {
                return new esQueryItem(this, AuditLogDataMetadata.ColumnNames.AuditLogID, esSystemType.Int32);
            }
        }

        public esQueryItem ColumnName
        {
            get
            {
                return new esQueryItem(this, AuditLogDataMetadata.ColumnNames.ColumnName, esSystemType.String);
            }
        }

        public esQueryItem OldValue
        {
            get
            {
                return new esQueryItem(this, AuditLogDataMetadata.ColumnNames.OldValue, esSystemType.String);
            }
        }

        public esQueryItem NewValue
        {
            get
            {
                return new esQueryItem(this, AuditLogDataMetadata.ColumnNames.NewValue, esSystemType.String);
            }
        }

        public esQueryItem IsInPrimaryKey
        {
            get
            {
                return new esQueryItem(this, AuditLogDataMetadata.ColumnNames.IsInPrimaryKey, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AuditLogDataCollection")]
    public partial class AuditLogDataCollection : esAuditLogDataCollection, IEnumerable<AuditLogData>
    {
        public AuditLogDataCollection()
        {

        }

        public static implicit operator List<AuditLogData>(AuditLogDataCollection coll)
        {
            List<AuditLogData> list = new List<AuditLogData>();

            foreach (AuditLogData emp in coll)
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
                return AuditLogDataMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AuditLogDataQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AuditLogData(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AuditLogData();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AuditLogDataQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AuditLogDataQuery();
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
        public bool Load(AuditLogDataQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AuditLogData AddNew()
        {
            AuditLogData entity = base.AddNewEntity() as AuditLogData;

            return entity;
        }
        public AuditLogData FindByPrimaryKey(Int32 auditLogID, String columnName)
        {
            return base.FindByPrimaryKey(auditLogID, columnName) as AuditLogData;
        }

        #region IEnumerable< AuditLogData> Members

        IEnumerator<AuditLogData> IEnumerable<AuditLogData>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AuditLogData;
            }
        }

        #endregion

        private AuditLogDataQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AuditLogData' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AuditLogData ({AuditLogID, ColumnName})")]
    [Serializable]
    public partial class AuditLogData : esAuditLogData
    {
        public AuditLogData()
        {
        }

        public AuditLogData(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AuditLogDataMetadata.Meta();
            }
        }

        override protected esAuditLogDataQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AuditLogDataQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AuditLogDataQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AuditLogDataQuery();
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
        public bool Load(AuditLogDataQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AuditLogDataQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AuditLogDataQuery : esAuditLogDataQuery
    {
        public AuditLogDataQuery()
        {

        }

        public AuditLogDataQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AuditLogDataQuery";
        }
    }

    [Serializable]
    public partial class AuditLogDataMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AuditLogDataMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AuditLogDataMetadata.ColumnNames.AuditLogID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AuditLogDataMetadata.PropertyNames.AuditLogID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogDataMetadata.ColumnNames.ColumnName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogDataMetadata.PropertyNames.ColumnName;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogDataMetadata.ColumnNames.OldValue, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogDataMetadata.PropertyNames.OldValue;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogDataMetadata.ColumnNames.NewValue, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogDataMetadata.PropertyNames.NewValue;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogDataMetadata.ColumnNames.IsInPrimaryKey, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AuditLogDataMetadata.PropertyNames.IsInPrimaryKey;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AuditLogDataMetadata Meta()
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
            public const string AuditLogID = "AuditLogID";
            public const string ColumnName = "ColumnName";
            public const string OldValue = "OldValue";
            public const string NewValue = "NewValue";
            public const string IsInPrimaryKey = "IsInPrimaryKey";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AuditLogID = "AuditLogID";
            public const string ColumnName = "ColumnName";
            public const string OldValue = "OldValue";
            public const string NewValue = "NewValue";
            public const string IsInPrimaryKey = "IsInPrimaryKey";
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
            lock (typeof(AuditLogDataMetadata))
            {
                if (AuditLogDataMetadata.mapDelegates == null)
                {
                    AuditLogDataMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AuditLogDataMetadata.meta == null)
                {
                    AuditLogDataMetadata.meta = new AuditLogDataMetadata();
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

                meta.AddTypeMap("AuditLogID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ColumnName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OldValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NewValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInPrimaryKey", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "AuditLogData";
                meta.Destination = "AuditLogData";
                meta.spInsert = "proc_AuditLogDataInsert";
                meta.spUpdate = "proc_AuditLogDataUpdate";
                meta.spDelete = "proc_AuditLogDataDelete";
                meta.spLoadAll = "proc_AuditLogDataLoadAll";
                meta.spLoadByPrimaryKey = "proc_AuditLogDataLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AuditLogDataMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
