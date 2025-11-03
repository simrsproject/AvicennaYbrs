/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/18/2018 9:36:15 AM
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
    abstract public class esInvoiceCustomerCollection : esEntityCollectionWAuditLog
    {
        public esInvoiceCustomerCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "InvoiceCustomerCollection";
        }

        #region Query Logic
        protected void InitQuery(esInvoiceCustomerQuery query)
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
            this.InitQuery(query as esInvoiceCustomerQuery);
        }
        #endregion

        virtual public InvoiceCustomer DetachEntity(InvoiceCustomer entity)
        {
            return base.DetachEntity(entity) as InvoiceCustomer;
        }

        virtual public InvoiceCustomer AttachEntity(InvoiceCustomer entity)
        {
            return base.AttachEntity(entity) as InvoiceCustomer;
        }

        virtual public void Combine(InvoiceCustomerCollection collection)
        {
            base.Combine(collection);
        }

        new public InvoiceCustomer this[int index]
        {
            get
            {
                return base[index] as InvoiceCustomer;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(InvoiceCustomer);
        }
    }

    [Serializable]
    abstract public class esInvoiceCustomer : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esInvoiceCustomerQuery GetDynamicQuery()
        {
            return null;
        }

        public esInvoiceCustomer()
        {
        }

        public esInvoiceCustomer(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String invoiceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(invoiceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(invoiceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String invoiceNo)
        {
            esInvoiceCustomerQuery query = this.GetDynamicQuery();
            query.Where(query.InvoiceNo == invoiceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("InvoiceNo", invoiceNo);
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
                        case "SRReceivableType": this.str.SRReceivableType = (string)value; break;
                        case "InvoiceDate": this.str.InvoiceDate = (string)value; break;
                        case "InvoiceDueDate": this.str.InvoiceDueDate = (string)value; break;
                        case "InvoiceTOP": this.str.InvoiceTOP = (string)value; break;
                        case "CustomerID": this.str.CustomerID = (string)value; break;
                        case "SRInvoicePayment": this.str.SRInvoicePayment = (string)value; break;
                        case "BankID": this.str.BankID = (string)value; break;
                        case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
                        case "InvoiceNotes": this.str.InvoiceNotes = (string)value; break;
                        case "PaymentDate": this.str.PaymentDate = (string)value; break;
                        case "SRReceivableStatus": this.str.SRReceivableStatus = (string)value; break;
                        case "VoucherID": this.str.VoucherID = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDate": this.str.VoidDate = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "VerifyDate": this.str.VerifyDate = (string)value; break;
                        case "VerifyByUserID": this.str.VerifyByUserID = (string)value; break;
                        case "PaymentByUserID": this.str.PaymentByUserID = (string)value; break;
                        case "AgingDate": this.str.AgingDate = (string)value; break;
                        case "IsPaymentApproved": this.str.IsPaymentApproved = (string)value; break;
                        case "PaymentApprovedDate": this.str.PaymentApprovedDate = (string)value; break;
                        case "PaymentApprovedByUserID": this.str.PaymentApprovedByUserID = (string)value; break;
                        case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
                        case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
                        case "SRCardProvider": this.str.SRCardProvider = (string)value; break;
                        case "SRCardType": this.str.SRCardType = (string)value; break;
                        case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
                        case "CardHolderName": this.str.CardHolderName = (string)value; break;
                        case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;
                        case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
                        case "IsInvoicePayment": this.str.IsInvoicePayment = (string)value; break;
                        case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;
                        case "Reason": this.str.Reason = (string)value; break;
                        case "TransferDate": this.str.TransferDate = (string)value; break;
                        case "TransferNumber": this.str.TransferNumber = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "InvoiceDate":

                            if (value == null || value is System.DateTime)
                                this.InvoiceDate = (System.DateTime?)value;
                            break;
                        case "InvoiceDueDate":

                            if (value == null || value is System.DateTime)
                                this.InvoiceDueDate = (System.DateTime?)value;
                            break;
                        case "InvoiceTOP":

                            if (value == null || value is System.Decimal)
                                this.InvoiceTOP = (System.Decimal?)value;
                            break;
                        case "PaymentDate":

                            if (value == null || value is System.DateTime)
                                this.PaymentDate = (System.DateTime?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDate":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDate = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "VoidDate":

                            if (value == null || value is System.DateTime)
                                this.VoidDate = (System.DateTime?)value;
                            break;
                        case "VerifyDate":

                            if (value == null || value is System.DateTime)
                                this.VerifyDate = (System.DateTime?)value;
                            break;
                        case "AgingDate":

                            if (value == null || value is System.DateTime)
                                this.AgingDate = (System.DateTime?)value;
                            break;
                        case "IsPaymentApproved":

                            if (value == null || value is System.Boolean)
                                this.IsPaymentApproved = (System.Boolean?)value;
                            break;
                        case "PaymentApprovedDate":

                            if (value == null || value is System.DateTime)
                                this.PaymentApprovedDate = (System.DateTime?)value;
                            break;
                        case "CardFeeAmount":

                            if (value == null || value is System.Decimal)
                                this.CardFeeAmount = (System.Decimal?)value;
                            break;
                        case "IsInvoicePayment":

                            if (value == null || value is System.Boolean)
                                this.IsInvoicePayment = (System.Boolean?)value;
                            break;
                        case "TransferDate":

                            if (value == null || value is System.DateTime)
                                this.TransferDate = (System.DateTime?)value;
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
        /// Maps to InvoiceCustomer.InvoiceNo
        /// </summary>
        virtual public System.String InvoiceNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRReceivableType
        /// </summary>
        virtual public System.String SRReceivableType
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRReceivableType);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRReceivableType, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.InvoiceDate
        /// </summary>
        virtual public System.DateTime? InvoiceDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.InvoiceDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.InvoiceDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.InvoiceDueDate
        /// </summary>
        virtual public System.DateTime? InvoiceDueDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.InvoiceDueDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.InvoiceDueDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.InvoiceTOP
        /// </summary>
        virtual public System.Decimal? InvoiceTOP
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerMetadata.ColumnNames.InvoiceTOP);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerMetadata.ColumnNames.InvoiceTOP, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.CustomerID
        /// </summary>
        virtual public System.String CustomerID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.CustomerID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.CustomerID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRInvoicePayment
        /// </summary>
        virtual public System.String SRInvoicePayment
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRInvoicePayment);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRInvoicePayment, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.BankID
        /// </summary>
        virtual public System.String BankID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.BankID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.BankID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.BankAccountNo
        /// </summary>
        virtual public System.String BankAccountNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.BankAccountNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.BankAccountNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.InvoiceNotes
        /// </summary>
        virtual public System.String InvoiceNotes
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceNotes);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceNotes, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.PaymentDate
        /// </summary>
        virtual public System.DateTime? PaymentDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.PaymentDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.PaymentDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRReceivableStatus
        /// </summary>
        virtual public System.String SRReceivableStatus
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRReceivableStatus);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRReceivableStatus, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.VoucherID
        /// </summary>
        virtual public System.String VoucherID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.VoucherID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.VoucherID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.ApprovedDate
        /// </summary>
        virtual public System.DateTime? ApprovedDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.ApprovedDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.ApprovedDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.VoidDate
        /// </summary>
        virtual public System.DateTime? VoidDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.VoidDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.VoidDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.VerifyDate
        /// </summary>
        virtual public System.DateTime? VerifyDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.VerifyDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.VerifyDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.VerifyByUserID
        /// </summary>
        virtual public System.String VerifyByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.VerifyByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.VerifyByUserID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.PaymentByUserID
        /// </summary>
        virtual public System.String PaymentByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.PaymentByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.PaymentByUserID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.AgingDate
        /// </summary>
        virtual public System.DateTime? AgingDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.AgingDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.AgingDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.IsPaymentApproved
        /// </summary>
        virtual public System.Boolean? IsPaymentApproved
        {
            get
            {
                return base.GetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsPaymentApproved);
            }

            set
            {
                base.SetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsPaymentApproved, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.PaymentApprovedDate
        /// </summary>
        virtual public System.DateTime? PaymentApprovedDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.PaymentApprovedByUserID
        /// </summary>
        virtual public System.String PaymentApprovedByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRPaymentType
        /// </summary>
        virtual public System.String SRPaymentType
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRPaymentType);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRPaymentType, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRPaymentMethod
        /// </summary>
        virtual public System.String SRPaymentMethod
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRPaymentMethod);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRPaymentMethod, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRCardProvider
        /// </summary>
        virtual public System.String SRCardProvider
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRCardProvider);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRCardProvider, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRCardType
        /// </summary>
        virtual public System.String SRCardType
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRCardType);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRCardType, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.EDCMachineID
        /// </summary>
        virtual public System.String EDCMachineID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.EDCMachineID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.EDCMachineID, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.CardHolderName
        /// </summary>
        virtual public System.String CardHolderName
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.CardHolderName);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.CardHolderName, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.CardFeeAmount
        /// </summary>
        virtual public System.Decimal? CardFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceCustomerMetadata.ColumnNames.CardFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceCustomerMetadata.ColumnNames.CardFeeAmount, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.SRDiscountReason
        /// </summary>
        virtual public System.String SRDiscountReason
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.SRDiscountReason);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.SRDiscountReason, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.IsInvoicePayment
        /// </summary>
        virtual public System.Boolean? IsInvoicePayment
        {
            get
            {
                return base.GetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsInvoicePayment);
            }

            set
            {
                base.SetSystemBoolean(InvoiceCustomerMetadata.ColumnNames.IsInvoicePayment, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.InvoiceReferenceNo
        /// </summary>
        virtual public System.String InvoiceReferenceNo
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceReferenceNo);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.InvoiceReferenceNo, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.Reason
        /// </summary>
        virtual public System.String Reason
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.Reason);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.Reason, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.TransferDate
        /// </summary>
        virtual public System.DateTime? TransferDate
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.TransferDate);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.TransferDate, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.TransferNumber
        /// </summary>
        virtual public System.String TransferNumber
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.TransferNumber);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.TransferNumber, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(InvoiceCustomerMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to InvoiceCustomer.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(InvoiceCustomerMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(InvoiceCustomerMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esInvoiceCustomer entity)
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
            public System.String SRReceivableType
            {
                get
                {
                    System.String data = entity.SRReceivableType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReceivableType = null;
                    else entity.SRReceivableType = Convert.ToString(value);
                }
            }
            public System.String InvoiceDate
            {
                get
                {
                    System.DateTime? data = entity.InvoiceDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceDate = null;
                    else entity.InvoiceDate = Convert.ToDateTime(value);
                }
            }
            public System.String InvoiceDueDate
            {
                get
                {
                    System.DateTime? data = entity.InvoiceDueDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceDueDate = null;
                    else entity.InvoiceDueDate = Convert.ToDateTime(value);
                }
            }
            public System.String InvoiceTOP
            {
                get
                {
                    System.Decimal? data = entity.InvoiceTOP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceTOP = null;
                    else entity.InvoiceTOP = Convert.ToDecimal(value);
                }
            }
            public System.String CustomerID
            {
                get
                {
                    System.String data = entity.CustomerID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CustomerID = null;
                    else entity.CustomerID = Convert.ToString(value);
                }
            }
            public System.String SRInvoicePayment
            {
                get
                {
                    System.String data = entity.SRInvoicePayment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRInvoicePayment = null;
                    else entity.SRInvoicePayment = Convert.ToString(value);
                }
            }
            public System.String BankID
            {
                get
                {
                    System.String data = entity.BankID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankID = null;
                    else entity.BankID = Convert.ToString(value);
                }
            }
            public System.String BankAccountNo
            {
                get
                {
                    System.String data = entity.BankAccountNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountNo = null;
                    else entity.BankAccountNo = Convert.ToString(value);
                }
            }
            public System.String InvoiceNotes
            {
                get
                {
                    System.String data = entity.InvoiceNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InvoiceNotes = null;
                    else entity.InvoiceNotes = Convert.ToString(value);
                }
            }
            public System.String PaymentDate
            {
                get
                {
                    System.DateTime? data = entity.PaymentDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentDate = null;
                    else entity.PaymentDate = Convert.ToDateTime(value);
                }
            }
            public System.String SRReceivableStatus
            {
                get
                {
                    System.String data = entity.SRReceivableStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReceivableStatus = null;
                    else entity.SRReceivableStatus = Convert.ToString(value);
                }
            }
            public System.String VoucherID
            {
                get
                {
                    System.String data = entity.VoucherID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoucherID = null;
                    else entity.VoucherID = Convert.ToString(value);
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
            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
                }
            }
            public System.String VoidDate
            {
                get
                {
                    System.DateTime? data = entity.VoidDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDate = null;
                    else entity.VoidDate = Convert.ToDateTime(value);
                }
            }
            public System.String VoidByUserID
            {
                get
                {
                    System.String data = entity.VoidByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidByUserID = null;
                    else entity.VoidByUserID = Convert.ToString(value);
                }
            }
            public System.String VerifyDate
            {
                get
                {
                    System.DateTime? data = entity.VerifyDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerifyDate = null;
                    else entity.VerifyDate = Convert.ToDateTime(value);
                }
            }
            public System.String VerifyByUserID
            {
                get
                {
                    System.String data = entity.VerifyByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerifyByUserID = null;
                    else entity.VerifyByUserID = Convert.ToString(value);
                }
            }
            public System.String PaymentByUserID
            {
                get
                {
                    System.String data = entity.PaymentByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentByUserID = null;
                    else entity.PaymentByUserID = Convert.ToString(value);
                }
            }
            public System.String AgingDate
            {
                get
                {
                    System.DateTime? data = entity.AgingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AgingDate = null;
                    else entity.AgingDate = Convert.ToDateTime(value);
                }
            }
            public System.String IsPaymentApproved
            {
                get
                {
                    System.Boolean? data = entity.IsPaymentApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPaymentApproved = null;
                    else entity.IsPaymentApproved = Convert.ToBoolean(value);
                }
            }
            public System.String PaymentApprovedDate
            {
                get
                {
                    System.DateTime? data = entity.PaymentApprovedDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentApprovedDate = null;
                    else entity.PaymentApprovedDate = Convert.ToDateTime(value);
                }
            }
            public System.String PaymentApprovedByUserID
            {
                get
                {
                    System.String data = entity.PaymentApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentApprovedByUserID = null;
                    else entity.PaymentApprovedByUserID = Convert.ToString(value);
                }
            }
            public System.String SRPaymentType
            {
                get
                {
                    System.String data = entity.SRPaymentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPaymentType = null;
                    else entity.SRPaymentType = Convert.ToString(value);
                }
            }
            public System.String SRPaymentMethod
            {
                get
                {
                    System.String data = entity.SRPaymentMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
                    else entity.SRPaymentMethod = Convert.ToString(value);
                }
            }
            public System.String SRCardProvider
            {
                get
                {
                    System.String data = entity.SRCardProvider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCardProvider = null;
                    else entity.SRCardProvider = Convert.ToString(value);
                }
            }
            public System.String SRCardType
            {
                get
                {
                    System.String data = entity.SRCardType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCardType = null;
                    else entity.SRCardType = Convert.ToString(value);
                }
            }
            public System.String EDCMachineID
            {
                get
                {
                    System.String data = entity.EDCMachineID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EDCMachineID = null;
                    else entity.EDCMachineID = Convert.ToString(value);
                }
            }
            public System.String CardHolderName
            {
                get
                {
                    System.String data = entity.CardHolderName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CardHolderName = null;
                    else entity.CardHolderName = Convert.ToString(value);
                }
            }
            public System.String CardFeeAmount
            {
                get
                {
                    System.Decimal? data = entity.CardFeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CardFeeAmount = null;
                    else entity.CardFeeAmount = Convert.ToDecimal(value);
                }
            }
            public System.String SRDiscountReason
            {
                get
                {
                    System.String data = entity.SRDiscountReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiscountReason = null;
                    else entity.SRDiscountReason = Convert.ToString(value);
                }
            }
            public System.String IsInvoicePayment
            {
                get
                {
                    System.Boolean? data = entity.IsInvoicePayment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInvoicePayment = null;
                    else entity.IsInvoicePayment = Convert.ToBoolean(value);
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
            public System.String TransferDate
            {
                get
                {
                    System.DateTime? data = entity.TransferDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferDate = null;
                    else entity.TransferDate = Convert.ToDateTime(value);
                }
            }
            public System.String TransferNumber
            {
                get
                {
                    System.String data = entity.TransferNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferNumber = null;
                    else entity.TransferNumber = Convert.ToString(value);
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
            private esInvoiceCustomer entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esInvoiceCustomerQuery query)
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
                throw new Exception("esInvoiceCustomer can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class InvoiceCustomer : esInvoiceCustomer
    {
    }

    [Serializable]
    abstract public class esInvoiceCustomerQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return InvoiceCustomerMetadata.Meta();
            }
        }

        public esQueryItem InvoiceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceNo, esSystemType.String);
            }
        }

        public esQueryItem SRReceivableType
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRReceivableType, esSystemType.String);
            }
        }

        public esQueryItem InvoiceDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceDate, esSystemType.DateTime);
            }
        }

        public esQueryItem InvoiceDueDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceDueDate, esSystemType.DateTime);
            }
        }

        public esQueryItem InvoiceTOP
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceTOP, esSystemType.Decimal);
            }
        }

        public esQueryItem CustomerID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.CustomerID, esSystemType.String);
            }
        }

        public esQueryItem SRInvoicePayment
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRInvoicePayment, esSystemType.String);
            }
        }

        public esQueryItem BankID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.BankID, esSystemType.String);
            }
        }

        public esQueryItem BankAccountNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.BankAccountNo, esSystemType.String);
            }
        }

        public esQueryItem InvoiceNotes
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceNotes, esSystemType.String);
            }
        }

        public esQueryItem PaymentDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRReceivableStatus
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRReceivableStatus, esSystemType.String);
            }
        }

        public esQueryItem VoucherID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.VoucherID, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem VerifyDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.VerifyDate, esSystemType.DateTime);
            }
        }

        public esQueryItem VerifyByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.VerifyByUserID, esSystemType.String);
            }
        }

        public esQueryItem PaymentByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.PaymentByUserID, esSystemType.String);
            }
        }

        public esQueryItem AgingDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.AgingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsPaymentApproved
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.IsPaymentApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem PaymentApprovedDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.PaymentApprovedDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PaymentApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.PaymentApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem SRPaymentType
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRPaymentType, esSystemType.String);
            }
        }

        public esQueryItem SRPaymentMethod
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
            }
        }

        public esQueryItem SRCardProvider
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRCardProvider, esSystemType.String);
            }
        }

        public esQueryItem SRCardType
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRCardType, esSystemType.String);
            }
        }

        public esQueryItem EDCMachineID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.EDCMachineID, esSystemType.String);
            }
        }

        public esQueryItem CardHolderName
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.CardHolderName, esSystemType.String);
            }
        }

        public esQueryItem CardFeeAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem SRDiscountReason
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
            }
        }

        public esQueryItem IsInvoicePayment
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.IsInvoicePayment, esSystemType.Boolean);
            }
        }

        public esQueryItem InvoiceReferenceNo
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem Reason
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.Reason, esSystemType.String);
            }
        }

        public esQueryItem TransferDate
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.TransferDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransferNumber
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.TransferNumber, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, InvoiceCustomerMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("InvoiceCustomerCollection")]
    public partial class InvoiceCustomerCollection : esInvoiceCustomerCollection, IEnumerable<InvoiceCustomer>
    {
        public InvoiceCustomerCollection()
        {

        }

        public static implicit operator List<InvoiceCustomer>(InvoiceCustomerCollection coll)
        {
            List<InvoiceCustomer> list = new List<InvoiceCustomer>();

            foreach (InvoiceCustomer emp in coll)
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
                return InvoiceCustomerMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceCustomerQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new InvoiceCustomer(row);
        }

        override protected esEntity CreateEntity()
        {
            return new InvoiceCustomer();
        }

        #endregion

        [BrowsableAttribute(false)]
        public InvoiceCustomerQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceCustomerQuery();
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
        public bool Load(InvoiceCustomerQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public InvoiceCustomer AddNew()
        {
            InvoiceCustomer entity = base.AddNewEntity() as InvoiceCustomer;

            return entity;
        }
        public InvoiceCustomer FindByPrimaryKey(String invoiceNo)
        {
            return base.FindByPrimaryKey(invoiceNo) as InvoiceCustomer;
        }

        #region IEnumerable< InvoiceCustomer> Members

        IEnumerator<InvoiceCustomer> IEnumerable<InvoiceCustomer>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as InvoiceCustomer;
            }
        }

        #endregion

        private InvoiceCustomerQuery query;
    }


    /// <summary>
    /// Encapsulates the 'InvoiceCustomer' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("InvoiceCustomer ({InvoiceNo})")]
    [Serializable]
    public partial class InvoiceCustomer : esInvoiceCustomer
    {
        public InvoiceCustomer()
        {
        }

        public InvoiceCustomer(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return InvoiceCustomerMetadata.Meta();
            }
        }

        override protected esInvoiceCustomerQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new InvoiceCustomerQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public InvoiceCustomerQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new InvoiceCustomerQuery();
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
        public bool Load(InvoiceCustomerQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private InvoiceCustomerQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class InvoiceCustomerQuery : esInvoiceCustomerQuery
    {
        public InvoiceCustomerQuery()
        {

        }

        public InvoiceCustomerQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "InvoiceCustomerQuery";
        }
    }

    [Serializable]
    public partial class InvoiceCustomerMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected InvoiceCustomerMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRReceivableType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRReceivableType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceDueDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceDueDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceTOP, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceTOP;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.CustomerID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.CustomerID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRInvoicePayment, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRInvoicePayment;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.BankID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.BankID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.BankAccountNo, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.BankAccountNo;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceNotes, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceNotes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.PaymentDate, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.PaymentDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRReceivableStatus, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRReceivableStatus;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.VoucherID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.VoucherID;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.IsApproved, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.ApprovedDate, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.ApprovedDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.ApprovedByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.IsVoid, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.VoidDate, 17, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.VoidDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.VoidByUserID, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.VerifyDate, 19, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.VerifyDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.VerifyByUserID, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.VerifyByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.PaymentByUserID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.PaymentByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.AgingDate, 22, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.AgingDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.IsPaymentApproved, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.IsPaymentApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedDate, 24, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.PaymentApprovedDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.PaymentApprovedByUserID, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.PaymentApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRPaymentType, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRPaymentType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRPaymentMethod, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRPaymentMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRCardProvider, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRCardProvider;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRCardType, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRCardType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.EDCMachineID, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.EDCMachineID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.CardHolderName, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.CardHolderName;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.CardFeeAmount, 32, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.CardFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.SRDiscountReason, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.SRDiscountReason;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.IsInvoicePayment, 34, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.IsInvoicePayment;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.InvoiceReferenceNo, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.InvoiceReferenceNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.Reason, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.Reason;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.TransferDate, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.TransferDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.TransferNumber, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.TransferNumber;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.LastUpdateDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(InvoiceCustomerMetadata.ColumnNames.LastUpdateByUserID, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = InvoiceCustomerMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public InvoiceCustomerMetadata Meta()
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
            public const string SRReceivableType = "SRReceivableType";
            public const string InvoiceDate = "InvoiceDate";
            public const string InvoiceDueDate = "InvoiceDueDate";
            public const string InvoiceTOP = "InvoiceTOP";
            public const string CustomerID = "CustomerID";
            public const string SRInvoicePayment = "SRInvoicePayment";
            public const string BankID = "BankID";
            public const string BankAccountNo = "BankAccountNo";
            public const string InvoiceNotes = "InvoiceNotes";
            public const string PaymentDate = "PaymentDate";
            public const string SRReceivableStatus = "SRReceivableStatus";
            public const string VoucherID = "VoucherID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDate = "VoidDate";
            public const string VoidByUserID = "VoidByUserID";
            public const string VerifyDate = "VerifyDate";
            public const string VerifyByUserID = "VerifyByUserID";
            public const string PaymentByUserID = "PaymentByUserID";
            public const string AgingDate = "AgingDate";
            public const string IsPaymentApproved = "IsPaymentApproved";
            public const string PaymentApprovedDate = "PaymentApprovedDate";
            public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
            public const string SRPaymentType = "SRPaymentType";
            public const string SRPaymentMethod = "SRPaymentMethod";
            public const string SRCardProvider = "SRCardProvider";
            public const string SRCardType = "SRCardType";
            public const string EDCMachineID = "EDCMachineID";
            public const string CardHolderName = "CardHolderName";
            public const string CardFeeAmount = "CardFeeAmount";
            public const string SRDiscountReason = "SRDiscountReason";
            public const string IsInvoicePayment = "IsInvoicePayment";
            public const string InvoiceReferenceNo = "InvoiceReferenceNo";
            public const string Reason = "Reason";
            public const string TransferDate = "TransferDate";
            public const string TransferNumber = "TransferNumber";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string InvoiceNo = "InvoiceNo";
            public const string SRReceivableType = "SRReceivableType";
            public const string InvoiceDate = "InvoiceDate";
            public const string InvoiceDueDate = "InvoiceDueDate";
            public const string InvoiceTOP = "InvoiceTOP";
            public const string CustomerID = "CustomerID";
            public const string SRInvoicePayment = "SRInvoicePayment";
            public const string BankID = "BankID";
            public const string BankAccountNo = "BankAccountNo";
            public const string InvoiceNotes = "InvoiceNotes";
            public const string PaymentDate = "PaymentDate";
            public const string SRReceivableStatus = "SRReceivableStatus";
            public const string VoucherID = "VoucherID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDate = "ApprovedDate";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDate = "VoidDate";
            public const string VoidByUserID = "VoidByUserID";
            public const string VerifyDate = "VerifyDate";
            public const string VerifyByUserID = "VerifyByUserID";
            public const string PaymentByUserID = "PaymentByUserID";
            public const string AgingDate = "AgingDate";
            public const string IsPaymentApproved = "IsPaymentApproved";
            public const string PaymentApprovedDate = "PaymentApprovedDate";
            public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
            public const string SRPaymentType = "SRPaymentType";
            public const string SRPaymentMethod = "SRPaymentMethod";
            public const string SRCardProvider = "SRCardProvider";
            public const string SRCardType = "SRCardType";
            public const string EDCMachineID = "EDCMachineID";
            public const string CardHolderName = "CardHolderName";
            public const string CardFeeAmount = "CardFeeAmount";
            public const string SRDiscountReason = "SRDiscountReason";
            public const string IsInvoicePayment = "IsInvoicePayment";
            public const string InvoiceReferenceNo = "InvoiceReferenceNo";
            public const string Reason = "Reason";
            public const string TransferDate = "TransferDate";
            public const string TransferNumber = "TransferNumber";
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
            lock (typeof(InvoiceCustomerMetadata))
            {
                if (InvoiceCustomerMetadata.mapDelegates == null)
                {
                    InvoiceCustomerMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (InvoiceCustomerMetadata.meta == null)
                {
                    InvoiceCustomerMetadata.meta = new InvoiceCustomerMetadata();
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
                meta.AddTypeMap("SRReceivableType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvoiceDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("InvoiceDueDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("InvoiceTOP", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CustomerID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRInvoicePayment", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InvoiceNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRReceivableStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VoucherID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VerifyDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VerifyByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AgingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsPaymentApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PaymentApprovedDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PaymentApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CardHolderName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInvoicePayment", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransferDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransferNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "InvoiceCustomer";
                meta.Destination = "InvoiceCustomer";
                meta.spInsert = "proc_InvoiceCustomerInsert";
                meta.spUpdate = "proc_InvoiceCustomerUpdate";
                meta.spDelete = "proc_InvoiceCustomerDelete";
                meta.spLoadAll = "proc_InvoiceCustomerLoadAll";
                meta.spLoadByPrimaryKey = "proc_InvoiceCustomerLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private InvoiceCustomerMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
