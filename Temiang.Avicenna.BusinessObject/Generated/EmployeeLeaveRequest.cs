/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/25/2023 5:08:05 PM
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
	abstract public class esEmployeeLeaveRequestCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLeaveRequestCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeLeaveRequestCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveRequestQuery query)
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
			this.InitQuery(query as esEmployeeLeaveRequestQuery);
		}
		#endregion

		virtual public EmployeeLeaveRequest DetachEntity(EmployeeLeaveRequest entity)
		{
			return base.DetachEntity(entity) as EmployeeLeaveRequest;
		}

		virtual public EmployeeLeaveRequest AttachEntity(EmployeeLeaveRequest entity)
		{
			return base.AttachEntity(entity) as EmployeeLeaveRequest;
		}

		virtual public void Combine(EmployeeLeaveRequestCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeLeaveRequest this[int index]
		{
			get
			{
				return base[index] as EmployeeLeaveRequest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLeaveRequest);
		}
	}

	[Serializable]
	abstract public class esEmployeeLeaveRequest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLeaveRequestQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLeaveRequest()
		{
		}

		public esEmployeeLeaveRequest(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 leaveRequestID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(leaveRequestID);
			else
				return LoadByPrimaryKeyStoredProcedure(leaveRequestID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 leaveRequestID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(leaveRequestID);
			else
				return LoadByPrimaryKeyStoredProcedure(leaveRequestID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 leaveRequestID)
		{
			esEmployeeLeaveRequestQuery query = this.GetDynamicQuery();
			query.Where(query.LeaveRequestID == leaveRequestID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 leaveRequestID)
		{
			esParameters parms = new esParameters();
			parms.Add("LeaveRequestID", leaveRequestID);
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
						case "LeaveRequestID": this.str.LeaveRequestID = (string)value; break;
						case "RequestDate": this.str.RequestDate = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "EmployeeLeaveID": this.str.EmployeeLeaveID = (string)value; break;
						case "RequestLeaveDateFrom": this.str.RequestLeaveDateFrom = (string)value; break;
						case "RequestLeaveDateTo": this.str.RequestLeaveDateTo = (string)value; break;
						case "RequestDays": this.str.RequestDays = (string)value; break;
						case "RequestWorkingDate": this.str.RequestWorkingDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsRequestApproved": this.str.IsRequestApproved = (string)value; break;
						case "ApprovedLeaveDateFrom": this.str.ApprovedLeaveDateFrom = (string)value; break;
						case "ApprovedLeaveDateTo": this.str.ApprovedLeaveDateTo = (string)value; break;
						case "ApprovedDays": this.str.ApprovedDays = (string)value; break;
						case "ApprovedWorkingDate": this.str.ApprovedWorkingDate = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsValidated": this.str.IsValidated = (string)value; break;
						case "ValidatedDateTime": this.str.ValidatedDateTime = (string)value; break;
						case "ValidatedByUserID": this.str.ValidatedByUserID = (string)value; break;
						case "ReplacementPersonID": this.str.ReplacementPersonID = (string)value; break;
						case "RejectedReason": this.str.RejectedReason = (string)value; break;
						case "IsValidated2": this.str.IsValidated2 = (string)value; break;
						case "Validated2DateTime": this.str.Validated2DateTime = (string)value; break;
						case "Validated2ByUserID": this.str.Validated2ByUserID = (string)value; break;
						case "PayCutDays": this.str.PayCutDays = (string)value; break;
						case "SRWorkingDay": this.str.SRWorkingDay = (string)value; break;
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
						case "IsPayCut": this.str.IsPayCut = (string)value; break;
						case "IsValidated1": this.str.IsValidated1 = (string)value; break;
						case "Validated1DateTime": this.str.Validated1DateTime = (string)value; break;
						case "Validated1ByUserID": this.str.Validated1ByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LeaveRequestID":

							if (value == null || value is System.Int64)
								this.LeaveRequestID = (System.Int64?)value;
							break;
						case "RequestDate":

							if (value == null || value is System.DateTime)
								this.RequestDate = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "EmployeeLeaveID":

							if (value == null || value is System.Int64)
								this.EmployeeLeaveID = (System.Int64?)value;
							break;
						case "RequestLeaveDateFrom":

							if (value == null || value is System.DateTime)
								this.RequestLeaveDateFrom = (System.DateTime?)value;
							break;
						case "RequestLeaveDateTo":

							if (value == null || value is System.DateTime)
								this.RequestLeaveDateTo = (System.DateTime?)value;
							break;
						case "RequestDays":

							if (value == null || value is System.Int32)
								this.RequestDays = (System.Int32?)value;
							break;
						case "RequestWorkingDate":

							if (value == null || value is System.DateTime)
								this.RequestWorkingDate = (System.DateTime?)value;
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
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsRequestApproved":

							if (value == null || value is System.Boolean)
								this.IsRequestApproved = (System.Boolean?)value;
							break;
						case "ApprovedLeaveDateFrom":

							if (value == null || value is System.DateTime)
								this.ApprovedLeaveDateFrom = (System.DateTime?)value;
							break;
						case "ApprovedLeaveDateTo":

							if (value == null || value is System.DateTime)
								this.ApprovedLeaveDateTo = (System.DateTime?)value;
							break;
						case "ApprovedDays":

							if (value == null || value is System.Int32)
								this.ApprovedDays = (System.Int32?)value;
							break;
						case "ApprovedWorkingDate":

							if (value == null || value is System.DateTime)
								this.ApprovedWorkingDate = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsValidated":

							if (value == null || value is System.Boolean)
								this.IsValidated = (System.Boolean?)value;
							break;
						case "ValidatedDateTime":

							if (value == null || value is System.DateTime)
								this.ValidatedDateTime = (System.DateTime?)value;
							break;
						case "ReplacementPersonID":

							if (value == null || value is System.Int32)
								this.ReplacementPersonID = (System.Int32?)value;
							break;
						case "IsValidated2":

							if (value == null || value is System.Boolean)
								this.IsValidated2 = (System.Boolean?)value;
							break;
						case "Validated2DateTime":

							if (value == null || value is System.DateTime)
								this.Validated2DateTime = (System.DateTime?)value;
							break;
						case "PayCutDays":

							if (value == null || value is System.Int32)
								this.PayCutDays = (System.Int32?)value;
							break;
						case "PayrollPeriodID":

							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						case "IsPayCut":

							if (value == null || value is System.Boolean)
								this.IsPayCut = (System.Boolean?)value;
							break;
						case "IsValidated1":

							if (value == null || value is System.Boolean)
								this.IsValidated1 = (System.Boolean?)value;
							break;
						case "Validated1DateTime":

							if (value == null || value is System.DateTime)
								this.Validated1DateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeLeaveRequest.LeaveRequestID
		/// </summary>
		virtual public System.Int64? LeaveRequestID
		{
			get
			{
				return base.GetSystemInt64(EmployeeLeaveRequestMetadata.ColumnNames.LeaveRequestID);
			}

			set
			{
				base.SetSystemInt64(EmployeeLeaveRequestMetadata.ColumnNames.LeaveRequestID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RequestDate
		/// </summary>
		virtual public System.DateTime? RequestDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.EmployeeLeaveID
		/// </summary>
		virtual public System.Int64? EmployeeLeaveID
		{
			get
			{
				return base.GetSystemInt64(EmployeeLeaveRequestMetadata.ColumnNames.EmployeeLeaveID);
			}

			set
			{
				base.SetSystemInt64(EmployeeLeaveRequestMetadata.ColumnNames.EmployeeLeaveID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RequestLeaveDateFrom
		/// </summary>
		virtual public System.DateTime? RequestLeaveDateFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RequestLeaveDateTo
		/// </summary>
		virtual public System.DateTime? RequestLeaveDateTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RequestDays
		/// </summary>
		virtual public System.Int32? RequestDays
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.RequestDays);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.RequestDays, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RequestWorkingDate
		/// </summary>
		virtual public System.DateTime? RequestWorkingDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestWorkingDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.RequestWorkingDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsRequestApproved
		/// </summary>
		virtual public System.Boolean? IsRequestApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsRequestApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsRequestApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedLeaveDateFrom
		/// </summary>
		virtual public System.DateTime? ApprovedLeaveDateFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedLeaveDateTo
		/// </summary>
		virtual public System.DateTime? ApprovedLeaveDateTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedDays
		/// </summary>
		virtual public System.Int32? ApprovedDays
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDays);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDays, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ApprovedWorkingDate
		/// </summary>
		virtual public System.DateTime? ApprovedWorkingDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedWorkingDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedWorkingDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsValidated
		/// </summary>
		virtual public System.Boolean? IsValidated
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ValidatedDateTime
		/// </summary>
		virtual public System.DateTime? ValidatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ValidatedByUserID
		/// </summary>
		virtual public System.String ValidatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.ReplacementPersonID
		/// </summary>
		virtual public System.Int32? ReplacementPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.ReplacementPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.ReplacementPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.RejectedReason
		/// </summary>
		virtual public System.String RejectedReason
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.RejectedReason);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.RejectedReason, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsValidated2
		/// </summary>
		virtual public System.Boolean? IsValidated2
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated2);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated2, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.Validated2DateTime
		/// </summary>
		virtual public System.DateTime? Validated2DateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.Validated2DateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.Validated2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.Validated2ByUserID
		/// </summary>
		virtual public System.String Validated2ByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Validated2ByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Validated2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.PayCutDays
		/// </summary>
		virtual public System.Int32? PayCutDays
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PayCutDays);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PayCutDays, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.SRWorkingDay
		/// </summary>
		virtual public System.String SRWorkingDay
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.SRWorkingDay);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.SRWorkingDay, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PayrollPeriodID);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveRequestMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsPayCut
		/// </summary>
		virtual public System.Boolean? IsPayCut
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsPayCut);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsPayCut, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.IsValidated1
		/// </summary>
		virtual public System.Boolean? IsValidated1
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated1);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated1, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.Validated1DateTime
		/// </summary>
		virtual public System.DateTime? Validated1DateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.Validated1DateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveRequestMetadata.ColumnNames.Validated1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeaveRequest.Validated1ByUserID
		/// </summary>
		virtual public System.String Validated1ByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Validated1ByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveRequestMetadata.ColumnNames.Validated1ByUserID, value);
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
			public esStrings(esEmployeeLeaveRequest entity)
			{
				this.entity = entity;
			}
			public System.String LeaveRequestID
			{
				get
				{
					System.Int64? data = entity.LeaveRequestID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveRequestID = null;
					else entity.LeaveRequestID = Convert.ToInt64(value);
				}
			}
			public System.String RequestDate
			{
				get
				{
					System.DateTime? data = entity.RequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDate = null;
					else entity.RequestDate = Convert.ToDateTime(value);
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
			public System.String EmployeeLeaveID
			{
				get
				{
					System.Int64? data = entity.EmployeeLeaveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLeaveID = null;
					else entity.EmployeeLeaveID = Convert.ToInt64(value);
				}
			}
			public System.String RequestLeaveDateFrom
			{
				get
				{
					System.DateTime? data = entity.RequestLeaveDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestLeaveDateFrom = null;
					else entity.RequestLeaveDateFrom = Convert.ToDateTime(value);
				}
			}
			public System.String RequestLeaveDateTo
			{
				get
				{
					System.DateTime? data = entity.RequestLeaveDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestLeaveDateTo = null;
					else entity.RequestLeaveDateTo = Convert.ToDateTime(value);
				}
			}
			public System.String RequestDays
			{
				get
				{
					System.Int32? data = entity.RequestDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDays = null;
					else entity.RequestDays = Convert.ToInt32(value);
				}
			}
			public System.String RequestWorkingDate
			{
				get
				{
					System.DateTime? data = entity.RequestWorkingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestWorkingDate = null;
					else entity.RequestWorkingDate = Convert.ToDateTime(value);
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
			public System.String IsRequestApproved
			{
				get
				{
					System.Boolean? data = entity.IsRequestApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRequestApproved = null;
					else entity.IsRequestApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedLeaveDateFrom
			{
				get
				{
					System.DateTime? data = entity.ApprovedLeaveDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedLeaveDateFrom = null;
					else entity.ApprovedLeaveDateFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedLeaveDateTo
			{
				get
				{
					System.DateTime? data = entity.ApprovedLeaveDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedLeaveDateTo = null;
					else entity.ApprovedLeaveDateTo = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedDays
			{
				get
				{
					System.Int32? data = entity.ApprovedDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDays = null;
					else entity.ApprovedDays = Convert.ToInt32(value);
				}
			}
			public System.String ApprovedWorkingDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedWorkingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedWorkingDate = null;
					else entity.ApprovedWorkingDate = Convert.ToDateTime(value);
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
			public System.String IsValidated
			{
				get
				{
					System.Boolean? data = entity.IsValidated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidated = null;
					else entity.IsValidated = Convert.ToBoolean(value);
				}
			}
			public System.String ValidatedDateTime
			{
				get
				{
					System.DateTime? data = entity.ValidatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedDateTime = null;
					else entity.ValidatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidatedByUserID
			{
				get
				{
					System.String data = entity.ValidatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedByUserID = null;
					else entity.ValidatedByUserID = Convert.ToString(value);
				}
			}
			public System.String ReplacementPersonID
			{
				get
				{
					System.Int32? data = entity.ReplacementPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReplacementPersonID = null;
					else entity.ReplacementPersonID = Convert.ToInt32(value);
				}
			}
			public System.String RejectedReason
			{
				get
				{
					System.String data = entity.RejectedReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RejectedReason = null;
					else entity.RejectedReason = Convert.ToString(value);
				}
			}
			public System.String IsValidated2
			{
				get
				{
					System.Boolean? data = entity.IsValidated2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidated2 = null;
					else entity.IsValidated2 = Convert.ToBoolean(value);
				}
			}
			public System.String Validated2DateTime
			{
				get
				{
					System.DateTime? data = entity.Validated2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Validated2DateTime = null;
					else entity.Validated2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Validated2ByUserID
			{
				get
				{
					System.String data = entity.Validated2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Validated2ByUserID = null;
					else entity.Validated2ByUserID = Convert.ToString(value);
				}
			}
			public System.String PayCutDays
			{
				get
				{
					System.Int32? data = entity.PayCutDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayCutDays = null;
					else entity.PayCutDays = Convert.ToInt32(value);
				}
			}
			public System.String SRWorkingDay
			{
				get
				{
					System.String data = entity.SRWorkingDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkingDay = null;
					else entity.SRWorkingDay = Convert.ToString(value);
				}
			}
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
			public System.String IsPayCut
			{
				get
				{
					System.Boolean? data = entity.IsPayCut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPayCut = null;
					else entity.IsPayCut = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidated1
			{
				get
				{
					System.Boolean? data = entity.IsValidated1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidated1 = null;
					else entity.IsValidated1 = Convert.ToBoolean(value);
				}
			}
			public System.String Validated1DateTime
			{
				get
				{
					System.DateTime? data = entity.Validated1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Validated1DateTime = null;
					else entity.Validated1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Validated1ByUserID
			{
				get
				{
					System.String data = entity.Validated1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Validated1ByUserID = null;
					else entity.Validated1ByUserID = Convert.ToString(value);
				}
			}
			private esEmployeeLeaveRequest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveRequestQuery query)
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
				throw new Exception("esEmployeeLeaveRequest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeLeaveRequest : esEmployeeLeaveRequest
	{
	}

	[Serializable]
	abstract public class esEmployeeLeaveRequestQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveRequestMetadata.Meta();
			}
		}

		public esQueryItem LeaveRequestID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.LeaveRequestID, esSystemType.Int64);
			}
		}

		public esQueryItem RequestDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RequestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeLeaveID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.EmployeeLeaveID, esSystemType.Int64);
			}
		}

		public esQueryItem RequestLeaveDateFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem RequestLeaveDateTo
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateTo, esSystemType.DateTime);
			}
		}

		public esQueryItem RequestDays
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RequestDays, esSystemType.Int32);
			}
		}

		public esQueryItem RequestWorkingDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RequestWorkingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRequestApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsRequestApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedLeaveDateFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedLeaveDateTo
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateTo, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedDays
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDays, esSystemType.Int32);
			}
		}

		public esQueryItem ApprovedWorkingDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ApprovedWorkingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidated
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsValidated, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ValidatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ValidatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReplacementPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.ReplacementPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem RejectedReason
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.RejectedReason, esSystemType.String);
			}
		}

		public esQueryItem IsValidated2
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsValidated2, esSystemType.Boolean);
			}
		}

		public esQueryItem Validated2DateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.Validated2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Validated2ByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.Validated2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem PayCutDays
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.PayCutDays, esSystemType.Int32);
			}
		}

		public esQueryItem SRWorkingDay
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.SRWorkingDay, esSystemType.String);
			}
		}

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem IsPayCut
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsPayCut, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidated1
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.IsValidated1, esSystemType.Boolean);
			}
		}

		public esQueryItem Validated1DateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.Validated1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Validated1ByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveRequestMetadata.ColumnNames.Validated1ByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLeaveRequestCollection")]
	public partial class EmployeeLeaveRequestCollection : esEmployeeLeaveRequestCollection, IEnumerable<EmployeeLeaveRequest>
	{
		public EmployeeLeaveRequestCollection()
		{

		}

		public static implicit operator List<EmployeeLeaveRequest>(EmployeeLeaveRequestCollection coll)
		{
			List<EmployeeLeaveRequest> list = new List<EmployeeLeaveRequest>();

			foreach (EmployeeLeaveRequest emp in coll)
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
				return EmployeeLeaveRequestMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLeaveRequest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLeaveRequest();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeLeaveRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveRequestQuery();
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
		public bool Load(EmployeeLeaveRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeLeaveRequest AddNew()
		{
			EmployeeLeaveRequest entity = base.AddNewEntity() as EmployeeLeaveRequest;

			return entity;
		}
		public EmployeeLeaveRequest FindByPrimaryKey(Int64 leaveRequestID)
		{
			return base.FindByPrimaryKey(leaveRequestID) as EmployeeLeaveRequest;
		}

		#region IEnumerable< EmployeeLeaveRequest> Members

		IEnumerator<EmployeeLeaveRequest> IEnumerable<EmployeeLeaveRequest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLeaveRequest;
			}
		}

		#endregion

		private EmployeeLeaveRequestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLeaveRequest' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeLeaveRequest ({LeaveRequestID})")]
	[Serializable]
	public partial class EmployeeLeaveRequest : esEmployeeLeaveRequest
	{
		public EmployeeLeaveRequest()
		{
		}

		public EmployeeLeaveRequest(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveRequestMetadata.Meta();
			}
		}

		override protected esEmployeeLeaveRequestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeLeaveRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveRequestQuery();
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
		public bool Load(EmployeeLeaveRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeLeaveRequestQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeLeaveRequestQuery : esEmployeeLeaveRequestQuery
	{
		public EmployeeLeaveRequestQuery()
		{

		}

		public EmployeeLeaveRequestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeLeaveRequestQuery";
		}
	}

	[Serializable]
	public partial class EmployeeLeaveRequestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLeaveRequestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.LeaveRequestID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.LeaveRequestID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RequestDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RequestDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.EmployeeLeaveID, 3, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.EmployeeLeaveID;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateFrom, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RequestLeaveDateFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RequestLeaveDateTo, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RequestLeaveDateTo;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RequestDays, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RequestDays;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RequestWorkingDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RequestWorkingDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.CreatedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsApproved, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.VoidDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.VoidByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsRequestApproved, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsRequestApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateFrom, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedLeaveDateFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedLeaveDateTo, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedLeaveDateTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedDays, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ApprovedWorkingDate, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ApprovedWorkingDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsVerified, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.VerifiedByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.LastUpdateByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsValidated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ValidatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ValidatedByUserID, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ValidatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.ReplacementPersonID, 30, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.ReplacementPersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.RejectedReason, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.RejectedReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated2, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsValidated2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.Validated2DateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.Validated2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.Validated2ByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.Validated2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.PayCutDays, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.PayCutDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.SRWorkingDay, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.SRWorkingDay;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.PayrollPeriodID, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsPayCut, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsPayCut;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.IsValidated1, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.IsValidated1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.Validated1DateTime, 40, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.Validated1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveRequestMetadata.ColumnNames.Validated1ByUserID, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveRequestMetadata.PropertyNames.Validated1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeLeaveRequestMetadata Meta()
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
			public const string LeaveRequestID = "LeaveRequestID";
			public const string RequestDate = "RequestDate";
			public const string PersonID = "PersonID";
			public const string EmployeeLeaveID = "EmployeeLeaveID";
			public const string RequestLeaveDateFrom = "RequestLeaveDateFrom";
			public const string RequestLeaveDateTo = "RequestLeaveDateTo";
			public const string RequestDays = "RequestDays";
			public const string RequestWorkingDate = "RequestWorkingDate";
			public const string Notes = "Notes";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsRequestApproved = "IsRequestApproved";
			public const string ApprovedLeaveDateFrom = "ApprovedLeaveDateFrom";
			public const string ApprovedLeaveDateTo = "ApprovedLeaveDateTo";
			public const string ApprovedDays = "ApprovedDays";
			public const string ApprovedWorkingDate = "ApprovedWorkingDate";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string ReplacementPersonID = "ReplacementPersonID";
			public const string RejectedReason = "RejectedReason";
			public const string IsValidated2 = "IsValidated2";
			public const string Validated2DateTime = "Validated2DateTime";
			public const string Validated2ByUserID = "Validated2ByUserID";
			public const string PayCutDays = "PayCutDays";
			public const string SRWorkingDay = "SRWorkingDay";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string IsPayCut = "IsPayCut";
			public const string IsValidated1 = "IsValidated1";
			public const string Validated1DateTime = "Validated1DateTime";
			public const string Validated1ByUserID = "Validated1ByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LeaveRequestID = "LeaveRequestID";
			public const string RequestDate = "RequestDate";
			public const string PersonID = "PersonID";
			public const string EmployeeLeaveID = "EmployeeLeaveID";
			public const string RequestLeaveDateFrom = "RequestLeaveDateFrom";
			public const string RequestLeaveDateTo = "RequestLeaveDateTo";
			public const string RequestDays = "RequestDays";
			public const string RequestWorkingDate = "RequestWorkingDate";
			public const string Notes = "Notes";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsRequestApproved = "IsRequestApproved";
			public const string ApprovedLeaveDateFrom = "ApprovedLeaveDateFrom";
			public const string ApprovedLeaveDateTo = "ApprovedLeaveDateTo";
			public const string ApprovedDays = "ApprovedDays";
			public const string ApprovedWorkingDate = "ApprovedWorkingDate";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string ReplacementPersonID = "ReplacementPersonID";
			public const string RejectedReason = "RejectedReason";
			public const string IsValidated2 = "IsValidated2";
			public const string Validated2DateTime = "Validated2DateTime";
			public const string Validated2ByUserID = "Validated2ByUserID";
			public const string PayCutDays = "PayCutDays";
			public const string SRWorkingDay = "SRWorkingDay";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string IsPayCut = "IsPayCut";
			public const string IsValidated1 = "IsValidated1";
			public const string Validated1DateTime = "Validated1DateTime";
			public const string Validated1ByUserID = "Validated1ByUserID";
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
			lock (typeof(EmployeeLeaveRequestMetadata))
			{
				if (EmployeeLeaveRequestMetadata.mapDelegates == null)
				{
					EmployeeLeaveRequestMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeLeaveRequestMetadata.meta == null)
				{
					EmployeeLeaveRequestMetadata.meta = new EmployeeLeaveRequestMetadata();
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

				meta.AddTypeMap("LeaveRequestID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("RequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeLeaveID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("RequestLeaveDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RequestLeaveDateTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RequestDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RequestWorkingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRequestApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedLeaveDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedLeaveDateTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApprovedWorkingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReplacementPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RejectedReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidated2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Validated2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Validated2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PayCutDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWorkingDay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsPayCut", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidated1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Validated1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Validated1ByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeLeaveRequest";
				meta.Destination = "EmployeeLeaveRequest";
				meta.spInsert = "proc_EmployeeLeaveRequestInsert";
				meta.spUpdate = "proc_EmployeeLeaveRequestUpdate";
				meta.spDelete = "proc_EmployeeLeaveRequestDelete";
				meta.spLoadAll = "proc_EmployeeLeaveRequestLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLeaveRequestLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLeaveRequestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
