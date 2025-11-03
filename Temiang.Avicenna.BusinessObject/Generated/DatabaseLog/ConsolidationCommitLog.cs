/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/30/17 10:44:52 AM
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
    abstract public class esConsolidationCommitLogCollection : esEntityCollectionWAuditLog
    {
        public esConsolidationCommitLogCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "ConsolidationCommitLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esConsolidationCommitLogQuery query)
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
            this.InitQuery(query as esConsolidationCommitLogQuery);
        }
        #endregion

        virtual public ConsolidationCommitLog DetachEntity(ConsolidationCommitLog entity)
        {
            return base.DetachEntity(entity) as ConsolidationCommitLog;
        }

        virtual public ConsolidationCommitLog AttachEntity(ConsolidationCommitLog entity)
        {
            return base.AttachEntity(entity) as ConsolidationCommitLog;
        }

        virtual public void Combine(ConsolidationCommitLogCollection collection)
        {
            base.Combine(collection);
        }

        new public ConsolidationCommitLog this[int index]
        {
            get
            {
                return base[index] as ConsolidationCommitLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ConsolidationCommitLog);
        }
    }

    [Serializable]
    abstract public class esConsolidationCommitLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esConsolidationCommitLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esConsolidationCommitLog()
        {
        }

        public esConsolidationCommitLog(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 commitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(commitID);
            else
                return LoadByPrimaryKeyStoredProcedure(commitID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 commitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(commitID);
            else
                return LoadByPrimaryKeyStoredProcedure(commitID);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 commitID)
        {
            esConsolidationCommitLogQuery query = this.GetDynamicQuery();
            query.Where(query.CommitID == commitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 commitID)
        {
            esParameters parms = new esParameters();
            parms.Add("CommitID", commitID);
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
                        case "CommitID": this.str.CommitID = (string)value; break;
                        case "CommitDateTime": this.str.CommitDateTime = (string)value; break;
                        case "HealthcareID": this.str.HealthcareID = (string)value; break;
                        case "IsManualLog": this.str.IsManualLog = (string)value; break;
                        case "CommitSummary": this.str.CommitSummary = (string)value; break;
                        case "CommitData": this.str.CommitData = (string)value; break;
                        case "StartConsolidationLogID": this.str.StartConsolidationLogID = (string)value; break;
                        case "EndConsolidationLogID": this.str.EndConsolidationLogID = (string)value; break;
                        case "IsError": this.str.IsError = (string)value; break;
                        case "ErrorMessage": this.str.ErrorMessage = (string)value; break;
                        case "IsSendFailed": this.str.IsSendFailed = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CommitID":

                            if (value == null || value is System.Int64)
                                this.CommitID = (System.Int64?)value;
                            break;
                        case "CommitDateTime":

                            if (value == null || value is System.DateTime)
                                this.CommitDateTime = (System.DateTime?)value;
                            break;
                        case "IsManualLog":

                            if (value == null || value is System.Boolean)
                                this.IsManualLog = (System.Boolean?)value;
                            break;
                        case "StartConsolidationLogID":

                            if (value == null || value is System.Int64)
                                this.StartConsolidationLogID = (System.Int64?)value;
                            break;
                        case "EndConsolidationLogID":

                            if (value == null || value is System.Int64)
                                this.EndConsolidationLogID = (System.Int64?)value;
                            break;
                        case "IsError":

                            if (value == null || value is System.Boolean)
                                this.IsError = (System.Boolean?)value;
                            break;
                        case "IsSendFailed":

                            if (value == null || value is System.Boolean)
                                this.IsSendFailed = (System.Boolean?)value;
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
        /// Maps to ConsolidationCommitLog.CommitID
        /// </summary>
        virtual public System.Int64? CommitID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.CommitID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.CommitID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.CommitDateTime
        /// </summary>
        virtual public System.DateTime? CommitDateTime
        {
            get
            {
                return base.GetSystemDateTime(ConsolidationCommitLogMetadata.ColumnNames.CommitDateTime);
            }

            set
            {
                base.SetSystemDateTime(ConsolidationCommitLogMetadata.ColumnNames.CommitDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.HealthcareID
        /// </summary>
        virtual public System.String HealthcareID
        {
            get
            {
                return base.GetSystemString(ConsolidationCommitLogMetadata.ColumnNames.HealthcareID);
            }

            set
            {
                base.SetSystemString(ConsolidationCommitLogMetadata.ColumnNames.HealthcareID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.IsManualLog
        /// </summary>
        virtual public System.Boolean? IsManualLog
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsManualLog);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsManualLog, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.CommitSummary
        /// </summary>
        virtual public System.String CommitSummary
        {
            get
            {
                return base.GetSystemString(ConsolidationCommitLogMetadata.ColumnNames.CommitSummary);
            }

            set
            {
                base.SetSystemString(ConsolidationCommitLogMetadata.ColumnNames.CommitSummary, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.CommitData
        /// </summary>
        virtual public System.String CommitData
        {
            get
            {
                return base.GetSystemString(ConsolidationCommitLogMetadata.ColumnNames.CommitData);
            }

            set
            {
                base.SetSystemString(ConsolidationCommitLogMetadata.ColumnNames.CommitData, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.StartConsolidationLogID
        /// </summary>
        virtual public System.Int64? StartConsolidationLogID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.StartConsolidationLogID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.StartConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.EndConsolidationLogID
        /// </summary>
        virtual public System.Int64? EndConsolidationLogID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.EndConsolidationLogID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationCommitLogMetadata.ColumnNames.EndConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.IsError
        /// </summary>
        virtual public System.Boolean? IsError
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsError);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsError, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.ErrorMessage
        /// </summary>
        virtual public System.String ErrorMessage
        {
            get
            {
                return base.GetSystemString(ConsolidationCommitLogMetadata.ColumnNames.ErrorMessage);
            }

            set
            {
                base.SetSystemString(ConsolidationCommitLogMetadata.ColumnNames.ErrorMessage, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationCommitLog.IsSendFailed
        /// </summary>
        virtual public System.Boolean? IsSendFailed
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsSendFailed);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationCommitLogMetadata.ColumnNames.IsSendFailed, value);
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
            public esStrings(esConsolidationCommitLog entity)
            {
                this.entity = entity;
            }
            public System.String CommitID
            {
                get
                {
                    System.Int64? data = entity.CommitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CommitID = null;
                    else entity.CommitID = Convert.ToInt64(value);
                }
            }
            public System.String CommitDateTime
            {
                get
                {
                    System.DateTime? data = entity.CommitDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CommitDateTime = null;
                    else entity.CommitDateTime = Convert.ToDateTime(value);
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
            public System.String CommitSummary
            {
                get
                {
                    System.String data = entity.CommitSummary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CommitSummary = null;
                    else entity.CommitSummary = Convert.ToString(value);
                }
            }
            public System.String CommitData
            {
                get
                {
                    System.String data = entity.CommitData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CommitData = null;
                    else entity.CommitData = Convert.ToString(value);
                }
            }
            public System.String StartConsolidationLogID
            {
                get
                {
                    System.Int64? data = entity.StartConsolidationLogID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartConsolidationLogID = null;
                    else entity.StartConsolidationLogID = Convert.ToInt64(value);
                }
            }
            public System.String EndConsolidationLogID
            {
                get
                {
                    System.Int64? data = entity.EndConsolidationLogID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndConsolidationLogID = null;
                    else entity.EndConsolidationLogID = Convert.ToInt64(value);
                }
            }
            public System.String IsError
            {
                get
                {
                    System.Boolean? data = entity.IsError;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsError = null;
                    else entity.IsError = Convert.ToBoolean(value);
                }
            }
            public System.String ErrorMessage
            {
                get
                {
                    System.String data = entity.ErrorMessage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ErrorMessage = null;
                    else entity.ErrorMessage = Convert.ToString(value);
                }
            }
            public System.String IsSendFailed
            {
                get
                {
                    System.Boolean? data = entity.IsSendFailed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSendFailed = null;
                    else entity.IsSendFailed = Convert.ToBoolean(value);
                }
            }
            private esConsolidationCommitLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esConsolidationCommitLogQuery query)
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
                throw new Exception("esConsolidationCommitLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ConsolidationCommitLog : esConsolidationCommitLog
    {
    }

    [Serializable]
    abstract public class esConsolidationCommitLogQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationCommitLogMetadata.Meta();
            }
        }

        public esQueryItem CommitID
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.CommitID, esSystemType.Int64);
            }
        }

        public esQueryItem CommitDateTime
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.CommitDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem HealthcareID
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.HealthcareID, esSystemType.String);
            }
        }

        public esQueryItem IsManualLog
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.IsManualLog, esSystemType.Boolean);
            }
        }

        public esQueryItem CommitSummary
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.CommitSummary, esSystemType.String);
            }
        }

        public esQueryItem CommitData
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.CommitData, esSystemType.String);
            }
        }

        public esQueryItem StartConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.StartConsolidationLogID, esSystemType.Int64);
            }
        }

        public esQueryItem EndConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.EndConsolidationLogID, esSystemType.Int64);
            }
        }

        public esQueryItem IsError
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.IsError, esSystemType.Boolean);
            }
        }

        public esQueryItem ErrorMessage
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.ErrorMessage, esSystemType.String);
            }
        }

        public esQueryItem IsSendFailed
        {
            get
            {
                return new esQueryItem(this, ConsolidationCommitLogMetadata.ColumnNames.IsSendFailed, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ConsolidationCommitLogCollection")]
    public partial class ConsolidationCommitLogCollection : esConsolidationCommitLogCollection, IEnumerable<ConsolidationCommitLog>
    {
        public ConsolidationCommitLogCollection()
        {

        }

        public static implicit operator List<ConsolidationCommitLog>(ConsolidationCommitLogCollection coll)
        {
            List<ConsolidationCommitLog> list = new List<ConsolidationCommitLog>();

            foreach (ConsolidationCommitLog emp in coll)
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
                return ConsolidationCommitLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationCommitLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ConsolidationCommitLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ConsolidationCommitLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationCommitLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationCommitLogQuery();
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
        public bool Load(ConsolidationCommitLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ConsolidationCommitLog AddNew()
        {
            ConsolidationCommitLog entity = base.AddNewEntity() as ConsolidationCommitLog;

            return entity;
        }
        public ConsolidationCommitLog FindByPrimaryKey(Int64 commitID)
        {
            return base.FindByPrimaryKey(commitID) as ConsolidationCommitLog;
        }

        #region IEnumerable< ConsolidationCommitLog> Members

        IEnumerator<ConsolidationCommitLog> IEnumerable<ConsolidationCommitLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ConsolidationCommitLog;
            }
        }

        #endregion

        private ConsolidationCommitLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ConsolidationCommitLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConsolidationCommitLog ({CommitID})")]
    [Serializable]
    public partial class ConsolidationCommitLog : esConsolidationCommitLog
    {
        public ConsolidationCommitLog()
        {
        }

        public ConsolidationCommitLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationCommitLogMetadata.Meta();
            }
        }

        override protected esConsolidationCommitLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationCommitLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationCommitLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationCommitLogQuery();
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
        public bool Load(ConsolidationCommitLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ConsolidationCommitLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ConsolidationCommitLogQuery : esConsolidationCommitLogQuery
    {
        public ConsolidationCommitLogQuery()
        {

        }

        public ConsolidationCommitLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ConsolidationCommitLogQuery";
        }
    }

    [Serializable]
    public partial class ConsolidationCommitLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ConsolidationCommitLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.CommitID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.CommitID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.CommitDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.CommitDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.HealthcareID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.HealthcareID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.IsManualLog, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.IsManualLog;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.CommitSummary, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.CommitSummary;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.CommitData, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.CommitData;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.StartConsolidationLogID, 6, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.StartConsolidationLogID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.EndConsolidationLogID, 7, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.EndConsolidationLogID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.IsError, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.IsError;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.ErrorMessage, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.ErrorMessage;
            c.CharacterMaxLength = 2000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationCommitLogMetadata.ColumnNames.IsSendFailed, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationCommitLogMetadata.PropertyNames.IsSendFailed;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ConsolidationCommitLogMetadata Meta()
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
            public const string CommitID = "CommitID";
            public const string CommitDateTime = "CommitDateTime";
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string CommitSummary = "CommitSummary";
            public const string CommitData = "CommitData";
            public const string StartConsolidationLogID = "StartConsolidationLogID";
            public const string EndConsolidationLogID = "EndConsolidationLogID";
            public const string IsError = "IsError";
            public const string ErrorMessage = "ErrorMessage";
            public const string IsSendFailed = "IsSendFailed";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string CommitID = "CommitID";
            public const string CommitDateTime = "CommitDateTime";
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string CommitSummary = "CommitSummary";
            public const string CommitData = "CommitData";
            public const string StartConsolidationLogID = "StartConsolidationLogID";
            public const string EndConsolidationLogID = "EndConsolidationLogID";
            public const string IsError = "IsError";
            public const string ErrorMessage = "ErrorMessage";
            public const string IsSendFailed = "IsSendFailed";
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
            lock (typeof(ConsolidationCommitLogMetadata))
            {
                if (ConsolidationCommitLogMetadata.mapDelegates == null)
                {
                    ConsolidationCommitLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ConsolidationCommitLogMetadata.meta == null)
                {
                    ConsolidationCommitLogMetadata.meta = new ConsolidationCommitLogMetadata();
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

                meta.AddTypeMap("CommitID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("CommitDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("HealthcareID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsManualLog", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CommitSummary", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CommitData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartConsolidationLogID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("EndConsolidationLogID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("IsError", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ErrorMessage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSendFailed", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ConsolidationCommitLog";
                meta.Destination = "ConsolidationCommitLog";
                meta.spInsert = "proc_ConsolidationCommitLogInsert";
                meta.spUpdate = "proc_ConsolidationCommitLogUpdate";
                meta.spDelete = "proc_ConsolidationCommitLogDelete";
                meta.spLoadAll = "proc_ConsolidationCommitLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_ConsolidationCommitLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ConsolidationCommitLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
