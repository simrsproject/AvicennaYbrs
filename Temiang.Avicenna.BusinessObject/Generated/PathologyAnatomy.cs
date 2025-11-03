/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/29/2023 11:26:32 AM
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
	abstract public class esPathologyAnatomyCollection : esEntityCollectionWAuditLog
	{
		public esPathologyAnatomyCollection()
		{

		}
		
		protected override string GetCollectionName()
		{
			return "PathologyAnatomyCollection";
		}

		#region Query Logic
		protected void InitQuery(esPathologyAnatomyQuery query)
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
			this.InitQuery(query as esPathologyAnatomyQuery);
		}
		#endregion

		virtual public PathologyAnatomy DetachEntity(PathologyAnatomy entity)
		{
			return base.DetachEntity(entity) as PathologyAnatomy;
		}

		virtual public PathologyAnatomy AttachEntity(PathologyAnatomy entity)
		{
			return base.AttachEntity(entity) as PathologyAnatomy;
		}

		virtual public void Combine(PathologyAnatomyCollection collection)
		{
			base.Combine(collection);
		}

		new public PathologyAnatomy this[int index]
		{
			get
			{
				return base[index] as PathologyAnatomy;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PathologyAnatomy);
		}
	}

	[Serializable]
	abstract public class esPathologyAnatomy : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPathologyAnatomyQuery GetDynamicQuery()
		{
			return null;
		}

		public esPathologyAnatomy()
		{
		}

		public esPathologyAnatomy(DataRow row)
			: base(row)
		{
		}	

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String resultNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String resultNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(resultNo);
			else
				return LoadByPrimaryKeyStoredProcedure(resultNo);
		}

		private bool LoadByPrimaryKeyDynamic(String resultNo)
		{
			esPathologyAnatomyQuery query = this.GetDynamicQuery();
			query.Where(query.ResultNo == resultNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String resultNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ResultNo", resultNo);
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
						case "ResultNo": this.str.ResultNo = (string)value; break;
						case "ResultDate": this.str.ResultDate = (string)value; break;
						case "ResultTime": this.str.ResultTime = (string)value; break;
						case "ResultType": this.str.ResultType = (string)value; break;
						case "OrderDate": this.str.OrderDate = (string)value; break;
						case "DateOfCompletion": this.str.DateOfCompletion = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "PhysicianSenders": this.str.PhysicianSenders = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "DiagnosisID": this.str.DiagnosisID = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "MorphologyID": this.str.MorphologyID = (string)value; break;
						case "SourceOfTissueID": this.str.SourceOfTissueID = (string)value; break;
						case "TissueID": this.str.TissueID = (string)value; break;
						case "IsMarried": this.str.IsMarried = (string)value; break;
						case "NumberOfChildren": this.str.NumberOfChildren = (string)value; break;
						case "Macroscopic": this.str.Macroscopic = (string)value; break;
						case "Microscopic": this.str.Microscopic = (string)value; break;
						case "Impression": this.str.Impression = (string)value; break;
						case "ImpressionGroupID": this.str.ImpressionGroupID = (string)value; break;
						case "ImpressionGroupItemID": this.str.ImpressionGroupItemID = (string)value; break;
						case "AdditionalNotes": this.str.AdditionalNotes = (string)value; break;
						case "IsEligibleAdequacyOfTheSpecimen": this.str.IsEligibleAdequacyOfTheSpecimen = (string)value; break;
						case "IsTrichomonasVaginalisInfection": this.str.IsTrichomonasVaginalisInfection = (string)value; break;
						case "IsCandidaMoniliaInfection": this.str.IsCandidaMoniliaInfection = (string)value; break;
						case "IsCoccobacillusGardnerellaInfection": this.str.IsCoccobacillusGardnerellaInfection = (string)value; break;
						case "IsActinomycesInfection": this.str.IsActinomycesInfection = (string)value; break;
						case "IsHpvInfection": this.str.IsHpvInfection = (string)value; break;
						case "IsHsv2Infection": this.str.IsHsv2Infection = (string)value; break;
						case "IsOtherInfections": this.str.IsOtherInfections = (string)value; break;
						case "OtherInfectionsDescription": this.str.OtherInfectionsDescription = (string)value; break;
						case "IsInflammatoryReaction": this.str.IsInflammatoryReaction = (string)value; break;
						case "IsRepair": this.str.IsRepair = (string)value; break;
						case "IsRadiation": this.str.IsRadiation = (string)value; break;
						case "IsAtrophy": this.str.IsAtrophy = (string)value; break;
						case "AtrophyDescription": this.str.AtrophyDescription = (string)value; break;
						case "IsFollicularCervicitis": this.str.IsFollicularCervicitis = (string)value; break;
						case "IsChemotherapyEffects": this.str.IsChemotherapyEffects = (string)value; break;
						case "IsHormonalEffects": this.str.IsHormonalEffects = (string)value; break;
						case "IsIudEffects": this.str.IsIudEffects = (string)value; break;
						case "IsAsc": this.str.IsAsc = (string)value; break;
						case "IsAscUs": this.str.IsAscUs = (string)value; break;
						case "IsAscH": this.str.IsAscH = (string)value; break;
						case "IsSquamousIntraepithelialLesion": this.str.IsSquamousIntraepithelialLesion = (string)value; break;
						case "IsLsil": this.str.IsLsil = (string)value; break;
						case "IsLsilHpv": this.str.IsLsilHpv = (string)value; break;
						case "IsLsilCin1": this.str.IsLsilCin1 = (string)value; break;
						case "IsHsil": this.str.IsHsil = (string)value; break;
						case "IsHsilCin2": this.str.IsHsilCin2 = (string)value; break;
						case "IsHsilCin3": this.str.IsHsilCin3 = (string)value; break;
						case "IsHsilCis": this.str.IsHsilCis = (string)value; break;
						case "IsSquamousCellCarcinoma": this.str.IsSquamousCellCarcinoma = (string)value; break;
						case "IsKeratinizing": this.str.IsKeratinizing = (string)value; break;
						case "IsNonKeratinizing": this.str.IsNonKeratinizing = (string)value; break;
						case "IsAgc": this.str.IsAgc = (string)value; break;
						case "IsAtypicalNos": this.str.IsAtypicalNos = (string)value; break;
						case "IsAtypicalFavorNeoplastic": this.str.IsAtypicalFavorNeoplastic = (string)value; break;
						case "IsAis": this.str.IsAis = (string)value; break;
						case "IsAdenocarcinoma": this.str.IsAdenocarcinoma = (string)value; break;
						case "IsEndoCervical": this.str.IsEndoCervical = (string)value; break;
						case "IsEndometrial": this.str.IsEndometrial = (string)value; break;
						case "IsEndometrialCells": this.str.IsEndometrialCells = (string)value; break;
						case "NonEpithelialMalignancies": this.str.NonEpithelialMalignancies = (string)value; break;
						case "IsHormonalPatternsAccordingToAgeAndHistory": this.str.IsHormonalPatternsAccordingToAgeAndHistory = (string)value; break;
						case "IsHormonalPatternDoesNotMatchTheAgeAndHistory": this.str.IsHormonalPatternDoesNotMatchTheAgeAndHistory = (string)value; break;
						case "IsHormonalEvaluationIsNotPossible": this.str.IsHormonalEvaluationIsNotPossible = (string)value; break;
						case "InterpretationOfResults": this.str.InterpretationOfResults = (string)value; break;
						case "Suggestion": this.str.Suggestion = (string)value; break;
						case "IsMammae": this.str.IsMammae = (string)value; break;
						case "PathologyAnatomyDiagnoses": this.str.PathologyAnatomyDiagnoses = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "ER": this.str.ER = (string)value; break;
						case "PR": this.str.PR = (string)value; break;
						case "Her2Neu": this.str.Her2Neu = (string)value; break;
						case "Ki67": this.str.Ki67 = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsReexamination": this.str.IsReexamination = (string)value; break;
						case "SRPaReexaminationType": this.str.SRPaReexaminationType = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DiagnosisName": this.str.DiagnosisName = (string)value; break;
						case "ClinicalData": this.str.ClinicalData = (string)value; break;
						case "ExaminationMaterial": this.str.ExaminationMaterial = (string)value; break;
						case "LocationName": this.str.LocationName = (string)value; break;
						case "PathologyNo": this.str.PathologyNo = (string)value; break;
						case "ReferralDescription": this.str.ReferralDescription = (string)value; break;
						case "ReferralAddress": this.str.ReferralAddress = (string)value; break;
						case "ServiceUnitDescription": this.str.ServiceUnitDescription = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ResultDate":

							if (value == null || value is System.DateTime)
								this.ResultDate = (System.DateTime?)value;
							break;
						case "OrderDate":

							if (value == null || value is System.DateTime)
								this.OrderDate = (System.DateTime?)value;
							break;
						case "DateOfCompletion":

							if (value == null || value is System.DateTime)
								this.DateOfCompletion = (System.DateTime?)value;
							break;
						case "IsMarried":

							if (value == null || value is System.Boolean)
								this.IsMarried = (System.Boolean?)value;
							break;
						case "NumberOfChildren":

							if (value == null || value is System.Int16)
								this.NumberOfChildren = (System.Int16?)value;
							break;
						case "IsEligibleAdequacyOfTheSpecimen":

							if (value == null || value is System.Boolean)
								this.IsEligibleAdequacyOfTheSpecimen = (System.Boolean?)value;
							break;
						case "IsTrichomonasVaginalisInfection":

							if (value == null || value is System.Boolean)
								this.IsTrichomonasVaginalisInfection = (System.Boolean?)value;
							break;
						case "IsCandidaMoniliaInfection":

							if (value == null || value is System.Boolean)
								this.IsCandidaMoniliaInfection = (System.Boolean?)value;
							break;
						case "IsCoccobacillusGardnerellaInfection":

							if (value == null || value is System.Boolean)
								this.IsCoccobacillusGardnerellaInfection = (System.Boolean?)value;
							break;
						case "IsActinomycesInfection":

							if (value == null || value is System.Boolean)
								this.IsActinomycesInfection = (System.Boolean?)value;
							break;
						case "IsHpvInfection":

							if (value == null || value is System.Boolean)
								this.IsHpvInfection = (System.Boolean?)value;
							break;
						case "IsHsv2Infection":

							if (value == null || value is System.Boolean)
								this.IsHsv2Infection = (System.Boolean?)value;
							break;
						case "IsOtherInfections":

							if (value == null || value is System.Boolean)
								this.IsOtherInfections = (System.Boolean?)value;
							break;
						case "IsInflammatoryReaction":

							if (value == null || value is System.Boolean)
								this.IsInflammatoryReaction = (System.Boolean?)value;
							break;
						case "IsRepair":

							if (value == null || value is System.Boolean)
								this.IsRepair = (System.Boolean?)value;
							break;
						case "IsRadiation":

							if (value == null || value is System.Boolean)
								this.IsRadiation = (System.Boolean?)value;
							break;
						case "IsAtrophy":

							if (value == null || value is System.Boolean)
								this.IsAtrophy = (System.Boolean?)value;
							break;
						case "IsFollicularCervicitis":

							if (value == null || value is System.Boolean)
								this.IsFollicularCervicitis = (System.Boolean?)value;
							break;
						case "IsChemotherapyEffects":

							if (value == null || value is System.Boolean)
								this.IsChemotherapyEffects = (System.Boolean?)value;
							break;
						case "IsHormonalEffects":

							if (value == null || value is System.Boolean)
								this.IsHormonalEffects = (System.Boolean?)value;
							break;
						case "IsIudEffects":

							if (value == null || value is System.Boolean)
								this.IsIudEffects = (System.Boolean?)value;
							break;
						case "IsAsc":

							if (value == null || value is System.Boolean)
								this.IsAsc = (System.Boolean?)value;
							break;
						case "IsAscUs":

							if (value == null || value is System.Boolean)
								this.IsAscUs = (System.Boolean?)value;
							break;
						case "IsAscH":

							if (value == null || value is System.Boolean)
								this.IsAscH = (System.Boolean?)value;
							break;
						case "IsSquamousIntraepithelialLesion":

							if (value == null || value is System.Boolean)
								this.IsSquamousIntraepithelialLesion = (System.Boolean?)value;
							break;
						case "IsLsil":

							if (value == null || value is System.Boolean)
								this.IsLsil = (System.Boolean?)value;
							break;
						case "IsLsilHpv":

							if (value == null || value is System.Boolean)
								this.IsLsilHpv = (System.Boolean?)value;
							break;
						case "IsLsilCin1":

							if (value == null || value is System.Boolean)
								this.IsLsilCin1 = (System.Boolean?)value;
							break;
						case "IsHsil":

							if (value == null || value is System.Boolean)
								this.IsHsil = (System.Boolean?)value;
							break;
						case "IsHsilCin2":

							if (value == null || value is System.Boolean)
								this.IsHsilCin2 = (System.Boolean?)value;
							break;
						case "IsHsilCin3":

							if (value == null || value is System.Boolean)
								this.IsHsilCin3 = (System.Boolean?)value;
							break;
						case "IsHsilCis":

							if (value == null || value is System.Boolean)
								this.IsHsilCis = (System.Boolean?)value;
							break;
						case "IsSquamousCellCarcinoma":

							if (value == null || value is System.Boolean)
								this.IsSquamousCellCarcinoma = (System.Boolean?)value;
							break;
						case "IsKeratinizing":

							if (value == null || value is System.Boolean)
								this.IsKeratinizing = (System.Boolean?)value;
							break;
						case "IsNonKeratinizing":

							if (value == null || value is System.Boolean)
								this.IsNonKeratinizing = (System.Boolean?)value;
							break;
						case "IsAgc":

							if (value == null || value is System.Boolean)
								this.IsAgc = (System.Boolean?)value;
							break;
						case "IsAtypicalNos":

							if (value == null || value is System.Boolean)
								this.IsAtypicalNos = (System.Boolean?)value;
							break;
						case "IsAtypicalFavorNeoplastic":

							if (value == null || value is System.Boolean)
								this.IsAtypicalFavorNeoplastic = (System.Boolean?)value;
							break;
						case "IsAis":

							if (value == null || value is System.Boolean)
								this.IsAis = (System.Boolean?)value;
							break;
						case "IsAdenocarcinoma":

							if (value == null || value is System.Boolean)
								this.IsAdenocarcinoma = (System.Boolean?)value;
							break;
						case "IsEndoCervical":

							if (value == null || value is System.Boolean)
								this.IsEndoCervical = (System.Boolean?)value;
							break;
						case "IsEndometrial":

							if (value == null || value is System.Boolean)
								this.IsEndometrial = (System.Boolean?)value;
							break;
						case "IsEndometrialCells":

							if (value == null || value is System.Boolean)
								this.IsEndometrialCells = (System.Boolean?)value;
							break;
						case "IsHormonalPatternsAccordingToAgeAndHistory":

							if (value == null || value is System.Boolean)
								this.IsHormonalPatternsAccordingToAgeAndHistory = (System.Boolean?)value;
							break;
						case "IsHormonalPatternDoesNotMatchTheAgeAndHistory":

							if (value == null || value is System.Boolean)
								this.IsHormonalPatternDoesNotMatchTheAgeAndHistory = (System.Boolean?)value;
							break;
						case "IsHormonalEvaluationIsNotPossible":

							if (value == null || value is System.Boolean)
								this.IsHormonalEvaluationIsNotPossible = (System.Boolean?)value;
							break;
						case "IsMammae":

							if (value == null || value is System.Boolean)
								this.IsMammae = (System.Boolean?)value;
							break;
						case "IsReexamination":

							if (value == null || value is System.Boolean)
								this.IsReexamination = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to PathologyAnatomy.ResultNo
		/// </summary>
		virtual public System.String ResultNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ResultDate
		/// </summary>
		virtual public System.DateTime? ResultDate
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.ResultDate);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.ResultDate, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ResultTime
		/// </summary>
		virtual public System.String ResultTime
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultTime);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ResultType
		/// </summary>
		virtual public System.String ResultType
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultType);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ResultType, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.OrderDate
		/// </summary>
		virtual public System.DateTime? OrderDate
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.OrderDate);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.OrderDate, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.DateOfCompletion
		/// </summary>
		virtual public System.DateTime? DateOfCompletion
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.DateOfCompletion);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.DateOfCompletion, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.PhysicianSenders
		/// </summary>
		virtual public System.String PhysicianSenders
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.PhysicianSenders);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.PhysicianSenders, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.DiagnosisID
		/// </summary>
		virtual public System.String DiagnosisID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.DiagnosisID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.DiagnosisID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.MorphologyID
		/// </summary>
		virtual public System.String MorphologyID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.MorphologyID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.MorphologyID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.SourceOfTissueID
		/// </summary>
		virtual public System.String SourceOfTissueID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.SourceOfTissueID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.SourceOfTissueID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.TissueID
		/// </summary>
		virtual public System.String TissueID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.TissueID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.TissueID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsMarried
		/// </summary>
		virtual public System.Boolean? IsMarried
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsMarried);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsMarried, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.NumberOfChildren
		/// </summary>
		virtual public System.Int16? NumberOfChildren
		{
			get
			{
				return base.GetSystemInt16(PathologyAnatomyMetadata.ColumnNames.NumberOfChildren);
			}

			set
			{
				base.SetSystemInt16(PathologyAnatomyMetadata.ColumnNames.NumberOfChildren, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Macroscopic
		/// </summary>
		virtual public System.String Macroscopic
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Macroscopic);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Macroscopic, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Microscopic
		/// </summary>
		virtual public System.String Microscopic
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Microscopic);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Microscopic, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Impression
		/// </summary>
		virtual public System.String Impression
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Impression);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Impression, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ImpressionGroupID
		/// </summary>
		virtual public System.String ImpressionGroupID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ImpressionGroupItemID
		/// </summary>
		virtual public System.String ImpressionGroupItemID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupItemID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupItemID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.AdditionalNotes
		/// </summary>
		virtual public System.String AdditionalNotes
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.AdditionalNotes);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.AdditionalNotes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsEligibleAdequacyOfTheSpecimen
		/// </summary>
		virtual public System.Boolean? IsEligibleAdequacyOfTheSpecimen
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEligibleAdequacyOfTheSpecimen);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEligibleAdequacyOfTheSpecimen, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsTrichomonasVaginalisInfection
		/// </summary>
		virtual public System.Boolean? IsTrichomonasVaginalisInfection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsTrichomonasVaginalisInfection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsTrichomonasVaginalisInfection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsCandidaMoniliaInfection
		/// </summary>
		virtual public System.Boolean? IsCandidaMoniliaInfection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsCandidaMoniliaInfection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsCandidaMoniliaInfection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsCoccobacillusGardnerellaInfection
		/// </summary>
		virtual public System.Boolean? IsCoccobacillusGardnerellaInfection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsCoccobacillusGardnerellaInfection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsCoccobacillusGardnerellaInfection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsActinomycesInfection
		/// </summary>
		virtual public System.Boolean? IsActinomycesInfection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsActinomycesInfection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsActinomycesInfection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHpvInfection
		/// </summary>
		virtual public System.Boolean? IsHpvInfection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHpvInfection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHpvInfection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHsv2Infection
		/// </summary>
		virtual public System.Boolean? IsHsv2Infection
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsv2Infection);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsv2Infection, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsOtherInfections
		/// </summary>
		virtual public System.Boolean? IsOtherInfections
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsOtherInfections);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsOtherInfections, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.OtherInfectionsDescription
		/// </summary>
		virtual public System.String OtherInfectionsDescription
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.OtherInfectionsDescription);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.OtherInfectionsDescription, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsInflammatoryReaction
		/// </summary>
		virtual public System.Boolean? IsInflammatoryReaction
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsInflammatoryReaction);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsInflammatoryReaction, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsRepair
		/// </summary>
		virtual public System.Boolean? IsRepair
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsRepair);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsRepair, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsRadiation
		/// </summary>
		virtual public System.Boolean? IsRadiation
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsRadiation);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsRadiation, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAtrophy
		/// </summary>
		virtual public System.Boolean? IsAtrophy
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtrophy);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtrophy, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.AtrophyDescription
		/// </summary>
		virtual public System.String AtrophyDescription
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.AtrophyDescription);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.AtrophyDescription, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsFollicularCervicitis
		/// </summary>
		virtual public System.Boolean? IsFollicularCervicitis
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsFollicularCervicitis);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsFollicularCervicitis, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsChemotherapyEffects
		/// </summary>
		virtual public System.Boolean? IsChemotherapyEffects
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsChemotherapyEffects);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsChemotherapyEffects, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHormonalEffects
		/// </summary>
		virtual public System.Boolean? IsHormonalEffects
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalEffects);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalEffects, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsIudEffects
		/// </summary>
		virtual public System.Boolean? IsIudEffects
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsIudEffects);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsIudEffects, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAsc
		/// </summary>
		virtual public System.Boolean? IsAsc
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAsc);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAsc, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAscUs
		/// </summary>
		virtual public System.Boolean? IsAscUs
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAscUs);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAscUs, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAscH
		/// </summary>
		virtual public System.Boolean? IsAscH
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAscH);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAscH, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsSquamousIntraepithelialLesion
		/// </summary>
		virtual public System.Boolean? IsSquamousIntraepithelialLesion
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsSquamousIntraepithelialLesion);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsSquamousIntraepithelialLesion, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsLsil
		/// </summary>
		virtual public System.Boolean? IsLsil
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsil);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsil, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsLsilHpv
		/// </summary>
		virtual public System.Boolean? IsLsilHpv
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsilHpv);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsilHpv, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsLsilCin1
		/// </summary>
		virtual public System.Boolean? IsLsilCin1
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsilCin1);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsLsilCin1, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHsil
		/// </summary>
		virtual public System.Boolean? IsHsil
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsil);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsil, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHsilCin2
		/// </summary>
		virtual public System.Boolean? IsHsilCin2
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCin2);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCin2, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHsilCin3
		/// </summary>
		virtual public System.Boolean? IsHsilCin3
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCin3);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCin3, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHsilCis
		/// </summary>
		virtual public System.Boolean? IsHsilCis
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCis);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHsilCis, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsSquamousCellCarcinoma
		/// </summary>
		virtual public System.Boolean? IsSquamousCellCarcinoma
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsSquamousCellCarcinoma);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsSquamousCellCarcinoma, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsKeratinizing
		/// </summary>
		virtual public System.Boolean? IsKeratinizing
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsKeratinizing);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsKeratinizing, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsNonKeratinizing
		/// </summary>
		virtual public System.Boolean? IsNonKeratinizing
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsNonKeratinizing);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsNonKeratinizing, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAgc
		/// </summary>
		virtual public System.Boolean? IsAgc
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAgc);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAgc, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAtypicalNos
		/// </summary>
		virtual public System.Boolean? IsAtypicalNos
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtypicalNos);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtypicalNos, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAtypicalFavorNeoplastic
		/// </summary>
		virtual public System.Boolean? IsAtypicalFavorNeoplastic
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtypicalFavorNeoplastic);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAtypicalFavorNeoplastic, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAis
		/// </summary>
		virtual public System.Boolean? IsAis
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAis);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAis, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsAdenocarcinoma
		/// </summary>
		virtual public System.Boolean? IsAdenocarcinoma
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAdenocarcinoma);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsAdenocarcinoma, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsEndoCervical
		/// </summary>
		virtual public System.Boolean? IsEndoCervical
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndoCervical);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndoCervical, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsEndometrial
		/// </summary>
		virtual public System.Boolean? IsEndometrial
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndometrial);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndometrial, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsEndometrialCells
		/// </summary>
		virtual public System.Boolean? IsEndometrialCells
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndometrialCells);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsEndometrialCells, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.NonEpithelialMalignancies
		/// </summary>
		virtual public System.String NonEpithelialMalignancies
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.NonEpithelialMalignancies);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.NonEpithelialMalignancies, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHormonalPatternsAccordingToAgeAndHistory
		/// </summary>
		virtual public System.Boolean? IsHormonalPatternsAccordingToAgeAndHistory
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternsAccordingToAgeAndHistory);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternsAccordingToAgeAndHistory, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHormonalPatternDoesNotMatchTheAgeAndHistory
		/// </summary>
		virtual public System.Boolean? IsHormonalPatternDoesNotMatchTheAgeAndHistory
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternDoesNotMatchTheAgeAndHistory);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternDoesNotMatchTheAgeAndHistory, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsHormonalEvaluationIsNotPossible
		/// </summary>
		virtual public System.Boolean? IsHormonalEvaluationIsNotPossible
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalEvaluationIsNotPossible);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsHormonalEvaluationIsNotPossible, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.InterpretationOfResults
		/// </summary>
		virtual public System.String InterpretationOfResults
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.InterpretationOfResults);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.InterpretationOfResults, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Suggestion
		/// </summary>
		virtual public System.String Suggestion
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Suggestion);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Suggestion, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsMammae
		/// </summary>
		virtual public System.Boolean? IsMammae
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsMammae);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsMammae, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.PathologyAnatomyDiagnoses
		/// </summary>
		virtual public System.String PathologyAnatomyDiagnoses
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.PathologyAnatomyDiagnoses);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.PathologyAnatomyDiagnoses, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ER
		/// </summary>
		virtual public System.String ER
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ER);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ER, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.PR
		/// </summary>
		virtual public System.String PR
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.PR);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.PR, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Her2Neu
		/// </summary>
		virtual public System.String Her2Neu
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Her2Neu);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Her2Neu, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Ki67
		/// </summary>
		virtual public System.String Ki67
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Ki67);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Ki67, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsReexamination
		/// </summary>
		virtual public System.Boolean? IsReexamination
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsReexamination);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsReexamination, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.SRPaReexaminationType
		/// </summary>
		virtual public System.String SRPaReexaminationType
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.SRPaReexaminationType);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.SRPaReexaminationType, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(PathologyAnatomyMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PathologyAnatomyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.DiagnosisName
		/// </summary>
		virtual public System.String DiagnosisName
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.DiagnosisName);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.DiagnosisName, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ClinicalData
		/// </summary>
		virtual public System.String ClinicalData
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ClinicalData);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ClinicalData, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ExaminationMaterial
		/// </summary>
		virtual public System.String ExaminationMaterial
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ExaminationMaterial);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ExaminationMaterial, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.LocationName
		/// </summary>
		virtual public System.String LocationName
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.LocationName);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.LocationName, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.PathologyNo
		/// </summary>
		virtual public System.String PathologyNo
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.PathologyNo);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.PathologyNo, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ReferralDescription
		/// </summary>
		virtual public System.String ReferralDescription
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferralDescription);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferralDescription, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ReferralAddress
		/// </summary>
		virtual public System.String ReferralAddress
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferralAddress);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ReferralAddress, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ServiceUnitDescription
		/// </summary>
		virtual public System.String ServiceUnitDescription
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ServiceUnitDescription);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ServiceUnitDescription, value);
			}
		}
		/// <summary>
		/// Maps to PathologyAnatomy.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(PathologyAnatomyMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(PathologyAnatomyMetadata.ColumnNames.ItemID, value);
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
			public esStrings(esPathologyAnatomy entity)
			{
				this.entity = entity;
			}
			public System.String ResultNo
			{
				get
				{
					System.String data = entity.ResultNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultNo = null;
					else entity.ResultNo = Convert.ToString(value);
				}
			}
			public System.String ResultDate
			{
				get
				{
					System.DateTime? data = entity.ResultDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultDate = null;
					else entity.ResultDate = Convert.ToDateTime(value);
				}
			}
			public System.String ResultTime
			{
				get
				{
					System.String data = entity.ResultTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultTime = null;
					else entity.ResultTime = Convert.ToString(value);
				}
			}
			public System.String ResultType
			{
				get
				{
					System.String data = entity.ResultType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultType = null;
					else entity.ResultType = Convert.ToString(value);
				}
			}
			public System.String OrderDate
			{
				get
				{
					System.DateTime? data = entity.OrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDate = null;
					else entity.OrderDate = Convert.ToDateTime(value);
				}
			}
			public System.String DateOfCompletion
			{
				get
				{
					System.DateTime? data = entity.DateOfCompletion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfCompletion = null;
					else entity.DateOfCompletion = Convert.ToDateTime(value);
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
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
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
			public System.String DiagnosisID
			{
				get
				{
					System.String data = entity.DiagnosisID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosisID = null;
					else entity.DiagnosisID = Convert.ToString(value);
				}
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String MorphologyID
			{
				get
				{
					System.String data = entity.MorphologyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MorphologyID = null;
					else entity.MorphologyID = Convert.ToString(value);
				}
			}
			public System.String SourceOfTissueID
			{
				get
				{
					System.String data = entity.SourceOfTissueID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SourceOfTissueID = null;
					else entity.SourceOfTissueID = Convert.ToString(value);
				}
			}
			public System.String TissueID
			{
				get
				{
					System.String data = entity.TissueID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TissueID = null;
					else entity.TissueID = Convert.ToString(value);
				}
			}
			public System.String IsMarried
			{
				get
				{
					System.Boolean? data = entity.IsMarried;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMarried = null;
					else entity.IsMarried = Convert.ToBoolean(value);
				}
			}
			public System.String NumberOfChildren
			{
				get
				{
					System.Int16? data = entity.NumberOfChildren;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfChildren = null;
					else entity.NumberOfChildren = Convert.ToInt16(value);
				}
			}
			public System.String Macroscopic
			{
				get
				{
					System.String data = entity.Macroscopic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Macroscopic = null;
					else entity.Macroscopic = Convert.ToString(value);
				}
			}
			public System.String Microscopic
			{
				get
				{
					System.String data = entity.Microscopic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Microscopic = null;
					else entity.Microscopic = Convert.ToString(value);
				}
			}
			public System.String Impression
			{
				get
				{
					System.String data = entity.Impression;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Impression = null;
					else entity.Impression = Convert.ToString(value);
				}
			}
			public System.String ImpressionGroupID
			{
				get
				{
					System.String data = entity.ImpressionGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImpressionGroupID = null;
					else entity.ImpressionGroupID = Convert.ToString(value);
				}
			}
			public System.String ImpressionGroupItemID
			{
				get
				{
					System.String data = entity.ImpressionGroupItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImpressionGroupItemID = null;
					else entity.ImpressionGroupItemID = Convert.ToString(value);
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
			public System.String IsEligibleAdequacyOfTheSpecimen
			{
				get
				{
					System.Boolean? data = entity.IsEligibleAdequacyOfTheSpecimen;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEligibleAdequacyOfTheSpecimen = null;
					else entity.IsEligibleAdequacyOfTheSpecimen = Convert.ToBoolean(value);
				}
			}
			public System.String IsTrichomonasVaginalisInfection
			{
				get
				{
					System.Boolean? data = entity.IsTrichomonasVaginalisInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTrichomonasVaginalisInfection = null;
					else entity.IsTrichomonasVaginalisInfection = Convert.ToBoolean(value);
				}
			}
			public System.String IsCandidaMoniliaInfection
			{
				get
				{
					System.Boolean? data = entity.IsCandidaMoniliaInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCandidaMoniliaInfection = null;
					else entity.IsCandidaMoniliaInfection = Convert.ToBoolean(value);
				}
			}
			public System.String IsCoccobacillusGardnerellaInfection
			{
				get
				{
					System.Boolean? data = entity.IsCoccobacillusGardnerellaInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCoccobacillusGardnerellaInfection = null;
					else entity.IsCoccobacillusGardnerellaInfection = Convert.ToBoolean(value);
				}
			}
			public System.String IsActinomycesInfection
			{
				get
				{
					System.Boolean? data = entity.IsActinomycesInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActinomycesInfection = null;
					else entity.IsActinomycesInfection = Convert.ToBoolean(value);
				}
			}
			public System.String IsHpvInfection
			{
				get
				{
					System.Boolean? data = entity.IsHpvInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHpvInfection = null;
					else entity.IsHpvInfection = Convert.ToBoolean(value);
				}
			}
			public System.String IsHsv2Infection
			{
				get
				{
					System.Boolean? data = entity.IsHsv2Infection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHsv2Infection = null;
					else entity.IsHsv2Infection = Convert.ToBoolean(value);
				}
			}
			public System.String IsOtherInfections
			{
				get
				{
					System.Boolean? data = entity.IsOtherInfections;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOtherInfections = null;
					else entity.IsOtherInfections = Convert.ToBoolean(value);
				}
			}
			public System.String OtherInfectionsDescription
			{
				get
				{
					System.String data = entity.OtherInfectionsDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherInfectionsDescription = null;
					else entity.OtherInfectionsDescription = Convert.ToString(value);
				}
			}
			public System.String IsInflammatoryReaction
			{
				get
				{
					System.Boolean? data = entity.IsInflammatoryReaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInflammatoryReaction = null;
					else entity.IsInflammatoryReaction = Convert.ToBoolean(value);
				}
			}
			public System.String IsRepair
			{
				get
				{
					System.Boolean? data = entity.IsRepair;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRepair = null;
					else entity.IsRepair = Convert.ToBoolean(value);
				}
			}
			public System.String IsRadiation
			{
				get
				{
					System.Boolean? data = entity.IsRadiation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRadiation = null;
					else entity.IsRadiation = Convert.ToBoolean(value);
				}
			}
			public System.String IsAtrophy
			{
				get
				{
					System.Boolean? data = entity.IsAtrophy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAtrophy = null;
					else entity.IsAtrophy = Convert.ToBoolean(value);
				}
			}
			public System.String AtrophyDescription
			{
				get
				{
					System.String data = entity.AtrophyDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AtrophyDescription = null;
					else entity.AtrophyDescription = Convert.ToString(value);
				}
			}
			public System.String IsFollicularCervicitis
			{
				get
				{
					System.Boolean? data = entity.IsFollicularCervicitis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFollicularCervicitis = null;
					else entity.IsFollicularCervicitis = Convert.ToBoolean(value);
				}
			}
			public System.String IsChemotherapyEffects
			{
				get
				{
					System.Boolean? data = entity.IsChemotherapyEffects;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChemotherapyEffects = null;
					else entity.IsChemotherapyEffects = Convert.ToBoolean(value);
				}
			}
			public System.String IsHormonalEffects
			{
				get
				{
					System.Boolean? data = entity.IsHormonalEffects;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHormonalEffects = null;
					else entity.IsHormonalEffects = Convert.ToBoolean(value);
				}
			}
			public System.String IsIudEffects
			{
				get
				{
					System.Boolean? data = entity.IsIudEffects;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIudEffects = null;
					else entity.IsIudEffects = Convert.ToBoolean(value);
				}
			}
			public System.String IsAsc
			{
				get
				{
					System.Boolean? data = entity.IsAsc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAsc = null;
					else entity.IsAsc = Convert.ToBoolean(value);
				}
			}
			public System.String IsAscUs
			{
				get
				{
					System.Boolean? data = entity.IsAscUs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAscUs = null;
					else entity.IsAscUs = Convert.ToBoolean(value);
				}
			}
			public System.String IsAscH
			{
				get
				{
					System.Boolean? data = entity.IsAscH;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAscH = null;
					else entity.IsAscH = Convert.ToBoolean(value);
				}
			}
			public System.String IsSquamousIntraepithelialLesion
			{
				get
				{
					System.Boolean? data = entity.IsSquamousIntraepithelialLesion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSquamousIntraepithelialLesion = null;
					else entity.IsSquamousIntraepithelialLesion = Convert.ToBoolean(value);
				}
			}
			public System.String IsLsil
			{
				get
				{
					System.Boolean? data = entity.IsLsil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLsil = null;
					else entity.IsLsil = Convert.ToBoolean(value);
				}
			}
			public System.String IsLsilHpv
			{
				get
				{
					System.Boolean? data = entity.IsLsilHpv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLsilHpv = null;
					else entity.IsLsilHpv = Convert.ToBoolean(value);
				}
			}
			public System.String IsLsilCin1
			{
				get
				{
					System.Boolean? data = entity.IsLsilCin1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLsilCin1 = null;
					else entity.IsLsilCin1 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHsil
			{
				get
				{
					System.Boolean? data = entity.IsHsil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHsil = null;
					else entity.IsHsil = Convert.ToBoolean(value);
				}
			}
			public System.String IsHsilCin2
			{
				get
				{
					System.Boolean? data = entity.IsHsilCin2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHsilCin2 = null;
					else entity.IsHsilCin2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHsilCin3
			{
				get
				{
					System.Boolean? data = entity.IsHsilCin3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHsilCin3 = null;
					else entity.IsHsilCin3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHsilCis
			{
				get
				{
					System.Boolean? data = entity.IsHsilCis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHsilCis = null;
					else entity.IsHsilCis = Convert.ToBoolean(value);
				}
			}
			public System.String IsSquamousCellCarcinoma
			{
				get
				{
					System.Boolean? data = entity.IsSquamousCellCarcinoma;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSquamousCellCarcinoma = null;
					else entity.IsSquamousCellCarcinoma = Convert.ToBoolean(value);
				}
			}
			public System.String IsKeratinizing
			{
				get
				{
					System.Boolean? data = entity.IsKeratinizing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKeratinizing = null;
					else entity.IsKeratinizing = Convert.ToBoolean(value);
				}
			}
			public System.String IsNonKeratinizing
			{
				get
				{
					System.Boolean? data = entity.IsNonKeratinizing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonKeratinizing = null;
					else entity.IsNonKeratinizing = Convert.ToBoolean(value);
				}
			}
			public System.String IsAgc
			{
				get
				{
					System.Boolean? data = entity.IsAgc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAgc = null;
					else entity.IsAgc = Convert.ToBoolean(value);
				}
			}
			public System.String IsAtypicalNos
			{
				get
				{
					System.Boolean? data = entity.IsAtypicalNos;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAtypicalNos = null;
					else entity.IsAtypicalNos = Convert.ToBoolean(value);
				}
			}
			public System.String IsAtypicalFavorNeoplastic
			{
				get
				{
					System.Boolean? data = entity.IsAtypicalFavorNeoplastic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAtypicalFavorNeoplastic = null;
					else entity.IsAtypicalFavorNeoplastic = Convert.ToBoolean(value);
				}
			}
			public System.String IsAis
			{
				get
				{
					System.Boolean? data = entity.IsAis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAis = null;
					else entity.IsAis = Convert.ToBoolean(value);
				}
			}
			public System.String IsAdenocarcinoma
			{
				get
				{
					System.Boolean? data = entity.IsAdenocarcinoma;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdenocarcinoma = null;
					else entity.IsAdenocarcinoma = Convert.ToBoolean(value);
				}
			}
			public System.String IsEndoCervical
			{
				get
				{
					System.Boolean? data = entity.IsEndoCervical;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEndoCervical = null;
					else entity.IsEndoCervical = Convert.ToBoolean(value);
				}
			}
			public System.String IsEndometrial
			{
				get
				{
					System.Boolean? data = entity.IsEndometrial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEndometrial = null;
					else entity.IsEndometrial = Convert.ToBoolean(value);
				}
			}
			public System.String IsEndometrialCells
			{
				get
				{
					System.Boolean? data = entity.IsEndometrialCells;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEndometrialCells = null;
					else entity.IsEndometrialCells = Convert.ToBoolean(value);
				}
			}
			public System.String NonEpithelialMalignancies
			{
				get
				{
					System.String data = entity.NonEpithelialMalignancies;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NonEpithelialMalignancies = null;
					else entity.NonEpithelialMalignancies = Convert.ToString(value);
				}
			}
			public System.String IsHormonalPatternsAccordingToAgeAndHistory
			{
				get
				{
					System.Boolean? data = entity.IsHormonalPatternsAccordingToAgeAndHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHormonalPatternsAccordingToAgeAndHistory = null;
					else entity.IsHormonalPatternsAccordingToAgeAndHistory = Convert.ToBoolean(value);
				}
			}
			public System.String IsHormonalPatternDoesNotMatchTheAgeAndHistory
			{
				get
				{
					System.Boolean? data = entity.IsHormonalPatternDoesNotMatchTheAgeAndHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHormonalPatternDoesNotMatchTheAgeAndHistory = null;
					else entity.IsHormonalPatternDoesNotMatchTheAgeAndHistory = Convert.ToBoolean(value);
				}
			}
			public System.String IsHormonalEvaluationIsNotPossible
			{
				get
				{
					System.Boolean? data = entity.IsHormonalEvaluationIsNotPossible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHormonalEvaluationIsNotPossible = null;
					else entity.IsHormonalEvaluationIsNotPossible = Convert.ToBoolean(value);
				}
			}
			public System.String InterpretationOfResults
			{
				get
				{
					System.String data = entity.InterpretationOfResults;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InterpretationOfResults = null;
					else entity.InterpretationOfResults = Convert.ToString(value);
				}
			}
			public System.String Suggestion
			{
				get
				{
					System.String data = entity.Suggestion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suggestion = null;
					else entity.Suggestion = Convert.ToString(value);
				}
			}
			public System.String IsMammae
			{
				get
				{
					System.Boolean? data = entity.IsMammae;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMammae = null;
					else entity.IsMammae = Convert.ToBoolean(value);
				}
			}
			public System.String PathologyAnatomyDiagnoses
			{
				get
				{
					System.String data = entity.PathologyAnatomyDiagnoses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathologyAnatomyDiagnoses = null;
					else entity.PathologyAnatomyDiagnoses = Convert.ToString(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
			public System.String ER
			{
				get
				{
					System.String data = entity.ER;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ER = null;
					else entity.ER = Convert.ToString(value);
				}
			}
			public System.String PR
			{
				get
				{
					System.String data = entity.PR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PR = null;
					else entity.PR = Convert.ToString(value);
				}
			}
			public System.String Her2Neu
			{
				get
				{
					System.String data = entity.Her2Neu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Her2Neu = null;
					else entity.Her2Neu = Convert.ToString(value);
				}
			}
			public System.String Ki67
			{
				get
				{
					System.String data = entity.Ki67;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ki67 = null;
					else entity.Ki67 = Convert.ToString(value);
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
			public System.String IsReexamination
			{
				get
				{
					System.Boolean? data = entity.IsReexamination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReexamination = null;
					else entity.IsReexamination = Convert.ToBoolean(value);
				}
			}
			public System.String SRPaReexaminationType
			{
				get
				{
					System.String data = entity.SRPaReexaminationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaReexaminationType = null;
					else entity.SRPaReexaminationType = Convert.ToString(value);
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
			public System.String DiagnosisName
			{
				get
				{
					System.String data = entity.DiagnosisName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosisName = null;
					else entity.DiagnosisName = Convert.ToString(value);
				}
			}
			public System.String ClinicalData
			{
				get
				{
					System.String data = entity.ClinicalData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalData = null;
					else entity.ClinicalData = Convert.ToString(value);
				}
			}
			public System.String ExaminationMaterial
			{
				get
				{
					System.String data = entity.ExaminationMaterial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExaminationMaterial = null;
					else entity.ExaminationMaterial = Convert.ToString(value);
				}
			}
			public System.String LocationName
			{
				get
				{
					System.String data = entity.LocationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationName = null;
					else entity.LocationName = Convert.ToString(value);
				}
			}
			public System.String PathologyNo
			{
				get
				{
					System.String data = entity.PathologyNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathologyNo = null;
					else entity.PathologyNo = Convert.ToString(value);
				}
			}
			public System.String ReferralDescription
			{
				get
				{
					System.String data = entity.ReferralDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralDescription = null;
					else entity.ReferralDescription = Convert.ToString(value);
				}
			}
			public System.String ReferralAddress
			{
				get
				{
					System.String data = entity.ReferralAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralAddress = null;
					else entity.ReferralAddress = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitDescription
			{
				get
				{
					System.String data = entity.ServiceUnitDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitDescription = null;
					else entity.ServiceUnitDescription = Convert.ToString(value);
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
			private esPathologyAnatomy entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPathologyAnatomyQuery query)
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
				throw new Exception("esPathologyAnatomy can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PathologyAnatomy : esPathologyAnatomy
	{
	}

	[Serializable]
	abstract public class esPathologyAnatomyQuery : esDynamicQuery
	{		

		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyMetadata.Meta();
			}
		}

		public esQueryItem ResultNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ResultNo, esSystemType.String);
			}
		}

		public esQueryItem ResultDate
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ResultDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ResultTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ResultTime, esSystemType.String);
			}
		}

		public esQueryItem ResultType
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ResultType, esSystemType.String);
			}
		}

		public esQueryItem OrderDate
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DateOfCompletion
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.DateOfCompletion, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem PhysicianSenders
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.PhysicianSenders, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem DiagnosisID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.DiagnosisID, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem MorphologyID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.MorphologyID, esSystemType.String);
			}
		}

		public esQueryItem SourceOfTissueID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.SourceOfTissueID, esSystemType.String);
			}
		}

		public esQueryItem TissueID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.TissueID, esSystemType.String);
			}
		}

		public esQueryItem IsMarried
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsMarried, esSystemType.Boolean);
			}
		}

		public esQueryItem NumberOfChildren
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.NumberOfChildren, esSystemType.Int16);
			}
		}

		public esQueryItem Macroscopic
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Macroscopic, esSystemType.String);
			}
		}

		public esQueryItem Microscopic
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Microscopic, esSystemType.String);
			}
		}

		public esQueryItem Impression
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Impression, esSystemType.String);
			}
		}

		public esQueryItem ImpressionGroupID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ImpressionGroupID, esSystemType.String);
			}
		}

		public esQueryItem ImpressionGroupItemID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ImpressionGroupItemID, esSystemType.String);
			}
		}

		public esQueryItem AdditionalNotes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.AdditionalNotes, esSystemType.String);
			}
		}

		public esQueryItem IsEligibleAdequacyOfTheSpecimen
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsEligibleAdequacyOfTheSpecimen, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTrichomonasVaginalisInfection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsTrichomonasVaginalisInfection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCandidaMoniliaInfection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsCandidaMoniliaInfection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCoccobacillusGardnerellaInfection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsCoccobacillusGardnerellaInfection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActinomycesInfection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsActinomycesInfection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHpvInfection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHpvInfection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHsv2Infection
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHsv2Infection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOtherInfections
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsOtherInfections, esSystemType.Boolean);
			}
		}

		public esQueryItem OtherInfectionsDescription
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.OtherInfectionsDescription, esSystemType.String);
			}
		}

		public esQueryItem IsInflammatoryReaction
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsInflammatoryReaction, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRepair
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsRepair, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRadiation
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsRadiation, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAtrophy
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAtrophy, esSystemType.Boolean);
			}
		}

		public esQueryItem AtrophyDescription
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.AtrophyDescription, esSystemType.String);
			}
		}

		public esQueryItem IsFollicularCervicitis
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsFollicularCervicitis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsChemotherapyEffects
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsChemotherapyEffects, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHormonalEffects
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHormonalEffects, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIudEffects
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsIudEffects, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAsc
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAsc, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAscUs
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAscUs, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAscH
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAscH, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSquamousIntraepithelialLesion
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsSquamousIntraepithelialLesion, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLsil
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsLsil, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLsilHpv
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsLsilHpv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLsilCin1
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsLsilCin1, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHsil
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHsil, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHsilCin2
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHsilCin2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHsilCin3
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHsilCin3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHsilCis
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHsilCis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSquamousCellCarcinoma
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsSquamousCellCarcinoma, esSystemType.Boolean);
			}
		}

		public esQueryItem IsKeratinizing
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsKeratinizing, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNonKeratinizing
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsNonKeratinizing, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAgc
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAgc, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAtypicalNos
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAtypicalNos, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAtypicalFavorNeoplastic
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAtypicalFavorNeoplastic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAis
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAdenocarcinoma
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsAdenocarcinoma, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEndoCervical
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsEndoCervical, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEndometrial
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsEndometrial, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEndometrialCells
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsEndometrialCells, esSystemType.Boolean);
			}
		}

		public esQueryItem NonEpithelialMalignancies
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.NonEpithelialMalignancies, esSystemType.String);
			}
		}

		public esQueryItem IsHormonalPatternsAccordingToAgeAndHistory
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternsAccordingToAgeAndHistory, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHormonalPatternDoesNotMatchTheAgeAndHistory
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternDoesNotMatchTheAgeAndHistory, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHormonalEvaluationIsNotPossible
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsHormonalEvaluationIsNotPossible, esSystemType.Boolean);
			}
		}

		public esQueryItem InterpretationOfResults
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.InterpretationOfResults, esSystemType.String);
			}
		}

		public esQueryItem Suggestion
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Suggestion, esSystemType.String);
			}
		}

		public esQueryItem IsMammae
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsMammae, esSystemType.Boolean);
			}
		}

		public esQueryItem PathologyAnatomyDiagnoses
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.PathologyAnatomyDiagnoses, esSystemType.String);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem ER
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ER, esSystemType.String);
			}
		}

		public esQueryItem PR
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.PR, esSystemType.String);
			}
		}

		public esQueryItem Her2Neu
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Her2Neu, esSystemType.String);
			}
		}

		public esQueryItem Ki67
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Ki67, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsReexamination
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsReexamination, esSystemType.Boolean);
			}
		}

		public esQueryItem SRPaReexaminationType
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.SRPaReexaminationType, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DiagnosisName
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.DiagnosisName, esSystemType.String);
			}
		}

		public esQueryItem ClinicalData
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ClinicalData, esSystemType.String);
			}
		}

		public esQueryItem ExaminationMaterial
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ExaminationMaterial, esSystemType.String);
			}
		}

		public esQueryItem LocationName
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.LocationName, esSystemType.String);
			}
		}

		public esQueryItem PathologyNo
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.PathologyNo, esSystemType.String);
			}
		}

		public esQueryItem ReferralDescription
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ReferralDescription, esSystemType.String);
			}
		}

		public esQueryItem ReferralAddress
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ReferralAddress, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitDescription
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ServiceUnitDescription, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, PathologyAnatomyMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PathologyAnatomyCollection")]
	public partial class PathologyAnatomyCollection : esPathologyAnatomyCollection, IEnumerable<PathologyAnatomy>
	{
		public PathologyAnatomyCollection()
		{

		}

		public static implicit operator List<PathologyAnatomy>(PathologyAnatomyCollection coll)
		{
			List<PathologyAnatomy> list = new List<PathologyAnatomy>();

			foreach (PathologyAnatomy emp in coll)
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
				return PathologyAnatomyMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PathologyAnatomy(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PathologyAnatomy();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PathologyAnatomyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyQuery();
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
		public bool Load(PathologyAnatomyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PathologyAnatomy AddNew()
		{
			PathologyAnatomy entity = base.AddNewEntity() as PathologyAnatomy;

			return entity;
		}
		public PathologyAnatomy FindByPrimaryKey(String resultNo)
		{
			return base.FindByPrimaryKey(resultNo) as PathologyAnatomy;
		}

		#region IEnumerable< PathologyAnatomy> Members

		IEnumerator<PathologyAnatomy> IEnumerable<PathologyAnatomy>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PathologyAnatomy;
			}
		}

		#endregion

		private PathologyAnatomyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PathologyAnatomy' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PathologyAnatomy ({ResultNo})")]
	[Serializable]
	public partial class PathologyAnatomy : esPathologyAnatomy
	{
		public PathologyAnatomy()
		{
		}

		public PathologyAnatomy(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PathologyAnatomyMetadata.Meta();
			}
		}

		override protected esPathologyAnatomyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PathologyAnatomyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PathologyAnatomyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PathologyAnatomyQuery();
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
		public bool Load(PathologyAnatomyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PathologyAnatomyQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PathologyAnatomyQuery : esPathologyAnatomyQuery
	{
		public PathologyAnatomyQuery()
		{

		}

		public PathologyAnatomyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PathologyAnatomyQuery";
		}
	}

	[Serializable]
	public partial class PathologyAnatomyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PathologyAnatomyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ResultNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ResultNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ResultDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ResultDate;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ResultTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ResultTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ResultType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ResultType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.OrderDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.OrderDate;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.DateOfCompletion, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.DateOfCompletion;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.RegistrationNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.TransactionNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.PhysicianSenders, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.PhysicianSenders;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.PhoneNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ParamedicID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.DiagnosisID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.DiagnosisID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.LocationID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.MorphologyID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.MorphologyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.SourceOfTissueID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.SourceOfTissueID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.TissueID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.TissueID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsMarried, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsMarried;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.NumberOfChildren, 17, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.NumberOfChildren;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Macroscopic, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Macroscopic;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Microscopic, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Microscopic;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Impression, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Impression;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ImpressionGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ImpressionGroupItemID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ImpressionGroupItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.AdditionalNotes, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.AdditionalNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsEligibleAdequacyOfTheSpecimen, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsEligibleAdequacyOfTheSpecimen;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsTrichomonasVaginalisInfection, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsTrichomonasVaginalisInfection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsCandidaMoniliaInfection, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsCandidaMoniliaInfection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsCoccobacillusGardnerellaInfection, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsCoccobacillusGardnerellaInfection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsActinomycesInfection, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsActinomycesInfection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHpvInfection, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHpvInfection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHsv2Infection, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHsv2Infection;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsOtherInfections, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsOtherInfections;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.OtherInfectionsDescription, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.OtherInfectionsDescription;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsInflammatoryReaction, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsInflammatoryReaction;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsRepair, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsRepair;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsRadiation, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsRadiation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAtrophy, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAtrophy;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.AtrophyDescription, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.AtrophyDescription;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsFollicularCervicitis, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsFollicularCervicitis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsChemotherapyEffects, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsChemotherapyEffects;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHormonalEffects, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHormonalEffects;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsIudEffects, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsIudEffects;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAsc, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAsc;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAscUs, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAscUs;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAscH, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAscH;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsSquamousIntraepithelialLesion, 45, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsSquamousIntraepithelialLesion;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsLsil, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsLsil;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsLsilHpv, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsLsilHpv;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsLsilCin1, 48, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsLsilCin1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHsil, 49, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHsil;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHsilCin2, 50, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHsilCin2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHsilCin3, 51, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHsilCin3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHsilCis, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHsilCis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsSquamousCellCarcinoma, 53, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsSquamousCellCarcinoma;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsKeratinizing, 54, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsKeratinizing;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsNonKeratinizing, 55, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsNonKeratinizing;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAgc, 56, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAgc;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAtypicalNos, 57, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAtypicalNos;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAtypicalFavorNeoplastic, 58, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAtypicalFavorNeoplastic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAis, 59, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsAdenocarcinoma, 60, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsAdenocarcinoma;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsEndoCervical, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsEndoCervical;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsEndometrial, 62, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsEndometrial;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsEndometrialCells, 63, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsEndometrialCells;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.NonEpithelialMalignancies, 64, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.NonEpithelialMalignancies;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternsAccordingToAgeAndHistory, 65, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHormonalPatternsAccordingToAgeAndHistory;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHormonalPatternDoesNotMatchTheAgeAndHistory, 66, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHormonalPatternDoesNotMatchTheAgeAndHistory;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsHormonalEvaluationIsNotPossible, 67, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsHormonalEvaluationIsNotPossible;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.InterpretationOfResults, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.InterpretationOfResults;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Suggestion, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Suggestion;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsMammae, 70, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsMammae;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.PathologyAnatomyDiagnoses, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.PathologyAnatomyDiagnoses;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Result, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ER, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ER;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.PR, 74, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.PR;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Her2Neu, 75, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Her2Neu;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Ki67, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Ki67;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.Notes, 77, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsReexamination, 78, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsReexamination;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.SRPaReexaminationType, 79, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.SRPaReexaminationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ReferenceNo, 80, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsApproved, 81, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.IsVoid, 82, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.CreatedDateTime, 83, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.CreatedByUserID, 84, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.LastUpdateDateTime, 85, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.LastUpdateByUserID, 86, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.DiagnosisName, 87, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.DiagnosisName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ClinicalData, 88, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ClinicalData;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ExaminationMaterial, 89, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ExaminationMaterial;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.LocationName, 90, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.LocationName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.PathologyNo, 91, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.PathologyNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ReferralDescription, 92, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ReferralDescription;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ReferralAddress, 93, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ReferralAddress;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ServiceUnitDescription, 94, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ServiceUnitDescription;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PathologyAnatomyMetadata.ColumnNames.ItemID, 95, typeof(System.String), esSystemType.String);
			c.PropertyName = PathologyAnatomyMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PathologyAnatomyMetadata Meta()
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
			public const string ResultNo = "ResultNo";
			public const string ResultDate = "ResultDate";
			public const string ResultTime = "ResultTime";
			public const string ResultType = "ResultType";
			public const string OrderDate = "OrderDate";
			public const string DateOfCompletion = "DateOfCompletion";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string PhoneNo = "PhoneNo";
			public const string ParamedicID = "ParamedicID";
			public const string DiagnosisID = "DiagnosisID";
			public const string LocationID = "LocationID";
			public const string MorphologyID = "MorphologyID";
			public const string SourceOfTissueID = "SourceOfTissueID";
			public const string TissueID = "TissueID";
			public const string IsMarried = "IsMarried";
			public const string NumberOfChildren = "NumberOfChildren";
			public const string Macroscopic = "Macroscopic";
			public const string Microscopic = "Microscopic";
			public const string Impression = "Impression";
			public const string ImpressionGroupID = "ImpressionGroupID";
			public const string ImpressionGroupItemID = "ImpressionGroupItemID";
			public const string AdditionalNotes = "AdditionalNotes";
			public const string IsEligibleAdequacyOfTheSpecimen = "IsEligibleAdequacyOfTheSpecimen";
			public const string IsTrichomonasVaginalisInfection = "IsTrichomonasVaginalisInfection";
			public const string IsCandidaMoniliaInfection = "IsCandidaMoniliaInfection";
			public const string IsCoccobacillusGardnerellaInfection = "IsCoccobacillusGardnerellaInfection";
			public const string IsActinomycesInfection = "IsActinomycesInfection";
			public const string IsHpvInfection = "IsHpvInfection";
			public const string IsHsv2Infection = "IsHsv2Infection";
			public const string IsOtherInfections = "IsOtherInfections";
			public const string OtherInfectionsDescription = "OtherInfectionsDescription";
			public const string IsInflammatoryReaction = "IsInflammatoryReaction";
			public const string IsRepair = "IsRepair";
			public const string IsRadiation = "IsRadiation";
			public const string IsAtrophy = "IsAtrophy";
			public const string AtrophyDescription = "AtrophyDescription";
			public const string IsFollicularCervicitis = "IsFollicularCervicitis";
			public const string IsChemotherapyEffects = "IsChemotherapyEffects";
			public const string IsHormonalEffects = "IsHormonalEffects";
			public const string IsIudEffects = "IsIudEffects";
			public const string IsAsc = "IsAsc";
			public const string IsAscUs = "IsAscUs";
			public const string IsAscH = "IsAscH";
			public const string IsSquamousIntraepithelialLesion = "IsSquamousIntraepithelialLesion";
			public const string IsLsil = "IsLsil";
			public const string IsLsilHpv = "IsLsilHpv";
			public const string IsLsilCin1 = "IsLsilCin1";
			public const string IsHsil = "IsHsil";
			public const string IsHsilCin2 = "IsHsilCin2";
			public const string IsHsilCin3 = "IsHsilCin3";
			public const string IsHsilCis = "IsHsilCis";
			public const string IsSquamousCellCarcinoma = "IsSquamousCellCarcinoma";
			public const string IsKeratinizing = "IsKeratinizing";
			public const string IsNonKeratinizing = "IsNonKeratinizing";
			public const string IsAgc = "IsAgc";
			public const string IsAtypicalNos = "IsAtypicalNos";
			public const string IsAtypicalFavorNeoplastic = "IsAtypicalFavorNeoplastic";
			public const string IsAis = "IsAis";
			public const string IsAdenocarcinoma = "IsAdenocarcinoma";
			public const string IsEndoCervical = "IsEndoCervical";
			public const string IsEndometrial = "IsEndometrial";
			public const string IsEndometrialCells = "IsEndometrialCells";
			public const string NonEpithelialMalignancies = "NonEpithelialMalignancies";
			public const string IsHormonalPatternsAccordingToAgeAndHistory = "IsHormonalPatternsAccordingToAgeAndHistory";
			public const string IsHormonalPatternDoesNotMatchTheAgeAndHistory = "IsHormonalPatternDoesNotMatchTheAgeAndHistory";
			public const string IsHormonalEvaluationIsNotPossible = "IsHormonalEvaluationIsNotPossible";
			public const string InterpretationOfResults = "InterpretationOfResults";
			public const string Suggestion = "Suggestion";
			public const string IsMammae = "IsMammae";
			public const string PathologyAnatomyDiagnoses = "PathologyAnatomyDiagnoses";
			public const string Result = "Result";
			public const string ER = "ER";
			public const string PR = "PR";
			public const string Her2Neu = "Her2Neu";
			public const string Ki67 = "Ki67";
			public const string Notes = "Notes";
			public const string IsReexamination = "IsReexamination";
			public const string SRPaReexaminationType = "SRPaReexaminationType";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DiagnosisName = "DiagnosisName";
			public const string ClinicalData = "ClinicalData";
			public const string ExaminationMaterial = "ExaminationMaterial";
			public const string LocationName = "LocationName";
			public const string PathologyNo = "PathologyNo";
			public const string ReferralDescription = "ReferralDescription";
			public const string ReferralAddress = "ReferralAddress";
			public const string ServiceUnitDescription = "ServiceUnitDescription";
			public const string ItemID = "ItemID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ResultNo = "ResultNo";
			public const string ResultDate = "ResultDate";
			public const string ResultTime = "ResultTime";
			public const string ResultType = "ResultType";
			public const string OrderDate = "OrderDate";
			public const string DateOfCompletion = "DateOfCompletion";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string PhoneNo = "PhoneNo";
			public const string ParamedicID = "ParamedicID";
			public const string DiagnosisID = "DiagnosisID";
			public const string LocationID = "LocationID";
			public const string MorphologyID = "MorphologyID";
			public const string SourceOfTissueID = "SourceOfTissueID";
			public const string TissueID = "TissueID";
			public const string IsMarried = "IsMarried";
			public const string NumberOfChildren = "NumberOfChildren";
			public const string Macroscopic = "Macroscopic";
			public const string Microscopic = "Microscopic";
			public const string Impression = "Impression";
			public const string ImpressionGroupID = "ImpressionGroupID";
			public const string ImpressionGroupItemID = "ImpressionGroupItemID";
			public const string AdditionalNotes = "AdditionalNotes";
			public const string IsEligibleAdequacyOfTheSpecimen = "IsEligibleAdequacyOfTheSpecimen";
			public const string IsTrichomonasVaginalisInfection = "IsTrichomonasVaginalisInfection";
			public const string IsCandidaMoniliaInfection = "IsCandidaMoniliaInfection";
			public const string IsCoccobacillusGardnerellaInfection = "IsCoccobacillusGardnerellaInfection";
			public const string IsActinomycesInfection = "IsActinomycesInfection";
			public const string IsHpvInfection = "IsHpvInfection";
			public const string IsHsv2Infection = "IsHsv2Infection";
			public const string IsOtherInfections = "IsOtherInfections";
			public const string OtherInfectionsDescription = "OtherInfectionsDescription";
			public const string IsInflammatoryReaction = "IsInflammatoryReaction";
			public const string IsRepair = "IsRepair";
			public const string IsRadiation = "IsRadiation";
			public const string IsAtrophy = "IsAtrophy";
			public const string AtrophyDescription = "AtrophyDescription";
			public const string IsFollicularCervicitis = "IsFollicularCervicitis";
			public const string IsChemotherapyEffects = "IsChemotherapyEffects";
			public const string IsHormonalEffects = "IsHormonalEffects";
			public const string IsIudEffects = "IsIudEffects";
			public const string IsAsc = "IsAsc";
			public const string IsAscUs = "IsAscUs";
			public const string IsAscH = "IsAscH";
			public const string IsSquamousIntraepithelialLesion = "IsSquamousIntraepithelialLesion";
			public const string IsLsil = "IsLsil";
			public const string IsLsilHpv = "IsLsilHpv";
			public const string IsLsilCin1 = "IsLsilCin1";
			public const string IsHsil = "IsHsil";
			public const string IsHsilCin2 = "IsHsilCin2";
			public const string IsHsilCin3 = "IsHsilCin3";
			public const string IsHsilCis = "IsHsilCis";
			public const string IsSquamousCellCarcinoma = "IsSquamousCellCarcinoma";
			public const string IsKeratinizing = "IsKeratinizing";
			public const string IsNonKeratinizing = "IsNonKeratinizing";
			public const string IsAgc = "IsAgc";
			public const string IsAtypicalNos = "IsAtypicalNos";
			public const string IsAtypicalFavorNeoplastic = "IsAtypicalFavorNeoplastic";
			public const string IsAis = "IsAis";
			public const string IsAdenocarcinoma = "IsAdenocarcinoma";
			public const string IsEndoCervical = "IsEndoCervical";
			public const string IsEndometrial = "IsEndometrial";
			public const string IsEndometrialCells = "IsEndometrialCells";
			public const string NonEpithelialMalignancies = "NonEpithelialMalignancies";
			public const string IsHormonalPatternsAccordingToAgeAndHistory = "IsHormonalPatternsAccordingToAgeAndHistory";
			public const string IsHormonalPatternDoesNotMatchTheAgeAndHistory = "IsHormonalPatternDoesNotMatchTheAgeAndHistory";
			public const string IsHormonalEvaluationIsNotPossible = "IsHormonalEvaluationIsNotPossible";
			public const string InterpretationOfResults = "InterpretationOfResults";
			public const string Suggestion = "Suggestion";
			public const string IsMammae = "IsMammae";
			public const string PathologyAnatomyDiagnoses = "PathologyAnatomyDiagnoses";
			public const string Result = "Result";
			public const string ER = "ER";
			public const string PR = "PR";
			public const string Her2Neu = "Her2Neu";
			public const string Ki67 = "Ki67";
			public const string Notes = "Notes";
			public const string IsReexamination = "IsReexamination";
			public const string SRPaReexaminationType = "SRPaReexaminationType";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DiagnosisName = "DiagnosisName";
			public const string ClinicalData = "ClinicalData";
			public const string ExaminationMaterial = "ExaminationMaterial";
			public const string LocationName = "LocationName";
			public const string PathologyNo = "PathologyNo";
			public const string ReferralDescription = "ReferralDescription";
			public const string ReferralAddress = "ReferralAddress";
			public const string ServiceUnitDescription = "ServiceUnitDescription";
			public const string ItemID = "ItemID";
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
			lock (typeof(PathologyAnatomyMetadata))
			{
				if (PathologyAnatomyMetadata.mapDelegates == null)
				{
					PathologyAnatomyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PathologyAnatomyMetadata.meta == null)
				{
					PathologyAnatomyMetadata.meta = new PathologyAnatomyMetadata();
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

				meta.AddTypeMap("ResultNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ResultTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DateOfCompletion", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianSenders", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosisID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MorphologyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SourceOfTissueID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TissueID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMarried", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NumberOfChildren", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Macroscopic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Microscopic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Impression", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImpressionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImpressionGroupItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEligibleAdequacyOfTheSpecimen", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTrichomonasVaginalisInfection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCandidaMoniliaInfection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCoccobacillusGardnerellaInfection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActinomycesInfection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHpvInfection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHsv2Infection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOtherInfections", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OtherInfectionsDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInflammatoryReaction", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRepair", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRadiation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAtrophy", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AtrophyDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFollicularCervicitis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsChemotherapyEffects", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHormonalEffects", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIudEffects", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAsc", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAscUs", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAscH", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSquamousIntraepithelialLesion", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLsil", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLsilHpv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLsilCin1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHsil", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHsilCin2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHsilCin3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHsilCis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSquamousCellCarcinoma", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsKeratinizing", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNonKeratinizing", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAgc", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAtypicalNos", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAtypicalFavorNeoplastic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAdenocarcinoma", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEndoCervical", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEndometrial", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEndometrialCells", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NonEpithelialMalignancies", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHormonalPatternsAccordingToAgeAndHistory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHormonalPatternDoesNotMatchTheAgeAndHistory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHormonalEvaluationIsNotPossible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InterpretationOfResults", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suggestion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMammae", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PathologyAnatomyDiagnoses", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ER", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Her2Neu", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ki67", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsReexamination", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPaReexaminationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosisName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalData", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExaminationMaterial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PathologyNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PathologyAnatomy";
				meta.Destination = "PathologyAnatomy";
				meta.spInsert = "proc_PathologyAnatomyInsert";
				meta.spUpdate = "proc_PathologyAnatomyUpdate";
				meta.spDelete = "proc_PathologyAnatomyDelete";
				meta.spLoadAll = "proc_PathologyAnatomyLoadAll";
				meta.spLoadByPrimaryKey = "proc_PathologyAnatomyLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PathologyAnatomyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
