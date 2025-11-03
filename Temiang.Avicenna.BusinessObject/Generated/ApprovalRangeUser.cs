/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/24/2017 2:39:02 PM
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
    abstract public class esApprovalRangeUserCollection : esEntityCollectionWAuditLog
    {
        public esApprovalRangeUserCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ApprovalRangeUserCollection";
        }

        #region Query Logic
        protected void InitQuery(esApprovalRangeUserQuery query)
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
            this.InitQuery(query as esApprovalRangeUserQuery);
        }
        #endregion

        virtual public ApprovalRangeUser DetachEntity(ApprovalRangeUser entity)
        {
            return base.DetachEntity(entity) as ApprovalRangeUser;
        }

        virtual public ApprovalRangeUser AttachEntity(ApprovalRangeUser entity)
        {
            return base.AttachEntity(entity) as ApprovalRangeUser;
        }

        virtual public void Combine(ApprovalRangeUserCollection collection)
        {
            base.Combine(collection);
        }

        new public ApprovalRangeUser this[int index]
        {
            get
            {
                return base[index] as ApprovalRangeUser;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ApprovalRangeUser);
        }
    }

    [Serializable]
    abstract public class esApprovalRangeUser : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esApprovalRangeUserQuery GetDynamicQuery()
        {
            return null;
        }

        public esApprovalRangeUser()
        {
        }

        public esApprovalRangeUser(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String approvalRangeID, Int32 approvalLevel, String userID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(approvalRangeID, approvalLevel, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(approvalRangeID, approvalLevel, userID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String approvalRangeID, Int32 approvalLevel, String userID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(approvalRangeID, approvalLevel, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(approvalRangeID, approvalLevel, userID);
        }

        private bool LoadByPrimaryKeyDynamic(String approvalRangeID, Int32 approvalLevel, String userID)
        {
            esApprovalRangeUserQuery query = this.GetDynamicQuery();
            query.Where(query.ApprovalRangeID == approvalRangeID, query.ApprovalLevel == approvalLevel, query.UserID == userID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String approvalRangeID, Int32 approvalLevel, String userID)
        {
            esParameters parms = new esParameters();
            parms.Add("ApprovalRangeID", approvalRangeID);
            parms.Add("ApprovalLevel", approvalLevel);
            parms.Add("UserID", userID);
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
                        case "ApprovalRangeID": this.str.ApprovalRangeID = (string)value; break;
                        case "ApprovalLevel": this.str.ApprovalLevel = (string)value; break;
                        case "UserID": this.str.UserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ApprovalLevel":

                            if (value == null || value is System.Int32)
                                this.ApprovalLevel = (System.Int32?)value;
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
        /// Maps to ApprovalRangeUser.ApprovalRangeID
        /// </summary>
        virtual public System.String ApprovalRangeID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeUserMetadata.ColumnNames.ApprovalRangeID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeUserMetadata.ColumnNames.ApprovalRangeID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRangeUser.ApprovalLevel
        /// </summary>
        virtual public System.Int32? ApprovalLevel
        {
            get
            {
                return base.GetSystemInt32(ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel);
            }

            set
            {
                base.SetSystemInt32(ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRangeUser.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeUserMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeUserMetadata.ColumnNames.UserID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRangeUser.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ApprovalRangeUserMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ApprovalRangeUserMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRangeUser.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeUserMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeUserMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esApprovalRangeUser entity)
            {
                this.entity = entity;
            }
            public System.String ApprovalRangeID
            {
                get
                {
                    System.String data = entity.ApprovalRangeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalRangeID = null;
                    else entity.ApprovalRangeID = Convert.ToString(value);
                }
            }
            public System.String ApprovalLevel
            {
                get
                {
                    System.Int32? data = entity.ApprovalLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalLevel = null;
                    else entity.ApprovalLevel = Convert.ToInt32(value);
                }
            }
            public System.String UserID
            {
                get
                {
                    System.String data = entity.UserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UserID = null;
                    else entity.UserID = Convert.ToString(value);
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
            private esApprovalRangeUser entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esApprovalRangeUserQuery query)
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
                throw new Exception("esApprovalRangeUser can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ApprovalRangeUser : esApprovalRangeUser
    {
    }

    [Serializable]
    abstract public class esApprovalRangeUserQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ApprovalRangeUserMetadata.Meta();
            }
        }

        public esQueryItem ApprovalRangeID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeUserMetadata.ColumnNames.ApprovalRangeID, esSystemType.String);
            }
        }

        public esQueryItem ApprovalLevel
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel, esSystemType.Int32);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeUserMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeUserMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeUserMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ApprovalRangeUserCollection")]
    public partial class ApprovalRangeUserCollection : esApprovalRangeUserCollection, IEnumerable<ApprovalRangeUser>
    {
        public ApprovalRangeUserCollection()
        {

        }

        public static implicit operator List<ApprovalRangeUser>(ApprovalRangeUserCollection coll)
        {
            List<ApprovalRangeUser> list = new List<ApprovalRangeUser>();

            foreach (ApprovalRangeUser emp in coll)
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
                return ApprovalRangeUserMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalRangeUserQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ApprovalRangeUser(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ApprovalRangeUser();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ApprovalRangeUserQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalRangeUserQuery();
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
        public bool Load(ApprovalRangeUserQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ApprovalRangeUser AddNew()
        {
            ApprovalRangeUser entity = base.AddNewEntity() as ApprovalRangeUser;

            return entity;
        }
        public ApprovalRangeUser FindByPrimaryKey(String approvalRangeID, Int32 approvalLevel, String userID)
        {
            return base.FindByPrimaryKey(approvalRangeID, approvalLevel, userID) as ApprovalRangeUser;
        }

        #region IEnumerable< ApprovalRangeUser> Members

        IEnumerator<ApprovalRangeUser> IEnumerable<ApprovalRangeUser>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ApprovalRangeUser;
            }
        }

        #endregion

        private ApprovalRangeUserQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ApprovalRangeUser' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ApprovalRangeUser ({ApprovalRangeID, ApprovalLevel, UserID})")]
    [Serializable]
    public partial class ApprovalRangeUser : esApprovalRangeUser
    {
        public ApprovalRangeUser()
        {
        }

        public ApprovalRangeUser(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ApprovalRangeUserMetadata.Meta();
            }
        }

        override protected esApprovalRangeUserQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalRangeUserQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ApprovalRangeUserQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalRangeUserQuery();
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
        public bool Load(ApprovalRangeUserQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ApprovalRangeUserQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ApprovalRangeUserQuery : esApprovalRangeUserQuery
    {
        public ApprovalRangeUserQuery()
        {

        }

        public ApprovalRangeUserQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ApprovalRangeUserQuery";
        }
    }

    [Serializable]
    public partial class ApprovalRangeUserMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ApprovalRangeUserMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ApprovalRangeUserMetadata.ColumnNames.ApprovalRangeID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeUserMetadata.PropertyNames.ApprovalRangeID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeUserMetadata.ColumnNames.ApprovalLevel, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ApprovalRangeUserMetadata.PropertyNames.ApprovalLevel;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeUserMetadata.ColumnNames.UserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeUserMetadata.PropertyNames.UserID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeUserMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ApprovalRangeUserMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeUserMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeUserMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ApprovalRangeUserMetadata Meta()
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
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string ApprovalLevel = "ApprovalLevel";
            public const string UserID = "UserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string ApprovalLevel = "ApprovalLevel";
            public const string UserID = "UserID";
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
            lock (typeof(ApprovalRangeUserMetadata))
            {
                if (ApprovalRangeUserMetadata.mapDelegates == null)
                {
                    ApprovalRangeUserMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ApprovalRangeUserMetadata.meta == null)
                {
                    ApprovalRangeUserMetadata.meta = new ApprovalRangeUserMetadata();
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

                meta.AddTypeMap("ApprovalRangeID", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("ApprovalLevel", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ApprovalRangeUser";
                meta.Destination = "ApprovalRangeUser";
                meta.spInsert = "proc_ApprovalRangeUserInsert";
                meta.spUpdate = "proc_ApprovalRangeUserUpdate";
                meta.spDelete = "proc_ApprovalRangeUserDelete";
                meta.spLoadAll = "proc_ApprovalRangeUserLoadAll";
                meta.spLoadByPrimaryKey = "proc_ApprovalRangeUserLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ApprovalRangeUserMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
