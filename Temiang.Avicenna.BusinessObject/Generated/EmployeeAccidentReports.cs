/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/13/2022 9:12:48 PM
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
	abstract public class esEmployeeAccidentReportsCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeAccidentReportsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeAccidentReportsCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeAccidentReportsQuery query)
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
			this.InitQuery(query as esEmployeeAccidentReportsQuery);
		}
		#endregion

		virtual public EmployeeAccidentReports DetachEntity(EmployeeAccidentReports entity)
		{
			return base.DetachEntity(entity) as EmployeeAccidentReports;
		}

		virtual public EmployeeAccidentReports AttachEntity(EmployeeAccidentReports entity)
		{
			return base.AttachEntity(entity) as EmployeeAccidentReports;
		}

		virtual public void Combine(EmployeeAccidentReportsCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeAccidentReports this[int index]
		{
			get
			{
				return base[index] as EmployeeAccidentReports;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeAccidentReports);
		}
	}

	[Serializable]
	abstract public class esEmployeeAccidentReports : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeAccidentReportsQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeAccidentReports()
		{
		}

		public esEmployeeAccidentReports(DataRow row)
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
			esEmployeeAccidentReportsQuery query = this.GetDynamicQuery();
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
						case "ReportingDateTime": this.str.ReportingDateTime = (string)value; break;
						case "IncidentDateTime": this.str.IncidentDateTime = (string)value; break;
						case "IncidentLocation": this.str.IncidentLocation = (string)value; break;
						case "SupervisorID": this.str.SupervisorID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "AgeInYear": this.str.AgeInYear = (string)value; break;
						case "AgeInMonth": this.str.AgeInMonth = (string)value; break;
						case "AgeInDay": this.str.AgeInDay = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SREmployeeInjuryCategory": this.str.SREmployeeInjuryCategory = (string)value; break;
						case "SREmployeeIncidentStatus": this.str.SREmployeeIncidentStatus = (string)value; break;
						case "ChronologicalEvents": this.str.ChronologicalEvents = (string)value; break;
						case "AspectsOfTheCause": this.str.AspectsOfTheCause = (string)value; break;
						case "InjuredLocation": this.str.InjuredLocation = (string)value; break;
						case "PlaceOfTreatment": this.str.PlaceOfTreatment = (string)value; break;
						case "SREmployeeIncidentType": this.str.SREmployeeIncidentType = (string)value; break;
						case "SRNeedleType": this.str.SRNeedleType = (string)value; break;
						case "LossTime": this.str.LossTime = (string)value; break;
						case "UnsafeCondition": this.str.UnsafeCondition = (string)value; break;
						case "UnsafeAct": this.str.UnsafeAct = (string)value; break;
						case "PersonalIndirectCause": this.str.PersonalIndirectCause = (string)value; break;
						case "WorkingIndirectCause": this.str.WorkingIndirectCause = (string)value; break;
						case "ActionPlan": this.str.ActionPlan = (string)value; break;
						case "Target": this.str.Target = (string)value; break;
						case "Authority": this.str.Authority = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SREmployeeAccidentReportStatus": this.str.SREmployeeAccidentReportStatus = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReportingDateTime":

							if (value == null || value is System.DateTime)
								this.ReportingDateTime = (System.DateTime?)value;
							break;
						case "IncidentDateTime":

							if (value == null || value is System.DateTime)
								this.IncidentDateTime = (System.DateTime?)value;
							break;
						case "SupervisorID":

							if (value == null || value is System.Int32)
								this.SupervisorID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "LossTime":

							if (value == null || value is System.Byte)
								this.LossTime = (System.Byte?)value;
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
		/// Maps to EmployeeAccidentReports.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.ReportingDateTime
		/// </summary>
		virtual public System.DateTime? ReportingDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.ReportingDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.ReportingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.IncidentDateTime
		/// </summary>
		virtual public System.DateTime? IncidentDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.IncidentDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.IncidentDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.IncidentLocation
		/// </summary>
		virtual public System.String IncidentLocation
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.IncidentLocation);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.IncidentLocation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SupervisorID
		/// </summary>
		virtual public System.Int32? SupervisorID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.SupervisorID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.SupervisorID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.AgeInYear
		/// </summary>
		virtual public System.Byte? AgeInYear
		{
			get
			{
				return base.GetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInYear);
			}

			set
			{
				base.SetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.AgeInMonth
		/// </summary>
		virtual public System.Byte? AgeInMonth
		{
			get
			{
				return base.GetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInMonth);
			}

			set
			{
				base.SetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInMonth, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.AgeInDay
		/// </summary>
		virtual public System.Byte? AgeInDay
		{
			get
			{
				return base.GetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInDay);
			}

			set
			{
				base.SetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.AgeInDay, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAccidentReportsMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SREmployeeInjuryCategory
		/// </summary>
		virtual public System.String SREmployeeInjuryCategory
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeInjuryCategory);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeInjuryCategory, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SREmployeeIncidentStatus
		/// </summary>
		virtual public System.String SREmployeeIncidentStatus
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentStatus);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.ChronologicalEvents
		/// </summary>
		virtual public System.String ChronologicalEvents
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ChronologicalEvents);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ChronologicalEvents, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.AspectsOfTheCause
		/// </summary>
		virtual public System.String AspectsOfTheCause
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.AspectsOfTheCause);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.AspectsOfTheCause, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.InjuredLocation
		/// </summary>
		virtual public System.String InjuredLocation
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.InjuredLocation);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.InjuredLocation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.PlaceOfTreatment
		/// </summary>
		virtual public System.String PlaceOfTreatment
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.PlaceOfTreatment);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.PlaceOfTreatment, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SREmployeeIncidentType
		/// </summary>
		virtual public System.String SREmployeeIncidentType
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentType);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SRNeedleType
		/// </summary>
		virtual public System.String SRNeedleType
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SRNeedleType);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SRNeedleType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.LossTime
		/// </summary>
		virtual public System.Byte? LossTime
		{
			get
			{
				return base.GetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.LossTime);
			}

			set
			{
				base.SetSystemByte(EmployeeAccidentReportsMetadata.ColumnNames.LossTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.UnsafeCondition
		/// </summary>
		virtual public System.String UnsafeCondition
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeCondition);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeCondition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.UnsafeAct
		/// </summary>
		virtual public System.String UnsafeAct
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeAct);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeAct, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.PersonalIndirectCause
		/// </summary>
		virtual public System.String PersonalIndirectCause
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.PersonalIndirectCause);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.PersonalIndirectCause, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.WorkingIndirectCause
		/// </summary>
		virtual public System.String WorkingIndirectCause
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.WorkingIndirectCause);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.WorkingIndirectCause, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.ActionPlan
		/// </summary>
		virtual public System.String ActionPlan
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ActionPlan);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ActionPlan, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.Target
		/// </summary>
		virtual public System.String Target
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.Target);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.Target, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.Authority
		/// </summary>
		virtual public System.String Authority
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.Authority);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.Authority, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeeAccidentReportsMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAccidentReports.SREmployeeAccidentReportStatus
		/// </summary>
		virtual public System.String SREmployeeAccidentReportStatus
		{
			get
			{
				return base.GetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeAccidentReportStatus);
			}

			set
			{
				base.SetSystemString(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeAccidentReportStatus, value);
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
			public esStrings(esEmployeeAccidentReports entity)
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
			public System.String ReportingDateTime
			{
				get
				{
					System.DateTime? data = entity.ReportingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportingDateTime = null;
					else entity.ReportingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IncidentDateTime
			{
				get
				{
					System.DateTime? data = entity.IncidentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentDateTime = null;
					else entity.IncidentDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IncidentLocation
			{
				get
				{
					System.String data = entity.IncidentLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentLocation = null;
					else entity.IncidentLocation = Convert.ToString(value);
				}
			}
			public System.String SupervisorID
			{
				get
				{
					System.Int32? data = entity.SupervisorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorID = null;
					else entity.SupervisorID = Convert.ToInt32(value);
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
			public System.String SREmployeeInjuryCategory
			{
				get
				{
					System.String data = entity.SREmployeeInjuryCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeInjuryCategory = null;
					else entity.SREmployeeInjuryCategory = Convert.ToString(value);
				}
			}
			public System.String SREmployeeIncidentStatus
			{
				get
				{
					System.String data = entity.SREmployeeIncidentStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeIncidentStatus = null;
					else entity.SREmployeeIncidentStatus = Convert.ToString(value);
				}
			}
			public System.String ChronologicalEvents
			{
				get
				{
					System.String data = entity.ChronologicalEvents;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChronologicalEvents = null;
					else entity.ChronologicalEvents = Convert.ToString(value);
				}
			}
			public System.String AspectsOfTheCause
			{
				get
				{
					System.String data = entity.AspectsOfTheCause;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfTheCause = null;
					else entity.AspectsOfTheCause = Convert.ToString(value);
				}
			}
			public System.String InjuredLocation
			{
				get
				{
					System.String data = entity.InjuredLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InjuredLocation = null;
					else entity.InjuredLocation = Convert.ToString(value);
				}
			}
			public System.String PlaceOfTreatment
			{
				get
				{
					System.String data = entity.PlaceOfTreatment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlaceOfTreatment = null;
					else entity.PlaceOfTreatment = Convert.ToString(value);
				}
			}
			public System.String SREmployeeIncidentType
			{
				get
				{
					System.String data = entity.SREmployeeIncidentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeIncidentType = null;
					else entity.SREmployeeIncidentType = Convert.ToString(value);
				}
			}
			public System.String SRNeedleType
			{
				get
				{
					System.String data = entity.SRNeedleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNeedleType = null;
					else entity.SRNeedleType = Convert.ToString(value);
				}
			}
			public System.String LossTime
			{
				get
				{
					System.Byte? data = entity.LossTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LossTime = null;
					else entity.LossTime = Convert.ToByte(value);
				}
			}
			public System.String UnsafeCondition
			{
				get
				{
					System.String data = entity.UnsafeCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnsafeCondition = null;
					else entity.UnsafeCondition = Convert.ToString(value);
				}
			}
			public System.String UnsafeAct
			{
				get
				{
					System.String data = entity.UnsafeAct;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnsafeAct = null;
					else entity.UnsafeAct = Convert.ToString(value);
				}
			}
			public System.String PersonalIndirectCause
			{
				get
				{
					System.String data = entity.PersonalIndirectCause;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalIndirectCause = null;
					else entity.PersonalIndirectCause = Convert.ToString(value);
				}
			}
			public System.String WorkingIndirectCause
			{
				get
				{
					System.String data = entity.WorkingIndirectCause;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingIndirectCause = null;
					else entity.WorkingIndirectCause = Convert.ToString(value);
				}
			}
			public System.String ActionPlan
			{
				get
				{
					System.String data = entity.ActionPlan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionPlan = null;
					else entity.ActionPlan = Convert.ToString(value);
				}
			}
			public System.String Target
			{
				get
				{
					System.String data = entity.Target;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Target = null;
					else entity.Target = Convert.ToString(value);
				}
			}
			public System.String Authority
			{
				get
				{
					System.String data = entity.Authority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Authority = null;
					else entity.Authority = Convert.ToString(value);
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
			public System.String SREmployeeAccidentReportStatus
			{
				get
				{
					System.String data = entity.SREmployeeAccidentReportStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeAccidentReportStatus = null;
					else entity.SREmployeeAccidentReportStatus = Convert.ToString(value);
				}
			}
			private esEmployeeAccidentReports entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeAccidentReportsQuery query)
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
				throw new Exception("esEmployeeAccidentReports can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeAccidentReports : esEmployeeAccidentReports
	{
	}

	[Serializable]
	abstract public class esEmployeeAccidentReportsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAccidentReportsMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem ReportingDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.ReportingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IncidentDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.IncidentDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IncidentLocation
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.IncidentLocation, esSystemType.String);
			}
		}

		public esQueryItem SupervisorID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SupervisorID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem AgeInYear
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.AgeInYear, esSystemType.Byte);
			}
		}

		public esQueryItem AgeInMonth
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.AgeInMonth, esSystemType.Byte);
			}
		}

		public esQueryItem AgeInDay
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.AgeInDay, esSystemType.Byte);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeInjuryCategory
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeInjuryCategory, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeIncidentStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentStatus, esSystemType.String);
			}
		}

		public esQueryItem ChronologicalEvents
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.ChronologicalEvents, esSystemType.String);
			}
		}

		public esQueryItem AspectsOfTheCause
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.AspectsOfTheCause, esSystemType.String);
			}
		}

		public esQueryItem InjuredLocation
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.InjuredLocation, esSystemType.String);
			}
		}

		public esQueryItem PlaceOfTreatment
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.PlaceOfTreatment, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeIncidentType
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentType, esSystemType.String);
			}
		}

		public esQueryItem SRNeedleType
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SRNeedleType, esSystemType.String);
			}
		}

		public esQueryItem LossTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.LossTime, esSystemType.Byte);
			}
		}

		public esQueryItem UnsafeCondition
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.UnsafeCondition, esSystemType.String);
			}
		}

		public esQueryItem UnsafeAct
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.UnsafeAct, esSystemType.String);
			}
		}

		public esQueryItem PersonalIndirectCause
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.PersonalIndirectCause, esSystemType.String);
			}
		}

		public esQueryItem WorkingIndirectCause
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.WorkingIndirectCause, esSystemType.String);
			}
		}

		public esQueryItem ActionPlan
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.ActionPlan, esSystemType.String);
			}
		}

		public esQueryItem Target
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.Target, esSystemType.String);
			}
		}

		public esQueryItem Authority
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.Authority, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeAccidentReportStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeAccidentReportStatus, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeAccidentReportsCollection")]
	public partial class EmployeeAccidentReportsCollection : esEmployeeAccidentReportsCollection, IEnumerable<EmployeeAccidentReports>
	{
		public EmployeeAccidentReportsCollection()
		{

		}

		public static implicit operator List<EmployeeAccidentReports>(EmployeeAccidentReportsCollection coll)
		{
			List<EmployeeAccidentReports> list = new List<EmployeeAccidentReports>();

			foreach (EmployeeAccidentReports emp in coll)
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
				return EmployeeAccidentReportsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAccidentReportsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeAccidentReports(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeAccidentReports();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeAccidentReportsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAccidentReportsQuery();
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
		public bool Load(EmployeeAccidentReportsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeAccidentReports AddNew()
		{
			EmployeeAccidentReports entity = base.AddNewEntity() as EmployeeAccidentReports;

			return entity;
		}
		public EmployeeAccidentReports FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as EmployeeAccidentReports;
		}

		#region IEnumerable< EmployeeAccidentReports> Members

		IEnumerator<EmployeeAccidentReports> IEnumerable<EmployeeAccidentReports>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeAccidentReports;
			}
		}

		#endregion

		private EmployeeAccidentReportsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeAccidentReports' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeAccidentReports ({TransactionNo})")]
	[Serializable]
	public partial class EmployeeAccidentReports : esEmployeeAccidentReports
	{
		public EmployeeAccidentReports()
		{
		}

		public EmployeeAccidentReports(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAccidentReportsMetadata.Meta();
			}
		}

		override protected esEmployeeAccidentReportsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAccidentReportsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeAccidentReportsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAccidentReportsQuery();
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
		public bool Load(EmployeeAccidentReportsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeAccidentReportsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeAccidentReportsQuery : esEmployeeAccidentReportsQuery
	{
		public EmployeeAccidentReportsQuery()
		{

		}

		public EmployeeAccidentReportsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeAccidentReportsQuery";
		}
	}

	[Serializable]
	public partial class EmployeeAccidentReportsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeAccidentReportsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.ReportingDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.ReportingDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.IncidentDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.IncidentDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.IncidentLocation, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.IncidentLocation;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SupervisorID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SupervisorID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.PersonID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.AgeInYear, 6, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.AgeInYear;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.AgeInMonth, 7, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.AgeInMonth;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.AgeInDay, 8, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.AgeInDay;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.PositionID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeInjuryCategory, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SREmployeeInjuryCategory;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentStatus, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SREmployeeIncidentStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.ChronologicalEvents, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.ChronologicalEvents;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.AspectsOfTheCause, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.AspectsOfTheCause;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.InjuredLocation, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.InjuredLocation;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.PlaceOfTreatment, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.PlaceOfTreatment;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeIncidentType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SREmployeeIncidentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SRNeedleType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SRNeedleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.LossTime, 18, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.LossTime;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeCondition, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.UnsafeCondition;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.UnsafeAct, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.UnsafeAct;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.PersonalIndirectCause, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.PersonalIndirectCause;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.WorkingIndirectCause, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.WorkingIndirectCause;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.ActionPlan, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.ActionPlan;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.Target, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.Target;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.Authority, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.Authority;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.IsApproved, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.ApprovedByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.IsVoid, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.VoidDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.VoidByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.IsVerified, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.VerifiedByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.CreatedDateTime, 35, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.CreatedByUserID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.LastUpdateByUserID, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAccidentReportsMetadata.ColumnNames.SREmployeeAccidentReportStatus, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAccidentReportsMetadata.PropertyNames.SREmployeeAccidentReportStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeAccidentReportsMetadata Meta()
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
			public const string ReportingDateTime = "ReportingDateTime";
			public const string IncidentDateTime = "IncidentDateTime";
			public const string IncidentLocation = "IncidentLocation";
			public const string SupervisorID = "SupervisorID";
			public const string PersonID = "PersonID";
			public const string AgeInYear = "AgeInYear";
			public const string AgeInMonth = "AgeInMonth";
			public const string AgeInDay = "AgeInDay";
			public const string PositionID = "PositionID";
			public const string SREmployeeInjuryCategory = "SREmployeeInjuryCategory";
			public const string SREmployeeIncidentStatus = "SREmployeeIncidentStatus";
			public const string ChronologicalEvents = "ChronologicalEvents";
			public const string AspectsOfTheCause = "AspectsOfTheCause";
			public const string InjuredLocation = "InjuredLocation";
			public const string PlaceOfTreatment = "PlaceOfTreatment";
			public const string SREmployeeIncidentType = "SREmployeeIncidentType";
			public const string SRNeedleType = "SRNeedleType";
			public const string LossTime = "LossTime";
			public const string UnsafeCondition = "UnsafeCondition";
			public const string UnsafeAct = "UnsafeAct";
			public const string PersonalIndirectCause = "PersonalIndirectCause";
			public const string WorkingIndirectCause = "WorkingIndirectCause";
			public const string ActionPlan = "ActionPlan";
			public const string Target = "Target";
			public const string Authority = "Authority";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREmployeeAccidentReportStatus = "SREmployeeAccidentReportStatus";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string ReportingDateTime = "ReportingDateTime";
			public const string IncidentDateTime = "IncidentDateTime";
			public const string IncidentLocation = "IncidentLocation";
			public const string SupervisorID = "SupervisorID";
			public const string PersonID = "PersonID";
			public const string AgeInYear = "AgeInYear";
			public const string AgeInMonth = "AgeInMonth";
			public const string AgeInDay = "AgeInDay";
			public const string PositionID = "PositionID";
			public const string SREmployeeInjuryCategory = "SREmployeeInjuryCategory";
			public const string SREmployeeIncidentStatus = "SREmployeeIncidentStatus";
			public const string ChronologicalEvents = "ChronologicalEvents";
			public const string AspectsOfTheCause = "AspectsOfTheCause";
			public const string InjuredLocation = "InjuredLocation";
			public const string PlaceOfTreatment = "PlaceOfTreatment";
			public const string SREmployeeIncidentType = "SREmployeeIncidentType";
			public const string SRNeedleType = "SRNeedleType";
			public const string LossTime = "LossTime";
			public const string UnsafeCondition = "UnsafeCondition";
			public const string UnsafeAct = "UnsafeAct";
			public const string PersonalIndirectCause = "PersonalIndirectCause";
			public const string WorkingIndirectCause = "WorkingIndirectCause";
			public const string ActionPlan = "ActionPlan";
			public const string Target = "Target";
			public const string Authority = "Authority";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREmployeeAccidentReportStatus = "SREmployeeAccidentReportStatus";
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
			lock (typeof(EmployeeAccidentReportsMetadata))
			{
				if (EmployeeAccidentReportsMetadata.mapDelegates == null)
				{
					EmployeeAccidentReportsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeAccidentReportsMetadata.meta == null)
				{
					EmployeeAccidentReportsMetadata.meta = new EmployeeAccidentReportsMetadata();
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
				meta.AddTypeMap("ReportingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IncidentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IncidentLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AgeInYear", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("AgeInMonth", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("AgeInDay", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeInjuryCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeIncidentStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChronologicalEvents", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AspectsOfTheCause", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InjuredLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlaceOfTreatment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeIncidentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNeedleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LossTime", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("UnsafeCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UnsafeAct", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonalIndirectCause", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingIndirectCause", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionPlan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Target", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Authority", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeAccidentReportStatus", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeAccidentReports";
				meta.Destination = "EmployeeAccidentReports";
				meta.spInsert = "proc_EmployeeAccidentReportsInsert";
				meta.spUpdate = "proc_EmployeeAccidentReportsUpdate";
				meta.spDelete = "proc_EmployeeAccidentReportsDelete";
				meta.spLoadAll = "proc_EmployeeAccidentReportsLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeAccidentReportsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeAccidentReportsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
