/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/8/2014 11:29:34 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esInvoiceAdjusmentCollection : esEntityCollectionWAuditLog
    {
        public esInvoiceAdjusmentCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "InvoiceAdjusmentCollection";
        }

        #region Query Logic
        protected void InitQuery(esInvoiceAdjusmentQuery query)
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
            this.InitQuery(query as esInvoiceAdjusmentQuery);
        }
        #endregion

        virtual public InvoiceAdjusment DetachEntity(InvoiceAdjusment entity)
        {
            return base.DetachEntity(entity) as InvoiceAdjusment;
        }

        virtual public InvoiceAdjusment AttachEntity(InvoiceAdjusment entity)
        {
            return base.AttachEntity(entity) as InvoiceAdjusment;
        }

        virtual public void Combine(InvoiceAdjusmentCollection collection)
        {
            base.Combine(collection);
        }

        new public InvoiceAdjusment this[int index]
        {
            get
            {
                return base[index] as InvoiceAdjusment;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(InvoiceAdjusment);
        }
    }



    [Serializable]
    abstract public class esInvoiceAdjusment : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esInvoiceAdjusmentQuery GetDynamicQuery()
        {
            return null;
        }

        public esInvoiceAdjusment()
        {

        }

        public esInvoiceAdjusment(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
        {
            esInvoiceAdjusmentQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
        {
            esParameters parms = new esParameters();
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "SupplierGuarantor": this.str.SupplierGuarantor = (string)value; break;
                        case "Type": this.str.Type = (string)value; break;
                        case "Amount": this.str.Amount = (string)value; break;
                        case "Reason": this.str.Reason = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "Note": this.str.Note = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "InvoicePaymentNo": this.str.InvoicePaymentNo = (string)value; break;
                        case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
                        case "OtherCost": this.str.OtherCost = (string)value; break;
                        case "BankCost": this.str.BankCost = (string)value; break;
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

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "PaymentAmount":

                            if (value == null || value is System.Decimal)
                                this.PaymentAmount = (System.Decimal?)value;
                            break;

                        case "OtherCost":

                            if (value == null || value is System.Decimal)
                                this.OtherCost = (System.Decimal?)value;
                            break;

                        case "BankCost":

                            if (value == null || value is System.Decimal)
                                this.BankCost = (System.Decimal?)value;
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
        /// Maps to InvoiceAdjusment.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceAdjusmentMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceAdjusmentMetadata.ColumnNames.TransactionDate, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.SupplierGuarantor
        /// </summary>
        virtual public System.String SupplierGuarantor
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.SupplierGuarantor);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.SupplierGuarantor, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.Type
        /// </summary>
        virtual public System.String Type
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Type);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Type, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.Amount
        /// </summary>
        virtual public System.Decimal? Amount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.Amount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.Amount, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.Reason
        /// </summary>
        virtual public System.String Reason
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Reason);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Reason, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(InvoiceAdjusmentMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(InvoiceAdjusmentMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.Note
        /// </summary>
        virtual public System.String Note
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Note);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.Note, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.InvoicePaymentNo
        /// </summary>
        virtual public System.String InvoicePaymentNo
        {
            get
            {
                return base.GetSystemString(InvoiceAdjusmentMetadata.ColumnNames.InvoicePaymentNo);
            }

            set
            {
                base.SetSystemString(InvoiceAdjusmentMetadata.ColumnNames.InvoicePaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.PaymentAmount
        /// </summary>
        virtual public System.Decimal? PaymentAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.PaymentAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.PaymentAmount, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.OtherCost
        /// </summary>
        virtual public System.Decimal? OtherCost
        {
            get
            {
                return base.GetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.OtherCost);
            }

            set
            {
                base.SetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.OtherCost, value);
            }
        }

        /// <summary>
        /// Maps to InvoiceAdjusment.BankCost
        /// </summary>
        virtual public System.Decimal? BankCost
        {
            get
            {
                return base.GetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.BankCost);
            }

            set
            {
                base.SetSystemDecimal(InvoiceAdjusmentMetadata.ColumnNames.BankCost, value);
            }
        }

        #endregion

        #region String Properties


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
            public esStrings(esInvoiceAdjusment entity)
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

            public System.String SupplierGuarantor
            {
                get
                {
                    System.String data = entity.SupplierGuarantor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierGuarantor = null;
                    else entity.SupplierGuarantor = Convert.ToString(value);
                }
            }

            public System.String Type
            {
                get
                {
                    System.String data = entity.Type;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Type = null;
                    else entity.Type = Convert.ToString(value);
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

            public System.String Reason
            {
                get
                {
                    System.String data = entity.Reason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Reason = null;
                    else entity.Reason = Convert.ToString(value);
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

            public System.String Note
            {
                get
                {
                    System.String data = entity.Note;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Note = null;
                    else entity.Note = Convert.ToString(value);
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

            public System.String InvoicePaymentNo
            {
                get
                {
                    System.String data = entity.InvoicePaymentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoicePaymentNo = null;
                    else entity.InvoicePaymentNo = Convert.ToString(value);
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

            public System.String OtherCost
            {
                get
                {
                    System.Decimal? data = entity.OtherCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OtherCost = null;
                    else entity.OtherCost = Convert.ToDecimal(value);
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


            private esInvoiceAdjusment entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esInvoiceAdjusmentQuery query)
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
                throw new Exception("esInvoiceAdjusment can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esInvoiceAdjusmentQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceAdjusmentMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SupplierGuarantor
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.SupplierGuarantor, esSystemType.String);
            }
        }

        public esQueryItem Type
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.Type, esSystemType.String);
            }
        }

        public esQueryItem Amount
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.Amount, esSystemType.Decimal);
            }
        }

        public esQueryItem Reason
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.Reason, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem Note
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.Note, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem InvoicePaymentNo
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.InvoicePaymentNo, esSystemType.String);
            }
        }

        public esQueryItem PaymentAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem OtherCost
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.OtherCost, esSystemType.Decimal);
            }
        }

        public esQueryItem BankCost
        {
            get
            {
                return new esQueryItem(this, InvoiceAdjusmentMetadata.ColumnNames.BankCost, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("InvoiceAdjusmentCollection")]
    public partial class InvoiceAdjusmentCollection : esInvoiceAdjusmentCollection, IEnumerable<InvoiceAdjusment>
    {
        public InvoiceAdjusmentCollection()
        {

        }

        public static implicit operator List<InvoiceAdjusment>(InvoiceAdjusmentCollection coll)
        {
            List<InvoiceAdjusment> list = new List<InvoiceAdjusment>();

            foreach (InvoiceAdjusment emp in coll)
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
                return InvoiceAdjusmentMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceAdjusmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new InvoiceAdjusment(row);
        }

        override protected esEntity CreateEntity()
        {
            return new InvoiceAdjusment();
        }


        #endregion


        [BrowsableAttribute(false)]
        public InvoiceAdjusmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceAdjusmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(InvoiceAdjusmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public InvoiceAdjusment AddNew()
        {
            InvoiceAdjusment entity = base.AddNewEntity() as InvoiceAdjusment;

            return entity;
        }

        public InvoiceAdjusment FindByPrimaryKey(System.String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as InvoiceAdjusment;
        }


        #region IEnumerable<InvoiceAdjusment> Members

        IEnumerator<InvoiceAdjusment> IEnumerable<InvoiceAdjusment>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as InvoiceAdjusment;
            }
        }

        #endregion

        private InvoiceAdjusmentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'InvoiceAdjusment' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("InvoiceAdjusment ({TransactionNo})")]
    [Serializable]
    public partial class InvoiceAdjusment : esInvoiceAdjusment
    {
        public InvoiceAdjusment()
        {

        }

        public InvoiceAdjusment(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceAdjusmentMetadata.Meta();
            }
        }



        override protected esInvoiceAdjusmentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceAdjusmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public InvoiceAdjusmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceAdjusmentQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(InvoiceAdjusmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private InvoiceAdjusmentQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class InvoiceAdjusmentQuery : esInvoiceAdjusmentQuery
    {
        public InvoiceAdjusmentQuery()
        {

        }

        public InvoiceAdjusmentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "InvoiceAdjusmentQuery";
        }


    }


    [Serializable]
    public partial class InvoiceAdjusmentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected InvoiceAdjusmentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.SupplierGuarantor, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.SupplierGuarantor;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.Type, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.Type;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.Amount;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.Reason, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.Reason;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.IsApproved;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.Note, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.Note;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.InvoicePaymentNo, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.InvoicePaymentNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.PaymentAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.PaymentAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.OtherCost, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.OtherCost;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceAdjusmentMetadata.ColumnNames.BankCost, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceAdjusmentMetadata.PropertyNames.BankCost;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public InvoiceAdjusmentMetadata Meta()
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
            public const string TransactionDate = "TransactionDate";
            public const string SupplierGuarantor = "SupplierGuarantor";
            public const string Type = "Type";
            public const string Amount = "Amount";
            public const string Reason = "Reason";
            public const string IsApproved = "IsApproved";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string InvoicePaymentNo = "InvoicePaymentNo";
            public const string PaymentAmount = "PaymentAmount";
            public const string OtherCost = "OtherCost";
            public const string BankCost = "BankCost";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string SupplierGuarantor = "SupplierGuarantor";
            public const string Type = "Type";
            public const string Amount = "Amount";
            public const string Reason = "Reason";
            public const string IsApproved = "IsApproved";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string InvoicePaymentNo = "InvoicePaymentNo";
            public const string PaymentAmount = "PaymentAmount";
            public const string OtherCost = "OtherCost";
            public const string BankCost = "BankCost";
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
            lock (typeof(InvoiceAdjusmentMetadata))
            {
                if (InvoiceAdjusmentMetadata.mapDelegates == null)
                {
                    InvoiceAdjusmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (InvoiceAdjusmentMetadata.meta == null)
                {
                    InvoiceAdjusmentMetadata.meta = new InvoiceAdjusmentMetadata();
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
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SupplierGuarantor", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Type", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
                meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvoicePaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("OtherCost", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BankCost", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "InvoiceAdjusment";
                meta.Destination = "InvoiceAdjusment";

                meta.spInsert = "proc_InvoiceAdjusmentInsert";
                meta.spUpdate = "proc_InvoiceAdjusmentUpdate";
                meta.spDelete = "proc_InvoiceAdjusmentDelete";
                meta.spLoadAll = "proc_InvoiceAdjusmentLoadAll";
                meta.spLoadByPrimaryKey = "proc_InvoiceAdjusmentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private InvoiceAdjusmentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
