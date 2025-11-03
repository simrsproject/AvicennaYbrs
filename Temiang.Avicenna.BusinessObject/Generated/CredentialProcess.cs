/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:32:54 PM
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
	abstract public class esCredentialProcessCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessQuery query)
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
			this.InitQuery(query as esCredentialProcessQuery);
		}
		#endregion

		virtual public CredentialProcess DetachEntity(CredentialProcess entity)
		{
			return base.DetachEntity(entity) as CredentialProcess;
		}

		virtual public CredentialProcess AttachEntity(CredentialProcess entity)
		{
			return base.AttachEntity(entity) as CredentialProcess;
		}

		virtual public void Combine(CredentialProcessCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcess this[int index]
		{
			get
			{
				return base[index] as CredentialProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcess);
		}
	}

	[Serializable]
	abstract public class esCredentialProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcess()
		{
		}

		public esCredentialProcess(DataRow row)
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
			esCredentialProcessQuery query = this.GetDynamicQuery();
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
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "QuestionnaireID": this.str.QuestionnaireID = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "SREducationalQualification": this.str.SREducationalQualification = (string)value; break;
						case "InstitutionName": this.str.InstitutionName = (string)value; break;
						case "DiplomaNumber": this.str.DiplomaNumber = (string)value; break;
						case "DiplomaDate": this.str.DiplomaDate = (string)value; break;
						case "CompetencyCertificateNo": this.str.CompetencyCertificateNo = (string)value; break;
						case "CompetencyCertificateDateOfIssue": this.str.CompetencyCertificateDateOfIssue = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "CompetencyAssessmentVerificationDate": this.str.CompetencyAssessmentVerificationDate = (string)value; break;
						case "CompetencyAssessmentVerificationBy": this.str.CompetencyAssessmentVerificationBy = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "CompetencyAssessmentVerificationDate2": this.str.CompetencyAssessmentVerificationDate2 = (string)value; break;
						case "CompetencyAssessmentVerificationBy2": this.str.CompetencyAssessmentVerificationBy2 = (string)value; break;
						case "IsVerified2": this.str.IsVerified2 = (string)value; break;
						case "VerifiedDateTime2": this.str.VerifiedDateTime2 = (string)value; break;
						case "VerifiedByUserID2": this.str.VerifiedByUserID2 = (string)value; break;
						case "IsCompletelyVerified": this.str.IsCompletelyVerified = (string)value; break;
						case "CredentialApplicationDate": this.str.CredentialApplicationDate = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SRCredentialingStatus": this.str.SRCredentialingStatus = (string)value; break;
						case "SRRecredentialReason": this.str.SRRecredentialReason = (string)value; break;
						case "IsCredentialApplication": this.str.IsCredentialApplication = (string)value; break;
						case "LastCredentialApplicationDateTime": this.str.LastCredentialApplicationDateTime = (string)value; break;
						case "LastCredentialApplicationByUserID": this.str.LastCredentialApplicationByUserID = (string)value; break;
						case "CredentialingDate": this.str.CredentialingDate = (string)value; break;
						case "IsCertificateVerification": this.str.IsCertificateVerification = (string)value; break;
						case "RecommendationNotes": this.str.RecommendationNotes = (string)value; break;
						case "IsPerform": this.str.IsPerform = (string)value; break;
						case "IsCredentialing": this.str.IsCredentialing = (string)value; break;
						case "LastCredentialingDateTime": this.str.LastCredentialingDateTime = (string)value; break;
						case "LastCredentialingByUserID": this.str.LastCredentialingByUserID = (string)value; break;
						case "IsReprocess": this.str.IsReprocess = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "RecommendationLetterDate": this.str.RecommendationLetterDate = (string)value; break;
						case "RecommendationLetterNo": this.str.RecommendationLetterNo = (string)value; break;
						case "IsRecommendationLetter": this.str.IsRecommendationLetter = (string)value; break;
						case "LastRecommendationLetterDateTime": this.str.LastRecommendationLetterDateTime = (string)value; break;
						case "LastRecommendationLetterByUserID": this.str.LastRecommendationLetterByUserID = (string)value; break;
						case "ClinicalAssignmentLetterDate": this.str.ClinicalAssignmentLetterDate = (string)value; break;
						case "DecreeNo": this.str.DecreeNo = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "IsClinicalAssignmentLetter": this.str.IsClinicalAssignmentLetter = (string)value; break;
						case "LastClinicalAssignmentLetterDateTime": this.str.LastClinicalAssignmentLetterDateTime = (string)value; break;
						case "LastClinicalAssignmentLetterByUserID": this.str.LastClinicalAssignmentLetterByUserID = (string)value; break;
						case "IsDocumentComplete": this.str.IsDocumentComplete = (string)value; break;
						case "DocumentIncompleteNotes": this.str.DocumentIncompleteNotes = (string)value; break;
						case "IsDocumentChecking": this.str.IsDocumentChecking = (string)value; break;
						case "DocumentCheckingDateTime": this.str.DocumentCheckingDateTime = (string)value; break;
						case "DocumentCheckingByUserID": this.str.DocumentCheckingByUserID = (string)value; break;
						case "EthicsQuestionnariePersonID": this.str.EthicsQuestionnariePersonID = (string)value; break;
						case "EthicsQuestionnarieDate": this.str.EthicsQuestionnarieDate = (string)value; break;
						case "EthicsQuestionnarieByUserID": this.str.EthicsQuestionnarieByUserID = (string)value; break;
						case "LastEthicsQuestionnarieDateTime": this.str.LastEthicsQuestionnarieDateTime = (string)value; break;
						case "CompetencyAssessmentDate": this.str.CompetencyAssessmentDate = (string)value; break;
						case "CompetencyAssessmentByUserID": this.str.CompetencyAssessmentByUserID = (string)value; break;
						case "LastCompetencyAssessmentDateTime": this.str.LastCompetencyAssessmentDateTime = (string)value; break;
						case "ObservationInstrumentQuestionnaireID": this.str.ObservationInstrumentQuestionnaireID = (string)value; break;
						case "IsCompletelyObservationInstrumentAssessment": this.str.IsCompletelyObservationInstrumentAssessment = (string)value; break;
						case "ObservationInstrumentAssessmentScore": this.str.ObservationInstrumentAssessmentScore = (string)value; break;
						case "LastObservationInstrumentAssessmentDateTime": this.str.LastObservationInstrumentAssessmentDateTime = (string)value; break;
						case "LastObservationInstrumentAssessmentByUserID": this.str.LastObservationInstrumentAssessmentByUserID = (string)value; break;
						case "DispositionNo": this.str.DispositionNo = (string)value; break;
						case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
						case "ScheduleTimeFrom": this.str.ScheduleTimeFrom = (string)value; break;
						case "ScheduleTimeTo": this.str.ScheduleTimeTo = (string)value; break;
						case "CredentialingLocation": this.str.CredentialingLocation = (string)value; break;
						case "CredentialingInvitationLetterNo": this.str.CredentialingInvitationLetterNo = (string)value; break;
						case "InvitationNo": this.str.InvitationNo = (string)value; break;
						case "SchedulingDateTime": this.str.SchedulingDateTime = (string)value; break;
						case "SchedulingByUserID": this.str.SchedulingByUserID = (string)value; break;
						case "IsRecommendation": this.str.IsRecommendation = (string)value; break;
						case "LastRecommendationDateTime": this.str.LastRecommendationDateTime = (string)value; break;
						case "LastRecommendationByUserID": this.str.LastRecommendationByUserID = (string)value; break;
						case "IsRecommendationResult": this.str.IsRecommendationResult = (string)value; break;
						case "RecommendationResultDate": this.str.RecommendationResultDate = (string)value; break;
						case "SRRecommendationResult": this.str.SRRecommendationResult = (string)value; break;
						case "RecommendationResultNotes": this.str.RecommendationResultNotes = (string)value; break;
						case "LastRecommendationResultDateTime": this.str.LastRecommendationResultDateTime = (string)value; break;
						case "LastRecommendationResultByUserID": this.str.LastRecommendationResultByUserID = (string)value; break;
						case "IsConclusion": this.str.IsConclusion = (string)value; break;
						case "ConclusionDate": this.str.ConclusionDate = (string)value; break;
						case "SRConclusionResult": this.str.SRConclusionResult = (string)value; break;
						case "ConclusionNotes": this.str.ConclusionNotes = (string)value; break;
						case "SRCredentialingConclusion": this.str.SRCredentialingConclusion = (string)value; break;
						case "CredentialingConclusionDesc": this.str.CredentialingConclusionDesc = (string)value; break;
						case "LastConclusionDateTime": this.str.LastConclusionDateTime = (string)value; break;
						case "LastConclusionByUserID": this.str.LastConclusionByUserID = (string)value; break;
						case "ClinicalAppoinmentNo": this.str.ClinicalAppoinmentNo = (string)value; break;
						case "ClinicalAppoinmentDateOfIssue": this.str.ClinicalAppoinmentDateOfIssue = (string)value; break;
						case "ClinicalAppoinmentValidTo": this.str.ClinicalAppoinmentValidTo = (string)value; break;
						case "SRClinicalAppoinmentStatus": this.str.SRClinicalAppoinmentStatus = (string)value; break;
						case "ClinicalAppoinmentNotes": this.str.ClinicalAppoinmentNotes = (string)value; break;
						case "LastClinicalAppoinmentDateTime": this.str.LastClinicalAppoinmentDateTime = (string)value; break;
						case "LastClinicalAppoinmentByUserID": this.str.LastClinicalAppoinmentByUserID = (string)value; break;
						case "IsCi": this.str.IsCi = (string)value; break;
						case "SRKtklLevel": this.str.SRKtklLevel = (string)value; break;
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
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "QuestionnaireID":

							if (value == null || value is System.Int32)
								this.QuestionnaireID = (System.Int32?)value;
							break;
						case "DiplomaDate":

							if (value == null || value is System.DateTime)
								this.DiplomaDate = (System.DateTime?)value;
							break;
						case "CompetencyCertificateDateOfIssue":

							if (value == null || value is System.DateTime)
								this.CompetencyCertificateDateOfIssue = (System.DateTime?)value;
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
						case "CompetencyAssessmentVerificationDate":

							if (value == null || value is System.DateTime)
								this.CompetencyAssessmentVerificationDate = (System.DateTime?)value;
							break;
						case "CompetencyAssessmentVerificationBy":

							if (value == null || value is System.Int32)
								this.CompetencyAssessmentVerificationBy = (System.Int32?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "CompetencyAssessmentVerificationDate2":

							if (value == null || value is System.DateTime)
								this.CompetencyAssessmentVerificationDate2 = (System.DateTime?)value;
							break;
						case "CompetencyAssessmentVerificationBy2":

							if (value == null || value is System.Int32)
								this.CompetencyAssessmentVerificationBy2 = (System.Int32?)value;
							break;
						case "IsVerified2":

							if (value == null || value is System.Boolean)
								this.IsVerified2 = (System.Boolean?)value;
							break;
						case "VerifiedDateTime2":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime2 = (System.DateTime?)value;
							break;
						case "IsCompletelyVerified":

							if (value == null || value is System.Boolean)
								this.IsCompletelyVerified = (System.Boolean?)value;
							break;
						case "CredentialApplicationDate":

							if (value == null || value is System.DateTime)
								this.CredentialApplicationDate = (System.DateTime?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "IsCredentialApplication":

							if (value == null || value is System.Boolean)
								this.IsCredentialApplication = (System.Boolean?)value;
							break;
						case "LastCredentialApplicationDateTime":

							if (value == null || value is System.DateTime)
								this.LastCredentialApplicationDateTime = (System.DateTime?)value;
							break;
						case "CredentialingDate":

							if (value == null || value is System.DateTime)
								this.CredentialingDate = (System.DateTime?)value;
							break;
						case "IsCertificateVerification":

							if (value == null || value is System.Boolean)
								this.IsCertificateVerification = (System.Boolean?)value;
							break;
						case "IsPerform":

							if (value == null || value is System.Boolean)
								this.IsPerform = (System.Boolean?)value;
							break;
						case "IsCredentialing":

							if (value == null || value is System.Boolean)
								this.IsCredentialing = (System.Boolean?)value;
							break;
						case "LastCredentialingDateTime":

							if (value == null || value is System.DateTime)
								this.LastCredentialingDateTime = (System.DateTime?)value;
							break;
						case "IsReprocess":

							if (value == null || value is System.Boolean)
								this.IsReprocess = (System.Boolean?)value;
							break;
						case "RecommendationLetterDate":

							if (value == null || value is System.DateTime)
								this.RecommendationLetterDate = (System.DateTime?)value;
							break;
						case "IsRecommendationLetter":

							if (value == null || value is System.Boolean)
								this.IsRecommendationLetter = (System.Boolean?)value;
							break;
						case "LastRecommendationLetterDateTime":

							if (value == null || value is System.DateTime)
								this.LastRecommendationLetterDateTime = (System.DateTime?)value;
							break;
						case "ClinicalAssignmentLetterDate":

							if (value == null || value is System.DateTime)
								this.ClinicalAssignmentLetterDate = (System.DateTime?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						case "IsClinicalAssignmentLetter":

							if (value == null || value is System.Boolean)
								this.IsClinicalAssignmentLetter = (System.Boolean?)value;
							break;
						case "LastClinicalAssignmentLetterDateTime":

							if (value == null || value is System.DateTime)
								this.LastClinicalAssignmentLetterDateTime = (System.DateTime?)value;
							break;
						case "IsDocumentComplete":

							if (value == null || value is System.Boolean)
								this.IsDocumentComplete = (System.Boolean?)value;
							break;
						case "IsDocumentChecking":

							if (value == null || value is System.Boolean)
								this.IsDocumentChecking = (System.Boolean?)value;
							break;
						case "DocumentCheckingDateTime":

							if (value == null || value is System.DateTime)
								this.DocumentCheckingDateTime = (System.DateTime?)value;
							break;
						case "EthicsQuestionnariePersonID":

							if (value == null || value is System.Int32)
								this.EthicsQuestionnariePersonID = (System.Int32?)value;
							break;
						case "EthicsQuestionnarieDate":

							if (value == null || value is System.DateTime)
								this.EthicsQuestionnarieDate = (System.DateTime?)value;
							break;
						case "LastEthicsQuestionnarieDateTime":

							if (value == null || value is System.DateTime)
								this.LastEthicsQuestionnarieDateTime = (System.DateTime?)value;
							break;
						case "CompetencyAssessmentDate":

							if (value == null || value is System.DateTime)
								this.CompetencyAssessmentDate = (System.DateTime?)value;
							break;
						case "LastCompetencyAssessmentDateTime":

							if (value == null || value is System.DateTime)
								this.LastCompetencyAssessmentDateTime = (System.DateTime?)value;
							break;
						case "ObservationInstrumentQuestionnaireID":

							if (value == null || value is System.Int32)
								this.ObservationInstrumentQuestionnaireID = (System.Int32?)value;
							break;
						case "IsCompletelyObservationInstrumentAssessment":

							if (value == null || value is System.Boolean)
								this.IsCompletelyObservationInstrumentAssessment = (System.Boolean?)value;
							break;
						case "ObservationInstrumentAssessmentScore":

							if (value == null || value is System.Decimal)
								this.ObservationInstrumentAssessmentScore = (System.Decimal?)value;
							break;
						case "LastObservationInstrumentAssessmentDateTime":

							if (value == null || value is System.DateTime)
								this.LastObservationInstrumentAssessmentDateTime = (System.DateTime?)value;
							break;
						case "ScheduleDate":

							if (value == null || value is System.DateTime)
								this.ScheduleDate = (System.DateTime?)value;
							break;
						case "SchedulingDateTime":

							if (value == null || value is System.DateTime)
								this.SchedulingDateTime = (System.DateTime?)value;
							break;
						case "IsRecommendation":

							if (value == null || value is System.Boolean)
								this.IsRecommendation = (System.Boolean?)value;
							break;
						case "LastRecommendationDateTime":

							if (value == null || value is System.DateTime)
								this.LastRecommendationDateTime = (System.DateTime?)value;
							break;
						case "IsRecommendationResult":

							if (value == null || value is System.Boolean)
								this.IsRecommendationResult = (System.Boolean?)value;
							break;
						case "RecommendationResultDate":

							if (value == null || value is System.DateTime)
								this.RecommendationResultDate = (System.DateTime?)value;
							break;
						case "LastRecommendationResultDateTime":

							if (value == null || value is System.DateTime)
								this.LastRecommendationResultDateTime = (System.DateTime?)value;
							break;
						case "IsConclusion":

							if (value == null || value is System.Boolean)
								this.IsConclusion = (System.Boolean?)value;
							break;
						case "ConclusionDate":

							if (value == null || value is System.DateTime)
								this.ConclusionDate = (System.DateTime?)value;
							break;
						case "LastConclusionDateTime":

							if (value == null || value is System.DateTime)
								this.LastConclusionDateTime = (System.DateTime?)value;
							break;
						case "ClinicalAppoinmentDateOfIssue":

							if (value == null || value is System.DateTime)
								this.ClinicalAppoinmentDateOfIssue = (System.DateTime?)value;
							break;
						case "ClinicalAppoinmentValidTo":

							if (value == null || value is System.DateTime)
								this.ClinicalAppoinmentValidTo = (System.DateTime?)value;
							break;
						case "LastClinicalAppoinmentDateTime":

							if (value == null || value is System.DateTime)
								this.LastClinicalAppoinmentDateTime = (System.DateTime?)value;
							break;
						case "IsCi":

							if (value == null || value is System.Boolean)
								this.IsCi = (System.Boolean?)value;
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
		/// Maps to CredentialProcess.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SREducationalQualification
		/// </summary>
		virtual public System.String SREducationalQualification
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SREducationalQualification);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SREducationalQualification, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.InstitutionName);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.InstitutionName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DiplomaNumber
		/// </summary>
		virtual public System.String DiplomaNumber
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.DiplomaNumber);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.DiplomaNumber, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DiplomaDate
		/// </summary>
		virtual public System.DateTime? DiplomaDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.DiplomaDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.DiplomaDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyCertificateNo
		/// </summary>
		virtual public System.String CompetencyCertificateNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CompetencyCertificateNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CompetencyCertificateNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyCertificateDateOfIssue
		/// </summary>
		virtual public System.DateTime? CompetencyCertificateDateOfIssue
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyCertificateDateOfIssue);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyCertificateDateOfIssue, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentVerificationDate
		/// </summary>
		virtual public System.DateTime? CompetencyAssessmentVerificationDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentVerificationBy
		/// </summary>
		virtual public System.Int32? CompetencyAssessmentVerificationBy
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentVerificationDate2
		/// </summary>
		virtual public System.DateTime? CompetencyAssessmentVerificationDate2
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate2);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentVerificationBy2
		/// </summary>
		virtual public System.Int32? CompetencyAssessmentVerificationBy2
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy2);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsVerified2
		/// </summary>
		virtual public System.Boolean? IsVerified2
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVerified2);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsVerified2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VerifiedDateTime2
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime2
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.VerifiedDateTime2);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.VerifiedDateTime2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.VerifiedByUserID2
		/// </summary>
		virtual public System.String VerifiedByUserID2
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.VerifiedByUserID2);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.VerifiedByUserID2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCompletelyVerified
		/// </summary>
		virtual public System.Boolean? IsCompletelyVerified
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCompletelyVerified);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCompletelyVerified, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CredentialApplicationDate
		/// </summary>
		virtual public System.DateTime? CredentialApplicationDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CredentialApplicationDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CredentialApplicationDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRCredentialingStatus
		/// </summary>
		virtual public System.String SRCredentialingStatus
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRCredentialingStatus);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRCredentialingStatus, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRRecredentialReason
		/// </summary>
		virtual public System.String SRRecredentialReason
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRRecredentialReason);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRRecredentialReason, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCredentialApplication
		/// </summary>
		virtual public System.Boolean? IsCredentialApplication
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCredentialApplication);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCredentialApplication, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastCredentialApplicationDateTime
		/// </summary>
		virtual public System.DateTime? LastCredentialApplicationDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastCredentialApplicationByUserID
		/// </summary>
		virtual public System.String LastCredentialApplicationByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CredentialingDate
		/// </summary>
		virtual public System.DateTime? CredentialingDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CredentialingDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CredentialingDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCertificateVerification
		/// </summary>
		virtual public System.Boolean? IsCertificateVerification
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCertificateVerification);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCertificateVerification, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.RecommendationNotes
		/// </summary>
		virtual public System.String RecommendationNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsPerform
		/// </summary>
		virtual public System.Boolean? IsPerform
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsPerform);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsPerform, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCredentialing
		/// </summary>
		virtual public System.Boolean? IsCredentialing
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCredentialing);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCredentialing, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastCredentialingDateTime
		/// </summary>
		virtual public System.DateTime? LastCredentialingDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCredentialingDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCredentialingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastCredentialingByUserID
		/// </summary>
		virtual public System.String LastCredentialingByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastCredentialingByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastCredentialingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsReprocess
		/// </summary>
		virtual public System.Boolean? IsReprocess
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsReprocess);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsReprocess, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.RecommendationLetterDate
		/// </summary>
		virtual public System.DateTime? RecommendationLetterDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.RecommendationLetterDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.RecommendationLetterDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.RecommendationLetterNo
		/// </summary>
		virtual public System.String RecommendationLetterNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationLetterNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationLetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsRecommendationLetter
		/// </summary>
		virtual public System.Boolean? IsRecommendationLetter
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendationLetter);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendationLetter, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationLetterDateTime
		/// </summary>
		virtual public System.DateTime? LastRecommendationLetterDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationLetterByUserID
		/// </summary>
		virtual public System.String LastRecommendationLetterByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ClinicalAssignmentLetterDate
		/// </summary>
		virtual public System.DateTime? ClinicalAssignmentLetterDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAssignmentLetterDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAssignmentLetterDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DecreeNo
		/// </summary>
		virtual public System.String DecreeNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.DecreeNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.DecreeNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsClinicalAssignmentLetter
		/// </summary>
		virtual public System.Boolean? IsClinicalAssignmentLetter
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsClinicalAssignmentLetter);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsClinicalAssignmentLetter, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastClinicalAssignmentLetterDateTime
		/// </summary>
		virtual public System.DateTime? LastClinicalAssignmentLetterDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastClinicalAssignmentLetterByUserID
		/// </summary>
		virtual public System.String LastClinicalAssignmentLetterByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsDocumentComplete
		/// </summary>
		virtual public System.Boolean? IsDocumentComplete
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsDocumentComplete);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsDocumentComplete, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DocumentIncompleteNotes
		/// </summary>
		virtual public System.String DocumentIncompleteNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.DocumentIncompleteNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.DocumentIncompleteNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsDocumentChecking
		/// </summary>
		virtual public System.Boolean? IsDocumentChecking
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsDocumentChecking);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsDocumentChecking, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DocumentCheckingDateTime
		/// </summary>
		virtual public System.DateTime? DocumentCheckingDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.DocumentCheckingDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.DocumentCheckingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DocumentCheckingByUserID
		/// </summary>
		virtual public System.String DocumentCheckingByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.DocumentCheckingByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.DocumentCheckingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.EthicsQuestionnariePersonID
		/// </summary>
		virtual public System.Int32? EthicsQuestionnariePersonID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.EthicsQuestionnariePersonID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.EthicsQuestionnariePersonID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.EthicsQuestionnarieDate
		/// </summary>
		virtual public System.DateTime? EthicsQuestionnarieDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.EthicsQuestionnarieByUserID
		/// </summary>
		virtual public System.String EthicsQuestionnarieByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastEthicsQuestionnarieDateTime
		/// </summary>
		virtual public System.DateTime? LastEthicsQuestionnarieDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastEthicsQuestionnarieDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastEthicsQuestionnarieDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentDate
		/// </summary>
		virtual public System.DateTime? CompetencyAssessmentDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CompetencyAssessmentByUserID
		/// </summary>
		virtual public System.String CompetencyAssessmentByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastCompetencyAssessmentDateTime
		/// </summary>
		virtual public System.DateTime? LastCompetencyAssessmentDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCompetencyAssessmentDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastCompetencyAssessmentDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ObservationInstrumentQuestionnaireID
		/// </summary>
		virtual public System.Int32? ObservationInstrumentQuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessMetadata.ColumnNames.ObservationInstrumentQuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessMetadata.ColumnNames.ObservationInstrumentQuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCompletelyObservationInstrumentAssessment
		/// </summary>
		virtual public System.Boolean? IsCompletelyObservationInstrumentAssessment
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCompletelyObservationInstrumentAssessment);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCompletelyObservationInstrumentAssessment, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ObservationInstrumentAssessmentScore
		/// </summary>
		virtual public System.Decimal? ObservationInstrumentAssessmentScore
		{
			get
			{
				return base.GetSystemDecimal(CredentialProcessMetadata.ColumnNames.ObservationInstrumentAssessmentScore);
			}

			set
			{
				base.SetSystemDecimal(CredentialProcessMetadata.ColumnNames.ObservationInstrumentAssessmentScore, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastObservationInstrumentAssessmentDateTime
		/// </summary>
		virtual public System.DateTime? LastObservationInstrumentAssessmentDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastObservationInstrumentAssessmentByUserID
		/// </summary>
		virtual public System.String LastObservationInstrumentAssessmentByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.DispositionNo
		/// </summary>
		virtual public System.String DispositionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.DispositionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.DispositionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ScheduleDate
		/// </summary>
		virtual public System.DateTime? ScheduleDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ScheduleDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ScheduleDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ScheduleTimeFrom
		/// </summary>
		virtual public System.String ScheduleTimeFrom
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ScheduleTimeFrom);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ScheduleTimeFrom, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ScheduleTimeTo
		/// </summary>
		virtual public System.String ScheduleTimeTo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ScheduleTimeTo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ScheduleTimeTo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CredentialingLocation
		/// </summary>
		virtual public System.String CredentialingLocation
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingLocation);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingLocation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CredentialingInvitationLetterNo
		/// </summary>
		virtual public System.String CredentialingInvitationLetterNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingInvitationLetterNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingInvitationLetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.InvitationNo
		/// </summary>
		virtual public System.String InvitationNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.InvitationNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.InvitationNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SchedulingDateTime
		/// </summary>
		virtual public System.DateTime? SchedulingDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.SchedulingDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.SchedulingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SchedulingByUserID
		/// </summary>
		virtual public System.String SchedulingByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SchedulingByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SchedulingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsRecommendation
		/// </summary>
		virtual public System.Boolean? IsRecommendation
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendation);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationDateTime
		/// </summary>
		virtual public System.DateTime? LastRecommendationDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationByUserID
		/// </summary>
		virtual public System.String LastRecommendationByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsRecommendationResult
		/// </summary>
		virtual public System.Boolean? IsRecommendationResult
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendationResult);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsRecommendationResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.RecommendationResultDate
		/// </summary>
		virtual public System.DateTime? RecommendationResultDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.RecommendationResultDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.RecommendationResultDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRRecommendationResult
		/// </summary>
		virtual public System.String SRRecommendationResult
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRRecommendationResult);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRRecommendationResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.RecommendationResultNotes
		/// </summary>
		virtual public System.String RecommendationResultNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationResultNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.RecommendationResultNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationResultDateTime
		/// </summary>
		virtual public System.DateTime? LastRecommendationResultDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationResultDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastRecommendationResultDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastRecommendationResultByUserID
		/// </summary>
		virtual public System.String LastRecommendationResultByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationResultByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastRecommendationResultByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsConclusion
		/// </summary>
		virtual public System.Boolean? IsConclusion
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsConclusion);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsConclusion, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ConclusionDate
		/// </summary>
		virtual public System.DateTime? ConclusionDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ConclusionDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ConclusionDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRConclusionResult
		/// </summary>
		virtual public System.String SRConclusionResult
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRConclusionResult);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRConclusionResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ConclusionNotes
		/// </summary>
		virtual public System.String ConclusionNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ConclusionNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ConclusionNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRCredentialingConclusion
		/// </summary>
		virtual public System.String SRCredentialingConclusion
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRCredentialingConclusion);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRCredentialingConclusion, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CredentialingConclusionDesc
		/// </summary>
		virtual public System.String CredentialingConclusionDesc
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingConclusionDesc);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CredentialingConclusionDesc, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastConclusionDateTime
		/// </summary>
		virtual public System.DateTime? LastConclusionDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastConclusionDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastConclusionDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastConclusionByUserID
		/// </summary>
		virtual public System.String LastConclusionByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastConclusionByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastConclusionByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ClinicalAppoinmentNo
		/// </summary>
		virtual public System.String ClinicalAppoinmentNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ClinicalAppoinmentDateOfIssue
		/// </summary>
		virtual public System.DateTime? ClinicalAppoinmentDateOfIssue
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentDateOfIssue);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentDateOfIssue, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ClinicalAppoinmentValidTo
		/// </summary>
		virtual public System.DateTime? ClinicalAppoinmentValidTo
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentValidTo);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentValidTo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRClinicalAppoinmentStatus
		/// </summary>
		virtual public System.String SRClinicalAppoinmentStatus
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalAppoinmentStatus);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRClinicalAppoinmentStatus, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.ClinicalAppoinmentNotes
		/// </summary>
		virtual public System.String ClinicalAppoinmentNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastClinicalAppoinmentDateTime
		/// </summary>
		virtual public System.DateTime? LastClinicalAppoinmentDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastClinicalAppoinmentByUserID
		/// </summary>
		virtual public System.String LastClinicalAppoinmentByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.IsCi
		/// </summary>
		virtual public System.Boolean? IsCi
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCi);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessMetadata.ColumnNames.IsCi, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.SRKtklLevel
		/// </summary>
		virtual public System.String SRKtklLevel
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.SRKtklLevel);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.SRKtklLevel, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcess entity)
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
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
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
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
				}
			}
			public System.String SRClinicalWorkArea
			{
				get
				{
					System.String data = entity.SRClinicalWorkArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalWorkArea = null;
					else entity.SRClinicalWorkArea = Convert.ToString(value);
				}
			}
			public System.String SRClinicalAuthorityLevel
			{
				get
				{
					System.String data = entity.SRClinicalAuthorityLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalAuthorityLevel = null;
					else entity.SRClinicalAuthorityLevel = Convert.ToString(value);
				}
			}
			public System.String QuestionnaireID
			{
				get
				{
					System.Int32? data = entity.QuestionnaireID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireID = null;
					else entity.QuestionnaireID = Convert.ToInt32(value);
				}
			}
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
			public System.String SREducationalQualification
			{
				get
				{
					System.String data = entity.SREducationalQualification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationalQualification = null;
					else entity.SREducationalQualification = Convert.ToString(value);
				}
			}
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
			public System.String DiplomaNumber
			{
				get
				{
					System.String data = entity.DiplomaNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiplomaNumber = null;
					else entity.DiplomaNumber = Convert.ToString(value);
				}
			}
			public System.String DiplomaDate
			{
				get
				{
					System.DateTime? data = entity.DiplomaDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiplomaDate = null;
					else entity.DiplomaDate = Convert.ToDateTime(value);
				}
			}
			public System.String CompetencyCertificateNo
			{
				get
				{
					System.String data = entity.CompetencyCertificateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyCertificateNo = null;
					else entity.CompetencyCertificateNo = Convert.ToString(value);
				}
			}
			public System.String CompetencyCertificateDateOfIssue
			{
				get
				{
					System.DateTime? data = entity.CompetencyCertificateDateOfIssue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyCertificateDateOfIssue = null;
					else entity.CompetencyCertificateDateOfIssue = Convert.ToDateTime(value);
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
			public System.String CompetencyAssessmentVerificationDate
			{
				get
				{
					System.DateTime? data = entity.CompetencyAssessmentVerificationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentVerificationDate = null;
					else entity.CompetencyAssessmentVerificationDate = Convert.ToDateTime(value);
				}
			}
			public System.String CompetencyAssessmentVerificationBy
			{
				get
				{
					System.Int32? data = entity.CompetencyAssessmentVerificationBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentVerificationBy = null;
					else entity.CompetencyAssessmentVerificationBy = Convert.ToInt32(value);
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
			public System.String CompetencyAssessmentVerificationDate2
			{
				get
				{
					System.DateTime? data = entity.CompetencyAssessmentVerificationDate2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentVerificationDate2 = null;
					else entity.CompetencyAssessmentVerificationDate2 = Convert.ToDateTime(value);
				}
			}
			public System.String CompetencyAssessmentVerificationBy2
			{
				get
				{
					System.Int32? data = entity.CompetencyAssessmentVerificationBy2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentVerificationBy2 = null;
					else entity.CompetencyAssessmentVerificationBy2 = Convert.ToInt32(value);
				}
			}
			public System.String IsVerified2
			{
				get
				{
					System.Boolean? data = entity.IsVerified2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerified2 = null;
					else entity.IsVerified2 = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedDateTime2
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime2 = null;
					else entity.VerifiedDateTime2 = Convert.ToDateTime(value);
				}
			}
			public System.String VerifiedByUserID2
			{
				get
				{
					System.String data = entity.VerifiedByUserID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID2 = null;
					else entity.VerifiedByUserID2 = Convert.ToString(value);
				}
			}
			public System.String IsCompletelyVerified
			{
				get
				{
					System.Boolean? data = entity.IsCompletelyVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCompletelyVerified = null;
					else entity.IsCompletelyVerified = Convert.ToBoolean(value);
				}
			}
			public System.String CredentialApplicationDate
			{
				get
				{
					System.DateTime? data = entity.CredentialApplicationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialApplicationDate = null;
					else entity.CredentialApplicationDate = Convert.ToDateTime(value);
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
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String SRCredentialingStatus
			{
				get
				{
					System.String data = entity.SRCredentialingStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialingStatus = null;
					else entity.SRCredentialingStatus = Convert.ToString(value);
				}
			}
			public System.String SRRecredentialReason
			{
				get
				{
					System.String data = entity.SRRecredentialReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecredentialReason = null;
					else entity.SRRecredentialReason = Convert.ToString(value);
				}
			}
			public System.String IsCredentialApplication
			{
				get
				{
					System.Boolean? data = entity.IsCredentialApplication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCredentialApplication = null;
					else entity.IsCredentialApplication = Convert.ToBoolean(value);
				}
			}
			public System.String LastCredentialApplicationDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCredentialApplicationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCredentialApplicationDateTime = null;
					else entity.LastCredentialApplicationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCredentialApplicationByUserID
			{
				get
				{
					System.String data = entity.LastCredentialApplicationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCredentialApplicationByUserID = null;
					else entity.LastCredentialApplicationByUserID = Convert.ToString(value);
				}
			}
			public System.String CredentialingDate
			{
				get
				{
					System.DateTime? data = entity.CredentialingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialingDate = null;
					else entity.CredentialingDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsCertificateVerification
			{
				get
				{
					System.Boolean? data = entity.IsCertificateVerification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCertificateVerification = null;
					else entity.IsCertificateVerification = Convert.ToBoolean(value);
				}
			}
			public System.String RecommendationNotes
			{
				get
				{
					System.String data = entity.RecommendationNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecommendationNotes = null;
					else entity.RecommendationNotes = Convert.ToString(value);
				}
			}
			public System.String IsPerform
			{
				get
				{
					System.Boolean? data = entity.IsPerform;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPerform = null;
					else entity.IsPerform = Convert.ToBoolean(value);
				}
			}
			public System.String IsCredentialing
			{
				get
				{
					System.Boolean? data = entity.IsCredentialing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCredentialing = null;
					else entity.IsCredentialing = Convert.ToBoolean(value);
				}
			}
			public System.String LastCredentialingDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCredentialingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCredentialingDateTime = null;
					else entity.LastCredentialingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCredentialingByUserID
			{
				get
				{
					System.String data = entity.LastCredentialingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCredentialingByUserID = null;
					else entity.LastCredentialingByUserID = Convert.ToString(value);
				}
			}
			public System.String IsReprocess
			{
				get
				{
					System.Boolean? data = entity.IsReprocess;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReprocess = null;
					else entity.IsReprocess = Convert.ToBoolean(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String RecommendationLetterDate
			{
				get
				{
					System.DateTime? data = entity.RecommendationLetterDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecommendationLetterDate = null;
					else entity.RecommendationLetterDate = Convert.ToDateTime(value);
				}
			}
			public System.String RecommendationLetterNo
			{
				get
				{
					System.String data = entity.RecommendationLetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecommendationLetterNo = null;
					else entity.RecommendationLetterNo = Convert.ToString(value);
				}
			}
			public System.String IsRecommendationLetter
			{
				get
				{
					System.Boolean? data = entity.IsRecommendationLetter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRecommendationLetter = null;
					else entity.IsRecommendationLetter = Convert.ToBoolean(value);
				}
			}
			public System.String LastRecommendationLetterDateTime
			{
				get
				{
					System.DateTime? data = entity.LastRecommendationLetterDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationLetterDateTime = null;
					else entity.LastRecommendationLetterDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastRecommendationLetterByUserID
			{
				get
				{
					System.String data = entity.LastRecommendationLetterByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationLetterByUserID = null;
					else entity.LastRecommendationLetterByUserID = Convert.ToString(value);
				}
			}
			public System.String ClinicalAssignmentLetterDate
			{
				get
				{
					System.DateTime? data = entity.ClinicalAssignmentLetterDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalAssignmentLetterDate = null;
					else entity.ClinicalAssignmentLetterDate = Convert.ToDateTime(value);
				}
			}
			public System.String DecreeNo
			{
				get
				{
					System.String data = entity.DecreeNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecreeNo = null;
					else entity.DecreeNo = Convert.ToString(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
			public System.String IsClinicalAssignmentLetter
			{
				get
				{
					System.Boolean? data = entity.IsClinicalAssignmentLetter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClinicalAssignmentLetter = null;
					else entity.IsClinicalAssignmentLetter = Convert.ToBoolean(value);
				}
			}
			public System.String LastClinicalAssignmentLetterDateTime
			{
				get
				{
					System.DateTime? data = entity.LastClinicalAssignmentLetterDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastClinicalAssignmentLetterDateTime = null;
					else entity.LastClinicalAssignmentLetterDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastClinicalAssignmentLetterByUserID
			{
				get
				{
					System.String data = entity.LastClinicalAssignmentLetterByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastClinicalAssignmentLetterByUserID = null;
					else entity.LastClinicalAssignmentLetterByUserID = Convert.ToString(value);
				}
			}
			public System.String IsDocumentComplete
			{
				get
				{
					System.Boolean? data = entity.IsDocumentComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDocumentComplete = null;
					else entity.IsDocumentComplete = Convert.ToBoolean(value);
				}
			}
			public System.String DocumentIncompleteNotes
			{
				get
				{
					System.String data = entity.DocumentIncompleteNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentIncompleteNotes = null;
					else entity.DocumentIncompleteNotes = Convert.ToString(value);
				}
			}
			public System.String IsDocumentChecking
			{
				get
				{
					System.Boolean? data = entity.IsDocumentChecking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDocumentChecking = null;
					else entity.IsDocumentChecking = Convert.ToBoolean(value);
				}
			}
			public System.String DocumentCheckingDateTime
			{
				get
				{
					System.DateTime? data = entity.DocumentCheckingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentCheckingDateTime = null;
					else entity.DocumentCheckingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String DocumentCheckingByUserID
			{
				get
				{
					System.String data = entity.DocumentCheckingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentCheckingByUserID = null;
					else entity.DocumentCheckingByUserID = Convert.ToString(value);
				}
			}
			public System.String EthicsQuestionnariePersonID
			{
				get
				{
					System.Int32? data = entity.EthicsQuestionnariePersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EthicsQuestionnariePersonID = null;
					else entity.EthicsQuestionnariePersonID = Convert.ToInt32(value);
				}
			}
			public System.String EthicsQuestionnarieDate
			{
				get
				{
					System.DateTime? data = entity.EthicsQuestionnarieDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EthicsQuestionnarieDate = null;
					else entity.EthicsQuestionnarieDate = Convert.ToDateTime(value);
				}
			}
			public System.String EthicsQuestionnarieByUserID
			{
				get
				{
					System.String data = entity.EthicsQuestionnarieByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EthicsQuestionnarieByUserID = null;
					else entity.EthicsQuestionnarieByUserID = Convert.ToString(value);
				}
			}
			public System.String LastEthicsQuestionnarieDateTime
			{
				get
				{
					System.DateTime? data = entity.LastEthicsQuestionnarieDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastEthicsQuestionnarieDateTime = null;
					else entity.LastEthicsQuestionnarieDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CompetencyAssessmentDate
			{
				get
				{
					System.DateTime? data = entity.CompetencyAssessmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentDate = null;
					else entity.CompetencyAssessmentDate = Convert.ToDateTime(value);
				}
			}
			public System.String CompetencyAssessmentByUserID
			{
				get
				{
					System.String data = entity.CompetencyAssessmentByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompetencyAssessmentByUserID = null;
					else entity.CompetencyAssessmentByUserID = Convert.ToString(value);
				}
			}
			public System.String LastCompetencyAssessmentDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCompetencyAssessmentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCompetencyAssessmentDateTime = null;
					else entity.LastCompetencyAssessmentDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ObservationInstrumentQuestionnaireID
			{
				get
				{
					System.Int32? data = entity.ObservationInstrumentQuestionnaireID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ObservationInstrumentQuestionnaireID = null;
					else entity.ObservationInstrumentQuestionnaireID = Convert.ToInt32(value);
				}
			}
			public System.String IsCompletelyObservationInstrumentAssessment
			{
				get
				{
					System.Boolean? data = entity.IsCompletelyObservationInstrumentAssessment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCompletelyObservationInstrumentAssessment = null;
					else entity.IsCompletelyObservationInstrumentAssessment = Convert.ToBoolean(value);
				}
			}
			public System.String ObservationInstrumentAssessmentScore
			{
				get
				{
					System.Decimal? data = entity.ObservationInstrumentAssessmentScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ObservationInstrumentAssessmentScore = null;
					else entity.ObservationInstrumentAssessmentScore = Convert.ToDecimal(value);
				}
			}
			public System.String LastObservationInstrumentAssessmentDateTime
			{
				get
				{
					System.DateTime? data = entity.LastObservationInstrumentAssessmentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastObservationInstrumentAssessmentDateTime = null;
					else entity.LastObservationInstrumentAssessmentDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastObservationInstrumentAssessmentByUserID
			{
				get
				{
					System.String data = entity.LastObservationInstrumentAssessmentByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastObservationInstrumentAssessmentByUserID = null;
					else entity.LastObservationInstrumentAssessmentByUserID = Convert.ToString(value);
				}
			}
			public System.String DispositionNo
			{
				get
				{
					System.String data = entity.DispositionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DispositionNo = null;
					else entity.DispositionNo = Convert.ToString(value);
				}
			}
			public System.String ScheduleDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleDate = null;
					else entity.ScheduleDate = Convert.ToDateTime(value);
				}
			}
			public System.String ScheduleTimeFrom
			{
				get
				{
					System.String data = entity.ScheduleTimeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleTimeFrom = null;
					else entity.ScheduleTimeFrom = Convert.ToString(value);
				}
			}
			public System.String ScheduleTimeTo
			{
				get
				{
					System.String data = entity.ScheduleTimeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleTimeTo = null;
					else entity.ScheduleTimeTo = Convert.ToString(value);
				}
			}
			public System.String CredentialingLocation
			{
				get
				{
					System.String data = entity.CredentialingLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialingLocation = null;
					else entity.CredentialingLocation = Convert.ToString(value);
				}
			}
			public System.String CredentialingInvitationLetterNo
			{
				get
				{
					System.String data = entity.CredentialingInvitationLetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialingInvitationLetterNo = null;
					else entity.CredentialingInvitationLetterNo = Convert.ToString(value);
				}
			}
			public System.String InvitationNo
			{
				get
				{
					System.String data = entity.InvitationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvitationNo = null;
					else entity.InvitationNo = Convert.ToString(value);
				}
			}
			public System.String SchedulingDateTime
			{
				get
				{
					System.DateTime? data = entity.SchedulingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchedulingDateTime = null;
					else entity.SchedulingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SchedulingByUserID
			{
				get
				{
					System.String data = entity.SchedulingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchedulingByUserID = null;
					else entity.SchedulingByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRecommendation
			{
				get
				{
					System.Boolean? data = entity.IsRecommendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRecommendation = null;
					else entity.IsRecommendation = Convert.ToBoolean(value);
				}
			}
			public System.String LastRecommendationDateTime
			{
				get
				{
					System.DateTime? data = entity.LastRecommendationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationDateTime = null;
					else entity.LastRecommendationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastRecommendationByUserID
			{
				get
				{
					System.String data = entity.LastRecommendationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationByUserID = null;
					else entity.LastRecommendationByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRecommendationResult
			{
				get
				{
					System.Boolean? data = entity.IsRecommendationResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRecommendationResult = null;
					else entity.IsRecommendationResult = Convert.ToBoolean(value);
				}
			}
			public System.String RecommendationResultDate
			{
				get
				{
					System.DateTime? data = entity.RecommendationResultDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecommendationResultDate = null;
					else entity.RecommendationResultDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRRecommendationResult
			{
				get
				{
					System.String data = entity.SRRecommendationResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecommendationResult = null;
					else entity.SRRecommendationResult = Convert.ToString(value);
				}
			}
			public System.String RecommendationResultNotes
			{
				get
				{
					System.String data = entity.RecommendationResultNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecommendationResultNotes = null;
					else entity.RecommendationResultNotes = Convert.ToString(value);
				}
			}
			public System.String LastRecommendationResultDateTime
			{
				get
				{
					System.DateTime? data = entity.LastRecommendationResultDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationResultDateTime = null;
					else entity.LastRecommendationResultDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastRecommendationResultByUserID
			{
				get
				{
					System.String data = entity.LastRecommendationResultByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastRecommendationResultByUserID = null;
					else entity.LastRecommendationResultByUserID = Convert.ToString(value);
				}
			}
			public System.String IsConclusion
			{
				get
				{
					System.Boolean? data = entity.IsConclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConclusion = null;
					else entity.IsConclusion = Convert.ToBoolean(value);
				}
			}
			public System.String ConclusionDate
			{
				get
				{
					System.DateTime? data = entity.ConclusionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionDate = null;
					else entity.ConclusionDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRConclusionResult
			{
				get
				{
					System.String data = entity.SRConclusionResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConclusionResult = null;
					else entity.SRConclusionResult = Convert.ToString(value);
				}
			}
			public System.String ConclusionNotes
			{
				get
				{
					System.String data = entity.ConclusionNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionNotes = null;
					else entity.ConclusionNotes = Convert.ToString(value);
				}
			}
			public System.String SRCredentialingConclusion
			{
				get
				{
					System.String data = entity.SRCredentialingConclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialingConclusion = null;
					else entity.SRCredentialingConclusion = Convert.ToString(value);
				}
			}
			public System.String CredentialingConclusionDesc
			{
				get
				{
					System.String data = entity.CredentialingConclusionDesc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialingConclusionDesc = null;
					else entity.CredentialingConclusionDesc = Convert.ToString(value);
				}
			}
			public System.String LastConclusionDateTime
			{
				get
				{
					System.DateTime? data = entity.LastConclusionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastConclusionDateTime = null;
					else entity.LastConclusionDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastConclusionByUserID
			{
				get
				{
					System.String data = entity.LastConclusionByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastConclusionByUserID = null;
					else entity.LastConclusionByUserID = Convert.ToString(value);
				}
			}
			public System.String ClinicalAppoinmentNo
			{
				get
				{
					System.String data = entity.ClinicalAppoinmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalAppoinmentNo = null;
					else entity.ClinicalAppoinmentNo = Convert.ToString(value);
				}
			}
			public System.String ClinicalAppoinmentDateOfIssue
			{
				get
				{
					System.DateTime? data = entity.ClinicalAppoinmentDateOfIssue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalAppoinmentDateOfIssue = null;
					else entity.ClinicalAppoinmentDateOfIssue = Convert.ToDateTime(value);
				}
			}
			public System.String ClinicalAppoinmentValidTo
			{
				get
				{
					System.DateTime? data = entity.ClinicalAppoinmentValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalAppoinmentValidTo = null;
					else entity.ClinicalAppoinmentValidTo = Convert.ToDateTime(value);
				}
			}
			public System.String SRClinicalAppoinmentStatus
			{
				get
				{
					System.String data = entity.SRClinicalAppoinmentStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalAppoinmentStatus = null;
					else entity.SRClinicalAppoinmentStatus = Convert.ToString(value);
				}
			}
			public System.String ClinicalAppoinmentNotes
			{
				get
				{
					System.String data = entity.ClinicalAppoinmentNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalAppoinmentNotes = null;
					else entity.ClinicalAppoinmentNotes = Convert.ToString(value);
				}
			}
			public System.String LastClinicalAppoinmentDateTime
			{
				get
				{
					System.DateTime? data = entity.LastClinicalAppoinmentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastClinicalAppoinmentDateTime = null;
					else entity.LastClinicalAppoinmentDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastClinicalAppoinmentByUserID
			{
				get
				{
					System.String data = entity.LastClinicalAppoinmentByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastClinicalAppoinmentByUserID = null;
					else entity.LastClinicalAppoinmentByUserID = Convert.ToString(value);
				}
			}
			public System.String IsCi
			{
				get
				{
					System.Boolean? data = entity.IsCi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCi = null;
					else entity.IsCi = Convert.ToBoolean(value);
				}
			}
			public System.String SRKtklLevel
			{
				get
				{
					System.String data = entity.SRKtklLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKtklLevel = null;
					else entity.SRKtklLevel = Convert.ToString(value);
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
			private esCredentialProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessQuery query)
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
				throw new Exception("esCredentialProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcess : esCredentialProcess
	{
	}

	[Serializable]
	abstract public class esCredentialProcessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem SREducationalQualification
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SREducationalQualification, esSystemType.String);
			}
		}

		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		}

		public esQueryItem DiplomaNumber
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DiplomaNumber, esSystemType.String);
			}
		}

		public esQueryItem DiplomaDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DiplomaDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CompetencyCertificateNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyCertificateNo, esSystemType.String);
			}
		}

		public esQueryItem CompetencyCertificateDateOfIssue
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyCertificateDateOfIssue, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem CompetencyAssessmentVerificationDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CompetencyAssessmentVerificationBy
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy, esSystemType.Int32);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CompetencyAssessmentVerificationDate2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate2, esSystemType.DateTime);
			}
		}

		public esQueryItem CompetencyAssessmentVerificationBy2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy2, esSystemType.Int32);
			}
		}

		public esQueryItem IsVerified2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsVerified2, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VerifiedDateTime2, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.VerifiedByUserID2, esSystemType.String);
			}
		}

		public esQueryItem IsCompletelyVerified
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCompletelyVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem CredentialApplicationDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CredentialApplicationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCredentialingStatus
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRCredentialingStatus, esSystemType.String);
			}
		}

		public esQueryItem SRRecredentialReason
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRRecredentialReason, esSystemType.String);
			}
		}

		public esQueryItem IsCredentialApplication
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCredentialApplication, esSystemType.Boolean);
			}
		}

		public esQueryItem LastCredentialApplicationDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastCredentialApplicationDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCredentialApplicationByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastCredentialApplicationByUserID, esSystemType.String);
			}
		}

		public esQueryItem CredentialingDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CredentialingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCertificateVerification
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCertificateVerification, esSystemType.Boolean);
			}
		}

		public esQueryItem RecommendationNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.RecommendationNotes, esSystemType.String);
			}
		}

		public esQueryItem IsPerform
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsPerform, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCredentialing
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCredentialing, esSystemType.Boolean);
			}
		}

		public esQueryItem LastCredentialingDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastCredentialingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCredentialingByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastCredentialingByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsReprocess
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsReprocess, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem RecommendationLetterDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.RecommendationLetterDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RecommendationLetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.RecommendationLetterNo, esSystemType.String);
			}
		}

		public esQueryItem IsRecommendationLetter
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsRecommendationLetter, esSystemType.Boolean);
			}
		}

		public esQueryItem LastRecommendationLetterDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationLetterDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastRecommendationLetterByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationLetterByUserID, esSystemType.String);
			}
		}

		public esQueryItem ClinicalAssignmentLetterDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ClinicalAssignmentLetterDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DecreeNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DecreeNo, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem IsClinicalAssignmentLetter
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsClinicalAssignmentLetter, esSystemType.Boolean);
			}
		}

		public esQueryItem LastClinicalAssignmentLetterDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastClinicalAssignmentLetterByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsDocumentComplete
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsDocumentComplete, esSystemType.Boolean);
			}
		}

		public esQueryItem DocumentIncompleteNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DocumentIncompleteNotes, esSystemType.String);
			}
		}

		public esQueryItem IsDocumentChecking
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsDocumentChecking, esSystemType.Boolean);
			}
		}

		public esQueryItem DocumentCheckingDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DocumentCheckingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem DocumentCheckingByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DocumentCheckingByUserID, esSystemType.String);
			}
		}

		public esQueryItem EthicsQuestionnariePersonID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.EthicsQuestionnariePersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EthicsQuestionnarieDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EthicsQuestionnarieByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastEthicsQuestionnarieDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastEthicsQuestionnarieDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CompetencyAssessmentDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CompetencyAssessmentByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CompetencyAssessmentByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastCompetencyAssessmentDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastCompetencyAssessmentDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ObservationInstrumentQuestionnaireID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ObservationInstrumentQuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem IsCompletelyObservationInstrumentAssessment
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCompletelyObservationInstrumentAssessment, esSystemType.Boolean);
			}
		}

		public esQueryItem ObservationInstrumentAssessmentScore
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ObservationInstrumentAssessmentScore, esSystemType.Decimal);
			}
		}

		public esQueryItem LastObservationInstrumentAssessmentDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastObservationInstrumentAssessmentByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentByUserID, esSystemType.String);
			}
		}

		public esQueryItem DispositionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.DispositionNo, esSystemType.String);
			}
		}

		public esQueryItem ScheduleDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ScheduleTimeFrom
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ScheduleTimeFrom, esSystemType.String);
			}
		}

		public esQueryItem ScheduleTimeTo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ScheduleTimeTo, esSystemType.String);
			}
		}

		public esQueryItem CredentialingLocation
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CredentialingLocation, esSystemType.String);
			}
		}

		public esQueryItem CredentialingInvitationLetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CredentialingInvitationLetterNo, esSystemType.String);
			}
		}

		public esQueryItem InvitationNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.InvitationNo, esSystemType.String);
			}
		}

		public esQueryItem SchedulingDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SchedulingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SchedulingByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SchedulingByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRecommendation
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsRecommendation, esSystemType.Boolean);
			}
		}

		public esQueryItem LastRecommendationDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastRecommendationByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRecommendationResult
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsRecommendationResult, esSystemType.Boolean);
			}
		}

		public esQueryItem RecommendationResultDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.RecommendationResultDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRRecommendationResult
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRRecommendationResult, esSystemType.String);
			}
		}

		public esQueryItem RecommendationResultNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.RecommendationResultNotes, esSystemType.String);
			}
		}

		public esQueryItem LastRecommendationResultDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationResultDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastRecommendationResultByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastRecommendationResultByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsConclusion
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsConclusion, esSystemType.Boolean);
			}
		}

		public esQueryItem ConclusionDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ConclusionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRConclusionResult
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRConclusionResult, esSystemType.String);
			}
		}

		public esQueryItem ConclusionNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ConclusionNotes, esSystemType.String);
			}
		}

		public esQueryItem SRCredentialingConclusion
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRCredentialingConclusion, esSystemType.String);
			}
		}

		public esQueryItem CredentialingConclusionDesc
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CredentialingConclusionDesc, esSystemType.String);
			}
		}

		public esQueryItem LastConclusionDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastConclusionDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastConclusionByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastConclusionByUserID, esSystemType.String);
			}
		}

		public esQueryItem ClinicalAppoinmentNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNo, esSystemType.String);
			}
		}

		public esQueryItem ClinicalAppoinmentDateOfIssue
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentDateOfIssue, esSystemType.DateTime);
			}
		}

		public esQueryItem ClinicalAppoinmentValidTo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem SRClinicalAppoinmentStatus
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRClinicalAppoinmentStatus, esSystemType.String);
			}
		}

		public esQueryItem ClinicalAppoinmentNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNotes, esSystemType.String);
			}
		}

		public esQueryItem LastClinicalAppoinmentDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastClinicalAppoinmentByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsCi
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.IsCi, esSystemType.Boolean);
			}
		}

		public esQueryItem SRKtklLevel
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.SRKtklLevel, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessCollection")]
	public partial class CredentialProcessCollection : esCredentialProcessCollection, IEnumerable<CredentialProcess>
	{
		public CredentialProcessCollection()
		{

		}

		public static implicit operator List<CredentialProcess>(CredentialProcessCollection coll)
		{
			List<CredentialProcess> list = new List<CredentialProcess>();

			foreach (CredentialProcess emp in coll)
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
				return CredentialProcessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcess();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessQuery();
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
		public bool Load(CredentialProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcess AddNew()
		{
			CredentialProcess entity = base.AddNewEntity() as CredentialProcess;

			return entity;
		}
		public CredentialProcess FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as CredentialProcess;
		}

		#region IEnumerable< CredentialProcess> Members

		IEnumerator<CredentialProcess> IEnumerable<CredentialProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcess;
			}
		}

		#endregion

		private CredentialProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcess ({TransactionNo})")]
	[Serializable]
	public partial class CredentialProcess : esCredentialProcess
	{
		public CredentialProcess()
		{
		}

		public CredentialProcess(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessMetadata.Meta();
			}
		}

		override protected esCredentialProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessQuery();
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
		public bool Load(CredentialProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessQuery : esCredentialProcessQuery
	{
		public CredentialProcessQuery()
		{

		}

		public CredentialProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.QuestionFormID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.PersonID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SREmploymentType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRProfessionGroup, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRClinicalWorkArea, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRClinicalAuthorityLevel, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.QuestionnaireID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.QuestionnaireID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SREducationLevel, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SREducationalQualification, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SREducationalQualification;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.InstitutionName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DiplomaNumber, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DiplomaNumber;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DiplomaDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DiplomaDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyCertificateNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyCertificateNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyCertificateDateOfIssue, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyCertificateDateOfIssue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsApproved, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ApprovedDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ApprovedByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsVoid, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VoidDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VoidByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentVerificationDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentVerificationBy;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsVerified, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VerifiedDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VerifiedByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationDate2, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentVerificationDate2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentVerificationBy2, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentVerificationBy2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsVerified2, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsVerified2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VerifiedDateTime2, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VerifiedDateTime2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.VerifiedByUserID2, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.VerifiedByUserID2;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCompletelyVerified, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCompletelyVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CredentialApplicationDate, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CredentialApplicationDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ServiceUnitID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.PositionID, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRCredentialingStatus, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRCredentialingStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRRecredentialReason, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRRecredentialReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCredentialApplication, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCredentialApplication;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastCredentialApplicationDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastCredentialApplicationByUserID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastCredentialApplicationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CredentialingDate, 41, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CredentialingDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCertificateVerification, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCertificateVerification;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.RecommendationNotes, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.RecommendationNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsPerform, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsPerform;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCredentialing, 45, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCredentialing;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastCredentialingDateTime, 46, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastCredentialingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastCredentialingByUserID, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastCredentialingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsReprocess, 48, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsReprocess;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ReferenceNo, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.RecommendationLetterDate, 50, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.RecommendationLetterDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.RecommendationLetterNo, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.RecommendationLetterNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsRecommendationLetter, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsRecommendationLetter;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterDateTime, 53, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationLetterDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationLetterByUserID, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationLetterByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ClinicalAssignmentLetterDate, 55, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ClinicalAssignmentLetterDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DecreeNo, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DecreeNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ValidFrom, 57, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ValidTo, 58, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsClinicalAssignmentLetter, 59, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsClinicalAssignmentLetter;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterDateTime, 60, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastClinicalAssignmentLetterDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastClinicalAssignmentLetterByUserID, 61, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastClinicalAssignmentLetterByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsDocumentComplete, 62, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsDocumentComplete;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DocumentIncompleteNotes, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DocumentIncompleteNotes;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsDocumentChecking, 64, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsDocumentChecking;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DocumentCheckingDateTime, 65, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DocumentCheckingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DocumentCheckingByUserID, 66, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DocumentCheckingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.EthicsQuestionnariePersonID, 67, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.EthicsQuestionnariePersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieDate, 68, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.EthicsQuestionnarieDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.EthicsQuestionnarieByUserID, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.EthicsQuestionnarieByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastEthicsQuestionnarieDateTime, 70, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastEthicsQuestionnarieDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentDate, 71, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CompetencyAssessmentByUserID, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CompetencyAssessmentByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastCompetencyAssessmentDateTime, 73, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastCompetencyAssessmentDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ObservationInstrumentQuestionnaireID, 74, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ObservationInstrumentQuestionnaireID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCompletelyObservationInstrumentAssessment, 75, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCompletelyObservationInstrumentAssessment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ObservationInstrumentAssessmentScore, 76, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ObservationInstrumentAssessmentScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentDateTime, 77, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastObservationInstrumentAssessmentDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastObservationInstrumentAssessmentByUserID, 78, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastObservationInstrumentAssessmentByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.DispositionNo, 79, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.DispositionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ScheduleDate, 80, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ScheduleDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ScheduleTimeFrom, 81, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ScheduleTimeFrom;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ScheduleTimeTo, 82, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ScheduleTimeTo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CredentialingLocation, 83, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CredentialingLocation;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CredentialingInvitationLetterNo, 84, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CredentialingInvitationLetterNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.InvitationNo, 85, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.InvitationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SchedulingDateTime, 86, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SchedulingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SchedulingByUserID, 87, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SchedulingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsRecommendation, 88, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsRecommendation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationDateTime, 89, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationByUserID, 90, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsRecommendationResult, 91, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsRecommendationResult;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.RecommendationResultDate, 92, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.RecommendationResultDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRRecommendationResult, 93, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRRecommendationResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.RecommendationResultNotes, 94, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.RecommendationResultNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationResultDateTime, 95, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationResultDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastRecommendationResultByUserID, 96, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastRecommendationResultByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsConclusion, 97, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsConclusion;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ConclusionDate, 98, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ConclusionDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRConclusionResult, 99, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRConclusionResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ConclusionNotes, 100, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ConclusionNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRCredentialingConclusion, 101, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRCredentialingConclusion;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CredentialingConclusionDesc, 102, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CredentialingConclusionDesc;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastConclusionDateTime, 103, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastConclusionDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastConclusionByUserID, 104, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastConclusionByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNo, 105, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ClinicalAppoinmentNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentDateOfIssue, 106, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ClinicalAppoinmentDateOfIssue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentValidTo, 107, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ClinicalAppoinmentValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRClinicalAppoinmentStatus, 108, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRClinicalAppoinmentStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.ClinicalAppoinmentNotes, 109, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.ClinicalAppoinmentNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentDateTime, 110, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastClinicalAppoinmentDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastClinicalAppoinmentByUserID, 111, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastClinicalAppoinmentByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.IsCi, 112, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.IsCi;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.SRKtklLevel, 113, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.SRKtklLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CreatedDateTime, 114, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.CreatedByUserID, 115, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastUpdateDateTime, 116, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessMetadata.ColumnNames.LastUpdateByUserID, 117, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessMetadata Meta()
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
			public const string QuestionFormID = "QuestionFormID";
			public const string TransactionDate = "TransactionDate";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string SREducationLevel = "SREducationLevel";
			public const string SREducationalQualification = "SREducationalQualification";
			public const string InstitutionName = "InstitutionName";
			public const string DiplomaNumber = "DiplomaNumber";
			public const string DiplomaDate = "DiplomaDate";
			public const string CompetencyCertificateNo = "CompetencyCertificateNo";
			public const string CompetencyCertificateDateOfIssue = "CompetencyCertificateDateOfIssue";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CompetencyAssessmentVerificationDate = "CompetencyAssessmentVerificationDate";
			public const string CompetencyAssessmentVerificationBy = "CompetencyAssessmentVerificationBy";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CompetencyAssessmentVerificationDate2 = "CompetencyAssessmentVerificationDate2";
			public const string CompetencyAssessmentVerificationBy2 = "CompetencyAssessmentVerificationBy2";
			public const string IsVerified2 = "IsVerified2";
			public const string VerifiedDateTime2 = "VerifiedDateTime2";
			public const string VerifiedByUserID2 = "VerifiedByUserID2";
			public const string IsCompletelyVerified = "IsCompletelyVerified";
			public const string CredentialApplicationDate = "CredentialApplicationDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PositionID = "PositionID";
			public const string SRCredentialingStatus = "SRCredentialingStatus";
			public const string SRRecredentialReason = "SRRecredentialReason";
			public const string IsCredentialApplication = "IsCredentialApplication";
			public const string LastCredentialApplicationDateTime = "LastCredentialApplicationDateTime";
			public const string LastCredentialApplicationByUserID = "LastCredentialApplicationByUserID";
			public const string CredentialingDate = "CredentialingDate";
			public const string IsCertificateVerification = "IsCertificateVerification";
			public const string RecommendationNotes = "RecommendationNotes";
			public const string IsPerform = "IsPerform";
			public const string IsCredentialing = "IsCredentialing";
			public const string LastCredentialingDateTime = "LastCredentialingDateTime";
			public const string LastCredentialingByUserID = "LastCredentialingByUserID";
			public const string IsReprocess = "IsReprocess";
			public const string ReferenceNo = "ReferenceNo";
			public const string RecommendationLetterDate = "RecommendationLetterDate";
			public const string RecommendationLetterNo = "RecommendationLetterNo";
			public const string IsRecommendationLetter = "IsRecommendationLetter";
			public const string LastRecommendationLetterDateTime = "LastRecommendationLetterDateTime";
			public const string LastRecommendationLetterByUserID = "LastRecommendationLetterByUserID";
			public const string ClinicalAssignmentLetterDate = "ClinicalAssignmentLetterDate";
			public const string DecreeNo = "DecreeNo";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string IsClinicalAssignmentLetter = "IsClinicalAssignmentLetter";
			public const string LastClinicalAssignmentLetterDateTime = "LastClinicalAssignmentLetterDateTime";
			public const string LastClinicalAssignmentLetterByUserID = "LastClinicalAssignmentLetterByUserID";
			public const string IsDocumentComplete = "IsDocumentComplete";
			public const string DocumentIncompleteNotes = "DocumentIncompleteNotes";
			public const string IsDocumentChecking = "IsDocumentChecking";
			public const string DocumentCheckingDateTime = "DocumentCheckingDateTime";
			public const string DocumentCheckingByUserID = "DocumentCheckingByUserID";
			public const string EthicsQuestionnariePersonID = "EthicsQuestionnariePersonID";
			public const string EthicsQuestionnarieDate = "EthicsQuestionnarieDate";
			public const string EthicsQuestionnarieByUserID = "EthicsQuestionnarieByUserID";
			public const string LastEthicsQuestionnarieDateTime = "LastEthicsQuestionnarieDateTime";
			public const string CompetencyAssessmentDate = "CompetencyAssessmentDate";
			public const string CompetencyAssessmentByUserID = "CompetencyAssessmentByUserID";
			public const string LastCompetencyAssessmentDateTime = "LastCompetencyAssessmentDateTime";
			public const string ObservationInstrumentQuestionnaireID = "ObservationInstrumentQuestionnaireID";
			public const string IsCompletelyObservationInstrumentAssessment = "IsCompletelyObservationInstrumentAssessment";
			public const string ObservationInstrumentAssessmentScore = "ObservationInstrumentAssessmentScore";
			public const string LastObservationInstrumentAssessmentDateTime = "LastObservationInstrumentAssessmentDateTime";
			public const string LastObservationInstrumentAssessmentByUserID = "LastObservationInstrumentAssessmentByUserID";
			public const string DispositionNo = "DispositionNo";
			public const string ScheduleDate = "ScheduleDate";
			public const string ScheduleTimeFrom = "ScheduleTimeFrom";
			public const string ScheduleTimeTo = "ScheduleTimeTo";
			public const string CredentialingLocation = "CredentialingLocation";
			public const string CredentialingInvitationLetterNo = "CredentialingInvitationLetterNo";
			public const string InvitationNo = "InvitationNo";
			public const string SchedulingDateTime = "SchedulingDateTime";
			public const string SchedulingByUserID = "SchedulingByUserID";
			public const string IsRecommendation = "IsRecommendation";
			public const string LastRecommendationDateTime = "LastRecommendationDateTime";
			public const string LastRecommendationByUserID = "LastRecommendationByUserID";
			public const string IsRecommendationResult = "IsRecommendationResult";
			public const string RecommendationResultDate = "RecommendationResultDate";
			public const string SRRecommendationResult = "SRRecommendationResult";
			public const string RecommendationResultNotes = "RecommendationResultNotes";
			public const string LastRecommendationResultDateTime = "LastRecommendationResultDateTime";
			public const string LastRecommendationResultByUserID = "LastRecommendationResultByUserID";
			public const string IsConclusion = "IsConclusion";
			public const string ConclusionDate = "ConclusionDate";
			public const string SRConclusionResult = "SRConclusionResult";
			public const string ConclusionNotes = "ConclusionNotes";
			public const string SRCredentialingConclusion = "SRCredentialingConclusion";
			public const string CredentialingConclusionDesc = "CredentialingConclusionDesc";
			public const string LastConclusionDateTime = "LastConclusionDateTime";
			public const string LastConclusionByUserID = "LastConclusionByUserID";
			public const string ClinicalAppoinmentNo = "ClinicalAppoinmentNo";
			public const string ClinicalAppoinmentDateOfIssue = "ClinicalAppoinmentDateOfIssue";
			public const string ClinicalAppoinmentValidTo = "ClinicalAppoinmentValidTo";
			public const string SRClinicalAppoinmentStatus = "SRClinicalAppoinmentStatus";
			public const string ClinicalAppoinmentNotes = "ClinicalAppoinmentNotes";
			public const string LastClinicalAppoinmentDateTime = "LastClinicalAppoinmentDateTime";
			public const string LastClinicalAppoinmentByUserID = "LastClinicalAppoinmentByUserID";
			public const string IsCi = "IsCi";
			public const string SRKtklLevel = "SRKtklLevel";
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
			public const string QuestionFormID = "QuestionFormID";
			public const string TransactionDate = "TransactionDate";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string SREducationLevel = "SREducationLevel";
			public const string SREducationalQualification = "SREducationalQualification";
			public const string InstitutionName = "InstitutionName";
			public const string DiplomaNumber = "DiplomaNumber";
			public const string DiplomaDate = "DiplomaDate";
			public const string CompetencyCertificateNo = "CompetencyCertificateNo";
			public const string CompetencyCertificateDateOfIssue = "CompetencyCertificateDateOfIssue";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CompetencyAssessmentVerificationDate = "CompetencyAssessmentVerificationDate";
			public const string CompetencyAssessmentVerificationBy = "CompetencyAssessmentVerificationBy";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CompetencyAssessmentVerificationDate2 = "CompetencyAssessmentVerificationDate2";
			public const string CompetencyAssessmentVerificationBy2 = "CompetencyAssessmentVerificationBy2";
			public const string IsVerified2 = "IsVerified2";
			public const string VerifiedDateTime2 = "VerifiedDateTime2";
			public const string VerifiedByUserID2 = "VerifiedByUserID2";
			public const string IsCompletelyVerified = "IsCompletelyVerified";
			public const string CredentialApplicationDate = "CredentialApplicationDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PositionID = "PositionID";
			public const string SRCredentialingStatus = "SRCredentialingStatus";
			public const string SRRecredentialReason = "SRRecredentialReason";
			public const string IsCredentialApplication = "IsCredentialApplication";
			public const string LastCredentialApplicationDateTime = "LastCredentialApplicationDateTime";
			public const string LastCredentialApplicationByUserID = "LastCredentialApplicationByUserID";
			public const string CredentialingDate = "CredentialingDate";
			public const string IsCertificateVerification = "IsCertificateVerification";
			public const string RecommendationNotes = "RecommendationNotes";
			public const string IsPerform = "IsPerform";
			public const string IsCredentialing = "IsCredentialing";
			public const string LastCredentialingDateTime = "LastCredentialingDateTime";
			public const string LastCredentialingByUserID = "LastCredentialingByUserID";
			public const string IsReprocess = "IsReprocess";
			public const string ReferenceNo = "ReferenceNo";
			public const string RecommendationLetterDate = "RecommendationLetterDate";
			public const string RecommendationLetterNo = "RecommendationLetterNo";
			public const string IsRecommendationLetter = "IsRecommendationLetter";
			public const string LastRecommendationLetterDateTime = "LastRecommendationLetterDateTime";
			public const string LastRecommendationLetterByUserID = "LastRecommendationLetterByUserID";
			public const string ClinicalAssignmentLetterDate = "ClinicalAssignmentLetterDate";
			public const string DecreeNo = "DecreeNo";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string IsClinicalAssignmentLetter = "IsClinicalAssignmentLetter";
			public const string LastClinicalAssignmentLetterDateTime = "LastClinicalAssignmentLetterDateTime";
			public const string LastClinicalAssignmentLetterByUserID = "LastClinicalAssignmentLetterByUserID";
			public const string IsDocumentComplete = "IsDocumentComplete";
			public const string DocumentIncompleteNotes = "DocumentIncompleteNotes";
			public const string IsDocumentChecking = "IsDocumentChecking";
			public const string DocumentCheckingDateTime = "DocumentCheckingDateTime";
			public const string DocumentCheckingByUserID = "DocumentCheckingByUserID";
			public const string EthicsQuestionnariePersonID = "EthicsQuestionnariePersonID";
			public const string EthicsQuestionnarieDate = "EthicsQuestionnarieDate";
			public const string EthicsQuestionnarieByUserID = "EthicsQuestionnarieByUserID";
			public const string LastEthicsQuestionnarieDateTime = "LastEthicsQuestionnarieDateTime";
			public const string CompetencyAssessmentDate = "CompetencyAssessmentDate";
			public const string CompetencyAssessmentByUserID = "CompetencyAssessmentByUserID";
			public const string LastCompetencyAssessmentDateTime = "LastCompetencyAssessmentDateTime";
			public const string ObservationInstrumentQuestionnaireID = "ObservationInstrumentQuestionnaireID";
			public const string IsCompletelyObservationInstrumentAssessment = "IsCompletelyObservationInstrumentAssessment";
			public const string ObservationInstrumentAssessmentScore = "ObservationInstrumentAssessmentScore";
			public const string LastObservationInstrumentAssessmentDateTime = "LastObservationInstrumentAssessmentDateTime";
			public const string LastObservationInstrumentAssessmentByUserID = "LastObservationInstrumentAssessmentByUserID";
			public const string DispositionNo = "DispositionNo";
			public const string ScheduleDate = "ScheduleDate";
			public const string ScheduleTimeFrom = "ScheduleTimeFrom";
			public const string ScheduleTimeTo = "ScheduleTimeTo";
			public const string CredentialingLocation = "CredentialingLocation";
			public const string CredentialingInvitationLetterNo = "CredentialingInvitationLetterNo";
			public const string InvitationNo = "InvitationNo";
			public const string SchedulingDateTime = "SchedulingDateTime";
			public const string SchedulingByUserID = "SchedulingByUserID";
			public const string IsRecommendation = "IsRecommendation";
			public const string LastRecommendationDateTime = "LastRecommendationDateTime";
			public const string LastRecommendationByUserID = "LastRecommendationByUserID";
			public const string IsRecommendationResult = "IsRecommendationResult";
			public const string RecommendationResultDate = "RecommendationResultDate";
			public const string SRRecommendationResult = "SRRecommendationResult";
			public const string RecommendationResultNotes = "RecommendationResultNotes";
			public const string LastRecommendationResultDateTime = "LastRecommendationResultDateTime";
			public const string LastRecommendationResultByUserID = "LastRecommendationResultByUserID";
			public const string IsConclusion = "IsConclusion";
			public const string ConclusionDate = "ConclusionDate";
			public const string SRConclusionResult = "SRConclusionResult";
			public const string ConclusionNotes = "ConclusionNotes";
			public const string SRCredentialingConclusion = "SRCredentialingConclusion";
			public const string CredentialingConclusionDesc = "CredentialingConclusionDesc";
			public const string LastConclusionDateTime = "LastConclusionDateTime";
			public const string LastConclusionByUserID = "LastConclusionByUserID";
			public const string ClinicalAppoinmentNo = "ClinicalAppoinmentNo";
			public const string ClinicalAppoinmentDateOfIssue = "ClinicalAppoinmentDateOfIssue";
			public const string ClinicalAppoinmentValidTo = "ClinicalAppoinmentValidTo";
			public const string SRClinicalAppoinmentStatus = "SRClinicalAppoinmentStatus";
			public const string ClinicalAppoinmentNotes = "ClinicalAppoinmentNotes";
			public const string LastClinicalAppoinmentDateTime = "LastClinicalAppoinmentDateTime";
			public const string LastClinicalAppoinmentByUserID = "LastClinicalAppoinmentByUserID";
			public const string IsCi = "IsCi";
			public const string SRKtklLevel = "SRKtklLevel";
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
			lock (typeof(CredentialProcessMetadata))
			{
				if (CredentialProcessMetadata.mapDelegates == null)
				{
					CredentialProcessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessMetadata.meta == null)
				{
					CredentialProcessMetadata.meta = new CredentialProcessMetadata();
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
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationalQualification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiplomaNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiplomaDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompetencyCertificateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompetencyCertificateDateOfIssue", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompetencyAssessmentVerificationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompetencyAssessmentVerificationBy", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompetencyAssessmentVerificationDate2", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompetencyAssessmentVerificationBy2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsVerified2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime2", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCompletelyVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CredentialApplicationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCredentialingStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRecredentialReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCredentialApplication", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastCredentialApplicationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCredentialApplicationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CredentialingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCertificateVerification", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RecommendationNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPerform", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCredentialing", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastCredentialingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCredentialingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsReprocess", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecommendationLetterDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RecommendationLetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRecommendationLetter", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastRecommendationLetterDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastRecommendationLetterByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalAssignmentLetterDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DecreeNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsClinicalAssignmentLetter", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastClinicalAssignmentLetterDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastClinicalAssignmentLetterByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDocumentComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DocumentIncompleteNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDocumentChecking", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DocumentCheckingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DocumentCheckingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EthicsQuestionnariePersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EthicsQuestionnarieDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EthicsQuestionnarieByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastEthicsQuestionnarieDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompetencyAssessmentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CompetencyAssessmentByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCompetencyAssessmentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ObservationInstrumentQuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsCompletelyObservationInstrumentAssessment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ObservationInstrumentAssessmentScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastObservationInstrumentAssessmentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastObservationInstrumentAssessmentByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DispositionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ScheduleTimeFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleTimeTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CredentialingLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CredentialingInvitationLetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvitationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SchedulingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SchedulingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRecommendation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastRecommendationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastRecommendationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRecommendationResult", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RecommendationResultDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRRecommendationResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecommendationResultNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastRecommendationResultDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastRecommendationResultByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConclusion", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ConclusionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRConclusionResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConclusionNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCredentialingConclusion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CredentialingConclusionDesc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastConclusionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastConclusionByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalAppoinmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalAppoinmentDateOfIssue", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClinicalAppoinmentValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRClinicalAppoinmentStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalAppoinmentNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastClinicalAppoinmentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastClinicalAppoinmentByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRKtklLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcess";
				meta.Destination = "CredentialProcess";
				meta.spInsert = "proc_CredentialProcessInsert";
				meta.spUpdate = "proc_CredentialProcessUpdate";
				meta.spDelete = "proc_CredentialProcessDelete";
				meta.spLoadAll = "proc_CredentialProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
