/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 11:29:11 AM
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
	abstract public class esPerformancePlanNonJptItemCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanNonJptItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanNonJptItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptItemQuery query)
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
			this.InitQuery(query as esPerformancePlanNonJptItemQuery);
		}
		#endregion

		virtual public PerformancePlanNonJptItem DetachEntity(PerformancePlanNonJptItem entity)
		{
			return base.DetachEntity(entity) as PerformancePlanNonJptItem;
		}

		virtual public PerformancePlanNonJptItem AttachEntity(PerformancePlanNonJptItem entity)
		{
			return base.AttachEntity(entity) as PerformancePlanNonJptItem;
		}

		virtual public void Combine(PerformancePlanNonJptItemCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanNonJptItem this[int index]
		{
			get
			{
				return base[index] as PerformancePlanNonJptItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanNonJptItem);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanNonJptItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanNonJptItem()
		{
		}

		public esPerformancePlanNonJptItem(DataRow row)
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
			esPerformancePlanNonJptItemQuery query = this.GetDynamicQuery();
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
						case "SRPerformancePlanActivityType": this.str.SRPerformancePlanActivityType = (string)value; break;
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
		/// Maps to PerformancePlanNonJptItem.PerformancePlanItemID
		/// </summary>
		virtual public System.Int32? PerformancePlanItemID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanItemID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanItemID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.PerformancePlanNo
		/// </summary>
		virtual public System.String PerformancePlanNo
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanNo);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanNo, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.Activity
		/// </summary>
		virtual public System.String Activity
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.Activity);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.Activity, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.SRPerformancePlanActivityType
		/// </summary>
		virtual public System.String SRPerformancePlanActivityType
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRPerformancePlanActivityType);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRPerformancePlanActivityType, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.UnitTargets
		/// </summary>
		virtual public System.String UnitTargets
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.UnitTargets);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.UnitTargets, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.SRAchievementFormula
		/// </summary>
		virtual public System.String SRAchievementFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRAchievementFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRAchievementFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.SRRealizationFormula
		/// </summary>
		virtual public System.String SRRealizationFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRRealizationFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.SRRealizationFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.Quarter1
		/// </summary>
		virtual public System.Decimal? Quarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.Quarter2
		/// </summary>
		virtual public System.Decimal? Quarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.Quarter3
		/// </summary>
		virtual public System.Decimal? Quarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.Quarter4
		/// </summary>
		virtual public System.Decimal? Quarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.YearTarget
		/// </summary>
		virtual public System.Decimal? YearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.YearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptItemMetadata.ColumnNames.YearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanNonJptItem entity)
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
			public System.String SRPerformancePlanActivityType
			{
				get
				{
					System.String data = entity.SRPerformancePlanActivityType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPerformancePlanActivityType = null;
					else entity.SRPerformancePlanActivityType = Convert.ToString(value);
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
			private esPerformancePlanNonJptItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptItemQuery query)
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
				throw new Exception("esPerformancePlanNonJptItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanNonJptItem : esPerformancePlanNonJptItem
	{
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptItemMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanItemID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanItemID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanNo
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanNo, esSystemType.String);
			}
		}

		public esQueryItem Activity
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.Activity, esSystemType.String);
			}
		}

		public esQueryItem SRPerformancePlanActivityType
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.SRPerformancePlanActivityType, esSystemType.String);
			}
		}

		public esQueryItem UnitTargets
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.UnitTargets, esSystemType.String);
			}
		}

		public esQueryItem SRAchievementFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.SRAchievementFormula, esSystemType.String);
			}
		}

		public esQueryItem SRRealizationFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.SRRealizationFormula, esSystemType.String);
			}
		}

		public esQueryItem Quarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.Quarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.Quarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.Quarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.Quarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem YearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.YearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanNonJptItemCollection")]
	public partial class PerformancePlanNonJptItemCollection : esPerformancePlanNonJptItemCollection, IEnumerable<PerformancePlanNonJptItem>
	{
		public PerformancePlanNonJptItemCollection()
		{

		}

		public static implicit operator List<PerformancePlanNonJptItem>(PerformancePlanNonJptItemCollection coll)
		{
			List<PerformancePlanNonJptItem> list = new List<PerformancePlanNonJptItem>();

			foreach (PerformancePlanNonJptItem emp in coll)
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
				return PerformancePlanNonJptItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanNonJptItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanNonJptItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptItemQuery();
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
		public bool Load(PerformancePlanNonJptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanNonJptItem AddNew()
		{
			PerformancePlanNonJptItem entity = base.AddNewEntity() as PerformancePlanNonJptItem;

			return entity;
		}
		public PerformancePlanNonJptItem FindByPrimaryKey(Int32 performancePlanItemID)
		{
			return base.FindByPrimaryKey(performancePlanItemID) as PerformancePlanNonJptItem;
		}

		#region IEnumerable< PerformancePlanNonJptItem> Members

		IEnumerator<PerformancePlanNonJptItem> IEnumerable<PerformancePlanNonJptItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanNonJptItem;
			}
		}

		#endregion

		private PerformancePlanNonJptItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanNonJptItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanNonJptItem ({PerformancePlanItemID})")]
	[Serializable]
	public partial class PerformancePlanNonJptItem : esPerformancePlanNonJptItem
	{
		public PerformancePlanNonJptItem()
		{
		}

		public PerformancePlanNonJptItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptItemMetadata.Meta();
			}
		}

		override protected esPerformancePlanNonJptItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptItemQuery();
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
		public bool Load(PerformancePlanNonJptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanNonJptItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanNonJptItemQuery : esPerformancePlanNonJptItemQuery
	{
		public PerformancePlanNonJptItemQuery()
		{

		}

		public PerformancePlanNonJptItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanNonJptItemQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanNonJptItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanNonJptItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.PerformancePlanItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.PerformancePlanID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.PerformancePlanNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.PerformancePlanNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.Activity, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.Activity;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.SRPerformancePlanActivityType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.SRPerformancePlanActivityType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.UnitTargets, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.UnitTargets;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.SRAchievementFormula, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.SRAchievementFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.SRRealizationFormula, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.SRRealizationFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter1, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.Quarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter2, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.Quarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter3, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.Quarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.Quarter4, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.Quarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.YearTarget, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.YearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.CreatedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptItemMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanNonJptItemMetadata Meta()
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
			public const string SRPerformancePlanActivityType = "SRPerformancePlanActivityType";
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
			public const string PerformancePlanNo = "PerformancePlanNo";
			public const string Activity = "Activity";
			public const string SRPerformancePlanActivityType = "SRPerformancePlanActivityType";
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
			lock (typeof(PerformancePlanNonJptItemMetadata))
			{
				if (PerformancePlanNonJptItemMetadata.mapDelegates == null)
				{
					PerformancePlanNonJptItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanNonJptItemMetadata.meta == null)
				{
					PerformancePlanNonJptItemMetadata.meta = new PerformancePlanNonJptItemMetadata();
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
				meta.AddTypeMap("SRPerformancePlanActivityType", new esTypeMap("varchar", "System.String"));
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


				meta.Source = "PerformancePlanNonJptItem";
				meta.Destination = "PerformancePlanNonJptItem";
				meta.spInsert = "proc_PerformancePlanNonJptItemInsert";
				meta.spUpdate = "proc_PerformancePlanNonJptItemUpdate";
				meta.spDelete = "proc_PerformancePlanNonJptItemDelete";
				meta.spLoadAll = "proc_PerformancePlanNonJptItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanNonJptItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanNonJptItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
