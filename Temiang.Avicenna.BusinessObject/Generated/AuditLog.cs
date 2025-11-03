/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 22/11/2024 21:15:43
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
    abstract public class esAuditLogCollection : esEntityCollectionWAuditLog
    {
        public esAuditLogCollection()
        {

        }
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "AuditLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esAuditLogQuery query)
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
            this.InitQuery(query as esAuditLogQuery);
        }
        #endregion

        virtual public AuditLog DetachEntity(AuditLog entity)
        {
            return base.DetachEntity(entity) as AuditLog;
        }

        virtual public AuditLog AttachEntity(AuditLog entity)
        {
            return base.AttachEntity(entity) as AuditLog;
        }

        virtual public void Combine(AuditLogCollection collection)
        {
            base.Combine(collection);
        }

        new public AuditLog this[int index]
        {
            get
            {
                return base[index] as AuditLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AuditLog);
        }
    }

    [Serializable]
    abstract public class esAuditLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAuditLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esAuditLog()
        {
        }

        public esAuditLog(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 auditLogID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(auditLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(auditLogID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 auditLogID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(auditLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(auditLogID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 auditLogID)
        {
            esAuditLogQuery query = this.GetDynamicQuery();
            query.Where(query.AuditLogID == auditLogID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 auditLogID)
        {
            esParameters parms = new esParameters();
            parms.Add("AuditLogID", auditLogID);
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
                        case "TableName": this.str.TableName = (string)value; break;
                        case "AuditActionType": this.str.AuditActionType = (string)value; break;
                        case "PrimaryKeyData": this.str.PrimaryKeyData = (string)value; break;
                        case "ActionByUserID": this.str.ActionByUserID = (string)value; break;
                        case "LogDateTime": this.str.LogDateTime = (string)value; break;
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
                        case "LogDateTime":

                            if (value == null || value is System.DateTime)
                                this.LogDateTime = (System.DateTime?)value;
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
        /// Maps to AuditLog.AuditLogID
        /// </summary>
        virtual public System.Int32? AuditLogID
        {
            get
            {
                return base.GetSystemInt32(AuditLogMetadata.ColumnNames.AuditLogID);
            }

            set
            {
                base.SetSystemInt32(AuditLogMetadata.ColumnNames.AuditLogID, value);
            }
        }
        /// <summary>
        /// Maps to AuditLog.TableName
        /// </summary>
        virtual public System.String TableName
        {
            get
            {
                return base.GetSystemString(AuditLogMetadata.ColumnNames.TableName);
            }

            set
            {
                base.SetSystemString(AuditLogMetadata.ColumnNames.TableName, value);
            }
        }
        /// <summary>
        /// Maps to AuditLog.AuditActionType
        /// </summary>
        virtual public System.String AuditActionType
        {
            get
            {
                return base.GetSystemString(AuditLogMetadata.ColumnNames.AuditActionType);
            }

            set
            {
                base.SetSystemString(AuditLogMetadata.ColumnNames.AuditActionType, value);
            }
        }
        /// <summary>
        /// Maps to AuditLog.PrimaryKeyData
        /// </summary>
        virtual public System.String PrimaryKeyData
        {
            get
            {
                return base.GetSystemString(AuditLogMetadata.ColumnNames.PrimaryKeyData);
            }

            set
            {
                base.SetSystemString(AuditLogMetadata.ColumnNames.PrimaryKeyData, value);
            }
        }
        /// <summary>
        /// Maps to AuditLog.ActionByUserID
        /// </summary>
        virtual public System.String ActionByUserID
        {
            get
            {
                return base.GetSystemString(AuditLogMetadata.ColumnNames.ActionByUserID);
            }

            set
            {
                base.SetSystemString(AuditLogMetadata.ColumnNames.ActionByUserID, value);
            }
        }
        /// <summary>
        /// Maps to AuditLog.LogDateTime
        /// </summary>
        virtual public System.DateTime? LogDateTime
        {
            get
            {
                return base.GetSystemDateTime(AuditLogMetadata.ColumnNames.LogDateTime);
            }

            set
            {
                base.SetSystemDateTime(AuditLogMetadata.ColumnNames.LogDateTime, value);
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
            public esStrings(esAuditLog entity)
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
            public System.String TableName
            {
                get
                {
                    System.String data = entity.TableName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TableName = null;
                    else entity.TableName = Convert.ToString(value);
                }
            }
            public System.String AuditActionType
            {
                get
                {
                    System.String data = entity.AuditActionType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AuditActionType = null;
                    else entity.AuditActionType = Convert.ToString(value);
                }
            }
            public System.String PrimaryKeyData
            {
                get
                {
                    System.String data = entity.PrimaryKeyData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrimaryKeyData = null;
                    else entity.PrimaryKeyData = Convert.ToString(value);
                }
            }
            public System.String ActionByUserID
            {
                get
                {
                    System.String data = entity.ActionByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ActionByUserID = null;
                    else entity.ActionByUserID = Convert.ToString(value);
                }
            }
            public System.String LogDateTime
            {
                get
                {
                    System.DateTime? data = entity.LogDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LogDateTime = null;
                    else entity.LogDateTime = Convert.ToDateTime(value);
                }
            }
            private esAuditLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAuditLogQuery query)
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
                throw new Exception("esAuditLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AuditLog : esAuditLog
    {
    }

    [Serializable]
    abstract public class esAuditLogQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return AuditLogMetadata.Meta();
            }
        }

        public esQueryItem AuditLogID
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.AuditLogID, esSystemType.Int32);
            }
        }

        public esQueryItem TableName
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.TableName, esSystemType.String);
            }
        }

        public esQueryItem AuditActionType
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.AuditActionType, esSystemType.String);
            }
        }

        public esQueryItem PrimaryKeyData
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.PrimaryKeyData, esSystemType.String);
            }
        }

        public esQueryItem ActionByUserID
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.ActionByUserID, esSystemType.String);
            }
        }

        public esQueryItem LogDateTime
        {
            get
            {
                return new esQueryItem(this, AuditLogMetadata.ColumnNames.LogDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AuditLogCollection")]
    public partial class AuditLogCollection : esAuditLogCollection, IEnumerable<AuditLog>
    {
        public AuditLogCollection()
        {

        }

        public static implicit operator List<AuditLog>(AuditLogCollection coll)
        {
            List<AuditLog> list = new List<AuditLog>();

            foreach (AuditLog emp in coll)
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
                return AuditLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AuditLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AuditLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AuditLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AuditLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AuditLogQuery();
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
        public bool Load(AuditLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AuditLog AddNew()
        {
            AuditLog entity = base.AddNewEntity() as AuditLog;

            return entity;
        }
        public AuditLog FindByPrimaryKey(Int32 auditLogID)
        {
            return base.FindByPrimaryKey(auditLogID) as AuditLog;
        }

        #region IEnumerable< AuditLog> Members

        IEnumerator<AuditLog> IEnumerable<AuditLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AuditLog;
            }
        }

        #endregion

        private AuditLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AuditLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AuditLog ({AuditLogID})")]
    [Serializable]
    public partial class AuditLog : esAuditLog
    {
        public AuditLog()
        {
        }

        public AuditLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AuditLogMetadata.Meta();
            }
        }

        override protected esAuditLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AuditLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AuditLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AuditLogQuery();
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
        public bool Load(AuditLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AuditLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AuditLogQuery : esAuditLogQuery
    {
        public AuditLogQuery()
        {

        }

        public AuditLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AuditLogQuery";
        }
    }

    [Serializable]
    public partial class AuditLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AuditLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.AuditLogID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AuditLogMetadata.PropertyNames.AuditLogID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.TableName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogMetadata.PropertyNames.TableName;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.AuditActionType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogMetadata.PropertyNames.AuditActionType;
            c.CharacterMaxLength = 1;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.PrimaryKeyData, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogMetadata.PropertyNames.PrimaryKeyData;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.ActionByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AuditLogMetadata.PropertyNames.ActionByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(AuditLogMetadata.ColumnNames.LogDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AuditLogMetadata.PropertyNames.LogDateTime;
            _columns.Add(c);


        }
        #endregion

        static public AuditLogMetadata Meta()
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
            public const string TableName = "TableName";
            public const string AuditActionType = "AuditActionType";
            public const string PrimaryKeyData = "PrimaryKeyData";
            public const string ActionByUserID = "ActionByUserID";
            public const string LogDateTime = "LogDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string AuditLogID = "AuditLogID";
            public const string TableName = "TableName";
            public const string AuditActionType = "AuditActionType";
            public const string PrimaryKeyData = "PrimaryKeyData";
            public const string ActionByUserID = "ActionByUserID";
            public const string LogDateTime = "LogDateTime";
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
            lock (typeof(AuditLogMetadata))
            {
                if (AuditLogMetadata.mapDelegates == null)
                {
                    AuditLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AuditLogMetadata.meta == null)
                {
                    AuditLogMetadata.meta = new AuditLogMetadata();
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
                meta.AddTypeMap("TableName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AuditActionType", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("PrimaryKeyData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ActionByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LogDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "AuditLog";
                meta.Destination = "AuditLog";
                meta.spInsert = "proc_AuditLogInsert";
                meta.spUpdate = "proc_AuditLogUpdate";
                meta.spDelete = "proc_AuditLogDelete";
                meta.spLoadAll = "proc_AuditLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_AuditLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AuditLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
