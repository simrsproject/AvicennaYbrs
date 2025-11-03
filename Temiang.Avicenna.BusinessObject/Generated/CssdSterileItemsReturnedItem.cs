/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2017 1:30:49 PM
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
    abstract public class esCssdSterileItemsReturnedItemCollection : esEntityCollectionWAuditLog
    {
        public esCssdSterileItemsReturnedItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CssdSterileItemsReturnedItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esCssdSterileItemsReturnedItemQuery query)
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
            this.InitQuery(query as esCssdSterileItemsReturnedItemQuery);
        }
        #endregion

        virtual public CssdSterileItemsReturnedItem DetachEntity(CssdSterileItemsReturnedItem entity)
        {
            return base.DetachEntity(entity) as CssdSterileItemsReturnedItem;
        }

        virtual public CssdSterileItemsReturnedItem AttachEntity(CssdSterileItemsReturnedItem entity)
        {
            return base.AttachEntity(entity) as CssdSterileItemsReturnedItem;
        }

        virtual public void Combine(CssdSterileItemsReturnedItemCollection collection)
        {
            base.Combine(collection);
        }

        new public CssdSterileItemsReturnedItem this[int index]
        {
            get
            {
                return base[index] as CssdSterileItemsReturnedItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CssdSterileItemsReturnedItem);
        }
    }

    [Serializable]
    abstract public class esCssdSterileItemsReturnedItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCssdSterileItemsReturnedItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esCssdSterileItemsReturnedItem()
        {
        }

        public esCssdSterileItemsReturnedItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String returnNo, String returnSeqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(returnNo, returnSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(returnNo, returnSeqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String returnNo, String returnSeqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(returnNo, returnSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(returnNo, returnSeqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String returnNo, String returnSeqNo)
        {
            esCssdSterileItemsReturnedItemQuery query = this.GetDynamicQuery();
            query.Where(query.ReturnNo == returnNo, query.ReturnSeqNo == returnSeqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String returnNo, String returnSeqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ReturnNo", returnNo);
            parms.Add("ReturnSeqNo", returnSeqNo);
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
                        case "ReturnNo": this.str.ReturnNo = (string)value; break;
                        case "ReturnSeqNo": this.str.ReturnSeqNo = (string)value; break;
                        case "ProcessNo": this.str.ProcessNo = (string)value; break;
                        case "ProcessSeqNo": this.str.ProcessSeqNo = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
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
        /// Maps to CssdSterileItemsReturnedItem.ReturnNo
        /// </summary>
        virtual public System.String ReturnNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.ReturnSeqNo
        /// </summary>
        virtual public System.String ReturnSeqNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnSeqNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.ProcessNo
        /// </summary>
        virtual public System.String ProcessNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.ProcessSeqNo
        /// </summary>
        virtual public System.String ProcessSeqNo
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessSeqNo);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(CssdSterileItemsReturnedItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(CssdSterileItemsReturnedItemMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CssdSterileItemsReturnedItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCssdSterileItemsReturnedItem entity)
            {
                this.entity = entity;
            }
            public System.String ReturnNo
            {
                get
                {
                    System.String data = entity.ReturnNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReturnNo = null;
                    else entity.ReturnNo = Convert.ToString(value);
                }
            }
            public System.String ReturnSeqNo
            {
                get
                {
                    System.String data = entity.ReturnSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReturnSeqNo = null;
                    else entity.ReturnSeqNo = Convert.ToString(value);
                }
            }
            public System.String ProcessNo
            {
                get
                {
                    System.String data = entity.ProcessNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcessNo = null;
                    else entity.ProcessNo = Convert.ToString(value);
                }
            }
            public System.String ProcessSeqNo
            {
                get
                {
                    System.String data = entity.ProcessSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcessSeqNo = null;
                    else entity.ProcessSeqNo = Convert.ToString(value);
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
            private esCssdSterileItemsReturnedItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCssdSterileItemsReturnedItemQuery query)
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
                throw new Exception("esCssdSterileItemsReturnedItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CssdSterileItemsReturnedItem : esCssdSterileItemsReturnedItem
    {
    }

    [Serializable]
    abstract public class esCssdSterileItemsReturnedItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CssdSterileItemsReturnedItemMetadata.Meta();
            }
        }

        public esQueryItem ReturnNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnNo, esSystemType.String);
            }
        }

        public esQueryItem ReturnSeqNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnSeqNo, esSystemType.String);
            }
        }

        public esQueryItem ProcessNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessNo, esSystemType.String);
            }
        }

        public esQueryItem ProcessSeqNo
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessSeqNo, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CssdSterileItemsReturnedItemCollection")]
    public partial class CssdSterileItemsReturnedItemCollection : esCssdSterileItemsReturnedItemCollection, IEnumerable<CssdSterileItemsReturnedItem>
    {
        public CssdSterileItemsReturnedItemCollection()
        {

        }

        public static implicit operator List<CssdSterileItemsReturnedItem>(CssdSterileItemsReturnedItemCollection coll)
        {
            List<CssdSterileItemsReturnedItem> list = new List<CssdSterileItemsReturnedItem>();

            foreach (CssdSterileItemsReturnedItem emp in coll)
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
                return CssdSterileItemsReturnedItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdSterileItemsReturnedItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CssdSterileItemsReturnedItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CssdSterileItemsReturnedItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CssdSterileItemsReturnedItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdSterileItemsReturnedItemQuery();
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
        public bool Load(CssdSterileItemsReturnedItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CssdSterileItemsReturnedItem AddNew()
        {
            CssdSterileItemsReturnedItem entity = base.AddNewEntity() as CssdSterileItemsReturnedItem;

            return entity;
        }
        public CssdSterileItemsReturnedItem FindByPrimaryKey(String returnNo, String returnSeqNo)
        {
            return base.FindByPrimaryKey(returnNo, returnSeqNo) as CssdSterileItemsReturnedItem;
        }

        #region IEnumerable< CssdSterileItemsReturnedItem> Members

        IEnumerator<CssdSterileItemsReturnedItem> IEnumerable<CssdSterileItemsReturnedItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CssdSterileItemsReturnedItem;
            }
        }

        #endregion

        private CssdSterileItemsReturnedItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CssdSterileItemsReturnedItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CssdSterileItemsReturnedItem ({ReturnNo, ReturnSeqNo})")]
    [Serializable]
    public partial class CssdSterileItemsReturnedItem : esCssdSterileItemsReturnedItem
    {
        public CssdSterileItemsReturnedItem()
        {
        }

        public CssdSterileItemsReturnedItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CssdSterileItemsReturnedItemMetadata.Meta();
            }
        }

        override protected esCssdSterileItemsReturnedItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdSterileItemsReturnedItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CssdSterileItemsReturnedItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdSterileItemsReturnedItemQuery();
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
        public bool Load(CssdSterileItemsReturnedItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CssdSterileItemsReturnedItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CssdSterileItemsReturnedItemQuery : esCssdSterileItemsReturnedItemQuery
    {
        public CssdSterileItemsReturnedItemQuery()
        {

        }

        public CssdSterileItemsReturnedItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CssdSterileItemsReturnedItemQuery";
        }
    }

    [Serializable]
    public partial class CssdSterileItemsReturnedItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CssdSterileItemsReturnedItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.ReturnNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.ReturnSeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.ReturnSeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.ProcessNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.ProcessSeqNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.ProcessSeqNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CssdSterileItemsReturnedItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdSterileItemsReturnedItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CssdSterileItemsReturnedItemMetadata Meta()
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
            public const string ReturnNo = "ReturnNo";
            public const string ReturnSeqNo = "ReturnSeqNo";
            public const string ProcessNo = "ProcessNo";
            public const string ProcessSeqNo = "ProcessSeqNo";
            public const string Qty = "Qty";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ReturnNo = "ReturnNo";
            public const string ReturnSeqNo = "ReturnSeqNo";
            public const string ProcessNo = "ProcessNo";
            public const string ProcessSeqNo = "ProcessSeqNo";
            public const string Qty = "Qty";
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
            lock (typeof(CssdSterileItemsReturnedItemMetadata))
            {
                if (CssdSterileItemsReturnedItemMetadata.mapDelegates == null)
                {
                    CssdSterileItemsReturnedItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CssdSterileItemsReturnedItemMetadata.meta == null)
                {
                    CssdSterileItemsReturnedItemMetadata.meta = new CssdSterileItemsReturnedItemMetadata();
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

                meta.AddTypeMap("ReturnNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReturnSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcessSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CssdSterileItemsReturnedItem";
                meta.Destination = "CssdSterileItemsReturnedItem";
                meta.spInsert = "proc_CssdSterileItemsReturnedItemInsert";
                meta.spUpdate = "proc_CssdSterileItemsReturnedItemUpdate";
                meta.spDelete = "proc_CssdSterileItemsReturnedItemDelete";
                meta.spLoadAll = "proc_CssdSterileItemsReturnedItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_CssdSterileItemsReturnedItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CssdSterileItemsReturnedItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
