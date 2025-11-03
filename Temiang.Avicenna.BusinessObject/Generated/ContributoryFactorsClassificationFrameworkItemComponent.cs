/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/5/2016 1:17:11 PM
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
    abstract public class esContributoryFactorsClassificationFrameworkItemComponentCollection : esEntityCollectionWAuditLog
    {
        public esContributoryFactorsClassificationFrameworkItemComponentCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ContributoryFactorsClassificationFrameworkItemComponentCollection";
        }

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkItemComponentQuery query)
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
            this.InitQuery(query as esContributoryFactorsClassificationFrameworkItemComponentQuery);
        }
        #endregion

        virtual public ContributoryFactorsClassificationFrameworkItemComponent DetachEntity(ContributoryFactorsClassificationFrameworkItemComponent entity)
        {
            return base.DetachEntity(entity) as ContributoryFactorsClassificationFrameworkItemComponent;
        }

        virtual public ContributoryFactorsClassificationFrameworkItemComponent AttachEntity(ContributoryFactorsClassificationFrameworkItemComponent entity)
        {
            return base.AttachEntity(entity) as ContributoryFactorsClassificationFrameworkItemComponent;
        }

        virtual public void Combine(ContributoryFactorsClassificationFrameworkItemComponentCollection collection)
        {
            base.Combine(collection);
        }

        new public ContributoryFactorsClassificationFrameworkItemComponent this[int index]
        {
            get
            {
                return base[index] as ContributoryFactorsClassificationFrameworkItemComponent;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ContributoryFactorsClassificationFrameworkItemComponent);
        }
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFrameworkItemComponent : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esContributoryFactorsClassificationFrameworkItemComponentQuery GetDynamicQuery()
        {
            return null;
        }

        public esContributoryFactorsClassificationFrameworkItemComponent()
        {
        }

        public esContributoryFactorsClassificationFrameworkItemComponent(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String factorID, String factorItemID, String componentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID, factorItemID, componentID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID, factorItemID, componentID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String factorID, String factorItemID, String componentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID, factorItemID, componentID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID, factorItemID, componentID);
        }

        private bool LoadByPrimaryKeyDynamic(String factorID, String factorItemID, String componentID)
        {
            esContributoryFactorsClassificationFrameworkItemComponentQuery query = this.GetDynamicQuery();
            query.Where(query.FactorID == factorID, query.FactorItemID == factorItemID, query.ComponentID == componentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String factorID, String factorItemID, String componentID)
        {
            esParameters parms = new esParameters();
            parms.Add("FactorID", factorID);
            parms.Add("FactorItemID", factorItemID);
            parms.Add("ComponentID", componentID);
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
                        case "FactorID": this.str.FactorID = (string)value; break;
                        case "FactorItemID": this.str.FactorItemID = (string)value; break;
                        case "ComponentID": this.str.ComponentID = (string)value; break;
                        case "ComponentName": this.str.ComponentName = (string)value; break;
                        case "IsAllowEdit": this.str.IsAllowEdit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAllowEdit":

                            if (value == null || value is System.Boolean)
                                this.IsAllowEdit = (System.Boolean?)value;
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
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.FactorID
        /// </summary>
        virtual public System.String FactorID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.FactorItemID
        /// </summary>
        virtual public System.String FactorItemID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorItemID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorItemID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.ComponentID
        /// </summary>
        virtual public System.String ComponentID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.ComponentName
        /// </summary>
        virtual public System.String ComponentName
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentName);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentName, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.IsAllowEdit
        /// </summary>
        virtual public System.Boolean? IsAllowEdit
        {
            get
            {
                return base.GetSystemBoolean(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.IsAllowEdit);
            }

            set
            {
                base.SetSystemBoolean(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.IsAllowEdit, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItemComponent.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esContributoryFactorsClassificationFrameworkItemComponent entity)
            {
                this.entity = entity;
            }
            public System.String FactorID
            {
                get
                {
                    System.String data = entity.FactorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorID = null;
                    else entity.FactorID = Convert.ToString(value);
                }
            }
            public System.String FactorItemID
            {
                get
                {
                    System.String data = entity.FactorItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorItemID = null;
                    else entity.FactorItemID = Convert.ToString(value);
                }
            }
            public System.String ComponentID
            {
                get
                {
                    System.String data = entity.ComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ComponentID = null;
                    else entity.ComponentID = Convert.ToString(value);
                }
            }
            public System.String ComponentName
            {
                get
                {
                    System.String data = entity.ComponentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ComponentName = null;
                    else entity.ComponentName = Convert.ToString(value);
                }
            }
            public System.String IsAllowEdit
            {
                get
                {
                    System.Boolean? data = entity.IsAllowEdit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowEdit = null;
                    else entity.IsAllowEdit = Convert.ToBoolean(value);
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
            private esContributoryFactorsClassificationFrameworkItemComponent entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkItemComponentQuery query)
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
                throw new Exception("esContributoryFactorsClassificationFrameworkItemComponent can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ContributoryFactorsClassificationFrameworkItemComponent : esContributoryFactorsClassificationFrameworkItemComponent
    {
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFrameworkItemComponentQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkItemComponentMetadata.Meta();
            }
        }

        public esQueryItem FactorID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorID, esSystemType.String);
            }
        }

        public esQueryItem FactorItemID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorItemID, esSystemType.String);
            }
        }

        public esQueryItem ComponentID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID, esSystemType.String);
            }
        }

        public esQueryItem ComponentName
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentName, esSystemType.String);
            }
        }

        public esQueryItem IsAllowEdit
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.IsAllowEdit, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ContributoryFactorsClassificationFrameworkItemComponentCollection")]
    public partial class ContributoryFactorsClassificationFrameworkItemComponentCollection : esContributoryFactorsClassificationFrameworkItemComponentCollection, IEnumerable<ContributoryFactorsClassificationFrameworkItemComponent>
    {
        public ContributoryFactorsClassificationFrameworkItemComponentCollection()
        {

        }

        public static implicit operator List<ContributoryFactorsClassificationFrameworkItemComponent>(ContributoryFactorsClassificationFrameworkItemComponentCollection coll)
        {
            List<ContributoryFactorsClassificationFrameworkItemComponent> list = new List<ContributoryFactorsClassificationFrameworkItemComponent>();

            foreach (ContributoryFactorsClassificationFrameworkItemComponent emp in coll)
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
                return ContributoryFactorsClassificationFrameworkItemComponentMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ContributoryFactorsClassificationFrameworkItemComponent(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ContributoryFactorsClassificationFrameworkItemComponent();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkItemComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkItemComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ContributoryFactorsClassificationFrameworkItemComponent AddNew()
        {
            ContributoryFactorsClassificationFrameworkItemComponent entity = base.AddNewEntity() as ContributoryFactorsClassificationFrameworkItemComponent;

            return entity;
        }
        public ContributoryFactorsClassificationFrameworkItemComponent FindByPrimaryKey(String factorID, String factorItemID, String componentID)
        {
            return base.FindByPrimaryKey(factorID, factorItemID, componentID) as ContributoryFactorsClassificationFrameworkItemComponent;
        }

        #region IEnumerable< ContributoryFactorsClassificationFrameworkItemComponent> Members

        IEnumerator<ContributoryFactorsClassificationFrameworkItemComponent> IEnumerable<ContributoryFactorsClassificationFrameworkItemComponent>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ContributoryFactorsClassificationFrameworkItemComponent;
            }
        }

        #endregion

        private ContributoryFactorsClassificationFrameworkItemComponentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ContributoryFactorsClassificationFrameworkItemComponent' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ContributoryFactorsClassificationFrameworkItemComponent ({FactorID, FactorItemID, ComponentID})")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItemComponent : esContributoryFactorsClassificationFrameworkItemComponent
    {
        public ContributoryFactorsClassificationFrameworkItemComponent()
        {
        }

        public ContributoryFactorsClassificationFrameworkItemComponent(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkItemComponentMetadata.Meta();
            }
        }

        override protected esContributoryFactorsClassificationFrameworkItemComponentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkItemComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkItemComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ContributoryFactorsClassificationFrameworkItemComponentQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItemComponentQuery : esContributoryFactorsClassificationFrameworkItemComponentQuery
    {
        public ContributoryFactorsClassificationFrameworkItemComponentQuery()
        {

        }

        public ContributoryFactorsClassificationFrameworkItemComponentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ContributoryFactorsClassificationFrameworkItemComponentQuery";
        }
    }

    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItemComponentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ContributoryFactorsClassificationFrameworkItemComponentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.FactorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.FactorItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.FactorItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.ComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.ComponentName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.IsAllowEdit, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.IsAllowEdit;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemComponentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ContributoryFactorsClassificationFrameworkItemComponentMetadata Meta()
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
            public const string FactorID = "FactorID";
            public const string FactorItemID = "FactorItemID";
            public const string ComponentID = "ComponentID";
            public const string ComponentName = "ComponentName";
            public const string IsAllowEdit = "IsAllowEdit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string FactorID = "FactorID";
            public const string FactorItemID = "FactorItemID";
            public const string ComponentID = "ComponentID";
            public const string ComponentName = "ComponentName";
            public const string IsAllowEdit = "IsAllowEdit";
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
            lock (typeof(ContributoryFactorsClassificationFrameworkItemComponentMetadata))
            {
                if (ContributoryFactorsClassificationFrameworkItemComponentMetadata.mapDelegates == null)
                {
                    ContributoryFactorsClassificationFrameworkItemComponentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ContributoryFactorsClassificationFrameworkItemComponentMetadata.meta == null)
                {
                    ContributoryFactorsClassificationFrameworkItemComponentMetadata.meta = new ContributoryFactorsClassificationFrameworkItemComponentMetadata();
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

                meta.AddTypeMap("FactorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FactorItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ComponentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAllowEdit", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ContributoryFactorsClassificationFrameworkItemComponent";
                meta.Destination = "ContributoryFactorsClassificationFrameworkItemComponent";
                meta.spInsert = "proc_ContributoryFactorsClassificationFrameworkItemComponentInsert";
                meta.spUpdate = "proc_ContributoryFactorsClassificationFrameworkItemComponentUpdate";
                meta.spDelete = "proc_ContributoryFactorsClassificationFrameworkItemComponentDelete";
                meta.spLoadAll = "proc_ContributoryFactorsClassificationFrameworkItemComponentLoadAll";
                meta.spLoadByPrimaryKey = "proc_ContributoryFactorsClassificationFrameworkItemComponentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ContributoryFactorsClassificationFrameworkItemComponentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
