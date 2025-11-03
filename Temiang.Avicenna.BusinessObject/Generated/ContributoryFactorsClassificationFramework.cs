/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/5/2016 1:15:57 PM
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
    abstract public class esContributoryFactorsClassificationFrameworkCollection : esEntityCollectionWAuditLog
    {
        public esContributoryFactorsClassificationFrameworkCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ContributoryFactorsClassificationFrameworkCollection";
        }

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkQuery query)
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
            this.InitQuery(query as esContributoryFactorsClassificationFrameworkQuery);
        }
        #endregion

        virtual public ContributoryFactorsClassificationFramework DetachEntity(ContributoryFactorsClassificationFramework entity)
        {
            return base.DetachEntity(entity) as ContributoryFactorsClassificationFramework;
        }

        virtual public ContributoryFactorsClassificationFramework AttachEntity(ContributoryFactorsClassificationFramework entity)
        {
            return base.AttachEntity(entity) as ContributoryFactorsClassificationFramework;
        }

        virtual public void Combine(ContributoryFactorsClassificationFrameworkCollection collection)
        {
            base.Combine(collection);
        }

        new public ContributoryFactorsClassificationFramework this[int index]
        {
            get
            {
                return base[index] as ContributoryFactorsClassificationFramework;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ContributoryFactorsClassificationFramework);
        }
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFramework : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esContributoryFactorsClassificationFrameworkQuery GetDynamicQuery()
        {
            return null;
        }

        public esContributoryFactorsClassificationFramework()
        {
        }

        public esContributoryFactorsClassificationFramework(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String factorID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String factorID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID);
        }

        private bool LoadByPrimaryKeyDynamic(String factorID)
        {
            esContributoryFactorsClassificationFrameworkQuery query = this.GetDynamicQuery();
            query.Where(query.FactorID == factorID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String factorID)
        {
            esParameters parms = new esParameters();
            parms.Add("FactorID", factorID);
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
                        case "FactorID": this.str.FactorID = (string)value; break;
                        case "FactorName": this.str.FactorName = (string)value; break;
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
        /// Maps to ContributoryFactorsClassificationFramework.FactorID
        /// </summary>
        virtual public System.String FactorID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFramework.FactorName
        /// </summary>
        virtual public System.String FactorName
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorName);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorName, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFramework.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFramework.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esContributoryFactorsClassificationFramework entity)
            {
                this.entity = entity;
            }
            public System.String FactorID
            {
                get
                {
                    System.String data = entity.FactorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorID = null;
                    else entity.FactorID = Convert.ToString(value);
                }
            }
            public System.String FactorName
            {
                get
                {
                    System.String data = entity.FactorName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorName = null;
                    else entity.FactorName = Convert.ToString(value);
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
            private esContributoryFactorsClassificationFramework entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkQuery query)
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
                throw new Exception("esContributoryFactorsClassificationFramework can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ContributoryFactorsClassificationFramework : esContributoryFactorsClassificationFramework
    {
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFrameworkQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkMetadata.Meta();
            }
        }

        public esQueryItem FactorID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorID, esSystemType.String);
            }
        }

        public esQueryItem FactorName
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ContributoryFactorsClassificationFrameworkCollection")]
    public partial class ContributoryFactorsClassificationFrameworkCollection : esContributoryFactorsClassificationFrameworkCollection, IEnumerable<ContributoryFactorsClassificationFramework>
    {
        public ContributoryFactorsClassificationFrameworkCollection()
        {

        }

        public static implicit operator List<ContributoryFactorsClassificationFramework>(ContributoryFactorsClassificationFrameworkCollection coll)
        {
            List<ContributoryFactorsClassificationFramework> list = new List<ContributoryFactorsClassificationFramework>();

            foreach (ContributoryFactorsClassificationFramework emp in coll)
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
                return ContributoryFactorsClassificationFrameworkMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ContributoryFactorsClassificationFramework(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ContributoryFactorsClassificationFramework();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ContributoryFactorsClassificationFramework AddNew()
        {
            ContributoryFactorsClassificationFramework entity = base.AddNewEntity() as ContributoryFactorsClassificationFramework;

            return entity;
        }
        public ContributoryFactorsClassificationFramework FindByPrimaryKey(String factorID)
        {
            return base.FindByPrimaryKey(factorID) as ContributoryFactorsClassificationFramework;
        }

        #region IEnumerable< ContributoryFactorsClassificationFramework> Members

        IEnumerator<ContributoryFactorsClassificationFramework> IEnumerable<ContributoryFactorsClassificationFramework>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ContributoryFactorsClassificationFramework;
            }
        }

        #endregion

        private ContributoryFactorsClassificationFrameworkQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ContributoryFactorsClassificationFramework' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ContributoryFactorsClassificationFramework ({FactorID})")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFramework : esContributoryFactorsClassificationFramework
    {
        public ContributoryFactorsClassificationFramework()
        {
        }

        public ContributoryFactorsClassificationFramework(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkMetadata.Meta();
            }
        }

        override protected esContributoryFactorsClassificationFrameworkQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ContributoryFactorsClassificationFrameworkQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkQuery : esContributoryFactorsClassificationFrameworkQuery
    {
        public ContributoryFactorsClassificationFrameworkQuery()
        {

        }

        public ContributoryFactorsClassificationFrameworkQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ContributoryFactorsClassificationFrameworkQuery";
        }
    }

    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ContributoryFactorsClassificationFrameworkMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkMetadata.PropertyNames.FactorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.FactorName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkMetadata.PropertyNames.FactorName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ContributoryFactorsClassificationFrameworkMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ContributoryFactorsClassificationFrameworkMetadata Meta()
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
            public const string FactorID = "FactorID";
            public const string FactorName = "FactorName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string FactorID = "FactorID";
            public const string FactorName = "FactorName";
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
            lock (typeof(ContributoryFactorsClassificationFrameworkMetadata))
            {
                if (ContributoryFactorsClassificationFrameworkMetadata.mapDelegates == null)
                {
                    ContributoryFactorsClassificationFrameworkMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ContributoryFactorsClassificationFrameworkMetadata.meta == null)
                {
                    ContributoryFactorsClassificationFrameworkMetadata.meta = new ContributoryFactorsClassificationFrameworkMetadata();
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

                meta.AddTypeMap("FactorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FactorName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ContributoryFactorsClassificationFramework";
                meta.Destination = "ContributoryFactorsClassificationFramework";
                meta.spInsert = "proc_ContributoryFactorsClassificationFrameworkInsert";
                meta.spUpdate = "proc_ContributoryFactorsClassificationFrameworkUpdate";
                meta.spDelete = "proc_ContributoryFactorsClassificationFrameworkDelete";
                meta.spLoadAll = "proc_ContributoryFactorsClassificationFrameworkLoadAll";
                meta.spLoadByPrimaryKey = "proc_ContributoryFactorsClassificationFrameworkLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ContributoryFactorsClassificationFrameworkMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
