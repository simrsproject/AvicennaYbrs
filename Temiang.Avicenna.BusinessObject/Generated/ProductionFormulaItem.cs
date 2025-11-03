/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/15/2016 11:23:10 AM
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
    abstract public class esProductionFormulaItemCollection : esEntityCollectionWAuditLog
    {
        public esProductionFormulaItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ProductionFormulaItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esProductionFormulaItemQuery query)
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
            this.InitQuery(query as esProductionFormulaItemQuery);
        }
        #endregion

        virtual public ProductionFormulaItem DetachEntity(ProductionFormulaItem entity)
        {
            return base.DetachEntity(entity) as ProductionFormulaItem;
        }

        virtual public ProductionFormulaItem AttachEntity(ProductionFormulaItem entity)
        {
            return base.AttachEntity(entity) as ProductionFormulaItem;
        }

        virtual public void Combine(ProductionFormulaItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ProductionFormulaItem this[int index]
        {
            get
            {
                return base[index] as ProductionFormulaItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ProductionFormulaItem);
        }
    }

    [Serializable]
    abstract public class esProductionFormulaItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esProductionFormulaItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esProductionFormulaItem()
        {
        }

        public esProductionFormulaItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String formulaID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(formulaID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(formulaID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String formulaID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(formulaID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(formulaID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String formulaID, String itemID)
        {
            esProductionFormulaItemQuery query = this.GetDynamicQuery();
            query.Where(query.FormulaID == formulaID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String formulaID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("FormulaID", formulaID);
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
                        case "FormulaID": this.str.FormulaID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
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
        /// Maps to ProductionFormulaItem.FormulaID
        /// </summary>
        virtual public System.String FormulaID
        {
            get
            {
                return base.GetSystemString(ProductionFormulaItemMetadata.ColumnNames.FormulaID);
            }

            set
            {
                base.SetSystemString(ProductionFormulaItemMetadata.ColumnNames.FormulaID, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ProductionFormulaItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ProductionFormulaItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(ProductionFormulaItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(ProductionFormulaItemMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ProductionFormulaItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ProductionFormulaItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ProductionFormulaItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ProductionFormulaItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ProductionFormulaItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ProductionFormulaItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ProductionFormulaItem.IsConsumables
        /// </summary>
        virtual public System.Boolean? IsConsumables
        {
            get
            {
                return base.GetSystemBoolean(ProductionFormulaItemMetadata.ColumnNames.IsConsumables);
            }

            set
            {
                base.SetSystemBoolean(ProductionFormulaItemMetadata.ColumnNames.IsConsumables, value);
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
            public esStrings(esProductionFormulaItem entity)
            {
                this.entity = entity;
            }
            public System.String FormulaID
            {
                get
                {
                    System.String data = entity.FormulaID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FormulaID = null;
                    else entity.FormulaID = Convert.ToString(value);
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
            private esProductionFormulaItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esProductionFormulaItemQuery query)
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
                throw new Exception("esProductionFormulaItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ProductionFormulaItem : esProductionFormulaItem
    {
    }

    [Serializable]
    abstract public class esProductionFormulaItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ProductionFormulaItemMetadata.Meta();
            }
        }

        public esQueryItem FormulaID
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.FormulaID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsConsumables
        {
            get
            {
                return new esQueryItem(this, ProductionFormulaItemMetadata.ColumnNames.IsConsumables, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ProductionFormulaItemCollection")]
    public partial class ProductionFormulaItemCollection : esProductionFormulaItemCollection, IEnumerable<ProductionFormulaItem>
    {
        public ProductionFormulaItemCollection()
        {

        }

        public static implicit operator List<ProductionFormulaItem>(ProductionFormulaItemCollection coll)
        {
            List<ProductionFormulaItem> list = new List<ProductionFormulaItem>();

            foreach (ProductionFormulaItem emp in coll)
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
                return ProductionFormulaItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProductionFormulaItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ProductionFormulaItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ProductionFormulaItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ProductionFormulaItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProductionFormulaItemQuery();
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
        public bool Load(ProductionFormulaItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ProductionFormulaItem AddNew()
        {
            ProductionFormulaItem entity = base.AddNewEntity() as ProductionFormulaItem;

            return entity;
        }
        public ProductionFormulaItem FindByPrimaryKey(String formulaID, String itemID)
        {
            return base.FindByPrimaryKey(formulaID, itemID) as ProductionFormulaItem;
        }

        #region IEnumerable< ProductionFormulaItem> Members

        IEnumerator<ProductionFormulaItem> IEnumerable<ProductionFormulaItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ProductionFormulaItem;
            }
        }

        #endregion

        private ProductionFormulaItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ProductionFormulaItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ProductionFormulaItem ({FormulaID, ItemID})")]
    [Serializable]
    public partial class ProductionFormulaItem : esProductionFormulaItem
    {
        public ProductionFormulaItem()
        {
        }

        public ProductionFormulaItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ProductionFormulaItemMetadata.Meta();
            }
        }

        override protected esProductionFormulaItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProductionFormulaItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ProductionFormulaItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProductionFormulaItemQuery();
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
        public bool Load(ProductionFormulaItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ProductionFormulaItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ProductionFormulaItemQuery : esProductionFormulaItemQuery
    {
        public ProductionFormulaItemQuery()
        {

        }

        public ProductionFormulaItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ProductionFormulaItemQuery";
        }
    }

    [Serializable]
    public partial class ProductionFormulaItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ProductionFormulaItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.FormulaID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.FormulaID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 4;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProductionFormulaItemMetadata.ColumnNames.IsConsumables, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProductionFormulaItemMetadata.PropertyNames.IsConsumables;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ProductionFormulaItemMetadata Meta()
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
            public const string FormulaID = "FormulaID";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsConsumables = "IsConsumables";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string FormulaID = "FormulaID";
            public const string ItemID = "ItemID";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(ProductionFormulaItemMetadata))
            {
                if (ProductionFormulaItemMetadata.mapDelegates == null)
                {
                    ProductionFormulaItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ProductionFormulaItemMetadata.meta == null)
                {
                    ProductionFormulaItemMetadata.meta = new ProductionFormulaItemMetadata();
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

                meta.AddTypeMap("FormulaID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsConsumables", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ProductionFormulaItem";
                meta.Destination = "ProductionFormulaItem";
                meta.spInsert = "proc_ProductionFormulaItemInsert";
                meta.spUpdate = "proc_ProductionFormulaItemUpdate";
                meta.spDelete = "proc_ProductionFormulaItemDelete";
                meta.spLoadAll = "proc_ProductionFormulaItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ProductionFormulaItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ProductionFormulaItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
