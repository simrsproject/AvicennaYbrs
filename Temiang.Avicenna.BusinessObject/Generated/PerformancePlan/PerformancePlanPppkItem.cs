/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 1:13:35 PM
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
	abstract public class esPerformancePlanPppkItemCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanPppkItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanPppkItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanPppkItemQuery query)
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
			this.InitQuery(query as esPerformancePlanPppkItemQuery);
		}
		#endregion

		virtual public PerformancePlanPppkItem DetachEntity(PerformancePlanPppkItem entity)
		{
			return base.DetachEntity(entity) as PerformancePlanPppkItem;
		}

		virtual public PerformancePlanPppkItem AttachEntity(PerformancePlanPppkItem entity)
		{
			return base.AttachEntity(entity) as PerformancePlanPppkItem;
		}

		virtual public void Combine(PerformancePlanPppkItemCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanPppkItem this[int index]
		{
			get
			{
				return base[index] as PerformancePlanPppkItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanPppkItem);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanPppkItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanPppkItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanPppkItem()
		{
		}

		public esPerformancePlanPppkItem(DataRow row)
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
			esPerformancePlanPppkItemQuery query = this.GetDynamicQuery();
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
						case "PerformancePlanNo": this.str.PerformancePlanNo = (string)value; break;
						case "Activity": this.str.Activity = (string)value; break;
						case "UnitTargets": this.str.UnitTargets = (string)value; break;
						case "Time": this.str.Time = (string)value; break;
						case "SRAchievementFormula": this.str.SRAchievementFormula = (string)value; break;
						case "SRRealizationFormula": this.str.SRRealizationFormula = (string)value; break;
						case "Month01": this.str.Month01 = (string)value; break;
						case "Month02": this.str.Month02 = (string)value; break;
						case "Month03": this.str.Month03 = (string)value; break;
						case "Month04": this.str.Month04 = (string)value; break;
						case "Month05": this.str.Month05 = (string)value; break;
						case "Month06": this.str.Month06 = (string)value; break;
						case "Month07": this.str.Month07 = (string)value; break;
						case "Month08": this.str.Month08 = (string)value; break;
						case "Month09": this.str.Month09 = (string)value; break;
						case "Month10": this.str.Month10 = (string)value; break;
						case "Month11": this.str.Month11 = (string)value; break;
						case "Month12": this.str.Month12 = (string)value; break;
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
						case "Time":

							if (value == null || value is System.Int32)
								this.Time = (System.Int32?)value;
							break;
						case "Month01":

							if (value == null || value is System.Decimal)
								this.Month01 = (System.Decimal?)value;
							break;
						case "Month02":

							if (value == null || value is System.Decimal)
								this.Month02 = (System.Decimal?)value;
							break;
						case "Month03":

							if (value == null || value is System.Decimal)
								this.Month03 = (System.Decimal?)value;
							break;
						case "Month04":

							if (value == null || value is System.Decimal)
								this.Month04 = (System.Decimal?)value;
							break;
						case "Month05":

							if (value == null || value is System.Decimal)
								this.Month05 = (System.Decimal?)value;
							break;
						case "Month06":

							if (value == null || value is System.Decimal)
								this.Month06 = (System.Decimal?)value;
							break;
						case "Month07":

							if (value == null || value is System.Decimal)
								this.Month07 = (System.Decimal?)value;
							break;
						case "Month08":

							if (value == null || value is System.Decimal)
								this.Month08 = (System.Decimal?)value;
							break;
						case "Month09":

							if (value == null || value is System.Decimal)
								this.Month09 = (System.Decimal?)value;
							break;
						case "Month10":

							if (value == null || value is System.Decimal)
								this.Month10 = (System.Decimal?)value;
							break;
						case "Month11":

							if (value == null || value is System.Decimal)
								this.Month11 = (System.Decimal?)value;
							break;
						case "Month12":

							if (value == null || value is System.Decimal)
								this.Month12 = (System.Decimal?)value;
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
		/// Maps to PerformancePlanPppkItem.PerformancePlanItemID
		/// </summary>
		virtual public System.Int32? PerformancePlanItemID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanItemID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanItemID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.PerformancePlanNo
		/// </summary>
		virtual public System.String PerformancePlanNo
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanNo);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanNo, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Activity
		/// </summary>
		virtual public System.String Activity
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.Activity);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.Activity, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.UnitTargets
		/// </summary>
		virtual public System.String UnitTargets
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.UnitTargets);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.UnitTargets, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Time
		/// </summary>
		virtual public System.Int32? Time
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.Time);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkItemMetadata.ColumnNames.Time, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.SRAchievementFormula
		/// </summary>
		virtual public System.String SRAchievementFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.SRAchievementFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.SRAchievementFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.SRRealizationFormula
		/// </summary>
		virtual public System.String SRRealizationFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.SRRealizationFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.SRRealizationFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month01
		/// </summary>
		virtual public System.Decimal? Month01
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month01);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month01, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month02
		/// </summary>
		virtual public System.Decimal? Month02
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month02);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month02, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month03
		/// </summary>
		virtual public System.Decimal? Month03
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month03);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month03, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month04
		/// </summary>
		virtual public System.Decimal? Month04
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month04);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month04, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month05
		/// </summary>
		virtual public System.Decimal? Month05
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month05);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month05, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month06
		/// </summary>
		virtual public System.Decimal? Month06
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month06);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month06, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month07
		/// </summary>
		virtual public System.Decimal? Month07
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month07);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month07, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month08
		/// </summary>
		virtual public System.Decimal? Month08
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month08);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month08, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month09
		/// </summary>
		virtual public System.Decimal? Month09
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month09);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month09, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month10
		/// </summary>
		virtual public System.Decimal? Month10
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month10);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month10, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month11
		/// </summary>
		virtual public System.Decimal? Month11
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month11);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month11, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.Month12
		/// </summary>
		virtual public System.Decimal? Month12
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month12);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.Month12, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.YearTarget
		/// </summary>
		virtual public System.Decimal? YearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.YearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanPppkItemMetadata.ColumnNames.YearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanPppkItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanPppkItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppkItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanPppkItem entity)
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
			public System.String Time
			{
				get
				{
					System.Int32? data = entity.Time;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time = null;
					else entity.Time = Convert.ToInt32(value);
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
			public System.String Month01
			{
				get
				{
					System.Decimal? data = entity.Month01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month01 = null;
					else entity.Month01 = Convert.ToDecimal(value);
				}
			}
			public System.String Month02
			{
				get
				{
					System.Decimal? data = entity.Month02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month02 = null;
					else entity.Month02 = Convert.ToDecimal(value);
				}
			}
			public System.String Month03
			{
				get
				{
					System.Decimal? data = entity.Month03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month03 = null;
					else entity.Month03 = Convert.ToDecimal(value);
				}
			}
			public System.String Month04
			{
				get
				{
					System.Decimal? data = entity.Month04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month04 = null;
					else entity.Month04 = Convert.ToDecimal(value);
				}
			}
			public System.String Month05
			{
				get
				{
					System.Decimal? data = entity.Month05;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month05 = null;
					else entity.Month05 = Convert.ToDecimal(value);
				}
			}
			public System.String Month06
			{
				get
				{
					System.Decimal? data = entity.Month06;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month06 = null;
					else entity.Month06 = Convert.ToDecimal(value);
				}
			}
			public System.String Month07
			{
				get
				{
					System.Decimal? data = entity.Month07;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month07 = null;
					else entity.Month07 = Convert.ToDecimal(value);
				}
			}
			public System.String Month08
			{
				get
				{
					System.Decimal? data = entity.Month08;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month08 = null;
					else entity.Month08 = Convert.ToDecimal(value);
				}
			}
			public System.String Month09
			{
				get
				{
					System.Decimal? data = entity.Month09;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month09 = null;
					else entity.Month09 = Convert.ToDecimal(value);
				}
			}
			public System.String Month10
			{
				get
				{
					System.Decimal? data = entity.Month10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month10 = null;
					else entity.Month10 = Convert.ToDecimal(value);
				}
			}
			public System.String Month11
			{
				get
				{
					System.Decimal? data = entity.Month11;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month11 = null;
					else entity.Month11 = Convert.ToDecimal(value);
				}
			}
			public System.String Month12
			{
				get
				{
					System.Decimal? data = entity.Month12;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month12 = null;
					else entity.Month12 = Convert.ToDecimal(value);
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
			private esPerformancePlanPppkItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanPppkItemQuery query)
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
				throw new Exception("esPerformancePlanPppkItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanPppkItem : esPerformancePlanPppkItem
	{
	}

	[Serializable]
	abstract public class esPerformancePlanPppkItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanPppkItemMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanItemID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanItemID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanNo
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanNo, esSystemType.String);
			}
		}

		public esQueryItem Activity
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Activity, esSystemType.String);
			}
		}

		public esQueryItem UnitTargets
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.UnitTargets, esSystemType.String);
			}
		}

		public esQueryItem Time
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Time, esSystemType.Int32);
			}
		}

		public esQueryItem SRAchievementFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.SRAchievementFormula, esSystemType.String);
			}
		}

		public esQueryItem SRRealizationFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.SRRealizationFormula, esSystemType.String);
			}
		}

		public esQueryItem Month01
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month01, esSystemType.Decimal);
			}
		}

		public esQueryItem Month02
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month02, esSystemType.Decimal);
			}
		}

		public esQueryItem Month03
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month03, esSystemType.Decimal);
			}
		}

		public esQueryItem Month04
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month04, esSystemType.Decimal);
			}
		}

		public esQueryItem Month05
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month05, esSystemType.Decimal);
			}
		}

		public esQueryItem Month06
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month06, esSystemType.Decimal);
			}
		}

		public esQueryItem Month07
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month07, esSystemType.Decimal);
			}
		}

		public esQueryItem Month08
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month08, esSystemType.Decimal);
			}
		}

		public esQueryItem Month09
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month09, esSystemType.Decimal);
			}
		}

		public esQueryItem Month10
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month10, esSystemType.Decimal);
			}
		}

		public esQueryItem Month11
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month11, esSystemType.Decimal);
			}
		}

		public esQueryItem Month12
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.Month12, esSystemType.Decimal);
			}
		}

		public esQueryItem YearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.YearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanPppkItemCollection")]
	public partial class PerformancePlanPppkItemCollection : esPerformancePlanPppkItemCollection, IEnumerable<PerformancePlanPppkItem>
	{
		public PerformancePlanPppkItemCollection()
		{

		}

		public static implicit operator List<PerformancePlanPppkItem>(PerformancePlanPppkItemCollection coll)
		{
			List<PerformancePlanPppkItem> list = new List<PerformancePlanPppkItem>();

			foreach (PerformancePlanPppkItem emp in coll)
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
				return PerformancePlanPppkItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanPppkItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanPppkItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanPppkItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanPppkItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanPppkItemQuery();
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
		public bool Load(PerformancePlanPppkItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanPppkItem AddNew()
		{
			PerformancePlanPppkItem entity = base.AddNewEntity() as PerformancePlanPppkItem;

			return entity;
		}
		public PerformancePlanPppkItem FindByPrimaryKey(Int32 performancePlanItemID)
		{
			return base.FindByPrimaryKey(performancePlanItemID) as PerformancePlanPppkItem;
		}

		#region IEnumerable< PerformancePlanPppkItem> Members

		IEnumerator<PerformancePlanPppkItem> IEnumerable<PerformancePlanPppkItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanPppkItem;
			}
		}

		#endregion

		private PerformancePlanPppkItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanPppkItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanPppkItem ({PerformancePlanItemID})")]
	[Serializable]
	public partial class PerformancePlanPppkItem : esPerformancePlanPppkItem
	{
		public PerformancePlanPppkItem()
		{
		}

		public PerformancePlanPppkItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanPppkItemMetadata.Meta();
			}
		}

		override protected esPerformancePlanPppkItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanPppkItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanPppkItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanPppkItemQuery();
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
		public bool Load(PerformancePlanPppkItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanPppkItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanPppkItemQuery : esPerformancePlanPppkItemQuery
	{
		public PerformancePlanPppkItemQuery()
		{

		}

		public PerformancePlanPppkItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanPppkItemQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanPppkItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanPppkItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.PerformancePlanItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.PerformancePlanID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.PerformancePlanNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.PerformancePlanNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Activity, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Activity;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.UnitTargets, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.UnitTargets;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Time, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Time;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.SRAchievementFormula, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.SRAchievementFormula;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.SRRealizationFormula, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.SRRealizationFormula;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month01, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month01;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month02, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month02;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month03, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month03;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month04, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month04;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month05, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month05;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month06, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month06;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month07, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month07;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month08, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month08;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month09, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month09;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month10, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month11, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month11;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.Month12, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.Month12;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.YearTarget, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.YearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.CreatedDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.CreatedByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkItemMetadata.ColumnNames.LastUpdateByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanPppkItemMetadata Meta()
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
			public const string PerformancePlanNo = "PerformancePlanNo";
			public const string Activity = "Activity";
			public const string UnitTargets = "UnitTargets";
			public const string Time = "Time";
			public const string SRAchievementFormula = "SRAchievementFormula";
			public const string SRRealizationFormula = "SRRealizationFormula";
			public const string Month01 = "Month01";
			public const string Month02 = "Month02";
			public const string Month03 = "Month03";
			public const string Month04 = "Month04";
			public const string Month05 = "Month05";
			public const string Month06 = "Month06";
			public const string Month07 = "Month07";
			public const string Month08 = "Month08";
			public const string Month09 = "Month09";
			public const string Month10 = "Month10";
			public const string Month11 = "Month11";
			public const string Month12 = "Month12";
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
			public const string PerformancePlanNo = "PerformancePlanNo";
			public const string Activity = "Activity";
			public const string UnitTargets = "UnitTargets";
			public const string Time = "Time";
			public const string SRAchievementFormula = "SRAchievementFormula";
			public const string SRRealizationFormula = "SRRealizationFormula";
			public const string Month01 = "Month01";
			public const string Month02 = "Month02";
			public const string Month03 = "Month03";
			public const string Month04 = "Month04";
			public const string Month05 = "Month05";
			public const string Month06 = "Month06";
			public const string Month07 = "Month07";
			public const string Month08 = "Month08";
			public const string Month09 = "Month09";
			public const string Month10 = "Month10";
			public const string Month11 = "Month11";
			public const string Month12 = "Month12";
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
			lock (typeof(PerformancePlanPppkItemMetadata))
			{
				if (PerformancePlanPppkItemMetadata.mapDelegates == null)
				{
					PerformancePlanPppkItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanPppkItemMetadata.meta == null)
				{
					PerformancePlanPppkItemMetadata.meta = new PerformancePlanPppkItemMetadata();
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
				meta.AddTypeMap("PerformancePlanNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Activity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UnitTargets", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Time", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAchievementFormula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRealizationFormula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Month01", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month02", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month03", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month04", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month05", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month06", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month07", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month08", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month09", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month11", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Month12", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("YearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanPppkItem";
				meta.Destination = "PerformancePlanPppkItem";
				meta.spInsert = "proc_PerformancePlanPppkItemInsert";
				meta.spUpdate = "proc_PerformancePlanPppkItemUpdate";
				meta.spDelete = "proc_PerformancePlanPppkItemDelete";
				meta.spLoadAll = "proc_PerformancePlanPppkItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanPppkItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanPppkItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
