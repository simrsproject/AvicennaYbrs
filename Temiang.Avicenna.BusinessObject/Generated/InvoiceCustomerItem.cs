/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/17/2018 12:59:45 PM
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
    abstract public class esInvoiceCustomerItemCollection : esEntityCollectionWAuditLog
    {
        public esInvoiceCustomerItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "InvoiceCustomerItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esInvoiceCustomerItemQuery query)
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
            this.InitQuery(query as esInvoiceCustomerItemQuery);
        }
        #endregion

        virtual public InvoiceCustomerItem DetachEntity(InvoiceCustomerItem entity)
        {
            return base.DetachEntity(entity) as InvoiceCustomerItem;
        }

        virtual public InvoiceCustomerItem AttachEntity(InvoiceCustomerItem entity)
        {
            return base.AttachEntity(entity) as InvoiceCustomerItem;
        }

        virtual public void Combine(InvoiceCustomerItemCollection collection)
        {
            base.Combine(collection);
        }

        new public InvoiceCustomerItem this[int index]
        {
            get
            {
                return base[index] as InvoiceCustomerItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(InvoiceCustomerItem);
        }
    }

    [Serializable]
    abstract public class esInvoiceCustomerItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esInvoiceCustomerItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esInvoiceCustomerItem()
        {
        }

        public esInvoiceCustomerItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String invoiceNo, String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String invoiceNo, String transactionNo)
        {
            esInvoiceCustomerItemQuery query = this.GetDynamicQuery();
            query.Where(query.InvoiceNo == invoiceNo, query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo, String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("InvoiceNo", invoiceNo);
            parms.Add("TransactionNo", transactionNo);
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
                        case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                        case "VerifyAmount": this.str.VerifyAmount = (string)value; break;
                        case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
                        case "OtherAmount": this.str.OtherAmount = (string)value; break;
                        case "BankCost": this.str.BankCost = (string)value; break;
                        case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;
                        case "AccountID": this.str.AccountID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;
                        case "Amount":

                            if (value == null || value is System.Decimal)
                                this.Amount = (System.Decimal?)value;
                            break;
                        case "VerifyAmount":

                            if (value == null || value is System.Decimal)
                                this.VerifyAmount = (System.Decimal?)value;
                            break;
                        case "PaymentAmount":

                            if (value == null || value is System.Decimal)
                                this.PaymentAmount = (System.Decimal?)value;
                            break;
                        case "OtherAmount":

                            if (value == null || value is System.Decimal)
                                this.OtherAmount = (System.Decimal?)value;
                            break;
                        case "BankCost":

                            if (value == null || value is System.Decimal)
                                this.BankCost = (System.Decimal?)value;
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
        /// Maps to InvoiceCustomerItem.InvoiceNo
        /// </summary>
        virtual public System.String InvoiceNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.InvoiceNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.InvoiceNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerItemMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerItemMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.Amount, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.VerifyAmount
        /// </summary>
        virtual public System.Decimal? VerifyAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.VerifyAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.VerifyAmount, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.PaymentAmount
        /// </summary>
        virtual public System.Decimal? PaymentAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.PaymentAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.PaymentAmount, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.OtherAmount
        /// </summary>
        virtual public System.Decimal? OtherAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.OtherAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.OtherAmount, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.BankCost
        /// </summary>
        virtual public System.Decimal? BankCost
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.BankCost);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerItemMetadata.ColumnNames.BankCost, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.InvoiceReferenceNo
        /// </summary>
        virtual public System.String InvoiceReferenceNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.InvoiceReferenceNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.InvoiceReferenceNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.AccountID
        /// </summary>
        virtual public System.String AccountID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.AccountID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.AccountID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomerItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esInvoiceCustomerItem entity)
            {
                this.entity = entity;
            }
            public System.String InvoiceNo
            {
                get
                {
                    System.String data = entity.InvoiceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceNo = null;
                    else entity.InvoiceNo = Convert.ToString(value);
                }
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
            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
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
            public System.String Amount
            {
                get
                {
                    System.Decimal? data = entity.Amount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Amount = null;
                    else entity.Amount = Convert.ToDecimal(value);
                }
            }
            public System.String VerifyAmount
            {
                get
                {
                    System.Decimal? data = entity.VerifyAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerifyAmount = null;
                    else entity.VerifyAmount = Convert.ToDecimal(value);
                }
            }
            public System.String PaymentAmount
            {
                get
                {
                    System.Decimal? data = entity.PaymentAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentAmount = null;
                    else entity.PaymentAmount = Convert.ToDecimal(value);
                }
            }
            public System.String OtherAmount
            {
                get
                {
                    System.Decimal? data = entity.OtherAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OtherAmount = null;
                    else entity.OtherAmount = Convert.ToDecimal(value);
                }
            }
            public System.String BankCost
            {
                get
                {
                    System.Decimal? data = entity.BankCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankCost = null;
                    else entity.BankCost = Convert.ToDecimal(value);
                }
            }
            public System.String InvoiceReferenceNo
            {
                get
                {
                    System.String data = entity.InvoiceReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceReferenceNo = null;
                    else entity.InvoiceReferenceNo = Convert.ToString(value);
                }
            }
            public System.String AccountID
            {
                get
                {
                    System.String data = entity.AccountID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AccountID = null;
                    else entity.AccountID = Convert.ToString(value);
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
            private esInvoiceCustomerItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esInvoiceCustomerItemQuery query)
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
                throw new Exception("esInvoiceCustomerItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class InvoiceCustomerItem : esInvoiceCustomerItem
    {
    }

    [Serializable]
    abstract public class esInvoiceCustomerItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return InvoiceCustomerItemMetadata.Meta();
            }
        }

        public esQueryItem InvoiceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.InvoiceNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem VerifyAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.VerifyAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem PaymentAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem OtherAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.OtherAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem BankCost
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.BankCost, esSystemType.Decimal);
            }
        }

        public esQueryItem InvoiceReferenceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem AccountID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.AccountID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("InvoiceCustomerItemCollection")]
    public partial class InvoiceCustomerItemCollection : esInvoiceCustomerItemCollection, IEnumerable<InvoiceCustomerItem>
    {
        public InvoiceCustomerItemCollection()
        {

        }

        public static implicit operator List<InvoiceCustomerItem>(InvoiceCustomerItemCollection coll)
        {
            List<InvoiceCustomerItem> list = new List<InvoiceCustomerItem>();

            foreach (InvoiceCustomerItem emp in coll)
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
                return InvoiceCustomerItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceCustomerItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new InvoiceCustomerItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new InvoiceCustomerItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public InvoiceCustomerItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceCustomerItemQuery();
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
        public bool Load(InvoiceCustomerItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public InvoiceCustomerItem AddNew()
        {
            InvoiceCustomerItem entity = base.AddNewEntity() as InvoiceCustomerItem;

            return entity;
        }
        public InvoiceCustomerItem FindByPrimaryKey(String invoiceNo, String transactionNo)
        {
            return base.FindByPrimaryKey(invoiceNo, transactionNo) as InvoiceCustomerItem;
        }

        #region IEnumerable< InvoiceCustomerItem> Members

        IEnumerator<InvoiceCustomerItem> IEnumerable<InvoiceCustomerItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as InvoiceCustomerItem;
            }
        }

        #endregion

        private InvoiceCustomerItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'InvoiceCustomerItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("InvoiceCustomerItem ({InvoiceNo, TransactionNo})")]
    [Serializable]
    public partial class InvoiceCustomerItem : esInvoiceCustomerItem
    {
        public InvoiceCustomerItem()
        {
        }

        public InvoiceCustomerItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceCustomerItemMetadata.Meta();
            }
        }

        override protected esInvoiceCustomerItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceCustomerItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public InvoiceCustomerItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceCustomerItemQuery();
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
        public bool Load(InvoiceCustomerItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private InvoiceCustomerItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class InvoiceCustomerItemQuery : esInvoiceCustomerItemQuery
    {
        public InvoiceCustomerItemQuery()
        {

        }

        public InvoiceCustomerItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "InvoiceCustomerItemQuery";
        }
    }

    [Serializable]
    public partial class InvoiceCustomerItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected InvoiceCustomerItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.InvoiceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.TransactionDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.Amount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.VerifyAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.VerifyAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.PaymentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.PaymentAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.OtherAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.OtherAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.BankCost, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.BankCost;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.InvoiceReferenceNo, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.InvoiceReferenceNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.AccountID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.AccountID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public InvoiceCustomerItemMetadata Meta()
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
            public const string InvoiceNo = "InvoiceNo";
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string Notes = "Notes";
            public const string Amount = "Amount";
            public const string VerifyAmount = "VerifyAmount";
            public const string PaymentAmount = "PaymentAmount";
            public const string OtherAmount = "OtherAmount";
            public const string BankCost = "BankCost";
            public const string InvoiceReferenceNo = "InvoiceReferenceNo";
            public const string AccountID = "AccountID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string InvoiceNo = "InvoiceNo";
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string Notes = "Notes";
            public const string Amount = "Amount";
            public const string VerifyAmount = "VerifyAmount";
            public const string PaymentAmount = "PaymentAmount";
            public const string OtherAmount = "OtherAmount";
            public const string BankCost = "BankCost";
            public const string InvoiceReferenceNo = "InvoiceReferenceNo";
            public const string AccountID = "AccountID";
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
            lock (typeof(InvoiceCustomerItemMetadata))
            {
                if (InvoiceCustomerItemMetadata.mapDelegates == null)
                {
                    InvoiceCustomerItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (InvoiceCustomerItemMetadata.meta == null)
                {
                    InvoiceCustomerItemMetadata.meta = new InvoiceCustomerItemMetadata();
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

                meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("VerifyAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PaymentAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("OtherAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BankCost", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AccountID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "InvoiceCustomerItem";
                meta.Destination = "InvoiceCustomerItem";
                meta.spInsert = "proc_InvoiceCustomerItemInsert";
                meta.spUpdate = "proc_InvoiceCustomerItemUpdate";
                meta.spDelete = "proc_InvoiceCustomerItemDelete";
                meta.spLoadAll = "proc_InvoiceCustomerItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_InvoiceCustomerItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private InvoiceCustomerItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
