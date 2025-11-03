/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/11/2017 8:38:35 PM
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
    abstract public class esItemTransactionItemHistoryCollection : esEntityCollectionWAuditLog
    {
        public esItemTransactionItemHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemTransactionItemHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemTransactionItemHistoryQuery query)
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
            this.InitQuery(query as esItemTransactionItemHistoryQuery);
        }
        #endregion

        virtual public ItemTransactionItemHistory DetachEntity(ItemTransactionItemHistory entity)
        {
            return base.DetachEntity(entity) as ItemTransactionItemHistory;
        }

        virtual public ItemTransactionItemHistory AttachEntity(ItemTransactionItemHistory entity)
        {
            return base.AttachEntity(entity) as ItemTransactionItemHistory;
        }

        virtual public void Combine(ItemTransactionItemHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemTransactionItemHistory this[int index]
        {
            get
            {
                return base[index] as ItemTransactionItemHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemTransactionItemHistory);
        }
    }

    [Serializable]
    abstract public class esItemTransactionItemHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemTransactionItemHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemTransactionItemHistory()
        {
        }

        public esItemTransactionItemHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String locationID, String itemID, String referenceNo, DateTime balanceDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, locationID, itemID, referenceNo, balanceDate);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, locationID, itemID, referenceNo, balanceDate);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String locationID, String itemID, String referenceNo, DateTime balanceDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, locationID, itemID, referenceNo, balanceDate);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, locationID, itemID, referenceNo, balanceDate);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String locationID, String itemID, String referenceNo, DateTime balanceDate)
        {
            esItemTransactionItemHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.LocationID == locationID, query.ItemID == itemID, query.ReferenceNo == referenceNo, query.BalanceDate == balanceDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String locationID, String itemID, String referenceNo, DateTime balanceDate)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("LocationID", locationID);
            parms.Add("ItemID", itemID);
            parms.Add("ReferenceNo", referenceNo);
            parms.Add("BalanceDate", balanceDate);
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
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "BalanceDate": this.str.BalanceDate = (string)value; break;
                        case "Balance": this.str.Balance = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "CostPrice": this.str.CostPrice = (string)value; break;
                        case "LastPriceInBaseUnit": this.str.LastPriceInBaseUnit = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "BalanceDate":

                            if (value == null || value is System.DateTime)
                                this.BalanceDate = (System.DateTime?)value;
                            break;
                        case "Balance":

                            if (value == null || value is System.Decimal)
                                this.Balance = (System.Decimal?)value;
                            break;
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "CostPrice":

                            if (value == null || value is System.Decimal)
                                this.CostPrice = (System.Decimal?)value;
                            break;
                        case "LastPriceInBaseUnit":

                            if (value == null || value is System.Decimal)
                                this.LastPriceInBaseUnit = (System.Decimal?)value;
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
        /// Maps to ItemTransactionItemHistory.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.ReferenceNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.BalanceDate
        /// </summary>
        virtual public System.DateTime? BalanceDate
        {
            get
            {
                return base.GetSystemDateTime(ItemTransactionItemHistoryMetadata.ColumnNames.BalanceDate);
            }

            set
            {
                base.SetSystemDateTime(ItemTransactionItemHistoryMetadata.ColumnNames.BalanceDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.Balance
        /// </summary>
        virtual public System.Decimal? Balance
        {
            get
            {
                return base.GetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.Balance);
            }

            set
            {
                base.SetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.Balance, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.CostPrice, value);
            }
        }
        /// <summary>
        /// Maps to ItemTransactionItemHistory.LastPriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? LastPriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.LastPriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ItemTransactionItemHistoryMetadata.ColumnNames.LastPriceInBaseUnit, value);
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
            public esStrings(esItemTransactionItemHistory entity)
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
            public System.String LocationID
            {
                get
                {
                    System.String data = entity.LocationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LocationID = null;
                    else entity.LocationID = Convert.ToString(value);
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
            public System.String ReferenceNo
            {
                get
                {
                    System.String data = entity.ReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNo = null;
                    else entity.ReferenceNo = Convert.ToString(value);
                }
            }
            public System.String BalanceDate
            {
                get
                {
                    System.DateTime? data = entity.BalanceDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BalanceDate = null;
                    else entity.BalanceDate = Convert.ToDateTime(value);
                }
            }
            public System.String Balance
            {
                get
                {
                    System.Decimal? data = entity.Balance;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Balance = null;
                    else entity.Balance = Convert.ToDecimal(value);
                }
            }
            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
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
            public System.String CostPrice
            {
                get
                {
                    System.Decimal? data = entity.CostPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CostPrice = null;
                    else entity.CostPrice = Convert.ToDecimal(value);
                }
            }
            public System.String LastPriceInBaseUnit
            {
                get
                {
                    System.Decimal? data = entity.LastPriceInBaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastPriceInBaseUnit = null;
                    else entity.LastPriceInBaseUnit = Convert.ToDecimal(value);
                }
            }
            private esItemTransactionItemHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemTransactionItemHistoryQuery query)
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
                throw new Exception("esItemTransactionItemHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemTransactionItemHistory : esItemTransactionItemHistory
    {
    }

    [Serializable]
    abstract public class esItemTransactionItemHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemTransactionItemHistoryMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem BalanceDate
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.BalanceDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Balance
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.Balance, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem LastPriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ItemTransactionItemHistoryMetadata.ColumnNames.LastPriceInBaseUnit, esSystemType.Decimal);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemTransactionItemHistoryCollection")]
    public partial class ItemTransactionItemHistoryCollection : esItemTransactionItemHistoryCollection, IEnumerable<ItemTransactionItemHistory>
    {
        public ItemTransactionItemHistoryCollection()
        {

        }

        public static implicit operator List<ItemTransactionItemHistory>(ItemTransactionItemHistoryCollection coll)
        {
            List<ItemTransactionItemHistory> list = new List<ItemTransactionItemHistory>();

            foreach (ItemTransactionItemHistory emp in coll)
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
                return ItemTransactionItemHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTransactionItemHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemTransactionItemHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemTransactionItemHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemTransactionItemHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTransactionItemHistoryQuery();
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
        public bool Load(ItemTransactionItemHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemTransactionItemHistory AddNew()
        {
            ItemTransactionItemHistory entity = base.AddNewEntity() as ItemTransactionItemHistory;

            return entity;
        }
        public ItemTransactionItemHistory FindByPrimaryKey(String transactionNo, String locationID, String itemID, String referenceNo, DateTime balanceDate)
        {
            return base.FindByPrimaryKey(transactionNo, locationID, itemID, referenceNo, balanceDate) as ItemTransactionItemHistory;
        }

        #region IEnumerable< ItemTransactionItemHistory> Members

        IEnumerator<ItemTransactionItemHistory> IEnumerable<ItemTransactionItemHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemTransactionItemHistory;
            }
        }

        #endregion

        private ItemTransactionItemHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemTransactionItemHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemTransactionItemHistory ({TransactionNo, LocationID, ItemID, ReferenceNo, BalanceDate})")]
    [Serializable]
    public partial class ItemTransactionItemHistory : esItemTransactionItemHistory
    {
        public ItemTransactionItemHistory()
        {
        }

        public ItemTransactionItemHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemTransactionItemHistoryMetadata.Meta();
            }
        }

        override protected esItemTransactionItemHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTransactionItemHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemTransactionItemHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTransactionItemHistoryQuery();
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
        public bool Load(ItemTransactionItemHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemTransactionItemHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemTransactionItemHistoryQuery : esItemTransactionItemHistoryQuery
    {
        public ItemTransactionItemHistoryQuery()
        {

        }

        public ItemTransactionItemHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemTransactionItemHistoryQuery";
        }
    }

    [Serializable]
    public partial class ItemTransactionItemHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemTransactionItemHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.LocationID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.LocationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.ReferenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.BalanceDate, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.BalanceDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.Balance, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.Balance;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.Price, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.CostPrice, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTransactionItemHistoryMetadata.ColumnNames.LastPriceInBaseUnit, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTransactionItemHistoryMetadata.PropertyNames.LastPriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemTransactionItemHistoryMetadata Meta()
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
            public const string LocationID = "LocationID";
            public const string ItemID = "ItemID";
            public const string ReferenceNo = "ReferenceNo";
            public const string BalanceDate = "BalanceDate";
            public const string Balance = "Balance";
            public const string Price = "Price";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CostPrice = "CostPrice";
            public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string LocationID = "LocationID";
            public const string ItemID = "ItemID";
            public const string ReferenceNo = "ReferenceNo";
            public const string BalanceDate = "BalanceDate";
            public const string Balance = "Balance";
            public const string Price = "Price";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CostPrice = "CostPrice";
            public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
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
            lock (typeof(ItemTransactionItemHistoryMetadata))
            {
                if (ItemTransactionItemHistoryMetadata.mapDelegates == null)
                {
                    ItemTransactionItemHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemTransactionItemHistoryMetadata.meta == null)
                {
                    ItemTransactionItemHistoryMetadata.meta = new ItemTransactionItemHistoryMetadata();
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
                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BalanceDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));


                meta.Source = "ItemTransactionItemHistory";
                meta.Destination = "ItemTransactionItemHistory";
                meta.spInsert = "proc_ItemTransactionItemHistoryInsert";
                meta.spUpdate = "proc_ItemTransactionItemHistoryUpdate";
                meta.spDelete = "proc_ItemTransactionItemHistoryDelete";
                meta.spLoadAll = "proc_ItemTransactionItemHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemTransactionItemHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemTransactionItemHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
