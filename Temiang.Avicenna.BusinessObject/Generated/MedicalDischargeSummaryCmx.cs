/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 04/04/2024 21:18:57
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
    abstract public class esMedicalDischargeSummaryCmxCollection : esEntityCollectionWAuditLog
    {
        public esMedicalDischargeSummaryCmxCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalDischargeSummaryCmxCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryCmxQuery query)
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
            this.InitQuery(query as esMedicalDischargeSummaryCmxQuery);
        }
        #endregion

        virtual public MedicalDischargeSummaryCmx DetachEntity(MedicalDischargeSummaryCmx entity)
        {
            return base.DetachEntity(entity) as MedicalDischargeSummaryCmx;
        }

        virtual public MedicalDischargeSummaryCmx AttachEntity(MedicalDischargeSummaryCmx entity)
        {
            return base.AttachEntity(entity) as MedicalDischargeSummaryCmx;
        }

        virtual public void Combine(MedicalDischargeSummaryCmxCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalDischargeSummaryCmx this[int index]
        {
            get
            {
                return base[index] as MedicalDischargeSummaryCmx;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalDischargeSummaryCmx);
        }
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryCmx : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalDischargeSummaryCmxQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalDischargeSummaryCmx()
        {
        }

        public esMedicalDischargeSummaryCmx(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esMedicalDischargeSummaryCmxQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ChiefComplaint": this.str.ChiefComplaint = (string)value; break;
                        case "HistOfPresentIllness": this.str.HistOfPresentIllness = (string)value; break;
                        case "Komorbiditas": this.str.Komorbiditas = (string)value; break;
                        case "PhysicalExam": this.str.PhysicalExam = (string)value; break;
                        case "AncillaryExam": this.str.AncillaryExam = (string)value; break;
                        case "MedicalProcedures": this.str.MedicalProcedures = (string)value; break;
                        case "ProcedureID": this.str.ProcedureID = (string)value; break;
                        case "Medications": this.str.Medications = (string)value; break;
                        case "AdmittingDiagnoseID1": this.str.AdmittingDiagnoseID1 = (string)value; break;
                        case "AdmittingDiagnoseName1": this.str.AdmittingDiagnoseName1 = (string)value; break;
                        case "AdmittingDiagnoseID2": this.str.AdmittingDiagnoseID2 = (string)value; break;
                        case "AdmittingDiagnoseName2": this.str.AdmittingDiagnoseName2 = (string)value; break;
                        case "FinalDiagnoseID1": this.str.FinalDiagnoseID1 = (string)value; break;
                        case "FinalDiagnoseName1": this.str.FinalDiagnoseName1 = (string)value; break;
                        case "FinalDiagnoseID2": this.str.FinalDiagnoseID2 = (string)value; break;
                        case "FinalDiagnoseName2": this.str.FinalDiagnoseName2 = (string)value; break;
                        case "FinalDiagnoseID3": this.str.FinalDiagnoseID3 = (string)value; break;
                        case "FinalDiagnoseName3": this.str.FinalDiagnoseName3 = (string)value; break;
                        case "PresentStatus": this.str.PresentStatus = (string)value; break;
                        case "SuggestionFollowUp": this.str.SuggestionFollowUp = (string)value; break;
                        case "TreatmentIndications": this.str.TreatmentIndications = (string)value; break;
                        case "PastMedicalHistory": this.str.PastMedicalHistory = (string)value; break;
                        case "ProcedureName": this.str.ProcedureName = (string)value; break;
                        case "DischargeDate": this.str.DischargeDate = (string)value; break;
                        case "DischargeTime": this.str.DischargeTime = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "ParamedicName": this.str.ParamedicName = (string)value; break;
                        case "SRUnitIntended": this.str.SRUnitIntended = (string)value; break;
                        case "SRDischargeMethod": this.str.SRDischargeMethod = (string)value; break;
                        case "SRDischargeCondition": this.str.SRDischargeCondition = (string)value; break;
                        case "Prognosis": this.str.Prognosis = (string)value; break;
                        case "IsRichTextMode": this.str.IsRichTextMode = (string)value; break;
                        case "AncillaryExamOther": this.str.AncillaryExamOther = (string)value; break;
                        case "Diet": this.str.Diet = (string)value; break;
                        case "DocumentDate": this.str.DocumentDate = (string)value; break;
                        case "ControlPlan": this.str.ControlPlan = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "HomeCare": this.str.HomeCare = (string)value; break;
                        case "EducationAtHome": this.str.EducationAtHome = (string)value; break;
                        case "Consul": this.str.Consul = (string)value; break;
                        case "MedicalSupport": this.str.MedicalSupport = (string)value; break;
                        case "InLocation": this.str.InLocation = (string)value; break;
                        case "CollectionDateTime": this.str.CollectionDateTime = (string)value; break;
                        case "InitialDiagnose": this.str.InitialDiagnose = (string)value; break;
                        case "SRTypeOfService": this.str.SRTypeOfService = (string)value; break;
                        case "SRCauseOfDisease": this.str.SRCauseOfDisease = (string)value; break;
                        case "SRCauseOfDevelopDisorder": this.str.SRCauseOfDevelopDisorder = (string)value; break;
                        case "CauseOfDevelopDisorder": this.str.CauseOfDevelopDisorder = (string)value; break;
                        case "SRNatureOfSurgery": this.str.SRNatureOfSurgery = (string)value; break;
                        case "IsInstruction1": this.str.IsInstruction1 = (string)value; break;
                        case "IsInstruction2": this.str.IsInstruction2 = (string)value; break;
                        case "IsInstruction3": this.str.IsInstruction3 = (string)value; break;
                        case "Instruction3": this.str.Instruction3 = (string)value; break;
                        case "IsInstruction4": this.str.IsInstruction4 = (string)value; break;
                        case "Instruction4": this.str.Instruction4 = (string)value; break;
                        case "IsInstruction5": this.str.IsInstruction5 = (string)value; break;
                        case "Instruction5": this.str.Instruction5 = (string)value; break;
                        case "IsInstruction6": this.str.IsInstruction6 = (string)value; break;
                        case "Instruction6": this.str.Instruction6 = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "DischargeDate":

                            if (value == null || value is System.DateTime)
                                this.DischargeDate = (System.DateTime?)value;
                            break;
                        case "IsRichTextMode":

                            if (value == null || value is System.Boolean)
                                this.IsRichTextMode = (System.Boolean?)value;
                            break;
                        case "DocumentDate":

                            if (value == null || value is System.DateTime)
                                this.DocumentDate = (System.DateTime?)value;
                            break;
                        case "PpaSign":

                            if (value == null || value is System.Byte[])
                                this.PpaSign = (System.Byte[])value;
                            break;
                        case "PatientSign":

                            if (value == null || value is System.Byte[])
                                this.PatientSign = (System.Byte[])value;
                            break;
                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;
                        case "CollectionDateTime":

                            if (value == null || value is System.DateTime)
                                this.CollectionDateTime = (System.DateTime?)value;
                            break;
                        case "IsInstruction1":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction1 = (System.Boolean?)value;
                            break;
                        case "IsInstruction2":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction2 = (System.Boolean?)value;
                            break;
                        case "IsInstruction3":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction3 = (System.Boolean?)value;
                            break;
                        case "IsInstruction4":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction4 = (System.Boolean?)value;
                            break;
                        case "IsInstruction5":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction5 = (System.Boolean?)value;
                            break;
                        case "IsInstruction6":

                            if (value == null || value is System.Boolean)
                                this.IsInstruction6 = (System.Boolean?)value;
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
        /// Maps to MedicalDischargeSummaryCmx.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ChiefComplaint
        /// </summary>
        virtual public System.String ChiefComplaint
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ChiefComplaint);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ChiefComplaint, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.HistOfPresentIllness
        /// </summary>
        virtual public System.String HistOfPresentIllness
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.HistOfPresentIllness);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.HistOfPresentIllness, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Komorbiditas
        /// </summary>
        virtual public System.String Komorbiditas
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Komorbiditas);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Komorbiditas, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.PhysicalExam
        /// </summary>
        virtual public System.String PhysicalExam
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PhysicalExam);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PhysicalExam, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AncillaryExam
        /// </summary>
        virtual public System.String AncillaryExam
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExam);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExam, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.MedicalProcedures
        /// </summary>
        virtual public System.String MedicalProcedures
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalProcedures);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalProcedures, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Medications
        /// </summary>
        virtual public System.String Medications
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Medications);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Medications, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AdmittingDiagnoseID1
        /// </summary>
        virtual public System.String AdmittingDiagnoseID1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AdmittingDiagnoseName1
        /// </summary>
        virtual public System.String AdmittingDiagnoseName1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AdmittingDiagnoseID2
        /// </summary>
        virtual public System.String AdmittingDiagnoseID2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AdmittingDiagnoseName2
        /// </summary>
        virtual public System.String AdmittingDiagnoseName2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseID1
        /// </summary>
        virtual public System.String FinalDiagnoseID1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseName1
        /// </summary>
        virtual public System.String FinalDiagnoseName1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseID2
        /// </summary>
        virtual public System.String FinalDiagnoseID2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseName2
        /// </summary>
        virtual public System.String FinalDiagnoseName2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseID3
        /// </summary>
        virtual public System.String FinalDiagnoseID3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.FinalDiagnoseName3
        /// </summary>
        virtual public System.String FinalDiagnoseName3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.PresentStatus
        /// </summary>
        virtual public System.String PresentStatus
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PresentStatus);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PresentStatus, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SuggestionFollowUp
        /// </summary>
        virtual public System.String SuggestionFollowUp
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SuggestionFollowUp);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SuggestionFollowUp, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.TreatmentIndications
        /// </summary>
        virtual public System.String TreatmentIndications
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.TreatmentIndications);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.TreatmentIndications, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.PastMedicalHistory
        /// </summary>
        virtual public System.String PastMedicalHistory
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PastMedicalHistory);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.PastMedicalHistory, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ProcedureName
        /// </summary>
        virtual public System.String ProcedureName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.DischargeDate
        /// </summary>
        virtual public System.DateTime? DischargeDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeDate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.DischargeTime
        /// </summary>
        virtual public System.String DischargeTime
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeTime);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ParamedicName
        /// </summary>
        virtual public System.String ParamedicName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRUnitIntended
        /// </summary>
        virtual public System.String SRUnitIntended
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRUnitIntended);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRUnitIntended, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRDischargeMethod
        /// </summary>
        virtual public System.String SRDischargeMethod
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeMethod);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeMethod, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRDischargeCondition
        /// </summary>
        virtual public System.String SRDischargeCondition
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeCondition);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeCondition, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Prognosis
        /// </summary>
        virtual public System.String Prognosis
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Prognosis);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Prognosis, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsRichTextMode
        /// </summary>
        virtual public System.Boolean? IsRichTextMode
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsRichTextMode);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsRichTextMode, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.AncillaryExamOther
        /// </summary>
        virtual public System.String AncillaryExamOther
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExamOther);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExamOther, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Diet
        /// </summary>
        virtual public System.String Diet
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Diet);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Diet, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.DocumentDate
        /// </summary>
        virtual public System.DateTime? DocumentDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.DocumentDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.DocumentDate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.PpaSign
        /// </summary>
        virtual public System.Byte[] PpaSign
        {
            get
            {
                return base.GetSystemByteArray(MedicalDischargeSummaryCmxMetadata.ColumnNames.PpaSign);
            }

            set
            {
                base.SetSystemByteArray(MedicalDischargeSummaryCmxMetadata.ColumnNames.PpaSign, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.PatientSign
        /// </summary>
        virtual public System.Byte[] PatientSign
        {
            get
            {
                return base.GetSystemByteArray(MedicalDischargeSummaryCmxMetadata.ColumnNames.PatientSign);
            }

            set
            {
                base.SetSystemByteArray(MedicalDischargeSummaryCmxMetadata.ColumnNames.PatientSign, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.ControlPlan
        /// </summary>
        virtual public System.String ControlPlan
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ControlPlan);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.ControlPlan, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsApproved, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.HomeCare
        /// </summary>
        virtual public System.String HomeCare
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.HomeCare);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.HomeCare, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.EducationAtHome
        /// </summary>
        virtual public System.String EducationAtHome
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.EducationAtHome);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.EducationAtHome, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Consul
        /// </summary>
        virtual public System.String Consul
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Consul);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Consul, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.MedicalSupport
        /// </summary>
        virtual public System.String MedicalSupport
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalSupport);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalSupport, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.InLocation
        /// </summary>
        virtual public System.String InLocation
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.InLocation);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.InLocation, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.CollectionDateTime
        /// </summary>
        virtual public System.DateTime? CollectionDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.CollectionDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryCmxMetadata.ColumnNames.CollectionDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.InitialDiagnose
        /// </summary>
        virtual public System.String InitialDiagnose
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.InitialDiagnose);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.InitialDiagnose, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRTypeOfService
        /// </summary>
        virtual public System.String SRTypeOfService
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRTypeOfService);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRTypeOfService, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRCauseOfDisease
        /// </summary>
        virtual public System.String SRCauseOfDisease
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDisease);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDisease, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRCauseOfDevelopDisorder
        /// </summary>
        virtual public System.String SRCauseOfDevelopDisorder
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDevelopDisorder);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDevelopDisorder, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.CauseOfDevelopDisorder
        /// </summary>
        virtual public System.String CauseOfDevelopDisorder
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.CauseOfDevelopDisorder);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.CauseOfDevelopDisorder, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.SRNatureOfSurgery
        /// </summary>
        virtual public System.String SRNatureOfSurgery
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRNatureOfSurgery);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRNatureOfSurgery, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction1
        /// </summary>
        virtual public System.Boolean? IsInstruction1
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction1);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction2
        /// </summary>
        virtual public System.Boolean? IsInstruction2
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction2);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction3
        /// </summary>
        virtual public System.Boolean? IsInstruction3
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction3);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Instruction3
        /// </summary>
        virtual public System.String Instruction3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction4
        /// </summary>
        virtual public System.Boolean? IsInstruction4
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction4);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction4, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Instruction4
        /// </summary>
        virtual public System.String Instruction4
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction4);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction4, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction5
        /// </summary>
        virtual public System.Boolean? IsInstruction5
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction5);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction5, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Instruction5
        /// </summary>
        virtual public System.String Instruction5
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction5);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction5, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.IsInstruction6
        /// </summary>
        virtual public System.Boolean? IsInstruction6
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction6);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction6, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryCmx.Instruction6
        /// </summary>
        virtual public System.String Instruction6
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction6);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction6, value);
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
            public esStrings(esMedicalDischargeSummaryCmx entity)
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
            public System.String HistOfPresentIllness
            {
                get
                {
                    System.String data = entity.HistOfPresentIllness;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HistOfPresentIllness = null;
                    else entity.HistOfPresentIllness = Convert.ToString(value);
                }
            }
            public System.String Komorbiditas
            {
                get
                {
                    System.String data = entity.Komorbiditas;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Komorbiditas = null;
                    else entity.Komorbiditas = Convert.ToString(value);
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
            public System.String AncillaryExam
            {
                get
                {
                    System.String data = entity.AncillaryExam;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AncillaryExam = null;
                    else entity.AncillaryExam = Convert.ToString(value);
                }
            }
            public System.String MedicalProcedures
            {
                get
                {
                    System.String data = entity.MedicalProcedures;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalProcedures = null;
                    else entity.MedicalProcedures = Convert.ToString(value);
                }
            }
            public System.String ProcedureID
            {
                get
                {
                    System.String data = entity.ProcedureID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureID = null;
                    else entity.ProcedureID = Convert.ToString(value);
                }
            }
            public System.String Medications
            {
                get
                {
                    System.String data = entity.Medications;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Medications = null;
                    else entity.Medications = Convert.ToString(value);
                }
            }
            public System.String AdmittingDiagnoseID1
            {
                get
                {
                    System.String data = entity.AdmittingDiagnoseID1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdmittingDiagnoseID1 = null;
                    else entity.AdmittingDiagnoseID1 = Convert.ToString(value);
                }
            }
            public System.String AdmittingDiagnoseName1
            {
                get
                {
                    System.String data = entity.AdmittingDiagnoseName1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdmittingDiagnoseName1 = null;
                    else entity.AdmittingDiagnoseName1 = Convert.ToString(value);
                }
            }
            public System.String AdmittingDiagnoseID2
            {
                get
                {
                    System.String data = entity.AdmittingDiagnoseID2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdmittingDiagnoseID2 = null;
                    else entity.AdmittingDiagnoseID2 = Convert.ToString(value);
                }
            }
            public System.String AdmittingDiagnoseName2
            {
                get
                {
                    System.String data = entity.AdmittingDiagnoseName2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AdmittingDiagnoseName2 = null;
                    else entity.AdmittingDiagnoseName2 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseID1
            {
                get
                {
                    System.String data = entity.FinalDiagnoseID1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseID1 = null;
                    else entity.FinalDiagnoseID1 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseName1
            {
                get
                {
                    System.String data = entity.FinalDiagnoseName1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseName1 = null;
                    else entity.FinalDiagnoseName1 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseID2
            {
                get
                {
                    System.String data = entity.FinalDiagnoseID2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseID2 = null;
                    else entity.FinalDiagnoseID2 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseName2
            {
                get
                {
                    System.String data = entity.FinalDiagnoseName2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseName2 = null;
                    else entity.FinalDiagnoseName2 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseID3
            {
                get
                {
                    System.String data = entity.FinalDiagnoseID3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseID3 = null;
                    else entity.FinalDiagnoseID3 = Convert.ToString(value);
                }
            }
            public System.String FinalDiagnoseName3
            {
                get
                {
                    System.String data = entity.FinalDiagnoseName3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalDiagnoseName3 = null;
                    else entity.FinalDiagnoseName3 = Convert.ToString(value);
                }
            }
            public System.String PresentStatus
            {
                get
                {
                    System.String data = entity.PresentStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PresentStatus = null;
                    else entity.PresentStatus = Convert.ToString(value);
                }
            }
            public System.String SuggestionFollowUp
            {
                get
                {
                    System.String data = entity.SuggestionFollowUp;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SuggestionFollowUp = null;
                    else entity.SuggestionFollowUp = Convert.ToString(value);
                }
            }
            public System.String TreatmentIndications
            {
                get
                {
                    System.String data = entity.TreatmentIndications;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TreatmentIndications = null;
                    else entity.TreatmentIndications = Convert.ToString(value);
                }
            }
            public System.String PastMedicalHistory
            {
                get
                {
                    System.String data = entity.PastMedicalHistory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PastMedicalHistory = null;
                    else entity.PastMedicalHistory = Convert.ToString(value);
                }
            }
            public System.String ProcedureName
            {
                get
                {
                    System.String data = entity.ProcedureName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureName = null;
                    else entity.ProcedureName = Convert.ToString(value);
                }
            }
            public System.String DischargeDate
            {
                get
                {
                    System.DateTime? data = entity.DischargeDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DischargeDate = null;
                    else entity.DischargeDate = Convert.ToDateTime(value);
                }
            }
            public System.String DischargeTime
            {
                get
                {
                    System.String data = entity.DischargeTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DischargeTime = null;
                    else entity.DischargeTime = Convert.ToString(value);
                }
            }
            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
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
            public System.String SRUnitIntended
            {
                get
                {
                    System.String data = entity.SRUnitIntended;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRUnitIntended = null;
                    else entity.SRUnitIntended = Convert.ToString(value);
                }
            }
            public System.String SRDischargeMethod
            {
                get
                {
                    System.String data = entity.SRDischargeMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDischargeMethod = null;
                    else entity.SRDischargeMethod = Convert.ToString(value);
                }
            }
            public System.String SRDischargeCondition
            {
                get
                {
                    System.String data = entity.SRDischargeCondition;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDischargeCondition = null;
                    else entity.SRDischargeCondition = Convert.ToString(value);
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
            public System.String IsRichTextMode
            {
                get
                {
                    System.Boolean? data = entity.IsRichTextMode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsRichTextMode = null;
                    else entity.IsRichTextMode = Convert.ToBoolean(value);
                }
            }
            public System.String AncillaryExamOther
            {
                get
                {
                    System.String data = entity.AncillaryExamOther;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AncillaryExamOther = null;
                    else entity.AncillaryExamOther = Convert.ToString(value);
                }
            }
            public System.String Diet
            {
                get
                {
                    System.String data = entity.Diet;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Diet = null;
                    else entity.Diet = Convert.ToString(value);
                }
            }
            public System.String DocumentDate
            {
                get
                {
                    System.DateTime? data = entity.DocumentDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DocumentDate = null;
                    else entity.DocumentDate = Convert.ToDateTime(value);
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
            public System.String HomeCare
            {
                get
                {
                    System.String data = entity.HomeCare;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HomeCare = null;
                    else entity.HomeCare = Convert.ToString(value);
                }
            }
            public System.String EducationAtHome
            {
                get
                {
                    System.String data = entity.EducationAtHome;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EducationAtHome = null;
                    else entity.EducationAtHome = Convert.ToString(value);
                }
            }
            public System.String Consul
            {
                get
                {
                    System.String data = entity.Consul;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Consul = null;
                    else entity.Consul = Convert.ToString(value);
                }
            }
            public System.String MedicalSupport
            {
                get
                {
                    System.String data = entity.MedicalSupport;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalSupport = null;
                    else entity.MedicalSupport = Convert.ToString(value);
                }
            }
            public System.String InLocation
            {
                get
                {
                    System.String data = entity.InLocation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InLocation = null;
                    else entity.InLocation = Convert.ToString(value);
                }
            }
            public System.String CollectionDateTime
            {
                get
                {
                    System.DateTime? data = entity.CollectionDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CollectionDateTime = null;
                    else entity.CollectionDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String InitialDiagnose
            {
                get
                {
                    System.String data = entity.InitialDiagnose;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InitialDiagnose = null;
                    else entity.InitialDiagnose = Convert.ToString(value);
                }
            }
            public System.String SRTypeOfService
            {
                get
                {
                    System.String data = entity.SRTypeOfService;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTypeOfService = null;
                    else entity.SRTypeOfService = Convert.ToString(value);
                }
            }
            public System.String SRCauseOfDisease
            {
                get
                {
                    System.String data = entity.SRCauseOfDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCauseOfDisease = null;
                    else entity.SRCauseOfDisease = Convert.ToString(value);
                }
            }
            public System.String SRCauseOfDevelopDisorder
            {
                get
                {
                    System.String data = entity.SRCauseOfDevelopDisorder;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCauseOfDevelopDisorder = null;
                    else entity.SRCauseOfDevelopDisorder = Convert.ToString(value);
                }
            }
            public System.String CauseOfDevelopDisorder
            {
                get
                {
                    System.String data = entity.CauseOfDevelopDisorder;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CauseOfDevelopDisorder = null;
                    else entity.CauseOfDevelopDisorder = Convert.ToString(value);
                }
            }
            public System.String SRNatureOfSurgery
            {
                get
                {
                    System.String data = entity.SRNatureOfSurgery;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRNatureOfSurgery = null;
                    else entity.SRNatureOfSurgery = Convert.ToString(value);
                }
            }
            public System.String IsInstruction1
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction1 = null;
                    else entity.IsInstruction1 = Convert.ToBoolean(value);
                }
            }
            public System.String IsInstruction2
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction2 = null;
                    else entity.IsInstruction2 = Convert.ToBoolean(value);
                }
            }
            public System.String IsInstruction3
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction3 = null;
                    else entity.IsInstruction3 = Convert.ToBoolean(value);
                }
            }
            public System.String Instruction3
            {
                get
                {
                    System.String data = entity.Instruction3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Instruction3 = null;
                    else entity.Instruction3 = Convert.ToString(value);
                }
            }
            public System.String IsInstruction4
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction4;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction4 = null;
                    else entity.IsInstruction4 = Convert.ToBoolean(value);
                }
            }
            public System.String Instruction4
            {
                get
                {
                    System.String data = entity.Instruction4;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Instruction4 = null;
                    else entity.Instruction4 = Convert.ToString(value);
                }
            }
            public System.String IsInstruction5
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction5;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction5 = null;
                    else entity.IsInstruction5 = Convert.ToBoolean(value);
                }
            }
            public System.String Instruction5
            {
                get
                {
                    System.String data = entity.Instruction5;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Instruction5 = null;
                    else entity.Instruction5 = Convert.ToString(value);
                }
            }
            public System.String IsInstruction6
            {
                get
                {
                    System.Boolean? data = entity.IsInstruction6;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInstruction6 = null;
                    else entity.IsInstruction6 = Convert.ToBoolean(value);
                }
            }
            public System.String Instruction6
            {
                get
                {
                    System.String data = entity.Instruction6;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Instruction6 = null;
                    else entity.Instruction6 = Convert.ToString(value);
                }
            }
            private esMedicalDischargeSummaryCmx entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryCmxQuery query)
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
                throw new Exception("esMedicalDischargeSummaryCmx can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalDischargeSummaryCmx : esMedicalDischargeSummaryCmx
    {
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryCmxQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryCmxMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ChiefComplaint
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ChiefComplaint, esSystemType.String);
            }
        }

        public esQueryItem HistOfPresentIllness
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.HistOfPresentIllness, esSystemType.String);
            }
        }

        public esQueryItem Komorbiditas
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Komorbiditas, esSystemType.String);
            }
        }

        public esQueryItem PhysicalExam
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.PhysicalExam, esSystemType.String);
            }
        }

        public esQueryItem AncillaryExam
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExam, esSystemType.String);
            }
        }

        public esQueryItem MedicalProcedures
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalProcedures, esSystemType.String);
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem Medications
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Medications, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseID1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID1, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseName1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName1, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseID2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID2, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseName2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID1, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName1, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID3, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName3, esSystemType.String);
            }
        }

        public esQueryItem PresentStatus
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.PresentStatus, esSystemType.String);
            }
        }

        public esQueryItem SuggestionFollowUp
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SuggestionFollowUp, esSystemType.String);
            }
        }

        public esQueryItem TreatmentIndications
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.TreatmentIndications, esSystemType.String);
            }
        }

        public esQueryItem PastMedicalHistory
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.PastMedicalHistory, esSystemType.String);
            }
        }

        public esQueryItem ProcedureName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureName, esSystemType.String);
            }
        }

        public esQueryItem DischargeDate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
            }
        }

        public esQueryItem DischargeTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeTime, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicName, esSystemType.String);
            }
        }

        public esQueryItem SRUnitIntended
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRUnitIntended, esSystemType.String);
            }
        }

        public esQueryItem SRDischargeMethod
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeMethod, esSystemType.String);
            }
        }

        public esQueryItem SRDischargeCondition
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeCondition, esSystemType.String);
            }
        }

        public esQueryItem Prognosis
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Prognosis, esSystemType.String);
            }
        }

        public esQueryItem IsRichTextMode
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsRichTextMode, esSystemType.Boolean);
            }
        }

        public esQueryItem AncillaryExamOther
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExamOther, esSystemType.String);
            }
        }

        public esQueryItem Diet
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Diet, esSystemType.String);
            }
        }

        public esQueryItem DocumentDate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PpaSign
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.PpaSign, esSystemType.ByteArray);
            }
        }

        public esQueryItem PatientSign
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.PatientSign, esSystemType.ByteArray);
            }
        }

        public esQueryItem ControlPlan
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.ControlPlan, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem HomeCare
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.HomeCare, esSystemType.String);
            }
        }

        public esQueryItem EducationAtHome
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.EducationAtHome, esSystemType.String);
            }
        }

        public esQueryItem Consul
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Consul, esSystemType.String);
            }
        }

        public esQueryItem MedicalSupport
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalSupport, esSystemType.String);
            }
        }

        public esQueryItem InLocation
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.InLocation, esSystemType.String);
            }
        }

        public esQueryItem CollectionDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.CollectionDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem InitialDiagnose
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.InitialDiagnose, esSystemType.String);
            }
        }

        public esQueryItem SRTypeOfService
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRTypeOfService, esSystemType.String);
            }
        }

        public esQueryItem SRCauseOfDisease
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDisease, esSystemType.String);
            }
        }

        public esQueryItem SRCauseOfDevelopDisorder
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDevelopDisorder, esSystemType.String);
            }
        }

        public esQueryItem CauseOfDevelopDisorder
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.CauseOfDevelopDisorder, esSystemType.String);
            }
        }

        public esQueryItem SRNatureOfSurgery
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.SRNatureOfSurgery, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction1, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInstruction2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction2, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInstruction3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction3, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction3, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction4
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction4, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction4
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction4, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction5
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction5, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction5
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction5, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction6
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction6, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction6
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction6, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalDischargeSummaryCmxCollection")]
    public partial class MedicalDischargeSummaryCmxCollection : esMedicalDischargeSummaryCmxCollection, IEnumerable<MedicalDischargeSummaryCmx>
    {
        public MedicalDischargeSummaryCmxCollection()
        {

        }

        public static implicit operator List<MedicalDischargeSummaryCmx>(MedicalDischargeSummaryCmxCollection coll)
        {
            List<MedicalDischargeSummaryCmx> list = new List<MedicalDischargeSummaryCmx>();

            foreach (MedicalDischargeSummaryCmx emp in coll)
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
                return MedicalDischargeSummaryCmxMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalDischargeSummaryCmx(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalDischargeSummaryCmx();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryCmxQuery();
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
        public bool Load(MedicalDischargeSummaryCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalDischargeSummaryCmx AddNew()
        {
            MedicalDischargeSummaryCmx entity = base.AddNewEntity() as MedicalDischargeSummaryCmx;

            return entity;
        }
        public MedicalDischargeSummaryCmx FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as MedicalDischargeSummaryCmx;
        }

        #region IEnumerable< MedicalDischargeSummaryCmx> Members

        IEnumerator<MedicalDischargeSummaryCmx> IEnumerable<MedicalDischargeSummaryCmx>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalDischargeSummaryCmx;
            }
        }

        #endregion

        private MedicalDischargeSummaryCmxQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalDischargeSummaryCmx' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryCmx ({RegistrationNo})")]
    [Serializable]
    public partial class MedicalDischargeSummaryCmx : esMedicalDischargeSummaryCmx
    {
        public MedicalDischargeSummaryCmx()
        {
        }

        public MedicalDischargeSummaryCmx(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryCmxMetadata.Meta();
            }
        }

        override protected esMedicalDischargeSummaryCmxQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryCmxQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryCmxQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryCmxQuery();
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
        public bool Load(MedicalDischargeSummaryCmxQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalDischargeSummaryCmxQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalDischargeSummaryCmxQuery : esMedicalDischargeSummaryCmxQuery
    {
        public MedicalDischargeSummaryCmxQuery()
        {

        }

        public MedicalDischargeSummaryCmxQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryCmxQuery";
        }
    }

    [Serializable]
    public partial class MedicalDischargeSummaryCmxMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalDischargeSummaryCmxMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ChiefComplaint, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ChiefComplaint;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.HistOfPresentIllness, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.HistOfPresentIllness;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Komorbiditas, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Komorbiditas;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.PhysicalExam, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.PhysicalExam;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExam, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AncillaryExam;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalProcedures, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.MedicalProcedures;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ProcedureID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Medications, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Medications;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID1, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AdmittingDiagnoseID1;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName1, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AdmittingDiagnoseName1;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseID2, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AdmittingDiagnoseID2;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AdmittingDiagnoseName2, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AdmittingDiagnoseName2;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID1, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseID1;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName1, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseName1;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID2, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseID2;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName2, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseName2;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseID3, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseID3;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.FinalDiagnoseName3, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.FinalDiagnoseName3;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.PresentStatus, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.PresentStatus;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SuggestionFollowUp, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SuggestionFollowUp;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.TreatmentIndications, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.TreatmentIndications;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.PastMedicalHistory, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.PastMedicalHistory;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ProcedureName, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ProcedureName;
            c.CharacterMaxLength = 400;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeDate, 26, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.DischargeDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.DischargeTime, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.DischargeTime;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicID, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ParamedicName, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ParamedicName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRUnitIntended, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRUnitIntended;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeMethod, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRDischargeMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRDischargeCondition, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRDischargeCondition;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Prognosis, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Prognosis;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsRichTextMode, 34, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsRichTextMode;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.AncillaryExamOther, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.AncillaryExamOther;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Diet, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Diet;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.DocumentDate, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.DocumentDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.PpaSign, 38, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.PpaSign;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.PatientSign, 39, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.PatientSign;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.ControlPlan, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.ControlPlan;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsApproved, 41, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.HomeCare, 42, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.HomeCare;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.EducationAtHome, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.EducationAtHome;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Consul, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Consul;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.MedicalSupport, 45, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.MedicalSupport;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.InLocation, 46, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.InLocation;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.CollectionDateTime, 47, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.CollectionDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.InitialDiagnose, 48, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.InitialDiagnose;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRTypeOfService, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRTypeOfService;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDisease, 50, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRCauseOfDisease;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRCauseOfDevelopDisorder, 51, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRCauseOfDevelopDisorder;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.CauseOfDevelopDisorder, 52, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.CauseOfDevelopDisorder;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.SRNatureOfSurgery, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.SRNatureOfSurgery;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction1, 54, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction2, 55, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction3, 56, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction3, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Instruction3;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction4, 58, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction4;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction4, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Instruction4;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction5, 60, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction5, 61, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Instruction5;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.IsInstruction6, 62, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.IsInstruction6;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryCmxMetadata.ColumnNames.Instruction6, 63, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryCmxMetadata.PropertyNames.Instruction6;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalDischargeSummaryCmxMetadata Meta()
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
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ChiefComplaint = "ChiefComplaint";
            public const string HistOfPresentIllness = "HistOfPresentIllness";
            public const string Komorbiditas = "Komorbiditas";
            public const string PhysicalExam = "PhysicalExam";
            public const string AncillaryExam = "AncillaryExam";
            public const string MedicalProcedures = "MedicalProcedures";
            public const string ProcedureID = "ProcedureID";
            public const string Medications = "Medications";
            public const string AdmittingDiagnoseID1 = "AdmittingDiagnoseID1";
            public const string AdmittingDiagnoseName1 = "AdmittingDiagnoseName1";
            public const string AdmittingDiagnoseID2 = "AdmittingDiagnoseID2";
            public const string AdmittingDiagnoseName2 = "AdmittingDiagnoseName2";
            public const string FinalDiagnoseID1 = "FinalDiagnoseID1";
            public const string FinalDiagnoseName1 = "FinalDiagnoseName1";
            public const string FinalDiagnoseID2 = "FinalDiagnoseID2";
            public const string FinalDiagnoseName2 = "FinalDiagnoseName2";
            public const string FinalDiagnoseID3 = "FinalDiagnoseID3";
            public const string FinalDiagnoseName3 = "FinalDiagnoseName3";
            public const string PresentStatus = "PresentStatus";
            public const string SuggestionFollowUp = "SuggestionFollowUp";
            public const string TreatmentIndications = "TreatmentIndications";
            public const string PastMedicalHistory = "PastMedicalHistory";
            public const string ProcedureName = "ProcedureName";
            public const string DischargeDate = "DischargeDate";
            public const string DischargeTime = "DischargeTime";
            public const string ParamedicID = "ParamedicID";
            public const string ParamedicName = "ParamedicName";
            public const string SRUnitIntended = "SRUnitIntended";
            public const string SRDischargeMethod = "SRDischargeMethod";
            public const string SRDischargeCondition = "SRDischargeCondition";
            public const string Prognosis = "Prognosis";
            public const string IsRichTextMode = "IsRichTextMode";
            public const string AncillaryExamOther = "AncillaryExamOther";
            public const string Diet = "Diet";
            public const string DocumentDate = "DocumentDate";
            public const string PpaSign = "PpaSign";
            public const string PatientSign = "PatientSign";
            public const string ControlPlan = "ControlPlan";
            public const string IsApproved = "IsApproved";
            public const string HomeCare = "HomeCare";
            public const string EducationAtHome = "EducationAtHome";
            public const string Consul = "Consul";
            public const string MedicalSupport = "MedicalSupport";
            public const string InLocation = "InLocation";
            public const string CollectionDateTime = "CollectionDateTime";
            public const string InitialDiagnose = "InitialDiagnose";
            public const string SRTypeOfService = "SRTypeOfService";
            public const string SRCauseOfDisease = "SRCauseOfDisease";
            public const string SRCauseOfDevelopDisorder = "SRCauseOfDevelopDisorder";
            public const string CauseOfDevelopDisorder = "CauseOfDevelopDisorder";
            public const string SRNatureOfSurgery = "SRNatureOfSurgery";
            public const string IsInstruction1 = "IsInstruction1";
            public const string IsInstruction2 = "IsInstruction2";
            public const string IsInstruction3 = "IsInstruction3";
            public const string Instruction3 = "Instruction3";
            public const string IsInstruction4 = "IsInstruction4";
            public const string Instruction4 = "Instruction4";
            public const string IsInstruction5 = "IsInstruction5";
            public const string Instruction5 = "Instruction5";
            public const string IsInstruction6 = "IsInstruction6";
            public const string Instruction6 = "Instruction6";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ChiefComplaint = "ChiefComplaint";
            public const string HistOfPresentIllness = "HistOfPresentIllness";
            public const string Komorbiditas = "Komorbiditas";
            public const string PhysicalExam = "PhysicalExam";
            public const string AncillaryExam = "AncillaryExam";
            public const string MedicalProcedures = "MedicalProcedures";
            public const string ProcedureID = "ProcedureID";
            public const string Medications = "Medications";
            public const string AdmittingDiagnoseID1 = "AdmittingDiagnoseID1";
            public const string AdmittingDiagnoseName1 = "AdmittingDiagnoseName1";
            public const string AdmittingDiagnoseID2 = "AdmittingDiagnoseID2";
            public const string AdmittingDiagnoseName2 = "AdmittingDiagnoseName2";
            public const string FinalDiagnoseID1 = "FinalDiagnoseID1";
            public const string FinalDiagnoseName1 = "FinalDiagnoseName1";
            public const string FinalDiagnoseID2 = "FinalDiagnoseID2";
            public const string FinalDiagnoseName2 = "FinalDiagnoseName2";
            public const string FinalDiagnoseID3 = "FinalDiagnoseID3";
            public const string FinalDiagnoseName3 = "FinalDiagnoseName3";
            public const string PresentStatus = "PresentStatus";
            public const string SuggestionFollowUp = "SuggestionFollowUp";
            public const string TreatmentIndications = "TreatmentIndications";
            public const string PastMedicalHistory = "PastMedicalHistory";
            public const string ProcedureName = "ProcedureName";
            public const string DischargeDate = "DischargeDate";
            public const string DischargeTime = "DischargeTime";
            public const string ParamedicID = "ParamedicID";
            public const string ParamedicName = "ParamedicName";
            public const string SRUnitIntended = "SRUnitIntended";
            public const string SRDischargeMethod = "SRDischargeMethod";
            public const string SRDischargeCondition = "SRDischargeCondition";
            public const string Prognosis = "Prognosis";
            public const string IsRichTextMode = "IsRichTextMode";
            public const string AncillaryExamOther = "AncillaryExamOther";
            public const string Diet = "Diet";
            public const string DocumentDate = "DocumentDate";
            public const string PpaSign = "PpaSign";
            public const string PatientSign = "PatientSign";
            public const string ControlPlan = "ControlPlan";
            public const string IsApproved = "IsApproved";
            public const string HomeCare = "HomeCare";
            public const string EducationAtHome = "EducationAtHome";
            public const string Consul = "Consul";
            public const string MedicalSupport = "MedicalSupport";
            public const string InLocation = "InLocation";
            public const string CollectionDateTime = "CollectionDateTime";
            public const string InitialDiagnose = "InitialDiagnose";
            public const string SRTypeOfService = "SRTypeOfService";
            public const string SRCauseOfDisease = "SRCauseOfDisease";
            public const string SRCauseOfDevelopDisorder = "SRCauseOfDevelopDisorder";
            public const string CauseOfDevelopDisorder = "CauseOfDevelopDisorder";
            public const string SRNatureOfSurgery = "SRNatureOfSurgery";
            public const string IsInstruction1 = "IsInstruction1";
            public const string IsInstruction2 = "IsInstruction2";
            public const string IsInstruction3 = "IsInstruction3";
            public const string Instruction3 = "Instruction3";
            public const string IsInstruction4 = "IsInstruction4";
            public const string Instruction4 = "Instruction4";
            public const string IsInstruction5 = "IsInstruction5";
            public const string Instruction5 = "Instruction5";
            public const string IsInstruction6 = "IsInstruction6";
            public const string Instruction6 = "Instruction6";
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
            lock (typeof(MedicalDischargeSummaryCmxMetadata))
            {
                if (MedicalDischargeSummaryCmxMetadata.mapDelegates == null)
                {
                    MedicalDischargeSummaryCmxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalDischargeSummaryCmxMetadata.meta == null)
                {
                    MedicalDischargeSummaryCmxMetadata.meta = new MedicalDischargeSummaryCmxMetadata();
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
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChiefComplaint", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HistOfPresentIllness", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Komorbiditas", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhysicalExam", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AncillaryExam", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicalProcedures", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Medications", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AdmittingDiagnoseID1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AdmittingDiagnoseName1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AdmittingDiagnoseID2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AdmittingDiagnoseName2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseID1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseName1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseID2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseName2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseID3", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalDiagnoseName3", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PresentStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SuggestionFollowUp", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TreatmentIndications", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PastMedicalHistory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DischargeDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("DischargeTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRUnitIntended", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDischargeMethod", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDischargeCondition", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Prognosis", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsRichTextMode", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AncillaryExamOther", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Diet", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DocumentDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PpaSign", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("PatientSign", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("ControlPlan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("HomeCare", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EducationAtHome", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Consul", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicalSupport", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InLocation", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CollectionDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("InitialDiagnose", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTypeOfService", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCauseOfDisease", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCauseOfDevelopDisorder", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CauseOfDevelopDisorder", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRNatureOfSurgery", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInstruction1", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsInstruction2", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsInstruction3", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Instruction3", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInstruction4", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Instruction4", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInstruction5", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Instruction5", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsInstruction6", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Instruction6", new esTypeMap("varchar", "System.String"));


                meta.Source = "MedicalDischargeSummaryCmx";
                meta.Destination = "MedicalDischargeSummaryCmx";
                meta.spInsert = "proc_MedicalDischargeSummaryCmxInsert";
                meta.spUpdate = "proc_MedicalDischargeSummaryCmxUpdate";
                meta.spDelete = "proc_MedicalDischargeSummaryCmxDelete";
                meta.spLoadAll = "proc_MedicalDischargeSummaryCmxLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryCmxLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalDischargeSummaryCmxMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
