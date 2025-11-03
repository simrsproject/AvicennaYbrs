/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/5/2023 12:20:16 PM
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
	abstract public class esRegistrationCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationQuery query)
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
			this.InitQuery(query as esRegistrationQuery);
		}
		#endregion

		virtual public Registration DetachEntity(Registration entity)
		{
			return base.DetachEntity(entity) as Registration;
		}

		virtual public Registration AttachEntity(Registration entity)
		{
			return base.AttachEntity(entity) as Registration;
		}

		virtual public void Combine(RegistrationCollection collection)
		{
			base.Combine(collection);
		}

		new public Registration this[int index]
		{
			get
			{
				return base[index] as Registration;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Registration);
		}
	}

	[Serializable]
	abstract public class esRegistration : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistration()
		{
		}

		public esRegistration(DataRow row)
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
			esRegistrationQuery query = this.GetDynamicQuery();
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
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "RegistrationDate": this.str.RegistrationDate = (string)value; break;
						case "RegistrationTime": this.str.RegistrationTime = (string)value; break;
						case "AppointmentNo": this.str.AppointmentNo = (string)value; break;
						case "AgeInYear": this.str.AgeInYear = (string)value; break;
						case "AgeInMonth": this.str.AgeInMonth = (string)value; break;
						case "AgeInDay": this.str.AgeInDay = (string)value; break;
						case "SRShift": this.str.SRShift = (string)value; break;
						case "SRPatientInType": this.str.SRPatientInType = (string)value; break;
						case "InsuranceID": this.str.InsuranceID = (string)value; break;
						case "SRPatientCategory": this.str.SRPatientCategory = (string)value; break;
						case "SRERCaseType": this.str.SRERCaseType = (string)value; break;
						case "SRVisitReason": this.str.SRVisitReason = (string)value; break;
						case "SRBussinesMethod": this.str.SRBussinesMethod = (string)value; break;
						case "PlavonAmount": this.str.PlavonAmount = (string)value; break;
						case "DepartmentID": this.str.DepartmentID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
						case "CoverageClassID": this.str.CoverageClassID = (string)value; break;
						case "VisitTypeID": this.str.VisitTypeID = (string)value; break;
						case "ReferralID": this.str.ReferralID = (string)value; break;
						case "Anamnesis": this.str.Anamnesis = (string)value; break;
						case "Complaint": this.str.Complaint = (string)value; break;
						case "InitialDiagnose": this.str.InitialDiagnose = (string)value; break;
						case "MedicationPlanning": this.str.MedicationPlanning = (string)value; break;
						case "SRTriage": this.str.SRTriage = (string)value; break;
						case "IsPrintingPatientCard": this.str.IsPrintingPatientCard = (string)value; break;
						case "DischargeDate": this.str.DischargeDate = (string)value; break;
						case "DischargeTime": this.str.DischargeTime = (string)value; break;
						case "DischargeMedicalNotes": this.str.DischargeMedicalNotes = (string)value; break;
						case "DischargeNotes": this.str.DischargeNotes = (string)value; break;
						case "SRDischargeCondition": this.str.SRDischargeCondition = (string)value; break;
						case "SRDischargeMethod": this.str.SRDischargeMethod = (string)value; break;
						case "LOSInYear": this.str.LOSInYear = (string)value; break;
						case "LOSInMonth": this.str.LOSInMonth = (string)value; break;
						case "LOSInDay": this.str.LOSInDay = (string)value; break;
						case "DischargeOperatorID": this.str.DischargeOperatorID = (string)value; break;
						case "AccountNo": this.str.AccountNo = (string)value; break;
						case "TransactionAmount": this.str.TransactionAmount = (string)value; break;
						case "AdministrationAmount": this.str.AdministrationAmount = (string)value; break;
						case "RoundingAmount": this.str.RoundingAmount = (string)value; break;
						case "RemainingAmount": this.str.RemainingAmount = (string)value; break;
						case "IsTransferedToInpatient": this.str.IsTransferedToInpatient = (string)value; break;
						case "IsNewPatient": this.str.IsNewPatient = (string)value; break;
						case "IsNewBornInfant": this.str.IsNewBornInfant = (string)value; break;
						case "IsParturition": this.str.IsParturition = (string)value; break;
						case "IsHoldTransactionEntry": this.str.IsHoldTransactionEntry = (string)value; break;
						case "IsHasCorrection": this.str.IsHasCorrection = (string)value; break;
						case "IsEMRValid": this.str.IsEMRValid = (string)value; break;
						case "IsBackDate": this.str.IsBackDate = (string)value; break;
						case "ActualVisitDate": this.str.ActualVisitDate = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "SRVoidReason": this.str.SRVoidReason = (string)value; break;
						case "VoidNotes": this.str.VoidNotes = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "IsEpisodeComplete": this.str.IsEpisodeComplete = (string)value; break;
						case "IsClusterAssessment": this.str.IsClusterAssessment = (string)value; break;
						case "IsConsul": this.str.IsConsul = (string)value; break;
						case "IsFromDispensary": this.str.IsFromDispensary = (string)value; break;
						case "IsNewVisit": this.str.IsNewVisit = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
						case "LastCreateUserID": this.str.LastCreateUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsDirectPrescriptionReturn": this.str.IsDirectPrescriptionReturn = (string)value; break;
						case "RegistrationQue": this.str.RegistrationQue = (string)value; break;
						case "VisiteRegistrationNo": this.str.VisiteRegistrationNo = (string)value; break;
						case "IsGenerateHL7": this.str.IsGenerateHL7 = (string)value; break;
						case "ReferralName": this.str.ReferralName = (string)value; break;
						case "IsObservation": this.str.IsObservation = (string)value; break;
						case "CauseOfAccident": this.str.CauseOfAccident = (string)value; break;
						case "ReferTo": this.str.ReferTo = (string)value; break;
						case "IsOldCase": this.str.IsOldCase = (string)value; break;
						case "IsDHF": this.str.IsDHF = (string)value; break;
						case "IsEKG": this.str.IsEKG = (string)value; break;
						case "EmrDiagnoseID": this.str.EmrDiagnoseID = (string)value; break;
						case "IsGlobalPlafond": this.str.IsGlobalPlafond = (string)value; break;
						case "FirstResponDate": this.str.FirstResponDate = (string)value; break;
						case "FirstResponTime": this.str.FirstResponTime = (string)value; break;
						case "PhysicianResponDate": this.str.PhysicianResponDate = (string)value; break;
						case "PhysicianResponTime": this.str.PhysicianResponTime = (string)value; break;
						case "IsRoomIn": this.str.IsRoomIn = (string)value; break;
						case "IsLockVerifiedBilling": this.str.IsLockVerifiedBilling = (string)value; break;
						case "LockVerifiedBillingDateTime": this.str.LockVerifiedBillingDateTime = (string)value; break;
						case "LockVerifiedBillingByUserID": this.str.LockVerifiedBillingByUserID = (string)value; break;
						case "ProcedureChargeClassID": this.str.ProcedureChargeClassID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;
						case "SREmployeeRelationship": this.str.SREmployeeRelationship = (string)value; break;
						case "GuarantorCardNo": this.str.GuarantorCardNo = (string)value; break;
						case "DischargePlanDate": this.str.DischargePlanDate = (string)value; break;
						case "UsertInsertDischargePlan": this.str.UsertInsertDischargePlan = (string)value; break;
						case "IsNonPatient": this.str.IsNonPatient = (string)value; break;
						case "ReasonsForTreatmentID": this.str.ReasonsForTreatmentID = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
						case "PatientAdm": this.str.PatientAdm = (string)value; break;
						case "GuarantorAdm": this.str.GuarantorAdm = (string)value; break;
						case "ReasonsForTreatmentDescID": this.str.ReasonsForTreatmentDescID = (string)value; break;
						case "SRReferralGroup": this.str.SRReferralGroup = (string)value; break;
						case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
						case "PhysicianSenders": this.str.PhysicianSenders = (string)value; break;
						case "DiscAdmPatient": this.str.DiscAdmPatient = (string)value; break;
						case "DiscAdmGuarantor": this.str.DiscAdmGuarantor = (string)value; break;
						case "SRPatientInCondition": this.str.SRPatientInCondition = (string)value; break;
						case "SRKiaCaseType": this.str.SRKiaCaseType = (string)value; break;
						case "SRObstetricType": this.str.SRObstetricType = (string)value; break;
						case "IsHoldTransactionEntryByUserID": this.str.IsHoldTransactionEntryByUserID = (string)value; break;
						case "FromRegistrationNo": this.str.FromRegistrationNo = (string)value; break;
						case "IsConfirmedAttendance": this.str.IsConfirmedAttendance = (string)value; break;
						case "ConfirmedAttendanceByUserID": this.str.ConfirmedAttendanceByUserID = (string)value; break;
						case "ConfirmedAttendanceDateTime": this.str.ConfirmedAttendanceDateTime = (string)value; break;
						case "BpjsSepNo": this.str.BpjsSepNo = (string)value; break;
						case "PlavonAmount2": this.str.PlavonAmount2 = (string)value; break;
						case "DeathCertificateNo": this.str.DeathCertificateNo = (string)value; break;
						case "BpjsCoverageFormula": this.str.BpjsCoverageFormula = (string)value; break;
						case "BpjsPackageID": this.str.BpjsPackageID = (string)value; break;
						case "ApproximatePlafondAmount": this.str.ApproximatePlafondAmount = (string)value; break;
						case "SentToBillingDateTime": this.str.SentToBillingDateTime = (string)value; break;
						case "SentToBillingByUserID": this.str.SentToBillingByUserID = (string)value; break;
						case "IsAdjusted": this.str.IsAdjusted = (string)value; break;
						case "AdjustLog": this.str.AdjustLog = (string)value; break;
						case "IsAllowPatientCheckOut": this.str.IsAllowPatientCheckOut = (string)value; break;
						case "AllowPatientCheckOutDateTime": this.str.AllowPatientCheckOutDateTime = (string)value; break;
						case "AllowPatientCheckOutByUserID": this.str.AllowPatientCheckOutByUserID = (string)value; break;
						case "ReferByParamedicID": this.str.ReferByParamedicID = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "SROccupation": this.str.SROccupation = (string)value; break;
						case "SRRelationshipQuality": this.str.SRRelationshipQuality = (string)value; break;
						case "SRResidentialHome": this.str.SRResidentialHome = (string)value; break;
						case "SRFatherOccupation": this.str.SRFatherOccupation = (string)value; break;
						case "IsPregnant": this.str.IsPregnant = (string)value; break;
						case "GestationalAge": this.str.GestationalAge = (string)value; break;
						case "IsBreastFeeding": this.str.IsBreastFeeding = (string)value; break;
						case "AgeOfBabyInYear": this.str.AgeOfBabyInYear = (string)value; break;
						case "AgeOfBabyInMonth": this.str.AgeOfBabyInMonth = (string)value; break;
						case "AgeOfBabyInDay": this.str.AgeOfBabyInDay = (string)value; break;
						case "IsKidneyFunctionImpaired": this.str.IsKidneyFunctionImpaired = (string)value; break;
						case "CreatinineSerumValue": this.str.CreatinineSerumValue = (string)value; break;
						case "Hpi": this.str.Hpi = (string)value; break;
						case "MembershipDetailID": this.str.MembershipDetailID = (string)value; break;
						case "ExternalQueNo": this.str.ExternalQueNo = (string)value; break;
						case "ReferralIdTo": this.str.ReferralIdTo = (string)value; break;
						case "ReferralNameTo": this.str.ReferralNameTo = (string)value; break;
						case "IsReconcile": this.str.IsReconcile = (string)value; break;
						case "IsSkipAutoBill": this.str.IsSkipAutoBill = (string)value; break;
						case "SRCrashSite": this.str.SRCrashSite = (string)value; break;
						case "CrashSiteDetail": this.str.CrashSiteDetail = (string)value; break;
						case "MembershipNo": this.str.MembershipNo = (string)value; break;
						case "IsOpenEntryMR": this.str.IsOpenEntryMR = (string)value; break;
						case "SRCovidStatus": this.str.SRCovidStatus = (string)value; break;
						case "VoucherNo": this.str.VoucherNo = (string)value; break;
						case "SRCovidComorbidStatus": this.str.SRCovidComorbidStatus = (string)value; break;
						case "IsDisability": this.str.IsDisability = (string)value; break;
						case "IsTracer": this.str.IsTracer = (string)value; break;
						case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
						case "SRPatientRiskStatus": this.str.SRPatientRiskStatus = (string)value; break;
						case "IsFinishedAttendance": this.str.IsFinishedAttendance = (string)value; break;
						case "FinishedAttendanceByUserID": this.str.FinishedAttendanceByUserID = (string)value; break;
						case "FinishedAttendanceDateTime": this.str.FinishedAttendanceDateTime = (string)value; break;
                        case "SRPatientRiskColor": this.str.SRPatientRiskColor = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{
						case "RegistrationDate":

							if (value == null || value is System.DateTime)
								this.RegistrationDate = (System.DateTime?)value;
							break;
						case "AgeInYear":

							if (value == null || value is System.Byte)
								this.AgeInYear = (System.Byte?)value;
							break;
						case "AgeInMonth":

							if (value == null || value is System.Byte)
								this.AgeInMonth = (System.Byte?)value;
							break;
						case "AgeInDay":

							if (value == null || value is System.Byte)
								this.AgeInDay = (System.Byte?)value;
							break;
						case "PlavonAmount":

							if (value == null || value is System.Decimal)
								this.PlavonAmount = (System.Decimal?)value;
							break;
						case "IsPrintingPatientCard":

							if (value == null || value is System.Boolean)
								this.IsPrintingPatientCard = (System.Boolean?)value;
							break;
						case "DischargeDate":

							if (value == null || value is System.DateTime)
								this.DischargeDate = (System.DateTime?)value;
							break;
						case "LOSInYear":

							if (value == null || value is System.Byte)
								this.LOSInYear = (System.Byte?)value;
							break;
						case "LOSInMonth":

							if (value == null || value is System.Byte)
								this.LOSInMonth = (System.Byte?)value;
							break;
						case "LOSInDay":

							if (value == null || value is System.Byte)
								this.LOSInDay = (System.Byte?)value;
							break;
						case "TransactionAmount":

							if (value == null || value is System.Decimal)
								this.TransactionAmount = (System.Decimal?)value;
							break;
						case "AdministrationAmount":

							if (value == null || value is System.Decimal)
								this.AdministrationAmount = (System.Decimal?)value;
							break;
						case "RoundingAmount":

							if (value == null || value is System.Decimal)
								this.RoundingAmount = (System.Decimal?)value;
							break;
						case "RemainingAmount":

							if (value == null || value is System.Decimal)
								this.RemainingAmount = (System.Decimal?)value;
							break;
						case "IsTransferedToInpatient":

							if (value == null || value is System.Boolean)
								this.IsTransferedToInpatient = (System.Boolean?)value;
							break;
						case "IsNewPatient":

							if (value == null || value is System.Boolean)
								this.IsNewPatient = (System.Boolean?)value;
							break;
						case "IsNewBornInfant":

							if (value == null || value is System.Boolean)
								this.IsNewBornInfant = (System.Boolean?)value;
							break;
						case "IsParturition":

							if (value == null || value is System.Boolean)
								this.IsParturition = (System.Boolean?)value;
							break;
						case "IsHoldTransactionEntry":

							if (value == null || value is System.Boolean)
								this.IsHoldTransactionEntry = (System.Boolean?)value;
							break;
						case "IsHasCorrection":

							if (value == null || value is System.Boolean)
								this.IsHasCorrection = (System.Boolean?)value;
							break;
						case "IsEMRValid":

							if (value == null || value is System.Boolean)
								this.IsEMRValid = (System.Boolean?)value;
							break;
						case "IsBackDate":

							if (value == null || value is System.Boolean)
								this.IsBackDate = (System.Boolean?)value;
							break;
						case "ActualVisitDate":

							if (value == null || value is System.DateTime)
								this.ActualVisitDate = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDate":

							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "IsEpisodeComplete":

							if (value == null || value is System.Boolean)
								this.IsEpisodeComplete = (System.Boolean?)value;
							break;
						case "IsClusterAssessment":

							if (value == null || value is System.Boolean)
								this.IsClusterAssessment = (System.Boolean?)value;
							break;
						case "IsConsul":

							if (value == null || value is System.Boolean)
								this.IsConsul = (System.Boolean?)value;
							break;
						case "IsFromDispensary":

							if (value == null || value is System.Boolean)
								this.IsFromDispensary = (System.Boolean?)value;
							break;
						case "IsNewVisit":

							if (value == null || value is System.Boolean)
								this.IsNewVisit = (System.Boolean?)value;
							break;
						case "LastCreateDateTime":

							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "isDirectPrescriptionReturn":

							if (value == null || value is System.Boolean)
								this.IsDirectPrescriptionReturn = (System.Boolean?)value;
							break;
						case "RegistrationQue":

							if (value == null || value is System.Int32)
								this.RegistrationQue = (System.Int32?)value;
							break;
						case "IsGenerateHL7":

							if (value == null || value is System.Boolean)
								this.IsGenerateHL7 = (System.Boolean?)value;
							break;
						case "IsObservation":

							if (value == null || value is System.Boolean)
								this.IsObservation = (System.Boolean?)value;
							break;
						case "IsOldCase":

							if (value == null || value is System.Boolean)
								this.IsOldCase = (System.Boolean?)value;
							break;
						case "IsDHF":

							if (value == null || value is System.Boolean)
								this.IsDHF = (System.Boolean?)value;
							break;
						case "IsEKG":

							if (value == null || value is System.Boolean)
								this.IsEKG = (System.Boolean?)value;
							break;
						case "IsGlobalPlafond":

							if (value == null || value is System.Boolean)
								this.IsGlobalPlafond = (System.Boolean?)value;
							break;
						case "FirstResponDate":

							if (value == null || value is System.DateTime)
								this.FirstResponDate = (System.DateTime?)value;
							break;
						case "PhysicianResponDate":

							if (value == null || value is System.DateTime)
								this.PhysicianResponDate = (System.DateTime?)value;
							break;
						case "IsRoomIn":

							if (value == null || value is System.Boolean)
								this.IsRoomIn = (System.Boolean?)value;
							break;
						case "IsLockVerifiedBilling":

							if (value == null || value is System.Boolean)
								this.IsLockVerifiedBilling = (System.Boolean?)value;
							break;
						case "LockVerifiedBillingDateTime":

							if (value == null || value is System.DateTime)
								this.LockVerifiedBillingDateTime = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "DischargePlanDate":

							if (value == null || value is System.DateTime)
								this.DischargePlanDate = (System.DateTime?)value;
							break;
						case "IsNonPatient":

							if (value == null || value is System.Boolean)
								this.IsNonPatient = (System.Boolean?)value;
							break;
						case "PatientAdm":

							if (value == null || value is System.Decimal)
								this.PatientAdm = (System.Decimal?)value;
							break;
						case "GuarantorAdm":

							if (value == null || value is System.Decimal)
								this.GuarantorAdm = (System.Decimal?)value;
							break;
						case "DiscAdmPatient":

							if (value == null || value is System.Decimal)
								this.DiscAdmPatient = (System.Decimal?)value;
							break;
						case "DiscAdmGuarantor":

							if (value == null || value is System.Decimal)
								this.DiscAdmGuarantor = (System.Decimal?)value;
							break;
						case "IsConfirmedAttendance":

							if (value == null || value is System.Boolean)
								this.IsConfirmedAttendance = (System.Boolean?)value;
							break;
						case "ConfirmedAttendanceDateTime":

							if (value == null || value is System.DateTime)
								this.ConfirmedAttendanceDateTime = (System.DateTime?)value;
							break;
						case "PlavonAmount2":

							if (value == null || value is System.Decimal)
								this.PlavonAmount2 = (System.Decimal?)value;
							break;
						case "BpjsCoverageFormula":

							if (value == null || value is System.Decimal)
								this.BpjsCoverageFormula = (System.Decimal?)value;
							break;
						case "ApproximatePlafondAmount":

							if (value == null || value is System.Decimal)
								this.ApproximatePlafondAmount = (System.Decimal?)value;
							break;
						case "SentToBillingDateTime":

							if (value == null || value is System.DateTime)
								this.SentToBillingDateTime = (System.DateTime?)value;
							break;
						case "IsAdjusted":

							if (value == null || value is System.Boolean)
								this.IsAdjusted = (System.Boolean?)value;
							break;
						case "IsAllowPatientCheckOut":

							if (value == null || value is System.Boolean)
								this.IsAllowPatientCheckOut = (System.Boolean?)value;
							break;
						case "AllowPatientCheckOutDateTime":

							if (value == null || value is System.DateTime)
								this.AllowPatientCheckOutDateTime = (System.DateTime?)value;
							break;
						case "IsPregnant":

							if (value == null || value is System.Boolean)
								this.IsPregnant = (System.Boolean?)value;
							break;
						case "GestationalAge":

							if (value == null || value is System.Int16)
								this.GestationalAge = (System.Int16?)value;
							break;
						case "IsBreastFeeding":

							if (value == null || value is System.Boolean)
								this.IsBreastFeeding = (System.Boolean?)value;
							break;
						case "AgeOfBabyInYear":

							if (value == null || value is System.Int16)
								this.AgeOfBabyInYear = (System.Int16?)value;
							break;
						case "AgeOfBabyInMonth":

							if (value == null || value is System.Int16)
								this.AgeOfBabyInMonth = (System.Int16?)value;
							break;
						case "AgeOfBabyInDay":

							if (value == null || value is System.Int16)
								this.AgeOfBabyInDay = (System.Int16?)value;
							break;
						case "IsKidneyFunctionImpaired":

							if (value == null || value is System.Boolean)
								this.IsKidneyFunctionImpaired = (System.Boolean?)value;
							break;
						case "CreatinineSerumValue":

							if (value == null || value is System.Int16)
								this.CreatinineSerumValue = (System.Int16?)value;
							break;
						case "MembershipDetailID":

							if (value == null || value is System.Int64)
								this.MembershipDetailID = (System.Int64?)value;
							break;
						case "IsReconcile":

							if (value == null || value is System.Boolean)
								this.IsReconcile = (System.Boolean?)value;
							break;
						case "IsSkipAutoBill":

							if (value == null || value is System.Boolean)
								this.IsSkipAutoBill = (System.Boolean?)value;
							break;
						case "IsOpenEntryMR":

							if (value == null || value is System.Boolean)
								this.IsOpenEntryMR = (System.Boolean?)value;
							break;
						case "SRCovidStatus":

							if (value == null || value is System.Byte)
								this.SRCovidStatus = (System.Byte?)value;
							break;
						case "IsDisability":

							if (value == null || value is System.Boolean)
								this.IsDisability = (System.Boolean?)value;
							break;
						case "IsTracer":

							if (value == null || value is System.Boolean)
								this.IsTracer = (System.Boolean?)value;
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
		/// Maps to Registration.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRRegistrationType);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RegistrationDate
		/// </summary>
		virtual public System.DateTime? RegistrationDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.RegistrationDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.RegistrationDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RegistrationTime
		/// </summary>
		virtual public System.String RegistrationTime
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RegistrationTime);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RegistrationTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AppointmentNo
		/// </summary>
		virtual public System.String AppointmentNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AppointmentNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AppointmentNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeInYear
		/// </summary>
		virtual public System.Byte? AgeInYear
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.AgeInYear);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.AgeInYear, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeInMonth
		/// </summary>
		virtual public System.Byte? AgeInMonth
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.AgeInMonth);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.AgeInMonth, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeInDay
		/// </summary>
		virtual public System.Byte? AgeInDay
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.AgeInDay);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.AgeInDay, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRShift
		/// </summary>
		virtual public System.String SRShift
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRShift);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRShift, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRPatientInType
		/// </summary>
		virtual public System.String SRPatientInType
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRPatientInType);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRPatientInType, value);
			}
		}
		/// <summary>
		/// Maps to Registration.InsuranceID
		/// </summary>
		virtual public System.String InsuranceID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.InsuranceID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.InsuranceID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRPatientCategory
		/// </summary>
		virtual public System.String SRPatientCategory
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRPatientCategory);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRPatientCategory, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRERCaseType
		/// </summary>
		virtual public System.String SRERCaseType
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRERCaseType);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRERCaseType, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRVisitReason
		/// </summary>
		virtual public System.String SRVisitReason
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRVisitReason);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRVisitReason, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRBussinesMethod
		/// </summary>
		virtual public System.String SRBussinesMethod
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRBussinesMethod);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRBussinesMethod, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PlavonAmount
		/// </summary>
		virtual public System.Decimal? PlavonAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.PlavonAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.PlavonAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DepartmentID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ChargeClassID
		/// </summary>
		virtual public System.String ChargeClassID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ChargeClassID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ChargeClassID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.CoverageClassID
		/// </summary>
		virtual public System.String CoverageClassID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.CoverageClassID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.CoverageClassID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VisitTypeID
		/// </summary>
		virtual public System.String VisitTypeID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VisitTypeID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VisitTypeID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferralID
		/// </summary>
		virtual public System.String ReferralID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferralID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferralID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.Anamnesis
		/// </summary>
		virtual public System.String Anamnesis
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.Anamnesis);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.Anamnesis, value);
			}
		}
		/// <summary>
		/// Maps to Registration.Complaint
		/// </summary>
		virtual public System.String Complaint
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.Complaint);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.Complaint, value);
			}
		}
		/// <summary>
		/// Maps to Registration.InitialDiagnose
		/// </summary>
		virtual public System.String InitialDiagnose
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.InitialDiagnose);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.InitialDiagnose, value);
			}
		}
		/// <summary>
		/// Maps to Registration.MedicationPlanning
		/// </summary>
		virtual public System.String MedicationPlanning
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.MedicationPlanning);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.MedicationPlanning, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRTriage
		/// </summary>
		virtual public System.String SRTriage
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRTriage);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRTriage, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsPrintingPatientCard
		/// </summary>
		virtual public System.Boolean? IsPrintingPatientCard
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsPrintingPatientCard);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsPrintingPatientCard, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargeDate
		/// </summary>
		virtual public System.DateTime? DischargeDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.DischargeDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.DischargeDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargeTime
		/// </summary>
		virtual public System.String DischargeTime
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DischargeTime);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DischargeTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargeMedicalNotes
		/// </summary>
		virtual public System.String DischargeMedicalNotes
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DischargeMedicalNotes);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DischargeMedicalNotes, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargeNotes
		/// </summary>
		virtual public System.String DischargeNotes
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DischargeNotes);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DischargeNotes, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRDischargeCondition
		/// </summary>
		virtual public System.String SRDischargeCondition
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRDischargeCondition);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRDischargeCondition, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRDischargeMethod
		/// </summary>
		virtual public System.String SRDischargeMethod
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRDischargeMethod);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRDischargeMethod, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LOSInYear
		/// </summary>
		virtual public System.Byte? LOSInYear
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.LOSInYear);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.LOSInYear, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LOSInMonth
		/// </summary>
		virtual public System.Byte? LOSInMonth
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.LOSInMonth);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.LOSInMonth, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LOSInDay
		/// </summary>
		virtual public System.Byte? LOSInDay
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.LOSInDay);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.LOSInDay, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargeOperatorID
		/// </summary>
		virtual public System.String DischargeOperatorID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DischargeOperatorID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DischargeOperatorID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AccountNo
		/// </summary>
		virtual public System.String AccountNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AccountNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AccountNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.TransactionAmount
		/// </summary>
		virtual public System.Decimal? TransactionAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.TransactionAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.TransactionAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AdministrationAmount
		/// </summary>
		virtual public System.Decimal? AdministrationAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.AdministrationAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.AdministrationAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RoundingAmount
		/// </summary>
		virtual public System.Decimal? RoundingAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.RoundingAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.RoundingAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RemainingAmount
		/// </summary>
		virtual public System.Decimal? RemainingAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.RemainingAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.RemainingAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsTransferedToInpatient
		/// </summary>
		virtual public System.Boolean? IsTransferedToInpatient
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsTransferedToInpatient);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsTransferedToInpatient, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsNewPatient
		/// </summary>
		virtual public System.Boolean? IsNewPatient
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewPatient);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewPatient, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsNewBornInfant
		/// </summary>
		virtual public System.Boolean? IsNewBornInfant
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewBornInfant);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewBornInfant, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsParturition
		/// </summary>
		virtual public System.Boolean? IsParturition
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsParturition);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsParturition, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsHoldTransactionEntry
		/// </summary>
		virtual public System.Boolean? IsHoldTransactionEntry
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsHoldTransactionEntry);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsHoldTransactionEntry, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsHasCorrection
		/// </summary>
		virtual public System.Boolean? IsHasCorrection
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsHasCorrection);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsHasCorrection, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsEMRValid
		/// </summary>
		virtual public System.Boolean? IsEMRValid
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsEMRValid);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsEMRValid, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsBackDate
		/// </summary>
		virtual public System.Boolean? IsBackDate
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsBackDate);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsBackDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ActualVisitDate
		/// </summary>
		virtual public System.DateTime? ActualVisitDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.ActualVisitDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.ActualVisitDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRVoidReason
		/// </summary>
		virtual public System.String SRVoidReason
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRVoidReason);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRVoidReason, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VoidNotes
		/// </summary>
		virtual public System.String VoidNotes
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VoidNotes);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VoidNotes, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.VoidDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsEpisodeComplete
		/// </summary>
		virtual public System.Boolean? IsEpisodeComplete
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsEpisodeComplete);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsEpisodeComplete, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsClusterAssessment
		/// </summary>
		virtual public System.Boolean? IsClusterAssessment
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsClusterAssessment);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsClusterAssessment, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsConsul
		/// </summary>
		virtual public System.Boolean? IsConsul
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsConsul);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsConsul, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsFromDispensary
		/// </summary>
		virtual public System.Boolean? IsFromDispensary
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsFromDispensary);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsFromDispensary, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsNewVisit
		/// </summary>
		virtual public System.Boolean? IsNewVisit
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewVisit);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsNewVisit, value);
			}
		}
		/// <summary>
		/// Maps to Registration.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.LastCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LastCreateUserID
		/// </summary>
		virtual public System.String LastCreateUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.LastCreateUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.LastCreateUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.isDirectPrescriptionReturn
		/// </summary>
		virtual public System.Boolean? IsDirectPrescriptionReturn
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsDirectPrescriptionReturn);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsDirectPrescriptionReturn, value);
			}
		}
		/// <summary>
		/// Maps to Registration.RegistrationQue
		/// </summary>
		virtual public System.Int32? RegistrationQue
		{
			get
			{
				return base.GetSystemInt32(RegistrationMetadata.ColumnNames.RegistrationQue);
			}

			set
			{
				base.SetSystemInt32(RegistrationMetadata.ColumnNames.RegistrationQue, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VisiteRegistrationNo
		/// </summary>
		virtual public System.String VisiteRegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VisiteRegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VisiteRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsGenerateHL7
		/// </summary>
		virtual public System.Boolean? IsGenerateHL7
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsGenerateHL7);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsGenerateHL7, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferralName
		/// </summary>
		virtual public System.String ReferralName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferralName);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferralName, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsObservation
		/// </summary>
		virtual public System.Boolean? IsObservation
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsObservation);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsObservation, value);
			}
		}
		/// <summary>
		/// Maps to Registration.CauseOfAccident
		/// </summary>
		virtual public System.String CauseOfAccident
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.CauseOfAccident);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.CauseOfAccident, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferTo
		/// </summary>
		virtual public System.String ReferTo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferTo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferTo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsOldCase
		/// </summary>
		virtual public System.Boolean? IsOldCase
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsOldCase);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsOldCase, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsDHF
		/// </summary>
		virtual public System.Boolean? IsDHF
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsDHF);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsDHF, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsEKG
		/// </summary>
		virtual public System.Boolean? IsEKG
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsEKG);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsEKG, value);
			}
		}
		/// <summary>
		/// Maps to Registration.EmrDiagnoseID
		/// </summary>
		virtual public System.String EmrDiagnoseID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.EmrDiagnoseID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.EmrDiagnoseID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsGlobalPlafond
		/// </summary>
		virtual public System.Boolean? IsGlobalPlafond
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsGlobalPlafond);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsGlobalPlafond, value);
			}
		}
		/// <summary>
		/// Maps to Registration.FirstResponDate
		/// </summary>
		virtual public System.DateTime? FirstResponDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.FirstResponDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.FirstResponDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.FirstResponTime
		/// </summary>
		virtual public System.String FirstResponTime
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.FirstResponTime);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.FirstResponTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PhysicianResponDate
		/// </summary>
		virtual public System.DateTime? PhysicianResponDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.PhysicianResponDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.PhysicianResponDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PhysicianResponTime
		/// </summary>
		virtual public System.String PhysicianResponTime
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.PhysicianResponTime);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.PhysicianResponTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsRoomIn
		/// </summary>
		virtual public System.Boolean? IsRoomIn
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsRoomIn);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsRoomIn, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsLockVerifiedBilling
		/// </summary>
		virtual public System.Boolean? IsLockVerifiedBilling
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsLockVerifiedBilling);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsLockVerifiedBilling, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LockVerifiedBillingDateTime
		/// </summary>
		virtual public System.DateTime? LockVerifiedBillingDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.LockVerifiedBillingDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.LockVerifiedBillingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.LockVerifiedBillingByUserID
		/// </summary>
		virtual public System.String LockVerifiedBillingByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.LockVerifiedBillingByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.LockVerifiedBillingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ProcedureChargeClassID
		/// </summary>
		virtual public System.String ProcedureChargeClassID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ProcedureChargeClassID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ProcedureChargeClassID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(RegistrationMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(RegistrationMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.EmployeeNumber
		/// </summary>
		virtual public System.String EmployeeNumber
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.EmployeeNumber);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.EmployeeNumber, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SREmployeeRelationship
		/// </summary>
		virtual public System.String SREmployeeRelationship
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SREmployeeRelationship);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SREmployeeRelationship, value);
			}
		}
		/// <summary>
		/// Maps to Registration.GuarantorCardNo
		/// </summary>
		virtual public System.String GuarantorCardNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.GuarantorCardNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.GuarantorCardNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DischargePlanDate
		/// </summary>
		virtual public System.DateTime? DischargePlanDate
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.DischargePlanDate);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.DischargePlanDate, value);
			}
		}
		/// <summary>
		/// Maps to Registration.UsertInsertDischargePlan
		/// </summary>
		virtual public System.String UsertInsertDischargePlan
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.UsertInsertDischargePlan);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.UsertInsertDischargePlan, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsNonPatient
		/// </summary>
		virtual public System.Boolean? IsNonPatient
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsNonPatient);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsNonPatient, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReasonsForTreatmentID
		/// </summary>
		virtual public System.String ReasonsForTreatmentID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReasonsForTreatmentID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReasonsForTreatmentID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SmfID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PatientAdm
		/// </summary>
		virtual public System.Decimal? PatientAdm
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.PatientAdm);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.PatientAdm, value);
			}
		}
		/// <summary>
		/// Maps to Registration.GuarantorAdm
		/// </summary>
		virtual public System.Decimal? GuarantorAdm
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.GuarantorAdm);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.GuarantorAdm, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReasonsForTreatmentDescID
		/// </summary>
		virtual public System.String ReasonsForTreatmentDescID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReasonsForTreatmentDescID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReasonsForTreatmentDescID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRReferralGroup
		/// </summary>
		virtual public System.String SRReferralGroup
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRReferralGroup);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRReferralGroup, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRDiscountReason);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PhysicianSenders
		/// </summary>
		virtual public System.String PhysicianSenders
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.PhysicianSenders);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.PhysicianSenders, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DiscAdmPatient
		/// </summary>
		virtual public System.Decimal? DiscAdmPatient
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.DiscAdmPatient);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.DiscAdmPatient, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DiscAdmGuarantor
		/// </summary>
		virtual public System.Decimal? DiscAdmGuarantor
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.DiscAdmGuarantor);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.DiscAdmGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRPatientInCondition
		/// </summary>
		virtual public System.String SRPatientInCondition
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRPatientInCondition);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRPatientInCondition, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRKiaCaseType
		/// </summary>
		virtual public System.String SRKiaCaseType
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRKiaCaseType);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRKiaCaseType, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRObstetricType
		/// </summary>
		virtual public System.String SRObstetricType
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRObstetricType);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRObstetricType, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsHoldTransactionEntryByUserID
		/// </summary>
		virtual public System.String IsHoldTransactionEntryByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.IsHoldTransactionEntryByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.IsHoldTransactionEntryByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.FromRegistrationNo
		/// </summary>
		virtual public System.String FromRegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.FromRegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.FromRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsConfirmedAttendance
		/// </summary>
		virtual public System.Boolean? IsConfirmedAttendance
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsConfirmedAttendance);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsConfirmedAttendance, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ConfirmedAttendanceByUserID
		/// </summary>
		virtual public System.String ConfirmedAttendanceByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ConfirmedAttendanceByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ConfirmedAttendanceByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ConfirmedAttendanceDateTime
		/// </summary>
		virtual public System.DateTime? ConfirmedAttendanceDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.ConfirmedAttendanceDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.ConfirmedAttendanceDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.BpjsSepNo
		/// </summary>
		virtual public System.String BpjsSepNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.BpjsSepNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.BpjsSepNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.PlavonAmount2
		/// </summary>
		virtual public System.Decimal? PlavonAmount2
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.PlavonAmount2);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.PlavonAmount2, value);
			}
		}
		/// <summary>
		/// Maps to Registration.DeathCertificateNo
		/// </summary>
		virtual public System.String DeathCertificateNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DeathCertificateNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DeathCertificateNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.BpjsCoverageFormula
		/// </summary>
		virtual public System.Decimal? BpjsCoverageFormula
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.BpjsCoverageFormula);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.BpjsCoverageFormula, value);
			}
		}
		/// <summary>
		/// Maps to Registration.BpjsPackageID
		/// </summary>
		virtual public System.String BpjsPackageID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.BpjsPackageID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.BpjsPackageID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ApproximatePlafondAmount
		/// </summary>
		virtual public System.Decimal? ApproximatePlafondAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationMetadata.ColumnNames.ApproximatePlafondAmount);
			}

			set
			{
				base.SetSystemDecimal(RegistrationMetadata.ColumnNames.ApproximatePlafondAmount, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SentToBillingDateTime
		/// </summary>
		virtual public System.DateTime? SentToBillingDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.SentToBillingDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.SentToBillingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SentToBillingByUserID
		/// </summary>
		virtual public System.String SentToBillingByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SentToBillingByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SentToBillingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsAdjusted
		/// </summary>
		virtual public System.Boolean? IsAdjusted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsAdjusted);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsAdjusted, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AdjustLog
		/// </summary>
		virtual public System.String AdjustLog
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AdjustLog);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AdjustLog, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsAllowPatientCheckOut
		/// </summary>
		virtual public System.Boolean? IsAllowPatientCheckOut
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsAllowPatientCheckOut);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsAllowPatientCheckOut, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AllowPatientCheckOutDateTime
		/// </summary>
		virtual public System.DateTime? AllowPatientCheckOutDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.AllowPatientCheckOutDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.AllowPatientCheckOutDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AllowPatientCheckOutByUserID
		/// </summary>
		virtual public System.String AllowPatientCheckOutByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AllowPatientCheckOutByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AllowPatientCheckOutByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferByParamedicID
		/// </summary>
		virtual public System.String ReferByParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferByParamedicID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferByParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SROccupation
		/// </summary>
		virtual public System.String SROccupation
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SROccupation);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SROccupation, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRRelationshipQuality
		/// </summary>
		virtual public System.String SRRelationshipQuality
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRRelationshipQuality);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRRelationshipQuality, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRResidentialHome
		/// </summary>
		virtual public System.String SRResidentialHome
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRResidentialHome);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRResidentialHome, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRFatherOccupation
		/// </summary>
		virtual public System.String SRFatherOccupation
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRFatherOccupation);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRFatherOccupation, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsPregnant
		/// </summary>
		virtual public System.Boolean? IsPregnant
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsPregnant);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsPregnant, value);
			}
		}
		/// <summary>
		/// Maps to Registration.GestationalAge
		/// </summary>
		virtual public System.Int16? GestationalAge
		{
			get
			{
				return base.GetSystemInt16(RegistrationMetadata.ColumnNames.GestationalAge);
			}

			set
			{
				base.SetSystemInt16(RegistrationMetadata.ColumnNames.GestationalAge, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsBreastFeeding
		/// </summary>
		virtual public System.Boolean? IsBreastFeeding
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsBreastFeeding);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsBreastFeeding, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeOfBabyInYear
		/// </summary>
		virtual public System.Int16? AgeOfBabyInYear
		{
			get
			{
				return base.GetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInYear);
			}

			set
			{
				base.SetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInYear, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeOfBabyInMonth
		/// </summary>
		virtual public System.Int16? AgeOfBabyInMonth
		{
			get
			{
				return base.GetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInMonth);
			}

			set
			{
				base.SetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInMonth, value);
			}
		}
		/// <summary>
		/// Maps to Registration.AgeOfBabyInDay
		/// </summary>
		virtual public System.Int16? AgeOfBabyInDay
		{
			get
			{
				return base.GetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInDay);
			}

			set
			{
				base.SetSystemInt16(RegistrationMetadata.ColumnNames.AgeOfBabyInDay, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsKidneyFunctionImpaired
		/// </summary>
		virtual public System.Boolean? IsKidneyFunctionImpaired
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsKidneyFunctionImpaired);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsKidneyFunctionImpaired, value);
			}
		}
		/// <summary>
		/// Maps to Registration.CreatinineSerumValue
		/// </summary>
		virtual public System.Int16? CreatinineSerumValue
		{
			get
			{
				return base.GetSystemInt16(RegistrationMetadata.ColumnNames.CreatinineSerumValue);
			}

			set
			{
				base.SetSystemInt16(RegistrationMetadata.ColumnNames.CreatinineSerumValue, value);
			}
		}
		/// <summary>
		/// Maps to Registration.Hpi
		/// </summary>
		virtual public System.String Hpi
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.Hpi);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.Hpi, value);
			}
		}
		/// <summary>
		/// Maps to Registration.MembershipDetailID
		/// </summary>
		virtual public System.Int64? MembershipDetailID
		{
			get
			{
				return base.GetSystemInt64(RegistrationMetadata.ColumnNames.MembershipDetailID);
			}

			set
			{
				base.SetSystemInt64(RegistrationMetadata.ColumnNames.MembershipDetailID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ExternalQueNo
		/// </summary>
		virtual public System.String ExternalQueNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ExternalQueNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ExternalQueNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferralIdTo
		/// </summary>
		virtual public System.String ReferralIdTo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferralIdTo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferralIdTo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ReferralNameTo
		/// </summary>
		virtual public System.String ReferralNameTo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ReferralNameTo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ReferralNameTo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsReconcile
		/// </summary>
		virtual public System.Boolean? IsReconcile
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsReconcile);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsReconcile, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsSkipAutoBill
		/// </summary>
		virtual public System.Boolean? IsSkipAutoBill
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsSkipAutoBill);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsSkipAutoBill, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRCrashSite
		/// </summary>
		virtual public System.String SRCrashSite
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRCrashSite);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRCrashSite, value);
			}
		}
		/// <summary>
		/// Maps to Registration.CrashSiteDetail
		/// </summary>
		virtual public System.String CrashSiteDetail
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.CrashSiteDetail);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.CrashSiteDetail, value);
			}
		}
		/// <summary>
		/// Maps to Registration.MembershipNo
		/// </summary>
		virtual public System.String MembershipNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.MembershipNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.MembershipNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsOpenEntryMR
		/// </summary>
		virtual public System.Boolean? IsOpenEntryMR
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsOpenEntryMR);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsOpenEntryMR, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRCovidStatus
		/// </summary>
		virtual public System.Byte? SRCovidStatus
		{
			get
			{
				return base.GetSystemByte(RegistrationMetadata.ColumnNames.SRCovidStatus);
			}

			set
			{
				base.SetSystemByte(RegistrationMetadata.ColumnNames.SRCovidStatus, value);
			}
		}
		/// <summary>
		/// Maps to Registration.VoucherNo
		/// </summary>
		virtual public System.String VoucherNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VoucherNo);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VoucherNo, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRCovidComorbidStatus
		/// </summary>
		virtual public System.String SRCovidComorbidStatus
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRCovidComorbidStatus);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRCovidComorbidStatus, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsDisability
		/// </summary>
		virtual public System.Boolean? IsDisability
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsDisability);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsDisability, value);
			}
		}
		/// <summary>
		/// Maps to Registration.IsTracer
		/// </summary>
		virtual public System.Boolean? IsTracer
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsTracer);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsTracer, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ItemConditionRuleID
		/// </summary>
		virtual public System.String ItemConditionRuleID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ItemConditionRuleID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ItemConditionRuleID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.SRPatientRiskStatus
		/// </summary>
		virtual public System.String SRPatientRiskStatus
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.SRPatientRiskStatus);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.SRPatientRiskStatus, value);
			}
		}

		/// <summary>
		/// Maps to Registration.IsFinishedAttendance
		/// </summary>
		virtual public System.Boolean? IsFinishedAttendance
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.IsFinishedAttendance);
			}

			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.IsFinishedAttendance, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ConfirmedAttendanceByUserID
		/// </summary>
		virtual public System.String FinishedAttendanceByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.FinishedAttendanceByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.FinishedAttendanceByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Registration.ConfirmedAttendanceDateTime
		/// </summary>
		virtual public System.DateTime? FinishedAttendanceDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.FinishedAttendanceDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.FinishedAttendanceDateTime, value);
			}
		}

        /// <summary>
        /// Maps to Registration.SRPatientRiskColor
        /// </summary>
        virtual public System.String SRPatientRiskColor
        {
            get
            {
                return base.GetSystemString(RegistrationMetadata.ColumnNames.SRPatientRiskColor);
            }

            set
            {
                base.SetSystemString(RegistrationMetadata.ColumnNames.SRPatientRiskColor, value);
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
			public esStrings(esRegistration entity)
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
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
			public System.String RegistrationDate
			{
				get
				{
					System.DateTime? data = entity.RegistrationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationDate = null;
					else entity.RegistrationDate = Convert.ToDateTime(value);
				}
			}
			public System.String RegistrationTime
			{
				get
				{
					System.String data = entity.RegistrationTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationTime = null;
					else entity.RegistrationTime = Convert.ToString(value);
				}
			}
			public System.String AppointmentNo
			{
				get
				{
					System.String data = entity.AppointmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentNo = null;
					else entity.AppointmentNo = Convert.ToString(value);
				}
			}
			public System.String AgeInYear
			{
				get
				{
					System.Byte? data = entity.AgeInYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeInYear = null;
					else entity.AgeInYear = Convert.ToByte(value);
				}
			}
			public System.String AgeInMonth
			{
				get
				{
					System.Byte? data = entity.AgeInMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeInMonth = null;
					else entity.AgeInMonth = Convert.ToByte(value);
				}
			}
			public System.String AgeInDay
			{
				get
				{
					System.Byte? data = entity.AgeInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeInDay = null;
					else entity.AgeInDay = Convert.ToByte(value);
				}
			}
			public System.String SRShift
			{
				get
				{
					System.String data = entity.SRShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRShift = null;
					else entity.SRShift = Convert.ToString(value);
				}
			}
			public System.String SRPatientInType
			{
				get
				{
					System.String data = entity.SRPatientInType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientInType = null;
					else entity.SRPatientInType = Convert.ToString(value);
				}
			}
			public System.String InsuranceID
			{
				get
				{
					System.String data = entity.InsuranceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsuranceID = null;
					else entity.InsuranceID = Convert.ToString(value);
				}
			}
			public System.String SRPatientCategory
			{
				get
				{
					System.String data = entity.SRPatientCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientCategory = null;
					else entity.SRPatientCategory = Convert.ToString(value);
				}
			}
			public System.String SRERCaseType
			{
				get
				{
					System.String data = entity.SRERCaseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRERCaseType = null;
					else entity.SRERCaseType = Convert.ToString(value);
				}
			}
			public System.String SRVisitReason
			{
				get
				{
					System.String data = entity.SRVisitReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVisitReason = null;
					else entity.SRVisitReason = Convert.ToString(value);
				}
			}
			public System.String SRBussinesMethod
			{
				get
				{
					System.String data = entity.SRBussinesMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBussinesMethod = null;
					else entity.SRBussinesMethod = Convert.ToString(value);
				}
			}
			public System.String PlavonAmount
			{
				get
				{
					System.Decimal? data = entity.PlavonAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlavonAmount = null;
					else entity.PlavonAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
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
			public System.String CoverageClassID
			{
				get
				{
					System.String data = entity.CoverageClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageClassID = null;
					else entity.CoverageClassID = Convert.ToString(value);
				}
			}
			public System.String VisitTypeID
			{
				get
				{
					System.String data = entity.VisitTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeID = null;
					else entity.VisitTypeID = Convert.ToString(value);
				}
			}
			public System.String ReferralID
			{
				get
				{
					System.String data = entity.ReferralID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralID = null;
					else entity.ReferralID = Convert.ToString(value);
				}
			}
			public System.String Anamnesis
			{
				get
				{
					System.String data = entity.Anamnesis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Anamnesis = null;
					else entity.Anamnesis = Convert.ToString(value);
				}
			}
			public System.String Complaint
			{
				get
				{
					System.String data = entity.Complaint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Complaint = null;
					else entity.Complaint = Convert.ToString(value);
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
			public System.String MedicationPlanning
			{
				get
				{
					System.String data = entity.MedicationPlanning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationPlanning = null;
					else entity.MedicationPlanning = Convert.ToString(value);
				}
			}
			public System.String SRTriage
			{
				get
				{
					System.String data = entity.SRTriage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTriage = null;
					else entity.SRTriage = Convert.ToString(value);
				}
			}
			public System.String IsPrintingPatientCard
			{
				get
				{
					System.Boolean? data = entity.IsPrintingPatientCard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrintingPatientCard = null;
					else entity.IsPrintingPatientCard = Convert.ToBoolean(value);
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
			public System.String DischargeMedicalNotes
			{
				get
				{
					System.String data = entity.DischargeMedicalNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeMedicalNotes = null;
					else entity.DischargeMedicalNotes = Convert.ToString(value);
				}
			}
			public System.String DischargeNotes
			{
				get
				{
					System.String data = entity.DischargeNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeNotes = null;
					else entity.DischargeNotes = Convert.ToString(value);
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
			public System.String LOSInYear
			{
				get
				{
					System.Byte? data = entity.LOSInYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LOSInYear = null;
					else entity.LOSInYear = Convert.ToByte(value);
				}
			}
			public System.String LOSInMonth
			{
				get
				{
					System.Byte? data = entity.LOSInMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LOSInMonth = null;
					else entity.LOSInMonth = Convert.ToByte(value);
				}
			}
			public System.String LOSInDay
			{
				get
				{
					System.Byte? data = entity.LOSInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LOSInDay = null;
					else entity.LOSInDay = Convert.ToByte(value);
				}
			}
			public System.String DischargeOperatorID
			{
				get
				{
					System.String data = entity.DischargeOperatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeOperatorID = null;
					else entity.DischargeOperatorID = Convert.ToString(value);
				}
			}
			public System.String AccountNo
			{
				get
				{
					System.String data = entity.AccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountNo = null;
					else entity.AccountNo = Convert.ToString(value);
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
			public System.String AdministrationAmount
			{
				get
				{
					System.Decimal? data = entity.AdministrationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdministrationAmount = null;
					else entity.AdministrationAmount = Convert.ToDecimal(value);
				}
			}
			public System.String RoundingAmount
			{
				get
				{
					System.Decimal? data = entity.RoundingAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoundingAmount = null;
					else entity.RoundingAmount = Convert.ToDecimal(value);
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
			public System.String IsTransferedToInpatient
			{
				get
				{
					System.Boolean? data = entity.IsTransferedToInpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTransferedToInpatient = null;
					else entity.IsTransferedToInpatient = Convert.ToBoolean(value);
				}
			}
			public System.String IsNewPatient
			{
				get
				{
					System.Boolean? data = entity.IsNewPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNewPatient = null;
					else entity.IsNewPatient = Convert.ToBoolean(value);
				}
			}
			public System.String IsNewBornInfant
			{
				get
				{
					System.Boolean? data = entity.IsNewBornInfant;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNewBornInfant = null;
					else entity.IsNewBornInfant = Convert.ToBoolean(value);
				}
			}
			public System.String IsParturition
			{
				get
				{
					System.Boolean? data = entity.IsParturition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParturition = null;
					else entity.IsParturition = Convert.ToBoolean(value);
				}
			}
			public System.String IsHoldTransactionEntry
			{
				get
				{
					System.Boolean? data = entity.IsHoldTransactionEntry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHoldTransactionEntry = null;
					else entity.IsHoldTransactionEntry = Convert.ToBoolean(value);
				}
			}
			public System.String IsHasCorrection
			{
				get
				{
					System.Boolean? data = entity.IsHasCorrection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHasCorrection = null;
					else entity.IsHasCorrection = Convert.ToBoolean(value);
				}
			}
			public System.String IsEMRValid
			{
				get
				{
					System.Boolean? data = entity.IsEMRValid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEMRValid = null;
					else entity.IsEMRValid = Convert.ToBoolean(value);
				}
			}
			public System.String IsBackDate
			{
				get
				{
					System.Boolean? data = entity.IsBackDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBackDate = null;
					else entity.IsBackDate = Convert.ToBoolean(value);
				}
			}
			public System.String ActualVisitDate
			{
				get
				{
					System.DateTime? data = entity.ActualVisitDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActualVisitDate = null;
					else entity.ActualVisitDate = Convert.ToDateTime(value);
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
			public System.String SRVoidReason
			{
				get
				{
					System.String data = entity.SRVoidReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVoidReason = null;
					else entity.SRVoidReason = Convert.ToString(value);
				}
			}
			public System.String VoidNotes
			{
				get
				{
					System.String data = entity.VoidNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidNotes = null;
					else entity.VoidNotes = Convert.ToString(value);
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
			public System.String IsEpisodeComplete
			{
				get
				{
					System.Boolean? data = entity.IsEpisodeComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEpisodeComplete = null;
					else entity.IsEpisodeComplete = Convert.ToBoolean(value);
				}
			}
			public System.String IsClusterAssessment
			{
				get
				{
					System.Boolean? data = entity.IsClusterAssessment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClusterAssessment = null;
					else entity.IsClusterAssessment = Convert.ToBoolean(value);
				}
			}
			public System.String IsConsul
			{
				get
				{
					System.Boolean? data = entity.IsConsul;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsul = null;
					else entity.IsConsul = Convert.ToBoolean(value);
				}
			}
			public System.String IsFromDispensary
			{
				get
				{
					System.Boolean? data = entity.IsFromDispensary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromDispensary = null;
					else entity.IsFromDispensary = Convert.ToBoolean(value);
				}
			}
			public System.String IsNewVisit
			{
				get
				{
					System.Boolean? data = entity.IsNewVisit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNewVisit = null;
					else entity.IsNewVisit = Convert.ToBoolean(value);
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
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCreateUserID
			{
				get
				{
					System.String data = entity.LastCreateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateUserID = null;
					else entity.LastCreateUserID = Convert.ToString(value);
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
			public System.String IsDirectPrescriptionReturn
			{
				get
				{
					System.Boolean? data = entity.IsDirectPrescriptionReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirectPrescriptionReturn = null;
					else entity.IsDirectPrescriptionReturn = Convert.ToBoolean(value);
				}
			}
			public System.String RegistrationQue
			{
				get
				{
					System.Int32? data = entity.RegistrationQue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationQue = null;
					else entity.RegistrationQue = Convert.ToInt32(value);
				}
			}
			public System.String VisiteRegistrationNo
			{
				get
				{
					System.String data = entity.VisiteRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisiteRegistrationNo = null;
					else entity.VisiteRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String IsGenerateHL7
			{
				get
				{
					System.Boolean? data = entity.IsGenerateHL7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateHL7 = null;
					else entity.IsGenerateHL7 = Convert.ToBoolean(value);
				}
			}
			public System.String ReferralName
			{
				get
				{
					System.String data = entity.ReferralName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralName = null;
					else entity.ReferralName = Convert.ToString(value);
				}
			}
			public System.String IsObservation
			{
				get
				{
					System.Boolean? data = entity.IsObservation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsObservation = null;
					else entity.IsObservation = Convert.ToBoolean(value);
				}
			}
			public System.String CauseOfAccident
			{
				get
				{
					System.String data = entity.CauseOfAccident;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CauseOfAccident = null;
					else entity.CauseOfAccident = Convert.ToString(value);
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
			public System.String IsOldCase
			{
				get
				{
					System.Boolean? data = entity.IsOldCase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOldCase = null;
					else entity.IsOldCase = Convert.ToBoolean(value);
				}
			}
			public System.String IsDHF
			{
				get
				{
					System.Boolean? data = entity.IsDHF;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDHF = null;
					else entity.IsDHF = Convert.ToBoolean(value);
				}
			}
			public System.String IsEKG
			{
				get
				{
					System.Boolean? data = entity.IsEKG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEKG = null;
					else entity.IsEKG = Convert.ToBoolean(value);
				}
			}
			public System.String EmrDiagnoseID
			{
				get
				{
					System.String data = entity.EmrDiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmrDiagnoseID = null;
					else entity.EmrDiagnoseID = Convert.ToString(value);
				}
			}
			public System.String IsGlobalPlafond
			{
				get
				{
					System.Boolean? data = entity.IsGlobalPlafond;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGlobalPlafond = null;
					else entity.IsGlobalPlafond = Convert.ToBoolean(value);
				}
			}
			public System.String FirstResponDate
			{
				get
				{
					System.DateTime? data = entity.FirstResponDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstResponDate = null;
					else entity.FirstResponDate = Convert.ToDateTime(value);
				}
			}
			public System.String FirstResponTime
			{
				get
				{
					System.String data = entity.FirstResponTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstResponTime = null;
					else entity.FirstResponTime = Convert.ToString(value);
				}
			}
			public System.String PhysicianResponDate
			{
				get
				{
					System.DateTime? data = entity.PhysicianResponDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianResponDate = null;
					else entity.PhysicianResponDate = Convert.ToDateTime(value);
				}
			}
			public System.String PhysicianResponTime
			{
				get
				{
					System.String data = entity.PhysicianResponTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianResponTime = null;
					else entity.PhysicianResponTime = Convert.ToString(value);
				}
			}
			public System.String IsRoomIn
			{
				get
				{
					System.Boolean? data = entity.IsRoomIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRoomIn = null;
					else entity.IsRoomIn = Convert.ToBoolean(value);
				}
			}
			public System.String IsLockVerifiedBilling
			{
				get
				{
					System.Boolean? data = entity.IsLockVerifiedBilling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLockVerifiedBilling = null;
					else entity.IsLockVerifiedBilling = Convert.ToBoolean(value);
				}
			}
			public System.String LockVerifiedBillingDateTime
			{
				get
				{
					System.DateTime? data = entity.LockVerifiedBillingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LockVerifiedBillingDateTime = null;
					else entity.LockVerifiedBillingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LockVerifiedBillingByUserID
			{
				get
				{
					System.String data = entity.LockVerifiedBillingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LockVerifiedBillingByUserID = null;
					else entity.LockVerifiedBillingByUserID = Convert.ToString(value);
				}
			}
			public System.String ProcedureChargeClassID
			{
				get
				{
					System.String data = entity.ProcedureChargeClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureChargeClassID = null;
					else entity.ProcedureChargeClassID = Convert.ToString(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String EmployeeNumber
			{
				get
				{
					System.String data = entity.EmployeeNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNumber = null;
					else entity.EmployeeNumber = Convert.ToString(value);
				}
			}
			public System.String SREmployeeRelationship
			{
				get
				{
					System.String data = entity.SREmployeeRelationship;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeRelationship = null;
					else entity.SREmployeeRelationship = Convert.ToString(value);
				}
			}
			public System.String GuarantorCardNo
			{
				get
				{
					System.String data = entity.GuarantorCardNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorCardNo = null;
					else entity.GuarantorCardNo = Convert.ToString(value);
				}
			}
			public System.String DischargePlanDate
			{
				get
				{
					System.DateTime? data = entity.DischargePlanDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargePlanDate = null;
					else entity.DischargePlanDate = Convert.ToDateTime(value);
				}
			}
			public System.String UsertInsertDischargePlan
			{
				get
				{
					System.String data = entity.UsertInsertDischargePlan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UsertInsertDischargePlan = null;
					else entity.UsertInsertDischargePlan = Convert.ToString(value);
				}
			}
			public System.String IsNonPatient
			{
				get
				{
					System.Boolean? data = entity.IsNonPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonPatient = null;
					else entity.IsNonPatient = Convert.ToBoolean(value);
				}
			}
			public System.String ReasonsForTreatmentID
			{
				get
				{
					System.String data = entity.ReasonsForTreatmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsForTreatmentID = null;
					else entity.ReasonsForTreatmentID = Convert.ToString(value);
				}
			}
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String PatientAdm
			{
				get
				{
					System.Decimal? data = entity.PatientAdm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientAdm = null;
					else entity.PatientAdm = Convert.ToDecimal(value);
				}
			}
			public System.String GuarantorAdm
			{
				get
				{
					System.Decimal? data = entity.GuarantorAdm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorAdm = null;
					else entity.GuarantorAdm = Convert.ToDecimal(value);
				}
			}
			public System.String ReasonsForTreatmentDescID
			{
				get
				{
					System.String data = entity.ReasonsForTreatmentDescID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsForTreatmentDescID = null;
					else entity.ReasonsForTreatmentDescID = Convert.ToString(value);
				}
			}
			public System.String SRReferralGroup
			{
				get
				{
					System.String data = entity.SRReferralGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReferralGroup = null;
					else entity.SRReferralGroup = Convert.ToString(value);
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
			public System.String PhysicianSenders
			{
				get
				{
					System.String data = entity.PhysicianSenders;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianSenders = null;
					else entity.PhysicianSenders = Convert.ToString(value);
				}
			}
			public System.String DiscAdmPatient
			{
				get
				{
					System.Decimal? data = entity.DiscAdmPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscAdmPatient = null;
					else entity.DiscAdmPatient = Convert.ToDecimal(value);
				}
			}
			public System.String DiscAdmGuarantor
			{
				get
				{
					System.Decimal? data = entity.DiscAdmGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscAdmGuarantor = null;
					else entity.DiscAdmGuarantor = Convert.ToDecimal(value);
				}
			}
			public System.String SRPatientInCondition
			{
				get
				{
					System.String data = entity.SRPatientInCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientInCondition = null;
					else entity.SRPatientInCondition = Convert.ToString(value);
				}
			}
			public System.String SRKiaCaseType
			{
				get
				{
					System.String data = entity.SRKiaCaseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKiaCaseType = null;
					else entity.SRKiaCaseType = Convert.ToString(value);
				}
			}
			public System.String SRObstetricType
			{
				get
				{
					System.String data = entity.SRObstetricType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRObstetricType = null;
					else entity.SRObstetricType = Convert.ToString(value);
				}
			}
			public System.String IsHoldTransactionEntryByUserID
			{
				get
				{
					System.String data = entity.IsHoldTransactionEntryByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHoldTransactionEntryByUserID = null;
					else entity.IsHoldTransactionEntryByUserID = Convert.ToString(value);
				}
			}
			public System.String FromRegistrationNo
			{
				get
				{
					System.String data = entity.FromRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRegistrationNo = null;
					else entity.FromRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String IsConfirmedAttendance
			{
				get
				{
					System.Boolean? data = entity.IsConfirmedAttendance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfirmedAttendance = null;
					else entity.IsConfirmedAttendance = Convert.ToBoolean(value);
				}
			}
			public System.String ConfirmedAttendanceByUserID
			{
				get
				{
					System.String data = entity.ConfirmedAttendanceByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmedAttendanceByUserID = null;
					else entity.ConfirmedAttendanceByUserID = Convert.ToString(value);
				}
			}
			public System.String ConfirmedAttendanceDateTime
			{
				get
				{
					System.DateTime? data = entity.ConfirmedAttendanceDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmedAttendanceDateTime = null;
					else entity.ConfirmedAttendanceDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String BpjsSepNo
			{
				get
				{
					System.String data = entity.BpjsSepNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsSepNo = null;
					else entity.BpjsSepNo = Convert.ToString(value);
				}
			}
			public System.String PlavonAmount2
			{
				get
				{
					System.Decimal? data = entity.PlavonAmount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlavonAmount2 = null;
					else entity.PlavonAmount2 = Convert.ToDecimal(value);
				}
			}
			public System.String DeathCertificateNo
			{
				get
				{
					System.String data = entity.DeathCertificateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeathCertificateNo = null;
					else entity.DeathCertificateNo = Convert.ToString(value);
				}
			}
			public System.String BpjsCoverageFormula
			{
				get
				{
					System.Decimal? data = entity.BpjsCoverageFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsCoverageFormula = null;
					else entity.BpjsCoverageFormula = Convert.ToDecimal(value);
				}
			}
			public System.String BpjsPackageID
			{
				get
				{
					System.String data = entity.BpjsPackageID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsPackageID = null;
					else entity.BpjsPackageID = Convert.ToString(value);
				}
			}
			public System.String ApproximatePlafondAmount
			{
				get
				{
					System.Decimal? data = entity.ApproximatePlafondAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproximatePlafondAmount = null;
					else entity.ApproximatePlafondAmount = Convert.ToDecimal(value);
				}
			}
			public System.String SentToBillingDateTime
			{
				get
				{
					System.DateTime? data = entity.SentToBillingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SentToBillingDateTime = null;
					else entity.SentToBillingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SentToBillingByUserID
			{
				get
				{
					System.String data = entity.SentToBillingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SentToBillingByUserID = null;
					else entity.SentToBillingByUserID = Convert.ToString(value);
				}
			}
			public System.String IsAdjusted
			{
				get
				{
					System.Boolean? data = entity.IsAdjusted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdjusted = null;
					else entity.IsAdjusted = Convert.ToBoolean(value);
				}
			}
			public System.String AdjustLog
			{
				get
				{
					System.String data = entity.AdjustLog;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustLog = null;
					else entity.AdjustLog = Convert.ToString(value);
				}
			}
			public System.String IsAllowPatientCheckOut
			{
				get
				{
					System.Boolean? data = entity.IsAllowPatientCheckOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowPatientCheckOut = null;
					else entity.IsAllowPatientCheckOut = Convert.ToBoolean(value);
				}
			}
			public System.String AllowPatientCheckOutDateTime
			{
				get
				{
					System.DateTime? data = entity.AllowPatientCheckOutDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AllowPatientCheckOutDateTime = null;
					else entity.AllowPatientCheckOutDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String AllowPatientCheckOutByUserID
			{
				get
				{
					System.String data = entity.AllowPatientCheckOutByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AllowPatientCheckOutByUserID = null;
					else entity.AllowPatientCheckOutByUserID = Convert.ToString(value);
				}
			}
			public System.String ReferByParamedicID
			{
				get
				{
					System.String data = entity.ReferByParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferByParamedicID = null;
					else entity.ReferByParamedicID = Convert.ToString(value);
				}
			}
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
				}
			}
			public System.String SROccupation
			{
				get
				{
					System.String data = entity.SROccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROccupation = null;
					else entity.SROccupation = Convert.ToString(value);
				}
			}
			public System.String SRRelationshipQuality
			{
				get
				{
					System.String data = entity.SRRelationshipQuality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRelationshipQuality = null;
					else entity.SRRelationshipQuality = Convert.ToString(value);
				}
			}
			public System.String SRResidentialHome
			{
				get
				{
					System.String data = entity.SRResidentialHome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResidentialHome = null;
					else entity.SRResidentialHome = Convert.ToString(value);
				}
			}
			public System.String SRFatherOccupation
			{
				get
				{
					System.String data = entity.SRFatherOccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFatherOccupation = null;
					else entity.SRFatherOccupation = Convert.ToString(value);
				}
			}
			public System.String IsPregnant
			{
				get
				{
					System.Boolean? data = entity.IsPregnant;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPregnant = null;
					else entity.IsPregnant = Convert.ToBoolean(value);
				}
			}
			public System.String GestationalAge
			{
				get
				{
					System.Int16? data = entity.GestationalAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GestationalAge = null;
					else entity.GestationalAge = Convert.ToInt16(value);
				}
			}
			public System.String IsBreastFeeding
			{
				get
				{
					System.Boolean? data = entity.IsBreastFeeding;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBreastFeeding = null;
					else entity.IsBreastFeeding = Convert.ToBoolean(value);
				}
			}
			public System.String AgeOfBabyInYear
			{
				get
				{
					System.Int16? data = entity.AgeOfBabyInYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeOfBabyInYear = null;
					else entity.AgeOfBabyInYear = Convert.ToInt16(value);
				}
			}
			public System.String AgeOfBabyInMonth
			{
				get
				{
					System.Int16? data = entity.AgeOfBabyInMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeOfBabyInMonth = null;
					else entity.AgeOfBabyInMonth = Convert.ToInt16(value);
				}
			}
			public System.String AgeOfBabyInDay
			{
				get
				{
					System.Int16? data = entity.AgeOfBabyInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeOfBabyInDay = null;
					else entity.AgeOfBabyInDay = Convert.ToInt16(value);
				}
			}
			public System.String IsKidneyFunctionImpaired
			{
				get
				{
					System.Boolean? data = entity.IsKidneyFunctionImpaired;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKidneyFunctionImpaired = null;
					else entity.IsKidneyFunctionImpaired = Convert.ToBoolean(value);
				}
			}
			public System.String CreatinineSerumValue
			{
				get
				{
					System.Int16? data = entity.CreatinineSerumValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatinineSerumValue = null;
					else entity.CreatinineSerumValue = Convert.ToInt16(value);
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
			public System.String MembershipDetailID
			{
				get
				{
					System.Int64? data = entity.MembershipDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipDetailID = null;
					else entity.MembershipDetailID = Convert.ToInt64(value);
				}
			}
			public System.String ExternalQueNo
			{
				get
				{
					System.String data = entity.ExternalQueNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExternalQueNo = null;
					else entity.ExternalQueNo = Convert.ToString(value);
				}
			}
			public System.String ReferralIdTo
			{
				get
				{
					System.String data = entity.ReferralIdTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralIdTo = null;
					else entity.ReferralIdTo = Convert.ToString(value);
				}
			}
			public System.String ReferralNameTo
			{
				get
				{
					System.String data = entity.ReferralNameTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralNameTo = null;
					else entity.ReferralNameTo = Convert.ToString(value);
				}
			}
			public System.String IsReconcile
			{
				get
				{
					System.Boolean? data = entity.IsReconcile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReconcile = null;
					else entity.IsReconcile = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkipAutoBill
			{
				get
				{
					System.Boolean? data = entity.IsSkipAutoBill;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkipAutoBill = null;
					else entity.IsSkipAutoBill = Convert.ToBoolean(value);
				}
			}
			public System.String SRCrashSite
			{
				get
				{
					System.String data = entity.SRCrashSite;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCrashSite = null;
					else entity.SRCrashSite = Convert.ToString(value);
				}
			}
			public System.String CrashSiteDetail
			{
				get
				{
					System.String data = entity.CrashSiteDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrashSiteDetail = null;
					else entity.CrashSiteDetail = Convert.ToString(value);
				}
			}
			public System.String MembershipNo
			{
				get
				{
					System.String data = entity.MembershipNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipNo = null;
					else entity.MembershipNo = Convert.ToString(value);
				}
			}
			public System.String IsOpenEntryMR
			{
				get
				{
					System.Boolean? data = entity.IsOpenEntryMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenEntryMR = null;
					else entity.IsOpenEntryMR = Convert.ToBoolean(value);
				}
			}
			public System.String SRCovidStatus
			{
				get
				{
					System.Byte? data = entity.SRCovidStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCovidStatus = null;
					else entity.SRCovidStatus = Convert.ToByte(value);
				}
			}
			public System.String VoucherNo
			{
				get
				{
					System.String data = entity.VoucherNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherNo = null;
					else entity.VoucherNo = Convert.ToString(value);
				}
			}
			public System.String SRCovidComorbidStatus
			{
				get
				{
					System.String data = entity.SRCovidComorbidStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCovidComorbidStatus = null;
					else entity.SRCovidComorbidStatus = Convert.ToString(value);
				}
			}
			public System.String IsDisability
			{
				get
				{
					System.Boolean? data = entity.IsDisability;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisability = null;
					else entity.IsDisability = Convert.ToBoolean(value);
				}
			}
			public System.String IsTracer
			{
				get
				{
					System.Boolean? data = entity.IsTracer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTracer = null;
					else entity.IsTracer = Convert.ToBoolean(value);
				}
			}
			public System.String ItemConditionRuleID
			{
				get
				{
					System.String data = entity.ItemConditionRuleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemConditionRuleID = null;
					else entity.ItemConditionRuleID = Convert.ToString(value);
				}
			}
			public System.String SRPatientRiskStatus
			{
				get
				{
					System.String data = entity.SRPatientRiskStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientRiskStatus = null;
					else entity.SRPatientRiskStatus = Convert.ToString(value);
				}
			}

			public System.String IsFinishedAttendance
			{
				get
				{
					System.Boolean? data = entity.IsFinishedAttendance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFinishedAttendance = null;
					else entity.IsFinishedAttendance = Convert.ToBoolean(value);
				}
			}
			public System.String FinishedAttendanceByUserID
			{
				get
				{
					System.String data = entity.FinishedAttendanceByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinishedAttendanceByUserID = null;
					else entity.FinishedAttendanceByUserID = Convert.ToString(value);
				}
			}
			public System.String FinishedAttendanceDateTime
			{
				get
				{
					System.DateTime? data = entity.FinishedAttendanceDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinishedAttendanceDateTime = null;
					else entity.FinishedAttendanceDateTime = Convert.ToDateTime(value);
				}
			}
            public System.String SRPatientRiskColor
            {
                get
                {
                    System.String data = entity.SRPatientRiskColor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPatientRiskColor = null;
                    else entity.SRPatientRiskColor = Convert.ToString(value);
                }
            }
            private esRegistration entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationQuery query)
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
				throw new Exception("esRegistration can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Registration : esRegistration
	{
	}

	[Serializable]
	abstract public class esRegistrationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RegistrationTime, esSystemType.String);
			}
		}

		public esQueryItem AppointmentNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AppointmentNo, esSystemType.String);
			}
		}

		public esQueryItem AgeInYear
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeInYear, esSystemType.Byte);
			}
		}

		public esQueryItem AgeInMonth
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeInMonth, esSystemType.Byte);
			}
		}

		public esQueryItem AgeInDay
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeInDay, esSystemType.Byte);
			}
		}

		public esQueryItem SRShift
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRShift, esSystemType.String);
			}
		}

		public esQueryItem SRPatientInType
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRPatientInType, esSystemType.String);
			}
		}

		public esQueryItem InsuranceID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.InsuranceID, esSystemType.String);
			}
		}

		public esQueryItem SRPatientCategory
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRPatientCategory, esSystemType.String);
			}
		}

		public esQueryItem SRERCaseType
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRERCaseType, esSystemType.String);
			}
		}

		public esQueryItem SRVisitReason
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRVisitReason, esSystemType.String);
			}
		}

		public esQueryItem SRBussinesMethod
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRBussinesMethod, esSystemType.String);
			}
		}

		public esQueryItem PlavonAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PlavonAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem ChargeClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ChargeClassID, esSystemType.String);
			}
		}

		public esQueryItem CoverageClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.CoverageClassID, esSystemType.String);
			}
		}

		public esQueryItem VisitTypeID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VisitTypeID, esSystemType.String);
			}
		}

		public esQueryItem ReferralID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferralID, esSystemType.String);
			}
		}

		public esQueryItem Anamnesis
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Anamnesis, esSystemType.String);
			}
		}

		public esQueryItem Complaint
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Complaint, esSystemType.String);
			}
		}

		public esQueryItem InitialDiagnose
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.InitialDiagnose, esSystemType.String);
			}
		}

		public esQueryItem MedicationPlanning
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.MedicationPlanning, esSystemType.String);
			}
		}

		public esQueryItem SRTriage
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRTriage, esSystemType.String);
			}
		}

		public esQueryItem IsPrintingPatientCard
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsPrintingPatientCard, esSystemType.Boolean);
			}
		}

		public esQueryItem DischargeDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DischargeTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargeTime, esSystemType.String);
			}
		}

		public esQueryItem DischargeMedicalNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargeMedicalNotes, esSystemType.String);
			}
		}

		public esQueryItem DischargeNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargeNotes, esSystemType.String);
			}
		}

		public esQueryItem SRDischargeCondition
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRDischargeCondition, esSystemType.String);
			}
		}

		public esQueryItem SRDischargeMethod
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRDischargeMethod, esSystemType.String);
			}
		}

		public esQueryItem LOSInYear
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LOSInYear, esSystemType.Byte);
			}
		}

		public esQueryItem LOSInMonth
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LOSInMonth, esSystemType.Byte);
			}
		}

		public esQueryItem LOSInDay
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LOSInDay, esSystemType.Byte);
			}
		}

		public esQueryItem DischargeOperatorID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargeOperatorID, esSystemType.String);
			}
		}

		public esQueryItem AccountNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AccountNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.TransactionAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem AdministrationAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AdministrationAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem RoundingAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem RemainingAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RemainingAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsTransferedToInpatient
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsTransferedToInpatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNewPatient
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsNewPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNewBornInfant
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsNewBornInfant, esSystemType.Boolean);
			}
		}

		public esQueryItem IsParturition
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsParturition, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHoldTransactionEntry
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsHoldTransactionEntry, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHasCorrection
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsHasCorrection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEMRValid
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsEMRValid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBackDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsBackDate, esSystemType.Boolean);
			}
		}

		public esQueryItem ActualVisitDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ActualVisitDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem SRVoidReason
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRVoidReason, esSystemType.String);
			}
		}

		public esQueryItem VoidNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VoidNotes, esSystemType.String);
			}
		}

		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEpisodeComplete
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsEpisodeComplete, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClusterAssessment
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsClusterAssessment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsConsul
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsConsul, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFromDispensary
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsFromDispensary, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNewVisit
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsNewVisit, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCreateUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LastCreateUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsDirectPrescriptionReturn
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsDirectPrescriptionReturn, esSystemType.Boolean);
			}
		}

		public esQueryItem RegistrationQue
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RegistrationQue, esSystemType.Int32);
			}
		}

		public esQueryItem VisiteRegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VisiteRegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem IsGenerateHL7
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsGenerateHL7, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferralName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferralName, esSystemType.String);
			}
		}

		public esQueryItem IsObservation
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsObservation, esSystemType.Boolean);
			}
		}

		public esQueryItem CauseOfAccident
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.CauseOfAccident, esSystemType.String);
			}
		}

		public esQueryItem ReferTo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferTo, esSystemType.String);
			}
		}

		public esQueryItem IsOldCase
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsOldCase, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDHF
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsDHF, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEKG
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsEKG, esSystemType.Boolean);
			}
		}

		public esQueryItem EmrDiagnoseID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.EmrDiagnoseID, esSystemType.String);
			}
		}

		public esQueryItem IsGlobalPlafond
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsGlobalPlafond, esSystemType.Boolean);
			}
		}

		public esQueryItem FirstResponDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.FirstResponDate, esSystemType.DateTime);
			}
		}

		public esQueryItem FirstResponTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.FirstResponTime, esSystemType.String);
			}
		}

		public esQueryItem PhysicianResponDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PhysicianResponDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PhysicianResponTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PhysicianResponTime, esSystemType.String);
			}
		}

		public esQueryItem IsRoomIn
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsRoomIn, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLockVerifiedBilling
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsLockVerifiedBilling, esSystemType.Boolean);
			}
		}

		public esQueryItem LockVerifiedBillingDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LockVerifiedBillingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LockVerifiedBillingByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LockVerifiedBillingByUserID, esSystemType.String);
			}
		}

		public esQueryItem ProcedureChargeClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ProcedureChargeClassID, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeNumber
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeRelationship
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SREmployeeRelationship, esSystemType.String);
			}
		}

		public esQueryItem GuarantorCardNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GuarantorCardNo, esSystemType.String);
			}
		}

		public esQueryItem DischargePlanDate
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DischargePlanDate, esSystemType.DateTime);
			}
		}

		public esQueryItem UsertInsertDischargePlan
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.UsertInsertDischargePlan, esSystemType.String);
			}
		}

		public esQueryItem IsNonPatient
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsNonPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem ReasonsForTreatmentID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReasonsForTreatmentID, esSystemType.String);
			}
		}

		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		}

		public esQueryItem PatientAdm
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PatientAdm, esSystemType.Decimal);
			}
		}

		public esQueryItem GuarantorAdm
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GuarantorAdm, esSystemType.Decimal);
			}
		}

		public esQueryItem ReasonsForTreatmentDescID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReasonsForTreatmentDescID, esSystemType.String);
			}
		}

		public esQueryItem SRReferralGroup
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRReferralGroup, esSystemType.String);
			}
		}

		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		}

		public esQueryItem PhysicianSenders
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PhysicianSenders, esSystemType.String);
			}
		}

		public esQueryItem DiscAdmPatient
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DiscAdmPatient, esSystemType.Decimal);
			}
		}

		public esQueryItem DiscAdmGuarantor
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DiscAdmGuarantor, esSystemType.Decimal);
			}
		}

		public esQueryItem SRPatientInCondition
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRPatientInCondition, esSystemType.String);
			}
		}

		public esQueryItem SRKiaCaseType
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRKiaCaseType, esSystemType.String);
			}
		}

		public esQueryItem SRObstetricType
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRObstetricType, esSystemType.String);
			}
		}

		public esQueryItem IsHoldTransactionEntryByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsHoldTransactionEntryByUserID, esSystemType.String);
			}
		}

		public esQueryItem FromRegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.FromRegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem IsConfirmedAttendance
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsConfirmedAttendance, esSystemType.Boolean);
			}
		}

		public esQueryItem ConfirmedAttendanceByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ConfirmedAttendanceByUserID, esSystemType.String);
			}
		}

		public esQueryItem ConfirmedAttendanceDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ConfirmedAttendanceDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem BpjsSepNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BpjsSepNo, esSystemType.String);
			}
		}

		public esQueryItem PlavonAmount2
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PlavonAmount2, esSystemType.Decimal);
			}
		}

		public esQueryItem DeathCertificateNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DeathCertificateNo, esSystemType.String);
			}
		}

		public esQueryItem BpjsCoverageFormula
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BpjsCoverageFormula, esSystemType.Decimal);
			}
		}

		public esQueryItem BpjsPackageID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BpjsPackageID, esSystemType.String);
			}
		}

		public esQueryItem ApproximatePlafondAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ApproximatePlafondAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem SentToBillingDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SentToBillingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SentToBillingByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SentToBillingByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsAdjusted
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsAdjusted, esSystemType.Boolean);
			}
		}

		public esQueryItem AdjustLog
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AdjustLog, esSystemType.String);
			}
		}

		public esQueryItem IsAllowPatientCheckOut
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsAllowPatientCheckOut, esSystemType.Boolean);
			}
		}

		public esQueryItem AllowPatientCheckOutDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AllowPatientCheckOutDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem AllowPatientCheckOutByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AllowPatientCheckOutByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReferByParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferByParamedicID, esSystemType.String);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem SROccupation
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SROccupation, esSystemType.String);
			}
		}

		public esQueryItem SRRelationshipQuality
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRRelationshipQuality, esSystemType.String);
			}
		}

		public esQueryItem SRResidentialHome
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRResidentialHome, esSystemType.String);
			}
		}

		public esQueryItem SRFatherOccupation
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRFatherOccupation, esSystemType.String);
			}
		}

		public esQueryItem IsPregnant
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsPregnant, esSystemType.Boolean);
			}
		}

		public esQueryItem GestationalAge
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GestationalAge, esSystemType.Int16);
			}
		}

		public esQueryItem IsBreastFeeding
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsBreastFeeding, esSystemType.Boolean);
			}
		}

		public esQueryItem AgeOfBabyInYear
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeOfBabyInYear, esSystemType.Int16);
			}
		}

		public esQueryItem AgeOfBabyInMonth
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeOfBabyInMonth, esSystemType.Int16);
			}
		}

		public esQueryItem AgeOfBabyInDay
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgeOfBabyInDay, esSystemType.Int16);
			}
		}

		public esQueryItem IsKidneyFunctionImpaired
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsKidneyFunctionImpaired, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatinineSerumValue
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.CreatinineSerumValue, esSystemType.Int16);
			}
		}

		public esQueryItem Hpi
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Hpi, esSystemType.String);
			}
		}

		public esQueryItem MembershipDetailID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.MembershipDetailID, esSystemType.Int64);
			}
		}

		public esQueryItem ExternalQueNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ExternalQueNo, esSystemType.String);
			}
		}

		public esQueryItem ReferralIdTo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferralIdTo, esSystemType.String);
			}
		}

		public esQueryItem ReferralNameTo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ReferralNameTo, esSystemType.String);
			}
		}

		public esQueryItem IsReconcile
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsReconcile, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSkipAutoBill
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsSkipAutoBill, esSystemType.Boolean);
			}
		}

		public esQueryItem SRCrashSite
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRCrashSite, esSystemType.String);
			}
		}

		public esQueryItem CrashSiteDetail
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.CrashSiteDetail, esSystemType.String);
			}
		}

		public esQueryItem MembershipNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.MembershipNo, esSystemType.String);
			}
		}

		public esQueryItem IsOpenEntryMR
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsOpenEntryMR, esSystemType.Boolean);
			}
		}

		public esQueryItem SRCovidStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRCovidStatus, esSystemType.Byte);
			}
		}

		public esQueryItem VoucherNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VoucherNo, esSystemType.String);
			}
		}

		public esQueryItem SRCovidComorbidStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRCovidComorbidStatus, esSystemType.String);
			}
		}

		public esQueryItem IsDisability
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsDisability, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTracer
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsTracer, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemConditionRuleID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
			}
		}

		public esQueryItem SRPatientRiskStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRPatientRiskStatus, esSystemType.String);
			}
		}

		public esQueryItem IsFinishedAttendance
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.IsFinishedAttendance, esSystemType.Boolean);
			}
		}

		public esQueryItem FinishedAttendanceByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.FinishedAttendanceByUserID, esSystemType.String);
			}
		}

		public esQueryItem FinishedAttendanceDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.FinishedAttendanceDateTime, esSystemType.DateTime);
			}
		}

        public esQueryItem SRPatientRiskColor
        {
            get
            {
                return new esQueryItem(this, RegistrationMetadata.ColumnNames.SRPatientRiskColor, esSystemType.String);
            }
        }

    }

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCollection")]
	public partial class RegistrationCollection : esRegistrationCollection, IEnumerable<Registration>
	{
		public RegistrationCollection()
		{

		}

		public static implicit operator List<Registration>(RegistrationCollection coll)
		{
			List<Registration> list = new List<Registration>();

			foreach (Registration emp in coll)
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
				return RegistrationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Registration(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Registration();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuery();
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
		public bool Load(RegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Registration AddNew()
		{
			Registration entity = base.AddNewEntity() as Registration;

			return entity;
		}
		public Registration FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as Registration;
		}

		#region IEnumerable< Registration> Members

		IEnumerator<Registration> IEnumerable<Registration>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Registration;
			}
		}

		#endregion

		private RegistrationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Registration' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Registration ({RegistrationNo})")]
	[Serializable]
	public partial class Registration : esRegistration
	{
		public Registration()
		{
		}

		public Registration(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMetadata.Meta();
			}
		}

		override protected esRegistrationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuery();
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
		public bool Load(RegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationQuery : esRegistrationQuery
	{
		public RegistrationQuery()
		{

		}

		public RegistrationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationQuery";
		}
	}

	[Serializable]
	public partial class RegistrationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRRegistrationType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GuarantorID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PatientID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ClassID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RegistrationDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.RegistrationDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RegistrationTime, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RegistrationTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AppointmentNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AppointmentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeInYear, 9, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeInYear;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeInMonth, 10, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeInMonth;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeInDay, 11, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeInDay;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRShift, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRShift;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRPatientInType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRPatientInType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.InsuranceID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.InsuranceID;
			c.CharacterMaxLength = 255;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRPatientCategory, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRPatientCategory;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRERCaseType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRERCaseType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRVisitReason, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRVisitReason;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRBussinesMethod, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRBussinesMethod;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PlavonAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.PlavonAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DepartmentID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ServiceUnitID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RoomID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BedID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ChargeClassID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ChargeClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.CoverageClassID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.CoverageClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VisitTypeID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VisitTypeID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferralID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferralID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Anamnesis, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.Anamnesis;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Complaint, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.Complaint;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.InitialDiagnose, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.InitialDiagnose;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.MedicationPlanning, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.MedicationPlanning;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRTriage, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRTriage;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsPrintingPatientCard, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsPrintingPatientCard;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargeDate, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargeDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargeTime, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargeTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargeMedicalNotes, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargeMedicalNotes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargeNotes, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargeNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRDischargeCondition, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRDischargeCondition;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRDischargeMethod, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRDischargeMethod;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LOSInYear, 40, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.LOSInYear;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LOSInMonth, 41, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.LOSInMonth;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LOSInDay, 42, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.LOSInDay;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargeOperatorID, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargeOperatorID;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AccountNo, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AccountNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.TransactionAmount, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.TransactionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AdministrationAmount, 46, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.AdministrationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RoundingAmount, 47, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.RoundingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RemainingAmount, 48, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.RemainingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsTransferedToInpatient, 49, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsTransferedToInpatient;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsNewPatient, 50, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsNewPatient;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsNewBornInfant, 51, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsNewBornInfant;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsParturition, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsParturition;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsHoldTransactionEntry, 53, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsHoldTransactionEntry;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsHasCorrection, 54, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsHasCorrection;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsEMRValid, 55, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsEMRValid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsBackDate, 56, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsBackDate;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ActualVisitDate, 57, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.ActualVisitDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsVoid, 58, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRVoidReason, 59, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRVoidReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VoidNotes, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VoidNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VoidDate, 61, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VoidByUserID, 62, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsClosed, 63, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsClosed;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsEpisodeComplete, 64, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsEpisodeComplete;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsClusterAssessment, 65, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsClusterAssessment;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsConsul, 66, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsConsul;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsFromDispensary, 67, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsFromDispensary;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsNewVisit, 68, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsNewVisit;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Notes, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LastCreateDateTime, 70, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LastCreateUserID, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.LastCreateUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LastUpdateDateTime, 72, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LastUpdateByUserID, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsDirectPrescriptionReturn, 74, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsDirectPrescriptionReturn;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RegistrationQue, 75, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationMetadata.PropertyNames.RegistrationQue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VisiteRegistrationNo, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VisiteRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsGenerateHL7, 77, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsGenerateHL7;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferralName, 78, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferralName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsObservation, 79, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsObservation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.CauseOfAccident, 80, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.CauseOfAccident;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferTo, 81, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferTo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsOldCase, 82, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsOldCase;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsDHF, 83, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsDHF;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsEKG, 84, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsEKG;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.EmrDiagnoseID, 85, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.EmrDiagnoseID;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsGlobalPlafond, 86, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsGlobalPlafond;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.FirstResponDate, 87, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.FirstResponDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.FirstResponTime, 88, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.FirstResponTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PhysicianResponDate, 89, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.PhysicianResponDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PhysicianResponTime, 90, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.PhysicianResponTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsRoomIn, 91, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsRoomIn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsLockVerifiedBilling, 92, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsLockVerifiedBilling;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LockVerifiedBillingDateTime, 93, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.LockVerifiedBillingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LockVerifiedBillingByUserID, 94, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.LockVerifiedBillingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ProcedureChargeClassID, 95, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ProcedureChargeClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PersonID, 96, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.EmployeeNumber, 97, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.EmployeeNumber;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SREmployeeRelationship, 98, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SREmployeeRelationship;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GuarantorCardNo, 99, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.GuarantorCardNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DischargePlanDate, 100, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.DischargePlanDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.UsertInsertDischargePlan, 101, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.UsertInsertDischargePlan;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsNonPatient, 102, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsNonPatient;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReasonsForTreatmentID, 103, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReasonsForTreatmentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SmfID, 104, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PatientAdm, 105, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.PatientAdm;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GuarantorAdm, 106, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.GuarantorAdm;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReasonsForTreatmentDescID, 107, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReasonsForTreatmentDescID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRReferralGroup, 108, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRReferralGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRDiscountReason, 109, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PhysicianSenders, 110, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.PhysicianSenders;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DiscAdmPatient, 111, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.DiscAdmPatient;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DiscAdmGuarantor, 112, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.DiscAdmGuarantor;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRPatientInCondition, 113, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRPatientInCondition;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRKiaCaseType, 114, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRKiaCaseType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRObstetricType, 115, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRObstetricType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsHoldTransactionEntryByUserID, 116, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsHoldTransactionEntryByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.FromRegistrationNo, 117, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.FromRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsConfirmedAttendance, 118, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsConfirmedAttendance;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ConfirmedAttendanceByUserID, 119, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ConfirmedAttendanceByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ConfirmedAttendanceDateTime, 120, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.ConfirmedAttendanceDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BpjsSepNo, 121, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.BpjsSepNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PlavonAmount2, 122, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.PlavonAmount2;
			c.NumericPrecision = 18;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DeathCertificateNo, 123, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DeathCertificateNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BpjsCoverageFormula, 124, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.BpjsCoverageFormula;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BpjsPackageID, 125, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.BpjsPackageID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ApproximatePlafondAmount, 126, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationMetadata.PropertyNames.ApproximatePlafondAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SentToBillingDateTime, 127, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.SentToBillingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SentToBillingByUserID, 128, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SentToBillingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsAdjusted, 129, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsAdjusted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AdjustLog, 130, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AdjustLog;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsAllowPatientCheckOut, 131, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsAllowPatientCheckOut;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AllowPatientCheckOutDateTime, 132, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.AllowPatientCheckOutDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AllowPatientCheckOutByUserID, 133, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AllowPatientCheckOutByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferByParamedicID, 134, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferByParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRMaritalStatus, 135, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SROccupation, 136, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SROccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRRelationshipQuality, 137, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRRelationshipQuality;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRResidentialHome, 138, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRResidentialHome;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRFatherOccupation, 139, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRFatherOccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsPregnant, 140, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsPregnant;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GestationalAge, 141, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RegistrationMetadata.PropertyNames.GestationalAge;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsBreastFeeding, 142, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsBreastFeeding;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeOfBabyInYear, 143, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeOfBabyInYear;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeOfBabyInMonth, 144, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeOfBabyInMonth;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgeOfBabyInDay, 145, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgeOfBabyInDay;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsKidneyFunctionImpaired, 146, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsKidneyFunctionImpaired;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.CreatinineSerumValue, 147, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RegistrationMetadata.PropertyNames.CreatinineSerumValue;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Hpi, 148, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.Hpi;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.MembershipDetailID, 149, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = RegistrationMetadata.PropertyNames.MembershipDetailID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ExternalQueNo, 150, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ExternalQueNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferralIdTo, 151, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferralIdTo;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ReferralNameTo, 152, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ReferralNameTo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsReconcile, 153, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsReconcile;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsSkipAutoBill, 154, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsSkipAutoBill;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRCrashSite, 155, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRCrashSite;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.CrashSiteDetail, 156, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.CrashSiteDetail;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.MembershipNo, 157, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.MembershipNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsOpenEntryMR, 158, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsOpenEntryMR;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRCovidStatus, 159, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRCovidStatus;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VoucherNo, 160, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VoucherNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRCovidComorbidStatus, 161, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRCovidComorbidStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsDisability, 162, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsDisability;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsTracer, 163, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsTracer;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ItemConditionRuleID, 164, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ItemConditionRuleID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRPatientRiskStatus, 165, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.SRPatientRiskStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.IsFinishedAttendance, 166, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.IsFinishedAttendance;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.FinishedAttendanceByUserID, 167, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.FinishedAttendanceByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.FinishedAttendanceDateTime, 168, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.FinishedAttendanceDateTime;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(RegistrationMetadata.ColumnNames.SRPatientRiskColor, 169, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMetadata.PropertyNames.SRPatientRiskColor;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
		#endregion

		static public RegistrationMetadata Meta()
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
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ParamedicID = "ParamedicID";
			public const string GuarantorID = "GuarantorID";
			public const string PatientID = "PatientID";
			public const string ClassID = "ClassID";
			public const string RegistrationDate = "RegistrationDate";
			public const string RegistrationTime = "RegistrationTime";
			public const string AppointmentNo = "AppointmentNo";
			public const string AgeInYear = "AgeInYear";
			public const string AgeInMonth = "AgeInMonth";
			public const string AgeInDay = "AgeInDay";
			public const string SRShift = "SRShift";
			public const string SRPatientInType = "SRPatientInType";
			public const string InsuranceID = "InsuranceID";
			public const string SRPatientCategory = "SRPatientCategory";
			public const string SRERCaseType = "SRERCaseType";
			public const string SRVisitReason = "SRVisitReason";
			public const string SRBussinesMethod = "SRBussinesMethod";
			public const string PlavonAmount = "PlavonAmount";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string ChargeClassID = "ChargeClassID";
			public const string CoverageClassID = "CoverageClassID";
			public const string VisitTypeID = "VisitTypeID";
			public const string ReferralID = "ReferralID";
			public const string Anamnesis = "Anamnesis";
			public const string Complaint = "Complaint";
			public const string InitialDiagnose = "InitialDiagnose";
			public const string MedicationPlanning = "MedicationPlanning";
			public const string SRTriage = "SRTriage";
			public const string IsPrintingPatientCard = "IsPrintingPatientCard";
			public const string DischargeDate = "DischargeDate";
			public const string DischargeTime = "DischargeTime";
			public const string DischargeMedicalNotes = "DischargeMedicalNotes";
			public const string DischargeNotes = "DischargeNotes";
			public const string SRDischargeCondition = "SRDischargeCondition";
			public const string SRDischargeMethod = "SRDischargeMethod";
			public const string LOSInYear = "LOSInYear";
			public const string LOSInMonth = "LOSInMonth";
			public const string LOSInDay = "LOSInDay";
			public const string DischargeOperatorID = "DischargeOperatorID";
			public const string AccountNo = "AccountNo";
			public const string TransactionAmount = "TransactionAmount";
			public const string AdministrationAmount = "AdministrationAmount";
			public const string RoundingAmount = "RoundingAmount";
			public const string RemainingAmount = "RemainingAmount";
			public const string IsTransferedToInpatient = "IsTransferedToInpatient";
			public const string IsNewPatient = "IsNewPatient";
			public const string IsNewBornInfant = "IsNewBornInfant";
			public const string IsParturition = "IsParturition";
			public const string IsHoldTransactionEntry = "IsHoldTransactionEntry";
			public const string IsHasCorrection = "IsHasCorrection";
			public const string IsEMRValid = "IsEMRValid";
			public const string IsBackDate = "IsBackDate";
			public const string ActualVisitDate = "ActualVisitDate";
			public const string IsVoid = "IsVoid";
			public const string SRVoidReason = "SRVoidReason";
			public const string VoidNotes = "VoidNotes";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string IsEpisodeComplete = "IsEpisodeComplete";
			public const string IsClusterAssessment = "IsClusterAssessment";
			public const string IsConsul = "IsConsul";
			public const string IsFromDispensary = "IsFromDispensary";
			public const string IsNewVisit = "IsNewVisit";
			public const string Notes = "Notes";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateUserID = "LastCreateUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDirectPrescriptionReturn = "isDirectPrescriptionReturn";
			public const string RegistrationQue = "RegistrationQue";
			public const string VisiteRegistrationNo = "VisiteRegistrationNo";
			public const string IsGenerateHL7 = "IsGenerateHL7";
			public const string ReferralName = "ReferralName";
			public const string IsObservation = "IsObservation";
			public const string CauseOfAccident = "CauseOfAccident";
			public const string ReferTo = "ReferTo";
			public const string IsOldCase = "IsOldCase";
			public const string IsDHF = "IsDHF";
			public const string IsEKG = "IsEKG";
			public const string EmrDiagnoseID = "EmrDiagnoseID";
			public const string IsGlobalPlafond = "IsGlobalPlafond";
			public const string FirstResponDate = "FirstResponDate";
			public const string FirstResponTime = "FirstResponTime";
			public const string PhysicianResponDate = "PhysicianResponDate";
			public const string PhysicianResponTime = "PhysicianResponTime";
			public const string IsRoomIn = "IsRoomIn";
			public const string IsLockVerifiedBilling = "IsLockVerifiedBilling";
			public const string LockVerifiedBillingDateTime = "LockVerifiedBillingDateTime";
			public const string LockVerifiedBillingByUserID = "LockVerifiedBillingByUserID";
			public const string ProcedureChargeClassID = "ProcedureChargeClassID";
			public const string PersonID = "PersonID";
			public const string EmployeeNumber = "EmployeeNumber";
			public const string SREmployeeRelationship = "SREmployeeRelationship";
			public const string GuarantorCardNo = "GuarantorCardNo";
			public const string DischargePlanDate = "DischargePlanDate";
			public const string UsertInsertDischargePlan = "UsertInsertDischargePlan";
			public const string IsNonPatient = "IsNonPatient";
			public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
			public const string SmfID = "SmfID";
			public const string PatientAdm = "PatientAdm";
			public const string GuarantorAdm = "GuarantorAdm";
			public const string ReasonsForTreatmentDescID = "ReasonsForTreatmentDescID";
			public const string SRReferralGroup = "SRReferralGroup";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string DiscAdmPatient = "DiscAdmPatient";
			public const string DiscAdmGuarantor = "DiscAdmGuarantor";
			public const string SRPatientInCondition = "SRPatientInCondition";
			public const string SRKiaCaseType = "SRKiaCaseType";
			public const string SRObstetricType = "SRObstetricType";
			public const string IsHoldTransactionEntryByUserID = "IsHoldTransactionEntryByUserID";
			public const string FromRegistrationNo = "FromRegistrationNo";
			public const string IsConfirmedAttendance = "IsConfirmedAttendance";
			public const string ConfirmedAttendanceByUserID = "ConfirmedAttendanceByUserID";
			public const string ConfirmedAttendanceDateTime = "ConfirmedAttendanceDateTime";
			public const string BpjsSepNo = "BpjsSepNo";
			public const string PlavonAmount2 = "PlavonAmount2";
			public const string DeathCertificateNo = "DeathCertificateNo";
			public const string BpjsCoverageFormula = "BpjsCoverageFormula";
			public const string BpjsPackageID = "BpjsPackageID";
			public const string ApproximatePlafondAmount = "ApproximatePlafondAmount";
			public const string SentToBillingDateTime = "SentToBillingDateTime";
			public const string SentToBillingByUserID = "SentToBillingByUserID";
			public const string IsAdjusted = "IsAdjusted";
			public const string AdjustLog = "AdjustLog";
			public const string IsAllowPatientCheckOut = "IsAllowPatientCheckOut";
			public const string AllowPatientCheckOutDateTime = "AllowPatientCheckOutDateTime";
			public const string AllowPatientCheckOutByUserID = "AllowPatientCheckOutByUserID";
			public const string ReferByParamedicID = "ReferByParamedicID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string SROccupation = "SROccupation";
			public const string SRRelationshipQuality = "SRRelationshipQuality";
			public const string SRResidentialHome = "SRResidentialHome";
			public const string SRFatherOccupation = "SRFatherOccupation";
			public const string IsPregnant = "IsPregnant";
			public const string GestationalAge = "GestationalAge";
			public const string IsBreastFeeding = "IsBreastFeeding";
			public const string AgeOfBabyInYear = "AgeOfBabyInYear";
			public const string AgeOfBabyInMonth = "AgeOfBabyInMonth";
			public const string AgeOfBabyInDay = "AgeOfBabyInDay";
			public const string IsKidneyFunctionImpaired = "IsKidneyFunctionImpaired";
			public const string CreatinineSerumValue = "CreatinineSerumValue";
			public const string Hpi = "Hpi";
			public const string MembershipDetailID = "MembershipDetailID";
			public const string ExternalQueNo = "ExternalQueNo";
			public const string ReferralIdTo = "ReferralIdTo";
			public const string ReferralNameTo = "ReferralNameTo";
			public const string IsReconcile = "IsReconcile";
			public const string IsSkipAutoBill = "IsSkipAutoBill";
			public const string SRCrashSite = "SRCrashSite";
			public const string CrashSiteDetail = "CrashSiteDetail";
			public const string MembershipNo = "MembershipNo";
			public const string IsOpenEntryMR = "IsOpenEntryMR";
			public const string SRCovidStatus = "SRCovidStatus";
			public const string VoucherNo = "VoucherNo";
			public const string SRCovidComorbidStatus = "SRCovidComorbidStatus";
			public const string IsDisability = "IsDisability";
			public const string IsTracer = "IsTracer";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string SRPatientRiskStatus = "SRPatientRiskStatus";
			public const string IsFinishedAttendance = "IsFinishedAttendance";
			public const string FinishedAttendanceByUserID = "FinishedAttendanceByUserID";
			public const string FinishedAttendanceDateTime = "FinishedAttendanceDateTime";
            public const string SRPatientRiskColor = "SRPatientRiskColor";
        }
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ParamedicID = "ParamedicID";
			public const string GuarantorID = "GuarantorID";
			public const string PatientID = "PatientID";
			public const string ClassID = "ClassID";
			public const string RegistrationDate = "RegistrationDate";
			public const string RegistrationTime = "RegistrationTime";
			public const string AppointmentNo = "AppointmentNo";
			public const string AgeInYear = "AgeInYear";
			public const string AgeInMonth = "AgeInMonth";
			public const string AgeInDay = "AgeInDay";
			public const string SRShift = "SRShift";
			public const string SRPatientInType = "SRPatientInType";
			public const string InsuranceID = "InsuranceID";
			public const string SRPatientCategory = "SRPatientCategory";
			public const string SRERCaseType = "SRERCaseType";
			public const string SRVisitReason = "SRVisitReason";
			public const string SRBussinesMethod = "SRBussinesMethod";
			public const string PlavonAmount = "PlavonAmount";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string ChargeClassID = "ChargeClassID";
			public const string CoverageClassID = "CoverageClassID";
			public const string VisitTypeID = "VisitTypeID";
			public const string ReferralID = "ReferralID";
			public const string Anamnesis = "Anamnesis";
			public const string Complaint = "Complaint";
			public const string InitialDiagnose = "InitialDiagnose";
			public const string MedicationPlanning = "MedicationPlanning";
			public const string SRTriage = "SRTriage";
			public const string IsPrintingPatientCard = "IsPrintingPatientCard";
			public const string DischargeDate = "DischargeDate";
			public const string DischargeTime = "DischargeTime";
			public const string DischargeMedicalNotes = "DischargeMedicalNotes";
			public const string DischargeNotes = "DischargeNotes";
			public const string SRDischargeCondition = "SRDischargeCondition";
			public const string SRDischargeMethod = "SRDischargeMethod";
			public const string LOSInYear = "LOSInYear";
			public const string LOSInMonth = "LOSInMonth";
			public const string LOSInDay = "LOSInDay";
			public const string DischargeOperatorID = "DischargeOperatorID";
			public const string AccountNo = "AccountNo";
			public const string TransactionAmount = "TransactionAmount";
			public const string AdministrationAmount = "AdministrationAmount";
			public const string RoundingAmount = "RoundingAmount";
			public const string RemainingAmount = "RemainingAmount";
			public const string IsTransferedToInpatient = "IsTransferedToInpatient";
			public const string IsNewPatient = "IsNewPatient";
			public const string IsNewBornInfant = "IsNewBornInfant";
			public const string IsParturition = "IsParturition";
			public const string IsHoldTransactionEntry = "IsHoldTransactionEntry";
			public const string IsHasCorrection = "IsHasCorrection";
			public const string IsEMRValid = "IsEMRValid";
			public const string IsBackDate = "IsBackDate";
			public const string ActualVisitDate = "ActualVisitDate";
			public const string IsVoid = "IsVoid";
			public const string SRVoidReason = "SRVoidReason";
			public const string VoidNotes = "VoidNotes";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string IsEpisodeComplete = "IsEpisodeComplete";
			public const string IsClusterAssessment = "IsClusterAssessment";
			public const string IsConsul = "IsConsul";
			public const string IsFromDispensary = "IsFromDispensary";
			public const string IsNewVisit = "IsNewVisit";
			public const string Notes = "Notes";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateUserID = "LastCreateUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsDirectPrescriptionReturn = "IsDirectPrescriptionReturn";
			public const string RegistrationQue = "RegistrationQue";
			public const string VisiteRegistrationNo = "VisiteRegistrationNo";
			public const string IsGenerateHL7 = "IsGenerateHL7";
			public const string ReferralName = "ReferralName";
			public const string IsObservation = "IsObservation";
			public const string CauseOfAccident = "CauseOfAccident";
			public const string ReferTo = "ReferTo";
			public const string IsOldCase = "IsOldCase";
			public const string IsDHF = "IsDHF";
			public const string IsEKG = "IsEKG";
			public const string EmrDiagnoseID = "EmrDiagnoseID";
			public const string IsGlobalPlafond = "IsGlobalPlafond";
			public const string FirstResponDate = "FirstResponDate";
			public const string FirstResponTime = "FirstResponTime";
			public const string PhysicianResponDate = "PhysicianResponDate";
			public const string PhysicianResponTime = "PhysicianResponTime";
			public const string IsRoomIn = "IsRoomIn";
			public const string IsLockVerifiedBilling = "IsLockVerifiedBilling";
			public const string LockVerifiedBillingDateTime = "LockVerifiedBillingDateTime";
			public const string LockVerifiedBillingByUserID = "LockVerifiedBillingByUserID";
			public const string ProcedureChargeClassID = "ProcedureChargeClassID";
			public const string PersonID = "PersonID";
			public const string EmployeeNumber = "EmployeeNumber";
			public const string SREmployeeRelationship = "SREmployeeRelationship";
			public const string GuarantorCardNo = "GuarantorCardNo";
			public const string DischargePlanDate = "DischargePlanDate";
			public const string UsertInsertDischargePlan = "UsertInsertDischargePlan";
			public const string IsNonPatient = "IsNonPatient";
			public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
			public const string SmfID = "SmfID";
			public const string PatientAdm = "PatientAdm";
			public const string GuarantorAdm = "GuarantorAdm";
			public const string ReasonsForTreatmentDescID = "ReasonsForTreatmentDescID";
			public const string SRReferralGroup = "SRReferralGroup";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string DiscAdmPatient = "DiscAdmPatient";
			public const string DiscAdmGuarantor = "DiscAdmGuarantor";
			public const string SRPatientInCondition = "SRPatientInCondition";
			public const string SRKiaCaseType = "SRKiaCaseType";
			public const string SRObstetricType = "SRObstetricType";
			public const string IsHoldTransactionEntryByUserID = "IsHoldTransactionEntryByUserID";
			public const string FromRegistrationNo = "FromRegistrationNo";
			public const string IsConfirmedAttendance = "IsConfirmedAttendance";
			public const string ConfirmedAttendanceByUserID = "ConfirmedAttendanceByUserID";
			public const string ConfirmedAttendanceDateTime = "ConfirmedAttendanceDateTime";
			public const string BpjsSepNo = "BpjsSepNo";
			public const string PlavonAmount2 = "PlavonAmount2";
			public const string DeathCertificateNo = "DeathCertificateNo";
			public const string BpjsCoverageFormula = "BpjsCoverageFormula";
			public const string BpjsPackageID = "BpjsPackageID";
			public const string ApproximatePlafondAmount = "ApproximatePlafondAmount";
			public const string SentToBillingDateTime = "SentToBillingDateTime";
			public const string SentToBillingByUserID = "SentToBillingByUserID";
			public const string IsAdjusted = "IsAdjusted";
			public const string AdjustLog = "AdjustLog";
			public const string IsAllowPatientCheckOut = "IsAllowPatientCheckOut";
			public const string AllowPatientCheckOutDateTime = "AllowPatientCheckOutDateTime";
			public const string AllowPatientCheckOutByUserID = "AllowPatientCheckOutByUserID";
			public const string ReferByParamedicID = "ReferByParamedicID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string SROccupation = "SROccupation";
			public const string SRRelationshipQuality = "SRRelationshipQuality";
			public const string SRResidentialHome = "SRResidentialHome";
			public const string SRFatherOccupation = "SRFatherOccupation";
			public const string IsPregnant = "IsPregnant";
			public const string GestationalAge = "GestationalAge";
			public const string IsBreastFeeding = "IsBreastFeeding";
			public const string AgeOfBabyInYear = "AgeOfBabyInYear";
			public const string AgeOfBabyInMonth = "AgeOfBabyInMonth";
			public const string AgeOfBabyInDay = "AgeOfBabyInDay";
			public const string IsKidneyFunctionImpaired = "IsKidneyFunctionImpaired";
			public const string CreatinineSerumValue = "CreatinineSerumValue";
			public const string Hpi = "Hpi";
			public const string MembershipDetailID = "MembershipDetailID";
			public const string ExternalQueNo = "ExternalQueNo";
			public const string ReferralIdTo = "ReferralIdTo";
			public const string ReferralNameTo = "ReferralNameTo";
			public const string IsReconcile = "IsReconcile";
			public const string IsSkipAutoBill = "IsSkipAutoBill";
			public const string SRCrashSite = "SRCrashSite";
			public const string CrashSiteDetail = "CrashSiteDetail";
			public const string MembershipNo = "MembershipNo";
			public const string IsOpenEntryMR = "IsOpenEntryMR";
			public const string SRCovidStatus = "SRCovidStatus";
			public const string VoucherNo = "VoucherNo";
			public const string SRCovidComorbidStatus = "SRCovidComorbidStatus";
			public const string IsDisability = "IsDisability";
			public const string IsTracer = "IsTracer";
			public const string ItemConditionRuleID = "ItemConditionRuleID";
			public const string SRPatientRiskStatus = "SRPatientRiskStatus";
			public const string IsFinishedAttendance = "IsFinishedAttendance";
			public const string FinishedAttendanceByUserID = "FinishedAttendanceByUserID";
			public const string FinishedAttendanceDateTime = "FinishedAttendanceDateTime";
            public const string SRPatientRiskColor = "SRPatientRiskColor";
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
			lock (typeof(RegistrationMetadata))
			{
				if (RegistrationMetadata.mapDelegates == null)
				{
					RegistrationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationMetadata.meta == null)
				{
					RegistrationMetadata.meta = new RegistrationMetadata();
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
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("AppointmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgeInYear", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("AgeInMonth", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("AgeInDay", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("SRShift", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientInType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsuranceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRERCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRVisitReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBussinesMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlavonAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitTypeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Anamnesis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Complaint", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InitialDiagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicationPlanning", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTriage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrintingPatientCard", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DischargeDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("DischargeTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("DischargeMedicalNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDischargeCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDischargeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LOSInYear", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("LOSInMonth", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("LOSInDay", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("DischargeOperatorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdministrationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RoundingAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RemainingAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsTransferedToInpatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNewPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNewBornInfant", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsParturition", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHoldTransactionEntry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHasCorrection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEMRValid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBackDate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ActualVisitDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRVoidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEpisodeComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClusterAssessment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsConsul", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFromDispensary", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNewVisit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDirectPrescriptionReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RegistrationQue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VisiteRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGenerateHL7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferralName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsObservation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CauseOfAccident", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOldCase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDHF", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEKG", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("EmrDiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGlobalPlafond", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FirstResponDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("FirstResponTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PhysicianResponDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PhysicianResponTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsRoomIn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLockVerifiedBilling", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LockVerifiedBillingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LockVerifiedBillingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureChargeClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeRelationship", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorCardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargePlanDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UsertInsertDischargePlan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNonPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReasonsForTreatmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientAdm", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("GuarantorAdm", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ReasonsForTreatmentDescID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReferralGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianSenders", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiscAdmPatient", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscAdmGuarantor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRPatientInCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRKiaCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRObstetricType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHoldTransactionEntryByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConfirmedAttendance", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ConfirmedAttendanceByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConfirmedAttendanceDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BpjsSepNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlavonAmount2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeathCertificateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpjsCoverageFormula", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BpjsPackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApproximatePlafondAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SentToBillingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SentToBillingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAdjusted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AdjustLog", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAllowPatientCheckOut", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AllowPatientCheckOutDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AllowPatientCheckOutByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferByParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRelationshipQuality", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRResidentialHome", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFatherOccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPregnant", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("GestationalAge", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsBreastFeeding", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AgeOfBabyInYear", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AgeOfBabyInMonth", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AgeOfBabyInDay", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsKidneyFunctionImpaired", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatinineSerumValue", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Hpi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MembershipDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ExternalQueNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralIdTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralNameTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsReconcile", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkipAutoBill", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRCrashSite", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CrashSiteDetail", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MembershipNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenEntryMR", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRCovidStatus", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("VoucherNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCovidComorbidStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDisability", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTracer", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientRiskStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFinishedAttendance", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FinishedAttendanceByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FinishedAttendanceDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRPatientRiskColor", new esTypeMap("varchar", "System.String"));


                meta.Source = "Registration";
				meta.Destination = "Registration";
				meta.spInsert = "proc_RegistrationInsert";
				meta.spUpdate = "proc_RegistrationUpdate";
				meta.spDelete = "proc_RegistrationDelete";
				meta.spLoadAll = "proc_RegistrationLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
