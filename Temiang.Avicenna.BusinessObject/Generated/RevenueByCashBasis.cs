/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/3/2015 3:49:25 PM
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
    abstract public class esRevenueByCashBasisCollection : esEntityCollectionWAuditLog
    {
        public esRevenueByCashBasisCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RevenueByCashBasisCollection";
        }

        #region Query Logic
        protected void InitQuery(esRevenueByCashBasisQuery query)
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
            this.InitQuery(query as esRevenueByCashBasisQuery);
        }
        #endregion

        virtual public RevenueByCashBasis DetachEntity(RevenueByCashBasis entity)
        {
            return base.DetachEntity(entity) as RevenueByCashBasis;
        }

        virtual public RevenueByCashBasis AttachEntity(RevenueByCashBasis entity)
        {
            return base.AttachEntity(entity) as RevenueByCashBasis;
        }

        virtual public void Combine(RevenueByCashBasisCollection collection)
        {
            base.Combine(collection);
        }

        new public RevenueByCashBasis this[int index]
        {
            get
            {
                return base[index] as RevenueByCashBasis;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RevenueByCashBasis);
        }
    }



    [Serializable]
    abstract public class esRevenueByCashBasis : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRevenueByCashBasisQuery GetDynamicQuery()
        {
            return null;
        }

        public esRevenueByCashBasis()
        {

        }

        public esRevenueByCashBasis(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.DateTime endDate, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.DateTime startDate, System.String tariffComponentName, System.String transactionNo, System.String txType, System.String userID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(endDate, paymentNo, paymentReferenceNo, sequenceNo, startDate, tariffComponentName, transactionNo, txType, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(endDate, paymentNo, paymentReferenceNo, sequenceNo, startDate, tariffComponentName, transactionNo, txType, userID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime endDate, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.DateTime startDate, System.String tariffComponentName, System.String transactionNo, System.String txType, System.String userID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(endDate, paymentNo, paymentReferenceNo, sequenceNo, startDate, tariffComponentName, transactionNo, txType, userID);
            else
                return LoadByPrimaryKeyStoredProcedure(endDate, paymentNo, paymentReferenceNo, sequenceNo, startDate, tariffComponentName, transactionNo, txType, userID);
        }

        private bool LoadByPrimaryKeyDynamic(System.DateTime endDate, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.DateTime startDate, System.String tariffComponentName, System.String transactionNo, System.String txType, System.String userID)
        {
            esRevenueByCashBasisQuery query = this.GetDynamicQuery();
            query.Where(query.EndDate == endDate, query.PaymentNo == paymentNo, query.PaymentReferenceNo == paymentReferenceNo, query.SequenceNo == sequenceNo, query.StartDate == startDate, query.TariffComponentName == tariffComponentName, query.TransactionNo == transactionNo, query.TxType == txType, query.UserID == userID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.DateTime endDate, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.DateTime startDate, System.String tariffComponentName, System.String transactionNo, System.String txType, System.String userID)
        {
            esParameters parms = new esParameters();
            parms.Add("EndDate", endDate); parms.Add("PaymentNo", paymentNo); parms.Add("PaymentReferenceNo", paymentReferenceNo); parms.Add("SequenceNo", sequenceNo); parms.Add("StartDate", startDate); parms.Add("TariffComponentName", tariffComponentName); parms.Add("TransactionNo", transactionNo); parms.Add("TxType", txType); parms.Add("UserID", userID);
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
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "UserID": this.str.UserID = (string)value; break;
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                        case "PaymentReferenceNo": this.str.PaymentReferenceNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "TariffComponentName": this.str.TariffComponentName = (string)value; break;
                        case "ItemName": this.str.ItemName = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "Discount": this.str.Discount = (string)value; break;
                        case "PatientAmount": this.str.PatientAmount = (string)value; break;
                        case "GuarantorAmount": this.str.GuarantorAmount = (string)value; break;
                        case "DiscountPatientAmount": this.str.DiscountPatientAmount = (string)value; break;
                        case "DiscountGuarantorAmount": this.str.DiscountGuarantorAmount = (string)value; break;
                        case "TotalIncome": this.str.TotalIncome = (string)value; break;
                        case "ParamedicName": this.str.ParamedicName = (string)value; break;
                        case "TxType": this.str.TxType = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartDate":

                            if (value == null || value is System.DateTime)
                                this.StartDate = (System.DateTime?)value;
                            break;

                        case "EndDate":

                            if (value == null || value is System.DateTime)
                                this.EndDate = (System.DateTime?)value;
                            break;

                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;

                        case "Qty":

                            if (value == null || value is System.Decimal)
                                this.Qty = (System.Decimal?)value;
                            break;

                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;

                        case "Discount":

                            if (value == null || value is System.Decimal)
                                this.Discount = (System.Decimal?)value;
                            break;

                        case "PatientAmount":

                            if (value == null || value is System.Decimal)
                                this.PatientAmount = (System.Decimal?)value;
                            break;

                        case "GuarantorAmount":

                            if (value == null || value is System.Decimal)
                                this.GuarantorAmount = (System.Decimal?)value;
                            break;

                        case "DiscountPatientAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountPatientAmount = (System.Decimal?)value;
                            break;

                        case "DiscountGuarantorAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountGuarantorAmount = (System.Decimal?)value;
                            break;

                        case "TotalIncome":

                            if (value == null || value is System.Decimal)
                                this.TotalIncome = (System.Decimal?)value;
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
        /// Maps to RevenueByCashBasis.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.StartDate, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.EndDate, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.UserID
        /// </summary>
        virtual public System.String UserID
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.UserID);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.UserID, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.PaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.PaymentReferenceNo
        /// </summary>
        virtual public System.String PaymentReferenceNo
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.PaymentReferenceNo);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.PaymentReferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.SRRegistrationType
        /// </summary>
        virtual public System.String SRRegistrationType
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.SRRegistrationType);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.SRRegistrationType, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.PatientID, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(RevenueByCashBasisMetadata.ColumnNames.TransactionDate, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.SequenceNo, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.TariffComponentName
        /// </summary>
        virtual public System.String TariffComponentName
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.TariffComponentName);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.TariffComponentName, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.ItemName
        /// </summary>
        virtual public System.String ItemName
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.ItemName);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.ItemName, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.Qty
        /// </summary>
        virtual public System.Decimal? Qty
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Qty, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Price, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.Discount
        /// </summary>
        virtual public System.Decimal? Discount
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Discount);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.Discount, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.PatientAmount
        /// </summary>
        virtual public System.Decimal? PatientAmount
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.PatientAmount);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.PatientAmount, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.GuarantorAmount
        /// </summary>
        virtual public System.Decimal? GuarantorAmount
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.GuarantorAmount);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.GuarantorAmount, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.DiscountPatientAmount
        /// </summary>
        virtual public System.Decimal? DiscountPatientAmount
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.DiscountPatientAmount);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.DiscountPatientAmount, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.DiscountGuarantorAmount
        /// </summary>
        virtual public System.Decimal? DiscountGuarantorAmount
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.DiscountGuarantorAmount);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.DiscountGuarantorAmount, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.TotalIncome
        /// </summary>
        virtual public System.Decimal? TotalIncome
        {
            get
            {
                return base.GetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.TotalIncome);
            }

            set
            {
                base.SetSystemDecimal(RevenueByCashBasisMetadata.ColumnNames.TotalIncome, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.ParamedicName
        /// </summary>
        virtual public System.String ParamedicName
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.ParamedicName);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.ParamedicName, value);
            }
        }

        /// <summary>
        /// Maps to RevenueByCashBasis.TxType
        /// </summary>
        virtual public System.String TxType
        {
            get
            {
                return base.GetSystemString(RevenueByCashBasisMetadata.ColumnNames.TxType);
            }

            set
            {
                base.SetSystemString(RevenueByCashBasisMetadata.ColumnNames.TxType, value);
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
            public esStrings(esRevenueByCashBasis entity)
            {
                this.entity = entity;
            }


            public System.String StartDate
            {
                get
                {
                    System.DateTime? data = entity.StartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartDate = null;
                    else entity.StartDate = Convert.ToDateTime(value);
                }
            }

            public System.String EndDate
            {
                get
                {
                    System.DateTime? data = entity.EndDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EndDate = null;
                    else entity.EndDate = Convert.ToDateTime(value);
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

            public System.String PaymentNo
            {
                get
                {
                    System.String data = entity.PaymentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentNo = null;
                    else entity.PaymentNo = Convert.ToString(value);
                }
            }

            public System.String PaymentReferenceNo
            {
                get
                {
                    System.String data = entity.PaymentReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentReferenceNo = null;
                    else entity.PaymentReferenceNo = Convert.ToString(value);
                }
            }

            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }

            public System.String SRRegistrationType
            {
                get
                {
                    System.String data = entity.SRRegistrationType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRegistrationType = null;
                    else entity.SRRegistrationType = Convert.ToString(value);
                }
            }

            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }

            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
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

            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }

            public System.String TariffComponentName
            {
                get
                {
                    System.String data = entity.TariffComponentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffComponentName = null;
                    else entity.TariffComponentName = Convert.ToString(value);
                }
            }

            public System.String ItemName
            {
                get
                {
                    System.String data = entity.ItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemName = null;
                    else entity.ItemName = Convert.ToString(value);
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

            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
                }
            }

            public System.String Discount
            {
                get
                {
                    System.Decimal? data = entity.Discount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Discount = null;
                    else entity.Discount = Convert.ToDecimal(value);
                }
            }

            public System.String PatientAmount
            {
                get
                {
                    System.Decimal? data = entity.PatientAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientAmount = null;
                    else entity.PatientAmount = Convert.ToDecimal(value);
                }
            }

            public System.String GuarantorAmount
            {
                get
                {
                    System.Decimal? data = entity.GuarantorAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorAmount = null;
                    else entity.GuarantorAmount = Convert.ToDecimal(value);
                }
            }

            public System.String DiscountPatientAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountPatientAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountPatientAmount = null;
                    else entity.DiscountPatientAmount = Convert.ToDecimal(value);
                }
            }

            public System.String DiscountGuarantorAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountGuarantorAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountGuarantorAmount = null;
                    else entity.DiscountGuarantorAmount = Convert.ToDecimal(value);
                }
            }

            public System.String TotalIncome
            {
                get
                {
                    System.Decimal? data = entity.TotalIncome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalIncome = null;
                    else entity.TotalIncome = Convert.ToDecimal(value);
                }
            }

            public System.String ParamedicName
            {
                get
                {
                    System.String data = entity.ParamedicName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicName = null;
                    else entity.ParamedicName = Convert.ToString(value);
                }
            }

            public System.String TxType
            {
                get
                {
                    System.String data = entity.TxType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TxType = null;
                    else entity.TxType = Convert.ToString(value);
                }
            }


            private esRevenueByCashBasis entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRevenueByCashBasisQuery query)
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
                throw new Exception("esRevenueByCashBasis can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RevenueByCashBasis : esRevenueByCashBasis
    {


        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }

    }



    [Serializable]
    abstract public class esRevenueByCashBasisQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RevenueByCashBasisMetadata.Meta();
            }
        }


        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem UserID
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.UserID, esSystemType.String);
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem PaymentReferenceNo
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.PaymentReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SRRegistrationType
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem TariffComponentName
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.TariffComponentName, esSystemType.String);
            }
        }

        public esQueryItem ItemName
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.ItemName, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.Qty, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem Discount
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.Discount, esSystemType.Decimal);
            }
        }

        public esQueryItem PatientAmount
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem GuarantorAmount
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountPatientAmount
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.DiscountPatientAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountGuarantorAmount
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.DiscountGuarantorAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem TotalIncome
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.TotalIncome, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicName
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.ParamedicName, esSystemType.String);
            }
        }

        public esQueryItem TxType
        {
            get
            {
                return new esQueryItem(this, RevenueByCashBasisMetadata.ColumnNames.TxType, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RevenueByCashBasisCollection")]
    public partial class RevenueByCashBasisCollection : esRevenueByCashBasisCollection, IEnumerable<RevenueByCashBasis>
    {
        public RevenueByCashBasisCollection()
        {

        }

        public static implicit operator List<RevenueByCashBasis>(RevenueByCashBasisCollection coll)
        {
            List<RevenueByCashBasis> list = new List<RevenueByCashBasis>();

            foreach (RevenueByCashBasis emp in coll)
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
                return RevenueByCashBasisMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RevenueByCashBasisQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RevenueByCashBasis(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RevenueByCashBasis();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RevenueByCashBasisQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RevenueByCashBasisQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RevenueByCashBasisQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RevenueByCashBasis AddNew()
        {
            RevenueByCashBasis entity = base.AddNewEntity() as RevenueByCashBasis;

            return entity;
        }

        public RevenueByCashBasis FindByPrimaryKey(System.DateTime endDate, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.DateTime startDate, System.String tariffComponentName, System.String transactionNo, System.String txType, System.String userID)
        {
            return base.FindByPrimaryKey(endDate, paymentNo, paymentReferenceNo, sequenceNo, startDate, tariffComponentName, transactionNo, txType, userID) as RevenueByCashBasis;
        }


        #region IEnumerable<RevenueByCashBasis> Members

        IEnumerator<RevenueByCashBasis> IEnumerable<RevenueByCashBasis>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RevenueByCashBasis;
            }
        }

        #endregion

        private RevenueByCashBasisQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RevenueByCashBasis' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RevenueByCashBasis ({StartDate},{EndDate},{UserID},{PaymentNo},{PaymentReferenceNo},{TransactionNo},{SequenceNo},{TariffComponentName},{TxType})")]
    [Serializable]
    public partial class RevenueByCashBasis : esRevenueByCashBasis
    {
        public RevenueByCashBasis()
        {

        }

        public RevenueByCashBasis(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RevenueByCashBasisMetadata.Meta();
            }
        }



        override protected esRevenueByCashBasisQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RevenueByCashBasisQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RevenueByCashBasisQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RevenueByCashBasisQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RevenueByCashBasisQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RevenueByCashBasisQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RevenueByCashBasisQuery : esRevenueByCashBasisQuery
    {
        public RevenueByCashBasisQuery()
        {

        }

        public RevenueByCashBasisQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RevenueByCashBasisQuery";
        }


    }


    [Serializable]
    public partial class RevenueByCashBasisMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RevenueByCashBasisMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.StartDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.StartDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.EndDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.EndDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.UserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.UserID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.PaymentNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.PaymentNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.PaymentReferenceNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.PaymentReferenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.SRRegistrationType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.SRRegistrationType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.PatientID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.PatientID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.ServiceUnitID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.ClassID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.TransactionDate, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.TransactionDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.TransactionNo, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.SequenceNo, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.TariffComponentName, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.TariffComponentName;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.ItemName, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.ItemName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.Qty, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.Price, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.Discount, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.Discount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.PatientAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.PatientAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.GuarantorAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.GuarantorAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.DiscountPatientAmount, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.DiscountPatientAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.DiscountGuarantorAmount, 21, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.DiscountGuarantorAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.TotalIncome, 22, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.TotalIncome;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.ParamedicName, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.ParamedicName;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RevenueByCashBasisMetadata.ColumnNames.TxType, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = RevenueByCashBasisMetadata.PropertyNames.TxType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 1;
            _columns.Add(c);

        }
        #endregion

        static public RevenueByCashBasisMetadata Meta()
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
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string UserID = "UserID";
            public const string PaymentNo = "PaymentNo";
            public const string PaymentReferenceNo = "PaymentReferenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string PatientID = "PatientID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string TransactionDate = "TransactionDate";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string TariffComponentName = "TariffComponentName";
            public const string ItemName = "ItemName";
            public const string Qty = "Qty";
            public const string Price = "Price";
            public const string Discount = "Discount";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountPatientAmount = "DiscountPatientAmount";
            public const string DiscountGuarantorAmount = "DiscountGuarantorAmount";
            public const string TotalIncome = "TotalIncome";
            public const string ParamedicName = "ParamedicName";
            public const string TxType = "TxType";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string UserID = "UserID";
            public const string PaymentNo = "PaymentNo";
            public const string PaymentReferenceNo = "PaymentReferenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string SRRegistrationType = "SRRegistrationType";
            public const string PatientID = "PatientID";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string TransactionDate = "TransactionDate";
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string TariffComponentName = "TariffComponentName";
            public const string ItemName = "ItemName";
            public const string Qty = "Qty";
            public const string Price = "Price";
            public const string Discount = "Discount";
            public const string PatientAmount = "PatientAmount";
            public const string GuarantorAmount = "GuarantorAmount";
            public const string DiscountPatientAmount = "DiscountPatientAmount";
            public const string DiscountGuarantorAmount = "DiscountGuarantorAmount";
            public const string TotalIncome = "TotalIncome";
            public const string ParamedicName = "ParamedicName";
            public const string TxType = "TxType";
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
            lock (typeof(RevenueByCashBasisMetadata))
            {
                if (RevenueByCashBasisMetadata.mapDelegates == null)
                {
                    RevenueByCashBasisMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RevenueByCashBasisMetadata.meta == null)
                {
                    RevenueByCashBasisMetadata.meta = new RevenueByCashBasisMetadata();
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


                meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TariffComponentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PatientAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountPatientAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountGuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TotalIncome", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TxType", new esTypeMap("char", "System.String"));



                meta.Source = "RevenueByCashBasis";
                meta.Destination = "RevenueByCashBasis";

                meta.spInsert = "proc_RevenueByCashBasisInsert";
                meta.spUpdate = "proc_RevenueByCashBasisUpdate";
                meta.spDelete = "proc_RevenueByCashBasisDelete";
                meta.spLoadAll = "proc_RevenueByCashBasisLoadAll";
                meta.spLoadByPrimaryKey = "proc_RevenueByCashBasisLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RevenueByCashBasisMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
