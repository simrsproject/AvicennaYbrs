/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/14/2023 11:25:38 AM
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
	abstract public class esPerformancePlanJptScheduleCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanJptScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanJptScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptScheduleQuery query)
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
			this.InitQuery(query as esPerformancePlanJptScheduleQuery);
		}
		#endregion

		virtual public PerformancePlanJptSchedule DetachEntity(PerformancePlanJptSchedule entity)
		{
			return base.DetachEntity(entity) as PerformancePlanJptSchedule;
		}

		virtual public PerformancePlanJptSchedule AttachEntity(PerformancePlanJptSchedule entity)
		{
			return base.AttachEntity(entity) as PerformancePlanJptSchedule;
		}

		virtual public void Combine(PerformancePlanJptScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanJptSchedule this[int index]
		{
			get
			{
				return base[index] as PerformancePlanJptSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanJptSchedule);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanJptSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanJptScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanJptSchedule()
		{
		}

		public esPerformancePlanJptSchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String yearPeriod)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearPeriod);
			else
				return LoadByPrimaryKeyStoredProcedure(yearPeriod);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String yearPeriod)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearPeriod);
			else
				return LoadByPrimaryKeyStoredProcedure(yearPeriod);
		}

		private bool LoadByPrimaryKeyDynamic(String yearPeriod)
		{
			esPerformancePlanJptScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.YearPeriod == yearPeriod);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String yearPeriod)
		{
			esParameters parms = new esParameters();
			parms.Add("YearPeriod", yearPeriod);
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
						case "YearPeriod": this.str.YearPeriod = (string)value; break;
						case "IsOpenInput": this.str.IsOpenInput = (string)value; break;
						case "OpenInputDateTime": this.str.OpenInputDateTime = (string)value; break;
						case "OpenInputByUserID": this.str.OpenInputByUserID = (string)value; break;
						case "IsOpenRealizationQuarter1": this.str.IsOpenRealizationQuarter1 = (string)value; break;
						case "OpenRealizationQuarter1DateTime": this.str.OpenRealizationQuarter1DateTime = (string)value; break;
						case "OpenRealizationQuarter1ByUserID": this.str.OpenRealizationQuarter1ByUserID = (string)value; break;
						case "IsOpenRealizationQuarter2": this.str.IsOpenRealizationQuarter2 = (string)value; break;
						case "OpenRealizationQuarter2DateTime": this.str.OpenRealizationQuarter2DateTime = (string)value; break;
						case "OpenRealizationQuarter2ByUserID": this.str.OpenRealizationQuarter2ByUserID = (string)value; break;
						case "IsOpenRealizationQuarter3": this.str.IsOpenRealizationQuarter3 = (string)value; break;
						case "OpenRealizationQuarter3DateTime": this.str.OpenRealizationQuarter3DateTime = (string)value; break;
						case "OpenRealizationQuarter3ByUserID": this.str.OpenRealizationQuarter3ByUserID = (string)value; break;
						case "IsOpenRealizationQuarter4": this.str.IsOpenRealizationQuarter4 = (string)value; break;
						case "OpenRealizationQuarter4DateTime": this.str.OpenRealizationQuarter4DateTime = (string)value; break;
						case "OpenRealizationQuarter4ByUserID": this.str.OpenRealizationQuarter4ByUserID = (string)value; break;
						case "IsOpenVerificationQuarter1": this.str.IsOpenVerificationQuarter1 = (string)value; break;
						case "OpenVerificationQuarter1DateTime": this.str.OpenVerificationQuarter1DateTime = (string)value; break;
						case "OpenVerificationQuarter1ByUserID": this.str.OpenVerificationQuarter1ByUserID = (string)value; break;
						case "IsOpenVerificationQuarter2": this.str.IsOpenVerificationQuarter2 = (string)value; break;
						case "OpenVerificationQuarter2DateTime": this.str.OpenVerificationQuarter2DateTime = (string)value; break;
						case "OpenVerificationQuarter2ByUserID": this.str.OpenVerificationQuarter2ByUserID = (string)value; break;
						case "IsOpenVerificationQuarter3": this.str.IsOpenVerificationQuarter3 = (string)value; break;
						case "OpenVerificationQuarter3DateTime": this.str.OpenVerificationQuarter3DateTime = (string)value; break;
						case "OpenVerificationQuarter3ByUserID": this.str.OpenVerificationQuarter3ByUserID = (string)value; break;
						case "IsOpenVerificationQuarter4": this.str.IsOpenVerificationQuarter4 = (string)value; break;
						case "OpenVerificationQuarter4DateTime": this.str.OpenVerificationQuarter4DateTime = (string)value; break;
						case "OpenVerificationQuarter4ByUserID": this.str.OpenVerificationQuarter4ByUserID = (string)value; break;
						case "IsOpenValidationQuarter1": this.str.IsOpenValidationQuarter1 = (string)value; break;
						case "OpenValidationQuarter1DateTime": this.str.OpenValidationQuarter1DateTime = (string)value; break;
						case "OpenValidationQuarter1ByUserID": this.str.OpenValidationQuarter1ByUserID = (string)value; break;
						case "IsOpenValidationQuarter2": this.str.IsOpenValidationQuarter2 = (string)value; break;
						case "OpenValidationQuarter2DateTime": this.str.OpenValidationQuarter2DateTime = (string)value; break;
						case "OpenValidationQuarter2ByUserID": this.str.OpenValidationQuarter2ByUserID = (string)value; break;
						case "IsOpenValidationQuarter3": this.str.IsOpenValidationQuarter3 = (string)value; break;
						case "OpenValidationQuarter3DateTime": this.str.OpenValidationQuarter3DateTime = (string)value; break;
						case "OpenValidationQuarter3ByUserID": this.str.OpenValidationQuarter3ByUserID = (string)value; break;
						case "IsOpenValidationQuarter4": this.str.IsOpenValidationQuarter4 = (string)value; break;
						case "OpenValidationQuarter4DateTime": this.str.OpenValidationQuarter4DateTime = (string)value; break;
						case "OpenValidationQuarter4ByUserID": this.str.OpenValidationQuarter4ByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsOpenInput":

							if (value == null || value is System.Boolean)
								this.IsOpenInput = (System.Boolean?)value;
							break;
						case "OpenInputDateTime":

							if (value == null || value is System.DateTime)
								this.OpenInputDateTime = (System.DateTime?)value;
							break;
						case "IsOpenRealizationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsOpenRealizationQuarter1 = (System.Boolean?)value;
							break;
						case "OpenRealizationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.OpenRealizationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsOpenRealizationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsOpenRealizationQuarter2 = (System.Boolean?)value;
							break;
						case "OpenRealizationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.OpenRealizationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "IsOpenRealizationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsOpenRealizationQuarter3 = (System.Boolean?)value;
							break;
						case "OpenRealizationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.OpenRealizationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsOpenRealizationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsOpenRealizationQuarter4 = (System.Boolean?)value;
							break;
						case "OpenRealizationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.OpenRealizationQuarter4DateTime = (System.DateTime?)value;
							break;
						case "IsOpenVerificationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsOpenVerificationQuarter1 = (System.Boolean?)value;
							break;
						case "OpenVerificationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.OpenVerificationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsOpenVerificationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsOpenVerificationQuarter2 = (System.Boolean?)value;
							break;
						case "OpenVerificationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.OpenVerificationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "IsOpenVerificationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsOpenVerificationQuarter3 = (System.Boolean?)value;
							break;
						case "OpenVerificationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.OpenVerificationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsOpenVerificationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsOpenVerificationQuarter4 = (System.Boolean?)value;
							break;
						case "OpenVerificationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.OpenVerificationQuarter4DateTime = (System.DateTime?)value;
							break;
						case "IsOpenValidationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsOpenValidationQuarter1 = (System.Boolean?)value;
							break;
						case "OpenValidationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.OpenValidationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsOpenValidationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsOpenValidationQuarter2 = (System.Boolean?)value;
							break;
						case "OpenValidationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.OpenValidationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "IsOpenValidationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsOpenValidationQuarter3 = (System.Boolean?)value;
							break;
						case "OpenValidationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.OpenValidationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsOpenValidationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsOpenValidationQuarter4 = (System.Boolean?)value;
							break;
						case "OpenValidationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.OpenValidationQuarter4DateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanJptSchedule.YearPeriod
		/// </summary>
		virtual public System.String YearPeriod
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.YearPeriod);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.YearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenInput
		/// </summary>
		virtual public System.Boolean? IsOpenInput
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenInput);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenInput, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenInputDateTime
		/// </summary>
		virtual public System.DateTime? OpenInputDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenInputByUserID
		/// </summary>
		virtual public System.String OpenInputByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenRealizationQuarter1
		/// </summary>
		virtual public System.Boolean? IsOpenRealizationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? OpenRealizationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter1ByUserID
		/// </summary>
		virtual public System.String OpenRealizationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenRealizationQuarter2
		/// </summary>
		virtual public System.Boolean? IsOpenRealizationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? OpenRealizationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter2ByUserID
		/// </summary>
		virtual public System.String OpenRealizationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenRealizationQuarter3
		/// </summary>
		virtual public System.Boolean? IsOpenRealizationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? OpenRealizationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter3ByUserID
		/// </summary>
		virtual public System.String OpenRealizationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenRealizationQuarter4
		/// </summary>
		virtual public System.Boolean? IsOpenRealizationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? OpenRealizationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenRealizationQuarter4ByUserID
		/// </summary>
		virtual public System.String OpenRealizationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenVerificationQuarter1
		/// </summary>
		virtual public System.Boolean? IsOpenVerificationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? OpenVerificationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter1ByUserID
		/// </summary>
		virtual public System.String OpenVerificationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenVerificationQuarter2
		/// </summary>
		virtual public System.Boolean? IsOpenVerificationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? OpenVerificationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter2ByUserID
		/// </summary>
		virtual public System.String OpenVerificationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenVerificationQuarter3
		/// </summary>
		virtual public System.Boolean? IsOpenVerificationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? OpenVerificationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter3ByUserID
		/// </summary>
		virtual public System.String OpenVerificationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenVerificationQuarter4
		/// </summary>
		virtual public System.Boolean? IsOpenVerificationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? OpenVerificationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenVerificationQuarter4ByUserID
		/// </summary>
		virtual public System.String OpenVerificationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenValidationQuarter1
		/// </summary>
		virtual public System.Boolean? IsOpenValidationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? OpenValidationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter1ByUserID
		/// </summary>
		virtual public System.String OpenValidationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenValidationQuarter2
		/// </summary>
		virtual public System.Boolean? IsOpenValidationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? OpenValidationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter2ByUserID
		/// </summary>
		virtual public System.String OpenValidationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenValidationQuarter3
		/// </summary>
		virtual public System.Boolean? IsOpenValidationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? OpenValidationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter3ByUserID
		/// </summary>
		virtual public System.String OpenValidationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.IsOpenValidationQuarter4
		/// </summary>
		virtual public System.Boolean? IsOpenValidationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? OpenValidationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.OpenValidationQuarter4ByUserID
		/// </summary>
		virtual public System.String OpenValidationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanJptSchedule entity)
			{
				this.entity = entity;
			}
			public System.String YearPeriod
			{
				get
				{
					System.String data = entity.YearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriod = null;
					else entity.YearPeriod = Convert.ToString(value);
				}
			}
			public System.String IsOpenInput
			{
				get
				{
					System.Boolean? data = entity.IsOpenInput;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenInput = null;
					else entity.IsOpenInput = Convert.ToBoolean(value);
				}
			}
			public System.String OpenInputDateTime
			{
				get
				{
					System.DateTime? data = entity.OpenInputDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenInputDateTime = null;
					else entity.OpenInputDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenInputByUserID
			{
				get
				{
					System.String data = entity.OpenInputByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenInputByUserID = null;
					else entity.OpenInputByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenRealizationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsOpenRealizationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenRealizationQuarter1 = null;
					else entity.IsOpenRealizationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenRealizationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenRealizationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter1DateTime = null;
					else entity.OpenRealizationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenRealizationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.OpenRealizationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter1ByUserID = null;
					else entity.OpenRealizationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenRealizationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsOpenRealizationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenRealizationQuarter2 = null;
					else entity.IsOpenRealizationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenRealizationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenRealizationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter2DateTime = null;
					else entity.OpenRealizationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenRealizationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.OpenRealizationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter2ByUserID = null;
					else entity.OpenRealizationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenRealizationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsOpenRealizationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenRealizationQuarter3 = null;
					else entity.IsOpenRealizationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenRealizationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenRealizationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter3DateTime = null;
					else entity.OpenRealizationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenRealizationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.OpenRealizationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter3ByUserID = null;
					else entity.OpenRealizationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenRealizationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsOpenRealizationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenRealizationQuarter4 = null;
					else entity.IsOpenRealizationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenRealizationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenRealizationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter4DateTime = null;
					else entity.OpenRealizationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenRealizationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.OpenRealizationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenRealizationQuarter4ByUserID = null;
					else entity.OpenRealizationQuarter4ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenVerificationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsOpenVerificationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenVerificationQuarter1 = null;
					else entity.IsOpenVerificationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenVerificationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenVerificationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter1DateTime = null;
					else entity.OpenVerificationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenVerificationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.OpenVerificationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter1ByUserID = null;
					else entity.OpenVerificationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenVerificationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsOpenVerificationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenVerificationQuarter2 = null;
					else entity.IsOpenVerificationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenVerificationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenVerificationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter2DateTime = null;
					else entity.OpenVerificationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenVerificationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.OpenVerificationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter2ByUserID = null;
					else entity.OpenVerificationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenVerificationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsOpenVerificationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenVerificationQuarter3 = null;
					else entity.IsOpenVerificationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenVerificationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenVerificationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter3DateTime = null;
					else entity.OpenVerificationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenVerificationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.OpenVerificationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter3ByUserID = null;
					else entity.OpenVerificationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenVerificationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsOpenVerificationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenVerificationQuarter4 = null;
					else entity.IsOpenVerificationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenVerificationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenVerificationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter4DateTime = null;
					else entity.OpenVerificationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenVerificationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.OpenVerificationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenVerificationQuarter4ByUserID = null;
					else entity.OpenVerificationQuarter4ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenValidationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsOpenValidationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenValidationQuarter1 = null;
					else entity.IsOpenValidationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenValidationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenValidationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter1DateTime = null;
					else entity.OpenValidationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenValidationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.OpenValidationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter1ByUserID = null;
					else entity.OpenValidationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenValidationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsOpenValidationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenValidationQuarter2 = null;
					else entity.IsOpenValidationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenValidationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenValidationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter2DateTime = null;
					else entity.OpenValidationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenValidationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.OpenValidationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter2ByUserID = null;
					else entity.OpenValidationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenValidationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsOpenValidationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenValidationQuarter3 = null;
					else entity.IsOpenValidationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenValidationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenValidationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter3DateTime = null;
					else entity.OpenValidationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenValidationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.OpenValidationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter3ByUserID = null;
					else entity.OpenValidationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpenValidationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsOpenValidationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpenValidationQuarter4 = null;
					else entity.IsOpenValidationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String OpenValidationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.OpenValidationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter4DateTime = null;
					else entity.OpenValidationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String OpenValidationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.OpenValidationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpenValidationQuarter4ByUserID = null;
					else entity.OpenValidationQuarter4ByUserID = Convert.ToString(value);
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
			private esPerformancePlanJptSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptScheduleQuery query)
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
				throw new Exception("esPerformancePlanJptSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanJptSchedule : esPerformancePlanJptSchedule
	{
	}

	[Serializable]
	abstract public class esPerformancePlanJptScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptScheduleMetadata.Meta();
			}
		}

		public esQueryItem YearPeriod
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.YearPeriod, esSystemType.String);
			}
		}

		public esQueryItem IsOpenInput
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenInput, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenInputDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenInputByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenRealizationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenRealizationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenRealizationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenRealizationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenRealizationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenRealizationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenRealizationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenRealizationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenRealizationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenRealizationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenRealizationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenRealizationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenVerificationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenVerificationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenVerificationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenVerificationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenVerificationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenVerificationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenVerificationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenVerificationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenVerificationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenVerificationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenVerificationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenVerificationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenValidationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenValidationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenValidationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenValidationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenValidationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenValidationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenValidationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenValidationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenValidationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpenValidationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem OpenValidationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem OpenValidationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanJptScheduleCollection")]
	public partial class PerformancePlanJptScheduleCollection : esPerformancePlanJptScheduleCollection, IEnumerable<PerformancePlanJptSchedule>
	{
		public PerformancePlanJptScheduleCollection()
		{

		}

		public static implicit operator List<PerformancePlanJptSchedule>(PerformancePlanJptScheduleCollection coll)
		{
			List<PerformancePlanJptSchedule> list = new List<PerformancePlanJptSchedule>();

			foreach (PerformancePlanJptSchedule emp in coll)
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
				return PerformancePlanJptScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanJptSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanJptSchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptScheduleQuery();
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
		public bool Load(PerformancePlanJptScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanJptSchedule AddNew()
		{
			PerformancePlanJptSchedule entity = base.AddNewEntity() as PerformancePlanJptSchedule;

			return entity;
		}
		public PerformancePlanJptSchedule FindByPrimaryKey(String yearPeriod)
		{
			return base.FindByPrimaryKey(yearPeriod) as PerformancePlanJptSchedule;
		}

		#region IEnumerable< PerformancePlanJptSchedule> Members

		IEnumerator<PerformancePlanJptSchedule> IEnumerable<PerformancePlanJptSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanJptSchedule;
			}
		}

		#endregion

		private PerformancePlanJptScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanJptSchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanJptSchedule ({YearPeriod})")]
	[Serializable]
	public partial class PerformancePlanJptSchedule : esPerformancePlanJptSchedule
	{
		public PerformancePlanJptSchedule()
		{
		}

		public PerformancePlanJptSchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptScheduleMetadata.Meta();
			}
		}

		override protected esPerformancePlanJptScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptScheduleQuery();
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
		public bool Load(PerformancePlanJptScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanJptScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanJptScheduleQuery : esPerformancePlanJptScheduleQuery
	{
		public PerformancePlanJptScheduleQuery()
		{

		}

		public PerformancePlanJptScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanJptScheduleQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanJptScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanJptScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.YearPeriod, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.YearPeriod;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenInput, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenInput;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenInputDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenInputByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenInputByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter1, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenRealizationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1DateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter1ByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter2, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenRealizationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2DateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter2ByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter3, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenRealizationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3DateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter3ByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenRealizationQuarter4, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenRealizationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4DateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenRealizationQuarter4ByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenRealizationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter1, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenVerificationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1DateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter1ByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter2, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenVerificationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2DateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter2ByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter3, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenVerificationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3DateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter3ByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenVerificationQuarter4, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenVerificationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4DateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenVerificationQuarter4ByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenVerificationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter1, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenValidationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1DateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter1ByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter2, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenValidationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2DateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter2ByUserID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter3, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenValidationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3DateTime, 35, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter3ByUserID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.IsOpenValidationQuarter4, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.IsOpenValidationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4DateTime, 38, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.OpenValidationQuarter4ByUserID, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.OpenValidationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedDateTime, 40, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.CreatedByUserID, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateDateTime, 42, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptScheduleMetadata.ColumnNames.LastUpdateByUserID, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanJptScheduleMetadata Meta()
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
			public const string YearPeriod = "YearPeriod";
			public const string IsOpenInput = "IsOpenInput";
			public const string OpenInputDateTime = "OpenInputDateTime";
			public const string OpenInputByUserID = "OpenInputByUserID";
			public const string IsOpenRealizationQuarter1 = "IsOpenRealizationQuarter1";
			public const string OpenRealizationQuarter1DateTime = "OpenRealizationQuarter1DateTime";
			public const string OpenRealizationQuarter1ByUserID = "OpenRealizationQuarter1ByUserID";
			public const string IsOpenRealizationQuarter2 = "IsOpenRealizationQuarter2";
			public const string OpenRealizationQuarter2DateTime = "OpenRealizationQuarter2DateTime";
			public const string OpenRealizationQuarter2ByUserID = "OpenRealizationQuarter2ByUserID";
			public const string IsOpenRealizationQuarter3 = "IsOpenRealizationQuarter3";
			public const string OpenRealizationQuarter3DateTime = "OpenRealizationQuarter3DateTime";
			public const string OpenRealizationQuarter3ByUserID = "OpenRealizationQuarter3ByUserID";
			public const string IsOpenRealizationQuarter4 = "IsOpenRealizationQuarter4";
			public const string OpenRealizationQuarter4DateTime = "OpenRealizationQuarter4DateTime";
			public const string OpenRealizationQuarter4ByUserID = "OpenRealizationQuarter4ByUserID";
			public const string IsOpenVerificationQuarter1 = "IsOpenVerificationQuarter1";
			public const string OpenVerificationQuarter1DateTime = "OpenVerificationQuarter1DateTime";
			public const string OpenVerificationQuarter1ByUserID = "OpenVerificationQuarter1ByUserID";
			public const string IsOpenVerificationQuarter2 = "IsOpenVerificationQuarter2";
			public const string OpenVerificationQuarter2DateTime = "OpenVerificationQuarter2DateTime";
			public const string OpenVerificationQuarter2ByUserID = "OpenVerificationQuarter2ByUserID";
			public const string IsOpenVerificationQuarter3 = "IsOpenVerificationQuarter3";
			public const string OpenVerificationQuarter3DateTime = "OpenVerificationQuarter3DateTime";
			public const string OpenVerificationQuarter3ByUserID = "OpenVerificationQuarter3ByUserID";
			public const string IsOpenVerificationQuarter4 = "IsOpenVerificationQuarter4";
			public const string OpenVerificationQuarter4DateTime = "OpenVerificationQuarter4DateTime";
			public const string OpenVerificationQuarter4ByUserID = "OpenVerificationQuarter4ByUserID";
			public const string IsOpenValidationQuarter1 = "IsOpenValidationQuarter1";
			public const string OpenValidationQuarter1DateTime = "OpenValidationQuarter1DateTime";
			public const string OpenValidationQuarter1ByUserID = "OpenValidationQuarter1ByUserID";
			public const string IsOpenValidationQuarter2 = "IsOpenValidationQuarter2";
			public const string OpenValidationQuarter2DateTime = "OpenValidationQuarter2DateTime";
			public const string OpenValidationQuarter2ByUserID = "OpenValidationQuarter2ByUserID";
			public const string IsOpenValidationQuarter3 = "IsOpenValidationQuarter3";
			public const string OpenValidationQuarter3DateTime = "OpenValidationQuarter3DateTime";
			public const string OpenValidationQuarter3ByUserID = "OpenValidationQuarter3ByUserID";
			public const string IsOpenValidationQuarter4 = "IsOpenValidationQuarter4";
			public const string OpenValidationQuarter4DateTime = "OpenValidationQuarter4DateTime";
			public const string OpenValidationQuarter4ByUserID = "OpenValidationQuarter4ByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string YearPeriod = "YearPeriod";
			public const string IsOpenInput = "IsOpenInput";
			public const string OpenInputDateTime = "OpenInputDateTime";
			public const string OpenInputByUserID = "OpenInputByUserID";
			public const string IsOpenRealizationQuarter1 = "IsOpenRealizationQuarter1";
			public const string OpenRealizationQuarter1DateTime = "OpenRealizationQuarter1DateTime";
			public const string OpenRealizationQuarter1ByUserID = "OpenRealizationQuarter1ByUserID";
			public const string IsOpenRealizationQuarter2 = "IsOpenRealizationQuarter2";
			public const string OpenRealizationQuarter2DateTime = "OpenRealizationQuarter2DateTime";
			public const string OpenRealizationQuarter2ByUserID = "OpenRealizationQuarter2ByUserID";
			public const string IsOpenRealizationQuarter3 = "IsOpenRealizationQuarter3";
			public const string OpenRealizationQuarter3DateTime = "OpenRealizationQuarter3DateTime";
			public const string OpenRealizationQuarter3ByUserID = "OpenRealizationQuarter3ByUserID";
			public const string IsOpenRealizationQuarter4 = "IsOpenRealizationQuarter4";
			public const string OpenRealizationQuarter4DateTime = "OpenRealizationQuarter4DateTime";
			public const string OpenRealizationQuarter4ByUserID = "OpenRealizationQuarter4ByUserID";
			public const string IsOpenVerificationQuarter1 = "IsOpenVerificationQuarter1";
			public const string OpenVerificationQuarter1DateTime = "OpenVerificationQuarter1DateTime";
			public const string OpenVerificationQuarter1ByUserID = "OpenVerificationQuarter1ByUserID";
			public const string IsOpenVerificationQuarter2 = "IsOpenVerificationQuarter2";
			public const string OpenVerificationQuarter2DateTime = "OpenVerificationQuarter2DateTime";
			public const string OpenVerificationQuarter2ByUserID = "OpenVerificationQuarter2ByUserID";
			public const string IsOpenVerificationQuarter3 = "IsOpenVerificationQuarter3";
			public const string OpenVerificationQuarter3DateTime = "OpenVerificationQuarter3DateTime";
			public const string OpenVerificationQuarter3ByUserID = "OpenVerificationQuarter3ByUserID";
			public const string IsOpenVerificationQuarter4 = "IsOpenVerificationQuarter4";
			public const string OpenVerificationQuarter4DateTime = "OpenVerificationQuarter4DateTime";
			public const string OpenVerificationQuarter4ByUserID = "OpenVerificationQuarter4ByUserID";
			public const string IsOpenValidationQuarter1 = "IsOpenValidationQuarter1";
			public const string OpenValidationQuarter1DateTime = "OpenValidationQuarter1DateTime";
			public const string OpenValidationQuarter1ByUserID = "OpenValidationQuarter1ByUserID";
			public const string IsOpenValidationQuarter2 = "IsOpenValidationQuarter2";
			public const string OpenValidationQuarter2DateTime = "OpenValidationQuarter2DateTime";
			public const string OpenValidationQuarter2ByUserID = "OpenValidationQuarter2ByUserID";
			public const string IsOpenValidationQuarter3 = "IsOpenValidationQuarter3";
			public const string OpenValidationQuarter3DateTime = "OpenValidationQuarter3DateTime";
			public const string OpenValidationQuarter3ByUserID = "OpenValidationQuarter3ByUserID";
			public const string IsOpenValidationQuarter4 = "IsOpenValidationQuarter4";
			public const string OpenValidationQuarter4DateTime = "OpenValidationQuarter4DateTime";
			public const string OpenValidationQuarter4ByUserID = "OpenValidationQuarter4ByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(PerformancePlanJptScheduleMetadata))
			{
				if (PerformancePlanJptScheduleMetadata.mapDelegates == null)
				{
					PerformancePlanJptScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanJptScheduleMetadata.meta == null)
				{
					PerformancePlanJptScheduleMetadata.meta = new PerformancePlanJptScheduleMetadata();
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

				meta.AddTypeMap("YearPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenInput", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenInputDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenInputByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenRealizationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenRealizationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenRealizationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenRealizationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenRealizationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenRealizationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenRealizationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenRealizationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenRealizationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenRealizationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenRealizationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenRealizationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenVerificationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenVerificationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenVerificationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenVerificationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenVerificationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenVerificationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenVerificationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenVerificationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenVerificationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenVerificationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenVerificationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenVerificationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenValidationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenValidationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenValidationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenValidationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenValidationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenValidationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenValidationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenValidationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenValidationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpenValidationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OpenValidationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OpenValidationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanJptSchedule";
				meta.Destination = "PerformancePlanJptSchedule";
				meta.spInsert = "proc_PerformancePlanJptScheduleInsert";
				meta.spUpdate = "proc_PerformancePlanJptScheduleUpdate";
				meta.spDelete = "proc_PerformancePlanJptScheduleDelete";
				meta.spLoadAll = "proc_PerformancePlanJptScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanJptScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanJptScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
