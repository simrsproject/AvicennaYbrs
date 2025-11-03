/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/1/2019 8:35:36 PM
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
    abstract public class esItemPackageCollection : esEntityCollectionWAuditLog
    {
        public esItemPackageCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemPackageCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemPackageQuery query)
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
            this.InitQuery(query as esItemPackageQuery);
        }
        #endregion

        virtual public ItemPackage DetachEntity(ItemPackage entity)
        {
            return base.DetachEntity(entity) as ItemPackage;
        }

        virtual public ItemPackage AttachEntity(ItemPackage entity)
        {
            return base.AttachEntity(entity) as ItemPackage;
        }

        virtual public void Combine(ItemPackageCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemPackage this[int index]
        {
            get
            {
                return base[index] as ItemPackage;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemPackage);
        }
    }

    [Serializable]
    abstract public class esItemPackage : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemPackageQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemPackage()
        {
        }

        public esItemPackage(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemID, String detailItemID, String serviceUnitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, detailItemID, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, detailItemID, serviceUnitID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID, String detailItemID, String serviceUnitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, detailItemID, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, detailItemID, serviceUnitID);
        }

        private bool LoadByPrimaryKeyDynamic(String itemID, String detailItemID, String serviceUnitID)
        {
            esItemPackageQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID, query.DetailItemID == detailItemID, query.ServiceUnitID == serviceUnitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemID, String detailItemID, String serviceUnitID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemID", itemID);
            parms.Add("DetailItemID", detailItemID);
            parms.Add("ServiceUnitID", serviceUnitID);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "DetailItemID": this.str.DetailItemID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "Quantity": this.str.Quantity = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "IsStockControl": this.str.IsStockControl = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsExtraItem": this.str.IsExtraItem = (string)value; break;
                        case "DiscountValue": this.str.DiscountValue = (string)value; break;
                        case "IsDiscountInPercent": this.str.IsDiscountInPercent = (string)value; break;
                        case "IsAutoApprove": this.str.IsAutoApprove = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Quantity":

                            if (value == null || value is System.Decimal)
                                this.Quantity = (System.Decimal?)value;
                            break;
                        case "IsStockControl":

                            if (value == null || value is System.Boolean)
                                this.IsStockControl = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsExtraItem":

                            if (value == null || value is System.Boolean)
                                this.IsExtraItem = (System.Boolean?)value;
                            break;
                        case "DiscountValue":

                            if (value == null || value is System.Decimal)
                                this.DiscountValue = (System.Decimal?)value;
                            break;
                        case "IsDiscountInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsDiscountInPercent = (System.Boolean?)value;
                            break;
                        case "IsAutoApprove":

                            if (value == null || value is System.Boolean)
                                this.IsAutoApprove = (System.Boolean?)value;
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
        /// Maps to ItemPackage.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemPackageMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemPackageMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.DetailItemID
        /// </summary>
        virtual public System.String DetailItemID
        {
            get
            {
                return base.GetSystemString(ItemPackageMetadata.ColumnNames.DetailItemID);
            }

            set
            {
                base.SetSystemString(ItemPackageMetadata.ColumnNames.DetailItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ItemPackageMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ItemPackageMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.Quantity
        /// </summary>
        virtual public System.Decimal? Quantity
        {
            get
            {
                return base.GetSystemDecimal(ItemPackageMetadata.ColumnNames.Quantity);
            }

            set
            {
                base.SetSystemDecimal(ItemPackageMetadata.ColumnNames.Quantity, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ItemPackageMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ItemPackageMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.IsStockControl
        /// </summary>
        virtual public System.Boolean? IsStockControl
        {
            get
            {
                return base.GetSystemBoolean(ItemPackageMetadata.ColumnNames.IsStockControl);
            }

            set
            {
                base.SetSystemBoolean(ItemPackageMetadata.ColumnNames.IsStockControl, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(ItemPackageMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(ItemPackageMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemPackageMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemPackageMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemPackageMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemPackageMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.IsExtraItem
        /// </summary>
        virtual public System.Boolean? IsExtraItem
        {
            get
            {
                return base.GetSystemBoolean(ItemPackageMetadata.ColumnNames.IsExtraItem);
            }

            set
            {
                base.SetSystemBoolean(ItemPackageMetadata.ColumnNames.IsExtraItem, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.DiscountValue
        /// </summary>
        virtual public System.Decimal? DiscountValue
        {
            get
            {
                return base.GetSystemDecimal(ItemPackageMetadata.ColumnNames.DiscountValue);
            }

            set
            {
                base.SetSystemDecimal(ItemPackageMetadata.ColumnNames.DiscountValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.IsDiscountInPercent
        /// </summary>
        virtual public System.Boolean? IsDiscountInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemPackageMetadata.ColumnNames.IsDiscountInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemPackageMetadata.ColumnNames.IsDiscountInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackage.IsAutoApprove
        /// </summary>
        virtual public System.Boolean? IsAutoApprove
        {
            get
            {
                return base.GetSystemBoolean(ItemPackageMetadata.ColumnNames.IsAutoApprove);
            }

            set
            {
                base.SetSystemBoolean(ItemPackageMetadata.ColumnNames.IsAutoApprove, value);
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
            public esStrings(esItemPackage entity)
            {
                this.entity = entity;
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
            public System.String DetailItemID
            {
                get
                {
                    System.String data = entity.DetailItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DetailItemID = null;
                    else entity.DetailItemID = Convert.ToString(value);
                }
            }
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String Quantity
            {
                get
                {
                    System.Decimal? data = entity.Quantity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Quantity = null;
                    else entity.Quantity = Convert.ToDecimal(value);
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
            public System.String IsStockControl
            {
                get
                {
                    System.Boolean? data = entity.IsStockControl;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsStockControl = null;
                    else entity.IsStockControl = Convert.ToBoolean(value);
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
            public System.String IsExtraItem
            {
                get
                {
                    System.Boolean? data = entity.IsExtraItem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsExtraItem = null;
                    else entity.IsExtraItem = Convert.ToBoolean(value);
                }
            }
            public System.String DiscountValue
            {
                get
                {
                    System.Decimal? data = entity.DiscountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountValue = null;
                    else entity.DiscountValue = Convert.ToDecimal(value);
                }
            }
            public System.String IsDiscountInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsDiscountInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDiscountInPercent = null;
                    else entity.IsDiscountInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String IsAutoApprove
            {
                get
                {
                    System.Boolean? data = entity.IsAutoApprove;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAutoApprove = null;
                    else entity.IsAutoApprove = Convert.ToBoolean(value);
                }
            }
            private esItemPackage entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemPackageQuery query)
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
                throw new Exception("esItemPackage can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemPackage : esItemPackage
    {
    }

    [Serializable]
    abstract public class esItemPackageQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemPackageMetadata.Meta();
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem DetailItemID
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.DetailItemID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem Quantity
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.Quantity, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem IsStockControl
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.IsStockControl, esSystemType.Boolean);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsExtraItem
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.IsExtraItem, esSystemType.Boolean);
            }
        }

        public esQueryItem DiscountValue
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.DiscountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem IsDiscountInPercent
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.IsDiscountInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAutoApprove
        {
            get
            {
                return new esQueryItem(this, ItemPackageMetadata.ColumnNames.IsAutoApprove, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemPackageCollection")]
    public partial class ItemPackageCollection : esItemPackageCollection, IEnumerable<ItemPackage>
    {
        public ItemPackageCollection()
        {

        }

        public static implicit operator List<ItemPackage>(ItemPackageCollection coll)
        {
            List<ItemPackage> list = new List<ItemPackage>();

            foreach (ItemPackage emp in coll)
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
                return ItemPackageMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemPackageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemPackage(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemPackage();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemPackageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemPackageQuery();
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
        public bool Load(ItemPackageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemPackage AddNew()
        {
            ItemPackage entity = base.AddNewEntity() as ItemPackage;

            return entity;
        }
        public ItemPackage FindByPrimaryKey(String itemID, String detailItemID, String serviceUnitID)
        {
            return base.FindByPrimaryKey(itemID, detailItemID, serviceUnitID) as ItemPackage;
        }

        #region IEnumerable< ItemPackage> Members

        IEnumerator<ItemPackage> IEnumerable<ItemPackage>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemPackage;
            }
        }

        #endregion

        private ItemPackageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemPackage' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemPackage ({ItemID, DetailItemID, ServiceUnitID})")]
    [Serializable]
    public partial class ItemPackage : esItemPackage
    {
        public ItemPackage()
        {
        }

        public ItemPackage(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemPackageMetadata.Meta();
            }
        }

        override protected esItemPackageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemPackageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemPackageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemPackageQuery();
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
        public bool Load(ItemPackageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemPackageQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemPackageQuery : esItemPackageQuery
    {
        public ItemPackageQuery()
        {

        }

        public ItemPackageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemPackageQuery";
        }
    }

    [Serializable]
    public partial class ItemPackageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemPackageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.DetailItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageMetadata.PropertyNames.DetailItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.Quantity, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemPackageMetadata.PropertyNames.Quantity;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.IsStockControl, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemPackageMetadata.PropertyNames.IsStockControl;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemPackageMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemPackageMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.IsExtraItem, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemPackageMetadata.PropertyNames.IsExtraItem;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.DiscountValue, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemPackageMetadata.PropertyNames.DiscountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.IsDiscountInPercent, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemPackageMetadata.PropertyNames.IsDiscountInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageMetadata.ColumnNames.IsAutoApprove, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemPackageMetadata.PropertyNames.IsAutoApprove;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemPackageMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string DetailItemID = "DetailItemID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string Quantity = "Quantity";
            public const string SRItemUnit = "SRItemUnit";
            public const string IsStockControl = "IsStockControl";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsExtraItem = "IsExtraItem";
            public const string DiscountValue = "DiscountValue";
            public const string IsDiscountInPercent = "IsDiscountInPercent";
            public const string IsAutoApprove = "IsAutoApprove";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string DetailItemID = "DetailItemID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string Quantity = "Quantity";
            public const string SRItemUnit = "SRItemUnit";
            public const string IsStockControl = "IsStockControl";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsExtraItem = "IsExtraItem";
            public const string DiscountValue = "DiscountValue";
            public const string IsDiscountInPercent = "IsDiscountInPercent";
            public const string IsAutoApprove = "IsAutoApprove";
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
            lock (typeof(ItemPackageMetadata))
            {
                if (ItemPackageMetadata.mapDelegates == null)
                {
                    ItemPackageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemPackageMetadata.meta == null)
                {
                    ItemPackageMetadata.meta = new ItemPackageMetadata();
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

                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DetailItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsStockControl", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsExtraItem", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DiscountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsDiscountInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAutoApprove", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ItemPackage";
                meta.Destination = "ItemPackage";
                meta.spInsert = "proc_ItemPackageInsert";
                meta.spUpdate = "proc_ItemPackageUpdate";
                meta.spDelete = "proc_ItemPackageDelete";
                meta.spLoadAll = "proc_ItemPackageLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemPackageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemPackageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
