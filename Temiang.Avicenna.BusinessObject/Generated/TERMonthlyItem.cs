/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/5/2024 12:50:53 PM
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
    abstract public class esTERMonthlyItemCollection : esEntityCollectionWAuditLog
    {
        public esTERMonthlyItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TERMonthlyItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esTERMonthlyItemQuery query)
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
            this.InitQuery(query as esTERMonthlyItemQuery);
        }
        #endregion

        virtual public TERMonthlyItem DetachEntity(TERMonthlyItem entity)
        {
            return base.DetachEntity(entity) as TERMonthlyItem;
        }

        virtual public TERMonthlyItem AttachEntity(TERMonthlyItem entity)
        {
            return base.AttachEntity(entity) as TERMonthlyItem;
        }

        virtual public void Combine(TERMonthlyItemCollection collection)
        {
            base.Combine(collection);
        }

        new public TERMonthlyItem this[int index]
        {
            get
            {
                return base[index] as TERMonthlyItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TERMonthlyItem);
        }
    }

    [Serializable]
    abstract public class esTERMonthlyItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTERMonthlyItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esTERMonthlyItem()
        {
        }

        public esTERMonthlyItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 tERMonthlyItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tERMonthlyItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(tERMonthlyItemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 tERMonthlyItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tERMonthlyItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(tERMonthlyItemID);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 tERMonthlyItemID)
        {
            esTERMonthlyItemQuery query = this.GetDynamicQuery();
            query.Where(query.TERMonthlyItemID == tERMonthlyItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 tERMonthlyItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("TERMonthlyItemID", tERMonthlyItemID);
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
                        case "TERMonthlyItemID": this.str.TERMonthlyItemID = (string)value; break;
                        case "TERMonthlyID": this.str.TERMonthlyID = (string)value; break;
                        case "LowerLimit": this.str.LowerLimit = (string)value; break;
                        case "UpperLimit": this.str.UpperLimit = (string)value; break;
                        case "TaxRate": this.str.TaxRate = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TERMonthlyItemID":

                            if (value == null || value is System.Int32)
                                this.TERMonthlyItemID = (System.Int32?)value;
                            break;
                        case "TERMonthlyID":

                            if (value == null || value is System.Int32)
                                this.TERMonthlyID = (System.Int32?)value;
                            break;
                        case "LowerLimit":

                            if (value == null || value is System.Decimal)
                                this.LowerLimit = (System.Decimal?)value;
                            break;
                        case "UpperLimit":

                            if (value == null || value is System.Decimal)
                                this.UpperLimit = (System.Decimal?)value;
                            break;
                        case "TaxRate":

                            if (value == null || value is System.Decimal)
                                this.TaxRate = (System.Decimal?)value;
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
        /// Maps to TERMonthlyItem.TERMonthlyItemID
        /// </summary>
        virtual public System.Int32? TERMonthlyItemID
        {
            get
            {
                return base.GetSystemInt32(TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID);
            }

            set
            {
                base.SetSystemInt32(TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.TERMonthlyID
        /// </summary>
        virtual public System.Int32? TERMonthlyID
        {
            get
            {
                return base.GetSystemInt32(TERMonthlyItemMetadata.ColumnNames.TERMonthlyID);
            }

            set
            {
                base.SetSystemInt32(TERMonthlyItemMetadata.ColumnNames.TERMonthlyID, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.LowerLimit
        /// </summary>
        virtual public System.Decimal? LowerLimit
        {
            get
            {
                return base.GetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.LowerLimit);
            }

            set
            {
                base.SetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.LowerLimit, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.UpperLimit
        /// </summary>
        virtual public System.Decimal? UpperLimit
        {
            get
            {
                return base.GetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.UpperLimit);
            }

            set
            {
                base.SetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.UpperLimit, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.TaxRate
        /// </summary>
        virtual public System.Decimal? TaxRate
        {
            get
            {
                return base.GetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.TaxRate);
            }

            set
            {
                base.SetSystemDecimal(TERMonthlyItemMetadata.ColumnNames.TaxRate, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TERMonthlyItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TERMonthlyItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TERMonthlyItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TERMonthlyItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TERMonthlyItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTERMonthlyItem entity)
            {
                this.entity = entity;
            }
            public System.String TERMonthlyItemID
            {
                get
                {
                    System.Int32? data = entity.TERMonthlyItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TERMonthlyItemID = null;
                    else entity.TERMonthlyItemID = Convert.ToInt32(value);
                }
            }
            public System.String TERMonthlyID
            {
                get
                {
                    System.Int32? data = entity.TERMonthlyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TERMonthlyID = null;
                    else entity.TERMonthlyID = Convert.ToInt32(value);
                }
            }
            public System.String LowerLimit
            {
                get
                {
                    System.Decimal? data = entity.LowerLimit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LowerLimit = null;
                    else entity.LowerLimit = Convert.ToDecimal(value);
                }
            }
            public System.String UpperLimit
            {
                get
                {
                    System.Decimal? data = entity.UpperLimit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpperLimit = null;
                    else entity.UpperLimit = Convert.ToDecimal(value);
                }
            }
            public System.String TaxRate
            {
                get
                {
                    System.Decimal? data = entity.TaxRate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxRate = null;
                    else entity.TaxRate = Convert.ToDecimal(value);
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
            private esTERMonthlyItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTERMonthlyItemQuery query)
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
                throw new Exception("esTERMonthlyItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TERMonthlyItem : esTERMonthlyItem
    {
    }

    [Serializable]
    abstract public class esTERMonthlyItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TERMonthlyItemMetadata.Meta();
            }
        }

        public esQueryItem TERMonthlyItemID
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID, esSystemType.Int32);
            }
        }

        public esQueryItem TERMonthlyID
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.TERMonthlyID, esSystemType.Int32);
            }
        }

        public esQueryItem LowerLimit
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.LowerLimit, esSystemType.Decimal);
            }
        }

        public esQueryItem UpperLimit
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.UpperLimit, esSystemType.Decimal);
            }
        }

        public esQueryItem TaxRate
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.TaxRate, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TERMonthlyItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TERMonthlyItemCollection")]
    public partial class TERMonthlyItemCollection : esTERMonthlyItemCollection, IEnumerable<TERMonthlyItem>
    {
        public TERMonthlyItemCollection()
        {

        }

        public static implicit operator List<TERMonthlyItem>(TERMonthlyItemCollection coll)
        {
            List<TERMonthlyItem> list = new List<TERMonthlyItem>();

            foreach (TERMonthlyItem emp in coll)
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
                return TERMonthlyItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TERMonthlyItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TERMonthlyItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TERMonthlyItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TERMonthlyItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TERMonthlyItemQuery();
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
        public bool Load(TERMonthlyItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TERMonthlyItem AddNew()
        {
            TERMonthlyItem entity = base.AddNewEntity() as TERMonthlyItem;

            return entity;
        }
        public TERMonthlyItem FindByPrimaryKey(Int32 tERMonthlyItemID)
        {
            return base.FindByPrimaryKey(tERMonthlyItemID) as TERMonthlyItem;
        }

        #region IEnumerable< TERMonthlyItem> Members

        IEnumerator<TERMonthlyItem> IEnumerable<TERMonthlyItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TERMonthlyItem;
            }
        }

        #endregion

        private TERMonthlyItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TERMonthlyItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TERMonthlyItem ({TERMonthlyItemID})")]
    [Serializable]
    public partial class TERMonthlyItem : esTERMonthlyItem
    {
        public TERMonthlyItem()
        {
        }

        public TERMonthlyItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TERMonthlyItemMetadata.Meta();
            }
        }

        override protected esTERMonthlyItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TERMonthlyItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TERMonthlyItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TERMonthlyItemQuery();
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
        public bool Load(TERMonthlyItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TERMonthlyItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TERMonthlyItemQuery : esTERMonthlyItemQuery
    {
        public TERMonthlyItemQuery()
        {

        }

        public TERMonthlyItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TERMonthlyItemQuery";
        }
    }

    [Serializable]
    public partial class TERMonthlyItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TERMonthlyItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.TERMonthlyItemID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.TERMonthlyItemID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.TERMonthlyID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.TERMonthlyID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.LowerLimit, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.LowerLimit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.UpperLimit, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.UpperLimit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.TaxRate, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.TaxRate;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TERMonthlyItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = TERMonthlyItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TERMonthlyItemMetadata Meta()
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
            public const string TERMonthlyItemID = "TERMonthlyItemID";
            public const string TERMonthlyID = "TERMonthlyID";
            public const string LowerLimit = "LowerLimit";
            public const string UpperLimit = "UpperLimit";
            public const string TaxRate = "TaxRate";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TERMonthlyItemID = "TERMonthlyItemID";
            public const string TERMonthlyID = "TERMonthlyID";
            public const string LowerLimit = "LowerLimit";
            public const string UpperLimit = "UpperLimit";
            public const string TaxRate = "TaxRate";
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
            lock (typeof(TERMonthlyItemMetadata))
            {
                if (TERMonthlyItemMetadata.mapDelegates == null)
                {
                    TERMonthlyItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TERMonthlyItemMetadata.meta == null)
                {
                    TERMonthlyItemMetadata.meta = new TERMonthlyItemMetadata();
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

                meta.AddTypeMap("TERMonthlyItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TERMonthlyID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LowerLimit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("UpperLimit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TaxRate", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TERMonthlyItem";
                meta.Destination = "TERMonthlyItem";
                meta.spInsert = "proc_TERMonthlyItemInsert";
                meta.spUpdate = "proc_TERMonthlyItemUpdate";
                meta.spDelete = "proc_TERMonthlyItemDelete";
                meta.spLoadAll = "proc_TERMonthlyItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_TERMonthlyItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TERMonthlyItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
