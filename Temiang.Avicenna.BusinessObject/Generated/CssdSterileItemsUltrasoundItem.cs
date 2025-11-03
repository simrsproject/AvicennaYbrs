/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/6/2017 3:29:12 PM
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
    abstract public class esCssdSterileItemsUltrasoundItemCollection : esEntityCollectionWAuditLog
    {
        public esCssdSterileItemsUltrasoundItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CssdSterileItemsUltrasoundItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esCssdSterileItemsUltrasoundItemQuery query)
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
            this.InitQuery(query as esCssdSterileItemsUltrasoundItemQuery);
        }
        #endregion

        virtual public CssdSterileItemsUltrasoundItem DetachEntity(CssdSterileItemsUltrasoundItem entity)
        {
            return base.DetachEntity(entity) as CssdSterileItemsUltrasoundItem;
        }

        virtual public CssdSterileItemsUltrasoundItem AttachEntity(CssdSterileItemsUltrasoundItem entity)
        {
            return base.AttachEntity(entity) as CssdSterileItemsUltrasoundItem;
        }

        virtual public void Combine(CssdSterileItemsUltrasoundItemCollection collection)
        {
            base.Combine(collection);
        }

        new public CssdSterileItemsUltrasoundItem this[int index]
        {
            get
            {
                return base[index] as CssdSterileItemsUltrasoundItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CssdSterileItemsUltrasoundItem);
        }
    }

    [Serializable]
    abstract public class esCssdSterileItemsUltrasoundItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCssdSterileItemsUltrasoundItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esCssdSterileItemsUltrasoundItem()
        {
        }

        public esCssdSterileItemsUltrasoundItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String transactionSeqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, transactionSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, transactionSeqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String transactionSeqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, transactionSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, transactionSeqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String transactionSeqNo)
        {
            esCssdSterileItemsUltrasoundItemQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.TransactionSeqNo == transactionSeqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String transactionSeqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("TransactionSeqNo", transactionSeqNo);
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
                        case "TransactionSeqNo": this.str.TransactionSeqNo = (string)value; break;
                        case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
                        case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
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
        /// Maps to CssdSterileItemsUltrasoundItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsUltrasoundItem.TransactionSeqNo
        /// </summary>
        virtual public System.String TransactionSeqNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionSeqNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsUltrasoundItem.ReceivedNo
        /// </summary>
        virtual public System.String ReceivedNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsUltrasoundItem.ReceivedSeqNo
        /// </summary>
        virtual public System.String ReceivedSeqNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedSeqNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsUltrasoundItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsUltrasoundItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCssdSterileItemsUltrasoundItem entity)
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
            public System.String TransactionSeqNo
            {
                get
                {
                    System.String data = entity.TransactionSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionSeqNo = null;
                    else entity.TransactionSeqNo = Convert.ToString(value);
                }
            }
            public System.String ReceivedNo
            {
                get
                {
                    System.String data = entity.ReceivedNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceivedNo = null;
                    else entity.ReceivedNo = Convert.ToString(value);
                }
            }
            public System.String ReceivedSeqNo
            {
                get
                {
                    System.String data = entity.ReceivedSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceivedSeqNo = null;
                    else entity.ReceivedSeqNo = Convert.ToString(value);
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
            private esCssdSterileItemsUltrasoundItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCssdSterileItemsUltrasoundItemQuery query)
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
                throw new Exception("esCssdSterileItemsUltrasoundItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CssdSterileItemsUltrasoundItem : esCssdSterileItemsUltrasoundItem
    {
    }

    [Serializable]
    abstract public class esCssdSterileItemsUltrasoundItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CssdSterileItemsUltrasoundItemMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionSeqNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionSeqNo, esSystemType.String);
            }
        }

        public esQueryItem ReceivedNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
            }
        }

        public esQueryItem ReceivedSeqNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CssdSterileItemsUltrasoundItemCollection")]
    public partial class CssdSterileItemsUltrasoundItemCollection : esCssdSterileItemsUltrasoundItemCollection, IEnumerable<CssdSterileItemsUltrasoundItem>
    {
        public CssdSterileItemsUltrasoundItemCollection()
        {

        }

        public static implicit operator List<CssdSterileItemsUltrasoundItem>(CssdSterileItemsUltrasoundItemCollection coll)
        {
            List<CssdSterileItemsUltrasoundItem> list = new List<CssdSterileItemsUltrasoundItem>();

            foreach (CssdSterileItemsUltrasoundItem emp in coll)
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
                return CssdSterileItemsUltrasoundItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdSterileItemsUltrasoundItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CssdSterileItemsUltrasoundItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CssdSterileItemsUltrasoundItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CssdSterileItemsUltrasoundItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdSterileItemsUltrasoundItemQuery();
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
        public bool Load(CssdSterileItemsUltrasoundItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CssdSterileItemsUltrasoundItem AddNew()
        {
            CssdSterileItemsUltrasoundItem entity = base.AddNewEntity() as CssdSterileItemsUltrasoundItem;

            return entity;
        }
        public CssdSterileItemsUltrasoundItem FindByPrimaryKey(String transactionNo, String transactionSeqNo)
        {
            return base.FindByPrimaryKey(transactionNo, transactionSeqNo) as CssdSterileItemsUltrasoundItem;
        }

        #region IEnumerable< CssdSterileItemsUltrasoundItem> Members

        IEnumerator<CssdSterileItemsUltrasoundItem> IEnumerable<CssdSterileItemsUltrasoundItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CssdSterileItemsUltrasoundItem;
            }
        }

        #endregion

        private CssdSterileItemsUltrasoundItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CssdSterileItemsUltrasoundItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CssdSterileItemsUltrasoundItem ({TransactionNo, TransactionSeqNo})")]
    [Serializable]
    public partial class CssdSterileItemsUltrasoundItem : esCssdSterileItemsUltrasoundItem
    {
        public CssdSterileItemsUltrasoundItem()
        {
        }

        public CssdSterileItemsUltrasoundItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CssdSterileItemsUltrasoundItemMetadata.Meta();
            }
        }

        override protected esCssdSterileItemsUltrasoundItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdSterileItemsUltrasoundItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CssdSterileItemsUltrasoundItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdSterileItemsUltrasoundItemQuery();
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
        public bool Load(CssdSterileItemsUltrasoundItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CssdSterileItemsUltrasoundItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CssdSterileItemsUltrasoundItemQuery : esCssdSterileItemsUltrasoundItemQuery
    {
        public CssdSterileItemsUltrasoundItemQuery()
        {

        }

        public CssdSterileItemsUltrasoundItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CssdSterileItemsUltrasoundItemQuery";
        }
    }

    [Serializable]
    public partial class CssdSterileItemsUltrasoundItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CssdSterileItemsUltrasoundItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.TransactionSeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.TransactionSeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.ReceivedNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.ReceivedSeqNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsUltrasoundItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsUltrasoundItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CssdSterileItemsUltrasoundItemMetadata Meta()
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
            public const string TransactionSeqNo = "TransactionSeqNo";
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionSeqNo = "TransactionSeqNo";
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
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
            lock (typeof(CssdSterileItemsUltrasoundItemMetadata))
            {
                if (CssdSterileItemsUltrasoundItemMetadata.mapDelegates == null)
                {
                    CssdSterileItemsUltrasoundItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CssdSterileItemsUltrasoundItemMetadata.meta == null)
                {
                    CssdSterileItemsUltrasoundItemMetadata.meta = new CssdSterileItemsUltrasoundItemMetadata();
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
                meta.AddTypeMap("TransactionSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CssdSterileItemsUltrasoundItem";
                meta.Destination = "CssdSterileItemsUltrasoundItem";
                meta.spInsert = "proc_CssdSterileItemsUltrasoundItemInsert";
                meta.spUpdate = "proc_CssdSterileItemsUltrasoundItemUpdate";
                meta.spDelete = "proc_CssdSterileItemsUltrasoundItemDelete";
                meta.spLoadAll = "proc_CssdSterileItemsUltrasoundItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_CssdSterileItemsUltrasoundItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CssdSterileItemsUltrasoundItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
