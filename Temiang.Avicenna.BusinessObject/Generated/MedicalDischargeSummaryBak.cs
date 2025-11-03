/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 04/04/2024 21:34:05
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
    abstract public class esMedicalDischargeSummaryBakCollection : esEntityCollectionWAuditLog
    {
        public esMedicalDischargeSummaryBakCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicalDischargeSummaryBakCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryBakQuery query)
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
            this.InitQuery(query as esMedicalDischargeSummaryBakQuery);
        }
        #endregion

        virtual public MedicalDischargeSummaryBak DetachEntity(MedicalDischargeSummaryBak entity)
        {
            return base.DetachEntity(entity) as MedicalDischargeSummaryBak;
        }

        virtual public MedicalDischargeSummaryBak AttachEntity(MedicalDischargeSummaryBak entity)
        {
            return base.AttachEntity(entity) as MedicalDischargeSummaryBak;
        }

        virtual public void Combine(MedicalDischargeSummaryBakCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalDischargeSummaryBak this[int index]
        {
            get
            {
                return base[index] as MedicalDischargeSummaryBak;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalDischargeSummaryBak);
        }
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryBak : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalDischargeSummaryBakQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalDischargeSummaryBak()
        {
        }

        public esMedicalDischargeSummaryBak(DataRow row)
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
            esMedicalDischargeSummaryBakQuery query = this.GetDynamicQuery();
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
        /// Maps to MedicalDischargeSummaryBak.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ChiefComplaint
        /// </summary>
        virtual public System.String ChiefComplaint
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ChiefComplaint);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ChiefComplaint, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.HistOfPresentIllness
        /// </summary>
        virtual public System.String HistOfPresentIllness
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.HistOfPresentIllness);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.HistOfPresentIllness, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Komorbiditas
        /// </summary>
        virtual public System.String Komorbiditas
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Komorbiditas);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Komorbiditas, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.PhysicalExam
        /// </summary>
        virtual public System.String PhysicalExam
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PhysicalExam);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PhysicalExam, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AncillaryExam
        /// </summary>
        virtual public System.String AncillaryExam
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExam);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExam, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.MedicalProcedures
        /// </summary>
        virtual public System.String MedicalProcedures
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalProcedures);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalProcedures, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Medications
        /// </summary>
        virtual public System.String Medications
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Medications);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Medications, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AdmittingDiagnoseID1
        /// </summary>
        virtual public System.String AdmittingDiagnoseID1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AdmittingDiagnoseName1
        /// </summary>
        virtual public System.String AdmittingDiagnoseName1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AdmittingDiagnoseID2
        /// </summary>
        virtual public System.String AdmittingDiagnoseID2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AdmittingDiagnoseName2
        /// </summary>
        virtual public System.String AdmittingDiagnoseName2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseID1
        /// </summary>
        virtual public System.String FinalDiagnoseID1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseName1
        /// </summary>
        virtual public System.String FinalDiagnoseName1
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName1);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseID2
        /// </summary>
        virtual public System.String FinalDiagnoseID2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseName2
        /// </summary>
        virtual public System.String FinalDiagnoseName2
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName2);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseID3
        /// </summary>
        virtual public System.String FinalDiagnoseID3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.FinalDiagnoseName3
        /// </summary>
        virtual public System.String FinalDiagnoseName3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.PresentStatus
        /// </summary>
        virtual public System.String PresentStatus
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PresentStatus);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PresentStatus, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SuggestionFollowUp
        /// </summary>
        virtual public System.String SuggestionFollowUp
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SuggestionFollowUp);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SuggestionFollowUp, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.TreatmentIndications
        /// </summary>
        virtual public System.String TreatmentIndications
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.TreatmentIndications);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.TreatmentIndications, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.PastMedicalHistory
        /// </summary>
        virtual public System.String PastMedicalHistory
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PastMedicalHistory);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.PastMedicalHistory, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ProcedureName
        /// </summary>
        virtual public System.String ProcedureName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.DischargeDate
        /// </summary>
        virtual public System.DateTime? DischargeDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeDate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.DischargeTime
        /// </summary>
        virtual public System.String DischargeTime
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeTime);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ParamedicName
        /// </summary>
        virtual public System.String ParamedicName
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicName);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicName, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRUnitIntended
        /// </summary>
        virtual public System.String SRUnitIntended
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRUnitIntended);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRUnitIntended, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRDischargeMethod
        /// </summary>
        virtual public System.String SRDischargeMethod
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeMethod);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeMethod, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRDischargeCondition
        /// </summary>
        virtual public System.String SRDischargeCondition
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeCondition);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeCondition, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Prognosis
        /// </summary>
        virtual public System.String Prognosis
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Prognosis);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Prognosis, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsRichTextMode
        /// </summary>
        virtual public System.Boolean? IsRichTextMode
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsRichTextMode);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsRichTextMode, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.AncillaryExamOther
        /// </summary>
        virtual public System.String AncillaryExamOther
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExamOther);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExamOther, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Diet
        /// </summary>
        virtual public System.String Diet
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Diet);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Diet, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.DocumentDate
        /// </summary>
        virtual public System.DateTime? DocumentDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.DocumentDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.DocumentDate, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.PpaSign
        /// </summary>
        virtual public System.Byte[] PpaSign
        {
            get
            {
                return base.GetSystemByteArray(MedicalDischargeSummaryBakMetadata.ColumnNames.PpaSign);
            }

            set
            {
                base.SetSystemByteArray(MedicalDischargeSummaryBakMetadata.ColumnNames.PpaSign, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.ControlPlan
        /// </summary>
        virtual public System.String ControlPlan
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ControlPlan);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.ControlPlan, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.HomeCare
        /// </summary>
        virtual public System.String HomeCare
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.HomeCare);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.HomeCare, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.EducationAtHome
        /// </summary>
        virtual public System.String EducationAtHome
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.EducationAtHome);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.EducationAtHome, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Consul
        /// </summary>
        virtual public System.String Consul
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Consul);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Consul, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.MedicalSupport
        /// </summary>
        virtual public System.String MedicalSupport
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalSupport);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalSupport, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.InLocation
        /// </summary>
        virtual public System.String InLocation
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.InLocation);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.InLocation, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.CollectionDateTime
        /// </summary>
        virtual public System.DateTime? CollectionDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.CollectionDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalDischargeSummaryBakMetadata.ColumnNames.CollectionDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.InitialDiagnose
        /// </summary>
        virtual public System.String InitialDiagnose
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.InitialDiagnose);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.InitialDiagnose, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRTypeOfService
        /// </summary>
        virtual public System.String SRTypeOfService
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRTypeOfService);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRTypeOfService, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRCauseOfDisease
        /// </summary>
        virtual public System.String SRCauseOfDisease
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDisease);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDisease, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRCauseOfDevelopDisorder
        /// </summary>
        virtual public System.String SRCauseOfDevelopDisorder
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDevelopDisorder);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDevelopDisorder, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.CauseOfDevelopDisorder
        /// </summary>
        virtual public System.String CauseOfDevelopDisorder
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.CauseOfDevelopDisorder);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.CauseOfDevelopDisorder, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.SRNatureOfSurgery
        /// </summary>
        virtual public System.String SRNatureOfSurgery
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRNatureOfSurgery);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.SRNatureOfSurgery, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction1
        /// </summary>
        virtual public System.Boolean? IsInstruction1
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction1);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction1, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction2
        /// </summary>
        virtual public System.Boolean? IsInstruction2
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction2);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction2, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction3
        /// </summary>
        virtual public System.Boolean? IsInstruction3
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction3);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Instruction3
        /// </summary>
        virtual public System.String Instruction3
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction3);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction3, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction4
        /// </summary>
        virtual public System.Boolean? IsInstruction4
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction4);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction4, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Instruction4
        /// </summary>
        virtual public System.String Instruction4
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction4);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction4, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction5
        /// </summary>
        virtual public System.Boolean? IsInstruction5
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction5);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction5, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Instruction5
        /// </summary>
        virtual public System.String Instruction5
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction5);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction5, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.IsInstruction6
        /// </summary>
        virtual public System.Boolean? IsInstruction6
        {
            get
            {
                return base.GetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction6);
            }

            set
            {
                base.SetSystemBoolean(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction6, value);
            }
        }
        /// <summary>
        /// Maps to MedicalDischargeSummaryBak.Instruction6
        /// </summary>
        virtual public System.String Instruction6
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction6);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction6, value);
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
            public esStrings(esMedicalDischargeSummaryBak entity)
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
            private esMedicalDischargeSummaryBak entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalDischargeSummaryBakQuery query)
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
                throw new Exception("esMedicalDischargeSummaryBak can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicalDischargeSummaryBak : esMedicalDischargeSummaryBak
    {
    }

    [Serializable]
    abstract public class esMedicalDischargeSummaryBakQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryBakMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ChiefComplaint
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ChiefComplaint, esSystemType.String);
            }
        }

        public esQueryItem HistOfPresentIllness
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.HistOfPresentIllness, esSystemType.String);
            }
        }

        public esQueryItem Komorbiditas
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Komorbiditas, esSystemType.String);
            }
        }

        public esQueryItem PhysicalExam
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.PhysicalExam, esSystemType.String);
            }
        }

        public esQueryItem AncillaryExam
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExam, esSystemType.String);
            }
        }

        public esQueryItem MedicalProcedures
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalProcedures, esSystemType.String);
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem Medications
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Medications, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseID1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID1, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseName1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName1, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseID2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID2, esSystemType.String);
            }
        }

        public esQueryItem AdmittingDiagnoseName2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID1, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName1, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName2, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseID3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID3, esSystemType.String);
            }
        }

        public esQueryItem FinalDiagnoseName3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName3, esSystemType.String);
            }
        }

        public esQueryItem PresentStatus
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.PresentStatus, esSystemType.String);
            }
        }

        public esQueryItem SuggestionFollowUp
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SuggestionFollowUp, esSystemType.String);
            }
        }

        public esQueryItem TreatmentIndications
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.TreatmentIndications, esSystemType.String);
            }
        }

        public esQueryItem PastMedicalHistory
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.PastMedicalHistory, esSystemType.String);
            }
        }

        public esQueryItem ProcedureName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureName, esSystemType.String);
            }
        }

        public esQueryItem DischargeDate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
            }
        }

        public esQueryItem DischargeTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeTime, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicName
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicName, esSystemType.String);
            }
        }

        public esQueryItem SRUnitIntended
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRUnitIntended, esSystemType.String);
            }
        }

        public esQueryItem SRDischargeMethod
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeMethod, esSystemType.String);
            }
        }

        public esQueryItem SRDischargeCondition
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeCondition, esSystemType.String);
            }
        }

        public esQueryItem Prognosis
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Prognosis, esSystemType.String);
            }
        }

        public esQueryItem IsRichTextMode
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsRichTextMode, esSystemType.Boolean);
            }
        }

        public esQueryItem AncillaryExamOther
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExamOther, esSystemType.String);
            }
        }

        public esQueryItem Diet
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Diet, esSystemType.String);
            }
        }

        public esQueryItem DocumentDate
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
            }
        }

        public esQueryItem PpaSign
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.PpaSign, esSystemType.ByteArray);
            }
        }

        public esQueryItem ControlPlan
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.ControlPlan, esSystemType.String);
            }
        }

        public esQueryItem HomeCare
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.HomeCare, esSystemType.String);
            }
        }

        public esQueryItem EducationAtHome
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.EducationAtHome, esSystemType.String);
            }
        }

        public esQueryItem Consul
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Consul, esSystemType.String);
            }
        }

        public esQueryItem MedicalSupport
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalSupport, esSystemType.String);
            }
        }

        public esQueryItem InLocation
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.InLocation, esSystemType.String);
            }
        }

        public esQueryItem CollectionDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.CollectionDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem InitialDiagnose
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.InitialDiagnose, esSystemType.String);
            }
        }

        public esQueryItem SRTypeOfService
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRTypeOfService, esSystemType.String);
            }
        }

        public esQueryItem SRCauseOfDisease
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDisease, esSystemType.String);
            }
        }

        public esQueryItem SRCauseOfDevelopDisorder
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDevelopDisorder, esSystemType.String);
            }
        }

        public esQueryItem CauseOfDevelopDisorder
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.CauseOfDevelopDisorder, esSystemType.String);
            }
        }

        public esQueryItem SRNatureOfSurgery
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.SRNatureOfSurgery, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction1
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction1, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInstruction2
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction2, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInstruction3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction3, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction3
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction3, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction4
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction4, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction4
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction4, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction5
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction5, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction5
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction5, esSystemType.String);
            }
        }

        public esQueryItem IsInstruction6
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction6, esSystemType.Boolean);
            }
        }

        public esQueryItem Instruction6
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction6, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalDischargeSummaryBakCollection")]
    public partial class MedicalDischargeSummaryBakCollection : esMedicalDischargeSummaryBakCollection, IEnumerable<MedicalDischargeSummaryBak>
    {
        public MedicalDischargeSummaryBakCollection()
        {

        }

        public static implicit operator List<MedicalDischargeSummaryBak>(MedicalDischargeSummaryBakCollection coll)
        {
            List<MedicalDischargeSummaryBak> list = new List<MedicalDischargeSummaryBak>();

            foreach (MedicalDischargeSummaryBak emp in coll)
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
                return MedicalDischargeSummaryBakMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryBakQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalDischargeSummaryBak(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalDischargeSummaryBak();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryBakQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryBakQuery();
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
        public bool Load(MedicalDischargeSummaryBakQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicalDischargeSummaryBak AddNew()
        {
            MedicalDischargeSummaryBak entity = base.AddNewEntity() as MedicalDischargeSummaryBak;

            return entity;
        }
        public MedicalDischargeSummaryBak FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as MedicalDischargeSummaryBak;
        }

        #region IEnumerable< MedicalDischargeSummaryBak> Members

        IEnumerator<MedicalDischargeSummaryBak> IEnumerable<MedicalDischargeSummaryBak>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalDischargeSummaryBak;
            }
        }

        #endregion

        private MedicalDischargeSummaryBakQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalDischargeSummaryBak' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryBak ({RegistrationNo})")]
    [Serializable]
    public partial class MedicalDischargeSummaryBak : esMedicalDischargeSummaryBak
    {
        public MedicalDischargeSummaryBak()
        {
        }

        public MedicalDischargeSummaryBak(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalDischargeSummaryBakMetadata.Meta();
            }
        }

        override protected esMedicalDischargeSummaryBakQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalDischargeSummaryBakQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicalDischargeSummaryBakQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalDischargeSummaryBakQuery();
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
        public bool Load(MedicalDischargeSummaryBakQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalDischargeSummaryBakQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicalDischargeSummaryBakQuery : esMedicalDischargeSummaryBakQuery
    {
        public MedicalDischargeSummaryBakQuery()
        {

        }

        public MedicalDischargeSummaryBakQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryBakQuery";
        }
    }

    [Serializable]
    public partial class MedicalDischargeSummaryBakMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalDischargeSummaryBakMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ChiefComplaint, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ChiefComplaint;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.HistOfPresentIllness, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.HistOfPresentIllness;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Komorbiditas, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Komorbiditas;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.PhysicalExam, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.PhysicalExam;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExam, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AncillaryExam;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalProcedures, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.MedicalProcedures;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ProcedureID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Medications, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Medications;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID1, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AdmittingDiagnoseID1;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName1, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AdmittingDiagnoseName1;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseID2, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AdmittingDiagnoseID2;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AdmittingDiagnoseName2, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AdmittingDiagnoseName2;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID1, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseID1;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName1, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseName1;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID2, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseID2;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName2, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseName2;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseID3, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseID3;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.FinalDiagnoseName3, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.FinalDiagnoseName3;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.PresentStatus, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.PresentStatus;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SuggestionFollowUp, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SuggestionFollowUp;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.TreatmentIndications, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.TreatmentIndications;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.PastMedicalHistory, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.PastMedicalHistory;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ProcedureName, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ProcedureName;
            c.CharacterMaxLength = 400;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeDate, 26, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.DischargeDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.DischargeTime, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.DischargeTime;
            c.CharacterMaxLength = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicID, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ParamedicName, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ParamedicName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRUnitIntended, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRUnitIntended;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeMethod, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRDischargeMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRDischargeCondition, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRDischargeCondition;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Prognosis, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Prognosis;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsRichTextMode, 34, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsRichTextMode;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.AncillaryExamOther, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.AncillaryExamOther;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Diet, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Diet;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.DocumentDate, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.DocumentDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.PpaSign, 38, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.PpaSign;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.ControlPlan, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.ControlPlan;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.HomeCare, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.HomeCare;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.EducationAtHome, 41, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.EducationAtHome;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Consul, 42, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Consul;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.MedicalSupport, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.MedicalSupport;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.InLocation, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.InLocation;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.CollectionDateTime, 45, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.CollectionDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.InitialDiagnose, 46, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.InitialDiagnose;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRTypeOfService, 47, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRTypeOfService;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDisease, 48, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRCauseOfDisease;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRCauseOfDevelopDisorder, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRCauseOfDevelopDisorder;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.CauseOfDevelopDisorder, 50, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.CauseOfDevelopDisorder;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.SRNatureOfSurgery, 51, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.SRNatureOfSurgery;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction1, 52, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction2, 53, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction3, 54, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction3, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Instruction3;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction4, 56, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction4;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction4, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Instruction4;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction5, 58, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction5, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Instruction5;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.IsInstruction6, 60, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.IsInstruction6;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryBakMetadata.ColumnNames.Instruction6, 61, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryBakMetadata.PropertyNames.Instruction6;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicalDischargeSummaryBakMetadata Meta()
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
            public const string ControlPlan = "ControlPlan";
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
            public const string ControlPlan = "ControlPlan";
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
            lock (typeof(MedicalDischargeSummaryBakMetadata))
            {
                if (MedicalDischargeSummaryBakMetadata.mapDelegates == null)
                {
                    MedicalDischargeSummaryBakMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalDischargeSummaryBakMetadata.meta == null)
                {
                    MedicalDischargeSummaryBakMetadata.meta = new MedicalDischargeSummaryBakMetadata();
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
                meta.AddTypeMap("ControlPlan", new esTypeMap("varchar", "System.String"));
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


                meta.Source = "MedicalDischargeSummaryBak";
                meta.Destination = "MedicalDischargeSummaryBak";
                meta.spInsert = "proc_MedicalDischargeSummaryBakInsert";
                meta.spUpdate = "proc_MedicalDischargeSummaryBakUpdate";
                meta.spDelete = "proc_MedicalDischargeSummaryBakDelete";
                meta.spLoadAll = "proc_MedicalDischargeSummaryBakLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryBakLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalDischargeSummaryBakMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
