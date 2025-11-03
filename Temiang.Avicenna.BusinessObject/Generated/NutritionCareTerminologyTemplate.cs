/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2019 11:30:03 AM
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
    abstract public class esNutritionCareTerminologyTemplateCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareTerminologyTemplateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareTerminologyTemplateCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyTemplateQuery query)
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
            this.InitQuery(query as esNutritionCareTerminologyTemplateQuery);
        }
        #endregion

        virtual public NutritionCareTerminologyTemplate DetachEntity(NutritionCareTerminologyTemplate entity)
        {
            return base.DetachEntity(entity) as NutritionCareTerminologyTemplate;
        }

        virtual public NutritionCareTerminologyTemplate AttachEntity(NutritionCareTerminologyTemplate entity)
        {
            return base.AttachEntity(entity) as NutritionCareTerminologyTemplate;
        }

        virtual public void Combine(NutritionCareTerminologyTemplateCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareTerminologyTemplate this[int index]
        {
            get
            {
                return base[index] as NutritionCareTerminologyTemplate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareTerminologyTemplate);
        }
    }

    [Serializable]
    abstract public class esNutritionCareTerminologyTemplate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareTerminologyTemplateQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareTerminologyTemplate()
        {
        }

        public esNutritionCareTerminologyTemplate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 templateID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateID);
            else
                return LoadByPrimaryKeyStoredProcedure(templateID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(templateID);
            else
                return LoadByPrimaryKeyStoredProcedure(templateID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 templateID)
        {
            esNutritionCareTerminologyTemplateQuery query = this.GetDynamicQuery();
            query.Where(query.TemplateID == templateID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 templateID)
        {
            esParameters parms = new esParameters();
            parms.Add("TemplateID", templateID);
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
                        case "TemplateName": this.str.TemplateName = (string)value; break;
                        case "TemplateText": this.str.TemplateText = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
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
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to NutritionCareTerminologyTemplate.TemplateID
        /// </summary>
        virtual public System.Int32? TemplateID
        {
            get
            {
                return base.GetSystemInt32(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateID);
            }

            set
            {
                base.SetSystemInt32(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.TemplateName
        /// </summary>
        virtual public System.String TemplateName
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateName);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateName, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.TemplateText
        /// </summary>
        virtual public System.String TemplateText
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateText);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateText, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareTerminologyTemplateMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareTerminologyTemplateMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareTerminologyTemplate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareTerminologyTemplate entity)
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
            public System.String TemplateName
            {
                get
                {
                    System.String data = entity.TemplateName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateName = null;
                    else entity.TemplateName = Convert.ToString(value);
                }
            }
            public System.String TemplateText
            {
                get
                {
                    System.String data = entity.TemplateText;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TemplateText = null;
                    else entity.TemplateText = Convert.ToString(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esNutritionCareTerminologyTemplate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareTerminologyTemplateQuery query)
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
                throw new Exception("esNutritionCareTerminologyTemplate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareTerminologyTemplate : esNutritionCareTerminologyTemplate
    {
    }

    [Serializable]
    abstract public class esNutritionCareTerminologyTemplateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyTemplateMetadata.Meta();
            }
        }

        public esQueryItem TemplateID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateID, esSystemType.Int32);
            }
        }

        public esQueryItem TemplateName
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
            }
        }

        public esQueryItem TemplateText
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateText, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareTerminologyTemplateCollection")]
    public partial class NutritionCareTerminologyTemplateCollection : esNutritionCareTerminologyTemplateCollection, IEnumerable<NutritionCareTerminologyTemplate>
    {
        public NutritionCareTerminologyTemplateCollection()
        {

        }

        public static implicit operator List<NutritionCareTerminologyTemplate>(NutritionCareTerminologyTemplateCollection coll)
        {
            List<NutritionCareTerminologyTemplate> list = new List<NutritionCareTerminologyTemplate>();

            foreach (NutritionCareTerminologyTemplate emp in coll)
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
                return NutritionCareTerminologyTemplateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareTerminologyTemplate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareTerminologyTemplate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyTemplateQuery();
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
        public bool Load(NutritionCareTerminologyTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareTerminologyTemplate AddNew()
        {
            NutritionCareTerminologyTemplate entity = base.AddNewEntity() as NutritionCareTerminologyTemplate;

            return entity;
        }
        public NutritionCareTerminologyTemplate FindByPrimaryKey(Int32 templateID)
        {
            return base.FindByPrimaryKey(templateID) as NutritionCareTerminologyTemplate;
        }

        #region IEnumerable< NutritionCareTerminologyTemplate> Members

        IEnumerator<NutritionCareTerminologyTemplate> IEnumerable<NutritionCareTerminologyTemplate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareTerminologyTemplate;
            }
        }

        #endregion

        private NutritionCareTerminologyTemplateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareTerminologyTemplate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareTerminologyTemplate ({TemplateID})")]
    [Serializable]
    public partial class NutritionCareTerminologyTemplate : esNutritionCareTerminologyTemplate
    {
        public NutritionCareTerminologyTemplate()
        {
        }

        public NutritionCareTerminologyTemplate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareTerminologyTemplateMetadata.Meta();
            }
        }

        override protected esNutritionCareTerminologyTemplateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareTerminologyTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareTerminologyTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareTerminologyTemplateQuery();
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
        public bool Load(NutritionCareTerminologyTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareTerminologyTemplateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareTerminologyTemplateQuery : esNutritionCareTerminologyTemplateQuery
    {
        public NutritionCareTerminologyTemplateQuery()
        {

        }

        public NutritionCareTerminologyTemplateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareTerminologyTemplateQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareTerminologyTemplateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareTerminologyTemplateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.TemplateID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.TemplateName;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.TemplateText, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.TemplateText;
            c.CharacterMaxLength = 300;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareTerminologyTemplateMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareTerminologyTemplateMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareTerminologyTemplateMetadata Meta()
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
            public const string TemplateName = "TemplateName";
            public const string TemplateText = "TemplateText";
            public const string IsActive = "IsActive";
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
            public const string TemplateName = "TemplateName";
            public const string TemplateText = "TemplateText";
            public const string IsActive = "IsActive";
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
            lock (typeof(NutritionCareTerminologyTemplateMetadata))
            {
                if (NutritionCareTerminologyTemplateMetadata.mapDelegates == null)
                {
                    NutritionCareTerminologyTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareTerminologyTemplateMetadata.meta == null)
                {
                    NutritionCareTerminologyTemplateMetadata.meta = new NutritionCareTerminologyTemplateMetadata();
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
                meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TemplateText", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareTerminologyTemplate";
                meta.Destination = "NutritionCareTerminologyTemplate";
                meta.spInsert = "proc_NutritionCareTerminologyTemplateInsert";
                meta.spUpdate = "proc_NutritionCareTerminologyTemplateUpdate";
                meta.spDelete = "proc_NutritionCareTerminologyTemplateDelete";
                meta.spLoadAll = "proc_NutritionCareTerminologyTemplateLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareTerminologyTemplateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareTerminologyTemplateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
