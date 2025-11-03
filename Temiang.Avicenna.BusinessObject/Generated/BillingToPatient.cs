/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/19/2013 2:29:17 PM
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
    abstract public class esBillingToPatientCollection : esEntityCollectionWAuditLog
    {
        public esBillingToPatientCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "BillingToPatientCollection";
        }

        #region Query Logic
        protected void InitQuery(esBillingToPatientQuery query)
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
            this.InitQuery(query as esBillingToPatientQuery);
        }
        #endregion

        virtual public BillingToPatient DetachEntity(BillingToPatient entity)
        {
            return base.DetachEntity(entity) as BillingToPatient;
        }

        virtual public BillingToPatient AttachEntity(BillingToPatient entity)
        {
            return base.AttachEntity(entity) as BillingToPatient;
        }

        virtual public void Combine(BillingToPatientCollection collection)
        {
            base.Combine(collection);
        }

        new public BillingToPatient this[int index]
        {
            get
            {
                return base[index] as BillingToPatient;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BillingToPatient);
        }
    }



    [Serializable]
    abstract public class esBillingToPatient : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBillingToPatientQuery GetDynamicQuery()
        {
            return null;
        }

        public esBillingToPatient()
        {

        }

        public esBillingToPatient(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String billingNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(billingNo);
            else
                return LoadByPrimaryKeyStoredProcedure(billingNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String billingNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(billingNo);
            else
                return LoadByPrimaryKeyStoredProcedure(billingNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String billingNo)
        {
            esBillingToPatientQuery query = this.GetDynamicQuery();
            query.Where(query.BillingNo == billingNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String billingNo)
        {
            esParameters parms = new esParameters();
            parms.Add("BillingNo", billingNo);
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
                        case "BillingNo": this.str.BillingNo = (string)value; break;
                        case "BillingCreatedDateTime": this.str.BillingCreatedDateTime = (string)value; break;
                        case "BillingCreatedByUserID": this.str.BillingCreatedByUserID = (string)value; break;
                        case "BillingDate": this.str.BillingDate = (string)value; break;
                        case "SRBillingType": this.str.SRBillingType = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "RoomID": this.str.RoomID = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "TransactionAmount": this.str.TransactionAmount = (string)value; break;
                        case "DownPaymentAmount": this.str.DownPaymentAmount = (string)value; break;
                        case "PlafondAmount": this.str.PlafondAmount = (string)value; break;
                        case "RemainingAmount": this.str.RemainingAmount = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
                        case "PaymentDate": this.str.PaymentDate = (string)value; break;
                        case "PaymentByUserID": this.str.PaymentByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "BillingCreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.BillingCreatedDateTime = (System.DateTime?)value;
                            break;

                        case "BillingDate":

                            if (value == null || value is System.DateTime)
                                this.BillingDate = (System.DateTime?)value;
                            break;

                        case "StartDate":

                            if (value == null || value is System.DateTime)
                                this.StartDate = (System.DateTime?)value;
                            break;

                        case "EndDate":

                            if (value == null || value is System.DateTime)
                                this.EndDate = (System.DateTime?)value;
                            break;

                        case "TransactionAmount":

                            if (value == null || value is System.Decimal)
                                this.TransactionAmount = (System.Decimal?)value;
                            break;

                        case "DownPaymentAmount":

                            if (value == null || value is System.Decimal)
                                this.DownPaymentAmount = (System.Decimal?)value;
                            break;

                        case "PlafondAmount":

                            if (value == null || value is System.Decimal)
                                this.PlafondAmount = (System.Decimal?)value;
                            break;

                        case "RemainingAmount":

                            if (value == null || value is System.Decimal)
                                this.RemainingAmount = (System.Decimal?)value;
                            break;

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;

                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
                            break;

                        case "PaymentAmount":

                            if (value == null || value is System.Decimal)
                                this.PaymentAmount = (System.Decimal?)value;
                            break;

                        case "PaymentDate":

                            if (value == null || value is System.DateTime)
                                this.PaymentDate = (System.DateTime?)value;
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
        /// Maps to BillingToPatient.BillingNo
        /// </summary>
        virtual public System.String BillingNo
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.BillingNo);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.BillingNo, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.BillingCreatedDateTime
        /// </summary>
        virtual public System.DateTime? BillingCreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.BillingCreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.BillingCreatedDateTime, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.BillingCreatedByUserID
        /// </summary>
        virtual public System.String BillingCreatedByUserID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.BillingCreatedByUserID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.BillingCreatedByUserID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.BillingDate
        /// </summary>
        virtual public System.DateTime? BillingDate
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.BillingDate);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.BillingDate, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.SRBillingType
        /// </summary>
        virtual public System.String SRBillingType
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.SRBillingType);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.SRBillingType, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.RoomID
        /// </summary>
        virtual public System.String RoomID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.RoomID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.RoomID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.BedID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.ChargeClassID
        /// </summary>
        virtual public System.String ChargeClassID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.ChargeClassID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.ChargeClassID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.StartDate, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.EndDate, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.TransactionAmount
        /// </summary>
        virtual public System.Decimal? TransactionAmount
        {
            get
            {
                return base.GetSystemDecimal(BillingToPatientMetadata.ColumnNames.TransactionAmount);
            }

            set
            {
                base.SetSystemDecimal(BillingToPatientMetadata.ColumnNames.TransactionAmount, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.DownPaymentAmount
        /// </summary>
        virtual public System.Decimal? DownPaymentAmount
        {
            get
            {
                return base.GetSystemDecimal(BillingToPatientMetadata.ColumnNames.DownPaymentAmount);
            }

            set
            {
                base.SetSystemDecimal(BillingToPatientMetadata.ColumnNames.DownPaymentAmount, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.PlafondAmount
        /// </summary>
        virtual public System.Decimal? PlafondAmount
        {
            get
            {
                return base.GetSystemDecimal(BillingToPatientMetadata.ColumnNames.PlafondAmount);
            }

            set
            {
                base.SetSystemDecimal(BillingToPatientMetadata.ColumnNames.PlafondAmount, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.RemainingAmount
        /// </summary>
        virtual public System.Decimal? RemainingAmount
        {
            get
            {
                return base.GetSystemDecimal(BillingToPatientMetadata.ColumnNames.RemainingAmount);
            }

            set
            {
                base.SetSystemDecimal(BillingToPatientMetadata.ColumnNames.RemainingAmount, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(BillingToPatientMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(BillingToPatientMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(BillingToPatientMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(BillingToPatientMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.VoidDateTime, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.VoidByUserID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.PaymentAmount
        /// </summary>
        virtual public System.Decimal? PaymentAmount
        {
            get
            {
                return base.GetSystemDecimal(BillingToPatientMetadata.ColumnNames.PaymentAmount);
            }

            set
            {
                base.SetSystemDecimal(BillingToPatientMetadata.ColumnNames.PaymentAmount, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.PaymentDate
        /// </summary>
        virtual public System.DateTime? PaymentDate
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.PaymentDate);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.PaymentDate, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.PaymentByUserID
        /// </summary>
        virtual public System.String PaymentByUserID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.PaymentByUserID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.PaymentByUserID, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BillingToPatientMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BillingToPatientMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to BillingToPatient.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BillingToPatientMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BillingToPatientMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBillingToPatient entity)
            {
                this.entity = entity;
            }


            public System.String BillingNo
            {
                get
                {
                    System.String data = entity.BillingNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BillingNo = null;
                    else entity.BillingNo = Convert.ToString(value);
                }
            }

            public System.String BillingCreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.BillingCreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BillingCreatedDateTime = null;
                    else entity.BillingCreatedDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String BillingCreatedByUserID
            {
                get
                {
                    System.String data = entity.BillingCreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BillingCreatedByUserID = null;
                    else entity.BillingCreatedByUserID = Convert.ToString(value);
                }
            }

            public System.String BillingDate
            {
                get
                {
                    System.DateTime? data = entity.BillingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BillingDate = null;
                    else entity.BillingDate = Convert.ToDateTime(value);
                }
            }

            public System.String SRBillingType
            {
                get
                {
                    System.String data = entity.SRBillingType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBillingType = null;
                    else entity.SRBillingType = Convert.ToString(value);
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

            public System.String RoomID
            {
                get
                {
                    System.String data = entity.RoomID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomID = null;
                    else entity.RoomID = Convert.ToString(value);
                }
            }

            public System.String BedID
            {
                get
                {
                    System.String data = entity.BedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedID = null;
                    else entity.BedID = Convert.ToString(value);
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

            public System.String ChargeClassID
            {
                get
                {
                    System.String data = entity.ChargeClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChargeClassID = null;
                    else entity.ChargeClassID = Convert.ToString(value);
                }
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

            public System.String TransactionAmount
            {
                get
                {
                    System.Decimal? data = entity.TransactionAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionAmount = null;
                    else entity.TransactionAmount = Convert.ToDecimal(value);
                }
            }

            public System.String DownPaymentAmount
            {
                get
                {
                    System.Decimal? data = entity.DownPaymentAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DownPaymentAmount = null;
                    else entity.DownPaymentAmount = Convert.ToDecimal(value);
                }
            }

            public System.String PlafondAmount
            {
                get
                {
                    System.Decimal? data = entity.PlafondAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PlafondAmount = null;
                    else entity.PlafondAmount = Convert.ToDecimal(value);
                }
            }

            public System.String RemainingAmount
            {
                get
                {
                    System.Decimal? data = entity.RemainingAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RemainingAmount = null;
                    else entity.RemainingAmount = Convert.ToDecimal(value);
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

            public System.String ApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
                    else entity.ApprovedDateTime = Convert.ToDateTime(value);
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

            public System.String VoidDateTime
            {
                get
                {
                    System.DateTime? data = entity.VoidDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDateTime = null;
                    else entity.VoidDateTime = Convert.ToDateTime(value);
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


            private esBillingToPatient entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBillingToPatientQuery query)
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
                throw new Exception("esBillingToPatient can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class BillingToPatient : esBillingToPatient
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
    abstract public class esBillingToPatientQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return BillingToPatientMetadata.Meta();
            }
        }


        public esQueryItem BillingNo
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.BillingNo, esSystemType.String);
            }
        }

        public esQueryItem BillingCreatedDateTime
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.BillingCreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem BillingCreatedByUserID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.BillingCreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem BillingDate
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.BillingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRBillingType
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.SRBillingType, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem RoomID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.RoomID, esSystemType.String);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem ChargeClassID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.ChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem TransactionAmount
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.TransactionAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DownPaymentAmount
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.DownPaymentAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem PlafondAmount
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.PlafondAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem RemainingAmount
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.RemainingAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem PaymentAmount
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem PaymentDate
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PaymentByUserID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.PaymentByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BillingToPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BillingToPatientCollection")]
    public partial class BillingToPatientCollection : esBillingToPatientCollection, IEnumerable<BillingToPatient>
    {
        public BillingToPatientCollection()
        {

        }

        public static implicit operator List<BillingToPatient>(BillingToPatientCollection coll)
        {
            List<BillingToPatient> list = new List<BillingToPatient>();

            foreach (BillingToPatient emp in coll)
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
                return BillingToPatientMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BillingToPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BillingToPatient(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BillingToPatient();
        }


        #endregion


        [BrowsableAttribute(false)]
        public BillingToPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BillingToPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(BillingToPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public BillingToPatient AddNew()
        {
            BillingToPatient entity = base.AddNewEntity() as BillingToPatient;

            return entity;
        }

        public BillingToPatient FindByPrimaryKey(System.String billingNo)
        {
            return base.FindByPrimaryKey(billingNo) as BillingToPatient;
        }


        #region IEnumerable<BillingToPatient> Members

        IEnumerator<BillingToPatient> IEnumerable<BillingToPatient>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BillingToPatient;
            }
        }

        #endregion

        private BillingToPatientQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BillingToPatient' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("BillingToPatient ({BillingNo})")]
    [Serializable]
    public partial class BillingToPatient : esBillingToPatient
    {
        public BillingToPatient()
        {

        }

        public BillingToPatient(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BillingToPatientMetadata.Meta();
            }
        }



        override protected esBillingToPatientQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BillingToPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public BillingToPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BillingToPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(BillingToPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BillingToPatientQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class BillingToPatientQuery : esBillingToPatientQuery
    {
        public BillingToPatientQuery()
        {

        }

        public BillingToPatientQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BillingToPatientQuery";
        }


    }


    [Serializable]
    public partial class BillingToPatientMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BillingToPatientMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.BillingNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.BillingNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.BillingCreatedDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.BillingCreatedDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.BillingCreatedByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.BillingCreatedByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.BillingDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.BillingDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.SRBillingType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.SRBillingType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.RoomID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.RoomID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.BedID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.ClassID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.ChargeClassID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.ChargeClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.StartDate, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.StartDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.EndDate, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.EndDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.TransactionAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.TransactionAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.DownPaymentAmount, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.DownPaymentAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.PlafondAmount, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.PlafondAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.RemainingAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.RemainingAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.Notes, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.IsApproved, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.ApprovedDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.ApprovedByUserID, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.IsVoid, 21, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.VoidDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.VoidByUserID, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.PaymentAmount, 24, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.PaymentAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.PaymentDate, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.PaymentDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.PaymentByUserID, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.PaymentByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.LastUpdateDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BillingToPatientMetadata.ColumnNames.LastUpdateByUserID, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = BillingToPatientMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public BillingToPatientMetadata Meta()
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
            public const string BillingNo = "BillingNo";
            public const string BillingCreatedDateTime = "BillingCreatedDateTime";
            public const string BillingCreatedByUserID = "BillingCreatedByUserID";
            public const string BillingDate = "BillingDate";
            public const string SRBillingType = "SRBillingType";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string ClassID = "ClassID";
            public const string ChargeClassID = "ChargeClassID";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string TransactionAmount = "TransactionAmount";
            public const string DownPaymentAmount = "DownPaymentAmount";
            public const string PlafondAmount = "PlafondAmount";
            public const string RemainingAmount = "RemainingAmount";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string PaymentAmount = "PaymentAmount";
            public const string PaymentDate = "PaymentDate";
            public const string PaymentByUserID = "PaymentByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string BillingNo = "BillingNo";
            public const string BillingCreatedDateTime = "BillingCreatedDateTime";
            public const string BillingCreatedByUserID = "BillingCreatedByUserID";
            public const string BillingDate = "BillingDate";
            public const string SRBillingType = "SRBillingType";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string ClassID = "ClassID";
            public const string ChargeClassID = "ChargeClassID";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string TransactionAmount = "TransactionAmount";
            public const string DownPaymentAmount = "DownPaymentAmount";
            public const string PlafondAmount = "PlafondAmount";
            public const string RemainingAmount = "RemainingAmount";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string PaymentAmount = "PaymentAmount";
            public const string PaymentDate = "PaymentDate";
            public const string PaymentByUserID = "PaymentByUserID";
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
            lock (typeof(BillingToPatientMetadata))
            {
                if (BillingToPatientMetadata.mapDelegates == null)
                {
                    BillingToPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BillingToPatientMetadata.meta == null)
                {
                    BillingToPatientMetadata.meta = new BillingToPatientMetadata();
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


                meta.AddTypeMap("BillingNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BillingCreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("BillingCreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BillingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRBillingType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransactionAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DownPaymentAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PlafondAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RemainingAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PaymentAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PaymentByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "BillingToPatient";
                meta.Destination = "BillingToPatient";

                meta.spInsert = "proc_BillingToPatientInsert";
                meta.spUpdate = "proc_BillingToPatientUpdate";
                meta.spDelete = "proc_BillingToPatientDelete";
                meta.spLoadAll = "proc_BillingToPatientLoadAll";
                meta.spLoadByPrimaryKey = "proc_BillingToPatientLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BillingToPatientMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
