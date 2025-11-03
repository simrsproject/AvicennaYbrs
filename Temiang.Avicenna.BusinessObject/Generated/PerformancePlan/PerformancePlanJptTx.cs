/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/20/2023 1:19:49 PM
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
	abstract public class esPerformancePlanJptTxCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanJptTxCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanJptTxCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptTxQuery query)
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
			this.InitQuery(query as esPerformancePlanJptTxQuery);
		}
		#endregion

		virtual public PerformancePlanJptTx DetachEntity(PerformancePlanJptTx entity)
		{
			return base.DetachEntity(entity) as PerformancePlanJptTx;
		}

		virtual public PerformancePlanJptTx AttachEntity(PerformancePlanJptTx entity)
		{
			return base.AttachEntity(entity) as PerformancePlanJptTx;
		}

		virtual public void Combine(PerformancePlanJptTxCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanJptTx this[int index]
		{
			get
			{
				return base[index] as PerformancePlanJptTx;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanJptTx);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanJptTx : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanJptTxQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanJptTx()
		{
		}

		public esPerformancePlanJptTx(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 performancePlanID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 performancePlanID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 performancePlanID)
		{
			esPerformancePlanJptTxQuery query = this.GetDynamicQuery();
			query.Where(query.PerformancePlanID == performancePlanID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 performancePlanID)
		{
			esParameters parms = new esParameters();
			parms.Add("PerformancePlanID", performancePlanID);
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
						case "PerformancePlanID": this.str.PerformancePlanID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "YearPeriod": this.str.YearPeriod = (string)value; break;
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
						case "RealizationQuarter1": this.str.RealizationQuarter1 = (string)value; break;
						case "RealizationQuarter2": this.str.RealizationQuarter2 = (string)value; break;
						case "RealizationQuarter3": this.str.RealizationQuarter3 = (string)value; break;
						case "RealizationQuarter4": this.str.RealizationQuarter4 = (string)value; break;
						case "RealizationYearTarget": this.str.RealizationYearTarget = (string)value; break;
						case "RealizationNotesQuarter1": this.str.RealizationNotesQuarter1 = (string)value; break;
						case "RealizationNotesQuarter2": this.str.RealizationNotesQuarter2 = (string)value; break;
						case "RealizationNotesQuarter3": this.str.RealizationNotesQuarter3 = (string)value; break;
						case "RealizationNotesQuarter4": this.str.RealizationNotesQuarter4 = (string)value; break;
						case "VerificationQuarter1": this.str.VerificationQuarter1 = (string)value; break;
						case "VerificationQuarter2": this.str.VerificationQuarter2 = (string)value; break;
						case "VerificationQuarter3": this.str.VerificationQuarter3 = (string)value; break;
						case "VerificationQuarter4": this.str.VerificationQuarter4 = (string)value; break;
						case "VerificationYearTarget": this.str.VerificationYearTarget = (string)value; break;
						case "VerificationNotesQuarter1": this.str.VerificationNotesQuarter1 = (string)value; break;
						case "VerificationNotesQuarter2": this.str.VerificationNotesQuarter2 = (string)value; break;
						case "VerificationNotesQuarter3": this.str.VerificationNotesQuarter3 = (string)value; break;
						case "VerificationNotesQuarter4": this.str.VerificationNotesQuarter4 = (string)value; break;
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
						case "IsVerificationQuarter1": this.str.IsVerificationQuarter1 = (string)value; break;
						case "VerificationQuarter1DateTime": this.str.VerificationQuarter1DateTime = (string)value; break;
						case "VerificationQuarter1ByUserID": this.str.VerificationQuarter1ByUserID = (string)value; break;
						case "IsVerificationQuarter2": this.str.IsVerificationQuarter2 = (string)value; break;
						case "VerificationQuarter2DateTime": this.str.VerificationQuarter2DateTime = (string)value; break;
						case "VerificationQuarter2ByUserID": this.str.VerificationQuarter2ByUserID = (string)value; break;
						case "IsVerificationQuarter3": this.str.IsVerificationQuarter3 = (string)value; break;
						case "VerificationQuarter3DateTime": this.str.VerificationQuarter3DateTime = (string)value; break;
						case "VerificationQuarter3ByUserID": this.str.VerificationQuarter3ByUserID = (string)value; break;
						case "IsVerificationQuarter4": this.str.IsVerificationQuarter4 = (string)value; break;
						case "VerificationQuarter4DateTime": this.str.VerificationQuarter4DateTime = (string)value; break;
						case "VerificationQuarter4ByUserID": this.str.VerificationQuarter4ByUserID = (string)value; break;
						case "IsValidationQuarter1": this.str.IsValidationQuarter1 = (string)value; break;
						case "ValidationQuarter1DateTime": this.str.ValidationQuarter1DateTime = (string)value; break;
						case "ValidationQuarter1ByUserID": this.str.ValidationQuarter1ByUserID = (string)value; break;
						case "IsValidationQuarter2": this.str.IsValidationQuarter2 = (string)value; break;
						case "ValidationQuarter2DateTime": this.str.ValidationQuarter2DateTime = (string)value; break;
						case "ValidationQuarter2ByUserID": this.str.ValidationQuarter2ByUserID = (string)value; break;
						case "IsValidationQuarter3": this.str.IsValidationQuarter3 = (string)value; break;
						case "ValidationQuarter3DateTime": this.str.ValidationQuarter3DateTime = (string)value; break;
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
						case "PerformancePlanID":

							if (value == null || value is System.Int64)
								this.PerformancePlanID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
						case "VerificationQuarter1":

							if (value == null || value is System.Decimal)
								this.VerificationQuarter1 = (System.Decimal?)value;
							break;
						case "VerificationQuarter2":

							if (value == null || value is System.Decimal)
								this.VerificationQuarter2 = (System.Decimal?)value;
							break;
						case "VerificationQuarter3":

							if (value == null || value is System.Decimal)
								this.VerificationQuarter3 = (System.Decimal?)value;
							break;
						case "VerificationQuarter4":

							if (value == null || value is System.Decimal)
								this.VerificationQuarter4 = (System.Decimal?)value;
							break;
						case "VerificationYearTarget":

							if (value == null || value is System.Decimal)
								this.VerificationYearTarget = (System.Decimal?)value;
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
						case "IsVerificationQuarter1":

							if (value == null || value is System.Boolean)
								this.IsVerificationQuarter1 = (System.Boolean?)value;
							break;
						case "VerificationQuarter1DateTime":

							if (value == null || value is System.DateTime)
								this.VerificationQuarter1DateTime = (System.DateTime?)value;
							break;
						case "IsVerificationQuarter2":

							if (value == null || value is System.Boolean)
								this.IsVerificationQuarter2 = (System.Boolean?)value;
							break;
						case "VerificationQuarter2DateTime":

							if (value == null || value is System.DateTime)
								this.VerificationQuarter2DateTime = (System.DateTime?)value;
							break;
						case "IsVerificationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsVerificationQuarter3 = (System.Boolean?)value;
							break;
						case "VerificationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.VerificationQuarter3DateTime = (System.DateTime?)value;
							break;
						case "IsVerificationQuarter4":

							if (value == null || value is System.Boolean)
								this.IsVerificationQuarter4 = (System.Boolean?)value;
							break;
						case "VerificationQuarter4DateTime":

							if (value == null || value is System.DateTime)
								this.VerificationQuarter4DateTime = (System.DateTime?)value;
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
						case "IsValidationQuarter3":

							if (value == null || value is System.Boolean)
								this.IsValidationQuarter3 = (System.Boolean?)value;
							break;
						case "ValidationQuarter3DateTime":

							if (value == null || value is System.DateTime)
								this.ValidationQuarter3DateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanJptTx.PerformancePlanID
		/// </summary>
		virtual public System.Int64? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptTxMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptTxMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.YearPeriod
		/// </summary>
		virtual public System.String YearPeriod
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.YearPeriod);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.YearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.SRPerformancePlanIndicator
		/// </summary>
		virtual public System.String SRPerformancePlanIndicator
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanIndicator);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanIndicator, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.PerformancePlanNo
		/// </summary>
		virtual public System.String PerformancePlanNo
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanNo);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanNo, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.SRPerformancePlanDataSource
		/// </summary>
		virtual public System.String SRPerformancePlanDataSource
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanDataSource);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanDataSource, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.SRSectionPerspective
		/// </summary>
		virtual public System.String SRSectionPerspective
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRSectionPerspective);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRSectionPerspective, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Target
		/// </summary>
		virtual public System.String Target
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.Target);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.Target, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.PerformanceIndicators
		/// </summary>
		virtual public System.String PerformanceIndicators
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.PerformanceIndicators);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.PerformanceIndicators, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Measurement
		/// </summary>
		virtual public System.String Measurement
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.Measurement);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.Measurement, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.UnitTargets
		/// </summary>
		virtual public System.String UnitTargets
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.UnitTargets);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.UnitTargets, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.SRAchievementFormula
		/// </summary>
		virtual public System.String SRAchievementFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRAchievementFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRAchievementFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.SRRealizationFormula
		/// </summary>
		virtual public System.String SRRealizationFormula
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRRealizationFormula);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.SRRealizationFormula, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Quarter1
		/// </summary>
		virtual public System.Decimal? Quarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Quarter2
		/// </summary>
		virtual public System.Decimal? Quarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Quarter3
		/// </summary>
		virtual public System.Decimal? Quarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.Quarter4
		/// </summary>
		virtual public System.Decimal? Quarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.Quarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.YearTarget
		/// </summary>
		virtual public System.Decimal? YearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.YearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.YearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter1
		/// </summary>
		virtual public System.Decimal? RealizationQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter2
		/// </summary>
		virtual public System.Decimal? RealizationQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter3
		/// </summary>
		virtual public System.Decimal? RealizationQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter4
		/// </summary>
		virtual public System.Decimal? RealizationQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationYearTarget
		/// </summary>
		virtual public System.Decimal? RealizationYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.RealizationYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationNotesQuarter1
		/// </summary>
		virtual public System.String RealizationNotesQuarter1
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter1);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationNotesQuarter2
		/// </summary>
		virtual public System.String RealizationNotesQuarter2
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter2);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationNotesQuarter3
		/// </summary>
		virtual public System.String RealizationNotesQuarter3
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter3);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationNotesQuarter4
		/// </summary>
		virtual public System.String RealizationNotesQuarter4
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter4);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter1
		/// </summary>
		virtual public System.Decimal? VerificationQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter2
		/// </summary>
		virtual public System.Decimal? VerificationQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter3
		/// </summary>
		virtual public System.Decimal? VerificationQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter4
		/// </summary>
		virtual public System.Decimal? VerificationQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationYearTarget
		/// </summary>
		virtual public System.Decimal? VerificationYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.VerificationYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationNotesQuarter1
		/// </summary>
		virtual public System.String VerificationNotesQuarter1
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter1);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationNotesQuarter2
		/// </summary>
		virtual public System.String VerificationNotesQuarter2
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter2);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationNotesQuarter3
		/// </summary>
		virtual public System.String VerificationNotesQuarter3
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter3);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationNotesQuarter4
		/// </summary>
		virtual public System.String VerificationNotesQuarter4
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter4);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter1
		/// </summary>
		virtual public System.Decimal? ValidationQuarter1
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter2
		/// </summary>
		virtual public System.Decimal? ValidationQuarter2
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter3
		/// </summary>
		virtual public System.Decimal? ValidationQuarter3
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter4
		/// </summary>
		virtual public System.Decimal? ValidationQuarter4
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationYearTarget
		/// </summary>
		virtual public System.Decimal? ValidationYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.ValidationYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationNotesQuarter1
		/// </summary>
		virtual public System.String ValidationNotesQuarter1
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter1);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationNotesQuarter2
		/// </summary>
		virtual public System.String ValidationNotesQuarter2
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter2);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationNotesQuarter3
		/// </summary>
		virtual public System.String ValidationNotesQuarter3
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter3);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationNotesQuarter4
		/// </summary>
		virtual public System.String ValidationNotesQuarter4
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter4);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.AchievementsYearTarget
		/// </summary>
		virtual public System.Decimal? AchievementsYearTarget
		{
			get
			{
				return base.GetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.AchievementsYearTarget);
			}

			set
			{
				base.SetSystemDecimal(PerformancePlanJptTxMetadata.ColumnNames.AchievementsYearTarget, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsAdditional
		/// </summary>
		virtual public System.Boolean? IsAdditional
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsAdditional);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsAdditional, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsRealizationQuarter1
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter1ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsRealizationQuarter2
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter2ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsRealizationQuarter3
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter3ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsRealizationQuarter4
		/// </summary>
		virtual public System.Boolean? IsRealizationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? RealizationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.RealizationQuarter4ByUserID
		/// </summary>
		virtual public System.String RealizationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsVerificationQuarter1
		/// </summary>
		virtual public System.Boolean? IsVerificationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? VerificationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter1ByUserID
		/// </summary>
		virtual public System.String VerificationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsVerificationQuarter2
		/// </summary>
		virtual public System.Boolean? IsVerificationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? VerificationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter2ByUserID
		/// </summary>
		virtual public System.String VerificationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsVerificationQuarter3
		/// </summary>
		virtual public System.Boolean? IsVerificationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? VerificationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter3ByUserID
		/// </summary>
		virtual public System.String VerificationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsVerificationQuarter4
		/// </summary>
		virtual public System.Boolean? IsVerificationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? VerificationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.VerificationQuarter4ByUserID
		/// </summary>
		virtual public System.String VerificationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsValidationQuarter1
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter1
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter1);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter1, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter1DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter1DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter1ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter1ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsValidationQuarter2
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter2
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter2);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter2, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter2DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter2DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter2ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter2ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsValidationQuarter3
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter3
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter3);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter3, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter3DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter3DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter3ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter3ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.IsValidationQuarter4
		/// </summary>
		virtual public System.Boolean? IsValidationQuarter4
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter4);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter4, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter4DateTime
		/// </summary>
		virtual public System.DateTime? ValidationQuarter4DateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4DateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4DateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.ValidationQuarter4ByUserID
		/// </summary>
		virtual public System.String ValidationQuarter4ByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4ByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4ByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJptTx.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanJptTx entity)
			{
				this.entity = entity;
			}
			public System.String PerformancePlanID
			{
				get
				{
					System.Int64? data = entity.PerformancePlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanID = null;
					else entity.PerformancePlanID = Convert.ToInt64(value);
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
			public System.String VerificationQuarter1
			{
				get
				{
					System.Decimal? data = entity.VerificationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter1 = null;
					else entity.VerificationQuarter1 = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationQuarter2
			{
				get
				{
					System.Decimal? data = entity.VerificationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter2 = null;
					else entity.VerificationQuarter2 = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationQuarter3
			{
				get
				{
					System.Decimal? data = entity.VerificationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter3 = null;
					else entity.VerificationQuarter3 = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationQuarter4
			{
				get
				{
					System.Decimal? data = entity.VerificationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter4 = null;
					else entity.VerificationQuarter4 = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationYearTarget
			{
				get
				{
					System.Decimal? data = entity.VerificationYearTarget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationYearTarget = null;
					else entity.VerificationYearTarget = Convert.ToDecimal(value);
				}
			}
			public System.String VerificationNotesQuarter1
			{
				get
				{
					System.String data = entity.VerificationNotesQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNotesQuarter1 = null;
					else entity.VerificationNotesQuarter1 = Convert.ToString(value);
				}
			}
			public System.String VerificationNotesQuarter2
			{
				get
				{
					System.String data = entity.VerificationNotesQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNotesQuarter2 = null;
					else entity.VerificationNotesQuarter2 = Convert.ToString(value);
				}
			}
			public System.String VerificationNotesQuarter3
			{
				get
				{
					System.String data = entity.VerificationNotesQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNotesQuarter3 = null;
					else entity.VerificationNotesQuarter3 = Convert.ToString(value);
				}
			}
			public System.String VerificationNotesQuarter4
			{
				get
				{
					System.String data = entity.VerificationNotesQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNotesQuarter4 = null;
					else entity.VerificationNotesQuarter4 = Convert.ToString(value);
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
			public System.String IsVerificationQuarter1
			{
				get
				{
					System.Boolean? data = entity.IsVerificationQuarter1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerificationQuarter1 = null;
					else entity.IsVerificationQuarter1 = Convert.ToBoolean(value);
				}
			}
			public System.String VerificationQuarter1DateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationQuarter1DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter1DateTime = null;
					else entity.VerificationQuarter1DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationQuarter1ByUserID
			{
				get
				{
					System.String data = entity.VerificationQuarter1ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter1ByUserID = null;
					else entity.VerificationQuarter1ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVerificationQuarter2
			{
				get
				{
					System.Boolean? data = entity.IsVerificationQuarter2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerificationQuarter2 = null;
					else entity.IsVerificationQuarter2 = Convert.ToBoolean(value);
				}
			}
			public System.String VerificationQuarter2DateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationQuarter2DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter2DateTime = null;
					else entity.VerificationQuarter2DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationQuarter2ByUserID
			{
				get
				{
					System.String data = entity.VerificationQuarter2ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter2ByUserID = null;
					else entity.VerificationQuarter2ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVerificationQuarter3
			{
				get
				{
					System.Boolean? data = entity.IsVerificationQuarter3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerificationQuarter3 = null;
					else entity.IsVerificationQuarter3 = Convert.ToBoolean(value);
				}
			}
			public System.String VerificationQuarter3DateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationQuarter3DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter3DateTime = null;
					else entity.VerificationQuarter3DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationQuarter3ByUserID
			{
				get
				{
					System.String data = entity.VerificationQuarter3ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter3ByUserID = null;
					else entity.VerificationQuarter3ByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVerificationQuarter4
			{
				get
				{
					System.Boolean? data = entity.IsVerificationQuarter4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerificationQuarter4 = null;
					else entity.IsVerificationQuarter4 = Convert.ToBoolean(value);
				}
			}
			public System.String VerificationQuarter4DateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationQuarter4DateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter4DateTime = null;
					else entity.VerificationQuarter4DateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationQuarter4ByUserID
			{
				get
				{
					System.String data = entity.VerificationQuarter4ByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationQuarter4ByUserID = null;
					else entity.VerificationQuarter4ByUserID = Convert.ToString(value);
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
			private esPerformancePlanJptTx entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptTxQuery query)
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
				throw new Exception("esPerformancePlanJptTx can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanJptTx : esPerformancePlanJptTx
	{
	}

	[Serializable]
	abstract public class esPerformancePlanJptTxQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptTxMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem YearPeriod
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.YearPeriod, esSystemType.String);
			}
		}

		public esQueryItem SRPerformancePlanIndicator
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanIndicator, esSystemType.String);
			}
		}

		public esQueryItem PerformancePlanNo
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanNo, esSystemType.String);
			}
		}

		public esQueryItem SRPerformancePlanDataSource
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanDataSource, esSystemType.String);
			}
		}

		public esQueryItem SRSectionPerspective
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.SRSectionPerspective, esSystemType.String);
			}
		}

		public esQueryItem Target
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Target, esSystemType.String);
			}
		}

		public esQueryItem PerformanceIndicators
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.PerformanceIndicators, esSystemType.String);
			}
		}

		public esQueryItem Measurement
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Measurement, esSystemType.String);
			}
		}

		public esQueryItem UnitTargets
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.UnitTargets, esSystemType.String);
			}
		}

		public esQueryItem SRAchievementFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.SRAchievementFormula, esSystemType.String);
			}
		}

		public esQueryItem SRRealizationFormula
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.SRRealizationFormula, esSystemType.String);
			}
		}

		public esQueryItem Quarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Quarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Quarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Quarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem Quarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.Quarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem YearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.YearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationNotesQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter1, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter2, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter3, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotesQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter4, esSystemType.String);
			}
		}

		public esQueryItem VerificationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem VerificationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem VerificationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem VerificationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem VerificationYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem VerificationNotesQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter1, esSystemType.String);
			}
		}

		public esQueryItem VerificationNotesQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter2, esSystemType.String);
			}
		}

		public esQueryItem VerificationNotesQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter3, esSystemType.String);
			}
		}

		public esQueryItem VerificationNotesQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter4, esSystemType.String);
			}
		}

		public esQueryItem ValidationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidationNotesQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter1, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter2, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter3, esSystemType.String);
			}
		}

		public esQueryItem ValidationNotesQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter4, esSystemType.String);
			}
		}

		public esQueryItem AchievementsYearTarget
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.AchievementsYearTarget, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAdditional
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsAdditional, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRealizationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerificationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem VerificationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerificationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem VerificationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerificationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem VerificationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerificationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem VerificationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter1
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter1, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter1DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter1ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter2
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter2, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter2DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter2ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter3
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter3, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter3DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter3ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3ByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidationQuarter4
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter4, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidationQuarter4DateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4DateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidationQuarter4ByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4ByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptTxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanJptTxCollection")]
	public partial class PerformancePlanJptTxCollection : esPerformancePlanJptTxCollection, IEnumerable<PerformancePlanJptTx>
	{
		public PerformancePlanJptTxCollection()
		{

		}

		public static implicit operator List<PerformancePlanJptTx>(PerformancePlanJptTxCollection coll)
		{
			List<PerformancePlanJptTx> list = new List<PerformancePlanJptTx>();

			foreach (PerformancePlanJptTx emp in coll)
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
				return PerformancePlanJptTxMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptTxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanJptTx(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanJptTx();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptTxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptTxQuery();
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
		public bool Load(PerformancePlanJptTxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanJptTx AddNew()
		{
			PerformancePlanJptTx entity = base.AddNewEntity() as PerformancePlanJptTx;

			return entity;
		}
		public PerformancePlanJptTx FindByPrimaryKey(Int64 performancePlanID)
		{
			return base.FindByPrimaryKey(performancePlanID) as PerformancePlanJptTx;
		}

		#region IEnumerable< PerformancePlanJptTx> Members

		IEnumerator<PerformancePlanJptTx> IEnumerable<PerformancePlanJptTx>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanJptTx;
			}
		}

		#endregion

		private PerformancePlanJptTxQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanJptTx' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanJptTx ({PerformancePlanID})")]
	[Serializable]
	public partial class PerformancePlanJptTx : esPerformancePlanJptTx
	{
		public PerformancePlanJptTx()
		{
		}

		public PerformancePlanJptTx(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptTxMetadata.Meta();
			}
		}

		override protected esPerformancePlanJptTxQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptTxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptTxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptTxQuery();
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
		public bool Load(PerformancePlanJptTxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanJptTxQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanJptTxQuery : esPerformancePlanJptTxQuery
	{
		public PerformancePlanJptTxQuery()
		{

		}

		public PerformancePlanJptTxQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanJptTxQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanJptTxMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanJptTxMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.PerformancePlanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.YearPeriod, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.YearPeriod;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanIndicator, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.SRPerformancePlanIndicator;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.PerformancePlanNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.PerformancePlanNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.SRPerformancePlanDataSource, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.SRPerformancePlanDataSource;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.SRSectionPerspective, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.SRSectionPerspective;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Target, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Target;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.PerformanceIndicators, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.PerformanceIndicators;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Measurement, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Measurement;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.UnitTargets, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.UnitTargets;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.SRAchievementFormula, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.SRAchievementFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.SRRealizationFormula, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.SRRealizationFormula;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Quarter1, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Quarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Quarter2, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Quarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Quarter3, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Quarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.Quarter4, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.Quarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.YearTarget, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.YearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationYearTarget, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter1, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationNotesQuarter1;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter2, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationNotesQuarter2;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter3, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationNotesQuarter3;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationNotesQuarter4, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationNotesQuarter4;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationYearTarget, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter1, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationNotesQuarter1;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter2, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationNotesQuarter2;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter3, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationNotesQuarter3;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationNotesQuarter4, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationNotesQuarter4;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationYearTarget, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter1, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationNotesQuarter1;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter2, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationNotesQuarter2;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter3, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationNotesQuarter3;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationNotesQuarter4, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationNotesQuarter4;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.AchievementsYearTarget, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.AchievementsYearTarget;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsAdditional, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsAdditional;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.CreatedDateTime, 47, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.CreatedByUserID, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter1, 49, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsRealizationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1DateTime, 50, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter1ByUserID, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter2, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsRealizationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2DateTime, 53, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter2ByUserID, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter3, 55, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsRealizationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3DateTime, 56, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter3ByUserID, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsRealizationQuarter4, 58, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsRealizationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4DateTime, 59, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.RealizationQuarter4ByUserID, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.RealizationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter1, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsVerificationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1DateTime, 62, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter1ByUserID, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter2, 64, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsVerificationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2DateTime, 65, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter2ByUserID, 66, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter3, 67, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsVerificationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3DateTime, 68, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter3ByUserID, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsVerificationQuarter4, 70, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsVerificationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4DateTime, 71, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.VerificationQuarter4ByUserID, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.VerificationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter1, 73, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsValidationQuarter1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1DateTime, 74, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter1DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter1ByUserID, 75, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter1ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter2, 76, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsValidationQuarter2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2DateTime, 77, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter2DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter2ByUserID, 78, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter2ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter3, 79, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsValidationQuarter3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3DateTime, 80, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter3DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter3ByUserID, 81, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter3ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.IsValidationQuarter4, 82, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.IsValidationQuarter4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4DateTime, 83, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter4DateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.ValidationQuarter4ByUserID, 84, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.ValidationQuarter4ByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateDateTime, 85, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptTxMetadata.ColumnNames.LastUpdateByUserID, 86, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptTxMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanJptTxMetadata Meta()
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
			public const string PerformancePlanID = "PerformancePlanID";
			public const string PersonID = "PersonID";
			public const string YearPeriod = "YearPeriod";
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
			public const string RealizationQuarter1 = "RealizationQuarter1";
			public const string RealizationQuarter2 = "RealizationQuarter2";
			public const string RealizationQuarter3 = "RealizationQuarter3";
			public const string RealizationQuarter4 = "RealizationQuarter4";
			public const string RealizationYearTarget = "RealizationYearTarget";
			public const string RealizationNotesQuarter1 = "RealizationNotesQuarter1";
			public const string RealizationNotesQuarter2 = "RealizationNotesQuarter2";
			public const string RealizationNotesQuarter3 = "RealizationNotesQuarter3";
			public const string RealizationNotesQuarter4 = "RealizationNotesQuarter4";
			public const string VerificationQuarter1 = "VerificationQuarter1";
			public const string VerificationQuarter2 = "VerificationQuarter2";
			public const string VerificationQuarter3 = "VerificationQuarter3";
			public const string VerificationQuarter4 = "VerificationQuarter4";
			public const string VerificationYearTarget = "VerificationYearTarget";
			public const string VerificationNotesQuarter1 = "VerificationNotesQuarter1";
			public const string VerificationNotesQuarter2 = "VerificationNotesQuarter2";
			public const string VerificationNotesQuarter3 = "VerificationNotesQuarter3";
			public const string VerificationNotesQuarter4 = "VerificationNotesQuarter4";
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
			public const string IsVerificationQuarter1 = "IsVerificationQuarter1";
			public const string VerificationQuarter1DateTime = "VerificationQuarter1DateTime";
			public const string VerificationQuarter1ByUserID = "VerificationQuarter1ByUserID";
			public const string IsVerificationQuarter2 = "IsVerificationQuarter2";
			public const string VerificationQuarter2DateTime = "VerificationQuarter2DateTime";
			public const string VerificationQuarter2ByUserID = "VerificationQuarter2ByUserID";
			public const string IsVerificationQuarter3 = "IsVerificationQuarter3";
			public const string VerificationQuarter3DateTime = "VerificationQuarter3DateTime";
			public const string VerificationQuarter3ByUserID = "VerificationQuarter3ByUserID";
			public const string IsVerificationQuarter4 = "IsVerificationQuarter4";
			public const string VerificationQuarter4DateTime = "VerificationQuarter4DateTime";
			public const string VerificationQuarter4ByUserID = "VerificationQuarter4ByUserID";
			public const string IsValidationQuarter1 = "IsValidationQuarter1";
			public const string ValidationQuarter1DateTime = "ValidationQuarter1DateTime";
			public const string ValidationQuarter1ByUserID = "ValidationQuarter1ByUserID";
			public const string IsValidationQuarter2 = "IsValidationQuarter2";
			public const string ValidationQuarter2DateTime = "ValidationQuarter2DateTime";
			public const string ValidationQuarter2ByUserID = "ValidationQuarter2ByUserID";
			public const string IsValidationQuarter3 = "IsValidationQuarter3";
			public const string ValidationQuarter3DateTime = "ValidationQuarter3DateTime";
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
			public const string PerformancePlanID = "PerformancePlanID";
			public const string PersonID = "PersonID";
			public const string YearPeriod = "YearPeriod";
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
			public const string RealizationQuarter1 = "RealizationQuarter1";
			public const string RealizationQuarter2 = "RealizationQuarter2";
			public const string RealizationQuarter3 = "RealizationQuarter3";
			public const string RealizationQuarter4 = "RealizationQuarter4";
			public const string RealizationYearTarget = "RealizationYearTarget";
			public const string RealizationNotesQuarter1 = "RealizationNotesQuarter1";
			public const string RealizationNotesQuarter2 = "RealizationNotesQuarter2";
			public const string RealizationNotesQuarter3 = "RealizationNotesQuarter3";
			public const string RealizationNotesQuarter4 = "RealizationNotesQuarter4";
			public const string VerificationQuarter1 = "VerificationQuarter1";
			public const string VerificationQuarter2 = "VerificationQuarter2";
			public const string VerificationQuarter3 = "VerificationQuarter3";
			public const string VerificationQuarter4 = "VerificationQuarter4";
			public const string VerificationYearTarget = "VerificationYearTarget";
			public const string VerificationNotesQuarter1 = "VerificationNotesQuarter1";
			public const string VerificationNotesQuarter2 = "VerificationNotesQuarter2";
			public const string VerificationNotesQuarter3 = "VerificationNotesQuarter3";
			public const string VerificationNotesQuarter4 = "VerificationNotesQuarter4";
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
			public const string IsVerificationQuarter1 = "IsVerificationQuarter1";
			public const string VerificationQuarter1DateTime = "VerificationQuarter1DateTime";
			public const string VerificationQuarter1ByUserID = "VerificationQuarter1ByUserID";
			public const string IsVerificationQuarter2 = "IsVerificationQuarter2";
			public const string VerificationQuarter2DateTime = "VerificationQuarter2DateTime";
			public const string VerificationQuarter2ByUserID = "VerificationQuarter2ByUserID";
			public const string IsVerificationQuarter3 = "IsVerificationQuarter3";
			public const string VerificationQuarter3DateTime = "VerificationQuarter3DateTime";
			public const string VerificationQuarter3ByUserID = "VerificationQuarter3ByUserID";
			public const string IsVerificationQuarter4 = "IsVerificationQuarter4";
			public const string VerificationQuarter4DateTime = "VerificationQuarter4DateTime";
			public const string VerificationQuarter4ByUserID = "VerificationQuarter4ByUserID";
			public const string IsValidationQuarter1 = "IsValidationQuarter1";
			public const string ValidationQuarter1DateTime = "ValidationQuarter1DateTime";
			public const string ValidationQuarter1ByUserID = "ValidationQuarter1ByUserID";
			public const string IsValidationQuarter2 = "IsValidationQuarter2";
			public const string ValidationQuarter2DateTime = "ValidationQuarter2DateTime";
			public const string ValidationQuarter2ByUserID = "ValidationQuarter2ByUserID";
			public const string IsValidationQuarter3 = "IsValidationQuarter3";
			public const string ValidationQuarter3DateTime = "ValidationQuarter3DateTime";
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
			lock (typeof(PerformancePlanJptTxMetadata))
			{
				if (PerformancePlanJptTxMetadata.mapDelegates == null)
				{
					PerformancePlanJptTxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanJptTxMetadata.meta == null)
				{
					PerformancePlanJptTxMetadata.meta = new PerformancePlanJptTxMetadata();
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

				meta.AddTypeMap("PerformancePlanID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriod", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("RealizationQuarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationQuarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationNotesQuarter1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotesQuarter4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationQuarter1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationQuarter2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationQuarter3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationQuarter4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationYearTarget", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VerificationNotesQuarter1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNotesQuarter2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNotesQuarter3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNotesQuarter4", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("IsVerificationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerificationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerificationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerificationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter1DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter1ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter2DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter2ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter3DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter3ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidationQuarter4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidationQuarter4DateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidationQuarter4ByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanJptTx";
				meta.Destination = "PerformancePlanJptTx";
				meta.spInsert = "proc_PerformancePlanJptTxInsert";
				meta.spUpdate = "proc_PerformancePlanJptTxUpdate";
				meta.spDelete = "proc_PerformancePlanJptTxDelete";
				meta.spLoadAll = "proc_PerformancePlanJptTxLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanJptTxLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanJptTxMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
