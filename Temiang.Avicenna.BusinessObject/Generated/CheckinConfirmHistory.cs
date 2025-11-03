/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/14/2017 1:58:51 PM
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
    abstract public class esCheckinConfirmHistoryCollection : esEntityCollectionWAuditLog
    {
        public esCheckinConfirmHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CheckinConfirmHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esCheckinConfirmHistoryQuery query)
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
            this.InitQuery(query as esCheckinConfirmHistoryQuery);
        }
        #endregion

        virtual public CheckinConfirmHistory DetachEntity(CheckinConfirmHistory entity)
        {
            return base.DetachEntity(entity) as CheckinConfirmHistory;
        }

        virtual public CheckinConfirmHistory AttachEntity(CheckinConfirmHistory entity)
        {
            return base.AttachEntity(entity) as CheckinConfirmHistory;
        }

        virtual public void Combine(CheckinConfirmHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public CheckinConfirmHistory this[int index]
        {
            get
            {
                return base[index] as CheckinConfirmHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CheckinConfirmHistory);
        }
    }

    [Serializable]
    abstract public class esCheckinConfirmHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCheckinConfirmHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esCheckinConfirmHistory()
        {
        }

        public esCheckinConfirmHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Guid checkinConfirmId)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(checkinConfirmId);
            else
                return LoadByPrimaryKeyStoredProcedure(checkinConfirmId);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Guid checkinConfirmId)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(checkinConfirmId);
            else
                return LoadByPrimaryKeyStoredProcedure(checkinConfirmId);
        }

        private bool LoadByPrimaryKeyDynamic(Guid checkinConfirmId)
        {
            esCheckinConfirmHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.CheckinConfirmId == checkinConfirmId);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Guid checkinConfirmId)
        {
            esParameters parms = new esParameters();
            parms.Add("CheckinConfirmId", checkinConfirmId);
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
                        case "CheckinConfirmId": this.str.CheckinConfirmId = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "TransferNo": this.str.TransferNo = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CheckinConfirmId":

                            if (value == null || value is System.Guid)
                                this.CheckinConfirmId = (System.Guid?)value;
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
        /// Maps to CheckinConfirmHistory.CheckinConfirmId
        /// </summary>
        virtual public System.Guid? CheckinConfirmId
        {
            get
            {
                return base.GetSystemGuid(CheckinConfirmHistoryMetadata.ColumnNames.CheckinConfirmId);
            }

            set
            {
                base.SetSystemGuid(CheckinConfirmHistoryMetadata.ColumnNames.CheckinConfirmId, value);
            }
        }
        /// <summary>
        /// Maps to CheckinConfirmHistory.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to CheckinConfirmHistory.TransferNo
        /// </summary>
        virtual public System.String TransferNo
        {
            get
            {
                return base.GetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.TransferNo);
            }

            set
            {
                base.SetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.TransferNo, value);
            }
        }
        /// <summary>
        /// Maps to CheckinConfirmHistory.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.BedID, value);
            }
        }
        /// <summary>
        /// Maps to CheckinConfirmHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CheckinConfirmHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCheckinConfirmHistory entity)
            {
                this.entity = entity;
            }
            public System.String CheckinConfirmId
            {
                get
                {
                    System.Guid? data = entity.CheckinConfirmId;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CheckinConfirmId = null;
                    else entity.CheckinConfirmId = new Guid(value);
                }
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
            public System.String TransferNo
            {
                get
                {
                    System.String data = entity.TransferNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferNo = null;
                    else entity.TransferNo = Convert.ToString(value);
                }
            }
            public System.String BedID
            {
                get
                {
                    System.String data = entity.BedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedID = null;
                    else entity.BedID = Convert.ToString(value);
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
            private esCheckinConfirmHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCheckinConfirmHistoryQuery query)
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
                throw new Exception("esCheckinConfirmHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CheckinConfirmHistory : esCheckinConfirmHistory
    {
    }

    [Serializable]
    abstract public class esCheckinConfirmHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CheckinConfirmHistoryMetadata.Meta();
            }
        }

        public esQueryItem CheckinConfirmId
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.CheckinConfirmId, esSystemType.Guid);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TransferNo
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.TransferNo, esSystemType.String);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CheckinConfirmHistoryCollection")]
    public partial class CheckinConfirmHistoryCollection : esCheckinConfirmHistoryCollection, IEnumerable<CheckinConfirmHistory>
    {
        public CheckinConfirmHistoryCollection()
        {

        }

        public static implicit operator List<CheckinConfirmHistory>(CheckinConfirmHistoryCollection coll)
        {
            List<CheckinConfirmHistory> list = new List<CheckinConfirmHistory>();

            foreach (CheckinConfirmHistory emp in coll)
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
                return CheckinConfirmHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CheckinConfirmHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CheckinConfirmHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CheckinConfirmHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CheckinConfirmHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CheckinConfirmHistoryQuery();
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
        public bool Load(CheckinConfirmHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CheckinConfirmHistory AddNew()
        {
            CheckinConfirmHistory entity = base.AddNewEntity() as CheckinConfirmHistory;

            return entity;
        }
        public CheckinConfirmHistory FindByPrimaryKey(Guid checkinConfirmId)
        {
            return base.FindByPrimaryKey(checkinConfirmId) as CheckinConfirmHistory;
        }

        #region IEnumerable< CheckinConfirmHistory> Members

        IEnumerator<CheckinConfirmHistory> IEnumerable<CheckinConfirmHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CheckinConfirmHistory;
            }
        }

        #endregion

        private CheckinConfirmHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CheckinConfirmHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CheckinConfirmHistory ({CheckinConfirmId})")]
    [Serializable]
    public partial class CheckinConfirmHistory : esCheckinConfirmHistory
    {
        public CheckinConfirmHistory()
        {
        }

        public CheckinConfirmHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CheckinConfirmHistoryMetadata.Meta();
            }
        }

        override protected esCheckinConfirmHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CheckinConfirmHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CheckinConfirmHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CheckinConfirmHistoryQuery();
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
        public bool Load(CheckinConfirmHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CheckinConfirmHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CheckinConfirmHistoryQuery : esCheckinConfirmHistoryQuery
    {
        public CheckinConfirmHistoryQuery()
        {

        }

        public CheckinConfirmHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CheckinConfirmHistoryQuery";
        }
    }

    [Serializable]
    public partial class CheckinConfirmHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CheckinConfirmHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.CheckinConfirmId, 0, typeof(System.Guid), esSystemType.Guid);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.CheckinConfirmId;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 0;
            c.HasDefault = true;
            c.Default = @"(newid())";
            _columns.Add(c);

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.TransferNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.TransferNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.BedID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CheckinConfirmHistoryMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = CheckinConfirmHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CheckinConfirmHistoryMetadata Meta()
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
            public const string CheckinConfirmId = "CheckinConfirmId";
            public const string RegistrationNo = "RegistrationNo";
            public const string TransferNo = "TransferNo";
            public const string BedID = "BedID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string CheckinConfirmId = "CheckinConfirmId";
            public const string RegistrationNo = "RegistrationNo";
            public const string TransferNo = "TransferNo";
            public const string BedID = "BedID";
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
            lock (typeof(CheckinConfirmHistoryMetadata))
            {
                if (CheckinConfirmHistoryMetadata.mapDelegates == null)
                {
                    CheckinConfirmHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CheckinConfirmHistoryMetadata.meta == null)
                {
                    CheckinConfirmHistoryMetadata.meta = new CheckinConfirmHistoryMetadata();
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

                meta.AddTypeMap("CheckinConfirmId", new esTypeMap("uniqueidentifier", "System.Guid"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransferNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CheckinConfirmHistory";
                meta.Destination = "CheckinConfirmHistory";
                meta.spInsert = "proc_CheckinConfirmHistoryInsert";
                meta.spUpdate = "proc_CheckinConfirmHistoryUpdate";
                meta.spDelete = "proc_CheckinConfirmHistoryDelete";
                meta.spLoadAll = "proc_CheckinConfirmHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_CheckinConfirmHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CheckinConfirmHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
