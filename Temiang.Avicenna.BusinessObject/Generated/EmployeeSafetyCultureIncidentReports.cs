/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/23/2022 3:55:40 PM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReports DetachEntity(EmployeeSafetyCultureIncidentReports entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReports;
		}

		virtual public EmployeeSafetyCultureIncidentReports AttachEntity(EmployeeSafetyCultureIncidentReports entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReports;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReports this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReports;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReports);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReports : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReports()
		{
		}

		public esEmployeeSafetyCultureIncidentReports(DataRow row)
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
			esEmployeeSafetyCultureIncidentReportsQuery query = this.GetDynamicQuery();
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
						case "ReportDescription": this.str.ReportDescription = (string)value; break;
						case "ReportDate": this.str.ReportDate = (string)value; break;
						case "VictimPersonID": this.str.VictimPersonID = (string)value; break;
						case "VictimSRProfessionType": this.str.VictimSRProfessionType = (string)value; break;
						case "VictimOrganizationID": this.str.VictimOrganizationID = (string)value; break;
						case "VictimSubOrganizationID": this.str.VictimSubOrganizationID = (string)value; break;
						case "VictimSubDivisonID": this.str.VictimSubDivisonID = (string)value; break;
						case "VictimServiceUnitID": this.str.VictimServiceUnitID = (string)value; break;
						case "SRIncidentReportStatus": this.str.SRIncidentReportStatus = (string)value; break;
						case "Resume": this.str.Resume = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReportDate":

							if (value == null || value is System.DateTime)
								this.ReportDate = (System.DateTime?)value;
							break;
						case "VictimPersonID":

							if (value == null || value is System.Int32)
								this.VictimPersonID = (System.Int32?)value;
							break;
						case "VictimOrganizationID":

							if (value == null || value is System.Int32)
								this.VictimOrganizationID = (System.Int32?)value;
							break;
						case "VictimSubOrganizationID":

							if (value == null || value is System.Int32)
								this.VictimSubOrganizationID = (System.Int32?)value;
							break;
						case "VictimSubDivisonID":

							if (value == null || value is System.Int32)
								this.VictimSubDivisonID = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeSafetyCultureIncidentReports.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ReportDescription
		/// </summary>
		virtual public System.String ReportDescription
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDescription);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDescription, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ReportDate
		/// </summary>
		virtual public System.DateTime? ReportDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimPersonID
		/// </summary>
		virtual public System.Int32? VictimPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimSRProfessionType
		/// </summary>
		virtual public System.String VictimSRProfessionType
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSRProfessionType);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSRProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimOrganizationID
		/// </summary>
		virtual public System.Int32? VictimOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimSubOrganizationID
		/// </summary>
		virtual public System.Int32? VictimSubOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimSubDivisonID
		/// </summary>
		virtual public System.Int32? VictimSubDivisonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubDivisonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubDivisonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VictimServiceUnitID
		/// </summary>
		virtual public System.String VictimServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimServiceUnitID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.SRIncidentReportStatus
		/// </summary>
		virtual public System.String SRIncidentReportStatus
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.SRIncidentReportStatus);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.SRIncidentReportStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.Resume
		/// </summary>
		virtual public System.String Resume
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.Resume);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.Resume, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReports.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReports entity)
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
			public System.String ReportDescription
			{
				get
				{
					System.String data = entity.ReportDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportDescription = null;
					else entity.ReportDescription = Convert.ToString(value);
				}
			}
			public System.String ReportDate
			{
				get
				{
					System.DateTime? data = entity.ReportDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportDate = null;
					else entity.ReportDate = Convert.ToDateTime(value);
				}
			}
			public System.String VictimPersonID
			{
				get
				{
					System.Int32? data = entity.VictimPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimPersonID = null;
					else entity.VictimPersonID = Convert.ToInt32(value);
				}
			}
			public System.String VictimSRProfessionType
			{
				get
				{
					System.String data = entity.VictimSRProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimSRProfessionType = null;
					else entity.VictimSRProfessionType = Convert.ToString(value);
				}
			}
			public System.String VictimOrganizationID
			{
				get
				{
					System.Int32? data = entity.VictimOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimOrganizationID = null;
					else entity.VictimOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String VictimSubOrganizationID
			{
				get
				{
					System.Int32? data = entity.VictimSubOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimSubOrganizationID = null;
					else entity.VictimSubOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String VictimSubDivisonID
			{
				get
				{
					System.Int32? data = entity.VictimSubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimSubDivisonID = null;
					else entity.VictimSubDivisonID = Convert.ToInt32(value);
				}
			}
			public System.String VictimServiceUnitID
			{
				get
				{
					System.String data = entity.VictimServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VictimServiceUnitID = null;
					else entity.VictimServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String SRIncidentReportStatus
			{
				get
				{
					System.String data = entity.SRIncidentReportStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentReportStatus = null;
					else entity.SRIncidentReportStatus = Convert.ToString(value);
				}
			}
			public System.String Resume
			{
				get
				{
					System.String data = entity.Resume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Resume = null;
					else entity.Resume = Convert.ToString(value);
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
			public System.String ClosedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime = null;
					else entity.ClosedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedByUserID
			{
				get
				{
					System.String data = entity.ClosedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedByUserID = null;
					else entity.ClosedByUserID = Convert.ToString(value);
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
			private esEmployeeSafetyCultureIncidentReports entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReports can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReports : esEmployeeSafetyCultureIncidentReports
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		}

		public esQueryItem ReportDescription
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDescription, esSystemType.String);
			}
		}

		public esQueryItem ReportDate
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VictimPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem VictimSRProfessionType
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSRProfessionType, esSystemType.String);
			}
		}

		public esQueryItem VictimOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem VictimSubOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem VictimSubDivisonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubDivisonID, esSystemType.Int32);
			}
		}

		public esQueryItem VictimServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentReportStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.SRIncidentReportStatus, esSystemType.String);
			}
		}

		public esQueryItem Resume
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.Resume, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsCollection : esEmployeeSafetyCultureIncidentReportsCollection, IEnumerable<EmployeeSafetyCultureIncidentReports>
	{
		public EmployeeSafetyCultureIncidentReportsCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReports>(EmployeeSafetyCultureIncidentReportsCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReports> list = new List<EmployeeSafetyCultureIncidentReports>();

			foreach (EmployeeSafetyCultureIncidentReports emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReports(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReports();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReports AddNew()
		{
			EmployeeSafetyCultureIncidentReports entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReports;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReports FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as EmployeeSafetyCultureIncidentReports;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReports> Members

		IEnumerator<EmployeeSafetyCultureIncidentReports> IEnumerable<EmployeeSafetyCultureIncidentReports>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReports;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReports' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReports ({TransactionNo})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReports : esEmployeeSafetyCultureIncidentReports
	{
		public EmployeeSafetyCultureIncidentReports()
		{
		}

		public EmployeeSafetyCultureIncidentReports(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsQuery : esEmployeeSafetyCultureIncidentReportsQuery
	{
		public EmployeeSafetyCultureIncidentReportsQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.QuestionFormID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDescription, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ReportDescription;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ReportDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ReportDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimPersonID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimPersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSRProfessionType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimSRProfessionType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimOrganizationID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubOrganizationID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimSubOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimSubDivisonID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimSubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VictimServiceUnitID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VictimServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.SRIncidentReportStatus, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.SRIncidentReportStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.Resume, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.Resume;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.CreatedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVerified, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VerifiedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsVoid, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.VoidByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.IsClosed, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.ClosedByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMetadata.ColumnNames.LastUpdateByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsMetadata Meta()
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
			public const string ReportDescription = "ReportDescription";
			public const string ReportDate = "ReportDate";
			public const string VictimPersonID = "VictimPersonID";
			public const string VictimSRProfessionType = "VictimSRProfessionType";
			public const string VictimOrganizationID = "VictimOrganizationID";
			public const string VictimSubOrganizationID = "VictimSubOrganizationID";
			public const string VictimSubDivisonID = "VictimSubDivisonID";
			public const string VictimServiceUnitID = "VictimServiceUnitID";
			public const string SRIncidentReportStatus = "SRIncidentReportStatus";
			public const string Resume = "Resume";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string ReportDescription = "ReportDescription";
			public const string ReportDate = "ReportDate";
			public const string VictimPersonID = "VictimPersonID";
			public const string VictimSRProfessionType = "VictimSRProfessionType";
			public const string VictimOrganizationID = "VictimOrganizationID";
			public const string VictimSubOrganizationID = "VictimSubOrganizationID";
			public const string VictimSubDivisonID = "VictimSubDivisonID";
			public const string VictimServiceUnitID = "VictimServiceUnitID";
			public const string SRIncidentReportStatus = "SRIncidentReportStatus";
			public const string Resume = "Resume";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsMetadata.meta = new EmployeeSafetyCultureIncidentReportsMetadata();
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
				meta.AddTypeMap("ReportDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VictimPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VictimSRProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VictimOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VictimSubOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VictimSubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VictimServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentReportStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Resume", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSafetyCultureIncidentReports";
				meta.Destination = "EmployeeSafetyCultureIncidentReports";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
