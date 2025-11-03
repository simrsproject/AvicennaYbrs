/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2022 11:17:28 AM
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
	abstract public class esSalaryComponentCollection : esEntityCollectionWAuditLog
	{
		public esSalaryComponentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SalaryComponentCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryComponentQuery query)
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
			this.InitQuery(query as esSalaryComponentQuery);
		}
		#endregion

		virtual public SalaryComponent DetachEntity(SalaryComponent entity)
		{
			return base.DetachEntity(entity) as SalaryComponent;
		}

		virtual public SalaryComponent AttachEntity(SalaryComponent entity)
		{
			return base.AttachEntity(entity) as SalaryComponent;
		}

		virtual public void Combine(SalaryComponentCollection collection)
		{
			base.Combine(collection);
		}

		new public SalaryComponent this[int index]
		{
			get
			{
				return base[index] as SalaryComponent;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryComponent);
		}
	}

	[Serializable]
	abstract public class esSalaryComponent : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryComponentQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryComponent()
		{
		}

		public esSalaryComponent(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 salaryComponentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 salaryComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 salaryComponentID)
		{
			esSalaryComponentQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryComponentID == salaryComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 salaryComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryComponentID", salaryComponentID);
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
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
						case "SalaryComponentCode": this.str.SalaryComponentCode = (string)value; break;
						case "SalaryComponentName": this.str.SalaryComponentName = (string)value; break;
						case "SRSalaryType": this.str.SRSalaryType = (string)value; break;
						case "SRSalaryCategory": this.str.SRSalaryCategory = (string)value; break;
						case "SRIncomeTaxMethod": this.str.SRIncomeTaxMethod = (string)value; break;
						case "SRDeductionType": this.str.SRDeductionType = (string)value; break;
						case "SRJamsostekType": this.str.SRJamsostekType = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "FaktorRule": this.str.FaktorRule = (string)value; break;
						case "FaktorRuleDisplay": this.str.FaktorRuleDisplay = (string)value; break;
						case "SalaryComponentRoundingID": this.str.SalaryComponentRoundingID = (string)value; break;
						case "DisplayInPaySlip": this.str.DisplayInPaySlip = (string)value; break;
						case "DisplayInPayRekapReport": this.str.DisplayInPayRekapReport = (string)value; break;
						case "IsOrganizationUnit": this.str.IsOrganizationUnit = (string)value; break;
						case "IsEmployeeStatus": this.str.IsEmployeeStatus = (string)value; break;
						case "IsPosition": this.str.IsPosition = (string)value; break;
						case "IsReligion": this.str.IsReligion = (string)value; break;
						case "IsEmployee": this.str.IsEmployee = (string)value; break;
						case "IsEmploymentType": this.str.IsEmploymentType = (string)value; break;
						case "IsPositionGrade": this.str.IsPositionGrade = (string)value; break;
						case "IsMaritalStatus": this.str.IsMaritalStatus = (string)value; break;
						case "IsServiceYear": this.str.IsServiceYear = (string)value; break;
						case "IsSalaryTableNumber": this.str.IsSalaryTableNumber = (string)value; break;
						case "IsEmployeeGrade": this.str.IsEmployeeGrade = (string)value; break;
						case "IsNoOfDependent": this.str.IsNoOfDependent = (string)value; break;
						case "IsAttedanceMatrixID": this.str.IsAttedanceMatrixID = (string)value; break;
						case "IsComponent1": this.str.IsComponent1 = (string)value; break;
						case "IsComponent2": this.str.IsComponent2 = (string)value; break;
						case "IsComponent3": this.str.IsComponent3 = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsKWI": this.str.IsKWI = (string)value; break;
						case "IsEducationLevel": this.str.IsEducationLevel = (string)value; break;
						case "IsEmployeeType": this.str.IsEmployeeType = (string)value; break;
						case "IsServiceUnitID": this.str.IsServiceUnitID = (string)value; break;
						case "MinAmount": this.str.MinAmount = (string)value; break;
						case "MaxAmount": this.str.MaxAmount = (string)value; break;
						case "IsPeriodicSalary": this.str.IsPeriodicSalary = (string)value; break;
						case "IsThr": this.str.IsThr = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "NormalBalance": this.str.NormalBalance = (string)value; break;
						case "IsDisplayInThrSlip": this.str.IsDisplayInThrSlip = (string)value; break;
						case "ChartOfAccountIdThr": this.str.ChartOfAccountIdThr = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "SubLedgerIdThr": this.str.SubLedgerIdThr = (string)value; break;
						case "NormalBalanceThr": this.str.NormalBalanceThr = (string)value; break;
						case "SRSalaryComponentGroup": this.str.SRSalaryComponentGroup = (string)value; break;
						case "ChartOfAccountIdIndirect": this.str.ChartOfAccountIdIndirect = (string)value; break;
						case "NormalBalanceIndirect": this.str.NormalBalanceIndirect = (string)value; break;
						case "SubLedgerIdIndirect": this.str.SubLedgerIdIndirect = (string)value; break;
						case "ChartOfAccountIdThrIndirect": this.str.ChartOfAccountIdThrIndirect = (string)value; break;
						case "NormalBalanceThrIndirect": this.str.NormalBalanceThrIndirect = (string)value; break;
						case "SubLedgerIdThrIndirect": this.str.SubLedgerIdThrIndirect = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SalaryComponentID":

							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "FaktorRule":

							if (value == null || value is System.Double)
								this.FaktorRule = (System.Double?)value;
							break;
						case "SalaryComponentRoundingID":

							if (value == null || value is System.Int32)
								this.SalaryComponentRoundingID = (System.Int32?)value;
							break;
						case "DisplayInPaySlip":

							if (value == null || value is System.Boolean)
								this.DisplayInPaySlip = (System.Boolean?)value;
							break;
						case "DisplayInPayRekapReport":

							if (value == null || value is System.Boolean)
								this.DisplayInPayRekapReport = (System.Boolean?)value;
							break;
						case "IsOrganizationUnit":

							if (value == null || value is System.Boolean)
								this.IsOrganizationUnit = (System.Boolean?)value;
							break;
						case "IsEmployeeStatus":

							if (value == null || value is System.Boolean)
								this.IsEmployeeStatus = (System.Boolean?)value;
							break;
						case "IsPosition":

							if (value == null || value is System.Boolean)
								this.IsPosition = (System.Boolean?)value;
							break;
						case "IsReligion":

							if (value == null || value is System.Boolean)
								this.IsReligion = (System.Boolean?)value;
							break;
						case "IsEmployee":

							if (value == null || value is System.Boolean)
								this.IsEmployee = (System.Boolean?)value;
							break;
						case "IsEmploymentType":

							if (value == null || value is System.Boolean)
								this.IsEmploymentType = (System.Boolean?)value;
							break;
						case "IsPositionGrade":

							if (value == null || value is System.Boolean)
								this.IsPositionGrade = (System.Boolean?)value;
							break;
						case "IsMaritalStatus":

							if (value == null || value is System.Boolean)
								this.IsMaritalStatus = (System.Boolean?)value;
							break;
						case "IsServiceYear":

							if (value == null || value is System.Boolean)
								this.IsServiceYear = (System.Boolean?)value;
							break;
						case "IsSalaryTableNumber":

							if (value == null || value is System.Boolean)
								this.IsSalaryTableNumber = (System.Boolean?)value;
							break;
						case "IsEmployeeGrade":

							if (value == null || value is System.Boolean)
								this.IsEmployeeGrade = (System.Boolean?)value;
							break;
						case "IsNoOfDependent":

							if (value == null || value is System.Boolean)
								this.IsNoOfDependent = (System.Boolean?)value;
							break;
						case "IsAttedanceMatrixID":

							if (value == null || value is System.Boolean)
								this.IsAttedanceMatrixID = (System.Boolean?)value;
							break;
						case "IsComponent1":

							if (value == null || value is System.Boolean)
								this.IsComponent1 = (System.Boolean?)value;
							break;
						case "IsComponent2":

							if (value == null || value is System.Boolean)
								this.IsComponent2 = (System.Boolean?)value;
							break;
						case "IsComponent3":

							if (value == null || value is System.Boolean)
								this.IsComponent3 = (System.Boolean?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsKWI":

							if (value == null || value is System.Boolean)
								this.IsKWI = (System.Boolean?)value;
							break;
						case "IsEducationLevel":

							if (value == null || value is System.Boolean)
								this.IsEducationLevel = (System.Boolean?)value;
							break;
						case "IsEmployeeType":

							if (value == null || value is System.Boolean)
								this.IsEmployeeType = (System.Boolean?)value;
							break;
						case "IsServiceUnitID":

							if (value == null || value is System.Boolean)
								this.IsServiceUnitID = (System.Boolean?)value;
							break;
						case "MinAmount":

							if (value == null || value is System.Decimal)
								this.MinAmount = (System.Decimal?)value;
							break;
						case "MaxAmount":

							if (value == null || value is System.Decimal)
								this.MaxAmount = (System.Decimal?)value;
							break;
						case "IsPeriodicSalary":

							if (value == null || value is System.Boolean)
								this.IsPeriodicSalary = (System.Boolean?)value;
							break;
						case "IsThr":

							if (value == null || value is System.Boolean)
								this.IsThr = (System.Boolean?)value;
							break;
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "IsDisplayInThrSlip":

							if (value == null || value is System.Boolean)
								this.IsDisplayInThrSlip = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdThr":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdThr = (System.Int32?)value;
							break;
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "SubLedgerIdThr":

							if (value == null || value is System.Int32)
								this.SubLedgerIdThr = (System.Int32?)value;
							break;
						case "ChartOfAccountIdIndirect":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIndirect = (System.Int32?)value;
							break;
						case "SubLedgerIdIndirect":

							if (value == null || value is System.Int32)
								this.SubLedgerIdIndirect = (System.Int32?)value;
							break;
						case "ChartOfAccountIdThrIndirect":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdThrIndirect = (System.Int32?)value;
							break;
						case "SubLedgerIdThrIndirect":

							if (value == null || value is System.Int32)
								this.SubLedgerIdThrIndirect = (System.Int32?)value;
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
		/// Maps to SalaryComponent.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SalaryComponentID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SalaryComponentCode
		/// </summary>
		virtual public System.String SalaryComponentCode
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SalaryComponentCode);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SalaryComponentCode, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SalaryComponentName
		/// </summary>
		virtual public System.String SalaryComponentName
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SalaryComponentName);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SalaryComponentName, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRSalaryType
		/// </summary>
		virtual public System.String SRSalaryType
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryType);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRSalaryCategory
		/// </summary>
		virtual public System.String SRSalaryCategory
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryCategory);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryCategory, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRIncomeTaxMethod
		/// </summary>
		virtual public System.String SRIncomeTaxMethod
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRIncomeTaxMethod);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRIncomeTaxMethod, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRDeductionType
		/// </summary>
		virtual public System.String SRDeductionType
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRDeductionType);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRDeductionType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRJamsostekType
		/// </summary>
		virtual public System.String SRJamsostekType
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRJamsostekType);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRJamsostekType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(SalaryComponentMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.FaktorRule
		/// </summary>
		virtual public System.Double? FaktorRule
		{
			get
			{
				return base.GetSystemDouble(SalaryComponentMetadata.ColumnNames.FaktorRule);
			}

			set
			{
				base.SetSystemDouble(SalaryComponentMetadata.ColumnNames.FaktorRule, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.FaktorRuleDisplay
		/// </summary>
		virtual public System.String FaktorRuleDisplay
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.FaktorRuleDisplay);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.FaktorRuleDisplay, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SalaryComponentRoundingID
		/// </summary>
		virtual public System.Int32? SalaryComponentRoundingID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SalaryComponentRoundingID);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SalaryComponentRoundingID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.DisplayInPaySlip
		/// </summary>
		virtual public System.Boolean? DisplayInPaySlip
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.DisplayInPaySlip);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.DisplayInPaySlip, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.DisplayInPayRekapReport
		/// </summary>
		virtual public System.Boolean? DisplayInPayRekapReport
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.DisplayInPayRekapReport);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.DisplayInPayRekapReport, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsOrganizationUnit
		/// </summary>
		virtual public System.Boolean? IsOrganizationUnit
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsOrganizationUnit);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsOrganizationUnit, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEmployeeStatus
		/// </summary>
		virtual public System.Boolean? IsEmployeeStatus
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeStatus);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeStatus, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsPosition
		/// </summary>
		virtual public System.Boolean? IsPosition
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPosition);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPosition, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsReligion
		/// </summary>
		virtual public System.Boolean? IsReligion
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsReligion);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsReligion, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEmployee
		/// </summary>
		virtual public System.Boolean? IsEmployee
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployee);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployee, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEmploymentType
		/// </summary>
		virtual public System.Boolean? IsEmploymentType
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmploymentType);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsPositionGrade
		/// </summary>
		virtual public System.Boolean? IsPositionGrade
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPositionGrade);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPositionGrade, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsMaritalStatus
		/// </summary>
		virtual public System.Boolean? IsMaritalStatus
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsMaritalStatus);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsServiceYear
		/// </summary>
		virtual public System.Boolean? IsServiceYear
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsServiceYear);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsServiceYear, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsSalaryTableNumber
		/// </summary>
		virtual public System.Boolean? IsSalaryTableNumber
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsSalaryTableNumber);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsSalaryTableNumber, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEmployeeGrade
		/// </summary>
		virtual public System.Boolean? IsEmployeeGrade
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeGrade);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeGrade, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsNoOfDependent
		/// </summary>
		virtual public System.Boolean? IsNoOfDependent
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsNoOfDependent);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsNoOfDependent, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsAttedanceMatrixID
		/// </summary>
		virtual public System.Boolean? IsAttedanceMatrixID
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsAttedanceMatrixID);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsAttedanceMatrixID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsComponent1
		/// </summary>
		virtual public System.Boolean? IsComponent1
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent1);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent1, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsComponent2
		/// </summary>
		virtual public System.Boolean? IsComponent2
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent2);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent2, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsComponent3
		/// </summary>
		virtual public System.Boolean? IsComponent3
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent3);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsComponent3, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SalaryComponentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsKWI
		/// </summary>
		virtual public System.Boolean? IsKWI
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsKWI);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsKWI, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEducationLevel
		/// </summary>
		virtual public System.Boolean? IsEducationLevel
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEducationLevel);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsEmployeeType
		/// </summary>
		virtual public System.Boolean? IsEmployeeType
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeType);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsEmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsServiceUnitID
		/// </summary>
		virtual public System.Boolean? IsServiceUnitID
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsServiceUnitID);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.MinAmount
		/// </summary>
		virtual public System.Decimal? MinAmount
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentMetadata.ColumnNames.MinAmount);
			}

			set
			{
				base.SetSystemDecimal(SalaryComponentMetadata.ColumnNames.MinAmount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.MaxAmount
		/// </summary>
		virtual public System.Decimal? MaxAmount
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentMetadata.ColumnNames.MaxAmount);
			}

			set
			{
				base.SetSystemDecimal(SalaryComponentMetadata.ColumnNames.MaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsPeriodicSalary
		/// </summary>
		virtual public System.Boolean? IsPeriodicSalary
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPeriodicSalary);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsPeriodicSalary, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsThr
		/// </summary>
		virtual public System.Boolean? IsThr
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsThr);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsThr, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.NormalBalance
		/// </summary>
		virtual public System.String NormalBalance
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalance);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalance, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.IsDisplayInThrSlip
		/// </summary>
		virtual public System.Boolean? IsDisplayInThrSlip
		{
			get
			{
				return base.GetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsDisplayInThrSlip);
			}

			set
			{
				base.SetSystemBoolean(SalaryComponentMetadata.ColumnNames.IsDisplayInThrSlip, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ChartOfAccountIdThr
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdThr
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThr);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThr, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SubLedgerIdThr
		/// </summary>
		virtual public System.Int32? SubLedgerIdThr
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdThr);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdThr, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.NormalBalanceThr
		/// </summary>
		virtual public System.String NormalBalanceThr
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceThr);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceThr, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SRSalaryComponentGroup
		/// </summary>
		virtual public System.String SRSalaryComponentGroup
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryComponentGroup);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.SRSalaryComponentGroup, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ChartOfAccountIdIndirect
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIndirect
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdIndirect);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdIndirect, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.NormalBalanceIndirect
		/// </summary>
		virtual public System.String NormalBalanceIndirect
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceIndirect);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceIndirect, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SubLedgerIdIndirect
		/// </summary>
		virtual public System.Int32? SubLedgerIdIndirect
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdIndirect);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdIndirect, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.ChartOfAccountIdThrIndirect
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdThrIndirect
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThrIndirect);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThrIndirect, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.NormalBalanceThrIndirect
		/// </summary>
		virtual public System.String NormalBalanceThrIndirect
		{
			get
			{
				return base.GetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceThrIndirect);
			}

			set
			{
				base.SetSystemString(SalaryComponentMetadata.ColumnNames.NormalBalanceThrIndirect, value);
			}
		}
		/// <summary>
		/// Maps to SalaryComponent.SubLedgerIdThrIndirect
		/// </summary>
		virtual public System.Int32? SubLedgerIdThrIndirect
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdThrIndirect);
			}

			set
			{
				base.SetSystemInt32(SalaryComponentMetadata.ColumnNames.SubLedgerIdThrIndirect, value);
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
			public esStrings(esSalaryComponent entity)
			{
				this.entity = entity;
			}
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
				}
			}
			public System.String SalaryComponentCode
			{
				get
				{
					System.String data = entity.SalaryComponentCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentCode = null;
					else entity.SalaryComponentCode = Convert.ToString(value);
				}
			}
			public System.String SalaryComponentName
			{
				get
				{
					System.String data = entity.SalaryComponentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentName = null;
					else entity.SalaryComponentName = Convert.ToString(value);
				}
			}
			public System.String SRSalaryType
			{
				get
				{
					System.String data = entity.SRSalaryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSalaryType = null;
					else entity.SRSalaryType = Convert.ToString(value);
				}
			}
			public System.String SRSalaryCategory
			{
				get
				{
					System.String data = entity.SRSalaryCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSalaryCategory = null;
					else entity.SRSalaryCategory = Convert.ToString(value);
				}
			}
			public System.String SRIncomeTaxMethod
			{
				get
				{
					System.String data = entity.SRIncomeTaxMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncomeTaxMethod = null;
					else entity.SRIncomeTaxMethod = Convert.ToString(value);
				}
			}
			public System.String SRDeductionType
			{
				get
				{
					System.String data = entity.SRDeductionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDeductionType = null;
					else entity.SRDeductionType = Convert.ToString(value);
				}
			}
			public System.String SRJamsostekType
			{
				get
				{
					System.String data = entity.SRJamsostekType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRJamsostekType = null;
					else entity.SRJamsostekType = Convert.ToString(value);
				}
			}
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
			public System.String FaktorRule
			{
				get
				{
					System.Double? data = entity.FaktorRule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaktorRule = null;
					else entity.FaktorRule = Convert.ToDouble(value);
				}
			}
			public System.String FaktorRuleDisplay
			{
				get
				{
					System.String data = entity.FaktorRuleDisplay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaktorRuleDisplay = null;
					else entity.FaktorRuleDisplay = Convert.ToString(value);
				}
			}
			public System.String SalaryComponentRoundingID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentRoundingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentRoundingID = null;
					else entity.SalaryComponentRoundingID = Convert.ToInt32(value);
				}
			}
			public System.String DisplayInPaySlip
			{
				get
				{
					System.Boolean? data = entity.DisplayInPaySlip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplayInPaySlip = null;
					else entity.DisplayInPaySlip = Convert.ToBoolean(value);
				}
			}
			public System.String DisplayInPayRekapReport
			{
				get
				{
					System.Boolean? data = entity.DisplayInPayRekapReport;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplayInPayRekapReport = null;
					else entity.DisplayInPayRekapReport = Convert.ToBoolean(value);
				}
			}
			public System.String IsOrganizationUnit
			{
				get
				{
					System.Boolean? data = entity.IsOrganizationUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOrganizationUnit = null;
					else entity.IsOrganizationUnit = Convert.ToBoolean(value);
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
			public System.String IsPosition
			{
				get
				{
					System.Boolean? data = entity.IsPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosition = null;
					else entity.IsPosition = Convert.ToBoolean(value);
				}
			}
			public System.String IsReligion
			{
				get
				{
					System.Boolean? data = entity.IsReligion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReligion = null;
					else entity.IsReligion = Convert.ToBoolean(value);
				}
			}
			public System.String IsEmployee
			{
				get
				{
					System.Boolean? data = entity.IsEmployee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmployee = null;
					else entity.IsEmployee = Convert.ToBoolean(value);
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
			public System.String IsMaritalStatus
			{
				get
				{
					System.Boolean? data = entity.IsMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMaritalStatus = null;
					else entity.IsMaritalStatus = Convert.ToBoolean(value);
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
			public System.String IsSalaryTableNumber
			{
				get
				{
					System.Boolean? data = entity.IsSalaryTableNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalaryTableNumber = null;
					else entity.IsSalaryTableNumber = Convert.ToBoolean(value);
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
			public System.String IsNoOfDependent
			{
				get
				{
					System.Boolean? data = entity.IsNoOfDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNoOfDependent = null;
					else entity.IsNoOfDependent = Convert.ToBoolean(value);
				}
			}
			public System.String IsAttedanceMatrixID
			{
				get
				{
					System.Boolean? data = entity.IsAttedanceMatrixID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAttedanceMatrixID = null;
					else entity.IsAttedanceMatrixID = Convert.ToBoolean(value);
				}
			}
			public System.String IsComponent1
			{
				get
				{
					System.Boolean? data = entity.IsComponent1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComponent1 = null;
					else entity.IsComponent1 = Convert.ToBoolean(value);
				}
			}
			public System.String IsComponent2
			{
				get
				{
					System.Boolean? data = entity.IsComponent2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComponent2 = null;
					else entity.IsComponent2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsComponent3
			{
				get
				{
					System.Boolean? data = entity.IsComponent3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComponent3 = null;
					else entity.IsComponent3 = Convert.ToBoolean(value);
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
			public System.String IsKWI
			{
				get
				{
					System.Boolean? data = entity.IsKWI;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKWI = null;
					else entity.IsKWI = Convert.ToBoolean(value);
				}
			}
			public System.String IsEducationLevel
			{
				get
				{
					System.Boolean? data = entity.IsEducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEducationLevel = null;
					else entity.IsEducationLevel = Convert.ToBoolean(value);
				}
			}
			public System.String IsEmployeeType
			{
				get
				{
					System.Boolean? data = entity.IsEmployeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmployeeType = null;
					else entity.IsEmployeeType = Convert.ToBoolean(value);
				}
			}
			public System.String IsServiceUnitID
			{
				get
				{
					System.Boolean? data = entity.IsServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsServiceUnitID = null;
					else entity.IsServiceUnitID = Convert.ToBoolean(value);
				}
			}
			public System.String MinAmount
			{
				get
				{
					System.Decimal? data = entity.MinAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinAmount = null;
					else entity.MinAmount = Convert.ToDecimal(value);
				}
			}
			public System.String MaxAmount
			{
				get
				{
					System.Decimal? data = entity.MaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxAmount = null;
					else entity.MaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsPeriodicSalary
			{
				get
				{
					System.Boolean? data = entity.IsPeriodicSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPeriodicSalary = null;
					else entity.IsPeriodicSalary = Convert.ToBoolean(value);
				}
			}
			public System.String IsThr
			{
				get
				{
					System.Boolean? data = entity.IsThr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsThr = null;
					else entity.IsThr = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String NormalBalance
			{
				get
				{
					System.String data = entity.NormalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalance = null;
					else entity.NormalBalance = Convert.ToString(value);
				}
			}
			public System.String IsDisplayInThrSlip
			{
				get
				{
					System.Boolean? data = entity.IsDisplayInThrSlip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisplayInThrSlip = null;
					else entity.IsDisplayInThrSlip = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdThr
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdThr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdThr = null;
					else entity.ChartOfAccountIdThr = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerIdThr
			{
				get
				{
					System.Int32? data = entity.SubLedgerIdThr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerIdThr = null;
					else entity.SubLedgerIdThr = Convert.ToInt32(value);
				}
			}
			public System.String NormalBalanceThr
			{
				get
				{
					System.String data = entity.NormalBalanceThr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalanceThr = null;
					else entity.NormalBalanceThr = Convert.ToString(value);
				}
			}
			public System.String SRSalaryComponentGroup
			{
				get
				{
					System.String data = entity.SRSalaryComponentGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSalaryComponentGroup = null;
					else entity.SRSalaryComponentGroup = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdIndirect
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIndirect = null;
					else entity.ChartOfAccountIdIndirect = Convert.ToInt32(value);
				}
			}
			public System.String NormalBalanceIndirect
			{
				get
				{
					System.String data = entity.NormalBalanceIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalanceIndirect = null;
					else entity.NormalBalanceIndirect = Convert.ToString(value);
				}
			}
			public System.String SubLedgerIdIndirect
			{
				get
				{
					System.Int32? data = entity.SubLedgerIdIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerIdIndirect = null;
					else entity.SubLedgerIdIndirect = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdThrIndirect
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdThrIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdThrIndirect = null;
					else entity.ChartOfAccountIdThrIndirect = Convert.ToInt32(value);
				}
			}
			public System.String NormalBalanceThrIndirect
			{
				get
				{
					System.String data = entity.NormalBalanceThrIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalanceThrIndirect = null;
					else entity.NormalBalanceThrIndirect = Convert.ToString(value);
				}
			}
			public System.String SubLedgerIdThrIndirect
			{
				get
				{
					System.Int32? data = entity.SubLedgerIdThrIndirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerIdThrIndirect = null;
					else entity.SubLedgerIdThrIndirect = Convert.ToInt32(value);
				}
			}
			private esSalaryComponent entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryComponentQuery query)
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
				throw new Exception("esSalaryComponent can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SalaryComponent : esSalaryComponent
	{
	}

	[Serializable]
	abstract public class esSalaryComponentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentMetadata.Meta();
			}
		}

		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		}

		public esQueryItem SalaryComponentCode
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SalaryComponentCode, esSystemType.String);
			}
		}

		public esQueryItem SalaryComponentName
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SalaryComponentName, esSystemType.String);
			}
		}

		public esQueryItem SRSalaryType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRSalaryType, esSystemType.String);
			}
		}

		public esQueryItem SRSalaryCategory
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRSalaryCategory, esSystemType.String);
			}
		}

		public esQueryItem SRIncomeTaxMethod
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRIncomeTaxMethod, esSystemType.String);
			}
		}

		public esQueryItem SRDeductionType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRDeductionType, esSystemType.String);
			}
		}

		public esQueryItem SRJamsostekType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRJamsostekType, esSystemType.String);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

		public esQueryItem FaktorRule
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.FaktorRule, esSystemType.Double);
			}
		}

		public esQueryItem FaktorRuleDisplay
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.FaktorRuleDisplay, esSystemType.String);
			}
		}

		public esQueryItem SalaryComponentRoundingID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SalaryComponentRoundingID, esSystemType.Int32);
			}
		}

		public esQueryItem DisplayInPaySlip
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.DisplayInPaySlip, esSystemType.Boolean);
			}
		}

		public esQueryItem DisplayInPayRekapReport
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.DisplayInPayRekapReport, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOrganizationUnit
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsOrganizationUnit, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmployeeStatus
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEmployeeStatus, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPosition
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsPosition, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReligion
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsReligion, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmployee
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEmployee, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmploymentType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEmploymentType, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPositionGrade
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsPositionGrade, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMaritalStatus
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsMaritalStatus, esSystemType.Boolean);
			}
		}

		public esQueryItem IsServiceYear
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsServiceYear, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsSalaryTableNumber, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmployeeGrade
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEmployeeGrade, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNoOfDependent
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsNoOfDependent, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAttedanceMatrixID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsAttedanceMatrixID, esSystemType.Boolean);
			}
		}

		public esQueryItem IsComponent1
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsComponent1, esSystemType.Boolean);
			}
		}

		public esQueryItem IsComponent2
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsComponent2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsComponent3
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsComponent3, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsKWI
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsKWI, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEducationLevel
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEducationLevel, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmployeeType
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsEmployeeType, esSystemType.Boolean);
			}
		}

		public esQueryItem IsServiceUnitID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsServiceUnitID, esSystemType.Boolean);
			}
		}

		public esQueryItem MinAmount
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.MinAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxAmount
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.MaxAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPeriodicSalary
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsPeriodicSalary, esSystemType.Boolean);
			}
		}

		public esQueryItem IsThr
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsThr, esSystemType.Boolean);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem NormalBalance
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.NormalBalance, esSystemType.String);
			}
		}

		public esQueryItem IsDisplayInThrSlip
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.IsDisplayInThrSlip, esSystemType.Boolean);
			}
		}

		public esQueryItem ChartOfAccountIdThr
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThr, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerIdThr
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SubLedgerIdThr, esSystemType.Int32);
			}
		}

		public esQueryItem NormalBalanceThr
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.NormalBalanceThr, esSystemType.String);
			}
		}

		public esQueryItem SRSalaryComponentGroup
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SRSalaryComponentGroup, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountIdIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ChartOfAccountIdIndirect, esSystemType.Int32);
			}
		}

		public esQueryItem NormalBalanceIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.NormalBalanceIndirect, esSystemType.String);
			}
		}

		public esQueryItem SubLedgerIdIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SubLedgerIdIndirect, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdThrIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThrIndirect, esSystemType.Int32);
			}
		}

		public esQueryItem NormalBalanceThrIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.NormalBalanceThrIndirect, esSystemType.String);
			}
		}

		public esQueryItem SubLedgerIdThrIndirect
		{
			get
			{
				return new esQueryItem(this, SalaryComponentMetadata.ColumnNames.SubLedgerIdThrIndirect, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryComponentCollection")]
	public partial class SalaryComponentCollection : esSalaryComponentCollection, IEnumerable<SalaryComponent>
	{
		public SalaryComponentCollection()
		{

		}

		public static implicit operator List<SalaryComponent>(SalaryComponentCollection coll)
		{
			List<SalaryComponent> list = new List<SalaryComponent>();

			foreach (SalaryComponent emp in coll)
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
				return SalaryComponentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryComponent(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryComponent();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SalaryComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentQuery();
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
		public bool Load(SalaryComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SalaryComponent AddNew()
		{
			SalaryComponent entity = base.AddNewEntity() as SalaryComponent;

			return entity;
		}
		public SalaryComponent FindByPrimaryKey(Int32 salaryComponentID)
		{
			return base.FindByPrimaryKey(salaryComponentID) as SalaryComponent;
		}

		#region IEnumerable< SalaryComponent> Members

		IEnumerator<SalaryComponent> IEnumerable<SalaryComponent>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SalaryComponent;
			}
		}

		#endregion

		private SalaryComponentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryComponent' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SalaryComponent ({SalaryComponentID})")]
	[Serializable]
	public partial class SalaryComponent : esSalaryComponent
	{
		public SalaryComponent()
		{
		}

		public SalaryComponent(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentMetadata.Meta();
			}
		}

		override protected esSalaryComponentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SalaryComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentQuery();
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
		public bool Load(SalaryComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SalaryComponentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SalaryComponentQuery : esSalaryComponentQuery
	{
		public SalaryComponentQuery()
		{

		}

		public SalaryComponentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SalaryComponentQuery";
		}
	}

	[Serializable]
	public partial class SalaryComponentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryComponentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SalaryComponentID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SalaryComponentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SalaryComponentCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SalaryComponentCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SalaryComponentName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SalaryComponentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRSalaryType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRSalaryType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRSalaryCategory, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRSalaryCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRIncomeTaxMethod, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRIncomeTaxMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRDeductionType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRDeductionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRJamsostekType, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRJamsostekType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.Amount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.FaktorRule, 9, typeof(System.Double), esSystemType.Double);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.FaktorRule;
			c.NumericPrecision = 53;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.FaktorRuleDisplay, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.FaktorRuleDisplay;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SalaryComponentRoundingID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SalaryComponentRoundingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.DisplayInPaySlip, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.DisplayInPaySlip;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.DisplayInPayRekapReport, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.DisplayInPayRekapReport;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsOrganizationUnit, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsOrganizationUnit;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEmployeeStatus, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEmployeeStatus;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsPosition, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsPosition;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsReligion, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsReligion;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEmployee, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEmployee;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEmploymentType, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEmploymentType;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsPositionGrade, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsPositionGrade;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsMaritalStatus, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsMaritalStatus;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsServiceYear, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsServiceYear;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsSalaryTableNumber, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsSalaryTableNumber;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEmployeeGrade, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEmployeeGrade;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsNoOfDependent, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsNoOfDependent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsAttedanceMatrixID, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsAttedanceMatrixID;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsComponent1, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsComponent1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsComponent2, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsComponent2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsComponent3, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsComponent3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ValidFrom, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ValidTo, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ValidTo;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.LastUpdateDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.LastUpdateByUserID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsKWI, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsKWI;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEducationLevel, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEducationLevel;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsEmployeeType, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsEmployeeType;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsServiceUnitID, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsServiceUnitID;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.MinAmount, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.MinAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.MaxAmount, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.MaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsPeriodicSalary, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsPeriodicSalary;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsThr, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsThr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ChartOfAccountId, 42, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.NormalBalance, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.NormalBalance;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.IsDisplayInThrSlip, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.IsDisplayInThrSlip;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThr, 45, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ChartOfAccountIdThr;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SubLedgerId, 46, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SubLedgerIdThr, 47, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SubLedgerIdThr;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.NormalBalanceThr, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.NormalBalanceThr;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SRSalaryComponentGroup, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SRSalaryComponentGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdIndirect, 50, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ChartOfAccountIdIndirect;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.NormalBalanceIndirect, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.NormalBalanceIndirect;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SubLedgerIdIndirect, 52, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SubLedgerIdIndirect;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.ChartOfAccountIdThrIndirect, 53, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.ChartOfAccountIdThrIndirect;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.NormalBalanceThrIndirect, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.NormalBalanceThrIndirect;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryComponentMetadata.ColumnNames.SubLedgerIdThrIndirect, 55, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentMetadata.PropertyNames.SubLedgerIdThrIndirect;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SalaryComponentMetadata Meta()
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
			public const string SalaryComponentID = "SalaryComponentID";
			public const string SalaryComponentCode = "SalaryComponentCode";
			public const string SalaryComponentName = "SalaryComponentName";
			public const string SRSalaryType = "SRSalaryType";
			public const string SRSalaryCategory = "SRSalaryCategory";
			public const string SRIncomeTaxMethod = "SRIncomeTaxMethod";
			public const string SRDeductionType = "SRDeductionType";
			public const string SRJamsostekType = "SRJamsostekType";
			public const string Amount = "Amount";
			public const string FaktorRule = "FaktorRule";
			public const string FaktorRuleDisplay = "FaktorRuleDisplay";
			public const string SalaryComponentRoundingID = "SalaryComponentRoundingID";
			public const string DisplayInPaySlip = "DisplayInPaySlip";
			public const string DisplayInPayRekapReport = "DisplayInPayRekapReport";
			public const string IsOrganizationUnit = "IsOrganizationUnit";
			public const string IsEmployeeStatus = "IsEmployeeStatus";
			public const string IsPosition = "IsPosition";
			public const string IsReligion = "IsReligion";
			public const string IsEmployee = "IsEmployee";
			public const string IsEmploymentType = "IsEmploymentType";
			public const string IsPositionGrade = "IsPositionGrade";
			public const string IsMaritalStatus = "IsMaritalStatus";
			public const string IsServiceYear = "IsServiceYear";
			public const string IsSalaryTableNumber = "IsSalaryTableNumber";
			public const string IsEmployeeGrade = "IsEmployeeGrade";
			public const string IsNoOfDependent = "IsNoOfDependent";
			public const string IsAttedanceMatrixID = "IsAttedanceMatrixID";
			public const string IsComponent1 = "IsComponent1";
			public const string IsComponent2 = "IsComponent2";
			public const string IsComponent3 = "IsComponent3";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsKWI = "IsKWI";
			public const string IsEducationLevel = "IsEducationLevel";
			public const string IsEmployeeType = "IsEmployeeType";
			public const string IsServiceUnitID = "IsServiceUnitID";
			public const string MinAmount = "MinAmount";
			public const string MaxAmount = "MaxAmount";
			public const string IsPeriodicSalary = "IsPeriodicSalary";
			public const string IsThr = "IsThr";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string NormalBalance = "NormalBalance";
			public const string IsDisplayInThrSlip = "IsDisplayInThrSlip";
			public const string ChartOfAccountIdThr = "ChartOfAccountIdThr";
			public const string SubLedgerId = "SubLedgerId";
			public const string SubLedgerIdThr = "SubLedgerIdThr";
			public const string NormalBalanceThr = "NormalBalanceThr";
			public const string SRSalaryComponentGroup = "SRSalaryComponentGroup";
			public const string ChartOfAccountIdIndirect = "ChartOfAccountIdIndirect";
			public const string NormalBalanceIndirect = "NormalBalanceIndirect";
			public const string SubLedgerIdIndirect = "SubLedgerIdIndirect";
			public const string ChartOfAccountIdThrIndirect = "ChartOfAccountIdThrIndirect";
			public const string NormalBalanceThrIndirect = "NormalBalanceThrIndirect";
			public const string SubLedgerIdThrIndirect = "SubLedgerIdThrIndirect";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SalaryComponentID = "SalaryComponentID";
			public const string SalaryComponentCode = "SalaryComponentCode";
			public const string SalaryComponentName = "SalaryComponentName";
			public const string SRSalaryType = "SRSalaryType";
			public const string SRSalaryCategory = "SRSalaryCategory";
			public const string SRIncomeTaxMethod = "SRIncomeTaxMethod";
			public const string SRDeductionType = "SRDeductionType";
			public const string SRJamsostekType = "SRJamsostekType";
			public const string Amount = "Amount";
			public const string FaktorRule = "FaktorRule";
			public const string FaktorRuleDisplay = "FaktorRuleDisplay";
			public const string SalaryComponentRoundingID = "SalaryComponentRoundingID";
			public const string DisplayInPaySlip = "DisplayInPaySlip";
			public const string DisplayInPayRekapReport = "DisplayInPayRekapReport";
			public const string IsOrganizationUnit = "IsOrganizationUnit";
			public const string IsEmployeeStatus = "IsEmployeeStatus";
			public const string IsPosition = "IsPosition";
			public const string IsReligion = "IsReligion";
			public const string IsEmployee = "IsEmployee";
			public const string IsEmploymentType = "IsEmploymentType";
			public const string IsPositionGrade = "IsPositionGrade";
			public const string IsMaritalStatus = "IsMaritalStatus";
			public const string IsServiceYear = "IsServiceYear";
			public const string IsSalaryTableNumber = "IsSalaryTableNumber";
			public const string IsEmployeeGrade = "IsEmployeeGrade";
			public const string IsNoOfDependent = "IsNoOfDependent";
			public const string IsAttedanceMatrixID = "IsAttedanceMatrixID";
			public const string IsComponent1 = "IsComponent1";
			public const string IsComponent2 = "IsComponent2";
			public const string IsComponent3 = "IsComponent3";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsKWI = "IsKWI";
			public const string IsEducationLevel = "IsEducationLevel";
			public const string IsEmployeeType = "IsEmployeeType";
			public const string IsServiceUnitID = "IsServiceUnitID";
			public const string MinAmount = "MinAmount";
			public const string MaxAmount = "MaxAmount";
			public const string IsPeriodicSalary = "IsPeriodicSalary";
			public const string IsThr = "IsThr";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string NormalBalance = "NormalBalance";
			public const string IsDisplayInThrSlip = "IsDisplayInThrSlip";
			public const string ChartOfAccountIdThr = "ChartOfAccountIdThr";
			public const string SubLedgerId = "SubLedgerId";
			public const string SubLedgerIdThr = "SubLedgerIdThr";
			public const string NormalBalanceThr = "NormalBalanceThr";
			public const string SRSalaryComponentGroup = "SRSalaryComponentGroup";
			public const string ChartOfAccountIdIndirect = "ChartOfAccountIdIndirect";
			public const string NormalBalanceIndirect = "NormalBalanceIndirect";
			public const string SubLedgerIdIndirect = "SubLedgerIdIndirect";
			public const string ChartOfAccountIdThrIndirect = "ChartOfAccountIdThrIndirect";
			public const string NormalBalanceThrIndirect = "NormalBalanceThrIndirect";
			public const string SubLedgerIdThrIndirect = "SubLedgerIdThrIndirect";
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
			lock (typeof(SalaryComponentMetadata))
			{
				if (SalaryComponentMetadata.mapDelegates == null)
				{
					SalaryComponentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SalaryComponentMetadata.meta == null)
				{
					SalaryComponentMetadata.meta = new SalaryComponentMetadata();
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

				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SalaryComponentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalaryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalaryCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncomeTaxMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDeductionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRJamsostekType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FaktorRule", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("FaktorRuleDisplay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryComponentRoundingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DisplayInPaySlip", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DisplayInPayRekapReport", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOrganizationUnit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployeeStatus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPosition", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReligion", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployee", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmploymentType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPositionGrade", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMaritalStatus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsServiceYear", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSalaryTableNumber", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployeeGrade", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNoOfDependent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAttedanceMatrixID", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsComponent1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsComponent2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsComponent3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsKWI", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEducationLevel", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmployeeType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsServiceUnitID", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("MinAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPeriodicSalary", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsThr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NormalBalance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDisplayInThrSlip", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdThr", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerIdThr", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NormalBalanceThr", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalaryComponentGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdIndirect", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NormalBalanceIndirect", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubLedgerIdIndirect", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdThrIndirect", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NormalBalanceThrIndirect", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubLedgerIdThrIndirect", new esTypeMap("int", "System.Int32"));


				meta.Source = "SalaryComponent";
				meta.Destination = "SalaryComponent";
				meta.spInsert = "proc_SalaryComponentInsert";
				meta.spUpdate = "proc_SalaryComponentUpdate";
				meta.spDelete = "proc_SalaryComponentDelete";
				meta.spLoadAll = "proc_SalaryComponentLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryComponentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryComponentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
