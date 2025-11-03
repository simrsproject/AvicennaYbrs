/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2019 11:30:39 AM
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
    abstract public class esNutritionCareTerminologyTemplateDetailCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareTerminologyTemplateDetailCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareTerminologyTemplateDetailCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyTemplateDetailQuery query)
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
            this.InitQuery(query as esNutritionCareTerminologyTemplateDetailQuery);
        }
        #endregion

        virtual public NutritionCareTerminologyTemplateDetail DetachEntity(NutritionCareTerminologyTemplateDetail entity)
        {
            return base.DetachEntity(entity) as NutritionCareTerminologyTemplateDetail;
        }

        virtual public NutritionCareTerminologyTemplateDetail AttachEntity(NutritionCareTerminologyTemplateDetail entity)
        {
            return base.AttachEntity(entity) as NutritionCareTerminologyTemplateDetail;
        }

        virtual public void Combine(NutritionCareTerminologyTemplateDetailCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareTerminologyTemplateDetail this[int index]
        {
            get
            {
                return base[index] as NutritionCareTerminologyTemplateDetail;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareTerminologyTemplateDetail);
        }
    }

    [Serializable]
    abstract public class esNutritionCareTerminologyTemplateDetail : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareTerminologyTemplateDetailQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareTerminologyTemplateDetail()
        {
        }

        public esNutritionCareTerminologyTemplateDetail(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 templateID, String questionID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateID, questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(templateID, questionID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateID, String questionID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateID, questionID);
            else
                return LoadByPrimaryKeyStoredProcedure(templateID, questionID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 templateID, String questionID)
        {
            esNutritionCareTerminologyTemplateDetailQuery query = this.GetDynamicQuery();
            query.Where(query.TemplateID == templateID, query.QuestionID == questionID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 templateID, String questionID)
        {
            esParameters parms = new esParameters();
            parms.Add("TemplateID", templateID);
            parms.Add("QuestionID", questionID);
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
                        case "TemplateID": this.str.TemplateID = (string)value; break;
                        case "QuestionID": this.str.QuestionID = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TemplateID":

                            if (value == null || value is System.Int32)
                                this.TemplateID = (System.Int32?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to NutritionCareTerminologyTemplateDetail.TemplateID
        /// </summary>
        virtual public System.Int32? TemplateID
        {
            get
            {
                return base.GetSystemInt32(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.TemplateID);
            }

            set
            {
                base.SetSystemInt32(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.TemplateID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplateDetail.QuestionID
        /// </summary>
        virtual public System.String QuestionID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.QuestionID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.QuestionID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplateDetail.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplateDetail.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplateDetail.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplateDetail.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareTerminologyTemplateDetail entity)
            {
                this.entity = entity;
            }
            public System.String TemplateID
            {
                get
                {
                    System.Int32? data = entity.TemplateID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateID = null;
                    else entity.TemplateID = Convert.ToInt32(value);
                }
            }
            public System.String QuestionID
            {
                get
                {
                    System.String data = entity.QuestionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionID = null;
                    else entity.QuestionID = Convert.ToString(value);
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
            private esNutritionCareTerminologyTemplateDetail entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyTemplateDetailQuery query)
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
                throw new Exception("esNutritionCareTerminologyTemplateDetail can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareTerminologyTemplateDetail : esNutritionCareTerminologyTemplateDetail
    {
    }

    [Serializable]
    abstract public class esNutritionCareTerminologyTemplateDetailQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyTemplateDetailMetadata.Meta();
            }
        }

        public esQueryItem TemplateID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.TemplateID, esSystemType.Int32);
            }
        }

        public esQueryItem QuestionID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.QuestionID, esSystemType.String);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareTerminologyTemplateDetailCollection")]
    public partial class NutritionCareTerminologyTemplateDetailCollection : esNutritionCareTerminologyTemplateDetailCollection, IEnumerable<NutritionCareTerminologyTemplateDetail>
    {
        public NutritionCareTerminologyTemplateDetailCollection()
        {

        }

        public static implicit operator List<NutritionCareTerminologyTemplateDetail>(NutritionCareTerminologyTemplateDetailCollection coll)
        {
            List<NutritionCareTerminologyTemplateDetail> list = new List<NutritionCareTerminologyTemplateDetail>();

            foreach (NutritionCareTerminologyTemplateDetail emp in coll)
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
                return NutritionCareTerminologyTemplateDetailMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyTemplateDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareTerminologyTemplateDetail(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareTerminologyTemplateDetail();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyTemplateDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyTemplateDetailQuery();
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
        public bool Load(NutritionCareTerminologyTemplateDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareTerminologyTemplateDetail AddNew()
        {
            NutritionCareTerminologyTemplateDetail entity = base.AddNewEntity() as NutritionCareTerminologyTemplateDetail;

            return entity;
        }
        public NutritionCareTerminologyTemplateDetail FindByPrimaryKey(Int32 templateID, String questionID)
        {
            return base.FindByPrimaryKey(templateID, questionID) as NutritionCareTerminologyTemplateDetail;
        }

        #region IEnumerable< NutritionCareTerminologyTemplateDetail> Members

        IEnumerator<NutritionCareTerminologyTemplateDetail> IEnumerable<NutritionCareTerminologyTemplateDetail>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareTerminologyTemplateDetail;
            }
        }

        #endregion

        private NutritionCareTerminologyTemplateDetailQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareTerminologyTemplateDetail' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareTerminologyTemplateDetail ({TemplateID, QuestionID})")]
    [Serializable]
    public partial class NutritionCareTerminologyTemplateDetail : esNutritionCareTerminologyTemplateDetail
    {
        public NutritionCareTerminologyTemplateDetail()
        {
        }

        public NutritionCareTerminologyTemplateDetail(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyTemplateDetailMetadata.Meta();
            }
        }

        override protected esNutritionCareTerminologyTemplateDetailQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyTemplateDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyTemplateDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyTemplateDetailQuery();
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
        public bool Load(NutritionCareTerminologyTemplateDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareTerminologyTemplateDetailQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareTerminologyTemplateDetailQuery : esNutritionCareTerminologyTemplateDetailQuery
    {
        public NutritionCareTerminologyTemplateDetailQuery()
        {

        }

        public NutritionCareTerminologyTemplateDetailQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareTerminologyTemplateDetailQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareTerminologyTemplateDetailMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareTerminologyTemplateDetailMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.TemplateID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.TemplateID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.QuestionID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.QuestionID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.CreateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyTemplateDetailMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareTerminologyTemplateDetailMetadata Meta()
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
            public const string TemplateID = "TemplateID";
            public const string QuestionID = "QuestionID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TemplateID = "TemplateID";
            public const string QuestionID = "QuestionID";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
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
            lock (typeof(NutritionCareTerminologyTemplateDetailMetadata))
            {
                if (NutritionCareTerminologyTemplateDetailMetadata.mapDelegates == null)
                {
                    NutritionCareTerminologyTemplateDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareTerminologyTemplateDetailMetadata.meta == null)
                {
                    NutritionCareTerminologyTemplateDetailMetadata.meta = new NutritionCareTerminologyTemplateDetailMetadata();
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

                meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareTerminologyTemplateDetail";
                meta.Destination = "NutritionCareTerminologyTemplateDetail";
                meta.spInsert = "proc_NutritionCareTerminologyTemplateDetailInsert";
                meta.spUpdate = "proc_NutritionCareTerminologyTemplateDetailUpdate";
                meta.spDelete = "proc_NutritionCareTerminologyTemplateDetailDelete";
                meta.spLoadAll = "proc_NutritionCareTerminologyTemplateDetailLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareTerminologyTemplateDetailLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareTerminologyTemplateDetailMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
