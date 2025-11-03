/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/25/2020 7:14:04 PM
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
	abstract public class esMonthlyAttendanceCollection : esEntityCollectionWAuditLog
	{
		public esMonthlyAttendanceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MonthlyAttendanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceQuery query)
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
			this.InitQuery(query as esMonthlyAttendanceQuery);
		}
		#endregion

		virtual public MonthlyAttendance DetachEntity(MonthlyAttendance entity)
		{
			return base.DetachEntity(entity) as MonthlyAttendance;
		}

		virtual public MonthlyAttendance AttachEntity(MonthlyAttendance entity)
		{
			return base.AttachEntity(entity) as MonthlyAttendance;
		}

		virtual public void Combine(MonthlyAttendanceCollection collection)
		{
			base.Combine(collection);
		}

		new public MonthlyAttendance this[int index]
		{
			get
			{
				return base[index] as MonthlyAttendance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MonthlyAttendance);
		}
	}

	[Serializable]
	abstract public class esMonthlyAttendance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMonthlyAttendanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esMonthlyAttendance()
		{
		}

		public esMonthlyAttendance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 monthlyAttendanceID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 monthlyAttendanceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 monthlyAttendanceID)
		{
			esMonthlyAttendanceQuery query = this.GetDynamicQuery();
			query.Where(query.MonthlyAttendanceID == monthlyAttendanceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 monthlyAttendanceID)
		{
			esParameters parms = new esParameters();
			parms.Add("MonthlyAttendanceID", monthlyAttendanceID);
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
						case "MonthlyAttendanceID": this.str.MonthlyAttendanceID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
						case "PayDays": this.str.PayDays = (string)value; break;
						case "UnPayDays": this.str.UnPayDays = (string)value; break;
						case "AbsenceCount": this.str.AbsenceCount = (string)value; break;
						case "SRAttedanceInsentif": this.str.SRAttedanceInsentif = (string)value; break;
						case "WorkingDays": this.str.WorkingDays = (string)value; break;
						case "Holidays": this.str.Holidays = (string)value; break;
						case "OvertimeDays": this.str.OvertimeDays = (string)value; break;
						case "HolidayWorking": this.str.HolidayWorking = (string)value; break;
						case "LateDays": this.str.LateDays = (string)value; break;
						case "EarlyLeaveDays": this.str.EarlyLeaveDays = (string)value; break;
						case "DisciplinarySanctionsAmount": this.str.DisciplinarySanctionsAmount = (string)value; break;
						case "BasicWorkingTime": this.str.BasicWorkingTime = (string)value; break;
						case "OvertimeHours": this.str.OvertimeHours = (string)value; break;
						case "ConvertOvertimeHours": this.str.ConvertOvertimeHours = (string)value; break;
						case "TotalWorkingTime": this.str.TotalWorkingTime = (string)value; break;
						case "BasicFoodExspenses": this.str.BasicFoodExspenses = (string)value; break;
						case "OvertimeFoodExspenses": this.str.OvertimeFoodExspenses = (string)value; break;
						case "RamadhanFoodExspenses": this.str.RamadhanFoodExspenses = (string)value; break;
						case "Shift1Compensation": this.str.Shift1Compensation = (string)value; break;
						case "Shift2Compensation": this.str.Shift2Compensation = (string)value; break;
						case "Shift3Compensation": this.str.Shift3Compensation = (string)value; break;
						case "Shift4Compensation": this.str.Shift4Compensation = (string)value; break;
						case "SRAttendanceFileFormat": this.str.SRAttendanceFileFormat = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MonthlyAttendanceID":

							if (value == null || value is System.Int64)
								this.MonthlyAttendanceID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PayrollPeriodID":

							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						case "PayDays":

							if (value == null || value is System.Int32)
								this.PayDays = (System.Int32?)value;
							break;
						case "UnPayDays":

							if (value == null || value is System.Int32)
								this.UnPayDays = (System.Int32?)value;
							break;
						case "AbsenceCount":

							if (value == null || value is System.Int32)
								this.AbsenceCount = (System.Int32?)value;
							break;
						case "WorkingDays":

							if (value == null || value is System.Int32)
								this.WorkingDays = (System.Int32?)value;
							break;
						case "Holidays":

							if (value == null || value is System.Int32)
								this.Holidays = (System.Int32?)value;
							break;
						case "OvertimeDays":

							if (value == null || value is System.Int32)
								this.OvertimeDays = (System.Int32?)value;
							break;
						case "HolidayWorking":

							if (value == null || value is System.Int32)
								this.HolidayWorking = (System.Int32?)value;
							break;
						case "LateDays":

							if (value == null || value is System.Int32)
								this.LateDays = (System.Int32?)value;
							break;
						case "EarlyLeaveDays":

							if (value == null || value is System.Int32)
								this.EarlyLeaveDays = (System.Int32?)value;
							break;
						case "DisciplinarySanctionsAmount":

							if (value == null || value is System.Decimal)
								this.DisciplinarySanctionsAmount = (System.Decimal?)value;
							break;
						case "BasicWorkingTime":

							if (value == null || value is System.Decimal)
								this.BasicWorkingTime = (System.Decimal?)value;
							break;
						case "OvertimeHours":

							if (value == null || value is System.Decimal)
								this.OvertimeHours = (System.Decimal?)value;
							break;
						case "ConvertOvertimeHours":

							if (value == null || value is System.Decimal)
								this.ConvertOvertimeHours = (System.Decimal?)value;
							break;
						case "TotalWorkingTime":

							if (value == null || value is System.Decimal)
								this.TotalWorkingTime = (System.Decimal?)value;
							break;
						case "BasicFoodExspenses":

							if (value == null || value is System.Int32)
								this.BasicFoodExspenses = (System.Int32?)value;
							break;
						case "OvertimeFoodExspenses":

							if (value == null || value is System.Int32)
								this.OvertimeFoodExspenses = (System.Int32?)value;
							break;
						case "RamadhanFoodExspenses":

							if (value == null || value is System.Int32)
								this.RamadhanFoodExspenses = (System.Int32?)value;
							break;
						case "Shift1Compensation":

							if (value == null || value is System.Int32)
								this.Shift1Compensation = (System.Int32?)value;
							break;
						case "Shift2Compensation":

							if (value == null || value is System.Int32)
								this.Shift2Compensation = (System.Int32?)value;
							break;
						case "Shift3Compensation":

							if (value == null || value is System.Int32)
								this.Shift3Compensation = (System.Int32?)value;
							break;
						case "Shift4Compensation":

							if (value == null || value is System.Int32)
								this.Shift4Compensation = (System.Int32?)value;
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
		/// Maps to MonthlyAttendance.MonthlyAttendanceID
		/// </summary>
		virtual public System.Int64? MonthlyAttendanceID
		{
			get
			{
				return base.GetSystemInt64(MonthlyAttendanceMetadata.ColumnNames.MonthlyAttendanceID);
			}

			set
			{
				base.SetSystemInt64(MonthlyAttendanceMetadata.ColumnNames.MonthlyAttendanceID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PayrollPeriodID);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.PayDays
		/// </summary>
		virtual public System.Int32? PayDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PayDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.PayDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.UnPayDays
		/// </summary>
		virtual public System.Int32? UnPayDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.UnPayDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.UnPayDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.AbsenceCount
		/// </summary>
		virtual public System.Int32? AbsenceCount
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.AbsenceCount);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.AbsenceCount, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.SRAttedanceInsentif
		/// </summary>
		virtual public System.String SRAttedanceInsentif
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceMetadata.ColumnNames.SRAttedanceInsentif);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceMetadata.ColumnNames.SRAttedanceInsentif, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.WorkingDays
		/// </summary>
		virtual public System.Int32? WorkingDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.WorkingDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.WorkingDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.Holidays
		/// </summary>
		virtual public System.Int32? Holidays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Holidays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Holidays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.OvertimeDays
		/// </summary>
		virtual public System.Int32? OvertimeDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.OvertimeDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.OvertimeDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.HolidayWorking
		/// </summary>
		virtual public System.Int32? HolidayWorking
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.HolidayWorking);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.HolidayWorking, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.LateDays
		/// </summary>
		virtual public System.Int32? LateDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.LateDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.LateDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.EarlyLeaveDays
		/// </summary>
		virtual public System.Int32? EarlyLeaveDays
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.EarlyLeaveDays);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.EarlyLeaveDays, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.DisciplinarySanctionsAmount
		/// </summary>
		virtual public System.Decimal? DisciplinarySanctionsAmount
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.DisciplinarySanctionsAmount);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.DisciplinarySanctionsAmount, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.BasicWorkingTime
		/// </summary>
		virtual public System.Decimal? BasicWorkingTime
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.BasicWorkingTime);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.BasicWorkingTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.OvertimeHours
		/// </summary>
		virtual public System.Decimal? OvertimeHours
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.OvertimeHours);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.OvertimeHours, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.ConvertOvertimeHours
		/// </summary>
		virtual public System.Decimal? ConvertOvertimeHours
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.ConvertOvertimeHours);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.ConvertOvertimeHours, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.TotalWorkingTime
		/// </summary>
		virtual public System.Decimal? TotalWorkingTime
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.TotalWorkingTime);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceMetadata.ColumnNames.TotalWorkingTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.BasicFoodExspenses
		/// </summary>
		virtual public System.Int32? BasicFoodExspenses
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.BasicFoodExspenses);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.BasicFoodExspenses, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.OvertimeFoodExspenses
		/// </summary>
		virtual public System.Int32? OvertimeFoodExspenses
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.OvertimeFoodExspenses);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.OvertimeFoodExspenses, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.RamadhanFoodExspenses
		/// </summary>
		virtual public System.Int32? RamadhanFoodExspenses
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.RamadhanFoodExspenses);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.RamadhanFoodExspenses, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.Shift1Compensation
		/// </summary>
		virtual public System.Int32? Shift1Compensation
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift1Compensation);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift1Compensation, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.Shift2Compensation
		/// </summary>
		virtual public System.Int32? Shift2Compensation
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift2Compensation);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift2Compensation, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.Shift3Compensation
		/// </summary>
		virtual public System.Int32? Shift3Compensation
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift3Compensation);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift3Compensation, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.Shift4Compensation
		/// </summary>
		virtual public System.Int32? Shift4Compensation
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift4Compensation);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceMetadata.ColumnNames.Shift4Compensation, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.SRAttendanceFileFormat
		/// </summary>
		virtual public System.String SRAttendanceFileFormat
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceMetadata.ColumnNames.SRAttendanceFileFormat);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceMetadata.ColumnNames.SRAttendanceFileFormat, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MonthlyAttendanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MonthlyAttendanceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MonthlyAttendanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMonthlyAttendance entity)
			{
				this.entity = entity;
			}
			public System.String MonthlyAttendanceID
			{
				get
				{
					System.Int64? data = entity.MonthlyAttendanceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthlyAttendanceID = null;
					else entity.MonthlyAttendanceID = Convert.ToInt64(value);
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
			public System.String PayDays
			{
				get
				{
					System.Int32? data = entity.PayDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayDays = null;
					else entity.PayDays = Convert.ToInt32(value);
				}
			}
			public System.String UnPayDays
			{
				get
				{
					System.Int32? data = entity.UnPayDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnPayDays = null;
					else entity.UnPayDays = Convert.ToInt32(value);
				}
			}
			public System.String AbsenceCount
			{
				get
				{
					System.Int32? data = entity.AbsenceCount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbsenceCount = null;
					else entity.AbsenceCount = Convert.ToInt32(value);
				}
			}
			public System.String SRAttedanceInsentif
			{
				get
				{
					System.String data = entity.SRAttedanceInsentif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAttedanceInsentif = null;
					else entity.SRAttedanceInsentif = Convert.ToString(value);
				}
			}
			public System.String WorkingDays
			{
				get
				{
					System.Int32? data = entity.WorkingDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingDays = null;
					else entity.WorkingDays = Convert.ToInt32(value);
				}
			}
			public System.String Holidays
			{
				get
				{
					System.Int32? data = entity.Holidays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Holidays = null;
					else entity.Holidays = Convert.ToInt32(value);
				}
			}
			public System.String OvertimeDays
			{
				get
				{
					System.Int32? data = entity.OvertimeDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OvertimeDays = null;
					else entity.OvertimeDays = Convert.ToInt32(value);
				}
			}
			public System.String HolidayWorking
			{
				get
				{
					System.Int32? data = entity.HolidayWorking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HolidayWorking = null;
					else entity.HolidayWorking = Convert.ToInt32(value);
				}
			}
			public System.String LateDays
			{
				get
				{
					System.Int32? data = entity.LateDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LateDays = null;
					else entity.LateDays = Convert.ToInt32(value);
				}
			}
			public System.String EarlyLeaveDays
			{
				get
				{
					System.Int32? data = entity.EarlyLeaveDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EarlyLeaveDays = null;
					else entity.EarlyLeaveDays = Convert.ToInt32(value);
				}
			}
			public System.String DisciplinarySanctionsAmount
			{
				get
				{
					System.Decimal? data = entity.DisciplinarySanctionsAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisciplinarySanctionsAmount = null;
					else entity.DisciplinarySanctionsAmount = Convert.ToDecimal(value);
				}
			}
			public System.String BasicWorkingTime
			{
				get
				{
					System.Decimal? data = entity.BasicWorkingTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BasicWorkingTime = null;
					else entity.BasicWorkingTime = Convert.ToDecimal(value);
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
			public System.String ConvertOvertimeHours
			{
				get
				{
					System.Decimal? data = entity.ConvertOvertimeHours;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConvertOvertimeHours = null;
					else entity.ConvertOvertimeHours = Convert.ToDecimal(value);
				}
			}
			public System.String TotalWorkingTime
			{
				get
				{
					System.Decimal? data = entity.TotalWorkingTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalWorkingTime = null;
					else entity.TotalWorkingTime = Convert.ToDecimal(value);
				}
			}
			public System.String BasicFoodExspenses
			{
				get
				{
					System.Int32? data = entity.BasicFoodExspenses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BasicFoodExspenses = null;
					else entity.BasicFoodExspenses = Convert.ToInt32(value);
				}
			}
			public System.String OvertimeFoodExspenses
			{
				get
				{
					System.Int32? data = entity.OvertimeFoodExspenses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OvertimeFoodExspenses = null;
					else entity.OvertimeFoodExspenses = Convert.ToInt32(value);
				}
			}
			public System.String RamadhanFoodExspenses
			{
				get
				{
					System.Int32? data = entity.RamadhanFoodExspenses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RamadhanFoodExspenses = null;
					else entity.RamadhanFoodExspenses = Convert.ToInt32(value);
				}
			}
			public System.String Shift1Compensation
			{
				get
				{
					System.Int32? data = entity.Shift1Compensation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Shift1Compensation = null;
					else entity.Shift1Compensation = Convert.ToInt32(value);
				}
			}
			public System.String Shift2Compensation
			{
				get
				{
					System.Int32? data = entity.Shift2Compensation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Shift2Compensation = null;
					else entity.Shift2Compensation = Convert.ToInt32(value);
				}
			}
			public System.String Shift3Compensation
			{
				get
				{
					System.Int32? data = entity.Shift3Compensation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Shift3Compensation = null;
					else entity.Shift3Compensation = Convert.ToInt32(value);
				}
			}
			public System.String Shift4Compensation
			{
				get
				{
					System.Int32? data = entity.Shift4Compensation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Shift4Compensation = null;
					else entity.Shift4Compensation = Convert.ToInt32(value);
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
			private esMonthlyAttendance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceQuery query)
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
				throw new Exception("esMonthlyAttendance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MonthlyAttendance : esMonthlyAttendance
	{
	}

	[Serializable]
	abstract public class esMonthlyAttendanceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceMetadata.Meta();
			}
		}

		public esQueryItem MonthlyAttendanceID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.MonthlyAttendanceID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem PayDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.PayDays, esSystemType.Int32);
			}
		}

		public esQueryItem UnPayDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.UnPayDays, esSystemType.Int32);
			}
		}

		public esQueryItem AbsenceCount
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.AbsenceCount, esSystemType.Int32);
			}
		}

		public esQueryItem SRAttedanceInsentif
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.SRAttedanceInsentif, esSystemType.String);
			}
		}

		public esQueryItem WorkingDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.WorkingDays, esSystemType.Int32);
			}
		}

		public esQueryItem Holidays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.Holidays, esSystemType.Int32);
			}
		}

		public esQueryItem OvertimeDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.OvertimeDays, esSystemType.Int32);
			}
		}

		public esQueryItem HolidayWorking
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.HolidayWorking, esSystemType.Int32);
			}
		}

		public esQueryItem LateDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.LateDays, esSystemType.Int32);
			}
		}

		public esQueryItem EarlyLeaveDays
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.EarlyLeaveDays, esSystemType.Int32);
			}
		}

		public esQueryItem DisciplinarySanctionsAmount
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.DisciplinarySanctionsAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem BasicWorkingTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.BasicWorkingTime, esSystemType.Decimal);
			}
		}

		public esQueryItem OvertimeHours
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.OvertimeHours, esSystemType.Decimal);
			}
		}

		public esQueryItem ConvertOvertimeHours
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.ConvertOvertimeHours, esSystemType.Decimal);
			}
		}

		public esQueryItem TotalWorkingTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.TotalWorkingTime, esSystemType.Decimal);
			}
		}

		public esQueryItem BasicFoodExspenses
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.BasicFoodExspenses, esSystemType.Int32);
			}
		}

		public esQueryItem OvertimeFoodExspenses
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.OvertimeFoodExspenses, esSystemType.Int32);
			}
		}

		public esQueryItem RamadhanFoodExspenses
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.RamadhanFoodExspenses, esSystemType.Int32);
			}
		}

		public esQueryItem Shift1Compensation
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.Shift1Compensation, esSystemType.Int32);
			}
		}

		public esQueryItem Shift2Compensation
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.Shift2Compensation, esSystemType.Int32);
			}
		}

		public esQueryItem Shift3Compensation
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.Shift3Compensation, esSystemType.Int32);
			}
		}

		public esQueryItem Shift4Compensation
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.Shift4Compensation, esSystemType.Int32);
			}
		}

		public esQueryItem SRAttendanceFileFormat
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.SRAttendanceFileFormat, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MonthlyAttendanceCollection")]
	public partial class MonthlyAttendanceCollection : esMonthlyAttendanceCollection, IEnumerable<MonthlyAttendance>
	{
		public MonthlyAttendanceCollection()
		{

		}

		public static implicit operator List<MonthlyAttendance>(MonthlyAttendanceCollection coll)
		{
			List<MonthlyAttendance> list = new List<MonthlyAttendance>();

			foreach (MonthlyAttendance emp in coll)
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
				return MonthlyAttendanceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MonthlyAttendance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MonthlyAttendance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceQuery();
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
		public bool Load(MonthlyAttendanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MonthlyAttendance AddNew()
		{
			MonthlyAttendance entity = base.AddNewEntity() as MonthlyAttendance;

			return entity;
		}
		public MonthlyAttendance FindByPrimaryKey(Int64 monthlyAttendanceID)
		{
			return base.FindByPrimaryKey(monthlyAttendanceID) as MonthlyAttendance;
		}

		#region IEnumerable< MonthlyAttendance> Members

		IEnumerator<MonthlyAttendance> IEnumerable<MonthlyAttendance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MonthlyAttendance;
			}
		}

		#endregion

		private MonthlyAttendanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MonthlyAttendance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MonthlyAttendance ({MonthlyAttendanceID})")]
	[Serializable]
	public partial class MonthlyAttendance : esMonthlyAttendance
	{
		public MonthlyAttendance()
		{
		}

		public MonthlyAttendance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceMetadata.Meta();
			}
		}

		override protected esMonthlyAttendanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceQuery();
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
		public bool Load(MonthlyAttendanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MonthlyAttendanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MonthlyAttendanceQuery : esMonthlyAttendanceQuery
	{
		public MonthlyAttendanceQuery()
		{

		}

		public MonthlyAttendanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MonthlyAttendanceQuery";
		}
	}

	[Serializable]
	public partial class MonthlyAttendanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MonthlyAttendanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.MonthlyAttendanceID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.MonthlyAttendanceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.PayrollPeriodID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.PayDays, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.PayDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.UnPayDays, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.UnPayDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.AbsenceCount, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.AbsenceCount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.SRAttedanceInsentif, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.SRAttedanceInsentif;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.WorkingDays, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.WorkingDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.Holidays, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.Holidays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.OvertimeDays, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.OvertimeDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.HolidayWorking, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.HolidayWorking;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.LateDays, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.LateDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.EarlyLeaveDays, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.EarlyLeaveDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.DisciplinarySanctionsAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.DisciplinarySanctionsAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.BasicWorkingTime, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.BasicWorkingTime;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.OvertimeHours, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.OvertimeHours;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.ConvertOvertimeHours, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.ConvertOvertimeHours;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.TotalWorkingTime, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.TotalWorkingTime;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.BasicFoodExspenses, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.BasicFoodExspenses;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.OvertimeFoodExspenses, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.OvertimeFoodExspenses;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.RamadhanFoodExspenses, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.RamadhanFoodExspenses;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.Shift1Compensation, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.Shift1Compensation;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.Shift2Compensation, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.Shift2Compensation;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.Shift3Compensation, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.Shift3Compensation;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.Shift4Compensation, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.Shift4Compensation;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.SRAttendanceFileFormat, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.SRAttendanceFileFormat;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceMetadata.ColumnNames.LastUpdateByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = MonthlyAttendanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public MonthlyAttendanceMetadata Meta()
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
			public const string MonthlyAttendanceID = "MonthlyAttendanceID";
			public const string PersonID = "PersonID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string PayDays = "PayDays";
			public const string UnPayDays = "UnPayDays";
			public const string AbsenceCount = "AbsenceCount";
			public const string SRAttedanceInsentif = "SRAttedanceInsentif";
			public const string WorkingDays = "WorkingDays";
			public const string Holidays = "Holidays";
			public const string OvertimeDays = "OvertimeDays";
			public const string HolidayWorking = "HolidayWorking";
			public const string LateDays = "LateDays";
			public const string EarlyLeaveDays = "EarlyLeaveDays";
			public const string DisciplinarySanctionsAmount = "DisciplinarySanctionsAmount";
			public const string BasicWorkingTime = "BasicWorkingTime";
			public const string OvertimeHours = "OvertimeHours";
			public const string ConvertOvertimeHours = "ConvertOvertimeHours";
			public const string TotalWorkingTime = "TotalWorkingTime";
			public const string BasicFoodExspenses = "BasicFoodExspenses";
			public const string OvertimeFoodExspenses = "OvertimeFoodExspenses";
			public const string RamadhanFoodExspenses = "RamadhanFoodExspenses";
			public const string Shift1Compensation = "Shift1Compensation";
			public const string Shift2Compensation = "Shift2Compensation";
			public const string Shift3Compensation = "Shift3Compensation";
			public const string Shift4Compensation = "Shift4Compensation";
			public const string SRAttendanceFileFormat = "SRAttendanceFileFormat";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MonthlyAttendanceID = "MonthlyAttendanceID";
			public const string PersonID = "PersonID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string PayDays = "PayDays";
			public const string UnPayDays = "UnPayDays";
			public const string AbsenceCount = "AbsenceCount";
			public const string SRAttedanceInsentif = "SRAttedanceInsentif";
			public const string WorkingDays = "WorkingDays";
			public const string Holidays = "Holidays";
			public const string OvertimeDays = "OvertimeDays";
			public const string HolidayWorking = "HolidayWorking";
			public const string LateDays = "LateDays";
			public const string EarlyLeaveDays = "EarlyLeaveDays";
			public const string DisciplinarySanctionsAmount = "DisciplinarySanctionsAmount";
			public const string BasicWorkingTime = "BasicWorkingTime";
			public const string OvertimeHours = "OvertimeHours";
			public const string ConvertOvertimeHours = "ConvertOvertimeHours";
			public const string TotalWorkingTime = "TotalWorkingTime";
			public const string BasicFoodExspenses = "BasicFoodExspenses";
			public const string OvertimeFoodExspenses = "OvertimeFoodExspenses";
			public const string RamadhanFoodExspenses = "RamadhanFoodExspenses";
			public const string Shift1Compensation = "Shift1Compensation";
			public const string Shift2Compensation = "Shift2Compensation";
			public const string Shift3Compensation = "Shift3Compensation";
			public const string Shift4Compensation = "Shift4Compensation";
			public const string SRAttendanceFileFormat = "SRAttendanceFileFormat";
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
			lock (typeof(MonthlyAttendanceMetadata))
			{
				if (MonthlyAttendanceMetadata.mapDelegates == null)
				{
					MonthlyAttendanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MonthlyAttendanceMetadata.meta == null)
				{
					MonthlyAttendanceMetadata.meta = new MonthlyAttendanceMetadata();
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

				meta.AddTypeMap("MonthlyAttendanceID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UnPayDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AbsenceCount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAttedanceInsentif", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Holidays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OvertimeDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HolidayWorking", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LateDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EarlyLeaveDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DisciplinarySanctionsAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BasicWorkingTime", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OvertimeHours", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ConvertOvertimeHours", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalWorkingTime", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BasicFoodExspenses", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OvertimeFoodExspenses", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RamadhanFoodExspenses", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Shift1Compensation", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Shift2Compensation", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Shift3Compensation", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Shift4Compensation", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAttendanceFileFormat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MonthlyAttendance";
				meta.Destination = "MonthlyAttendance";
				meta.spInsert = "proc_MonthlyAttendanceInsert";
				meta.spUpdate = "proc_MonthlyAttendanceUpdate";
				meta.spDelete = "proc_MonthlyAttendanceDelete";
				meta.spLoadAll = "proc_MonthlyAttendanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_MonthlyAttendanceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MonthlyAttendanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
