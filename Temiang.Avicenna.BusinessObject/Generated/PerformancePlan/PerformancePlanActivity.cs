/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/11/2023 7:05:37 PM
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
	abstract public class esPerformancePlanActivityCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanActivityCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanActivityCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanActivityQuery query)
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
			this.InitQuery(query as esPerformancePlanActivityQuery);
		}
		#endregion

		virtual public PerformancePlanActivity DetachEntity(PerformancePlanActivity entity)
		{
			return base.DetachEntity(entity) as PerformancePlanActivity;
		}

		virtual public PerformancePlanActivity AttachEntity(PerformancePlanActivity entity)
		{
			return base.AttachEntity(entity) as PerformancePlanActivity;
		}

		virtual public void Combine(PerformancePlanActivityCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanActivity this[int index]
		{
			get
			{
				return base[index] as PerformancePlanActivity;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanActivity);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanActivity : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanActivityQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanActivity()
		{
		}

		public esPerformancePlanActivity(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 activityID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(activityID);
			else
				return LoadByPrimaryKeyStoredProcedure(activityID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 activityID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(activityID);
			else
				return LoadByPrimaryKeyStoredProcedure(activityID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 activityID)
		{
			esPerformancePlanActivityQuery query = this.GetDynamicQuery();
			query.Where(query.ActivityID == activityID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 activityID)
		{
			esParameters parms = new esParameters();
			parms.Add("ActivityID", activityID);
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
						case "ActivityID": this.str.ActivityID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "ActivityDate": this.str.ActivityDate = (string)value; break;
						case "StartTime": this.str.StartTime = (string)value; break;
						case "EndTime": this.str.EndTime = (string)value; break;
						case "SRActivityCategory": this.str.SRActivityCategory = (string)value; break;
						case "WorkPlanIndicators": this.str.WorkPlanIndicators = (string)value; break;
						case "Activity": this.str.Activity = (string)value; break;
						case "EffectiveTime": this.str.EffectiveTime = (string)value; break;
						case "Volume": this.str.Volume = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "DeletedDateTime": this.str.DeletedDateTime = (string)value; break;
						case "DeletedByUserID": this.str.DeletedByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsRejected": this.str.IsRejected = (string)value; break;
						case "RejectedDateTime": this.str.RejectedDateTime = (string)value; break;
						case "RejectedByUserID": this.str.RejectedByUserID = (string)value; break;
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
						case "ActivityID":

							if (value == null || value is System.Int64)
								this.ActivityID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ActivityDate":

							if (value == null || value is System.DateTime)
								this.ActivityDate = (System.DateTime?)value;
							break;
						case "EffectiveTime":

							if (value == null || value is System.Decimal)
								this.EffectiveTime = (System.Decimal?)value;
							break;
						case "Volume":

							if (value == null || value is System.Decimal)
								this.Volume = (System.Decimal?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "DeletedDateTime":

							if (value == null || value is System.DateTime)
								this.DeletedDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsRejected":

							if (value == null || value is System.Boolean)
								this.IsRejected = (System.Boolean?)value;
							break;
						case "RejectedDateTime":

							if (value == null || value is System.DateTime)
								this.RejectedDateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanActivity.ActivityID
		/// </summary>
		virtual public System.Int64? ActivityID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanActivityMetadata.ColumnNames.ActivityID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanActivityMetadata.ColumnNames.ActivityID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanActivityMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanActivityMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.ActivityDate
		/// </summary>
		virtual public System.DateTime? ActivityDate
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.ActivityDate);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.ActivityDate, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.StartTime
		/// </summary>
		virtual public System.String StartTime
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.StartTime);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.StartTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.EndTime
		/// </summary>
		virtual public System.String EndTime
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.EndTime);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.EndTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.SRActivityCategory
		/// </summary>
		virtual public System.String SRActivityCategory
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.SRActivityCategory);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.SRActivityCategory, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.WorkPlanIndicators
		/// </summary>
		virtual public System.String WorkPlanIndicators
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.WorkPlanIndicators);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.WorkPlanIndicators, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.Activity
		/// </summary>
		virtual public System.String Activity
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.Activity);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.Activity, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.EffectiveTime
		/// </summary>
		virtual public System.Decimal? EffectiveTime
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanActivityMetadata.ColumnNames.EffectiveTime);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanActivityMetadata.ColumnNames.EffectiveTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.Volume
		/// </summary>
		virtual public System.Decimal? Volume
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanActivityMetadata.ColumnNames.Volume);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanActivityMetadata.ColumnNames.Volume, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.DeletedDateTime
		/// </summary>
		virtual public System.DateTime? DeletedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.DeletedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.DeletedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.DeletedByUserID
		/// </summary>
		virtual public System.String DeletedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.DeletedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.DeletedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.IsRejected
		/// </summary>
		virtual public System.Boolean? IsRejected
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsRejected);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanActivityMetadata.ColumnNames.IsRejected, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.RejectedDateTime
		/// </summary>
		virtual public System.DateTime? RejectedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.RejectedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.RejectedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.RejectedByUserID
		/// </summary>
		virtual public System.String RejectedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.RejectedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.RejectedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanActivityMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanActivity.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanActivityMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanActivityMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanActivity entity)
			{
				this.entity = entity;
			}
			public System.String ActivityID
			{
				get
				{
					System.Int64? data = entity.ActivityID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActivityID = null;
					else entity.ActivityID = Convert.ToInt64(value);
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
			public System.String ActivityDate
			{
				get
				{
					System.DateTime? data = entity.ActivityDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActivityDate = null;
					else entity.ActivityDate = Convert.ToDateTime(value);
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
			public System.String SRActivityCategory
			{
				get
				{
					System.String data = entity.SRActivityCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRActivityCategory = null;
					else entity.SRActivityCategory = Convert.ToString(value);
				}
			}
			public System.String WorkPlanIndicators
			{
				get
				{
					System.String data = entity.WorkPlanIndicators;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkPlanIndicators = null;
					else entity.WorkPlanIndicators = Convert.ToString(value);
				}
			}
			public System.String Activity
			{
				get
				{
					System.String data = entity.Activity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Activity = null;
					else entity.Activity = Convert.ToString(value);
				}
			}
			public System.String EffectiveTime
			{
				get
				{
					System.Decimal? data = entity.EffectiveTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveTime = null;
					else entity.EffectiveTime = Convert.ToDecimal(value);
				}
			}
			public System.String Volume
			{
				get
				{
					System.Decimal? data = entity.Volume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Volume = null;
					else entity.Volume = Convert.ToDecimal(value);
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
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
				}
			}
			public System.String DeletedDateTime
			{
				get
				{
					System.DateTime? data = entity.DeletedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeletedDateTime = null;
					else entity.DeletedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String DeletedByUserID
			{
				get
				{
					System.String data = entity.DeletedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeletedByUserID = null;
					else entity.DeletedByUserID = Convert.ToString(value);
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
			public System.String IsRejected
			{
				get
				{
					System.Boolean? data = entity.IsRejected;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRejected = null;
					else entity.IsRejected = Convert.ToBoolean(value);
				}
			}
			public System.String RejectedDateTime
			{
				get
				{
					System.DateTime? data = entity.RejectedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RejectedDateTime = null;
					else entity.RejectedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RejectedByUserID
			{
				get
				{
					System.String data = entity.RejectedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RejectedByUserID = null;
					else entity.RejectedByUserID = Convert.ToString(value);
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
			private esPerformancePlanActivity entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanActivityQuery query)
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
				throw new Exception("esPerformancePlanActivity can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanActivity : esPerformancePlanActivity
	{
	}

	[Serializable]
	abstract public class esPerformancePlanActivityQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanActivityMetadata.Meta();
			}
		}

		public esQueryItem ActivityID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.ActivityID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem ActivityDate
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.ActivityDate, esSystemType.DateTime);
			}
		}

		public esQueryItem StartTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.StartTime, esSystemType.String);
			}
		}

		public esQueryItem EndTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.EndTime, esSystemType.String);
			}
		}

		public esQueryItem SRActivityCategory
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.SRActivityCategory, esSystemType.String);
			}
		}

		public esQueryItem WorkPlanIndicators
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.WorkPlanIndicators, esSystemType.String);
			}
		}

		public esQueryItem Activity
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.Activity, esSystemType.String);
			}
		}

		public esQueryItem EffectiveTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.EffectiveTime, esSystemType.Decimal);
			}
		}

		public esQueryItem Volume
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.Volume, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem DeletedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.DeletedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem DeletedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.DeletedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRejected
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.IsRejected, esSystemType.Boolean);
			}
		}

		public esQueryItem RejectedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.RejectedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RejectedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.RejectedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanActivityMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanActivityCollection")]
	public partial class PerformancePlanActivityCollection : esPerformancePlanActivityCollection, IEnumerable<PerformancePlanActivity>
	{
		public PerformancePlanActivityCollection()
		{

		}

		public static implicit operator List<PerformancePlanActivity>(PerformancePlanActivityCollection coll)
		{
			List<PerformancePlanActivity> list = new List<PerformancePlanActivity>();

			foreach (PerformancePlanActivity emp in coll)
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
				return PerformancePlanActivityMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanActivityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanActivity(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanActivity();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanActivityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanActivityQuery();
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
		public bool Load(PerformancePlanActivityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanActivity AddNew()
		{
			PerformancePlanActivity entity = base.AddNewEntity() as PerformancePlanActivity;

			return entity;
		}
		public PerformancePlanActivity FindByPrimaryKey(Int64 activityID)
		{
			return base.FindByPrimaryKey(activityID) as PerformancePlanActivity;
		}

		#region IEnumerable< PerformancePlanActivity> Members

		IEnumerator<PerformancePlanActivity> IEnumerable<PerformancePlanActivity>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanActivity;
			}
		}

		#endregion

		private PerformancePlanActivityQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanActivity' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanActivity ({ActivityID})")]
	[Serializable]
	public partial class PerformancePlanActivity : esPerformancePlanActivity
	{
		public PerformancePlanActivity()
		{
		}

		public PerformancePlanActivity(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanActivityMetadata.Meta();
			}
		}

		override protected esPerformancePlanActivityQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanActivityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanActivityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanActivityQuery();
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
		public bool Load(PerformancePlanActivityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanActivityQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanActivityQuery : esPerformancePlanActivityQuery
	{
		public PerformancePlanActivityQuery()
		{

		}

		public PerformancePlanActivityQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanActivityQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanActivityMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanActivityMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.ActivityID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.ActivityID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.ActivityDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.ActivityDate;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.StartTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.StartTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.EndTime, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.EndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.SRActivityCategory, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.SRActivityCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.WorkPlanIndicators, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.WorkPlanIndicators;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.Activity, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.Activity;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.EffectiveTime, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.EffectiveTime;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.Volume, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.Volume;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.Notes, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.IsDeleted, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.DeletedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.DeletedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.DeletedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.DeletedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.ApprovedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.IsRejected, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.IsRejected;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.RejectedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.RejectedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.RejectedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.RejectedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.CreatedDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.CreatedByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanActivityMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanActivityMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanActivityMetadata Meta()
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
			public const string ActivityID = "ActivityID";
			public const string PersonID = "PersonID";
			public const string ActivityDate = "ActivityDate";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
			public const string SRActivityCategory = "SRActivityCategory";
			public const string WorkPlanIndicators = "WorkPlanIndicators";
			public const string Activity = "Activity";
			public const string EffectiveTime = "EffectiveTime";
			public const string Volume = "Volume";
			public const string Notes = "Notes";
			public const string IsDeleted = "IsDeleted";
			public const string DeletedDateTime = "DeletedDateTime";
			public const string DeletedByUserID = "DeletedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsRejected = "IsRejected";
			public const string RejectedDateTime = "RejectedDateTime";
			public const string RejectedByUserID = "RejectedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ActivityID = "ActivityID";
			public const string PersonID = "PersonID";
			public const string ActivityDate = "ActivityDate";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
			public const string SRActivityCategory = "SRActivityCategory";
			public const string WorkPlanIndicators = "WorkPlanIndicators";
			public const string Activity = "Activity";
			public const string EffectiveTime = "EffectiveTime";
			public const string Volume = "Volume";
			public const string Notes = "Notes";
			public const string IsDeleted = "IsDeleted";
			public const string DeletedDateTime = "DeletedDateTime";
			public const string DeletedByUserID = "DeletedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsRejected = "IsRejected";
			public const string RejectedDateTime = "RejectedDateTime";
			public const string RejectedByUserID = "RejectedByUserID";
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
			lock (typeof(PerformancePlanActivityMetadata))
			{
				if (PerformancePlanActivityMetadata.mapDelegates == null)
				{
					PerformancePlanActivityMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanActivityMetadata.meta == null)
				{
					PerformancePlanActivityMetadata.meta = new PerformancePlanActivityMetadata();
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

				meta.AddTypeMap("ActivityID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ActivityDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRActivityCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkPlanIndicators", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Activity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectiveTime", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Volume", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeletedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DeletedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRejected", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RejectedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RejectedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanActivity";
				meta.Destination = "PerformancePlanActivity";
				meta.spInsert = "proc_PerformancePlanActivityInsert";
				meta.spUpdate = "proc_PerformancePlanActivityUpdate";
				meta.spDelete = "proc_PerformancePlanActivityDelete";
				meta.spLoadAll = "proc_PerformancePlanActivityLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanActivityLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanActivityMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
