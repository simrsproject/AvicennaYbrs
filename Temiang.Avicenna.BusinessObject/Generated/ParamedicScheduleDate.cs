/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/1/2021 2:12:58 PM
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
	abstract public class esParamedicScheduleDateCollection : esEntityCollectionWAuditLog
	{
		public esParamedicScheduleDateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ParamedicScheduleDateCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicScheduleDateQuery query)
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
			this.InitQuery(query as esParamedicScheduleDateQuery);
		}
		#endregion

		virtual public ParamedicScheduleDate DetachEntity(ParamedicScheduleDate entity)
		{
			return base.DetachEntity(entity) as ParamedicScheduleDate;
		}

		virtual public ParamedicScheduleDate AttachEntity(ParamedicScheduleDate entity)
		{
			return base.AttachEntity(entity) as ParamedicScheduleDate;
		}

		virtual public void Combine(ParamedicScheduleDateCollection collection)
		{
			base.Combine(collection);
		}

		new public ParamedicScheduleDate this[int index]
		{
			get
			{
				return base[index] as ParamedicScheduleDate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicScheduleDate);
		}
	}

	[Serializable]
	abstract public class esParamedicScheduleDate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicScheduleDateQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicScheduleDate()
		{
		}

		public esParamedicScheduleDate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String paramedicID, String periodYear, DateTime scheduleDate)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, periodYear, scheduleDate);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, periodYear, scheduleDate);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String paramedicID, String periodYear, DateTime scheduleDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, periodYear, scheduleDate);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, periodYear, scheduleDate);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String paramedicID, String periodYear, DateTime scheduleDate)
		{
			esParamedicScheduleDateQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ParamedicID == paramedicID, query.PeriodYear == periodYear, query.ScheduleDate == scheduleDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String paramedicID, String periodYear, DateTime scheduleDate)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ParamedicID", paramedicID);
			parms.Add("PeriodYear", periodYear);
			parms.Add("ScheduleDate", scheduleDate);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
						case "OperationalTimeID": this.str.OperationalTimeID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsClosedTime1": this.str.IsClosedTime1 = (string)value; break;
						case "ClosedDateTime1": this.str.ClosedDateTime1 = (string)value; break;
						case "ClosedTime1ByUserID": this.str.ClosedTime1ByUserID = (string)value; break;
						case "IsClosedTime2": this.str.IsClosedTime2 = (string)value; break;
						case "ClosedDateTime2": this.str.ClosedDateTime2 = (string)value; break;
						case "ClosedTime2ByUserID": this.str.ClosedTime2ByUserID = (string)value; break;
						case "IsClosedTime3": this.str.IsClosedTime3 = (string)value; break;
						case "ClosedDateTime3": this.str.ClosedDateTime3 = (string)value; break;
						case "ClosedTime3ByUserID": this.str.ClosedTime3ByUserID = (string)value; break;
						case "IsClosedTime4": this.str.IsClosedTime4 = (string)value; break;
						case "ClosedDateTime4": this.str.ClosedDateTime4 = (string)value; break;
						case "ClosedTime4ByUserID": this.str.ClosedTime4ByUserID = (string)value; break;
						case "IsClosedTime5": this.str.IsClosedTime5 = (string)value; break;
						case "ClosedDateTime5": this.str.ClosedDateTime5 = (string)value; break;
						case "ClosedTime5ByUserID": this.str.ClosedTime5ByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsIpr": this.str.IsIpr = (string)value; break;
						case "IsOpr": this.str.IsOpr = (string)value; break;
						case "IsEmr": this.str.IsEmr = (string)value; break;
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
						case "AddQuota": this.str.AddQuota = (string)value; break;
						case "AddQuotaOnline": this.str.AddQuotaOnline = (string)value; break;
						case "AddQuotaBpjs": this.str.AddQuotaBpjs = (string)value; break;
						case "AddQuotaBpjsOnline": this.str.AddQuotaBpjsOnline = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ScheduleDate":

							if (value == null || value is System.DateTime)
								this.ScheduleDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsClosedTime1":

							if (value == null || value is System.Boolean)
								this.IsClosedTime1 = (System.Boolean?)value;
							break;
						case "ClosedDateTime1":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime1 = (System.DateTime?)value;
							break;
						case "IsClosedTime2":

							if (value == null || value is System.Boolean)
								this.IsClosedTime2 = (System.Boolean?)value;
							break;
						case "ClosedDateTime2":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime2 = (System.DateTime?)value;
							break;
						case "IsClosedTime3":

							if (value == null || value is System.Boolean)
								this.IsClosedTime3 = (System.Boolean?)value;
							break;
						case "ClosedDateTime3":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime3 = (System.DateTime?)value;
							break;
						case "IsClosedTime4":

							if (value == null || value is System.Boolean)
								this.IsClosedTime4 = (System.Boolean?)value;
							break;
						case "ClosedDateTime4":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime4 = (System.DateTime?)value;
							break;
						case "IsClosedTime5":

							if (value == null || value is System.Boolean)
								this.IsClosedTime5 = (System.Boolean?)value;
							break;
						case "ClosedDateTime5":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime5 = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsIpr":

							if (value == null || value is System.Boolean)
								this.IsIpr = (System.Boolean?)value;
							break;
						case "IsOpr":

							if (value == null || value is System.Boolean)
								this.IsOpr = (System.Boolean?)value;
							break;
						case "IsEmr":

							if (value == null || value is System.Boolean)
								this.IsEmr = (System.Boolean?)value;
							break;
						case "AddQuota":

							if (value == null || value is System.Int16)
								this.AddQuota = (System.Int16?)value;
							break;
						case "AddQuotaOnline":

							if (value == null || value is System.Int16)
								this.AddQuotaOnline = (System.Int16?)value;
							break;
						case "AddQuotaBpjs":

							if (value == null || value is System.Int16)
								this.AddQuotaBpjs = (System.Int16?)value;
							break;
						case "AddQuotaBpjsOnline":

							if (value == null || value is System.Int16)
								this.AddQuotaBpjsOnline = (System.Int16?)value;
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
		/// Maps to ParamedicScheduleDate.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ScheduleDate
		/// </summary>
		virtual public System.DateTime? ScheduleDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ScheduleDate);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ScheduleDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.OperationalTimeID
		/// </summary>
		virtual public System.String OperationalTimeID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.OperationalTimeID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.OperationalTimeID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsClosedTime1
		/// </summary>
		virtual public System.Boolean? IsClosedTime1
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime1);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime1, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedDateTime1
		/// </summary>
		virtual public System.DateTime? ClosedDateTime1
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime1);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime1, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedTime1ByUserID
		/// </summary>
		virtual public System.String ClosedTime1ByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime1ByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsClosedTime2
		/// </summary>
		virtual public System.Boolean? IsClosedTime2
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime2);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime2, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedDateTime2
		/// </summary>
		virtual public System.DateTime? ClosedDateTime2
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime2);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime2, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedTime2ByUserID
		/// </summary>
		virtual public System.String ClosedTime2ByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime2ByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsClosedTime3
		/// </summary>
		virtual public System.Boolean? IsClosedTime3
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime3);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime3, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedDateTime3
		/// </summary>
		virtual public System.DateTime? ClosedDateTime3
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime3);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime3, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedTime3ByUserID
		/// </summary>
		virtual public System.String ClosedTime3ByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime3ByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsClosedTime4
		/// </summary>
		virtual public System.Boolean? IsClosedTime4
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime4);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime4, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedDateTime4
		/// </summary>
		virtual public System.DateTime? ClosedDateTime4
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime4);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime4, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedTime4ByUserID
		/// </summary>
		virtual public System.String ClosedTime4ByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime4ByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsClosedTime5
		/// </summary>
		virtual public System.Boolean? IsClosedTime5
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime5);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime5, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedDateTime5
		/// </summary>
		virtual public System.DateTime? ClosedDateTime5
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime5);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime5, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.ClosedTime5ByUserID
		/// </summary>
		virtual public System.String ClosedTime5ByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime5ByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime5ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsIpr
		/// </summary>
		virtual public System.Boolean? IsIpr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsIpr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsIpr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsOpr
		/// </summary>
		virtual public System.Boolean? IsOpr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsOpr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsOpr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.IsEmr
		/// </summary>
		virtual public System.Boolean? IsEmr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsEmr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateMetadata.ColumnNames.IsEmr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.PeriodMonth
		/// </summary>
		virtual public System.String PeriodMonth
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateMetadata.ColumnNames.PeriodMonth);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.AddQuota
		/// </summary>
		virtual public System.Int16? AddQuota
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuota);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuota, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.AddQuotaOnline
		/// </summary>
		virtual public System.Int16? AddQuotaOnline
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaOnline);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaOnline, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.AddQuotaBpjs
		/// </summary>
		virtual public System.Int16? AddQuotaBpjs
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjs);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjs, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDate.AddQuotaBpjsOnline
		/// </summary>
		virtual public System.Int16? AddQuotaBpjsOnline
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjsOnline);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjsOnline, value);
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
			public esStrings(esParamedicScheduleDate entity)
			{
				this.entity = entity;
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
			public System.String ScheduleDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleDate = null;
					else entity.ScheduleDate = Convert.ToDateTime(value);
				}
			}
			public System.String OperationalTimeID
			{
				get
				{
					System.String data = entity.OperationalTimeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeID = null;
					else entity.OperationalTimeID = Convert.ToString(value);
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
			public System.String IsClosedTime1
			{
				get
				{
					System.Boolean? data = entity.IsClosedTime1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedTime1 = null;
					else entity.IsClosedTime1 = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime1
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime1 = null;
					else entity.ClosedDateTime1 = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedTime1ByUserID
			{
				get
				{
					System.String data = entity.ClosedTime1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedTime1ByUserID = null;
					else entity.ClosedTime1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsClosedTime2
			{
				get
				{
					System.Boolean? data = entity.IsClosedTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedTime2 = null;
					else entity.IsClosedTime2 = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime2
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime2 = null;
					else entity.ClosedDateTime2 = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedTime2ByUserID
			{
				get
				{
					System.String data = entity.ClosedTime2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedTime2ByUserID = null;
					else entity.ClosedTime2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsClosedTime3
			{
				get
				{
					System.Boolean? data = entity.IsClosedTime3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedTime3 = null;
					else entity.IsClosedTime3 = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime3
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime3 = null;
					else entity.ClosedDateTime3 = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedTime3ByUserID
			{
				get
				{
					System.String data = entity.ClosedTime3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedTime3ByUserID = null;
					else entity.ClosedTime3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsClosedTime4
			{
				get
				{
					System.Boolean? data = entity.IsClosedTime4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedTime4 = null;
					else entity.IsClosedTime4 = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime4
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime4 = null;
					else entity.ClosedDateTime4 = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedTime4ByUserID
			{
				get
				{
					System.String data = entity.ClosedTime4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedTime4ByUserID = null;
					else entity.ClosedTime4ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsClosedTime5
			{
				get
				{
					System.Boolean? data = entity.IsClosedTime5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedTime5 = null;
					else entity.IsClosedTime5 = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime5
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime5 = null;
					else entity.ClosedDateTime5 = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedTime5ByUserID
			{
				get
				{
					System.String data = entity.ClosedTime5ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedTime5ByUserID = null;
					else entity.ClosedTime5ByUserID = Convert.ToString(value);
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
			public System.String IsIpr
			{
				get
				{
					System.Boolean? data = entity.IsIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIpr = null;
					else entity.IsIpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsOpr
			{
				get
				{
					System.Boolean? data = entity.IsOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpr = null;
					else entity.IsOpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsEmr
			{
				get
				{
					System.Boolean? data = entity.IsEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmr = null;
					else entity.IsEmr = Convert.ToBoolean(value);
				}
			}
			public System.String PeriodMonth
			{
				get
				{
					System.String data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToString(value);
				}
			}
			public System.String AddQuota
			{
				get
				{
					System.Int16? data = entity.AddQuota;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddQuota = null;
					else entity.AddQuota = Convert.ToInt16(value);
				}
			}
			public System.String AddQuotaOnline
			{
				get
				{
					System.Int16? data = entity.AddQuotaOnline;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddQuotaOnline = null;
					else entity.AddQuotaOnline = Convert.ToInt16(value);
				}
			}
			public System.String AddQuotaBpjs
			{
				get
				{
					System.Int16? data = entity.AddQuotaBpjs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddQuotaBpjs = null;
					else entity.AddQuotaBpjs = Convert.ToInt16(value);
				}
			}
			public System.String AddQuotaBpjsOnline
			{
				get
				{
					System.Int16? data = entity.AddQuotaBpjsOnline;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddQuotaBpjsOnline = null;
					else entity.AddQuotaBpjsOnline = Convert.ToInt16(value);
				}
			}
			private esParamedicScheduleDate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicScheduleDateQuery query)
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
				throw new Exception("esParamedicScheduleDate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicScheduleDate : esParamedicScheduleDate
	{
	}

	[Serializable]
	abstract public class esParamedicScheduleDateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleDateMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem ScheduleDate
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
			}
		}

		public esQueryItem OperationalTimeID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.OperationalTimeID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosedTime1
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime1, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime1
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime1, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedTime1ByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedTime1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosedTime2
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime2, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime2
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime2, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedTime2ByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedTime2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosedTime3
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime3, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime3
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime3, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedTime3ByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedTime3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosedTime4
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime4, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime4
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime4, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedTime4ByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedTime4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosedTime5
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime5, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime5
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime5, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedTime5ByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.ClosedTime5ByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsIpr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsIpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOpr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsOpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.IsEmr, esSystemType.Boolean);
			}
		}

		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.PeriodMonth, esSystemType.String);
			}
		}

		public esQueryItem AddQuota
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.AddQuota, esSystemType.Int16);
			}
		}

		public esQueryItem AddQuotaOnline
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.AddQuotaOnline, esSystemType.Int16);
			}
		}

		public esQueryItem AddQuotaBpjs
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjs, esSystemType.Int16);
			}
		}

		public esQueryItem AddQuotaBpjsOnline
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjsOnline, esSystemType.Int16);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicScheduleDateCollection")]
	public partial class ParamedicScheduleDateCollection : esParamedicScheduleDateCollection, IEnumerable<ParamedicScheduleDate>
	{
		public ParamedicScheduleDateCollection()
		{

		}

		public static implicit operator List<ParamedicScheduleDate>(ParamedicScheduleDateCollection coll)
		{
			List<ParamedicScheduleDate> list = new List<ParamedicScheduleDate>();

			foreach (ParamedicScheduleDate emp in coll)
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
				return ParamedicScheduleDateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicScheduleDate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicScheduleDate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleDateQuery();
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
		public bool Load(ParamedicScheduleDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicScheduleDate AddNew()
		{
			ParamedicScheduleDate entity = base.AddNewEntity() as ParamedicScheduleDate;

			return entity;
		}
		public ParamedicScheduleDate FindByPrimaryKey(String serviceUnitID, String paramedicID, String periodYear, DateTime scheduleDate)
		{
			return base.FindByPrimaryKey(serviceUnitID, paramedicID, periodYear, scheduleDate) as ParamedicScheduleDate;
		}

		#region IEnumerable< ParamedicScheduleDate> Members

		IEnumerator<ParamedicScheduleDate> IEnumerable<ParamedicScheduleDate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicScheduleDate;
			}
		}

		#endregion

		private ParamedicScheduleDateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicScheduleDate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicScheduleDate ({ServiceUnitID, ParamedicID, PeriodYear, ScheduleDate})")]
	[Serializable]
	public partial class ParamedicScheduleDate : esParamedicScheduleDate
	{
		public ParamedicScheduleDate()
		{
		}

		public ParamedicScheduleDate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleDateMetadata.Meta();
			}
		}

		override protected esParamedicScheduleDateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleDateQuery();
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
		public bool Load(ParamedicScheduleDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ParamedicScheduleDateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicScheduleDateQuery : esParamedicScheduleDateQuery
	{
		public ParamedicScheduleDateQuery()
		{

		}

		public ParamedicScheduleDateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ParamedicScheduleDateQuery";
		}
	}

	[Serializable]
	public partial class ParamedicScheduleDateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicScheduleDateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ScheduleDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ScheduleDate;
			c.IsInPrimaryKey = true;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.OperationalTimeID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.OperationalTimeID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime1, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsClosedTime1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime1, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedDateTime1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime1ByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedTime1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime2, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsClosedTime2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime2, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedDateTime2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime2ByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedTime2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime3, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsClosedTime3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime3, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedDateTime3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime3ByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedTime3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime4, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsClosedTime4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime4, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedDateTime4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime4ByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedTime4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsClosedTime5, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsClosedTime5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedDateTime5, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedDateTime5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.ClosedTime5ByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.ClosedTime5ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.CreatedDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.CreatedByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsIpr, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsIpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsOpr, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsOpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.IsEmr, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.IsEmr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.PeriodMonth, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.PeriodMonth;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.AddQuota, 28, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.AddQuota;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaOnline, 29, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.AddQuotaOnline;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjs, 30, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.AddQuotaBpjs;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateMetadata.ColumnNames.AddQuotaBpjsOnline, 31, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleDateMetadata.PropertyNames.AddQuotaBpjsOnline;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ParamedicScheduleDateMetadata Meta()
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
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PeriodYear = "PeriodYear";
			public const string ScheduleDate = "ScheduleDate";
			public const string OperationalTimeID = "OperationalTimeID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsClosedTime1 = "IsClosedTime1";
			public const string ClosedDateTime1 = "ClosedDateTime1";
			public const string ClosedTime1ByUserID = "ClosedTime1ByUserID";
			public const string IsClosedTime2 = "IsClosedTime2";
			public const string ClosedDateTime2 = "ClosedDateTime2";
			public const string ClosedTime2ByUserID = "ClosedTime2ByUserID";
			public const string IsClosedTime3 = "IsClosedTime3";
			public const string ClosedDateTime3 = "ClosedDateTime3";
			public const string ClosedTime3ByUserID = "ClosedTime3ByUserID";
			public const string IsClosedTime4 = "IsClosedTime4";
			public const string ClosedDateTime4 = "ClosedDateTime4";
			public const string ClosedTime4ByUserID = "ClosedTime4ByUserID";
			public const string IsClosedTime5 = "IsClosedTime5";
			public const string ClosedDateTime5 = "ClosedDateTime5";
			public const string ClosedTime5ByUserID = "ClosedTime5ByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsIpr = "IsIpr";
			public const string IsOpr = "IsOpr";
			public const string IsEmr = "IsEmr";
			public const string PeriodMonth = "PeriodMonth";
			public const string AddQuota = "AddQuota";
			public const string AddQuotaOnline = "AddQuotaOnline";
			public const string AddQuotaBpjs = "AddQuotaBpjs";
			public const string AddQuotaBpjsOnline = "AddQuotaBpjsOnline";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PeriodYear = "PeriodYear";
			public const string ScheduleDate = "ScheduleDate";
			public const string OperationalTimeID = "OperationalTimeID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsClosedTime1 = "IsClosedTime1";
			public const string ClosedDateTime1 = "ClosedDateTime1";
			public const string ClosedTime1ByUserID = "ClosedTime1ByUserID";
			public const string IsClosedTime2 = "IsClosedTime2";
			public const string ClosedDateTime2 = "ClosedDateTime2";
			public const string ClosedTime2ByUserID = "ClosedTime2ByUserID";
			public const string IsClosedTime3 = "IsClosedTime3";
			public const string ClosedDateTime3 = "ClosedDateTime3";
			public const string ClosedTime3ByUserID = "ClosedTime3ByUserID";
			public const string IsClosedTime4 = "IsClosedTime4";
			public const string ClosedDateTime4 = "ClosedDateTime4";
			public const string ClosedTime4ByUserID = "ClosedTime4ByUserID";
			public const string IsClosedTime5 = "IsClosedTime5";
			public const string ClosedDateTime5 = "ClosedDateTime5";
			public const string ClosedTime5ByUserID = "ClosedTime5ByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsIpr = "IsIpr";
			public const string IsOpr = "IsOpr";
			public const string IsEmr = "IsEmr";
			public const string PeriodMonth = "PeriodMonth";
			public const string AddQuota = "AddQuota";
			public const string AddQuotaOnline = "AddQuotaOnline";
			public const string AddQuotaBpjs = "AddQuotaBpjs";
			public const string AddQuotaBpjsOnline = "AddQuotaBpjsOnline";
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
			lock (typeof(ParamedicScheduleDateMetadata))
			{
				if (ParamedicScheduleDateMetadata.mapDelegates == null)
				{
					ParamedicScheduleDateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ParamedicScheduleDateMetadata.meta == null)
				{
					ParamedicScheduleDateMetadata.meta = new ParamedicScheduleDateMetadata();
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

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OperationalTimeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosedTime1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime1", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedTime1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosedTime2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime2", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedTime2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosedTime3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime3", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedTime3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosedTime4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime4", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedTime4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosedTime5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime5", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedTime5ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PeriodMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AddQuota", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AddQuotaOnline", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AddQuotaBpjs", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AddQuotaBpjsOnline", new esTypeMap("smallint", "System.Int16"));


				meta.Source = "ParamedicScheduleDate";
				meta.Destination = "ParamedicScheduleDate";
				meta.spInsert = "proc_ParamedicScheduleDateInsert";
				meta.spUpdate = "proc_ParamedicScheduleDateUpdate";
				meta.spDelete = "proc_ParamedicScheduleDateDelete";
				meta.spLoadAll = "proc_ParamedicScheduleDateLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicScheduleDateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicScheduleDateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
