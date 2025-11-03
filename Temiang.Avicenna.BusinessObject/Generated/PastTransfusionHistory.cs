/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/12/2018 6:40:32 PM
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
    abstract public class esPastTransfusionHistoryCollection : esEntityCollectionWAuditLog
    {
        public esPastTransfusionHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PastTransfusionHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esPastTransfusionHistoryQuery query)
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
            this.InitQuery(query as esPastTransfusionHistoryQuery);
        }
        #endregion

        virtual public PastTransfusionHistory DetachEntity(PastTransfusionHistory entity)
        {
            return base.DetachEntity(entity) as PastTransfusionHistory;
        }

        virtual public PastTransfusionHistory AttachEntity(PastTransfusionHistory entity)
        {
            return base.AttachEntity(entity) as PastTransfusionHistory;
        }

        virtual public void Combine(PastTransfusionHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public PastTransfusionHistory this[int index]
        {
            get
            {
                return base[index] as PastTransfusionHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PastTransfusionHistory);
        }
    }

    [Serializable]
    abstract public class esPastTransfusionHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPastTransfusionHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esPastTransfusionHistory()
        {
        }

        public esPastTransfusionHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID)
        {
            esPastTransfusionHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
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
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "Year": this.str.Year = (string)value; break;
                        case "AllergicReaction": this.str.AllergicReaction = (string)value; break;
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
        /// Maps to PastTransfusionHistory.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PastTransfusionHistoryMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PastTransfusionHistoryMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PastTransfusionHistory.Year
        /// </summary>
        virtual public System.String Year
        {
            get
            {
                return base.GetSystemString(PastTransfusionHistoryMetadata.ColumnNames.Year);
            }

            set
            {
                base.SetSystemString(PastTransfusionHistoryMetadata.ColumnNames.Year, value);
            }
        }
        /// <summary>
        /// Maps to PastTransfusionHistory.AllergicReaction
        /// </summary>
        virtual public System.String AllergicReaction
        {
            get
            {
                return base.GetSystemString(PastTransfusionHistoryMetadata.ColumnNames.AllergicReaction);
            }

            set
            {
                base.SetSystemString(PastTransfusionHistoryMetadata.ColumnNames.AllergicReaction, value);
            }
        }
        /// <summary>
        /// Maps to PastTransfusionHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PastTransfusionHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPastTransfusionHistory entity)
            {
                this.entity = entity;
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String Year
            {
                get
                {
                    System.String data = entity.Year;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Year = null;
                    else entity.Year = Convert.ToString(value);
                }
            }
            public System.String AllergicReaction
            {
                get
                {
                    System.String data = entity.AllergicReaction;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AllergicReaction = null;
                    else entity.AllergicReaction = Convert.ToString(value);
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
            private esPastTransfusionHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPastTransfusionHistoryQuery query)
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
                throw new Exception("esPastTransfusionHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PastTransfusionHistory : esPastTransfusionHistory
    {
    }

    [Serializable]
    abstract public class esPastTransfusionHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PastTransfusionHistoryMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PastTransfusionHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem Year
        {
            get
            {
                return new esQueryItem(this, PastTransfusionHistoryMetadata.ColumnNames.Year, esSystemType.String);
            }
        }

        public esQueryItem AllergicReaction
        {
            get
            {
                return new esQueryItem(this, PastTransfusionHistoryMetadata.ColumnNames.AllergicReaction, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PastTransfusionHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PastTransfusionHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PastTransfusionHistoryCollection")]
    public partial class PastTransfusionHistoryCollection : esPastTransfusionHistoryCollection, IEnumerable<PastTransfusionHistory>
    {
        public PastTransfusionHistoryCollection()
        {

        }

        public static implicit operator List<PastTransfusionHistory>(PastTransfusionHistoryCollection coll)
        {
            List<PastTransfusionHistory> list = new List<PastTransfusionHistory>();

            foreach (PastTransfusionHistory emp in coll)
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
                return PastTransfusionHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PastTransfusionHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PastTransfusionHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PastTransfusionHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PastTransfusionHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PastTransfusionHistoryQuery();
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
        public bool Load(PastTransfusionHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PastTransfusionHistory AddNew()
        {
            PastTransfusionHistory entity = base.AddNewEntity() as PastTransfusionHistory;

            return entity;
        }
        public PastTransfusionHistory FindByPrimaryKey(String patientID)
        {
            return base.FindByPrimaryKey(patientID) as PastTransfusionHistory;
        }

        #region IEnumerable< PastTransfusionHistory> Members

        IEnumerator<PastTransfusionHistory> IEnumerable<PastTransfusionHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PastTransfusionHistory;
            }
        }

        #endregion

        private PastTransfusionHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PastTransfusionHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PastTransfusionHistory ({PatientID})")]
    [Serializable]
    public partial class PastTransfusionHistory : esPastTransfusionHistory
    {
        public PastTransfusionHistory()
        {
        }

        public PastTransfusionHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PastTransfusionHistoryMetadata.Meta();
            }
        }

        override protected esPastTransfusionHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PastTransfusionHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PastTransfusionHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PastTransfusionHistoryQuery();
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
        public bool Load(PastTransfusionHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PastTransfusionHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PastTransfusionHistoryQuery : esPastTransfusionHistoryQuery
    {
        public PastTransfusionHistoryQuery()
        {

        }

        public PastTransfusionHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PastTransfusionHistoryQuery";
        }
    }

    [Serializable]
    public partial class PastTransfusionHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PastTransfusionHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PastTransfusionHistoryMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PastTransfusionHistoryMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PastTransfusionHistoryMetadata.ColumnNames.Year, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PastTransfusionHistoryMetadata.PropertyNames.Year;
            c.CharacterMaxLength = 4;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PastTransfusionHistoryMetadata.ColumnNames.AllergicReaction, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PastTransfusionHistoryMetadata.PropertyNames.AllergicReaction;
            c.CharacterMaxLength = 1500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PastTransfusionHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PastTransfusionHistoryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PastTransfusionHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PastTransfusionHistoryMetadata Meta()
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
            public const string PatientID = "PatientID";
            public const string Year = "Year";
            public const string AllergicReaction = "AllergicReaction";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string Year = "Year";
            public const string AllergicReaction = "AllergicReaction";
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
            lock (typeof(PastTransfusionHistoryMetadata))
            {
                if (PastTransfusionHistoryMetadata.mapDelegates == null)
                {
                    PastTransfusionHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PastTransfusionHistoryMetadata.meta == null)
                {
                    PastTransfusionHistoryMetadata.meta = new PastTransfusionHistoryMetadata();
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

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AllergicReaction", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PastTransfusionHistory";
                meta.Destination = "PastTransfusionHistory";
                meta.spInsert = "proc_PastTransfusionHistoryInsert";
                meta.spUpdate = "proc_PastTransfusionHistoryUpdate";
                meta.spDelete = "proc_PastTransfusionHistoryDelete";
                meta.spLoadAll = "proc_PastTransfusionHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_PastTransfusionHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PastTransfusionHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
