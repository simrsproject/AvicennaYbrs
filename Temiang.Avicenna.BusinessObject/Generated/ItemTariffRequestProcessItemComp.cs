/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/24/2020 1:41:08 PM
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
    abstract public class esItemTariffRequestProcessItemCompCollection : esEntityCollectionWAuditLog
    {
        public esItemTariffRequestProcessItemCompCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemTariffRequestProcessItemCompCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemTariffRequestProcessItemCompQuery query)
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
            this.InitQuery(query as esItemTariffRequestProcessItemCompQuery);
        }
        #endregion

        virtual public ItemTariffRequestProcessItemComp DetachEntity(ItemTariffRequestProcessItemComp entity)
        {
            return base.DetachEntity(entity) as ItemTariffRequestProcessItemComp;
        }

        virtual public ItemTariffRequestProcessItemComp AttachEntity(ItemTariffRequestProcessItemComp entity)
        {
            return base.AttachEntity(entity) as ItemTariffRequestProcessItemComp;
        }

        virtual public void Combine(ItemTariffRequestProcessItemCompCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemTariffRequestProcessItemComp this[int index]
        {
            get
            {
                return base[index] as ItemTariffRequestProcessItemComp;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemTariffRequestProcessItemComp);
        }
    }

    [Serializable]
    abstract public class esItemTariffRequestProcessItemComp : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemTariffRequestProcessItemCompQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemTariffRequestProcessItemComp()
        {
        }

        public esItemTariffRequestProcessItemComp(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String tariffRequestNo, String tariffComponentID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, tariffComponentID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tariffRequestNo, String tariffComponentID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tariffRequestNo, tariffComponentID);
            else
                return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, tariffComponentID);
        }

        private bool LoadByPrimaryKeyDynamic(String tariffRequestNo, String tariffComponentID)
        {
            esItemTariffRequestProcessItemCompQuery query = this.GetDynamicQuery();
            query.Where(query.TariffRequestNo == tariffRequestNo, query.TariffComponentID == tariffComponentID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String tariffRequestNo, String tariffComponentID)
        {
            esParameters parms = new esParameters();
            parms.Add("TariffRequestNo", tariffRequestNo);
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
                        case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;
                        case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
                        case "AmountValue": this.str.AmountValue = (string)value; break;
                        case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AmountValue":

                            if (value == null || value is System.Decimal)
                                this.AmountValue = (System.Decimal?)value;
                            break;
                        case "IsValueInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsValueInPercent = (System.Boolean?)value;
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
        /// Maps to ItemTariffRequestProcessItemComp.TariffRequestNo
        /// </summary>
        virtual public System.String TariffRequestNo
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffRequestNo);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffRequestNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequestProcessItemComp.TariffComponentID
        /// </summary>
        virtual public System.String TariffComponentID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffComponentID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffComponentID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequestProcessItemComp.AmountValue
        /// </summary>
        virtual public System.Decimal? AmountValue
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffRequestProcessItemCompMetadata.ColumnNames.AmountValue);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffRequestProcessItemCompMetadata.ColumnNames.AmountValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequestProcessItemComp.IsValueInPercent
        /// </summary>
        virtual public System.Boolean? IsValueInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffRequestProcessItemCompMetadata.ColumnNames.IsValueInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffRequestProcessItemCompMetadata.ColumnNames.IsValueInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequestProcessItemComp.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffRequestProcessItemComp.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemTariffRequestProcessItemComp entity)
            {
                this.entity = entity;
            }
            public System.String TariffRequestNo
            {
                get
                {
                    System.String data = entity.TariffRequestNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffRequestNo = null;
                    else entity.TariffRequestNo = Convert.ToString(value);
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
            public System.String AmountValue
            {
                get
                {
                    System.Decimal? data = entity.AmountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AmountValue = null;
                    else entity.AmountValue = Convert.ToDecimal(value);
                }
            }
            public System.String IsValueInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsValueInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsValueInPercent = null;
                    else entity.IsValueInPercent = Convert.ToBoolean(value);
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
            private esItemTariffRequestProcessItemComp entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemTariffRequestProcessItemCompQuery query)
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
                throw new Exception("esItemTariffRequestProcessItemComp can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemTariffRequestProcessItemComp : esItemTariffRequestProcessItemComp
    {
    }

    [Serializable]
    abstract public class esItemTariffRequestProcessItemCompQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequestProcessItemCompMetadata.Meta();
            }
        }

        public esQueryItem TariffRequestNo
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
            }
        }

        public esQueryItem TariffComponentID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
            }
        }

        public esQueryItem AmountValue
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem IsValueInPercent
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemTariffRequestProcessItemCompCollection")]
    public partial class ItemTariffRequestProcessItemCompCollection : esItemTariffRequestProcessItemCompCollection, IEnumerable<ItemTariffRequestProcessItemComp>
    {
        public ItemTariffRequestProcessItemCompCollection()
        {

        }

        public static implicit operator List<ItemTariffRequestProcessItemComp>(ItemTariffRequestProcessItemCompCollection coll)
        {
            List<ItemTariffRequestProcessItemComp> list = new List<ItemTariffRequestProcessItemComp>();

            foreach (ItemTariffRequestProcessItemComp emp in coll)
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
                return ItemTariffRequestProcessItemCompMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequestProcessItemCompQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemTariffRequestProcessItemComp(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemTariffRequestProcessItemComp();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffRequestProcessItemCompQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequestProcessItemCompQuery();
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
        public bool Load(ItemTariffRequestProcessItemCompQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemTariffRequestProcessItemComp AddNew()
        {
            ItemTariffRequestProcessItemComp entity = base.AddNewEntity() as ItemTariffRequestProcessItemComp;

            return entity;
        }
        public ItemTariffRequestProcessItemComp FindByPrimaryKey(String tariffRequestNo, String tariffComponentID)
        {
            return base.FindByPrimaryKey(tariffRequestNo, tariffComponentID) as ItemTariffRequestProcessItemComp;
        }

        #region IEnumerable< ItemTariffRequestProcessItemComp> Members

        IEnumerator<ItemTariffRequestProcessItemComp> IEnumerable<ItemTariffRequestProcessItemComp>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemTariffRequestProcessItemComp;
            }
        }

        #endregion

        private ItemTariffRequestProcessItemCompQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemTariffRequestProcessItemComp' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemTariffRequestProcessItemComp ({TariffRequestNo, TariffComponentID})")]
    [Serializable]
    public partial class ItemTariffRequestProcessItemComp : esItemTariffRequestProcessItemComp
    {
        public ItemTariffRequestProcessItemComp()
        {
        }

        public ItemTariffRequestProcessItemComp(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffRequestProcessItemCompMetadata.Meta();
            }
        }

        override protected esItemTariffRequestProcessItemCompQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffRequestProcessItemCompQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffRequestProcessItemCompQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffRequestProcessItemCompQuery();
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
        public bool Load(ItemTariffRequestProcessItemCompQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemTariffRequestProcessItemCompQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemTariffRequestProcessItemCompQuery : esItemTariffRequestProcessItemCompQuery
    {
        public ItemTariffRequestProcessItemCompQuery()
        {

        }

        public ItemTariffRequestProcessItemCompQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemTariffRequestProcessItemCompQuery";
        }
    }

    [Serializable]
    public partial class ItemTariffRequestProcessItemCompMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemTariffRequestProcessItemCompMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.TariffRequestNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.TariffComponentID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.TariffComponentID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.AmountValue, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.AmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.IsValueInPercent, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.IsValueInPercent;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffRequestProcessItemCompMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffRequestProcessItemCompMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemTariffRequestProcessItemCompMetadata Meta()
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
            public const string TariffRequestNo = "TariffRequestNo";
            public const string TariffComponentID = "TariffComponentID";
            public const string AmountValue = "AmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TariffRequestNo = "TariffRequestNo";
            public const string TariffComponentID = "TariffComponentID";
            public const string AmountValue = "AmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
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
            lock (typeof(ItemTariffRequestProcessItemCompMetadata))
            {
                if (ItemTariffRequestProcessItemCompMetadata.mapDelegates == null)
                {
                    ItemTariffRequestProcessItemCompMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemTariffRequestProcessItemCompMetadata.meta == null)
                {
                    ItemTariffRequestProcessItemCompMetadata.meta = new ItemTariffRequestProcessItemCompMetadata();
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

                meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AmountValue", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemTariffRequestProcessItemComp";
                meta.Destination = "ItemTariffRequestProcessItemComp";
                meta.spInsert = "proc_ItemTariffRequestProcessItemCompInsert";
                meta.spUpdate = "proc_ItemTariffRequestProcessItemCompUpdate";
                meta.spDelete = "proc_ItemTariffRequestProcessItemCompDelete";
                meta.spLoadAll = "proc_ItemTariffRequestProcessItemCompLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemTariffRequestProcessItemCompLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemTariffRequestProcessItemCompMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
