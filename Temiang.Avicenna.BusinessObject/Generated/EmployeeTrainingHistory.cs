/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2023 4:33:05 PM
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
	abstract public class esEmployeeTrainingHistoryCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingHistoryQuery query)
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
			this.InitQuery(query as esEmployeeTrainingHistoryQuery);
		}
		#endregion

		virtual public EmployeeTrainingHistory DetachEntity(EmployeeTrainingHistory entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingHistory;
		}

		virtual public EmployeeTrainingHistory AttachEntity(EmployeeTrainingHistory entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingHistory;
		}

		virtual public void Combine(EmployeeTrainingHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingHistory this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingHistory);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingHistory()
		{
		}

		public esEmployeeTrainingHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeTrainingHistoryID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingHistoryID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeTrainingHistoryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingHistoryID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeTrainingHistoryID)
		{
			esEmployeeTrainingHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeTrainingHistoryID == employeeTrainingHistoryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeTrainingHistoryID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeTrainingHistoryID", employeeTrainingHistoryID);
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
						case "EmployeeTrainingHistoryID": this.str.EmployeeTrainingHistoryID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "EventName": this.str.EventName = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "TrainingLocation": this.str.TrainingLocation = (string)value; break;
						case "TrainingInstitution": this.str.TrainingInstitution = (string)value; break;
						case "Fee": this.str.Fee = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsAttending": this.str.IsAttending = (string)value; break;
						case "EmployeeTrainingID": this.str.EmployeeTrainingID = (string)value; break;
						case "SponsorFee": this.str.SponsorFee = (string)value; break;
						case "TotalHour": this.str.TotalHour = (string)value; break;
						case "IsInHouseTraining": this.str.IsInHouseTraining = (string)value; break;
						case "CreditPoint": this.str.CreditPoint = (string)value; break;
						case "IsScheduledTraining": this.str.IsScheduledTraining = (string)value; break;
						case "SRActivityType": this.str.SRActivityType = (string)value; break;
						case "CertificateValidityPeriod": this.str.CertificateValidityPeriod = (string)value; break;
						case "IsCommitmentToWork": this.str.IsCommitmentToWork = (string)value; break;
						case "LengthOfService": this.str.LengthOfService = (string)value; break;
						case "StartServiceDate": this.str.StartServiceDate = (string)value; break;
						case "EndServiceDate": this.str.EndServiceDate = (string)value; break;
						case "SRTrainingFinancingSources": this.str.SRTrainingFinancingSources = (string)value; break;
						case "EvaluationDate": this.str.EvaluationDate = (string)value; break;
						case "EvaluationNote": this.str.EvaluationNote = (string)value; break;
						case "EvaluationNoteDateTime": this.str.EvaluationNoteDateTime = (string)value; break;
						case "SupervisorEvaluationNote": this.str.SupervisorEvaluationNote = (string)value; break;
						case "SupervisorEvaluationDateTime": this.str.SupervisorEvaluationDateTime = (string)value; break;
						case "SupervisorEvaluationNoteByUserID": this.str.SupervisorEvaluationNoteByUserID = (string)value; break;
						case "DurationHour": this.str.DurationHour = (string)value; break;
						case "DurationMinutes": this.str.DurationMinutes = (string)value; break;
						case "SREmployeeTrainingPointType": this.str.SREmployeeTrainingPointType = (string)value; break;
						case "SREmployeeTrainingDateSeparator": this.str.SREmployeeTrainingDateSeparator = (string)value; break;
						case "SREmployeeTrainingRole": this.str.SREmployeeTrainingRole = (string)value; break;
						case "EvaluationScore": this.str.EvaluationScore = (string)value; break;
						case "Recommendation": this.str.Recommendation = (string)value; break;
						case "SRActivitySubType": this.str.SRActivitySubType = (string)value; break;
						case "PlanningCosts": this.str.PlanningCosts = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeTrainingHistoryID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingHistoryID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "Fee":

							if (value == null || value is System.Decimal)
								this.Fee = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsAttending":

							if (value == null || value is System.Boolean)
								this.IsAttending = (System.Boolean?)value;
							break;
						case "EmployeeTrainingID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingID = (System.Int32?)value;
							break;
						case "SponsorFee":

							if (value == null || value is System.Decimal)
								this.SponsorFee = (System.Decimal?)value;
							break;
						case "TotalHour":

							if (value == null || value is System.Int32)
								this.TotalHour = (System.Int32?)value;
							break;
						case "IsInHouseTraining":

							if (value == null || value is System.Boolean)
								this.IsInHouseTraining = (System.Boolean?)value;
							break;
						case "CreditPoint":

							if (value == null || value is System.Decimal)
								this.CreditPoint = (System.Decimal?)value;
							break;
						case "IsScheduledTraining":

							if (value == null || value is System.Boolean)
								this.IsScheduledTraining = (System.Boolean?)value;
							break;
						case "CertificateValidityPeriod":

							if (value == null || value is System.DateTime)
								this.CertificateValidityPeriod = (System.DateTime?)value;
							break;
						case "IsCommitmentToWork":

							if (value == null || value is System.Boolean)
								this.IsCommitmentToWork = (System.Boolean?)value;
							break;
						case "LengthOfService":

							if (value == null || value is System.Int16)
								this.LengthOfService = (System.Int16?)value;
							break;
						case "StartServiceDate":

							if (value == null || value is System.DateTime)
								this.StartServiceDate = (System.DateTime?)value;
							break;
						case "EndServiceDate":

							if (value == null || value is System.DateTime)
								this.EndServiceDate = (System.DateTime?)value;
							break;
						case "EvaluationDate":

							if (value == null || value is System.DateTime)
								this.EvaluationDate = (System.DateTime?)value;
							break;
						case "EvaluationNoteDateTime":

							if (value == null || value is System.DateTime)
								this.EvaluationNoteDateTime = (System.DateTime?)value;
							break;
						case "SupervisorEvaluationDateTime":

							if (value == null || value is System.DateTime)
								this.SupervisorEvaluationDateTime = (System.DateTime?)value;
							break;
						case "DurationHour":

							if (value == null || value is System.Decimal)
								this.DurationHour = (System.Decimal?)value;
							break;
						case "DurationMinutes":

							if (value == null || value is System.Decimal)
								this.DurationMinutes = (System.Decimal?)value;
							break;
						case "EvaluationScore":

							if (value == null || value is System.Decimal)
								this.EvaluationScore = (System.Decimal?)value;
							break;
						case "PlanningCosts":

							if (value == null || value is System.Decimal)
								this.PlanningCosts = (System.Decimal?)value;
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
		/// Maps to EmployeeTrainingHistory.EmployeeTrainingHistoryID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingHistoryID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EventName
		/// </summary>
		virtual public System.String EventName
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.EventName);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.EventName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.TrainingLocation
		/// </summary>
		virtual public System.String TrainingLocation
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingLocation);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingLocation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.TrainingInstitution
		/// </summary>
		virtual public System.String TrainingInstitution
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingInstitution);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingInstitution, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.Fee
		/// </summary>
		virtual public System.Decimal? Fee
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.Fee);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.Fee, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.IsAttending
		/// </summary>
		virtual public System.Boolean? IsAttending
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EmployeeTrainingID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SponsorFee
		/// </summary>
		virtual public System.Decimal? SponsorFee
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.SponsorFee);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.SponsorFee, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.TotalHour
		/// </summary>
		virtual public System.Int32? TotalHour
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.TotalHour);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.TotalHour, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.IsInHouseTraining
		/// </summary>
		virtual public System.Boolean? IsInHouseTraining
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsInHouseTraining);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsInHouseTraining, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.CreditPoint
		/// </summary>
		virtual public System.Decimal? CreditPoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.CreditPoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.CreditPoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.IsScheduledTraining
		/// </summary>
		virtual public System.Boolean? IsScheduledTraining
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsScheduledTraining);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsScheduledTraining, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SRActivityType
		/// </summary>
		virtual public System.String SRActivityType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivityType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivityType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.CertificateValidityPeriod
		/// </summary>
		virtual public System.DateTime? CertificateValidityPeriod
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.IsCommitmentToWork
		/// </summary>
		virtual public System.Boolean? IsCommitmentToWork
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsCommitmentToWork);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingHistoryMetadata.ColumnNames.IsCommitmentToWork, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.LengthOfService
		/// </summary>
		virtual public System.Int16? LengthOfService
		{
			get
			{
				return base.GetSystemInt16(EmployeeTrainingHistoryMetadata.ColumnNames.LengthOfService);
			}

			set
			{
				base.SetSystemInt16(EmployeeTrainingHistoryMetadata.ColumnNames.LengthOfService, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.StartServiceDate
		/// </summary>
		virtual public System.DateTime? StartServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EndServiceDate
		/// </summary>
		virtual public System.DateTime? EndServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SRTrainingFinancingSources
		/// </summary>
		virtual public System.String SRTrainingFinancingSources
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRTrainingFinancingSources);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRTrainingFinancingSources, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EvaluationDate
		/// </summary>
		virtual public System.DateTime? EvaluationDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EvaluationNote
		/// </summary>
		virtual public System.String EvaluationNote
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNote);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNote, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EvaluationNoteDateTime
		/// </summary>
		virtual public System.DateTime? EvaluationNoteDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNoteDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNoteDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SupervisorEvaluationNote
		/// </summary>
		virtual public System.String SupervisorEvaluationNote
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNote);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNote, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SupervisorEvaluationDateTime
		/// </summary>
		virtual public System.DateTime? SupervisorEvaluationDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SupervisorEvaluationNoteByUserID
		/// </summary>
		virtual public System.String SupervisorEvaluationNoteByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNoteByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNoteByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.DurationHour
		/// </summary>
		virtual public System.Decimal? DurationHour
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.DurationHour);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.DurationHour, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.DurationMinutes
		/// </summary>
		virtual public System.Decimal? DurationMinutes
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.DurationMinutes);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.DurationMinutes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SREmployeeTrainingPointType
		/// </summary>
		virtual public System.String SREmployeeTrainingPointType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingPointType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingPointType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SREmployeeTrainingDateSeparator
		/// </summary>
		virtual public System.String SREmployeeTrainingDateSeparator
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingDateSeparator);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingDateSeparator, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SREmployeeTrainingRole
		/// </summary>
		virtual public System.String SREmployeeTrainingRole
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EvaluationScore
		/// </summary>
		virtual public System.Decimal? EvaluationScore
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationScore);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationScore, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.Recommendation
		/// </summary>
		virtual public System.String Recommendation
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.Recommendation);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.Recommendation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.SRActivitySubType
		/// </summary>
		virtual public System.String SRActivitySubType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivitySubType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivitySubType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.PlanningCosts
		/// </summary>
		virtual public System.Decimal? PlanningCosts
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.PlanningCosts);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryMetadata.ColumnNames.PlanningCosts, value);
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
			public esStrings(esEmployeeTrainingHistory entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeTrainingHistoryID
			{
				get
				{
					System.Int32? data = entity.EmployeeTrainingHistoryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingHistoryID = null;
					else entity.EmployeeTrainingHistoryID = Convert.ToInt32(value);
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
			public System.String EventName
			{
				get
				{
					System.String data = entity.EventName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EventName = null;
					else entity.EventName = Convert.ToString(value);
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
			public System.String TrainingLocation
			{
				get
				{
					System.String data = entity.TrainingLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TrainingLocation = null;
					else entity.TrainingLocation = Convert.ToString(value);
				}
			}
			public System.String TrainingInstitution
			{
				get
				{
					System.String data = entity.TrainingInstitution;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TrainingInstitution = null;
					else entity.TrainingInstitution = Convert.ToString(value);
				}
			}
			public System.String Fee
			{
				get
				{
					System.Decimal? data = entity.Fee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fee = null;
					else entity.Fee = Convert.ToDecimal(value);
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
			public System.String IsAttending
			{
				get
				{
					System.Boolean? data = entity.IsAttending;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAttending = null;
					else entity.IsAttending = Convert.ToBoolean(value);
				}
			}
			public System.String EmployeeTrainingID
			{
				get
				{
					System.Int32? data = entity.EmployeeTrainingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingID = null;
					else entity.EmployeeTrainingID = Convert.ToInt32(value);
				}
			}
			public System.String SponsorFee
			{
				get
				{
					System.Decimal? data = entity.SponsorFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SponsorFee = null;
					else entity.SponsorFee = Convert.ToDecimal(value);
				}
			}
			public System.String TotalHour
			{
				get
				{
					System.Int32? data = entity.TotalHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalHour = null;
					else entity.TotalHour = Convert.ToInt32(value);
				}
			}
			public System.String IsInHouseTraining
			{
				get
				{
					System.Boolean? data = entity.IsInHouseTraining;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInHouseTraining = null;
					else entity.IsInHouseTraining = Convert.ToBoolean(value);
				}
			}
			public System.String CreditPoint
			{
				get
				{
					System.Decimal? data = entity.CreditPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditPoint = null;
					else entity.CreditPoint = Convert.ToDecimal(value);
				}
			}
			public System.String IsScheduledTraining
			{
				get
				{
					System.Boolean? data = entity.IsScheduledTraining;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScheduledTraining = null;
					else entity.IsScheduledTraining = Convert.ToBoolean(value);
				}
			}
			public System.String SRActivityType
			{
				get
				{
					System.String data = entity.SRActivityType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRActivityType = null;
					else entity.SRActivityType = Convert.ToString(value);
				}
			}
			public System.String CertificateValidityPeriod
			{
				get
				{
					System.DateTime? data = entity.CertificateValidityPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CertificateValidityPeriod = null;
					else entity.CertificateValidityPeriod = Convert.ToDateTime(value);
				}
			}
			public System.String IsCommitmentToWork
			{
				get
				{
					System.Boolean? data = entity.IsCommitmentToWork;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCommitmentToWork = null;
					else entity.IsCommitmentToWork = Convert.ToBoolean(value);
				}
			}
			public System.String LengthOfService
			{
				get
				{
					System.Int16? data = entity.LengthOfService;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LengthOfService = null;
					else entity.LengthOfService = Convert.ToInt16(value);
				}
			}
			public System.String StartServiceDate
			{
				get
				{
					System.DateTime? data = entity.StartServiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartServiceDate = null;
					else entity.StartServiceDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndServiceDate
			{
				get
				{
					System.DateTime? data = entity.EndServiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndServiceDate = null;
					else entity.EndServiceDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRTrainingFinancingSources
			{
				get
				{
					System.String data = entity.SRTrainingFinancingSources;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTrainingFinancingSources = null;
					else entity.SRTrainingFinancingSources = Convert.ToString(value);
				}
			}
			public System.String EvaluationDate
			{
				get
				{
					System.DateTime? data = entity.EvaluationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationDate = null;
					else entity.EvaluationDate = Convert.ToDateTime(value);
				}
			}
			public System.String EvaluationNote
			{
				get
				{
					System.String data = entity.EvaluationNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationNote = null;
					else entity.EvaluationNote = Convert.ToString(value);
				}
			}
			public System.String EvaluationNoteDateTime
			{
				get
				{
					System.DateTime? data = entity.EvaluationNoteDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationNoteDateTime = null;
					else entity.EvaluationNoteDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SupervisorEvaluationNote
			{
				get
				{
					System.String data = entity.SupervisorEvaluationNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorEvaluationNote = null;
					else entity.SupervisorEvaluationNote = Convert.ToString(value);
				}
			}
			public System.String SupervisorEvaluationDateTime
			{
				get
				{
					System.DateTime? data = entity.SupervisorEvaluationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorEvaluationDateTime = null;
					else entity.SupervisorEvaluationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SupervisorEvaluationNoteByUserID
			{
				get
				{
					System.String data = entity.SupervisorEvaluationNoteByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorEvaluationNoteByUserID = null;
					else entity.SupervisorEvaluationNoteByUserID = Convert.ToString(value);
				}
			}
			public System.String DurationHour
			{
				get
				{
					System.Decimal? data = entity.DurationHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationHour = null;
					else entity.DurationHour = Convert.ToDecimal(value);
				}
			}
			public System.String DurationMinutes
			{
				get
				{
					System.Decimal? data = entity.DurationMinutes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationMinutes = null;
					else entity.DurationMinutes = Convert.ToDecimal(value);
				}
			}
			public System.String SREmployeeTrainingPointType
			{
				get
				{
					System.String data = entity.SREmployeeTrainingPointType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeTrainingPointType = null;
					else entity.SREmployeeTrainingPointType = Convert.ToString(value);
				}
			}
			public System.String SREmployeeTrainingDateSeparator
			{
				get
				{
					System.String data = entity.SREmployeeTrainingDateSeparator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeTrainingDateSeparator = null;
					else entity.SREmployeeTrainingDateSeparator = Convert.ToString(value);
				}
			}
			public System.String SREmployeeTrainingRole
			{
				get
				{
					System.String data = entity.SREmployeeTrainingRole;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeTrainingRole = null;
					else entity.SREmployeeTrainingRole = Convert.ToString(value);
				}
			}
			public System.String EvaluationScore
			{
				get
				{
					System.Decimal? data = entity.EvaluationScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluationScore = null;
					else entity.EvaluationScore = Convert.ToDecimal(value);
				}
			}
			public System.String Recommendation
			{
				get
				{
					System.String data = entity.Recommendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recommendation = null;
					else entity.Recommendation = Convert.ToString(value);
				}
			}
			public System.String SRActivitySubType
			{
				get
				{
					System.String data = entity.SRActivitySubType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRActivitySubType = null;
					else entity.SRActivitySubType = Convert.ToString(value);
				}
			}
			public System.String PlanningCosts
			{
				get
				{
					System.Decimal? data = entity.PlanningCosts;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlanningCosts = null;
					else entity.PlanningCosts = Convert.ToDecimal(value);
				}
			}
			private esEmployeeTrainingHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingHistoryQuery query)
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
				throw new Exception("esEmployeeTrainingHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingHistory : esEmployeeTrainingHistory
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingHistoryMetadata.Meta();
			}
		}

		public esQueryItem EmployeeTrainingHistoryID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EventName
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EventName, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TrainingLocation
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.TrainingLocation, esSystemType.String);
			}
		}

		public esQueryItem TrainingInstitution
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.TrainingInstitution, esSystemType.String);
			}
		}

		public esQueryItem Fee
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.Fee, esSystemType.Decimal);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsAttending
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending, esSystemType.Boolean);
			}
		}

		public esQueryItem EmployeeTrainingID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingID, esSystemType.Int32);
			}
		}

		public esQueryItem SponsorFee
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SponsorFee, esSystemType.Decimal);
			}
		}

		public esQueryItem TotalHour
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.TotalHour, esSystemType.Int32);
			}
		}

		public esQueryItem IsInHouseTraining
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.IsInHouseTraining, esSystemType.Boolean);
			}
		}

		public esQueryItem CreditPoint
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.CreditPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem IsScheduledTraining
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.IsScheduledTraining, esSystemType.Boolean);
			}
		}

		public esQueryItem SRActivityType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SRActivityType, esSystemType.String);
			}
		}

		public esQueryItem CertificateValidityPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCommitmentToWork
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.IsCommitmentToWork, esSystemType.Boolean);
			}
		}

		public esQueryItem LengthOfService
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.LengthOfService, esSystemType.Int16);
			}
		}

		public esQueryItem StartServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRTrainingFinancingSources
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SRTrainingFinancingSources, esSystemType.String);
			}
		}

		public esQueryItem EvaluationDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EvaluationNote
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNote, esSystemType.String);
			}
		}

		public esQueryItem EvaluationNoteDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNoteDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SupervisorEvaluationNote
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNote, esSystemType.String);
			}
		}

		public esQueryItem SupervisorEvaluationDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SupervisorEvaluationNoteByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNoteByUserID, esSystemType.String);
			}
		}

		public esQueryItem DurationHour
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.DurationHour, esSystemType.Decimal);
			}
		}

		public esQueryItem DurationMinutes
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.DurationMinutes, esSystemType.Decimal);
			}
		}

		public esQueryItem SREmployeeTrainingPointType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingPointType, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeTrainingDateSeparator
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingDateSeparator, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeTrainingRole
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole, esSystemType.String);
			}
		}

		public esQueryItem EvaluationScore
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationScore, esSystemType.Decimal);
			}
		}

		public esQueryItem Recommendation
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.Recommendation, esSystemType.String);
			}
		}

		public esQueryItem SRActivitySubType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.SRActivitySubType, esSystemType.String);
			}
		}

		public esQueryItem PlanningCosts
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryMetadata.ColumnNames.PlanningCosts, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingHistoryCollection")]
	public partial class EmployeeTrainingHistoryCollection : esEmployeeTrainingHistoryCollection, IEnumerable<EmployeeTrainingHistory>
	{
		public EmployeeTrainingHistoryCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingHistory>(EmployeeTrainingHistoryCollection coll)
		{
			List<EmployeeTrainingHistory> list = new List<EmployeeTrainingHistory>();

			foreach (EmployeeTrainingHistory emp in coll)
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
				return EmployeeTrainingHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingHistoryQuery();
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
		public bool Load(EmployeeTrainingHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingHistory AddNew()
		{
			EmployeeTrainingHistory entity = base.AddNewEntity() as EmployeeTrainingHistory;

			return entity;
		}
		public EmployeeTrainingHistory FindByPrimaryKey(Int32 employeeTrainingHistoryID)
		{
			return base.FindByPrimaryKey(employeeTrainingHistoryID) as EmployeeTrainingHistory;
		}

		#region IEnumerable< EmployeeTrainingHistory> Members

		IEnumerator<EmployeeTrainingHistory> IEnumerable<EmployeeTrainingHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingHistory;
			}
		}

		#endregion

		private EmployeeTrainingHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingHistory ({EmployeeTrainingHistoryID})")]
	[Serializable]
	public partial class EmployeeTrainingHistory : esEmployeeTrainingHistory
	{
		public EmployeeTrainingHistory()
		{
		}

		public EmployeeTrainingHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingHistoryMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingHistoryQuery();
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
		public bool Load(EmployeeTrainingHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingHistoryQuery : esEmployeeTrainingHistoryQuery
	{
		public EmployeeTrainingHistoryQuery()
		{

		}

		public EmployeeTrainingHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingHistoryQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EmployeeTrainingHistoryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EventName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EventName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.StartDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EndDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingLocation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.TrainingLocation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.TrainingInstitution, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.TrainingInstitution;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.Fee, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.Fee;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.IsAttending;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingID, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EmployeeTrainingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SponsorFee, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SponsorFee;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.TotalHour, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.TotalHour;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.IsInHouseTraining, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.IsInHouseTraining;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.CreditPoint, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.CreditPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.IsScheduledTraining, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.IsScheduledTraining;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivityType, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SRActivityType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.CertificateValidityPeriod;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.IsCommitmentToWork, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.IsCommitmentToWork;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.LengthOfService, 21, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.LengthOfService;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.StartServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EndServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SRTrainingFinancingSources, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SRTrainingFinancingSources;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EvaluationDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNote, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EvaluationNote;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationNoteDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EvaluationNoteDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNote, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SupervisorEvaluationNote;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SupervisorEvaluationDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationNoteByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SupervisorEvaluationNoteByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.DurationHour, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.DurationHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.DurationMinutes, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.DurationMinutes;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingPointType, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SREmployeeTrainingPointType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingDateSeparator, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SREmployeeTrainingDateSeparator;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SREmployeeTrainingRole;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationScore, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.EvaluationScore;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.Recommendation, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.Recommendation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.SRActivitySubType, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.SRActivitySubType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.PlanningCosts, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.PlanningCosts;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingHistoryMetadata Meta()
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
			public const string EmployeeTrainingHistoryID = "EmployeeTrainingHistoryID";
			public const string PersonID = "PersonID";
			public const string EventName = "EventName";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TrainingLocation = "TrainingLocation";
			public const string TrainingInstitution = "TrainingInstitution";
			public const string Fee = "Fee";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsAttending = "IsAttending";
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string SponsorFee = "SponsorFee";
			public const string TotalHour = "TotalHour";
			public const string IsInHouseTraining = "IsInHouseTraining";
			public const string CreditPoint = "CreditPoint";
			public const string IsScheduledTraining = "IsScheduledTraining";
			public const string SRActivityType = "SRActivityType";
			public const string CertificateValidityPeriod = "CertificateValidityPeriod";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string LengthOfService = "LengthOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
			public const string SRTrainingFinancingSources = "SRTrainingFinancingSources";
			public const string EvaluationDate = "EvaluationDate";
			public const string EvaluationNote = "EvaluationNote";
			public const string EvaluationNoteDateTime = "EvaluationNoteDateTime";
			public const string SupervisorEvaluationNote = "SupervisorEvaluationNote";
			public const string SupervisorEvaluationDateTime = "SupervisorEvaluationDateTime";
			public const string SupervisorEvaluationNoteByUserID = "SupervisorEvaluationNoteByUserID";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
			public const string SREmployeeTrainingPointType = "SREmployeeTrainingPointType";
			public const string SREmployeeTrainingDateSeparator = "SREmployeeTrainingDateSeparator";
			public const string SREmployeeTrainingRole = "SREmployeeTrainingRole";
			public const string EvaluationScore = "EvaluationScore";
			public const string Recommendation = "Recommendation";
			public const string SRActivitySubType = "SRActivitySubType";
			public const string PlanningCosts = "PlanningCosts";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeTrainingHistoryID = "EmployeeTrainingHistoryID";
			public const string PersonID = "PersonID";
			public const string EventName = "EventName";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TrainingLocation = "TrainingLocation";
			public const string TrainingInstitution = "TrainingInstitution";
			public const string Fee = "Fee";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsAttending = "IsAttending";
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string SponsorFee = "SponsorFee";
			public const string TotalHour = "TotalHour";
			public const string IsInHouseTraining = "IsInHouseTraining";
			public const string CreditPoint = "CreditPoint";
			public const string IsScheduledTraining = "IsScheduledTraining";
			public const string SRActivityType = "SRActivityType";
			public const string CertificateValidityPeriod = "CertificateValidityPeriod";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string LengthOfService = "LengthOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
			public const string SRTrainingFinancingSources = "SRTrainingFinancingSources";
			public const string EvaluationDate = "EvaluationDate";
			public const string EvaluationNote = "EvaluationNote";
			public const string EvaluationNoteDateTime = "EvaluationNoteDateTime";
			public const string SupervisorEvaluationNote = "SupervisorEvaluationNote";
			public const string SupervisorEvaluationDateTime = "SupervisorEvaluationDateTime";
			public const string SupervisorEvaluationNoteByUserID = "SupervisorEvaluationNoteByUserID";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
			public const string SREmployeeTrainingPointType = "SREmployeeTrainingPointType";
			public const string SREmployeeTrainingDateSeparator = "SREmployeeTrainingDateSeparator";
			public const string SREmployeeTrainingRole = "SREmployeeTrainingRole";
			public const string EvaluationScore = "EvaluationScore";
			public const string Recommendation = "Recommendation";
			public const string SRActivitySubType = "SRActivitySubType";
			public const string PlanningCosts = "PlanningCosts";
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
			lock (typeof(EmployeeTrainingHistoryMetadata))
			{
				if (EmployeeTrainingHistoryMetadata.mapDelegates == null)
				{
					EmployeeTrainingHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingHistoryMetadata.meta == null)
				{
					EmployeeTrainingHistoryMetadata.meta = new EmployeeTrainingHistoryMetadata();
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

				meta.AddTypeMap("EmployeeTrainingHistoryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EventName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TrainingLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TrainingInstitution", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Fee", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAttending", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("EmployeeTrainingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SponsorFee", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalHour", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsInHouseTraining", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreditPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsScheduledTraining", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRActivityType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CertificateValidityPeriod", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCommitmentToWork", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LengthOfService", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("StartServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRTrainingFinancingSources", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EvaluationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EvaluationNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EvaluationNoteDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SupervisorEvaluationNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisorEvaluationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SupervisorEvaluationNoteByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DurationHour", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DurationMinutes", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SREmployeeTrainingPointType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeTrainingDateSeparator", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeTrainingRole", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EvaluationScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRActivitySubType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlanningCosts", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "EmployeeTrainingHistory";
				meta.Destination = "EmployeeTrainingHistory";
				meta.spInsert = "proc_EmployeeTrainingHistoryInsert";
				meta.spUpdate = "proc_EmployeeTrainingHistoryUpdate";
				meta.spDelete = "proc_EmployeeTrainingHistoryDelete";
				meta.spLoadAll = "proc_EmployeeTrainingHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
