/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/14/2017 11:40:40 AM
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
    abstract public class esLaunderedProcessItemCollection : esEntityCollectionWAuditLog
    {
        public esLaunderedProcessItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "LaunderedProcessItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esLaunderedProcessItemQuery query)
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
            this.InitQuery(query as esLaunderedProcessItemQuery);
        }
        #endregion

        virtual public LaunderedProcessItem DetachEntity(LaunderedProcessItem entity)
        {
            return base.DetachEntity(entity) as LaunderedProcessItem;
        }

        virtual public LaunderedProcessItem AttachEntity(LaunderedProcessItem entity)
        {
            return base.AttachEntity(entity) as LaunderedProcessItem;
        }

        virtual public void Combine(LaunderedProcessItemCollection collection)
        {
            base.Combine(collection);
        }

        new public LaunderedProcessItem this[int index]
        {
            get
            {
                return base[index] as LaunderedProcessItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(LaunderedProcessItem);
        }
    }

    [Serializable]
    abstract public class esLaunderedProcessItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esLaunderedProcessItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esLaunderedProcessItem()
        {
        }

        public esLaunderedProcessItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String processNo, String processSeqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(processNo, processSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(processNo, processSeqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String processNo, String processSeqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(processNo, processSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(processNo, processSeqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String processNo, String processSeqNo)
        {
            esLaunderedProcessItemQuery query = this.GetDynamicQuery();
            query.Where(query.ProcessNo == processNo, query.ProcessSeqNo == processSeqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String processNo, String processSeqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("ProcessNo", processNo);
            parms.Add("ProcessSeqNo", processSeqNo);
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
                        case "ProcessNo": this.str.ProcessNo = (string)value; break;
                        case "ProcessSeqNo": this.str.ProcessSeqNo = (string)value; break;
                        case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
                        case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
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
                        case "ConversionFactor":

                            if (value == null || value is System.Decimal)
                                this.ConversionFactor = (System.Decimal?)value;
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
        /// Maps to LaunderedProcessItem.ProcessNo
        /// </summary>
        virtual public System.String ProcessNo
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.ProcessNo);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.ProcessNo, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.ProcessSeqNo
        /// </summary>
        virtual public System.String ProcessSeqNo
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.ReceivedNo
        /// </summary>
        virtual public System.String ReceivedNo
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.ReceivedNo);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.ReceivedNo, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.ReceivedSeqNo
        /// </summary>
        virtual public System.String ReceivedSeqNo
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.ReceivedSeqNo);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.ReceivedSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(LaunderedProcessItemMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(LaunderedProcessItemMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.ConversionFactor
        /// </summary>
        virtual public System.Decimal? ConversionFactor
        {
            get
            {
                return base.GetSystemDecimal(LaunderedProcessItemMetadata.ColumnNames.ConversionFactor);
            }

            set
            {
                base.SetSystemDecimal(LaunderedProcessItemMetadata.ColumnNames.ConversionFactor, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(LaunderedProcessItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(LaunderedProcessItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to LaunderedProcessItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(LaunderedProcessItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(LaunderedProcessItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esLaunderedProcessItem entity)
            {
                this.entity = entity;
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
            public System.String ConversionFactor
            {
                get
                {
                    System.Decimal? data = entity.ConversionFactor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConversionFactor = null;
                    else entity.ConversionFactor = Convert.ToDecimal(value);
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
            private esLaunderedProcessItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esLaunderedProcessItemQuery query)
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
                throw new Exception("esLaunderedProcessItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class LaunderedProcessItem : esLaunderedProcessItem
    {
    }

    [Serializable]
    abstract public class esLaunderedProcessItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return LaunderedProcessItemMetadata.Meta();
            }
        }

        public esQueryItem ProcessNo
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.ProcessNo, esSystemType.String);
            }
        }

        public esQueryItem ProcessSeqNo
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo, esSystemType.String);
            }
        }

        public esQueryItem ReceivedNo
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
            }
        }

        public esQueryItem ReceivedSeqNo
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem ConversionFactor
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, LaunderedProcessItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("LaunderedProcessItemCollection")]
    public partial class LaunderedProcessItemCollection : esLaunderedProcessItemCollection, IEnumerable<LaunderedProcessItem>
    {
        public LaunderedProcessItemCollection()
        {

        }

        public static implicit operator List<LaunderedProcessItem>(LaunderedProcessItemCollection coll)
        {
            List<LaunderedProcessItem> list = new List<LaunderedProcessItem>();

            foreach (LaunderedProcessItem emp in coll)
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
                return LaunderedProcessItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LaunderedProcessItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new LaunderedProcessItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new LaunderedProcessItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public LaunderedProcessItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LaunderedProcessItemQuery();
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
        public bool Load(LaunderedProcessItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public LaunderedProcessItem AddNew()
        {
            LaunderedProcessItem entity = base.AddNewEntity() as LaunderedProcessItem;

            return entity;
        }
        public LaunderedProcessItem FindByPrimaryKey(String processNo, String processSeqNo)
        {
            return base.FindByPrimaryKey(processNo, processSeqNo) as LaunderedProcessItem;
        }

        #region IEnumerable< LaunderedProcessItem> Members

        IEnumerator<LaunderedProcessItem> IEnumerable<LaunderedProcessItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as LaunderedProcessItem;
            }
        }

        #endregion

        private LaunderedProcessItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'LaunderedProcessItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("LaunderedProcessItem ({ProcessNo, ProcessSeqNo})")]
    [Serializable]
    public partial class LaunderedProcessItem : esLaunderedProcessItem
    {
        public LaunderedProcessItem()
        {
        }

        public LaunderedProcessItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return LaunderedProcessItemMetadata.Meta();
            }
        }

        override protected esLaunderedProcessItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new LaunderedProcessItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public LaunderedProcessItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new LaunderedProcessItemQuery();
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
        public bool Load(LaunderedProcessItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private LaunderedProcessItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class LaunderedProcessItemQuery : esLaunderedProcessItemQuery
    {
        public LaunderedProcessItemQuery()
        {

        }

        public LaunderedProcessItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "LaunderedProcessItemQuery";
        }
    }

    [Serializable]
    public partial class LaunderedProcessItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected LaunderedProcessItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.ProcessNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.ProcessSeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.ReceivedNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.ReceivedSeqNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.ConversionFactor, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.ConversionFactor;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(LaunderedProcessItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = LaunderedProcessItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public LaunderedProcessItemMetadata Meta()
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
            public const string ProcessNo = "ProcessNo";
            public const string ProcessSeqNo = "ProcessSeqNo";
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string ConversionFactor = "ConversionFactor";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProcessNo = "ProcessNo";
            public const string ProcessSeqNo = "ProcessSeqNo";
            public const string ReceivedNo = "ReceivedNo";
            public const string ReceivedSeqNo = "ReceivedSeqNo";
            public const string Qty = "Qty";
            public const string SRItemUnit = "SRItemUnit";
            public const string ConversionFactor = "ConversionFactor";
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
            lock (typeof(LaunderedProcessItemMetadata))
            {
                if (LaunderedProcessItemMetadata.mapDelegates == null)
                {
                    LaunderedProcessItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (LaunderedProcessItemMetadata.meta == null)
                {
                    LaunderedProcessItemMetadata.meta = new LaunderedProcessItemMetadata();
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

                meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcessSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "LaunderedProcessItem";
                meta.Destination = "LaunderedProcessItem";
                meta.spInsert = "proc_LaunderedProcessItemInsert";
                meta.spUpdate = "proc_LaunderedProcessItemUpdate";
                meta.spDelete = "proc_LaunderedProcessItemDelete";
                meta.spLoadAll = "proc_LaunderedProcessItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_LaunderedProcessItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private LaunderedProcessItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
