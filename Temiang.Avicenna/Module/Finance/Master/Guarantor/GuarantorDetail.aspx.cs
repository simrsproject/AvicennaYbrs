        using System;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewId()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.GuarantorId);

            return _autoNumber.LastCompleteNumber;
        }

        private void ShowHideCoaIprOpr()
        {
            var ShowCoaIPR = AppSession.Parameter.acc_IsCoaInvoiceGuarantorSplitIprOpr;

            trCoaIpr.Visible = ShowCoaIPR;
            trSlIpr.Visible = ShowCoaIPR;
            trCoaTempIpr.Visible = ShowCoaIPR;
            trSlTempIpr.Visible = ShowCoaIPR;

            lblChartOfAccountId.Text = ShowCoaIPR ? "COA (A/R Invoice) - OPR" : "COA (A/R Invoice)";
            lblChartOfAccountIdTemporary.Text = ShowCoaIPR ? "COA (A/R Process) - OPR" : "COA (A/R Process)";
        }

        private void SetEntityValue(Guarantor entity)
        {
            if (entity.es.IsAdded && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateGuarantorIdAutomatic) == "Yes")
            {
                txtGuarantorID.Text = GetNewId();
                _autoNumber.Save();
            }

            entity.TariffCalculationMethod = Convert.ToByte(rblTariffCalculation.SelectedValue);
            entity.GuarantorID = txtGuarantorID.Text.ToUpper();
            entity.GuarantorName = txtGuarantorName.Text;
            entity.ShortName = txtShortName.Text;
            entity.SRGuarantorType = cboSRGuarantorType.SelectedValue;
            entity.ContractNumber = txtContractNumber.Text;
            entity.ContractStart = txtContractStart.SelectedDate;
            entity.ContractEnd = txtContractEnd.SelectedDate;
            entity.ContractSummary = txtContractSummary.Text;
            entity.NoteCompanyList = txtNoteCompanyList.Text;
            entity.TermOfPayment = (int)txtTOP.Value;
            entity.ContactPerson = txtContactPerson.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.SRBusinessMethod = cboSRBusinessMethod.SelectedValue;
            entity.SRTariffType = cboSRTariffType.SelectedValue;
            entity.SRGuarantorRuleType = cboSRGuarantorRuleType.SelectedValue;
            entity.AmountValue = (decimal)txtAmountValue.Value;
            entity.OutpatientAmountValue = (decimal)txtOutpatientAmountValue.Value;
            entity.EmergencyAmountValue = (decimal)txtEmergencyAmountValue.Value;
            entity.IsValueInPercent = chkIsAmountInPercent.Checked;
            entity.AdminPercentage = (decimal)txtAdminPercentage.Value;
            entity.AdminPercentageOp = (decimal)txtAdminPercentageOp.Value;
            entity.IsUsingDefaultRecipeAmount = chkIsUsingDefaultRecipeAmount.Checked;
            entity.RecipeMarginValueNonCompound = (decimal)txtRecipeMarginValueNonCompound.Value;

            entity.AdminValueMinimum = (decimal)txtAdminAmountMin.Value;
            entity.AdminAmountLimit = (decimal)txtAdminAmountMax.Value;
            entity.AdminValueMinimumOp = (decimal)txtAdminAmountMinOp.Value;
            entity.AdminAmountLimitOp = (decimal)txtAdminAmountMaxOp.Value;

            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountId = chartOfAccountId;
            entity.SubLedgerId = subLedgerId;

            int chartOfAccountIdTemporary = 0;
            int subLedgerIdTemporary = 0;
            int.TryParse(cboChartOfAccountIdTemporary.SelectedValue, out chartOfAccountIdTemporary);
            int.TryParse(cboSubledgerIdTemporary.SelectedValue, out subLedgerIdTemporary);
            entity.ChartOfAccountIdTemporary = chartOfAccountIdTemporary;
            entity.SubledgerIdTemporary = subLedgerIdTemporary;

            //Edited by Fajri
            int chartOfAccountIdIPR = 0;
            int subLedgerIdIPR = 0;
            int.TryParse(cboChartOfAccountIdIPR.SelectedValue, out chartOfAccountIdIPR);
            int.TryParse(cboSubledgerIdIPR.SelectedValue, out subLedgerIdIPR);
            entity.ChartOfAccountIdIPR = chartOfAccountIdIPR;
            entity.SubLedgerIdIPR = subLedgerIdIPR;

            int chartOfAccountIdTemporaryIPR = 0;
            int subLedgerIdTemporaryIPR = 0;
            int.TryParse(cboChartOfAccountIdTemporaryIPR.SelectedValue, out chartOfAccountIdTemporaryIPR);
            int.TryParse(cboSubledgerIdTemporaryIPR.SelectedValue, out subLedgerIdTemporaryIPR);
            entity.ChartOfAccountIdTemporaryIPR = chartOfAccountIdTemporaryIPR;
            entity.SubledgerIdTemporaryIPR = subLedgerIdTemporaryIPR;
            //Edited by Fajri

            int chartOfAccountIdDeposit = 0;
            int subLedgerIdDeposit = 0;
            int.TryParse(cboChartOfAccountIdDeposit.SelectedValue, out chartOfAccountIdDeposit);
            int.TryParse(cboSubledgerIdDeposit.SelectedValue, out subLedgerIdDeposit);
            entity.ChartOfAccountIdDeposit = chartOfAccountIdDeposit;
            entity.SubledgerIdDeposit = subLedgerIdDeposit;

            int chartOfAccountIdOverPayment = 0;
            int subLedgerIdOverPayment = 0;
            int.TryParse(cboChartOfAccountIdOverPayment.SelectedValue, out chartOfAccountIdOverPayment);
            int.TryParse(cboSubledgerIdOverPayment.SelectedValue, out subLedgerIdOverPayment);
            entity.ChartOfAccountIdOverPayment = chartOfAccountIdOverPayment;
            entity.SubledgerIdOverPayment = subLedgerIdOverPayment;

            int chartOfAccountIdOverPaymentMin = 0;
            int subLedgerIdOverPaymentMin = 0;
            int.TryParse(cboChartOfAccountIdOverPaymentMin.SelectedValue, out chartOfAccountIdOverPaymentMin);
            int.TryParse(cboSubledgerIdOverPaymentMin.SelectedValue, out subLedgerIdOverPaymentMin);
            entity.ChartOfAccountIdOverPaymentMin = chartOfAccountIdOverPaymentMin;
            entity.SubledgerIdOverPaymentMin = subLedgerIdOverPaymentMin;

            int chartOfAccountIdCostParamedicFee = 0;
            int subLedgerIdCostParamedicFee = 0;
            int.TryParse(cboChartOfAccountIdCostParamedicFee.SelectedValue, out chartOfAccountIdCostParamedicFee);
            int.TryParse(cboSubledgerIdCostParamedicFee.SelectedValue, out subLedgerIdCostParamedicFee);
            entity.ChartOfAccountIdCostParamedicFee = chartOfAccountIdCostParamedicFee;
            entity.SubledgerIdCostParamedicFee = subLedgerIdCostParamedicFee;

            entity.IsIncludeItemMedical = chkIsIncludeItemMedical.Checked;
            entity.IsIncludeItemMedicalToGuarantor = (entity.IsIncludeItemMedical ?? false) && rblToGuarantorMedical.SelectedIndex == 0;

            entity.IsIncludeItemNonMedical = chkIsIncludeItemNonMedical.Checked;
            entity.IsIncludeItemNonMedicalToGuarantor = (entity.IsIncludeItemNonMedical ?? false) && rblToGuarantorNonMedical.SelectedIndex == 0;

            entity.IsIncludeItemOptic = chkIsIncludeItemOptic.Checked;
            entity.IsIncludeItemOpticToGuarantor = (entity.IsIncludeItemOptic ?? false) && rblToGuarantorOptic.SelectedIndex == 0;
            entity.IsItemRestrictionsFornas = chkIsItemRestrictionsFornas.Checked;
            entity.IsItemRestrictionsFormularium = chkIsItemRestrictionsFormularium.Checked;
            entity.IsItemRestrictionsGeneric = chkIsItemRestrictionsGeneric.Checked;
            entity.IsItemRestrictionsNonGeneric = chkIsItemRestrictionsNonGeneric.Checked;
            entity.IsItemRestrictionsNonGenericLimited = chkIsItemRestrictionsNonGenericLimited.Checked;

            entity.IsCoverInpatient = chkIsCoverInpatient.Checked;
            entity.IsCoverOutpatient = chkIsCoverOutpatient.Checked;
            entity.ItemMedicMarginPercentage = (decimal)txtItemMedicMarginPercentage.Value;
            entity.ItemMedicMarginID = cboItemMedicMarginID.SelectedValue;
            entity.ItemNonMedicMarginPercentage = (decimal)txtItemNonMedicMarginPercentage.Value;
            entity.ItemNonMedicMarginID = cboItemNonMedicMarginID.SelectedValue;
            entity.GuarantorHeaderID = string.IsNullOrEmpty(cboGuarantorHeaderID.SelectedValue) ? txtGuarantorID.Text.ToUpper() : cboGuarantorHeaderID.SelectedValue;

            entity.SRGuarantorIncomeGroup = cboAccountGroupID.SelectedValue;

            //Address
            entity.StreetName = ctlAddress.StreetName;
            entity.District = ctlAddress.District;
            entity.City = ctlAddress.City;
            entity.County = ctlAddress.County;
            entity.State = ctlAddress.State;
            entity.ZipCode = ctlAddress.ZipCode;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.FaxNo = ctlAddress.FaxNo;
            entity.Email = ctlAddress.Email;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;

            entity.IsIncludeAdminValue = chkIsIncludeAdminValue.Checked;
            entity.IsCoverAllAdminCosts = chkIsCoverAllAdminCosts.Checked;
            entity.IsItemRuleUsingDefaultAmountValue = chkIPRAmountDefault.Checked;
            entity.IsGlobalPlafond = true;//chkIsPlavonTypeGlobal.Checked;
            entity.IsAdminFromTotal = chkIsAdminFromTotal.Checked;
            entity.IsAdminCalcBeforeDiscount = chkIsAdminCalcBeforeDiscount.Checked;
            entity.SRPaymentType = cboSRPaymentType.SelectedValue;
            entity.SRPhysicianFeeType = cboSRPhysicianFeeType.SelectedValue;
            entity.SRPhysicianFeeCategory = cboSRPhysicianFeeCategory.SelectedValue;
            entity.IsProrateParamedicFee = chkIsProrata.Checked;
            entity.IsParamedicFeeRemun = chkIsParamedicFeeRemun.Checked;
            entity.IsFeeBrutoFromFeeAmount = false; // tidak dipakai lagi
            entity.IsExcessPlafonToDiscount = chkExcessPlafondToDiscount.Checked;
            entity.IsDiscountProrataToRevenue = chkIsDiscountProrataToRevenue.Checked;
            entity.RoundingTransaction = Convert.ToDecimal(txtRoundingTransaction.Value);
            entity.IsUsingRoundingDown = chkIsUsingRoundingDown.Checked;

            entity.ReportRLID = cboReportRLID.SelectedValue;
            entity.RlMasterReportItemID = string.IsNullOrEmpty(cboRlMasterReportItemID.SelectedValue) ? 0 : Convert.ToInt32(cboRlMasterReportItemID.SelectedValue);

            entity.VirtualAccountNo = txtVirtualAccountNo.Text;
            entity.VirtualAccountBank = txtVirtualAccountBank.Text;
            entity.VirtualAccountName = txtVirtualAccountName.Text;

            entity.PrescriptionServiceUnitIdIPR = cboPSuIPR.SelectedValue;
            entity.PrescriptionServiceUnitIdOPR = cboPSuOPR.SelectedValue;
            entity.PrescriptionServiceUnitIdEMR = cboPSuEMR.SelectedValue;

            entity.PrescriptionLocationIdIPR = cboPLocIPR.SelectedValue;
            entity.PrescriptionLocationIdOPR = cboPLocOPR.SelectedValue;
            entity.PrescriptionLocationIdEMR = cboPLocEMR.SelectedValue;

            entity.IsItemServiceRestrictionStatusAllowed = rbtIsItemServiceRestrictionStatusAllowed.SelectedValue == "1";
            entity.IsItemLabRestrictionStatusAllowed = rbtIsItemLabRestrictionStatusAllowed.SelectedValue == "1";
            entity.IsItemRadRestrictionStatusAllowed = rbtIsItemRadRestrictionStatusAllowed.SelectedValue == "1";
            entity.IsItemProductRestrictionStatusAllowed = rbtIsItemProductRestrictionStatusAllowed.SelectedValue == "1";

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Guarantor Item Rule
            foreach (GuarantorItemRule rule in GuarantorItemRules.Where(rule => rule.es.IsAdded || rule.es.IsModified))
            {
                rule.GuarantorID = txtGuarantorID.Text;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = DateTime.Now;
            }

            //Guarantor Item Type Rule
            foreach (GridDataItem dataItem in grdItemTypeRule.MasterTableView.Items)
            {
                GuarantorItemTypeRule rule = GuarantorItemTypeRules.FindByPrimaryKey(txtGuarantorID.Text, dataItem["ItemID"].Text);
                if (rule == null)
                {
                    rule = GuarantorItemTypeRules.AddNew();
                    rule.GuarantorID = txtGuarantorID.Text;
                    rule.SRItemType = dataItem["ItemID"].Text;
                }

                var radio1 = (dataItem["ItemID"].FindControl("Radio1") as HtmlInputRadioButton);
                rule.IsToGuarantor = radio1.Checked;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = DateTime.Now;
            }

            //Guarantor Item Prescription Rule
            foreach (GuarantorItemPrescriptionRule rule in GuarantorItemPrescriptionRules.Where(rule => rule.es.IsAdded || rule.es.IsModified))
            {
                rule.GuarantorID = txtGuarantorID.Text;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = DateTime.Now;
            }

            //Guarantor Service Unit Rule
            foreach (GuarantorServiceUnitRule rule in GuarantorServiceUnitRules.Where(rule => rule.es.IsAdded || rule.es.IsModified))
            {
                rule.GuarantorID = txtGuarantorID.Text;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorBridging rule in GuarantorBridgings.Where(rule => rule.es.IsAdded || rule.es.IsModified))
            {
                rule.GuarantorID = txtGuarantorID.Text;
                rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rule.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorItemRestrictions item in GuarantorItemRestrictionss.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorItemRestrictions item in GuarantorItemServiceRestrictionss.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorItemRestrictions item in GuarantorItemLaboratoryRestrictionss.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorItemRestrictions item in GuarantorItemRadiologyRestrictionss.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorItemGroupProductMargin item in GuarantorItemGroupProductMargins.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorRecipeMarginValue item in GuarantorRecipeMarginValues.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorServiceUnitPlafond item in GuarantorServiceUnitPlafonds.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorAutoBillItem item in GuarantorAutoBillItems.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

            foreach (GuarantorDocumentChecklist item in GuarantorDocumentChecklists.Where(item => item.es.IsAdded || item.es.IsModified))
            {
                item.GuarantorID = txtGuarantorID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Guarantor entity = new Guarantor();
            if (parameters.Length > 0)
            {
                String guarantorID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(guarantorID);
            }
            else
                entity.LoadByPrimaryKey(txtGuarantorID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var guarantor = (Guarantor)entity;
            rblTariffCalculation.SelectedIndex = guarantor.TariffCalculationMethod ?? 0;
            txtGuarantorID.Text = guarantor.GuarantorID;
            txtGuarantorName.Text = guarantor.GuarantorName;
            txtShortName.Text = guarantor.ShortName;
            cboSRGuarantorType.SelectedValue = guarantor.SRGuarantorType;
            txtContractNumber.Text = guarantor.ContractNumber;
            txtContractStart.SelectedDate = guarantor.ContractStart;
            txtContractEnd.SelectedDate = guarantor.ContractEnd;
            txtContractSummary.Text = guarantor.ContractSummary;
            txtNoteCompanyList.Text = guarantor.NoteCompanyList;
            txtTOP.Value = guarantor.TermOfPayment ?? 0;
            txtContactPerson.Text = guarantor.ContactPerson;
            chkIsActive.Checked = guarantor.IsActive ?? false;
            cboSRBusinessMethod.SelectedValue = guarantor.SRBusinessMethod;
            cboSRTariffType.SelectedValue = guarantor.SRTariffType;
            cboSRGuarantorRuleType.SelectedValue = guarantor.SRGuarantorRuleType;
            txtAmountValue.Value = Convert.ToDouble(guarantor.AmountValue);
            txtOutpatientAmountValue.Value = Convert.ToDouble(guarantor.OutpatientAmountValue);
            txtEmergencyAmountValue.Value = Convert.ToDouble(guarantor.EmergencyAmountValue);
            chkIsAmountInPercent.Checked = guarantor.IsValueInPercent ?? false;
            txtAdminPercentage.Value = Convert.ToDouble(guarantor.AdminPercentage);
            txtAdminPercentageOp.Value = Convert.ToDouble(guarantor.AdminPercentageOp);
            txtRecipeMarginValueNonCompound.Value = Convert.ToDouble(guarantor.RecipeMarginValueNonCompound);

            chkIsUsingDefaultRecipeAmount.Checked = guarantor.IsUsingDefaultRecipeAmount ?? true;           
            chkIsItemRestrictionsFornas.Checked = guarantor.IsItemRestrictionsFornas ?? false;
            chkIsItemRestrictionsFormularium.Checked = guarantor.IsItemRestrictionsFormularium ?? false;
            chkIsItemRestrictionsGeneric.Checked = guarantor.IsItemRestrictionsGeneric ?? false;
            chkIsItemRestrictionsNonGeneric.Checked = guarantor.IsItemRestrictionsNonGeneric ?? false;
            chkIsItemRestrictionsNonGenericLimited.Checked = guarantor.IsItemRestrictionsNonGenericLimited ?? false;

            txtAdminAmountMin.Value = Convert.ToDouble(guarantor.AdminValueMinimum);
            txtAdminAmountMax.Value = Convert.ToDouble(guarantor.AdminAmountLimit);
            txtAdminAmountMinOp.Value = Convert.ToDouble(guarantor.AdminValueMinimumOp);
            txtAdminAmountMaxOp.Value = Convert.ToDouble(guarantor.AdminAmountLimitOp);

            cboPSuIPR.SelectedValue = guarantor.PrescriptionServiceUnitIdIPR;
            cboPSuOPR.SelectedValue = guarantor.PrescriptionServiceUnitIdOPR;
            cboPSuEMR.SelectedValue = guarantor.PrescriptionServiceUnitIdEMR;

            ComboBox.PopulateWithServiceUnitForLocation(cboPLocIPR, cboPSuIPR.SelectedValue);
            ComboBox.PopulateWithServiceUnitForLocation(cboPLocOPR, cboPSuOPR.SelectedValue);
            ComboBox.PopulateWithServiceUnitForLocation(cboPLocEMR, cboPSuEMR.SelectedValue);

            cboPLocIPR.SelectedValue = guarantor.PrescriptionLocationIdIPR;
            cboPLocOPR.SelectedValue = guarantor.PrescriptionLocationIdOPR;
            cboPLocEMR.SelectedValue = guarantor.PrescriptionLocationIdEMR;

            rbtIsItemServiceRestrictionStatusAllowed.SelectedValue = (guarantor.IsItemServiceRestrictionStatusAllowed ?? true) ? "1" : "0";
            rbtIsItemLabRestrictionStatusAllowed.SelectedValue = (guarantor.IsItemLabRestrictionStatusAllowed ?? true) ? "1" : "0";
            rbtIsItemRadRestrictionStatusAllowed.SelectedValue = (guarantor.IsItemRadRestrictionStatusAllowed ?? true) ? "1" : "0";
            rbtIsItemProductRestrictionStatusAllowed.SelectedValue = (guarantor.IsItemProductRestrictionStatusAllowed ?? true) ? "1" : "0";

            if (txtGuarantorID.Text != string.Empty)
            {
                int chartOfAccountId = (guarantor.ChartOfAccountId.HasValue ? guarantor.ChartOfAccountId.Value : 0);
                int subLedgerId = (guarantor.SubLedgerId.HasValue ? guarantor.SubLedgerId.Value : 0);
                int chartOfAccountIdTemporary = (guarantor.ChartOfAccountIdTemporary.HasValue ? guarantor.ChartOfAccountIdTemporary.Value : 0);
                int subLedgerIdTemporary = (guarantor.SubledgerIdTemporary.HasValue ? guarantor.SubledgerIdTemporary.Value : 0);

                //Edited by Fajri
                int chartOfAccountIdIPR = (guarantor.ChartOfAccountIdIPR.HasValue ? guarantor.ChartOfAccountIdIPR.Value : 0);
                int subLedgerIdIPR = (guarantor.SubLedgerIdIPR.HasValue ? guarantor.SubLedgerIdIPR.Value : 0);
                int chartOfAccountIdTemporaryIPR = (guarantor.ChartOfAccountIdTemporaryIPR.HasValue ? guarantor.ChartOfAccountIdTemporaryIPR.Value : 0);
                int subLedgerIdTemporaryIPR = (guarantor.SubledgerIdTemporaryIPR.HasValue ? guarantor.SubledgerIdTemporaryIPR.Value : 0);
                //Edited by Fajri

                int chartOfAccountIdDeposit = (guarantor.ChartOfAccountIdDeposit.HasValue ? guarantor.ChartOfAccountIdDeposit.Value : 0);
                int subLedgerIdDeposit = (guarantor.SubledgerIdDeposit.HasValue ? guarantor.SubledgerIdDeposit.Value : 0);
                int chartOfAccountIdOverPayment = (guarantor.ChartOfAccountIdOverPayment.HasValue ? guarantor.ChartOfAccountIdOverPayment.Value : 0);
                int subLedgerIdOverPayment = (guarantor.SubledgerIdOverPayment.HasValue ? guarantor.SubledgerIdOverPayment.Value : 0);
                int chartOfAccountIdOverPaymentMin = (guarantor.ChartOfAccountIdOverPaymentMin.HasValue ? guarantor.ChartOfAccountIdOverPaymentMin.Value : 0);
                int subLedgerIdOverPaymentMin = (guarantor.SubledgerIdOverPaymentMin.HasValue ? guarantor.SubledgerIdOverPaymentMin.Value : 0);
                int chartOfAccountIdCostParamedicFee = (guarantor.ChartOfAccountIdCostParamedicFee.HasValue ? guarantor.ChartOfAccountIdCostParamedicFee.Value : 0);
                int subLedgerIdCostParamedicFee = (guarantor.SubledgerIdCostParamedicFee.HasValue ? guarantor.SubledgerIdCostParamedicFee.Value : 0);

                cboAccountGroupID.SelectedValue = guarantor.SRGuarantorIncomeGroup;

                if (chartOfAccountId != 0)
                {
                    ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
                    coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
                    coaQ.Where(coaQ.ChartOfAccountId == chartOfAccountId);
                    DataTable dtbCoa = coaQ.LoadDataTable();
                    cboChartOfAccountId.DataSource = dtbCoa;
                    cboChartOfAccountId.DataBind();
                    cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();
                    if (subLedgerId != 0)
                    {
                        SubLedgersQuery slQ = new SubLedgersQuery();
                        slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
                        slQ.Where(slQ.SubLedgerId == subLedgerId);
                        DataTable dtbSl = slQ.LoadDataTable();
                        cboSubledgerId.DataSource = dtbSl;
                        cboSubledgerId.DataBind();
                        cboSubledgerId.SelectedValue = subLedgerId.ToString();
                    }
                    else
                    {
                        cboSubledgerId.Items.Clear();
                        cboSubledgerId.Text = string.Empty;
                    }
                }
                else
                {
                    cboChartOfAccountId.Items.Clear();
                    cboSubledgerId.Items.Clear();
                    cboChartOfAccountId.Text = string.Empty;
                    cboSubledgerId.Text = string.Empty;
                }

                if (chartOfAccountIdTemporary != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdTemporary, chartOfAccountIdTemporary);
                    if (subLedgerIdTemporary != 0)
                        ComboBox.PopulateCboSubLedger(cboSubledgerIdTemporary, subLedgerIdTemporary);
                    else
                        ClearCombobox(cboSubledgerIdTemporary);

                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdTemporary);
                    ClearCombobox(cboSubledgerIdTemporary);
                }

                //Edited by Fajri
                if (chartOfAccountIdIPR != 0)
                {
                    ChartOfAccountsQuery coaQQ = new ChartOfAccountsQuery();
                    coaQQ.Select(coaQQ.ChartOfAccountId, coaQQ.ChartOfAccountCode, coaQQ.ChartOfAccountName);
                    coaQQ.Where(coaQQ.ChartOfAccountId == chartOfAccountIdIPR);
                    DataTable dtbcoa = coaQQ.LoadDataTable();
                    cboChartOfAccountIdIPR.DataSource = dtbcoa;
                    cboChartOfAccountIdIPR.DataBind();
                    cboChartOfAccountIdIPR.SelectedValue = chartOfAccountIdIPR.ToString();
                    if (subLedgerIdIPR != 0)
                    {
                        SubLedgersQuery sq = new SubLedgersQuery();
                        sq.Select(sq.SubLedgerId, sq.SubLedgerName, sq.Description);
                        sq.Where(sq.SubLedgerId == subLedgerIdIPR);
                        DataTable dtbsl = sq.LoadDataTable();
                        cboSubledgerIdIPR.DataSource = dtbsl;
                        cboSubledgerIdIPR.DataBind();
                        cboSubledgerIdIPR.SelectedValue = subLedgerIdIPR.ToString();
                    }
                    else
                    {
                        cboSubledgerIdIPR.Items.Clear();
                        cboSubledgerIdIPR.Text = string.Empty;
                    }
                }
                else
                {
                    cboChartOfAccountIdIPR.Items.Clear();
                    cboSubledgerIdIPR.Items.Clear();
                    cboChartOfAccountIdIPR.Text = String.Empty;
                    cboSubledgerIdIPR.Text = String.Empty;
                }

                if (chartOfAccountIdTemporaryIPR != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdTemporaryIPR, chartOfAccountIdTemporaryIPR);
                    if (subLedgerIdTemporaryIPR != 0)
                    {
                        ComboBox.PopulateCboSubLedger(cboSubledgerIdTemporaryIPR, subLedgerIdTemporaryIPR);
                    }
                    else
                    {
                        ClearCombobox(cboSubledgerIdTemporaryIPR);
                    }
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdTemporaryIPR);
                    ClearCombobox(cboSubledgerIdTemporaryIPR);
                }
                //Edited by Fajri


                if (chartOfAccountIdDeposit != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdDeposit, chartOfAccountIdDeposit);
                    if (subLedgerIdTemporary != 0)
                        ComboBox.PopulateCboSubLedger(cboSubledgerIdDeposit, subLedgerIdDeposit);
                    else
                        ClearCombobox(cboSubledgerIdDeposit);

                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdDeposit);
                    ClearCombobox(cboSubledgerIdDeposit);
                }

                if (chartOfAccountIdOverPayment != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdOverPayment, chartOfAccountIdOverPayment);
                    if (subLedgerIdOverPayment != 0)
                        ComboBox.PopulateCboSubLedger(cboSubledgerIdOverPayment, subLedgerIdOverPayment);
                    else
                        ClearCombobox(cboSubledgerIdOverPayment);

                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdOverPayment);
                    ClearCombobox(cboSubledgerIdOverPayment);
                }
                if (chartOfAccountIdOverPaymentMin != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdOverPaymentMin, chartOfAccountIdOverPaymentMin);
                    if (subLedgerIdOverPaymentMin != 0)
                        ComboBox.PopulateCboSubLedger(cboSubledgerIdOverPaymentMin, subLedgerIdOverPaymentMin);
                    else
                        ClearCombobox(cboSubledgerIdOverPaymentMin);

                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdOverPaymentMin);
                    ClearCombobox(cboSubledgerIdOverPaymentMin);
                }

                if (chartOfAccountIdCostParamedicFee != 0)
                {
                    ComboBox.PopulateCboChartOfAccount(cboChartOfAccountIdCostParamedicFee, chartOfAccountIdCostParamedicFee);
                    if (subLedgerIdCostParamedicFee != 0)
                        ComboBox.PopulateCboChartOfAccount(cboSubledgerIdCostParamedicFee, subLedgerIdCostParamedicFee);
                    else
                        ClearCombobox(cboSubledgerIdCostParamedicFee);

                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCostParamedicFee);
                    ClearCombobox(cboSubledgerIdCostParamedicFee);
                }
            }
            else
            {
                //Edited by Fajri
                cboChartOfAccountId.Items.Clear();
                cboSubledgerId.Items.Clear();
                cboChartOfAccountIdIPR.Items.Clear();
                cboSubledgerIdIPR.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                cboSubledgerId.Text = string.Empty;
                cboChartOfAccountIdIPR.Text = string.Empty;
                cboSubledgerIdIPR.Text = string.Empty;
                ClearCombobox(cboChartOfAccountIdTemporary);
                ClearCombobox(cboSubledgerIdTemporary);
                ClearCombobox(cboChartOfAccountIdTemporaryIPR);
                ClearCombobox(cboSubledgerIdTemporaryIPR);
                ClearCombobox(cboChartOfAccountIdDeposit);
                ClearCombobox(cboSubledgerIdDeposit);
                ClearCombobox(cboChartOfAccountIdOverPayment);
                ClearCombobox(cboSubledgerIdOverPayment);
                //Edited by Fajri
                ClearCombobox(cboChartOfAccountIdOverPaymentMin);
                ClearCombobox(cboSubledgerIdOverPaymentMin);
            }

            chkIsIncludeItemMedical.Checked = guarantor.IsIncludeItemMedical ?? false;
            rblToGuarantorMedical.SelectedIndex = (guarantor.IsIncludeItemMedicalToGuarantor ?? false) ? 0 : 1;

            chkIsIncludeItemNonMedical.Checked = guarantor.IsIncludeItemNonMedical ?? false;
            rblToGuarantorNonMedical.SelectedIndex = (guarantor.IsIncludeItemNonMedicalToGuarantor ?? false) ? 0 : 1;

            chkIsIncludeItemOptic.Checked = guarantor.IsIncludeItemOptic ?? false;
            rblToGuarantorOptic.SelectedIndex = (guarantor.IsIncludeItemOpticToGuarantor ?? false) ? 0 : 1;

            chkIsCoverInpatient.Checked = guarantor.IsCoverInpatient ?? false;
            chkIsCoverOutpatient.Checked = guarantor.IsCoverOutpatient ?? false;
            txtItemMedicMarginPercentage.Value = Convert.ToDouble(guarantor.ItemMedicMarginPercentage);
            cboItemMedicMarginID.SelectedValue = guarantor.ItemMedicMarginID;
            txtItemNonMedicMarginPercentage.Value = Convert.ToDouble(guarantor.ItemNonMedicMarginPercentage);
            cboItemNonMedicMarginID.SelectedValue = guarantor.ItemNonMedicMarginID;

            if (!string.IsNullOrEmpty(guarantor.GuarantorHeaderID))
            {
                var gQ = new GuarantorQuery();
                gQ.Select(gQ.GuarantorID, gQ.GuarantorName);
                gQ.Where(gQ.GuarantorID == guarantor.GuarantorHeaderID);
                DataTable dtbG = gQ.LoadDataTable();
                cboGuarantorHeaderID.DataSource = dtbG;
                cboGuarantorHeaderID.DataBind();
                cboGuarantorHeaderID.SelectedValue = guarantor.GuarantorHeaderID;
                cboGuarantorHeaderID.Text = dtbG.Rows[0]["GuarantorID"].ToString() + " - " + dtbG.Rows[0]["GuarantorName"].ToString();
            }
            else
            {
                cboGuarantorHeaderID.Items.Clear();
                cboGuarantorHeaderID.Text = string.Empty;
            }

            chkIPRAmountDefault.Checked = guarantor.IsItemRuleUsingDefaultAmountValue ?? true;
            chkIsIncludeAdminValue.Checked = guarantor.IsIncludeAdminValue ?? false;
            chkIsCoverAllAdminCosts.Checked = guarantor.IsCoverAllAdminCosts ?? false;
            //chkIsCoverAllAdminCosts.Enabled = guarantor.IsIncludeAdminValue ?? false;
            //chkIsPlavonTypeGlobal.Enabled = (cboSRGuarantorRuleType.SelectedValue == AppSession.Parameter.GuarantorRuleTypePlavon);
            //chkIsPlavonTypeGlobal.Checked = guarantor.IsGlobalPlafond ?? false;
            chkIsAdminFromTotal.Checked = guarantor.IsAdminFromTotal ?? false;
            chkIsAdminCalcBeforeDiscount.Checked = guarantor.IsAdminCalcBeforeDiscount ?? false;
            cboSRPaymentType.SelectedValue = guarantor.SRPaymentType;
            cboSRPhysicianFeeType.SelectedValue = guarantor.SRPhysicianFeeType;
            cboSRPhysicianFeeCategory.SelectedValue = guarantor.SRPhysicianFeeCategory;
            cboSRPhysicianFeeCategory_OnSelectedIndexChanged(cboSRPhysicianFeeCategory, new RadComboBoxSelectedIndexChangedEventArgs(
                cboSRPhysicianFeeCategory.Text, cboSRPhysicianFeeCategory.Text,
                cboSRPhysicianFeeCategory.Text, cboSRPhysicianFeeCategory.Text));
            chkExcessPlafondToDiscount.Checked = guarantor.IsExcessPlafonToDiscount ?? false;
            chkIsDiscountProrataToRevenue.Checked = guarantor.IsDiscountProrataToRevenue ?? false;
            txtRoundingTransaction.Value = Convert.ToDouble(guarantor.RoundingTransaction);
            chkIsUsingRoundingDown.Checked = guarantor.IsUsingRoundingDown ?? false;
            chkIsProrata.Checked = guarantor.IsProrateParamedicFee ?? false;
            chkIsParamedicFeeRemun.Checked = guarantor.IsParamedicFeeRemun ?? false;
            cboReportRLID.SelectedValue = guarantor.ReportRLID;
            if (!string.IsNullOrEmpty(guarantor.ReportRLID))
            {
                PopulateRlMasterReportItemId(guarantor.ReportRLID);
                cboRlMasterReportItemID.SelectedValue = guarantor.RlMasterReportItemID.ToString();
            }
            //if (guarantor.RlMasterReportItemID != null & guarantor.RlMasterReportItemID != 0)
            //{
            //    var rlq = new RlMasterReportItemQuery();
            //    rlq.Where(rlq.RlMasterReportItemID == guarantor.RlMasterReportItemID);
            //    cboRlMasterReportItemID.DataSource = rlq.LoadDataTable();
            //    cboRlMasterReportItemID.DataBind();
            //    cboRlMasterReportItemID.SelectedValue = guarantor.RlMasterReportItemID.ToString();

            //    var rl = new RlMasterReportItem();
            //    if (rl.LoadByPrimaryKey(Convert.ToInt32(guarantor.RlMasterReportItemID)))
            //        cboRlMasterReportItemID.Text = rl.RlMasterReportItemCode + " - " + rl.RlMasterReportItemName;
            //}

            txtVirtualAccountNo.Text = guarantor.VirtualAccountNo;
            txtVirtualAccountBank.Text = guarantor.VirtualAccountBank;
            txtVirtualAccountName.Text = guarantor.VirtualAccountName;

            //Address
            ctlAddress.StreetName = guarantor.StreetName;
            ctlAddress.District = guarantor.District;
            ctlAddress.City = guarantor.City;
            ctlAddress.County = guarantor.County;
            ctlAddress.State = guarantor.State;
            ctlAddress.ZipCode = guarantor.ZipCode;
            ctlAddress.PhoneNo = guarantor.PhoneNo;
            ctlAddress.FaxNo = guarantor.FaxNo;
            ctlAddress.Email = guarantor.Email;
            ctlAddress.MobilePhoneNo = guarantor.MobilePhoneNo;

            PopulateGridDetail();
            PopulateItemBirdgingGrid();
            PopulateItemGroupMarginGrid();
            PopulateItemPlafondGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            GuarantorQuery que = new GuarantorQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.GuarantorID > txtGuarantorID.Text);
                que.OrderBy(que.GuarantorID.Ascending);
            }
            else
            {
                que.Where(que.GuarantorID < txtGuarantorID.Text);
                que.OrderBy(que.GuarantorID.Descending);
            }
            Guarantor entity = new Guarantor();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTypeRule, grdItemTypeRule);
            ajax.AddAjaxSetting(grdGuarantorItemRule, grdGuarantorItemRule);
            ajax.AddAjaxSetting(grdGuarantorItemPrescriptionRule, grdGuarantorItemPrescriptionRule);
            ajax.AddAjaxSetting(grdGuarantorItemPrescriptionByTherapyRule, grdGuarantorItemPrescriptionByTherapyRule);
            
            ajax.AddAjaxSetting(grdUnitRule, grdUnitRule);
            ajax.AddAjaxSetting(grdAliasName, grdAliasName);
            ajax.AddAjaxSetting(grdGuarantorItemServiceRestrictions, grdGuarantorItemServiceRestrictions);
            ajax.AddAjaxSetting(grdGuarantorItemLabRestrictions, grdGuarantorItemLabRestrictions);
            ajax.AddAjaxSetting(grdGuarantorItemRestrictions, grdGuarantorItemRestrictions);
            ajax.AddAjaxSetting(grdItemProductGroupMargin, grdItemProductGroupMargin);
            ajax.AddAjaxSetting(grdGuarantorRecipeAmount, grdGuarantorRecipeAmount);
            ajax.AddAjaxSetting(grdPlafond, grdPlafond);
            ajax.AddAjaxSetting(grdDocumentChecklist, grdDocumentChecklist);

            ajax.AddAjaxSetting(chkIsIncludeAdminValue, chkIsIncludeAdminValue);
            ajax.AddAjaxSetting(chkIsIncludeAdminValue, chkIsCoverAllAdminCosts);

            ajax.AddAjaxSetting(btnImport, grdGuarantorItemRule);
            ajax.AddAjaxSetting(btnImport, grdGuarantorItemPrescriptionRule);
            ajax.AddAjaxSetting(btnImport, pnlInfo);
            ajax.AddAjaxSetting(btnImport, btnImport);
        }

        protected override void OnMenuEditClick()
        {
            chkIsCoverAllAdminCosts.Enabled = chkIsIncludeAdminValue.Checked;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Guarantor());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateGuarantorIdAutomatic) == "Yes")
                txtGuarantorID.Text = GetNewId();

            rblTariffCalculation.SelectedIndex = 0;
            chkIsActive.Checked = true;
            chkIsCoverAllAdminCosts.Enabled = false;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = "GuarantorID='" + txtGuarantorID.Text.Trim() + "'";
            auditLogFilter.TableName = "Guarantor";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtGuarantorID.ReadOnly = (newVal != AppEnum.DataMode.New) || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateGuarantorIdAutomatic) == "Yes";

            RefreshCommandItemGrid(oldVal, newVal);
            RefreshCommandItemGuarantorBridging(newVal);
            RefreshCommandItemGroupMargin(newVal);
            RefreshCommandItemPlafond(newVal);
            RefreshCommandItemAutoBill(newVal);

            cboImportRuleId.Enabled = (newVal == AppEnum.DataMode.Read);
            cboImportItemType.Enabled = (newVal == AppEnum.DataMode.Read);
            cboImportItemGroup.Enabled = (newVal == AppEnum.DataMode.Read);
            rblImportInclude.Enabled = (newVal == AppEnum.DataMode.Read);
            rblImportToGuarantor.Enabled = (newVal == AppEnum.DataMode.Read);
            btnImport.Visible = (newVal == AppEnum.DataMode.Read);
            cboImportSRGuarantorRuleType.Enabled = (newVal == AppEnum.DataMode.Read);
            chkImportIsValueInPercent.Enabled = (newVal == AppEnum.DataMode.Read);
            txtImportAmountValue.ReadOnly = (newVal != AppEnum.DataMode.Read);
            txtImportOutpatientAmountValue.ReadOnly = (newVal != AppEnum.DataMode.Read);
            txtImportEmergencyAmountValue.ReadOnly = (newVal != AppEnum.DataMode.Read);

            txtFilterGIRule.ReadOnly = false;
            txtFilterGIPRule.ReadOnly = false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 230;

            // Url Search & List
            UrlPageSearch = "GuarantorSearch.aspx";
            UrlPageList = "GuarantorList.aspx";

            ProgramID = AppConstant.Program.GUARANTOR;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRGuarantorType, AppEnum.StandardReference.GuarantorType);
                StandardReference.InitializeIncludeSpace(cboSRBusinessMethod, AppEnum.StandardReference.BusinessMethod);
                StandardReference.InitializeIncludeSpace(cboSRTariffType, AppEnum.StandardReference.TariffType);
                StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

                StandardReference.InitializeIncludeSpace(cboAccountGroupID, AppEnum.StandardReference.GuarantorIncomeGroup);
                if (cboAccountGroupID.Items.Count == 2) cboAccountGroupID.SelectedIndex = 1;

                ComboBox.PopulateWithItemProductMargin(cboItemMedicMarginID);
                ComboBox.PopulateWithItemProductMargin(cboItemNonMedicMarginID);

                var ptColl = new PaymentTypeCollection();
                ptColl.Query.Where(ptColl.Query.SRPaymentTypeID.In(AppSession.Parameter.PaymentTypeCorporateAR, AppSession.Parameter.PaymentTypePersonalAR, AppSession.Parameter.PaymentTypeSaldoAR));
                ptColl.LoadAll();

                cboSRPaymentType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var pt in ptColl)
                {
                    cboSRPaymentType.Items.Add(new RadComboBoxItem(pt.PaymentTypeName, pt.SRPaymentTypeID));
                }
                StandardReference.InitializeIncludeSpace(cboSRPhysicianFeeCategory, AppEnum.StandardReference.PhysicianFeeCategory);
                StandardReference.InitializeIncludeSpace(cboSRPhysicianFeeType, AppEnum.StandardReference.PhysicianFeeType);
                StandardReference.InitializeIncludeSpace(cboImportItemType, AppEnum.StandardReference.ItemType);

                cboImportRuleId.Items.Add(new RadComboBoxItem("Guarantor Item Rule", "1"));
                cboImportRuleId.Items.Add(new RadComboBoxItem("Guarantor Item Prescription Rule", "2"));

                StandardReference.InitializeIncludeSpace(cboImportSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

                txtImportAmountValue.Value = 0D;
                txtImportOutpatientAmountValue.Value = 0D;
                txtImportEmergencyAmountValue.Value = 0D;

                //trSRPhysicianFeeCategory.Visible = AppSession.Parameter.IsPhyicianFeeCalcBasedOnGuarantorCategory == "Yes";

                var ApRl = new AppProgramCollection();
                ApRl.Query.Where(ApRl.Query.ProgramID == AppConstant.Program.RlReportV2025, ApRl.Query.IsVisible == true);
                ApRl.LoadAll();
                if (ApRl.Count > 0)
                {
                    var rl = new RlMasterReportV2025Collection();
                    rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID == 18);
                    rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID == 18);
                    rl.LoadAll();
                    cboReportRLID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (RlMasterReportV2025 entity in rl)
                    {
                        cboReportRLID.Items.Add(new RadComboBoxItem(entity.RlMasterReportNo + " - " + entity.RlMasterReportName, entity.RlMasterReportID.ToString()));
                    }
                }
                else
                {
                    var rl = new RlMasterReportCollection();
                    rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID == 19);
                    rl.LoadAll();
                    cboReportRLID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (RlMasterReport entity in rl)
                    {
                        cboReportRLID.Items.Add(new RadComboBoxItem(entity.RlMasterReportNo + " - " + entity.RlMasterReportName, entity.RlMasterReportID.ToString()));
                    }
                }

                // populate prescription location
                ComboBox.PopulateWithServiceUnitForTransaction(cboPSuIPR, BusinessObject.Reference.TransactionCode.Prescription, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboPSuOPR, BusinessObject.Reference.TransactionCode.Prescription, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboPSuEMR, BusinessObject.Reference.TransactionCode.Prescription, false);

                ComboBox.PopulateWithServiceUnitForLocation(cboPLocIPR, cboPSuIPR.SelectedValue);
                ComboBox.PopulateWithServiceUnitForLocation(cboPLocOPR, cboPSuOPR.SelectedValue);
                ComboBox.PopulateWithServiceUnitForLocation(cboPLocEMR, cboPSuEMR.SelectedValue);

                rfvChartOfAccountId.Enabled = AppSession.Parameter.IsGuarantorValidateCOA;

                trIsProrata.Visible = AppSession.Parameter.IsFeeCalculateProporsionalOnPayment;
                trIsRemun.Visible = AppSession.Parameter.IsFeeEnableRemunByGuarantor;

                ShowHideCoaIprOpr();

                chkIsDiscountProrataToRevenue.Visible = AppSession.Parameter.acc_IsEnableGuarDiscProrataToRevenue;
                RadTabStrip1.Tabs[9].Visible = AppSession.Parameter.IsVisibleGuarantorAutoBillItem; //pgAutoBill
            }

            if (!IsCallback)
            {
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, this.Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductMedical, this.Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemService, this.Page);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Guarantor entity = new Guarantor();
            if (entity.LoadByPrimaryKey(txtGuarantorID.Text))
            {
                GuarantorItemRuleCollection rule = new GuarantorItemRuleCollection();
                rule.Query.Where(rule.Query.GuarantorID == entity.GuarantorID);
                rule.LoadAll();
                rule.MarkAllAsDeleted();

                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    rule.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsPhysicianFeeCalcBasedOnGuarantorCategory && string.IsNullOrEmpty(cboSRPhysicianFeeType.SelectedValue))
            {
                args.MessageText = "Physician Fee Type required.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.ValidateGuarantorContractPeriode == "Yes" && txtContractEnd.IsEmpty)
            {
                args.MessageText = "Contract period required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Guarantor();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity, GuarantorItemRules, GuarantorItemTypeRules, GuarantorItemPrescriptionRules, GuarantorItemPrescriptionByTherapyRules, GuarantorServiceUnitRules);
        }

        private void SaveEntity(Guarantor entity, GuarantorItemRuleCollection rule, GuarantorItemTypeRuleCollection typeRule, GuarantorItemPrescriptionRuleCollection prescRule, GuarantorItemPrescriptionByTherapyRuleCollection prescByTherapyRule, GuarantorServiceUnitRuleCollection unitRule)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var IsNew = entity.es.IsAdded;

                entity.Save();
                rule.Save();
                typeRule.Save();
                prescRule.Save();
                prescByTherapyRule.Save();
                unitRule.Save();

                GuarantorBridgings.Save();
                GuarantorItemRuleTariffComponents.Save();
                GuarantorItemRestrictionss.Save();
                GuarantorItemServiceRestrictionss.Save();
                GuarantorItemLaboratoryRestrictionss.Save();
                GuarantorItemRadiologyRestrictionss.Save();
                GuarantorDocumentChecklists.Save();
                GuarantorItemGroupProductMargins.Save();
                GuarantorServiceUnitPlafonds.Save();
                GuarantorAutoBillItems.Save();

                if (chkIsUsingDefaultRecipeAmount.Checked == false)
                {
                    GuarantorRecipeMarginValues.Save();
                }
                //subledger
                var subledgerGroupId = AppSession.Parameter.SubLedgerGroupIdGuarantor;
                if (subledgerGroupId != "")
                {
                    var sub = new BusinessObject.SubLedgers()
                    {
                        GroupId = subledgerGroupId.ToInt(),
                        SubLedgerName = entity.GuarantorID,
                        Description = entity.GuarantorName,
                        DateCreated = DateTime.Now,
                        LastUpdateDateTime = DateTime.Now,
                        CreatedBy = AppSession.UserLogin.UserID,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };

                    sub.Query.Where(sub.Query.SubLedgerName == entity.GuarantorID,
                        sub.Query.GroupId == subledgerGroupId.ToInt());
                    if (sub.Query.Load())
                    {
                        sub.Description = entity.GuarantorName;
                        sub.LastUpdateDateTime = DateTime.Now;
                        sub.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        sub = new BusinessObject.SubLedgers()
                        {
                            GroupId = subledgerGroupId.ToInt(),
                            SubLedgerName = entity.GuarantorID,
                            Description = entity.GuarantorName,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            CreatedBy = AppSession.UserLogin.UserID,
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };
                    }

                    sub.Save();

                    //if (IsNew)
                    {
                        if (sub.SubLedgerId.HasValue)
                        {
                            if ((entity.ChartOfAccountId ?? 0) != 0 && (entity.SubLedgerId ?? 0) == 0)
                            {
                                entity.SubLedgerId = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdTemporary ?? 0) != 0 && (entity.SubledgerIdTemporary ?? 0) == 0)
                            {
                                entity.SubledgerIdTemporary = sub.SubLedgerId;
                            }

                            //Edited by Fajri
                            if ((entity.ChartOfAccountIdIPR ?? 0) != 0 && (entity.SubLedgerId ?? 0) == 0)
                            {
                                entity.SubLedgerIdIPR = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdTemporaryIPR ?? 0) != 0 && (entity.SubledgerIdTemporaryIPR ?? 0) == 0)
                            {
                                entity.SubledgerIdTemporaryIPR = sub.SubLedgerId;
                            }
                            //Edited by Fajri

                            if ((entity.ChartOfAccountIdDeposit ?? 0) != 0 && (entity.SubledgerIdDeposit ?? 0) == 0)
                            {
                                entity.SubledgerIdDeposit = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdOverPayment ?? 0) != 0 && (entity.SubledgerIdOverPayment ?? 0) == 0)
                            {
                                entity.SubledgerIdOverPayment = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdOverPaymentMin ?? 0) != 0 && (entity.SubledgerIdOverPaymentMin ?? 0) == 0)
                            {
                                entity.SubledgerIdOverPaymentMin = sub.SubLedgerId;
                            }
                            entity.Save();
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (AppSession.Parameter.IsPhysicianFeeCalcBasedOnGuarantorCategory && string.IsNullOrEmpty(cboSRPhysicianFeeType.SelectedValue))
            {
                if (cboSRPhysicianFeeCategory.SelectedValue == "01")
                {
                    args.MessageText = "Physician Fee Type required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (AppSession.Parameter.ValidateGuarantorContractPeriode == "Yes" && txtContractEnd.IsEmpty)
            {
                args.MessageText = "Contract period required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Guarantor();
            if (entity.LoadByPrimaryKey(txtGuarantorID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity, GuarantorItemRules, GuarantorItemTypeRules, GuarantorItemPrescriptionRules, GuarantorItemPrescriptionByTherapyRules, GuarantorServiceUnitRules);
            }
        }

        #endregion

        #region Record Detail Method Function

        private void PopulateGridDetail()
        {
            //Display Data Detail
            GuarantorItemRules = null; //Reset Record Detail
            grdGuarantorItemRule.DataSource = GuarantorItemRules;
            grdGuarantorItemRule.DataBind();

            GuarantorItemRuleTariffComponents = null;
            var girc = GuarantorItemRuleTariffComponents;

            GuarantorItemTypeRules = null;
            grdItemTypeRule.DataSource = GuarantorItemTypes;
            grdItemTypeRule.DataBind();

            GuarantorItemPrescriptionRules = null; //Reset Record Detail
            grdGuarantorItemPrescriptionRule.DataSource = GuarantorItemPrescriptionRules;
            grdGuarantorItemPrescriptionRule.DataBind();

            GuarantorItemPrescriptionByTherapyRules = null; //Reset Record Detail
            grdGuarantorItemPrescriptionByTherapyRule.DataSource = GuarantorItemPrescriptionByTherapyRules;
            grdGuarantorItemPrescriptionByTherapyRule.DataBind();

            GuarantorServiceUnitRules = null;
            grdUnitRule.DataSource = GuarantorServiceUnitRules;
            grdUnitRule.DataBind();

            GuarantorBridgings = null;
            grdAliasName.DataSource = GuarantorBridgings;
            grdAliasName.DataBind();

            GuarantorItemRestrictionss = null;
            grdGuarantorItemRestrictions.DataSource = GuarantorItemRestrictionss;
            grdGuarantorItemRestrictions.DataBind();

            GuarantorItemServiceRestrictionss = null;
            grdGuarantorItemServiceRestrictions.DataSource = GuarantorItemServiceRestrictionss;
            grdGuarantorItemServiceRestrictions.DataBind();

            GuarantorItemLaboratoryRestrictionss = null;
            grdGuarantorItemLabRestrictions.DataSource = GuarantorItemLaboratoryRestrictionss;
            grdGuarantorItemLabRestrictions.DataBind();

            GuarantorItemRadiologyRestrictionss = null;
            grdGuarantorItemRadRestrictions.DataSource = GuarantorItemRadiologyRestrictionss;
            grdGuarantorItemRadRestrictions.DataBind();
            
            GuarantorRecipeMarginValues = null;
            grdGuarantorRecipeAmount.DataSource = GuarantorRecipeMarginValues;
            grdGuarantorRecipeAmount.DataBind();
            
            GuarantorDocumentChecklists = null;
            grdDocumentChecklist.DataSource = GuarantorDocumentChecklists;
            grdDocumentChecklist.DataBind();
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdGuarantorItemRule.Columns[0].Visible = isVisible;
            grdGuarantorItemRule.Columns[grdGuarantorItemRule.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemRule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemRules = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemRule.Rebind();

            //---------------------------------------------------
            grdGuarantorItemPrescriptionRule.Columns[0].Visible = isVisible;
            grdGuarantorItemPrescriptionRule.Columns[grdGuarantorItemPrescriptionRule.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemPrescriptionRule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemPrescriptionRules = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemPrescriptionRule.Rebind();

            //---------------------------------------------------
            grdGuarantorItemPrescriptionByTherapyRule.Columns[0].Visible = isVisible;
            grdGuarantorItemPrescriptionByTherapyRule.Columns[grdGuarantorItemPrescriptionByTherapyRule.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemPrescriptionByTherapyRule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemPrescriptionByTherapyRules = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemPrescriptionByTherapyRule.Rebind();

            //---------------------------------------------------
            grdUnitRule.Columns[0].Visible = isVisible;
            grdUnitRule.Columns[grdUnitRule.Columns.Count - 1].Visible = isVisible;
            grdUnitRule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorServiceUnitRules = null;

            //Perbaharui tampilan dan data
            grdUnitRule.Rebind();

            //---------------------------------------------------
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;
            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorBridgings = null;
            //Perbaharui tampilan dan data
            grdAliasName.Rebind();

            //---------------------------------------------------
            grdGuarantorItemRestrictions.Columns[grdGuarantorItemRestrictions.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemRestrictions.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemRestrictionss = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemRestrictions.Rebind();

            //---------------------------------------------------
            grdGuarantorItemServiceRestrictions.Columns[grdGuarantorItemServiceRestrictions.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemServiceRestrictions.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemServiceRestrictionss = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemServiceRestrictions.Rebind();

            //---------------------------------------------------
            grdGuarantorItemLabRestrictions.Columns[grdGuarantorItemLabRestrictions.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemLabRestrictions.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemLaboratoryRestrictionss = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemLabRestrictions.Rebind();

            //---------------------------------------------------
            grdGuarantorItemRadRestrictions.Columns[grdGuarantorItemRadRestrictions.Columns.Count - 1].Visible = isVisible;
            grdGuarantorItemRadRestrictions.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorItemRadiologyRestrictionss = null;
            //Perbaharui tampilan dan data
            grdGuarantorItemRadRestrictions.Rebind();


            //---------------------------------------------------
            grdGuarantorRecipeAmount.Columns[grdGuarantorRecipeAmount.Columns.Count - 1].Visible = isVisible;
            grdGuarantorRecipeAmount.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorRecipeMarginValues = null;
            //Perbaharui tampilan dan data
            grdGuarantorRecipeAmount.Rebind();

            //---------------------------------------------------
            grdDocumentChecklist.Columns[0].Visible = isVisible;
            grdDocumentChecklist.Columns[grdDocumentChecklist.Columns.Count - 1].Visible = isVisible;
            grdDocumentChecklist.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                GuarantorDocumentChecklists = null;
            //Perbaharui tampilan dan data
            grdDocumentChecklist.Rebind();
        }

        #region GuarantorItemRule
        private GuarantorItemRuleCollection GuarantorItemRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemRule"];
                    if (obj != null)
                        return ((GuarantorItemRuleCollection)(obj));
                }

                GuarantorItemRuleCollection coll = new GuarantorItemRuleCollection();
                GuarantorItemRuleQuery query = new GuarantorItemRuleQuery("a");
                ItemQuery iq = new ItemQuery("b");
                AppStandardReferenceItemQuery qSr = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        qSr.ItemName.As("refToSRItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(qSr).On(query.SRGuarantorRuleType == qSr.ItemID & qSr.StandardReferenceID == "GuarantorRuleType");

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collGuarantorItemRule"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemRule"] = value; }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            switch (RadMultiPage2.SelectedIndex)
            {
                case 1:
                    {
                        grdGuarantorItemRule.CurrentPageIndex = 0;
                        grdGuarantorItemRule.Rebind();
                        break;
                    }
                case 2:
                    {
                        grdGuarantorItemPrescriptionRule.CurrentPageIndex = 0;
                        grdGuarantorItemPrescriptionRule.Rebind();
                        break;
                    }
            }

        }

        protected void grdGuarantorItemRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterGIRule.Text.Trim() != string.Empty)
            {
                var ds = from d in GuarantorItemRules
                         where d.ItemName.ToLower().Contains(txtFilterGIRule.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterGIRule.Text.ToLower())
                         select d;
                grdGuarantorItemRule.DataSource = ds;
            }
            else
            {
                grdGuarantorItemRule.DataSource = GuarantorItemRules;
            }
        }

        protected void grdGuarantorItemRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorItemRuleMetadata.ColumnNames.ItemID]);
            GuarantorItemRule entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private GuarantorItemRule FindItemGrid(string itemID)
        {
            GuarantorItemRuleCollection coll = GuarantorItemRules;
            GuarantorItemRule retval = null;
            foreach (GuarantorItemRule rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemRuleMetadata.ColumnNames.ItemID]);
            GuarantorItemRule entity = FindItemGrid(itemID);
            if (entity != null)
            {
                var coll = GuarantorItemRuleTariffComponents;
                coll.Filter = "GuarantorID = '" + entity.GuarantorID + "' AND ItemID = '" + entity.ItemID + "'";
                coll.MarkAllAsDeleted();

                entity.MarkAsDeleted();
            }
        }

        protected void grdGuarantorItemRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemRule entity = GuarantorItemRules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemRule.Rebind();
        }

        private void SetEntityValue(GuarantorItemRule entity, GridCommandEventArgs e)
        {
            GuarantorItemRuleDetail userControl = (GuarantorItemRuleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRGuarantorRuleType = userControl.SRGuarantorRuleType;
                entity.GuarantorRuleTypeName = userControl.GuarantorRuleTypeName;
                entity.AmountValue = userControl.AmountValue;
                entity.IsValueInPercent = userControl.IsValueInPercent;
                entity.IsInclude = userControl.IsInclude;
                entity.IsToGuarantor = userControl.IsToGuarantor;
                entity.OutpatientAmountValue = userControl.OPRAmountValue;
                entity.EmergencyAmountValue = userControl.EMRAmountValue;
                entity.IsByTariffComponent = userControl.IsByTariffComponent;

                if (entity.IsByTariffComponent ?? false)
                {
                    foreach (DataRow row in userControl.GetGuarantorItemRuleTariffComponent.Rows)
                    {
                        var girtc = GuarantorItemRuleTariffComponents.FindByPrimaryKey(entity.GuarantorID, entity.ItemID, row["TariffComponentID"].ToString());
                        if (girtc == null) girtc = GuarantorItemRuleTariffComponents.AddNew();
                        girtc.GuarantorID = entity.GuarantorID;
                        girtc.ItemID = entity.ItemID;
                        girtc.TariffComponentID = row["TariffComponentID"].ToString();

                        var tc = new TariffComponent();
                        tc.LoadByPrimaryKey(girtc.TariffComponentID);
                        girtc.TariffComponentName = tc.TariffComponentName;

                        girtc.AmountValue = Convert.ToDecimal(row["AmountValue"]);
                        girtc.LastUpdateDateTime = DateTime.Now;
                        girtc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        girtc.OutpatientAmountValue = Convert.ToDecimal(row["OutpatientAmountValue"]);
                        girtc.EmergencyAmountValue = Convert.ToDecimal(row["EmergencyAmountValue"]);
                    }
                }
                else
                {
                    var coll = GuarantorItemRuleTariffComponents;
                    coll.Filter = "GuarantorID = '" + entity.GuarantorID + "' AND ItemID = '" + entity.ItemID + "'";
                    coll.MarkAllAsDeleted();
                }
            }
        }
        #endregion

        #region GuarantorItemPrescriptionRule
        private GuarantorItemPrescriptionRuleCollection GuarantorItemPrescriptionRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemPrescriptionRule"];
                    if (obj != null)
                        return ((GuarantorItemPrescriptionRuleCollection)(obj));
                }

                var coll = new GuarantorItemPrescriptionRuleCollection();
                var query = new GuarantorItemPrescriptionRuleQuery("a");
                var iq = new ItemQuery("b");
                var qSr = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        qSr.ItemName.As("refToSRItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(qSr).On(query.SRGuarantorRuleType == qSr.ItemID & qSr.StandardReferenceID == "GuarantorRuleType");

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                Session["collGuarantorItemPrescriptionRule"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemPrescriptionRule"] = value; }
        }

        protected void grdGuarantorItemPrescriptionRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //grdGuarantorItemPrescriptionRule.DataSource = GuarantorItemPrescriptionRules;
            if (txtFilterGIPRule.Text.Trim() != string.Empty)
            {
                var ds = from d in GuarantorItemPrescriptionRules
                         where d.ItemName.ToLower().Contains(txtFilterGIPRule.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterGIPRule.Text.ToLower())
                         select d;
                grdGuarantorItemPrescriptionRule.DataSource = ds;
            }
            else
            {
                grdGuarantorItemPrescriptionRule.DataSource = GuarantorItemPrescriptionRules;
            }
        }

        protected void grdGuarantorItemPrescriptionRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID]);
            GuarantorItemPrescriptionRule entity = FindItemPrescriptionGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private GuarantorItemPrescriptionRule FindItemPrescriptionGrid(string itemID)
        {
            GuarantorItemPrescriptionRuleCollection coll = GuarantorItemPrescriptionRules;
            GuarantorItemPrescriptionRule retval = null;
            foreach (GuarantorItemPrescriptionRule rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemPrescriptionRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID]);
            GuarantorItemPrescriptionRule entity = FindItemPrescriptionGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemPrescriptionRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemPrescriptionRule entity = GuarantorItemPrescriptionRules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemPrescriptionRule.Rebind();
        }

        private void SetEntityValue(GuarantorItemPrescriptionRule entity, GridCommandEventArgs e)
        {
            GuarantorItemPrescriptionRuleDetail userControl = (GuarantorItemPrescriptionRuleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRGuarantorRuleType = userControl.SRGuarantorRuleType;
                entity.GuarantorRuleTypeName = userControl.GuarantorRuleTypeName;
                entity.AmountValue = userControl.AmountValue;
                entity.IsValueInPercent = userControl.IsValueInPercent;
                entity.IsInclude = userControl.IsInclude;
                entity.IsToGuarantor = userControl.IsToGuarantor;
                entity.OutpatientAmountValue = userControl.OPRAmountValue;
                entity.EmergencyAmountValue = userControl.EMRAmountValue;
            }
        }
        #endregion

        #region GuarantorItemPrescriptionByTherapyRule
        private GuarantorItemPrescriptionByTherapyRuleCollection GuarantorItemPrescriptionByTherapyRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemPrescriptionByTherapyRule"];
                    if (obj != null)
                        return ((GuarantorItemPrescriptionByTherapyRuleCollection)(obj));
                }

                var coll = new GuarantorItemPrescriptionByTherapyRuleCollection();
                var query = new GuarantorItemPrescriptionByTherapyRuleQuery("a");
                var qtg = new AppStandardReferenceItemQuery("b");
                var qSr = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        qtg.ItemName.As("refToAppStandardReferenceItem_TherapyGroup"),
                        qSr.ItemName.As("refToAppStandardReferenceItem_GuarantorRuleType")
                    );

                query.InnerJoin(qtg).On(qtg.StandardReferenceID == AppEnum.StandardReference.TherapyGroup & qtg.ItemID == query.SRTherapyGroup);
                query.LeftJoin(qSr).On(qSr.StandardReferenceID == "GuarantorRuleType" & qSr.ItemID == query.SRGuarantorRuleType);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(qtg.ItemName.Ascending);

                coll.Load(query);

                Session["collGuarantorItemPrescriptionByTherapyRule"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemPrescriptionByTherapyRule"] = value; }
        }

        protected void grdGuarantorItemPrescriptionByTherapyRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorItemPrescriptionByTherapyRule.DataSource = GuarantorItemPrescriptionByTherapyRules;
        }

        protected void grdGuarantorItemPrescriptionByTherapyRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup]);
            GuarantorItemPrescriptionByTherapyRule entity = FindItemPrescriptionByTherapyGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private GuarantorItemPrescriptionByTherapyRule FindItemPrescriptionByTherapyGrid(string id)
        {
            GuarantorItemPrescriptionByTherapyRuleCollection coll = GuarantorItemPrescriptionByTherapyRules;
            GuarantorItemPrescriptionByTherapyRule retval = null;
            foreach (GuarantorItemPrescriptionByTherapyRule rec in coll)
            {
                if (rec.SRTherapyGroup.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemPrescriptionByTherapyRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup]);
            GuarantorItemPrescriptionByTherapyRule entity = FindItemPrescriptionByTherapyGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemPrescriptionByTherapyRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemPrescriptionByTherapyRule entity = GuarantorItemPrescriptionByTherapyRules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemPrescriptionByTherapyRule.Rebind();
        }

        private void SetEntityValue(GuarantorItemPrescriptionByTherapyRule entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemPrescriptionByTherapyRuleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.SRTherapyGroup = userControl.SRTherapyGroup;
                entity.TherapyGroupName = userControl.TherapyGroupName;
                entity.SRGuarantorRuleType = userControl.SRGuarantorRuleType;
                entity.GuarantorRuleTypeName = userControl.GuarantorRuleTypeName;
                entity.AmountValue = userControl.AmountValue;
                entity.IsValueInPercent = userControl.IsValueInPercent;
                entity.IsInclude = userControl.IsInclude;
                entity.IsToGuarantor = userControl.IsToGuarantor;
                entity.OutpatientAmountValue = userControl.OPRAmountValue;
                entity.EmergencyAmountValue = userControl.EMRAmountValue;
            }
        }
        #endregion

        #region GuarantorServiceUnitRule
        private GuarantorServiceUnitRuleCollection GuarantorServiceUnitRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorServiceUnitRule"];
                    if (obj != null)
                        return ((GuarantorServiceUnitRuleCollection)(obj));
                }

                var coll = new GuarantorServiceUnitRuleCollection();
                var query = new GuarantorServiceUnitRuleQuery("a");
                var uq = new ServiceUnitQuery("b");

                query.Select
                    (
                        query,
                        uq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );

                query.InnerJoin(uq).On(query.ServiceUnitID == uq.ServiceUnitID);
                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.ServiceUnitID.Ascending);

                coll.Load(query);

                Session["collGuarantorServiceUnitRule"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemPrescriptionRule"] = value; }
        }

        protected void grdUnitRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUnitRule.DataSource = GuarantorServiceUnitRules;
        }

        protected void grdUnitRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String unitId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID]);
            GuarantorServiceUnitRule entity = FindUnitGrid(unitId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private GuarantorServiceUnitRule FindUnitGrid(string unitId)
        {
            GuarantorServiceUnitRuleCollection coll = GuarantorServiceUnitRules;
            GuarantorServiceUnitRule retval = null;
            foreach (GuarantorServiceUnitRule rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdUnitRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String unitId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorServiceUnitRuleMetadata.ColumnNames.ServiceUnitID]);
            GuarantorServiceUnitRule entity = FindUnitGrid(unitId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdUnitRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorServiceUnitRule entity = GuarantorServiceUnitRules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdUnitRule.Rebind();
        }

        private void SetEntityValue(GuarantorServiceUnitRule entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorServiceUnitRulesDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ServiceUnitID = userControl.ServiceUnitId;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.IsCovered = userControl.IsCovered;

            }
        }
        #endregion

        #region GuarantorItemRestrictions - Item Product
        private GuarantorItemRestrictionsCollection GuarantorItemRestrictionss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemRestrictions"];
                    if (obj != null)
                        return ((GuarantorItemRestrictionsCollection)(obj));
                }

                var coll = new GuarantorItemRestrictionsCollection();
                var query = new GuarantorItemRestrictionsQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.GuarantorID == txtGuarantorID.Text,
                            iq.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);


                Session["collGuarantorItemRestrictions"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemRestrictions"] = value; }
        }

        protected void grdGuarantorItemRestrictions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorItemRestrictions.DataSource = GuarantorItemRestrictionss;
        }

        private GuarantorItemRestrictions FindItemRestrictionsGrid(string itemID)
        {
            GuarantorItemRestrictionsCollection coll = GuarantorItemRestrictionss;
            GuarantorItemRestrictions retval = null;
            foreach (GuarantorItemRestrictions rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemRestrictions_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemRestrictionsMetadata.ColumnNames.ItemID]);
            GuarantorItemRestrictions entity = FindItemRestrictionsGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemRestrictions_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemRestrictions entity = GuarantorItemRestrictionss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemRestrictions.Rebind();
        }

        private void SetEntityValue(GuarantorItemRestrictions entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemRestrictionsDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
            }
        }
        #endregion

        #region GuarantorItemRestrictions - Item Service
        private GuarantorItemRestrictionsCollection GuarantorItemServiceRestrictionss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemServiceRestrictions"];
                    if (obj != null)
                        return ((GuarantorItemRestrictionsCollection)(obj));
                }

                var coll = new GuarantorItemRestrictionsCollection();
                var query = new GuarantorItemRestrictionsQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.GuarantorID == txtGuarantorID.Text,
                            iq.SRItemType == ItemType.Service);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);


                Session["collGuarantorItemServiceRestrictions"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemServiceRestrictions"] = value; }
        }

        protected void grdGuarantorItemServiceRestrictions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorItemServiceRestrictions.DataSource = GuarantorItemServiceRestrictionss;
        }

        private GuarantorItemRestrictions FindItemServiceRestrictionsGrid(string itemID)
        {
            GuarantorItemRestrictionsCollection coll = GuarantorItemServiceRestrictionss;
            GuarantorItemRestrictions retval = null;
            foreach (GuarantorItemRestrictions rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemServiceRestrictions_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemRestrictionsMetadata.ColumnNames.ItemID]);
            GuarantorItemRestrictions entity = FindItemServiceRestrictionsGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemServiceRestrictions_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemRestrictions entity = GuarantorItemServiceRestrictionss.AddNew();
            SetEntityValueService(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemServiceRestrictions.Rebind();
        }

        private void SetEntityValueService(GuarantorItemRestrictions entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemRestrictionsServiceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
            }
        }
        #endregion

        #region GuarantorItemRestrictions - Item Laboratory
        private GuarantorItemRestrictionsCollection GuarantorItemLaboratoryRestrictionss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemLaboratoryRestrictions"];
                    if (obj != null)
                        return ((GuarantorItemRestrictionsCollection)(obj));
                }

                var coll = new GuarantorItemRestrictionsCollection();
                var query = new GuarantorItemRestrictionsQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.GuarantorID == txtGuarantorID.Text,
                            iq.SRItemType == ItemType.Laboratory);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);


                Session["collGuarantorItemLaboratoryRestrictions"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemLaboratoryRestrictions"] = value; }
        }

        protected void grdGuarantorItemLabRestrictions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorItemLabRestrictions.DataSource = GuarantorItemLaboratoryRestrictionss;
        }

        private GuarantorItemRestrictions FindItemLabRestrictionsGrid(string itemID)
        {
            GuarantorItemRestrictionsCollection coll = GuarantorItemLaboratoryRestrictionss;
            GuarantorItemRestrictions retval = null;
            foreach (GuarantorItemRestrictions rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemLabRestrictions_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemRestrictionsMetadata.ColumnNames.ItemID]);
            GuarantorItemRestrictions entity = FindItemLabRestrictionsGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemLabRestrictions_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemRestrictions entity = GuarantorItemLaboratoryRestrictionss.AddNew();
            SetEntityValueLab(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemServiceRestrictions.Rebind();
        }

        private void SetEntityValueLab(GuarantorItemRestrictions entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemRestrictionsLaboratoryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
            }
        }
        #endregion

        #region GuarantorItemRestrictions - Item Radiology
        private GuarantorItemRestrictionsCollection GuarantorItemRadiologyRestrictionss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemRadiologyRestrictions"];
                    if (obj != null)
                        return ((GuarantorItemRestrictionsCollection)(obj));
                }

                var coll = new GuarantorItemRestrictionsCollection();
                var query = new GuarantorItemRestrictionsQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.GuarantorID == txtGuarantorID.Text,
                            iq.SRItemType == ItemType.Radiology);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);


                Session["collGuarantorItemRadiologyRestrictions"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemRadiologyRestrictions"] = value; }
        }

        protected void grdGuarantorItemRadRestrictions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorItemRadRestrictions.DataSource = GuarantorItemRadiologyRestrictionss;
        }

        private GuarantorItemRestrictions FindItemRadRestrictionsGrid(string itemID)
        {
            GuarantorItemRestrictionsCollection coll = GuarantorItemRadiologyRestrictionss;
            GuarantorItemRestrictions retval = null;
            foreach (GuarantorItemRestrictions rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorItemRadRestrictions_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemRestrictionsMetadata.ColumnNames.ItemID]);
            GuarantorItemRestrictions entity = FindItemRadRestrictionsGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorItemRadRestrictions_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemRestrictions entity = GuarantorItemRadiologyRestrictionss.AddNew();
            SetEntityValueRad(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorItemLabRestrictions.Rebind();
        }

        private void SetEntityValueRad(GuarantorItemRestrictions entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemRestrictionsRadiologyDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
            }
        }
        #endregion

        #region GuarantorDocumentChecklist
        private GuarantorDocumentChecklistCollection GuarantorDocumentChecklists
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorDocumentChecklist"];
                    if (obj != null)
                        return ((GuarantorDocumentChecklistCollection)(obj));
                }

                var coll = new GuarantorDocumentChecklistCollection();
                var query = new GuarantorDocumentChecklistQuery("a");
                var stdrt = new AppStandardReferenceItemQuery("b");
                var stdfa = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        stdrt.ItemName.As("refToAppStandardReference_RegistrationType"),
                        stdfa.ItemName.As("refToAppStandardReference_DocumentChecklist")
                    );

                query.InnerJoin(stdrt).On(query.SRRegistrationType == stdrt.ItemID &&
                                          stdrt.StandardReferenceID == AppEnum.StandardReference.RegistrationType);
                query.InnerJoin(stdfa).On(query.SRDocumentChecklist == stdfa.ItemID &&
                                          stdfa.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.SRRegistrationType.Ascending);

                coll.Load(query);

                Session["collGuarantorDocumentChecklist"] = coll;
                return coll;
            }
            set { Session["collGuarantorDocumentChecklist"] = value; }
        }

        protected void grdDocumentChecklist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDocumentChecklist.DataSource = GuarantorDocumentChecklists;
        }

        private GuarantorDocumentChecklist FindItemDocumentChecklistGrid(string id)
        {
            GuarantorDocumentChecklistCollection coll = GuarantorDocumentChecklists;
            GuarantorDocumentChecklist retval = null;
            foreach (GuarantorDocumentChecklist rec in coll)
            {
                if (rec.SRRegistrationType.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdDocumentChecklist_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorDocumentChecklistMetadata.ColumnNames.SRRegistrationType]);
            GuarantorDocumentChecklist entity = FindItemDocumentChecklistGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDocumentChecklist_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorDocumentChecklist entity = GuarantorDocumentChecklists.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDocumentChecklist.Rebind();
        }

        private void SetEntityValue(GuarantorDocumentChecklist entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorDocumentChecklistDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.SRRegistrationType = userControl.SRRegistrationType;
                entity.RegistrationType = userControl.RegistrationType;
                entity.SRDocumentChecklist = userControl.SRDocumentChecklist;
                entity.DocumentChecklistName = userControl.DocumentChecklistName;
            }
        }
        #endregion

        private void ClearCombobox(RadComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
        }
        #endregion

        #region Method & Event TextChanged

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region ComboBox ChartOfAccountId

        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where(query.IsDetail == 1);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        #endregion

        #region ComboBox SubledgerId

        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountId.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        #endregion

        protected void cboGuarantorHeaderID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.Select(query.GuarantorID, query.GuarantorName);
            query.Where
                        (
                            query.Or
                            (
                                query.GuarantorID.Like(searchTextContain),
                                query.GuarantorName.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboGuarantorHeaderID.DataSource = dtb;
            cboGuarantorHeaderID.DataBind();
        }

        protected void cboGuarantorHeaderID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #region ComboBox ChartOfAccountIdTemporary

        protected void cboChartOfAccountIdTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdTemporary.DataSource = dtb;
            cboChartOfAccountIdTemporary.DataBind();
        }

        protected void cboChartOfAccountIdTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdTemporary_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdTemporary.Items.Clear();
            cboSubledgerIdTemporary.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdTemporary.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdTemporary.Items.Clear();
                cboChartOfAccountIdTemporary.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox SubledgerIdTemporary

        protected void cboSubledgerIdTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdTemporary.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdTemporary.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdTemporary.DataSource = dtb;
            cboSubledgerIdTemporary.DataBind();
        }

        protected void cboSubledgerIdTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion


        //Edited by Fajri
        #region ComboBox ChartOfAccountIdIPR
        protected void cboChartOfAccountIdIPR_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdIPR.Items.Clear();
            cboSubledgerIdIPR.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdIPR.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdIPR.Items.Clear();
                cboChartOfAccountIdIPR.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdIPR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where(query.IsDetail == 1);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdIPR.DataSource = dtb;
            cboChartOfAccountIdIPR.DataBind();
        }

        protected void cboChartOfAccountIdIPR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBoxSubLedgerIdIPR
        protected void cboSubledgerIdIPR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdIPR.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIPR.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdIPR.DataSource = dtb;
            cboSubledgerIdIPR.DataBind();
        }

        protected void cboSubledgerIdIPR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdTemporaryIPR
        protected void cboChartOfAccountIdTemporaryIPR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdTemporaryIPR.DataSource = dtb;
            cboChartOfAccountIdTemporaryIPR.DataBind();
        }

        protected void cboChartOfAccountIdTemporaryIPR_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdTemporaryIPR.Items.Clear();
            cboSubledgerIdTemporaryIPR.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdTemporaryIPR.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdTemporaryIPR.Items.Clear();
                cboChartOfAccountIdTemporaryIPR.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdTemporaryIPR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBoxSubLedgerIdTemporaryIPR
        protected void cboSubledgerIdTemporaryIPR_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdTemporaryIPR.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdTemporaryIPR.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdTemporaryIPR.DataSource = dtb;
            cboSubledgerIdTemporaryIPR.DataBind();
        }

        protected void cboSubledgerIdTemporaryIPR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion
        //Edited by Fajri


        #region ComboBox GuarantorHeaderID

        #endregion

        protected void grdItemTypeRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTypeRule.DataSource = GuarantorItemTypes;
        }

        protected void cboPharmacyServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cbo = (RadComboBox)o;
            RadComboBox cboL = null;
            switch (cbo.ID)
            {
                case "cboPSuIPR": cboL = cboPLocIPR; break;
                case "cboPSuOPR": cboL = cboPLocOPR; break;
                case "cboPSuEMR": cboL = cboPLocEMR; break;
            }
            ComboBox.PopulateWithServiceUnitForLocation(cboL, e.Value);
            cboL.SelectedIndex = cboL.Items.Count > 1 ? 1 : 0;
        }

        private GuarantorItemTypeRuleCollection GuarantorItemTypeRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemTypeRule"];
                    if (obj != null)
                        return ((GuarantorItemTypeRuleCollection)(obj));
                }

                var coll = new GuarantorItemTypeRuleCollection();
                coll.Query.Where(coll.Query.GuarantorID == txtGuarantorID.Text);
                coll.LoadAll();

                Session["collGuarantorItemTypeRule"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemTypeRule"] = value; }
        }

        private DataTable GuarantorItemTypes
        {
            get
            {
                var std = new AppStandardReferenceItemQuery();
                std.Select(
                    std.ItemID,
                    std.ItemName
                    );
                std.Where(
                    std.StandardReferenceID == AppEnum.StandardReference.ItemType,
                    std.IsActive == true,

                    //nanti diupdate
                    std.ReferenceID == "Service"
                    );
                var tbl = std.LoadDataTable();


                var dc = new DataColumn("GuarantorID", typeof(string));
                dc.DefaultValue = txtGuarantorID.Text;
                tbl.Columns.Add(dc);

                dc = new DataColumn("IsToGuarantor", typeof(bool));
                dc.DefaultValue = true;
                tbl.Columns.Add(dc);

                foreach (var rule in GuarantorItemTypeRules)
                {
                    var row = tbl.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == rule.SRItemType);
                    if (row != null)
                        row["IsToGuarantor"] = rule.IsToGuarantor;
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void cboSRBusinessMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRGuarantorRuleType.SelectedValue = e.Value == AppSession.Parameter.BusinessMethodFlavon ?
                AppSession.Parameter.GuarantorRuleTypePlavon : string.Empty;
            //chkIsPlavonTypeGlobal.Enabled = (cboSRGuarantorRuleType.SelectedValue == AppSession.Parameter.GuarantorRuleTypePlavon);
            //chkIsPlavonTypeGlobal.Checked = true; //(cboSRBusinessMethod.SelectedValue == AppSession.Parameter.BusinessMethodBpjs);
        }

        protected void cboSRGuarantorRuleType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //chkIsAmountInPercent.Enabled = (cboSRGuarantorRuleType.SelectedValue != AppSession.Parameter.GuarantorRuleTypePlavon);
            //chkIsPlavonTypeGlobal.Enabled = (cboSRGuarantorRuleType.SelectedValue == AppSession.Parameter.GuarantorRuleTypePlavon);
        }

        protected void cboImportItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboImportItemGroup.Items.Clear();

            var query = new ItemGroupQuery();
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where(query.IsActive == true, query.SRItemType == e.Value);
            query.OrderBy(query.ItemGroupID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboImportItemGroup.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboImportItemGroup.Items.Add(new RadComboBoxItem(row["ItemGroupName"].ToString(),
                                                                 row["ItemGroupID"].ToString()));
            }
        }

        protected void rblImportInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblRuleType.Visible = rblImportInclude.SelectedIndex != 1;

            cboImportSRGuarantorRuleType.SelectedValue = string.Empty;
            chkImportIsValueInPercent.Checked = false;
            txtImportAmountValue.Value = 0D;
            txtImportOutpatientAmountValue.Value = 0D;
            txtImportEmergencyAmountValue.Value = 0D;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            pnlInfo.Visible = false;
            var g = new Guarantor();
            if (g.LoadByPrimaryKey(txtGuarantorID.Text))
            {
                if (string.IsNullOrEmpty(cboImportItemGroup.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Group required.";
                    return;
                }

                if (rblImportInclude.SelectedIndex == 0 & string.IsNullOrEmpty(cboImportSRGuarantorRuleType.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Rule Type Name required.";
                    return;
                }

                var coll = new ItemCollection();
                coll.Query.Where(coll.Query.ItemGroupID == cboImportItemGroup.SelectedValue, coll.Query.IsActive == true);
                coll.LoadAll();

                if (cboImportRuleId.SelectedValue == "1")
                {
                    foreach (var c in coll)
                    {
                        var rule = new GuarantorItemRule();
                        if (!rule.LoadByPrimaryKey(txtGuarantorID.Text, c.ItemID))
                            rule.AddNew();
                        rule.GuarantorID = txtGuarantorID.Text;
                        rule.ItemID = c.ItemID;
                        var item = new Item();
                        item.LoadByPrimaryKey(c.ItemID);
                        rule.ItemName = item.ItemName;
                        rule.SRGuarantorRuleType = cboImportSRGuarantorRuleType.SelectedValue;
                        rule.GuarantorRuleTypeName = cboImportSRGuarantorRuleType.Text;
                        rule.IsValueInPercent = chkImportIsValueInPercent.Checked;
                        rule.AmountValue = Convert.ToDecimal(txtImportAmountValue.Value);
                        rule.OutpatientAmountValue = Convert.ToDecimal(txtImportOutpatientAmountValue.Value);
                        rule.EmergencyAmountValue = Convert.ToDecimal(txtImportEmergencyAmountValue.Value);
                        rule.IsInclude = rblImportInclude.SelectedIndex == 0;
                        rule.IsToGuarantor = rblImportToGuarantor.SelectedIndex == 0;
                        rule.IsByTariffComponent = false;
                        rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        rule.LastUpdateDateTime = DateTime.Now;

                        rule.Save();
                    }
                }
                else
                {
                    foreach (var c in coll)
                    {
                        var rule = new GuarantorItemPrescriptionRule();
                        if (!rule.LoadByPrimaryKey(txtGuarantorID.Text, c.ItemID))
                            rule.AddNew();

                        rule.GuarantorID = txtGuarantorID.Text;
                        rule.ItemID = c.ItemID;
                        var item = new Item();
                        item.LoadByPrimaryKey(c.ItemID);
                        rule.ItemName = item.ItemName;
                        rule.SRGuarantorRuleType = cboImportSRGuarantorRuleType.SelectedValue;
                        rule.GuarantorRuleTypeName = cboImportSRGuarantorRuleType.Text;
                        rule.IsValueInPercent = chkImportIsValueInPercent.Checked;
                        rule.AmountValue = Convert.ToDecimal(txtImportAmountValue.Value);
                        rule.OutpatientAmountValue = Convert.ToDecimal(txtImportOutpatientAmountValue.Value);
                        rule.EmergencyAmountValue = Convert.ToDecimal(txtImportEmergencyAmountValue.Value);
                        rule.IsInclude = rblImportInclude.SelectedIndex == 0;
                        rule.IsToGuarantor = rblImportToGuarantor.SelectedIndex == 0;
                        rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        rule.LastUpdateDateTime = DateTime.Now;

                        rule.Save();
                    }
                }

                GuarantorItemRules = null;
                grdGuarantorItemRule.Rebind();

                GuarantorItemPrescriptionRules = null;
                grdGuarantorItemPrescriptionRule.Rebind();

                pnlInfo.Visible = true;
                lblInfo.Text = "Import has been completed.";
            }
            else
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Guarantor has no exist.";
            }
        }

        #region ??
        protected void cboCoa_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            ((RadComboBox)sender).DataSource = dtb;
            ((RadComboBox)sender).DataBind();
        }

        protected void cboCoa_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        private void cboCoa_SelectedIndexChanged(RadComboBoxSelectedIndexChangedEventArgs e, RadComboBox cboCOA, RadComboBox cboSL)
        {
            cboSL.Items.Clear();
            cboSL.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboCOA.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboCOA.Items.Clear();
                cboCOA.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox ChartOfAccountIdDeposit

        protected void cboChartOfAccountIdDeposit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdDeposit.DataSource = dtb;
            cboChartOfAccountIdDeposit.DataBind();
        }

        protected void cboChartOfAccountIdDeposit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdDeposit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdDeposit.Items.Clear();
            cboSubledgerIdDeposit.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdDeposit.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdDeposit.Items.Clear();
                cboChartOfAccountIdDeposit.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox SubledgerIdDeposit

        protected void cboSubledgerIdDeposit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdDeposit.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDeposit.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdDeposit.DataSource = dtb;
            cboSubledgerIdDeposit.DataBind();
        }

        protected void cboSubledgerIdDeposit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdOverPayment

        protected void cboChartOfAccountIdOverPayment_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdOverPayment.DataSource = dtb;
            cboChartOfAccountIdOverPayment.DataBind();
        }

        protected void cboChartOfAccountIdOverPayment_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdOverPayment_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdOverPayment.Items.Clear();
            cboSubledgerIdOverPayment.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdOverPayment.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdOverPayment.Items.Clear();
                cboChartOfAccountIdOverPayment.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox SubledgerIdOverPayment

        protected void cboSubledgerIdOverPayment_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdOverPayment.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdOverPayment.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdOverPayment.DataSource = dtb;
            cboSubledgerIdOverPayment.DataBind();
        }

        protected void cboSubledgerIdOverPayment_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdOverPaymentMin

        protected void cboChartOfAccountIdOverPaymentMin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )

                        );
            query.Where(query.IsDetail == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdOverPaymentMin.DataSource = dtb;
            cboChartOfAccountIdOverPaymentMin.DataBind();
        }

        protected void cboChartOfAccountIdOverPaymentMin_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdOverPaymentMin_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdOverPaymentMin.Items.Clear();
            cboSubledgerIdOverPaymentMin.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdOverPaymentMin.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdOverPaymentMin.Items.Clear();
                cboChartOfAccountIdOverPaymentMin.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox SubledgerIdOverPaymentMin

        protected void cboSubledgerIdOverPaymentMin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdOverPaymentMin.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdOverPaymentMin.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdOverPaymentMin.DataSource = dtb;
            cboSubledgerIdOverPaymentMin.DataBind();
        }

        protected void cboSubledgerIdOverPaymentMin_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region COA Fee Dokter
        protected void cboChartOfAccountIdCostParamedicFee_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCoa_SelectedIndexChanged(e, (RadComboBox)o, cboSubledgerIdCostParamedicFee);
        }

        protected void cboSubledgerIdCostParamedicFee_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCostParamedicFee.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCostParamedicFee.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdCostParamedicFee.DataSource = dtb;
            cboSubledgerIdCostParamedicFee.DataBind();
        }

        protected void cboSubledgerIdCostParamedicFee_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        protected void cboSRPhysicianFeeCategory_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            initFeeTypeInterface();
        }

        private void initFeeTypeInterface()
        {
            trSRPhysicianFeeType.Visible = (cboSRPhysicianFeeCategory.SelectedValue != "02");
            trCoaCostParamedic.Visible = (cboSRPhysicianFeeCategory.SelectedValue == "02");
            trSLCostParamedic.Visible = (cboSRPhysicianFeeCategory.SelectedValue == "02");
        }

        protected void cboReportRLID_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateRlMasterReportItemId(e.Value);
        }

        private void PopulateRlMasterReportItemId(string rlMasterReportId)
        {
            cboRlMasterReportItemID.Items.Clear();
            cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(string.Empty, "0"));

            if (!string.IsNullOrEmpty(rlMasterReportId))
            {
                var ApRl = new AppProgramCollection();
                ApRl.Query.Where(ApRl.Query.ProgramID == AppConstant.Program.RlReportV2025, ApRl.Query.IsVisible == true);
                ApRl.LoadAll();
                if (ApRl.Count > 0)
                {
                    var coll = new RlMasterReportItemV2025Collection();
                    coll.Query.Where(coll.Query.RlMasterReportID == Convert.ToInt32(rlMasterReportId), coll.Query.IsActive == true,
                                     coll.Query.RlMasterReportItemID.NotIn(643, 647));
                    coll.LoadAll();

                    foreach (RlMasterReportItemV2025 entity in coll)
                    {
                        cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(entity.RlMasterReportItemCode + " - " + entity.RlMasterReportItemName, entity.RlMasterReportItemID.ToString()));
                    }
                }
                else
                {
                    var coll = new RlMasterReportItemCollection();
                    coll.Query.Where(coll.Query.RlMasterReportID == Convert.ToInt32(rlMasterReportId), coll.Query.IsActive == true,
                                     coll.Query.RlMasterReportItemID.NotIn(643, 647));
                    coll.LoadAll();

                    foreach (RlMasterReportItem entity in coll)
                    {
                        cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(entity.RlMasterReportItemCode + " - " + entity.RlMasterReportItemName, entity.RlMasterReportItemID.ToString()));
                    }
                }
            }
        }

        #region Record Detail Method Function GuarantorBridging

        private GuarantorBridgingCollection GuarantorBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorBridging"];
                    if (obj != null) return ((GuarantorBridgingCollection)(obj));
                }

                var coll = new GuarantorBridgingCollection();

                var query = new GuarantorBridgingQuery("a");
                var asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.GuarantorID == txtGuarantorID.Text);
                coll.Load(query);

                Session["collGuarantorBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collGuarantorBridging"] = value;
            }
        }

        private void RefreshCommandItemGuarantorBridging(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAliasName.Rebind();
        }

        private void PopulateItemBirdgingGrid()
        {
            //Display Data Detail
            GuarantorBridgings = null; //Reset Record Detail
            grdAliasName.DataSource = GuarantorBridgings; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = GuarantorBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorBridgingMetadata.ColumnNames.SRBridgingType]);

            var entity = FindItemBridging(type);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorBridgingMetadata.ColumnNames.SRBridgingType]);

            var entity = FindItemBridging(type);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = GuarantorBridgings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private GuarantorBridging FindItemBridging(String type)
        {
            var coll = GuarantorBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type));
        }

        private void SetEntityValue(GuarantorBridging entity, GridCommandEventArgs e)
        {
            GuarantorAliasDetail userControl = (GuarantorAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.CoverageValue = userControl.CoverageValue;
                entity.MarginValue = userControl.MarginValue;
                entity.IsActive = userControl.IsActive;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingCode = userControl.BridgingCode;
            }
        }

        #endregion

        #region GuarantorItemGroupProductMargin
        private GuarantorItemGroupProductMarginCollection GuarantorItemGroupProductMargins
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemGroupProductMargin"];
                    if (obj != null)
                        return ((GuarantorItemGroupProductMarginCollection)(obj));
                }

                var coll = new GuarantorItemGroupProductMarginCollection();
                var query = new GuarantorItemGroupProductMarginQuery("a");
                var igq = new ItemGroupQuery("b");
                var itq = new AppStandardReferenceItemQuery("c");
                var mq = new ItemProductMarginQuery("d");

                query.Select
                    (
                        query,
                        igq.ItemGroupName.As("refToItemGroup_ItemGroupName"),
                        itq.ItemName.As("refToStdRef_ItemType"),
                        mq.MarginName.As("refToItemProductMargin_MarginName")
                    );

                query.InnerJoin(igq).On(igq.ItemGroupID == query.ItemGroupID);
                query.InnerJoin(itq).On(itq.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString() && itq.ItemID == igq.SRItemType);
                query.LeftJoin(mq).On(mq.MarginID == query.MarginID);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(igq.SRItemType.Ascending, query.ItemGroupID.Ascending);

                coll.Load(query);

                Session["collGuarantorItemGroupProductMargin"] = coll;
                return coll;
            }
            set { Session["collGuarantorItemGroupProductMargin"] = value; }
        }

        private void RefreshCommandItemGroupMargin(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemProductGroupMargin.Columns[0].Visible = isVisible;
            grdItemProductGroupMargin.Columns[grdItemProductGroupMargin.Columns.Count - 1].Visible = isVisible;

            grdItemProductGroupMargin.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemProductGroupMargin.Rebind();
        }

        private void PopulateItemGroupMarginGrid()
        {
            //Display Data Detail
            GuarantorItemGroupProductMargins = null; //Reset Record Detail
            grdItemProductGroupMargin.DataSource = GuarantorItemGroupProductMargins; //Requery
            grdItemProductGroupMargin.MasterTableView.IsItemInserted = false;
            grdItemProductGroupMargin.MasterTableView.ClearEditItems();
            grdItemProductGroupMargin.DataBind();
        }


        protected void grdItemProductGroupMargin_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemProductGroupMargin.DataSource = GuarantorItemGroupProductMargins;
        }

        protected void grdItemProductGroupMargin_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorItemGroupProductMarginMetadata.ColumnNames.ItemGroupID]);
            GuarantorItemGroupProductMargin entity = FindItemGroupMarginGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private GuarantorItemGroupProductMargin FindItemGroupMarginGrid(string id)
        {
            GuarantorItemGroupProductMarginCollection coll = GuarantorItemGroupProductMargins;
            GuarantorItemGroupProductMargin retval = null;
            foreach (GuarantorItemGroupProductMargin rec in coll)
            {
                if (rec.ItemGroupID.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdItemProductGroupMargin_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorItemGroupProductMarginMetadata.ColumnNames.ItemGroupID]);
            GuarantorItemGroupProductMargin entity = FindItemGroupMarginGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemProductGroupMargin_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorItemGroupProductMargin entity = GuarantorItemGroupProductMargins.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemProductGroupMargin.Rebind();
        }

        private void SetEntityValue(GuarantorItemGroupProductMargin entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorItemGroupProductMarginDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ItemGroupID = userControl.ItemGroupID;
                entity.ItemGroupName = userControl.ItemGroupName;
                entity.MarginID = userControl.MarginID;
                entity.MarginName = userControl.MarginName;
                entity.MarginPercentage = userControl.MarginPercentage;
                var ig = new ItemGroup();
                if (ig.LoadByPrimaryKey(entity.ItemGroupID))
                {
                    var itype = new AppStandardReferenceItem();
                    if (itype.LoadByPrimaryKey(AppEnum.StandardReference.ItemType.ToString(), ig.SRItemType))
                        entity.ItemTypeName = itype.ItemName;
                }
            }
        }
        #endregion

        #region GuarantorRecipeAmount
        private GuarantorRecipeMarginValueCollection GuarantorRecipeMarginValues
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorRecipeMarginValue"];
                    if (obj != null)
                        return ((GuarantorRecipeMarginValueCollection)(obj));
                }

                var coll = new GuarantorRecipeMarginValueCollection();
                var query = new GuarantorRecipeMarginValueQuery("a");
                var iq = new GuarantorQuery("b");

                query.Select
                    (
                        query,
                        iq.IsUsingDefaultRecipeAmount,
                        iq.RecipeMarginValueNonCompound
                    );

                query.InnerJoin(iq).On(query.GuarantorID == iq.GuarantorID);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.GuarantorID.Ascending);

                coll.Load(query);


                Session["collGuarantorRecipeMarginValue"] = coll;
                return coll;
            }
            set { Session["collGuarantorRecipeMarginValue"] = value; }
        }       

        protected void grdGuarantorRecipeAmount_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorRecipeAmount.DataSource = GuarantorRecipeMarginValues;
        }

        private GuarantorRecipeMarginValue FindGuarantorRecipeAmountGrid(int counterID)
        {
            GuarantorRecipeMarginValueCollection coll = GuarantorRecipeMarginValues;
            GuarantorRecipeMarginValue retval = null;
            foreach (GuarantorRecipeMarginValue rec in coll)
            {
                if (rec.CounterID.Equals(counterID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void grdGuarantorRecipeAmount_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            if (item == null) return;
            int counterID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID]);
            GuarantorRecipeMarginValue entity = FindGuarantorRecipeAmountGrid(counterID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdGuarantorRecipeAmount_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int counterID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorRecipeMarginValueMetadata.ColumnNames.CounterID]);
            GuarantorRecipeMarginValue entity = FindGuarantorRecipeAmountGrid(counterID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdGuarantorRecipeAmount_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorRecipeMarginValue entity = GuarantorRecipeMarginValues.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantorRecipeAmount.Rebind();
        }

        private void SetEntityValue(GuarantorRecipeMarginValue entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorRecipeAmountDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.StartingValue = userControl.StartingValue;
                entity.EndingValue = userControl.EndingValue;
                entity.RecipeAmount = userControl.RecipeAmount;
            }
        }
        #endregion

        #region Record Detail Method Function GuarantorServiceUnitPlafond
        private GuarantorServiceUnitPlafondCollection GuarantorServiceUnitPlafonds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorServiceUnitPlafond"];
                    if (obj != null)
                        return ((GuarantorServiceUnitPlafondCollection)(obj));
                }

                var coll = new GuarantorServiceUnitPlafondCollection();
                var query = new GuarantorServiceUnitPlafondQuery("a");
                var unitq = new ServiceUnitQuery("b");

                query.Select
                    (
                        query,
                        unitq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );

                query.InnerJoin(unitq).On(unitq.ServiceUnitID == query.ServiceUnitID);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(unitq.ServiceUnitName.Ascending);

                coll.Load(query);

                Session["collGuarantorServiceUnitPlafond"] = coll;
                return coll;
            }
            set { Session["collGuarantorServiceUnitPlafond"] = value; }
        }

        private void RefreshCommandItemPlafond(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPlafond.Columns[0].Visible = isVisible;
            grdPlafond.Columns[grdPlafond.Columns.Count - 1].Visible = isVisible;

            grdPlafond.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdPlafond.Rebind();
        }

        private void PopulateItemPlafondGrid()
        {
            //Display Data Detail
            GuarantorServiceUnitPlafonds = null; //Reset Record Detail
            grdPlafond.DataSource = GuarantorServiceUnitPlafonds; //Requery
            grdPlafond.MasterTableView.IsItemInserted = false;
            grdPlafond.MasterTableView.ClearEditItems();
            grdPlafond.DataBind();
        }


        protected void grdPlafond_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPlafond.DataSource = GuarantorServiceUnitPlafonds;
        }

        protected void grdPlafond_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID]);
            GuarantorServiceUnitPlafond entity = FindItemPlafondGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPlafond_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorServiceUnitPlafond entity = GuarantorServiceUnitPlafonds.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPlafond.Rebind();
        }

        protected void grdPlafond_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID]);
            GuarantorServiceUnitPlafond entity = FindItemPlafondGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private GuarantorServiceUnitPlafond FindItemPlafondGrid(string id)
        {
            GuarantorServiceUnitPlafondCollection coll = GuarantorServiceUnitPlafonds;
            GuarantorServiceUnitPlafond retval = null;
            foreach (GuarantorServiceUnitPlafond rec in coll)
            {
                if (rec.ServiceUnitID.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValue(GuarantorServiceUnitPlafond entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorServiceUnitPlafondDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.PlafondAmount = userControl.PlafondAmount;
            }
        }
        #endregion

        #region Record Detail Method Function GuarantorAutoBillItem
        private GuarantorAutoBillItemCollection GuarantorAutoBillItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorAutoBillItem"];
                    if (obj != null)
                        return ((GuarantorAutoBillItemCollection)(obj));
                }

                var coll = new GuarantorAutoBillItemCollection();
                var query = new GuarantorAutoBillItemQuery("a");
                var unitq = new ServiceUnitQuery("b");
                var itemq = new ItemQuery("c");
                var itemsvq = new ItemServiceQuery("d");
                var itmunitq = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                        query,
                        unitq.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        itemq.ItemName.As("refToItem_ItemName"),
                        @"<ISNULL(e.ItemName, 'X') AS 'refToItem_ItemUnit'>"
                    );

                query.InnerJoin(unitq).On(unitq.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(itemq).On(itemq.ItemID == query.ItemID);
                query.InnerJoin(itemsvq).On(itemsvq.ItemID == query.ItemID);
                query.LeftJoin(itmunitq).On(itmunitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString() && itmunitq.ItemID == itemsvq.ItemID);

                query.Where(query.GuarantorID == txtGuarantorID.Text);

                query.OrderBy(query.ServiceUnitID.Ascending);

                coll.Load(query);

                Session["collGuarantorAutoBillItem"] = coll;
                return coll;
            }
            set { Session["collGuarantorAutoBillItem"] = value; }
        }

        private void RefreshCommandItemAutoBill(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAutoBillItem.Columns[0].Visible = isVisible;
            grdAutoBillItem.Columns[grdAutoBillItem.Columns.Count - 1].Visible = isVisible;

            grdAutoBillItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAutoBillItem.Rebind();
        }

        private void PopulateItemAutoBillGrid()
        {
            //Display Data Detail
            GuarantorAutoBillItems = null; //Reset Record Detail
            grdAutoBillItem.DataSource = GuarantorAutoBillItems; //Requery
            grdAutoBillItem.MasterTableView.IsItemInserted = false;
            grdAutoBillItem.MasterTableView.ClearEditItems();
            grdAutoBillItem.DataBind();
        }

        protected void grdAutoBillItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAutoBillItem.DataSource = GuarantorAutoBillItems;
        }

        protected void grdAutoBillItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String unitId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID]);
            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][GuarantorAutoBillItemMetadata.ColumnNames.ItemID]);
            GuarantorAutoBillItem entity = FindItemAutoBillGrid(unitId, itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdAutoBillItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            GuarantorAutoBillItem entity = GuarantorAutoBillItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAutoBillItem.Rebind();
        }

        protected void grdAutoBillItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String unitId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID]);
            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][GuarantorAutoBillItemMetadata.ColumnNames.ItemID]);
            GuarantorAutoBillItem entity = FindItemAutoBillGrid(unitId, itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private GuarantorAutoBillItem FindItemAutoBillGrid(string unitId, string itemId)
        {
            GuarantorAutoBillItemCollection coll = GuarantorAutoBillItems;
            GuarantorAutoBillItem retval = null;
            foreach (GuarantorAutoBillItem rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId) && rec.ItemID.Equals(itemId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        private void SetEntityValue(GuarantorAutoBillItem entity, GridCommandEventArgs e)
        {
            var userControl = (GuarantorAutoBillItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = txtGuarantorID.Text;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ItemUnit = string.IsNullOrEmpty(userControl.ItemUnit) ? "X" : userControl.ItemUnit;
                entity.Quantity = userControl.Quantity;
                entity.IsGenerateOnRegistration = userControl.IsGenerateOnRegistration;
                entity.IsGenerateOnNewRegistration = userControl.IsGenerateOnNewRegistration;
                entity.IsGenerateOnReferral = userControl.IsGenerateOnReferral;
                entity.IsGenerateOnFirstRegistration = userControl.IsGenerateOnFirstRegistration;
                entity.IsActive = userControl.IsActive;
            }
        }
        #endregion

        protected void chkIsIncludeAdminValue_CheckedChanged(object sender, EventArgs e)
        {
            chkIsCoverAllAdminCosts.Enabled = chkIsIncludeAdminValue.Checked;
            chkIsCoverAllAdminCosts.Checked = false;
        }

        private GuarantorItemRuleTariffComponentCollection GuarantorItemRuleTariffComponents
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collGuarantorItemRuleTariffComponent"];
                    if (obj != null) return ((GuarantorItemRuleTariffComponentCollection)(obj));
                }

                var a = new GuarantorItemRuleTariffComponentQuery("a");
                var b = new TariffComponentQuery("b");

                a.Select(a, b.TariffComponentName.As("refToTariffComponent_TariffComponentName"));
                a.InnerJoin(b).On(a.TariffComponentID == b.TariffComponentID);
                a.Where(a.GuarantorID == txtGuarantorID.Text);
                a.OrderBy(b.TariffComponentID.Ascending);

                var coll = new GuarantorItemRuleTariffComponentCollection();
                coll.Load(a);

                Session["collGuarantorItemRuleTariffComponent"] = coll;
                return coll;
            }
            set
            {
                Session["collGuarantorItemRuleTariffComponent"] = value;
            }
        }

        protected void grdGuarantorItemRule_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = GuarantorItemRuleTariffComponents.Where(g => g.GuarantorID == txtGuarantorID.Text && g.ItemID == e.DetailTableView.ParentItem.GetDataKeyValue("ItemID").ToString());
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(source is RadGrid))
                return;

            var grd = (RadGrid)source;
            if (grd.ID == "grdGuarantorItemServiceRestrictions")
            {
                grd.Rebind();
            }
            else if (grd.ID == "grdGuarantorItemLabRestrictions")
            {
                grd.Rebind();
            }
            else if (grd.ID == "grdGuarantorItemRadRestrictions")
            {
                grd.Rebind();
            }
        }
    }
}
