/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/20/19 11:14:20 AM
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
    abstract public class esItemStockOpnameApprovalCollection : esEntityCollectionWAuditLog
    {
        public esItemStockOpnameApprovalCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemStockOpnameApprovalCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemStockOpnameApprovalQuery query)
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
            this.InitQuery(query as esItemStockOpnameApprovalQuery);
        }
        #endregion

        virtual public ItemStockOpnameApproval DetachEntity(ItemStockOpnameApproval entity)
        {
            return base.DetachEntity(entity) as ItemStockOpnameApproval;
        }

        virtual public ItemStockOpnameApproval AttachEntity(ItemStockOpnameApproval entity)
        {
            return base.AttachEntity(entity) as ItemStockOpnameApproval;
        }

        virtual public void Combine(ItemStockOpnameApprovalCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemStockOpnameApproval this[int index]
        {
            get
            {
                return base[index] as ItemStockOpnameApproval;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemStockOpnameApproval);
        }
    }

    [Serializable]
    abstract public class esItemStockOpnameApproval : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemStockOpnameApprovalQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemStockOpnameApproval()
        {
        }

        public esItemStockOpnameApproval(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, Int32 pageNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, pageNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, pageNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 pageNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, pageNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, pageNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 pageNo)
        {
            esItemStockOpnameApprovalQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.PageNo == pageNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 pageNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("PageNo", pageNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "PageNo": this.str.PageNo = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PageNo":

                            if (value == null || value is System.Int32)
                                this.PageNo = (System.Int32?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDate":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDate = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to ItemStockOpnameApproval.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.PageNo
        /// </summary>
        virtual public System.Int32? PageNo
        {
            get
            {
                return base.GetSystemInt32(ItemStockOpnameApprovalMetadata.ColumnNames.PageNo);
            }

            set
            {
                base.SetSystemInt32(ItemStockOpnameApprovalMetadata.ColumnNames.PageNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(ItemStockOpnameApprovalMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(ItemStockOpnameApprovalMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.ApprovedDate
        /// </summary>
        virtual public System.DateTime? ApprovedDate
        {
            get
            {
                return base.GetSystemDateTime(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedDate);
            }

            set
            {
                base.SetSystemDateTime(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemStockOpnameApproval.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedByUserID, value);
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
            public esStrings(esItemStockOpnameApproval entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }
            public System.String PageNo
            {
                get
                {
                    System.Int32? data = entity.PageNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PageNo = null;
                    else entity.PageNo = Convert.ToInt32(value);
                }
            }
            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }
            public System.String ApprovedDate
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDate = null;
                    else entity.ApprovedDate = Convert.ToDateTime(value);
                }
            }
            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
                }
            }
            private esItemStockOpnameApproval entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemStockOpnameApprovalQuery query)
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
                throw new Exception("esItemStockOpnameApproval can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemStockOpnameApproval : esItemStockOpnameApproval
    {
    }

    [Serializable]
    abstract public class esItemStockOpnameApprovalQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemStockOpnameApprovalMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem PageNo
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.PageNo, esSystemType.Int32);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDate
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, ItemStockOpnameApprovalMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemStockOpnameApprovalCollection")]
    public partial class ItemStockOpnameApprovalCollection : esItemStockOpnameApprovalCollection, IEnumerable<ItemStockOpnameApproval>
    {
        public ItemStockOpnameApprovalCollection()
        {

        }

        public static implicit operator List<ItemStockOpnameApproval>(ItemStockOpnameApprovalCollection coll)
        {
            List<ItemStockOpnameApproval> list = new List<ItemStockOpnameApproval>();

            foreach (ItemStockOpnameApproval emp in coll)
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
                return ItemStockOpnameApprovalMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemStockOpnameApprovalQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemStockOpnameApproval(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemStockOpnameApproval();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemStockOpnameApprovalQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemStockOpnameApprovalQuery();
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
        public bool Load(ItemStockOpnameApprovalQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemStockOpnameApproval AddNew()
        {
            ItemStockOpnameApproval entity = base.AddNewEntity() as ItemStockOpnameApproval;

            return entity;
        }
        public ItemStockOpnameApproval FindByPrimaryKey(String transactionNo, Int32 pageNo)
        {
            return base.FindByPrimaryKey(transactionNo, pageNo) as ItemStockOpnameApproval;
        }

        #region IEnumerable< ItemStockOpnameApproval> Members

        IEnumerator<ItemStockOpnameApproval> IEnumerable<ItemStockOpnameApproval>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemStockOpnameApproval;
            }
        }

        #endregion

        private ItemStockOpnameApprovalQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemStockOpnameApproval' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemStockOpnameApproval ({TransactionNo, PageNo})")]
    [Serializable]
    public partial class ItemStockOpnameApproval : esItemStockOpnameApproval
    {
        public ItemStockOpnameApproval()
        {
        }

        public ItemStockOpnameApproval(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemStockOpnameApprovalMetadata.Meta();
            }
        }

        override protected esItemStockOpnameApprovalQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemStockOpnameApprovalQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemStockOpnameApprovalQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemStockOpnameApprovalQuery();
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
        public bool Load(ItemStockOpnameApprovalQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemStockOpnameApprovalQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemStockOpnameApprovalQuery : esItemStockOpnameApprovalQuery
    {
        public ItemStockOpnameApprovalQuery()
        {

        }

        public ItemStockOpnameApprovalQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemStockOpnameApprovalQuery";
        }
    }

    [Serializable]
    public partial class ItemStockOpnameApprovalMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemStockOpnameApprovalMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.PageNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.PageNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.IsApproved, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.ApprovedDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemStockOpnameApprovalMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemStockOpnameApprovalMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemStockOpnameApprovalMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string PageNo = "PageNo";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string PageNo = "PageNo";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(ItemStockOpnameApprovalMetadata))
            {
                if (ItemStockOpnameApprovalMetadata.mapDelegates == null)
                {
                    ItemStockOpnameApprovalMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemStockOpnameApprovalMetadata.meta == null)
                {
                    ItemStockOpnameApprovalMetadata.meta = new ItemStockOpnameApprovalMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PageNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemStockOpnameApproval";
                meta.Destination = "ItemStockOpnameApproval";
                meta.spInsert = "proc_ItemStockOpnameApprovalInsert";
                meta.spUpdate = "proc_ItemStockOpnameApprovalUpdate";
                meta.spDelete = "proc_ItemStockOpnameApprovalDelete";
                meta.spLoadAll = "proc_ItemStockOpnameApprovalLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemStockOpnameApprovalLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemStockOpnameApprovalMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
