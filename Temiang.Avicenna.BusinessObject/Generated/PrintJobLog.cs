/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/16/18 9:00:56 PM
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
    abstract public class esPrintJobLogCollection : esEntityCollectionWAuditLog
    {
        public esPrintJobLogCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PrintJobLogCollection";
        }

        #region Query Logic
        protected void InitQuery(esPrintJobLogQuery query)
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
            this.InitQuery(query as esPrintJobLogQuery);
        }
        #endregion

        virtual public PrintJobLog DetachEntity(PrintJobLog entity)
        {
            return base.DetachEntity(entity) as PrintJobLog;
        }

        virtual public PrintJobLog AttachEntity(PrintJobLog entity)
        {
            return base.AttachEntity(entity) as PrintJobLog;
        }

        virtual public void Combine(PrintJobLogCollection collection)
        {
            base.Combine(collection);
        }

        new public PrintJobLog this[int index]
        {
            get
            {
                return base[index] as PrintJobLog;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PrintJobLog);
        }
    }

    [Serializable]
    abstract public class esPrintJobLog : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPrintJobLogQuery GetDynamicQuery()
        {
            return null;
        }

        public esPrintJobLog()
        {
        }

        public esPrintJobLog(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 printNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(printNo);
            else
                return LoadByPrimaryKeyStoredProcedure(printNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 printNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(printNo);
            else
                return LoadByPrimaryKeyStoredProcedure(printNo);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 printNo)
        {
            esPrintJobLogQuery query = this.GetDynamicQuery();
            query.Where(query.PrintNo == printNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 printNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PrintNo", printNo);
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
                        case "PrintNo": this.str.PrintNo = (string)value; break;
                        case "PrintDateTime": this.str.PrintDateTime = (string)value; break;
                        case "ProgramID": this.str.ProgramID = (string)value; break;
                        case "PrinterID": this.str.PrinterID = (string)value; break;
                        case "UserID": this.str.UserID = (string)value; break;
                        case "ZplCommand": this.str.ZplCommand = (string)value; break;
                        case "IsFailed": this.str.IsFailed = (string)value; break;
                        case "FailedMessage": this.str.FailedMessage = (string)value; break;
                        case "ApplicationID": this.str.ApplicationID = (string)value; break;
                        case "UserHostName": this.str.UserHostName = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PrintNo":

                            if (value == null || value is System.Int64)
                                this.PrintNo = (System.Int64?)value;
                            break;
                        case "PrintDateTime":

                            if (value == null || value is System.DateTime)
                                this.PrintDateTime = (System.DateTime?)value;
                            break;
                        case "IsFailed":

                            if (value == null || value is System.Boolean)
                                this.IsFailed = (System.Boolean?)value;
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
        /// Maps to PrintJobLog.PrintNo
        /// </summary>
        virtual public System.Int64? PrintNo
        {
            get
            {
                return base.GetSystemInt64(PrintJobLogMetadata.ColumnNames.PrintNo);
            }

            set
            {
                base.SetSystemInt64(PrintJobLogMetadata.ColumnNames.PrintNo, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.PrintDateTime
        /// </summary>
        virtual public System.DateTime? PrintDateTime
        {
            get
            {
                return base.GetSystemDateTime(PrintJobLogMetadata.ColumnNames.PrintDateTime);
            }

            set
            {
                base.SetSystemDateTime(PrintJobLogMetadata.ColumnNames.PrintDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.ProgramID
        /// </summary>
        virtual public System.String ProgramID
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.ProgramID);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.ProgramID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.PrinterID
        /// </summary>
        virtual public System.String PrinterID
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.PrinterID);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.PrinterID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.UserID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.ZplCommand
        /// </summary>
        virtual public System.String ZplCommand
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.ZplCommand);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.ZplCommand, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.IsFailed
        /// </summary>
        virtual public System.Boolean? IsFailed
        {
            get
            {
                return base.GetSystemBoolean(PrintJobLogMetadata.ColumnNames.IsFailed);
            }

            set
            {
                base.SetSystemBoolean(PrintJobLogMetadata.ColumnNames.IsFailed, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.FailedMessage
        /// </summary>
        virtual public System.String FailedMessage
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.FailedMessage);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.FailedMessage, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.ApplicationID
        /// </summary>
        virtual public System.String ApplicationID
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.ApplicationID);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.ApplicationID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJobLog.UserHostName
        /// </summary>
        virtual public System.String UserHostName
        {
            get
            {
                return base.GetSystemString(PrintJobLogMetadata.ColumnNames.UserHostName);
            }

            set
            {
                base.SetSystemString(PrintJobLogMetadata.ColumnNames.UserHostName, value);
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
            public esStrings(esPrintJobLog entity)
            {
                this.entity = entity;
            }
            public System.String PrintNo
            {
                get
                {
                    System.Int64? data = entity.PrintNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrintNo = null;
                    else entity.PrintNo = Convert.ToInt64(value);
                }
            }
            public System.String PrintDateTime
            {
                get
                {
                    System.DateTime? data = entity.PrintDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrintDateTime = null;
                    else entity.PrintDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ProgramID
            {
                get
                {
                    System.String data = entity.ProgramID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProgramID = null;
                    else entity.ProgramID = Convert.ToString(value);
                }
            }
            public System.String PrinterID
            {
                get
                {
                    System.String data = entity.PrinterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrinterID = null;
                    else entity.PrinterID = Convert.ToString(value);
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
            public System.String ZplCommand
            {
                get
                {
                    System.String data = entity.ZplCommand;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ZplCommand = null;
                    else entity.ZplCommand = Convert.ToString(value);
                }
            }
            public System.String IsFailed
            {
                get
                {
                    System.Boolean? data = entity.IsFailed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsFailed = null;
                    else entity.IsFailed = Convert.ToBoolean(value);
                }
            }
            public System.String FailedMessage
            {
                get
                {
                    System.String data = entity.FailedMessage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FailedMessage = null;
                    else entity.FailedMessage = Convert.ToString(value);
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
            public System.String UserHostName
            {
                get
                {
                    System.String data = entity.UserHostName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UserHostName = null;
                    else entity.UserHostName = Convert.ToString(value);
                }
            }
            private esPrintJobLog entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPrintJobLogQuery query)
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
                throw new Exception("esPrintJobLog can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PrintJobLog : esPrintJobLog
    {
    }

    [Serializable]
    abstract public class esPrintJobLogQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PrintJobLogMetadata.Meta();
            }
        }

        public esQueryItem PrintNo
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.PrintNo, esSystemType.Int64);
            }
        }

        public esQueryItem PrintDateTime
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.PrintDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ProgramID
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.ProgramID, esSystemType.String);
            }
        }

        public esQueryItem PrinterID
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.PrinterID, esSystemType.String);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem ZplCommand
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.ZplCommand, esSystemType.String);
            }
        }

        public esQueryItem IsFailed
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.IsFailed, esSystemType.Boolean);
            }
        }

        public esQueryItem FailedMessage
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.FailedMessage, esSystemType.String);
            }
        }

        public esQueryItem ApplicationID
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.ApplicationID, esSystemType.String);
            }
        }

        public esQueryItem UserHostName
        {
            get
            {
                return new esQueryItem(this, PrintJobLogMetadata.ColumnNames.UserHostName, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PrintJobLogCollection")]
    public partial class PrintJobLogCollection : esPrintJobLogCollection, IEnumerable<PrintJobLog>
    {
        public PrintJobLogCollection()
        {

        }

        public static implicit operator List<PrintJobLog>(PrintJobLogCollection coll)
        {
            List<PrintJobLog> list = new List<PrintJobLog>();

            foreach (PrintJobLog emp in coll)
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
                return PrintJobLogMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrintJobLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PrintJobLog(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PrintJobLog();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PrintJobLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrintJobLogQuery();
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
        public bool Load(PrintJobLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PrintJobLog AddNew()
        {
            PrintJobLog entity = base.AddNewEntity() as PrintJobLog;

            return entity;
        }
        public PrintJobLog FindByPrimaryKey(Int64 printNo)
        {
            return base.FindByPrimaryKey(printNo) as PrintJobLog;
        }

        #region IEnumerable< PrintJobLog> Members

        IEnumerator<PrintJobLog> IEnumerable<PrintJobLog>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PrintJobLog;
            }
        }

        #endregion

        private PrintJobLogQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PrintJobLog' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PrintJobLog ({PrintNo})")]
    [Serializable]
    public partial class PrintJobLog : esPrintJobLog
    {
        public PrintJobLog()
        {
        }

        public PrintJobLog(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PrintJobLogMetadata.Meta();
            }
        }

        override protected esPrintJobLogQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrintJobLogQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PrintJobLogQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrintJobLogQuery();
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
        public bool Load(PrintJobLogQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PrintJobLogQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PrintJobLogQuery : esPrintJobLogQuery
    {
        public PrintJobLogQuery()
        {

        }

        public PrintJobLogQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PrintJobLogQuery";
        }
    }

    [Serializable]
    public partial class PrintJobLogMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PrintJobLogMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.PrintNo, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.PrintNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.PrintDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.PrintDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.ProgramID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.ProgramID;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.PrinterID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.PrinterID;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.UserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.UserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.ZplCommand, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.ZplCommand;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.IsFailed, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.IsFailed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.FailedMessage, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.FailedMessage;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.ApplicationID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.ApplicationID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobLogMetadata.ColumnNames.UserHostName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobLogMetadata.PropertyNames.UserHostName;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PrintJobLogMetadata Meta()
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
            public const string PrintNo = "PrintNo";
            public const string PrintDateTime = "PrintDateTime";
            public const string ProgramID = "ProgramID";
            public const string PrinterID = "PrinterID";
            public const string UserID = "UserID";
            public const string ZplCommand = "ZplCommand";
            public const string IsFailed = "IsFailed";
            public const string FailedMessage = "FailedMessage";
            public const string ApplicationID = "ApplicationID";
            public const string UserHostName = "UserHostName";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrintNo = "PrintNo";
            public const string PrintDateTime = "PrintDateTime";
            public const string ProgramID = "ProgramID";
            public const string PrinterID = "PrinterID";
            public const string UserID = "UserID";
            public const string ZplCommand = "ZplCommand";
            public const string IsFailed = "IsFailed";
            public const string FailedMessage = "FailedMessage";
            public const string ApplicationID = "ApplicationID";
            public const string UserHostName = "UserHostName";
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
            lock (typeof(PrintJobLogMetadata))
            {
                if (PrintJobLogMetadata.mapDelegates == null)
                {
                    PrintJobLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PrintJobLogMetadata.meta == null)
                {
                    PrintJobLogMetadata.meta = new PrintJobLogMetadata();
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

                meta.AddTypeMap("PrintNo", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("PrintDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PrinterID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZplCommand", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsFailed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("FailedMessage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ApplicationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UserHostName", new esTypeMap("varchar", "System.String"));


                meta.Source = "PrintJobLog";
                meta.Destination = "PrintJobLog";
                meta.spInsert = "proc_PrintJobLogInsert";
                meta.spUpdate = "proc_PrintJobLogUpdate";
                meta.spDelete = "proc_PrintJobLogDelete";
                meta.spLoadAll = "proc_PrintJobLogLoadAll";
                meta.spLoadByPrimaryKey = "proc_PrintJobLogLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PrintJobLogMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
