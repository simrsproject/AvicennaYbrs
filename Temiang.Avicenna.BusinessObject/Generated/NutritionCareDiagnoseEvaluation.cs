/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/14/2019 9:32:27 PM
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
    abstract public class esNutritionCareDiagnoseEvaluationCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareDiagnoseEvaluationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareDiagnoseEvaluationCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareDiagnoseEvaluationQuery query)
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
            this.InitQuery(query as esNutritionCareDiagnoseEvaluationQuery);
        }
        #endregion

        virtual public NutritionCareDiagnoseEvaluation DetachEntity(NutritionCareDiagnoseEvaluation entity)
        {
            return base.DetachEntity(entity) as NutritionCareDiagnoseEvaluation;
        }

        virtual public NutritionCareDiagnoseEvaluation AttachEntity(NutritionCareDiagnoseEvaluation entity)
        {
            return base.AttachEntity(entity) as NutritionCareDiagnoseEvaluation;
        }

        virtual public void Combine(NutritionCareDiagnoseEvaluationCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareDiagnoseEvaluation this[int index]
        {
            get
            {
                return base[index] as NutritionCareDiagnoseEvaluation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareDiagnoseEvaluation);
        }
    }

    [Serializable]
    abstract public class esNutritionCareDiagnoseEvaluation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareDiagnoseEvaluationQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareDiagnoseEvaluation()
        {
        }

        public esNutritionCareDiagnoseEvaluation(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 iD)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 iD)
        {
            esNutritionCareDiagnoseEvaluationQuery query = this.GetDynamicQuery();
            query.Where(query.ID == iD);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
        {
            esParameters parms = new esParameters();
            parms.Add("ID", iD);
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
                        case "ID": this.str.ID = (string)value; break;
                        case "EvaluationID": this.str.EvaluationID = (string)value; break;
                        case "InterventionID": this.str.InterventionID = (string)value; break;
                        case "NutritionCareInterventionID": this.str.NutritionCareInterventionID = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ID":

                            if (value == null || value is System.Int64)
                                this.ID = (System.Int64?)value;
                            break;
                        case "EvaluationID":

                            if (value == null || value is System.Int64)
                                this.EvaluationID = (System.Int64?)value;
                            break;
                        case "InterventionID":

                            if (value == null || value is System.Int64)
                                this.InterventionID = (System.Int64?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to NutritionCareDiagnoseEvaluation.ID
        /// </summary>
        virtual public System.Int64? ID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseEvaluation.EvaluationID
        /// </summary>
        virtual public System.Int64? EvaluationID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.EvaluationID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.EvaluationID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseEvaluation.InterventionID
        /// </summary>
        virtual public System.Int64? InterventionID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.InterventionID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.InterventionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseEvaluation.NutritionCareInterventionID
        /// </summary>
        virtual public System.String NutritionCareInterventionID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.NutritionCareInterventionID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.NutritionCareInterventionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseEvaluation.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseEvaluation.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateDateTime, value);
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
            public esStrings(esNutritionCareDiagnoseEvaluation entity)
            {
                this.entity = entity;
            }
            public System.String ID
            {
                get
                {
                    System.Int64? data = entity.ID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ID = null;
                    else entity.ID = Convert.ToInt64(value);
                }
            }
            public System.String EvaluationID
            {
                get
                {
                    System.Int64? data = entity.EvaluationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EvaluationID = null;
                    else entity.EvaluationID = Convert.ToInt64(value);
                }
            }
            public System.String InterventionID
            {
                get
                {
                    System.Int64? data = entity.InterventionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InterventionID = null;
                    else entity.InterventionID = Convert.ToInt64(value);
                }
            }
            public System.String NutritionCareInterventionID
            {
                get
                {
                    System.String data = entity.NutritionCareInterventionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NutritionCareInterventionID = null;
                    else entity.NutritionCareInterventionID = Convert.ToString(value);
                }
            }
            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
                }
            }
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
                }
            }
            private esNutritionCareDiagnoseEvaluation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareDiagnoseEvaluationQuery query)
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
                throw new Exception("esNutritionCareDiagnoseEvaluation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareDiagnoseEvaluation : esNutritionCareDiagnoseEvaluation
    {
    }

    [Serializable]
    abstract public class esNutritionCareDiagnoseEvaluationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareDiagnoseEvaluationMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.ID, esSystemType.Int64);
            }
        }

        public esQueryItem EvaluationID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.EvaluationID, esSystemType.Int64);
            }
        }

        public esQueryItem InterventionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.InterventionID, esSystemType.Int64);
            }
        }

        public esQueryItem NutritionCareInterventionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.NutritionCareInterventionID, esSystemType.String);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareDiagnoseEvaluationCollection")]
    public partial class NutritionCareDiagnoseEvaluationCollection : esNutritionCareDiagnoseEvaluationCollection, IEnumerable<NutritionCareDiagnoseEvaluation>
    {
        public NutritionCareDiagnoseEvaluationCollection()
        {

        }

        public static implicit operator List<NutritionCareDiagnoseEvaluation>(NutritionCareDiagnoseEvaluationCollection coll)
        {
            List<NutritionCareDiagnoseEvaluation> list = new List<NutritionCareDiagnoseEvaluation>();

            foreach (NutritionCareDiagnoseEvaluation emp in coll)
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
                return NutritionCareDiagnoseEvaluationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareDiagnoseEvaluationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareDiagnoseEvaluation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareDiagnoseEvaluation();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareDiagnoseEvaluationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareDiagnoseEvaluationQuery();
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
        public bool Load(NutritionCareDiagnoseEvaluationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareDiagnoseEvaluation AddNew()
        {
            NutritionCareDiagnoseEvaluation entity = base.AddNewEntity() as NutritionCareDiagnoseEvaluation;

            return entity;
        }
        public NutritionCareDiagnoseEvaluation FindByPrimaryKey(Int64 iD)
        {
            return base.FindByPrimaryKey(iD) as NutritionCareDiagnoseEvaluation;
        }

        #region IEnumerable< NutritionCareDiagnoseEvaluation> Members

        IEnumerator<NutritionCareDiagnoseEvaluation> IEnumerable<NutritionCareDiagnoseEvaluation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareDiagnoseEvaluation;
            }
        }

        #endregion

        private NutritionCareDiagnoseEvaluationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareDiagnoseEvaluation' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareDiagnoseEvaluation ({ID})")]
    [Serializable]
    public partial class NutritionCareDiagnoseEvaluation : esNutritionCareDiagnoseEvaluation
    {
        public NutritionCareDiagnoseEvaluation()
        {
        }

        public NutritionCareDiagnoseEvaluation(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareDiagnoseEvaluationMetadata.Meta();
            }
        }

        override protected esNutritionCareDiagnoseEvaluationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareDiagnoseEvaluationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareDiagnoseEvaluationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareDiagnoseEvaluationQuery();
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
        public bool Load(NutritionCareDiagnoseEvaluationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareDiagnoseEvaluationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareDiagnoseEvaluationQuery : esNutritionCareDiagnoseEvaluationQuery
    {
        public NutritionCareDiagnoseEvaluationQuery()
        {

        }

        public NutritionCareDiagnoseEvaluationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareDiagnoseEvaluationQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareDiagnoseEvaluationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareDiagnoseEvaluationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.EvaluationID, 1, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.EvaluationID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.InterventionID, 2, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.InterventionID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.NutritionCareInterventionID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.NutritionCareInterventionID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseEvaluationMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareDiagnoseEvaluationMetadata.PropertyNames.CreateDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareDiagnoseEvaluationMetadata Meta()
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
            public const string ID = "ID";
            public const string EvaluationID = "EvaluationID";
            public const string InterventionID = "InterventionID";
            public const string NutritionCareInterventionID = "NutritionCareInterventionID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ID = "ID";
            public const string EvaluationID = "EvaluationID";
            public const string InterventionID = "InterventionID";
            public const string NutritionCareInterventionID = "NutritionCareInterventionID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
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
            lock (typeof(NutritionCareDiagnoseEvaluationMetadata))
            {
                if (NutritionCareDiagnoseEvaluationMetadata.mapDelegates == null)
                {
                    NutritionCareDiagnoseEvaluationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareDiagnoseEvaluationMetadata.meta == null)
                {
                    NutritionCareDiagnoseEvaluationMetadata.meta = new NutritionCareDiagnoseEvaluationMetadata();
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

                meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("EvaluationID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("InterventionID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("NutritionCareInterventionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareDiagnoseEvaluation";
                meta.Destination = "NutritionCareDiagnoseEvaluation";
                meta.spInsert = "proc_NutritionCareDiagnoseEvaluationInsert";
                meta.spUpdate = "proc_NutritionCareDiagnoseEvaluationUpdate";
                meta.spDelete = "proc_NutritionCareDiagnoseEvaluationDelete";
                meta.spLoadAll = "proc_NutritionCareDiagnoseEvaluationLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareDiagnoseEvaluationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareDiagnoseEvaluationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
