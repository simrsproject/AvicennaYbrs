/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2016 1:34:43 PM
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
    abstract public class esConsolidationLogDataCollection : esEntityCollectionWAuditLog
    {
        public esConsolidationLogDataCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "ConsolidationLogDataCollection";
        }

        #region Query Logic
        protected void InitQuery(esConsolidationLogDataQuery query)
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
            this.InitQuery(query as esConsolidationLogDataQuery);
        }
        #endregion

        virtual public ConsolidationLogData DetachEntity(ConsolidationLogData entity)
        {
            return base.DetachEntity(entity) as ConsolidationLogData;
        }

        virtual public ConsolidationLogData AttachEntity(ConsolidationLogData entity)
        {
            return base.AttachEntity(entity) as ConsolidationLogData;
        }

        virtual public void Combine(ConsolidationLogDataCollection collection)
        {
            base.Combine(collection);
        }

        new public ConsolidationLogData this[int index]
        {
            get
            {
                return base[index] as ConsolidationLogData;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ConsolidationLogData);
        }
    }

    [Serializable]
    abstract public class esConsolidationLogData : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esConsolidationLogDataQuery GetDynamicQuery()
        {
            return null;
        }

        public esConsolidationLogData()
        {
        }

        public esConsolidationLogData(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 consolidationLogID, String columnName)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consolidationLogID, columnName);
            else
                return LoadByPrimaryKeyStoredProcedure(consolidationLogID, columnName);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 consolidationLogID, String columnName)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consolidationLogID, columnName);
            else
                return LoadByPrimaryKeyStoredProcedure(consolidationLogID, columnName);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 consolidationLogID, String columnName)
        {
            esConsolidationLogDataQuery query = this.GetDynamicQuery();
            query.Where(query.ConsolidationLogID == consolidationLogID, query.ColumnName == columnName);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 consolidationLogID, String columnName)
        {
            esParameters parms = new esParameters();
            parms.Add("ConsolidationLogID", consolidationLogID);
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
                        case "ConsolidationLogID": this.str.ConsolidationLogID = (string)value; break;
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
                        case "ConsolidationLogID":

                            if (value == null || value is System.Int32)
                                this.ConsolidationLogID = (System.Int32?)value;
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
        /// Maps to ConsolidationLogData.ConsolidationLogID
        /// </summary>
        virtual public System.Int32? ConsolidationLogID
        {
            get
            {
                return base.GetSystemInt32(ConsolidationLogDataMetadata.ColumnNames.ConsolidationLogID);
            }

            set
            {
                base.SetSystemInt32(ConsolidationLogDataMetadata.ColumnNames.ConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLogData.ColumnName
        /// </summary>
        virtual public System.String ColumnName
        {
            get
            {
                return base.GetSystemString(ConsolidationLogDataMetadata.ColumnNames.ColumnName);
            }

            set
            {
                base.SetSystemString(ConsolidationLogDataMetadata.ColumnNames.ColumnName, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLogData.OldValue
        /// </summary>
        virtual public System.String OldValue
        {
            get
            {
                return base.GetSystemString(ConsolidationLogDataMetadata.ColumnNames.OldValue);
            }

            set
            {
                base.SetSystemString(ConsolidationLogDataMetadata.ColumnNames.OldValue, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLogData.NewValue
        /// </summary>
        virtual public System.String NewValue
        {
            get
            {
                return base.GetSystemString(ConsolidationLogDataMetadata.ColumnNames.NewValue);
            }

            set
            {
                base.SetSystemString(ConsolidationLogDataMetadata.ColumnNames.NewValue, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLogData.IsInPrimaryKey
        /// </summary>
        virtual public System.Boolean? IsInPrimaryKey
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationLogDataMetadata.ColumnNames.IsInPrimaryKey);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationLogDataMetadata.ColumnNames.IsInPrimaryKey, value);
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
            public esStrings(esConsolidationLogData entity)
            {
                this.entity = entity;
            }
            public System.String ConsolidationLogID
            {
                get
                {
                    System.Int32? data = entity.ConsolidationLogID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsolidationLogID = null;
                    else entity.ConsolidationLogID = Convert.ToInt32(value);
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
            private esConsolidationLogData entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esConsolidationLogDataQuery query)
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
                throw new Exception("esConsolidationLogData can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ConsolidationLogData : esConsolidationLogData
    {
    }

    [Serializable]
    abstract public class esConsolidationLogDataQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationLogDataMetadata.Meta();
            }
        }

        public esQueryItem ConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogDataMetadata.ColumnNames.ConsolidationLogID, esSystemType.Int32);
            }
        }

        public esQueryItem ColumnName
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogDataMetadata.ColumnNames.ColumnName, esSystemType.String);
            }
        }

        public esQueryItem OldValue
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogDataMetadata.ColumnNames.OldValue, esSystemType.String);
            }
        }

        public esQueryItem NewValue
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogDataMetadata.ColumnNames.NewValue, esSystemType.String);
            }
        }

        public esQueryItem IsInPrimaryKey
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogDataMetadata.ColumnNames.IsInPrimaryKey, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ConsolidationLogDataCollection")]
    public partial class ConsolidationLogDataCollection : esConsolidationLogDataCollection, IEnumerable<ConsolidationLogData>
    {
        public ConsolidationLogDataCollection()
        {

        }

        public static implicit operator List<ConsolidationLogData>(ConsolidationLogDataCollection coll)
        {
            List<ConsolidationLogData> list = new List<ConsolidationLogData>();

            foreach (ConsolidationLogData emp in coll)
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
                return ConsolidationLogDataMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationLogDataQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ConsolidationLogData(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ConsolidationLogData();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationLogDataQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationLogDataQuery();
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
        public bool Load(ConsolidationLogDataQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ConsolidationLogData AddNew()
        {
            ConsolidationLogData entity = base.AddNewEntity() as ConsolidationLogData;

            return entity;
        }
        public ConsolidationLogData FindByPrimaryKey(Int32 consolidationLogID, String columnName)
        {
            return base.FindByPrimaryKey(consolidationLogID, columnName) as ConsolidationLogData;
        }

        #region IEnumerable< ConsolidationLogData> Members

        IEnumerator<ConsolidationLogData> IEnumerable<ConsolidationLogData>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ConsolidationLogData;
            }
        }

        #endregion

        private ConsolidationLogDataQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ConsolidationLogData' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConsolidationLogData ({ConsolidationLogID, ColumnName})")]
    [Serializable]
    public partial class ConsolidationLogData : esConsolidationLogData
    {
        public ConsolidationLogData()
        {
        }

        public ConsolidationLogData(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationLogDataMetadata.Meta();
            }
        }

        override protected esConsolidationLogDataQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationLogDataQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationLogDataQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationLogDataQuery();
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
        public bool Load(ConsolidationLogDataQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ConsolidationLogDataQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ConsolidationLogDataQuery : esConsolidationLogDataQuery
    {
        public ConsolidationLogDataQuery()
        {

        }

        public ConsolidationLogDataQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ConsolidationLogDataQuery";
        }
    }

    [Serializable]
    public partial class ConsolidationLogDataMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ConsolidationLogDataMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ConsolidationLogDataMetadata.ColumnNames.ConsolidationLogID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ConsolidationLogDataMetadata.PropertyNames.ConsolidationLogID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogDataMetadata.ColumnNames.ColumnName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogDataMetadata.PropertyNames.ColumnName;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogDataMetadata.ColumnNames.OldValue, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogDataMetadata.PropertyNames.OldValue;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogDataMetadata.ColumnNames.NewValue, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogDataMetadata.PropertyNames.NewValue;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogDataMetadata.ColumnNames.IsInPrimaryKey, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationLogDataMetadata.PropertyNames.IsInPrimaryKey;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ConsolidationLogDataMetadata Meta()
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
            public const string ConsolidationLogID = "ConsolidationLogID";
            public const string ColumnName = "ColumnName";
            public const string OldValue = "OldValue";
            public const string NewValue = "NewValue";
            public const string IsInPrimaryKey = "IsInPrimaryKey";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ConsolidationLogID = "ConsolidationLogID";
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
            lock (typeof(ConsolidationLogDataMetadata))
            {
                if (ConsolidationLogDataMetadata.mapDelegates == null)
                {
                    ConsolidationLogDataMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ConsolidationLogDataMetadata.meta == null)
                {
                    ConsolidationLogDataMetadata.meta = new ConsolidationLogDataMetadata();
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

                meta.AddTypeMap("ConsolidationLogID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ColumnName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OldValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NewValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInPrimaryKey", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ConsolidationLogData";
                meta.Destination = "ConsolidationLogData";
                meta.spInsert = "proc_ConsolidationLogDataInsert";
                meta.spUpdate = "proc_ConsolidationLogDataUpdate";
                meta.spDelete = "proc_ConsolidationLogDataDelete";
                meta.spLoadAll = "proc_ConsolidationLogDataLoadAll";
                meta.spLoadByPrimaryKey = "proc_ConsolidationLogDataLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ConsolidationLogDataMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
