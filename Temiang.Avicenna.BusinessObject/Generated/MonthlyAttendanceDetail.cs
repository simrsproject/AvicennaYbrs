/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/16/2022 3:01:39 PM
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
	abstract public class esMonthlyAttendanceDetailCollection : esEntityCollectionWAuditLog
	{
		public esMonthlyAttendanceDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MonthlyAttendanceDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailQuery query)
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
			this.InitQuery(query as esMonthlyAttendanceDetailQuery);
		}
		#endregion

		virtual public MonthlyAttendanceDetail DetachEntity(MonthlyAttendanceDetail entity)
		{
			return base.DetachEntity(entity) as MonthlyAttendanceDetail;
		}

		virtual public MonthlyAttendanceDetail AttachEntity(MonthlyAttendanceDetail entity)
		{
			return base.AttachEntity(entity) as MonthlyAttendanceDetail;
		}

		virtual public void Combine(MonthlyAttendanceDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public MonthlyAttendanceDetail this[int index]
		{
			get
			{
				return base[index] as MonthlyAttendanceDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MonthlyAttendanceDetail);
		}
	}

	[Serializable]
	abstract public class esMonthlyAttendanceDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMonthlyAttendanceDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esMonthlyAttendanceDetail()
		{
		}

		public esMonthlyAttendanceDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 monthlyAttendanceDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 monthlyAttendanceDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 monthlyAttendanceDetailID)
		{
			esMonthlyAttendanceDetailQuery query = this.GetDynamicQuery();
			query.Where(query.MonthlyAttendanceDetailID == monthlyAttendanceDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 monthlyAttendanceDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("MonthlyAttendanceDetailID", monthlyAttendanceDetailID);
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
						case "MonthlyAttendanceDetailID": this.str.MonthlyAttendanceDetailID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
						case "ScheduleInDate": this.str.ScheduleInDate = (string)value; break;
						case "ScheduleInTime": this.str.ScheduleInTime = (string)value; break;
						case "ScheduleOutDate": this.str.ScheduleOutDate = (string)value; break;
						case "ScheduleOutTime": this.str.ScheduleOutTime = (string)value; break;
						case "CheckInDate": this.str.CheckInDate = (string)value; break;
						case "CheckInTime": this.str.CheckInTime = (string)value; break;
						case "CheckOutDate": this.str.CheckOutDate = (string)value; break;
						case "CheckOutTime": this.str.CheckOutTime = (string)value; break;
						case "IsOvertime": this.str.IsOvertime = (string)value; break;
						case "OvertimeHours": this.str.OvertimeHours = (string)value; break;
						case "LateMinutes": this.str.LateMinutes = (string)value; break;
						case "LateCutPercentage": this.str.LateCutPercentage = (string)value; break;
						case "EarlyLeaveMinutes": this.str.EarlyLeaveMinutes = (string)value; break;
						case "EarlyLeaveCutPercentage": this.str.EarlyLeaveCutPercentage = (string)value; break;
						case "IsInvalid": this.str.IsInvalid = (string)value; break;
						case "IsHasPermission": this.str.IsHasPermission = (string)value; break;
						case "SRAttendanceFileFormat": this.str.SRAttendanceFileFormat = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsOff": this.str.IsOff = (string)value; break;
						case "IsPayCut": this.str.IsPayCut = (string)value; break;
						case "WorkingHourID": this.str.WorkingHourID = (string)value; break;
						case "CutAmount": this.str.CutAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MonthlyAttendanceDetailID":

							if (value == null || value is System.Int64)
								this.MonthlyAttendanceDetailID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PayrollPeriodID":

							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						case "ScheduleInDate":

							if (value == null || value is System.DateTime)
								this.ScheduleInDate = (System.DateTime?)value;
							break;
						case "ScheduleOutDate":

							if (value == null || value is System.DateTime)
								this.ScheduleOutDate = (System.DateTime?)value;
							break;
						case "CheckInDate":

							if (value == null || value is System.DateTime)
								this.CheckInDate = (System.DateTime?)value;
							break;
						case "CheckOutDate":

							if (value == null || value is System.DateTime)
								this.CheckOutDate = (System.DateTime?)value;
							break;
						case "IsOvertime":

							if (value == null || value is System.Boolean)
								this.IsOvertime = (System.Boolean?)value;
							break;
						case "OvertimeHours":

							if (value == null || value is System.Decimal)
								this.OvertimeHours = (System.Decimal?)value;
							break;
						case "LateMinutes":

							if (value == null || value is System.Int16)
								this.LateMinutes = (System.Int16?)value;
							break;
						case "LateCutPercentage":

							if (value == null || value is System.Decimal)
								this.LateCutPercentage = (System.Decimal?)value;
							break;
						case "EarlyLeaveMinutes":

							if (value == null || value is System.Int16)
								this.EarlyLeaveMinutes = (System.Int16?)value;
							break;
						case "EarlyLeaveCutPercentage":

							if (value == null || value is System.Decimal)
								this.EarlyLeaveCutPercentage = (System.Decimal?)value;
							break;
						case "IsInvalid":

							if (value == null || value is System.Boolean)
								this.IsInvalid = (System.Boolean?)value;
							break;
						case "IsHasPermission":

							if (value == null || value is System.Boolean)
								this.IsHasPermission = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsOff":

							if (value == null || value is System.Boolean)
								this.IsOff = (System.Boolean?)value;
							break;
						case "IsPayCut":

							if (value == null || value is System.Boolean)
								this.IsPayCut = (System.Boolean?)value;
							break;
						case "WorkingHourID":

							if (value == null || value is System.Int32)
								this.WorkingHourID = (System.Int32?)value;
							break;
						case "CutAmount":

							if (value == null || value is System.Decimal)
								this.CutAmount = (System.Decimal?)value;
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
		/// Maps to MonthlyAttendanceDetail.MonthlyAttendanceDetailID
		/// </summary>
		virtual public System.Int64? MonthlyAttendanceDetailID
		{
			get
			{
				return base.GetSystemInt64(MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID);
			}

			set
			{
				base.SetSystemInt64(MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.PayrollPeriodID);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.ScheduleInDate
		/// </summary>
		virtual public System.DateTime? ScheduleInDate
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.ScheduleInTime
		/// </summary>
		virtual public System.String ScheduleInTime
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.ScheduleOutDate
		/// </summary>
		virtual public System.DateTime? ScheduleOutDate
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.ScheduleOutTime
		/// </summary>
		virtual public System.String ScheduleOutTime
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.CheckInDate
		/// </summary>
		virtual public System.DateTime? CheckInDate
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInDate);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInDate, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.CheckInTime
		/// </summary>
		virtual public System.String CheckInTime
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInTime);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.CheckOutDate
		/// </summary>
		virtual public System.DateTime? CheckOutDate
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutDate);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutDate, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.CheckOutTime
		/// </summary>
		virtual public System.String CheckOutTime
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutTime);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.IsOvertime
		/// </summary>
		virtual public System.Boolean? IsOvertime
		{
			get
			{
				return base.GetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsOvertime);
			}

			set
			{
				base.SetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsOvertime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.OvertimeHours
		/// </summary>
		virtual public System.Decimal? OvertimeHours
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.OvertimeHours);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.OvertimeHours, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.LateMinutes
		/// </summary>
		virtual public System.Int16? LateMinutes
		{
			get
			{
				return base.GetSystemInt16(MonthlyAttendanceDetailMetadata.ColumnNames.LateMinutes);
			}

			set
			{
				base.SetSystemInt16(MonthlyAttendanceDetailMetadata.ColumnNames.LateMinutes, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.LateCutPercentage
		/// </summary>
		virtual public System.Decimal? LateCutPercentage
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.LateCutPercentage);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.LateCutPercentage, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.EarlyLeaveMinutes
		/// </summary>
		virtual public System.Int16? EarlyLeaveMinutes
		{
			get
			{
				return base.GetSystemInt16(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveMinutes);
			}

			set
			{
				base.SetSystemInt16(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveMinutes, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.EarlyLeaveCutPercentage
		/// </summary>
		virtual public System.Decimal? EarlyLeaveCutPercentage
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveCutPercentage);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveCutPercentage, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.IsInvalid
		/// </summary>
		virtual public System.Boolean? IsInvalid
		{
			get
			{
				return base.GetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsInvalid);
			}

			set
			{
				base.SetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsInvalid, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.IsHasPermission
		/// </summary>
		virtual public System.Boolean? IsHasPermission
		{
			get
			{
				return base.GetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsHasPermission);
			}

			set
			{
				base.SetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsHasPermission, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.SRAttendanceFileFormat
		/// </summary>
		virtual public System.String SRAttendanceFileFormat
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.SRAttendanceFileFormat);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.SRAttendanceFileFormat, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.IsOff
		/// </summary>
		virtual public System.Boolean? IsOff
		{
			get
			{
				return base.GetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsOff);
			}

			set
			{
				base.SetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsOff, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.IsPayCut
		/// </summary>
		virtual public System.Boolean? IsPayCut
		{
			get
			{
				return base.GetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsPayCut);
			}

			set
			{
				base.SetSystemBoolean(MonthlyAttendanceDetailMetadata.ColumnNames.IsPayCut, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.WorkingHourID
		/// </summary>
		virtual public System.Int32? WorkingHourID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.WorkingHourID);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailMetadata.ColumnNames.WorkingHourID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetail.CutAmount
		/// </summary>
		virtual public System.Decimal? CutAmount
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.CutAmount);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailMetadata.ColumnNames.CutAmount, value);
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
			public esStrings(esMonthlyAttendanceDetail entity)
			{
				this.entity = entity;
			}
			public System.String MonthlyAttendanceDetailID
			{
				get
				{
					System.Int64? data = entity.MonthlyAttendanceDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthlyAttendanceDetailID = null;
					else entity.MonthlyAttendanceDetailID = Convert.ToInt64(value);
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
			public System.String ScheduleInDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleInDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleInDate = null;
					else entity.ScheduleInDate = Convert.ToDateTime(value);
				}
			}
			public System.String ScheduleInTime
			{
				get
				{
					System.String data = entity.ScheduleInTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleInTime = null;
					else entity.ScheduleInTime = Convert.ToString(value);
				}
			}
			public System.String ScheduleOutDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleOutDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleOutDate = null;
					else entity.ScheduleOutDate = Convert.ToDateTime(value);
				}
			}
			public System.String ScheduleOutTime
			{
				get
				{
					System.String data = entity.ScheduleOutTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleOutTime = null;
					else entity.ScheduleOutTime = Convert.ToString(value);
				}
			}
			public System.String CheckInDate
			{
				get
				{
					System.DateTime? data = entity.CheckInDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckInDate = null;
					else entity.CheckInDate = Convert.ToDateTime(value);
				}
			}
			public System.String CheckInTime
			{
				get
				{
					System.String data = entity.CheckInTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckInTime = null;
					else entity.CheckInTime = Convert.ToString(value);
				}
			}
			public System.String CheckOutDate
			{
				get
				{
					System.DateTime? data = entity.CheckOutDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckOutDate = null;
					else entity.CheckOutDate = Convert.ToDateTime(value);
				}
			}
			public System.String CheckOutTime
			{
				get
				{
					System.String data = entity.CheckOutTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckOutTime = null;
					else entity.CheckOutTime = Convert.ToString(value);
				}
			}
			public System.String IsOvertime
			{
				get
				{
					System.Boolean? data = entity.IsOvertime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOvertime = null;
					else entity.IsOvertime = Convert.ToBoolean(value);
				}
			}
			public System.String OvertimeHours
			{
				get
				{
					System.Decimal? data = entity.OvertimeHours;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OvertimeHours = null;
					else entity.OvertimeHours = Convert.ToDecimal(value);
				}
			}
			public System.String LateMinutes
			{
				get
				{
					System.Int16? data = entity.LateMinutes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LateMinutes = null;
					else entity.LateMinutes = Convert.ToInt16(value);
				}
			}
			public System.String LateCutPercentage
			{
				get
				{
					System.Decimal? data = entity.LateCutPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LateCutPercentage = null;
					else entity.LateCutPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String EarlyLeaveMinutes
			{
				get
				{
					System.Int16? data = entity.EarlyLeaveMinutes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EarlyLeaveMinutes = null;
					else entity.EarlyLeaveMinutes = Convert.ToInt16(value);
				}
			}
			public System.String EarlyLeaveCutPercentage
			{
				get
				{
					System.Decimal? data = entity.EarlyLeaveCutPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EarlyLeaveCutPercentage = null;
					else entity.EarlyLeaveCutPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsInvalid
			{
				get
				{
					System.Boolean? data = entity.IsInvalid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInvalid = null;
					else entity.IsInvalid = Convert.ToBoolean(value);
				}
			}
			public System.String IsHasPermission
			{
				get
				{
					System.Boolean? data = entity.IsHasPermission;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHasPermission = null;
					else entity.IsHasPermission = Convert.ToBoolean(value);
				}
			}
			public System.String SRAttendanceFileFormat
			{
				get
				{
					System.String data = entity.SRAttendanceFileFormat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAttendanceFileFormat = null;
					else entity.SRAttendanceFileFormat = Convert.ToString(value);
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
			public System.String IsOff
			{
				get
				{
					System.Boolean? data = entity.IsOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOff = null;
					else entity.IsOff = Convert.ToBoolean(value);
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
			public System.String WorkingHourID
			{
				get
				{
					System.Int32? data = entity.WorkingHourID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourID = null;
					else entity.WorkingHourID = Convert.ToInt32(value);
				}
			}
			public System.String CutAmount
			{
				get
				{
					System.Decimal? data = entity.CutAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CutAmount = null;
					else entity.CutAmount = Convert.ToDecimal(value);
				}
			}
			private esMonthlyAttendanceDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailQuery query)
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
				throw new Exception("esMonthlyAttendanceDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MonthlyAttendanceDetail : esMonthlyAttendanceDetail
	{
	}

	[Serializable]
	abstract public class esMonthlyAttendanceDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailMetadata.Meta();
			}
		}

		public esQueryItem MonthlyAttendanceDetailID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem ScheduleInDate
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ScheduleInTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime, esSystemType.String);
			}
		}

		public esQueryItem ScheduleOutDate
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ScheduleOutTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime, esSystemType.String);
			}
		}

		public esQueryItem CheckInDate
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.CheckInDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CheckInTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.CheckInTime, esSystemType.String);
			}
		}

		public esQueryItem CheckOutDate
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CheckOutTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutTime, esSystemType.String);
			}
		}

		public esQueryItem IsOvertime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.IsOvertime, esSystemType.Boolean);
			}
		}

		public esQueryItem OvertimeHours
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.OvertimeHours, esSystemType.Decimal);
			}
		}

		public esQueryItem LateMinutes
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.LateMinutes, esSystemType.Int16);
			}
		}

		public esQueryItem LateCutPercentage
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.LateCutPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem EarlyLeaveMinutes
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveMinutes, esSystemType.Int16);
			}
		}

		public esQueryItem EarlyLeaveCutPercentage
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveCutPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsInvalid
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.IsInvalid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHasPermission
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.IsHasPermission, esSystemType.Boolean);
			}
		}

		public esQueryItem SRAttendanceFileFormat
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.SRAttendanceFileFormat, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOff
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.IsOff, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPayCut
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.IsPayCut, esSystemType.Boolean);
			}
		}

		public esQueryItem WorkingHourID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.WorkingHourID, esSystemType.Int32);
			}
		}

		public esQueryItem CutAmount
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailMetadata.ColumnNames.CutAmount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MonthlyAttendanceDetailCollection")]
	public partial class MonthlyAttendanceDetailCollection : esMonthlyAttendanceDetailCollection, IEnumerable<MonthlyAttendanceDetail>
	{
		public MonthlyAttendanceDetailCollection()
		{

		}

		public static implicit operator List<MonthlyAttendanceDetail>(MonthlyAttendanceDetailCollection coll)
		{
			List<MonthlyAttendanceDetail> list = new List<MonthlyAttendanceDetail>();

			foreach (MonthlyAttendanceDetail emp in coll)
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
				return MonthlyAttendanceDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MonthlyAttendanceDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MonthlyAttendanceDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailQuery();
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
		public bool Load(MonthlyAttendanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MonthlyAttendanceDetail AddNew()
		{
			MonthlyAttendanceDetail entity = base.AddNewEntity() as MonthlyAttendanceDetail;

			return entity;
		}
		public MonthlyAttendanceDetail FindByPrimaryKey(Int64 monthlyAttendanceDetailID)
		{
			return base.FindByPrimaryKey(monthlyAttendanceDetailID) as MonthlyAttendanceDetail;
		}

		#region IEnumerable< MonthlyAttendanceDetail> Members

		IEnumerator<MonthlyAttendanceDetail> IEnumerable<MonthlyAttendanceDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MonthlyAttendanceDetail;
			}
		}

		#endregion

		private MonthlyAttendanceDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MonthlyAttendanceDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MonthlyAttendanceDetail ({MonthlyAttendanceDetailID})")]
	[Serializable]
	public partial class MonthlyAttendanceDetail : esMonthlyAttendanceDetail
	{
		public MonthlyAttendanceDetail()
		{
		}

		public MonthlyAttendanceDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailMetadata.Meta();
			}
		}

		override protected esMonthlyAttendanceDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailQuery();
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
		public bool Load(MonthlyAttendanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MonthlyAttendanceDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MonthlyAttendanceDetailQuery : esMonthlyAttendanceDetailQuery
	{
		public MonthlyAttendanceDetailQuery()
		{

		}

		public MonthlyAttendanceDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MonthlyAttendanceDetailQuery";
		}
	}

	[Serializable]
	public partial class MonthlyAttendanceDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MonthlyAttendanceDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.MonthlyAttendanceDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.PayrollPeriodID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.ScheduleInDate;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.ScheduleInTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.ScheduleOutDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.ScheduleOutTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.CheckInDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.CheckInTime, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.CheckInTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.CheckOutDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutTime, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.CheckOutTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.IsOvertime, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.IsOvertime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.OvertimeHours, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.OvertimeHours;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.LateMinutes, 13, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.LateMinutes;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.LateCutPercentage, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.LateCutPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveMinutes, 15, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.EarlyLeaveMinutes;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveCutPercentage, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.EarlyLeaveCutPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.IsInvalid, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.IsInvalid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.IsHasPermission, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.IsHasPermission;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.SRAttendanceFileFormat, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.SRAttendanceFileFormat;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.IsOff, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.IsOff;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.IsPayCut, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.IsPayCut;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.WorkingHourID, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.WorkingHourID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailMetadata.ColumnNames.CutAmount, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailMetadata.PropertyNames.CutAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MonthlyAttendanceDetailMetadata Meta()
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
			public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			public const string PersonID = "PersonID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string ScheduleInDate = "ScheduleInDate";
			public const string ScheduleInTime = "ScheduleInTime";
			public const string ScheduleOutDate = "ScheduleOutDate";
			public const string ScheduleOutTime = "ScheduleOutTime";
			public const string CheckInDate = "CheckInDate";
			public const string CheckInTime = "CheckInTime";
			public const string CheckOutDate = "CheckOutDate";
			public const string CheckOutTime = "CheckOutTime";
			public const string IsOvertime = "IsOvertime";
			public const string OvertimeHours = "OvertimeHours";
			public const string LateMinutes = "LateMinutes";
			public const string LateCutPercentage = "LateCutPercentage";
			public const string EarlyLeaveMinutes = "EarlyLeaveMinutes";
			public const string EarlyLeaveCutPercentage = "EarlyLeaveCutPercentage";
			public const string IsInvalid = "IsInvalid";
			public const string IsHasPermission = "IsHasPermission";
			public const string SRAttendanceFileFormat = "SRAttendanceFileFormat";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsOff = "IsOff";
			public const string IsPayCut = "IsPayCut";
			public const string WorkingHourID = "WorkingHourID";
			public const string CutAmount = "CutAmount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			public const string PersonID = "PersonID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string ScheduleInDate = "ScheduleInDate";
			public const string ScheduleInTime = "ScheduleInTime";
			public const string ScheduleOutDate = "ScheduleOutDate";
			public const string ScheduleOutTime = "ScheduleOutTime";
			public const string CheckInDate = "CheckInDate";
			public const string CheckInTime = "CheckInTime";
			public const string CheckOutDate = "CheckOutDate";
			public const string CheckOutTime = "CheckOutTime";
			public const string IsOvertime = "IsOvertime";
			public const string OvertimeHours = "OvertimeHours";
			public const string LateMinutes = "LateMinutes";
			public const string LateCutPercentage = "LateCutPercentage";
			public const string EarlyLeaveMinutes = "EarlyLeaveMinutes";
			public const string EarlyLeaveCutPercentage = "EarlyLeaveCutPercentage";
			public const string IsInvalid = "IsInvalid";
			public const string IsHasPermission = "IsHasPermission";
			public const string SRAttendanceFileFormat = "SRAttendanceFileFormat";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsOff = "IsOff";
			public const string IsPayCut = "IsPayCut";
			public const string WorkingHourID = "WorkingHourID";
			public const string CutAmount = "CutAmount";
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
			lock (typeof(MonthlyAttendanceDetailMetadata))
			{
				if (MonthlyAttendanceDetailMetadata.mapDelegates == null)
				{
					MonthlyAttendanceDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MonthlyAttendanceDetailMetadata.meta == null)
				{
					MonthlyAttendanceDetailMetadata.meta = new MonthlyAttendanceDetailMetadata();
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

				meta.AddTypeMap("MonthlyAttendanceDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ScheduleInDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ScheduleInTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleOutDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ScheduleOutTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckInDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("CheckInTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckOutDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("CheckOutTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOvertime", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OvertimeHours", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LateMinutes", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LateCutPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EarlyLeaveMinutes", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("EarlyLeaveCutPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsInvalid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHasPermission", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRAttendanceFileFormat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOff", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPayCut", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("WorkingHourID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CutAmount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "MonthlyAttendanceDetail";
				meta.Destination = "MonthlyAttendanceDetail";
				meta.spInsert = "proc_MonthlyAttendanceDetailInsert";
				meta.spUpdate = "proc_MonthlyAttendanceDetailUpdate";
				meta.spDelete = "proc_MonthlyAttendanceDetailDelete";
				meta.spLoadAll = "proc_MonthlyAttendanceDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_MonthlyAttendanceDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MonthlyAttendanceDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
