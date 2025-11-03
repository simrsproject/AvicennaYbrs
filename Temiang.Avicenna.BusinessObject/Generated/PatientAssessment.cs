/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 27/06/2024 17:00:42
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
    abstract public class esPatientAssessmentCollection : esEntityCollectionWAuditLog
    {
        public esPatientAssessmentCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientAssessmentCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientAssessmentQuery query)
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
            this.InitQuery(query as esPatientAssessmentQuery);
        }
        #endregion

        virtual public PatientAssessment DetachEntity(PatientAssessment entity)
        {
            return base.DetachEntity(entity) as PatientAssessment;
        }

        virtual public PatientAssessment AttachEntity(PatientAssessment entity)
        {
            return base.AttachEntity(entity) as PatientAssessment;
        }

        virtual public void Combine(PatientAssessmentCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientAssessment this[int index]
        {
            get
            {
                return base[index] as PatientAssessment;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientAssessment);
        }
    }

    [Serializable]
    abstract public class esPatientAssessment : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientAssessmentQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientAssessment()
        {
        }

        public esPatientAssessment(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationInfoMedicID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID)
        {
            esPatientAssessmentQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationInfoMedicID", registrationInfoMedicID);
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
                        case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
                        case "AssessmentDateTime": this.str.AssessmentDateTime = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "IsAutoAnamnesis": this.str.IsAutoAnamnesis = (string)value; break;
                        case "AllowAnamnesisSource": this.str.AllowAnamnesisSource = (string)value; break;
                        case "Hpi": this.str.Hpi = (string)value; break;
                        case "Medikamentosa": this.str.Medikamentosa = (string)value; break;
                        case "ReviewOfSystem": this.str.ReviewOfSystem = (string)value; break;
                        case "PhysicalExam": this.str.PhysicalExam = (string)value; break;
                        case "OtherExam": this.str.OtherExam = (string)value; break;
                        case "Diagnose": this.str.Diagnose = (string)value; break;
                        case "Therapy": this.str.Therapy = (string)value; break;
                        case "Education": this.str.Education = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "AnamnesisNotes": this.str.AnamnesisNotes = (string)value; break;
                        case "IsInitialAssessment": this.str.IsInitialAssessment = (string)value; break;
                        case "EstimatedDayInPatient": this.str.EstimatedDayInPatient = (string)value; break;
                        case "Prognosis": this.str.Prognosis = (string)value; break;
                        case "FollowUpPlanType": this.str.FollowUpPlanType = (string)value; break;
                        case "ConsulToType": this.str.ConsulToType = (string)value; break;
                        case "ConsulTo": this.str.ConsulTo = (string)value; break;
                        case "InpatientIndication": this.str.InpatientIndication = (string)value; break;
                        case "ControlPlan": this.str.ControlPlan = (string)value; break;
                        case "JobHistNotes": this.str.JobHistNotes = (string)value; break;
                        case "HighRiskCriteria": this.str.HighRiskCriteria = (string)value; break;
                        case "ConsultDate": this.str.ConsultDate = (string)value; break;
                        case "ReferToHospital": this.str.ReferToHospital = (string)value; break;
                        case "ReferToFamilyDoctor": this.str.ReferToFamilyDoctor = (string)value; break;
                        case "RoomInPatient": this.str.RoomInPatient = (string)value; break;
                        case "DpjpInPatient": this.str.DpjpInPatient = (string)value; break;
                        case "IsInPatientGuide": this.str.IsInPatientGuide = (string)value; break;
                        case "PatientEducationSeqNo": this.str.PatientEducationSeqNo = (string)value; break;
                        case "DischargeDatePlan": this.str.DischargeDatePlan = (string)value; break;
                        case "DischargeMedicalPlan": this.str.DischargeMedicalPlan = (string)value; break;
                        case "DoaDateTime": this.str.DoaDateTime = (string)value; break;
                        case "SurgicalDateTime": this.str.SurgicalDateTime = (string)value; break;
                        case "InPatientRejectReason": this.str.InPatientRejectReason = (string)value; break;
                        case "ReferReason": this.str.ReferReason = (string)value; break;
                        case "IsDeleted": this.str.IsDeleted = (string)value; break;
                        case "AdditionalNotes": this.str.AdditionalNotes = (string)value; break;
                        case "DpjpInPatientID": this.str.DpjpInPatientID = (string)value; break;
                        case "DiagnoseDiff": this.str.DiagnoseDiff = (string)value; break;
                        case "SubjectiveAddNote": this.str.SubjectiveAddNote = (string)value; break;
                        case "Fdolm": this.str.Fdolm = (string)value; break;
                        case "PastmedicalHistory": this.str.PastmedicalHistory = (string)value; break;
                        case "FamilyMedicalHistory": this.str.FamilyMedicalHistory = (string)value; break;
                        case "ReferTo": this.str.ReferTo = (string)value; break;
                        case "ToInpatient": this.str.ToInpatient = (string)value; break;
                        case "ChiefComplaint": this.str.ChiefComplaint = (string)value; break;
                        case "SCTChiefComplaint": this.str.SCTChiefComplaint = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AssessmentDateTime":

                            if (value == null || value is System.DateTime)
                                this.AssessmentDateTime = (System.DateTime?)value;
                            break;
                        case "IsAutoAnamnesis":

                            if (value == null || value is System.Boolean)
                                this.IsAutoAnamnesis = (System.Boolean?)value;
                            break;
                        case "Genogram":

                            if (value == null || value is System.Byte[])
                                this.Genogram = (System.Byte[])value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsInitialAssessment":

                            if (value == null || value is System.Boolean)
                                this.IsInitialAssessment = (System.Boolean?)value;
                            break;
                        case "EstimatedDayInPatient":

                            if (value == null || value is System.Int32)
                                this.EstimatedDayInPatient = (System.Int32?)value;
                            break;
                        case "ConsultDate":

                            if (value == null || value is System.DateTime)
                                this.ConsultDate = (System.DateTime?)value;
                            break;
                        case "IsInPatientGuide":

                            if (value == null || value is System.Boolean)
                                this.IsInPatientGuide = (System.Boolean?)value;
                            break;
                        case "PatientEducationSeqNo":

                            if (value == null || value is System.Int32)
                                this.PatientEducationSeqNo = (System.Int32?)value;
                            break;
                        case "DischargeDatePlan":

                            if (value == null || value is System.DateTime)
                                this.DischargeDatePlan = (System.DateTime?)value;
                            break;
                        case "DoaDateTime":

                            if (value == null || value is System.DateTime)
                                this.DoaDateTime = (System.DateTime?)value;
                            break;
                        case "SurgicalDateTime":

                            if (value == null || value is System.DateTime)
                                this.SurgicalDateTime = (System.DateTime?)value;
                            break;
                        case "IsDeleted":

                            if (value == null || value is System.Boolean)
                                this.IsDeleted = (System.Boolean?)value;
                            break;
                        case "Photo":

                            if (value == null || value is System.Byte[])
                                this.Photo = (System.Byte[])value;
                            break;
                        case "SignImg":

                            if (value == null || value is System.Byte[])
                                this.SignImg = (System.Byte[])value;
                            break;
                        case "PatientSignImg":

                            if (value == null || value is System.Byte[])
                                this.PatientSignImg = (System.Byte[])value;
                            break;
                        case "Fdolm":

                            if (value == null || value is System.DateTime)
                                this.Fdolm = (System.DateTime?)value;
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
        /// Maps to PatientAssessment.RegistrationInfoMedicID
        /// </summary>
        virtual public System.String RegistrationInfoMedicID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.RegistrationInfoMedicID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.RegistrationInfoMedicID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.SRAssessmentType
        /// </summary>
        virtual public System.String SRAssessmentType
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.SRAssessmentType);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.SRAssessmentType, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.AssessmentDateTime
        /// </summary>
        virtual public System.DateTime? AssessmentDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.AssessmentDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.AssessmentDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.IsAutoAnamnesis
        /// </summary>
        virtual public System.Boolean? IsAutoAnamnesis
        {
            get
            {
                return base.GetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsAutoAnamnesis);
            }

            set
            {
                base.SetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsAutoAnamnesis, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.AllowAnamnesisSource
        /// </summary>
        virtual public System.String AllowAnamnesisSource
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.AllowAnamnesisSource);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.AllowAnamnesisSource, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Hpi
        /// </summary>
        virtual public System.String Hpi
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Hpi);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Hpi, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Medikamentosa
        /// </summary>
        virtual public System.String Medikamentosa
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Medikamentosa);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Medikamentosa, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ReviewOfSystem
        /// </summary>
        virtual public System.String ReviewOfSystem
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ReviewOfSystem);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ReviewOfSystem, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.PhysicalExam
        /// </summary>
        virtual public System.String PhysicalExam
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.PhysicalExam);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.PhysicalExam, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.OtherExam
        /// </summary>
        virtual public System.String OtherExam
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.OtherExam);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.OtherExam, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Diagnose
        /// </summary>
        virtual public System.String Diagnose
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Diagnose);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Diagnose, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Therapy
        /// </summary>
        virtual public System.String Therapy
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Therapy);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Therapy, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Education
        /// </summary>
        virtual public System.String Education
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Education);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Education, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Genogram
        /// </summary>
        virtual public System.Byte[] Genogram
        {
            get
            {
                return base.GetSystemByteArray(PatientAssessmentMetadata.ColumnNames.Genogram);
            }

            set
            {
                base.SetSystemByteArray(PatientAssessmentMetadata.ColumnNames.Genogram, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.AnamnesisNotes
        /// </summary>
        virtual public System.String AnamnesisNotes
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.AnamnesisNotes);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.AnamnesisNotes, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.IsInitialAssessment
        /// </summary>
        virtual public System.Boolean? IsInitialAssessment
        {
            get
            {
                return base.GetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsInitialAssessment);
            }

            set
            {
                base.SetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsInitialAssessment, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.EstimatedDayInPatient
        /// </summary>
        virtual public System.Int32? EstimatedDayInPatient
        {
            get
            {
                return base.GetSystemInt32(PatientAssessmentMetadata.ColumnNames.EstimatedDayInPatient);
            }

            set
            {
                base.SetSystemInt32(PatientAssessmentMetadata.ColumnNames.EstimatedDayInPatient, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Prognosis
        /// </summary>
        virtual public System.String Prognosis
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.Prognosis);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.Prognosis, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.FollowUpPlanType
        /// </summary>
        virtual public System.String FollowUpPlanType
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.FollowUpPlanType);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.FollowUpPlanType, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ConsulToType
        /// </summary>
        virtual public System.String ConsulToType
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ConsulToType);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ConsulToType, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ConsulTo
        /// </summary>
        virtual public System.String ConsulTo
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ConsulTo);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ConsulTo, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.InpatientIndication
        /// </summary>
        virtual public System.String InpatientIndication
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.InpatientIndication);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.InpatientIndication, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ControlPlan
        /// </summary>
        virtual public System.String ControlPlan
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ControlPlan);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ControlPlan, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.JobHistNotes
        /// </summary>
        virtual public System.String JobHistNotes
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.JobHistNotes);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.JobHistNotes, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.HighRiskCriteria
        /// </summary>
        virtual public System.String HighRiskCriteria
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.HighRiskCriteria);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.HighRiskCriteria, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ConsultDate
        /// </summary>
        virtual public System.DateTime? ConsultDate
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.ConsultDate);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.ConsultDate, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ReferToHospital
        /// </summary>
        virtual public System.String ReferToHospital
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ReferToHospital);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ReferToHospital, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ReferToFamilyDoctor
        /// </summary>
        virtual public System.String ReferToFamilyDoctor
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ReferToFamilyDoctor);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ReferToFamilyDoctor, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.RoomInPatient
        /// </summary>
        virtual public System.String RoomInPatient
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.RoomInPatient);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.RoomInPatient, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DpjpInPatient
        /// </summary>
        virtual public System.String DpjpInPatient
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.DpjpInPatient);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.DpjpInPatient, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.IsInPatientGuide
        /// </summary>
        virtual public System.Boolean? IsInPatientGuide
        {
            get
            {
                return base.GetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsInPatientGuide);
            }

            set
            {
                base.SetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsInPatientGuide, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.PatientEducationSeqNo
        /// </summary>
        virtual public System.Int32? PatientEducationSeqNo
        {
            get
            {
                return base.GetSystemInt32(PatientAssessmentMetadata.ColumnNames.PatientEducationSeqNo);
            }

            set
            {
                base.SetSystemInt32(PatientAssessmentMetadata.ColumnNames.PatientEducationSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DischargeDatePlan
        /// </summary>
        virtual public System.DateTime? DischargeDatePlan
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.DischargeDatePlan);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.DischargeDatePlan, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DischargeMedicalPlan
        /// </summary>
        virtual public System.String DischargeMedicalPlan
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.DischargeMedicalPlan);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.DischargeMedicalPlan, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DoaDateTime
        /// </summary>
        virtual public System.DateTime? DoaDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.DoaDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.DoaDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.SurgicalDateTime
        /// </summary>
        virtual public System.DateTime? SurgicalDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.SurgicalDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.SurgicalDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.InPatientRejectReason
        /// </summary>
        virtual public System.String InPatientRejectReason
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.InPatientRejectReason);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.InPatientRejectReason, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ReferReason
        /// </summary>
        virtual public System.String ReferReason
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ReferReason);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ReferReason, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.IsDeleted
        /// </summary>
        virtual public System.Boolean? IsDeleted
        {
            get
            {
                return base.GetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsDeleted);
            }

            set
            {
                base.SetSystemBoolean(PatientAssessmentMetadata.ColumnNames.IsDeleted, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Photo
        /// </summary>
        virtual public System.Byte[] Photo
        {
            get
            {
                return base.GetSystemByteArray(PatientAssessmentMetadata.ColumnNames.Photo);
            }

            set
            {
                base.SetSystemByteArray(PatientAssessmentMetadata.ColumnNames.Photo, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.AdditionalNotes
        /// </summary>
        virtual public System.String AdditionalNotes
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.AdditionalNotes);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.AdditionalNotes, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DpjpInPatientID
        /// </summary>
        virtual public System.String DpjpInPatientID
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.DpjpInPatientID);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.DpjpInPatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.SignImg
        /// </summary>
        virtual public System.Byte[] SignImg
        {
            get
            {
                return base.GetSystemByteArray(PatientAssessmentMetadata.ColumnNames.SignImg);
            }

            set
            {
                base.SetSystemByteArray(PatientAssessmentMetadata.ColumnNames.SignImg, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.PatientSignImg
        /// </summary>
        virtual public System.Byte[] PatientSignImg
        {
            get
            {
                return base.GetSystemByteArray(PatientAssessmentMetadata.ColumnNames.PatientSignImg);
            }

            set
            {
                base.SetSystemByteArray(PatientAssessmentMetadata.ColumnNames.PatientSignImg, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.DiagnoseDiff
        /// </summary>
        virtual public System.String DiagnoseDiff
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.DiagnoseDiff);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.DiagnoseDiff, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.SubjectiveAddNote
        /// </summary>
        virtual public System.String SubjectiveAddNote
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.SubjectiveAddNote);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.SubjectiveAddNote, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.Fdolm
        /// </summary>
        virtual public System.DateTime? Fdolm
        {
            get
            {
                return base.GetSystemDateTime(PatientAssessmentMetadata.ColumnNames.Fdolm);
            }

            set
            {
                base.SetSystemDateTime(PatientAssessmentMetadata.ColumnNames.Fdolm, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.PastmedicalHistory
        /// </summary>
        virtual public System.String PastmedicalHistory
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.PastmedicalHistory);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.PastmedicalHistory, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.FamilyMedicalHistory
        /// </summary>
        virtual public System.String FamilyMedicalHistory
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.FamilyMedicalHistory);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.FamilyMedicalHistory, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ReferTo
        /// </summary>
        virtual public System.String ReferTo
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ReferTo);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ReferTo, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ToInpatient
        /// </summary>
        virtual public System.String ToInpatient
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ToInpatient);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ToInpatient, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.ChiefComplaint
        /// </summary>
        virtual public System.String ChiefComplaint
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.ChiefComplaint);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.ChiefComplaint, value);
            }
        }
        /// <summary>
        /// Maps to PatientAssessment.SCTChiefComplaint
        /// </summary>
        virtual public System.String SCTChiefComplaint
        {
            get
            {
                return base.GetSystemString(PatientAssessmentMetadata.ColumnNames.SCTChiefComplaint);
            }

            set
            {
                base.SetSystemString(PatientAssessmentMetadata.ColumnNames.SCTChiefComplaint, value);
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
            public esStrings(esPatientAssessment entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationInfoMedicID
            {
                get
                {
                    System.String data = entity.RegistrationInfoMedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
                    else entity.RegistrationInfoMedicID = Convert.ToString(value);
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
            public System.String SRAssessmentType
            {
                get
                {
                    System.String data = entity.SRAssessmentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAssessmentType = null;
                    else entity.SRAssessmentType = Convert.ToString(value);
                }
            }
            public System.String AssessmentDateTime
            {
                get
                {
                    System.DateTime? data = entity.AssessmentDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssessmentDateTime = null;
                    else entity.AssessmentDateTime = Convert.ToDateTime(value);
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
            public System.String IsAutoAnamnesis
            {
                get
                {
                    System.Boolean? data = entity.IsAutoAnamnesis;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAutoAnamnesis = null;
                    else entity.IsAutoAnamnesis = Convert.ToBoolean(value);
                }
            }
            public System.String AllowAnamnesisSource
            {
                get
                {
                    System.String data = entity.AllowAnamnesisSource;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AllowAnamnesisSource = null;
                    else entity.AllowAnamnesisSource = Convert.ToString(value);
                }
            }
            public System.String Hpi
            {
                get
                {
                    System.String data = entity.Hpi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Hpi = null;
                    else entity.Hpi = Convert.ToString(value);
                }
            }
            public System.String Medikamentosa
            {
                get
                {
                    System.String data = entity.Medikamentosa;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Medikamentosa = null;
                    else entity.Medikamentosa = Convert.ToString(value);
                }
            }
            public System.String ReviewOfSystem
            {
                get
                {
                    System.String data = entity.ReviewOfSystem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReviewOfSystem = null;
                    else entity.ReviewOfSystem = Convert.ToString(value);
                }
            }
            public System.String PhysicalExam
            {
                get
                {
                    System.String data = entity.PhysicalExam;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PhysicalExam = null;
                    else entity.PhysicalExam = Convert.ToString(value);
                }
            }
            public System.String OtherExam
            {
                get
                {
                    System.String data = entity.OtherExam;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OtherExam = null;
                    else entity.OtherExam = Convert.ToString(value);
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
            public System.String Therapy
            {
                get
                {
                    System.String data = entity.Therapy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Therapy = null;
                    else entity.Therapy = Convert.ToString(value);
                }
            }
            public System.String Education
            {
                get
                {
                    System.String data = entity.Education;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Education = null;
                    else entity.Education = Convert.ToString(value);
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
            public System.String AnamnesisNotes
            {
                get
                {
                    System.String data = entity.AnamnesisNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AnamnesisNotes = null;
                    else entity.AnamnesisNotes = Convert.ToString(value);
                }
            }
            public System.String IsInitialAssessment
            {
                get
                {
                    System.Boolean? data = entity.IsInitialAssessment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInitialAssessment = null;
                    else entity.IsInitialAssessment = Convert.ToBoolean(value);
                }
            }
            public System.String EstimatedDayInPatient
            {
                get
                {
                    System.Int32? data = entity.EstimatedDayInPatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EstimatedDayInPatient = null;
                    else entity.EstimatedDayInPatient = Convert.ToInt32(value);
                }
            }
            public System.String Prognosis
            {
                get
                {
                    System.String data = entity.Prognosis;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Prognosis = null;
                    else entity.Prognosis = Convert.ToString(value);
                }
            }
            public System.String FollowUpPlanType
            {
                get
                {
                    System.String data = entity.FollowUpPlanType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FollowUpPlanType = null;
                    else entity.FollowUpPlanType = Convert.ToString(value);
                }
            }
            public System.String ConsulToType
            {
                get
                {
                    System.String data = entity.ConsulToType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsulToType = null;
                    else entity.ConsulToType = Convert.ToString(value);
                }
            }
            public System.String ConsulTo
            {
                get
                {
                    System.String data = entity.ConsulTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsulTo = null;
                    else entity.ConsulTo = Convert.ToString(value);
                }
            }
            public System.String InpatientIndication
            {
                get
                {
                    System.String data = entity.InpatientIndication;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InpatientIndication = null;
                    else entity.InpatientIndication = Convert.ToString(value);
                }
            }
            public System.String ControlPlan
            {
                get
                {
                    System.String data = entity.ControlPlan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ControlPlan = null;
                    else entity.ControlPlan = Convert.ToString(value);
                }
            }
            public System.String JobHistNotes
            {
                get
                {
                    System.String data = entity.JobHistNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JobHistNotes = null;
                    else entity.JobHistNotes = Convert.ToString(value);
                }
            }
            public System.String HighRiskCriteria
            {
                get
                {
                    System.String data = entity.HighRiskCriteria;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HighRiskCriteria = null;
                    else entity.HighRiskCriteria = Convert.ToString(value);
                }
            }
            public System.String ConsultDate
            {
                get
                {
                    System.DateTime? data = entity.ConsultDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConsultDate = null;
                    else entity.ConsultDate = Convert.ToDateTime(value);
                }
            }
            public System.String ReferToHospital
            {
                get
                {
                    System.String data = entity.ReferToHospital;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferToHospital = null;
                    else entity.ReferToHospital = Convert.ToString(value);
                }
            }
            public System.String ReferToFamilyDoctor
            {
                get
                {
                    System.String data = entity.ReferToFamilyDoctor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferToFamilyDoctor = null;
                    else entity.ReferToFamilyDoctor = Convert.ToString(value);
                }
            }
            public System.String RoomInPatient
            {
                get
                {
                    System.String data = entity.RoomInPatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomInPatient = null;
                    else entity.RoomInPatient = Convert.ToString(value);
                }
            }
            public System.String DpjpInPatient
            {
                get
                {
                    System.String data = entity.DpjpInPatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DpjpInPatient = null;
                    else entity.DpjpInPatient = Convert.ToString(value);
                }
            }
            public System.String IsInPatientGuide
            {
                get
                {
                    System.Boolean? data = entity.IsInPatientGuide;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInPatientGuide = null;
                    else entity.IsInPatientGuide = Convert.ToBoolean(value);
                }
            }
            public System.String PatientEducationSeqNo
            {
                get
                {
                    System.Int32? data = entity.PatientEducationSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientEducationSeqNo = null;
                    else entity.PatientEducationSeqNo = Convert.ToInt32(value);
                }
            }
            public System.String DischargeDatePlan
            {
                get
                {
                    System.DateTime? data = entity.DischargeDatePlan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DischargeDatePlan = null;
                    else entity.DischargeDatePlan = Convert.ToDateTime(value);
                }
            }
            public System.String DischargeMedicalPlan
            {
                get
                {
                    System.String data = entity.DischargeMedicalPlan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DischargeMedicalPlan = null;
                    else entity.DischargeMedicalPlan = Convert.ToString(value);
                }
            }
            public System.String DoaDateTime
            {
                get
                {
                    System.DateTime? data = entity.DoaDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DoaDateTime = null;
                    else entity.DoaDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SurgicalDateTime
            {
                get
                {
                    System.DateTime? data = entity.SurgicalDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SurgicalDateTime = null;
                    else entity.SurgicalDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String InPatientRejectReason
            {
                get
                {
                    System.String data = entity.InPatientRejectReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InPatientRejectReason = null;
                    else entity.InPatientRejectReason = Convert.ToString(value);
                }
            }
            public System.String ReferReason
            {
                get
                {
                    System.String data = entity.ReferReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferReason = null;
                    else entity.ReferReason = Convert.ToString(value);
                }
            }
            public System.String IsDeleted
            {
                get
                {
                    System.Boolean? data = entity.IsDeleted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeleted = null;
                    else entity.IsDeleted = Convert.ToBoolean(value);
                }
            }
            public System.String AdditionalNotes
            {
                get
                {
                    System.String data = entity.AdditionalNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdditionalNotes = null;
                    else entity.AdditionalNotes = Convert.ToString(value);
                }
            }
            public System.String DpjpInPatientID
            {
                get
                {
                    System.String data = entity.DpjpInPatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DpjpInPatientID = null;
                    else entity.DpjpInPatientID = Convert.ToString(value);
                }
            }
            public System.String DiagnoseDiff
            {
                get
                {
                    System.String data = entity.DiagnoseDiff;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseDiff = null;
                    else entity.DiagnoseDiff = Convert.ToString(value);
                }
            }
            public System.String SubjectiveAddNote
            {
                get
                {
                    System.String data = entity.SubjectiveAddNote;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubjectiveAddNote = null;
                    else entity.SubjectiveAddNote = Convert.ToString(value);
                }
            }
            public System.String Fdolm
            {
                get
                {
                    System.DateTime? data = entity.Fdolm;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Fdolm = null;
                    else entity.Fdolm = Convert.ToDateTime(value);
                }
            }
            public System.String PastmedicalHistory
            {
                get
                {
                    System.String data = entity.PastmedicalHistory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PastmedicalHistory = null;
                    else entity.PastmedicalHistory = Convert.ToString(value);
                }
            }
            public System.String FamilyMedicalHistory
            {
                get
                {
                    System.String data = entity.FamilyMedicalHistory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FamilyMedicalHistory = null;
                    else entity.FamilyMedicalHistory = Convert.ToString(value);
                }
            }
            public System.String ReferTo
            {
                get
                {
                    System.String data = entity.ReferTo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferTo = null;
                    else entity.ReferTo = Convert.ToString(value);
                }
            }
            public System.String ToInpatient
            {
                get
                {
                    System.String data = entity.ToInpatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToInpatient = null;
                    else entity.ToInpatient = Convert.ToString(value);
                }
            }
            public System.String ChiefComplaint
            {
                get
                {
                    System.String data = entity.ChiefComplaint;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChiefComplaint = null;
                    else entity.ChiefComplaint = Convert.ToString(value);
                }
            }
            public System.String SCTChiefComplaint
            {
                get
                {
                    System.String data = entity.SCTChiefComplaint;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SCTChiefComplaint = null;
                    else entity.SCTChiefComplaint = Convert.ToString(value);
                }
            }
            private esPatientAssessment entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientAssessmentQuery query)
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
                throw new Exception("esPatientAssessment can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientAssessment : esPatientAssessment
    {
    }

    [Serializable]
    abstract public class esPatientAssessmentQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientAssessmentMetadata.Meta();
            }
        }

        public esQueryItem RegistrationInfoMedicID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem SRAssessmentType
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
            }
        }

        public esQueryItem AssessmentDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.AssessmentDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem IsAutoAnamnesis
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.IsAutoAnamnesis, esSystemType.Boolean);
            }
        }

        public esQueryItem AllowAnamnesisSource
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.AllowAnamnesisSource, esSystemType.String);
            }
        }

        public esQueryItem Hpi
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Hpi, esSystemType.String);
            }
        }

        public esQueryItem Medikamentosa
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Medikamentosa, esSystemType.String);
            }
        }

        public esQueryItem ReviewOfSystem
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ReviewOfSystem, esSystemType.String);
            }
        }

        public esQueryItem PhysicalExam
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.PhysicalExam, esSystemType.String);
            }
        }

        public esQueryItem OtherExam
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.OtherExam, esSystemType.String);
            }
        }

        public esQueryItem Diagnose
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Diagnose, esSystemType.String);
            }
        }

        public esQueryItem Therapy
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Therapy, esSystemType.String);
            }
        }

        public esQueryItem Education
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Education, esSystemType.String);
            }
        }

        public esQueryItem Genogram
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Genogram, esSystemType.ByteArray);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem AnamnesisNotes
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.AnamnesisNotes, esSystemType.String);
            }
        }

        public esQueryItem IsInitialAssessment
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.IsInitialAssessment, esSystemType.Boolean);
            }
        }

        public esQueryItem EstimatedDayInPatient
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.EstimatedDayInPatient, esSystemType.Int32);
            }
        }

        public esQueryItem Prognosis
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Prognosis, esSystemType.String);
            }
        }

        public esQueryItem FollowUpPlanType
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.FollowUpPlanType, esSystemType.String);
            }
        }

        public esQueryItem ConsulToType
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ConsulToType, esSystemType.String);
            }
        }

        public esQueryItem ConsulTo
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ConsulTo, esSystemType.String);
            }
        }

        public esQueryItem InpatientIndication
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.InpatientIndication, esSystemType.String);
            }
        }

        public esQueryItem ControlPlan
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ControlPlan, esSystemType.String);
            }
        }

        public esQueryItem JobHistNotes
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.JobHistNotes, esSystemType.String);
            }
        }

        public esQueryItem HighRiskCriteria
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.HighRiskCriteria, esSystemType.String);
            }
        }

        public esQueryItem ConsultDate
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ConsultDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ReferToHospital
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ReferToHospital, esSystemType.String);
            }
        }

        public esQueryItem ReferToFamilyDoctor
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ReferToFamilyDoctor, esSystemType.String);
            }
        }

        public esQueryItem RoomInPatient
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.RoomInPatient, esSystemType.String);
            }
        }

        public esQueryItem DpjpInPatient
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DpjpInPatient, esSystemType.String);
            }
        }

        public esQueryItem IsInPatientGuide
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.IsInPatientGuide, esSystemType.Boolean);
            }
        }

        public esQueryItem PatientEducationSeqNo
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.PatientEducationSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem DischargeDatePlan
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DischargeDatePlan, esSystemType.DateTime);
            }
        }

        public esQueryItem DischargeMedicalPlan
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DischargeMedicalPlan, esSystemType.String);
            }
        }

        public esQueryItem DoaDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DoaDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SurgicalDateTime
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.SurgicalDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem InPatientRejectReason
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.InPatientRejectReason, esSystemType.String);
            }
        }

        public esQueryItem ReferReason
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ReferReason, esSystemType.String);
            }
        }

        public esQueryItem IsDeleted
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
            }
        }

        public esQueryItem Photo
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Photo, esSystemType.ByteArray);
            }
        }

        public esQueryItem AdditionalNotes
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.AdditionalNotes, esSystemType.String);
            }
        }

        public esQueryItem DpjpInPatientID
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DpjpInPatientID, esSystemType.String);
            }
        }

        public esQueryItem SignImg
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.SignImg, esSystemType.ByteArray);
            }
        }

        public esQueryItem PatientSignImg
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.PatientSignImg, esSystemType.ByteArray);
            }
        }

        public esQueryItem DiagnoseDiff
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.DiagnoseDiff, esSystemType.String);
            }
        }

        public esQueryItem SubjectiveAddNote
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.SubjectiveAddNote, esSystemType.String);
            }
        }

        public esQueryItem Fdolm
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.Fdolm, esSystemType.DateTime);
            }
        }

        public esQueryItem PastmedicalHistory
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.PastmedicalHistory, esSystemType.String);
            }
        }

        public esQueryItem FamilyMedicalHistory
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.FamilyMedicalHistory, esSystemType.String);
            }
        }

        public esQueryItem ReferTo
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ReferTo, esSystemType.String);
            }
        }

        public esQueryItem ToInpatient
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ToInpatient, esSystemType.String);
            }
        }

        public esQueryItem ChiefComplaint
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.ChiefComplaint, esSystemType.String);
            }
        }

        public esQueryItem SCTChiefComplaint
        {
            get
            {
                return new esQueryItem(this, PatientAssessmentMetadata.ColumnNames.SCTChiefComplaint, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientAssessmentCollection")]
    public partial class PatientAssessmentCollection : esPatientAssessmentCollection, IEnumerable<PatientAssessment>
    {
        public PatientAssessmentCollection()
        {

        }

        public static implicit operator List<PatientAssessment>(PatientAssessmentCollection coll)
        {
            List<PatientAssessment> list = new List<PatientAssessment>();

            foreach (PatientAssessment emp in coll)
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
                return PatientAssessmentMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientAssessmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientAssessment(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientAssessment();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientAssessmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientAssessmentQuery();
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
        public bool Load(PatientAssessmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientAssessment AddNew()
        {
            PatientAssessment entity = base.AddNewEntity() as PatientAssessment;

            return entity;
        }
        public PatientAssessment FindByPrimaryKey(String registrationInfoMedicID)
        {
            return base.FindByPrimaryKey(registrationInfoMedicID) as PatientAssessment;
        }

        #region IEnumerable< PatientAssessment> Members

        IEnumerator<PatientAssessment> IEnumerable<PatientAssessment>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientAssessment;
            }
        }

        #endregion

        private PatientAssessmentQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientAssessment' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientAssessment ({RegistrationInfoMedicID})")]
    [Serializable]
    public partial class PatientAssessment : esPatientAssessment
    {
        public PatientAssessment()
        {
        }

        public PatientAssessment(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientAssessmentMetadata.Meta();
            }
        }

        override protected esPatientAssessmentQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientAssessmentQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientAssessmentQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientAssessmentQuery();
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
        public bool Load(PatientAssessmentQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientAssessmentQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientAssessmentQuery : esPatientAssessmentQuery
    {
        public PatientAssessmentQuery()
        {

        }

        public PatientAssessmentQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientAssessmentQuery";
        }
    }

    [Serializable]
    public partial class PatientAssessmentMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientAssessmentMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.RegistrationInfoMedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.PatientID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.SRAssessmentType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.SRAssessmentType;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.AssessmentDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.AssessmentDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ServiceUnitID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.IsAutoAnamnesis, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.IsAutoAnamnesis;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.AllowAnamnesisSource, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.AllowAnamnesisSource;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Hpi, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Hpi;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Medikamentosa, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Medikamentosa;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ReviewOfSystem, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ReviewOfSystem;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.PhysicalExam, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.PhysicalExam;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.OtherExam, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.OtherExam;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Diagnose, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Diagnose;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Therapy, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Therapy;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Education, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Education;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Genogram, 16, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Genogram;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Notes, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.CreatedByUserID, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.CreatedDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.AnamnesisNotes, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.AnamnesisNotes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.IsInitialAssessment, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.IsInitialAssessment;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.EstimatedDayInPatient, 24, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.EstimatedDayInPatient;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Prognosis, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Prognosis;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.FollowUpPlanType, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.FollowUpPlanType;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ConsulToType, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ConsulToType;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ConsulTo, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ConsulTo;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.InpatientIndication, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.InpatientIndication;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ControlPlan, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ControlPlan;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.JobHistNotes, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.JobHistNotes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.HighRiskCriteria, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.HighRiskCriteria;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ConsultDate, 33, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ConsultDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ReferToHospital, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ReferToHospital;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ReferToFamilyDoctor, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ReferToFamilyDoctor;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.RoomInPatient, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.RoomInPatient;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DpjpInPatient, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DpjpInPatient;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.IsInPatientGuide, 38, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.IsInPatientGuide;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.PatientEducationSeqNo, 39, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.PatientEducationSeqNo;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DischargeDatePlan, 40, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DischargeDatePlan;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DischargeMedicalPlan, 41, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DischargeMedicalPlan;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DoaDateTime, 42, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DoaDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.SurgicalDateTime, 43, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.SurgicalDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.InPatientRejectReason, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.InPatientRejectReason;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ReferReason, 45, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ReferReason;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.IsDeleted, 46, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.IsDeleted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Photo, 47, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Photo;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.AdditionalNotes, 48, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.AdditionalNotes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DpjpInPatientID, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DpjpInPatientID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.SignImg, 50, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.SignImg;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.PatientSignImg, 51, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.PatientSignImg;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.DiagnoseDiff, 52, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.DiagnoseDiff;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.SubjectiveAddNote, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.SubjectiveAddNote;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.Fdolm, 54, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.Fdolm;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.PastmedicalHistory, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.PastmedicalHistory;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.FamilyMedicalHistory, 56, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.FamilyMedicalHistory;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ReferTo, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ReferTo;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ToInpatient, 58, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ToInpatient;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.ChiefComplaint, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.ChiefComplaint;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientAssessmentMetadata.ColumnNames.SCTChiefComplaint, 60, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientAssessmentMetadata.PropertyNames.SCTChiefComplaint;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientAssessmentMetadata Meta()
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
            public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
            public const string PatientID = "PatientID";
            public const string SRAssessmentType = "SRAssessmentType";
            public const string AssessmentDateTime = "AssessmentDateTime";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsAutoAnamnesis = "IsAutoAnamnesis";
            public const string AllowAnamnesisSource = "AllowAnamnesisSource";
            public const string Hpi = "Hpi";
            public const string Medikamentosa = "Medikamentosa";
            public const string ReviewOfSystem = "ReviewOfSystem";
            public const string PhysicalExam = "PhysicalExam";
            public const string OtherExam = "OtherExam";
            public const string Diagnose = "Diagnose";
            public const string Therapy = "Therapy";
            public const string Education = "Education";
            public const string Genogram = "Genogram";
            public const string Notes = "Notes";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string AnamnesisNotes = "AnamnesisNotes";
            public const string IsInitialAssessment = "IsInitialAssessment";
            public const string EstimatedDayInPatient = "EstimatedDayInPatient";
            public const string Prognosis = "Prognosis";
            public const string FollowUpPlanType = "FollowUpPlanType";
            public const string ConsulToType = "ConsulToType";
            public const string ConsulTo = "ConsulTo";
            public const string InpatientIndication = "InpatientIndication";
            public const string ControlPlan = "ControlPlan";
            public const string JobHistNotes = "JobHistNotes";
            public const string HighRiskCriteria = "HighRiskCriteria";
            public const string ConsultDate = "ConsultDate";
            public const string ReferToHospital = "ReferToHospital";
            public const string ReferToFamilyDoctor = "ReferToFamilyDoctor";
            public const string RoomInPatient = "RoomInPatient";
            public const string DpjpInPatient = "DpjpInPatient";
            public const string IsInPatientGuide = "IsInPatientGuide";
            public const string PatientEducationSeqNo = "PatientEducationSeqNo";
            public const string DischargeDatePlan = "DischargeDatePlan";
            public const string DischargeMedicalPlan = "DischargeMedicalPlan";
            public const string DoaDateTime = "DoaDateTime";
            public const string SurgicalDateTime = "SurgicalDateTime";
            public const string InPatientRejectReason = "InPatientRejectReason";
            public const string ReferReason = "ReferReason";
            public const string IsDeleted = "IsDeleted";
            public const string Photo = "Photo";
            public const string AdditionalNotes = "AdditionalNotes";
            public const string DpjpInPatientID = "DpjpInPatientID";
            public const string SignImg = "SignImg";
            public const string PatientSignImg = "PatientSignImg";
            public const string DiagnoseDiff = "DiagnoseDiff";
            public const string SubjectiveAddNote = "SubjectiveAddNote";
            public const string Fdolm = "Fdolm";
            public const string PastmedicalHistory = "PastmedicalHistory";
            public const string FamilyMedicalHistory = "FamilyMedicalHistory";
            public const string ReferTo = "ReferTo";
            public const string ToInpatient = "ToInpatient";
            public const string ChiefComplaint = "ChiefComplaint";
            public const string SCTChiefComplaint = "SCTChiefComplaint";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
            public const string PatientID = "PatientID";
            public const string SRAssessmentType = "SRAssessmentType";
            public const string AssessmentDateTime = "AssessmentDateTime";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string IsAutoAnamnesis = "IsAutoAnamnesis";
            public const string AllowAnamnesisSource = "AllowAnamnesisSource";
            public const string Hpi = "Hpi";
            public const string Medikamentosa = "Medikamentosa";
            public const string ReviewOfSystem = "ReviewOfSystem";
            public const string PhysicalExam = "PhysicalExam";
            public const string OtherExam = "OtherExam";
            public const string Diagnose = "Diagnose";
            public const string Therapy = "Therapy";
            public const string Education = "Education";
            public const string Genogram = "Genogram";
            public const string Notes = "Notes";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string AnamnesisNotes = "AnamnesisNotes";
            public const string IsInitialAssessment = "IsInitialAssessment";
            public const string EstimatedDayInPatient = "EstimatedDayInPatient";
            public const string Prognosis = "Prognosis";
            public const string FollowUpPlanType = "FollowUpPlanType";
            public const string ConsulToType = "ConsulToType";
            public const string ConsulTo = "ConsulTo";
            public const string InpatientIndication = "InpatientIndication";
            public const string ControlPlan = "ControlPlan";
            public const string JobHistNotes = "JobHistNotes";
            public const string HighRiskCriteria = "HighRiskCriteria";
            public const string ConsultDate = "ConsultDate";
            public const string ReferToHospital = "ReferToHospital";
            public const string ReferToFamilyDoctor = "ReferToFamilyDoctor";
            public const string RoomInPatient = "RoomInPatient";
            public const string DpjpInPatient = "DpjpInPatient";
            public const string IsInPatientGuide = "IsInPatientGuide";
            public const string PatientEducationSeqNo = "PatientEducationSeqNo";
            public const string DischargeDatePlan = "DischargeDatePlan";
            public const string DischargeMedicalPlan = "DischargeMedicalPlan";
            public const string DoaDateTime = "DoaDateTime";
            public const string SurgicalDateTime = "SurgicalDateTime";
            public const string InPatientRejectReason = "InPatientRejectReason";
            public const string ReferReason = "ReferReason";
            public const string IsDeleted = "IsDeleted";
            public const string Photo = "Photo";
            public const string AdditionalNotes = "AdditionalNotes";
            public const string DpjpInPatientID = "DpjpInPatientID";
            public const string SignImg = "SignImg";
            public const string PatientSignImg = "PatientSignImg";
            public const string DiagnoseDiff = "DiagnoseDiff";
            public const string SubjectiveAddNote = "SubjectiveAddNote";
            public const string Fdolm = "Fdolm";
            public const string PastmedicalHistory = "PastmedicalHistory";
            public const string FamilyMedicalHistory = "FamilyMedicalHistory";
            public const string ReferTo = "ReferTo";
            public const string ToInpatient = "ToInpatient";
            public const string ChiefComplaint = "ChiefComplaint";
            public const string SCTChiefComplaint = "SCTChiefComplaint";
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
            lock (typeof(PatientAssessmentMetadata))
            {
                if (PatientAssessmentMetadata.mapDelegates == null)
                {
                    PatientAssessmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientAssessmentMetadata.meta == null)
                {
                    PatientAssessmentMetadata.meta = new PatientAssessmentMetadata();
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

                meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssessmentDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAutoAnamnesis", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AllowAnamnesisSource", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Hpi", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Medikamentosa", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReviewOfSystem", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhysicalExam", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OtherExam", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Therapy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Education", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Genogram", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("AnamnesisNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInitialAssessment", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("EstimatedDayInPatient", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Prognosis", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FollowUpPlanType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsulToType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsulTo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InpatientIndication", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ControlPlan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JobHistNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HighRiskCriteria", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConsultDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ReferToHospital", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferToFamilyDoctor", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomInPatient", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DpjpInPatient", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInPatientGuide", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PatientEducationSeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DischargeDatePlan", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DischargeMedicalPlan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DoaDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SurgicalDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("InPatientRejectReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Photo", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("AdditionalNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DpjpInPatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SignImg", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("PatientSignImg", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("DiagnoseDiff", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SubjectiveAddNote", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Fdolm", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PastmedicalHistory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FamilyMedicalHistory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferTo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToInpatient", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChiefComplaint", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SCTChiefComplaint", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientAssessment";
                meta.Destination = "PatientAssessment";
                meta.spInsert = "proc_PatientAssessmentInsert";
                meta.spUpdate = "proc_PatientAssessmentUpdate";
                meta.spDelete = "proc_PatientAssessmentDelete";
                meta.spLoadAll = "proc_PatientAssessmentLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientAssessmentLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientAssessmentMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
