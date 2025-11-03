/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/13/2023 4:27:42 PM
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
	abstract public class esPerformancePlanJptItemCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanJptItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanJptItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptItemQuery query)
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
			this.InitQuery(query as esPerformancePlanJptItemQuery);
		}
		#endregion

		virtual public PerformancePlanJptItem DetachEntity(PerformancePlanJptItem entity)
		{
			return base.DetachEntity(entity) as PerformancePlanJptItem;
		}

		virtual public PerformancePlanJptItem AttachEntity(PerformancePlanJptItem entity)
		{
			return base.AttachEntity(entity) as PerformancePlanJptItem;
		}

		virtual public void Combine(PerformancePlanJptItemCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanJptItem this[int index]
		{
			get
			{
				return base[index] as PerformancePlanJptItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanJptItem);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanJptItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanJptItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanJptItem()
		{
		}

		public esPerformancePlanJptItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 performancePlanItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 performancePlanItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 performancePlanItemID)
		{
			esPerformancePlanJptItemQuery query = this.GetDynamicQuery();
			query.Where(query.PerformancePlanItemID == performancePlanItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 performancePlanItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("PerformancePlanItemID", performancePlanItemID);
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
						case "PerformancePlanItemID": this.str.PerformancePlanItemID = (string)value; break;
						case "PerformancePlanID": this.str.PerformancePlanID = (string)value; break;
						case "SRPerformancePlanIndicator": this.str.SRPerformancePlanIndicator = (string)value; break;
						case "PerformancePlanNo": this.str.PerformancePlanNo = (string)value; break;
						case "SRPerformancePlanDataSource": this.str.SRPerformancePlanDataSource = (string)value; break;
						case "SRSectionPerspective": this.str.SRSectionPerspective = (string)value; break;
						case "Target": this.str.Target = (string)value; break;
						case "PerformanceIndicators": this.str.PerformanceIndicators = (string)value; break;
						case "Measurement": this.str.Measurement = (string)value; break;
						case "UnitTargets": this.str.UnitTargets = (string)value; break;
						case "SRAchievementFormula": this.str.SRAchievementFormula = (string)value; break;
						case "SRRealizationFormula": this.str.SRRealizationFormula = (string)value; break;
						case "Quarter1": this.str.Quarter1 = (string)value; break;
						case "Quarter2": this.str.Quarter2 = (string)value; break;
						case "Quarter3": this.str.Quarter3 = (string)value; break;
						case "Quarter4": this.str.Quarter4 = (string)value; break;
						case "YearTarget": this.str.YearTarget = (string)value; break;
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
						case "PerformancePlanItemID":

							if (value == null || value is System.Int32)
								this.PerformancePlanItemID = (System.Int32?)value;
							break;
						case "PerformancePlanID":

							if (value == null || value is System.Int32)
								this.PerformancePlanID = (System.Int32?)value;
							break;
						case "Quarter1":

							if (value == null || value is System.Decimal)
								this.Quarter1 = (System.Decimal?)value;
							break;
						case "Quarter2":

							if (value == null || value is System.Decimal)
								this.Quarter2 = (System.Decimal?)value;
							break;
						case "Quarter3":

							if (value == null || value is System.Decimal)
								this.Quarter3 = (System.Decimal?)value;
							break;
						case "Quarter4":

							if (value == null || value is System.Decimal)
								this.Quarter4 = (System.Decimal?)value;
							break;
						case "YearTarget":

							if (value == null || value is System.Decimal)
								this.YearTarget = (System.Decimal?)value;
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
		/// Maps to PerformancePlanJptItem.PerformancePlanItemID
		/// </summary>
		virtual public System.Int32? PerformancePlanItemID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanItemID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanItemID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.SRPerformancePlanIndicator
		/// </summary>
		virtual public System.String SRPerformancePlanIndicator
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanIndicator);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanIndicator, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.PerformancePlanNo
		/// </summary>
		virtual public System.String PerformancePlanNo
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanNo);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanNo, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.SRPerformancePlanDataSource
		/// </summary>
		virtual public System.String SRPerformancePlanDataSource
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanDataSource);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanDataSource, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.SRSectionPerspective
		/// </summary>
		virtual public System.String SRSectionPerspective
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRSectionPerspective);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRSectionPerspective, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Target
		/// </summary>
		virtual public System.String Target
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.Target);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.Target, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.PerformanceIndicators
		/// </summary>
		virtual public System.String PerformanceIndicators
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.PerformanceIndicators);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.PerformanceIndicators, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Measurement
		/// </summary>
		virtual public System.String Measurement
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.Measurement);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.Measurement, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.UnitTargets
		/// </summary>
		virtual public System.String UnitTargets
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.UnitTargets);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.UnitTargets, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.SRAchievementFormula
		/// </summary>
		virtual public System.String SRAchievementFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRAchievementFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRAchievementFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.SRRealizationFormula
		/// </summary>
		virtual public System.String SRRealizationFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRRealizationFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.SRRealizationFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Quarter1
		/// </summary>
		virtual public System.Decimal? Quarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Quarter2
		/// </summary>
		virtual public System.Decimal? Quarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Quarter3
		/// </summary>
		virtual public System.Decimal? Quarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.Quarter4
		/// </summary>
		virtual public System.Decimal? Quarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.Quarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.YearTarget
		/// </summary>
		virtual public System.Decimal? YearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.YearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptItemMetadata.ColumnNames.YearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanJptItem entity)
			{
				this.entity = entity;
			}
			public System.String PerformancePlanItemID
			{
				get
				{
					System.Int32? data = entity.PerformancePlanItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanItemID = null;
					else entity.PerformancePlanItemID = Convert.ToInt32(value);
				}
			}
			public System.String PerformancePlanID
			{
				get
				{
					System.Int32? data = entity.PerformancePlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanID = null;
					else entity.PerformancePlanID = Convert.ToInt32(value);
				}
			}
			public System.String SRPerformancePlanIndicator
			{
				get
				{
					System.String data = entity.SRPerformancePlanIndicator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPerformancePlanIndicator = null;
					else entity.SRPerformancePlanIndicator = Convert.ToString(value);
				}
			}
			public System.String PerformancePlanNo
			{
				get
				{
					System.String data = entity.PerformancePlanNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanNo = null;
					else entity.PerformancePlanNo = Convert.ToString(value);
				}
			}
			public System.String SRPerformancePlanDataSource
			{
				get
				{
					System.String data = entity.SRPerformancePlanDataSource;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPerformancePlanDataSource = null;
					else entity.SRPerformancePlanDataSource = Convert.ToString(value);
				}
			}
			public System.String SRSectionPerspective
			{
				get
				{
					System.String data = entity.SRSectionPerspective;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSectionPerspective = null;
					else entity.SRSectionPerspective = Convert.ToString(value);
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
			public System.String PerformanceIndicators
			{
				get
				{
					System.String data = entity.PerformanceIndicators;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformanceIndicators = null;
					else entity.PerformanceIndicators = Convert.ToString(value);
				}
			}
			public System.String Measurement
			{
				get
				{
					System.String data = entity.Measurement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Measurement = null;
					else entity.Measurement = Convert.ToString(value);
				}
			}
			public System.String UnitTargets
			{
				get
				{
					System.String data = entity.UnitTargets;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnitTargets = null;
					else entity.UnitTargets = Convert.ToString(value);
				}
			}
			public System.String SRAchievementFormula
			{
				get
				{
					System.String data = entity.SRAchievementFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAchievementFormula = null;
					else entity.SRAchievementFormula = Convert.ToString(value);
				}
			}
			public System.String SRRealizationFormula
			{
				get
				{
					System.String data = entity.SRRealizationFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRealizationFormula = null;
					else entity.SRRealizationFormula = Convert.ToString(value);
				}
			}
			public System.String Quarter1
			{
				get
				{
					System.Decimal? data = entity.Quarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quarter1 = null;
					else entity.Quarter1 = Convert.ToDecimal(value);
				}
			}
			public System.String Quarter2
			{
				get
				{
					System.Decimal? data = entity.Quarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quarter2 = null;
					else entity.Quarter2 = Convert.ToDecimal(value);
				}
			}
			public System.String Quarter3
			{
				get
				{
					System.Decimal? data = entity.Quarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quarter3 = null;
					else entity.Quarter3 = Convert.ToDecimal(value);
				}
			}
			public System.String Quarter4
			{
				get
				{
					System.Decimal? data = entity.Quarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quarter4 = null;
					else entity.Quarter4 = Convert.ToDecimal(value);
				}
			}
			public System.String YearTarget
			{
				get
				{
					System.Decimal? data = entity.YearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearTarget = null;
					else entity.YearTarget = Convert.ToDecimal(value);
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
			private esPerformancePlanJptItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptItemQuery query)
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
				throw new Exception("esPerformancePlanJptItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanJptItem : esPerformancePlanJptItem
	{
	}

	[Serializable]
	abstract public class esPerformancePlanJptItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptItemMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanItemID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanItemID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem SRPerformancePlanIndicator
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanIndicator, esSystemType.String);
			}
		}

		public esQueryItem PerformancePlanNo
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanNo, esSystemType.String);
			}
		}

		public esQueryItem SRPerformancePlanDataSource
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanDataSource, esSystemType.String);
			}
		}

		public esQueryItem SRSectionPerspective
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.SRSectionPerspective, esSystemType.String);
			}
		}

		public esQueryItem Target
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Target, esSystemType.String);
			}
		}

		public esQueryItem PerformanceIndicators
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.PerformanceIndicators, esSystemType.String);
			}
		}

		public esQueryItem Measurement
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Measurement, esSystemType.String);
			}
		}

		public esQueryItem UnitTargets
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.UnitTargets, esSystemType.String);
			}
		}

		public esQueryItem SRAchievementFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.SRAchievementFormula, esSystemType.String);
			}
		}

		public esQueryItem SRRealizationFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.SRRealizationFormula, esSystemType.String);
			}
		}

		public esQueryItem Quarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Quarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Quarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Quarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.Quarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem YearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.YearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanJptItemCollection")]
	public partial class PerformancePlanJptItemCollection : esPerformancePlanJptItemCollection, IEnumerable<PerformancePlanJptItem>
	{
		public PerformancePlanJptItemCollection()
		{

		}

		public static implicit operator List<PerformancePlanJptItem>(PerformancePlanJptItemCollection coll)
		{
			List<PerformancePlanJptItem> list = new List<PerformancePlanJptItem>();

			foreach (PerformancePlanJptItem emp in coll)
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
				return PerformancePlanJptItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanJptItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanJptItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptItemQuery();
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
		public bool Load(PerformancePlanJptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanJptItem AddNew()
		{
			PerformancePlanJptItem entity = base.AddNewEntity() as PerformancePlanJptItem;

			return entity;
		}
		public PerformancePlanJptItem FindByPrimaryKey(Int32 performancePlanItemID)
		{
			return base.FindByPrimaryKey(performancePlanItemID) as PerformancePlanJptItem;
		}

		#region IEnumerable< PerformancePlanJptItem> Members

		IEnumerator<PerformancePlanJptItem> IEnumerable<PerformancePlanJptItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanJptItem;
			}
		}

		#endregion

		private PerformancePlanJptItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanJptItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanJptItem ({PerformancePlanItemID})")]
	[Serializable]
	public partial class PerformancePlanJptItem : esPerformancePlanJptItem
	{
		public PerformancePlanJptItem()
		{
		}

		public PerformancePlanJptItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptItemMetadata.Meta();
			}
		}

		override protected esPerformancePlanJptItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptItemQuery();
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
		public bool Load(PerformancePlanJptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanJptItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanJptItemQuery : esPerformancePlanJptItemQuery
	{
		public PerformancePlanJptItemQuery()
		{

		}

		public PerformancePlanJptItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanJptItemQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanJptItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanJptItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.PerformancePlanItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.PerformancePlanID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanIndicator, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.SRPerformancePlanIndicator;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.PerformancePlanNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.PerformancePlanNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.SRPerformancePlanDataSource, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.SRPerformancePlanDataSource;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.SRSectionPerspective, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.SRSectionPerspective;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Target, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Target;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.PerformanceIndicators, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.PerformanceIndicators;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Measurement, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Measurement;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.UnitTargets, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.UnitTargets;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.SRAchievementFormula, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.SRAchievementFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.SRRealizationFormula, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.SRRealizationFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Quarter1, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Quarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Quarter2, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Quarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Quarter3, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Quarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.Quarter4, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.Quarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.YearTarget, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.YearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.CreatedDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.CreatedByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptItemMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanJptItemMetadata Meta()
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
			public const string PerformancePlanItemID = "PerformancePlanItemID";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string SRPerformancePlanIndicator = "SRPerformancePlanIndicator";
			public const string PerformancePlanNo = "PerformancePlanNo";
			public const string SRPerformancePlanDataSource = "SRPerformancePlanDataSource";
			public const string SRSectionPerspective = "SRSectionPerspective";
			public const string Target = "Target";
			public const string PerformanceIndicators = "PerformanceIndicators";
			public const string Measurement = "Measurement";
			public const string UnitTargets = "UnitTargets";
			public const string SRAchievementFormula = "SRAchievementFormula";
			public const string SRRealizationFormula = "SRRealizationFormula";
			public const string Quarter1 = "Quarter1";
			public const string Quarter2 = "Quarter2";
			public const string Quarter3 = "Quarter3";
			public const string Quarter4 = "Quarter4";
			public const string YearTarget = "YearTarget";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PerformancePlanItemID = "PerformancePlanItemID";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string SRPerformancePlanIndicator = "SRPerformancePlanIndicator";
			public const string PerformancePlanNo = "PerformancePlanNo";
			public const string SRPerformancePlanDataSource = "SRPerformancePlanDataSource";
			public const string SRSectionPerspective = "SRSectionPerspective";
			public const string Target = "Target";
			public const string PerformanceIndicators = "PerformanceIndicators";
			public const string Measurement = "Measurement";
			public const string UnitTargets = "UnitTargets";
			public const string SRAchievementFormula = "SRAchievementFormula";
			public const string SRRealizationFormula = "SRRealizationFormula";
			public const string Quarter1 = "Quarter1";
			public const string Quarter2 = "Quarter2";
			public const string Quarter3 = "Quarter3";
			public const string Quarter4 = "Quarter4";
			public const string YearTarget = "YearTarget";
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
			lock (typeof(PerformancePlanJptItemMetadata))
			{
				if (PerformancePlanJptItemMetadata.mapDelegates == null)
				{
					PerformancePlanJptItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanJptItemMetadata.meta == null)
				{
					PerformancePlanJptItemMetadata.meta = new PerformancePlanJptItemMetadata();
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

				meta.AddTypeMap("PerformancePlanItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PerformancePlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPerformancePlanIndicator", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PerformancePlanNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPerformancePlanDataSource", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSectionPerspective", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Target", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PerformanceIndicators", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Measurement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UnitTargets", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAchievementFormula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRealizationFormula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Quarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Quarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Quarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("YearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanJptItem";
				meta.Destination = "PerformancePlanJptItem";
				meta.spInsert = "proc_PerformancePlanJptItemInsert";
				meta.spUpdate = "proc_PerformancePlanJptItemUpdate";
				meta.spDelete = "proc_PerformancePlanJptItemDelete";
				meta.spLoadAll = "proc_PerformancePlanJptItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanJptItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanJptItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
