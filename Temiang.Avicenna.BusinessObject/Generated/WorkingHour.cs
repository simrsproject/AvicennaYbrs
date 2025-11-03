/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/15/2022 9:51:44 AM
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
	abstract public class esWorkingHourCollection : esEntityCollectionWAuditLog
	{
		public esWorkingHourCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "WorkingHourCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingHourQuery query)
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
			this.InitQuery(query as esWorkingHourQuery);
		}
		#endregion

		virtual public WorkingHour DetachEntity(WorkingHour entity)
		{
			return base.DetachEntity(entity) as WorkingHour;
		}

		virtual public WorkingHour AttachEntity(WorkingHour entity)
		{
			return base.AttachEntity(entity) as WorkingHour;
		}

		virtual public void Combine(WorkingHourCollection collection)
		{
			base.Combine(collection);
		}

		new public WorkingHour this[int index]
		{
			get
			{
				return base[index] as WorkingHour;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingHour);
		}
	}

	[Serializable]
	abstract public class esWorkingHour : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingHourQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingHour()
		{
		}

		public esWorkingHour(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 workingHourID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingHourID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingHourID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 workingHourID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingHourID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingHourID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 workingHourID)
		{
			esWorkingHourQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingHourID == workingHourID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 workingHourID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingHourID", workingHourID);
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
						case "WorkingHourID": this.str.WorkingHourID = (string)value; break;
						case "WorkingHourName": this.str.WorkingHourName = (string)value; break;
						case "StartTime": this.str.StartTime = (string)value; break;
						case "MinimumStartTime": this.str.MinimumStartTime = (string)value; break;
						case "MaximumStartTime": this.str.MaximumStartTime = (string)value; break;
						case "EndTime": this.str.EndTime = (string)value; break;
						case "MinimumEndTime": this.str.MinimumEndTime = (string)value; break;
						case "MaximumEndTime": this.str.MaximumEndTime = (string)value; break;
						case "IsOvertimeWorkingHour": this.str.IsOvertimeWorkingHour = (string)value; break;
						case "OvertimeValueInMinutes": this.str.OvertimeValueInMinutes = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
						case "MealQty": this.str.MealQty = (string)value; break;
						case "SRShift": this.str.SRShift = (string)value; break;
						case "IsShiftLeader": this.str.IsShiftLeader = (string)value; break;
						case "IsNotValidated": this.str.IsNotValidated = (string)value; break;
						case "IsOffDay": this.str.IsOffDay = (string)value; break;
						case "IsCrossDay": this.str.IsCrossDay = (string)value; break;
						case "StartTime2": this.str.StartTime2 = (string)value; break;
						case "MinimumStartTime2": this.str.MinimumStartTime2 = (string)value; break;
						case "MaximumStartTime2": this.str.MaximumStartTime2 = (string)value; break;
						case "EndTime2": this.str.EndTime2 = (string)value; break;
						case "MinimumEndTime2": this.str.MinimumEndTime2 = (string)value; break;
						case "MaximumEndTime2": this.str.MaximumEndTime2 = (string)value; break;
						case "WorkingDay": this.str.WorkingDay = (string)value; break;
						case "SRWorkingDay": this.str.SRWorkingDay = (string)value; break;
						case "IsShiftLeaderNormalDay": this.str.IsShiftLeaderNormalDay = (string)value; break;
						case "IsShiftLeaderOffDay": this.str.IsShiftLeaderOffDay = (string)value; break;
						case "IsHoliday": this.str.IsHoliday = (string)value; break;
						case "IsShiftLeader2": this.str.IsShiftLeader2 = (string)value; break;
						case "IsLongShift": this.str.IsLongShift = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WorkingHourID":

							if (value == null || value is System.Int32)
								this.WorkingHourID = (System.Int32?)value;
							break;
						case "IsOvertimeWorkingHour":

							if (value == null || value is System.Boolean)
								this.IsOvertimeWorkingHour = (System.Boolean?)value;
							break;
						case "OvertimeValueInMinutes":

							if (value == null || value is System.Int32)
								this.OvertimeValueInMinutes = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "MealQty":

							if (value == null || value is System.Int32)
								this.MealQty = (System.Int32?)value;
							break;
						case "IsShiftLeader":

							if (value == null || value is System.Boolean)
								this.IsShiftLeader = (System.Boolean?)value;
							break;
						case "IsNotValidated":

							if (value == null || value is System.Boolean)
								this.IsNotValidated = (System.Boolean?)value;
							break;
						case "IsOffDay":

							if (value == null || value is System.Boolean)
								this.IsOffDay = (System.Boolean?)value;
							break;
						case "IsCrossDay":

							if (value == null || value is System.Boolean)
								this.IsCrossDay = (System.Boolean?)value;
							break;
						case "WorkingDay":

							if (value == null || value is System.Int16)
								this.WorkingDay = (System.Int16?)value;
							break;
						case "IsShiftLeaderNormalDay":

							if (value == null || value is System.Boolean)
								this.IsShiftLeaderNormalDay = (System.Boolean?)value;
							break;
						case "IsShiftLeaderOffDay":

							if (value == null || value is System.Boolean)
								this.IsShiftLeaderOffDay = (System.Boolean?)value;
							break;
						case "IsHoliday":

							if (value == null || value is System.Boolean)
								this.IsHoliday = (System.Boolean?)value;
							break;
						case "IsShiftLeader2":

							if (value == null || value is System.Boolean)
								this.IsShiftLeader2 = (System.Boolean?)value;
							break;
						case "IsLongShift":

							if (value == null || value is System.Boolean)
								this.IsLongShift = (System.Boolean?)value;
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
		/// Maps to WorkingHour.WorkingHourID
		/// </summary>
		virtual public System.Int32? WorkingHourID
		{
			get
			{
				return base.GetSystemInt32(WorkingHourMetadata.ColumnNames.WorkingHourID);
			}

			set
			{
				base.SetSystemInt32(WorkingHourMetadata.ColumnNames.WorkingHourID, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.WorkingHourName
		/// </summary>
		virtual public System.String WorkingHourName
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.WorkingHourName);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.WorkingHourName, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.StartTime
		/// </summary>
		virtual public System.String StartTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.StartTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.StartTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MinimumStartTime
		/// </summary>
		virtual public System.String MinimumStartTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MinimumStartTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MinimumStartTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MaximumStartTime
		/// </summary>
		virtual public System.String MaximumStartTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MaximumStartTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MaximumStartTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.EndTime
		/// </summary>
		virtual public System.String EndTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.EndTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.EndTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MinimumEndTime
		/// </summary>
		virtual public System.String MinimumEndTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MinimumEndTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MinimumEndTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MaximumEndTime
		/// </summary>
		virtual public System.String MaximumEndTime
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MaximumEndTime);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MaximumEndTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsOvertimeWorkingHour
		/// </summary>
		virtual public System.Boolean? IsOvertimeWorkingHour
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsOvertimeWorkingHour);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsOvertimeWorkingHour, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.OvertimeValueInMinutes
		/// </summary>
		virtual public System.Int32? OvertimeValueInMinutes
		{
			get
			{
				return base.GetSystemInt32(WorkingHourMetadata.ColumnNames.OvertimeValueInMinutes);
			}

			set
			{
				base.SetSystemInt32(WorkingHourMetadata.ColumnNames.OvertimeValueInMinutes, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingHourMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(WorkingHourMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.LastUpdateUserID);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.LastUpdateUserID, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MealQty
		/// </summary>
		virtual public System.Int32? MealQty
		{
			get
			{
				return base.GetSystemInt32(WorkingHourMetadata.ColumnNames.MealQty);
			}

			set
			{
				base.SetSystemInt32(WorkingHourMetadata.ColumnNames.MealQty, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.SRShift
		/// </summary>
		virtual public System.String SRShift
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.SRShift);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.SRShift, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsShiftLeader
		/// </summary>
		virtual public System.Boolean? IsShiftLeader
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeader);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeader, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsNotValidated
		/// </summary>
		virtual public System.Boolean? IsNotValidated
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsNotValidated);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsNotValidated, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsOffDay
		/// </summary>
		virtual public System.Boolean? IsOffDay
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsOffDay);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsOffDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsCrossDay
		/// </summary>
		virtual public System.Boolean? IsCrossDay
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsCrossDay);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsCrossDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.StartTime2
		/// </summary>
		virtual public System.String StartTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.StartTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.StartTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MinimumStartTime2
		/// </summary>
		virtual public System.String MinimumStartTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MinimumStartTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MinimumStartTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MaximumStartTime2
		/// </summary>
		virtual public System.String MaximumStartTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MaximumStartTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MaximumStartTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.EndTime2
		/// </summary>
		virtual public System.String EndTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.EndTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.EndTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MinimumEndTime2
		/// </summary>
		virtual public System.String MinimumEndTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MinimumEndTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MinimumEndTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.MaximumEndTime2
		/// </summary>
		virtual public System.String MaximumEndTime2
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.MaximumEndTime2);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.MaximumEndTime2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.WorkingDay
		/// </summary>
		virtual public System.Int16? WorkingDay
		{
			get
			{
				return base.GetSystemInt16(WorkingHourMetadata.ColumnNames.WorkingDay);
			}

			set
			{
				base.SetSystemInt16(WorkingHourMetadata.ColumnNames.WorkingDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.SRWorkingDay
		/// </summary>
		virtual public System.String SRWorkingDay
		{
			get
			{
				return base.GetSystemString(WorkingHourMetadata.ColumnNames.SRWorkingDay);
			}

			set
			{
				base.SetSystemString(WorkingHourMetadata.ColumnNames.SRWorkingDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsShiftLeaderNormalDay
		/// </summary>
		virtual public System.Boolean? IsShiftLeaderNormalDay
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeaderNormalDay);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeaderNormalDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsShiftLeaderOffDay
		/// </summary>
		virtual public System.Boolean? IsShiftLeaderOffDay
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeaderOffDay);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeaderOffDay, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsHoliday
		/// </summary>
		virtual public System.Boolean? IsHoliday
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsHoliday);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsHoliday, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsShiftLeader2
		/// </summary>
		virtual public System.Boolean? IsShiftLeader2
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeader2);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsShiftLeader2, value);
			}
		}
		/// <summary>
		/// Maps to WorkingHour.IsLongShift
		/// </summary>
		virtual public System.Boolean? IsLongShift
		{
			get
			{
				return base.GetSystemBoolean(WorkingHourMetadata.ColumnNames.IsLongShift);
			}

			set
			{
				base.SetSystemBoolean(WorkingHourMetadata.ColumnNames.IsLongShift, value);
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
			public esStrings(esWorkingHour entity)
			{
				this.entity = entity;
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
			public System.String WorkingHourName
			{
				get
				{
					System.String data = entity.WorkingHourName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourName = null;
					else entity.WorkingHourName = Convert.ToString(value);
				}
			}
			public System.String StartTime
			{
				get
				{
					System.String data = entity.StartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime = null;
					else entity.StartTime = Convert.ToString(value);
				}
			}
			public System.String MinimumStartTime
			{
				get
				{
					System.String data = entity.MinimumStartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumStartTime = null;
					else entity.MinimumStartTime = Convert.ToString(value);
				}
			}
			public System.String MaximumStartTime
			{
				get
				{
					System.String data = entity.MaximumStartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaximumStartTime = null;
					else entity.MaximumStartTime = Convert.ToString(value);
				}
			}
			public System.String EndTime
			{
				get
				{
					System.String data = entity.EndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime = null;
					else entity.EndTime = Convert.ToString(value);
				}
			}
			public System.String MinimumEndTime
			{
				get
				{
					System.String data = entity.MinimumEndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumEndTime = null;
					else entity.MinimumEndTime = Convert.ToString(value);
				}
			}
			public System.String MaximumEndTime
			{
				get
				{
					System.String data = entity.MaximumEndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaximumEndTime = null;
					else entity.MaximumEndTime = Convert.ToString(value);
				}
			}
			public System.String IsOvertimeWorkingHour
			{
				get
				{
					System.Boolean? data = entity.IsOvertimeWorkingHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOvertimeWorkingHour = null;
					else entity.IsOvertimeWorkingHour = Convert.ToBoolean(value);
				}
			}
			public System.String OvertimeValueInMinutes
			{
				get
				{
					System.Int32? data = entity.OvertimeValueInMinutes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OvertimeValueInMinutes = null;
					else entity.OvertimeValueInMinutes = Convert.ToInt32(value);
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
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			public System.String MealQty
			{
				get
				{
					System.Int32? data = entity.MealQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MealQty = null;
					else entity.MealQty = Convert.ToInt32(value);
				}
			}
			public System.String SRShift
			{
				get
				{
					System.String data = entity.SRShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRShift = null;
					else entity.SRShift = Convert.ToString(value);
				}
			}
			public System.String IsShiftLeader
			{
				get
				{
					System.Boolean? data = entity.IsShiftLeader;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiftLeader = null;
					else entity.IsShiftLeader = Convert.ToBoolean(value);
				}
			}
			public System.String IsNotValidated
			{
				get
				{
					System.Boolean? data = entity.IsNotValidated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotValidated = null;
					else entity.IsNotValidated = Convert.ToBoolean(value);
				}
			}
			public System.String IsOffDay
			{
				get
				{
					System.Boolean? data = entity.IsOffDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOffDay = null;
					else entity.IsOffDay = Convert.ToBoolean(value);
				}
			}
			public System.String IsCrossDay
			{
				get
				{
					System.Boolean? data = entity.IsCrossDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossDay = null;
					else entity.IsCrossDay = Convert.ToBoolean(value);
				}
			}
			public System.String StartTime2
			{
				get
				{
					System.String data = entity.StartTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime2 = null;
					else entity.StartTime2 = Convert.ToString(value);
				}
			}
			public System.String MinimumStartTime2
			{
				get
				{
					System.String data = entity.MinimumStartTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumStartTime2 = null;
					else entity.MinimumStartTime2 = Convert.ToString(value);
				}
			}
			public System.String MaximumStartTime2
			{
				get
				{
					System.String data = entity.MaximumStartTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaximumStartTime2 = null;
					else entity.MaximumStartTime2 = Convert.ToString(value);
				}
			}
			public System.String EndTime2
			{
				get
				{
					System.String data = entity.EndTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime2 = null;
					else entity.EndTime2 = Convert.ToString(value);
				}
			}
			public System.String MinimumEndTime2
			{
				get
				{
					System.String data = entity.MinimumEndTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinimumEndTime2 = null;
					else entity.MinimumEndTime2 = Convert.ToString(value);
				}
			}
			public System.String MaximumEndTime2
			{
				get
				{
					System.String data = entity.MaximumEndTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaximumEndTime2 = null;
					else entity.MaximumEndTime2 = Convert.ToString(value);
				}
			}
			public System.String WorkingDay
			{
				get
				{
					System.Int16? data = entity.WorkingDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingDay = null;
					else entity.WorkingDay = Convert.ToInt16(value);
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
			public System.String IsShiftLeaderNormalDay
			{
				get
				{
					System.Boolean? data = entity.IsShiftLeaderNormalDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiftLeaderNormalDay = null;
					else entity.IsShiftLeaderNormalDay = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiftLeaderOffDay
			{
				get
				{
					System.Boolean? data = entity.IsShiftLeaderOffDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiftLeaderOffDay = null;
					else entity.IsShiftLeaderOffDay = Convert.ToBoolean(value);
				}
			}
			public System.String IsHoliday
			{
				get
				{
					System.Boolean? data = entity.IsHoliday;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHoliday = null;
					else entity.IsHoliday = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiftLeader2
			{
				get
				{
					System.Boolean? data = entity.IsShiftLeader2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiftLeader2 = null;
					else entity.IsShiftLeader2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsLongShift
			{
				get
				{
					System.Boolean? data = entity.IsLongShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLongShift = null;
					else entity.IsLongShift = Convert.ToBoolean(value);
				}
			}
			private esWorkingHour entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingHourQuery query)
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
				throw new Exception("esWorkingHour can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class WorkingHour : esWorkingHour
	{
	}

	[Serializable]
	abstract public class esWorkingHourQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return WorkingHourMetadata.Meta();
			}
		}

		public esQueryItem WorkingHourID
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.WorkingHourID, esSystemType.Int32);
			}
		}

		public esQueryItem WorkingHourName
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.WorkingHourName, esSystemType.String);
			}
		}

		public esQueryItem StartTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.StartTime, esSystemType.String);
			}
		}

		public esQueryItem MinimumStartTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MinimumStartTime, esSystemType.String);
			}
		}

		public esQueryItem MaximumStartTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MaximumStartTime, esSystemType.String);
			}
		}

		public esQueryItem EndTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.EndTime, esSystemType.String);
			}
		}

		public esQueryItem MinimumEndTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MinimumEndTime, esSystemType.String);
			}
		}

		public esQueryItem MaximumEndTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MaximumEndTime, esSystemType.String);
			}
		}

		public esQueryItem IsOvertimeWorkingHour
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsOvertimeWorkingHour, esSystemType.Boolean);
			}
		}

		public esQueryItem OvertimeValueInMinutes
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.OvertimeValueInMinutes, esSystemType.Int32);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		}

		public esQueryItem MealQty
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MealQty, esSystemType.Int32);
			}
		}

		public esQueryItem SRShift
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.SRShift, esSystemType.String);
			}
		}

		public esQueryItem IsShiftLeader
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsShiftLeader, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNotValidated
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsNotValidated, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOffDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsOffDay, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCrossDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsCrossDay, esSystemType.Boolean);
			}
		}

		public esQueryItem StartTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.StartTime2, esSystemType.String);
			}
		}

		public esQueryItem MinimumStartTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MinimumStartTime2, esSystemType.String);
			}
		}

		public esQueryItem MaximumStartTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MaximumStartTime2, esSystemType.String);
			}
		}

		public esQueryItem EndTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.EndTime2, esSystemType.String);
			}
		}

		public esQueryItem MinimumEndTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MinimumEndTime2, esSystemType.String);
			}
		}

		public esQueryItem MaximumEndTime2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.MaximumEndTime2, esSystemType.String);
			}
		}

		public esQueryItem WorkingDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.WorkingDay, esSystemType.Int16);
			}
		}

		public esQueryItem SRWorkingDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.SRWorkingDay, esSystemType.String);
			}
		}

		public esQueryItem IsShiftLeaderNormalDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsShiftLeaderNormalDay, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiftLeaderOffDay
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsShiftLeaderOffDay, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHoliday
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsHoliday, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiftLeader2
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsShiftLeader2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLongShift
		{
			get
			{
				return new esQueryItem(this, WorkingHourMetadata.ColumnNames.IsLongShift, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingHourCollection")]
	public partial class WorkingHourCollection : esWorkingHourCollection, IEnumerable<WorkingHour>
	{
		public WorkingHourCollection()
		{

		}

		public static implicit operator List<WorkingHour>(WorkingHourCollection coll)
		{
			List<WorkingHour> list = new List<WorkingHour>();

			foreach (WorkingHour emp in coll)
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
				return WorkingHourMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingHourQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingHour(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingHour();
		}

		#endregion

		[BrowsableAttribute(false)]
		public WorkingHourQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingHourQuery();
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
		public bool Load(WorkingHourQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public WorkingHour AddNew()
		{
			WorkingHour entity = base.AddNewEntity() as WorkingHour;

			return entity;
		}
		public WorkingHour FindByPrimaryKey(Int32 workingHourID)
		{
			return base.FindByPrimaryKey(workingHourID) as WorkingHour;
		}

		#region IEnumerable< WorkingHour> Members

		IEnumerator<WorkingHour> IEnumerable<WorkingHour>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as WorkingHour;
			}
		}

		#endregion

		private WorkingHourQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingHour' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("WorkingHour ({WorkingHourID})")]
	[Serializable]
	public partial class WorkingHour : esWorkingHour
	{
		public WorkingHour()
		{
		}

		public WorkingHour(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingHourMetadata.Meta();
			}
		}

		override protected esWorkingHourQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingHourQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public WorkingHourQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingHourQuery();
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
		public bool Load(WorkingHourQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private WorkingHourQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class WorkingHourQuery : esWorkingHourQuery
	{
		public WorkingHourQuery()
		{

		}

		public WorkingHourQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "WorkingHourQuery";
		}
	}

	[Serializable]
	public partial class WorkingHourMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingHourMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.WorkingHourID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingHourMetadata.PropertyNames.WorkingHourID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.WorkingHourName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.WorkingHourName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.StartTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.StartTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MinimumStartTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MinimumStartTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MaximumStartTime, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MaximumStartTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.EndTime, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.EndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MinimumEndTime, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MinimumEndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MaximumEndTime, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MaximumEndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsOvertimeWorkingHour, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsOvertimeWorkingHour;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.OvertimeValueInMinutes, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingHourMetadata.PropertyNames.OvertimeValueInMinutes;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingHourMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.LastUpdateUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MealQty, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MealQty;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.SRShift, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.SRShift;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsShiftLeader, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsShiftLeader;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsNotValidated, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsNotValidated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsOffDay, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsOffDay;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsCrossDay, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsCrossDay;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.StartTime2, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.StartTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MinimumStartTime2, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MinimumStartTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MaximumStartTime2, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MaximumStartTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.EndTime2, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.EndTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MinimumEndTime2, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MinimumEndTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.MaximumEndTime2, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.MaximumEndTime2;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.WorkingDay, 25, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = WorkingHourMetadata.PropertyNames.WorkingDay;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.SRWorkingDay, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingHourMetadata.PropertyNames.SRWorkingDay;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsShiftLeaderNormalDay, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsShiftLeaderNormalDay;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsShiftLeaderOffDay, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsShiftLeaderOffDay;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsHoliday, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsHoliday;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsShiftLeader2, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsShiftLeader2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WorkingHourMetadata.ColumnNames.IsLongShift, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WorkingHourMetadata.PropertyNames.IsLongShift;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public WorkingHourMetadata Meta()
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
			public const string WorkingHourID = "WorkingHourID";
			public const string WorkingHourName = "WorkingHourName";
			public const string StartTime = "StartTime";
			public const string MinimumStartTime = "MinimumStartTime";
			public const string MaximumStartTime = "MaximumStartTime";
			public const string EndTime = "EndTime";
			public const string MinimumEndTime = "MinimumEndTime";
			public const string MaximumEndTime = "MaximumEndTime";
			public const string IsOvertimeWorkingHour = "IsOvertimeWorkingHour";
			public const string OvertimeValueInMinutes = "OvertimeValueInMinutes";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateUserID = "LastUpdateUserID";
			public const string MealQty = "MealQty";
			public const string SRShift = "SRShift";
			public const string IsShiftLeader = "IsShiftLeader";
			public const string IsNotValidated = "IsNotValidated";
			public const string IsOffDay = "IsOffDay";
			public const string IsCrossDay = "IsCrossDay";
			public const string StartTime2 = "StartTime2";
			public const string MinimumStartTime2 = "MinimumStartTime2";
			public const string MaximumStartTime2 = "MaximumStartTime2";
			public const string EndTime2 = "EndTime2";
			public const string MinimumEndTime2 = "MinimumEndTime2";
			public const string MaximumEndTime2 = "MaximumEndTime2";
			public const string WorkingDay = "WorkingDay";
			public const string SRWorkingDay = "SRWorkingDay";
			public const string IsShiftLeaderNormalDay = "IsShiftLeaderNormalDay";
			public const string IsShiftLeaderOffDay = "IsShiftLeaderOffDay";
			public const string IsHoliday = "IsHoliday";
			public const string IsShiftLeader2 = "IsShiftLeader2";
			public const string IsLongShift = "IsLongShift";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WorkingHourID = "WorkingHourID";
			public const string WorkingHourName = "WorkingHourName";
			public const string StartTime = "StartTime";
			public const string MinimumStartTime = "MinimumStartTime";
			public const string MaximumStartTime = "MaximumStartTime";
			public const string EndTime = "EndTime";
			public const string MinimumEndTime = "MinimumEndTime";
			public const string MaximumEndTime = "MaximumEndTime";
			public const string IsOvertimeWorkingHour = "IsOvertimeWorkingHour";
			public const string OvertimeValueInMinutes = "OvertimeValueInMinutes";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateUserID = "LastUpdateUserID";
			public const string MealQty = "MealQty";
			public const string SRShift = "SRShift";
			public const string IsShiftLeader = "IsShiftLeader";
			public const string IsNotValidated = "IsNotValidated";
			public const string IsOffDay = "IsOffDay";
			public const string IsCrossDay = "IsCrossDay";
			public const string StartTime2 = "StartTime2";
			public const string MinimumStartTime2 = "MinimumStartTime2";
			public const string MaximumStartTime2 = "MaximumStartTime2";
			public const string EndTime2 = "EndTime2";
			public const string MinimumEndTime2 = "MinimumEndTime2";
			public const string MaximumEndTime2 = "MaximumEndTime2";
			public const string WorkingDay = "WorkingDay";
			public const string SRWorkingDay = "SRWorkingDay";
			public const string IsShiftLeaderNormalDay = "IsShiftLeaderNormalDay";
			public const string IsShiftLeaderOffDay = "IsShiftLeaderOffDay";
			public const string IsHoliday = "IsHoliday";
			public const string IsShiftLeader2 = "IsShiftLeader2";
			public const string IsLongShift = "IsLongShift";
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
			lock (typeof(WorkingHourMetadata))
			{
				if (WorkingHourMetadata.mapDelegates == null)
				{
					WorkingHourMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (WorkingHourMetadata.meta == null)
				{
					WorkingHourMetadata.meta = new WorkingHourMetadata();
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

				meta.AddTypeMap("WorkingHourID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinimumStartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaximumStartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinimumEndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaximumEndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOvertimeWorkingHour", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OvertimeValueInMinutes", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MealQty", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRShift", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsShiftLeader", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNotValidated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOffDay", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCrossDay", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("StartTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinimumStartTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaximumStartTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinimumEndTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaximumEndTime2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingDay", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRWorkingDay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsShiftLeaderNormalDay", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiftLeaderOffDay", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHoliday", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiftLeader2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLongShift", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "WorkingHour";
				meta.Destination = "WorkingHour";
				meta.spInsert = "proc_WorkingHourInsert";
				meta.spUpdate = "proc_WorkingHourUpdate";
				meta.spDelete = "proc_WorkingHourDelete";
				meta.spLoadAll = "proc_WorkingHourLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingHourLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingHourMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
