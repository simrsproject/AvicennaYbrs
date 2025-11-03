/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/30/2018 5:00:52 PM
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
    abstract public class esItemConditionRuleServiceUnitCollection : esEntityCollectionWAuditLog
    {
        public esItemConditionRuleServiceUnitCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemConditionRuleServiceUnitCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemConditionRuleServiceUnitQuery query)
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
            this.InitQuery(query as esItemConditionRuleServiceUnitQuery);
        }
        #endregion

        virtual public ItemConditionRuleServiceUnit DetachEntity(ItemConditionRuleServiceUnit entity)
        {
            return base.DetachEntity(entity) as ItemConditionRuleServiceUnit;
        }

        virtual public ItemConditionRuleServiceUnit AttachEntity(ItemConditionRuleServiceUnit entity)
        {
            return base.AttachEntity(entity) as ItemConditionRuleServiceUnit;
        }

        virtual public void Combine(ItemConditionRuleServiceUnitCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemConditionRuleServiceUnit this[int index]
        {
            get
            {
                return base[index] as ItemConditionRuleServiceUnit;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemConditionRuleServiceUnit);
        }
    }

    [Serializable]
    abstract public class esItemConditionRuleServiceUnit : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemConditionRuleServiceUnitQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemConditionRuleServiceUnit()
        {
        }

        public esItemConditionRuleServiceUnit(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemConditionRuleID, String serviceUnitID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemConditionRuleID, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemConditionRuleID, serviceUnitID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemConditionRuleID, String serviceUnitID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemConditionRuleID, serviceUnitID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemConditionRuleID, serviceUnitID);
        }

        private bool LoadByPrimaryKeyDynamic(String itemConditionRuleID, String serviceUnitID)
        {
            esItemConditionRuleServiceUnitQuery query = this.GetDynamicQuery();
            query.Where(query.ItemConditionRuleID == itemConditionRuleID, query.ServiceUnitID == serviceUnitID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemConditionRuleID, String serviceUnitID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemConditionRuleID", itemConditionRuleID);
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
                        case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
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
        /// Maps to ItemConditionRuleServiceUnit.ItemConditionRuleID
        /// </summary>
        virtual public System.String ItemConditionRuleID
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.ItemConditionRuleID);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.ItemConditionRuleID, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRuleServiceUnit.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRuleServiceUnit.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemConditionRuleServiceUnit.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemConditionRuleServiceUnit entity)
            {
                this.entity = entity;
            }
            public System.String ItemConditionRuleID
            {
                get
                {
                    System.String data = entity.ItemConditionRuleID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemConditionRuleID = null;
                    else entity.ItemConditionRuleID = Convert.ToString(value);
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
            private esItemConditionRuleServiceUnit entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemConditionRuleServiceUnitQuery query)
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
                throw new Exception("esItemConditionRuleServiceUnit can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemConditionRuleServiceUnit : esItemConditionRuleServiceUnit
    {
    }

    [Serializable]
    abstract public class esItemConditionRuleServiceUnitQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemConditionRuleServiceUnitMetadata.Meta();
            }
        }

        public esQueryItem ItemConditionRuleID
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleServiceUnitMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemConditionRuleServiceUnitCollection")]
    public partial class ItemConditionRuleServiceUnitCollection : esItemConditionRuleServiceUnitCollection, IEnumerable<ItemConditionRuleServiceUnit>
    {
        public ItemConditionRuleServiceUnitCollection()
        {

        }

        public static implicit operator List<ItemConditionRuleServiceUnit>(ItemConditionRuleServiceUnitCollection coll)
        {
            List<ItemConditionRuleServiceUnit> list = new List<ItemConditionRuleServiceUnit>();

            foreach (ItemConditionRuleServiceUnit emp in coll)
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
                return ItemConditionRuleServiceUnitMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemConditionRuleServiceUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemConditionRuleServiceUnit(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemConditionRuleServiceUnit();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemConditionRuleServiceUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemConditionRuleServiceUnitQuery();
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
        public bool Load(ItemConditionRuleServiceUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemConditionRuleServiceUnit AddNew()
        {
            ItemConditionRuleServiceUnit entity = base.AddNewEntity() as ItemConditionRuleServiceUnit;

            return entity;
        }
        public ItemConditionRuleServiceUnit FindByPrimaryKey(String itemConditionRuleID, String serviceUnitID)
        {
            return base.FindByPrimaryKey(itemConditionRuleID, serviceUnitID) as ItemConditionRuleServiceUnit;
        }

        #region IEnumerable< ItemConditionRuleServiceUnit> Members

        IEnumerator<ItemConditionRuleServiceUnit> IEnumerable<ItemConditionRuleServiceUnit>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemConditionRuleServiceUnit;
            }
        }

        #endregion

        private ItemConditionRuleServiceUnitQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemConditionRuleServiceUnit' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemConditionRuleServiceUnit ({ItemConditionRuleID, ServiceUnitID})")]
    [Serializable]
    public partial class ItemConditionRuleServiceUnit : esItemConditionRuleServiceUnit
    {
        public ItemConditionRuleServiceUnit()
        {
        }

        public ItemConditionRuleServiceUnit(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemConditionRuleServiceUnitMetadata.Meta();
            }
        }

        override protected esItemConditionRuleServiceUnitQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemConditionRuleServiceUnitQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemConditionRuleServiceUnitQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemConditionRuleServiceUnitQuery();
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
        public bool Load(ItemConditionRuleServiceUnitQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemConditionRuleServiceUnitQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemConditionRuleServiceUnitQuery : esItemConditionRuleServiceUnitQuery
    {
        public ItemConditionRuleServiceUnitQuery()
        {

        }

        public ItemConditionRuleServiceUnitQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemConditionRuleServiceUnitQuery";
        }
    }

    [Serializable]
    public partial class ItemConditionRuleServiceUnitMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemConditionRuleServiceUnitMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemConditionRuleServiceUnitMetadata.ColumnNames.ItemConditionRuleID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleServiceUnitMetadata.PropertyNames.ItemConditionRuleID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleServiceUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleServiceUnitMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemConditionRuleServiceUnitMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemConditionRuleServiceUnitMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemConditionRuleServiceUnitMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemConditionRuleServiceUnitMetadata Meta()
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
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string ServiceUnitID = "ServiceUnitID";
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
            lock (typeof(ItemConditionRuleServiceUnitMetadata))
            {
                if (ItemConditionRuleServiceUnitMetadata.mapDelegates == null)
                {
                    ItemConditionRuleServiceUnitMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemConditionRuleServiceUnitMetadata.meta == null)
                {
                    ItemConditionRuleServiceUnitMetadata.meta = new ItemConditionRuleServiceUnitMetadata();
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

                meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemConditionRuleServiceUnit";
                meta.Destination = "ItemConditionRuleServiceUnit";
                meta.spInsert = "proc_ItemConditionRuleServiceUnitInsert";
                meta.spUpdate = "proc_ItemConditionRuleServiceUnitUpdate";
                meta.spDelete = "proc_ItemConditionRuleServiceUnitDelete";
                meta.spLoadAll = "proc_ItemConditionRuleServiceUnitLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemConditionRuleServiceUnitLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemConditionRuleServiceUnitMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
