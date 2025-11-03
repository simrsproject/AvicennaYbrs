/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/27/2022 9:47:08 PM
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
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheetCollection : esEntityCollectionWAuditLog
	{
		public esClinicalPerformanceAppraisalQuestionnaireScoresheetCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection";
		}

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query)
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
			this.InitQuery(query as esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery);
		}
		#endregion

		virtual public ClinicalPerformanceAppraisalQuestionnaireScoresheet DetachEntity(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity)
		{
			return base.DetachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireScoresheet;
		}

		virtual public ClinicalPerformanceAppraisalQuestionnaireScoresheet AttachEntity(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity)
		{
			return base.AttachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireScoresheet;
		}

		virtual public void Combine(ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection collection)
		{
			base.Combine(collection);
		}

		new public ClinicalPerformanceAppraisalQuestionnaireScoresheet this[int index]
		{
			get
			{
				return base[index] as ClinicalPerformanceAppraisalQuestionnaireScoresheet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClinicalPerformanceAppraisalQuestionnaireScoresheet);
		}
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery GetDynamicQuery()
		{
			return null;
		}

		public esClinicalPerformanceAppraisalQuestionnaireScoresheet()
		{
		}

		public esClinicalPerformanceAppraisalQuestionnaireScoresheet(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String scoresheetNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetNo);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String scoresheetNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetNo);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetNo);
		}

		private bool LoadByPrimaryKeyDynamic(String scoresheetNo)
		{
			esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query = this.GetDynamicQuery();
			query.Where(query.ScoresheetNo == scoresheetNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String scoresheetNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ScoresheetNo", scoresheetNo);
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
						case "ScoresheetNo": this.str.ScoresheetNo = (string)value; break;
						case "ScoringDate": this.str.ScoringDate = (string)value; break;
						case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "QuestionnaireID": this.str.QuestionnaireID = (string)value; break;
						case "TotalScore": this.str.TotalScore = (string)value; break;
						case "ConclusionGrade": this.str.ConclusionGrade = (string)value; break;
						case "ConclusionGradeName": this.str.ConclusionGradeName = (string)value; break;
						case "ConclusionNotes": this.str.ConclusionNotes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ScoringDate":

							if (value == null || value is System.DateTime)
								this.ScoringDate = (System.DateTime?)value;
							break;
						case "EvaluatorID":

							if (value == null || value is System.Int32)
								this.EvaluatorID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "QuestionnaireID":

							if (value == null || value is System.Int32)
								this.QuestionnaireID = (System.Int32?)value;
							break;
						case "TotalScore":

							if (value == null || value is System.Int16)
								this.TotalScore = (System.Int16?)value;
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
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ScoresheetNo
		/// </summary>
		virtual public System.String ScoresheetNo
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoresheetNo);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoresheetNo, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ScoringDate
		/// </summary>
		virtual public System.DateTime? ScoringDate
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoringDate);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoringDate, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.TotalScore
		/// </summary>
		virtual public System.Int16? TotalScore
		{
			get
			{
				return base.GetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.TotalScore);
			}

			set
			{
				base.SetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.TotalScore, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ConclusionGrade
		/// </summary>
		virtual public System.String ConclusionGrade
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGrade);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGrade, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ConclusionGradeName
		/// </summary>
		virtual public System.String ConclusionGradeName
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGradeName);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGradeName, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ConclusionNotes
		/// </summary>
		virtual public System.String ConclusionNotes
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionNotes);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionNotes, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheet.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClinicalPerformanceAppraisalQuestionnaireScoresheet entity)
			{
				this.entity = entity;
			}
			public System.String ScoresheetNo
			{
				get
				{
					System.String data = entity.ScoresheetNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoresheetNo = null;
					else entity.ScoresheetNo = Convert.ToString(value);
				}
			}
			public System.String ScoringDate
			{
				get
				{
					System.DateTime? data = entity.ScoringDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoringDate = null;
					else entity.ScoringDate = Convert.ToDateTime(value);
				}
			}
			public System.String EvaluatorID
			{
				get
				{
					System.Int32? data = entity.EvaluatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluatorID = null;
					else entity.EvaluatorID = Convert.ToInt32(value);
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
			public System.String TotalScore
			{
				get
				{
					System.Int16? data = entity.TotalScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalScore = null;
					else entity.TotalScore = Convert.ToInt16(value);
				}
			}
			public System.String ConclusionGrade
			{
				get
				{
					System.String data = entity.ConclusionGrade;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionGrade = null;
					else entity.ConclusionGrade = Convert.ToString(value);
				}
			}
			public System.String ConclusionGradeName
			{
				get
				{
					System.String data = entity.ConclusionGradeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionGradeName = null;
					else entity.ConclusionGradeName = Convert.ToString(value);
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
			private esClinicalPerformanceAppraisalQuestionnaireScoresheet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query)
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
				throw new Exception("esClinicalPerformanceAppraisalQuestionnaireScoresheet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheet : esClinicalPerformanceAppraisalQuestionnaireScoresheet
	{
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.Meta();
			}
		}

		public esQueryItem ScoresheetNo
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoresheetNo, esSystemType.String);
			}
		}

		public esQueryItem ScoringDate
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoringDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem TotalScore
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.TotalScore, esSystemType.Int16);
			}
		}

		public esQueryItem ConclusionGrade
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGrade, esSystemType.String);
			}
		}

		public esQueryItem ConclusionGradeName
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGradeName, esSystemType.String);
			}
		}

		public esQueryItem ConclusionNotes
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionNotes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection")]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection : esClinicalPerformanceAppraisalQuestionnaireScoresheetCollection, IEnumerable<ClinicalPerformanceAppraisalQuestionnaireScoresheet>
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection()
		{

		}

		public static implicit operator List<ClinicalPerformanceAppraisalQuestionnaireScoresheet>(ClinicalPerformanceAppraisalQuestionnaireScoresheetCollection coll)
		{
			List<ClinicalPerformanceAppraisalQuestionnaireScoresheet> list = new List<ClinicalPerformanceAppraisalQuestionnaireScoresheet>();

			foreach (ClinicalPerformanceAppraisalQuestionnaireScoresheet emp in coll)
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
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClinicalPerformanceAppraisalQuestionnaireScoresheet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClinicalPerformanceAppraisalQuestionnaireScoresheet AddNew()
		{
			ClinicalPerformanceAppraisalQuestionnaireScoresheet entity = base.AddNewEntity() as ClinicalPerformanceAppraisalQuestionnaireScoresheet;

			return entity;
		}
		public ClinicalPerformanceAppraisalQuestionnaireScoresheet FindByPrimaryKey(String scoresheetNo)
		{
			return base.FindByPrimaryKey(scoresheetNo) as ClinicalPerformanceAppraisalQuestionnaireScoresheet;
		}

		#region IEnumerable< ClinicalPerformanceAppraisalQuestionnaireScoresheet> Members

		IEnumerator<ClinicalPerformanceAppraisalQuestionnaireScoresheet> IEnumerable<ClinicalPerformanceAppraisalQuestionnaireScoresheet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClinicalPerformanceAppraisalQuestionnaireScoresheet;
			}
		}

		#endregion

		private ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClinicalPerformanceAppraisalQuestionnaireScoresheet' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClinicalPerformanceAppraisalQuestionnaireScoresheet ({ScoresheetNo})")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheet : esClinicalPerformanceAppraisalQuestionnaireScoresheet
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheet()
		{
		}

		public ClinicalPerformanceAppraisalQuestionnaireScoresheet(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.Meta();
			}
		}

		override protected esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery : esClinicalPerformanceAppraisalQuestionnaireScoresheetQuery
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery()
		{

		}

		public ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery";
		}
	}

	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoresheetNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ScoresheetNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ScoringDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ScoringDate;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.EvaluatorID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.EvaluatorID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.PersonID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRProfessionGroup, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalWorkArea, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.SRClinicalAuthorityLevel, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.QuestionnaireID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.QuestionnaireID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.TotalScore, 8, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.TotalScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGrade, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ConclusionGrade;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionGradeName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ConclusionGradeName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ConclusionNotes, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ConclusionNotes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsApproved, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.ApprovedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.IsVoid, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.VoidByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata Meta()
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
			public const string ScoresheetNo = "ScoresheetNo";
			public const string ScoringDate = "ScoringDate";
			public const string EvaluatorID = "EvaluatorID";
			public const string PersonID = "PersonID";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string TotalScore = "TotalScore";
			public const string ConclusionGrade = "ConclusionGrade";
			public const string ConclusionGradeName = "ConclusionGradeName";
			public const string ConclusionNotes = "ConclusionNotes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ScoresheetNo = "ScoresheetNo";
			public const string ScoringDate = "ScoringDate";
			public const string EvaluatorID = "EvaluatorID";
			public const string PersonID = "PersonID";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string TotalScore = "TotalScore";
			public const string ConclusionGrade = "ConclusionGrade";
			public const string ConclusionGradeName = "ConclusionGradeName";
			public const string ConclusionNotes = "ConclusionNotes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata))
			{
				if (ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.mapDelegates == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.meta == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata.meta = new ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata();
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

				meta.AddTypeMap("ScoresheetNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScoringDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ConclusionGrade", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConclusionGradeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConclusionNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ClinicalPerformanceAppraisalQuestionnaireScoresheet";
				meta.Destination = "ClinicalPerformanceAppraisalQuestionnaireScoresheet";
				meta.spInsert = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetInsert";
				meta.spUpdate = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetUpdate";
				meta.spDelete = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetDelete";
				meta.spLoadAll = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClinicalPerformanceAppraisalQuestionnaireScoresheetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
