//TODO: Field SRPurchaseUnit di table SupplierItem jangan dipakai, nanti dihapus

using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewId()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.SupplierId);

            return _autoNumber.LastCompleteNumber;
        }

        private void SetEntityValue(Supplier entity)
        {
            if (entity.es.IsAdded && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateSupplierIdAutomatic) == "Yes")
            {
                txtSupplierID.Text = GetNewId();
                _autoNumber.Save();
            }

            entity.SupplierID = txtSupplierID.Text.ToUpper();
            entity.SupplierName = txtSupplierName.Text;
            entity.ShortName = txtShortName.Text;
            entity.Branch = txtBranch.Text;
            entity.SRSupplierType = cboSRSupplierType.SelectedValue;
            entity.ContractNumber = txtContractNumber.Text;
            entity.ContractStart = txtContractStart.SelectedDate;
            entity.ContractEnd = txtContractEnd.SelectedDate;
            entity.ContractSummary = txtContractSummary.Text;
            entity.ContactPerson = txtContactPerson.Text;
            entity.IsPKP = chkIsPKP.Checked;
            entity.TaxRegistrationNo = txtTaxRegistrationNo.Text;
            entity.TermOfPayment = Convert.ToDecimal(txtTermOfPayment.Value);
            entity.LeadTime = Convert.ToByte(txtLeadTime.Value);
            entity.CreditLimit = Convert.ToDecimal(txtCreditLimit.Value);
            entity.TaxPercentage = (decimal)txtTaxPercentage.Value;
            entity.IsActive = chkIsActive.Checked;
            entity.SRApAgingDateType = cboSRApAgingDateType.SelectedValue;
            
            entity.PBFLicenseNo = txtPBFLicenseNo.Text;
            entity.PBFLicenseValidDate = dtpPBFLicenseValidDate.SelectedDate;
            entity.BankAccountNo = txtBankAccountNo.Text;
            entity.BankName = txtBankName.Text;
            entity.IsUsingRounding = chkIsUsingRounding.Checked;

            int chartOfAccountIdAP = 0;
            int subLedgerIdAP = 0;
            int.TryParse(cboChartOfAccountIdAP.SelectedValue, out chartOfAccountIdAP);
            int.TryParse(cboSubledgerIdAP.SelectedValue, out subLedgerIdAP);
            entity.ChartOfAccountIdAP = chartOfAccountIdAP;
            entity.SubledgerIdAP = subLedgerIdAP;
            int chartOfAccountIdAPInProcess = 0;
            int subLedgerIdAPInProcess = 0;
            int.TryParse(cboChartOfAccountIdAPInProcess.SelectedValue, out chartOfAccountIdAPInProcess);
            int.TryParse(cboSubledgerIdAPInProcess.SelectedValue, out subLedgerIdAPInProcess);
            entity.ChartOfAccountIdAPInProcess = chartOfAccountIdAPInProcess;
            entity.SubledgerIdAPInProcess = subLedgerIdAPInProcess;

            int chartOfAccountIdAPTemporary = 0;
            int subLedgerIdAPTemporary = 0;
            int.TryParse(cboChartOfAccountIdAPTemporary.SelectedValue, out chartOfAccountIdAPTemporary);
            int.TryParse(cboSubledgerIdAPTemporary.SelectedValue, out subLedgerIdAPTemporary);
            entity.ChartOfAccountIdAPTemporary = chartOfAccountIdAPTemporary;
            entity.SubledgerIdAPTemporary = subLedgerIdAPTemporary;

            int ChartOfAccountIdAPCost = 0;
            int subLedgerIdCost = 0;
            int.TryParse(cboChartOfAccountIdAPCost.SelectedValue, out ChartOfAccountIdAPCost);
            int.TryParse(cboSubledgerIdAPCost.SelectedValue, out subLedgerIdCost);
            entity.ChartOfAccountIdAPCost = ChartOfAccountIdAPCost;
            entity.SubledgerIdAPCost = subLedgerIdCost;

            int ChartOfAccountIdPOReturn = 0;
            int subLedgerIdPOReturn = 0;
            int.TryParse(cboChartOfAccountIdPOReturn.SelectedValue, out ChartOfAccountIdPOReturn);
            int.TryParse(cboSubledgerIdPOReturn.SelectedValue, out subLedgerIdPOReturn);
            entity.ChartOfAccountIdPOReturn = ChartOfAccountIdPOReturn;
            entity.SubledgerIdPOReturn = subLedgerIdPOReturn;

            int chartOfAccountIdAPNonMedic = 0;
            int subLedgerIdAPNonMedic = 0;
            int.TryParse(cboChartOfAccountIdAPNonMedic.SelectedValue, out chartOfAccountIdAPNonMedic);
            int.TryParse(cboSubledgerIdAPNonMedic.SelectedValue, out subLedgerIdAPNonMedic);
            entity.ChartOfAccountIdAPNonMedic = chartOfAccountIdAPNonMedic;
            entity.SubledgerIdAPNonMedic = subLedgerIdAPNonMedic;

            int chartOfAccountIdAPTemporaryNonMedic = 0;
            int subLedgerIdAPTemporaryNonMedic = 0;
            int.TryParse(cboChartOfAccountIdAPTemporaryNonMedical.SelectedValue, out chartOfAccountIdAPTemporaryNonMedic);
            int.TryParse(cboSubledgerIdAPTemporaryNonMedical.SelectedValue, out subLedgerIdAPTemporaryNonMedic);
            entity.ChartOfAccountIdAPTemporaryNonMedic = chartOfAccountIdAPTemporaryNonMedic;
            entity.SubledgerIdAPTemporaryNonMedic = subLedgerIdAPTemporaryNonMedic;

            int ChartOfAccountIdPOReturnNM = 0;
            int subLedgerIdPOReturnNM = 0;
            int.TryParse(cboChartOfAccountIdPOReturnNonMedical.SelectedValue, out ChartOfAccountIdPOReturnNM);
            int.TryParse(cboSubledgerIdPOReturnNonMedical.SelectedValue, out subLedgerIdPOReturnNM);
            entity.ChartOfAccountIdPOReturnNonMedic = ChartOfAccountIdPOReturnNM;
            entity.SubledgerIdPOReturnNonMedic = subLedgerIdPOReturnNM;

            int chartOfAccountIdGrantReceive = 0;
            int subLedgerIdGrantReceive = 0;
            int.TryParse(cboChartOfAccountIdGrantReceive.SelectedValue, out chartOfAccountIdGrantReceive);
            int.TryParse(cboSubledgerIdGrantReceive.SelectedValue, out subLedgerIdGrantReceive);
            entity.ChartOfAccountIdGrantReceive = chartOfAccountIdGrantReceive;
            entity.SubledgerIdGrantReceive = subLedgerIdGrantReceive;

            int chartOfAccountIdGrantReceiveNmed = 0;
            int subLedgerIdGrantReceiveNmed = 0;
            int.TryParse(cboChartOfAccountIdGrantReceiveNmed.SelectedValue, out chartOfAccountIdGrantReceiveNmed);
            int.TryParse(cboSubledgerIdGrantReceiveNmed.SelectedValue, out subLedgerIdGrantReceiveNmed);
            entity.ChartOfAccountIdGrantReceiveNmed = chartOfAccountIdGrantReceiveNmed;
            entity.SubledgerIdGrantReceiveNmed = subLedgerIdGrantReceiveNmed;

            entity.StreetName = ctlAddress.StreetName;
            entity.ZipCode = ctlAddress.ZipCode;
            entity.District = ctlAddress.District;
            entity.County = ctlAddress.County;
            entity.City = ctlAddress.City;
            entity.State = ctlAddress.State;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.FaxNo = ctlAddress.FaxNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (SupplierItem item in SupplierItems.Concat(SupplierItemNonMedicals).Concat(SupplierItemKitchens))
            {
                item.SupplierID = txtSupplierID.Text.ToUpper();

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (SupplierBank item in SupplierBanks)
            {
                item.SupplierID = txtSupplierID.Text.ToUpper();

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (SupplierLocation item in SupplierLocations)
            {
                item.SupplierID = txtSupplierID.Text.ToUpper();

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SupplierQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SupplierID > txtSupplierID.Text);
                que.OrderBy(que.SupplierID.Ascending);
            }
            else
            {
                que.Where(que.SupplierID < txtSupplierID.Text);
                que.OrderBy(que.SupplierID.Descending);
            }

            var entity = new Supplier();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Supplier();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtSupplierID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var supplier = (Supplier)entity;
            txtSupplierID.Text = supplier.SupplierID;
            txtSupplierName.Text = supplier.SupplierName;
            txtShortName.Text = supplier.ShortName;
            txtBranch.Text = supplier.Branch;
            cboSRSupplierType.SelectedValue = supplier.SRSupplierType;
            txtContractNumber.Text = supplier.ContractNumber;
            txtContractStart.SelectedDate = supplier.ContractStart;
            txtContractEnd.SelectedDate = supplier.ContractEnd;
            txtContractSummary.Text = supplier.ContractSummary;
            txtContactPerson.Text = supplier.ContactPerson;
            chkIsPKP.Checked = supplier.IsPKP ?? false;
            txtTaxRegistrationNo.Text = supplier.TaxRegistrationNo;
            txtTermOfPayment.Value = Convert.ToDouble(supplier.TermOfPayment);
            txtLeadTime.Value = supplier.LeadTime;
            txtCreditLimit.Value = Convert.ToDouble(supplier.CreditLimit);
            txtTaxPercentage.Value = Convert.ToDouble(supplier.TaxPercentage);
            chkIsActive.Checked = supplier.IsActive ?? false;
            txtPBFLicenseNo.Text = supplier.PBFLicenseNo;
            dtpPBFLicenseValidDate.SelectedDate = supplier.PBFLicenseValidDate;
            txtBankAccountNo.Text = supplier.BankAccountNo;
            txtBankName.Text = supplier.BankName;
            cboSRApAgingDateType.SelectedValue = supplier.SRApAgingDateType;
            chkIsUsingRounding.Checked = supplier.IsUsingRounding ?? false;

            ctlAddress.StreetName = supplier.StreetName;

            var zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == supplier.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            ctlAddress.District = supplier.District;
            ctlAddress.County = supplier.County;
            ctlAddress.City = supplier.City;
            ctlAddress.State = supplier.State;
            ctlAddress.PhoneNo = supplier.PhoneNo;
            ctlAddress.FaxNo = supplier.FaxNo;
            ctlAddress.MobilePhoneNo = supplier.MobilePhoneNo;
            ctlAddress.Email = supplier.Email;
            if (txtSupplierID.Text != string.Empty)
            {
                int coaAP = (supplier.ChartOfAccountIdAP.HasValue ? supplier.ChartOfAccountIdAP.Value : 0);
                int slAP = (supplier.SubledgerIdAP.HasValue ? supplier.SubledgerIdAP.Value : 0);
                int coaAPInProcess = (supplier.ChartOfAccountIdAPInProcess.HasValue ? supplier.ChartOfAccountIdAPInProcess.Value : 0);
                int slAPInProcess = (supplier.SubledgerIdAPInProcess.HasValue ? supplier.SubledgerIdAPInProcess.Value : 0);
                int coaAPTemporary = (supplier.ChartOfAccountIdAPTemporary.HasValue ? supplier.ChartOfAccountIdAPTemporary.Value : 0);
                int slAPTemporary = (supplier.SubledgerIdAPTemporary.HasValue ? supplier.SubledgerIdAPTemporary.Value : 0);

                int coaAPNM = (supplier.ChartOfAccountIdAPNonMedic.HasValue ? supplier.ChartOfAccountIdAPNonMedic.Value : 0);
                int slAPNM = (supplier.SubledgerIdAPNonMedic.HasValue ? supplier.SubledgerIdAPNonMedic.Value : 0);
                int coaAPTempNM = (supplier.ChartOfAccountIdAPTemporaryNonMedic.HasValue ? supplier.ChartOfAccountIdAPTemporaryNonMedic.Value : 0);
                int slAPTempNM = (supplier.SubledgerIdAPTemporaryNonMedic.HasValue ? supplier.SubledgerIdAPTemporaryNonMedic.Value : 0);
                
                int coaAPCost = (supplier.ChartOfAccountIdAPCost.HasValue ? supplier.ChartOfAccountIdAPCost.Value : 0);
                int slAPCost = (supplier.SubledgerIdAPCost.HasValue ? supplier.SubledgerIdAPCost.Value : 0);
                int coaPOReturn = (supplier.ChartOfAccountIdPOReturn.HasValue ? supplier.ChartOfAccountIdPOReturn.Value : 0);
                int slPOReturn = (supplier.SubledgerIdPOReturn.HasValue ? supplier.SubledgerIdPOReturn.Value : 0);

                int coaPOReturnNM = (supplier.ChartOfAccountIdPOReturnNonMedic.HasValue ? supplier.ChartOfAccountIdPOReturnNonMedic.Value : 0);
                int slPOReturnNM = (supplier.SubledgerIdPOReturnNonMedic.HasValue ? supplier.SubledgerIdPOReturnNonMedic.Value : 0);

                int coaGrantReceive = (supplier.ChartOfAccountIdGrantReceive.HasValue ? supplier.ChartOfAccountIdGrantReceive.Value : 0);
                int slGrantReceive = (supplier.SubledgerIdGrantReceive.HasValue ? supplier.SubledgerIdGrantReceive.Value : 0);

                int coaGrantReceiveNmed = (supplier.ChartOfAccountIdGrantReceive ?? 0);
                int slGrantReceiveNmed = (supplier.SubledgerIdGrantReceive ?? 0);


                if (coaAP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAP, coaAP);
                    if (slAP != 0)
                        PopulateCboSubLedger(cboSubledgerIdAP, slAP);
                    else
                        ClearCombobox(cboSubledgerIdAP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAP);
                    ClearCombobox(cboSubledgerIdAP);
                }
                if (coaAPInProcess != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAPInProcess, coaAPInProcess);
                    if (slAPInProcess != 0)
                        PopulateCboSubLedger(cboSubledgerIdAPInProcess, slAPInProcess);
                    else
                        ClearCombobox(cboSubledgerIdAPInProcess);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAPInProcess);
                    ClearCombobox(cboSubledgerIdAPInProcess);
                }

                if (coaAPTemporary != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAPTemporary, coaAPTemporary);
                    if (slAP != 0)
                        PopulateCboSubLedger(cboSubledgerIdAPTemporary, slAP);
                    else
                        ClearCombobox(cboSubledgerIdAPTemporary);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAPTemporary);
                    ClearCombobox(cboSubledgerIdAPTemporary);
                }

                if (coaAPNM != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAPNonMedic, coaAPNM);
                    if (slAPNM != 0)
                        PopulateCboSubLedger(cboSubledgerIdAPNonMedic, slAPNM);
                    else
                        ClearCombobox(cboSubledgerIdAPNonMedic);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAPNonMedic);
                    ClearCombobox(cboSubledgerIdAPNonMedic);
                }
                if (coaAPTempNM != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAPTemporaryNonMedical, coaAPTempNM);
                    if (slAPTempNM != 0)
                        PopulateCboSubLedger(cboSubledgerIdAPTemporaryNonMedical, slAPTempNM);
                    else
                        ClearCombobox(cboSubledgerIdAPTemporaryNonMedical);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAPTemporaryNonMedical);
                    ClearCombobox(cboSubledgerIdAPTemporaryNonMedical);
                }

                if (coaAPCost != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAPCost, coaAPCost);
                    if (slAPCost != 0)
                        PopulateCboSubLedger(cboSubledgerIdAPCost, slAPCost);
                    else
                        ClearCombobox(cboSubledgerIdAPCost);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAPCost);
                    ClearCombobox(cboSubledgerIdAPCost);
                }

                if (coaPOReturn != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPOReturn, coaPOReturn);
                    if (slPOReturn != 0)
                        PopulateCboSubLedger(cboSubledgerIdPOReturn, slPOReturn);
                    else
                        ClearCombobox(cboSubledgerIdPOReturn);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPOReturn);
                    ClearCombobox(cboSubledgerIdPOReturn);
                }

                if (coaPOReturnNM != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPOReturnNonMedical, coaPOReturnNM);
                    if (slPOReturnNM != 0)
                        PopulateCboSubLedger(cboSubledgerIdPOReturnNonMedical, slPOReturnNM);
                    else
                        ClearCombobox(cboSubledgerIdPOReturnNonMedical);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPOReturnNonMedical);
                    ClearCombobox(cboSubledgerIdPOReturnNonMedical);
                }

                if (coaGrantReceive != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdGrantReceive, coaGrantReceive);
                    if (slGrantReceive != 0)
                        PopulateCboSubLedger(cboSubledgerIdGrantReceive, slGrantReceive);
                    else
                        ClearCombobox(cboSubledgerIdGrantReceive);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdGrantReceive);
                    ClearCombobox(cboSubledgerIdGrantReceive);
                }

                if (coaGrantReceiveNmed != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdGrantReceiveNmed, coaGrantReceiveNmed);
                    if (slGrantReceiveNmed != 0)
                        PopulateCboSubLedger(cboSubledgerIdGrantReceiveNmed, slGrantReceiveNmed);
                    else
                        ClearCombobox(cboSubledgerIdGrantReceiveNmed);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdGrantReceiveNmed);
                    ClearCombobox(cboSubledgerIdGrantReceiveNmed);
                }
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdAP);
                ClearCombobox(cboChartOfAccountIdAPInProcess);
                ClearCombobox(cboChartOfAccountIdAPTemporary);
                ClearCombobox(cboChartOfAccountIdAPNonMedic);
                ClearCombobox(cboChartOfAccountIdAPTemporaryNonMedical);
                ClearCombobox(cboChartOfAccountIdAPCost);
                ClearCombobox(cboChartOfAccountIdPOReturn);
                ClearCombobox(cboChartOfAccountIdGrantReceive);
                ClearCombobox(cboChartOfAccountIdGrantReceiveNmed);

                ClearCombobox(cboSubledgerIdAP);
                ClearCombobox(cboSubledgerIdAPInProcess);
                ClearCombobox(cboSubledgerIdAPTemporary);
                ClearCombobox(cboSubledgerIdAPNonMedic);
                ClearCombobox(cboSubledgerIdAPTemporaryNonMedical);
                ClearCombobox(cboSubledgerIdAPCost);
                ClearCombobox(cboSubledgerIdPOReturn);
                ClearCombobox(cboSubledgerIdGrantReceive);
                ClearCombobox(cboSubledgerIdGrantReceiveNmed);
            }

            PopulateGridDetail();
            PopulateGridDetailNonMedical();
            PopulateGridDetailKitchen();
            PopulateGridBankDetail();
            PopulateGridLocationDetail();
        }

        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        private void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            SubLedgersQuery slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.SubLedgerId == subLedgerID);
            DataTable dtbSl = slQ.LoadDataTable();
            comboBox.DataSource = dtbSl;
            comboBox.DataBind();
            comboBox.SelectedValue = subLedgerID.ToString();
        }

        private void ClearCombobox(RadComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
        }

        protected void cboChartOfAccountIdAP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAP, e);
        }

        protected void cboChartOfAccountIdAPNonMedic_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAPNonMedic, e);
        }

        private void cboCOA_SelectedIndexChanged(RadComboBox sender, RadComboBox relatedSL, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            relatedSL.Items.Clear();
            relatedSL.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    sender.Text = string.Empty;
                    return;
                }
            }
            else
            {
                sender.Items.Clear();
                sender.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAPInProcess_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAPInProcess, e);
        }

        protected void cboSubledgerIdAPInProcess_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboSubledgerIdAPInProcess.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboSubledgerIdAPInProcess.Items.Clear();
                cboSubledgerIdAPInProcess.Text = string.Empty;
                return;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Supplier());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateSupplierIdAutomatic) == "Yes")
                txtSupplierID.Text = GetNewId();

            txtTaxPercentage.Value = AppSession.Parameter.TaxPercentage;
            chkIsActive.Checked = true;
            ctlAddress.ZipCodeCombo.Text = string.Empty;
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
            auditLogFilter.PrimaryKeyData = string.Format("SupplierID='{0}'", txtSupplierID.Text.Trim());
            auditLogFilter.TableName = "Supplier";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSupplierID.ReadOnly = (newVal != AppEnum.DataMode.New) || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateSupplierIdAutomatic) == "Yes";

            RefreshCommandItemGrid(oldVal, newVal);
            RefreshCommandItemNonMedicalGrid(oldVal, newVal);
            RefreshCommandItemKitchenGrid(oldVal, newVal);
            RefreshCommandBankGrid(oldVal, newVal);
            RefreshCommandLocationGrid(oldVal, newVal);

            txtFilterItem.ReadOnly = false;
            txtFilterItemNonMedical.ReadOnly = false;
            txtFilterItemKitchen.ReadOnly = false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "SupplierSearch.aspx";
            UrlPageList = "SupplierList.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.SUPPLIER;

            if (!IsPostBack)
            {
                //Untuk detail entry dari grid, lakukan dgn cara 
                // 1. Register fungsi untuk memanggil popup window
                // 2. Set Button pd RadtextBox di control entry dgn fungsi PopUpSearch.InitializeOnButtonClick();
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductMedical, Page);

                StandardReference.InitializeIncludeSpace(cboSRSupplierType, AppEnum.StandardReference.SupplierType);
                StandardReference.InitializeIncludeSpace(cboSRApAgingDateType, AppEnum.StandardReference.ApAgingDateType);

                trCOAApNonMedic.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;
                trSlAPNonMedic.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;
                trCOAApTempNonMedic.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;
                trSlApTempNonMedic.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;
                trCOAPOReturnNM.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;
                trSlPOReturnNM.Visible = AppSession.Parameter.IsCoaAPNonMedicSeparated;

                chkIsUsingRounding.Visible = AppSession.Parameter.IsUsingRoundingPaymentAP;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Supplier();
            if (entity.LoadByPrimaryKey(txtSupplierID.Text))
            {
                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                    SupplierItems.MarkAllAsDeleted();
                    SupplierItems.Save();
                    SupplierItemNonMedicals.MarkAllAsDeleted();
                    SupplierItemNonMedicals.Save();
                    SupplierItemKitchens.MarkAllAsDeleted();
                    SupplierItemKitchens.Save();
                    SupplierBanks.MarkAllAsDeleted();
                    SupplierBanks.Save();
                    SupplierLocations.MarkAllAsDeleted();
                    SupplierLocations.Save();

                    entity.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (chkIsPKP.Checked)
            {
                if (txtTaxPercentage.Value == 0)
                {
                    args.MessageText = "Tax Percentage must be greater than 0.";
                    args.IsCancel = true;
                    return;
                }
            }

            int chartOfAccountIdAP = 0;
            int.TryParse(cboChartOfAccountIdAP.SelectedValue, out chartOfAccountIdAP);

            int chartOfAccountIdAPNonMedic = 0;
            int.TryParse(cboChartOfAccountIdAPNonMedic.SelectedValue, out chartOfAccountIdAPNonMedic);

            if (chartOfAccountIdAP == 0 && chartOfAccountIdAPNonMedic == 0)
            {
                args.MessageText = "Chart Of Account required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Supplier();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Supplier entity)
        {
            using (var trans = new esTransactionScope())
            {
                var IsNew = entity.es.IsAdded;

                entity.Save();
                SupplierItems.Save();
                SupplierItemNonMedicals.Save();
                SupplierItemKitchens.Save();
                SupplierBanks.Save();
                SupplierLocations.Save();

                //subledger
                var subledgerGroupId = AppSession.Parameter.SubLedgerGroupIdSupplier;
                if (subledgerGroupId != "")
                {
                    var sub = new BusinessObject.SubLedgers()
                    {
                        GroupId = subledgerGroupId.ToInt(),
                        SubLedgerName = entity.SupplierID,
                        Description = entity.SupplierName,
                        DateCreated = DateTime.Now,
                        LastUpdateDateTime = DateTime.Now,
                        CreatedBy = AppSession.UserLogin.UserID,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };

                    sub.Query.Where(sub.Query.SubLedgerName == entity.SupplierID);
                    if (sub.Query.Load())
                    {
                        sub.Description = entity.SupplierName;
                        sub.LastUpdateDateTime = DateTime.Now;
                        sub.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        sub = new BusinessObject.SubLedgers()
                        {
                            GroupId = subledgerGroupId.ToInt(),
                            SubLedgerName = entity.SupplierID,
                            Description = entity.SupplierName,
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
                            if ((entity.ChartOfAccountIdAP ?? 0) != 0 && (entity.SubledgerIdAP ?? 0) == 0)
                            {
                                entity.SubledgerIdAP = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdAPInProcess ?? 0) != 0 && (entity.SubledgerIdAPInProcess ?? 0) == 0)
                            {
                                entity.SubledgerIdAPInProcess = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdAPTemporary ?? 0) != 0 && (entity.SubledgerIdAPTemporary ?? 0) == 0)
                            {
                                entity.SubledgerIdAPTemporary = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdAPCost ?? 0) != 0 && (entity.SubledgerIdAPCost ?? 0) == 0)
                            {
                                entity.SubledgerIdAPCost = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdPOReturn ?? 0) != 0 && (entity.SubledgerIdPOReturn ?? 0) == 0)
                            {
                                entity.SubledgerIdPOReturn = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdAPNonMedic ?? 0) != 0 && (entity.SubledgerIdAPNonMedic ?? 0) == 0)
                            {
                                entity.SubledgerIdAPNonMedic = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdAPTemporaryNonMedic ?? 0) != 0 && (entity.SubledgerIdAPTemporaryNonMedic ?? 0) == 0)
                            {
                                entity.SubledgerIdAPTemporaryNonMedic = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdPOReturnNonMedic ?? 0) != 0 && (entity.SubledgerIdPOReturnNonMedic ?? 0) == 0)
                            {
                                entity.SubledgerIdPOReturnNonMedic = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdGrantReceive ?? 0) != 0 && (entity.SubledgerIdGrantReceive ?? 0) == 0)
                            {
                                entity.SubledgerIdGrantReceive = sub.SubLedgerId;
                            }
                            if ((entity.ChartOfAccountIdGrantReceiveNmed ?? 0) != 0 && (entity.SubledgerIdGrantReceiveNmed ?? 0) == 0)
                            {
                                entity.SubledgerIdGrantReceiveNmed = sub.SubLedgerId;
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
            if (chkIsPKP.Checked)
            {
                if (txtTaxPercentage.Value == 0)
                {
                    args.MessageText = "Tax Percentage must be greater than 0.";
                    args.IsCancel = true;
                    return;
                }
            }

            int chartOfAccountIdAP = 0;
            int.TryParse(cboChartOfAccountIdAP.SelectedValue, out chartOfAccountIdAP);

            int chartOfAccountIdAPNonMedic = 0;
            int.TryParse(cboChartOfAccountIdAPNonMedic.SelectedValue, out chartOfAccountIdAPNonMedic);

            if (chartOfAccountIdAP == 0 && chartOfAccountIdAPNonMedic == 0)
            {
                args.MessageText = "Chart Of Account required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Supplier();
            if (entity.LoadByPrimaryKey(txtSupplierID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private SupplierItemCollection SupplierItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierItem"];
                    if (obj != null)
                        return ((SupplierItemCollection)(obj));
                }

                var coll = new SupplierItemCollection();
                var query = new SupplierItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemProductMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.SupplierID == txtSupplierID.Text, iq.SRItemType == ItemType.Medical);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.DrugDistributionLicenseNo,
                        query.ConversionFactor,
                        iq.SRItemType.As("refToItem_ItemType")
                    );

                query.OrderBy(iq.ItemName.Ascending);

                coll.Load(query);

                Session["collSupplierItem"] = coll;
                return coll;
            }
            set { Session["collSupplierItem"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierItem.Columns[0].Visible = isVisible;
            grdSupplierItem.Columns[grdSupplierItem.Columns.Count - 1].Visible = isVisible;

            grdSupplierItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierItems = null;

            //Perbaharui tampilan dan data
            grdSupplierItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            SupplierItems = null; //Reset Record Detail
            grdSupplierItem.DataSource = SupplierItems;
            grdSupplierItem.MasterTableView.IsItemInserted = false;
            grdSupplierItem.MasterTableView.ClearEditItems();
            grdSupplierItem.DataBind();
        }

        protected void grdSupplierItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItem.Text.Trim() != string.Empty)
            {
                var ds = from d in SupplierItems
                         where d.ItemName.ToLower().Contains(txtFilterItem.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItem.Text.ToLower())
                         select d;
                grdSupplierItem.DataSource = ds;
            }
            else
            {
                grdSupplierItem.DataSource = SupplierItems;
            }
        }

        protected void grdSupplierItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSupplierItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdSupplierItem.Rebind();
        }

        private void SetEntityValue(SupplierItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.IsActive = userControl.IsActive;
            }
        }

        private SupplierItem FindItemGrid(string itemID)
        {
            var coll = SupplierItems;
            SupplierItem retval = null;
            foreach (SupplierItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Item Non Medical

        private SupplierItemCollection SupplierItemNonMedicals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierItemNonMedical"];
                    if (obj != null)
                        return ((SupplierItemCollection)(obj));
                }

                var coll = new SupplierItemCollection();
                var query = new SupplierItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemProductNonMedicQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.SupplierID == txtSupplierID.Text, iq.SRItemType == ItemType.NonMedical);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.DrugDistributionLicenseNo,
                        query.ConversionFactor,
                        iq.SRItemType.As("refToItem_ItemType")
                    );

                query.OrderBy(iq.ItemName.Ascending);

                coll.Load(query);

                Session["collSupplierItemNonMedical"] = coll;
                return coll;
            }
            set { Session["collSupplierItemNonMedical"] = value; }
        }

        private void RefreshCommandItemNonMedicalGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierItemNonMedical.Columns[0].Visible = isVisible;
            grdSupplierItemNonMedical.Columns[grdSupplierItemNonMedical.Columns.Count - 1].Visible = isVisible;

            grdSupplierItemNonMedical.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierItemNonMedicals = null;

            //Perbaharui tampilan dan data
            grdSupplierItemNonMedical.Rebind();
        }

        private void PopulateGridDetailNonMedical()
        {
            //Display Data Detail
            SupplierItemNonMedicals = null; //Reset Record Detail
            grdSupplierItemNonMedical.DataSource = SupplierItemNonMedicals;
            grdSupplierItemNonMedical.MasterTableView.IsItemInserted = false;
            grdSupplierItemNonMedical.MasterTableView.ClearEditItems();
            grdSupplierItemNonMedical.DataBind();
        }

        protected void grdSupplierItemNonMedical_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItem.Text.Trim() != string.Empty)
            {
                var ds = from d in SupplierItemNonMedicals
                         where d.ItemName.ToLower().Contains(txtFilterItem.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItem.Text.ToLower())
                         select d;
                grdSupplierItemNonMedical.DataSource = ds;
            }
            else
            {
                grdSupplierItemNonMedical.DataSource = SupplierItemNonMedicals;
            }
        }

        protected void grdSupplierItemNonMedical_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemNonMedicalGrid(itemID);
            if (entity != null)
                SetEntityValueNonMedical(entity, e);
        }

        protected void grdSupplierItemNonMedical_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemNonMedicalGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierItemNonMedical_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierItemNonMedicals.AddNew();
            SetEntityValueNonMedical(entity, e);

            e.Canceled = true;
            grdSupplierItemNonMedical.Rebind();
        }

        private void SetEntityValueNonMedical(SupplierItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierItemDetailNonMedical)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.IsActive = userControl.IsActive;
            }
        }

        private SupplierItem FindItemNonMedicalGrid(string itemID)
        {
            var coll = SupplierItemNonMedicals;
            SupplierItem retval = null;
            foreach (SupplierItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Item Kitchen

        private SupplierItemCollection SupplierItemKitchens
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierItemKitchen"];
                    if (obj != null)
                        return ((SupplierItemCollection)(obj));
                }

                var coll = new SupplierItemCollection();
                var query = new SupplierItemQuery("a");

                var iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                var prodmedQ = new ItemKitchenQuery("p");
                query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);

                query.Where(query.SupplierID == txtSupplierID.Text, iq.SRItemType == ItemType.Kitchen);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        iq.ItemName.As("refToItem_ItemName"),
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        prodmedQ.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.DrugDistributionLicenseNo,
                        query.ConversionFactor,
                        iq.SRItemType.As("refToItem_ItemType")
                    );

                query.OrderBy(iq.ItemName.Ascending);

                coll.Load(query);

                Session["collSupplierItemKitchen"] = coll;
                return coll;
            }
            set { Session["collSupplierItemKitchen"] = value; }
        }

        private void RefreshCommandItemKitchenGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierItemKitchen.Columns[0].Visible = isVisible;
            grdSupplierItemKitchen.Columns[grdSupplierItemKitchen.Columns.Count - 1].Visible = isVisible;

            grdSupplierItemKitchen.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierItemKitchens = null;

            //Perbaharui tampilan dan data
            grdSupplierItemKitchen.Rebind();
        }

        private void PopulateGridDetailKitchen()
        {
            //Display Data Detail
            SupplierItemKitchens = null; //Reset Record Detail
            grdSupplierItemKitchen.DataSource = SupplierItemKitchens;
            grdSupplierItemKitchen.MasterTableView.IsItemInserted = false;
            grdSupplierItemKitchen.MasterTableView.ClearEditItems();
            grdSupplierItemKitchen.DataBind();
        }

        protected void grdSupplierItemKitchen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItem.Text.Trim() != string.Empty)
            {
                var ds = from d in SupplierItemKitchens
                         where d.ItemName.ToLower().Contains(txtFilterItem.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItem.Text.ToLower())
                         select d;
                grdSupplierItemKitchen.DataSource = ds;
            }
            else
            {
                grdSupplierItemKitchen.DataSource = SupplierItemKitchens;
            }
        }

        protected void grdSupplierItemKitchen_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemKitchenGrid(itemID);
            if (entity != null)
                SetEntityValueKitchen(entity, e);
        }

        protected void grdSupplierItemKitchen_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemKitchenGrid(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierItemKitchen_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierItemKitchens.AddNew();
            SetEntityValueKitchen(entity, e);

            e.Canceled = true;
            grdSupplierItemKitchen.Rebind();
        }

        private void SetEntityValueKitchen(SupplierItem entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierItemDetailKitchen)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.PurchaseDiscount1 = userControl.PurchaseDiscount1;
                entity.PurchaseDiscount2 = userControl.PurchaseDiscount2;
                entity.SRPurchaseUnit = userControl.SRPurchaseUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.PriceInPurchaseUnit = userControl.PriceInPurchaseUnit;
                entity.IsActive = userControl.IsActive;
            }
        }

        private SupplierItem FindItemKitchenGrid(string itemID)
        {
            var coll = SupplierItemKitchens;
            SupplierItem retval = null;
            foreach (SupplierItem rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Bank

        private SupplierBankCollection SupplierBanks
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierBank"];
                    if (obj != null)
                        return ((SupplierBankCollection)(obj));
                }

                var coll = new SupplierBankCollection();
                var query = new SupplierBankQuery("a");

                query.Where(query.SupplierID == txtSupplierID.Text);

                query.Select
                    (
                        query.SupplierID,
                        query.BankAccountNo,
                        query.BankName,
                        query.BankAccountName,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );

                query.OrderBy(query.BankAccountNo.Ascending);

                coll.Load(query);

                Session["collSupplierBank"] = coll;
                return coll;
            }
            set { Session["collSupplierBank"] = value; }
        }

        private void RefreshCommandBankGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierBank.Columns[0].Visible = isVisible;
            grdSupplierBank.Columns[grdSupplierBank.Columns.Count - 1].Visible = isVisible;

            grdSupplierBank.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierBanks = null;

            //Perbaharui tampilan dan data
            grdSupplierBank.Rebind();
        }

        private void PopulateGridBankDetail()
        {
            //Display Data Detail
            SupplierBanks = null; //Reset Record Detail
            grdSupplierBank.DataSource = SupplierBanks;
            grdSupplierBank.MasterTableView.IsItemInserted = false;
            grdSupplierBank.MasterTableView.ClearEditItems();
            grdSupplierBank.DataBind();
        }

        protected void grdSupplierBank_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSupplierBank.DataSource = SupplierBanks;
        }

        protected void grdSupplierBank_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String accNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierBankMetadata.ColumnNames.BankAccountNo]);
            var entity = FindBankGrid(accNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSupplierBank_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String accNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierBankMetadata.ColumnNames.BankAccountNo]);
            var entity = FindBankGrid(accNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierBank_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierBanks.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdSupplierBank.Rebind();
        }

        private void SetEntityValue(SupplierBank entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierBankItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BankAccountNo = userControl.BankAccountNo;
                entity.BankName = userControl.BankName;
                entity.BankAccountName = userControl.BankAccountName;
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        private SupplierBank FindBankGrid(string accNo)
        {
            var coll = SupplierBanks;
            SupplierBank retval = null;
            foreach (SupplierBank rec in coll)
            {
                if (rec.BankAccountNo.Equals(accNo))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Record Detail Method Function - Location

        private SupplierLocationCollection SupplierLocations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSupplierLocation"];
                    if (obj != null)
                        return ((SupplierLocationCollection)(obj));
                }

                var coll = new SupplierLocationCollection();
                var query = new SupplierLocationQuery("a");
                var loc = new LocationQuery("b");
                query.InnerJoin(loc).On(query.LocationID == loc.LocationID);

                query.Where(query.SupplierID == txtSupplierID.Text);

                query.Select
                    (
                        query.SupplierID,
                        query.LocationID,
                        loc.LocationName.As("refToLocation_LocationName"),
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );

                query.OrderBy(query.LocationID.Ascending);

                coll.Load(query);

                Session["collSupplierLocation"] = coll;
                return coll;
            }
            set { Session["collSupplierLocation"] = value; }
        }

        private void RefreshCommandLocationGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSupplierLocation.Columns[0].Visible = isVisible;
            grdSupplierLocation.Columns[grdSupplierLocation.Columns.Count - 1].Visible = isVisible;

            grdSupplierLocation.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SupplierLocations = null;

            //Perbaharui tampilan dan data
            grdSupplierLocation.Rebind();
        }

        private void PopulateGridLocationDetail()
        {
            //Display Data Detail
            SupplierLocations = null; //Reset Record Detail
            grdSupplierLocation.DataSource = SupplierLocations;
            grdSupplierLocation.MasterTableView.IsItemInserted = false;
            grdSupplierLocation.MasterTableView.ClearEditItems();
            grdSupplierLocation.DataBind();
        }

        protected void grdSupplierLocation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSupplierLocation.DataSource = SupplierLocations;
        }

        protected void grdSupplierLocation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            String locId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SupplierLocationMetadata.ColumnNames.LocationID]);
            var entity = FindLocationGrid(locId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSupplierLocation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            String locId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SupplierLocationMetadata.ColumnNames.LocationID]);
            var entity = FindLocationGrid(locId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSupplierLocation_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SupplierLocations.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdSupplierLocation.Rebind();
        }

        private void SetEntityValue(SupplierLocation entity, GridCommandEventArgs e)
        {
            var userControl = (SupplierLocationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SupplierID = txtSupplierID.Text;
                entity.LocationID = userControl.LocationID;
                entity.LocationName = userControl.LocationName;
                entity.IsActive = userControl.IsActive;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        private SupplierLocation FindLocationGrid(string locId)
        {
            var coll = SupplierLocations;
            SupplierLocation retval = null;
            foreach (SupplierLocation rec in coll)
            {
                if (rec.LocationID.Equals(locId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        #region Combobox

        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            RadComboBox cbo = (RadComboBox)sender;

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
            cbo.DataSource = dtb;
            cbo.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdAP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAP, e);
        }

        protected void cboSubledgerIdAPNonMedic_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAP, e);
        }

        private void cboSubledger_ItemRequested(RadComboBox cboSL, RadComboBox cboCOA, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboCOA.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboCOA.SelectedValue));
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
            cboSL.DataSource = dtb;
            cboSL.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdAPInProcess_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdAPInProcess.DataSource = dtb;
            cboChartOfAccountIdAPInProcess.DataBind();
        }

        protected void cboChartOfAccountIdAPInProcess_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdAPInProcess_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAPInProcess, e);
        }

        protected void cboSubledgerIdAPInProcess_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdAPTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdAPTemporary.DataSource = dtb;
            cboChartOfAccountIdAPTemporary.DataBind();
        }

        protected void cboChartOfAccountIdAPTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdAPTemporary_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAPTemporary, e);
        }

        protected void cboSubledgerIdAPTemporary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAPTemporary, e);
        }

        protected void cboChartOfAccountIdAPTemporaryNonMedical_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAPTemporaryNonMedical, e);
        }

        protected void cboSubledgerIdAPTemporaryNonMedical_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAPTemporaryNonMedical, e);
        }

        protected void cboSubledgerIdAPInTemporary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        //
        protected void cboChartOfAccountIdAPCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdAPCost.DataSource = dtb;
            cboChartOfAccountIdAPCost.DataBind();
        }

        protected void cboChartOfAccountIdAPCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdAPCost_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdAPCost, e);
        }

        protected void cboSubledgerIdAPCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdAPCost, e);
        }

        protected void cboSubledgerIdAPCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        // COA PO RET
        protected void cboChartOfAccountIdPOReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdPOReturn.DataSource = dtb;
            cboChartOfAccountIdPOReturn.DataBind();
        }

        protected void cboChartOfAccountIdPOReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountIdPOReturn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdPOReturn, e);
        }

        protected void cboSubledgerIdPOReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdPOReturn, e);
        }

        protected void cboSubledgerIdPOReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdPOReturnNonMedical_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdPOReturnNonMedical, e);
        }

        protected void cboSubledgerIdPOReturnNonMedical_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdPOReturnNonMedical, e);
        }

        protected void cboChartOfAccountIdGrantReceive_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdGrantReceive, e);
        }

        protected void cboSubledgerIdGrantReceive_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdGrantReceive, e);
        }

        protected void cboChartOfAccountIdGrantReceiveNmed_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubledgerIdGrantReceiveNmed, e);
        }

        protected void cboSubledgerIdGrantReceiveNmed_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdGrantReceiveNmed, e);
        }
        #endregion

        protected void btnFilterItem_Click(object sender, ImageClickEventArgs e)
        {
            switch (RadMultiPage2.SelectedIndex)
            {
                case 0:
                    {
                        grdSupplierItem.CurrentPageIndex = 0;
                        grdSupplierItem.Rebind();
                        break;
                    }
                case 1:
                    {
                        grdSupplierItemNonMedical.CurrentPageIndex = 0;
                        grdSupplierItemNonMedical.Rebind();
                        break;
                    }
                case 2:
                    {
                        grdSupplierItemKitchen.CurrentPageIndex = 0;
                        grdSupplierItemKitchen.Rebind();
                        break;
                    }
            }
        }
    }
}