/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/15/2016 11:23:53 AM
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
    abstract public class esProductionOfGoodsItemCollection : esEntityCollectionWAuditLog
    {
        public esProductionOfGoodsItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ProductionOfGoodsItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esProductionOfGoodsItemQuery query)
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
            this.InitQuery(query as esProductionOfGoodsItemQuery);
        }
        #endregion

        virtual public ProductionOfGoodsItem DetachEntity(ProductionOfGoodsItem entity)
        {
            return base.DetachEntity(entity) as ProductionOfGoodsItem;
        }

        virtual public ProductionOfGoodsItem AttachEntity(ProductionOfGoodsItem entity)
        {
            return base.AttachEntity(entity) as ProductionOfGoodsItem;
        }

        virtual public void Combine(ProductionOfGoodsItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ProductionOfGoodsItem this[int index]
        {
            get
            {
                return base[index] as ProductionOfGoodsItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ProductionOfGoodsItem);
        }
    }

    [Serializable]
    abstract public class esProductionOfGoodsItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esProductionOfGoodsItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esProductionOfGoodsItem()
        {
        }

        public esProductionOfGoodsItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String productionNo, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(productionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(productionNo, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String productionNo, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(productionNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(productionNo, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String productionNo, String itemID)
        {
            esProductionOfGoodsItemQuery query = this.GetDynamicQuery();
            query.Where(query.ProductionNo == productionNo, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String productionNo, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("ProductionNo", productionNo);
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
                        case "ProductionNo": this.str.ProductionNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "CostPrice": this.str.CostPrice = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PriceInBaseUnit": this.str.PriceInBaseUnit = (string)value; break;
                        case "IsConsumables": this.str.IsConsumables = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Qty":

                            if (value == null || value is System.Decimal)
                                this.Qty = (System.Decimal?)value;
                            break;
                        case "CostPrice":

                            if (value == null || value is System.Decimal)
                                this.CostPrice = (System.Decimal?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "PriceInBaseUnit":

                            if (value == null || value is System.Decimal)
                                this.PriceInBaseUnit = (System.Decimal?)value;
                            break;
                        case "IsConsumables":

                            if (value == null || value is System.Boolean)
                                this.IsConsumables = (System.Boolean?)value;
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
        /// Maps to ProductionOfGoodsItem.ProductionNo
        /// </summary>
        virtual public System.String ProductionNo
        {
            get
            {
                return base.GetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.ProductionNo);
            }

            set
            {
                base.SetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.ProductionNo, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.CostPrice, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.PriceInBaseUnit
        /// </summary>
        virtual public System.Decimal? PriceInBaseUnit
        {
            get
            {
                return base.GetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.PriceInBaseUnit);
            }

            set
            {
                base.SetSystemDecimal(ProductionOfGoodsItemMetadata.ColumnNames.PriceInBaseUnit, value);
            }
        }
        /// <summary>
        /// Maps to ProductionOfGoodsItem.IsConsumables
        /// </summary>
        virtual public System.Boolean? IsConsumables
        {
            get
            {
                return base.GetSystemBoolean(ProductionOfGoodsItemMetadata.ColumnNames.IsConsumables);
            }

            set
            {
                base.SetSystemBoolean(ProductionOfGoodsItemMetadata.ColumnNames.IsConsumables, value);
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
            public esStrings(esProductionOfGoodsItem entity)
            {
                this.entity = entity;
            }
            public System.String ProductionNo
            {
                get
                {
                    System.String data = entity.ProductionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProductionNo = null;
                    else entity.ProductionNo = Convert.ToString(value);
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
            public System.String Qty
            {
                get
                {
                    System.Decimal? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToDecimal(value);
                }
            }
            public System.String SRItemUnit
            {
                get
                {
                    System.String data = entity.SRItemUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemUnit = null;
                    else entity.SRItemUnit = Convert.ToString(value);
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
            public System.String PriceInBaseUnit
            {
                get
                {
                    System.Decimal? data = entity.PriceInBaseUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceInBaseUnit = null;
                    else entity.PriceInBaseUnit = Convert.ToDecimal(value);
                }
            }
            public System.String IsConsumables
            {
                get
                {
                    System.Boolean? data = entity.IsConsumables;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsConsumables = null;
                    else entity.IsConsumables = Convert.ToBoolean(value);
                }
            }
            private esProductionOfGoodsItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esProductionOfGoodsItemQuery query)
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
                throw new Exception("esProductionOfGoodsItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ProductionOfGoodsItem : esProductionOfGoodsItem
    {
    }

    [Serializable]
    abstract public class esProductionOfGoodsItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ProductionOfGoodsItemMetadata.Meta();
            }
        }

        public esQueryItem ProductionNo
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.ProductionNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PriceInBaseUnit
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
            }
        }

        public esQueryItem IsConsumables
        {
            get
            {
                return new esQueryItem(this, ProductionOfGoodsItemMetadata.ColumnNames.IsConsumables, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ProductionOfGoodsItemCollection")]
    public partial class ProductionOfGoodsItemCollection : esProductionOfGoodsItemCollection, IEnumerable<ProductionOfGoodsItem>
    {
        public ProductionOfGoodsItemCollection()
        {

        }

        public static implicit operator List<ProductionOfGoodsItem>(ProductionOfGoodsItemCollection coll)
        {
            List<ProductionOfGoodsItem> list = new List<ProductionOfGoodsItem>();

            foreach (ProductionOfGoodsItem emp in coll)
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
                return ProductionOfGoodsItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProductionOfGoodsItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ProductionOfGoodsItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ProductionOfGoodsItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ProductionOfGoodsItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProductionOfGoodsItemQuery();
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
        public bool Load(ProductionOfGoodsItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ProductionOfGoodsItem AddNew()
        {
            ProductionOfGoodsItem entity = base.AddNewEntity() as ProductionOfGoodsItem;

            return entity;
        }
        public ProductionOfGoodsItem FindByPrimaryKey(String productionNo, String itemID)
        {
            return base.FindByPrimaryKey(productionNo, itemID) as ProductionOfGoodsItem;
        }

        #region IEnumerable< ProductionOfGoodsItem> Members

        IEnumerator<ProductionOfGoodsItem> IEnumerable<ProductionOfGoodsItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ProductionOfGoodsItem;
            }
        }

        #endregion

        private ProductionOfGoodsItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ProductionOfGoodsItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ProductionOfGoodsItem ({ProductionNo, ItemID})")]
    [Serializable]
    public partial class ProductionOfGoodsItem : esProductionOfGoodsItem
    {
        public ProductionOfGoodsItem()
        {
        }

        public ProductionOfGoodsItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ProductionOfGoodsItemMetadata.Meta();
            }
        }

        override protected esProductionOfGoodsItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProductionOfGoodsItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ProductionOfGoodsItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProductionOfGoodsItemQuery();
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
        public bool Load(ProductionOfGoodsItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ProductionOfGoodsItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ProductionOfGoodsItemQuery : esProductionOfGoodsItemQuery
    {
        public ProductionOfGoodsItemQuery()
        {

        }

        public ProductionOfGoodsItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ProductionOfGoodsItemQuery";
        }
    }

    [Serializable]
    public partial class ProductionOfGoodsItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ProductionOfGoodsItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.ProductionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.ProductionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.CostPrice, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.PriceInBaseUnit, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.PriceInBaseUnit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionOfGoodsItemMetadata.ColumnNames.IsConsumables, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProductionOfGoodsItemMetadata.PropertyNames.IsConsumables;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ProductionOfGoodsItemMetadata Meta()
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
            public const string ProductionNo = "ProductionNo";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string CostPrice = "CostPrice";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string IsConsumables = "IsConsumables";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProductionNo = "ProductionNo";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string CostPrice = "CostPrice";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PriceInBaseUnit = "PriceInBaseUnit";
            public const string IsConsumables = "IsConsumables";
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
            lock (typeof(ProductionOfGoodsItemMetadata))
            {
                if (ProductionOfGoodsItemMetadata.mapDelegates == null)
                {
                    ProductionOfGoodsItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ProductionOfGoodsItemMetadata.meta == null)
                {
                    ProductionOfGoodsItemMetadata.meta = new ProductionOfGoodsItemMetadata();
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

                meta.AddTypeMap("ProductionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsConsumables", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ProductionOfGoodsItem";
                meta.Destination = "ProductionOfGoodsItem";
                meta.spInsert = "proc_ProductionOfGoodsItemInsert";
                meta.spUpdate = "proc_ProductionOfGoodsItemUpdate";
                meta.spDelete = "proc_ProductionOfGoodsItemDelete";
                meta.spLoadAll = "proc_ProductionOfGoodsItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ProductionOfGoodsItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ProductionOfGoodsItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
