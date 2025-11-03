/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/5/2024 12:47:36 PM
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
    abstract public class esTERMonthlyCollection : esEntityCollectionWAuditLog
    {
        public esTERMonthlyCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TERMonthlyCollection";
        }

        #region Query Logic
        protected void InitQuery(esTERMonthlyQuery query)
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
            this.InitQuery(query as esTERMonthlyQuery);
        }
        #endregion

        virtual public TERMonthly DetachEntity(TERMonthly entity)
        {
            return base.DetachEntity(entity) as TERMonthly;
        }

        virtual public TERMonthly AttachEntity(TERMonthly entity)
        {
            return base.AttachEntity(entity) as TERMonthly;
        }

        virtual public void Combine(TERMonthlyCollection collection)
        {
            base.Combine(collection);
        }

        new public TERMonthly this[int index]
        {
            get
            {
                return base[index] as TERMonthly;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TERMonthly);
        }
    }

    [Serializable]
    abstract public class esTERMonthly : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTERMonthlyQuery GetDynamicQuery()
        {
            return null;
        }

        public esTERMonthly()
        {
        }

        public esTERMonthly(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 tERMonthlyID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tERMonthlyID);
            else
                return LoadByPrimaryKeyStoredProcedure(tERMonthlyID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 tERMonthlyID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tERMonthlyID);
            else
                return LoadByPrimaryKeyStoredProcedure(tERMonthlyID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 tERMonthlyID)
        {
            esTERMonthlyQuery query = this.GetDynamicQuery();
            query.Where(query.TERMonthlyID == tERMonthlyID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 tERMonthlyID)
        {
            esParameters parms = new esParameters();
            parms.Add("TERMonthlyID", tERMonthlyID);
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
                        case "TERMonthlyID": this.str.TERMonthlyID = (string)value; break;
                        case "ValidFrom": this.str.ValidFrom = (string)value; break;
                        case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TERMonthlyID":

                            if (value == null || value is System.Int32)
                                this.TERMonthlyID = (System.Int32?)value;
                            break;
                        case "ValidFrom":

                            if (value == null || value is System.DateTime)
                                this.ValidFrom = (System.DateTime?)value;
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
        /// Maps to TERMonthly.TERMonthlyID
        /// </summary>
        virtual public System.Int32? TERMonthlyID
        {
            get
            {
                return base.GetSystemInt32(TERMonthlyMetadata.ColumnNames.TERMonthlyID);
            }

            set
            {
                base.SetSystemInt32(TERMonthlyMetadata.ColumnNames.TERMonthlyID, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthly.ValidFrom
        /// </summary>
        virtual public System.DateTime? ValidFrom
        {
            get
            {
                return base.GetSystemDateTime(TERMonthlyMetadata.ColumnNames.ValidFrom);
            }

            set
            {
                base.SetSystemDateTime(TERMonthlyMetadata.ColumnNames.ValidFrom, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthly.SRTaxStatus
        /// </summary>
        virtual public System.String SRTaxStatus
        {
            get
            {
                return base.GetSystemString(TERMonthlyMetadata.ColumnNames.SRTaxStatus);
            }

            set
            {
                base.SetSystemString(TERMonthlyMetadata.ColumnNames.SRTaxStatus, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthly.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TERMonthlyMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TERMonthlyMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthly.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TERMonthlyMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TERMonthlyMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTERMonthly entity)
            {
                this.entity = entity;
            }
            public System.String TERMonthlyID
            {
                get
                {
                    System.Int32? data = entity.TERMonthlyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TERMonthlyID = null;
                    else entity.TERMonthlyID = Convert.ToInt32(value);
                }
            }
            public System.String ValidFrom
            {
                get
                {
                    System.DateTime? data = entity.ValidFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidFrom = null;
                    else entity.ValidFrom = Convert.ToDateTime(value);
                }
            }
            public System.String SRTaxStatus
            {
                get
                {
                    System.String data = entity.SRTaxStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTaxStatus = null;
                    else entity.SRTaxStatus = Convert.ToString(value);
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
            private esTERMonthly entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTERMonthlyQuery query)
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
                throw new Exception("esTERMonthly can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TERMonthly : esTERMonthly
    {
    }

    [Serializable]
    abstract public class esTERMonthlyQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TERMonthlyMetadata.Meta();
            }
        }

        public esQueryItem TERMonthlyID
        {
            get
            {
                return new esQueryItem(this, TERMonthlyMetadata.ColumnNames.TERMonthlyID, esSystemType.Int32);
            }
        }

        public esQueryItem ValidFrom
        {
            get
            {
                return new esQueryItem(this, TERMonthlyMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
            }
        }

        public esQueryItem SRTaxStatus
        {
            get
            {
                return new esQueryItem(this, TERMonthlyMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TERMonthlyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TERMonthlyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TERMonthlyCollection")]
    public partial class TERMonthlyCollection : esTERMonthlyCollection, IEnumerable<TERMonthly>
    {
        public TERMonthlyCollection()
        {

        }

        public static implicit operator List<TERMonthly>(TERMonthlyCollection coll)
        {
            List<TERMonthly> list = new List<TERMonthly>();

            foreach (TERMonthly emp in coll)
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
                return TERMonthlyMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TERMonthlyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TERMonthly(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TERMonthly();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TERMonthlyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TERMonthlyQuery();
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
        public bool Load(TERMonthlyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TERMonthly AddNew()
        {
            TERMonthly entity = base.AddNewEntity() as TERMonthly;

            return entity;
        }
        public TERMonthly FindByPrimaryKey(Int32 tERMonthlyID)
        {
            return base.FindByPrimaryKey(tERMonthlyID) as TERMonthly;
        }

        #region IEnumerable< TERMonthly> Members

        IEnumerator<TERMonthly> IEnumerable<TERMonthly>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TERMonthly;
            }
        }

        #endregion

        private TERMonthlyQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TERMonthly' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TERMonthly ({TERMonthlyID})")]
    [Serializable]
    public partial class TERMonthly : esTERMonthly
    {
        public TERMonthly()
        {
        }

        public TERMonthly(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TERMonthlyMetadata.Meta();
            }
        }

        override protected esTERMonthlyQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TERMonthlyQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TERMonthlyQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TERMonthlyQuery();
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
        public bool Load(TERMonthlyQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TERMonthlyQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TERMonthlyQuery : esTERMonthlyQuery
    {
        public TERMonthlyQuery()
        {

        }

        public TERMonthlyQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TERMonthlyQuery";
        }
    }

    [Serializable]
    public partial class TERMonthlyMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TERMonthlyMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TERMonthlyMetadata.ColumnNames.TERMonthlyID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TERMonthlyMetadata.PropertyNames.TERMonthlyID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyMetadata.ColumnNames.ValidFrom, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TERMonthlyMetadata.PropertyNames.ValidFrom;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyMetadata.ColumnNames.SRTaxStatus, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TERMonthlyMetadata.PropertyNames.SRTaxStatus;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TERMonthlyMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TERMonthlyMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TERMonthlyMetadata Meta()
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
            public const string TERMonthlyID = "TERMonthlyID";
            public const string ValidFrom = "ValidFrom";
            public const string SRTaxStatus = "SRTaxStatus";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TERMonthlyID = "TERMonthlyID";
            public const string ValidFrom = "ValidFrom";
            public const string SRTaxStatus = "SRTaxStatus";
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
            lock (typeof(TERMonthlyMetadata))
            {
                if (TERMonthlyMetadata.mapDelegates == null)
                {
                    TERMonthlyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TERMonthlyMetadata.meta == null)
                {
                    TERMonthlyMetadata.meta = new TERMonthlyMetadata();
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

                meta.AddTypeMap("TERMonthlyID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TERMonthly";
                meta.Destination = "TERMonthly";
                meta.spInsert = "proc_TERMonthlyInsert";
                meta.spUpdate = "proc_TERMonthlyUpdate";
                meta.spDelete = "proc_TERMonthlyDelete";
                meta.spLoadAll = "proc_TERMonthlyLoadAll";
                meta.spLoadByPrimaryKey = "proc_TERMonthlyLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TERMonthlyMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
