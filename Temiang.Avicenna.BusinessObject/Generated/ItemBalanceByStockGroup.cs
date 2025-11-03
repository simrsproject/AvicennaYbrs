/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/24/18 9:30:17 AM
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
    abstract public class esItemBalanceByStockGroupCollection : esEntityCollectionWAuditLog
    {
        public esItemBalanceByStockGroupCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemBalanceByStockGroupCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemBalanceByStockGroupQuery query)
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
            this.InitQuery(query as esItemBalanceByStockGroupQuery);
        }
        #endregion

        virtual public ItemBalanceByStockGroup DetachEntity(ItemBalanceByStockGroup entity)
        {
            return base.DetachEntity(entity) as ItemBalanceByStockGroup;
        }

        virtual public ItemBalanceByStockGroup AttachEntity(ItemBalanceByStockGroup entity)
        {
            return base.AttachEntity(entity) as ItemBalanceByStockGroup;
        }

        virtual public void Combine(ItemBalanceByStockGroupCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemBalanceByStockGroup this[int index]
        {
            get
            {
                return base[index] as ItemBalanceByStockGroup;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemBalanceByStockGroup);
        }
    }

    [Serializable]
    abstract public class esItemBalanceByStockGroup : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemBalanceByStockGroupQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemBalanceByStockGroup()
        {
        }

        public esItemBalanceByStockGroup(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRStockGroup, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRStockGroup, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRStockGroup, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRStockGroup, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRStockGroup, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRStockGroup, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(String sRStockGroup, String itemID)
        {
            esItemBalanceByStockGroupQuery query = this.GetDynamicQuery();
            query.Where(query.SRStockGroup == sRStockGroup, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRStockGroup, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRStockGroup", sRStockGroup);
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
                        case "SRStockGroup": this.str.SRStockGroup = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "Minimum": this.str.Minimum = (string)value; break;
                        case "Maximum": this.str.Maximum = (string)value; break;
                        case "Balance": this.str.Balance = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Minimum":

                            if (value == null || value is System.Decimal)
                                this.Minimum = (System.Decimal?)value;
                            break;
                        case "Maximum":

                            if (value == null || value is System.Decimal)
                                this.Maximum = (System.Decimal?)value;
                            break;
                        case "Balance":

                            if (value == null || value is System.Decimal)
                                this.Balance = (System.Decimal?)value;
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
        /// Maps to ItemBalanceByStockGroup.SRStockGroup
        /// </summary>
        virtual public System.String SRStockGroup
        {
            get
            {
                return base.GetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.SRStockGroup);
            }

            set
            {
                base.SetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.SRStockGroup, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.Minimum
        /// </summary>
        virtual public System.Decimal? Minimum
        {
            get
            {
                return base.GetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Minimum);
            }

            set
            {
                base.SetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Minimum, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.Maximum
        /// </summary>
        virtual public System.Decimal? Maximum
        {
            get
            {
                return base.GetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Maximum);
            }

            set
            {
                base.SetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Maximum, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.Balance
        /// </summary>
        virtual public System.Decimal? Balance
        {
            get
            {
                return base.GetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Balance);
            }

            set
            {
                base.SetSystemDecimal(ItemBalanceByStockGroupMetadata.ColumnNames.Balance, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemBalanceByStockGroup.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemBalanceByStockGroup entity)
            {
                this.entity = entity;
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
            public System.String Minimum
            {
                get
                {
                    System.Decimal? data = entity.Minimum;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Minimum = null;
                    else entity.Minimum = Convert.ToDecimal(value);
                }
            }
            public System.String Maximum
            {
                get
                {
                    System.Decimal? data = entity.Maximum;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Maximum = null;
                    else entity.Maximum = Convert.ToDecimal(value);
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
            private esItemBalanceByStockGroup entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemBalanceByStockGroupQuery query)
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
                throw new Exception("esItemBalanceByStockGroup can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemBalanceByStockGroup : esItemBalanceByStockGroup
    {
    }

    [Serializable]
    abstract public class esItemBalanceByStockGroupQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemBalanceByStockGroupMetadata.Meta();
            }
        }

        public esQueryItem SRStockGroup
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.SRStockGroup, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem Minimum
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.Minimum, esSystemType.Decimal);
            }
        }

        public esQueryItem Maximum
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.Maximum, esSystemType.Decimal);
            }
        }

        public esQueryItem Balance
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.Balance, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemBalanceByStockGroupCollection")]
    public partial class ItemBalanceByStockGroupCollection : esItemBalanceByStockGroupCollection, IEnumerable<ItemBalanceByStockGroup>
    {
        public ItemBalanceByStockGroupCollection()
        {

        }

        public static implicit operator List<ItemBalanceByStockGroup>(ItemBalanceByStockGroupCollection coll)
        {
            List<ItemBalanceByStockGroup> list = new List<ItemBalanceByStockGroup>();

            foreach (ItemBalanceByStockGroup emp in coll)
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
                return ItemBalanceByStockGroupMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemBalanceByStockGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemBalanceByStockGroup(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemBalanceByStockGroup();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemBalanceByStockGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemBalanceByStockGroupQuery();
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
        public bool Load(ItemBalanceByStockGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemBalanceByStockGroup AddNew()
        {
            ItemBalanceByStockGroup entity = base.AddNewEntity() as ItemBalanceByStockGroup;

            return entity;
        }
        public ItemBalanceByStockGroup FindByPrimaryKey(String sRStockGroup, String itemID)
        {
            return base.FindByPrimaryKey(sRStockGroup, itemID) as ItemBalanceByStockGroup;
        }

        #region IEnumerable< ItemBalanceByStockGroup> Members

        IEnumerator<ItemBalanceByStockGroup> IEnumerable<ItemBalanceByStockGroup>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemBalanceByStockGroup;
            }
        }

        #endregion

        private ItemBalanceByStockGroupQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemBalanceByStockGroup' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemBalanceByStockGroup ({SRStockGroup, ItemID})")]
    [Serializable]
    public partial class ItemBalanceByStockGroup : esItemBalanceByStockGroup
    {
        public ItemBalanceByStockGroup()
        {
        }

        public ItemBalanceByStockGroup(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemBalanceByStockGroupMetadata.Meta();
            }
        }

        override protected esItemBalanceByStockGroupQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemBalanceByStockGroupQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemBalanceByStockGroupQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemBalanceByStockGroupQuery();
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
        public bool Load(ItemBalanceByStockGroupQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemBalanceByStockGroupQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemBalanceByStockGroupQuery : esItemBalanceByStockGroupQuery
    {
        public ItemBalanceByStockGroupQuery()
        {

        }

        public ItemBalanceByStockGroupQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemBalanceByStockGroupQuery";
        }
    }

    [Serializable]
    public partial class ItemBalanceByStockGroupMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemBalanceByStockGroupMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.SRStockGroup, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.SRStockGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.Minimum, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.Minimum;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.Maximum, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.Maximum;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.Balance, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.Balance;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemBalanceByStockGroupMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemBalanceByStockGroupMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemBalanceByStockGroupMetadata Meta()
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
            public const string SRStockGroup = "SRStockGroup";
            public const string ItemID = "ItemID";
            public const string Minimum = "Minimum";
            public const string Maximum = "Maximum";
            public const string Balance = "Balance";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRStockGroup = "SRStockGroup";
            public const string ItemID = "ItemID";
            public const string Minimum = "Minimum";
            public const string Maximum = "Maximum";
            public const string Balance = "Balance";
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
            lock (typeof(ItemBalanceByStockGroupMetadata))
            {
                if (ItemBalanceByStockGroupMetadata.mapDelegates == null)
                {
                    ItemBalanceByStockGroupMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemBalanceByStockGroupMetadata.meta == null)
                {
                    ItemBalanceByStockGroupMetadata.meta = new ItemBalanceByStockGroupMetadata();
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

                meta.AddTypeMap("SRStockGroup", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Minimum", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Maximum", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemBalanceByStockGroup";
                meta.Destination = "ItemBalanceByStockGroup";
                meta.spInsert = "proc_ItemBalanceByStockGroupInsert";
                meta.spUpdate = "proc_ItemBalanceByStockGroupUpdate";
                meta.spDelete = "proc_ItemBalanceByStockGroupDelete";
                meta.spLoadAll = "proc_ItemBalanceByStockGroupLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemBalanceByStockGroupLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemBalanceByStockGroupMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
