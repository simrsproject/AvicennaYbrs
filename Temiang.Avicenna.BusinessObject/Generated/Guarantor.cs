/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/10/2023 5:09:48 PM
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
	abstract public class esGuarantorCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "GuarantorCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorQuery query)
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
			this.InitQuery(query as esGuarantorQuery);
		}
		#endregion

		virtual public Guarantor DetachEntity(Guarantor entity)
		{
			return base.DetachEntity(entity) as Guarantor;
		}

		virtual public Guarantor AttachEntity(Guarantor entity)
		{
			return base.AttachEntity(entity) as Guarantor;
		}

		virtual public void Combine(GuarantorCollection collection)
		{
			base.Combine(collection);
		}

		new public Guarantor this[int index]
		{
			get
			{
				return base[index] as Guarantor;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Guarantor);
		}
	}

	[Serializable]
	abstract public class esGuarantor : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantor()
		{
		}

		public esGuarantor(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String guarantorID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID);
		}

		private bool LoadByPrimaryKeyDynamic(String guarantorID)
		{
			esGuarantorQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String guarantorID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID", guarantorID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "GuarantorName": this.str.GuarantorName = (string)value; break;
						case "ShortName": this.str.ShortName = (string)value; break;
						case "SRGuarantorType": this.str.SRGuarantorType = (string)value; break;
						case "ContractNumber": this.str.ContractNumber = (string)value; break;
						case "ContractStart": this.str.ContractStart = (string)value; break;
						case "ContractEnd": this.str.ContractEnd = (string)value; break;
						case "ContractSummary": this.str.ContractSummary = (string)value; break;
						case "ContactPerson": this.str.ContactPerson = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "SRBusinessMethod": this.str.SRBusinessMethod = (string)value; break;
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "SRGuarantorRuleType": this.str.SRGuarantorRuleType = (string)value; break;
						case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;
						case "AmountValue": this.str.AmountValue = (string)value; break;
						case "AdminPercentage": this.str.AdminPercentage = (string)value; break;
						case "AdminAmountLimit": this.str.AdminAmountLimit = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "State": this.str.State = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsIncludeItemMedical": this.str.IsIncludeItemMedical = (string)value; break;
						case "IsIncludeItemNonMedical": this.str.IsIncludeItemNonMedical = (string)value; break;
						case "IsIncludeItemOptic": this.str.IsIncludeItemOptic = (string)value; break;
						case "IsIncludeItemMedicalToGuarantor": this.str.IsIncludeItemMedicalToGuarantor = (string)value; break;
						case "IsIncludeItemNonMedicalToGuarantor": this.str.IsIncludeItemNonMedicalToGuarantor = (string)value; break;
						case "IsIncludeItemOpticToGuarantor": this.str.IsIncludeItemOpticToGuarantor = (string)value; break;
						case "IsCoverInpatient": this.str.IsCoverInpatient = (string)value; break;
						case "IsCoverOutpatient": this.str.IsCoverOutpatient = (string)value; break;
						case "ItemMedicMarginPercentage": this.str.ItemMedicMarginPercentage = (string)value; break;
						case "ItemMedicMarginID": this.str.ItemMedicMarginID = (string)value; break;
						case "ItemNonMedicMarginPercentage": this.str.ItemNonMedicMarginPercentage = (string)value; break;
						case "ItemNonMedicMarginID": this.str.ItemNonMedicMarginID = (string)value; break;
						case "GuarantorHeaderID": this.str.GuarantorHeaderID = (string)value; break;
						case "IsIncludeAdminValue": this.str.IsIncludeAdminValue = (string)value; break;
						case "AdminValueMinimum": this.str.AdminValueMinimum = (string)value; break;
						case "IsGlobalPlafond": this.str.IsGlobalPlafond = (string)value; break;
						case "IsAdminFromTotal": this.str.IsAdminFromTotal = (string)value; break;
						case "AdminPercentageOp": this.str.AdminPercentageOp = (string)value; break;
						case "AdminAmountLimitOp": this.str.AdminAmountLimitOp = (string)value; break;
						case "AdminValueMinimumOp": this.str.AdminValueMinimumOp = (string)value; break;
						case "ChartOfAccountIdTemporary": this.str.ChartOfAccountIdTemporary = (string)value; break;
						case "SubledgerIdTemporary": this.str.SubledgerIdTemporary = (string)value; break;
						case "IsItemRuleUsingDefaultAmountValue": this.str.IsItemRuleUsingDefaultAmountValue = (string)value; break;
						case "OutpatientAmountValue": this.str.OutpatientAmountValue = (string)value; break;
						case "EmergencyAmountValue": this.str.EmergencyAmountValue = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPhysicianFeeType": this.str.SRPhysicianFeeType = (string)value; break;
						case "ChartOfAccountIdDeposit": this.str.ChartOfAccountIdDeposit = (string)value; break;
						case "SubledgerIdDeposit": this.str.SubledgerIdDeposit = (string)value; break;
						case "TermOfPayment": this.str.TermOfPayment = (string)value; break;
						case "ChartOfAccountIdOverPayment": this.str.ChartOfAccountIdOverPayment = (string)value; break;
						case "SubledgerIdOverPayment": this.str.SubledgerIdOverPayment = (string)value; break;
						case "SRPhysicianFeeCategory": this.str.SRPhysicianFeeCategory = (string)value; break;
						case "ChartOfAccountIdCostParamedicFee": this.str.ChartOfAccountIdCostParamedicFee = (string)value; break;
						case "SubledgerIdCostParamedicFee": this.str.SubledgerIdCostParamedicFee = (string)value; break;
						case "IsExcessPlafonToDiscount": this.str.IsExcessPlafonToDiscount = (string)value; break;
						case "VirtualAccountNo": this.str.VirtualAccountNo = (string)value; break;
						case "ReportRLID": this.str.ReportRLID = (string)value; break;
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
						case "IsCoverAllAdminCosts": this.str.IsCoverAllAdminCosts = (string)value; break;
						case "SRGuarantorIncomeGroup": this.str.SRGuarantorIncomeGroup = (string)value; break;
						case "PrescriptionServiceUnitIdIPR": this.str.PrescriptionServiceUnitIdIPR = (string)value; break;
						case "PrescriptionLocationIdIPR": this.str.PrescriptionLocationIdIPR = (string)value; break;
						case "PrescriptionServiceUnitIdOPR": this.str.PrescriptionServiceUnitIdOPR = (string)value; break;
						case "PrescriptionLocationIdOPR": this.str.PrescriptionLocationIdOPR = (string)value; break;
						case "PrescriptionServiceUnitIdEMR": this.str.PrescriptionServiceUnitIdEMR = (string)value; break;
						case "PrescriptionLocationIdEMR": this.str.PrescriptionLocationIdEMR = (string)value; break;
						case "IsItemRestrictionsFornas": this.str.IsItemRestrictionsFornas = (string)value; break;
						case "IsProrateParamedicFee": this.str.IsProrateParamedicFee = (string)value; break;
						case "TariffCalculationMethod": this.str.TariffCalculationMethod = (string)value; break;
						case "IsAdminCalcBeforeDiscount": this.str.IsAdminCalcBeforeDiscount = (string)value; break;
						case "NoteCompanyList": this.str.NoteCompanyList = (string)value; break;
						case "IsParamedicFeeRemun": this.str.IsParamedicFeeRemun = (string)value; break;
						case "RoundingTransaction": this.str.RoundingTransaction = (string)value; break;
						case "IsUsingRoundingDown": this.str.IsUsingRoundingDown = (string)value; break;
						case "CreditLimit": this.str.CreditLimit = (string)value; break;
						case "CreditAmount": this.str.CreditAmount = (string)value; break;
						case "VirtualAccountBank": this.str.VirtualAccountBank = (string)value; break;
						case "VirtualAccountName": this.str.VirtualAccountName = (string)value; break;
						case "IsItemRestrictionsFormularium": this.str.IsItemRestrictionsFormularium = (string)value; break;
						case "IsItemRestrictionsGeneric": this.str.IsItemRestrictionsGeneric = (string)value; break;
						case "IsItemRestrictionsNonGeneric": this.str.IsItemRestrictionsNonGeneric = (string)value; break;
						case "IsItemRestrictionsNonGenericLimited": this.str.IsItemRestrictionsNonGenericLimited = (string)value; break;
						case "IsFeeBrutoFromFeeAmount": this.str.IsFeeBrutoFromFeeAmount = (string)value; break;
						case "ChartOfAccountIdIPR": this.str.ChartOfAccountIdIPR = (string)value; break;
						case "SubLedgerIdIPR": this.str.SubLedgerIdIPR = (string)value; break;
						case "ChartOfAccountIdTemporaryIPR": this.str.ChartOfAccountIdTemporaryIPR = (string)value; break;
						case "SubledgerIdTemporaryIPR": this.str.SubledgerIdTemporaryIPR = (string)value; break;
						case "ChartOfAccountIdOverPaymentMin": this.str.ChartOfAccountIdOverPaymentMin = (string)value; break;
						case "SubledgerIdOverPaymentMin": this.str.SubledgerIdOverPaymentMin = (string)value; break;
						case "IsDiscountProrataToRevenue": this.str.IsDiscountProrataToRevenue = (string)value; break;
						case "RecipeMarginValueNonCompound": this.str.RecipeMarginValueNonCompound = (string)value; break;
						case "IsUsingDefaultRecipeAmount": this.str.IsUsingDefaultRecipeAmount = (string)value; break;
						case "IsItemServiceRestrictionStatusAllowed": this.str.IsItemServiceRestrictionStatusAllowed = (string)value; break;
						case "IsItemLabRestrictionStatusAllowed": this.str.IsItemLabRestrictionStatusAllowed = (string)value; break;
						case "IsItemRadRestrictionStatusAllowed": this.str.IsItemRadRestrictionStatusAllowed = (string)value; break;
						case "IsItemProductRestrictionStatusAllowed": this.str.IsItemProductRestrictionStatusAllowed = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ContractStart":

							if (value == null || value is System.DateTime)
								this.ContractStart = (System.DateTime?)value;
							break;
						case "ContractEnd":

							if (value == null || value is System.DateTime)
								this.ContractEnd = (System.DateTime?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsValueInPercent":

							if (value == null || value is System.Boolean)
								this.IsValueInPercent = (System.Boolean?)value;
							break;
						case "AmountValue":

							if (value == null || value is System.Decimal)
								this.AmountValue = (System.Decimal?)value;
							break;
						case "AdminPercentage":

							if (value == null || value is System.Decimal)
								this.AdminPercentage = (System.Decimal?)value;
							break;
						case "AdminAmountLimit":

							if (value == null || value is System.Decimal)
								this.AdminAmountLimit = (System.Decimal?)value;
							break;
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsIncludeItemMedical":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemMedical = (System.Boolean?)value;
							break;
						case "IsIncludeItemNonMedical":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemNonMedical = (System.Boolean?)value;
							break;
						case "IsIncludeItemOptic":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemOptic = (System.Boolean?)value;
							break;
						case "IsIncludeItemMedicalToGuarantor":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemMedicalToGuarantor = (System.Boolean?)value;
							break;
						case "IsIncludeItemNonMedicalToGuarantor":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemNonMedicalToGuarantor = (System.Boolean?)value;
							break;
						case "IsIncludeItemOpticToGuarantor":

							if (value == null || value is System.Boolean)
								this.IsIncludeItemOpticToGuarantor = (System.Boolean?)value;
							break;
						case "IsCoverInpatient":

							if (value == null || value is System.Boolean)
								this.IsCoverInpatient = (System.Boolean?)value;
							break;
						case "IsCoverOutpatient":

							if (value == null || value is System.Boolean)
								this.IsCoverOutpatient = (System.Boolean?)value;
							break;
						case "ItemMedicMarginPercentage":

							if (value == null || value is System.Decimal)
								this.ItemMedicMarginPercentage = (System.Decimal?)value;
							break;
						case "ItemNonMedicMarginPercentage":

							if (value == null || value is System.Decimal)
								this.ItemNonMedicMarginPercentage = (System.Decimal?)value;
							break;
						case "IsIncludeAdminValue":

							if (value == null || value is System.Boolean)
								this.IsIncludeAdminValue = (System.Boolean?)value;
							break;
						case "AdminValueMinimum":

							if (value == null || value is System.Decimal)
								this.AdminValueMinimum = (System.Decimal?)value;
							break;
						case "IsGlobalPlafond":

							if (value == null || value is System.Boolean)
								this.IsGlobalPlafond = (System.Boolean?)value;
							break;
						case "IsAdminFromTotal":

							if (value == null || value is System.Boolean)
								this.IsAdminFromTotal = (System.Boolean?)value;
							break;
						case "AdminPercentageOp":

							if (value == null || value is System.Decimal)
								this.AdminPercentageOp = (System.Decimal?)value;
							break;
						case "AdminAmountLimitOp":

							if (value == null || value is System.Decimal)
								this.AdminAmountLimitOp = (System.Decimal?)value;
							break;
						case "AdminValueMinimumOp":

							if (value == null || value is System.Decimal)
								this.AdminValueMinimumOp = (System.Decimal?)value;
							break;
						case "ChartOfAccountIdTemporary":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdTemporary = (System.Int32?)value;
							break;
						case "SubledgerIdTemporary":

							if (value == null || value is System.Int32)
								this.SubledgerIdTemporary = (System.Int32?)value;
							break;
						case "IsItemRuleUsingDefaultAmountValue":

							if (value == null || value is System.Boolean)
								this.IsItemRuleUsingDefaultAmountValue = (System.Boolean?)value;
							break;
						case "OutpatientAmountValue":

							if (value == null || value is System.Decimal)
								this.OutpatientAmountValue = (System.Decimal?)value;
							break;
						case "EmergencyAmountValue":

							if (value == null || value is System.Decimal)
								this.EmergencyAmountValue = (System.Decimal?)value;
							break;
						case "ChartOfAccountIdDeposit":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDeposit = (System.Int32?)value;
							break;
						case "SubledgerIdDeposit":

							if (value == null || value is System.Int32)
								this.SubledgerIdDeposit = (System.Int32?)value;
							break;
						case "TermOfPayment":

							if (value == null || value is System.Int32)
								this.TermOfPayment = (System.Int32?)value;
							break;
						case "ChartOfAccountIdOverPayment":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdOverPayment = (System.Int32?)value;
							break;
						case "SubledgerIdOverPayment":

							if (value == null || value is System.Int32)
								this.SubledgerIdOverPayment = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCostParamedicFee":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCostParamedicFee = (System.Int32?)value;
							break;
						case "SubledgerIdCostParamedicFee":

							if (value == null || value is System.Int32)
								this.SubledgerIdCostParamedicFee = (System.Int32?)value;
							break;
						case "IsExcessPlafonToDiscount":

							if (value == null || value is System.Boolean)
								this.IsExcessPlafonToDiscount = (System.Boolean?)value;
							break;
						case "RlMasterReportItemID":

							if (value == null || value is System.Int32)
								this.RlMasterReportItemID = (System.Int32?)value;
							break;
						case "IsCoverAllAdminCosts":

							if (value == null || value is System.Boolean)
								this.IsCoverAllAdminCosts = (System.Boolean?)value;
							break;
						case "IsItemRestrictionsFornas":

							if (value == null || value is System.Boolean)
								this.IsItemRestrictionsFornas = (System.Boolean?)value;
							break;
						case "IsProrateParamedicFee":

							if (value == null || value is System.Boolean)
								this.IsProrateParamedicFee = (System.Boolean?)value;
							break;
						case "TariffCalculationMethod":

							if (value == null || value is System.Byte)
								this.TariffCalculationMethod = (System.Byte?)value;
							break;
						case "IsAdminCalcBeforeDiscount":

							if (value == null || value is System.Boolean)
								this.IsAdminCalcBeforeDiscount = (System.Boolean?)value;
							break;
						case "IsParamedicFeeRemun":

							if (value == null || value is System.Boolean)
								this.IsParamedicFeeRemun = (System.Boolean?)value;
							break;
						case "RoundingTransaction":

							if (value == null || value is System.Decimal)
								this.RoundingTransaction = (System.Decimal?)value;
							break;
						case "IsUsingRoundingDown":

							if (value == null || value is System.Boolean)
								this.IsUsingRoundingDown = (System.Boolean?)value;
							break;
						case "CreditLimit":

							if (value == null || value is System.Decimal)
								this.CreditLimit = (System.Decimal?)value;
							break;
						case "CreditAmount":

							if (value == null || value is System.Decimal)
								this.CreditAmount = (System.Decimal?)value;
							break;
						case "IsItemRestrictionsFormularium":

							if (value == null || value is System.Boolean)
								this.IsItemRestrictionsFormularium = (System.Boolean?)value;
							break;
						case "IsItemRestrictionsGeneric":

							if (value == null || value is System.Boolean)
								this.IsItemRestrictionsGeneric = (System.Boolean?)value;
							break;
						case "IsItemRestrictionsNonGeneric":

							if (value == null || value is System.Boolean)
								this.IsItemRestrictionsNonGeneric = (System.Boolean?)value;
							break;
						case "IsItemRestrictionsNonGenericLimited":

							if (value == null || value is System.Boolean)
								this.IsItemRestrictionsNonGenericLimited = (System.Boolean?)value;
							break;
						case "IsFeeBrutoFromFeeAmount":

							if (value == null || value is System.Boolean)
								this.IsFeeBrutoFromFeeAmount = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdIPR":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIPR = (System.Int32?)value;
							break;
						case "SubLedgerIdIPR":

							if (value == null || value is System.Int32)
								this.SubLedgerIdIPR = (System.Int32?)value;
							break;
						case "ChartOfAccountIdTemporaryIPR":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdTemporaryIPR = (System.Int32?)value;
							break;
						case "SubledgerIdTemporaryIPR":

							if (value == null || value is System.Int32)
								this.SubledgerIdTemporaryIPR = (System.Int32?)value;
							break;
						case "ChartOfAccountIdOverPaymentMin":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdOverPaymentMin = (System.Int32?)value;
							break;
						case "SubledgerIdOverPaymentMin":

							if (value == null || value is System.Int32)
								this.SubledgerIdOverPaymentMin = (System.Int32?)value;
							break;
						case "IsDiscountProrataToRevenue":

							if (value == null || value is System.Boolean)
								this.IsDiscountProrataToRevenue = (System.Boolean?)value;
							break;
						case "RecipeMarginValueNonCompound":

							if (value == null || value is System.Decimal)
								this.RecipeMarginValueNonCompound = (System.Decimal?)value;
							break;
						case "IsUsingDefaultRecipeAmount":

							if (value == null || value is System.Boolean)
								this.IsUsingDefaultRecipeAmount = (System.Boolean?)value;
							break;
						case "IsItemServiceRestrictionStatusAllowed":

							if (value == null || value is System.Boolean)
								this.IsItemServiceRestrictionStatusAllowed = (System.Boolean?)value;
							break;
						case "IsItemLabRestrictionStatusAllowed":

							if (value == null || value is System.Boolean)
								this.IsItemLabRestrictionStatusAllowed = (System.Boolean?)value;
							break;
						case "IsItemRadRestrictionStatusAllowed":

							if (value == null || value is System.Boolean)
								this.IsItemRadRestrictionStatusAllowed = (System.Boolean?)value;
							break;
						case "IsItemProductRestrictionStatusAllowed":

							if (value == null || value is System.Boolean)
								this.IsItemProductRestrictionStatusAllowed = (System.Boolean?)value;
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
		/// Maps to Guarantor.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.GuarantorName
		/// </summary>
		virtual public System.String GuarantorName
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.GuarantorName);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.GuarantorName, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ShortName);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ShortName, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRGuarantorType
		/// </summary>
		virtual public System.String SRGuarantorType
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorType);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorType, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ContractNumber
		/// </summary>
		virtual public System.String ContractNumber
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ContractNumber);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ContractNumber, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ContractStart
		/// </summary>
		virtual public System.DateTime? ContractStart
		{
			get
			{
				return base.GetSystemDateTime(GuarantorMetadata.ColumnNames.ContractStart);
			}

			set
			{
				base.SetSystemDateTime(GuarantorMetadata.ColumnNames.ContractStart, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ContractEnd
		/// </summary>
		virtual public System.DateTime? ContractEnd
		{
			get
			{
				return base.GetSystemDateTime(GuarantorMetadata.ColumnNames.ContractEnd);
			}

			set
			{
				base.SetSystemDateTime(GuarantorMetadata.ColumnNames.ContractEnd, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ContractSummary
		/// </summary>
		virtual public System.String ContractSummary
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ContractSummary);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ContractSummary, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ContactPerson);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ContactPerson, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRBusinessMethod
		/// </summary>
		virtual public System.String SRBusinessMethod
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRBusinessMethod);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRBusinessMethod, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRTariffType);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRGuarantorRuleType
		/// </summary>
		virtual public System.String SRGuarantorRuleType
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorRuleType);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorRuleType, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsValueInPercent
		/// </summary>
		virtual public System.Boolean? IsValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsValueInPercent);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsValueInPercent, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AmountValue
		/// </summary>
		virtual public System.Decimal? AmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AmountValue);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AmountValue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminPercentage
		/// </summary>
		virtual public System.Decimal? AdminPercentage
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminPercentage);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminAmountLimit
		/// </summary>
		virtual public System.Decimal? AdminAmountLimit
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminAmountLimit);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminAmountLimit, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.StreetName);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.State);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.FaxNo);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.MobilePhoneNo);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(GuarantorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemMedical
		/// </summary>
		virtual public System.Boolean? IsIncludeItemMedical
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemMedical);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemMedical, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemNonMedical
		/// </summary>
		virtual public System.Boolean? IsIncludeItemNonMedical
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedical);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedical, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemOptic
		/// </summary>
		virtual public System.Boolean? IsIncludeItemOptic
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemOptic);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemOptic, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemMedicalToGuarantor
		/// </summary>
		virtual public System.Boolean? IsIncludeItemMedicalToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemMedicalToGuarantor);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemMedicalToGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemNonMedicalToGuarantor
		/// </summary>
		virtual public System.Boolean? IsIncludeItemNonMedicalToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedicalToGuarantor);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedicalToGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeItemOpticToGuarantor
		/// </summary>
		virtual public System.Boolean? IsIncludeItemOpticToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemOpticToGuarantor);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeItemOpticToGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsCoverInpatient
		/// </summary>
		virtual public System.Boolean? IsCoverInpatient
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverInpatient);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverInpatient, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsCoverOutpatient
		/// </summary>
		virtual public System.Boolean? IsCoverOutpatient
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverOutpatient);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverOutpatient, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ItemMedicMarginPercentage
		/// </summary>
		virtual public System.Decimal? ItemMedicMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.ItemMedicMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.ItemMedicMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ItemMedicMarginID
		/// </summary>
		virtual public System.String ItemMedicMarginID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ItemMedicMarginID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ItemMedicMarginID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ItemNonMedicMarginPercentage
		/// </summary>
		virtual public System.Decimal? ItemNonMedicMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.ItemNonMedicMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.ItemNonMedicMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ItemNonMedicMarginID
		/// </summary>
		virtual public System.String ItemNonMedicMarginID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ItemNonMedicMarginID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ItemNonMedicMarginID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.GuarantorHeaderID
		/// </summary>
		virtual public System.String GuarantorHeaderID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.GuarantorHeaderID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.GuarantorHeaderID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsIncludeAdminValue
		/// </summary>
		virtual public System.Boolean? IsIncludeAdminValue
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeAdminValue);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsIncludeAdminValue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminValueMinimum
		/// </summary>
		virtual public System.Decimal? AdminValueMinimum
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminValueMinimum);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminValueMinimum, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsGlobalPlafond
		/// </summary>
		virtual public System.Boolean? IsGlobalPlafond
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsGlobalPlafond);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsGlobalPlafond, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsAdminFromTotal
		/// </summary>
		virtual public System.Boolean? IsAdminFromTotal
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsAdminFromTotal);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsAdminFromTotal, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminPercentageOp
		/// </summary>
		virtual public System.Decimal? AdminPercentageOp
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminPercentageOp);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminPercentageOp, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminAmountLimitOp
		/// </summary>
		virtual public System.Decimal? AdminAmountLimitOp
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminAmountLimitOp);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminAmountLimitOp, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.AdminValueMinimumOp
		/// </summary>
		virtual public System.Decimal? AdminValueMinimumOp
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.AdminValueMinimumOp);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.AdminValueMinimumOp, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdTemporary
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdTemporary
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporary);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporary, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdTemporary
		/// </summary>
		virtual public System.Int32? SubledgerIdTemporary
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdTemporary);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdTemporary, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRuleUsingDefaultAmountValue
		/// </summary>
		virtual public System.Boolean? IsItemRuleUsingDefaultAmountValue
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRuleUsingDefaultAmountValue);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRuleUsingDefaultAmountValue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.OutpatientAmountValue
		/// </summary>
		virtual public System.Decimal? OutpatientAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.OutpatientAmountValue);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.OutpatientAmountValue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.EmergencyAmountValue
		/// </summary>
		virtual public System.Decimal? EmergencyAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.EmergencyAmountValue);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.EmergencyAmountValue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRPaymentType);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRPhysicianFeeType
		/// </summary>
		virtual public System.String SRPhysicianFeeType
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRPhysicianFeeType);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRPhysicianFeeType, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdDeposit
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDeposit
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdDeposit);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdDeposit, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdDeposit
		/// </summary>
		virtual public System.Int32? SubledgerIdDeposit
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdDeposit);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdDeposit, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.TermOfPayment
		/// </summary>
		virtual public System.Int32? TermOfPayment
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.TermOfPayment);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.TermOfPayment, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdOverPayment
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdOverPayment
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPayment);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPayment, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdOverPayment
		/// </summary>
		virtual public System.Int32? SubledgerIdOverPayment
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdOverPayment);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdOverPayment, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRPhysicianFeeCategory
		/// </summary>
		virtual public System.String SRPhysicianFeeCategory
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRPhysicianFeeCategory);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRPhysicianFeeCategory, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdCostParamedicFee
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCostParamedicFee
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdCostParamedicFee
		/// </summary>
		virtual public System.Int32? SubledgerIdCostParamedicFee
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdCostParamedicFee);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdCostParamedicFee, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsExcessPlafonToDiscount
		/// </summary>
		virtual public System.Boolean? IsExcessPlafonToDiscount
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsExcessPlafonToDiscount);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsExcessPlafonToDiscount, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.VirtualAccountNo
		/// </summary>
		virtual public System.String VirtualAccountNo
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountNo);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountNo, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ReportRLID
		/// </summary>
		virtual public System.String ReportRLID
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.ReportRLID);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.ReportRLID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.RlMasterReportItemID);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsCoverAllAdminCosts
		/// </summary>
		virtual public System.Boolean? IsCoverAllAdminCosts
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverAllAdminCosts);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsCoverAllAdminCosts, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SRGuarantorIncomeGroup
		/// </summary>
		virtual public System.String SRGuarantorIncomeGroup
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorIncomeGroup);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.SRGuarantorIncomeGroup, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionServiceUnitIdIPR
		/// </summary>
		virtual public System.String PrescriptionServiceUnitIdIPR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdIPR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionLocationIdIPR
		/// </summary>
		virtual public System.String PrescriptionLocationIdIPR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdIPR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionServiceUnitIdOPR
		/// </summary>
		virtual public System.String PrescriptionServiceUnitIdOPR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdOPR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdOPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionLocationIdOPR
		/// </summary>
		virtual public System.String PrescriptionLocationIdOPR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdOPR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdOPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionServiceUnitIdEMR
		/// </summary>
		virtual public System.String PrescriptionServiceUnitIdEMR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdEMR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdEMR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.PrescriptionLocationIdEMR
		/// </summary>
		virtual public System.String PrescriptionLocationIdEMR
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdEMR);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.PrescriptionLocationIdEMR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRestrictionsFornas
		/// </summary>
		virtual public System.Boolean? IsItemRestrictionsFornas
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsFornas);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsFornas, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsProrateParamedicFee
		/// </summary>
		virtual public System.Boolean? IsProrateParamedicFee
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsProrateParamedicFee);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsProrateParamedicFee, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.TariffCalculationMethod
		/// </summary>
		virtual public System.Byte? TariffCalculationMethod
		{
			get
			{
				return base.GetSystemByte(GuarantorMetadata.ColumnNames.TariffCalculationMethod);
			}

			set
			{
				base.SetSystemByte(GuarantorMetadata.ColumnNames.TariffCalculationMethod, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsAdminCalcBeforeDiscount
		/// </summary>
		virtual public System.Boolean? IsAdminCalcBeforeDiscount
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsAdminCalcBeforeDiscount);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsAdminCalcBeforeDiscount, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.NoteCompanyList
		/// </summary>
		virtual public System.String NoteCompanyList
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.NoteCompanyList);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.NoteCompanyList, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsParamedicFeeRemun
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeRemun
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsParamedicFeeRemun);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsParamedicFeeRemun, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.RoundingTransaction
		/// </summary>
		virtual public System.Decimal? RoundingTransaction
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.RoundingTransaction);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.RoundingTransaction, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsUsingRoundingDown
		/// </summary>
		virtual public System.Boolean? IsUsingRoundingDown
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsUsingRoundingDown);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsUsingRoundingDown, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.CreditLimit
		/// </summary>
		virtual public System.Decimal? CreditLimit
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.CreditLimit);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.CreditLimit, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.CreditAmount);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.CreditAmount, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.VirtualAccountBank
		/// </summary>
		virtual public System.String VirtualAccountBank
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountBank);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountBank, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.VirtualAccountName
		/// </summary>
		virtual public System.String VirtualAccountName
		{
			get
			{
				return base.GetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountName);
			}

			set
			{
				base.SetSystemString(GuarantorMetadata.ColumnNames.VirtualAccountName, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRestrictionsFormularium
		/// </summary>
		virtual public System.Boolean? IsItemRestrictionsFormularium
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsFormularium);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsFormularium, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRestrictionsGeneric
		/// </summary>
		virtual public System.Boolean? IsItemRestrictionsGeneric
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsGeneric);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsGeneric, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRestrictionsNonGeneric
		/// </summary>
		virtual public System.Boolean? IsItemRestrictionsNonGeneric
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGeneric);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGeneric, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRestrictionsNonGenericLimited
		/// </summary>
		virtual public System.Boolean? IsItemRestrictionsNonGenericLimited
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGenericLimited);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGenericLimited, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsFeeBrutoFromFeeAmount
		/// </summary>
		virtual public System.Boolean? IsFeeBrutoFromFeeAmount
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsFeeBrutoFromFeeAmount);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsFeeBrutoFromFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdIPR
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIPR
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdIPR);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubLedgerIdIPR
		/// </summary>
		virtual public System.Int32? SubLedgerIdIPR
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubLedgerIdIPR);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubLedgerIdIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdTemporaryIPR
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdTemporaryIPR
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporaryIPR);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporaryIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdTemporaryIPR
		/// </summary>
		virtual public System.Int32? SubledgerIdTemporaryIPR
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdTemporaryIPR);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdTemporaryIPR, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.ChartOfAccountIdOverPaymentMin
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdOverPaymentMin
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPaymentMin);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPaymentMin, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.SubledgerIdOverPaymentMin
		/// </summary>
		virtual public System.Int32? SubledgerIdOverPaymentMin
		{
			get
			{
				return base.GetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdOverPaymentMin);
			}

			set
			{
				base.SetSystemInt32(GuarantorMetadata.ColumnNames.SubledgerIdOverPaymentMin, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsDiscountProrataToRevenue
		/// </summary>
		virtual public System.Boolean? IsDiscountProrataToRevenue
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsDiscountProrataToRevenue);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsDiscountProrataToRevenue, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.RecipeMarginValueNonCompound
		/// </summary>
		virtual public System.Decimal? RecipeMarginValueNonCompound
		{
			get
			{
				return base.GetSystemDecimal(GuarantorMetadata.ColumnNames.RecipeMarginValueNonCompound);
			}

			set
			{
				base.SetSystemDecimal(GuarantorMetadata.ColumnNames.RecipeMarginValueNonCompound, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsUsingDefaultRecipeAmount
		/// </summary>
		virtual public System.Boolean? IsUsingDefaultRecipeAmount
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsUsingDefaultRecipeAmount);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsUsingDefaultRecipeAmount, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemServiceRestrictionStatusAllowed
		/// </summary>
		virtual public System.Boolean? IsItemServiceRestrictionStatusAllowed
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemServiceRestrictionStatusAllowed);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemServiceRestrictionStatusAllowed, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemLabRestrictionStatusAllowed
		/// </summary>
		virtual public System.Boolean? IsItemLabRestrictionStatusAllowed
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemLabRestrictionStatusAllowed);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemLabRestrictionStatusAllowed, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemRadRestrictionStatusAllowed
		/// </summary>
		virtual public System.Boolean? IsItemRadRestrictionStatusAllowed
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRadRestrictionStatusAllowed);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemRadRestrictionStatusAllowed, value);
			}
		}
		/// <summary>
		/// Maps to Guarantor.IsItemProductRestrictionStatusAllowed
		/// </summary>
		virtual public System.Boolean? IsItemProductRestrictionStatusAllowed
		{
			get
			{
				return base.GetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemProductRestrictionStatusAllowed);
			}

			set
			{
				base.SetSystemBoolean(GuarantorMetadata.ColumnNames.IsItemProductRestrictionStatusAllowed, value);
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
			public esStrings(esGuarantor entity)
			{
				this.entity = entity;
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
			public System.String GuarantorName
			{
				get
				{
					System.String data = entity.GuarantorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorName = null;
					else entity.GuarantorName = Convert.ToString(value);
				}
			}
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
			public System.String SRGuarantorType
			{
				get
				{
					System.String data = entity.SRGuarantorType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorType = null;
					else entity.SRGuarantorType = Convert.ToString(value);
				}
			}
			public System.String ContractNumber
			{
				get
				{
					System.String data = entity.ContractNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractNumber = null;
					else entity.ContractNumber = Convert.ToString(value);
				}
			}
			public System.String ContractStart
			{
				get
				{
					System.DateTime? data = entity.ContractStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractStart = null;
					else entity.ContractStart = Convert.ToDateTime(value);
				}
			}
			public System.String ContractEnd
			{
				get
				{
					System.DateTime? data = entity.ContractEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractEnd = null;
					else entity.ContractEnd = Convert.ToDateTime(value);
				}
			}
			public System.String ContractSummary
			{
				get
				{
					System.String data = entity.ContractSummary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractSummary = null;
					else entity.ContractSummary = Convert.ToString(value);
				}
			}
			public System.String ContactPerson
			{
				get
				{
					System.String data = entity.ContactPerson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactPerson = null;
					else entity.ContactPerson = Convert.ToString(value);
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
			public System.String SRBusinessMethod
			{
				get
				{
					System.String data = entity.SRBusinessMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBusinessMethod = null;
					else entity.SRBusinessMethod = Convert.ToString(value);
				}
			}
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
				}
			}
			public System.String SRGuarantorRuleType
			{
				get
				{
					System.String data = entity.SRGuarantorRuleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorRuleType = null;
					else entity.SRGuarantorRuleType = Convert.ToString(value);
				}
			}
			public System.String IsValueInPercent
			{
				get
				{
					System.Boolean? data = entity.IsValueInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValueInPercent = null;
					else entity.IsValueInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String AmountValue
			{
				get
				{
					System.Decimal? data = entity.AmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountValue = null;
					else entity.AmountValue = Convert.ToDecimal(value);
				}
			}
			public System.String AdminPercentage
			{
				get
				{
					System.Decimal? data = entity.AdminPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminPercentage = null;
					else entity.AdminPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String AdminAmountLimit
			{
				get
				{
					System.Decimal? data = entity.AdminAmountLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminAmountLimit = null;
					else entity.AdminAmountLimit = Convert.ToDecimal(value);
				}
			}
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
			public System.String State
			{
				get
				{
					System.String data = entity.State;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.State = null;
					else entity.State = Convert.ToString(value);
				}
			}
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
				}
			}
			public System.String FaxNo
			{
				get
				{
					System.String data = entity.FaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNo = null;
					else entity.FaxNo = Convert.ToString(value);
				}
			}
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
			public System.String MobilePhoneNo
			{
				get
				{
					System.String data = entity.MobilePhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
					else entity.MobilePhoneNo = Convert.ToString(value);
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
			public System.String IsIncludeItemMedical
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemMedical;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemMedical = null;
					else entity.IsIncludeItemMedical = Convert.ToBoolean(value);
				}
			}
			public System.String IsIncludeItemNonMedical
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemNonMedical;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemNonMedical = null;
					else entity.IsIncludeItemNonMedical = Convert.ToBoolean(value);
				}
			}
			public System.String IsIncludeItemOptic
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemOptic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemOptic = null;
					else entity.IsIncludeItemOptic = Convert.ToBoolean(value);
				}
			}
			public System.String IsIncludeItemMedicalToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemMedicalToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemMedicalToGuarantor = null;
					else entity.IsIncludeItemMedicalToGuarantor = Convert.ToBoolean(value);
				}
			}
			public System.String IsIncludeItemNonMedicalToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemNonMedicalToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemNonMedicalToGuarantor = null;
					else entity.IsIncludeItemNonMedicalToGuarantor = Convert.ToBoolean(value);
				}
			}
			public System.String IsIncludeItemOpticToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsIncludeItemOpticToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeItemOpticToGuarantor = null;
					else entity.IsIncludeItemOpticToGuarantor = Convert.ToBoolean(value);
				}
			}
			public System.String IsCoverInpatient
			{
				get
				{
					System.Boolean? data = entity.IsCoverInpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCoverInpatient = null;
					else entity.IsCoverInpatient = Convert.ToBoolean(value);
				}
			}
			public System.String IsCoverOutpatient
			{
				get
				{
					System.Boolean? data = entity.IsCoverOutpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCoverOutpatient = null;
					else entity.IsCoverOutpatient = Convert.ToBoolean(value);
				}
			}
			public System.String ItemMedicMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.ItemMedicMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemMedicMarginPercentage = null;
					else entity.ItemMedicMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String ItemMedicMarginID
			{
				get
				{
					System.String data = entity.ItemMedicMarginID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemMedicMarginID = null;
					else entity.ItemMedicMarginID = Convert.ToString(value);
				}
			}
			public System.String ItemNonMedicMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.ItemNonMedicMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemNonMedicMarginPercentage = null;
					else entity.ItemNonMedicMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String ItemNonMedicMarginID
			{
				get
				{
					System.String data = entity.ItemNonMedicMarginID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemNonMedicMarginID = null;
					else entity.ItemNonMedicMarginID = Convert.ToString(value);
				}
			}
			public System.String GuarantorHeaderID
			{
				get
				{
					System.String data = entity.GuarantorHeaderID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorHeaderID = null;
					else entity.GuarantorHeaderID = Convert.ToString(value);
				}
			}
			public System.String IsIncludeAdminValue
			{
				get
				{
					System.Boolean? data = entity.IsIncludeAdminValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeAdminValue = null;
					else entity.IsIncludeAdminValue = Convert.ToBoolean(value);
				}
			}
			public System.String AdminValueMinimum
			{
				get
				{
					System.Decimal? data = entity.AdminValueMinimum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminValueMinimum = null;
					else entity.AdminValueMinimum = Convert.ToDecimal(value);
				}
			}
			public System.String IsGlobalPlafond
			{
				get
				{
					System.Boolean? data = entity.IsGlobalPlafond;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGlobalPlafond = null;
					else entity.IsGlobalPlafond = Convert.ToBoolean(value);
				}
			}
			public System.String IsAdminFromTotal
			{
				get
				{
					System.Boolean? data = entity.IsAdminFromTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdminFromTotal = null;
					else entity.IsAdminFromTotal = Convert.ToBoolean(value);
				}
			}
			public System.String AdminPercentageOp
			{
				get
				{
					System.Decimal? data = entity.AdminPercentageOp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminPercentageOp = null;
					else entity.AdminPercentageOp = Convert.ToDecimal(value);
				}
			}
			public System.String AdminAmountLimitOp
			{
				get
				{
					System.Decimal? data = entity.AdminAmountLimitOp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminAmountLimitOp = null;
					else entity.AdminAmountLimitOp = Convert.ToDecimal(value);
				}
			}
			public System.String AdminValueMinimumOp
			{
				get
				{
					System.Decimal? data = entity.AdminValueMinimumOp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdminValueMinimumOp = null;
					else entity.AdminValueMinimumOp = Convert.ToDecimal(value);
				}
			}
			public System.String ChartOfAccountIdTemporary
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdTemporary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdTemporary = null;
					else entity.ChartOfAccountIdTemporary = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdTemporary
			{
				get
				{
					System.Int32? data = entity.SubledgerIdTemporary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdTemporary = null;
					else entity.SubledgerIdTemporary = Convert.ToInt32(value);
				}
			}
			public System.String IsItemRuleUsingDefaultAmountValue
			{
				get
				{
					System.Boolean? data = entity.IsItemRuleUsingDefaultAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRuleUsingDefaultAmountValue = null;
					else entity.IsItemRuleUsingDefaultAmountValue = Convert.ToBoolean(value);
				}
			}
			public System.String OutpatientAmountValue
			{
				get
				{
					System.Decimal? data = entity.OutpatientAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OutpatientAmountValue = null;
					else entity.OutpatientAmountValue = Convert.ToDecimal(value);
				}
			}
			public System.String EmergencyAmountValue
			{
				get
				{
					System.Decimal? data = entity.EmergencyAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmergencyAmountValue = null;
					else entity.EmergencyAmountValue = Convert.ToDecimal(value);
				}
			}
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRPhysicianFeeType
			{
				get
				{
					System.String data = entity.SRPhysicianFeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeType = null;
					else entity.SRPhysicianFeeType = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdDeposit
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDeposit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDeposit = null;
					else entity.ChartOfAccountIdDeposit = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDeposit
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDeposit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDeposit = null;
					else entity.SubledgerIdDeposit = Convert.ToInt32(value);
				}
			}
			public System.String TermOfPayment
			{
				get
				{
					System.Int32? data = entity.TermOfPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermOfPayment = null;
					else entity.TermOfPayment = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdOverPayment
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdOverPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdOverPayment = null;
					else entity.ChartOfAccountIdOverPayment = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdOverPayment
			{
				get
				{
					System.Int32? data = entity.SubledgerIdOverPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdOverPayment = null;
					else entity.SubledgerIdOverPayment = Convert.ToInt32(value);
				}
			}
			public System.String SRPhysicianFeeCategory
			{
				get
				{
					System.String data = entity.SRPhysicianFeeCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeCategory = null;
					else entity.SRPhysicianFeeCategory = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdCostParamedicFee
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCostParamedicFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCostParamedicFee = null;
					else entity.ChartOfAccountIdCostParamedicFee = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCostParamedicFee
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCostParamedicFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCostParamedicFee = null;
					else entity.SubledgerIdCostParamedicFee = Convert.ToInt32(value);
				}
			}
			public System.String IsExcessPlafonToDiscount
			{
				get
				{
					System.Boolean? data = entity.IsExcessPlafonToDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExcessPlafonToDiscount = null;
					else entity.IsExcessPlafonToDiscount = Convert.ToBoolean(value);
				}
			}
			public System.String VirtualAccountNo
			{
				get
				{
					System.String data = entity.VirtualAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VirtualAccountNo = null;
					else entity.VirtualAccountNo = Convert.ToString(value);
				}
			}
			public System.String ReportRLID
			{
				get
				{
					System.String data = entity.ReportRLID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportRLID = null;
					else entity.ReportRLID = Convert.ToString(value);
				}
			}
			public System.String RlMasterReportItemID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
					else entity.RlMasterReportItemID = Convert.ToInt32(value);
				}
			}
			public System.String IsCoverAllAdminCosts
			{
				get
				{
					System.Boolean? data = entity.IsCoverAllAdminCosts;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCoverAllAdminCosts = null;
					else entity.IsCoverAllAdminCosts = Convert.ToBoolean(value);
				}
			}
			public System.String SRGuarantorIncomeGroup
			{
				get
				{
					System.String data = entity.SRGuarantorIncomeGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorIncomeGroup = null;
					else entity.SRGuarantorIncomeGroup = Convert.ToString(value);
				}
			}
			public System.String PrescriptionServiceUnitIdIPR
			{
				get
				{
					System.String data = entity.PrescriptionServiceUnitIdIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionServiceUnitIdIPR = null;
					else entity.PrescriptionServiceUnitIdIPR = Convert.ToString(value);
				}
			}
			public System.String PrescriptionLocationIdIPR
			{
				get
				{
					System.String data = entity.PrescriptionLocationIdIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionLocationIdIPR = null;
					else entity.PrescriptionLocationIdIPR = Convert.ToString(value);
				}
			}
			public System.String PrescriptionServiceUnitIdOPR
			{
				get
				{
					System.String data = entity.PrescriptionServiceUnitIdOPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionServiceUnitIdOPR = null;
					else entity.PrescriptionServiceUnitIdOPR = Convert.ToString(value);
				}
			}
			public System.String PrescriptionLocationIdOPR
			{
				get
				{
					System.String data = entity.PrescriptionLocationIdOPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionLocationIdOPR = null;
					else entity.PrescriptionLocationIdOPR = Convert.ToString(value);
				}
			}
			public System.String PrescriptionServiceUnitIdEMR
			{
				get
				{
					System.String data = entity.PrescriptionServiceUnitIdEMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionServiceUnitIdEMR = null;
					else entity.PrescriptionServiceUnitIdEMR = Convert.ToString(value);
				}
			}
			public System.String PrescriptionLocationIdEMR
			{
				get
				{
					System.String data = entity.PrescriptionLocationIdEMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionLocationIdEMR = null;
					else entity.PrescriptionLocationIdEMR = Convert.ToString(value);
				}
			}
			public System.String IsItemRestrictionsFornas
			{
				get
				{
					System.Boolean? data = entity.IsItemRestrictionsFornas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRestrictionsFornas = null;
					else entity.IsItemRestrictionsFornas = Convert.ToBoolean(value);
				}
			}
			public System.String IsProrateParamedicFee
			{
				get
				{
					System.Boolean? data = entity.IsProrateParamedicFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProrateParamedicFee = null;
					else entity.IsProrateParamedicFee = Convert.ToBoolean(value);
				}
			}
			public System.String TariffCalculationMethod
			{
				get
				{
					System.Byte? data = entity.TariffCalculationMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffCalculationMethod = null;
					else entity.TariffCalculationMethod = Convert.ToByte(value);
				}
			}
			public System.String IsAdminCalcBeforeDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAdminCalcBeforeDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdminCalcBeforeDiscount = null;
					else entity.IsAdminCalcBeforeDiscount = Convert.ToBoolean(value);
				}
			}
			public System.String NoteCompanyList
			{
				get
				{
					System.String data = entity.NoteCompanyList;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoteCompanyList = null;
					else entity.NoteCompanyList = Convert.ToString(value);
				}
			}
			public System.String IsParamedicFeeRemun
			{
				get
				{
					System.Boolean? data = entity.IsParamedicFeeRemun;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParamedicFeeRemun = null;
					else entity.IsParamedicFeeRemun = Convert.ToBoolean(value);
				}
			}
			public System.String RoundingTransaction
			{
				get
				{
					System.Decimal? data = entity.RoundingTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoundingTransaction = null;
					else entity.RoundingTransaction = Convert.ToDecimal(value);
				}
			}
			public System.String IsUsingRoundingDown
			{
				get
				{
					System.Boolean? data = entity.IsUsingRoundingDown;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingRoundingDown = null;
					else entity.IsUsingRoundingDown = Convert.ToBoolean(value);
				}
			}
			public System.String CreditLimit
			{
				get
				{
					System.Decimal? data = entity.CreditLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditLimit = null;
					else entity.CreditLimit = Convert.ToDecimal(value);
				}
			}
			public System.String CreditAmount
			{
				get
				{
					System.Decimal? data = entity.CreditAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditAmount = null;
					else entity.CreditAmount = Convert.ToDecimal(value);
				}
			}
			public System.String VirtualAccountBank
			{
				get
				{
					System.String data = entity.VirtualAccountBank;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VirtualAccountBank = null;
					else entity.VirtualAccountBank = Convert.ToString(value);
				}
			}
			public System.String VirtualAccountName
			{
				get
				{
					System.String data = entity.VirtualAccountName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VirtualAccountName = null;
					else entity.VirtualAccountName = Convert.ToString(value);
				}
			}
			public System.String IsItemRestrictionsFormularium
			{
				get
				{
					System.Boolean? data = entity.IsItemRestrictionsFormularium;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRestrictionsFormularium = null;
					else entity.IsItemRestrictionsFormularium = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemRestrictionsGeneric
			{
				get
				{
					System.Boolean? data = entity.IsItemRestrictionsGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRestrictionsGeneric = null;
					else entity.IsItemRestrictionsGeneric = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemRestrictionsNonGeneric
			{
				get
				{
					System.Boolean? data = entity.IsItemRestrictionsNonGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRestrictionsNonGeneric = null;
					else entity.IsItemRestrictionsNonGeneric = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemRestrictionsNonGenericLimited
			{
				get
				{
					System.Boolean? data = entity.IsItemRestrictionsNonGenericLimited;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRestrictionsNonGenericLimited = null;
					else entity.IsItemRestrictionsNonGenericLimited = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeeBrutoFromFeeAmount
			{
				get
				{
					System.Boolean? data = entity.IsFeeBrutoFromFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeBrutoFromFeeAmount = null;
					else entity.IsFeeBrutoFromFeeAmount = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdIPR
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIPR = null;
					else entity.ChartOfAccountIdIPR = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerIdIPR
			{
				get
				{
					System.Int32? data = entity.SubLedgerIdIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerIdIPR = null;
					else entity.SubLedgerIdIPR = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdTemporaryIPR
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdTemporaryIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdTemporaryIPR = null;
					else entity.ChartOfAccountIdTemporaryIPR = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdTemporaryIPR
			{
				get
				{
					System.Int32? data = entity.SubledgerIdTemporaryIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdTemporaryIPR = null;
					else entity.SubledgerIdTemporaryIPR = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdOverPaymentMin
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdOverPaymentMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdOverPaymentMin = null;
					else entity.ChartOfAccountIdOverPaymentMin = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdOverPaymentMin
			{
				get
				{
					System.Int32? data = entity.SubledgerIdOverPaymentMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdOverPaymentMin = null;
					else entity.SubledgerIdOverPaymentMin = Convert.ToInt32(value);
				}
			}
			public System.String IsDiscountProrataToRevenue
			{
				get
				{
					System.Boolean? data = entity.IsDiscountProrataToRevenue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountProrataToRevenue = null;
					else entity.IsDiscountProrataToRevenue = Convert.ToBoolean(value);
				}
			}
			public System.String RecipeMarginValueNonCompound
			{
				get
				{
					System.Decimal? data = entity.RecipeMarginValueNonCompound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipeMarginValueNonCompound = null;
					else entity.RecipeMarginValueNonCompound = Convert.ToDecimal(value);
				}
			}
			public System.String IsUsingDefaultRecipeAmount
			{
				get
				{
					System.Boolean? data = entity.IsUsingDefaultRecipeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingDefaultRecipeAmount = null;
					else entity.IsUsingDefaultRecipeAmount = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemServiceRestrictionStatusAllowed
			{
				get
				{
					System.Boolean? data = entity.IsItemServiceRestrictionStatusAllowed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemServiceRestrictionStatusAllowed = null;
					else entity.IsItemServiceRestrictionStatusAllowed = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemLabRestrictionStatusAllowed
			{
				get
				{
					System.Boolean? data = entity.IsItemLabRestrictionStatusAllowed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemLabRestrictionStatusAllowed = null;
					else entity.IsItemLabRestrictionStatusAllowed = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemRadRestrictionStatusAllowed
			{
				get
				{
					System.Boolean? data = entity.IsItemRadRestrictionStatusAllowed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemRadRestrictionStatusAllowed = null;
					else entity.IsItemRadRestrictionStatusAllowed = Convert.ToBoolean(value);
				}
			}
			public System.String IsItemProductRestrictionStatusAllowed
			{
				get
				{
					System.Boolean? data = entity.IsItemProductRestrictionStatusAllowed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemProductRestrictionStatusAllowed = null;
					else entity.IsItemProductRestrictionStatusAllowed = Convert.ToBoolean(value);
				}
			}
			private esGuarantor entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorQuery query)
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
				throw new Exception("esGuarantor can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Guarantor : esGuarantor
	{
	}

	[Serializable]
	abstract public class esGuarantorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return GuarantorMetadata.Meta();
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem GuarantorName
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.GuarantorName, esSystemType.String);
			}
		}

		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		}

		public esQueryItem SRGuarantorType
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRGuarantorType, esSystemType.String);
			}
		}

		public esQueryItem ContractNumber
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ContractNumber, esSystemType.String);
			}
		}

		public esQueryItem ContractStart
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ContractStart, esSystemType.DateTime);
			}
		}

		public esQueryItem ContractEnd
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ContractEnd, esSystemType.DateTime);
			}
		}

		public esQueryItem ContractSummary
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ContractSummary, esSystemType.String);
			}
		}

		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem SRBusinessMethod
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRBusinessMethod, esSystemType.String);
			}
		}

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		}

		public esQueryItem SRGuarantorRuleType
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRGuarantorRuleType, esSystemType.String);
			}
		}

		public esQueryItem IsValueInPercent
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem AmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
			}
		}

		public esQueryItem AdminPercentage
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem AdminAmountLimit
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminAmountLimit, esSystemType.Decimal);
			}
		}

		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.State, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsIncludeItemMedical
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemMedical, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIncludeItemNonMedical
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemNonMedical, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIncludeItemOptic
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemOptic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIncludeItemMedicalToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemMedicalToGuarantor, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIncludeItemNonMedicalToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemNonMedicalToGuarantor, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIncludeItemOpticToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeItemOpticToGuarantor, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCoverInpatient
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsCoverInpatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCoverOutpatient
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsCoverOutpatient, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemMedicMarginPercentage
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ItemMedicMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem ItemMedicMarginID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ItemMedicMarginID, esSystemType.String);
			}
		}

		public esQueryItem ItemNonMedicMarginPercentage
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ItemNonMedicMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem ItemNonMedicMarginID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ItemNonMedicMarginID, esSystemType.String);
			}
		}

		public esQueryItem GuarantorHeaderID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.GuarantorHeaderID, esSystemType.String);
			}
		}

		public esQueryItem IsIncludeAdminValue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsIncludeAdminValue, esSystemType.Boolean);
			}
		}

		public esQueryItem AdminValueMinimum
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminValueMinimum, esSystemType.Decimal);
			}
		}

		public esQueryItem IsGlobalPlafond
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsGlobalPlafond, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAdminFromTotal
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsAdminFromTotal, esSystemType.Boolean);
			}
		}

		public esQueryItem AdminPercentageOp
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminPercentageOp, esSystemType.Decimal);
			}
		}

		public esQueryItem AdminAmountLimitOp
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminAmountLimitOp, esSystemType.Decimal);
			}
		}

		public esQueryItem AdminValueMinimumOp
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.AdminValueMinimumOp, esSystemType.Decimal);
			}
		}

		public esQueryItem ChartOfAccountIdTemporary
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporary, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdTemporary
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdTemporary, esSystemType.Int32);
			}
		}

		public esQueryItem IsItemRuleUsingDefaultAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRuleUsingDefaultAmountValue, esSystemType.Boolean);
			}
		}

		public esQueryItem OutpatientAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
			}
		}

		public esQueryItem EmergencyAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
			}
		}

		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		}

		public esQueryItem SRPhysicianFeeType
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRPhysicianFeeType, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountIdDeposit
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdDeposit, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdDeposit
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdDeposit, esSystemType.Int32);
			}
		}

		public esQueryItem TermOfPayment
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.TermOfPayment, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdOverPayment
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPayment, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdOverPayment
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdOverPayment, esSystemType.Int32);
			}
		}

		public esQueryItem SRPhysicianFeeCategory
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRPhysicianFeeCategory, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountIdCostParamedicFee
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdCostParamedicFee
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdCostParamedicFee, esSystemType.Int32);
			}
		}

		public esQueryItem IsExcessPlafonToDiscount
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsExcessPlafonToDiscount, esSystemType.Boolean);
			}
		}

		public esQueryItem VirtualAccountNo
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.VirtualAccountNo, esSystemType.String);
			}
		}

		public esQueryItem ReportRLID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ReportRLID, esSystemType.String);
			}
		}

		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		}

		public esQueryItem IsCoverAllAdminCosts
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsCoverAllAdminCosts, esSystemType.Boolean);
			}
		}

		public esQueryItem SRGuarantorIncomeGroup
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SRGuarantorIncomeGroup, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionServiceUnitIdIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdIPR, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionLocationIdIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionLocationIdIPR, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionServiceUnitIdOPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdOPR, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionLocationIdOPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionLocationIdOPR, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionServiceUnitIdEMR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdEMR, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionLocationIdEMR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.PrescriptionLocationIdEMR, esSystemType.String);
			}
		}

		public esQueryItem IsItemRestrictionsFornas
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRestrictionsFornas, esSystemType.Boolean);
			}
		}

		public esQueryItem IsProrateParamedicFee
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsProrateParamedicFee, esSystemType.Boolean);
			}
		}

		public esQueryItem TariffCalculationMethod
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.TariffCalculationMethod, esSystemType.Byte);
			}
		}

		public esQueryItem IsAdminCalcBeforeDiscount
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsAdminCalcBeforeDiscount, esSystemType.Boolean);
			}
		}

		public esQueryItem NoteCompanyList
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.NoteCompanyList, esSystemType.String);
			}
		}

		public esQueryItem IsParamedicFeeRemun
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsParamedicFeeRemun, esSystemType.Boolean);
			}
		}

		public esQueryItem RoundingTransaction
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.RoundingTransaction, esSystemType.Decimal);
			}
		}

		public esQueryItem IsUsingRoundingDown
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsUsingRoundingDown, esSystemType.Boolean);
			}
		}

		public esQueryItem CreditLimit
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.CreditLimit, esSystemType.Decimal);
			}
		}

		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem VirtualAccountBank
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.VirtualAccountBank, esSystemType.String);
			}
		}

		public esQueryItem VirtualAccountName
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.VirtualAccountName, esSystemType.String);
			}
		}

		public esQueryItem IsItemRestrictionsFormularium
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRestrictionsFormularium, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemRestrictionsGeneric
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRestrictionsGeneric, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemRestrictionsNonGeneric
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGeneric, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemRestrictionsNonGenericLimited
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGenericLimited, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeeBrutoFromFeeAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsFeeBrutoFromFeeAmount, esSystemType.Boolean);
			}
		}

		public esQueryItem ChartOfAccountIdIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdIPR, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerIdIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubLedgerIdIPR, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdTemporaryIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporaryIPR, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdTemporaryIPR
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdTemporaryIPR, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdOverPaymentMin
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPaymentMin, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdOverPaymentMin
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.SubledgerIdOverPaymentMin, esSystemType.Int32);
			}
		}

		public esQueryItem IsDiscountProrataToRevenue
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsDiscountProrataToRevenue, esSystemType.Boolean);
			}
		}

		public esQueryItem RecipeMarginValueNonCompound
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.RecipeMarginValueNonCompound, esSystemType.Decimal);
			}
		}

		public esQueryItem IsUsingDefaultRecipeAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsUsingDefaultRecipeAmount, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemServiceRestrictionStatusAllowed
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemServiceRestrictionStatusAllowed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemLabRestrictionStatusAllowed
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemLabRestrictionStatusAllowed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemRadRestrictionStatusAllowed
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemRadRestrictionStatusAllowed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsItemProductRestrictionStatusAllowed
		{
			get
			{
				return new esQueryItem(this, GuarantorMetadata.ColumnNames.IsItemProductRestrictionStatusAllowed, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorCollection")]
	public partial class GuarantorCollection : esGuarantorCollection, IEnumerable<Guarantor>
	{
		public GuarantorCollection()
		{

		}

		public static implicit operator List<Guarantor>(GuarantorCollection coll)
		{
			List<Guarantor> list = new List<Guarantor>();

			foreach (Guarantor emp in coll)
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
				return GuarantorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Guarantor(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Guarantor();
		}

		#endregion

		[BrowsableAttribute(false)]
		public GuarantorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorQuery();
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
		public bool Load(GuarantorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Guarantor AddNew()
		{
			Guarantor entity = base.AddNewEntity() as Guarantor;

			return entity;
		}
		public Guarantor FindByPrimaryKey(String guarantorID)
		{
			return base.FindByPrimaryKey(guarantorID) as Guarantor;
		}

		#region IEnumerable< Guarantor> Members

		IEnumerator<Guarantor> IEnumerable<Guarantor>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Guarantor;
			}
		}

		#endregion

		private GuarantorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Guarantor' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Guarantor ({GuarantorID})")]
	[Serializable]
	public partial class Guarantor : esGuarantor
	{
		public Guarantor()
		{
		}

		public Guarantor(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorMetadata.Meta();
			}
		}

		override protected esGuarantorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public GuarantorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorQuery();
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
		public bool Load(GuarantorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private GuarantorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class GuarantorQuery : esGuarantorQuery
	{
		public GuarantorQuery()
		{

		}

		public GuarantorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "GuarantorQuery";
		}
	}

	[Serializable]
	public partial class GuarantorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.GuarantorName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.GuarantorName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRGuarantorType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRGuarantorType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ContractNumber, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ContractNumber;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ContractStart, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorMetadata.PropertyNames.ContractStart;
			c.HasDefault = true;
			c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ContractEnd, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorMetadata.PropertyNames.ContractEnd;
			c.HasDefault = true;
			c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ContractSummary, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ContractSummary;
			c.CharacterMaxLength = 16;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ContactPerson, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsActive, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRBusinessMethod, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRBusinessMethod;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRTariffType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRGuarantorRuleType, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRGuarantorRuleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsValueInPercent, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsValueInPercent;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AmountValue, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminAmountLimit, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminAmountLimit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.StreetName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.District, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.City, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.County, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.State, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ZipCode, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PhoneNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.FaxNo, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.Email, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.MobilePhoneNo, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountId, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubLedgerId, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.LastUpdateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.LastUpdateByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemMedical, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemMedical;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedical, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemNonMedical;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemOptic, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemOptic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemMedicalToGuarantor, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemMedicalToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemNonMedicalToGuarantor, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemNonMedicalToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeItemOpticToGuarantor, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeItemOpticToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsCoverInpatient, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsCoverInpatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsCoverOutpatient, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsCoverOutpatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ItemMedicMarginPercentage, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.ItemMedicMarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ItemMedicMarginID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ItemMedicMarginID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ItemNonMedicMarginPercentage, 41, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.ItemNonMedicMarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ItemNonMedicMarginID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ItemNonMedicMarginID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.GuarantorHeaderID, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.GuarantorHeaderID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsIncludeAdminValue, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsIncludeAdminValue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminValueMinimum, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminValueMinimum;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsGlobalPlafond, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsGlobalPlafond;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsAdminFromTotal, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsAdminFromTotal;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminPercentageOp, 48, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminPercentageOp;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminAmountLimitOp, 49, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminAmountLimitOp;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.AdminValueMinimumOp, 50, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.AdminValueMinimumOp;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporary, 51, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdTemporary;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdTemporary, 52, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdTemporary;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRuleUsingDefaultAmountValue, 53, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRuleUsingDefaultAmountValue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.OutpatientAmountValue, 54, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.OutpatientAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.EmergencyAmountValue, 55, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.EmergencyAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRPaymentType, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRPhysicianFeeType, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRPhysicianFeeType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdDeposit, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdDeposit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdDeposit, 59, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdDeposit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.TermOfPayment, 60, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.TermOfPayment;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPayment, 61, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdOverPayment;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdOverPayment, 62, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdOverPayment;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRPhysicianFeeCategory, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRPhysicianFeeCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, 64, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdCostParamedicFee;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdCostParamedicFee, 65, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdCostParamedicFee;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsExcessPlafonToDiscount, 66, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsExcessPlafonToDiscount;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.VirtualAccountNo, 67, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.VirtualAccountNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ReportRLID, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.ReportRLID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.RlMasterReportItemID, 69, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.RlMasterReportItemID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsCoverAllAdminCosts, 70, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsCoverAllAdminCosts;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SRGuarantorIncomeGroup, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.SRGuarantorIncomeGroup;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdIPR, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionServiceUnitIdIPR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionLocationIdIPR, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionLocationIdIPR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdOPR, 74, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionServiceUnitIdOPR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionLocationIdOPR, 75, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionLocationIdOPR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionServiceUnitIdEMR, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionServiceUnitIdEMR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.PrescriptionLocationIdEMR, 77, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.PrescriptionLocationIdEMR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRestrictionsFornas, 78, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRestrictionsFornas;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsProrateParamedicFee, 79, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsProrateParamedicFee;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.TariffCalculationMethod, 80, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = GuarantorMetadata.PropertyNames.TariffCalculationMethod;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsAdminCalcBeforeDiscount, 81, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsAdminCalcBeforeDiscount;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.NoteCompanyList, 82, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.NoteCompanyList;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsParamedicFeeRemun, 83, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsParamedicFeeRemun;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.RoundingTransaction, 84, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.RoundingTransaction;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsUsingRoundingDown, 85, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsUsingRoundingDown;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.CreditLimit, 86, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.CreditLimit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.CreditAmount, 87, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.VirtualAccountBank, 88, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.VirtualAccountBank;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.VirtualAccountName, 89, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorMetadata.PropertyNames.VirtualAccountName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRestrictionsFormularium, 90, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRestrictionsFormularium;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRestrictionsGeneric, 91, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRestrictionsGeneric;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGeneric, 92, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRestrictionsNonGeneric;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRestrictionsNonGenericLimited, 93, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRestrictionsNonGenericLimited;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsFeeBrutoFromFeeAmount, 94, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsFeeBrutoFromFeeAmount;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdIPR, 95, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdIPR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubLedgerIdIPR, 96, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubLedgerIdIPR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdTemporaryIPR, 97, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdTemporaryIPR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdTemporaryIPR, 98, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdTemporaryIPR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.ChartOfAccountIdOverPaymentMin, 99, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.ChartOfAccountIdOverPaymentMin;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.SubledgerIdOverPaymentMin, 100, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorMetadata.PropertyNames.SubledgerIdOverPaymentMin;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsDiscountProrataToRevenue, 101, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsDiscountProrataToRevenue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.RecipeMarginValueNonCompound, 102, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorMetadata.PropertyNames.RecipeMarginValueNonCompound;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsUsingDefaultRecipeAmount, 103, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsUsingDefaultRecipeAmount;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemServiceRestrictionStatusAllowed, 104, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemServiceRestrictionStatusAllowed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemLabRestrictionStatusAllowed, 105, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemLabRestrictionStatusAllowed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemRadRestrictionStatusAllowed, 106, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemRadRestrictionStatusAllowed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorMetadata.ColumnNames.IsItemProductRestrictionStatusAllowed, 107, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorMetadata.PropertyNames.IsItemProductRestrictionStatusAllowed;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public GuarantorMetadata Meta()
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
			public const string GuarantorID = "GuarantorID";
			public const string GuarantorName = "GuarantorName";
			public const string ShortName = "ShortName";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string ContractNumber = "ContractNumber";
			public const string ContractStart = "ContractStart";
			public const string ContractEnd = "ContractEnd";
			public const string ContractSummary = "ContractSummary";
			public const string ContactPerson = "ContactPerson";
			public const string IsActive = "IsActive";
			public const string SRBusinessMethod = "SRBusinessMethod";
			public const string SRTariffType = "SRTariffType";
			public const string SRGuarantorRuleType = "SRGuarantorRuleType";
			public const string IsValueInPercent = "IsValueInPercent";
			public const string AmountValue = "AmountValue";
			public const string AdminPercentage = "AdminPercentage";
			public const string AdminAmountLimit = "AdminAmountLimit";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsIncludeItemMedical = "IsIncludeItemMedical";
			public const string IsIncludeItemNonMedical = "IsIncludeItemNonMedical";
			public const string IsIncludeItemOptic = "IsIncludeItemOptic";
			public const string IsIncludeItemMedicalToGuarantor = "IsIncludeItemMedicalToGuarantor";
			public const string IsIncludeItemNonMedicalToGuarantor = "IsIncludeItemNonMedicalToGuarantor";
			public const string IsIncludeItemOpticToGuarantor = "IsIncludeItemOpticToGuarantor";
			public const string IsCoverInpatient = "IsCoverInpatient";
			public const string IsCoverOutpatient = "IsCoverOutpatient";
			public const string ItemMedicMarginPercentage = "ItemMedicMarginPercentage";
			public const string ItemMedicMarginID = "ItemMedicMarginID";
			public const string ItemNonMedicMarginPercentage = "ItemNonMedicMarginPercentage";
			public const string ItemNonMedicMarginID = "ItemNonMedicMarginID";
			public const string GuarantorHeaderID = "GuarantorHeaderID";
			public const string IsIncludeAdminValue = "IsIncludeAdminValue";
			public const string AdminValueMinimum = "AdminValueMinimum";
			public const string IsGlobalPlafond = "IsGlobalPlafond";
			public const string IsAdminFromTotal = "IsAdminFromTotal";
			public const string AdminPercentageOp = "AdminPercentageOp";
			public const string AdminAmountLimitOp = "AdminAmountLimitOp";
			public const string AdminValueMinimumOp = "AdminValueMinimumOp";
			public const string ChartOfAccountIdTemporary = "ChartOfAccountIdTemporary";
			public const string SubledgerIdTemporary = "SubledgerIdTemporary";
			public const string IsItemRuleUsingDefaultAmountValue = "IsItemRuleUsingDefaultAmountValue";
			public const string OutpatientAmountValue = "OutpatientAmountValue";
			public const string EmergencyAmountValue = "EmergencyAmountValue";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPhysicianFeeType = "SRPhysicianFeeType";
			public const string ChartOfAccountIdDeposit = "ChartOfAccountIdDeposit";
			public const string SubledgerIdDeposit = "SubledgerIdDeposit";
			public const string TermOfPayment = "TermOfPayment";
			public const string ChartOfAccountIdOverPayment = "ChartOfAccountIdOverPayment";
			public const string SubledgerIdOverPayment = "SubledgerIdOverPayment";
			public const string SRPhysicianFeeCategory = "SRPhysicianFeeCategory";
			public const string ChartOfAccountIdCostParamedicFee = "ChartOfAccountIdCostParamedicFee";
			public const string SubledgerIdCostParamedicFee = "SubledgerIdCostParamedicFee";
			public const string IsExcessPlafonToDiscount = "IsExcessPlafonToDiscount";
			public const string VirtualAccountNo = "VirtualAccountNo";
			public const string ReportRLID = "ReportRLID";
			public const string RlMasterReportItemID = "RlMasterReportItemID";
			public const string IsCoverAllAdminCosts = "IsCoverAllAdminCosts";
			public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
			public const string PrescriptionServiceUnitIdIPR = "PrescriptionServiceUnitIdIPR";
			public const string PrescriptionLocationIdIPR = "PrescriptionLocationIdIPR";
			public const string PrescriptionServiceUnitIdOPR = "PrescriptionServiceUnitIdOPR";
			public const string PrescriptionLocationIdOPR = "PrescriptionLocationIdOPR";
			public const string PrescriptionServiceUnitIdEMR = "PrescriptionServiceUnitIdEMR";
			public const string PrescriptionLocationIdEMR = "PrescriptionLocationIdEMR";
			public const string IsItemRestrictionsFornas = "IsItemRestrictionsFornas";
			public const string IsProrateParamedicFee = "IsProrateParamedicFee";
			public const string TariffCalculationMethod = "TariffCalculationMethod";
			public const string IsAdminCalcBeforeDiscount = "IsAdminCalcBeforeDiscount";
			public const string NoteCompanyList = "NoteCompanyList";
			public const string IsParamedicFeeRemun = "IsParamedicFeeRemun";
			public const string RoundingTransaction = "RoundingTransaction";
			public const string IsUsingRoundingDown = "IsUsingRoundingDown";
			public const string CreditLimit = "CreditLimit";
			public const string CreditAmount = "CreditAmount";
			public const string VirtualAccountBank = "VirtualAccountBank";
			public const string VirtualAccountName = "VirtualAccountName";
			public const string IsItemRestrictionsFormularium = "IsItemRestrictionsFormularium";
			public const string IsItemRestrictionsGeneric = "IsItemRestrictionsGeneric";
			public const string IsItemRestrictionsNonGeneric = "IsItemRestrictionsNonGeneric";
			public const string IsItemRestrictionsNonGenericLimited = "IsItemRestrictionsNonGenericLimited";
			public const string IsFeeBrutoFromFeeAmount = "IsFeeBrutoFromFeeAmount";
			public const string ChartOfAccountIdIPR = "ChartOfAccountIdIPR";
			public const string SubLedgerIdIPR = "SubLedgerIdIPR";
			public const string ChartOfAccountIdTemporaryIPR = "ChartOfAccountIdTemporaryIPR";
			public const string SubledgerIdTemporaryIPR = "SubledgerIdTemporaryIPR";
			public const string ChartOfAccountIdOverPaymentMin = "ChartOfAccountIdOverPaymentMin";
			public const string SubledgerIdOverPaymentMin = "SubledgerIdOverPaymentMin";
			public const string IsDiscountProrataToRevenue = "IsDiscountProrataToRevenue";
			public const string RecipeMarginValueNonCompound = "RecipeMarginValueNonCompound";
			public const string IsUsingDefaultRecipeAmount = "IsUsingDefaultRecipeAmount";
			public const string IsItemServiceRestrictionStatusAllowed = "IsItemServiceRestrictionStatusAllowed";
			public const string IsItemLabRestrictionStatusAllowed = "IsItemLabRestrictionStatusAllowed";
			public const string IsItemRadRestrictionStatusAllowed = "IsItemRadRestrictionStatusAllowed";
			public const string IsItemProductRestrictionStatusAllowed = "IsItemProductRestrictionStatusAllowed";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string GuarantorID = "GuarantorID";
			public const string GuarantorName = "GuarantorName";
			public const string ShortName = "ShortName";
			public const string SRGuarantorType = "SRGuarantorType";
			public const string ContractNumber = "ContractNumber";
			public const string ContractStart = "ContractStart";
			public const string ContractEnd = "ContractEnd";
			public const string ContractSummary = "ContractSummary";
			public const string ContactPerson = "ContactPerson";
			public const string IsActive = "IsActive";
			public const string SRBusinessMethod = "SRBusinessMethod";
			public const string SRTariffType = "SRTariffType";
			public const string SRGuarantorRuleType = "SRGuarantorRuleType";
			public const string IsValueInPercent = "IsValueInPercent";
			public const string AmountValue = "AmountValue";
			public const string AdminPercentage = "AdminPercentage";
			public const string AdminAmountLimit = "AdminAmountLimit";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsIncludeItemMedical = "IsIncludeItemMedical";
			public const string IsIncludeItemNonMedical = "IsIncludeItemNonMedical";
			public const string IsIncludeItemOptic = "IsIncludeItemOptic";
			public const string IsIncludeItemMedicalToGuarantor = "IsIncludeItemMedicalToGuarantor";
			public const string IsIncludeItemNonMedicalToGuarantor = "IsIncludeItemNonMedicalToGuarantor";
			public const string IsIncludeItemOpticToGuarantor = "IsIncludeItemOpticToGuarantor";
			public const string IsCoverInpatient = "IsCoverInpatient";
			public const string IsCoverOutpatient = "IsCoverOutpatient";
			public const string ItemMedicMarginPercentage = "ItemMedicMarginPercentage";
			public const string ItemMedicMarginID = "ItemMedicMarginID";
			public const string ItemNonMedicMarginPercentage = "ItemNonMedicMarginPercentage";
			public const string ItemNonMedicMarginID = "ItemNonMedicMarginID";
			public const string GuarantorHeaderID = "GuarantorHeaderID";
			public const string IsIncludeAdminValue = "IsIncludeAdminValue";
			public const string AdminValueMinimum = "AdminValueMinimum";
			public const string IsGlobalPlafond = "IsGlobalPlafond";
			public const string IsAdminFromTotal = "IsAdminFromTotal";
			public const string AdminPercentageOp = "AdminPercentageOp";
			public const string AdminAmountLimitOp = "AdminAmountLimitOp";
			public const string AdminValueMinimumOp = "AdminValueMinimumOp";
			public const string ChartOfAccountIdTemporary = "ChartOfAccountIdTemporary";
			public const string SubledgerIdTemporary = "SubledgerIdTemporary";
			public const string IsItemRuleUsingDefaultAmountValue = "IsItemRuleUsingDefaultAmountValue";
			public const string OutpatientAmountValue = "OutpatientAmountValue";
			public const string EmergencyAmountValue = "EmergencyAmountValue";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPhysicianFeeType = "SRPhysicianFeeType";
			public const string ChartOfAccountIdDeposit = "ChartOfAccountIdDeposit";
			public const string SubledgerIdDeposit = "SubledgerIdDeposit";
			public const string TermOfPayment = "TermOfPayment";
			public const string ChartOfAccountIdOverPayment = "ChartOfAccountIdOverPayment";
			public const string SubledgerIdOverPayment = "SubledgerIdOverPayment";
			public const string SRPhysicianFeeCategory = "SRPhysicianFeeCategory";
			public const string ChartOfAccountIdCostParamedicFee = "ChartOfAccountIdCostParamedicFee";
			public const string SubledgerIdCostParamedicFee = "SubledgerIdCostParamedicFee";
			public const string IsExcessPlafonToDiscount = "IsExcessPlafonToDiscount";
			public const string VirtualAccountNo = "VirtualAccountNo";
			public const string ReportRLID = "ReportRLID";
			public const string RlMasterReportItemID = "RlMasterReportItemID";
			public const string IsCoverAllAdminCosts = "IsCoverAllAdminCosts";
			public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
			public const string PrescriptionServiceUnitIdIPR = "PrescriptionServiceUnitIdIPR";
			public const string PrescriptionLocationIdIPR = "PrescriptionLocationIdIPR";
			public const string PrescriptionServiceUnitIdOPR = "PrescriptionServiceUnitIdOPR";
			public const string PrescriptionLocationIdOPR = "PrescriptionLocationIdOPR";
			public const string PrescriptionServiceUnitIdEMR = "PrescriptionServiceUnitIdEMR";
			public const string PrescriptionLocationIdEMR = "PrescriptionLocationIdEMR";
			public const string IsItemRestrictionsFornas = "IsItemRestrictionsFornas";
			public const string IsProrateParamedicFee = "IsProrateParamedicFee";
			public const string TariffCalculationMethod = "TariffCalculationMethod";
			public const string IsAdminCalcBeforeDiscount = "IsAdminCalcBeforeDiscount";
			public const string NoteCompanyList = "NoteCompanyList";
			public const string IsParamedicFeeRemun = "IsParamedicFeeRemun";
			public const string RoundingTransaction = "RoundingTransaction";
			public const string IsUsingRoundingDown = "IsUsingRoundingDown";
			public const string CreditLimit = "CreditLimit";
			public const string CreditAmount = "CreditAmount";
			public const string VirtualAccountBank = "VirtualAccountBank";
			public const string VirtualAccountName = "VirtualAccountName";
			public const string IsItemRestrictionsFormularium = "IsItemRestrictionsFormularium";
			public const string IsItemRestrictionsGeneric = "IsItemRestrictionsGeneric";
			public const string IsItemRestrictionsNonGeneric = "IsItemRestrictionsNonGeneric";
			public const string IsItemRestrictionsNonGenericLimited = "IsItemRestrictionsNonGenericLimited";
			public const string IsFeeBrutoFromFeeAmount = "IsFeeBrutoFromFeeAmount";
			public const string ChartOfAccountIdIPR = "ChartOfAccountIdIPR";
			public const string SubLedgerIdIPR = "SubLedgerIdIPR";
			public const string ChartOfAccountIdTemporaryIPR = "ChartOfAccountIdTemporaryIPR";
			public const string SubledgerIdTemporaryIPR = "SubledgerIdTemporaryIPR";
			public const string ChartOfAccountIdOverPaymentMin = "ChartOfAccountIdOverPaymentMin";
			public const string SubledgerIdOverPaymentMin = "SubledgerIdOverPaymentMin";
			public const string IsDiscountProrataToRevenue = "IsDiscountProrataToRevenue";
			public const string RecipeMarginValueNonCompound = "RecipeMarginValueNonCompound";
			public const string IsUsingDefaultRecipeAmount = "IsUsingDefaultRecipeAmount";
			public const string IsItemServiceRestrictionStatusAllowed = "IsItemServiceRestrictionStatusAllowed";
			public const string IsItemLabRestrictionStatusAllowed = "IsItemLabRestrictionStatusAllowed";
			public const string IsItemRadRestrictionStatusAllowed = "IsItemRadRestrictionStatusAllowed";
			public const string IsItemProductRestrictionStatusAllowed = "IsItemProductRestrictionStatusAllowed";
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
			lock (typeof(GuarantorMetadata))
			{
				if (GuarantorMetadata.mapDelegates == null)
				{
					GuarantorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (GuarantorMetadata.meta == null)
				{
					GuarantorMetadata.meta = new GuarantorMetadata();
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

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGuarantorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractStart", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ContractEnd", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ContractSummary", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRBusinessMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGuarantorRuleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdminPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdminAmountLimit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIncludeItemMedical", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIncludeItemNonMedical", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIncludeItemOptic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIncludeItemMedicalToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIncludeItemNonMedicalToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIncludeItemOpticToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCoverInpatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCoverOutpatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemMedicMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ItemMedicMarginID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemNonMedicMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ItemNonMedicMarginID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorHeaderID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIncludeAdminValue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AdminValueMinimum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsGlobalPlafond", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAdminFromTotal", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AdminPercentageOp", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdminAmountLimitOp", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdminValueMinimumOp", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ChartOfAccountIdTemporary", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdTemporary", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsItemRuleUsingDefaultAmountValue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPhysicianFeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdDeposit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDeposit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TermOfPayment", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdOverPayment", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdOverPayment", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPhysicianFeeCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdCostParamedicFee", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCostParamedicFee", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsExcessPlafonToDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VirtualAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportRLID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsCoverAllAdminCosts", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRGuarantorIncomeGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionServiceUnitIdIPR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionLocationIdIPR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionServiceUnitIdOPR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionLocationIdOPR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionServiceUnitIdEMR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionLocationIdEMR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsItemRestrictionsFornas", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsProrateParamedicFee", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TariffCalculationMethod", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("IsAdminCalcBeforeDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NoteCompanyList", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParamedicFeeRemun", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RoundingTransaction", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsUsingRoundingDown", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreditLimit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VirtualAccountBank", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VirtualAccountName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsItemRestrictionsFormularium", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemRestrictionsGeneric", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemRestrictionsNonGeneric", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemRestrictionsNonGenericLimited", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeeBrutoFromFeeAmount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdIPR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerIdIPR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdTemporaryIPR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdTemporaryIPR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdOverPaymentMin", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdOverPaymentMin", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsDiscountProrataToRevenue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RecipeMarginValueNonCompound", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsUsingDefaultRecipeAmount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemServiceRestrictionStatusAllowed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemLabRestrictionStatusAllowed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemRadRestrictionStatusAllowed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsItemProductRestrictionStatusAllowed", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "Guarantor";
				meta.Destination = "Guarantor";
				meta.spInsert = "proc_GuarantorInsert";
				meta.spUpdate = "proc_GuarantorUpdate";
				meta.spDelete = "proc_GuarantorDelete";
				meta.spLoadAll = "proc_GuarantorLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
