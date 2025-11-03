/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/13/2022 4:00:35 PM
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
	abstract public class esComplaintResponseTimeCollection : esEntityCollectionWAuditLog
	{
		public esComplaintResponseTimeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ComplaintResponseTimeCollection";
		}

		#region Query Logic
		protected void InitQuery(esComplaintResponseTimeQuery query)
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
			this.InitQuery(query as esComplaintResponseTimeQuery);
		}
		#endregion

		virtual public ComplaintResponseTime DetachEntity(ComplaintResponseTime entity)
		{
			return base.DetachEntity(entity) as ComplaintResponseTime;
		}

		virtual public ComplaintResponseTime AttachEntity(ComplaintResponseTime entity)
		{
			return base.AttachEntity(entity) as ComplaintResponseTime;
		}

		virtual public void Combine(ComplaintResponseTimeCollection collection)
		{
			base.Combine(collection);
		}

		new public ComplaintResponseTime this[int index]
		{
			get
			{
				return base[index] as ComplaintResponseTime;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ComplaintResponseTime);
		}
	}

	[Serializable]
	abstract public class esComplaintResponseTime : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esComplaintResponseTimeQuery GetDynamicQuery()
		{
			return null;
		}

		public esComplaintResponseTime()
		{
		}

		public esComplaintResponseTime(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String complaintNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(complaintNo);
			else
				return LoadByPrimaryKeyStoredProcedure(complaintNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String complaintNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(complaintNo);
			else
				return LoadByPrimaryKeyStoredProcedure(complaintNo);
		}

		private bool LoadByPrimaryKeyDynamic(String complaintNo)
		{
			esComplaintResponseTimeQuery query = this.GetDynamicQuery();
			query.Where(query.ComplaintNo == complaintNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String complaintNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ComplaintNo", complaintNo);
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
						case "ComplaintNo": this.str.ComplaintNo = (string)value; break;
						case "ComplaintDate": this.str.ComplaintDate = (string)value; break;
						case "CustomerName": this.str.CustomerName = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "CustomerAddress": this.str.CustomerAddress = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "ComplaintDescription": this.str.ComplaintDescription = (string)value; break;
						case "SRComplaintRiskGrading": this.str.SRComplaintRiskGrading = (string)value; break;
						case "ReportReceivedMarketingDate": this.str.ReportReceivedMarketingDate = (string)value; break;
						case "ReportReceivedMarketingBy": this.str.ReportReceivedMarketingBy = (string)value; break;
						case "ReportReceivedUnitDate": this.str.ReportReceivedUnitDate = (string)value; break;
						case "ReportReceivedUnitBy": this.str.ReportReceivedUnitBy = (string)value; break;
						case "CorrectiveActionDate": this.str.CorrectiveActionDate = (string)value; break;
						case "CorrectiveActionBy": this.str.CorrectiveActionBy = (string)value; break;
						case "CorrectiveAction": this.str.CorrectiveAction = (string)value; break;
						case "PreventiveAction": this.str.PreventiveAction = (string)value; break;
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
						case "ComplaintDate":

							if (value == null || value is System.DateTime)
								this.ComplaintDate = (System.DateTime?)value;
							break;
						case "ReportReceivedMarketingDate":

							if (value == null || value is System.DateTime)
								this.ReportReceivedMarketingDate = (System.DateTime?)value;
							break;
						case "ReportReceivedUnitDate":

							if (value == null || value is System.DateTime)
								this.ReportReceivedUnitDate = (System.DateTime?)value;
							break;
						case "CorrectiveActionDate":

							if (value == null || value is System.DateTime)
								this.CorrectiveActionDate = (System.DateTime?)value;
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
		/// Maps to ComplaintResponseTime.ComplaintNo
		/// </summary>
		virtual public System.String ComplaintNo
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ComplaintNo);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ComplaintNo, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ComplaintDate
		/// </summary>
		virtual public System.DateTime? ComplaintDate
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDate);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDate, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.CustomerName
		/// </summary>
		virtual public System.String CustomerName
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CustomerName);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CustomerName, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.CustomerAddress
		/// </summary>
		virtual public System.String CustomerAddress
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CustomerAddress);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CustomerAddress, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ComplaintDescription
		/// </summary>
		virtual public System.String ComplaintDescription
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDescription);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDescription, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.SRComplaintRiskGrading
		/// </summary>
		virtual public System.String SRComplaintRiskGrading
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.SRComplaintRiskGrading);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.SRComplaintRiskGrading, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ReportReceivedMarketingDate
		/// </summary>
		virtual public System.DateTime? ReportReceivedMarketingDate
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingDate);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingDate, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ReportReceivedMarketingBy
		/// </summary>
		virtual public System.String ReportReceivedMarketingBy
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingBy);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingBy, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ReportReceivedUnitDate
		/// </summary>
		virtual public System.DateTime? ReportReceivedUnitDate
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitDate);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitDate, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ReportReceivedUnitBy
		/// </summary>
		virtual public System.String ReportReceivedUnitBy
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitBy);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitBy, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.CorrectiveActionDate
		/// </summary>
		virtual public System.DateTime? CorrectiveActionDate
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionDate);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionDate, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.CorrectiveActionBy
		/// </summary>
		virtual public System.String CorrectiveActionBy
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionBy);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionBy, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.CorrectiveAction
		/// </summary>
		virtual public System.String CorrectiveAction
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveAction);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveAction, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.PreventiveAction
		/// </summary>
		virtual public System.String PreventiveAction
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PreventiveAction);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.PreventiveAction, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ComplaintResponseTimeMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ComplaintResponseTimeMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ComplaintResponseTimeMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ComplaintResponseTimeMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ComplaintResponseTime.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esComplaintResponseTime entity)
			{
				this.entity = entity;
			}
			public System.String ComplaintNo
			{
				get
				{
					System.String data = entity.ComplaintNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComplaintNo = null;
					else entity.ComplaintNo = Convert.ToString(value);
				}
			}
			public System.String ComplaintDate
			{
				get
				{
					System.DateTime? data = entity.ComplaintDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComplaintDate = null;
					else entity.ComplaintDate = Convert.ToDateTime(value);
				}
			}
			public System.String CustomerName
			{
				get
				{
					System.String data = entity.CustomerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerName = null;
					else entity.CustomerName = Convert.ToString(value);
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
			public System.String CustomerAddress
			{
				get
				{
					System.String data = entity.CustomerAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerAddress = null;
					else entity.CustomerAddress = Convert.ToString(value);
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
			public System.String ComplaintDescription
			{
				get
				{
					System.String data = entity.ComplaintDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComplaintDescription = null;
					else entity.ComplaintDescription = Convert.ToString(value);
				}
			}
			public System.String SRComplaintRiskGrading
			{
				get
				{
					System.String data = entity.SRComplaintRiskGrading;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRComplaintRiskGrading = null;
					else entity.SRComplaintRiskGrading = Convert.ToString(value);
				}
			}
			public System.String ReportReceivedMarketingDate
			{
				get
				{
					System.DateTime? data = entity.ReportReceivedMarketingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportReceivedMarketingDate = null;
					else entity.ReportReceivedMarketingDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReportReceivedMarketingBy
			{
				get
				{
					System.String data = entity.ReportReceivedMarketingBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportReceivedMarketingBy = null;
					else entity.ReportReceivedMarketingBy = Convert.ToString(value);
				}
			}
			public System.String ReportReceivedUnitDate
			{
				get
				{
					System.DateTime? data = entity.ReportReceivedUnitDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportReceivedUnitDate = null;
					else entity.ReportReceivedUnitDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReportReceivedUnitBy
			{
				get
				{
					System.String data = entity.ReportReceivedUnitBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportReceivedUnitBy = null;
					else entity.ReportReceivedUnitBy = Convert.ToString(value);
				}
			}
			public System.String CorrectiveActionDate
			{
				get
				{
					System.DateTime? data = entity.CorrectiveActionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CorrectiveActionDate = null;
					else entity.CorrectiveActionDate = Convert.ToDateTime(value);
				}
			}
			public System.String CorrectiveActionBy
			{
				get
				{
					System.String data = entity.CorrectiveActionBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CorrectiveActionBy = null;
					else entity.CorrectiveActionBy = Convert.ToString(value);
				}
			}
			public System.String CorrectiveAction
			{
				get
				{
					System.String data = entity.CorrectiveAction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CorrectiveAction = null;
					else entity.CorrectiveAction = Convert.ToString(value);
				}
			}
			public System.String PreventiveAction
			{
				get
				{
					System.String data = entity.PreventiveAction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreventiveAction = null;
					else entity.PreventiveAction = Convert.ToString(value);
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
			private esComplaintResponseTime entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esComplaintResponseTimeQuery query)
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
				throw new Exception("esComplaintResponseTime can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ComplaintResponseTime : esComplaintResponseTime
	{
	}

	[Serializable]
	abstract public class esComplaintResponseTimeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ComplaintResponseTimeMetadata.Meta();
			}
		}

		public esQueryItem ComplaintNo
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ComplaintNo, esSystemType.String);
			}
		}

		public esQueryItem ComplaintDate
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ComplaintDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CustomerName
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.CustomerName, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem CustomerAddress
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.CustomerAddress, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem ComplaintDescription
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ComplaintDescription, esSystemType.String);
			}
		}

		public esQueryItem SRComplaintRiskGrading
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.SRComplaintRiskGrading, esSystemType.String);
			}
		}

		public esQueryItem ReportReceivedMarketingDate
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReportReceivedMarketingBy
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingBy, esSystemType.String);
			}
		}

		public esQueryItem ReportReceivedUnitDate
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReportReceivedUnitBy
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitBy, esSystemType.String);
			}
		}

		public esQueryItem CorrectiveActionDate
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CorrectiveActionBy
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionBy, esSystemType.String);
			}
		}

		public esQueryItem CorrectiveAction
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.CorrectiveAction, esSystemType.String);
			}
		}

		public esQueryItem PreventiveAction
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.PreventiveAction, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ComplaintResponseTimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ComplaintResponseTimeCollection")]
	public partial class ComplaintResponseTimeCollection : esComplaintResponseTimeCollection, IEnumerable<ComplaintResponseTime>
	{
		public ComplaintResponseTimeCollection()
		{

		}

		public static implicit operator List<ComplaintResponseTime>(ComplaintResponseTimeCollection coll)
		{
			List<ComplaintResponseTime> list = new List<ComplaintResponseTime>();

			foreach (ComplaintResponseTime emp in coll)
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
				return ComplaintResponseTimeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ComplaintResponseTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ComplaintResponseTime(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ComplaintResponseTime();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ComplaintResponseTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ComplaintResponseTimeQuery();
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
		public bool Load(ComplaintResponseTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ComplaintResponseTime AddNew()
		{
			ComplaintResponseTime entity = base.AddNewEntity() as ComplaintResponseTime;

			return entity;
		}
		public ComplaintResponseTime FindByPrimaryKey(String complaintNo)
		{
			return base.FindByPrimaryKey(complaintNo) as ComplaintResponseTime;
		}

		#region IEnumerable< ComplaintResponseTime> Members

		IEnumerator<ComplaintResponseTime> IEnumerable<ComplaintResponseTime>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ComplaintResponseTime;
			}
		}

		#endregion

		private ComplaintResponseTimeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ComplaintResponseTime' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ComplaintResponseTime ({ComplaintNo})")]
	[Serializable]
	public partial class ComplaintResponseTime : esComplaintResponseTime
	{
		public ComplaintResponseTime()
		{
		}

		public ComplaintResponseTime(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ComplaintResponseTimeMetadata.Meta();
			}
		}

		override protected esComplaintResponseTimeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ComplaintResponseTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ComplaintResponseTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ComplaintResponseTimeQuery();
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
		public bool Load(ComplaintResponseTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ComplaintResponseTimeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ComplaintResponseTimeQuery : esComplaintResponseTimeQuery
	{
		public ComplaintResponseTimeQuery()
		{

		}

		public ComplaintResponseTimeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ComplaintResponseTimeQuery";
		}
	}

	[Serializable]
	public partial class ComplaintResponseTimeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ComplaintResponseTimeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ComplaintNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ComplaintNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ComplaintDate;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.CustomerName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.CustomerName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.PatientID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.CustomerAddress, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.CustomerAddress;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ServiceUnitID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.PhoneNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ComplaintDescription, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ComplaintDescription;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.SRComplaintRiskGrading, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.SRComplaintRiskGrading;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ReportReceivedMarketingDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedMarketingBy, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ReportReceivedMarketingBy;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ReportReceivedUnitDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ReportReceivedUnitBy, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ReportReceivedUnitBy;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.CorrectiveActionDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveActionBy, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.CorrectiveActionBy;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.CorrectiveAction, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.CorrectiveAction;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.PreventiveAction, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.PreventiveAction;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.IsApproved, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ApprovedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.ApprovedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.IsVoid, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.VoidDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.VoidByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplaintResponseTimeMetadata.ColumnNames.LastUpdateByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplaintResponseTimeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ComplaintResponseTimeMetadata Meta()
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
			public const string ComplaintNo = "ComplaintNo";
			public const string ComplaintDate = "ComplaintDate";
			public const string CustomerName = "CustomerName";
			public const string PatientID = "PatientID";
			public const string CustomerAddress = "CustomerAddress";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PhoneNo = "PhoneNo";
			public const string ComplaintDescription = "ComplaintDescription";
			public const string SRComplaintRiskGrading = "SRComplaintRiskGrading";
			public const string ReportReceivedMarketingDate = "ReportReceivedMarketingDate";
			public const string ReportReceivedMarketingBy = "ReportReceivedMarketingBy";
			public const string ReportReceivedUnitDate = "ReportReceivedUnitDate";
			public const string ReportReceivedUnitBy = "ReportReceivedUnitBy";
			public const string CorrectiveActionDate = "CorrectiveActionDate";
			public const string CorrectiveActionBy = "CorrectiveActionBy";
			public const string CorrectiveAction = "CorrectiveAction";
			public const string PreventiveAction = "PreventiveAction";
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
			public const string ComplaintNo = "ComplaintNo";
			public const string ComplaintDate = "ComplaintDate";
			public const string CustomerName = "CustomerName";
			public const string PatientID = "PatientID";
			public const string CustomerAddress = "CustomerAddress";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PhoneNo = "PhoneNo";
			public const string ComplaintDescription = "ComplaintDescription";
			public const string SRComplaintRiskGrading = "SRComplaintRiskGrading";
			public const string ReportReceivedMarketingDate = "ReportReceivedMarketingDate";
			public const string ReportReceivedMarketingBy = "ReportReceivedMarketingBy";
			public const string ReportReceivedUnitDate = "ReportReceivedUnitDate";
			public const string ReportReceivedUnitBy = "ReportReceivedUnitBy";
			public const string CorrectiveActionDate = "CorrectiveActionDate";
			public const string CorrectiveActionBy = "CorrectiveActionBy";
			public const string CorrectiveAction = "CorrectiveAction";
			public const string PreventiveAction = "PreventiveAction";
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
			lock (typeof(ComplaintResponseTimeMetadata))
			{
				if (ComplaintResponseTimeMetadata.mapDelegates == null)
				{
					ComplaintResponseTimeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ComplaintResponseTimeMetadata.meta == null)
				{
					ComplaintResponseTimeMetadata.meta = new ComplaintResponseTimeMetadata();
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

				meta.AddTypeMap("ComplaintNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ComplaintDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CustomerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomerAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ComplaintDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRComplaintRiskGrading", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportReceivedMarketingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReportReceivedMarketingBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportReceivedUnitDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReportReceivedUnitBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CorrectiveActionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CorrectiveActionBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CorrectiveAction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreventiveAction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ComplaintResponseTime";
				meta.Destination = "ComplaintResponseTime";
				meta.spInsert = "proc_ComplaintResponseTimeInsert";
				meta.spUpdate = "proc_ComplaintResponseTimeUpdate";
				meta.spDelete = "proc_ComplaintResponseTimeDelete";
				meta.spLoadAll = "proc_ComplaintResponseTimeLoadAll";
				meta.spLoadByPrimaryKey = "proc_ComplaintResponseTimeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ComplaintResponseTimeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
