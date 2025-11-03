/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/21/2017 1:06:29 PM
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
    abstract public class esItemMovementPerDateCollection : esEntityCollectionWAuditLog
    {
        public esItemMovementPerDateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemMovementPerDateCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemMovementPerDateQuery query)
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
            this.InitQuery(query as esItemMovementPerDateQuery);
        }
        #endregion

        virtual public ItemMovementPerDate DetachEntity(ItemMovementPerDate entity)
        {
            return base.DetachEntity(entity) as ItemMovementPerDate;
        }

        virtual public ItemMovementPerDate AttachEntity(ItemMovementPerDate entity)
        {
            return base.AttachEntity(entity) as ItemMovementPerDate;
        }

        virtual public void Combine(ItemMovementPerDateCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemMovementPerDate this[int index]
        {
            get
            {
                return base[index] as ItemMovementPerDate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemMovementPerDate);
        }
    }

    [Serializable]
    abstract public class esItemMovementPerDate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemMovementPerDateQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemMovementPerDate()
        {
        }

        public esItemMovementPerDate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(DateTime movementDate, String locationID, String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementDate, locationID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementDate, locationID, itemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, DateTime movementDate, String locationID, String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(movementDate, locationID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(movementDate, locationID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(DateTime movementDate, String locationID, String itemID)
        {
            esItemMovementPerDateQuery query = this.GetDynamicQuery();
            query.Where(query.MovementDate == movementDate, query.LocationID == locationID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(DateTime movementDate, String locationID, String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("MovementDate", movementDate);
            parms.Add("LocationID", locationID);
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
                        case "MovementDate": this.str.MovementDate = (string)value; break;
                        case "LocationID": this.str.LocationID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "QuantityIn": this.str.QuantityIn = (string)value; break;
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
                        case "QuantityIn":

                            if (value == null || value is System.Decimal)
                                this.QuantityIn = (System.Decimal?)value;
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
        /// Maps to ItemMovementPerDate.MovementDate
        /// </summary>
        virtual public System.DateTime? MovementDate
        {
            get
            {
                return base.GetSystemDateTime(ItemMovementPerDateMetadata.ColumnNames.MovementDate);
            }

            set
            {
                base.SetSystemDateTime(ItemMovementPerDateMetadata.ColumnNames.MovementDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.LocationID
        /// </summary>
        virtual public System.String LocationID
        {
            get
            {
                return base.GetSystemString(ItemMovementPerDateMetadata.ColumnNames.LocationID);
            }

            set
            {
                base.SetSystemString(ItemMovementPerDateMetadata.ColumnNames.LocationID, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemMovementPerDateMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemMovementPerDateMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.QuantityIn
        /// </summary>
        virtual public System.Decimal? QuantityIn
        {
            get
            {
                return base.GetSystemDecimal(ItemMovementPerDateMetadata.ColumnNames.QuantityIn);
            }

            set
            {
                base.SetSystemDecimal(ItemMovementPerDateMetadata.ColumnNames.QuantityIn, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.QuantityOut
        /// </summary>
        virtual public System.Decimal? QuantityOut
        {
            get
            {
                return base.GetSystemDecimal(ItemMovementPerDateMetadata.ColumnNames.QuantityOut);
            }

            set
            {
                base.SetSystemDecimal(ItemMovementPerDateMetadata.ColumnNames.QuantityOut, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemMovementPerDateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemMovementPerDateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemMovementPerDate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemMovementPerDateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemMovementPerDateMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemMovementPerDate entity)
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
            public System.String QuantityIn
            {
                get
                {
                    System.Decimal? data = entity.QuantityIn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuantityIn = null;
                    else entity.QuantityIn = Convert.ToDecimal(value);
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
            private esItemMovementPerDate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemMovementPerDateQuery query)
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
                throw new Exception("esItemMovementPerDate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemMovementPerDate : esItemMovementPerDate
    {
    }

    [Serializable]
    abstract public class esItemMovementPerDateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemMovementPerDateMetadata.Meta();
            }
        }

        public esQueryItem MovementDate
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
            }
        }

        public esQueryItem LocationID
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.LocationID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem QuantityIn
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.QuantityIn, esSystemType.Decimal);
            }
        }

        public esQueryItem QuantityOut
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemMovementPerDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemMovementPerDateCollection")]
    public partial class ItemMovementPerDateCollection : esItemMovementPerDateCollection, IEnumerable<ItemMovementPerDate>
    {
        public ItemMovementPerDateCollection()
        {

        }

        public static implicit operator List<ItemMovementPerDate>(ItemMovementPerDateCollection coll)
        {
            List<ItemMovementPerDate> list = new List<ItemMovementPerDate>();

            foreach (ItemMovementPerDate emp in coll)
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
                return ItemMovementPerDateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemMovementPerDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemMovementPerDate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemMovementPerDate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemMovementPerDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemMovementPerDateQuery();
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
        public bool Load(ItemMovementPerDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemMovementPerDate AddNew()
        {
            ItemMovementPerDate entity = base.AddNewEntity() as ItemMovementPerDate;

            return entity;
        }
        public ItemMovementPerDate FindByPrimaryKey(DateTime movementDate, String locationID, String itemID)
        {
            return base.FindByPrimaryKey(movementDate, locationID, itemID) as ItemMovementPerDate;
        }

        #region IEnumerable< ItemMovementPerDate> Members

        IEnumerator<ItemMovementPerDate> IEnumerable<ItemMovementPerDate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemMovementPerDate;
            }
        }

        #endregion

        private ItemMovementPerDateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemMovementPerDate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemMovementPerDate ({MovementDate, LocationID, ItemID})")]
    [Serializable]
    public partial class ItemMovementPerDate : esItemMovementPerDate
    {
        public ItemMovementPerDate()
        {
        }

        public ItemMovementPerDate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemMovementPerDateMetadata.Meta();
            }
        }

        override protected esItemMovementPerDateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemMovementPerDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemMovementPerDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemMovementPerDateQuery();
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
        public bool Load(ItemMovementPerDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemMovementPerDateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemMovementPerDateQuery : esItemMovementPerDateQuery
    {
        public ItemMovementPerDateQuery()
        {

        }

        public ItemMovementPerDateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemMovementPerDateQuery";
        }
    }

    [Serializable]
    public partial class ItemMovementPerDateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemMovementPerDateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.MovementDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.MovementDate;
            c.IsInPrimaryKey = true;
            c.HasDefault = true;
            c.Default = @"(getdate())";
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.LocationID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.LocationID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.QuantityIn, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.QuantityIn;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.QuantityOut, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.QuantityOut;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemMovementPerDateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemMovementPerDateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemMovementPerDateMetadata Meta()
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
            public const string LocationID = "LocationID";
            public const string ItemID = "ItemID";
            public const string QuantityIn = "QuantityIn";
            public const string QuantityOut = "QuantityOut";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MovementDate = "MovementDate";
            public const string LocationID = "LocationID";
            public const string ItemID = "ItemID";
            public const string QuantityIn = "QuantityIn";
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
            lock (typeof(ItemMovementPerDateMetadata))
            {
                if (ItemMovementPerDateMetadata.mapDelegates == null)
                {
                    ItemMovementPerDateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemMovementPerDateMetadata.meta == null)
                {
                    ItemMovementPerDateMetadata.meta = new ItemMovementPerDateMetadata();
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
                meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("QuantityIn", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemMovementPerDate";
                meta.Destination = "ItemMovementPerDate";
                meta.spInsert = "proc_ItemMovementPerDateInsert";
                meta.spUpdate = "proc_ItemMovementPerDateUpdate";
                meta.spDelete = "proc_ItemMovementPerDateDelete";
                meta.spLoadAll = "proc_ItemMovementPerDateLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemMovementPerDateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemMovementPerDateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
