/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/16/18 9:00:27 PM
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
    abstract public class esPrintJobCollection : esEntityCollectionWAuditLog
    {
        public esPrintJobCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PrintJobCollection";
        }

        #region Query Logic
        protected void InitQuery(esPrintJobQuery query)
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
            this.InitQuery(query as esPrintJobQuery);
        }
        #endregion

        virtual public PrintJob DetachEntity(PrintJob entity)
        {
            return base.DetachEntity(entity) as PrintJob;
        }

        virtual public PrintJob AttachEntity(PrintJob entity)
        {
            return base.AttachEntity(entity) as PrintJob;
        }

        virtual public void Combine(PrintJobCollection collection)
        {
            base.Combine(collection);
        }

        new public PrintJob this[int index]
        {
            get
            {
                return base[index] as PrintJob;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PrintJob);
        }
    }

    [Serializable]
    abstract public class esPrintJob : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPrintJobQuery GetDynamicQuery()
        {
            return null;
        }

        public esPrintJob()
        {
        }

        public esPrintJob(DataRow row)
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
            esPrintJobQuery query = this.GetDynamicQuery();
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
        /// Maps to PrintJob.PrintNo
        /// </summary>
        virtual public System.Int64? PrintNo
        {
            get
            {
                return base.GetSystemInt64(PrintJobMetadata.ColumnNames.PrintNo);
            }

            set
            {
                base.SetSystemInt64(PrintJobMetadata.ColumnNames.PrintNo, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.PrintDateTime
        /// </summary>
        virtual public System.DateTime? PrintDateTime
        {
            get
            {
                return base.GetSystemDateTime(PrintJobMetadata.ColumnNames.PrintDateTime);
            }

            set
            {
                base.SetSystemDateTime(PrintJobMetadata.ColumnNames.PrintDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.ProgramID
        /// </summary>
        virtual public System.String ProgramID
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.ProgramID);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.ProgramID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.PrinterID
        /// </summary>
        virtual public System.String PrinterID
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.PrinterID);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.PrinterID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.UserID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.ZplCommand
        /// </summary>
        virtual public System.String ZplCommand
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.ZplCommand);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.ZplCommand, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.ApplicationID
        /// </summary>
        virtual public System.String ApplicationID
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.ApplicationID);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.ApplicationID, value);
            }
        }
        /// <summary>
        /// Maps to PrintJob.UserHostName
        /// </summary>
        virtual public System.String UserHostName
        {
            get
            {
                return base.GetSystemString(PrintJobMetadata.ColumnNames.UserHostName);
            }

            set
            {
                base.SetSystemString(PrintJobMetadata.ColumnNames.UserHostName, value);
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
            public esStrings(esPrintJob entity)
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
            private esPrintJob entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPrintJobQuery query)
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
                throw new Exception("esPrintJob can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PrintJob : esPrintJob
    {
    }

    [Serializable]
    abstract public class esPrintJobQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PrintJobMetadata.Meta();
            }
        }

        public esQueryItem PrintNo
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.PrintNo, esSystemType.Int64);
            }
        }

        public esQueryItem PrintDateTime
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.PrintDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ProgramID
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.ProgramID, esSystemType.String);
            }
        }

        public esQueryItem PrinterID
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.PrinterID, esSystemType.String);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem ZplCommand
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.ZplCommand, esSystemType.String);
            }
        }

        public esQueryItem ApplicationID
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.ApplicationID, esSystemType.String);
            }
        }

        public esQueryItem UserHostName
        {
            get
            {
                return new esQueryItem(this, PrintJobMetadata.ColumnNames.UserHostName, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PrintJobCollection")]
    public partial class PrintJobCollection : esPrintJobCollection, IEnumerable<PrintJob>
    {
        public PrintJobCollection()
        {

        }

        public static implicit operator List<PrintJob>(PrintJobCollection coll)
        {
            List<PrintJob> list = new List<PrintJob>();

            foreach (PrintJob emp in coll)
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
                return PrintJobMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrintJobQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PrintJob(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PrintJob();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PrintJobQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrintJobQuery();
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
        public bool Load(PrintJobQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PrintJob AddNew()
        {
            PrintJob entity = base.AddNewEntity() as PrintJob;

            return entity;
        }
        public PrintJob FindByPrimaryKey(Int64 printNo)
        {
            return base.FindByPrimaryKey(printNo) as PrintJob;
        }

        #region IEnumerable< PrintJob> Members

        IEnumerator<PrintJob> IEnumerable<PrintJob>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PrintJob;
            }
        }

        #endregion

        private PrintJobQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PrintJob' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PrintJob ({PrintNo})")]
    [Serializable]
    public partial class PrintJob : esPrintJob
    {
        public PrintJob()
        {
        }

        public PrintJob(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PrintJobMetadata.Meta();
            }
        }

        override protected esPrintJobQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PrintJobQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PrintJobQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PrintJobQuery();
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
        public bool Load(PrintJobQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PrintJobQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PrintJobQuery : esPrintJobQuery
    {
        public PrintJobQuery()
        {

        }

        public PrintJobQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PrintJobQuery";
        }
    }

    [Serializable]
    public partial class PrintJobMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PrintJobMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.PrintNo, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = PrintJobMetadata.PropertyNames.PrintNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.PrintDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PrintJobMetadata.PropertyNames.PrintDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.ProgramID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.ProgramID;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.PrinterID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.PrinterID;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.UserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.UserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.ZplCommand, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.ZplCommand;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.ApplicationID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.ApplicationID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PrintJobMetadata.ColumnNames.UserHostName, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PrintJobMetadata.PropertyNames.UserHostName;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PrintJobMetadata Meta()
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
            lock (typeof(PrintJobMetadata))
            {
                if (PrintJobMetadata.mapDelegates == null)
                {
                    PrintJobMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PrintJobMetadata.meta == null)
                {
                    PrintJobMetadata.meta = new PrintJobMetadata();
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
                meta.AddTypeMap("ApplicationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UserHostName", new esTypeMap("varchar", "System.String"));


                meta.Source = "PrintJob";
                meta.Destination = "PrintJob";
                meta.spInsert = "proc_PrintJobInsert";
                meta.spUpdate = "proc_PrintJobUpdate";
                meta.spDelete = "proc_PrintJobDelete";
                meta.spLoadAll = "proc_PrintJobLoadAll";
                meta.spLoadByPrimaryKey = "proc_PrintJobLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PrintJobMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
