using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SalaryComponentSearch.aspx";
            UrlPageList = "SalaryComponentList.aspx";

            ProgramID = AppConstant.Program.SalaryComponent; //TODO: Isi ProgramID
            txtSalaryComponentID.Text = "0";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRSalaryType, AppEnum.StandardReference.SalaryType);
                StandardReference.InitializeIncludeSpace(cboSRSalaryCategory, AppEnum.StandardReference.SalaryCategory);
                StandardReference.InitializeIncludeSpace(cboSRIncomeTaxMethod, AppEnum.StandardReference.IncomeTaxMethod);
                StandardReference.InitializeIncludeSpace(cboSRDeductionType, AppEnum.StandardReference.DeductionType);
                StandardReference.InitializeIncludeSpace(cboSRJamsostekType, AppEnum.StandardReference.JamsostekType);
                StandardReference.InitializeIncludeSpace(cboSRSalaryComponentGroup, AppEnum.StandardReference.SalaryComponentGroup);
                StandardReference.InitializeIncludeSpace(cboNormalBalance, AppEnum.StandardReference.AcctDbCr);
                StandardReference.InitializeIncludeSpace(cboNormalBalanceThr, AppEnum.StandardReference.AcctDbCr);
                StandardReference.InitializeIncludeSpace(cboNormalBalanceIndirect, AppEnum.StandardReference.AcctDbCr);
                StandardReference.InitializeIncludeSpace(cboNormalBalanceThrIndirect, AppEnum.StandardReference.AcctDbCr);

                trIsKwi.Visible = AppSession.Parameter.IsVisibleKwi;
                trIsEmployeeGrade.Visible = false;
                trIsSalaryTableNumber.Visible = false;
                trSRIncomeTaxMethod.Visible = false;
                trSRDeductionType.Visible = false;
                trSRJamsostekType.Visible = false;
                trIsThr.Visible = !AppSession.Parameter.IsThrIncludeInWageProcess;

                RadTabStrip1.Tabs[2].Visible = AppSession.Parameter.acc_IsAutoJournalPayroll;
                trNormalBalance.Visible = true; //!AppSession.Parameter.acc_IsJournalPayrollWithDefaultNormalBalance;
                trNormalBalanceThr.Visible = true; //!AppSession.Parameter.acc_IsJournalPayrollWithDefaultNormalBalance;

                if (!AppSession.Parameter.acc_IsJournalPayrollWithDirectIndirectCost)
                {
                    trIndirectCost.Visible = false;
                    trIndirectCostThr.Visible = false;
                    lblDirectCost.Text = string.Empty;
                    lblDirectCostThr.Text = string.Empty;
                }
                else
                {
                    lblDirectCost.Text = "DIRECT";
                    lblDirectCostThr.Text = "DIRECT";
                    trSubLedgerId.Visible = false;
                    trSubLedgerIdThr.Visible = false;
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SalaryComponent());

            txtSalaryComponentID.Text = "0";
            cboSRSalaryComponentGroup.SelectedValue = string.Empty;
            cboSRSalaryComponentGroup.Text = string.Empty;
            cboSRSalaryType.SelectedValue = string.Empty;
            cboSRSalaryType.Text = string.Empty;
            cboSRSalaryCategory.SelectedValue = string.Empty;
            cboSRSalaryCategory.Text = string.Empty;
            cboSalaryComponentRoundingID.Text = string.Empty;

            txtFaktorRule.Value = 1;
            txtFaktorRuleDisplay.Text = "1";
            txtValidFrom.SelectedDate = DateTime.Now;
            txtValidTo.SelectedDate = Convert.ToDateTime("1/1/2100");
            chkDisplayInPayRekapReport.Checked = true;
            chkDisplayInPaySlip.Checked = true;
            chkIsDisplayInThrSlip.Checked = false;
            chkIsThr.Checked = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            SalaryComponent entity = new SalaryComponent();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new SalaryComponent();

            entity.Query.es.Top = 1;
            entity.Query.Where(entity.Query.SalaryComponentCode == txtSalaryComponentCode.Text, 
                entity.Query.SalaryComponentID != txtSalaryComponentID.Value.ToInt());
            if (entity.Query.Load())
            {
                args.MessageText = "Salary Component Code already exist";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(cboChartOfAccountId.SelectedValue) && string.IsNullOrEmpty(cboNormalBalance.SelectedValue))
            {
                args.MessageText = "Normal Balance (Payroll) required.";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(cboChartOfAccountIdThr.SelectedValue) && string.IsNullOrEmpty(cboNormalBalanceThr.SelectedValue))
            {
                args.MessageText = "Normal Balance (THR) required.";
                args.IsCancel = true;
                return;
            }

            entity = new SalaryComponent();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new SalaryComponent();
            entity.Query.es.Top = 1;
            entity.Query.Where(entity.Query.SalaryComponentCode == txtSalaryComponentCode.Text, 
                entity.Query.SalaryComponentID != txtSalaryComponentID.Value.ToInt());
            if (entity.Query.Load())
            {
                args.MessageText = "Salary Component Code already exist";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(cboChartOfAccountId.SelectedValue) && string.IsNullOrEmpty(cboNormalBalance.SelectedValue))
            {
                args.MessageText = "Normal Balance (Payroll) required.";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(cboChartOfAccountIdThr.SelectedValue) && string.IsNullOrEmpty(cboNormalBalanceThr.SelectedValue))
            {
                args.MessageText = "Normal Balance (THR) required.";
                args.IsCancel = true;
                return;
            }

            entity = new SalaryComponent();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("SalaryComponentID='{0}'", txtSalaryComponentID.Text.Trim());
            auditLogFilter.TableName = "SalaryComponent";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtSalaryComponentID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemSalaryComponentRuleDefinition(newVal);
            RefreshCommandItemSalaryComponentRuleMatrix(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            SalaryComponent entity = new SalaryComponent();
            if (parameters.Length > 0)
            {
                string salaryComponentID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(salaryComponentID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryComponentID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            SalaryComponent salaryComponent = (SalaryComponent)entity;
            txtSalaryComponentID.Value = Convert.ToDouble(salaryComponent.SalaryComponentID);
            txtSalaryComponentCode.Text = salaryComponent.SalaryComponentCode;
            txtSalaryComponentName.Text = salaryComponent.SalaryComponentName;
            cboSRSalaryComponentGroup.SelectedValue = salaryComponent.SRSalaryComponentGroup;
            cboSRSalaryType.SelectedValue = salaryComponent.SRSalaryType;
            cboSRSalaryCategory.SelectedValue = salaryComponent.SRSalaryCategory;
            cboSRIncomeTaxMethod.SelectedValue = salaryComponent.SRIncomeTaxMethod;
            cboSRDeductionType.SelectedValue = salaryComponent.SRDeductionType;
            cboSRJamsostekType.SelectedValue = salaryComponent.SRJamsostekType;
            txtAmount.Value = Convert.ToDouble(salaryComponent.Amount);
            txtMinAmount.Value = Convert.ToDouble(salaryComponent.MinAmount);
            txtMaxAmount.Value = Convert.ToDouble(salaryComponent.MaxAmount);
            txtFaktorRule.Value = Convert.ToDouble(salaryComponent.FaktorRule);
            txtFaktorRuleDisplay.Text = salaryComponent.FaktorRuleDisplay;

            SalaryComponentRoundingQuery pgQuery = new SalaryComponentRoundingQuery();
            pgQuery.Where(pgQuery.SalaryComponentRoundingID == Convert.ToInt32(salaryComponent.SalaryComponentRoundingID));
            cboSalaryComponentRoundingID.DataSource = pgQuery.LoadDataTable();
            cboSalaryComponentRoundingID.DataBind();
            cboSalaryComponentRoundingID.SelectedValue = salaryComponent.SalaryComponentRoundingID.ToString();

            chkDisplayInPaySlip.Checked = salaryComponent.DisplayInPaySlip ?? false;
            chkDisplayInPayRekapReport.Checked = salaryComponent.DisplayInPayRekapReport ?? false;
            chkIsPeriodicSalary.Checked = salaryComponent.IsPeriodicSalary ?? false;
            chkIsThr.Checked = salaryComponent.IsThr ?? false;
            chkIsDisplayInThrSlip.Checked = salaryComponent.IsDisplayInThrSlip ?? false;

            chkIsOrganizationUnit.Checked = salaryComponent.IsOrganizationUnit ?? false;
            chkIsEmployeeStatus.Checked = salaryComponent.IsEmployeeStatus ?? false;
            chkIsPosition.Checked = salaryComponent.IsPosition ?? false;
            chkIsReligion.Checked = salaryComponent.IsReligion ?? false;
            chkIsEmployee.Checked = salaryComponent.IsEmployee ?? false;
            chkIsEmploymentType.Checked = salaryComponent.IsEmploymentType ?? false;
            chkIsPositionGrade.Checked = salaryComponent.IsPositionGrade ?? false;
            chkIsMaritalStatus.Checked = salaryComponent.IsMaritalStatus ?? false;
            chkIsServiceYear.Checked = salaryComponent.IsServiceYear ?? false;
            chkIsSalaryTableNumber.Checked = salaryComponent.IsSalaryTableNumber ?? false;
            chkIsEmployeeGrade.Checked = salaryComponent.IsEmployeeGrade ?? false;
            chkIsNoOfDependent.Checked = salaryComponent.IsNoOfDependent ?? false;
            chkIsAttedanceMatrixID.Checked = salaryComponent.IsAttedanceMatrixID ?? false;
            chkIsComponent1.Checked = salaryComponent.IsComponent1 ?? false;
            chkIsComponent2.Checked = salaryComponent.IsComponent2 ?? false;
            chkIsComponent3.Checked = salaryComponent.IsComponent3 ?? false;
            txtValidFrom.SelectedDate = salaryComponent.ValidFrom;
            txtValidTo.SelectedDate = salaryComponent.ValidTo;
            chkIsKWI.Checked = salaryComponent.IsKWI ?? false;
            chkEducationLevel.Checked = salaryComponent.IsEducationLevel ?? false;
            chkIsEmployeeType.Checked = salaryComponent.IsEmployeeType ?? false;

            int chartOfAccountId = (salaryComponent.ChartOfAccountId.HasValue ? salaryComponent.ChartOfAccountId.Value : 0);
            int subLedgerId = (salaryComponent.SubLedgerId.HasValue ? salaryComponent.SubLedgerId.Value : 0);
            if (chartOfAccountId != 0)
            {
                PopulateCboChartOfAccount(cboChartOfAccountId, chartOfAccountId);
                if (subLedgerId != 0)
                    PopulateCboSubLedger(cboSubLedgerId, subLedgerId);
                else
                    ClearCombobox(cboSubLedgerId);
            }
            else
            {
                ClearCombobox(cboChartOfAccountId);
                ClearCombobox(cboSubLedgerId);
            }
            int chartOfAccountIdThr = (salaryComponent.ChartOfAccountIdThr.HasValue ? salaryComponent.ChartOfAccountIdThr.Value : 0);
            int subLedgerIdThr = (salaryComponent.SubLedgerIdThr.HasValue ? salaryComponent.SubLedgerIdThr.Value : 0);
            if (chartOfAccountIdThr != 0)
            {
                PopulateCboChartOfAccount(cboChartOfAccountIdThr, chartOfAccountIdThr);
                if (subLedgerIdThr != 0)
                    PopulateCboSubLedger(cboSubLedgerIdThr, subLedgerIdThr);
                else
                    ClearCombobox(cboSubLedgerIdThr);
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdThr);
                ClearCombobox(cboSubLedgerIdThr);
            }
            cboNormalBalance.SelectedValue = salaryComponent.NormalBalance;
            cboNormalBalanceThr.SelectedValue = salaryComponent.NormalBalanceThr;

            int chartOfAccountIdIndirect = (salaryComponent.ChartOfAccountIdIndirect.HasValue ? salaryComponent.ChartOfAccountIdIndirect.Value : 0);
            int subLedgerIdIndirect = (salaryComponent.SubLedgerIdIndirect.HasValue ? salaryComponent.SubLedgerIdIndirect.Value : 0);
            if (chartOfAccountIdIndirect != 0)
            {
                PopulateCboChartOfAccount(cboChartOfAccountIdIndirect, chartOfAccountIdIndirect);
                if (subLedgerIdIndirect != 0)
                    PopulateCboSubLedger(cboSubLedgerIdIndirect, subLedgerIdIndirect);
                else
                    ClearCombobox(cboSubLedgerIdIndirect);
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdIndirect);
                ClearCombobox(cboSubLedgerIdIndirect);
            }
            int chartOfAccountIdThrIndirect = (salaryComponent.ChartOfAccountIdThrIndirect.HasValue ? salaryComponent.ChartOfAccountIdThrIndirect.Value : 0);
            int subLedgerIdThrIndirect = (salaryComponent.SubLedgerIdThrIndirect.HasValue ? salaryComponent.SubLedgerIdThrIndirect.Value : 0);
            if (chartOfAccountIdThrIndirect != 0)
            {
                PopulateCboChartOfAccount(cboChartOfAccountIdThrIndirect, chartOfAccountIdThrIndirect);
                if (subLedgerIdThrIndirect != 0)
                    PopulateCboSubLedger(cboSubLedgerIdThrIndirect, subLedgerIdThrIndirect);
                else
                    ClearCombobox(cboSubLedgerIdThrIndirect);
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdThrIndirect);
                ClearCombobox(cboSubLedgerIdThrIndirect);
            }
            cboNormalBalanceIndirect.SelectedValue = salaryComponent.NormalBalanceIndirect;
            cboNormalBalanceThrIndirect.SelectedValue = salaryComponent.NormalBalanceThrIndirect;

            //Display Data Detail
            PopulateSalaryComponentRuleDefinitionGrid();
            PopulateSalaryComponentRuleMatrixGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SalaryComponent entity)
        {
            entity.SalaryComponentCode = txtSalaryComponentCode.Text;
            entity.SalaryComponentName = txtSalaryComponentName.Text;
            entity.SRSalaryComponentGroup = cboSRSalaryComponentGroup.SelectedValue;
            entity.SRSalaryType = cboSRSalaryType.SelectedValue;
            entity.SRSalaryCategory = cboSRSalaryCategory.SelectedValue;
            entity.SRIncomeTaxMethod = cboSRIncomeTaxMethod.SelectedValue;
            entity.SRDeductionType = cboSRDeductionType.SelectedValue;
            entity.SRJamsostekType = cboSRJamsostekType.SelectedValue;
            entity.Amount = Convert.ToDecimal(txtAmount.Value);
            entity.MinAmount = Convert.ToDecimal(txtMinAmount.Value);
            entity.MaxAmount = Convert.ToDecimal(txtMaxAmount.Value);
            entity.FaktorRule = Convert.ToDouble(txtFaktorRule.Value);
            entity.FaktorRuleDisplay = txtFaktorRuleDisplay.Text;

            entity.SalaryComponentRoundingID = Convert.ToInt32(cboSalaryComponentRoundingID.SelectedValue);
            entity.DisplayInPaySlip = chkDisplayInPaySlip.Checked;
            entity.DisplayInPayRekapReport = chkDisplayInPayRekapReport.Checked;
            entity.IsPeriodicSalary = chkIsPeriodicSalary.Checked;
            entity.IsThr = chkIsThr.Checked;
            entity.IsDisplayInThrSlip = chkIsDisplayInThrSlip.Checked;

            entity.IsOrganizationUnit = chkIsOrganizationUnit.Checked;
            entity.IsEmployeeStatus = chkIsEmployeeStatus.Checked;
            entity.IsPosition = chkIsPosition.Checked;
            entity.IsReligion = chkIsReligion.Checked;
            entity.IsEmployee = chkIsEmployee.Checked;
            entity.IsEmploymentType = chkIsEmploymentType.Checked;
            entity.IsPositionGrade = chkIsPositionGrade.Checked;
            entity.IsMaritalStatus = chkIsMaritalStatus.Checked;
            entity.IsServiceYear = chkIsServiceYear.Checked;
            entity.IsSalaryTableNumber = chkIsSalaryTableNumber.Checked;
            entity.IsEmployeeGrade = chkIsEmployeeGrade.Checked;
            entity.IsNoOfDependent = chkIsNoOfDependent.Checked;
            entity.IsAttedanceMatrixID = chkIsAttedanceMatrixID.Checked;
            entity.IsComponent1 = chkIsComponent1.Checked;
            entity.IsComponent2 = chkIsComponent2.Checked;
            entity.IsComponent3 = chkIsComponent3.Checked;
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;
            entity.IsKWI = chkIsKWI.Checked;
            entity.IsEducationLevel = chkEducationLevel.Checked;
            entity.IsEmployeeType = chkIsEmployeeType.Checked;

            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubLedgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountId = chartOfAccountId;
            entity.SubLedgerId = subLedgerId;
            int chartOfAccountIdThr = 0;
            int subLedgerIdThr = 0;
            int.TryParse(cboChartOfAccountIdThr.SelectedValue, out chartOfAccountIdThr);
            int.TryParse(cboSubLedgerIdThr.SelectedValue, out subLedgerIdThr);
            entity.ChartOfAccountIdThr = chartOfAccountIdThr;
            entity.SubLedgerIdThr = subLedgerIdThr;
            entity.NormalBalance = cboNormalBalance.SelectedValue;
            entity.NormalBalanceThr = cboNormalBalanceThr.SelectedValue;

            int chartOfAccountIdIndirect = 0;
            int subLedgerIdIndirect = 0;
            int.TryParse(cboChartOfAccountIdIndirect.SelectedValue, out chartOfAccountIdIndirect);
            int.TryParse(cboSubLedgerIdIndirect.SelectedValue, out subLedgerIdIndirect);
            entity.ChartOfAccountIdIndirect = chartOfAccountIdIndirect;
            entity.SubLedgerIdIndirect = subLedgerIdIndirect;
            int chartOfAccountIdThrIndirect = 0;
            int subLedgerIdThrIndirect = 0;
            int.TryParse(cboChartOfAccountIdThrIndirect.SelectedValue, out chartOfAccountIdThrIndirect);
            int.TryParse(cboSubLedgerIdThrIndirect.SelectedValue, out subLedgerIdThrIndirect);
            entity.ChartOfAccountIdThrIndirect = chartOfAccountIdThrIndirect;
            entity.SubLedgerIdThrIndirect = subLedgerIdThrIndirect;
            entity.NormalBalanceIndirect = cboNormalBalanceIndirect.SelectedValue;
            entity.NormalBalanceThrIndirect = cboNormalBalanceThrIndirect.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(SalaryComponent entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //--> SalaryComponentRuleDefinition
                foreach (SalaryComponentRuleDefinition rule in SalaryComponentRuleDefinitions)
                {
                    rule.SalaryComponentID = entity.SalaryComponentID;
                    //Last Update Status
                    if (rule.es.IsAdded || rule.es.IsModified)
                    {
                        rule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        rule.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> SalaryComponentRuleMatrix
                foreach (SalaryComponentRuleMatrix matrix in SalaryComponentRuleMatrixs)
                {
                    matrix.SalaryComponentID = entity.SalaryComponentID;
                    //Last Update Status
                    if (matrix.es.IsAdded || matrix.es.IsModified)
                    {
                        matrix.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        matrix.LastUpdateDateTime = DateTime.Now;
                    }
                }

                SalaryComponentRuleDefinitions.Save();
                SalaryComponentRuleMatrixs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtSalaryComponentID.Text = entity.SalaryComponentID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            SalaryComponentQuery que = new SalaryComponentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SalaryComponentCode > txtSalaryComponentCode.Text);
                que.OrderBy(que.SalaryComponentCode.Ascending);
            }
            else
            {
                que.Where(que.SalaryComponentCode < txtSalaryComponentCode.Text);
                que.OrderBy(que.SalaryComponentCode.Descending);
            }
            SalaryComponent entity = new SalaryComponent();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged


        #endregion

        #region ComboBox Function

        protected void cboSalaryComponentRoundingID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            SalaryComponentRoundingQuery query = new SalaryComponentRoundingQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentRoundingID,
                    query.SalaryComponentRoundingName
                );
            query.Where
                (
                   query.SalaryComponentRoundingName.Like(searchTextContain)
                );

            cboSalaryComponentRoundingID.DataSource = query.LoadDataTable();
            cboSalaryComponentRoundingID.DataBind();
        }

        protected void cboSalaryComponentRoundingID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentRoundingName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentRoundingID"].ToString();
        }

        #endregion ComboBox Function

        #region Record Detail Method Function SalaryComponentRuleDefinition
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah SalaryComponentRuleDefinitions.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemSalaryComponentRuleDefinition(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSalaryComponentRuleDefinition.Columns[0].Visible = isVisible;
            grdSalaryComponentRuleDefinition.Columns[4].Visible = chkIsOrganizationUnit.Checked;
            grdSalaryComponentRuleDefinition.Columns[5].Visible = chkIsEmployeeStatus.Checked;
            grdSalaryComponentRuleDefinition.Columns[6].Visible = chkIsPosition.Checked;
            grdSalaryComponentRuleDefinition.Columns[7].Visible = chkIsReligion.Checked;
            grdSalaryComponentRuleDefinition.Columns[8].Visible = chkIsEmployee.Checked;
            grdSalaryComponentRuleDefinition.Columns[9].Visible = chkIsEmploymentType.Checked;
            grdSalaryComponentRuleDefinition.Columns[10].Visible = chkIsPositionGrade.Checked;
            grdSalaryComponentRuleDefinition.Columns[11].Visible = chkIsMaritalStatus.Checked;
            grdSalaryComponentRuleDefinition.Columns[12].Visible = chkIsServiceYear.Checked;
            grdSalaryComponentRuleDefinition.Columns[13].Visible = chkIsSalaryTableNumber.Checked;
            grdSalaryComponentRuleDefinition.Columns[14].Visible = chkIsEmployeeGrade.Checked;
            grdSalaryComponentRuleDefinition.Columns[15].Visible = chkIsNoOfDependent.Checked;
            grdSalaryComponentRuleDefinition.Columns[16].Visible = chkIsAttedanceMatrixID.Checked;
            grdSalaryComponentRuleDefinition.Columns[17].Visible = chkEducationLevel.Checked;
            grdSalaryComponentRuleDefinition.Columns[18].Visible = chkIsEmployeeType.Checked;
            grdSalaryComponentRuleDefinition.Columns[19].Visible = chkIsServiceUnitID.Checked;

            grdSalaryComponentRuleDefinition.Columns[grdSalaryComponentRuleDefinition.Columns.Count - 1].Visible = isVisible;

            grdSalaryComponentRuleDefinition.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdSalaryComponentRuleDefinition.Rebind();
        }

        private SalaryComponentRuleDefinitionCollection SalaryComponentRuleDefinitions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSalaryComponentRuleDefinition"];
                    if (obj != null)
                    {
                        return ((SalaryComponentRuleDefinitionCollection)(obj));
                    }
                }

                SalaryComponentRuleDefinitionCollection coll = new SalaryComponentRuleDefinitionCollection();

                AppStandardReferenceItemQuery ql = new AppStandardReferenceItemQuery("l");
                AppStandardReferenceItemQuery qk = new AppStandardReferenceItemQuery("k");
                AppStandardReferenceItemQuery qj = new AppStandardReferenceItemQuery("j");
                AppStandardReferenceItemQuery qi = new AppStandardReferenceItemQuery("i");
                AttedanceMatrixQuery qh = new AttedanceMatrixQuery("h");
                EmployeeGradeMasterQuery qg = new EmployeeGradeMasterQuery("g");
                PositionGradeQuery qf = new PositionGradeQuery("f");
                PersonalInfoQuery qe = new PersonalInfoQuery("e");
                PositionQuery qd = new PositionQuery("d");
                OrganizationUnitQuery qc = new OrganizationUnitQuery("c");
                SalaryComponentQuery qb = new SalaryComponentQuery("b");
                SalaryComponentRuleDefinitionQuery query = new SalaryComponentRuleDefinitionQuery("a");
                EmployeeWorkingInfoQuery ewi = new EmployeeWorkingInfoQuery("m");
                AppStandardReferenceItemQuery qx = new AppStandardReferenceItemQuery("x");
                AppStandardReferenceItemQuery qy = new AppStandardReferenceItemQuery("y");
                OrganizationUnitQuery qz = new OrganizationUnitQuery("z");

                query.Select
                    (
                       qb.SalaryComponentID,
                       query.SalaryComponentRuleDefinitionID,
                       query.ValidFrom,
                       query.ValidTo,
                       query.OrganizationUnitID,
                       qc.OrganizationUnitName.As("refTo_OrganizationUnitName"),
                       query.SREmployeeStatus,
                       qi.ItemName.As("refTo_EmployeeStatusName"),
                       query.PositionID,
                       qd.PositionName.As("refTo_PositionName"),
                       query.SRReligion,
                       qj.ItemName.As("refTo_ReligionName"),
                       query.PersonID,
                       qe.EmployeeName.As("refTo_EmployeeName"),
                       query.SREmploymentType.As("SREmploymentType"),
                       qk.ItemName.As("refTo_EmploymentTypeName"),
                       query.PositionGradeID,
                       qf.PositionGradeName.As("refTo_PositionGradeName"),
                       query.SRMaritalStatus,
                       ql.ItemName.As("refTo_MaritalStatusName"),
                       query.ServiceYear,
                       query.SalaryTableNumber,
                       query.EmployeeGradeID,
                       qg.EmployeeGradeName.As("refTo_EmployeeGradeName"),
                       query.NoOfDependent,
                       query.AttedanceMatrixID,
                       qh.AttedanceMatrixName.As("refTo_AttedanceMatrixName"),
                       query.NominalAmount,
                       query.PercentageAmount,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime,
                       query.PercentageComponentID,
                       query.SREducationLevelID,
                       qx.ItemName.As("refTo_EducationLevelName"),
                       query.SREmployeeType,
                       qy.ItemName.As("refTo_EmployeeTypeName"),
                       qz.OrganizationUnitName.As("refTo_ServiceUnitName")
                    );

                query.LeftJoin(qb).On(query.SalaryComponentID == qb.SalaryComponentID);
                query.LeftJoin(qc).On(query.OrganizationUnitID == qc.OrganizationUnitID);
                query.LeftJoin(qd).On(query.PositionID == qd.PositionID);
                query.LeftJoin(qe).On(query.PersonID == qe.PersonID);
                query.LeftJoin(ewi).On(query.PersonID == ewi.PersonID);
                query.LeftJoin(qf).On(query.PositionGradeID == qf.PositionGradeID);
                query.LeftJoin(qg).On(query.EmployeeGradeID == qg.EmployeeGradeMasterID);
                query.LeftJoin(qh).On(query.AttedanceMatrixID == qh.AttedanceMatrixID);
                query.LeftJoin(qi).On
                        (
                            query.SREmployeeStatus == qi.ItemID &
                            qi.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                        );
                query.LeftJoin(qj).On
                        (
                            query.SRReligion == qj.ItemID &
                            qj.StandardReferenceID == AppEnum.StandardReference.Religion
                        );
                query.LeftJoin(qk).On
                        (
                            query.SREmploymentType == qk.ItemID &
                            qk.StandardReferenceID == AppEnum.StandardReference.EmploymentType
                        );
                query.LeftJoin(ql).On
                        (
                            query.SRMaritalStatus == ql.ItemID &
                            ql.StandardReferenceID == AppEnum.StandardReference.TaxStatus
                        );
                query.LeftJoin(qx).On
                        (
                            query.SREducationLevelID == qx.ItemID &
                            qx.StandardReferenceID == AppEnum.StandardReference.EducationLevel
                        );
                query.LeftJoin(qy).On
                       (
                           query.SREmployeeType == qx.ItemID &
                           qx.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                       );
                query.LeftJoin(qz).On(query.ServiceUnitID == qz.OrganizationUnitID);
                query.Where(query.SalaryComponentID == txtSalaryComponentID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya

                coll.Load(query);
                Session["collSalaryComponentRuleDefinition"] = coll;
                return coll;
            }
            set { Session["collSalaryComponentRuleDefinition"] = value; }
        }

        private void PopulateSalaryComponentRuleDefinitionGrid()
        {
            //Display Data Detail
            SalaryComponentRuleDefinitions = null; //Reset Record Detail
            grdSalaryComponentRuleDefinition.DataSource = SalaryComponentRuleDefinitions; //Requery
            grdSalaryComponentRuleDefinition.MasterTableView.IsItemInserted = false;
            grdSalaryComponentRuleDefinition.MasterTableView.ClearEditItems();
            grdSalaryComponentRuleDefinition.DataBind();
        }

        protected void grdSalaryComponentRuleDefinition_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSalaryComponentRuleDefinition.DataSource = SalaryComponentRuleDefinitions;
        }

        protected void grdSalaryComponentRuleDefinition_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 salaryComponentRuleDefinitionID = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID]);
            SalaryComponentRuleDefinition entity = FindSalaryComponentRuleDefinition(salaryComponentRuleDefinitionID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSalaryComponentRuleDefinition_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int64 salaryComponentRuleDefinitionID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID]);
            SalaryComponentRuleDefinition entity = FindSalaryComponentRuleDefinition(salaryComponentRuleDefinitionID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSalaryComponentRuleDefinition_InsertCommand(object source, GridCommandEventArgs e)
        {
            SalaryComponentRuleDefinition entity = SalaryComponentRuleDefinitions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSalaryComponentRuleDefinition.Rebind();
        }
        private SalaryComponentRuleDefinition FindSalaryComponentRuleDefinition(Int64 salaryComponentRuleDefinitionID)
        {
            SalaryComponentRuleDefinitionCollection coll = SalaryComponentRuleDefinitions;
            SalaryComponentRuleDefinition retEntity = null;
            foreach (SalaryComponentRuleDefinition rec in coll)
            {
                if (rec.SalaryComponentRuleDefinitionID.Equals(salaryComponentRuleDefinitionID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(SalaryComponentRuleDefinition entity, GridCommandEventArgs e)
        {
            SalaryComponentRuleDefinitionDetail userControl = (SalaryComponentRuleDefinitionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.ValidFrom = userControl.ValidFrom;
                entity.ValidTo = userControl.ValidTo;
                if (chkIsOrganizationUnit.Checked)
                {
                    entity.OrganizationUnitID = userControl.OrganizationUnitID;
                    entity.OrganizationUnitName = userControl.OrganizationUnitName;
                }
                if (chkIsEmployeeStatus.Checked)
                {
                    entity.SREmployeeStatus = userControl.SREmployeeStatus;
                    entity.EmployeeStatusName = userControl.EmployeeStatusName;
                }
                if (chkIsPosition.Checked)
                {
                    entity.PositionID = userControl.PositionID;
                    entity.PositionName = userControl.PositionName;
                }
                if (chkIsReligion.Checked)
                {
                    entity.SRReligion = userControl.SRReligion;
                    entity.ReligionName = userControl.ReligionName;
                }
                if (chkIsEmployee.Checked)
                {
                    entity.PersonID = userControl.PersonID;
                    entity.EmployeeName = userControl.EmployeeName;
                }
                if (chkIsEmploymentType.Checked)
                {
                    entity.SREmploymentType = userControl.SREmploymentType;
                    entity.EmploymentTypeName = userControl.EmploymentTypeName;
                }
                if (chkIsPositionGrade.Checked)
                {
                    entity.PositionGradeID = userControl.PositionGradeID;
                    entity.PositionGradeName = userControl.PositionGradeName;
                }
                if (chkIsMaritalStatus.Checked)
                {
                    entity.SRMaritalStatus = userControl.SRMaritalStatus;
                    entity.MaritalStatusName = userControl.MaritalStatusName;
                }
                if (chkIsEmployeeGrade.Checked)
                {
                    entity.EmployeeGradeID = userControl.EmployeeGradeID;
                    entity.EmployeeGradeName = userControl.EmployeeGradeName;
                }
                if (chkIsAttedanceMatrixID.Checked)
                {
                    entity.AttedanceMatrixID = userControl.AttedanceMatrixID;
                    entity.AttedanceMatrixName = userControl.AttedanceMatrixName;
                }

                if (chkIsServiceYear.Checked)
                {
                    entity.ServiceYear = userControl.ServiceYear;
                }

                if (chkIsSalaryTableNumber.Checked)
                {
                    entity.SalaryTableNumber = userControl.SalaryTableNumber;
                }
                if (chkIsNoOfDependent.Checked)
                {
                    entity.NoOfDependent = userControl.NoOfDependent;
                }

                if (chkEducationLevel.Checked)
                {
                    entity.SREducationLevelID = userControl.EducationLevelID;
                    entity.EducationLevelName = userControl.EducationLevelName;
                }

                if (chkIsEmployeeType.Checked)
                {
                    entity.SREmployeeType = userControl.SREmployeeType;
                    entity.EmployeeTypeName = userControl.EmployeeTypeName;
                }

                if (chkIsServiceUnitID.Checked)
                {
                    entity.ServiceUnitID = userControl.ServiceUnitID;
                    entity.ServiceUnitName = userControl.ServiceUnitName;
                }

                entity.NominalAmount = userControl.NominalAmount;
                entity.PercentageAmount = userControl.PercentageAmount;
                entity.PercentageComponentID = userControl.PercentageComponentID;
            }
        }

        #endregion

        #region Record Detail Method Function SalaryComponentRuleMatrix
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah SalaryComponentRuleMatrixs.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemSalaryComponentRuleMatrix(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSalaryComponentRuleMatrix.Columns[0].Visible = isVisible;
            grdSalaryComponentRuleMatrix.Columns[grdSalaryComponentRuleMatrix.Columns.Count - 1].Visible = isVisible;

            grdSalaryComponentRuleMatrix.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdSalaryComponentRuleMatrix.Rebind();
        }

        private SalaryComponentRuleMatrixCollection SalaryComponentRuleMatrixs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSalaryComponentRuleMatrix"];
                    if (obj != null)
                    {
                        return ((SalaryComponentRuleMatrixCollection)(obj));
                    }
                }

                SalaryComponentRuleMatrixCollection coll = new SalaryComponentRuleMatrixCollection();
                AppStandardReferenceItemQuery qc = new AppStandardReferenceItemQuery("c");
                SalaryComponentQuery qb = new SalaryComponentQuery("b");
                SalaryComponentRuleMatrixQuery query = new SalaryComponentRuleMatrixQuery("a");

                query.Select
                    (
                       query.SalaryComponentRuleMatrixID,
                       query.SalaryRuleComponentID,
                       qb.SalaryComponentCode.As("refTo_SalaryComponentCode"),
                       qb.SalaryComponentName.As("refTo_SalaryComponentName"),
                       query.SalaryComponentID,
                       query.SROperandType,
                       qc.ItemName.As("refTo_OperandTypeName"),
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(qb).On(query.SalaryRuleComponentID == qb.SalaryComponentID);
                query.LeftJoin(qc).On
                        (
                            query.SROperandType == qc.ItemID &
                            qc.StandardReferenceID == AppEnum.StandardReference.OperandType
                        );

                query.Where(query.SalaryComponentID == txtSalaryComponentID.Text); //TODO: Betulkan parameternya
                query.OrderBy(qb.SalaryComponentCode.Ascending); //TODO: Betulkan ordernya

                coll.Load(query);
                Session["collSalaryComponentRuleMatrix"] = coll;
                return coll;
            }
            set { Session["collSalaryComponentRuleMatrix"] = value; }
        }

        private void PopulateSalaryComponentRuleMatrixGrid()
        {
            //Display Data Detail
            SalaryComponentRuleMatrixs = null; //Reset Record Detail
            grdSalaryComponentRuleMatrix.DataSource = SalaryComponentRuleMatrixs; //Requery
            grdSalaryComponentRuleMatrix.MasterTableView.IsItemInserted = false;
            grdSalaryComponentRuleMatrix.MasterTableView.ClearEditItems();
            grdSalaryComponentRuleMatrix.DataBind();
        }

        protected void grdSalaryComponentRuleMatrix_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSalaryComponentRuleMatrix.DataSource = SalaryComponentRuleMatrixs;
        }

        protected void grdSalaryComponentRuleMatrix_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 salaryComponentRuleMatrixID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID]);
            SalaryComponentRuleMatrix entity = FindSalaryComponentRuleMatrix(salaryComponentRuleMatrixID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSalaryComponentRuleMatrix_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 salaryComponentRuleMatrixID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID]);
            SalaryComponentRuleMatrix entity = FindSalaryComponentRuleMatrix(salaryComponentRuleMatrixID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSalaryComponentRuleMatrix_InsertCommand(object source, GridCommandEventArgs e)
        {
            SalaryComponentRuleMatrix entity = SalaryComponentRuleMatrixs.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSalaryComponentRuleMatrix.Rebind();
        }
        private SalaryComponentRuleMatrix FindSalaryComponentRuleMatrix(Int32 salaryComponentRuleMatrixID)
        {
            SalaryComponentRuleMatrixCollection coll = SalaryComponentRuleMatrixs;
            SalaryComponentRuleMatrix retEntity = null;
            foreach (SalaryComponentRuleMatrix rec in coll)
            {
                if (rec.SalaryComponentRuleMatrixID.Equals(salaryComponentRuleMatrixID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(SalaryComponentRuleMatrix entity, GridCommandEventArgs e)
        {
            SalaryComponentRuleMatrixDetail userControl = (SalaryComponentRuleMatrixDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.SalaryComponentRuleMatrixID = userControl.SalaryComponentRuleMatrixID;
                entity.SalaryRuleComponentID = userControl.SalaryRuleComponentID;
                entity.SalaryComponentCode = userControl.SalaryComponentName;
                entity.SalaryComponentName = userControl.SalaryComponentName;
                entity.SROperandType = userControl.SROperandType;
                entity.OperandTypeName = userControl.OperandTypeName;
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

        protected void cboChartOfAccountIdIndirect_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdIndirect.DataSource = dtb;
            cboChartOfAccountIdIndirect.DataBind();
        }

        protected void cboChartOfAccountIdThr_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdThr.DataSource = dtb;
            cboChartOfAccountIdThr.DataBind();
        }

        protected void cboChartOfAccountIdThrIndirect_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            cboChartOfAccountIdThrIndirect.DataSource = dtb;
            cboChartOfAccountIdThrIndirect.DataBind();
        }

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubLedgerId, e);
            cboCOA_SelectedIndexChanged2((RadComboBox)o, cboNormalBalance, e);
        }

        protected void cboChartOfAccountIdIndirect_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubLedgerIdIndirect, e);
            cboCOA_SelectedIndexChanged2((RadComboBox)o, cboNormalBalanceIndirect, e);
        }

        protected void cboChartOfAccountIdThr_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubLedgerIdThr, e);
            cboCOA_SelectedIndexChanged2((RadComboBox)o, cboNormalBalanceThr, e);
        }

        protected void cboChartOfAccountIdThrIndirect_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboCOA_SelectedIndexChanged((RadComboBox)o, cboSubLedgerIdThrIndirect, e);
            cboCOA_SelectedIndexChanged2((RadComboBox)o, cboNormalBalanceThrIndirect, e);
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

        private void cboCOA_SelectedIndexChanged2(RadComboBox sender, RadComboBox relatedNb, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            relatedNb.SelectedValue = string.Empty;
            relatedNb.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    relatedNb.SelectedValue = coa.NormalBalance;
                    return;
                }
            }
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
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

        #endregion

        #region ComboBox SubledgerId
        protected void cboSubLedgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountId, e);
        }

        protected void cboSubLedgerIdIndirect_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdIndirect, e);
        }

        protected void cboSubLedgerIdThr_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdThr, e);
        }

        protected void cboSubLedgerIdThrIndirect_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            cboSubledger_ItemRequested((RadComboBox)sender, cboChartOfAccountIdThrIndirect, e);
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

        protected void cboSubLedgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion
    }
}
