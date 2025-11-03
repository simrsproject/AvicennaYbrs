/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/22/2023 1:19:43 PM
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
	abstract public class esPerformancePlanNonJptTxItemCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanNonJptTxItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanNonJptTxItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptTxItemQuery query)
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
			this.InitQuery(query as esPerformancePlanNonJptTxItemQuery);
		}
		#endregion

		virtual public PerformancePlanNonJptTxItem DetachEntity(PerformancePlanNonJptTxItem entity)
		{
			return base.DetachEntity(entity) as PerformancePlanNonJptTxItem;
		}

		virtual public PerformancePlanNonJptTxItem AttachEntity(PerformancePlanNonJptTxItem entity)
		{
			return base.AttachEntity(entity) as PerformancePlanNonJptTxItem;
		}

		virtual public void Combine(PerformancePlanNonJptTxItemCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanNonJptTxItem this[int index]
		{
			get
			{
				return base[index] as PerformancePlanNonJptTxItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanNonJptTxItem);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptTxItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanNonJptTxItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanNonJptTxItem()
		{
		}

		public esPerformancePlanNonJptTxItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 txItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(txItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 txItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(txItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 txItemID)
		{
			esPerformancePlanNonJptTxItemQuery query = this.GetDynamicQuery();
			query.Where(query.TxItemID == txItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 txItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TxItemID", txItemID);
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
						case "TxItemID": this.str.TxItemID = (string)value; break;
						case "TxID": this.str.TxID = (string)value; break;
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
						case "RevisionQuarter1": this.str.RevisionQuarter1 = (string)value; break;
						case "RevisionQuarter2": this.str.RevisionQuarter2 = (string)value; break;
						case "RevisionQuarter3": this.str.RevisionQuarter3 = (string)value; break;
						case "RevisionQuarter4": this.str.RevisionQuarter4 = (string)value; break;
						case "RevisionYearTarget": this.str.RevisionYearTarget = (string)value; break;
						case "RealizationQuarter1": this.str.RealizationQuarter1 = (string)value; break;
						case "RealizationQuarter2": this.str.RealizationQuarter2 = (string)value; break;
						case "RealizationQuarter3": this.str.RealizationQuarter3 = (string)value; break;
						case "RealizationQuarter4": this.str.RealizationQuarter4 = (string)value; break;
						case "RealizationYearTarget": this.str.RealizationYearTarget = (string)value; break;
						case "RealizationNotesQuarter1": this.str.RealizationNotesQuarter1 = (string)value; break;
						case "RealizationNotesQuarter2": this.str.RealizationNotesQuarter2 = (string)value; break;
						case "RealizationNotesQuarter3": this.str.RealizationNotesQuarter3 = (string)value; break;
						case "RealizationNotesQuarter4": this.str.RealizationNotesQuarter4 = (string)value; break;
						case "ValidationQuarter1": this.str.ValidationQuarter1 = (string)value; break;
						case "ValidationQuarter2": this.str.ValidationQuarter2 = (string)value; break;
						case "ValidationQuarter3": this.str.ValidationQuarter3 = (string)value; break;
						case "ValidationQuarter4": this.str.ValidationQuarter4 = (string)value; break;
						case "ValidationYearTarget": this.str.ValidationYearTarget = (string)value; break;
						case "ValidationNotesQuarter1": this.str.ValidationNotesQuarter1 = (string)value; break;
						case "ValidationNotesQuarter2": this.str.ValidationNotesQuarter2 = (string)value; break;
						case "ValidationNotesQuarter3": this.str.ValidationNotesQuarter3 = (string)value; break;
						case "ValidationNotesQuarter4": this.str.ValidationNotesQuarter4 = (string)value; break;
						case "AchievementsYearTarget": this.str.AchievementsYearTarget = (string)value; break;
						case "IsAdditional": this.str.IsAdditional = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsRealizationQuarter1": this.str.IsRealizationQuarter1 = (string)value; break;
						case "RealizationQuarter1DateTime": this.str.RealizationQuarter1DateTime = (string)value; break;
						case "RealizationQuarter1ByUserID": this.str.RealizationQuarter1ByUserID = (string)value; break;
						case "IsRealizationQuarter2": this.str.IsRealizationQuarter2 = (string)value; break;
						case "RealizationQuarter2DateTime": this.str.RealizationQuarter2DateTime = (string)value; break;
						case "RealizationQuarter2ByUserID": this.str.RealizationQuarter2ByUserID = (string)value; break;
						case "IsRealizationQuarter3": this.str.IsRealizationQuarter3 = (string)value; break;
						case "RealizationQuarter3DateTime": this.str.RealizationQuarter3DateTime = (string)value; break;
						case "RealizationQuarter3ByUserID": this.str.RealizationQuarter3ByUserID = (string)value; break;
						case "IsRealizationQuarter4": this.str.IsRealizationQuarter4 = (string)value; break;
						case "RealizationQuarter4DateTime": this.str.RealizationQuarter4DateTime = (string)value; break;
						case "RealizationQuarter4ByUserID": this.str.RealizationQuarter4ByUserID = (string)value; break;
						case "IsValidationQuarter1": this.str.IsValidationQuarter1 = (string)value; break;
						case "ValidationQuarter1DateTime": this.str.ValidationQuarter1DateTime = (string)value; break;
						case "ValidationQuarter1ByUserID": this.str.ValidationQuarter1ByUserID = (string)value; break;
						case "IsValidationQuarter2": this.str.IsValidationQuarter2 = (string)value; break;
						case "ValidationQuarter2DateTime": this.str.ValidationQuarter2DateTime = (string)value; break;
						case "ValidationQuarter2ByUserID": this.str.ValidationQuarter2ByUserID = (string)value; break;
						case "ValidationQuarter3DateTime": this.str.ValidationQuarter3DateTime = (string)value; break;
						case "IsValidationQuarter3": this.str.IsValidationQuarter3 = (string)value; break;
						case "ValidationQuarter3ByUserID": this.str.ValidationQuarter3ByUserID = (string)value; break;
						case "IsValidationQuarter4": this.str.IsValidationQuarter4 = (string)value; break;
						case "ValidationQuarter4DateTime": this.str.ValidationQuarter4DateTime = (string)value; break;
						case "ValidationQuarter4ByUserID": this.str.ValidationQuarter4ByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TxItemID":

							if (value == null || value is System.Int64)
								this.TxItemID = (System.Int64?)value;
							break;
						case "TxID":

							if (value == null || value is System.Int64)
								this.TxID = (System.Int64?)value;
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
						case "RevisionQuarter1":

							if (value == null || value is System.Decimal)
								this.RevisionQuarter1 = (System.Decimal?)value;
							break;
						case "RevisionQuarter2":

							if (value == null || value is System.Decimal)
								this.RevisionQuarter2 = (System.Decimal?)value;
							break;
						case "RevisionQuarter3":

							if (value == null || value is System.Decimal)
								this.RevisionQuarter3 = (System.Decimal?)value;
							break;
						case "RevisionQuarter4":

							if (value == null || value is System.Decimal)
								this.RevisionQuarter4 = (System.Decimal?)value;
							break;
						case "RevisionYearTarget":

							if (value == null || value is System.Decimal)
								this.RevisionYearTarget = (System.Decimal?)value;
							break;
						case "RealizationQuarter1":

							if (value == null || value is System.Decimal)
								this.RealizationQuarter1 = (System.Decimal?)value;
							break;
						case "RealizationQuarter2":

							if (value == null || value is System.Decimal)
								this.RealizationQuarter2 = (System.Decimal?)value;
							break;
						case "RealizationQuarter3":

							if (value == null || value is System.Decimal)
								this.RealizationQuarter3 = (System.Decimal?)value;
							break;
						case "RealizationQuarter4":

							if (value == null || value is System.Decimal)
								this.RealizationQuarter4 = (System.Decimal?)value;
							break;
						case "RealizationYearTarget":

							if (value == null || value is System.Decimal)
								this.RealizationYearTarget = (System.Decimal?)value;
							break;
						case "ValidationQuarter1":

							if (value == null || value is System.Decimal)
								this.ValidationQuarter1 = (System.Decimal?)value;
							break;
						case "ValidationQuarter2":

							if (value == null || value is System.Decimal)
								this.ValidationQuarter2 = (System.Decimal?)value;
							break;
						case "ValidationQuarter3":

							if (value == null || value is System.Decimal)
								this.ValidationQuarter3 = (System.Decimal?)value;
							break;
						case "ValidationQuarter4":

							if (value == null || value is System.Decimal)
								this.ValidationQuarter4 = (System.Decimal?)value;
							break;
						case "ValidationYearTarget":

							if (value == null || value is System.Decimal)
								this.ValidationYearTarget = (System.Decimal?)value;
							break;
						case "AchievementsYearTarget":

							if (value == null || value is System.Decimal)
								this.AchievementsYearTarget = (System.Decimal?)value;
							break;
						case "IsAdditional":

							if (value == null || value is System.Boolean)
								this.IsAdditional = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsRealizationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsRealizationQuarter1 = (System.Boolean?)value;
							break;
						case "RealizationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.RealizationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsRealizationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsRealizationQuarter2 = (System.Boolean?)value;
							break;
						case "RealizationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.RealizationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "IsRealizationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsRealizationQuarter3 = (System.Boolean?)value;
							break;
						case "RealizationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.RealizationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsRealizationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsRealizationQuarter4 = (System.Boolean?)value;
							break;
						case "RealizationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.RealizationQuarter4DateTime = (System.DateTime?)value;
							break;
						case "IsValidationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsValidationQuarter1 = (System.Boolean?)value;
							break;
						case "ValidationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.ValidationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsValidationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsValidationQuarter2 = (System.Boolean?)value;
							break;
						case "ValidationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.ValidationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "ValidationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.ValidationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsValidationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsValidationQuarter3 = (System.Boolean?)value;
							break;
						case "IsValidationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsValidationQuarter4 = (System.Boolean?)value;
							break;
						case "ValidationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.ValidationQuarter4DateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanNonJptTxItem.TxItemID
		/// </summary>
		virtual public System.Int64? TxItemID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxItemID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxItemID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.TxID
		/// </summary>
		virtual public System.Int64? TxID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.PerformancePlanNo
		/// </summary>
		virtual public System.String PerformancePlanNo
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.PerformancePlanNo);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.PerformancePlanNo, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.Activity
		/// </summary>
		virtual public System.String Activity
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.Activity);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.Activity, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.SRPerformancePlanActivityType
		/// </summary>
		virtual public System.String SRPerformancePlanActivityType
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRPerformancePlanActivityType);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRPerformancePlanActivityType, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.UnitTargets
		/// </summary>
		virtual public System.String UnitTargets
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.UnitTargets);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.UnitTargets, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.SRAchievementFormula
		/// </summary>
		virtual public System.String SRAchievementFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRAchievementFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRAchievementFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.SRRealizationFormula
		/// </summary>
		virtual public System.String SRRealizationFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRRealizationFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRRealizationFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.Quarter1
		/// </summary>
		virtual public System.Decimal? Quarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.Quarter2
		/// </summary>
		virtual public System.Decimal? Quarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.Quarter3
		/// </summary>
		virtual public System.Decimal? Quarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.Quarter4
		/// </summary>
		virtual public System.Decimal? Quarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.YearTarget
		/// </summary>
		virtual public System.Decimal? YearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.YearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.YearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RevisionQuarter1
		/// </summary>
		virtual public System.Decimal? RevisionQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RevisionQuarter2
		/// </summary>
		virtual public System.Decimal? RevisionQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RevisionQuarter3
		/// </summary>
		virtual public System.Decimal? RevisionQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RevisionQuarter4
		/// </summary>
		virtual public System.Decimal? RevisionQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RevisionYearTarget
		/// </summary>
		virtual public System.Decimal? RevisionYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter1
		/// </summary>
		virtual public System.Decimal? RealizationQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter2
		/// </summary>
		virtual public System.Decimal? RealizationQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter3
		/// </summary>
		virtual public System.Decimal? RealizationQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter4
		/// </summary>
		virtual public System.Decimal? RealizationQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationYearTarget
		/// </summary>
		virtual public System.Decimal? RealizationYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationNotesQuarter1
		/// </summary>
		virtual public System.String RealizationNotesQuarter1
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter1);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationNotesQuarter2
		/// </summary>
		virtual public System.String RealizationNotesQuarter2
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter2);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationNotesQuarter3
		/// </summary>
		virtual public System.String RealizationNotesQuarter3
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter3);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationNotesQuarter4
		/// </summary>
		virtual public System.String RealizationNotesQuarter4
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter4);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter1
		/// </summary>
		virtual public System.Decimal? ValidationQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter2
		/// </summary>
		virtual public System.Decimal? ValidationQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter3
		/// </summary>
		virtual public System.Decimal? ValidationQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter4
		/// </summary>
		virtual public System.Decimal? ValidationQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationYearTarget
		/// </summary>
		virtual public System.Decimal? ValidationYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationNotesQuarter1
		/// </summary>
		virtual public System.String ValidationNotesQuarter1
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter1);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationNotesQuarter2
		/// </summary>
		virtual public System.String ValidationNotesQuarter2
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter2);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationNotesQuarter3
		/// </summary>
		virtual public System.String ValidationNotesQuarter3
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter3);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationNotesQuarter4
		/// </summary>
		virtual public System.String ValidationNotesQuarter4
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter4);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.AchievementsYearTarget
		/// </summary>
		virtual public System.Decimal? AchievementsYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.AchievementsYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanNonJptTxItemMetadata.ColumnNames.AchievementsYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsAdditional
		/// </summary>
		virtual public System.Boolean? IsAdditional
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsAdditional);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsAdditional, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsRealizationQuarter1
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter1ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsRealizationQuarter2
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter2ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsRealizationQuarter3
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter3ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsRealizationQuarter4
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.RealizationQuarter4ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsValidationQuarter1
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter1ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsValidationQuarter2
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter2ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsValidationQuarter3
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter3ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.IsValidationQuarter4
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.ValidationQuarter4ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTxItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanNonJptTxItem entity)
			{
				this.entity = entity;
			}
			public System.String TxItemID
			{
				get
				{
					System.Int64? data = entity.TxItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxItemID = null;
					else entity.TxItemID = Convert.ToInt64(value);
				}
			}
			public System.String TxID
			{
				get
				{
					System.Int64? data = entity.TxID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxID = null;
					else entity.TxID = Convert.ToInt64(value);
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
			public System.String RevisionQuarter1
			{
				get
				{
					System.Decimal? data = entity.RevisionQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionQuarter1 = null;
					else entity.RevisionQuarter1 = Convert.ToDecimal(value);
				}
			}
			public System.String RevisionQuarter2
			{
				get
				{
					System.Decimal? data = entity.RevisionQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionQuarter2 = null;
					else entity.RevisionQuarter2 = Convert.ToDecimal(value);
				}
			}
			public System.String RevisionQuarter3
			{
				get
				{
					System.Decimal? data = entity.RevisionQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionQuarter3 = null;
					else entity.RevisionQuarter3 = Convert.ToDecimal(value);
				}
			}
			public System.String RevisionQuarter4
			{
				get
				{
					System.Decimal? data = entity.RevisionQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionQuarter4 = null;
					else entity.RevisionQuarter4 = Convert.ToDecimal(value);
				}
			}
			public System.String RevisionYearTarget
			{
				get
				{
					System.Decimal? data = entity.RevisionYearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionYearTarget = null;
					else entity.RevisionYearTarget = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationQuarter1
			{
				get
				{
					System.Decimal? data = entity.RealizationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter1 = null;
					else entity.RealizationQuarter1 = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationQuarter2
			{
				get
				{
					System.Decimal? data = entity.RealizationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter2 = null;
					else entity.RealizationQuarter2 = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationQuarter3
			{
				get
				{
					System.Decimal? data = entity.RealizationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter3 = null;
					else entity.RealizationQuarter3 = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationQuarter4
			{
				get
				{
					System.Decimal? data = entity.RealizationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter4 = null;
					else entity.RealizationQuarter4 = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationYearTarget
			{
				get
				{
					System.Decimal? data = entity.RealizationYearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationYearTarget = null;
					else entity.RealizationYearTarget = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationNotesQuarter1
			{
				get
				{
					System.String data = entity.RealizationNotesQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationNotesQuarter1 = null;
					else entity.RealizationNotesQuarter1 = Convert.ToString(value);
				}
			}
			public System.String RealizationNotesQuarter2
			{
				get
				{
					System.String data = entity.RealizationNotesQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationNotesQuarter2 = null;
					else entity.RealizationNotesQuarter2 = Convert.ToString(value);
				}
			}
			public System.String RealizationNotesQuarter3
			{
				get
				{
					System.String data = entity.RealizationNotesQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationNotesQuarter3 = null;
					else entity.RealizationNotesQuarter3 = Convert.ToString(value);
				}
			}
			public System.String RealizationNotesQuarter4
			{
				get
				{
					System.String data = entity.RealizationNotesQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationNotesQuarter4 = null;
					else entity.RealizationNotesQuarter4 = Convert.ToString(value);
				}
			}
			public System.String ValidationQuarter1
			{
				get
				{
					System.Decimal? data = entity.ValidationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter1 = null;
					else entity.ValidationQuarter1 = Convert.ToDecimal(value);
				}
			}
			public System.String ValidationQuarter2
			{
				get
				{
					System.Decimal? data = entity.ValidationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter2 = null;
					else entity.ValidationQuarter2 = Convert.ToDecimal(value);
				}
			}
			public System.String ValidationQuarter3
			{
				get
				{
					System.Decimal? data = entity.ValidationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter3 = null;
					else entity.ValidationQuarter3 = Convert.ToDecimal(value);
				}
			}
			public System.String ValidationQuarter4
			{
				get
				{
					System.Decimal? data = entity.ValidationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter4 = null;
					else entity.ValidationQuarter4 = Convert.ToDecimal(value);
				}
			}
			public System.String ValidationYearTarget
			{
				get
				{
					System.Decimal? data = entity.ValidationYearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationYearTarget = null;
					else entity.ValidationYearTarget = Convert.ToDecimal(value);
				}
			}
			public System.String ValidationNotesQuarter1
			{
				get
				{
					System.String data = entity.ValidationNotesQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationNotesQuarter1 = null;
					else entity.ValidationNotesQuarter1 = Convert.ToString(value);
				}
			}
			public System.String ValidationNotesQuarter2
			{
				get
				{
					System.String data = entity.ValidationNotesQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationNotesQuarter2 = null;
					else entity.ValidationNotesQuarter2 = Convert.ToString(value);
				}
			}
			public System.String ValidationNotesQuarter3
			{
				get
				{
					System.String data = entity.ValidationNotesQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationNotesQuarter3 = null;
					else entity.ValidationNotesQuarter3 = Convert.ToString(value);
				}
			}
			public System.String ValidationNotesQuarter4
			{
				get
				{
					System.String data = entity.ValidationNotesQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationNotesQuarter4 = null;
					else entity.ValidationNotesQuarter4 = Convert.ToString(value);
				}
			}
			public System.String AchievementsYearTarget
			{
				get
				{
					System.Decimal? data = entity.AchievementsYearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AchievementsYearTarget = null;
					else entity.AchievementsYearTarget = Convert.ToDecimal(value);
				}
			}
			public System.String IsAdditional
			{
				get
				{
					System.Boolean? data = entity.IsAdditional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdditional = null;
					else entity.IsAdditional = Convert.ToBoolean(value);
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
			public System.String IsRealizationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsRealizationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRealizationQuarter1 = null;
					else entity.IsRealizationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String RealizationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.RealizationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter1DateTime = null;
					else entity.RealizationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.RealizationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter1ByUserID = null;
					else entity.RealizationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRealizationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsRealizationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRealizationQuarter2 = null;
					else entity.IsRealizationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String RealizationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.RealizationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter2DateTime = null;
					else entity.RealizationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.RealizationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter2ByUserID = null;
					else entity.RealizationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRealizationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsRealizationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRealizationQuarter3 = null;
					else entity.IsRealizationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String RealizationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.RealizationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter3DateTime = null;
					else entity.RealizationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.RealizationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter3ByUserID = null;
					else entity.RealizationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRealizationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsRealizationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRealizationQuarter4 = null;
					else entity.IsRealizationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String RealizationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.RealizationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter4DateTime = null;
					else entity.RealizationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.RealizationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQuarter4ByUserID = null;
					else entity.RealizationQuarter4ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsValidationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsValidationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidationQuarter1 = null;
					else entity.IsValidationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String ValidationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.ValidationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter1DateTime = null;
					else entity.ValidationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.ValidationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter1ByUserID = null;
					else entity.ValidationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsValidationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsValidationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidationQuarter2 = null;
					else entity.IsValidationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String ValidationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.ValidationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter2DateTime = null;
					else entity.ValidationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.ValidationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter2ByUserID = null;
					else entity.ValidationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String ValidationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.ValidationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter3DateTime = null;
					else entity.ValidationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsValidationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsValidationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidationQuarter3 = null;
					else entity.IsValidationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String ValidationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.ValidationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter3ByUserID = null;
					else entity.ValidationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsValidationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsValidationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidationQuarter4 = null;
					else entity.IsValidationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String ValidationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.ValidationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter4DateTime = null;
					else entity.ValidationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.ValidationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidationQuarter4ByUserID = null;
					else entity.ValidationQuarter4ByUserID = Convert.ToString(value);
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
			private esPerformancePlanNonJptTxItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptTxItemQuery query)
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
				throw new Exception("esPerformancePlanNonJptTxItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanNonJptTxItem : esPerformancePlanNonJptTxItem
	{
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptTxItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptTxItemMetadata.Meta();
			}
		}

		public esQueryItem TxItemID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.TxItemID, esSystemType.Int64);
			}
		}

		public esQueryItem TxID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.TxID, esSystemType.Int64);
			}
		}

		public esQueryItem PerformancePlanNo
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.PerformancePlanNo, esSystemType.String);
			}
		}

		public esQueryItem Activity
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.Activity, esSystemType.String);
			}
		}

		public esQueryItem SRPerformancePlanActivityType
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.SRPerformancePlanActivityType, esSystemType.String);
			}
		}

		public esQueryItem UnitTargets
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.UnitTargets, esSystemType.String);
			}
		}

		public esQueryItem SRAchievementFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.SRAchievementFormula, esSystemType.String);
			}
		}

		public esQueryItem SRRealizationFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.SRRealizationFormula, esSystemType.String);
			}
		}

		public esQueryItem Quarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem YearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.YearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationNotesQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter1, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter2, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter3, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter4, esSystemType.String);
			}
		}

		public esQueryItem ValidationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationNotesQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter1, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter2, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter3, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter4, esSystemType.String);
			}
		}

		public esQueryItem AchievementsYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.AchievementsYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAdditional
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsAdditional, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem ValidationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsValidationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanNonJptTxItemCollection")]
	public partial class PerformancePlanNonJptTxItemCollection : esPerformancePlanNonJptTxItemCollection, IEnumerable<PerformancePlanNonJptTxItem>
	{
		public PerformancePlanNonJptTxItemCollection()
		{

		}

		public static implicit operator List<PerformancePlanNonJptTxItem>(PerformancePlanNonJptTxItemCollection coll)
		{
			List<PerformancePlanNonJptTxItem> list = new List<PerformancePlanNonJptTxItem>();

			foreach (PerformancePlanNonJptTxItem emp in coll)
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
				return PerformancePlanNonJptTxItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptTxItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanNonJptTxItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanNonJptTxItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptTxItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptTxItemQuery();
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
		public bool Load(PerformancePlanNonJptTxItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanNonJptTxItem AddNew()
		{
			PerformancePlanNonJptTxItem entity = base.AddNewEntity() as PerformancePlanNonJptTxItem;

			return entity;
		}
		public PerformancePlanNonJptTxItem FindByPrimaryKey(Int64 txItemID)
		{
			return base.FindByPrimaryKey(txItemID) as PerformancePlanNonJptTxItem;
		}

		#region IEnumerable< PerformancePlanNonJptTxItem> Members

		IEnumerator<PerformancePlanNonJptTxItem> IEnumerable<PerformancePlanNonJptTxItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanNonJptTxItem;
			}
		}

		#endregion

		private PerformancePlanNonJptTxItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanNonJptTxItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanNonJptTxItem ({TxItemID})")]
	[Serializable]
	public partial class PerformancePlanNonJptTxItem : esPerformancePlanNonJptTxItem
	{
		public PerformancePlanNonJptTxItem()
		{
		}

		public PerformancePlanNonJptTxItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptTxItemMetadata.Meta();
			}
		}

		override protected esPerformancePlanNonJptTxItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptTxItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptTxItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptTxItemQuery();
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
		public bool Load(PerformancePlanNonJptTxItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanNonJptTxItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanNonJptTxItemQuery : esPerformancePlanNonJptTxItemQuery
	{
		public PerformancePlanNonJptTxItemQuery()
		{

		}

		public PerformancePlanNonJptTxItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanNonJptTxItemQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanNonJptTxItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanNonJptTxItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxItemID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.TxItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.TxID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.TxID;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.PerformancePlanNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.PerformancePlanNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.Activity, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.Activity;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRPerformancePlanActivityType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.SRPerformancePlanActivityType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.UnitTargets, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.UnitTargets;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRAchievementFormula, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.SRAchievementFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.SRRealizationFormula, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.SRRealizationFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter1, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.Quarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter2, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.Quarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter3, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.Quarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.Quarter4, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.Quarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.YearTarget, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.YearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter1, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RevisionQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter2, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RevisionQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter3, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RevisionQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionQuarter4, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RevisionQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RevisionYearTarget, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RevisionYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationYearTarget, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter1, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationNotesQuarter1;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter2, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationNotesQuarter2;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter3, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationNotesQuarter3;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationNotesQuarter4, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationNotesQuarter4;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationYearTarget, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter1, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationNotesQuarter1;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter2, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationNotesQuarter2;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter3, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationNotesQuarter3;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationNotesQuarter4, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationNotesQuarter4;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.AchievementsYearTarget, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.AchievementsYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsAdditional, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsAdditional;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedDateTime, 38, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.CreatedByUserID, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter1, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsRealizationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1DateTime, 41, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter1ByUserID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter2, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsRealizationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2DateTime, 44, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter2ByUserID, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter3, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsRealizationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3DateTime, 47, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter3ByUserID, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsRealizationQuarter4, 49, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsRealizationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4DateTime, 50, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.RealizationQuarter4ByUserID, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.RealizationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter1, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsValidationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1DateTime, 53, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter1ByUserID, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter2, 55, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsValidationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2DateTime, 56, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter2ByUserID, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3DateTime, 58, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter3, 59, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsValidationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter3ByUserID, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.IsValidationQuarter4, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.IsValidationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4DateTime, 62, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.ValidationQuarter4ByUserID, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.ValidationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateDateTime, 64, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxItemMetadata.ColumnNames.LastUpdateByUserID, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanNonJptTxItemMetadata Meta()
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
			public const string TxItemID = "TxItemID";
			public const string TxID = "TxID";
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
			public const string RevisionQuarter1 = "RevisionQuarter1";
			public const string RevisionQuarter2 = "RevisionQuarter2";
			public const string RevisionQuarter3 = "RevisionQuarter3";
			public const string RevisionQuarter4 = "RevisionQuarter4";
			public const string RevisionYearTarget = "RevisionYearTarget";
			public const string RealizationQuarter1 = "RealizationQuarter1";
			public const string RealizationQuarter2 = "RealizationQuarter2";
			public const string RealizationQuarter3 = "RealizationQuarter3";
			public const string RealizationQuarter4 = "RealizationQuarter4";
			public const string RealizationYearTarget = "RealizationYearTarget";
			public const string RealizationNotesQuarter1 = "RealizationNotesQuarter1";
			public const string RealizationNotesQuarter2 = "RealizationNotesQuarter2";
			public const string RealizationNotesQuarter3 = "RealizationNotesQuarter3";
			public const string RealizationNotesQuarter4 = "RealizationNotesQuarter4";
			public const string ValidationQuarter1 = "ValidationQuarter1";
			public const string ValidationQuarter2 = "ValidationQuarter2";
			public const string ValidationQuarter3 = "ValidationQuarter3";
			public const string ValidationQuarter4 = "ValidationQuarter4";
			public const string ValidationYearTarget = "ValidationYearTarget";
			public const string ValidationNotesQuarter1 = "ValidationNotesQuarter1";
			public const string ValidationNotesQuarter2 = "ValidationNotesQuarter2";
			public const string ValidationNotesQuarter3 = "ValidationNotesQuarter3";
			public const string ValidationNotesQuarter4 = "ValidationNotesQuarter4";
			public const string AchievementsYearTarget = "AchievementsYearTarget";
			public const string IsAdditional = "IsAdditional";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsRealizationQuarter1 = "IsRealizationQuarter1";
			public const string RealizationQuarter1DateTime = "RealizationQuarter1DateTime";
			public const string RealizationQuarter1ByUserID = "RealizationQuarter1ByUserID";
			public const string IsRealizationQuarter2 = "IsRealizationQuarter2";
			public const string RealizationQuarter2DateTime = "RealizationQuarter2DateTime";
			public const string RealizationQuarter2ByUserID = "RealizationQuarter2ByUserID";
			public const string IsRealizationQuarter3 = "IsRealizationQuarter3";
			public const string RealizationQuarter3DateTime = "RealizationQuarter3DateTime";
			public const string RealizationQuarter3ByUserID = "RealizationQuarter3ByUserID";
			public const string IsRealizationQuarter4 = "IsRealizationQuarter4";
			public const string RealizationQuarter4DateTime = "RealizationQuarter4DateTime";
			public const string RealizationQuarter4ByUserID = "RealizationQuarter4ByUserID";
			public const string IsValidationQuarter1 = "IsValidationQuarter1";
			public const string ValidationQuarter1DateTime = "ValidationQuarter1DateTime";
			public const string ValidationQuarter1ByUserID = "ValidationQuarter1ByUserID";
			public const string IsValidationQuarter2 = "IsValidationQuarter2";
			public const string ValidationQuarter2DateTime = "ValidationQuarter2DateTime";
			public const string ValidationQuarter2ByUserID = "ValidationQuarter2ByUserID";
			public const string ValidationQuarter3DateTime = "ValidationQuarter3DateTime";
			public const string IsValidationQuarter3 = "IsValidationQuarter3";
			public const string ValidationQuarter3ByUserID = "ValidationQuarter3ByUserID";
			public const string IsValidationQuarter4 = "IsValidationQuarter4";
			public const string ValidationQuarter4DateTime = "ValidationQuarter4DateTime";
			public const string ValidationQuarter4ByUserID = "ValidationQuarter4ByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TxItemID = "TxItemID";
			public const string TxID = "TxID";
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
			public const string RevisionQuarter1 = "RevisionQuarter1";
			public const string RevisionQuarter2 = "RevisionQuarter2";
			public const string RevisionQuarter3 = "RevisionQuarter3";
			public const string RevisionQuarter4 = "RevisionQuarter4";
			public const string RevisionYearTarget = "RevisionYearTarget";
			public const string RealizationQuarter1 = "RealizationQuarter1";
			public const string RealizationQuarter2 = "RealizationQuarter2";
			public const string RealizationQuarter3 = "RealizationQuarter3";
			public const string RealizationQuarter4 = "RealizationQuarter4";
			public const string RealizationYearTarget = "RealizationYearTarget";
			public const string RealizationNotesQuarter1 = "RealizationNotesQuarter1";
			public const string RealizationNotesQuarter2 = "RealizationNotesQuarter2";
			public const string RealizationNotesQuarter3 = "RealizationNotesQuarter3";
			public const string RealizationNotesQuarter4 = "RealizationNotesQuarter4";
			public const string ValidationQuarter1 = "ValidationQuarter1";
			public const string ValidationQuarter2 = "ValidationQuarter2";
			public const string ValidationQuarter3 = "ValidationQuarter3";
			public const string ValidationQuarter4 = "ValidationQuarter4";
			public const string ValidationYearTarget = "ValidationYearTarget";
			public const string ValidationNotesQuarter1 = "ValidationNotesQuarter1";
			public const string ValidationNotesQuarter2 = "ValidationNotesQuarter2";
			public const string ValidationNotesQuarter3 = "ValidationNotesQuarter3";
			public const string ValidationNotesQuarter4 = "ValidationNotesQuarter4";
			public const string AchievementsYearTarget = "AchievementsYearTarget";
			public const string IsAdditional = "IsAdditional";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsRealizationQuarter1 = "IsRealizationQuarter1";
			public const string RealizationQuarter1DateTime = "RealizationQuarter1DateTime";
			public const string RealizationQuarter1ByUserID = "RealizationQuarter1ByUserID";
			public const string IsRealizationQuarter2 = "IsRealizationQuarter2";
			public const string RealizationQuarter2DateTime = "RealizationQuarter2DateTime";
			public const string RealizationQuarter2ByUserID = "RealizationQuarter2ByUserID";
			public const string IsRealizationQuarter3 = "IsRealizationQuarter3";
			public const string RealizationQuarter3DateTime = "RealizationQuarter3DateTime";
			public const string RealizationQuarter3ByUserID = "RealizationQuarter3ByUserID";
			public const string IsRealizationQuarter4 = "IsRealizationQuarter4";
			public const string RealizationQuarter4DateTime = "RealizationQuarter4DateTime";
			public const string RealizationQuarter4ByUserID = "RealizationQuarter4ByUserID";
			public const string IsValidationQuarter1 = "IsValidationQuarter1";
			public const string ValidationQuarter1DateTime = "ValidationQuarter1DateTime";
			public const string ValidationQuarter1ByUserID = "ValidationQuarter1ByUserID";
			public const string IsValidationQuarter2 = "IsValidationQuarter2";
			public const string ValidationQuarter2DateTime = "ValidationQuarter2DateTime";
			public const string ValidationQuarter2ByUserID = "ValidationQuarter2ByUserID";
			public const string ValidationQuarter3DateTime = "ValidationQuarter3DateTime";
			public const string IsValidationQuarter3 = "IsValidationQuarter3";
			public const string ValidationQuarter3ByUserID = "ValidationQuarter3ByUserID";
			public const string IsValidationQuarter4 = "IsValidationQuarter4";
			public const string ValidationQuarter4DateTime = "ValidationQuarter4DateTime";
			public const string ValidationQuarter4ByUserID = "ValidationQuarter4ByUserID";
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
			lock (typeof(PerformancePlanNonJptTxItemMetadata))
			{
				if (PerformancePlanNonJptTxItemMetadata.mapDelegates == null)
				{
					PerformancePlanNonJptTxItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanNonJptTxItemMetadata.meta == null)
				{
					PerformancePlanNonJptTxItemMetadata.meta = new PerformancePlanNonJptTxItemMetadata();
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

				meta.AddTypeMap("TxItemID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("TxID", new esTypeMap("bigint", "System.Int64"));
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
				meta.AddTypeMap("RevisionQuarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RevisionQuarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RevisionQuarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RevisionQuarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RevisionYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationNotesQuarter1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidationQuarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidationQuarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidationQuarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidationQuarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidationYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidationNotesQuarter1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidationNotesQuarter2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidationNotesQuarter3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidationNotesQuarter4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AchievementsYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAdditional", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRealizationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RealizationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRealizationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RealizationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRealizationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RealizationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRealizationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RealizationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsValidationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanNonJptTxItem";
				meta.Destination = "PerformancePlanNonJptTxItem";
				meta.spInsert = "proc_PerformancePlanNonJptTxItemInsert";
				meta.spUpdate = "proc_PerformancePlanNonJptTxItemUpdate";
				meta.spDelete = "proc_PerformancePlanNonJptTxItemDelete";
				meta.spLoadAll = "proc_PerformancePlanNonJptTxItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanNonJptTxItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanNonJptTxItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
