/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 03/04/2024 22:23:13
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
    abstract public class esMedicalDischargeSummaryPrescHomeCmxCollection : esEntityCollectionWAuditLog
    {
        public esMedicalDischargeSummaryPrescHomeCmxCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalDischargeSummaryPrescHomeCmxCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryPrescHomeCmxQuery query)
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
            this.InitQuery(query as esMedicalDischargeSummaryPrescHomeCmxQuery);
        }
        #endregion

        virtual public MedicalDischargeSummaryPrescHomeCmx DetachEntity(MedicalDischargeSummaryPrescHomeCmx entity)
        {
            return base.DetachEntity(entity) as MedicalDischargeSummaryPrescHomeCmx;
        }

        virtual public MedicalDischargeSummaryPrescHomeCmx AttachEntity(MedicalDischargeSummaryPrescHomeCmx entity)
        {
            return base.AttachEntity(entity) as MedicalDischargeSummaryPrescHomeCmx;
        }

        virtual public void Combine(MedicalDischargeSummaryPrescHomeCmxCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalDischargeSummaryPrescHomeCmx this[int index]
        {
            get
            {
                return base[index] as MedicalDischargeSummaryPrescHomeCmx;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalDischargeSummaryPrescHomeCmx);
        }
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryPrescHomeCmx : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalDischargeSummaryPrescHomeCmxQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalDischargeSummaryPrescHomeCmx()
        {
        }

        public esMedicalDischargeSummaryPrescHomeCmx(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(medicationReceiveNo);
            else
                return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(medicationReceiveNo);
            else
                return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo)
        {
            esMedicalDischargeSummaryPrescHomeCmxQuery query = this.GetDynamicQuery();
            query.Where(query.MedicationReceiveNo == medicationReceiveNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo)
        {
            esParameters parms = new esParameters();
            parms.Add("MedicationReceiveNo", medicationReceiveNo);
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
                        case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ReceiveDateTime": this.str.ReceiveDateTime = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ItemDescription": this.str.ItemDescription = (string)value; break;
                        case "RefTransactionNo": this.str.RefTransactionNo = (string)value; break;
                        case "RefSequenceNo": this.str.RefSequenceNo = (string)value; break;
                        case "RefQty": this.str.RefQty = (string)value; break;
                        case "ReceiveQty": this.str.ReceiveQty = (string)value; break;
                        case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
                        case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
                        case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
                        case "BalanceQty": this.str.BalanceQty = (string)value; break;
                        case "StartDateTime": this.str.StartDateTime = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "IsContinue": this.str.IsContinue = (string)value; break;
                        case "Note": this.str.Note = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsAdmissionAppropriate": this.str.IsAdmissionAppropriate = (string)value; break;
                        case "IsTransferAppropriate": this.str.IsTransferAppropriate = (string)value; break;
                        case "IsDischargeAppropriate": this.str.IsDischargeAppropriate = (string)value; break;
                        case "IsBroughtHome": this.str.IsBroughtHome = (string)value; break;
                        case "BalanceRealQty": this.str.BalanceRealQty = (string)value; break;
                        case "AdmissionAppropriateDateTime": this.str.AdmissionAppropriateDateTime = (string)value; break;
                        case "TransferAppropriateDateTime": this.str.TransferAppropriateDateTime = (string)value; break;
                        case "DischargeAppropriateDateTime": this.str.DischargeAppropriateDateTime = (string)value; break;
                        case "SRMedicationConsume": this.str.SRMedicationConsume = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "RoomID": this.str.RoomID = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "IsAdmissionPresc": this.str.IsAdmissionPresc = (string)value; break;
                        case "IsTransferPresc": this.str.IsTransferPresc = (string)value; break;
                        case "IsDischargePresc": this.str.IsDischargePresc = (string)value; break;
                        case "ConsumeQtyInString": this.str.ConsumeQtyInString = (string)value; break;
                        case "SRMedicationRoute": this.str.SRMedicationRoute = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MedicationReceiveNo":

                            if (value == null || value is System.Int64)
                                this.MedicationReceiveNo = (System.Int64?)value;
                            break;
                        case "ReceiveDateTime":

                            if (value == null || value is System.DateTime)
                                this.ReceiveDateTime = (System.DateTime?)value;
                            break;
                        case "RefQty":

                            if (value == null || value is System.Decimal)
                                this.RefQty = (System.Decimal?)value;
                            break;
                        case "ReceiveQty":

                            if (value == null || value is System.Decimal)
                                this.ReceiveQty = (System.Decimal?)value;
                            break;
                        case "ConsumeQty":

                            if (value == null || value is System.Decimal)
                                this.ConsumeQty = (System.Decimal?)value;
                            break;
                        case "BalanceQty":

                            if (value == null || value is System.Decimal)
                                this.BalanceQty = (System.Decimal?)value;
                            break;
                        case "StartDateTime":

                            if (value == null || value is System.DateTime)
                                this.StartDateTime = (System.DateTime?)value;
                            break;
                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "IsContinue":

                            if (value == null || value is System.Boolean)
                                this.IsContinue = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsAdmissionAppropriate":

                            if (value == null || value is System.Boolean)
                                this.IsAdmissionAppropriate = (System.Boolean?)value;
                            break;
                        case "IsTransferAppropriate":

                            if (value == null || value is System.Boolean)
                                this.IsTransferAppropriate = (System.Boolean?)value;
                            break;
                        case "IsDischargeAppropriate":

                            if (value == null || value is System.Boolean)
                                this.IsDischargeAppropriate = (System.Boolean?)value;
                            break;
                        case "IsBroughtHome":

                            if (value == null || value is System.Boolean)
                                this.IsBroughtHome = (System.Boolean?)value;
                            break;
                        case "BalanceRealQty":

                            if (value == null || value is System.Decimal)
                                this.BalanceRealQty = (System.Decimal?)value;
                            break;
                        case "AdmissionAppropriateDateTime":

                            if (value == null || value is System.DateTime)
                                this.AdmissionAppropriateDateTime = (System.DateTime?)value;
                            break;
                        case "TransferAppropriateDateTime":

                            if (value == null || value is System.DateTime)
                                this.TransferAppropriateDateTime = (System.DateTime?)value;
                            break;
                        case "DischargeAppropriateDateTime":

                            if (value == null || value is System.DateTime)
                                this.DischargeAppropriateDateTime = (System.DateTime?)value;
                            break;
                        case "IsAdmissionPresc":

                            if (value == null || value is System.Boolean)
                                this.IsAdmissionPresc = (System.Boolean?)value;
                            break;
                        case "IsTransferPresc":

                            if (value == null || value is System.Boolean)
                                this.IsTransferPresc = (System.Boolean?)value;
                            break;
                        case "IsDischargePresc":

                            if (value == null || value is System.Boolean)
                                this.IsDischargePresc = (System.Boolean?)value;
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
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.MedicationReceiveNo
        /// </summary>
        virtual public System.Int64? MedicationReceiveNo
        {
            get
            {
                return base.GetSystemInt64(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.MedicationReceiveNo);
            }

            set
            {
                base.SetSystemInt64(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.MedicationReceiveNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ReceiveDateTime
        /// </summary>
        virtual public System.DateTime? ReceiveDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ItemDescription
        /// </summary>
        virtual public System.String ItemDescription
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemDescription);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemDescription, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.RefTransactionNo
        /// </summary>
        virtual public System.String RefTransactionNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefTransactionNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefTransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.RefSequenceNo
        /// </summary>
        virtual public System.String RefSequenceNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefSequenceNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefSequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.RefQty
        /// </summary>
        virtual public System.Decimal? RefQty
        {
            get
            {
                return base.GetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefQty);
            }

            set
            {
                base.SetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefQty, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ReceiveQty
        /// </summary>
        virtual public System.Decimal? ReceiveQty
        {
            get
            {
                return base.GetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveQty);
            }

            set
            {
                base.SetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveQty, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.SRConsumeUnit
        /// </summary>
        virtual public System.String SRConsumeUnit
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeUnit);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeUnit, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ConsumeQty
        /// </summary>
        virtual public System.Decimal? ConsumeQty
        {
            get
            {
                return base.GetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQty);
            }

            set
            {
                base.SetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQty, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.SRConsumeMethod
        /// </summary>
        virtual public System.String SRConsumeMethod
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeMethod);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeMethod, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.BalanceQty
        /// </summary>
        virtual public System.Decimal? BalanceQty
        {
            get
            {
                return base.GetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceQty);
            }

            set
            {
                base.SetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceQty, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.StartDateTime
        /// </summary>
        virtual public System.DateTime? StartDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.StartDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.StartDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsClosed, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsContinue
        /// </summary>
        virtual public System.Boolean? IsContinue
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsContinue);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsContinue, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.Note
        /// </summary>
        virtual public System.String Note
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.Note);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.Note, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsAdmissionAppropriate
        /// </summary>
        virtual public System.Boolean? IsAdmissionAppropriate
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionAppropriate);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionAppropriate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsTransferAppropriate
        /// </summary>
        virtual public System.Boolean? IsTransferAppropriate
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferAppropriate);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferAppropriate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsDischargeAppropriate
        /// </summary>
        virtual public System.Boolean? IsDischargeAppropriate
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargeAppropriate);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargeAppropriate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsBroughtHome
        /// </summary>
        virtual public System.Boolean? IsBroughtHome
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsBroughtHome);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsBroughtHome, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.BalanceRealQty
        /// </summary>
        virtual public System.Decimal? BalanceRealQty
        {
            get
            {
                return base.GetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceRealQty);
            }

            set
            {
                base.SetSystemDecimal(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceRealQty, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.AdmissionAppropriateDateTime
        /// </summary>
        virtual public System.DateTime? AdmissionAppropriateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.AdmissionAppropriateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.AdmissionAppropriateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.TransferAppropriateDateTime
        /// </summary>
        virtual public System.DateTime? TransferAppropriateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.TransferAppropriateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.TransferAppropriateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.DischargeAppropriateDateTime
        /// </summary>
        virtual public System.DateTime? DischargeAppropriateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.DischargeAppropriateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.DischargeAppropriateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.SRMedicationConsume
        /// </summary>
        virtual public System.String SRMedicationConsume
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationConsume);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationConsume, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.RoomID
        /// </summary>
        virtual public System.String RoomID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RoomID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RoomID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BedID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsAdmissionPresc
        /// </summary>
        virtual public System.Boolean? IsAdmissionPresc
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionPresc);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionPresc, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsTransferPresc
        /// </summary>
        virtual public System.Boolean? IsTransferPresc
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferPresc);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferPresc, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.IsDischargePresc
        /// </summary>
        virtual public System.Boolean? IsDischargePresc
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargePresc);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargePresc, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.ConsumeQtyInString
        /// </summary>
        virtual public System.String ConsumeQtyInString
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQtyInString);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQtyInString, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryPrescHomeCmx.SRMedicationRoute
        /// </summary>
        virtual public System.String SRMedicationRoute
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationRoute);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationRoute, value);
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
            public esStrings(esMedicalDischargeSummaryPrescHomeCmx entity)
            {
                this.entity = entity;
            }
            public System.String MedicationReceiveNo
            {
                get
                {
                    System.Int64? data = entity.MedicationReceiveNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
                    else entity.MedicationReceiveNo = Convert.ToInt64(value);
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
            public System.String ReceiveDateTime
            {
                get
                {
                    System.DateTime? data = entity.ReceiveDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceiveDateTime = null;
                    else entity.ReceiveDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String ItemDescription
            {
                get
                {
                    System.String data = entity.ItemDescription;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemDescription = null;
                    else entity.ItemDescription = Convert.ToString(value);
                }
            }
            public System.String RefTransactionNo
            {
                get
                {
                    System.String data = entity.RefTransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefTransactionNo = null;
                    else entity.RefTransactionNo = Convert.ToString(value);
                }
            }
            public System.String RefSequenceNo
            {
                get
                {
                    System.String data = entity.RefSequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefSequenceNo = null;
                    else entity.RefSequenceNo = Convert.ToString(value);
                }
            }
            public System.String RefQty
            {
                get
                {
                    System.Decimal? data = entity.RefQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefQty = null;
                    else entity.RefQty = Convert.ToDecimal(value);
                }
            }
            public System.String ReceiveQty
            {
                get
                {
                    System.Decimal? data = entity.ReceiveQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceiveQty = null;
                    else entity.ReceiveQty = Convert.ToDecimal(value);
                }
            }
            public System.String SRConsumeUnit
            {
                get
                {
                    System.String data = entity.SRConsumeUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
                    else entity.SRConsumeUnit = Convert.ToString(value);
                }
            }
            public System.String ConsumeQty
            {
                get
                {
                    System.Decimal? data = entity.ConsumeQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsumeQty = null;
                    else entity.ConsumeQty = Convert.ToDecimal(value);
                }
            }
            public System.String SRConsumeMethod
            {
                get
                {
                    System.String data = entity.SRConsumeMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
                    else entity.SRConsumeMethod = Convert.ToString(value);
                }
            }
            public System.String BalanceQty
            {
                get
                {
                    System.Decimal? data = entity.BalanceQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BalanceQty = null;
                    else entity.BalanceQty = Convert.ToDecimal(value);
                }
            }
            public System.String StartDateTime
            {
                get
                {
                    System.DateTime? data = entity.StartDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartDateTime = null;
                    else entity.StartDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String IsClosed
            {
                get
                {
                    System.Boolean? data = entity.IsClosed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsClosed = null;
                    else entity.IsClosed = Convert.ToBoolean(value);
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
            public System.String IsContinue
            {
                get
                {
                    System.Boolean? data = entity.IsContinue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsContinue = null;
                    else entity.IsContinue = Convert.ToBoolean(value);
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
            public System.String IsAdmissionAppropriate
            {
                get
                {
                    System.Boolean? data = entity.IsAdmissionAppropriate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAdmissionAppropriate = null;
                    else entity.IsAdmissionAppropriate = Convert.ToBoolean(value);
                }
            }
            public System.String IsTransferAppropriate
            {
                get
                {
                    System.Boolean? data = entity.IsTransferAppropriate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsTransferAppropriate = null;
                    else entity.IsTransferAppropriate = Convert.ToBoolean(value);
                }
            }
            public System.String IsDischargeAppropriate
            {
                get
                {
                    System.Boolean? data = entity.IsDischargeAppropriate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDischargeAppropriate = null;
                    else entity.IsDischargeAppropriate = Convert.ToBoolean(value);
                }
            }
            public System.String IsBroughtHome
            {
                get
                {
                    System.Boolean? data = entity.IsBroughtHome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsBroughtHome = null;
                    else entity.IsBroughtHome = Convert.ToBoolean(value);
                }
            }
            public System.String BalanceRealQty
            {
                get
                {
                    System.Decimal? data = entity.BalanceRealQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BalanceRealQty = null;
                    else entity.BalanceRealQty = Convert.ToDecimal(value);
                }
            }
            public System.String AdmissionAppropriateDateTime
            {
                get
                {
                    System.DateTime? data = entity.AdmissionAppropriateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdmissionAppropriateDateTime = null;
                    else entity.AdmissionAppropriateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String TransferAppropriateDateTime
            {
                get
                {
                    System.DateTime? data = entity.TransferAppropriateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransferAppropriateDateTime = null;
                    else entity.TransferAppropriateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String DischargeAppropriateDateTime
            {
                get
                {
                    System.DateTime? data = entity.DischargeAppropriateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DischargeAppropriateDateTime = null;
                    else entity.DischargeAppropriateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SRMedicationConsume
            {
                get
                {
                    System.String data = entity.SRMedicationConsume;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicationConsume = null;
                    else entity.SRMedicationConsume = Convert.ToString(value);
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
            public System.String IsAdmissionPresc
            {
                get
                {
                    System.Boolean? data = entity.IsAdmissionPresc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAdmissionPresc = null;
                    else entity.IsAdmissionPresc = Convert.ToBoolean(value);
                }
            }
            public System.String IsTransferPresc
            {
                get
                {
                    System.Boolean? data = entity.IsTransferPresc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsTransferPresc = null;
                    else entity.IsTransferPresc = Convert.ToBoolean(value);
                }
            }
            public System.String IsDischargePresc
            {
                get
                {
                    System.Boolean? data = entity.IsDischargePresc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDischargePresc = null;
                    else entity.IsDischargePresc = Convert.ToBoolean(value);
                }
            }
            public System.String ConsumeQtyInString
            {
                get
                {
                    System.String data = entity.ConsumeQtyInString;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsumeQtyInString = null;
                    else entity.ConsumeQtyInString = Convert.ToString(value);
                }
            }
            public System.String SRMedicationRoute
            {
                get
                {
                    System.String data = entity.SRMedicationRoute;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicationRoute = null;
                    else entity.SRMedicationRoute = Convert.ToString(value);
                }
            }
            private esMedicalDischargeSummaryPrescHomeCmx entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryPrescHomeCmxQuery query)
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
                throw new Exception("esMedicalDischargeSummaryPrescHomeCmx can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalDischargeSummaryPrescHomeCmx : esMedicalDischargeSummaryPrescHomeCmx
    {
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryPrescHomeCmxQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryPrescHomeCmxMetadata.Meta();
            }
        }

        public esQueryItem MedicationReceiveNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ReceiveDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ItemDescription
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemDescription, esSystemType.String);
            }
        }

        public esQueryItem RefTransactionNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefTransactionNo, esSystemType.String);
            }
        }

        public esQueryItem RefSequenceNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefSequenceNo, esSystemType.String);
            }
        }

        public esQueryItem RefQty
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefQty, esSystemType.Decimal);
            }
        }

        public esQueryItem ReceiveQty
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveQty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRConsumeUnit
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
            }
        }

        public esQueryItem ConsumeQty
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQty, esSystemType.Decimal);
            }
        }

        public esQueryItem SRConsumeMethod
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
            }
        }

        public esQueryItem BalanceQty
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceQty, esSystemType.Decimal);
            }
        }

        public esQueryItem StartDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem IsContinue
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsContinue, esSystemType.Boolean);
            }
        }

        public esQueryItem Note
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.Note, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsAdmissionAppropriate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionAppropriate, esSystemType.Boolean);
            }
        }

        public esQueryItem IsTransferAppropriate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferAppropriate, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDischargeAppropriate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargeAppropriate, esSystemType.Boolean);
            }
        }

        public esQueryItem IsBroughtHome
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsBroughtHome, esSystemType.Boolean);
            }
        }

        public esQueryItem BalanceRealQty
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceRealQty, esSystemType.Decimal);
            }
        }

        public esQueryItem AdmissionAppropriateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.AdmissionAppropriateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem TransferAppropriateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.TransferAppropriateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem DischargeAppropriateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.DischargeAppropriateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SRMedicationConsume
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationConsume, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem RoomID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RoomID, esSystemType.String);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem IsAdmissionPresc
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionPresc, esSystemType.Boolean);
            }
        }

        public esQueryItem IsTransferPresc
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferPresc, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDischargePresc
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargePresc, esSystemType.Boolean);
            }
        }

        public esQueryItem ConsumeQtyInString
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQtyInString, esSystemType.String);
            }
        }

        public esQueryItem SRMedicationRoute
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationRoute, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalDischargeSummaryPrescHomeCmxCollection")]
    public partial class MedicalDischargeSummaryPrescHomeCmxCollection : esMedicalDischargeSummaryPrescHomeCmxCollection, IEnumerable<MedicalDischargeSummaryPrescHomeCmx>
    {
        public MedicalDischargeSummaryPrescHomeCmxCollection()
        {

        }

        public static implicit operator List<MedicalDischargeSummaryPrescHomeCmx>(MedicalDischargeSummaryPrescHomeCmxCollection coll)
        {
            List<MedicalDischargeSummaryPrescHomeCmx> list = new List<MedicalDischargeSummaryPrescHomeCmx>();

            foreach (MedicalDischargeSummaryPrescHomeCmx emp in coll)
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
                return MedicalDischargeSummaryPrescHomeCmxMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryPrescHomeCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalDischargeSummaryPrescHomeCmx(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalDischargeSummaryPrescHomeCmx();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryPrescHomeCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryPrescHomeCmxQuery();
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
        public bool Load(MedicalDischargeSummaryPrescHomeCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalDischargeSummaryPrescHomeCmx AddNew()
        {
            MedicalDischargeSummaryPrescHomeCmx entity = base.AddNewEntity() as MedicalDischargeSummaryPrescHomeCmx;

            return entity;
        }
        public MedicalDischargeSummaryPrescHomeCmx FindByPrimaryKey(Int64 medicationReceiveNo)
        {
            return base.FindByPrimaryKey(medicationReceiveNo) as MedicalDischargeSummaryPrescHomeCmx;
        }

        #region IEnumerable< MedicalDischargeSummaryPrescHomeCmx> Members

        IEnumerator<MedicalDischargeSummaryPrescHomeCmx> IEnumerable<MedicalDischargeSummaryPrescHomeCmx>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalDischargeSummaryPrescHomeCmx;
            }
        }

        #endregion

        private MedicalDischargeSummaryPrescHomeCmxQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalDischargeSummaryPrescHomeCmx' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryPrescHomeCmx ({MedicationReceiveNo})")]
    [Serializable]
    public partial class MedicalDischargeSummaryPrescHomeCmx : esMedicalDischargeSummaryPrescHomeCmx
    {
        public MedicalDischargeSummaryPrescHomeCmx()
        {
        }

        public MedicalDischargeSummaryPrescHomeCmx(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryPrescHomeCmxMetadata.Meta();
            }
        }

        override protected esMedicalDischargeSummaryPrescHomeCmxQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryPrescHomeCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryPrescHomeCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryPrescHomeCmxQuery();
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
        public bool Load(MedicalDischargeSummaryPrescHomeCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalDischargeSummaryPrescHomeCmxQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalDischargeSummaryPrescHomeCmxQuery : esMedicalDischargeSummaryPrescHomeCmxQuery
    {
        public MedicalDischargeSummaryPrescHomeCmxQuery()
        {

        }

        public MedicalDischargeSummaryPrescHomeCmxQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryPrescHomeCmxQuery";
        }
    }

    [Serializable]
    public partial class MedicalDischargeSummaryPrescHomeCmxMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalDischargeSummaryPrescHomeCmxMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.MedicationReceiveNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ReceiveDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ItemDescription, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ItemDescription;
            c.CharacterMaxLength = 2000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefTransactionNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.RefTransactionNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefSequenceNo, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.RefSequenceNo;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RefQty, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.RefQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ReceiveQty, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ReceiveQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeUnit, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.SRConsumeUnit;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQty, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ConsumeQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRConsumeMethod, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.SRConsumeMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceQty, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.BalanceQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.StartDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.StartDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsClosed, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsClosed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsVoid, 15, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsContinue, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsContinue;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.Note, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.Note;
            c.CharacterMaxLength = 300;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionAppropriate, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsAdmissionAppropriate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferAppropriate, 21, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsTransferAppropriate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargeAppropriate, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsDischargeAppropriate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsBroughtHome, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsBroughtHome;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BalanceRealQty, 24, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.BalanceRealQty;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.AdmissionAppropriateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.AdmissionAppropriateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.TransferAppropriateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.TransferAppropriateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.DischargeAppropriateDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.DischargeAppropriateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationConsume, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.SRMedicationConsume;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ServiceUnitID, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.RoomID, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.RoomID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.BedID, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsAdmissionPresc, 32, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsAdmissionPresc;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsTransferPresc, 33, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsTransferPresc;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.IsDischargePresc, 34, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.IsDischargePresc;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.ConsumeQtyInString, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.ConsumeQtyInString;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryPrescHomeCmxMetadata.ColumnNames.SRMedicationRoute, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryPrescHomeCmxMetadata.PropertyNames.SRMedicationRoute;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalDischargeSummaryPrescHomeCmxMetadata Meta()
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
            public const string MedicationReceiveNo = "MedicationReceiveNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string ReceiveDateTime = "ReceiveDateTime";
            public const string ItemID = "ItemID";
            public const string ItemDescription = "ItemDescription";
            public const string RefTransactionNo = "RefTransactionNo";
            public const string RefSequenceNo = "RefSequenceNo";
            public const string RefQty = "RefQty";
            public const string ReceiveQty = "ReceiveQty";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string ConsumeQty = "ConsumeQty";
            public const string SRConsumeMethod = "SRConsumeMethod";
            public const string BalanceQty = "BalanceQty";
            public const string StartDateTime = "StartDateTime";
            public const string IsClosed = "IsClosed";
            public const string IsVoid = "IsVoid";
            public const string IsContinue = "IsContinue";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsAdmissionAppropriate = "IsAdmissionAppropriate";
            public const string IsTransferAppropriate = "IsTransferAppropriate";
            public const string IsDischargeAppropriate = "IsDischargeAppropriate";
            public const string IsBroughtHome = "IsBroughtHome";
            public const string BalanceRealQty = "BalanceRealQty";
            public const string AdmissionAppropriateDateTime = "AdmissionAppropriateDateTime";
            public const string TransferAppropriateDateTime = "TransferAppropriateDateTime";
            public const string DischargeAppropriateDateTime = "DischargeAppropriateDateTime";
            public const string SRMedicationConsume = "SRMedicationConsume";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string IsAdmissionPresc = "IsAdmissionPresc";
            public const string IsTransferPresc = "IsTransferPresc";
            public const string IsDischargePresc = "IsDischargePresc";
            public const string ConsumeQtyInString = "ConsumeQtyInString";
            public const string SRMedicationRoute = "SRMedicationRoute";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MedicationReceiveNo = "MedicationReceiveNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string ReceiveDateTime = "ReceiveDateTime";
            public const string ItemID = "ItemID";
            public const string ItemDescription = "ItemDescription";
            public const string RefTransactionNo = "RefTransactionNo";
            public const string RefSequenceNo = "RefSequenceNo";
            public const string RefQty = "RefQty";
            public const string ReceiveQty = "ReceiveQty";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string ConsumeQty = "ConsumeQty";
            public const string SRConsumeMethod = "SRConsumeMethod";
            public const string BalanceQty = "BalanceQty";
            public const string StartDateTime = "StartDateTime";
            public const string IsClosed = "IsClosed";
            public const string IsVoid = "IsVoid";
            public const string IsContinue = "IsContinue";
            public const string Note = "Note";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsAdmissionAppropriate = "IsAdmissionAppropriate";
            public const string IsTransferAppropriate = "IsTransferAppropriate";
            public const string IsDischargeAppropriate = "IsDischargeAppropriate";
            public const string IsBroughtHome = "IsBroughtHome";
            public const string BalanceRealQty = "BalanceRealQty";
            public const string AdmissionAppropriateDateTime = "AdmissionAppropriateDateTime";
            public const string TransferAppropriateDateTime = "TransferAppropriateDateTime";
            public const string DischargeAppropriateDateTime = "DischargeAppropriateDateTime";
            public const string SRMedicationConsume = "SRMedicationConsume";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string BedID = "BedID";
            public const string IsAdmissionPresc = "IsAdmissionPresc";
            public const string IsTransferPresc = "IsTransferPresc";
            public const string IsDischargePresc = "IsDischargePresc";
            public const string ConsumeQtyInString = "ConsumeQtyInString";
            public const string SRMedicationRoute = "SRMedicationRoute";
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
            lock (typeof(MedicalDischargeSummaryPrescHomeCmxMetadata))
            {
                if (MedicalDischargeSummaryPrescHomeCmxMetadata.mapDelegates == null)
                {
                    MedicalDischargeSummaryPrescHomeCmxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalDischargeSummaryPrescHomeCmxMetadata.meta == null)
                {
                    MedicalDischargeSummaryPrescHomeCmxMetadata.meta = new MedicalDischargeSummaryPrescHomeCmxMetadata();
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

                meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceiveDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemDescription", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefTransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefSequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ReceiveQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsumeQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BalanceQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsContinue", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAdmissionAppropriate", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsTransferAppropriate", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDischargeAppropriate", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsBroughtHome", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("BalanceRealQty", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("AdmissionAppropriateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TransferAppropriateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DischargeAppropriateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRMedicationConsume", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAdmissionPresc", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsTransferPresc", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDischargePresc", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ConsumeQtyInString", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicationRoute", new esTypeMap("varchar", "System.String"));


                meta.Source = "MedicalDischargeSummaryPrescHomeCmx";
                meta.Destination = "MedicalDischargeSummaryPrescHomeCmx";
                meta.spInsert = "proc_MedicalDischargeSummaryPrescHomeCmxInsert";
                meta.spUpdate = "proc_MedicalDischargeSummaryPrescHomeCmxUpdate";
                meta.spDelete = "proc_MedicalDischargeSummaryPrescHomeCmxDelete";
                meta.spLoadAll = "proc_MedicalDischargeSummaryPrescHomeCmxLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryPrescHomeCmxLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalDischargeSummaryPrescHomeCmxMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
