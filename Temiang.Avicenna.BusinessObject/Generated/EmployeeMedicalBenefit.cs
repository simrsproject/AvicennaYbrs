/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:15 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esEmployeeMedicalBenefitCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeMedicalBenefitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeMedicalBenefitCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalBenefitQuery query)
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
			this.InitQuery(query as esEmployeeMedicalBenefitQuery);
		}
		#endregion
		
		virtual public EmployeeMedicalBenefit DetachEntity(EmployeeMedicalBenefit entity)
		{
			return base.DetachEntity(entity) as EmployeeMedicalBenefit;
		}
		
		virtual public EmployeeMedicalBenefit AttachEntity(EmployeeMedicalBenefit entity)
		{
			return base.AttachEntity(entity) as EmployeeMedicalBenefit;
		}
		
		virtual public void Combine(EmployeeMedicalBenefitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeMedicalBenefit this[int index]
		{
			get
			{
				return base[index] as EmployeeMedicalBenefit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeMedicalBenefit);
		}
	}



	[Serializable]
	abstract public class esEmployeeMedicalBenefit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeMedicalBenefitQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeMedicalBenefit()
		{

		}

		public esEmployeeMedicalBenefit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 employeeMedicalBenefitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMedicalBenefitID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMedicalBenefitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 employeeMedicalBenefitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMedicalBenefitID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMedicalBenefitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 employeeMedicalBenefitID)
		{
			esEmployeeMedicalBenefitQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeMedicalBenefitID == employeeMedicalBenefitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 employeeMedicalBenefitID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeMedicalBenefitID",employeeMedicalBenefitID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "EmployeeMedicalBenefitID": this.str.EmployeeMedicalBenefitID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "MedicalBenefitInfoID": this.str.MedicalBenefitInfoID = (string)value; break;							
						case "YearPeriodID": this.str.YearPeriodID = (string)value; break;							
						case "IsUnlimited": this.str.IsUnlimited = (string)value; break;							
						case "SRMedicalPaidRules": this.str.SRMedicalPaidRules = (string)value; break;							
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "EmployeeBenefit": this.str.EmployeeBenefit = (string)value; break;							
						case "EmployeeUsedAmount": this.str.EmployeeUsedAmount = (string)value; break;							
						case "EmployeeAdjustmentAmount": this.str.EmployeeAdjustmentAmount = (string)value; break;							
						case "EmployeeUnusedAmount": this.str.EmployeeUnusedAmount = (string)value; break;							
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;							
						case "DependentGuarantorID": this.str.DependentGuarantorID = (string)value; break;							
						case "DependentBenefit": this.str.DependentBenefit = (string)value; break;							
						case "DependentUsedAmount": this.str.DependentUsedAmount = (string)value; break;							
						case "DependentAdjustmentAmount": this.str.DependentAdjustmentAmount = (string)value; break;							
						case "DependentUnusedAmount": this.str.DependentUnusedAmount = (string)value; break;							
						case "TotalBenefit": this.str.TotalBenefit = (string)value; break;							
						case "TotalUsedAmount": this.str.TotalUsedAmount = (string)value; break;							
						case "TotalAdjustmentAmount": this.str.TotalAdjustmentAmount = (string)value; break;							
						case "TotalUnusedAmount": this.str.TotalUnusedAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeMedicalBenefitID":
						
							if (value == null || value is System.Int64)
								this.EmployeeMedicalBenefitID = (System.Int64?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "MedicalBenefitInfoID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitInfoID = (System.Int32?)value;
							break;
						
						case "YearPeriodID":
						
							if (value == null || value is System.Int32)
								this.YearPeriodID = (System.Int32?)value;
							break;
						
						case "IsUnlimited":
						
							if (value == null || value is System.Boolean)
								this.IsUnlimited = (System.Boolean?)value;
							break;
						
						case "EmployeeBenefit":
						
							if (value == null || value is System.Decimal)
								this.EmployeeBenefit = (System.Decimal?)value;
							break;
						
						case "EmployeeUsedAmount":
						
							if (value == null || value is System.Decimal)
								this.EmployeeUsedAmount = (System.Decimal?)value;
							break;
						
						case "EmployeeAdjustmentAmount":
						
							if (value == null || value is System.Decimal)
								this.EmployeeAdjustmentAmount = (System.Decimal?)value;
							break;
						
						case "EmployeeUnusedAmount":
						
							if (value == null || value is System.Decimal)
								this.EmployeeUnusedAmount = (System.Decimal?)value;
							break;
						
						case "NoOfDependent":
						
							if (value == null || value is System.Int32)
								this.NoOfDependent = (System.Int32?)value;
							break;
						
						case "DependentBenefit":
						
							if (value == null || value is System.Decimal)
								this.DependentBenefit = (System.Decimal?)value;
							break;
						
						case "DependentUsedAmount":
						
							if (value == null || value is System.Decimal)
								this.DependentUsedAmount = (System.Decimal?)value;
							break;
						
						case "DependentAdjustmentAmount":
						
							if (value == null || value is System.Decimal)
								this.DependentAdjustmentAmount = (System.Decimal?)value;
							break;
						
						case "DependentUnusedAmount":
						
							if (value == null || value is System.Decimal)
								this.DependentUnusedAmount = (System.Decimal?)value;
							break;
						
						case "TotalBenefit":
						
							if (value == null || value is System.Decimal)
								this.TotalBenefit = (System.Decimal?)value;
							break;
						
						case "TotalUsedAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalUsedAmount = (System.Decimal?)value;
							break;
						
						case "TotalAdjustmentAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalAdjustmentAmount = (System.Decimal?)value;
							break;
						
						case "TotalUnusedAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalUnusedAmount = (System.Decimal?)value;
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
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}
		
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.EmployeeMedicalBenefitID
		/// </summary>
		virtual public System.Int64? EmployeeMedicalBenefitID
		{
			get
			{
				return base.GetSystemInt64(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeMedicalBenefitID);
			}
			
			set
			{
				base.SetSystemInt64(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeMedicalBenefitID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.MedicalBenefitInfoID
		/// </summary>
		virtual public System.Int32? MedicalBenefitInfoID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.MedicalBenefitInfoID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.MedicalBenefitInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.YearPeriodID
		/// </summary>
		virtual public System.Int32? YearPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.YearPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.YearPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.IsUnlimited
		/// </summary>
		virtual public System.Boolean? IsUnlimited
		{
			get
			{
				return base.GetSystemBoolean(EmployeeMedicalBenefitMetadata.ColumnNames.IsUnlimited);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeMedicalBenefitMetadata.ColumnNames.IsUnlimited, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.SRMedicalPaidRules
		/// </summary>
		virtual public System.String SRMedicalPaidRules
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.SRMedicalPaidRules);
			}
			
			set
			{
				base.SetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.SRMedicalPaidRules, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.EmployeeBenefit
		/// </summary>
		virtual public System.Decimal? EmployeeBenefit
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeBenefit);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeBenefit, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.EmployeeUsedAmount
		/// </summary>
		virtual public System.Decimal? EmployeeUsedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUsedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUsedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.EmployeeAdjustmentAmount
		/// </summary>
		virtual public System.Decimal? EmployeeAdjustmentAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeAdjustmentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeAdjustmentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.EmployeeUnusedAmount
		/// </summary>
		virtual public System.Decimal? EmployeeUnusedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUnusedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUnusedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.NoOfDependent);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMedicalBenefitMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.DependentGuarantorID
		/// </summary>
		virtual public System.String DependentGuarantorID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.DependentGuarantorID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.DependentGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.DependentBenefit
		/// </summary>
		virtual public System.Decimal? DependentBenefit
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentBenefit);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentBenefit, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.DependentUsedAmount
		/// </summary>
		virtual public System.Decimal? DependentUsedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUsedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUsedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.DependentAdjustmentAmount
		/// </summary>
		virtual public System.Decimal? DependentAdjustmentAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentAdjustmentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentAdjustmentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.DependentUnusedAmount
		/// </summary>
		virtual public System.Decimal? DependentUnusedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUnusedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUnusedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.TotalBenefit
		/// </summary>
		virtual public System.Decimal? TotalBenefit
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalBenefit);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalBenefit, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.TotalUsedAmount
		/// </summary>
		virtual public System.Decimal? TotalUsedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUsedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUsedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.TotalAdjustmentAmount
		/// </summary>
		virtual public System.Decimal? TotalAdjustmentAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalAdjustmentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalAdjustmentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.TotalUnusedAmount
		/// </summary>
		virtual public System.Decimal? TotalUnusedAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUnusedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUnusedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMedicalBenefit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


		[BrowsableAttribute( false )]
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
			public esStrings(esEmployeeMedicalBenefit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeMedicalBenefitID
			{
				get
				{
					System.Int64? data = entity.EmployeeMedicalBenefitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeMedicalBenefitID = null;
					else entity.EmployeeMedicalBenefitID = Convert.ToInt64(value);
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
				
			public System.String MedicalBenefitInfoID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitInfoID = null;
					else entity.MedicalBenefitInfoID = Convert.ToInt32(value);
				}
			}
				
			public System.String YearPeriodID
			{
				get
				{
					System.Int32? data = entity.YearPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriodID = null;
					else entity.YearPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsUnlimited
			{
				get
				{
					System.Boolean? data = entity.IsUnlimited;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnlimited = null;
					else entity.IsUnlimited = Convert.ToBoolean(value);
				}
			}
				
			public System.String SRMedicalPaidRules
			{
				get
				{
					System.String data = entity.SRMedicalPaidRules;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalPaidRules = null;
					else entity.SRMedicalPaidRules = Convert.ToString(value);
				}
			}
				
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeBenefit
			{
				get
				{
					System.Decimal? data = entity.EmployeeBenefit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeBenefit = null;
					else entity.EmployeeBenefit = Convert.ToDecimal(value);
				}
			}
				
			public System.String EmployeeUsedAmount
			{
				get
				{
					System.Decimal? data = entity.EmployeeUsedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeUsedAmount = null;
					else entity.EmployeeUsedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String EmployeeAdjustmentAmount
			{
				get
				{
					System.Decimal? data = entity.EmployeeAdjustmentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeAdjustmentAmount = null;
					else entity.EmployeeAdjustmentAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String EmployeeUnusedAmount
			{
				get
				{
					System.Decimal? data = entity.EmployeeUnusedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeUnusedAmount = null;
					else entity.EmployeeUnusedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String NoOfDependent
			{
				get
				{
					System.Int32? data = entity.NoOfDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoOfDependent = null;
					else entity.NoOfDependent = Convert.ToInt32(value);
				}
			}
				
			public System.String DependentGuarantorID
			{
				get
				{
					System.String data = entity.DependentGuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentGuarantorID = null;
					else entity.DependentGuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String DependentBenefit
			{
				get
				{
					System.Decimal? data = entity.DependentBenefit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentBenefit = null;
					else entity.DependentBenefit = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentUsedAmount
			{
				get
				{
					System.Decimal? data = entity.DependentUsedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentUsedAmount = null;
					else entity.DependentUsedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentAdjustmentAmount
			{
				get
				{
					System.Decimal? data = entity.DependentAdjustmentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentAdjustmentAmount = null;
					else entity.DependentAdjustmentAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentUnusedAmount
			{
				get
				{
					System.Decimal? data = entity.DependentUnusedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentUnusedAmount = null;
					else entity.DependentUnusedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalBenefit
			{
				get
				{
					System.Decimal? data = entity.TotalBenefit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalBenefit = null;
					else entity.TotalBenefit = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalUsedAmount
			{
				get
				{
					System.Decimal? data = entity.TotalUsedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalUsedAmount = null;
					else entity.TotalUsedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalAdjustmentAmount
			{
				get
				{
					System.Decimal? data = entity.TotalAdjustmentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAdjustmentAmount = null;
					else entity.TotalAdjustmentAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalUnusedAmount
			{
				get
				{
					System.Decimal? data = entity.TotalUnusedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalUnusedAmount = null;
					else entity.TotalUnusedAmount = Convert.ToDecimal(value);
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
			

			private esEmployeeMedicalBenefit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalBenefitQuery query)
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
				throw new Exception("esEmployeeMedicalBenefit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeMedicalBenefit : esEmployeeMedicalBenefit
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esEmployeeMedicalBenefitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalBenefitMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeMedicalBenefitID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeMedicalBenefitID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MedicalBenefitInfoID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.MedicalBenefitInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem YearPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.YearPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsUnlimited
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.IsUnlimited, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SRMedicalPaidRules
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.SRMedicalPaidRules, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeBenefit
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeBenefit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmployeeUsedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUsedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmployeeAdjustmentAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeAdjustmentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmployeeUnusedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUnusedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DependentGuarantorID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.DependentGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem DependentBenefit
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.DependentBenefit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentUsedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.DependentUsedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentAdjustmentAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.DependentAdjustmentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentUnusedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.DependentUnusedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalBenefit
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.TotalBenefit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalUsedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.TotalUsedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalAdjustmentAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.TotalAdjustmentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalUnusedAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.TotalUnusedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeMedicalBenefitCollection")]
	public partial class EmployeeMedicalBenefitCollection : esEmployeeMedicalBenefitCollection, IEnumerable<EmployeeMedicalBenefit>
	{
		public EmployeeMedicalBenefitCollection()
		{

		}
		
		public static implicit operator List<EmployeeMedicalBenefit>(EmployeeMedicalBenefitCollection coll)
		{
			List<EmployeeMedicalBenefit> list = new List<EmployeeMedicalBenefit>();
			
			foreach (EmployeeMedicalBenefit emp in coll)
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
				return  EmployeeMedicalBenefitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalBenefitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeMedicalBenefit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeMedicalBenefit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeMedicalBenefitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalBenefitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeMedicalBenefitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeMedicalBenefit AddNew()
		{
			EmployeeMedicalBenefit entity = base.AddNewEntity() as EmployeeMedicalBenefit;
			
			return entity;
		}

		public EmployeeMedicalBenefit FindByPrimaryKey(System.Int64 employeeMedicalBenefitID)
		{
			return base.FindByPrimaryKey(employeeMedicalBenefitID) as EmployeeMedicalBenefit;
		}


		#region IEnumerable<EmployeeMedicalBenefit> Members

		IEnumerator<EmployeeMedicalBenefit> IEnumerable<EmployeeMedicalBenefit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeMedicalBenefit;
			}
		}

		#endregion
		
		private EmployeeMedicalBenefitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeMedicalBenefit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeMedicalBenefit ({EmployeeMedicalBenefitID})")]
	[Serializable]
	public partial class EmployeeMedicalBenefit : esEmployeeMedicalBenefit
	{
		public EmployeeMedicalBenefit()
		{

		}
	
		public EmployeeMedicalBenefit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalBenefitMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeMedicalBenefitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalBenefitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeMedicalBenefitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalBenefitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeMedicalBenefitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeMedicalBenefitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeMedicalBenefitQuery : esEmployeeMedicalBenefitQuery
	{
		public EmployeeMedicalBenefitQuery()
		{

		}		
		
		public EmployeeMedicalBenefitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeMedicalBenefitQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeMedicalBenefitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeMedicalBenefitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeMedicalBenefitID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.EmployeeMedicalBenefitID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.MedicalBenefitInfoID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.MedicalBenefitInfoID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.YearPeriodID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.YearPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.IsUnlimited, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.IsUnlimited;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.SRMedicalPaidRules, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.SRMedicalPaidRules;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.GuarantorID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeBenefit, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.EmployeeBenefit;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUsedAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.EmployeeUsedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeAdjustmentAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.EmployeeAdjustmentAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.EmployeeUnusedAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.EmployeeUnusedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.NoOfDependent, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.DependentGuarantorID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.DependentGuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.DependentBenefit, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.DependentBenefit;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUsedAmount, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.DependentUsedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.DependentAdjustmentAmount, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.DependentAdjustmentAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.DependentUnusedAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.DependentUnusedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.TotalBenefit, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.TotalBenefit;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUsedAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.TotalUsedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.TotalAdjustmentAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.TotalAdjustmentAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.TotalUnusedAmount, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.TotalUnusedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMedicalBenefitMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalBenefitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeMedicalBenefitMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string EmployeeMedicalBenefitID = "EmployeeMedicalBenefitID";
			 public const string PersonID = "PersonID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string IsUnlimited = "IsUnlimited";
			 public const string SRMedicalPaidRules = "SRMedicalPaidRules";
			 public const string GuarantorID = "GuarantorID";
			 public const string EmployeeBenefit = "EmployeeBenefit";
			 public const string EmployeeUsedAmount = "EmployeeUsedAmount";
			 public const string EmployeeAdjustmentAmount = "EmployeeAdjustmentAmount";
			 public const string EmployeeUnusedAmount = "EmployeeUnusedAmount";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string DependentGuarantorID = "DependentGuarantorID";
			 public const string DependentBenefit = "DependentBenefit";
			 public const string DependentUsedAmount = "DependentUsedAmount";
			 public const string DependentAdjustmentAmount = "DependentAdjustmentAmount";
			 public const string DependentUnusedAmount = "DependentUnusedAmount";
			 public const string TotalBenefit = "TotalBenefit";
			 public const string TotalUsedAmount = "TotalUsedAmount";
			 public const string TotalAdjustmentAmount = "TotalAdjustmentAmount";
			 public const string TotalUnusedAmount = "TotalUnusedAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeMedicalBenefitID = "EmployeeMedicalBenefitID";
			 public const string PersonID = "PersonID";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string IsUnlimited = "IsUnlimited";
			 public const string SRMedicalPaidRules = "SRMedicalPaidRules";
			 public const string GuarantorID = "GuarantorID";
			 public const string EmployeeBenefit = "EmployeeBenefit";
			 public const string EmployeeUsedAmount = "EmployeeUsedAmount";
			 public const string EmployeeAdjustmentAmount = "EmployeeAdjustmentAmount";
			 public const string EmployeeUnusedAmount = "EmployeeUnusedAmount";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string DependentGuarantorID = "DependentGuarantorID";
			 public const string DependentBenefit = "DependentBenefit";
			 public const string DependentUsedAmount = "DependentUsedAmount";
			 public const string DependentAdjustmentAmount = "DependentAdjustmentAmount";
			 public const string DependentUnusedAmount = "DependentUnusedAmount";
			 public const string TotalBenefit = "TotalBenefit";
			 public const string TotalUsedAmount = "TotalUsedAmount";
			 public const string TotalAdjustmentAmount = "TotalAdjustmentAmount";
			 public const string TotalUnusedAmount = "TotalUnusedAmount";
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
			lock (typeof(EmployeeMedicalBenefitMetadata))
			{
				if(EmployeeMedicalBenefitMetadata.mapDelegates == null)
				{
					EmployeeMedicalBenefitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeMedicalBenefitMetadata.meta == null)
				{
					EmployeeMedicalBenefitMetadata.meta = new EmployeeMedicalBenefitMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				

				meta.AddTypeMap("EmployeeMedicalBenefitID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicalBenefitInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsUnlimited", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRMedicalPaidRules", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeBenefit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("EmployeeUsedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("EmployeeAdjustmentAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("EmployeeUnusedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DependentGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DependentBenefit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentUsedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentAdjustmentAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentUnusedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalBenefit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalUsedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalAdjustmentAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalUnusedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeMedicalBenefit";
				meta.Destination = "EmployeeMedicalBenefit";
				
				meta.spInsert = "proc_EmployeeMedicalBenefitInsert";				
				meta.spUpdate = "proc_EmployeeMedicalBenefitUpdate";		
				meta.spDelete = "proc_EmployeeMedicalBenefitDelete";
				meta.spLoadAll = "proc_EmployeeMedicalBenefitLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeMedicalBenefitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeMedicalBenefitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
