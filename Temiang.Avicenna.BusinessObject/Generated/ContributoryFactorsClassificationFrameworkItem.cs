/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/5/2016 1:16:41 PM
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
    abstract public class esContributoryFactorsClassificationFrameworkItemCollection : esEntityCollectionWAuditLog
    {
        public esContributoryFactorsClassificationFrameworkItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ContributoryFactorsClassificationFrameworkItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkItemQuery query)
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
            this.InitQuery(query as esContributoryFactorsClassificationFrameworkItemQuery);
        }
        #endregion

        virtual public ContributoryFactorsClassificationFrameworkItem DetachEntity(ContributoryFactorsClassificationFrameworkItem entity)
        {
            return base.DetachEntity(entity) as ContributoryFactorsClassificationFrameworkItem;
        }

        virtual public ContributoryFactorsClassificationFrameworkItem AttachEntity(ContributoryFactorsClassificationFrameworkItem entity)
        {
            return base.AttachEntity(entity) as ContributoryFactorsClassificationFrameworkItem;
        }

        virtual public void Combine(ContributoryFactorsClassificationFrameworkItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ContributoryFactorsClassificationFrameworkItem this[int index]
        {
            get
            {
                return base[index] as ContributoryFactorsClassificationFrameworkItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ContributoryFactorsClassificationFrameworkItem);
        }
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFrameworkItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esContributoryFactorsClassificationFrameworkItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esContributoryFactorsClassificationFrameworkItem()
        {
        }

        public esContributoryFactorsClassificationFrameworkItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String factorID, String factorItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID, factorItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID, factorItemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String factorID, String factorItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(factorID, factorItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(factorID, factorItemID);
        }

        private bool LoadByPrimaryKeyDynamic(String factorID, String factorItemID)
        {
            esContributoryFactorsClassificationFrameworkItemQuery query = this.GetDynamicQuery();
            query.Where(query.FactorID == factorID, query.FactorItemID == factorItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String factorID, String factorItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("FactorID", factorID);
            parms.Add("FactorItemID", factorItemID);
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
                        case "FactorItemName": this.str.FactorItemName = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
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
        /// Maps to ContributoryFactorsClassificationFrameworkItem.FactorID
        /// </summary>
        virtual public System.String FactorID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItem.FactorItemID
        /// </summary>
        virtual public System.String FactorItemID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItem.FactorItemName
        /// </summary>
        virtual public System.String FactorItemName
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemName);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemName, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ContributoryFactorsClassificationFrameworkItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esContributoryFactorsClassificationFrameworkItem entity)
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
            public System.String FactorItemName
            {
                get
                {
                    System.String data = entity.FactorItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FactorItemName = null;
                    else entity.FactorItemName = Convert.ToString(value);
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
            private esContributoryFactorsClassificationFrameworkItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esContributoryFactorsClassificationFrameworkItemQuery query)
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
                throw new Exception("esContributoryFactorsClassificationFrameworkItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ContributoryFactorsClassificationFrameworkItem : esContributoryFactorsClassificationFrameworkItem
    {
    }

    [Serializable]
    abstract public class esContributoryFactorsClassificationFrameworkItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkItemMetadata.Meta();
            }
        }

        public esQueryItem FactorID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorID, esSystemType.String);
            }
        }

        public esQueryItem FactorItemID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID, esSystemType.String);
            }
        }

        public esQueryItem FactorItemName
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ContributoryFactorsClassificationFrameworkItemCollection")]
    public partial class ContributoryFactorsClassificationFrameworkItemCollection : esContributoryFactorsClassificationFrameworkItemCollection, IEnumerable<ContributoryFactorsClassificationFrameworkItem>
    {
        public ContributoryFactorsClassificationFrameworkItemCollection()
        {

        }

        public static implicit operator List<ContributoryFactorsClassificationFrameworkItem>(ContributoryFactorsClassificationFrameworkItemCollection coll)
        {
            List<ContributoryFactorsClassificationFrameworkItem> list = new List<ContributoryFactorsClassificationFrameworkItem>();

            foreach (ContributoryFactorsClassificationFrameworkItem emp in coll)
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
                return ContributoryFactorsClassificationFrameworkItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ContributoryFactorsClassificationFrameworkItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ContributoryFactorsClassificationFrameworkItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkItemQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ContributoryFactorsClassificationFrameworkItem AddNew()
        {
            ContributoryFactorsClassificationFrameworkItem entity = base.AddNewEntity() as ContributoryFactorsClassificationFrameworkItem;

            return entity;
        }
        public ContributoryFactorsClassificationFrameworkItem FindByPrimaryKey(String factorID, String factorItemID)
        {
            return base.FindByPrimaryKey(factorID, factorItemID) as ContributoryFactorsClassificationFrameworkItem;
        }

        #region IEnumerable< ContributoryFactorsClassificationFrameworkItem> Members

        IEnumerator<ContributoryFactorsClassificationFrameworkItem> IEnumerable<ContributoryFactorsClassificationFrameworkItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ContributoryFactorsClassificationFrameworkItem;
            }
        }

        #endregion

        private ContributoryFactorsClassificationFrameworkItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ContributoryFactorsClassificationFrameworkItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ContributoryFactorsClassificationFrameworkItem ({FactorID, FactorItemID})")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItem : esContributoryFactorsClassificationFrameworkItem
    {
        public ContributoryFactorsClassificationFrameworkItem()
        {
        }

        public ContributoryFactorsClassificationFrameworkItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ContributoryFactorsClassificationFrameworkItemMetadata.Meta();
            }
        }

        override protected esContributoryFactorsClassificationFrameworkItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ContributoryFactorsClassificationFrameworkItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ContributoryFactorsClassificationFrameworkItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ContributoryFactorsClassificationFrameworkItemQuery();
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
        public bool Load(ContributoryFactorsClassificationFrameworkItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ContributoryFactorsClassificationFrameworkItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItemQuery : esContributoryFactorsClassificationFrameworkItemQuery
    {
        public ContributoryFactorsClassificationFrameworkItemQuery()
        {

        }

        public ContributoryFactorsClassificationFrameworkItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ContributoryFactorsClassificationFrameworkItemQuery";
        }
    }

    [Serializable]
    public partial class ContributoryFactorsClassificationFrameworkItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ContributoryFactorsClassificationFrameworkItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemMetadata.PropertyNames.FactorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemMetadata.PropertyNames.FactorItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.FactorItemName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemMetadata.PropertyNames.FactorItemName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ContributoryFactorsClassificationFrameworkItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ContributoryFactorsClassificationFrameworkItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ContributoryFactorsClassificationFrameworkItemMetadata Meta()
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
            public const string FactorItemName = "FactorItemName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string FactorID = "FactorID";
            public const string FactorItemID = "FactorItemID";
            public const string FactorItemName = "FactorItemName";
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
            lock (typeof(ContributoryFactorsClassificationFrameworkItemMetadata))
            {
                if (ContributoryFactorsClassificationFrameworkItemMetadata.mapDelegates == null)
                {
                    ContributoryFactorsClassificationFrameworkItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ContributoryFactorsClassificationFrameworkItemMetadata.meta == null)
                {
                    ContributoryFactorsClassificationFrameworkItemMetadata.meta = new ContributoryFactorsClassificationFrameworkItemMetadata();
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
                meta.AddTypeMap("FactorItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ContributoryFactorsClassificationFrameworkItem";
                meta.Destination = "ContributoryFactorsClassificationFrameworkItem";
                meta.spInsert = "proc_ContributoryFactorsClassificationFrameworkItemInsert";
                meta.spUpdate = "proc_ContributoryFactorsClassificationFrameworkItemUpdate";
                meta.spDelete = "proc_ContributoryFactorsClassificationFrameworkItemDelete";
                meta.spLoadAll = "proc_ContributoryFactorsClassificationFrameworkItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ContributoryFactorsClassificationFrameworkItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ContributoryFactorsClassificationFrameworkItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
