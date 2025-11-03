/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2017 7:44:34 PM
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
    abstract public class esAppProgramHealthcareCollection : esEntityCollectionWAuditLog
    {
        public esAppProgramHealthcareCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppProgramHealthcareCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppProgramHealthcareQuery query)
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
            this.InitQuery(query as esAppProgramHealthcareQuery);
        }
        #endregion

        virtual public AppProgramHealthcare DetachEntity(AppProgramHealthcare entity)
        {
            return base.DetachEntity(entity) as AppProgramHealthcare;
        }

        virtual public AppProgramHealthcare AttachEntity(AppProgramHealthcare entity)
        {
            return base.AttachEntity(entity) as AppProgramHealthcare;
        }

        virtual public void Combine(AppProgramHealthcareCollection collection)
        {
            base.Combine(collection);
        }

        new public AppProgramHealthcare this[int index]
        {
            get
            {
                return base[index] as AppProgramHealthcare;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppProgramHealthcare);
        }
    }

    [Serializable]
    abstract public class esAppProgramHealthcare : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppProgramHealthcareQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppProgramHealthcare()
        {
        }

        public esAppProgramHealthcare(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String programID, String healthcareInitial)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(programID, healthcareInitial);
            else
                return LoadByPrimaryKeyStoredProcedure(programID, healthcareInitial);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID, String healthcareInitial)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(programID, healthcareInitial);
            else
                return LoadByPrimaryKeyStoredProcedure(programID, healthcareInitial);
        }

        private bool LoadByPrimaryKeyDynamic(String programID, String healthcareInitial)
        {
            esAppProgramHealthcareQuery query = this.GetDynamicQuery();
            query.Where(query.ProgramID == programID, query.HealthcareInitial == healthcareInitial);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String programID, String healthcareInitial)
        {
            esParameters parms = new esParameters();
            parms.Add("ProgramID", programID);
            parms.Add("HealthcareInitial", healthcareInitial);
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
                        case "ProgramID": this.str.ProgramID = (string)value; break;
                        case "HealthcareInitial": this.str.HealthcareInitial = (string)value; break;
                        case "NavigateUrl": this.str.NavigateUrl = (string)value; break;
                        case "AssemblyName": this.str.AssemblyName = (string)value; break;
                        case "AssemblyClassName": this.str.AssemblyClassName = (string)value; break;
                        case "StoreProcedureName": this.str.StoreProcedureName = (string)value; break;
                        case "ProgramType": this.str.ProgramType = (string)value; break;
                        case "IsUsingReportHeader": this.str.IsUsingReportHeader = (string)value; break;
                        case "IsDirectPrintEnable": this.str.IsDirectPrintEnable = (string)value; break;
                        case "HelpLinkID": this.str.HelpLinkID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsUsingReportHeader":

                            if (value == null || value is System.Boolean)
                                this.IsUsingReportHeader = (System.Boolean?)value;
                            break;
                        case "IsDirectPrintEnable":

                            if (value == null || value is System.Boolean)
                                this.IsDirectPrintEnable = (System.Boolean?)value;
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
        /// Maps to AppProgramHealthcare.ProgramID
        /// </summary>
        virtual public System.String ProgramID
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.ProgramID);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.ProgramID, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.HealthcareInitial
        /// </summary>
        virtual public System.String HealthcareInitial
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.HealthcareInitial);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.HealthcareInitial, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.NavigateUrl
        /// </summary>
        virtual public System.String NavigateUrl
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.NavigateUrl);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.NavigateUrl, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.AssemblyName
        /// </summary>
        virtual public System.String AssemblyName
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.AssemblyName);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.AssemblyName, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.AssemblyClassName
        /// </summary>
        virtual public System.String AssemblyClassName
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.AssemblyClassName);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.AssemblyClassName, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.StoreProcedureName
        /// </summary>
        virtual public System.String StoreProcedureName
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.StoreProcedureName);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.StoreProcedureName, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.ProgramType
        /// </summary>
        virtual public System.String ProgramType
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.ProgramType);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.ProgramType, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.IsUsingReportHeader
        /// </summary>
        virtual public System.Boolean? IsUsingReportHeader
        {
            get
            {
                return base.GetSystemBoolean(AppProgramHealthcareMetadata.ColumnNames.IsUsingReportHeader);
            }

            set
            {
                base.SetSystemBoolean(AppProgramHealthcareMetadata.ColumnNames.IsUsingReportHeader, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.IsDirectPrintEnable
        /// </summary>
        virtual public System.Boolean? IsDirectPrintEnable
        {
            get
            {
                return base.GetSystemBoolean(AppProgramHealthcareMetadata.ColumnNames.IsDirectPrintEnable);
            }

            set
            {
                base.SetSystemBoolean(AppProgramHealthcareMetadata.ColumnNames.IsDirectPrintEnable, value);
            }
        }
        /// <summary>
        /// Maps to AppProgramHealthcare.HelpLinkID
        /// </summary>
        virtual public System.String HelpLinkID
        {
            get
            {
                return base.GetSystemString(AppProgramHealthcareMetadata.ColumnNames.HelpLinkID);
            }

            set
            {
                base.SetSystemString(AppProgramHealthcareMetadata.ColumnNames.HelpLinkID, value);
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
            public esStrings(esAppProgramHealthcare entity)
            {
                this.entity = entity;
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
            public System.String HealthcareInitial
            {
                get
                {
                    System.String data = entity.HealthcareInitial;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HealthcareInitial = null;
                    else entity.HealthcareInitial = Convert.ToString(value);
                }
            }
            public System.String NavigateUrl
            {
                get
                {
                    System.String data = entity.NavigateUrl;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NavigateUrl = null;
                    else entity.NavigateUrl = Convert.ToString(value);
                }
            }
            public System.String AssemblyName
            {
                get
                {
                    System.String data = entity.AssemblyName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssemblyName = null;
                    else entity.AssemblyName = Convert.ToString(value);
                }
            }
            public System.String AssemblyClassName
            {
                get
                {
                    System.String data = entity.AssemblyClassName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssemblyClassName = null;
                    else entity.AssemblyClassName = Convert.ToString(value);
                }
            }
            public System.String StoreProcedureName
            {
                get
                {
                    System.String data = entity.StoreProcedureName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StoreProcedureName = null;
                    else entity.StoreProcedureName = Convert.ToString(value);
                }
            }
            public System.String ProgramType
            {
                get
                {
                    System.String data = entity.ProgramType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProgramType = null;
                    else entity.ProgramType = Convert.ToString(value);
                }
            }
            public System.String IsUsingReportHeader
            {
                get
                {
                    System.Boolean? data = entity.IsUsingReportHeader;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsingReportHeader = null;
                    else entity.IsUsingReportHeader = Convert.ToBoolean(value);
                }
            }
            public System.String IsDirectPrintEnable
            {
                get
                {
                    System.Boolean? data = entity.IsDirectPrintEnable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDirectPrintEnable = null;
                    else entity.IsDirectPrintEnable = Convert.ToBoolean(value);
                }
            }
            public System.String HelpLinkID
            {
                get
                {
                    System.String data = entity.HelpLinkID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HelpLinkID = null;
                    else entity.HelpLinkID = Convert.ToString(value);
                }
            }
            private esAppProgramHealthcare entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppProgramHealthcareQuery query)
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
                throw new Exception("esAppProgramHealthcare can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppProgramHealthcare : esAppProgramHealthcare
    {
    }

    [Serializable]
    abstract public class esAppProgramHealthcareQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppProgramHealthcareMetadata.Meta();
            }
        }

        public esQueryItem ProgramID
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.ProgramID, esSystemType.String);
            }
        }

        public esQueryItem HealthcareInitial
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.HealthcareInitial, esSystemType.String);
            }
        }

        public esQueryItem NavigateUrl
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.NavigateUrl, esSystemType.String);
            }
        }

        public esQueryItem AssemblyName
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.AssemblyName, esSystemType.String);
            }
        }

        public esQueryItem AssemblyClassName
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.AssemblyClassName, esSystemType.String);
            }
        }

        public esQueryItem StoreProcedureName
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.StoreProcedureName, esSystemType.String);
            }
        }

        public esQueryItem ProgramType
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.ProgramType, esSystemType.String);
            }
        }

        public esQueryItem IsUsingReportHeader
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.IsUsingReportHeader, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDirectPrintEnable
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.IsDirectPrintEnable, esSystemType.Boolean);
            }
        }

        public esQueryItem HelpLinkID
        {
            get
            {
                return new esQueryItem(this, AppProgramHealthcareMetadata.ColumnNames.HelpLinkID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppProgramHealthcareCollection")]
    public partial class AppProgramHealthcareCollection : esAppProgramHealthcareCollection, IEnumerable<AppProgramHealthcare>
    {
        public AppProgramHealthcareCollection()
        {

        }

        public static implicit operator List<AppProgramHealthcare>(AppProgramHealthcareCollection coll)
        {
            List<AppProgramHealthcare> list = new List<AppProgramHealthcare>();

            foreach (AppProgramHealthcare emp in coll)
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
                return AppProgramHealthcareMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppProgramHealthcareQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppProgramHealthcare(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppProgramHealthcare();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppProgramHealthcareQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppProgramHealthcareQuery();
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
        public bool Load(AppProgramHealthcareQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppProgramHealthcare AddNew()
        {
            AppProgramHealthcare entity = base.AddNewEntity() as AppProgramHealthcare;

            return entity;
        }
        public AppProgramHealthcare FindByPrimaryKey(String programID, String healthcareInitial)
        {
            return base.FindByPrimaryKey(programID, healthcareInitial) as AppProgramHealthcare;
        }

        #region IEnumerable< AppProgramHealthcare> Members

        IEnumerator<AppProgramHealthcare> IEnumerable<AppProgramHealthcare>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppProgramHealthcare;
            }
        }

        #endregion

        private AppProgramHealthcareQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppProgramHealthcare' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppProgramHealthcare ({ProgramID, HealthcareInitial})")]
    [Serializable]
    public partial class AppProgramHealthcare : esAppProgramHealthcare
    {
        public AppProgramHealthcare()
        {
        }

        public AppProgramHealthcare(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppProgramHealthcareMetadata.Meta();
            }
        }

        override protected esAppProgramHealthcareQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppProgramHealthcareQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppProgramHealthcareQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppProgramHealthcareQuery();
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
        public bool Load(AppProgramHealthcareQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppProgramHealthcareQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppProgramHealthcareQuery : esAppProgramHealthcareQuery
    {
        public AppProgramHealthcareQuery()
        {

        }

        public AppProgramHealthcareQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppProgramHealthcareQuery";
        }
    }

    [Serializable]
    public partial class AppProgramHealthcareMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppProgramHealthcareMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.ProgramID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.HealthcareInitial, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.HealthcareInitial;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.NavigateUrl, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.NavigateUrl;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.AssemblyName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.AssemblyName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.AssemblyClassName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.AssemblyClassName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.StoreProcedureName, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.StoreProcedureName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.ProgramType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.ProgramType;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.IsUsingReportHeader, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.IsUsingReportHeader;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.IsDirectPrintEnable, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.IsDirectPrintEnable;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppProgramHealthcareMetadata.ColumnNames.HelpLinkID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = AppProgramHealthcareMetadata.PropertyNames.HelpLinkID;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppProgramHealthcareMetadata Meta()
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
            public const string ProgramID = "ProgramID";
            public const string HealthcareInitial = "HealthcareInitial";
            public const string NavigateUrl = "NavigateUrl";
            public const string AssemblyName = "AssemblyName";
            public const string AssemblyClassName = "AssemblyClassName";
            public const string StoreProcedureName = "StoreProcedureName";
            public const string ProgramType = "ProgramType";
            public const string IsUsingReportHeader = "IsUsingReportHeader";
            public const string IsDirectPrintEnable = "IsDirectPrintEnable";
            public const string HelpLinkID = "HelpLinkID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProgramID = "ProgramID";
            public const string HealthcareInitial = "HealthcareInitial";
            public const string NavigateUrl = "NavigateUrl";
            public const string AssemblyName = "AssemblyName";
            public const string AssemblyClassName = "AssemblyClassName";
            public const string StoreProcedureName = "StoreProcedureName";
            public const string ProgramType = "ProgramType";
            public const string IsUsingReportHeader = "IsUsingReportHeader";
            public const string IsDirectPrintEnable = "IsDirectPrintEnable";
            public const string HelpLinkID = "HelpLinkID";
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
            lock (typeof(AppProgramHealthcareMetadata))
            {
                if (AppProgramHealthcareMetadata.mapDelegates == null)
                {
                    AppProgramHealthcareMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppProgramHealthcareMetadata.meta == null)
                {
                    AppProgramHealthcareMetadata.meta = new AppProgramHealthcareMetadata();
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

                meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HealthcareInitial", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NavigateUrl", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssemblyName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssemblyClassName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StoreProcedureName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProgramType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsUsingReportHeader", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDirectPrintEnable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("HelpLinkID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppProgramHealthcare";
                meta.Destination = "AppProgramHealthcare";
                meta.spInsert = "proc_AppProgramHealthcareInsert";
                meta.spUpdate = "proc_AppProgramHealthcareUpdate";
                meta.spDelete = "proc_AppProgramHealthcareDelete";
                meta.spLoadAll = "proc_AppProgramHealthcareLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppProgramHealthcareLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppProgramHealthcareMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
