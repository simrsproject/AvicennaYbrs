/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/25/18 10:02:30 AM
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
    abstract public class esItemSalesPerDateCollection : esEntityCollectionWAuditLog
    {
        public esItemSalesPerDateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemSalesPerDateCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemSalesPerDateQuery query)
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
            this.InitQuery(query as esItemSalesPerDateQuery);
        }
        #endregion

        virtual public ItemSalesPerDate DetachEntity(ItemSalesPerDate entity)
        {
            return base.DetachEntity(entity) as ItemSalesPerDate;
        }

        virtual public ItemSalesPerDate AttachEntity(ItemSalesPerDate entity)
        {
            return base.AttachEntity(entity) as ItemSalesPerDate;
        }

        virtual public void Combine(ItemSalesPerDateCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemSalesPerDate this[int index]
        {
            get
            {
                return base[index] as ItemSalesPerDate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemSalesPerDate);
        }
    }

    [Serializable]
    abstract public class esItemSalesPerDate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemSalesPerDateQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemSalesPerDate()
        {
        }

        public esItemSalesPerDate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(DateTime movementDate, String sRStockGroup, String itemID, String serviceUnitID, String locationID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementDate, sRStockGroup, itemID, serviceUnitID, locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementDate, sRStockGroup, itemID, serviceUnitID, locationID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, DateTime movementDate, String sRStockGroup, String itemID, String serviceUnitID, String locationID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementDate, sRStockGroup, itemID, serviceUnitID, locationID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementDate, sRStockGroup, itemID, serviceUnitID, locationID);
        }

        private bool LoadByPrimaryKeyDynamic(DateTime movementDate, String sRStockGroup, String itemID, String serviceUnitID, String locationID)
        {
            esItemSalesPerDateQuery query = this.GetDynamicQuery();
            query.Where(query.MovementDate == movementDate, query.SRStockGroup == sRStockGroup, query.ItemID == itemID, query.ServiceUnitID == serviceUnitID, query.LocationID == locationID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(DateTime movementDate, String sRStockGroup, String itemID, String serviceUnitID, String locationID)
        {
            esParameters parms = new esParameters();
            parms.Add("MovementDate", movementDate);
            parms.Add("SRStockGroup", sRStockGroup);
            parms.Add("ItemID", itemID);
            parms.Add("ServiceUnitID", serviceUnitID);
            parms.Add("LocationID", locationID);
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
                        case "MovementDate": this.str.MovementDate = (string)value; break;
                        case "SRStockGroup": this.str.SRStockGroup = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "QuantityOut": this.str.QuantityOut = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MovementDate":

                            if (value == null || value is System.DateTime)
                                this.MovementDate = (System.DateTime?)value;
                            break;
                        case "QuantityOut":

                            if (value == null || value is System.Decimal)
                                this.QuantityOut = (System.Decimal?)value;
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
        /// Maps to ItemSalesPerDate.MovementDate
        /// </summary>
        virtual public System.DateTime? MovementDate
        {
            get
            {
                return base.GetSystemDateTime(ItemSalesPerDateMetadata.ColumnNames.MovementDate);
            }

            set
            {
                base.SetSystemDateTime(ItemSalesPerDateMetadata.ColumnNames.MovementDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.SRStockGroup
        /// </summary>
        virtual public System.String SRStockGroup
        {
            get
            {
                return base.GetSystemString(ItemSalesPerDateMetadata.ColumnNames.SRStockGroup);
            }

            set
            {
                base.SetSystemString(ItemSalesPerDateMetadata.ColumnNames.SRStockGroup, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemSalesPerDateMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemSalesPerDateMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ItemSalesPerDateMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ItemSalesPerDateMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(ItemSalesPerDateMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(ItemSalesPerDateMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.QuantityOut
        /// </summary>
        virtual public System.Decimal? QuantityOut
        {
            get
            {
                return base.GetSystemDecimal(ItemSalesPerDateMetadata.ColumnNames.QuantityOut);
            }

            set
            {
                base.SetSystemDecimal(ItemSalesPerDateMetadata.ColumnNames.QuantityOut, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemSalesPerDateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemSalesPerDateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemSalesPerDate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemSalesPerDateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemSalesPerDateMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemSalesPerDate entity)
            {
                this.entity = entity;
            }
            public System.String MovementDate
            {
                get
                {
                    System.DateTime? data = entity.MovementDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MovementDate = null;
                    else entity.MovementDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRStockGroup
            {
                get
                {
                    System.String data = entity.SRStockGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRStockGroup = null;
                    else entity.SRStockGroup = Convert.ToString(value);
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
            public System.String QuantityOut
            {
                get
                {
                    System.Decimal? data = entity.QuantityOut;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuantityOut = null;
                    else entity.QuantityOut = Convert.ToDecimal(value);
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
            private esItemSalesPerDate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemSalesPerDateQuery query)
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
                throw new Exception("esItemSalesPerDate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemSalesPerDate : esItemSalesPerDate
    {
    }

    [Serializable]
    abstract public class esItemSalesPerDateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemSalesPerDateMetadata.Meta();
            }
        }

        public esQueryItem MovementDate
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRStockGroup
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.SRStockGroup, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem QuantityOut
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemSalesPerDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemSalesPerDateCollection")]
    public partial class ItemSalesPerDateCollection : esItemSalesPerDateCollection, IEnumerable<ItemSalesPerDate>
    {
        public ItemSalesPerDateCollection()
        {

        }

        public static implicit operator List<ItemSalesPerDate>(ItemSalesPerDateCollection coll)
        {
            List<ItemSalesPerDate> list = new List<ItemSalesPerDate>();

            foreach (ItemSalesPerDate emp in coll)
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
                return ItemSalesPerDateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemSalesPerDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemSalesPerDate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemSalesPerDate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemSalesPerDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemSalesPerDateQuery();
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
        public bool Load(ItemSalesPerDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemSalesPerDate AddNew()
        {
            ItemSalesPerDate entity = base.AddNewEntity() as ItemSalesPerDate;

            return entity;
        }
        public ItemSalesPerDate FindByPrimaryKey(DateTime movementDate, String sRStockGroup, String itemID, String serviceUnitID, String locationID)
        {
            return base.FindByPrimaryKey(movementDate, sRStockGroup, itemID, serviceUnitID, locationID) as ItemSalesPerDate;
        }

        #region IEnumerable< ItemSalesPerDate> Members

        IEnumerator<ItemSalesPerDate> IEnumerable<ItemSalesPerDate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemSalesPerDate;
            }
        }

        #endregion

        private ItemSalesPerDateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemSalesPerDate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemSalesPerDate ({MovementDate, SRStockGroup, ItemID, ServiceUnitID, LocationID})")]
    [Serializable]
    public partial class ItemSalesPerDate : esItemSalesPerDate
    {
        public ItemSalesPerDate()
        {
        }

        public ItemSalesPerDate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemSalesPerDateMetadata.Meta();
            }
        }

        override protected esItemSalesPerDateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemSalesPerDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemSalesPerDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemSalesPerDateQuery();
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
        public bool Load(ItemSalesPerDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemSalesPerDateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemSalesPerDateQuery : esItemSalesPerDateQuery
    {
        public ItemSalesPerDateQuery()
        {

        }

        public ItemSalesPerDateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemSalesPerDateQuery";
        }
    }

    [Serializable]
    public partial class ItemSalesPerDateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemSalesPerDateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.MovementDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.MovementDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.SRStockGroup, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.SRStockGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.LocationID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.LocationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.QuantityOut, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.QuantityOut;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemSalesPerDateMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemSalesPerDateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemSalesPerDateMetadata Meta()
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
            public const string MovementDate = "MovementDate";
            public const string SRStockGroup = "SRStockGroup";
            public const string ItemID = "ItemID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string LocationID = "LocationID";
            public const string QuantityOut = "QuantityOut";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MovementDate = "MovementDate";
            public const string SRStockGroup = "SRStockGroup";
            public const string ItemID = "ItemID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string LocationID = "LocationID";
            public const string QuantityOut = "QuantityOut";
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
            lock (typeof(ItemSalesPerDateMetadata))
            {
                if (ItemSalesPerDateMetadata.mapDelegates == null)
                {
                    ItemSalesPerDateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemSalesPerDateMetadata.meta == null)
                {
                    ItemSalesPerDateMetadata.meta = new ItemSalesPerDateMetadata();
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

                meta.AddTypeMap("MovementDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRStockGroup", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemSalesPerDate";
                meta.Destination = "ItemSalesPerDate";
                meta.spInsert = "proc_ItemSalesPerDateInsert";
                meta.spUpdate = "proc_ItemSalesPerDateUpdate";
                meta.spDelete = "proc_ItemSalesPerDateDelete";
                meta.spLoadAll = "proc_ItemSalesPerDateLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemSalesPerDateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemSalesPerDateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
