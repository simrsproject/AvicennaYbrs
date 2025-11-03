/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/8/2012 11:19:01 AM
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
	abstract public class esMedicalBenefitInfoCollection : esEntityCollectionWAuditLog
	{
		public esMedicalBenefitInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalBenefitInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalBenefitInfoQuery query)
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
			this.InitQuery(query as esMedicalBenefitInfoQuery);
		}
		#endregion
		
		virtual public MedicalBenefitInfo DetachEntity(MedicalBenefitInfo entity)
		{
			return base.DetachEntity(entity) as MedicalBenefitInfo;
		}
		
		virtual public MedicalBenefitInfo AttachEntity(MedicalBenefitInfo entity)
		{
			return base.AttachEntity(entity) as MedicalBenefitInfo;
		}
		
		virtual public void Combine(MedicalBenefitInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalBenefitInfo this[int index]
		{
			get
			{
				return base[index] as MedicalBenefitInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalBenefitInfo);
		}
	}



	[Serializable]
	abstract public class esMedicalBenefitInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalBenefitInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalBenefitInfo()
		{

		}

		public esMedicalBenefitInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 medicalBenefitInfoID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitInfoID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 medicalBenefitInfoID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitInfoID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 medicalBenefitInfoID)
		{
			esMedicalBenefitInfoQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalBenefitInfoID == medicalBenefitInfoID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 medicalBenefitInfoID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalBenefitInfoID",medicalBenefitInfoID);
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
						case "MedicalBenefitInfoID": this.str.MedicalBenefitInfoID = (string)value; break;							
						case "MedicalBenefitName": this.str.MedicalBenefitName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "MedicalPercentPaid": this.str.MedicalPercentPaid = (string)value; break;							
						case "SRMedicalPaidRules": this.str.SRMedicalPaidRules = (string)value; break;							
						case "MedicalPaidMoney": this.str.MedicalPaidMoney = (string)value; break;							
						case "MedicalPaidFaktor": this.str.MedicalPaidFaktor = (string)value; break;							
						case "IsEmploymentType": this.str.IsEmploymentType = (string)value; break;							
						case "IsSpecificEmployee": this.str.IsSpecificEmployee = (string)value; break;							
						case "IsEmployeeStatus": this.str.IsEmployeeStatus = (string)value; break;							
						case "IsPositionGrade": this.str.IsPositionGrade = (string)value; break;							
						case "IsAge": this.str.IsAge = (string)value; break;							
						case "IsRemunerationType": this.str.IsRemunerationType = (string)value; break;							
						case "IsEmployeeGrade": this.str.IsEmployeeGrade = (string)value; break;							
						case "IsServiceYear": this.str.IsServiceYear = (string)value; break;							
						case "SettlementRuleID": this.str.SettlementRuleID = (string)value; break;							
						case "IsUnlimitedFrequency": this.str.IsUnlimitedFrequency = (string)value; break;							
						case "FrequencyValue": this.str.FrequencyValue = (string)value; break;							
						case "SRUnusedBalance": this.str.SRUnusedBalance = (string)value; break;							
						case "MaxCarryOver": this.str.MaxCarryOver = (string)value; break;							
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;							
						case "DependentGuarantorID": this.str.DependentGuarantorID = (string)value; break;							
						case "DependentPercentPaid": this.str.DependentPercentPaid = (string)value; break;							
						case "SRDependentPaidRules": this.str.SRDependentPaidRules = (string)value; break;							
						case "DependentFromEmployeeBenefit": this.str.DependentFromEmployeeBenefit = (string)value; break;							
						case "DependentAmountValue": this.str.DependentAmountValue = (string)value; break;							
						case "DependentAmountFaktor": this.str.DependentAmountFaktor = (string)value; break;							
						case "IsUnlimitedFrequencyDependent": this.str.IsUnlimitedFrequencyDependent = (string)value; break;							
						case "FrequencyValueDependent": this.str.FrequencyValueDependent = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicalBenefitInfoID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitInfoID = (System.Int32?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						
						case "MedicalPercentPaid":
						
							if (value == null || value is System.Int32)
								this.MedicalPercentPaid = (System.Int32?)value;
							break;
						
						case "MedicalPaidMoney":
						
							if (value == null || value is System.Decimal)
								this.MedicalPaidMoney = (System.Decimal?)value;
							break;
						
						case "MedicalPaidFaktor":
						
							if (value == null || value is System.Int32)
								this.MedicalPaidFaktor = (System.Int32?)value;
							break;
						
						case "IsEmploymentType":
						
							if (value == null || value is System.Boolean)
								this.IsEmploymentType = (System.Boolean?)value;
							break;
						
						case "IsSpecificEmployee":
						
							if (value == null || value is System.Boolean)
								this.IsSpecificEmployee = (System.Boolean?)value;
							break;
						
						case "IsEmployeeStatus":
						
							if (value == null || value is System.Boolean)
								this.IsEmployeeStatus = (System.Boolean?)value;
							break;
						
						case "IsPositionGrade":
						
							if (value == null || value is System.Boolean)
								this.IsPositionGrade = (System.Boolean?)value;
							break;
						
						case "IsAge":
						
							if (value == null || value is System.Boolean)
								this.IsAge = (System.Boolean?)value;
							break;
						
						case "IsRemunerationType":
						
							if (value == null || value is System.Boolean)
								this.IsRemunerationType = (System.Boolean?)value;
							break;
						
						case "IsEmployeeGrade":
						
							if (value == null || value is System.Boolean)
								this.IsEmployeeGrade = (System.Boolean?)value;
							break;
						
						case "IsServiceYear":
						
							if (value == null || value is System.Boolean)
								this.IsServiceYear = (System.Boolean?)value;
							break;
						
						case "SettlementRuleID":
						
							if (value == null || value is System.Int32)
								this.SettlementRuleID = (System.Int32?)value;
							break;
						
						case "IsUnlimitedFrequency":
						
							if (value == null || value is System.Boolean)
								this.IsUnlimitedFrequency = (System.Boolean?)value;
							break;
						
						case "FrequencyValue":
						
							if (value == null || value is System.Int32)
								this.FrequencyValue = (System.Int32?)value;
							break;
						
						case "MaxCarryOver":
						
							if (value == null || value is System.Decimal)
								this.MaxCarryOver = (System.Decimal?)value;
							break;
						
						case "NoOfDependent":
						
							if (value == null || value is System.Int32)
								this.NoOfDependent = (System.Int32?)value;
							break;
						
						case "DependentPercentPaid":
						
							if (value == null || value is System.Int32)
								this.DependentPercentPaid = (System.Int32?)value;
							break;
						
						case "DependentFromEmployeeBenefit":
						
							if (value == null || value is System.Int32)
								this.DependentFromEmployeeBenefit = (System.Int32?)value;
							break;
						
						case "DependentAmountValue":
						
							if (value == null || value is System.Decimal)
								this.DependentAmountValue = (System.Decimal?)value;
							break;
						
						case "DependentAmountFaktor":
						
							if (value == null || value is System.Int32)
								this.DependentAmountFaktor = (System.Int32?)value;
							break;
						
						case "IsUnlimitedFrequencyDependent":
						
							if (value == null || value is System.Boolean)
								this.IsUnlimitedFrequencyDependent = (System.Boolean?)value;
							break;
						
						case "FrequencyValueDependent":
						
							if (value == null || value is System.Int32)
								this.FrequencyValueDependent = (System.Int32?)value;
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
		/// Maps to MedicalBenefitInfo.MedicalBenefitInfoID
		/// </summary>
		virtual public System.Int32? MedicalBenefitInfoID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitInfoID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.MedicalBenefitName
		/// </summary>
		virtual public System.String MedicalBenefitName
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitName);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitName, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.MedicalPercentPaid
		/// </summary>
		virtual public System.Int32? MedicalPercentPaid
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalPercentPaid);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalPercentPaid, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.SRMedicalPaidRules
		/// </summary>
		virtual public System.String SRMedicalPaidRules
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRMedicalPaidRules);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRMedicalPaidRules, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.MedicalPaidMoney
		/// </summary>
		virtual public System.Decimal? MedicalPaidMoney
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidMoney);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidMoney, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.MedicalPaidFaktor
		/// </summary>
		virtual public System.Int32? MedicalPaidFaktor
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidFaktor);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidFaktor, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsEmploymentType
		/// </summary>
		virtual public System.Boolean? IsEmploymentType
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmploymentType);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmploymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsSpecificEmployee
		/// </summary>
		virtual public System.Boolean? IsSpecificEmployee
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsSpecificEmployee);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsSpecificEmployee, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsEmployeeStatus
		/// </summary>
		virtual public System.Boolean? IsEmployeeStatus
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeStatus);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsPositionGrade
		/// </summary>
		virtual public System.Boolean? IsPositionGrade
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsPositionGrade);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsPositionGrade, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsAge
		/// </summary>
		virtual public System.Boolean? IsAge
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsAge);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsAge, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsRemunerationType
		/// </summary>
		virtual public System.Boolean? IsRemunerationType
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsRemunerationType);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsRemunerationType, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsEmployeeGrade
		/// </summary>
		virtual public System.Boolean? IsEmployeeGrade
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeGrade);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeGrade, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsServiceYear
		/// </summary>
		virtual public System.Boolean? IsServiceYear
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsServiceYear);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsServiceYear, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.SettlementRuleID
		/// </summary>
		virtual public System.Int32? SettlementRuleID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.SettlementRuleID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.SettlementRuleID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsUnlimitedFrequency
		/// </summary>
		virtual public System.Boolean? IsUnlimitedFrequency
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequency);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequency, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.FrequencyValue
		/// </summary>
		virtual public System.Int32? FrequencyValue
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValue);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValue, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.SRUnusedBalance
		/// </summary>
		virtual public System.String SRUnusedBalance
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRUnusedBalance);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRUnusedBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.MaxCarryOver
		/// </summary>
		virtual public System.Decimal? MaxCarryOver
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.MaxCarryOver);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.MaxCarryOver, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.NoOfDependent);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.DependentGuarantorID
		/// </summary>
		virtual public System.String DependentGuarantorID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.DependentGuarantorID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.DependentGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.DependentPercentPaid
		/// </summary>
		virtual public System.Int32? DependentPercentPaid
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentPercentPaid);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentPercentPaid, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.SRDependentPaidRules
		/// </summary>
		virtual public System.String SRDependentPaidRules
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRDependentPaidRules);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.SRDependentPaidRules, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.DependentFromEmployeeBenefit
		/// </summary>
		virtual public System.Int32? DependentFromEmployeeBenefit
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentFromEmployeeBenefit);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentFromEmployeeBenefit, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.DependentAmountValue
		/// </summary>
		virtual public System.Decimal? DependentAmountValue
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.DependentAmountFaktor
		/// </summary>
		virtual public System.Int32? DependentAmountFaktor
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountFaktor);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountFaktor, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.IsUnlimitedFrequencyDependent
		/// </summary>
		virtual public System.Boolean? IsUnlimitedFrequencyDependent
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequencyDependent);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequencyDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.FrequencyValueDependent
		/// </summary>
		virtual public System.Int32? FrequencyValueDependent
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValueDependent);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValueDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalBenefitInfo entity)
			{
				this.entity = entity;
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
				
			public System.String MedicalBenefitName
			{
				get
				{
					System.String data = entity.MedicalBenefitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitName = null;
					else entity.MedicalBenefitName = Convert.ToString(value);
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
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
				
			public System.String MedicalPercentPaid
			{
				get
				{
					System.Int32? data = entity.MedicalPercentPaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalPercentPaid = null;
					else entity.MedicalPercentPaid = Convert.ToInt32(value);
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
				
			public System.String MedicalPaidMoney
			{
				get
				{
					System.Decimal? data = entity.MedicalPaidMoney;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalPaidMoney = null;
					else entity.MedicalPaidMoney = Convert.ToDecimal(value);
				}
			}
				
			public System.String MedicalPaidFaktor
			{
				get
				{
					System.Int32? data = entity.MedicalPaidFaktor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalPaidFaktor = null;
					else entity.MedicalPaidFaktor = Convert.ToInt32(value);
				}
			}
				
			public System.String IsEmploymentType
			{
				get
				{
					System.Boolean? data = entity.IsEmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmploymentType = null;
					else entity.IsEmploymentType = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsSpecificEmployee
			{
				get
				{
					System.Boolean? data = entity.IsSpecificEmployee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpecificEmployee = null;
					else entity.IsSpecificEmployee = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsEmployeeStatus
			{
				get
				{
					System.Boolean? data = entity.IsEmployeeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmployeeStatus = null;
					else entity.IsEmployeeStatus = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsPositionGrade
			{
				get
				{
					System.Boolean? data = entity.IsPositionGrade;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPositionGrade = null;
					else entity.IsPositionGrade = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAge
			{
				get
				{
					System.Boolean? data = entity.IsAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAge = null;
					else entity.IsAge = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsRemunerationType
			{
				get
				{
					System.Boolean? data = entity.IsRemunerationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRemunerationType = null;
					else entity.IsRemunerationType = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsEmployeeGrade
			{
				get
				{
					System.Boolean? data = entity.IsEmployeeGrade;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmployeeGrade = null;
					else entity.IsEmployeeGrade = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsServiceYear
			{
				get
				{
					System.Boolean? data = entity.IsServiceYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsServiceYear = null;
					else entity.IsServiceYear = Convert.ToBoolean(value);
				}
			}
				
			public System.String SettlementRuleID
			{
				get
				{
					System.Int32? data = entity.SettlementRuleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SettlementRuleID = null;
					else entity.SettlementRuleID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsUnlimitedFrequency
			{
				get
				{
					System.Boolean? data = entity.IsUnlimitedFrequency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnlimitedFrequency = null;
					else entity.IsUnlimitedFrequency = Convert.ToBoolean(value);
				}
			}
				
			public System.String FrequencyValue
			{
				get
				{
					System.Int32? data = entity.FrequencyValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FrequencyValue = null;
					else entity.FrequencyValue = Convert.ToInt32(value);
				}
			}
				
			public System.String SRUnusedBalance
			{
				get
				{
					System.String data = entity.SRUnusedBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUnusedBalance = null;
					else entity.SRUnusedBalance = Convert.ToString(value);
				}
			}
				
			public System.String MaxCarryOver
			{
				get
				{
					System.Decimal? data = entity.MaxCarryOver;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxCarryOver = null;
					else entity.MaxCarryOver = Convert.ToDecimal(value);
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
				
			public System.String DependentPercentPaid
			{
				get
				{
					System.Int32? data = entity.DependentPercentPaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentPercentPaid = null;
					else entity.DependentPercentPaid = Convert.ToInt32(value);
				}
			}
				
			public System.String SRDependentPaidRules
			{
				get
				{
					System.String data = entity.SRDependentPaidRules;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDependentPaidRules = null;
					else entity.SRDependentPaidRules = Convert.ToString(value);
				}
			}
				
			public System.String DependentFromEmployeeBenefit
			{
				get
				{
					System.Int32? data = entity.DependentFromEmployeeBenefit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentFromEmployeeBenefit = null;
					else entity.DependentFromEmployeeBenefit = Convert.ToInt32(value);
				}
			}
				
			public System.String DependentAmountValue
			{
				get
				{
					System.Decimal? data = entity.DependentAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentAmountValue = null;
					else entity.DependentAmountValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentAmountFaktor
			{
				get
				{
					System.Int32? data = entity.DependentAmountFaktor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentAmountFaktor = null;
					else entity.DependentAmountFaktor = Convert.ToInt32(value);
				}
			}
				
			public System.String IsUnlimitedFrequencyDependent
			{
				get
				{
					System.Boolean? data = entity.IsUnlimitedFrequencyDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnlimitedFrequencyDependent = null;
					else entity.IsUnlimitedFrequencyDependent = Convert.ToBoolean(value);
				}
			}
				
			public System.String FrequencyValueDependent
			{
				get
				{
					System.Int32? data = entity.FrequencyValueDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FrequencyValueDependent = null;
					else entity.FrequencyValueDependent = Convert.ToInt32(value);
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
			

			private esMedicalBenefitInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalBenefitInfoQuery query)
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
				throw new Exception("esMedicalBenefitInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalBenefitInfo : esMedicalBenefitInfo
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
	abstract public class esMedicalBenefitInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem MedicalBenefitInfoID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MedicalBenefitName
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem MedicalPercentPaid
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MedicalPercentPaid, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRMedicalPaidRules
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.SRMedicalPaidRules, esSystemType.String);
			}
		} 
		
		public esQueryItem MedicalPaidMoney
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidMoney, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MedicalPaidFaktor
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidFaktor, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsEmploymentType
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsEmploymentType, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsSpecificEmployee
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsSpecificEmployee, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsEmployeeStatus
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeStatus, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsPositionGrade
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsPositionGrade, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAge
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsAge, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsRemunerationType
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsRemunerationType, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsEmployeeGrade
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeGrade, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsServiceYear
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsServiceYear, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SettlementRuleID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.SettlementRuleID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsUnlimitedFrequency
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequency, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem FrequencyValue
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.FrequencyValue, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRUnusedBalance
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.SRUnusedBalance, esSystemType.String);
			}
		} 
		
		public esQueryItem MaxCarryOver
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.MaxCarryOver, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DependentGuarantorID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.DependentGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem DependentPercentPaid
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.DependentPercentPaid, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRDependentPaidRules
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.SRDependentPaidRules, esSystemType.String);
			}
		} 
		
		public esQueryItem DependentFromEmployeeBenefit
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.DependentFromEmployeeBenefit, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DependentAmountValue
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.DependentAmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentAmountFaktor
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.DependentAmountFaktor, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsUnlimitedFrequencyDependent
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequencyDependent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem FrequencyValueDependent
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.FrequencyValueDependent, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalBenefitInfoCollection")]
	public partial class MedicalBenefitInfoCollection : esMedicalBenefitInfoCollection, IEnumerable<MedicalBenefitInfo>
	{
		public MedicalBenefitInfoCollection()
		{

		}
		
		public static implicit operator List<MedicalBenefitInfo>(MedicalBenefitInfoCollection coll)
		{
			List<MedicalBenefitInfo> list = new List<MedicalBenefitInfo>();
			
			foreach (MedicalBenefitInfo emp in coll)
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
				return  MedicalBenefitInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalBenefitInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalBenefitInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalBenefitInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalBenefitInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalBenefitInfo AddNew()
		{
			MedicalBenefitInfo entity = base.AddNewEntity() as MedicalBenefitInfo;
			
			return entity;
		}

		public MedicalBenefitInfo FindByPrimaryKey(System.Int32 medicalBenefitInfoID)
		{
			return base.FindByPrimaryKey(medicalBenefitInfoID) as MedicalBenefitInfo;
		}


		#region IEnumerable<MedicalBenefitInfo> Members

		IEnumerator<MedicalBenefitInfo> IEnumerable<MedicalBenefitInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalBenefitInfo;
			}
		}

		#endregion
		
		private MedicalBenefitInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalBenefitInfo' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalBenefitInfo ({MedicalBenefitInfoID})")]
	[Serializable]
	public partial class MedicalBenefitInfo : esMedicalBenefitInfo
	{
		public MedicalBenefitInfo()
		{

		}
	
		public MedicalBenefitInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalBenefitInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalBenefitInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalBenefitInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalBenefitInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalBenefitInfoQuery : esMedicalBenefitInfoQuery
	{
		public MedicalBenefitInfoQuery()
		{

		}		
		
		public MedicalBenefitInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalBenefitInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalBenefitInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalBenefitInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitInfoID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MedicalBenefitInfoID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MedicalBenefitName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MedicalBenefitName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.GuarantorID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MedicalPercentPaid, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MedicalPercentPaid;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.SRMedicalPaidRules, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.SRMedicalPaidRules;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidMoney, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MedicalPaidMoney;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MedicalPaidFaktor, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MedicalPaidFaktor;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsEmploymentType, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsEmploymentType;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsSpecificEmployee, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsSpecificEmployee;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeStatus, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsEmployeeStatus;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsPositionGrade, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsPositionGrade;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsAge, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsAge;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsRemunerationType, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsRemunerationType;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsEmployeeGrade, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsEmployeeGrade;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsServiceYear, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsServiceYear;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.SettlementRuleID, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.SettlementRuleID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequency, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsUnlimitedFrequency;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValue, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.FrequencyValue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.SRUnusedBalance, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.SRUnusedBalance;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.MaxCarryOver, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.MaxCarryOver;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.NoOfDependent, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.DependentGuarantorID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.DependentGuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.DependentPercentPaid, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.DependentPercentPaid;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.SRDependentPaidRules, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.SRDependentPaidRules;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.DependentFromEmployeeBenefit, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.DependentFromEmployeeBenefit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountValue, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.DependentAmountValue;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.DependentAmountFaktor, 29, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.DependentAmountFaktor;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.IsUnlimitedFrequencyDependent, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.IsUnlimitedFrequencyDependent;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.FrequencyValueDependent, 31, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.FrequencyValueDependent;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitInfoMetadata.ColumnNames.LastUpdateByUserID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalBenefitInfoMetadata Meta()
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
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string MedicalBenefitName = "MedicalBenefitName";
			 public const string IsActive = "IsActive";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string GuarantorID = "GuarantorID";
			 public const string MedicalPercentPaid = "MedicalPercentPaid";
			 public const string SRMedicalPaidRules = "SRMedicalPaidRules";
			 public const string MedicalPaidMoney = "MedicalPaidMoney";
			 public const string MedicalPaidFaktor = "MedicalPaidFaktor";
			 public const string IsEmploymentType = "IsEmploymentType";
			 public const string IsSpecificEmployee = "IsSpecificEmployee";
			 public const string IsEmployeeStatus = "IsEmployeeStatus";
			 public const string IsPositionGrade = "IsPositionGrade";
			 public const string IsAge = "IsAge";
			 public const string IsRemunerationType = "IsRemunerationType";
			 public const string IsEmployeeGrade = "IsEmployeeGrade";
			 public const string IsServiceYear = "IsServiceYear";
			 public const string SettlementRuleID = "SettlementRuleID";
			 public const string IsUnlimitedFrequency = "IsUnlimitedFrequency";
			 public const string FrequencyValue = "FrequencyValue";
			 public const string SRUnusedBalance = "SRUnusedBalance";
			 public const string MaxCarryOver = "MaxCarryOver";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string DependentGuarantorID = "DependentGuarantorID";
			 public const string DependentPercentPaid = "DependentPercentPaid";
			 public const string SRDependentPaidRules = "SRDependentPaidRules";
			 public const string DependentFromEmployeeBenefit = "DependentFromEmployeeBenefit";
			 public const string DependentAmountValue = "DependentAmountValue";
			 public const string DependentAmountFaktor = "DependentAmountFaktor";
			 public const string IsUnlimitedFrequencyDependent = "IsUnlimitedFrequencyDependent";
			 public const string FrequencyValueDependent = "FrequencyValueDependent";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string MedicalBenefitName = "MedicalBenefitName";
			 public const string IsActive = "IsActive";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string GuarantorID = "GuarantorID";
			 public const string MedicalPercentPaid = "MedicalPercentPaid";
			 public const string SRMedicalPaidRules = "SRMedicalPaidRules";
			 public const string MedicalPaidMoney = "MedicalPaidMoney";
			 public const string MedicalPaidFaktor = "MedicalPaidFaktor";
			 public const string IsEmploymentType = "IsEmploymentType";
			 public const string IsSpecificEmployee = "IsSpecificEmployee";
			 public const string IsEmployeeStatus = "IsEmployeeStatus";
			 public const string IsPositionGrade = "IsPositionGrade";
			 public const string IsAge = "IsAge";
			 public const string IsRemunerationType = "IsRemunerationType";
			 public const string IsEmployeeGrade = "IsEmployeeGrade";
			 public const string IsServiceYear = "IsServiceYear";
			 public const string SettlementRuleID = "SettlementRuleID";
			 public const string IsUnlimitedFrequency = "IsUnlimitedFrequency";
			 public const string FrequencyValue = "FrequencyValue";
			 public const string SRUnusedBalance = "SRUnusedBalance";
			 public const string MaxCarryOver = "MaxCarryOver";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string DependentGuarantorID = "DependentGuarantorID";
			 public const string DependentPercentPaid = "DependentPercentPaid";
			 public const string SRDependentPaidRules = "SRDependentPaidRules";
			 public const string DependentFromEmployeeBenefit = "DependentFromEmployeeBenefit";
			 public const string DependentAmountValue = "DependentAmountValue";
			 public const string DependentAmountFaktor = "DependentAmountFaktor";
			 public const string IsUnlimitedFrequencyDependent = "IsUnlimitedFrequencyDependent";
			 public const string FrequencyValueDependent = "FrequencyValueDependent";
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
			lock (typeof(MedicalBenefitInfoMetadata))
			{
				if(MedicalBenefitInfoMetadata.mapDelegates == null)
				{
					MedicalBenefitInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalBenefitInfoMetadata.meta == null)
				{
					MedicalBenefitInfoMetadata.meta = new MedicalBenefitInfoMetadata();
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
				

				meta.AddTypeMap("MedicalBenefitInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicalBenefitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicalPercentPaid", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMedicalPaidRules", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicalPaidMoney", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("MedicalPaidFaktor", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsEmploymentType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSpecificEmployee", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployeeStatus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPositionGrade", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAge", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRemunerationType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployeeGrade", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsServiceYear", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SettlementRuleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsUnlimitedFrequency", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FrequencyValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRUnusedBalance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaxCarryOver", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DependentGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DependentPercentPaid", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDependentPaidRules", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DependentFromEmployeeBenefit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DependentAmountValue", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentAmountFaktor", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsUnlimitedFrequencyDependent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FrequencyValueDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalBenefitInfo";
				meta.Destination = "MedicalBenefitInfo";
				
				meta.spInsert = "proc_MedicalBenefitInfoInsert";				
				meta.spUpdate = "proc_MedicalBenefitInfoUpdate";		
				meta.spDelete = "proc_MedicalBenefitInfoDelete";
				meta.spLoadAll = "proc_MedicalBenefitInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalBenefitInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalBenefitInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
