/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/23/2018 10:58:46 AM
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
    abstract public class esBillTransferHistoryCollection : esEntityCollectionWAuditLog
    {
        public esBillTransferHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BillTransferHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esBillTransferHistoryQuery query)
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
            this.InitQuery(query as esBillTransferHistoryQuery);
        }
        #endregion

        virtual public BillTransferHistory DetachEntity(BillTransferHistory entity)
        {
            return base.DetachEntity(entity) as BillTransferHistory;
        }

        virtual public BillTransferHistory AttachEntity(BillTransferHistory entity)
        {
            return base.AttachEntity(entity) as BillTransferHistory;
        }

        virtual public void Combine(BillTransferHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public BillTransferHistory this[int index]
        {
            get
            {
                return base[index] as BillTransferHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BillTransferHistory);
        }
    }

    [Serializable]
    abstract public class esBillTransferHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBillTransferHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esBillTransferHistory()
        {
        }

        public esBillTransferHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, DateTime processDateTime, String processByUserID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, processDateTime, processByUserID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, processDateTime, processByUserID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, DateTime processDateTime, String processByUserID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, processDateTime, processByUserID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, processDateTime, processByUserID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, DateTime processDateTime, String processByUserID)
        {
            esBillTransferHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.ProcessDateTime == processDateTime, query.ProcessByUserID == processByUserID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, DateTime processDateTime, String processByUserID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("ProcessDateTime", processDateTime);
            parms.Add("ProcessByUserID", processByUserID);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ProcessDateTime": this.str.ProcessDateTime = (string)value; break;
                        case "ProcessByUserID": this.str.ProcessByUserID = (string)value; break;
                        case "IsPatientToGuarantor": this.str.IsPatientToGuarantor = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ProcessDateTime":

                            if (value == null || value is System.DateTime)
                                this.ProcessDateTime = (System.DateTime?)value;
                            break;
                        case "IsPatientToGuarantor":

                            if (value == null || value is System.Boolean)
                                this.IsPatientToGuarantor = (System.Boolean?)value;
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
        /// Maps to BillTransferHistory.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BillTransferHistoryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BillTransferHistoryMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BillTransferHistory.ProcessDateTime
        /// </summary>
        virtual public System.DateTime? ProcessDateTime
        {
            get
            {
                return base.GetSystemDateTime(BillTransferHistoryMetadata.ColumnNames.ProcessDateTime);
            }

            set
            {
                base.SetSystemDateTime(BillTransferHistoryMetadata.ColumnNames.ProcessDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BillTransferHistory.ProcessByUserID
        /// </summary>
        virtual public System.String ProcessByUserID
        {
            get
            {
                return base.GetSystemString(BillTransferHistoryMetadata.ColumnNames.ProcessByUserID);
            }

            set
            {
                base.SetSystemString(BillTransferHistoryMetadata.ColumnNames.ProcessByUserID, value);
            }
        }
        /// <summary>
        /// Maps to BillTransferHistory.IsPatientToGuarantor
        /// </summary>
        virtual public System.Boolean? IsPatientToGuarantor
        {
            get
            {
                return base.GetSystemBoolean(BillTransferHistoryMetadata.ColumnNames.IsPatientToGuarantor);
            }

            set
            {
                base.SetSystemBoolean(BillTransferHistoryMetadata.ColumnNames.IsPatientToGuarantor, value);
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
            public esStrings(esBillTransferHistory entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String ProcessDateTime
            {
                get
                {
                    System.DateTime? data = entity.ProcessDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcessDateTime = null;
                    else entity.ProcessDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ProcessByUserID
            {
                get
                {
                    System.String data = entity.ProcessByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcessByUserID = null;
                    else entity.ProcessByUserID = Convert.ToString(value);
                }
            }
            public System.String IsPatientToGuarantor
            {
                get
                {
                    System.Boolean? data = entity.IsPatientToGuarantor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPatientToGuarantor = null;
                    else entity.IsPatientToGuarantor = Convert.ToBoolean(value);
                }
            }
            private esBillTransferHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBillTransferHistoryQuery query)
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
                throw new Exception("esBillTransferHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BillTransferHistory : esBillTransferHistory
    {
    }

    [Serializable]
    abstract public class esBillTransferHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BillTransferHistoryMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BillTransferHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ProcessDateTime
        {
            get
            {
                return new esQueryItem(this, BillTransferHistoryMetadata.ColumnNames.ProcessDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ProcessByUserID
        {
            get
            {
                return new esQueryItem(this, BillTransferHistoryMetadata.ColumnNames.ProcessByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsPatientToGuarantor
        {
            get
            {
                return new esQueryItem(this, BillTransferHistoryMetadata.ColumnNames.IsPatientToGuarantor, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BillTransferHistoryCollection")]
    public partial class BillTransferHistoryCollection : esBillTransferHistoryCollection, IEnumerable<BillTransferHistory>
    {
        public BillTransferHistoryCollection()
        {

        }

        public static implicit operator List<BillTransferHistory>(BillTransferHistoryCollection coll)
        {
            List<BillTransferHistory> list = new List<BillTransferHistory>();

            foreach (BillTransferHistory emp in coll)
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
                return BillTransferHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BillTransferHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BillTransferHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BillTransferHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BillTransferHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BillTransferHistoryQuery();
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
        public bool Load(BillTransferHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BillTransferHistory AddNew()
        {
            BillTransferHistory entity = base.AddNewEntity() as BillTransferHistory;

            return entity;
        }
        public BillTransferHistory FindByPrimaryKey(String registrationNo, DateTime processDateTime, String processByUserID)
        {
            return base.FindByPrimaryKey(registrationNo, processDateTime, processByUserID) as BillTransferHistory;
        }

        #region IEnumerable< BillTransferHistory> Members

        IEnumerator<BillTransferHistory> IEnumerable<BillTransferHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BillTransferHistory;
            }
        }

        #endregion

        private BillTransferHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BillTransferHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BillTransferHistory ({RegistrationNo, ProcessDateTime, ProcessByUserID})")]
    [Serializable]
    public partial class BillTransferHistory : esBillTransferHistory
    {
        public BillTransferHistory()
        {
        }

        public BillTransferHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BillTransferHistoryMetadata.Meta();
            }
        }

        override protected esBillTransferHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BillTransferHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BillTransferHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BillTransferHistoryQuery();
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
        public bool Load(BillTransferHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BillTransferHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BillTransferHistoryQuery : esBillTransferHistoryQuery
    {
        public BillTransferHistoryQuery()
        {

        }

        public BillTransferHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BillTransferHistoryQuery";
        }
    }

    [Serializable]
    public partial class BillTransferHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BillTransferHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BillTransferHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BillTransferHistoryMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BillTransferHistoryMetadata.ColumnNames.ProcessDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillTransferHistoryMetadata.PropertyNames.ProcessDateTime;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillTransferHistoryMetadata.ColumnNames.ProcessByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BillTransferHistoryMetadata.PropertyNames.ProcessByUserID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(BillTransferHistoryMetadata.ColumnNames.IsPatientToGuarantor, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BillTransferHistoryMetadata.PropertyNames.IsPatientToGuarantor;
            _columns.Add(c);


        }
        #endregion

        static public BillTransferHistoryMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string ProcessDateTime = "ProcessDateTime";
            public const string ProcessByUserID = "ProcessByUserID";
            public const string IsPatientToGuarantor = "IsPatientToGuarantor";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string ProcessDateTime = "ProcessDateTime";
            public const string ProcessByUserID = "ProcessByUserID";
            public const string IsPatientToGuarantor = "IsPatientToGuarantor";
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
            lock (typeof(BillTransferHistoryMetadata))
            {
                if (BillTransferHistoryMetadata.mapDelegates == null)
                {
                    BillTransferHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BillTransferHistoryMetadata.meta == null)
                {
                    BillTransferHistoryMetadata.meta = new BillTransferHistoryMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcessDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ProcessByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPatientToGuarantor", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "BillTransferHistory";
                meta.Destination = "BillTransferHistory";
                meta.spInsert = "proc_BillTransferHistoryInsert";
                meta.spUpdate = "proc_BillTransferHistoryUpdate";
                meta.spDelete = "proc_BillTransferHistoryDelete";
                meta.spLoadAll = "proc_BillTransferHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_BillTransferHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BillTransferHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
