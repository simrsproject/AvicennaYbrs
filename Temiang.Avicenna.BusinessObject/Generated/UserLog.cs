/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/4/2016 1:06:56 PM
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
    abstract public class esUserLogCollection : esEntityCollectionWAuditLog
    {
        public esUserLogCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "UserLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esUserLogQuery query)
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
            this.InitQuery(query as esUserLogQuery);
        }
        #endregion

        virtual public UserLog DetachEntity(UserLog entity)
        {
            return base.DetachEntity(entity) as UserLog;
        }

        virtual public UserLog AttachEntity(UserLog entity)
        {
            return base.AttachEntity(entity) as UserLog;
        }

        virtual public void Combine(UserLogCollection collection)
        {
            base.Combine(collection);
        }

        new public UserLog this[int index]
        {
            get
            {
                return base[index] as UserLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(UserLog);
        }
    }

    [Serializable]
    abstract public class esUserLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esUserLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esUserLog()
        {
        }

        public esUserLog(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 userLogID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(userLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(userLogID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 userLogID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(userLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(userLogID);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 userLogID)
        {
            esUserLogQuery query = this.GetDynamicQuery();
            query.Where(query.UserLogID == userLogID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 userLogID)
        {
            esParameters parms = new esParameters();
            parms.Add("UserLogID", userLogID);
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
                        case "UserLogID": this.str.UserLogID = (string)value; break;
                        case "ApplicationID": this.str.ApplicationID = (string)value; break;
                        case "SessionID": this.str.SessionID = (string)value; break;
                        case "UserID": this.str.UserID = (string)value; break;
                        case "LoginDateTime": this.str.LoginDateTime = (string)value; break;
                        case "ClientIP": this.str.ClientIP = (string)value; break;
                        case "BrowserInfo": this.str.BrowserInfo = (string)value; break;
                        case "LogoutDateTime": this.str.LogoutDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "UserLogID":

                            if (value == null || value is System.Int64)
                                this.UserLogID = (System.Int64?)value;
                            break;
                        case "LoginDateTime":

                            if (value == null || value is System.DateTime)
                                this.LoginDateTime = (System.DateTime?)value;
                            break;
                        case "LogoutDateTime":

                            if (value == null || value is System.DateTime)
                                this.LogoutDateTime = (System.DateTime?)value;
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
        /// Maps to UserLog.UserLogID
        /// </summary>
        virtual public System.Int64? UserLogID
        {
            get
            {
                return base.GetSystemInt64(UserLogMetadata.ColumnNames.UserLogID);
            }

            set
            {
                base.SetSystemInt64(UserLogMetadata.ColumnNames.UserLogID, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.ApplicationID
        /// </summary>
        virtual public System.String ApplicationID
        {
            get
            {
                return base.GetSystemString(UserLogMetadata.ColumnNames.ApplicationID);
            }

            set
            {
                base.SetSystemString(UserLogMetadata.ColumnNames.ApplicationID, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.SessionID
        /// </summary>
        virtual public System.String SessionID
        {
            get
            {
                return base.GetSystemString(UserLogMetadata.ColumnNames.SessionID);
            }

            set
            {
                base.SetSystemString(UserLogMetadata.ColumnNames.SessionID, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(UserLogMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(UserLogMetadata.ColumnNames.UserID, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.LoginDateTime
        /// </summary>
        virtual public System.DateTime? LoginDateTime
        {
            get
            {
                return base.GetSystemDateTime(UserLogMetadata.ColumnNames.LoginDateTime);
            }

            set
            {
                base.SetSystemDateTime(UserLogMetadata.ColumnNames.LoginDateTime, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.ClientIP
        /// </summary>
        virtual public System.String ClientIP
        {
            get
            {
                return base.GetSystemString(UserLogMetadata.ColumnNames.ClientIP);
            }

            set
            {
                base.SetSystemString(UserLogMetadata.ColumnNames.ClientIP, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.BrowserInfo
        /// </summary>
        virtual public System.String BrowserInfo
        {
            get
            {
                return base.GetSystemString(UserLogMetadata.ColumnNames.BrowserInfo);
            }

            set
            {
                base.SetSystemString(UserLogMetadata.ColumnNames.BrowserInfo, value);
            }
        }
        /// <summary>
        /// Maps to UserLog.LogoutDateTime
        /// </summary>
        virtual public System.DateTime? LogoutDateTime
        {
            get
            {
                return base.GetSystemDateTime(UserLogMetadata.ColumnNames.LogoutDateTime);
            }

            set
            {
                base.SetSystemDateTime(UserLogMetadata.ColumnNames.LogoutDateTime, value);
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
            public esStrings(esUserLog entity)
            {
                this.entity = entity;
            }
            public System.String UserLogID
            {
                get
                {
                    System.Int64? data = entity.UserLogID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UserLogID = null;
                    else entity.UserLogID = Convert.ToInt64(value);
                }
            }
            public System.String ApplicationID
            {
                get
                {
                    System.String data = entity.ApplicationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApplicationID = null;
                    else entity.ApplicationID = Convert.ToString(value);
                }
            }
            public System.String SessionID
            {
                get
                {
                    System.String data = entity.SessionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SessionID = null;
                    else entity.SessionID = Convert.ToString(value);
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
            public System.String LoginDateTime
            {
                get
                {
                    System.DateTime? data = entity.LoginDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LoginDateTime = null;
                    else entity.LoginDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ClientIP
            {
                get
                {
                    System.String data = entity.ClientIP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClientIP = null;
                    else entity.ClientIP = Convert.ToString(value);
                }
            }
            public System.String BrowserInfo
            {
                get
                {
                    System.String data = entity.BrowserInfo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BrowserInfo = null;
                    else entity.BrowserInfo = Convert.ToString(value);
                }
            }
            public System.String LogoutDateTime
            {
                get
                {
                    System.DateTime? data = entity.LogoutDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LogoutDateTime = null;
                    else entity.LogoutDateTime = Convert.ToDateTime(value);
                }
            }
            private esUserLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esUserLogQuery query)
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
                throw new Exception("esUserLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class UserLog : esUserLog
    {
    }

    [Serializable]
    abstract public class esUserLogQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return UserLogMetadata.Meta();
            }
        }

        public esQueryItem UserLogID
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.UserLogID, esSystemType.Int64);
            }
        }

        public esQueryItem ApplicationID
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.ApplicationID, esSystemType.String);
            }
        }

        public esQueryItem SessionID
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.SessionID, esSystemType.String);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem LoginDateTime
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.LoginDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ClientIP
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.ClientIP, esSystemType.String);
            }
        }

        public esQueryItem BrowserInfo
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.BrowserInfo, esSystemType.String);
            }
        }

        public esQueryItem LogoutDateTime
        {
            get
            {
                return new esQueryItem(this, UserLogMetadata.ColumnNames.LogoutDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("UserLogCollection")]
    public partial class UserLogCollection : esUserLogCollection, IEnumerable<UserLog>
    {
        public UserLogCollection()
        {

        }

        public static implicit operator List<UserLog>(UserLogCollection coll)
        {
            List<UserLog> list = new List<UserLog>();

            foreach (UserLog emp in coll)
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
                return UserLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new UserLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new UserLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new UserLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public UserLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new UserLogQuery();
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
        public bool Load(UserLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public UserLog AddNew()
        {
            UserLog entity = base.AddNewEntity() as UserLog;

            return entity;
        }
        public UserLog FindByPrimaryKey(Int64 userLogID)
        {
            return base.FindByPrimaryKey(userLogID) as UserLog;
        }

        #region IEnumerable< UserLog> Members

        IEnumerator<UserLog> IEnumerable<UserLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as UserLog;
            }
        }

        #endregion

        private UserLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'UserLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("UserLog ({UserLogID})")]
    [Serializable]
    public partial class UserLog : esUserLog
    {
        public UserLog()
        {
        }

        public UserLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return UserLogMetadata.Meta();
            }
        }

        override protected esUserLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new UserLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public UserLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new UserLogQuery();
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
        public bool Load(UserLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private UserLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class UserLogQuery : esUserLogQuery
    {
        public UserLogQuery()
        {

        }

        public UserLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "UserLogQuery";
        }
    }

    [Serializable]
    public partial class UserLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected UserLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.UserLogID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = UserLogMetadata.PropertyNames.UserLogID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.ApplicationID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = UserLogMetadata.PropertyNames.ApplicationID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.SessionID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = UserLogMetadata.PropertyNames.SessionID;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.UserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = UserLogMetadata.PropertyNames.UserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.LoginDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = UserLogMetadata.PropertyNames.LoginDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.ClientIP, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = UserLogMetadata.PropertyNames.ClientIP;
            c.CharacterMaxLength = 11;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.BrowserInfo, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = UserLogMetadata.PropertyNames.BrowserInfo;
            c.CharacterMaxLength = 2000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(UserLogMetadata.ColumnNames.LogoutDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = UserLogMetadata.PropertyNames.LogoutDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public UserLogMetadata Meta()
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
            public const string UserLogID = "UserLogID";
            public const string ApplicationID = "ApplicationID";
            public const string SessionID = "SessionID";
            public const string UserID = "UserID";
            public const string LoginDateTime = "LoginDateTime";
            public const string ClientIP = "ClientIP";
            public const string BrowserInfo = "BrowserInfo";
            public const string LogoutDateTime = "LogoutDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string UserLogID = "UserLogID";
            public const string ApplicationID = "ApplicationID";
            public const string SessionID = "SessionID";
            public const string UserID = "UserID";
            public const string LoginDateTime = "LoginDateTime";
            public const string ClientIP = "ClientIP";
            public const string BrowserInfo = "BrowserInfo";
            public const string LogoutDateTime = "LogoutDateTime";
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
            lock (typeof(UserLogMetadata))
            {
                if (UserLogMetadata.mapDelegates == null)
                {
                    UserLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (UserLogMetadata.meta == null)
                {
                    UserLogMetadata.meta = new UserLogMetadata();
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

                meta.AddTypeMap("UserLogID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("ApplicationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SessionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LoginDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ClientIP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BrowserInfo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LogoutDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "UserLog";
                meta.Destination = "UserLog";
                meta.spInsert = "proc_UserLogInsert";
                meta.spUpdate = "proc_UserLogUpdate";
                meta.spDelete = "proc_UserLogDelete";
                meta.spLoadAll = "proc_UserLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_UserLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private UserLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
