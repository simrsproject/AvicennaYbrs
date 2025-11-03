/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/9/2017 7:43:55 PM
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
    abstract public class esPpiNeedlePuncturedCollection : esEntityCollectionWAuditLog
    {
        public esPpiNeedlePuncturedCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiNeedlePuncturedCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiNeedlePuncturedQuery query)
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
            this.InitQuery(query as esPpiNeedlePuncturedQuery);
        }
        #endregion

        virtual public PpiNeedlePunctured DetachEntity(PpiNeedlePunctured entity)
        {
            return base.DetachEntity(entity) as PpiNeedlePunctured;
        }

        virtual public PpiNeedlePunctured AttachEntity(PpiNeedlePunctured entity)
        {
            return base.AttachEntity(entity) as PpiNeedlePunctured;
        }

        virtual public void Combine(PpiNeedlePuncturedCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiNeedlePunctured this[int index]
        {
            get
            {
                return base[index] as PpiNeedlePunctured;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiNeedlePunctured);
        }
    }

    [Serializable]
    abstract public class esPpiNeedlePunctured : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiNeedlePuncturedQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiNeedlePunctured()
        {
        }

        public esPpiNeedlePunctured(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo)
        {
            esPpiNeedlePuncturedQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
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
                        case "OfficerName": this.str.OfficerName = (string)value; break;
                        case "DatePunctured": this.str.DatePunctured = (string)value; break;
                        case "PuncturedAreas": this.str.PuncturedAreas = (string)value; break;
                        case "CausePunctured": this.str.CausePunctured = (string)value; break;
                        case "IsBlood": this.str.IsBlood = (string)value; break;
                        case "IsFluidSperm": this.str.IsFluidSperm = (string)value; break;
                        case "IsVaginalSecretions": this.str.IsVaginalSecretions = (string)value; break;
                        case "IsCerebrospinal": this.str.IsCerebrospinal = (string)value; break;
                        case "IsUrine": this.str.IsUrine = (string)value; break;
                        case "IsFaeces": this.str.IsFaeces = (string)value; break;
                        case "IsOfficerHiv": this.str.IsOfficerHiv = (string)value; break;
                        case "IsOfficerHbv": this.str.IsOfficerHbv = (string)value; break;
                        case "IsOfficerHcv": this.str.IsOfficerHcv = (string)value; break;
                        case "OfficerImunizationHistory": this.str.OfficerImunizationHistory = (string)value; break;
                        case "Chronology": this.str.Chronology = (string)value; break;
                        case "MedicalNo": this.str.MedicalNo = (string)value; break;
                        case "PatientName": this.str.PatientName = (string)value; break;
                        case "Diagnose": this.str.Diagnose = (string)value; break;
                        case "IsPatientHiv": this.str.IsPatientHiv = (string)value; break;
                        case "IsPatientHbv": this.str.IsPatientHbv = (string)value; break;
                        case "IsPatientHcv": this.str.IsPatientHcv = (string)value; break;
                        case "PatientImunizationHistory": this.str.PatientImunizationHistory = (string)value; break;
                        case "KnownBy": this.str.KnownBy = (string)value; break;
                        case "FollowUpDate": this.str.FollowUpDate = (string)value; break;
                        case "FollowUp": this.str.FollowUp = (string)value; break;
                        case "FollowUpBy": this.str.FollowUpBy = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "IsVerified": this.str.IsVerified = (string)value; break;
                        case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
                        case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
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
                        case "DatePunctured":

                            if (value == null || value is System.DateTime)
                                this.DatePunctured = (System.DateTime?)value;
                            break;
                        case "IsBlood":

                            if (value == null || value is System.Boolean)
                                this.IsBlood = (System.Boolean?)value;
                            break;
                        case "IsFluidSperm":

                            if (value == null || value is System.Boolean)
                                this.IsFluidSperm = (System.Boolean?)value;
                            break;
                        case "IsVaginalSecretions":

                            if (value == null || value is System.Boolean)
                                this.IsVaginalSecretions = (System.Boolean?)value;
                            break;
                        case "IsCerebrospinal":

                            if (value == null || value is System.Boolean)
                                this.IsCerebrospinal = (System.Boolean?)value;
                            break;
                        case "IsUrine":

                            if (value == null || value is System.Boolean)
                                this.IsUrine = (System.Boolean?)value;
                            break;
                        case "IsFaeces":

                            if (value == null || value is System.Boolean)
                                this.IsFaeces = (System.Boolean?)value;
                            break;
                        case "IsOfficerHiv":

                            if (value == null || value is System.Boolean)
                                this.IsOfficerHiv = (System.Boolean?)value;
                            break;
                        case "IsOfficerHbv":

                            if (value == null || value is System.Boolean)
                                this.IsOfficerHbv = (System.Boolean?)value;
                            break;
                        case "IsOfficerHcv":

                            if (value == null || value is System.Boolean)
                                this.IsOfficerHcv = (System.Boolean?)value;
                            break;
                        case "IsPatientHiv":

                            if (value == null || value is System.Boolean)
                                this.IsPatientHiv = (System.Boolean?)value;
                            break;
                        case "IsPatientHbv":

                            if (value == null || value is System.Boolean)
                                this.IsPatientHbv = (System.Boolean?)value;
                            break;
                        case "IsPatientHcv":

                            if (value == null || value is System.Boolean)
                                this.IsPatientHcv = (System.Boolean?)value;
                            break;
                        case "FollowUpDate":

                            if (value == null || value is System.DateTime)
                                this.FollowUpDate = (System.DateTime?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;
                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
                            break;
                        case "IsVerified":

                            if (value == null || value is System.Boolean)
                                this.IsVerified = (System.Boolean?)value;
                            break;
                        case "VerifiedDateTime":

                            if (value == null || value is System.DateTime)
                                this.VerifiedDateTime = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to PpiNeedlePunctured.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.TransactionDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.OfficerName
        /// </summary>
        virtual public System.String OfficerName
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.OfficerName);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.OfficerName, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.DatePunctured
        /// </summary>
        virtual public System.DateTime? DatePunctured
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.DatePunctured);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.DatePunctured, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.PuncturedAreas
        /// </summary>
        virtual public System.String PuncturedAreas
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PuncturedAreas);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PuncturedAreas, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.CausePunctured
        /// </summary>
        virtual public System.String CausePunctured
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.CausePunctured);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.CausePunctured, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsBlood
        /// </summary>
        virtual public System.Boolean? IsBlood
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsBlood);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsBlood, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsFluidSperm
        /// </summary>
        virtual public System.Boolean? IsFluidSperm
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsFluidSperm);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsFluidSperm, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsVaginalSecretions
        /// </summary>
        virtual public System.Boolean? IsVaginalSecretions
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVaginalSecretions);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVaginalSecretions, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsCerebrospinal
        /// </summary>
        virtual public System.Boolean? IsCerebrospinal
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsCerebrospinal);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsCerebrospinal, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsUrine
        /// </summary>
        virtual public System.Boolean? IsUrine
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsUrine);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsUrine, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsFaeces
        /// </summary>
        virtual public System.Boolean? IsFaeces
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsFaeces);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsFaeces, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsOfficerHiv
        /// </summary>
        virtual public System.Boolean? IsOfficerHiv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHiv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHiv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsOfficerHbv
        /// </summary>
        virtual public System.Boolean? IsOfficerHbv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHbv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHbv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsOfficerHcv
        /// </summary>
        virtual public System.Boolean? IsOfficerHcv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHcv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHcv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.OfficerImunizationHistory
        /// </summary>
        virtual public System.String OfficerImunizationHistory
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.OfficerImunizationHistory);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.OfficerImunizationHistory, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.Chronology
        /// </summary>
        virtual public System.String Chronology
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.Chronology);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.Chronology, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.MedicalNo
        /// </summary>
        virtual public System.String MedicalNo
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.MedicalNo);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.MedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.PatientName
        /// </summary>
        virtual public System.String PatientName
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PatientName);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PatientName, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.Diagnose
        /// </summary>
        virtual public System.String Diagnose
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.Diagnose);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.Diagnose, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsPatientHiv
        /// </summary>
        virtual public System.Boolean? IsPatientHiv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHiv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHiv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsPatientHbv
        /// </summary>
        virtual public System.Boolean? IsPatientHbv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHbv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHbv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsPatientHcv
        /// </summary>
        virtual public System.Boolean? IsPatientHcv
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHcv);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHcv, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.PatientImunizationHistory
        /// </summary>
        virtual public System.String PatientImunizationHistory
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PatientImunizationHistory);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.PatientImunizationHistory, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.KnownBy
        /// </summary>
        virtual public System.String KnownBy
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.KnownBy);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.KnownBy, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.FollowUpDate
        /// </summary>
        virtual public System.DateTime? FollowUpDate
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpDate);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpDate, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.FollowUp
        /// </summary>
        virtual public System.String FollowUp
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.FollowUp);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.FollowUp, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.FollowUpBy
        /// </summary>
        virtual public System.String FollowUpBy
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpBy);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpBy, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.IsVerified
        /// </summary>
        virtual public System.Boolean? IsVerified
        {
            get
            {
                return base.GetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVerified);
            }

            set
            {
                base.SetSystemBoolean(PpiNeedlePuncturedMetadata.ColumnNames.IsVerified, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.VerifiedDateTime
        /// </summary>
        virtual public System.DateTime? VerifiedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.VerifiedByUserID
        /// </summary>
        virtual public System.String VerifiedByUserID
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedByUserID);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiNeedlePunctured.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiNeedlePunctured entity)
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
            public System.String OfficerName
            {
                get
                {
                    System.String data = entity.OfficerName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OfficerName = null;
                    else entity.OfficerName = Convert.ToString(value);
                }
            }
            public System.String DatePunctured
            {
                get
                {
                    System.DateTime? data = entity.DatePunctured;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DatePunctured = null;
                    else entity.DatePunctured = Convert.ToDateTime(value);
                }
            }
            public System.String PuncturedAreas
            {
                get
                {
                    System.String data = entity.PuncturedAreas;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PuncturedAreas = null;
                    else entity.PuncturedAreas = Convert.ToString(value);
                }
            }
            public System.String CausePunctured
            {
                get
                {
                    System.String data = entity.CausePunctured;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CausePunctured = null;
                    else entity.CausePunctured = Convert.ToString(value);
                }
            }
            public System.String IsBlood
            {
                get
                {
                    System.Boolean? data = entity.IsBlood;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsBlood = null;
                    else entity.IsBlood = Convert.ToBoolean(value);
                }
            }
            public System.String IsFluidSperm
            {
                get
                {
                    System.Boolean? data = entity.IsFluidSperm;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsFluidSperm = null;
                    else entity.IsFluidSperm = Convert.ToBoolean(value);
                }
            }
            public System.String IsVaginalSecretions
            {
                get
                {
                    System.Boolean? data = entity.IsVaginalSecretions;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVaginalSecretions = null;
                    else entity.IsVaginalSecretions = Convert.ToBoolean(value);
                }
            }
            public System.String IsCerebrospinal
            {
                get
                {
                    System.Boolean? data = entity.IsCerebrospinal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCerebrospinal = null;
                    else entity.IsCerebrospinal = Convert.ToBoolean(value);
                }
            }
            public System.String IsUrine
            {
                get
                {
                    System.Boolean? data = entity.IsUrine;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUrine = null;
                    else entity.IsUrine = Convert.ToBoolean(value);
                }
            }
            public System.String IsFaeces
            {
                get
                {
                    System.Boolean? data = entity.IsFaeces;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsFaeces = null;
                    else entity.IsFaeces = Convert.ToBoolean(value);
                }
            }
            public System.String IsOfficerHiv
            {
                get
                {
                    System.Boolean? data = entity.IsOfficerHiv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOfficerHiv = null;
                    else entity.IsOfficerHiv = Convert.ToBoolean(value);
                }
            }
            public System.String IsOfficerHbv
            {
                get
                {
                    System.Boolean? data = entity.IsOfficerHbv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOfficerHbv = null;
                    else entity.IsOfficerHbv = Convert.ToBoolean(value);
                }
            }
            public System.String IsOfficerHcv
            {
                get
                {
                    System.Boolean? data = entity.IsOfficerHcv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOfficerHcv = null;
                    else entity.IsOfficerHcv = Convert.ToBoolean(value);
                }
            }
            public System.String OfficerImunizationHistory
            {
                get
                {
                    System.String data = entity.OfficerImunizationHistory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OfficerImunizationHistory = null;
                    else entity.OfficerImunizationHistory = Convert.ToString(value);
                }
            }
            public System.String Chronology
            {
                get
                {
                    System.String data = entity.Chronology;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Chronology = null;
                    else entity.Chronology = Convert.ToString(value);
                }
            }
            public System.String MedicalNo
            {
                get
                {
                    System.String data = entity.MedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalNo = null;
                    else entity.MedicalNo = Convert.ToString(value);
                }
            }
            public System.String PatientName
            {
                get
                {
                    System.String data = entity.PatientName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientName = null;
                    else entity.PatientName = Convert.ToString(value);
                }
            }
            public System.String Diagnose
            {
                get
                {
                    System.String data = entity.Diagnose;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Diagnose = null;
                    else entity.Diagnose = Convert.ToString(value);
                }
            }
            public System.String IsPatientHiv
            {
                get
                {
                    System.Boolean? data = entity.IsPatientHiv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPatientHiv = null;
                    else entity.IsPatientHiv = Convert.ToBoolean(value);
                }
            }
            public System.String IsPatientHbv
            {
                get
                {
                    System.Boolean? data = entity.IsPatientHbv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPatientHbv = null;
                    else entity.IsPatientHbv = Convert.ToBoolean(value);
                }
            }
            public System.String IsPatientHcv
            {
                get
                {
                    System.Boolean? data = entity.IsPatientHcv;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPatientHcv = null;
                    else entity.IsPatientHcv = Convert.ToBoolean(value);
                }
            }
            public System.String PatientImunizationHistory
            {
                get
                {
                    System.String data = entity.PatientImunizationHistory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientImunizationHistory = null;
                    else entity.PatientImunizationHistory = Convert.ToString(value);
                }
            }
            public System.String KnownBy
            {
                get
                {
                    System.String data = entity.KnownBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KnownBy = null;
                    else entity.KnownBy = Convert.ToString(value);
                }
            }
            public System.String FollowUpDate
            {
                get
                {
                    System.DateTime? data = entity.FollowUpDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUpDate = null;
                    else entity.FollowUpDate = Convert.ToDateTime(value);
                }
            }
            public System.String FollowUp
            {
                get
                {
                    System.String data = entity.FollowUp;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUp = null;
                    else entity.FollowUp = Convert.ToString(value);
                }
            }
            public System.String FollowUpBy
            {
                get
                {
                    System.String data = entity.FollowUpBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUpBy = null;
                    else entity.FollowUpBy = Convert.ToString(value);
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
            public System.String IsVerified
            {
                get
                {
                    System.Boolean? data = entity.IsVerified;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVerified = null;
                    else entity.IsVerified = Convert.ToBoolean(value);
                }
            }
            public System.String VerifiedDateTime
            {
                get
                {
                    System.DateTime? data = entity.VerifiedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
                    else entity.VerifiedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String VerifiedByUserID
            {
                get
                {
                    System.String data = entity.VerifiedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
                    else entity.VerifiedByUserID = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
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
            private esPpiNeedlePunctured entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiNeedlePuncturedQuery query)
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
                throw new Exception("esPpiNeedlePunctured can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiNeedlePunctured : esPpiNeedlePunctured
    {
    }

    [Serializable]
    abstract public class esPpiNeedlePuncturedQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiNeedlePuncturedMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem OfficerName
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.OfficerName, esSystemType.String);
            }
        }

        public esQueryItem DatePunctured
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.DatePunctured, esSystemType.DateTime);
            }
        }

        public esQueryItem PuncturedAreas
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.PuncturedAreas, esSystemType.String);
            }
        }

        public esQueryItem CausePunctured
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.CausePunctured, esSystemType.String);
            }
        }

        public esQueryItem IsBlood
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsBlood, esSystemType.Boolean);
            }
        }

        public esQueryItem IsFluidSperm
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsFluidSperm, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVaginalSecretions
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsVaginalSecretions, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCerebrospinal
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsCerebrospinal, esSystemType.Boolean);
            }
        }

        public esQueryItem IsUrine
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsUrine, esSystemType.Boolean);
            }
        }

        public esQueryItem IsFaeces
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsFaeces, esSystemType.Boolean);
            }
        }

        public esQueryItem IsOfficerHiv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHiv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsOfficerHbv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHbv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsOfficerHcv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHcv, esSystemType.Boolean);
            }
        }

        public esQueryItem OfficerImunizationHistory
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.OfficerImunizationHistory, esSystemType.String);
            }
        }

        public esQueryItem Chronology
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.Chronology, esSystemType.String);
            }
        }

        public esQueryItem MedicalNo
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.MedicalNo, esSystemType.String);
            }
        }

        public esQueryItem PatientName
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.PatientName, esSystemType.String);
            }
        }

        public esQueryItem Diagnose
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.Diagnose, esSystemType.String);
            }
        }

        public esQueryItem IsPatientHiv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHiv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPatientHbv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHbv, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPatientHcv
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHcv, esSystemType.Boolean);
            }
        }

        public esQueryItem PatientImunizationHistory
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.PatientImunizationHistory, esSystemType.String);
            }
        }

        public esQueryItem KnownBy
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.KnownBy, esSystemType.String);
            }
        }

        public esQueryItem FollowUpDate
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.FollowUpDate, esSystemType.DateTime);
            }
        }

        public esQueryItem FollowUp
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.FollowUp, esSystemType.String);
            }
        }

        public esQueryItem FollowUpBy
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.FollowUpBy, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsVerified
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
            }
        }

        public esQueryItem VerifiedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VerifiedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiNeedlePuncturedCollection")]
    public partial class PpiNeedlePuncturedCollection : esPpiNeedlePuncturedCollection, IEnumerable<PpiNeedlePunctured>
    {
        public PpiNeedlePuncturedCollection()
        {

        }

        public static implicit operator List<PpiNeedlePunctured>(PpiNeedlePuncturedCollection coll)
        {
            List<PpiNeedlePunctured> list = new List<PpiNeedlePunctured>();

            foreach (PpiNeedlePunctured emp in coll)
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
                return PpiNeedlePuncturedMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiNeedlePuncturedQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiNeedlePunctured(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiNeedlePunctured();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiNeedlePuncturedQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiNeedlePuncturedQuery();
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
        public bool Load(PpiNeedlePuncturedQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiNeedlePunctured AddNew()
        {
            PpiNeedlePunctured entity = base.AddNewEntity() as PpiNeedlePunctured;

            return entity;
        }
        public PpiNeedlePunctured FindByPrimaryKey(String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as PpiNeedlePunctured;
        }

        #region IEnumerable< PpiNeedlePunctured> Members

        IEnumerator<PpiNeedlePunctured> IEnumerable<PpiNeedlePunctured>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiNeedlePunctured;
            }
        }

        #endregion

        private PpiNeedlePuncturedQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiNeedlePunctured' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiNeedlePunctured ({TransactionNo})")]
    [Serializable]
    public partial class PpiNeedlePunctured : esPpiNeedlePunctured
    {
        public PpiNeedlePunctured()
        {
        }

        public PpiNeedlePunctured(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiNeedlePuncturedMetadata.Meta();
            }
        }

        override protected esPpiNeedlePuncturedQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiNeedlePuncturedQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiNeedlePuncturedQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiNeedlePuncturedQuery();
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
        public bool Load(PpiNeedlePuncturedQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiNeedlePuncturedQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiNeedlePuncturedQuery : esPpiNeedlePuncturedQuery
    {
        public PpiNeedlePuncturedQuery()
        {

        }

        public PpiNeedlePuncturedQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiNeedlePuncturedQuery";
        }
    }

    [Serializable]
    public partial class PpiNeedlePuncturedMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiNeedlePuncturedMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.OfficerName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.OfficerName;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.DatePunctured, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.DatePunctured;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.PuncturedAreas, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.PuncturedAreas;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.CausePunctured, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.CausePunctured;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsBlood, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsBlood;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsFluidSperm, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsFluidSperm;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsVaginalSecretions, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsVaginalSecretions;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsCerebrospinal, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsCerebrospinal;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsUrine, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsUrine;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsFaeces, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsFaeces;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHiv, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsOfficerHiv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHbv, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsOfficerHbv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsOfficerHcv, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsOfficerHcv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.OfficerImunizationHistory, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.OfficerImunizationHistory;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.Chronology, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.Chronology;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.MedicalNo, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.MedicalNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.PatientName, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.PatientName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.Diagnose, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.Diagnose;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHiv, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsPatientHiv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHbv, 21, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsPatientHbv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsPatientHcv, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsPatientHcv;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.PatientImunizationHistory, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.PatientImunizationHistory;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.KnownBy, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.KnownBy;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpDate, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.FollowUpDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.FollowUp, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.FollowUp;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.FollowUpBy, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.FollowUpBy;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsVoid, 28, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.VoidDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.VoidByUserID, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsApproved, 31, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.ApprovedByUserID, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.IsVerified, 34, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.IsVerified;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedDateTime, 35, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.VerifiedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.VerifiedByUserID, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.VerifiedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.CreatedDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.CreatedByUserID, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiNeedlePuncturedMetadata.ColumnNames.LastUpdateByUserID, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiNeedlePuncturedMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiNeedlePuncturedMetadata Meta()
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
            public const string OfficerName = "OfficerName";
            public const string DatePunctured = "DatePunctured";
            public const string PuncturedAreas = "PuncturedAreas";
            public const string CausePunctured = "CausePunctured";
            public const string IsBlood = "IsBlood";
            public const string IsFluidSperm = "IsFluidSperm";
            public const string IsVaginalSecretions = "IsVaginalSecretions";
            public const string IsCerebrospinal = "IsCerebrospinal";
            public const string IsUrine = "IsUrine";
            public const string IsFaeces = "IsFaeces";
            public const string IsOfficerHiv = "IsOfficerHiv";
            public const string IsOfficerHbv = "IsOfficerHbv";
            public const string IsOfficerHcv = "IsOfficerHcv";
            public const string OfficerImunizationHistory = "OfficerImunizationHistory";
            public const string Chronology = "Chronology";
            public const string MedicalNo = "MedicalNo";
            public const string PatientName = "PatientName";
            public const string Diagnose = "Diagnose";
            public const string IsPatientHiv = "IsPatientHiv";
            public const string IsPatientHbv = "IsPatientHbv";
            public const string IsPatientHcv = "IsPatientHcv";
            public const string PatientImunizationHistory = "PatientImunizationHistory";
            public const string KnownBy = "KnownBy";
            public const string FollowUpDate = "FollowUpDate";
            public const string FollowUp = "FollowUp";
            public const string FollowUpBy = "FollowUpBy";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVerified = "IsVerified";
            public const string VerifiedDateTime = "VerifiedDateTime";
            public const string VerifiedByUserID = "VerifiedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string OfficerName = "OfficerName";
            public const string DatePunctured = "DatePunctured";
            public const string PuncturedAreas = "PuncturedAreas";
            public const string CausePunctured = "CausePunctured";
            public const string IsBlood = "IsBlood";
            public const string IsFluidSperm = "IsFluidSperm";
            public const string IsVaginalSecretions = "IsVaginalSecretions";
            public const string IsCerebrospinal = "IsCerebrospinal";
            public const string IsUrine = "IsUrine";
            public const string IsFaeces = "IsFaeces";
            public const string IsOfficerHiv = "IsOfficerHiv";
            public const string IsOfficerHbv = "IsOfficerHbv";
            public const string IsOfficerHcv = "IsOfficerHcv";
            public const string OfficerImunizationHistory = "OfficerImunizationHistory";
            public const string Chronology = "Chronology";
            public const string MedicalNo = "MedicalNo";
            public const string PatientName = "PatientName";
            public const string Diagnose = "Diagnose";
            public const string IsPatientHiv = "IsPatientHiv";
            public const string IsPatientHbv = "IsPatientHbv";
            public const string IsPatientHcv = "IsPatientHcv";
            public const string PatientImunizationHistory = "PatientImunizationHistory";
            public const string KnownBy = "KnownBy";
            public const string FollowUpDate = "FollowUpDate";
            public const string FollowUp = "FollowUp";
            public const string FollowUpBy = "FollowUpBy";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string IsVerified = "IsVerified";
            public const string VerifiedDateTime = "VerifiedDateTime";
            public const string VerifiedByUserID = "VerifiedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(PpiNeedlePuncturedMetadata))
            {
                if (PpiNeedlePuncturedMetadata.mapDelegates == null)
                {
                    PpiNeedlePuncturedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiNeedlePuncturedMetadata.meta == null)
                {
                    PpiNeedlePuncturedMetadata.meta = new PpiNeedlePuncturedMetadata();
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
                meta.AddTypeMap("OfficerName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DatePunctured", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PuncturedAreas", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CausePunctured", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsBlood", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsFluidSperm", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVaginalSecretions", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCerebrospinal", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsUrine", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsFaeces", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsOfficerHiv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsOfficerHbv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsOfficerHcv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("OfficerImunizationHistory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Chronology", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPatientHiv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPatientHbv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPatientHcv", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PatientImunizationHistory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KnownBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FollowUpDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FollowUp", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FollowUpBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiNeedlePunctured";
                meta.Destination = "PpiNeedlePunctured";
                meta.spInsert = "proc_PpiNeedlePuncturedInsert";
                meta.spUpdate = "proc_PpiNeedlePuncturedUpdate";
                meta.spDelete = "proc_PpiNeedlePuncturedDelete";
                meta.spLoadAll = "proc_PpiNeedlePuncturedLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiNeedlePuncturedLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiNeedlePuncturedMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
