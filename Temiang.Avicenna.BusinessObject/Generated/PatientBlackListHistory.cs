/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2018 10:30:10 AM
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
    abstract public class esPatientBlackListHistoryCollection : esEntityCollectionWAuditLog
    {
        public esPatientBlackListHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientBlackListHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientBlackListHistoryQuery query)
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
            this.InitQuery(query as esPatientBlackListHistoryQuery);
        }
        #endregion

        virtual public PatientBlackListHistory DetachEntity(PatientBlackListHistory entity)
        {
            return base.DetachEntity(entity) as PatientBlackListHistory;
        }

        virtual public PatientBlackListHistory AttachEntity(PatientBlackListHistory entity)
        {
            return base.AttachEntity(entity) as PatientBlackListHistory;
        }

        virtual public void Combine(PatientBlackListHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientBlackListHistory this[int index]
        {
            get
            {
                return base[index] as PatientBlackListHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientBlackListHistory);
        }
    }

    [Serializable]
    abstract public class esPatientBlackListHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientBlackListHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientBlackListHistory()
        {
        }

        public esPatientBlackListHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID, Boolean isBlackList, DateTime lastUpdateDateTime)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, isBlackList, lastUpdateDateTime);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, isBlackList, lastUpdateDateTime);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, Boolean isBlackList, DateTime lastUpdateDateTime)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, isBlackList, lastUpdateDateTime);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, isBlackList, lastUpdateDateTime);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID, Boolean isBlackList, DateTime lastUpdateDateTime)
        {
            esPatientBlackListHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID, query.IsBlackList == isBlackList, query.LastUpdateDateTime == lastUpdateDateTime);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID, Boolean isBlackList, DateTime lastUpdateDateTime)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
            parms.Add("IsBlackList", isBlackList);
            parms.Add("LastUpdateDateTime", lastUpdateDateTime);
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
                        case "IsBlackList": this.str.IsBlackList = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsBlackList":

                            if (value == null || value is System.Boolean)
                                this.IsBlackList = (System.Boolean?)value;
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
        /// Maps to PatientBlackListHistory.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientBlackListHistoryMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientBlackListHistoryMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientBlackListHistory.IsBlackList
        /// </summary>
        virtual public System.Boolean? IsBlackList
        {
            get
            {
                return base.GetSystemBoolean(PatientBlackListHistoryMetadata.ColumnNames.IsBlackList);
            }

            set
            {
                base.SetSystemBoolean(PatientBlackListHistoryMetadata.ColumnNames.IsBlackList, value);
            }
        }
        /// <summary>
        /// Maps to PatientBlackListHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientBlackListHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientBlackListHistory.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientBlackListHistoryMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientBlackListHistoryMetadata.ColumnNames.Notes, value);
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
            public esStrings(esPatientBlackListHistory entity)
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
            public System.String IsBlackList
            {
                get
                {
                    System.Boolean? data = entity.IsBlackList;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsBlackList = null;
                    else entity.IsBlackList = Convert.ToBoolean(value);
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
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }
            private esPatientBlackListHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientBlackListHistoryQuery query)
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
                throw new Exception("esPatientBlackListHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientBlackListHistory : esPatientBlackListHistory
    {
    }

    [Serializable]
    abstract public class esPatientBlackListHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientBlackListHistoryMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientBlackListHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem IsBlackList
        {
            get
            {
                return new esQueryItem(this, PatientBlackListHistoryMetadata.ColumnNames.IsBlackList, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientBlackListHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientBlackListHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientBlackListHistoryMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientBlackListHistoryCollection")]
    public partial class PatientBlackListHistoryCollection : esPatientBlackListHistoryCollection, IEnumerable<PatientBlackListHistory>
    {
        public PatientBlackListHistoryCollection()
        {

        }

        public static implicit operator List<PatientBlackListHistory>(PatientBlackListHistoryCollection coll)
        {
            List<PatientBlackListHistory> list = new List<PatientBlackListHistory>();

            foreach (PatientBlackListHistory emp in coll)
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
                return PatientBlackListHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientBlackListHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientBlackListHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientBlackListHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientBlackListHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientBlackListHistoryQuery();
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
        public bool Load(PatientBlackListHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientBlackListHistory AddNew()
        {
            PatientBlackListHistory entity = base.AddNewEntity() as PatientBlackListHistory;

            return entity;
        }
        public PatientBlackListHistory FindByPrimaryKey(String patientID, Boolean isBlackList, DateTime lastUpdateDateTime)
        {
            return base.FindByPrimaryKey(patientID, isBlackList, lastUpdateDateTime) as PatientBlackListHistory;
        }

        #region IEnumerable< PatientBlackListHistory> Members

        IEnumerator<PatientBlackListHistory> IEnumerable<PatientBlackListHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientBlackListHistory;
            }
        }

        #endregion

        private PatientBlackListHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientBlackListHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientBlackListHistory ({PatientID, IsBlackList, LastUpdateDateTime})")]
    [Serializable]
    public partial class PatientBlackListHistory : esPatientBlackListHistory
    {
        public PatientBlackListHistory()
        {
        }

        public PatientBlackListHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientBlackListHistoryMetadata.Meta();
            }
        }

        override protected esPatientBlackListHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientBlackListHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientBlackListHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientBlackListHistoryQuery();
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
        public bool Load(PatientBlackListHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientBlackListHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientBlackListHistoryQuery : esPatientBlackListHistoryQuery
    {
        public PatientBlackListHistoryQuery()
        {

        }

        public PatientBlackListHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientBlackListHistoryQuery";
        }
    }

    [Serializable]
    public partial class PatientBlackListHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientBlackListHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientBlackListHistoryMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientBlackListHistoryMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBlackListHistoryMetadata.ColumnNames.IsBlackList, 1, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientBlackListHistoryMetadata.PropertyNames.IsBlackList;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientBlackListHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBlackListHistoryMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientBlackListHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBlackListHistoryMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientBlackListHistoryMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 1000;
            _columns.Add(c);


        }
        #endregion

        static public PatientBlackListHistoryMetadata Meta()
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
            public const string IsBlackList = "IsBlackList";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string IsBlackList = "IsBlackList";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
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
            lock (typeof(PatientBlackListHistoryMetadata))
            {
                if (PatientBlackListHistoryMetadata.mapDelegates == null)
                {
                    PatientBlackListHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientBlackListHistoryMetadata.meta == null)
                {
                    PatientBlackListHistoryMetadata.meta = new PatientBlackListHistoryMetadata();
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
                meta.AddTypeMap("IsBlackList", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientBlackListHistory";
                meta.Destination = "PatientBlackListHistory";
                meta.spInsert = "proc_PatientBlackListHistoryInsert";
                meta.spUpdate = "proc_PatientBlackListHistoryUpdate";
                meta.spDelete = "proc_PatientBlackListHistoryDelete";
                meta.spLoadAll = "proc_PatientBlackListHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientBlackListHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientBlackListHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
