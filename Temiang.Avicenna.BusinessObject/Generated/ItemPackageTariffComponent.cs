/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/5/2016 3:42:53 PM
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
    abstract public class esItemPackageTariffComponentCollection : esEntityCollectionWAuditLog
    {
        public esItemPackageTariffComponentCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemPackageTariffComponentCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemPackageTariffComponentQuery query)
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
            this.InitQuery(query as esItemPackageTariffComponentQuery);
        }
        #endregion

        virtual public ItemPackageTariffComponent DetachEntity(ItemPackageTariffComponent entity)
        {
            return base.DetachEntity(entity) as ItemPackageTariffComponent;
        }

        virtual public ItemPackageTariffComponent AttachEntity(ItemPackageTariffComponent entity)
        {
            return base.AttachEntity(entity) as ItemPackageTariffComponent;
        }

        virtual public void Combine(ItemPackageTariffComponentCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemPackageTariffComponent this[int index]
        {
            get
            {
                return base[index] as ItemPackageTariffComponent;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemPackageTariffComponent);
        }
    }

    [Serializable]
    abstract public class esItemPackageTariffComponent : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemPackageTariffComponentQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemPackageTariffComponent()
        {
        }

        public esItemPackageTariffComponent(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemID, String detailItemID, String tariffComponentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, detailItemID, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, detailItemID, tariffComponentID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID, String detailItemID, String tariffComponentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, detailItemID, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, detailItemID, tariffComponentID);
        }

        private bool LoadByPrimaryKeyDynamic(String itemID, String detailItemID, String tariffComponentID)
        {
            esItemPackageTariffComponentQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID, query.DetailItemID == detailItemID, query.TariffComponentID == tariffComponentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemID, String detailItemID, String tariffComponentID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemID", itemID);
            parms.Add("DetailItemID", detailItemID);
            parms.Add("TariffComponentID", tariffComponentID);
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
                        case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "Discount": this.str.Discount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "Discount":

                            if (value == null || value is System.Decimal)
                                this.Discount = (System.Decimal?)value;
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
        /// Maps to ItemPackageTariffComponent.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.DetailItemID
        /// </summary>
        virtual public System.String DetailItemID
        {
            get
            {
                return base.GetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.DetailItemID);
            }

            set
            {
                base.SetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.DetailItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.TariffComponentID
        /// </summary>
        virtual public System.String TariffComponentID
        {
            get
            {
                return base.GetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.TariffComponentID);
            }

            set
            {
                base.SetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.TariffComponentID, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(ItemPackageTariffComponentMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(ItemPackageTariffComponentMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.Discount
        /// </summary>
        virtual public System.Decimal? Discount
        {
            get
            {
                return base.GetSystemDecimal(ItemPackageTariffComponentMetadata.ColumnNames.Discount);
            }

            set
            {
                base.SetSystemDecimal(ItemPackageTariffComponentMetadata.ColumnNames.Discount, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemPackageTariffComponent.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemPackageTariffComponent entity)
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
            public System.String TariffComponentID
            {
                get
                {
                    System.String data = entity.TariffComponentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffComponentID = null;
                    else entity.TariffComponentID = Convert.ToString(value);
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
            public System.String Discount
            {
                get
                {
                    System.Decimal? data = entity.Discount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Discount = null;
                    else entity.Discount = Convert.ToDecimal(value);
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
            private esItemPackageTariffComponent entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemPackageTariffComponentQuery query)
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
                throw new Exception("esItemPackageTariffComponent can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemPackageTariffComponent : esItemPackageTariffComponent
    {
    }

    [Serializable]
    abstract public class esItemPackageTariffComponentQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemPackageTariffComponentMetadata.Meta();
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem DetailItemID
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.DetailItemID, esSystemType.String);
            }
        }

        public esQueryItem TariffComponentID
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.TariffComponentID, esSystemType.String);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem Discount
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.Discount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemPackageTariffComponentCollection")]
    public partial class ItemPackageTariffComponentCollection : esItemPackageTariffComponentCollection, IEnumerable<ItemPackageTariffComponent>
    {
        public ItemPackageTariffComponentCollection()
        {

        }

        public static implicit operator List<ItemPackageTariffComponent>(ItemPackageTariffComponentCollection coll)
        {
            List<ItemPackageTariffComponent> list = new List<ItemPackageTariffComponent>();

            foreach (ItemPackageTariffComponent emp in coll)
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
                return ItemPackageTariffComponentMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemPackageTariffComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemPackageTariffComponent(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemPackageTariffComponent();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemPackageTariffComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemPackageTariffComponentQuery();
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
        public bool Load(ItemPackageTariffComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemPackageTariffComponent AddNew()
        {
            ItemPackageTariffComponent entity = base.AddNewEntity() as ItemPackageTariffComponent;

            return entity;
        }
        public ItemPackageTariffComponent FindByPrimaryKey(String itemID, String detailItemID, String tariffComponentID)
        {
            return base.FindByPrimaryKey(itemID, detailItemID, tariffComponentID) as ItemPackageTariffComponent;
        }

        #region IEnumerable< ItemPackageTariffComponent> Members

        IEnumerator<ItemPackageTariffComponent> IEnumerable<ItemPackageTariffComponent>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemPackageTariffComponent;
            }
        }

        #endregion

        private ItemPackageTariffComponentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemPackageTariffComponent' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemPackageTariffComponent ({ItemID, DetailItemID, TariffComponentID})")]
    [Serializable]
    public partial class ItemPackageTariffComponent : esItemPackageTariffComponent
    {
        public ItemPackageTariffComponent()
        {
        }

        public ItemPackageTariffComponent(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemPackageTariffComponentMetadata.Meta();
            }
        }

        override protected esItemPackageTariffComponentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemPackageTariffComponentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemPackageTariffComponentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemPackageTariffComponentQuery();
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
        public bool Load(ItemPackageTariffComponentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemPackageTariffComponentQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemPackageTariffComponentQuery : esItemPackageTariffComponentQuery
    {
        public ItemPackageTariffComponentQuery()
        {

        }

        public ItemPackageTariffComponentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemPackageTariffComponentQuery";
        }
    }

    [Serializable]
    public partial class ItemPackageTariffComponentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemPackageTariffComponentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.DetailItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.DetailItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.TariffComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.Discount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.Discount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(ItemPackageTariffComponentMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemPackageTariffComponentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);


        }
        #endregion

        static public ItemPackageTariffComponentMetadata Meta()
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
            public const string TariffComponentID = "TariffComponentID";
            public const string Price = "Price";
            public const string Discount = "Discount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string DetailItemID = "DetailItemID";
            public const string TariffComponentID = "TariffComponentID";
            public const string Price = "Price";
            public const string Discount = "Discount";
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
            lock (typeof(ItemPackageTariffComponentMetadata))
            {
                if (ItemPackageTariffComponentMetadata.mapDelegates == null)
                {
                    ItemPackageTariffComponentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemPackageTariffComponentMetadata.meta == null)
                {
                    ItemPackageTariffComponentMetadata.meta = new ItemPackageTariffComponentMetadata();
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
                meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemPackageTariffComponent";
                meta.Destination = "ItemPackageTariffComponent";
                meta.spInsert = "proc_ItemPackageTariffComponentInsert";
                meta.spUpdate = "proc_ItemPackageTariffComponentUpdate";
                meta.spDelete = "proc_ItemPackageTariffComponentDelete";
                meta.spLoadAll = "proc_ItemPackageTariffComponentLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemPackageTariffComponentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemPackageTariffComponentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
