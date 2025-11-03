/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/24/2017 7:33:56 AM
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
    abstract public class esTransChargesVisiteItemCollection : esEntityCollectionWAuditLog
    {
        public esTransChargesVisiteItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransChargesVisiteItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransChargesVisiteItemQuery query)
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
            this.InitQuery(query as esTransChargesVisiteItemQuery);
        }
        #endregion

        virtual public TransChargesVisiteItem DetachEntity(TransChargesVisiteItem entity)
        {
            return base.DetachEntity(entity) as TransChargesVisiteItem;
        }

        virtual public TransChargesVisiteItem AttachEntity(TransChargesVisiteItem entity)
        {
            return base.AttachEntity(entity) as TransChargesVisiteItem;
        }

        virtual public void Combine(TransChargesVisiteItemCollection collection)
        {
            base.Combine(collection);
        }

        new public TransChargesVisiteItem this[int index]
        {
            get
            {
                return base[index] as TransChargesVisiteItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransChargesVisiteItem);
        }
    }

    [Serializable]
    abstract public class esTransChargesVisiteItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransChargesVisiteItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransChargesVisiteItem()
        {
        }

        public esTransChargesVisiteItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemID)
        {
            esTransChargesVisiteItemQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("ItemID", itemID);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "VisiteQty": this.str.VisiteQty = (string)value; break;
                        case "RealizationQty": this.str.RealizationQty = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedDateByUserID": this.str.CreatedDateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "VisiteQty":

                            if (value == null || value is System.Int32)
                                this.VisiteQty = (System.Int32?)value;
                            break;
                        case "RealizationQty":

                            if (value == null || value is System.Int32)
                                this.RealizationQty = (System.Int32?)value;
                            break;
                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to TransChargesVisiteItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.VisiteQty
        /// </summary>
        virtual public System.Int32? VisiteQty
        {
            get
            {
                return base.GetSystemInt32(TransChargesVisiteItemMetadata.ColumnNames.VisiteQty);
            }

            set
            {
                base.SetSystemInt32(TransChargesVisiteItemMetadata.ColumnNames.VisiteQty, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.RealizationQty
        /// </summary>
        virtual public System.Int32? RealizationQty
        {
            get
            {
                return base.GetSystemInt32(TransChargesVisiteItemMetadata.ColumnNames.RealizationQty);
            }

            set
            {
                base.SetSystemInt32(TransChargesVisiteItemMetadata.ColumnNames.RealizationQty, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(TransChargesVisiteItemMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(TransChargesVisiteItemMetadata.ColumnNames.IsClosed, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.CreatedDateByUserID
        /// </summary>
        virtual public System.String CreatedDateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesVisiteItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransChargesVisiteItem entity)
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
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String VisiteQty
            {
                get
                {
                    System.Int32? data = entity.VisiteQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VisiteQty = null;
                    else entity.VisiteQty = Convert.ToInt32(value);
                }
            }
            public System.String RealizationQty
            {
                get
                {
                    System.Int32? data = entity.RealizationQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RealizationQty = null;
                    else entity.RealizationQty = Convert.ToInt32(value);
                }
            }
            public System.String IsClosed
            {
                get
                {
                    System.Boolean? data = entity.IsClosed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsClosed = null;
                    else entity.IsClosed = Convert.ToBoolean(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
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
            public System.String CreatedDateByUserID
            {
                get
                {
                    System.String data = entity.CreatedDateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateByUserID = null;
                    else entity.CreatedDateByUserID = Convert.ToString(value);
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
            private esTransChargesVisiteItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransChargesVisiteItemQuery query)
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
                throw new Exception("esTransChargesVisiteItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransChargesVisiteItem : esTransChargesVisiteItem
    {
    }

    [Serializable]
    abstract public class esTransChargesVisiteItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransChargesVisiteItemMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem VisiteQty
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.VisiteQty, esSystemType.Int32);
            }
        }

        public esQueryItem RealizationQty
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.RealizationQty, esSystemType.Int32);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedDateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.CreatedDateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesVisiteItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransChargesVisiteItemCollection")]
    public partial class TransChargesVisiteItemCollection : esTransChargesVisiteItemCollection, IEnumerable<TransChargesVisiteItem>
    {
        public TransChargesVisiteItemCollection()
        {

        }

        public static implicit operator List<TransChargesVisiteItem>(TransChargesVisiteItemCollection coll)
        {
            List<TransChargesVisiteItem> list = new List<TransChargesVisiteItem>();

            foreach (TransChargesVisiteItem emp in coll)
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
                return TransChargesVisiteItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesVisiteItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransChargesVisiteItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransChargesVisiteItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransChargesVisiteItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesVisiteItemQuery();
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
        public bool Load(TransChargesVisiteItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransChargesVisiteItem AddNew()
        {
            TransChargesVisiteItem entity = base.AddNewEntity() as TransChargesVisiteItem;

            return entity;
        }
        public TransChargesVisiteItem FindByPrimaryKey(String transactionNo, String itemID)
        {
            return base.FindByPrimaryKey(transactionNo, itemID) as TransChargesVisiteItem;
        }

        #region IEnumerable< TransChargesVisiteItem> Members

        IEnumerator<TransChargesVisiteItem> IEnumerable<TransChargesVisiteItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransChargesVisiteItem;
            }
        }

        #endregion

        private TransChargesVisiteItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransChargesVisiteItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransChargesVisiteItem ({TransactionNo, ItemID})")]
    [Serializable]
    public partial class TransChargesVisiteItem : esTransChargesVisiteItem
    {
        public TransChargesVisiteItem()
        {
        }

        public TransChargesVisiteItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesVisiteItemMetadata.Meta();
            }
        }

        override protected esTransChargesVisiteItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesVisiteItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransChargesVisiteItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesVisiteItemQuery();
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
        public bool Load(TransChargesVisiteItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransChargesVisiteItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransChargesVisiteItemQuery : esTransChargesVisiteItemQuery
    {
        public TransChargesVisiteItemQuery()
        {

        }

        public TransChargesVisiteItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransChargesVisiteItemQuery";
        }
    }

    [Serializable]
    public partial class TransChargesVisiteItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransChargesVisiteItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.VisiteQty, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.VisiteQty;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.RealizationQty, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.RealizationQty;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.IsClosed, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.IsClosed;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.CreatedDateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.CreatedDateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesVisiteItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesVisiteItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransChargesVisiteItemMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string VisiteQty = "VisiteQty";
            public const string RealizationQty = "RealizationQty";
            public const string IsClosed = "IsClosed";
            public const string Notes = "Notes";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedDateByUserID = "CreatedDateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string ItemID = "ItemID";
            public const string VisiteQty = "VisiteQty";
            public const string RealizationQty = "RealizationQty";
            public const string IsClosed = "IsClosed";
            public const string Notes = "Notes";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedDateByUserID = "CreatedDateByUserID";
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
            lock (typeof(TransChargesVisiteItemMetadata))
            {
                if (TransChargesVisiteItemMetadata.mapDelegates == null)
                {
                    TransChargesVisiteItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransChargesVisiteItemMetadata.meta == null)
                {
                    TransChargesVisiteItemMetadata.meta = new TransChargesVisiteItemMetadata();
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
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VisiteQty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RealizationQty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedDateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransChargesVisiteItem";
                meta.Destination = "TransChargesVisiteItem";
                meta.spInsert = "proc_TransChargesVisiteItemInsert";
                meta.spUpdate = "proc_TransChargesVisiteItemUpdate";
                meta.spDelete = "proc_TransChargesVisiteItemDelete";
                meta.spLoadAll = "proc_TransChargesVisiteItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransChargesVisiteItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransChargesVisiteItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
