/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/14/2017 11:41:49 AM
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
    abstract public class esLaundryReceivedItemCollection : esEntityCollectionWAuditLog
    {
        public esLaundryReceivedItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "LaundryReceivedItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esLaundryReceivedItemQuery query)
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
            this.InitQuery(query as esLaundryReceivedItemQuery);
        }
        #endregion

        virtual public LaundryReceivedItem DetachEntity(LaundryReceivedItem entity)
        {
            return base.DetachEntity(entity) as LaundryReceivedItem;
        }

        virtual public LaundryReceivedItem AttachEntity(LaundryReceivedItem entity)
        {
            return base.AttachEntity(entity) as LaundryReceivedItem;
        }

        virtual public void Combine(LaundryReceivedItemCollection collection)
        {
            base.Combine(collection);
        }

        new public LaundryReceivedItem this[int index]
        {
            get
            {
                return base[index] as LaundryReceivedItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(LaundryReceivedItem);
        }
    }

    [Serializable]
    abstract public class esLaundryReceivedItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esLaundryReceivedItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esLaundryReceivedItem()
        {
        }

        public esLaundryReceivedItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String receivedNo, String receivedSeqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String receivedNo, String receivedSeqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String receivedNo, String receivedSeqNo)
        {
            esLaundryReceivedItemQuery query = this.GetDynamicQuery();
            query.Where(query.ReceivedNo == receivedNo, query.ReceivedSeqNo == receivedSeqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String receivedNo, String receivedSeqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ReceivedNo", receivedNo);
            parms.Add("ReceivedSeqNo", receivedSeqNo);
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
                        case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
                        case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
        /// Maps to LaundryReceivedItem.ReceivedNo
        /// </summary>
        virtual public System.String ReceivedNo
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.ReceivedNo);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.ReceivedNo, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.ReceivedSeqNo
        /// </summary>
        virtual public System.String ReceivedSeqNo
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(LaundryReceivedItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(LaundryReceivedItemMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(LaundryReceivedItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(LaundryReceivedItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to LaundryReceivedItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(LaundryReceivedItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(LaundryReceivedItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esLaundryReceivedItem entity)
            {
                this.entity = entity;
            }
            public System.String ReceivedNo
            {
                get
                {
                    System.String data = entity.ReceivedNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceivedNo = null;
                    else entity.ReceivedNo = Convert.ToString(value);
                }
            }
            public System.String ReceivedSeqNo
            {
                get
                {
                    System.String data = entity.ReceivedSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceivedSeqNo = null;
                    else entity.ReceivedSeqNo = Convert.ToString(value);
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
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
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
            private esLaundryReceivedItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esLaundryReceivedItemQuery query)
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
                throw new Exception("esLaundryReceivedItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class LaundryReceivedItem : esLaundryReceivedItem
    {
    }

    [Serializable]
    abstract public class esLaundryReceivedItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return LaundryReceivedItemMetadata.Meta();
            }
        }

        public esQueryItem ReceivedNo
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
            }
        }

        public esQueryItem ReceivedSeqNo
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, LaundryReceivedItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("LaundryReceivedItemCollection")]
    public partial class LaundryReceivedItemCollection : esLaundryReceivedItemCollection, IEnumerable<LaundryReceivedItem>
    {
        public LaundryReceivedItemCollection()
        {

        }

        public static implicit operator List<LaundryReceivedItem>(LaundryReceivedItemCollection coll)
        {
            List<LaundryReceivedItem> list = new List<LaundryReceivedItem>();

            foreach (LaundryReceivedItem emp in coll)
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
                return LaundryReceivedItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LaundryReceivedItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new LaundryReceivedItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new LaundryReceivedItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public LaundryReceivedItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LaundryReceivedItemQuery();
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
        public bool Load(LaundryReceivedItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public LaundryReceivedItem AddNew()
        {
            LaundryReceivedItem entity = base.AddNewEntity() as LaundryReceivedItem;

            return entity;
        }
        public LaundryReceivedItem FindByPrimaryKey(String receivedNo, String receivedSeqNo)
        {
            return base.FindByPrimaryKey(receivedNo, receivedSeqNo) as LaundryReceivedItem;
        }

        #region IEnumerable< LaundryReceivedItem> Members

        IEnumerator<LaundryReceivedItem> IEnumerable<LaundryReceivedItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as LaundryReceivedItem;
            }
        }

        #endregion

        private LaundryReceivedItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'LaundryReceivedItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("LaundryReceivedItem ({ReceivedNo, ReceivedSeqNo})")]
    [Serializable]
    public partial class LaundryReceivedItem : esLaundryReceivedItem
    {
        public LaundryReceivedItem()
        {
        }

        public LaundryReceivedItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return LaundryReceivedItemMetadata.Meta();
            }
        }

        override protected esLaundryReceivedItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LaundryReceivedItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public LaundryReceivedItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LaundryReceivedItemQuery();
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
        public bool Load(LaundryReceivedItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private LaundryReceivedItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class LaundryReceivedItemQuery : esLaundryReceivedItemQuery
    {
        public LaundryReceivedItemQuery()
        {

        }

        public LaundryReceivedItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "LaundryReceivedItemQuery";
        }
    }

    [Serializable]
    public partial class LaundryReceivedItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected LaundryReceivedItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.ReceivedNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.ReceivedNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.ReceivedSeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LaundryReceivedItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = LaundryReceivedItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public LaundryReceivedItemMetadata Meta()
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
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string Qty = "Qty";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string Qty = "Qty";
            public const string Notes = "Notes";
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
            lock (typeof(LaundryReceivedItemMetadata))
            {
                if (LaundryReceivedItemMetadata.mapDelegates == null)
                {
                    LaundryReceivedItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (LaundryReceivedItemMetadata.meta == null)
                {
                    LaundryReceivedItemMetadata.meta = new LaundryReceivedItemMetadata();
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

                meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "LaundryReceivedItem";
                meta.Destination = "LaundryReceivedItem";
                meta.spInsert = "proc_LaundryReceivedItemInsert";
                meta.spUpdate = "proc_LaundryReceivedItemUpdate";
                meta.spDelete = "proc_LaundryReceivedItemDelete";
                meta.spLoadAll = "proc_LaundryReceivedItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_LaundryReceivedItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private LaundryReceivedItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
