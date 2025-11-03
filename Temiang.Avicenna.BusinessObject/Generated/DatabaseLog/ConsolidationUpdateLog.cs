/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/30/2016 10:56:18 AM
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
    abstract public class esConsolidationUpdateLogCollection : esEntityCollectionWAuditLog
    {
        public esConsolidationUpdateLogCollection()
        {

        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        protected override string GetCollectionName()
        {
            return "ConsolidationUpdateLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esConsolidationUpdateLogQuery query)
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
            this.InitQuery(query as esConsolidationUpdateLogQuery);
        }
        #endregion

        virtual public ConsolidationUpdateLog DetachEntity(ConsolidationUpdateLog entity)
        {
            return base.DetachEntity(entity) as ConsolidationUpdateLog;
        }

        virtual public ConsolidationUpdateLog AttachEntity(ConsolidationUpdateLog entity)
        {
            return base.AttachEntity(entity) as ConsolidationUpdateLog;
        }

        virtual public void Combine(ConsolidationUpdateLogCollection collection)
        {
            base.Combine(collection);
        }

        new public ConsolidationUpdateLog this[int index]
        {
            get
            {
                return base[index] as ConsolidationUpdateLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ConsolidationUpdateLog);
        }
    }

    [Serializable]
    abstract public class esConsolidationUpdateLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esConsolidationUpdateLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esConsolidationUpdateLog()
        {
        }

        public esConsolidationUpdateLog(DataRow row)
            : base(row)
        {
        }

        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 updateID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(updateID);
            else
                return LoadByPrimaryKeyStoredProcedure(updateID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 updateID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(updateID);
            else
                return LoadByPrimaryKeyStoredProcedure(updateID);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 updateID)
        {
            esConsolidationUpdateLogQuery query = this.GetDynamicQuery();
            query.Where(query.UpdateID == updateID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 updateID)
        {
            esParameters parms = new esParameters();
            parms.Add("UpdateID", updateID);
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
                        case "UpdateID": this.str.UpdateID = (string)value; break;
                        case "UpdateDateTime": this.str.UpdateDateTime = (string)value; break;
                        case "HealthcareID": this.str.HealthcareID = (string)value; break;
                        case "IsManualLog": this.str.IsManualLog = (string)value; break;
                        case "UpdateSummary": this.str.UpdateSummary = (string)value; break;
                        case "UpdateData": this.str.UpdateData = (string)value; break;
                        case "StartConsolidationLogID": this.str.StartConsolidationLogID = (string)value; break;
                        case "EndConsolidationLogID": this.str.EndConsolidationLogID = (string)value; break;
                        case "SuccessUpdateDateTime": this.str.SuccessUpdateDateTime = (string)value; break;
                        case "IsError": this.str.IsError = (string)value; break;
                        case "ErrorMessage": this.str.ErrorMessage = (string)value; break;
                        case "ReferenceID": this.str.ReferenceID = (string)value; break;
                        case "ReferenceNote": this.str.ReferenceNote = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "UpdateID":

                            if (value == null || value is System.Int64)
                                this.UpdateID = (System.Int64?)value;
                            break;
                        case "UpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.UpdateDateTime = (System.DateTime?)value;
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
                        case "SuccessUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.SuccessUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsError":

                            if (value == null || value is System.Boolean)
                                this.IsError = (System.Boolean?)value;
                            break;
                        case "ReferenceID":

                            if (value == null || value is System.Int64)
                                this.ReferenceID = (System.Int64?)value;
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
        /// Maps to ConsolidationUpdateLog.UpdateID
        /// </summary>
        virtual public System.Int64? UpdateID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.UpdateID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.UpdateID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.UpdateDateTime
        /// </summary>
        virtual public System.DateTime? UpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ConsolidationUpdateLogMetadata.ColumnNames.UpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ConsolidationUpdateLogMetadata.ColumnNames.UpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.HealthcareID
        /// </summary>
        virtual public System.String HealthcareID
        {
            get
            {
                return base.GetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.HealthcareID);
            }

            set
            {
                base.SetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.HealthcareID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.IsManualLog
        /// </summary>
        virtual public System.Boolean? IsManualLog
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationUpdateLogMetadata.ColumnNames.IsManualLog);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationUpdateLogMetadata.ColumnNames.IsManualLog, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.UpdateSummary
        /// </summary>
        virtual public System.String UpdateSummary
        {
            get
            {
                return base.GetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.UpdateSummary);
            }

            set
            {
                base.SetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.UpdateSummary, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.UpdateData
        /// </summary>
        virtual public System.String UpdateData
        {
            get
            {
                return base.GetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.UpdateData);
            }

            set
            {
                base.SetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.UpdateData, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.StartConsolidationLogID
        /// </summary>
        virtual public System.Int64? StartConsolidationLogID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.StartConsolidationLogID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.StartConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.EndConsolidationLogID
        /// </summary>
        virtual public System.Int64? EndConsolidationLogID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.EndConsolidationLogID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.EndConsolidationLogID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.SuccessUpdateDateTime
        /// </summary>
        virtual public System.DateTime? SuccessUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ConsolidationUpdateLogMetadata.ColumnNames.SuccessUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ConsolidationUpdateLogMetadata.ColumnNames.SuccessUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.IsError
        /// </summary>
        virtual public System.Boolean? IsError
        {
            get
            {
                return base.GetSystemBoolean(ConsolidationUpdateLogMetadata.ColumnNames.IsError);
            }

            set
            {
                base.SetSystemBoolean(ConsolidationUpdateLogMetadata.ColumnNames.IsError, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.ErrorMessage
        /// </summary>
        virtual public System.String ErrorMessage
        {
            get
            {
                return base.GetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.ErrorMessage);
            }

            set
            {
                base.SetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.ErrorMessage, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.ReferenceID
        /// </summary>
        virtual public System.Int64? ReferenceID
        {
            get
            {
                return base.GetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceID);
            }

            set
            {
                base.SetSystemInt64(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceID, value);
            }
        }
        /// <summary>
        /// Maps to ConsolidationUpdateLog.ReferenceNote
        /// </summary>
        virtual public System.String ReferenceNote
        {
            get
            {
                return base.GetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceNote);
            }

            set
            {
                base.SetSystemString(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceNote, value);
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
            public esStrings(esConsolidationUpdateLog entity)
            {
                this.entity = entity;
            }
            public System.String UpdateID
            {
                get
                {
                    System.Int64? data = entity.UpdateID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateID = null;
                    else entity.UpdateID = Convert.ToInt64(value);
                }
            }
            public System.String UpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.UpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateDateTime = null;
                    else entity.UpdateDateTime = Convert.ToDateTime(value);
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
            public System.String UpdateSummary
            {
                get
                {
                    System.String data = entity.UpdateSummary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateSummary = null;
                    else entity.UpdateSummary = Convert.ToString(value);
                }
            }
            public System.String UpdateData
            {
                get
                {
                    System.String data = entity.UpdateData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateData = null;
                    else entity.UpdateData = Convert.ToString(value);
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
            public System.String SuccessUpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.SuccessUpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SuccessUpdateDateTime = null;
                    else entity.SuccessUpdateDateTime = Convert.ToDateTime(value);
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
            public System.String ReferenceID
            {
                get
                {
                    System.Int64? data = entity.ReferenceID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceID = null;
                    else entity.ReferenceID = Convert.ToInt64(value);
                }
            }
            public System.String ReferenceNote
            {
                get
                {
                    System.String data = entity.ReferenceNote;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNote = null;
                    else entity.ReferenceNote = Convert.ToString(value);
                }
            }
            private esConsolidationUpdateLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esConsolidationUpdateLogQuery query)
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
                throw new Exception("esConsolidationUpdateLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ConsolidationUpdateLog : esConsolidationUpdateLog
    {
    }

    [Serializable]
    abstract public class esConsolidationUpdateLogQuery : esDynamicQuery
    {
        protected override string GetConnectionName()
        {
            return "DatabaseLog";
        }

        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationUpdateLogMetadata.Meta();
            }
        }

        public esQueryItem UpdateID
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.UpdateID, esSystemType.Int64);
            }
        }

        public esQueryItem UpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.UpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem HealthcareID
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.HealthcareID, esSystemType.String);
            }
        }

        public esQueryItem IsManualLog
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.IsManualLog, esSystemType.Boolean);
            }
        }

        public esQueryItem UpdateSummary
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.UpdateSummary, esSystemType.String);
            }
        }

        public esQueryItem UpdateData
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.UpdateData, esSystemType.String);
            }
        }

        public esQueryItem StartConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.StartConsolidationLogID, esSystemType.Int64);
            }
        }

        public esQueryItem EndConsolidationLogID
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.EndConsolidationLogID, esSystemType.Int64);
            }
        }

        public esQueryItem SuccessUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.SuccessUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsError
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.IsError, esSystemType.Boolean);
            }
        }

        public esQueryItem ErrorMessage
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.ErrorMessage, esSystemType.String);
            }
        }

        public esQueryItem ReferenceID
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.ReferenceID, esSystemType.Int64);
            }
        }

        public esQueryItem ReferenceNote
        {
            get
            {
                return new esQueryItem(this, ConsolidationUpdateLogMetadata.ColumnNames.ReferenceNote, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ConsolidationUpdateLogCollection")]
    public partial class ConsolidationUpdateLogCollection : esConsolidationUpdateLogCollection, IEnumerable<ConsolidationUpdateLog>
    {
        public ConsolidationUpdateLogCollection()
        {

        }

        public static implicit operator List<ConsolidationUpdateLog>(ConsolidationUpdateLogCollection coll)
        {
            List<ConsolidationUpdateLog> list = new List<ConsolidationUpdateLog>();

            foreach (ConsolidationUpdateLog emp in coll)
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
                return ConsolidationUpdateLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationUpdateLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ConsolidationUpdateLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ConsolidationUpdateLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationUpdateLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationUpdateLogQuery();
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
        public bool Load(ConsolidationUpdateLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ConsolidationUpdateLog AddNew()
        {
            ConsolidationUpdateLog entity = base.AddNewEntity() as ConsolidationUpdateLog;

            return entity;
        }
        public ConsolidationUpdateLog FindByPrimaryKey(Int64 updateID)
        {
            return base.FindByPrimaryKey(updateID) as ConsolidationUpdateLog;
        }

        #region IEnumerable< ConsolidationUpdateLog> Members

        IEnumerator<ConsolidationUpdateLog> IEnumerable<ConsolidationUpdateLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ConsolidationUpdateLog;
            }
        }

        #endregion

        private ConsolidationUpdateLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ConsolidationUpdateLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConsolidationUpdateLog ({UpdateID})")]
    [Serializable]
    public partial class ConsolidationUpdateLog : esConsolidationUpdateLog
    {
        public ConsolidationUpdateLog()
        {
        }

        public ConsolidationUpdateLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ConsolidationUpdateLogMetadata.Meta();
            }
        }

        override protected esConsolidationUpdateLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ConsolidationUpdateLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ConsolidationUpdateLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ConsolidationUpdateLogQuery();
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
        public bool Load(ConsolidationUpdateLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ConsolidationUpdateLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ConsolidationUpdateLogQuery : esConsolidationUpdateLogQuery
    {
        public ConsolidationUpdateLogQuery()
        {

        }

        public ConsolidationUpdateLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ConsolidationUpdateLogQuery";
        }
    }

    [Serializable]
    public partial class ConsolidationUpdateLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ConsolidationUpdateLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.UpdateID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.UpdateID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.UpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.UpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.HealthcareID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.HealthcareID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.IsManualLog, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.IsManualLog;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.UpdateSummary, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.UpdateSummary;
            c.CharacterMaxLength = -1;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.UpdateData, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.UpdateData;
            c.CharacterMaxLength = -1;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.StartConsolidationLogID, 6, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.StartConsolidationLogID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.EndConsolidationLogID, 7, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.EndConsolidationLogID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.SuccessUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.SuccessUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.IsError, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.IsError;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.ErrorMessage, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.ErrorMessage;
            c.CharacterMaxLength = 2000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceID, 11, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.ReferenceID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ConsolidationUpdateLogMetadata.ColumnNames.ReferenceNote, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ConsolidationUpdateLogMetadata.PropertyNames.ReferenceNote;
            c.CharacterMaxLength = -1;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ConsolidationUpdateLogMetadata Meta()
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
            public const string UpdateID = "UpdateID";
            public const string UpdateDateTime = "UpdateDateTime";
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string UpdateSummary = "UpdateSummary";
            public const string UpdateData = "UpdateData";
            public const string StartConsolidationLogID = "StartConsolidationLogID";
            public const string EndConsolidationLogID = "EndConsolidationLogID";
            public const string SuccessUpdateDateTime = "SuccessUpdateDateTime";
            public const string IsError = "IsError";
            public const string ErrorMessage = "ErrorMessage";
            public const string ReferenceID = "ReferenceID";
            public const string ReferenceNote = "ReferenceNote";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string UpdateID = "UpdateID";
            public const string UpdateDateTime = "UpdateDateTime";
            public const string HealthcareID = "HealthcareID";
            public const string IsManualLog = "IsManualLog";
            public const string UpdateSummary = "UpdateSummary";
            public const string UpdateData = "UpdateData";
            public const string StartConsolidationLogID = "StartConsolidationLogID";
            public const string EndConsolidationLogID = "EndConsolidationLogID";
            public const string SuccessUpdateDateTime = "SuccessUpdateDateTime";
            public const string IsError = "IsError";
            public const string ErrorMessage = "ErrorMessage";
            public const string ReferenceID = "ReferenceID";
            public const string ReferenceNote = "ReferenceNote";
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
            lock (typeof(ConsolidationUpdateLogMetadata))
            {
                if (ConsolidationUpdateLogMetadata.mapDelegates == null)
                {
                    ConsolidationUpdateLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ConsolidationUpdateLogMetadata.meta == null)
                {
                    ConsolidationUpdateLogMetadata.meta = new ConsolidationUpdateLogMetadata();
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

                meta.AddTypeMap("UpdateID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("UpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("HealthcareID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsManualLog", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("UpdateSummary", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UpdateData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartConsolidationLogID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("EndConsolidationLogID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("SuccessUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsError", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ErrorMessage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("ReferenceNote", new esTypeMap("varchar", "System.String"));


                meta.Source = "ConsolidationUpdateLog";
                meta.Destination = "ConsolidationUpdateLog";
                meta.spInsert = "proc_ConsolidationUpdateLogInsert";
                meta.spUpdate = "proc_ConsolidationUpdateLogUpdate";
                meta.spDelete = "proc_ConsolidationUpdateLogDelete";
                meta.spLoadAll = "proc_ConsolidationUpdateLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_ConsolidationUpdateLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ConsolidationUpdateLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
