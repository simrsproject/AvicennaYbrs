/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/21/18 9:16:38 AM
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
    abstract public class esApprovalRangeCollection : esEntityCollectionWAuditLog
    {
        public esApprovalRangeCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ApprovalRangeCollection";
        }

        #region Query Logic
        protected void InitQuery(esApprovalRangeQuery query)
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
            this.InitQuery(query as esApprovalRangeQuery);
        }
        #endregion

        virtual public ApprovalRange DetachEntity(ApprovalRange entity)
        {
            return base.DetachEntity(entity) as ApprovalRange;
        }

        virtual public ApprovalRange AttachEntity(ApprovalRange entity)
        {
            return base.AttachEntity(entity) as ApprovalRange;
        }

        virtual public void Combine(ApprovalRangeCollection collection)
        {
            base.Combine(collection);
        }

        new public ApprovalRange this[int index]
        {
            get
            {
                return base[index] as ApprovalRange;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ApprovalRange);
        }
    }

    [Serializable]
    abstract public class esApprovalRange : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esApprovalRangeQuery GetDynamicQuery()
        {
            return null;
        }

        public esApprovalRange()
        {
        }

        public esApprovalRange(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String approvalRangeID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(approvalRangeID);
            else
                return LoadByPrimaryKeyStoredProcedure(approvalRangeID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String approvalRangeID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(approvalRangeID);
            else
                return LoadByPrimaryKeyStoredProcedure(approvalRangeID);
        }

        private bool LoadByPrimaryKeyDynamic(String approvalRangeID)
        {
            esApprovalRangeQuery query = this.GetDynamicQuery();
            query.Where(query.ApprovalRangeID == approvalRangeID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String approvalRangeID)
        {
            esParameters parms = new esParameters();
            parms.Add("ApprovalRangeID", approvalRangeID);
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
                        case "ApprovalRangeID": this.str.ApprovalRangeID = (string)value; break;
                        case "SRItemType": this.str.SRItemType = (string)value; break;
                        case "AmountFrom": this.str.AmountFrom = (string)value; break;
                        case "ApprovalLevelFinal": this.str.ApprovalLevelFinal = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
                        case "TransactionCode": this.str.TransactionCode = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AmountFrom":

                            if (value == null || value is System.Decimal)
                                this.AmountFrom = (System.Decimal?)value;
                            break;
                        case "ApprovalLevelFinal":

                            if (value == null || value is System.Int32)
                                this.ApprovalLevelFinal = (System.Int32?)value;
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
        /// Maps to ApprovalRange.ApprovalRangeID
        /// </summary>
        virtual public System.String ApprovalRangeID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeMetadata.ColumnNames.ApprovalRangeID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeMetadata.ColumnNames.ApprovalRangeID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.SRItemType
        /// </summary>
        virtual public System.String SRItemType
        {
            get
            {
                return base.GetSystemString(ApprovalRangeMetadata.ColumnNames.SRItemType);
            }

            set
            {
                base.SetSystemString(ApprovalRangeMetadata.ColumnNames.SRItemType, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.AmountFrom
        /// </summary>
        virtual public System.Decimal? AmountFrom
        {
            get
            {
                return base.GetSystemDecimal(ApprovalRangeMetadata.ColumnNames.AmountFrom);
            }

            set
            {
                base.SetSystemDecimal(ApprovalRangeMetadata.ColumnNames.AmountFrom, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.ApprovalLevelFinal
        /// </summary>
        virtual public System.Int32? ApprovalLevelFinal
        {
            get
            {
                return base.GetSystemInt32(ApprovalRangeMetadata.ColumnNames.ApprovalLevelFinal);
            }

            set
            {
                base.SetSystemInt32(ApprovalRangeMetadata.ColumnNames.ApprovalLevelFinal, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ApprovalRangeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ApprovalRangeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.ItemGroupID
        /// </summary>
        virtual public System.String ItemGroupID
        {
            get
            {
                return base.GetSystemString(ApprovalRangeMetadata.ColumnNames.ItemGroupID);
            }

            set
            {
                base.SetSystemString(ApprovalRangeMetadata.ColumnNames.ItemGroupID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalRange.TransactionCode
        /// </summary>
        virtual public System.String TransactionCode
        {
            get
            {
                return base.GetSystemString(ApprovalRangeMetadata.ColumnNames.TransactionCode);
            }

            set
            {
                base.SetSystemString(ApprovalRangeMetadata.ColumnNames.TransactionCode, value);
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
            public esStrings(esApprovalRange entity)
            {
                this.entity = entity;
            }
            public System.String ApprovalRangeID
            {
                get
                {
                    System.String data = entity.ApprovalRangeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalRangeID = null;
                    else entity.ApprovalRangeID = Convert.ToString(value);
                }
            }
            public System.String SRItemType
            {
                get
                {
                    System.String data = entity.SRItemType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemType = null;
                    else entity.SRItemType = Convert.ToString(value);
                }
            }
            public System.String AmountFrom
            {
                get
                {
                    System.Decimal? data = entity.AmountFrom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AmountFrom = null;
                    else entity.AmountFrom = Convert.ToDecimal(value);
                }
            }
            public System.String ApprovalLevelFinal
            {
                get
                {
                    System.Int32? data = entity.ApprovalLevelFinal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalLevelFinal = null;
                    else entity.ApprovalLevelFinal = Convert.ToInt32(value);
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
            public System.String ItemGroupID
            {
                get
                {
                    System.String data = entity.ItemGroupID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemGroupID = null;
                    else entity.ItemGroupID = Convert.ToString(value);
                }
            }
            public System.String TransactionCode
            {
                get
                {
                    System.String data = entity.TransactionCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionCode = null;
                    else entity.TransactionCode = Convert.ToString(value);
                }
            }
            private esApprovalRange entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esApprovalRangeQuery query)
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
                throw new Exception("esApprovalRange can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ApprovalRange : esApprovalRange
    {
    }

    [Serializable]
    abstract public class esApprovalRangeQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ApprovalRangeMetadata.Meta();
            }
        }

        public esQueryItem ApprovalRangeID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.ApprovalRangeID, esSystemType.String);
            }
        }

        public esQueryItem SRItemType
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.SRItemType, esSystemType.String);
            }
        }

        public esQueryItem AmountFrom
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.AmountFrom, esSystemType.Decimal);
            }
        }

        public esQueryItem ApprovalLevelFinal
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.ApprovalLevelFinal, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ItemGroupID
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.ItemGroupID, esSystemType.String);
            }
        }

        public esQueryItem TransactionCode
        {
            get
            {
                return new esQueryItem(this, ApprovalRangeMetadata.ColumnNames.TransactionCode, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ApprovalRangeCollection")]
    public partial class ApprovalRangeCollection : esApprovalRangeCollection, IEnumerable<ApprovalRange>
    {
        public ApprovalRangeCollection()
        {

        }

        public static implicit operator List<ApprovalRange>(ApprovalRangeCollection coll)
        {
            List<ApprovalRange> list = new List<ApprovalRange>();

            foreach (ApprovalRange emp in coll)
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
                return ApprovalRangeMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalRangeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ApprovalRange(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ApprovalRange();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ApprovalRangeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalRangeQuery();
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
        public bool Load(ApprovalRangeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ApprovalRange AddNew()
        {
            ApprovalRange entity = base.AddNewEntity() as ApprovalRange;

            return entity;
        }
        public ApprovalRange FindByPrimaryKey(String approvalRangeID)
        {
            return base.FindByPrimaryKey(approvalRangeID) as ApprovalRange;
        }

        #region IEnumerable< ApprovalRange> Members

        IEnumerator<ApprovalRange> IEnumerable<ApprovalRange>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ApprovalRange;
            }
        }

        #endregion

        private ApprovalRangeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ApprovalRange' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ApprovalRange ({ApprovalRangeID})")]
    [Serializable]
    public partial class ApprovalRange : esApprovalRange
    {
        public ApprovalRange()
        {
        }

        public ApprovalRange(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ApprovalRangeMetadata.Meta();
            }
        }

        override protected esApprovalRangeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalRangeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ApprovalRangeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalRangeQuery();
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
        public bool Load(ApprovalRangeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ApprovalRangeQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ApprovalRangeQuery : esApprovalRangeQuery
    {
        public ApprovalRangeQuery()
        {

        }

        public ApprovalRangeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ApprovalRangeQuery";
        }
    }

    [Serializable]
    public partial class ApprovalRangeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ApprovalRangeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.ApprovalRangeID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.ApprovalRangeID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.SRItemType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.SRItemType;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.AmountFrom, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.AmountFrom;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.ApprovalLevelFinal, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.ApprovalLevelFinal;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.ItemGroupID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.ItemGroupID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalRangeMetadata.ColumnNames.TransactionCode, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalRangeMetadata.PropertyNames.TransactionCode;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ApprovalRangeMetadata Meta()
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
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string SRItemType = "SRItemType";
            public const string AmountFrom = "AmountFrom";
            public const string ApprovalLevelFinal = "ApprovalLevelFinal";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ItemGroupID = "ItemGroupID";
            public const string TransactionCode = "TransactionCode";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string SRItemType = "SRItemType";
            public const string AmountFrom = "AmountFrom";
            public const string ApprovalLevelFinal = "ApprovalLevelFinal";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ItemGroupID = "ItemGroupID";
            public const string TransactionCode = "TransactionCode";
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
            lock (typeof(ApprovalRangeMetadata))
            {
                if (ApprovalRangeMetadata.mapDelegates == null)
                {
                    ApprovalRangeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ApprovalRangeMetadata.meta == null)
                {
                    ApprovalRangeMetadata.meta = new ApprovalRangeMetadata();
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

                meta.AddTypeMap("ApprovalRangeID", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AmountFrom", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ApprovalLevelFinal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));


                meta.Source = "ApprovalRange";
                meta.Destination = "ApprovalRange";
                meta.spInsert = "proc_ApprovalRangeInsert";
                meta.spUpdate = "proc_ApprovalRangeUpdate";
                meta.spDelete = "proc_ApprovalRangeDelete";
                meta.spLoadAll = "proc_ApprovalRangeLoadAll";
                meta.spLoadByPrimaryKey = "proc_ApprovalRangeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ApprovalRangeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
