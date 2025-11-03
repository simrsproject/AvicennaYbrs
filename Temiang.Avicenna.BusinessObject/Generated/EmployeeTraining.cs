/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2023 4:30:56 PM
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
	abstract public class esEmployeeTrainingCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingQuery query)
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
			this.InitQuery(query as esEmployeeTrainingQuery);
		}
		#endregion

		virtual public EmployeeTraining DetachEntity(EmployeeTraining entity)
		{
			return base.DetachEntity(entity) as EmployeeTraining;
		}

		virtual public EmployeeTraining AttachEntity(EmployeeTraining entity)
		{
			return base.AttachEntity(entity) as EmployeeTraining;
		}

		virtual public void Combine(EmployeeTrainingCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTraining this[int index]
		{
			get
			{
				return base[index] as EmployeeTraining;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTraining);
		}
	}

	[Serializable]
	abstract public class esEmployeeTraining : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTraining()
		{
		}

		public esEmployeeTraining(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeTrainingID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeTrainingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeTrainingID)
		{
			esEmployeeTrainingQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeTrainingID == employeeTrainingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeTrainingID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeTrainingID", employeeTrainingID);
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
						case "EmployeeTrainingID": this.str.EmployeeTrainingID = (string)value; break;
						case "EmployeeTrainingName": this.str.EmployeeTrainingName = (string)value; break;
						case "TrainingLocation": this.str.TrainingLocation = (string)value; break;
						case "TrainingOrganizer": this.str.TrainingOrganizer = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "StartTime": this.str.StartTime = (string)value; break;
						case "EndTime": this.str.EndTime = (string)value; break;
						case "TotalHour": this.str.TotalHour = (string)value; break;
						case "IsInHouseTraining": this.str.IsInHouseTraining = (string)value; break;
						case "CreditPoint": this.str.CreditPoint = (string)value; break;
						case "TrainingFee": this.str.TrainingFee = (string)value; break;
						case "SponsorFee": this.str.SponsorFee = (string)value; break;
						case "IsScheduledTraining": this.str.IsScheduledTraining = (string)value; break;
						case "TargetAttendance": this.str.TargetAttendance = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ReferenceID": this.str.ReferenceID = (string)value; break;
						case "IsProposal": this.str.IsProposal = (string)value; break;
						case "SRActivityType": this.str.SRActivityType = (string)value; break;
						case "CertificateValidityPeriod": this.str.CertificateValidityPeriod = (string)value; break;
						case "IsCommitmentToWork": this.str.IsCommitmentToWork = (string)value; break;
						case "LengthOfService": this.str.LengthOfService = (string)value; break;
						case "StartServiceDate": this.str.StartServiceDate = (string)value; break;
						case "EndServiceDate": this.str.EndServiceDate = (string)value; break;
						case "SRTrainingFinancingSources": this.str.SRTrainingFinancingSources = (string)value; break;
						case "EvaluationDate": this.str.EvaluationDate = (string)value; break;
						case "DurationHour": this.str.DurationHour = (string)value; break;
						case "DurationMinutes": this.str.DurationMinutes = (string)value; break;
						case "SREmployeeTrainingPointType": this.str.SREmployeeTrainingPointType = (string)value; break;
						case "SREmployeeTrainingDateSeparator": this.str.SREmployeeTrainingDateSeparator = (string)value; break;
						case "SRActivitySubType": this.str.SRActivitySubType = (string)value; break;
						case "PlanningCosts": this.str.PlanningCosts = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeTrainingID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingID = (System.Int32?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "StartTime":

							if (value == null || value is System.TimeSpan)
								this.StartTime = (System.TimeSpan?)value;
							break;
						case "EndTime":

							if (value == null || value is System.TimeSpan)
								this.EndTime = (System.TimeSpan?)value;
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
						case "TrainingFee":

							if (value == null || value is System.Decimal)
								this.TrainingFee = (System.Decimal?)value;
							break;
						case "SponsorFee":

							if (value == null || value is System.Decimal)
								this.SponsorFee = (System.Decimal?)value;
							break;
						case "IsScheduledTraining":

							if (value == null || value is System.Boolean)
								this.IsScheduledTraining = (System.Boolean?)value;
							break;
						case "TargetAttendance":

							if (value == null || value is System.Int32)
								this.TargetAttendance = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ReferenceID":

							if (value == null || value is System.Int32)
								this.ReferenceID = (System.Int32?)value;
							break;
						case "IsProposal":

							if (value == null || value is System.Boolean)
								this.IsProposal = (System.Boolean?)value;
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
						case "DurationHour":

							if (value == null || value is System.Decimal)
								this.DurationHour = (System.Decimal?)value;
							break;
						case "DurationMinutes":

							if (value == null || value is System.Decimal)
								this.DurationMinutes = (System.Decimal?)value;
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
		/// Maps to EmployeeTraining.EmployeeTrainingID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.EmployeeTrainingName
		/// </summary>
		virtual public System.String EmployeeTrainingName
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingName);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.TrainingLocation
		/// </summary>
		virtual public System.String TrainingLocation
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.TrainingLocation);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.TrainingLocation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.TrainingOrganizer
		/// </summary>
		virtual public System.String TrainingOrganizer
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.TrainingOrganizer);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.TrainingOrganizer, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.StartTime
		/// </summary>
		virtual public System.TimeSpan? StartTime
		{
			get
			{
				return base.GetSystemTimeSpan(EmployeeTrainingMetadata.ColumnNames.StartTime);
			}

			set
			{
				base.SetSystemTimeSpan(EmployeeTrainingMetadata.ColumnNames.StartTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.EndTime
		/// </summary>
		virtual public System.TimeSpan? EndTime
		{
			get
			{
				return base.GetSystemTimeSpan(EmployeeTrainingMetadata.ColumnNames.EndTime);
			}

			set
			{
				base.SetSystemTimeSpan(EmployeeTrainingMetadata.ColumnNames.EndTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.TotalHour
		/// </summary>
		virtual public System.Int32? TotalHour
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingMetadata.ColumnNames.TotalHour);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingMetadata.ColumnNames.TotalHour, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.IsInHouseTraining
		/// </summary>
		virtual public System.Boolean? IsInHouseTraining
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsInHouseTraining);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsInHouseTraining, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.CreditPoint
		/// </summary>
		virtual public System.Decimal? CreditPoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.CreditPoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.CreditPoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.TrainingFee
		/// </summary>
		virtual public System.Decimal? TrainingFee
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.TrainingFee);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.TrainingFee, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SponsorFee
		/// </summary>
		virtual public System.Decimal? SponsorFee
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.SponsorFee);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.SponsorFee, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.IsScheduledTraining
		/// </summary>
		virtual public System.Boolean? IsScheduledTraining
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsScheduledTraining);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsScheduledTraining, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.TargetAttendance
		/// </summary>
		virtual public System.Int32? TargetAttendance
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingMetadata.ColumnNames.TargetAttendance);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingMetadata.ColumnNames.TargetAttendance, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.ReferenceID
		/// </summary>
		virtual public System.Int32? ReferenceID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingMetadata.ColumnNames.ReferenceID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingMetadata.ColumnNames.ReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.IsProposal
		/// </summary>
		virtual public System.Boolean? IsProposal
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsProposal);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsProposal, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SRActivityType
		/// </summary>
		virtual public System.String SRActivityType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.SRActivityType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.SRActivityType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.CertificateValidityPeriod
		/// </summary>
		virtual public System.DateTime? CertificateValidityPeriod
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.CertificateValidityPeriod);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.CertificateValidityPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.IsCommitmentToWork
		/// </summary>
		virtual public System.Boolean? IsCommitmentToWork
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsCommitmentToWork);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingMetadata.ColumnNames.IsCommitmentToWork, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.LengthOfService
		/// </summary>
		virtual public System.Int16? LengthOfService
		{
			get
			{
				return base.GetSystemInt16(EmployeeTrainingMetadata.ColumnNames.LengthOfService);
			}

			set
			{
				base.SetSystemInt16(EmployeeTrainingMetadata.ColumnNames.LengthOfService, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.StartServiceDate
		/// </summary>
		virtual public System.DateTime? StartServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.StartServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.StartServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.EndServiceDate
		/// </summary>
		virtual public System.DateTime? EndServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EndServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EndServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SRTrainingFinancingSources
		/// </summary>
		virtual public System.String SRTrainingFinancingSources
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.SRTrainingFinancingSources);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.SRTrainingFinancingSources, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.EvaluationDate
		/// </summary>
		virtual public System.DateTime? EvaluationDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EvaluationDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingMetadata.ColumnNames.EvaluationDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.DurationHour
		/// </summary>
		virtual public System.Decimal? DurationHour
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.DurationHour);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.DurationHour, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.DurationMinutes
		/// </summary>
		virtual public System.Decimal? DurationMinutes
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.DurationMinutes);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.DurationMinutes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SREmployeeTrainingPointType
		/// </summary>
		virtual public System.String SREmployeeTrainingPointType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingPointType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingPointType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SREmployeeTrainingDateSeparator
		/// </summary>
		virtual public System.String SREmployeeTrainingDateSeparator
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingDateSeparator);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingDateSeparator, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.SRActivitySubType
		/// </summary>
		virtual public System.String SRActivitySubType
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingMetadata.ColumnNames.SRActivitySubType);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingMetadata.ColumnNames.SRActivitySubType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTraining.PlanningCosts
		/// </summary>
		virtual public System.Decimal? PlanningCosts
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.PlanningCosts);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingMetadata.ColumnNames.PlanningCosts, value);
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
			public esStrings(esEmployeeTraining entity)
			{
				this.entity = entity;
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
			public System.String EmployeeTrainingName
			{
				get
				{
					System.String data = entity.EmployeeTrainingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingName = null;
					else entity.EmployeeTrainingName = Convert.ToString(value);
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
			public System.String TrainingOrganizer
			{
				get
				{
					System.String data = entity.TrainingOrganizer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TrainingOrganizer = null;
					else entity.TrainingOrganizer = Convert.ToString(value);
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
			public System.String StartTime
			{
				get
				{
					System.TimeSpan? data = entity.StartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime = null;
					else entity.StartTime = TimeSpan.Parse(value);
				}
			}
			public System.String EndTime
			{
				get
				{
					System.TimeSpan? data = entity.EndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime = null;
					else entity.EndTime = TimeSpan.Parse(value);
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
			public System.String TrainingFee
			{
				get
				{
					System.Decimal? data = entity.TrainingFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TrainingFee = null;
					else entity.TrainingFee = Convert.ToDecimal(value);
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
			public System.String TargetAttendance
			{
				get
				{
					System.Int32? data = entity.TargetAttendance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetAttendance = null;
					else entity.TargetAttendance = Convert.ToInt32(value);
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
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String ReferenceID
			{
				get
				{
					System.Int32? data = entity.ReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceID = null;
					else entity.ReferenceID = Convert.ToInt32(value);
				}
			}
			public System.String IsProposal
			{
				get
				{
					System.Boolean? data = entity.IsProposal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProposal = null;
					else entity.IsProposal = Convert.ToBoolean(value);
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
			private esEmployeeTraining entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingQuery query)
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
				throw new Exception("esEmployeeTraining can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTraining : esEmployeeTraining
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingMetadata.Meta();
			}
		}

		public esQueryItem EmployeeTrainingID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeTrainingName
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingName, esSystemType.String);
			}
		}

		public esQueryItem TrainingLocation
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.TrainingLocation, esSystemType.String);
			}
		}

		public esQueryItem TrainingOrganizer
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.TrainingOrganizer, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem StartTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.StartTime, esSystemType.TimeSpan);
			}
		}

		public esQueryItem EndTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EndTime, esSystemType.TimeSpan);
			}
		}

		public esQueryItem TotalHour
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.TotalHour, esSystemType.Int32);
			}
		}

		public esQueryItem IsInHouseTraining
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.IsInHouseTraining, esSystemType.Boolean);
			}
		}

		public esQueryItem CreditPoint
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.CreditPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem TrainingFee
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.TrainingFee, esSystemType.Decimal);
			}
		}

		public esQueryItem SponsorFee
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SponsorFee, esSystemType.Decimal);
			}
		}

		public esQueryItem IsScheduledTraining
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.IsScheduledTraining, esSystemType.Boolean);
			}
		}

		public esQueryItem TargetAttendance
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.TargetAttendance, esSystemType.Int32);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.ReferenceID, esSystemType.Int32);
			}
		}

		public esQueryItem IsProposal
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.IsProposal, esSystemType.Boolean);
			}
		}

		public esQueryItem SRActivityType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SRActivityType, esSystemType.String);
			}
		}

		public esQueryItem CertificateValidityPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.CertificateValidityPeriod, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCommitmentToWork
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.IsCommitmentToWork, esSystemType.Boolean);
			}
		}

		public esQueryItem LengthOfService
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.LengthOfService, esSystemType.Int16);
			}
		}

		public esQueryItem StartServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.StartServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EndServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRTrainingFinancingSources
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SRTrainingFinancingSources, esSystemType.String);
			}
		}

		public esQueryItem EvaluationDate
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.EvaluationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DurationHour
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.DurationHour, esSystemType.Decimal);
			}
		}

		public esQueryItem DurationMinutes
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.DurationMinutes, esSystemType.Decimal);
			}
		}

		public esQueryItem SREmployeeTrainingPointType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingPointType, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeTrainingDateSeparator
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingDateSeparator, esSystemType.String);
			}
		}

		public esQueryItem SRActivitySubType
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.SRActivitySubType, esSystemType.String);
			}
		}

		public esQueryItem PlanningCosts
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingMetadata.ColumnNames.PlanningCosts, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingCollection")]
	public partial class EmployeeTrainingCollection : esEmployeeTrainingCollection, IEnumerable<EmployeeTraining>
	{
		public EmployeeTrainingCollection()
		{

		}

		public static implicit operator List<EmployeeTraining>(EmployeeTrainingCollection coll)
		{
			List<EmployeeTraining> list = new List<EmployeeTraining>();

			foreach (EmployeeTraining emp in coll)
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
				return EmployeeTrainingMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTraining(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTraining();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingQuery();
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
		public bool Load(EmployeeTrainingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTraining AddNew()
		{
			EmployeeTraining entity = base.AddNewEntity() as EmployeeTraining;

			return entity;
		}
		public EmployeeTraining FindByPrimaryKey(Int32 employeeTrainingID)
		{
			return base.FindByPrimaryKey(employeeTrainingID) as EmployeeTraining;
		}

		#region IEnumerable< EmployeeTraining> Members

		IEnumerator<EmployeeTraining> IEnumerable<EmployeeTraining>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTraining;
			}
		}

		#endregion

		private EmployeeTrainingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTraining' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTraining ({EmployeeTrainingID})")]
	[Serializable]
	public partial class EmployeeTraining : esEmployeeTraining
	{
		public EmployeeTraining()
		{
		}

		public EmployeeTraining(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingQuery();
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
		public bool Load(EmployeeTrainingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingQuery : esEmployeeTrainingQuery
	{
		public EmployeeTrainingQuery()
		{

		}

		public EmployeeTrainingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EmployeeTrainingID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EmployeeTrainingName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.TrainingLocation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.TrainingLocation;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.TrainingOrganizer, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.TrainingOrganizer;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.StartDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EndDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.StartTime, 6, typeof(System.TimeSpan), esSystemType.TimeSpan);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.StartTime;
			c.NumericPrecision = 16;
			c.NumericScale = 7;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EndTime, 7, typeof(System.TimeSpan), esSystemType.TimeSpan);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EndTime;
			c.NumericPrecision = 16;
			c.NumericScale = 7;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.TotalHour, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.TotalHour;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.IsInHouseTraining, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.IsInHouseTraining;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.CreditPoint, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.CreditPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.TrainingFee, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.TrainingFee;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SponsorFee, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SponsorFee;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.IsScheduledTraining, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.IsScheduledTraining;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.TargetAttendance, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.TargetAttendance;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.Note, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 16;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.IsActive, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.ReferenceID, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.ReferenceID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.IsProposal, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.IsProposal;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SRActivityType, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SRActivityType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.CertificateValidityPeriod, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.CertificateValidityPeriod;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.IsCommitmentToWork, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.IsCommitmentToWork;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.LengthOfService, 24, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.LengthOfService;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.StartServiceDate, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.StartServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EndServiceDate, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EndServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SRTrainingFinancingSources, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SRTrainingFinancingSources;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.EvaluationDate, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.EvaluationDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.DurationHour, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.DurationHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.DurationMinutes, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.DurationMinutes;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingPointType, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SREmployeeTrainingPointType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SREmployeeTrainingDateSeparator, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SREmployeeTrainingDateSeparator;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.SRActivitySubType, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.SRActivitySubType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingMetadata.ColumnNames.PlanningCosts, 34, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingMetadata.PropertyNames.PlanningCosts;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingMetadata Meta()
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
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string EmployeeTrainingName = "EmployeeTrainingName";
			public const string TrainingLocation = "TrainingLocation";
			public const string TrainingOrganizer = "TrainingOrganizer";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
			public const string TotalHour = "TotalHour";
			public const string IsInHouseTraining = "IsInHouseTraining";
			public const string CreditPoint = "CreditPoint";
			public const string TrainingFee = "TrainingFee";
			public const string SponsorFee = "SponsorFee";
			public const string IsScheduledTraining = "IsScheduledTraining";
			public const string TargetAttendance = "TargetAttendance";
			public const string Note = "Note";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceID = "ReferenceID";
			public const string IsProposal = "IsProposal";
			public const string SRActivityType = "SRActivityType";
			public const string CertificateValidityPeriod = "CertificateValidityPeriod";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string LengthOfService = "LengthOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
			public const string SRTrainingFinancingSources = "SRTrainingFinancingSources";
			public const string EvaluationDate = "EvaluationDate";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
			public const string SREmployeeTrainingPointType = "SREmployeeTrainingPointType";
			public const string SREmployeeTrainingDateSeparator = "SREmployeeTrainingDateSeparator";
			public const string SRActivitySubType = "SRActivitySubType";
			public const string PlanningCosts = "PlanningCosts";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string EmployeeTrainingName = "EmployeeTrainingName";
			public const string TrainingLocation = "TrainingLocation";
			public const string TrainingOrganizer = "TrainingOrganizer";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
			public const string TotalHour = "TotalHour";
			public const string IsInHouseTraining = "IsInHouseTraining";
			public const string CreditPoint = "CreditPoint";
			public const string TrainingFee = "TrainingFee";
			public const string SponsorFee = "SponsorFee";
			public const string IsScheduledTraining = "IsScheduledTraining";
			public const string TargetAttendance = "TargetAttendance";
			public const string Note = "Note";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceID = "ReferenceID";
			public const string IsProposal = "IsProposal";
			public const string SRActivityType = "SRActivityType";
			public const string CertificateValidityPeriod = "CertificateValidityPeriod";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string LengthOfService = "LengthOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
			public const string SRTrainingFinancingSources = "SRTrainingFinancingSources";
			public const string EvaluationDate = "EvaluationDate";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
			public const string SREmployeeTrainingPointType = "SREmployeeTrainingPointType";
			public const string SREmployeeTrainingDateSeparator = "SREmployeeTrainingDateSeparator";
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
			lock (typeof(EmployeeTrainingMetadata))
			{
				if (EmployeeTrainingMetadata.mapDelegates == null)
				{
					EmployeeTrainingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingMetadata.meta == null)
				{
					EmployeeTrainingMetadata.meta = new EmployeeTrainingMetadata();
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

				meta.AddTypeMap("EmployeeTrainingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeTrainingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TrainingLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TrainingOrganizer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StartTime", new esTypeMap("time", "System.TimeSpan"));
				meta.AddTypeMap("EndTime", new esTypeMap("time", "System.TimeSpan"));
				meta.AddTypeMap("TotalHour", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsInHouseTraining", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreditPoint", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TrainingFee", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SponsorFee", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsScheduledTraining", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TargetAttendance", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Note", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsProposal", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRActivityType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CertificateValidityPeriod", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCommitmentToWork", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LengthOfService", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("StartServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRTrainingFinancingSources", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EvaluationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DurationHour", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DurationMinutes", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SREmployeeTrainingPointType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeTrainingDateSeparator", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRActivitySubType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlanningCosts", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "EmployeeTraining";
				meta.Destination = "EmployeeTraining";
				meta.spInsert = "proc_EmployeeTrainingInsert";
				meta.spUpdate = "proc_EmployeeTrainingUpdate";
				meta.spDelete = "proc_EmployeeTrainingDelete";
				meta.spLoadAll = "proc_EmployeeTrainingLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
