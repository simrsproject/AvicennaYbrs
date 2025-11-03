/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2017 1:19:55 PM
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
    abstract public class esAppReportParameterHealthcareCollection : esEntityCollectionWAuditLog
    {
        public esAppReportParameterHealthcareCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppReportParameterHealthcareCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppReportParameterHealthcareQuery query)
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
            this.InitQuery(query as esAppReportParameterHealthcareQuery);
        }
        #endregion

        virtual public AppReportParameterHealthcare DetachEntity(AppReportParameterHealthcare entity)
        {
            return base.DetachEntity(entity) as AppReportParameterHealthcare;
        }

        virtual public AppReportParameterHealthcare AttachEntity(AppReportParameterHealthcare entity)
        {
            return base.AttachEntity(entity) as AppReportParameterHealthcare;
        }

        virtual public void Combine(AppReportParameterHealthcareCollection collection)
        {
            base.Combine(collection);
        }

        new public AppReportParameterHealthcare this[int index]
        {
            get
            {
                return base[index] as AppReportParameterHealthcare;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppReportParameterHealthcare);
        }
    }

    [Serializable]
    abstract public class esAppReportParameterHealthcare : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppReportParameterHealthcareQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppReportParameterHealthcare()
        {
        }

        public esAppReportParameterHealthcare(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String programID, String healthcareInitial, String parameterName)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(programID, healthcareInitial, parameterName);
            else
                return LoadByPrimaryKeyStoredProcedure(programID, healthcareInitial, parameterName);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID, String healthcareInitial, String parameterName)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(programID, healthcareInitial, parameterName);
            else
                return LoadByPrimaryKeyStoredProcedure(programID, healthcareInitial, parameterName);
        }

        private bool LoadByPrimaryKeyDynamic(String programID, String healthcareInitial, String parameterName)
        {
            esAppReportParameterHealthcareQuery query = this.GetDynamicQuery();
            query.Where(query.ProgramID == programID, query.HealthcareInitial == healthcareInitial, query.ParameterName == parameterName);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String programID, String healthcareInitial, String parameterName)
        {
            esParameters parms = new esParameters();
            parms.Add("ProgramID", programID);
            parms.Add("HealthcareInitial", healthcareInitial);
            parms.Add("ParameterName", parameterName);
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
                        case "ParameterName": this.str.ParameterName = (string)value; break;
                        case "ReportControlName": this.str.ReportControlName = (string)value; break;
                        case "IndexNo": this.str.IndexNo = (string)value; break;
                        case "ParameterCaption": this.str.ParameterCaption = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IndexNo":

                            if (value == null || value is System.Int32)
                                this.IndexNo = (System.Int32?)value;
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
        /// Maps to AppReportParameterHealthcare.ProgramID
        /// </summary>
        virtual public System.String ProgramID
        {
            get
            {
                return base.GetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ProgramID);
            }

            set
            {
                base.SetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ProgramID, value);
            }
        }
        /// <summary>
        /// Maps to AppReportParameterHealthcare.HealthcareInitial
        /// </summary>
        virtual public System.String HealthcareInitial
        {
            get
            {
                return base.GetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.HealthcareInitial);
            }

            set
            {
                base.SetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.HealthcareInitial, value);
            }
        }
        /// <summary>
        /// Maps to AppReportParameterHealthcare.ParameterName
        /// </summary>
        virtual public System.String ParameterName
        {
            get
            {
                return base.GetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ParameterName);
            }

            set
            {
                base.SetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ParameterName, value);
            }
        }
        /// <summary>
        /// Maps to AppReportParameterHealthcare.ReportControlName
        /// </summary>
        virtual public System.String ReportControlName
        {
            get
            {
                return base.GetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ReportControlName);
            }

            set
            {
                base.SetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ReportControlName, value);
            }
        }
        /// <summary>
        /// Maps to AppReportParameterHealthcare.IndexNo
        /// </summary>
        virtual public System.Int32? IndexNo
        {
            get
            {
                return base.GetSystemInt32(AppReportParameterHealthcareMetadata.ColumnNames.IndexNo);
            }

            set
            {
                base.SetSystemInt32(AppReportParameterHealthcareMetadata.ColumnNames.IndexNo, value);
            }
        }
        /// <summary>
        /// Maps to AppReportParameterHealthcare.ParameterCaption
        /// </summary>
        virtual public System.String ParameterCaption
        {
            get
            {
                return base.GetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ParameterCaption);
            }

            set
            {
                base.SetSystemString(AppReportParameterHealthcareMetadata.ColumnNames.ParameterCaption, value);
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
            public esStrings(esAppReportParameterHealthcare entity)
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
            public System.String ParameterName
            {
                get
                {
                    System.String data = entity.ParameterName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParameterName = null;
                    else entity.ParameterName = Convert.ToString(value);
                }
            }
            public System.String ReportControlName
            {
                get
                {
                    System.String data = entity.ReportControlName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReportControlName = null;
                    else entity.ReportControlName = Convert.ToString(value);
                }
            }
            public System.String IndexNo
            {
                get
                {
                    System.Int32? data = entity.IndexNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IndexNo = null;
                    else entity.IndexNo = Convert.ToInt32(value);
                }
            }
            public System.String ParameterCaption
            {
                get
                {
                    System.String data = entity.ParameterCaption;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParameterCaption = null;
                    else entity.ParameterCaption = Convert.ToString(value);
                }
            }
            private esAppReportParameterHealthcare entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppReportParameterHealthcareQuery query)
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
                throw new Exception("esAppReportParameterHealthcare can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppReportParameterHealthcare : esAppReportParameterHealthcare
    {
    }

    [Serializable]
    abstract public class esAppReportParameterHealthcareQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppReportParameterHealthcareMetadata.Meta();
            }
        }

        public esQueryItem ProgramID
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.ProgramID, esSystemType.String);
            }
        }

        public esQueryItem HealthcareInitial
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.HealthcareInitial, esSystemType.String);
            }
        }

        public esQueryItem ParameterName
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.ParameterName, esSystemType.String);
            }
        }

        public esQueryItem ReportControlName
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.ReportControlName, esSystemType.String);
            }
        }

        public esQueryItem IndexNo
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.IndexNo, esSystemType.Int32);
            }
        }

        public esQueryItem ParameterCaption
        {
            get
            {
                return new esQueryItem(this, AppReportParameterHealthcareMetadata.ColumnNames.ParameterCaption, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppReportParameterHealthcareCollection")]
    public partial class AppReportParameterHealthcareCollection : esAppReportParameterHealthcareCollection, IEnumerable<AppReportParameterHealthcare>
    {
        public AppReportParameterHealthcareCollection()
        {

        }

        public static implicit operator List<AppReportParameterHealthcare>(AppReportParameterHealthcareCollection coll)
        {
            List<AppReportParameterHealthcare> list = new List<AppReportParameterHealthcare>();

            foreach (AppReportParameterHealthcare emp in coll)
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
                return AppReportParameterHealthcareMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppReportParameterHealthcareQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppReportParameterHealthcare(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppReportParameterHealthcare();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppReportParameterHealthcareQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppReportParameterHealthcareQuery();
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
        public bool Load(AppReportParameterHealthcareQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppReportParameterHealthcare AddNew()
        {
            AppReportParameterHealthcare entity = base.AddNewEntity() as AppReportParameterHealthcare;

            return entity;
        }
        public AppReportParameterHealthcare FindByPrimaryKey(String programID, String healthcareInitial, String parameterName)
        {
            return base.FindByPrimaryKey(programID, healthcareInitial, parameterName) as AppReportParameterHealthcare;
        }

        #region IEnumerable< AppReportParameterHealthcare> Members

        IEnumerator<AppReportParameterHealthcare> IEnumerable<AppReportParameterHealthcare>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppReportParameterHealthcare;
            }
        }

        #endregion

        private AppReportParameterHealthcareQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppReportParameterHealthcare' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppReportParameterHealthcare ({ProgramID, HealthcareInitial, ParameterName})")]
    [Serializable]
    public partial class AppReportParameterHealthcare : esAppReportParameterHealthcare
    {
        public AppReportParameterHealthcare()
        {
        }

        public AppReportParameterHealthcare(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppReportParameterHealthcareMetadata.Meta();
            }
        }

        override protected esAppReportParameterHealthcareQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppReportParameterHealthcareQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppReportParameterHealthcareQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppReportParameterHealthcareQuery();
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
        public bool Load(AppReportParameterHealthcareQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppReportParameterHealthcareQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppReportParameterHealthcareQuery : esAppReportParameterHealthcareQuery
    {
        public AppReportParameterHealthcareQuery()
        {

        }

        public AppReportParameterHealthcareQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppReportParameterHealthcareQuery";
        }
    }

    [Serializable]
    public partial class AppReportParameterHealthcareMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppReportParameterHealthcareMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.ProgramID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            _columns.Add(c);

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.HealthcareInitial, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.HealthcareInitial;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.ParameterName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.ParameterName;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.ReportControlName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.ReportControlName;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.IndexNo, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.IndexNo;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AppReportParameterHealthcareMetadata.ColumnNames.ParameterCaption, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AppReportParameterHealthcareMetadata.PropertyNames.ParameterCaption;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);


        }
        #endregion

        static public AppReportParameterHealthcareMetadata Meta()
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
            public const string ParameterName = "ParameterName";
            public const string ReportControlName = "ReportControlName";
            public const string IndexNo = "IndexNo";
            public const string ParameterCaption = "ParameterCaption";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProgramID = "ProgramID";
            public const string HealthcareInitial = "HealthcareInitial";
            public const string ParameterName = "ParameterName";
            public const string ReportControlName = "ReportControlName";
            public const string IndexNo = "IndexNo";
            public const string ParameterCaption = "ParameterCaption";
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
            lock (typeof(AppReportParameterHealthcareMetadata))
            {
                if (AppReportParameterHealthcareMetadata.mapDelegates == null)
                {
                    AppReportParameterHealthcareMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppReportParameterHealthcareMetadata.meta == null)
                {
                    AppReportParameterHealthcareMetadata.meta = new AppReportParameterHealthcareMetadata();
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
                meta.AddTypeMap("ParameterName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReportControlName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ParameterCaption", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppReportParameterHealthcare";
                meta.Destination = "AppReportParameterHealthcare";
                meta.spInsert = "proc_AppReportParameterHealthcareInsert";
                meta.spUpdate = "proc_AppReportParameterHealthcareUpdate";
                meta.spDelete = "proc_AppReportParameterHealthcareDelete";
                meta.spLoadAll = "proc_AppReportParameterHealthcareLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppReportParameterHealthcareLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppReportParameterHealthcareMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
