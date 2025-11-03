/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/8/2016 5:51:11 PM
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
    abstract public class esConsolidationLogCollection : esEntityCollectionWAuditLog
    {
        public esConsolidationLogCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "ConsolidationLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esConsolidationLogQuery query)
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
            this.InitQuery(query as esConsolidationLogQuery);
        }
        #endregion

        virtual public ConsolidationLog DetachEntity(ConsolidationLog entity)
        {
            return base.DetachEntity(entity) as ConsolidationLog;
        }

        virtual public ConsolidationLog AttachEntity(ConsolidationLog entity)
        {
            return base.AttachEntity(entity) as ConsolidationLog;
        }

        virtual public void Combine(ConsolidationLogCollection collection)
        {
            base.Combine(collection);
        }

        new public ConsolidationLog this[int index]
        {
            get
            {
                return base[index] as ConsolidationLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ConsolidationLog);
        }
    }

    [Serializable]
    abstract public class esConsolidationLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esConsolidationLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esConsolidationLog()
        {
        }

        public esConsolidationLog(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 consolidationLogID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consolidationLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(consolidationLogID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 consolidationLogID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(consolidationLogID);
            else
                return LoadByPrimaryKeyStoredProcedure(consolidationLogID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 consolidationLogID)
        {
            esConsolidationLogQuery query = this.GetDynamicQuery();
            query.Where(query.ConsolidationLogID == consolidationLogID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 consolidationLogID)
        {
            esParameters parms = new esParameters();
            parms.Add("ConsolidationLogID", consolidationLogID);
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
                        case "HealthcareID": this.str.HealthcareID = (string)value; break;
                        case "IsManualLog": this.str.IsManualLog = (string)value; break;
                        case "TableName": this.str.TableName = (string)value; break;
                        case "ConsolidationType": this.str.ConsolidationType = (string)value; break;
                        case "PrimaryKeyData": this.str.PrimaryKeyData = (string)value; break;
                        case "LogDateTime": this.str.LogDateTime = (string)value; break;
                        case "IsSend": this.str.IsSend = (string)value; break;
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
                        case "IsManualLog":

                            if (value == null || value is System.Boolean)
                                this.IsManualLog = (System.Boolean?)value;
                            break;
                        case "LogDateTime":

                            if (value == null || value is System.DateTime)
                                this.LogDateTime = (System.DateTime?)value;
                            break;
                        case "IsSend":

                            if (value == null || value is System.Boolean)
                                this.IsSend = (System.Boolean?)value;
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
        /// Maps to ConsolidationLog.ConsolidationLogID
        /// </summary>
        virtual public System.Int32? ConsolidationLogID
        {
            get
            {
                return base.GetSystemInt32(ConsolidationLogMetadata.ColumnNames.ConsolidationLogID);
            }

            set
            {
                base.SetSystemInt32(ConsolidationLogMetadata.ColumnNames.ConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.HealthcareID
        /// </summary>
        virtual public System.String HealthcareID
        {
            get
            {
                return base.GetSystemString(ConsolidationLogMetadata.ColumnNames.HealthcareID);
            }

            set
            {
                base.SetSystemString(ConsolidationLogMetadata.ColumnNames.HealthcareID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.IsManualLog
        /// </summary>
        virtual public System.Boolean? IsManualLog
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationLogMetadata.ColumnNames.IsManualLog);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationLogMetadata.ColumnNames.IsManualLog, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.TableName
        /// </summary>
        virtual public System.String TableName
        {
            get
            {
                return base.GetSystemString(ConsolidationLogMetadata.ColumnNames.TableName);
            }

            set
            {
                base.SetSystemString(ConsolidationLogMetadata.ColumnNames.TableName, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.ConsolidationType
        /// </summary>
        virtual public System.String ConsolidationType
        {
            get
            {
                return base.GetSystemString(ConsolidationLogMetadata.ColumnNames.ConsolidationType);
            }

            set
            {
                base.SetSystemString(ConsolidationLogMetadata.ColumnNames.ConsolidationType, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.PrimaryKeyData
        /// </summary>
        virtual public System.String PrimaryKeyData
        {
            get
            {
                return base.GetSystemString(ConsolidationLogMetadata.ColumnNames.PrimaryKeyData);
            }

            set
            {
                base.SetSystemString(ConsolidationLogMetadata.ColumnNames.PrimaryKeyData, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.LogDateTime
        /// </summary>
        virtual public System.DateTime? LogDateTime
        {
            get
            {
                return base.GetSystemDateTime(ConsolidationLogMetadata.ColumnNames.LogDateTime);
            }

            set
            {
                base.SetSystemDateTime(ConsolidationLogMetadata.ColumnNames.LogDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationLog.IsSend
        /// </summary>
        virtual public System.Boolean? IsSend
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationLogMetadata.ColumnNames.IsSend);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationLogMetadata.ColumnNames.IsSend, value);
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
            public esStrings(esConsolidationLog entity)
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
            public System.String HealthcareID
            {
                get
                {
                    System.String data = entity.HealthcareID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HealthcareID = null;
                    else entity.HealthcareID = Convert.ToString(value);
                }
            }
            public System.String IsManualLog
            {
                get
                {
                    System.Boolean? data = entity.IsManualLog;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsManualLog = null;
                    else entity.IsManualLog = Convert.ToBoolean(value);
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
            public System.String ConsolidationType
            {
                get
                {
                    System.String data = entity.ConsolidationType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsolidationType = null;
                    else entity.ConsolidationType = Convert.ToString(value);
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
            public System.String IsSend
            {
                get
                {
                    System.Boolean? data = entity.IsSend;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSend = null;
                    else entity.IsSend = Convert.ToBoolean(value);
                }
            }
            private esConsolidationLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esConsolidationLogQuery query)
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
                throw new Exception("esConsolidationLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ConsolidationLog : esConsolidationLog
    {
    }

    [Serializable]
    abstract public class esConsolidationLogQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationLogMetadata.Meta();
            }
        }

        public esQueryItem ConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.ConsolidationLogID, esSystemType.Int32);
            }
        }

        public esQueryItem HealthcareID
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.HealthcareID, esSystemType.String);
            }
        }

        public esQueryItem IsManualLog
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.IsManualLog, esSystemType.Boolean);
            }
        }

        public esQueryItem TableName
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.TableName, esSystemType.String);
            }
        }

        public esQueryItem ConsolidationType
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.ConsolidationType, esSystemType.String);
            }
        }

        public esQueryItem PrimaryKeyData
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.PrimaryKeyData, esSystemType.String);
            }
        }

        public esQueryItem LogDateTime
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.LogDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsSend
        {
            get
            {
                return new esQueryItem(this, ConsolidationLogMetadata.ColumnNames.IsSend, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ConsolidationLogCollection")]
    public partial class ConsolidationLogCollection : esConsolidationLogCollection, IEnumerable<ConsolidationLog>
    {
        public ConsolidationLogCollection()
        {

        }

        public static implicit operator List<ConsolidationLog>(ConsolidationLogCollection coll)
        {
            List<ConsolidationLog> list = new List<ConsolidationLog>();

            foreach (ConsolidationLog emp in coll)
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
                return ConsolidationLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ConsolidationLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ConsolidationLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationLogQuery();
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
        public bool Load(ConsolidationLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ConsolidationLog AddNew()
        {
            ConsolidationLog entity = base.AddNewEntity() as ConsolidationLog;

            return entity;
        }
        public ConsolidationLog FindByPrimaryKey(Int32 consolidationLogID)
        {
            return base.FindByPrimaryKey(consolidationLogID) as ConsolidationLog;
        }

        #region IEnumerable< ConsolidationLog> Members

        IEnumerator<ConsolidationLog> IEnumerable<ConsolidationLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ConsolidationLog;
            }
        }

        #endregion

        private ConsolidationLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ConsolidationLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConsolidationLog ({ConsolidationLogID})")]
    [Serializable]
    public partial class ConsolidationLog : esConsolidationLog
    {
        public ConsolidationLog()
        {
        }

        public ConsolidationLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationLogMetadata.Meta();
            }
        }

        override protected esConsolidationLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationLogQuery();
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
        public bool Load(ConsolidationLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ConsolidationLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ConsolidationLogQuery : esConsolidationLogQuery
    {
        public ConsolidationLogQuery()
        {

        }

        public ConsolidationLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ConsolidationLogQuery";
        }
    }

    [Serializable]
    public partial class ConsolidationLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ConsolidationLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.ConsolidationLogID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.ConsolidationLogID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.HealthcareID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.HealthcareID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.IsManualLog, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.IsManualLog;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.TableName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.TableName;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.ConsolidationType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.ConsolidationType;
            c.CharacterMaxLength = 1;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.PrimaryKeyData, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.PrimaryKeyData;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.LogDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.LogDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationLogMetadata.ColumnNames.IsSend, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationLogMetadata.PropertyNames.IsSend;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ConsolidationLogMetadata Meta()
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
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string TableName = "TableName";
            public const string ConsolidationType = "ConsolidationType";
            public const string PrimaryKeyData = "PrimaryKeyData";
            public const string LogDateTime = "LogDateTime";
            public const string IsSend = "IsSend";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ConsolidationLogID = "ConsolidationLogID";
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string TableName = "TableName";
            public const string ConsolidationType = "ConsolidationType";
            public const string PrimaryKeyData = "PrimaryKeyData";
            public const string LogDateTime = "LogDateTime";
            public const string IsSend = "IsSend";
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
            lock (typeof(ConsolidationLogMetadata))
            {
                if (ConsolidationLogMetadata.mapDelegates == null)
                {
                    ConsolidationLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ConsolidationLogMetadata.meta == null)
                {
                    ConsolidationLogMetadata.meta = new ConsolidationLogMetadata();
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
                meta.AddTypeMap("HealthcareID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsManualLog", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("TableName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsolidationType", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("PrimaryKeyData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LogDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsSend", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ConsolidationLog";
                meta.Destination = "ConsolidationLog";
                meta.spInsert = "proc_ConsolidationLogInsert";
                meta.spUpdate = "proc_ConsolidationLogUpdate";
                meta.spDelete = "proc_ConsolidationLogDelete";
                meta.spLoadAll = "proc_ConsolidationLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_ConsolidationLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ConsolidationLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
