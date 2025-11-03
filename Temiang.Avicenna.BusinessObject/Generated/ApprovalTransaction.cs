/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/20/18 8:09:45 PM
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
    abstract public class esApprovalTransactionCollection : esEntityCollectionWAuditLog
    {
        public esApprovalTransactionCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ApprovalTransactionCollection";
        }

        #region Query Logic
        protected void InitQuery(esApprovalTransactionQuery query)
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
            this.InitQuery(query as esApprovalTransactionQuery);
        }
        #endregion

        virtual public ApprovalTransaction DetachEntity(ApprovalTransaction entity)
        {
            return base.DetachEntity(entity) as ApprovalTransaction;
        }

        virtual public ApprovalTransaction AttachEntity(ApprovalTransaction entity)
        {
            return base.AttachEntity(entity) as ApprovalTransaction;
        }

        virtual public void Combine(ApprovalTransactionCollection collection)
        {
            base.Combine(collection);
        }

        new public ApprovalTransaction this[int index]
        {
            get
            {
                return base[index] as ApprovalTransaction;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ApprovalTransaction);
        }
    }

    [Serializable]
    abstract public class esApprovalTransaction : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esApprovalTransactionQuery GetDynamicQuery()
        {
            return null;
        }

        public esApprovalTransaction()
        {
        }

        public esApprovalTransaction(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, Int32 approvalLevel, String userID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, approvalLevel, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, approvalLevel, userID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 approvalLevel, String userID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, approvalLevel, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, approvalLevel, userID);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 approvalLevel, String userID)
        {
            esApprovalTransactionQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.ApprovalLevel == approvalLevel, query.UserID == userID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 approvalLevel, String userID)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("ApprovalLevel", approvalLevel);
            parms.Add("UserID", userID);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "ApprovalLevel": this.str.ApprovalLevel = (string)value; break;
                        case "UserID": this.str.UserID = (string)value; break;
                        case "ApprovalRangeID": this.str.ApprovalRangeID = (string)value; break;
                        case "IsApprovalLevelFinal": this.str.IsApprovalLevelFinal = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "TransactionCode": this.str.TransactionCode = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ApprovalLevel":

                            if (value == null || value is System.Int32)
                                this.ApprovalLevel = (System.Int32?)value;
                            break;
                        case "IsApprovalLevelFinal":

                            if (value == null || value is System.Boolean)
                                this.IsApprovalLevelFinal = (System.Boolean?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDate":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDate = (System.DateTime?)value;
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
        /// Maps to ApprovalTransaction.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.ApprovalLevel
        /// </summary>
        virtual public System.Int32? ApprovalLevel
        {
            get
            {
                return base.GetSystemInt32(ApprovalTransactionMetadata.ColumnNames.ApprovalLevel);
            }

            set
            {
                base.SetSystemInt32(ApprovalTransactionMetadata.ColumnNames.ApprovalLevel, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.UserID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.ApprovalRangeID
        /// </summary>
        virtual public System.String ApprovalRangeID
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.ApprovalRangeID);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.ApprovalRangeID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.IsApprovalLevelFinal
        /// </summary>
        virtual public System.Boolean? IsApprovalLevelFinal
        {
            get
            {
                return base.GetSystemBoolean(ApprovalTransactionMetadata.ColumnNames.IsApprovalLevelFinal);
            }

            set
            {
                base.SetSystemBoolean(ApprovalTransactionMetadata.ColumnNames.IsApprovalLevelFinal, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(ApprovalTransactionMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(ApprovalTransactionMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.ApprovedDate
        /// </summary>
        virtual public System.DateTime? ApprovedDate
        {
            get
            {
                return base.GetSystemDateTime(ApprovalTransactionMetadata.ColumnNames.ApprovedDate);
            }

            set
            {
                base.SetSystemDateTime(ApprovalTransactionMetadata.ColumnNames.ApprovedDate, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ApprovalTransactionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ApprovalTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ApprovalTransaction.TransactionCode
        /// </summary>
        virtual public System.String TransactionCode
        {
            get
            {
                return base.GetSystemString(ApprovalTransactionMetadata.ColumnNames.TransactionCode);
            }

            set
            {
                base.SetSystemString(ApprovalTransactionMetadata.ColumnNames.TransactionCode, value);
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
            public esStrings(esApprovalTransaction entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }
            public System.String ApprovalLevel
            {
                get
                {
                    System.Int32? data = entity.ApprovalLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovalLevel = null;
                    else entity.ApprovalLevel = Convert.ToInt32(value);
                }
            }
            public System.String UserID
            {
                get
                {
                    System.String data = entity.UserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UserID = null;
                    else entity.UserID = Convert.ToString(value);
                }
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
            public System.String IsApprovalLevelFinal
            {
                get
                {
                    System.Boolean? data = entity.IsApprovalLevelFinal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApprovalLevelFinal = null;
                    else entity.IsApprovalLevelFinal = Convert.ToBoolean(value);
                }
            }
            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }
            public System.String ApprovedDate
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDate = null;
                    else entity.ApprovedDate = Convert.ToDateTime(value);
                }
            }
            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
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
            private esApprovalTransaction entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esApprovalTransactionQuery query)
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
                throw new Exception("esApprovalTransaction can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ApprovalTransaction : esApprovalTransaction
    {
    }

    [Serializable]
    abstract public class esApprovalTransactionQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ApprovalTransactionMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem ApprovalLevel
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.ApprovalLevel, esSystemType.Int32);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem ApprovalRangeID
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.ApprovalRangeID, esSystemType.String);
            }
        }

        public esQueryItem IsApprovalLevelFinal
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.IsApprovalLevelFinal, esSystemType.Boolean);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDate
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem TransactionCode
        {
            get
            {
                return new esQueryItem(this, ApprovalTransactionMetadata.ColumnNames.TransactionCode, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ApprovalTransactionCollection")]
    public partial class ApprovalTransactionCollection : esApprovalTransactionCollection, IEnumerable<ApprovalTransaction>
    {
        public ApprovalTransactionCollection()
        {

        }

        public static implicit operator List<ApprovalTransaction>(ApprovalTransactionCollection coll)
        {
            List<ApprovalTransaction> list = new List<ApprovalTransaction>();

            foreach (ApprovalTransaction emp in coll)
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
                return ApprovalTransactionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ApprovalTransaction(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ApprovalTransaction();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ApprovalTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalTransactionQuery();
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
        public bool Load(ApprovalTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ApprovalTransaction AddNew()
        {
            ApprovalTransaction entity = base.AddNewEntity() as ApprovalTransaction;

            return entity;
        }
        public ApprovalTransaction FindByPrimaryKey(String transactionNo, Int32 approvalLevel, String userID)
        {
            return base.FindByPrimaryKey(transactionNo, approvalLevel, userID) as ApprovalTransaction;
        }

        #region IEnumerable< ApprovalTransaction> Members

        IEnumerator<ApprovalTransaction> IEnumerable<ApprovalTransaction>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ApprovalTransaction;
            }
        }

        #endregion

        private ApprovalTransactionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ApprovalTransaction' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ApprovalTransaction ({TransactionNo, ApprovalLevel, UserID})")]
    [Serializable]
    public partial class ApprovalTransaction : esApprovalTransaction
    {
        public ApprovalTransaction()
        {
        }

        public ApprovalTransaction(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ApprovalTransactionMetadata.Meta();
            }
        }

        override protected esApprovalTransactionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ApprovalTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ApprovalTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ApprovalTransactionQuery();
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
        public bool Load(ApprovalTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ApprovalTransactionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ApprovalTransactionQuery : esApprovalTransactionQuery
    {
        public ApprovalTransactionQuery()
        {

        }

        public ApprovalTransactionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ApprovalTransactionQuery";
        }
    }

    [Serializable]
    public partial class ApprovalTransactionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ApprovalTransactionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.ApprovalLevel, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.ApprovalLevel;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.UserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.UserID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.ApprovalRangeID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.ApprovalRangeID;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.IsApprovalLevelFinal, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.IsApprovalLevelFinal;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.IsApproved;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.ApprovedDate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.ApprovedDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.ApprovedByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ApprovalTransactionMetadata.ColumnNames.TransactionCode, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ApprovalTransactionMetadata.PropertyNames.TransactionCode;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ApprovalTransactionMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string ApprovalLevel = "ApprovalLevel";
            public const string UserID = "UserID";
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string IsApprovalLevelFinal = "IsApprovalLevelFinal";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string TransactionCode = "TransactionCode";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string ApprovalLevel = "ApprovalLevel";
            public const string UserID = "UserID";
            public const string ApprovalRangeID = "ApprovalRangeID";
            public const string IsApprovalLevelFinal = "IsApprovalLevelFinal";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(ApprovalTransactionMetadata))
            {
                if (ApprovalTransactionMetadata.mapDelegates == null)
                {
                    ApprovalTransactionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ApprovalTransactionMetadata.meta == null)
                {
                    ApprovalTransactionMetadata.meta = new ApprovalTransactionMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ApprovalLevel", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ApprovalRangeID", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("IsApprovalLevelFinal", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));


                meta.Source = "ApprovalTransaction";
                meta.Destination = "ApprovalTransaction";
                meta.spInsert = "proc_ApprovalTransactionInsert";
                meta.spUpdate = "proc_ApprovalTransactionUpdate";
                meta.spDelete = "proc_ApprovalTransactionDelete";
                meta.spLoadAll = "proc_ApprovalTransactionLoadAll";
                meta.spLoadByPrimaryKey = "proc_ApprovalTransactionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ApprovalTransactionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
