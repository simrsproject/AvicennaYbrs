/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/15/2013 10:16:57 AM
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
    abstract public class esAskesCovered2Collection : esEntityCollectionWAuditLog
    {
        public esAskesCovered2Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AskesCovered2Collection";
        }

        #region Query Logic
        protected void InitQuery(esAskesCovered2Query query)
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
            this.InitQuery(query as esAskesCovered2Query);
        }
        #endregion

        virtual public AskesCovered2 DetachEntity(AskesCovered2 entity)
        {
            return base.DetachEntity(entity) as AskesCovered2;
        }

        virtual public AskesCovered2 AttachEntity(AskesCovered2 entity)
        {
            return base.AttachEntity(entity) as AskesCovered2;
        }

        virtual public void Combine(AskesCovered2Collection collection)
        {
            base.Combine(collection);
        }

        new public AskesCovered2 this[int index]
        {
            get
            {
                return base[index] as AskesCovered2;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AskesCovered2);
        }
    }



    [Serializable]
    abstract public class esAskesCovered2 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAskesCovered2Query GetDynamicQuery()
        {
            return null;
        }

        public esAskesCovered2()
        {

        }

        public esAskesCovered2(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String seqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String seqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String seqNo)
        {
            esAskesCovered2Query query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String seqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("SeqNo", seqNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "StartDate": this.str.StartDate = (string)value; break;
                        case "EndDate": this.str.EndDate = (string)value; break;
                        case "RoomAmount": this.str.RoomAmount = (string)value; break;
                        case "RoomDays": this.str.RoomDays = (string)value; break;
                        case "IccuAmount": this.str.IccuAmount = (string)value; break;
                        case "IccuDays": this.str.IccuDays = (string)value; break;
                        case "HcuAmount": this.str.HcuAmount = (string)value; break;
                        case "HcuDays": this.str.HcuDays = (string)value; break;
                        case "SurgeryAmount": this.str.SurgeryAmount = (string)value; break;
                        case "MedicalSupportAmount": this.str.MedicalSupportAmount = (string)value; break;
                        case "HaemodialiseAmount": this.str.HaemodialiseAmount = (string)value; break;
                        case "CtScanAmount": this.str.CtScanAmount = (string)value; break;
                        case "IsLock": this.str.IsLock = (string)value; break;
                        case "IsPaid": this.str.IsPaid = (string)value; break;
                        case "PaymentNo": this.str.PaymentNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
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

                        case "RoomAmount":

                            if (value == null || value is System.Decimal)
                                this.RoomAmount = (System.Decimal?)value;
                            break;

                        case "RoomDays":

                            if (value == null || value is System.Int32)
                                this.RoomDays = (System.Int32?)value;
                            break;

                        case "IccuAmount":

                            if (value == null || value is System.Decimal)
                                this.IccuAmount = (System.Decimal?)value;
                            break;

                        case "IccuDays":

                            if (value == null || value is System.Int32)
                                this.IccuDays = (System.Int32?)value;
                            break;

                        case "HcuAmount":

                            if (value == null || value is System.Decimal)
                                this.HcuAmount = (System.Decimal?)value;
                            break;

                        case "HcuDays":

                            if (value == null || value is System.Int32)
                                this.HcuDays = (System.Int32?)value;
                            break;

                        case "SurgeryAmount":

                            if (value == null || value is System.Decimal)
                                this.SurgeryAmount = (System.Decimal?)value;
                            break;

                        case "MedicalSupportAmount":

                            if (value == null || value is System.Decimal)
                                this.MedicalSupportAmount = (System.Decimal?)value;
                            break;

                        case "HaemodialiseAmount":

                            if (value == null || value is System.Decimal)
                                this.HaemodialiseAmount = (System.Decimal?)value;
                            break;

                        case "CtScanAmount":

                            if (value == null || value is System.Decimal)
                                this.CtScanAmount = (System.Decimal?)value;
                            break;

                        case "IsLock":

                            if (value == null || value is System.Boolean)
                                this.IsLock = (System.Boolean?)value;
                            break;

                        case "IsPaid":

                            if (value == null || value is System.Boolean)
                                this.IsPaid = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;

                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
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
        /// Maps to AskesCovered2.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(AskesCovered2Metadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(AskesCovered2Metadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(AskesCovered2Metadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(AskesCovered2Metadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.StartDate
        /// </summary>
        virtual public System.DateTime? StartDate
        {
            get
            {
                return base.GetSystemDateTime(AskesCovered2Metadata.ColumnNames.StartDate);
            }

            set
            {
                base.SetSystemDateTime(AskesCovered2Metadata.ColumnNames.StartDate, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.EndDate
        /// </summary>
        virtual public System.DateTime? EndDate
        {
            get
            {
                return base.GetSystemDateTime(AskesCovered2Metadata.ColumnNames.EndDate);
            }

            set
            {
                base.SetSystemDateTime(AskesCovered2Metadata.ColumnNames.EndDate, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.RoomAmount
        /// </summary>
        virtual public System.Decimal? RoomAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.RoomAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.RoomAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.RoomDays
        /// </summary>
        virtual public System.Int32? RoomDays
        {
            get
            {
                return base.GetSystemInt32(AskesCovered2Metadata.ColumnNames.RoomDays);
            }

            set
            {
                base.SetSystemInt32(AskesCovered2Metadata.ColumnNames.RoomDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.IccuAmount
        /// </summary>
        virtual public System.Decimal? IccuAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.IccuAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.IccuAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.IccuDays
        /// </summary>
        virtual public System.Int32? IccuDays
        {
            get
            {
                return base.GetSystemInt32(AskesCovered2Metadata.ColumnNames.IccuDays);
            }

            set
            {
                base.SetSystemInt32(AskesCovered2Metadata.ColumnNames.IccuDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.HcuAmount
        /// </summary>
        virtual public System.Decimal? HcuAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.HcuAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.HcuAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.HcuDays
        /// </summary>
        virtual public System.Int32? HcuDays
        {
            get
            {
                return base.GetSystemInt32(AskesCovered2Metadata.ColumnNames.HcuDays);
            }

            set
            {
                base.SetSystemInt32(AskesCovered2Metadata.ColumnNames.HcuDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.SurgeryAmount
        /// </summary>
        virtual public System.Decimal? SurgeryAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.SurgeryAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.SurgeryAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.MedicalSupportAmount
        /// </summary>
        virtual public System.Decimal? MedicalSupportAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.MedicalSupportAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.MedicalSupportAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.HaemodialiseAmount
        /// </summary>
        virtual public System.Decimal? HaemodialiseAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.HaemodialiseAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.HaemodialiseAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.CtScanAmount
        /// </summary>
        virtual public System.Decimal? CtScanAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCovered2Metadata.ColumnNames.CtScanAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCovered2Metadata.ColumnNames.CtScanAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.IsLock
        /// </summary>
        virtual public System.Boolean? IsLock
        {
            get
            {
                return base.GetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsLock);
            }

            set
            {
                base.SetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsLock, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.IsPaid
        /// </summary>
        virtual public System.Boolean? IsPaid
        {
            get
            {
                return base.GetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsPaid);
            }

            set
            {
                base.SetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsPaid, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.PaymentNo
        /// </summary>
        virtual public System.String PaymentNo
        {
            get
            {
                return base.GetSystemString(AskesCovered2Metadata.ColumnNames.PaymentNo);
            }

            set
            {
                base.SetSystemString(AskesCovered2Metadata.ColumnNames.PaymentNo, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AskesCovered2Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AskesCovered2Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AskesCovered2Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AskesCovered2Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(AskesCovered2Metadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(AskesCovered2Metadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(AskesCovered2Metadata.ColumnNames.VoidDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered2.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(AskesCovered2Metadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(AskesCovered2Metadata.ColumnNames.VoidByUserID, value);
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
            public esStrings(esAskesCovered2 entity)
            {
                this.entity = entity;
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

            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
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

            public System.String RoomAmount
            {
                get
                {
                    System.Decimal? data = entity.RoomAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomAmount = null;
                    else entity.RoomAmount = Convert.ToDecimal(value);
                }
            }

            public System.String RoomDays
            {
                get
                {
                    System.Int32? data = entity.RoomDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomDays = null;
                    else entity.RoomDays = Convert.ToInt32(value);
                }
            }

            public System.String IccuAmount
            {
                get
                {
                    System.Decimal? data = entity.IccuAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IccuAmount = null;
                    else entity.IccuAmount = Convert.ToDecimal(value);
                }
            }

            public System.String IccuDays
            {
                get
                {
                    System.Int32? data = entity.IccuDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IccuDays = null;
                    else entity.IccuDays = Convert.ToInt32(value);
                }
            }

            public System.String HcuAmount
            {
                get
                {
                    System.Decimal? data = entity.HcuAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HcuAmount = null;
                    else entity.HcuAmount = Convert.ToDecimal(value);
                }
            }

            public System.String HcuDays
            {
                get
                {
                    System.Int32? data = entity.HcuDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HcuDays = null;
                    else entity.HcuDays = Convert.ToInt32(value);
                }
            }

            public System.String SurgeryAmount
            {
                get
                {
                    System.Decimal? data = entity.SurgeryAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SurgeryAmount = null;
                    else entity.SurgeryAmount = Convert.ToDecimal(value);
                }
            }

            public System.String MedicalSupportAmount
            {
                get
                {
                    System.Decimal? data = entity.MedicalSupportAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalSupportAmount = null;
                    else entity.MedicalSupportAmount = Convert.ToDecimal(value);
                }
            }

            public System.String HaemodialiseAmount
            {
                get
                {
                    System.Decimal? data = entity.HaemodialiseAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HaemodialiseAmount = null;
                    else entity.HaemodialiseAmount = Convert.ToDecimal(value);
                }
            }

            public System.String CtScanAmount
            {
                get
                {
                    System.Decimal? data = entity.CtScanAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CtScanAmount = null;
                    else entity.CtScanAmount = Convert.ToDecimal(value);
                }
            }

            public System.String IsLock
            {
                get
                {
                    System.Boolean? data = entity.IsLock;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsLock = null;
                    else entity.IsLock = Convert.ToBoolean(value);
                }
            }

            public System.String IsPaid
            {
                get
                {
                    System.Boolean? data = entity.IsPaid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPaid = null;
                    else entity.IsPaid = Convert.ToBoolean(value);
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


            private esAskesCovered2 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAskesCovered2Query query)
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
                throw new Exception("esAskesCovered2 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class AskesCovered2 : esAskesCovered2
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
    abstract public class esAskesCovered2Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AskesCovered2Metadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem StartDate
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.StartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EndDate
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.EndDate, esSystemType.DateTime);
            }
        }

        public esQueryItem RoomAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.RoomAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem RoomDays
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.RoomDays, esSystemType.Int32);
            }
        }

        public esQueryItem IccuAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.IccuAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem IccuDays
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.IccuDays, esSystemType.Int32);
            }
        }

        public esQueryItem HcuAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.HcuAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem HcuDays
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.HcuDays, esSystemType.Int32);
            }
        }

        public esQueryItem SurgeryAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.SurgeryAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem MedicalSupportAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.MedicalSupportAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem HaemodialiseAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.HaemodialiseAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem CtScanAmount
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.CtScanAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem IsLock
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.IsLock, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPaid
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.IsPaid, esSystemType.Boolean);
            }
        }

        public esQueryItem PaymentNo
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.PaymentNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, AskesCovered2Metadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AskesCovered2Collection")]
    public partial class AskesCovered2Collection : esAskesCovered2Collection, IEnumerable<AskesCovered2>
    {
        public AskesCovered2Collection()
        {

        }

        public static implicit operator List<AskesCovered2>(AskesCovered2Collection coll)
        {
            List<AskesCovered2> list = new List<AskesCovered2>();

            foreach (AskesCovered2 emp in coll)
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
                return AskesCovered2Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AskesCovered2Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AskesCovered2(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AskesCovered2();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AskesCovered2Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AskesCovered2Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AskesCovered2Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AskesCovered2 AddNew()
        {
            AskesCovered2 entity = base.AddNewEntity() as AskesCovered2;

            return entity;
        }

        public AskesCovered2 FindByPrimaryKey(System.String registrationNo, System.String seqNo)
        {
            return base.FindByPrimaryKey(registrationNo, seqNo) as AskesCovered2;
        }


        #region IEnumerable<AskesCovered2> Members

        IEnumerator<AskesCovered2> IEnumerable<AskesCovered2>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AskesCovered2;
            }
        }

        #endregion

        private AskesCovered2Query query;
    }


    /// <summary>
    /// Encapsulates the 'AskesCovered2' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AskesCovered2 ({RegistrationNo},{SeqNo})")]
    [Serializable]
    public partial class AskesCovered2 : esAskesCovered2
    {
        public AskesCovered2()
        {

        }

        public AskesCovered2(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AskesCovered2Metadata.Meta();
            }
        }



        override protected esAskesCovered2Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AskesCovered2Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AskesCovered2Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AskesCovered2Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AskesCovered2Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AskesCovered2Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AskesCovered2Query : esAskesCovered2Query
    {
        public AskesCovered2Query()
        {

        }

        public AskesCovered2Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AskesCovered2Query";
        }


    }


    [Serializable]
    public partial class AskesCovered2Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AskesCovered2Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.StartDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.StartDate;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.EndDate, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.EndDate;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.RoomAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.RoomAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.RoomDays, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.RoomDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.IccuAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.IccuAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.IccuDays, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.IccuDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.HcuAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.HcuAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.HcuDays, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.HcuDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.SurgeryAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.SurgeryAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.MedicalSupportAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.MedicalSupportAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.HaemodialiseAmount, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.HaemodialiseAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.CtScanAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.CtScanAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.IsLock, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.IsLock;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.IsPaid, 15, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.IsPaid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.PaymentNo, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.PaymentNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.IsVoid, 19, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.VoidDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCovered2Metadata.ColumnNames.VoidByUserID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCovered2Metadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AskesCovered2Metadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string SeqNo = "SeqNo";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string RoomAmount = "RoomAmount";
            public const string RoomDays = "RoomDays";
            public const string IccuAmount = "IccuAmount";
            public const string IccuDays = "IccuDays";
            public const string HcuAmount = "HcuAmount";
            public const string HcuDays = "HcuDays";
            public const string SurgeryAmount = "SurgeryAmount";
            public const string MedicalSupportAmount = "MedicalSupportAmount";
            public const string HaemodialiseAmount = "HaemodialiseAmount";
            public const string CtScanAmount = "CtScanAmount";
            public const string IsLock = "IsLock";
            public const string IsPaid = "IsPaid";
            public const string PaymentNo = "PaymentNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SeqNo = "SeqNo";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string RoomAmount = "RoomAmount";
            public const string RoomDays = "RoomDays";
            public const string IccuAmount = "IccuAmount";
            public const string IccuDays = "IccuDays";
            public const string HcuAmount = "HcuAmount";
            public const string HcuDays = "HcuDays";
            public const string SurgeryAmount = "SurgeryAmount";
            public const string MedicalSupportAmount = "MedicalSupportAmount";
            public const string HaemodialiseAmount = "HaemodialiseAmount";
            public const string CtScanAmount = "CtScanAmount";
            public const string IsLock = "IsLock";
            public const string IsPaid = "IsPaid";
            public const string PaymentNo = "PaymentNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(AskesCovered2Metadata))
            {
                if (AskesCovered2Metadata.mapDelegates == null)
                {
                    AskesCovered2Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AskesCovered2Metadata.meta == null)
                {
                    AskesCovered2Metadata.meta = new AskesCovered2Metadata();
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


                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RoomAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RoomDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IccuAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IccuDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HcuAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HcuDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SurgeryAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("MedicalSupportAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HaemodialiseAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CtScanAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsLock", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPaid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AskesCovered2";
                meta.Destination = "AskesCovered2";

                meta.spInsert = "proc_AskesCovered2Insert";
                meta.spUpdate = "proc_AskesCovered2Update";
                meta.spDelete = "proc_AskesCovered2Delete";
                meta.spLoadAll = "proc_AskesCovered2LoadAll";
                meta.spLoadByPrimaryKey = "proc_AskesCovered2LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AskesCovered2Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
