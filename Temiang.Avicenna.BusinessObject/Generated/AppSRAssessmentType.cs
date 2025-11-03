/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/30/19 6:51:40 PM
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
    abstract public class esAppSRAssessmentTypeCollection : esEntityCollectionWAuditLog
    {
        public esAppSRAssessmentTypeCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppSRAssessmentTypeCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppSRAssessmentTypeQuery query)
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
            this.InitQuery(query as esAppSRAssessmentTypeQuery);
        }
        #endregion

        virtual public AppSRAssessmentType DetachEntity(AppSRAssessmentType entity)
        {
            return base.DetachEntity(entity) as AppSRAssessmentType;
        }

        virtual public AppSRAssessmentType AttachEntity(AppSRAssessmentType entity)
        {
            return base.AttachEntity(entity) as AppSRAssessmentType;
        }

        virtual public void Combine(AppSRAssessmentTypeCollection collection)
        {
            base.Combine(collection);
        }

        new public AppSRAssessmentType this[int index]
        {
            get
            {
                return base[index] as AppSRAssessmentType;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppSRAssessmentType);
        }
    }

    [Serializable]
    abstract public class esAppSRAssessmentType : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppSRAssessmentTypeQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppSRAssessmentType()
        {
        }

        public esAppSRAssessmentType(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRAssessmentType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAssessmentType);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAssessmentType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRAssessmentType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAssessmentType);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAssessmentType);
        }

        private bool LoadByPrimaryKeyDynamic(String sRAssessmentType)
        {
            esAppSRAssessmentTypeQuery query = this.GetDynamicQuery();
            query.Where(query.SRAssessmentType == sRAssessmentType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRAssessmentType)
        {
            esParameters parms = new esParameters();
            parms.Add("SRAssessmentType", sRAssessmentType);
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
                        case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
                        case "ContinuedSRAssessmentType": this.str.ContinuedSRAssessmentType = (string)value; break;
                        case "IsInitialAssessment": this.str.IsInitialAssessment = (string)value; break;
                        case "IsSingleEntry": this.str.IsSingleEntry = (string)value; break;
                        case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
                        case "ReportProgramID": this.str.ReportProgramID = (string)value; break;
                        case "NursingQuestionFormID": this.str.NursingQuestionFormID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsInitialAssessment":

                            if (value == null || value is System.Boolean)
                                this.IsInitialAssessment = (System.Boolean?)value;
                            break;
                        case "IsSingleEntry":

                            if (value == null || value is System.Boolean)
                                this.IsSingleEntry = (System.Boolean?)value;
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
        /// Maps to AppSRAssessmentType.SRAssessmentType
        /// </summary>
        virtual public System.String SRAssessmentType
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.SRAssessmentType);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.SRAssessmentType, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.ContinuedSRAssessmentType
        /// </summary>
        virtual public System.String ContinuedSRAssessmentType
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.ContinuedSRAssessmentType);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.ContinuedSRAssessmentType, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.IsInitialAssessment
        /// </summary>
        virtual public System.Boolean? IsInitialAssessment
        {
            get
            {
                return base.GetSystemBoolean(AppSRAssessmentTypeMetadata.ColumnNames.IsInitialAssessment);
            }

            set
            {
                base.SetSystemBoolean(AppSRAssessmentTypeMetadata.ColumnNames.IsInitialAssessment, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.IsSingleEntry
        /// </summary>
        virtual public System.Boolean? IsSingleEntry
        {
            get
            {
                return base.GetSystemBoolean(AppSRAssessmentTypeMetadata.ColumnNames.IsSingleEntry);
            }

            set
            {
                base.SetSystemBoolean(AppSRAssessmentTypeMetadata.ColumnNames.IsSingleEntry, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.SRRegistrationType
        /// </summary>
        virtual public System.String SRRegistrationType
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.SRRegistrationType);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.SRRegistrationType, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.ReportProgramID
        /// </summary>
        virtual public System.String ReportProgramID
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.ReportProgramID);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.ReportProgramID, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.NursingQuestionFormID
        /// </summary>
        virtual public System.String NursingQuestionFormID
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.NursingQuestionFormID);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.NursingQuestionFormID, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AppSRAssessmentType.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAppSRAssessmentType entity)
            {
                this.entity = entity;
            }
            public System.String SRAssessmentType
            {
                get
                {
                    System.String data = entity.SRAssessmentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAssessmentType = null;
                    else entity.SRAssessmentType = Convert.ToString(value);
                }
            }
            public System.String ContinuedSRAssessmentType
            {
                get
                {
                    System.String data = entity.ContinuedSRAssessmentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContinuedSRAssessmentType = null;
                    else entity.ContinuedSRAssessmentType = Convert.ToString(value);
                }
            }
            public System.String IsInitialAssessment
            {
                get
                {
                    System.Boolean? data = entity.IsInitialAssessment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInitialAssessment = null;
                    else entity.IsInitialAssessment = Convert.ToBoolean(value);
                }
            }
            public System.String IsSingleEntry
            {
                get
                {
                    System.Boolean? data = entity.IsSingleEntry;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSingleEntry = null;
                    else entity.IsSingleEntry = Convert.ToBoolean(value);
                }
            }
            public System.String SRRegistrationType
            {
                get
                {
                    System.String data = entity.SRRegistrationType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRegistrationType = null;
                    else entity.SRRegistrationType = Convert.ToString(value);
                }
            }
            public System.String ReportProgramID
            {
                get
                {
                    System.String data = entity.ReportProgramID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReportProgramID = null;
                    else entity.ReportProgramID = Convert.ToString(value);
                }
            }
            public System.String NursingQuestionFormID
            {
                get
                {
                    System.String data = entity.NursingQuestionFormID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NursingQuestionFormID = null;
                    else entity.NursingQuestionFormID = Convert.ToString(value);
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
            private esAppSRAssessmentType entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppSRAssessmentTypeQuery query)
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
                throw new Exception("esAppSRAssessmentType can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppSRAssessmentType : esAppSRAssessmentType
    {
    }

    [Serializable]
    abstract public class esAppSRAssessmentTypeQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppSRAssessmentTypeMetadata.Meta();
            }
        }

        public esQueryItem SRAssessmentType
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
            }
        }

        public esQueryItem ContinuedSRAssessmentType
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.ContinuedSRAssessmentType, esSystemType.String);
            }
        }

        public esQueryItem IsInitialAssessment
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.IsInitialAssessment, esSystemType.Boolean);
            }
        }

        public esQueryItem IsSingleEntry
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.IsSingleEntry, esSystemType.Boolean);
            }
        }

        public esQueryItem SRRegistrationType
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
            }
        }

        public esQueryItem ReportProgramID
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.ReportProgramID, esSystemType.String);
            }
        }

        public esQueryItem NursingQuestionFormID
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.NursingQuestionFormID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppSRAssessmentTypeCollection")]
    public partial class AppSRAssessmentTypeCollection : esAppSRAssessmentTypeCollection, IEnumerable<AppSRAssessmentType>
    {
        public AppSRAssessmentTypeCollection()
        {

        }

        public static implicit operator List<AppSRAssessmentType>(AppSRAssessmentTypeCollection coll)
        {
            List<AppSRAssessmentType> list = new List<AppSRAssessmentType>();

            foreach (AppSRAssessmentType emp in coll)
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
                return AppSRAssessmentTypeMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppSRAssessmentTypeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppSRAssessmentType(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppSRAssessmentType();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppSRAssessmentTypeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppSRAssessmentTypeQuery();
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
        public bool Load(AppSRAssessmentTypeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppSRAssessmentType AddNew()
        {
            AppSRAssessmentType entity = base.AddNewEntity() as AppSRAssessmentType;

            return entity;
        }
        public AppSRAssessmentType FindByPrimaryKey(String sRAssessmentType)
        {
            return base.FindByPrimaryKey(sRAssessmentType) as AppSRAssessmentType;
        }

        #region IEnumerable< AppSRAssessmentType> Members

        IEnumerator<AppSRAssessmentType> IEnumerable<AppSRAssessmentType>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppSRAssessmentType;
            }
        }

        #endregion

        private AppSRAssessmentTypeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppSRAssessmentType' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppSRAssessmentType ({SRAssessmentType})")]
    [Serializable]
    public partial class AppSRAssessmentType : esAppSRAssessmentType
    {
        public AppSRAssessmentType()
        {
        }

        public AppSRAssessmentType(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppSRAssessmentTypeMetadata.Meta();
            }
        }

        override protected esAppSRAssessmentTypeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppSRAssessmentTypeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppSRAssessmentTypeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppSRAssessmentTypeQuery();
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
        public bool Load(AppSRAssessmentTypeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppSRAssessmentTypeQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppSRAssessmentTypeQuery : esAppSRAssessmentTypeQuery
    {
        public AppSRAssessmentTypeQuery()
        {

        }

        public AppSRAssessmentTypeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppSRAssessmentTypeQuery";
        }
    }

    [Serializable]
    public partial class AppSRAssessmentTypeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppSRAssessmentTypeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.SRAssessmentType, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.SRAssessmentType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.ContinuedSRAssessmentType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.ContinuedSRAssessmentType;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.IsInitialAssessment, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.IsInitialAssessment;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.IsSingleEntry, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.IsSingleEntry;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.SRRegistrationType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.SRRegistrationType;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.ReportProgramID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.ReportProgramID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.NursingQuestionFormID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.NursingQuestionFormID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppSRAssessmentTypeMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = AppSRAssessmentTypeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppSRAssessmentTypeMetadata Meta()
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
            public const string SRAssessmentType = "SRAssessmentType";
            public const string ContinuedSRAssessmentType = "ContinuedSRAssessmentType";
            public const string IsInitialAssessment = "IsInitialAssessment";
            public const string IsSingleEntry = "IsSingleEntry";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string ReportProgramID = "ReportProgramID";
            public const string NursingQuestionFormID = "NursingQuestionFormID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRAssessmentType = "SRAssessmentType";
            public const string ContinuedSRAssessmentType = "ContinuedSRAssessmentType";
            public const string IsInitialAssessment = "IsInitialAssessment";
            public const string IsSingleEntry = "IsSingleEntry";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string ReportProgramID = "ReportProgramID";
            public const string NursingQuestionFormID = "NursingQuestionFormID";
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
            lock (typeof(AppSRAssessmentTypeMetadata))
            {
                if (AppSRAssessmentTypeMetadata.mapDelegates == null)
                {
                    AppSRAssessmentTypeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppSRAssessmentTypeMetadata.meta == null)
                {
                    AppSRAssessmentTypeMetadata.meta = new AppSRAssessmentTypeMetadata();
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

                meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContinuedSRAssessmentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInitialAssessment", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsSingleEntry", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReportProgramID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NursingQuestionFormID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppSRAssessmentType";
                meta.Destination = "AppSRAssessmentType";
                meta.spInsert = "proc_AppSRAssessmentTypeInsert";
                meta.spUpdate = "proc_AppSRAssessmentTypeUpdate";
                meta.spDelete = "proc_AppSRAssessmentTypeDelete";
                meta.spLoadAll = "proc_AppSRAssessmentTypeLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppSRAssessmentTypeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppSRAssessmentTypeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
