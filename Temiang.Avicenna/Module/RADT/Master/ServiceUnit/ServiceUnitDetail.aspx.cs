using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            WindowSearch.Height = 140;

            // Url Search & List
            UrlPageSearch = "ServiceUnitSearch.aspx";
            UrlPageList = "ServiceUnitList.aspx";

            ProgramID = AppConstant.Program.ServiceUnit;

            trPcare.Visible = pcareReference.IsPCareValidation;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ServiceUnitVisitTypes = null;
                ServiceUnitAutoBillItems = null;
                ServiceUnitParamedics = null;
                ServiceUnitItemServices = null;
                ServiceUnitItemServiceCompMappings = null;
                ServiceUnitItemServiceClasses = null;
                ServiceUnitProductAccountMappings = null;
                ServiceUnitBridgings = null;

                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
                StandardReference.InitializeIncludeSpace(cboSRAssessmentType, AppEnum.StandardReference.AssessmentType);
                StandardReference.InitializeIncludeSpace(cboSRServiceUnitGroup, AppEnum.StandardReference.ServiceUnitGroup);
                StandardReference.InitializeIncludeSpace(cboMappingItemType, AppEnum.StandardReference.ItemType, "Service");
                StandardReference.InitializeIncludeSpace(cboInpatientType, AppEnum.StandardReference.InpatientClassificationUnit);
                StandardReference.InitializeIncludeSpace(cboSRBuilding, AppEnum.StandardReference.Building);

                ComboBox.PopulateWithClassTariff(cboDefaultChargeClassID);

                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.ItemType.ToString(), BusinessObject.Reference.ItemType.Kitchen))
                    grdServiceUnitTransactionCode.Columns[5].Visible = std.IsActive ?? false;

                var isUsingCpoeModule = AppSession.Parameter.IsUsingCpoeModule;
                //tabDetail.Tabs[5].Visible = isUsingCpoeModule == "Yes";
                //tabDetail.Tabs[6].Visible = isUsingCpoeModule == "Yes";
                //tabDetail.Tabs[7].Visible = isUsingCpoeModule == "Yes";
                tabHeader.Tabs[1].Visible = isUsingCpoeModule;

                pnlAccrual.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased).ToLower() == "no";

                trMedicalFileFolderColor.Visible = !AppSession.Application.IsHisMode;

            }

            //PopUp Search
            if (!IsCallback)
            {
                PopUpSearch.Initialize(AppEnum.PopUpSearch.Department, Page, txtDepartmentID);
                PopUpSearch.Initialize(AppEnum.PopUpSearch.Location, Page, txtLocationID);

                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ServiceUnitAutoBillItem, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ServiceUnitItemService, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.VisitType, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Paramedic, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitPharmacy, BusinessObject.Reference.TransactionCode.Prescription, false);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(txtDepartmentID, txtDepartmentID);
            ajax.AddAjaxSetting(txtDepartmentID, lblDepartmentName);
            ajax.AddAjaxSetting(txtDepartmentID, grdServiceUnitItemService);
            ajax.AddAjaxSetting(txtLocationID, txtLocationID);
            ajax.AddAjaxSetting(txtLocationID, lblLocationName);
            ajax.AddAjaxSetting(cboSRRegistrationType, grdServiceUnitItemService);

            ajax.AddAjaxSetting(grdServiceUnitItemService, grdServiceUnitItemService);
            ajax.AddAjaxSetting(btnMappingProcess, grdServiceUnitItemService);
            ajax.AddAjaxSetting(btnMappingProcess, pnlInfo);
            ajax.AddAjaxSetting(btnMappingProcess, btnMappingProcess);

        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceUnit());
            chkIsActive.Checked = true;
            cboMappingItemType.SelectedValue = string.Empty;
            cboMappingItemType.Text = string.Empty;
            cboMappingItemGroup.Items.Clear();
            pnlInfo.Visible = false;
        }

        protected override void OnMenuEditClick()
        {
            PopulateServiceUnitTransactionCodeGrid();
            PopulateBodyDiagramServiceUnitGrid();
            PopulateServiceUnitImageTemplateGrid();
            PopulateServiceUnitBridgingGrid();
            PopulateServiceUnitLocationGrid();
            PopulateServiceUnitAssessmentTypeGrid();

            cboMappingItemType.SelectedValue = string.Empty;
            cboMappingItemType.Text = string.Empty;
            cboMappingItemGroup.Items.Clear();
            pnlInfo.Visible = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ServiceUnit entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.ServiceUnitID == entity.ServiceUnitID);
                regs.LoadAll();
                if (regs.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }

                var txs = new ItemTransactionCollection();
                txs.Query.Where(txs.Query.Or(txs.Query.FromServiceUnitID == entity.ServiceUnitID, txs.Query.ToServiceUnitID == entity.ServiceUnitID));
                txs.LoadAll();
                if (txs.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                ServiceUnitVisitTypeCollection collVisitType = new ServiceUnitVisitTypeCollection();
                ServiceUnitParamedicCollection collParamedic = new ServiceUnitParamedicCollection();
                ServiceUnitAutoBillItemCollection collAutoBill = new ServiceUnitAutoBillItemCollection();
                ServiceUnitItemServiceCollection collItemService = new ServiceUnitItemServiceCollection();
                ServiceUnitItemServiceClassCollection collClassComp = new ServiceUnitItemServiceClassCollection();
                ServiceUnitItemServiceCompMappingCollection collComp = new ServiceUnitItemServiceCompMappingCollection();
                BodyDiagramServiceUnitCollection collBodyDiagram = new BodyDiagramServiceUnitCollection();
                ServiceUnitImageTemplateCollection collImageTemplate = new ServiceUnitImageTemplateCollection();

                collBodyDiagram.Query.Where(collBodyDiagram.Query.ServiceUnitID == txtServiceUnitID.Text);
                collBodyDiagram.LoadAll();
                collBodyDiagram.MarkAllAsDeleted();

                collImageTemplate.Query.Where(collImageTemplate.Query.ServiceUnitID == txtServiceUnitID.Text);
                collImageTemplate.LoadAll();
                collImageTemplate.MarkAllAsDeleted();

                collVisitType.Query.Where(collVisitType.Query.ServiceUnitID == txtServiceUnitID.Text);
                collVisitType.LoadAll();
                collVisitType.MarkAllAsDeleted();

                collAutoBill.Query.Where(collAutoBill.Query.ServiceUnitID == txtServiceUnitID.Text);
                collAutoBill.LoadAll();
                collAutoBill.MarkAllAsDeleted();

                collParamedic.Query.Where(collParamedic.Query.ServiceUnitID == txtServiceUnitID.Text);
                collParamedic.LoadAll();
                collParamedic.MarkAllAsDeleted();

                collItemService.Query.Where(collItemService.Query.ServiceUnitID == txtServiceUnitID.Text);
                collItemService.LoadAll();
                collItemService.MarkAllAsDeleted();

                collClassComp.Query.Where(collClassComp.Query.ServiceUnitID == txtServiceUnitID.Text);
                collClassComp.LoadAll();
                collClassComp.MarkAllAsDeleted();

                collComp.Query.Where(collComp.Query.ServiceUnitID == txtServiceUnitID.Text);
                collComp.LoadAll();
                collComp.MarkAllAsDeleted();

                ServiceUnitBridgings.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    collVisitType.Save();
                    collAutoBill.Save();
                    collParamedic.Save();
                    collClassComp.Save();
                    collComp.Save();
                    ServiceUnitItemServices.Save();
                    collBodyDiagram.Save();
                    collImageTemplate.Save();
                    ServiceUnitBridgings.Save();

                    entity.Save();

                    //PCareReferenceItemMapping
                    pcareReference.Delete(entity.ServiceUnitID);

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            bool isMain = false;
            foreach (var loc in ServiceUnitLocations)
            {
                if (loc.IsLocationMain == true)
                    isMain = true;
            }

            if (isMain == false)
            {
                args.MessageText = "The main inventory location has not been defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ServiceUnit();
            entity.AddNew();
            SetEntityValue(entity);

            // Check SatuSehat status create new flag
            var ssbType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);
            var sb = ServiceUnitBridgings.Where(s => s.SRBridgingType == ssbType && s.BridgingID == "CREATE").FirstOrDefault();
            var isCreateSsb = false;
            if (sb != null)
            {
                isCreateSsb = true;
                sb.MarkAsDeleted();
            }

            SaveEntity(entity);

            // Post to SatuSehat
            if (isCreateSsb)
                SaveToSatuSehat();
        }

        private void SaveToSatuSehat()
        {
            // Post to SatuSehat
            var satuSehat = new Bridging.SatuSehat.Utils();
            satuSehat.PostServiceUnit(txtServiceUnitID.Text);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            bool isMain = false;
            foreach (var loc in ServiceUnitLocations)
            {
                if (loc.IsLocationMain == true)
                    isMain = true;
            }

            if (isMain == false)
            {
                args.MessageText = "The main inventory location has not been defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                SetEntityValue(entity);

                // Check SatuSehat status create new flag
                var ssbType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);
                var sb = ServiceUnitBridgings.Where(s => s.SRBridgingType == ssbType && s.BridgingID == "CREATE").FirstOrDefault();
                var isCreateSsb = false;
                if (sb != null)
                {
                    isCreateSsb = true;
                    sb.MarkAsDeleted();
                }

                SaveEntity(entity);

                // Post to SatuSehat
                if (isCreateSsb)
                    SaveToSatuSehat();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("ServiceUnitID='{0}'", txtServiceUnitID.Text.Trim());
            auditLogFilter.TableName = "ServiceUnit";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtServiceUnitID.ReadOnly = (newVal != AppEnum.DataMode.New);

            RefreshCommandItemServiceUnitVisitType(newVal);
            RefreshCommandItemServiceUnitAutoBillItem(newVal);
            RefreshCommandItemServiceUnitParamedic(newVal);
            RefreshCommandItemServiceUnitItemService(newVal);
            RefreshCommandItemServiceUnitTransactionCode(newVal);
            RefreshCommandItemBodyDiagramServiceUnit(newVal);
            RefreshCommandItemServiceUnitImageTemplate(newVal);
            RefreshCommandItemServiceUnitVitalSign(newVal);
            RefreshCommandItemServiceUnitBridging(newVal);
            RefreshCommandItemServiceUnitLocation(newVal);
            RefreshCommandItemServiceUnitAssessmentType(newVal);
            RefreshCommandItemServiceUnitSchedule(newVal);

            txtFilterItemService.ReadOnly = false;

            cboMappingItemType.Enabled = (newVal == AppEnum.DataMode.Read);
            cboMappingItemGroup.Enabled = (newVal == AppEnum.DataMode.Read);
            btnMappingProcess.Visible = (newVal == AppEnum.DataMode.Read);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ServiceUnit entity = new ServiceUnit();
            if (parameters.Length > 0)
            {
                String serviceUnitID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(serviceUnitID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ServiceUnit serviceUnit = (ServiceUnit)entity;
            cboInpatientType.SelectedValue = serviceUnit.SRInpatientClassificationUnit;
            txtServiceUnitID.Text = serviceUnit.ServiceUnitID;
            txtDepartmentID.Text = serviceUnit.DepartmentID;
            PopulateDepartmentName(false);
            txtServiceUnitName.Text = serviceUnit.ServiceUnitName;
            txtShortName.Text = serviceUnit.ShortName;
            txtServiceUnitOfficer.Text = serviceUnit.ServiceUnitOfficer;
            txtEmail.Text = serviceUnit.Email;
            txtLocationID.Text = (new ServiceUnit()).GetMainLocationId(serviceUnit.ServiceUnitID);
            PopulateLocationName(false);
            cboSRRegistrationType.SelectedValue = serviceUnit.SRRegistrationType;
            cboSRGenderType.SelectedValue = serviceUnit.SRGenderType;
            chkIsUsingJobOrder.Checked = serviceUnit.IsUsingJobOrder ?? false;
            chkIsPatientTransaction.Checked = serviceUnit.IsPatientTransaction ?? false;
            chkIsTransactionEntry.Checked = serviceUnit.IsTransactionEntry ?? false;
            chkIsDispensaryUnit.Checked = serviceUnit.IsDispensaryUnit ?? false;
            chkIsPurchasingUnit.Checked = serviceUnit.IsPurchasingUnit ?? false;
            chkIsGenerateMedicalNo.Checked = serviceUnit.IsGenerateMedicalNo ?? false;
            chkIsActive.Checked = serviceUnit.IsActive ?? false;
            chkIsDirectPayment.Checked = serviceUnit.IsDirectPayment ?? false;

            cboServiceUnitPharmacy.SelectedValue = serviceUnit.ServiceUnitPharmacyID;
            cboServiceUnitPharmacy_OnSelectedIndexChanged(cboServiceUnitPharmacy,
                new RadComboBoxSelectedIndexChangedEventArgs(cboServiceUnitPharmacy.Text, string.Empty,
                    cboServiceUnitPharmacy.SelectedValue, string.Empty));
            cboLocationPharmacy.SelectedValue = serviceUnit.LocationPharmacyID;

            cboSRAssessmentType.SelectedValue = serviceUnit.SRAssessmentType;
            cboDefaultChargeClassID.SelectedValue = serviceUnit.DefaultChargeClassID;
            cboSRServiceUnitGroup.SelectedValue = serviceUnit.SRServiceUnitGroup;
            cboSRBuilding.SelectedValue = serviceUnit.SRBuilding;
            chkIsNeedConfirmationOfAttendance.Checked = serviceUnit.IsNeedConfirmationOfAttendance ?? false;
            chkShoOnKiosk.Checked = serviceUnit.IsShowOnKiosk ?? false;
            chkIsAllowAccessPatientWithServiceUnitParamedic.Checked = serviceUnit.IsAllowAccessPatientWithServiceUnitParamedic ?? false;

            if (!string.IsNullOrEmpty(serviceUnit.ServiceUnitPorID))
            {
                var upor = new ServiceUnitQuery();
                upor.Where(upor.ServiceUnitID == serviceUnit.ServiceUnitPorID);
                cboServiceUnitPorID.DataSource = upor.LoadDataTable();
                cboServiceUnitPorID.DataBind();
                cboServiceUnitPorID.SelectedValue = serviceUnit.ServiceUnitPorID;
            }
            else
            {
                cboServiceUnitPorID.Items.Clear();
                cboServiceUnitPorID.Text = string.Empty;
            }
            cboServiceUnitPorID_OnSelectedIndexChanged(cboServiceUnitPorID,
                new RadComboBoxSelectedIndexChangedEventArgs(cboServiceUnitPorID.Text, string.Empty,
                    cboServiceUnitPorID.SelectedValue, string.Empty));
            cboLocationPorID.SelectedValue = serviceUnit.LocationPorID;
            txtMedicalFileFolderColor.SelectedColor = ColorTranslator.FromHtml(serviceUnit.MedicalFileFolderColor);

            if (txtServiceUnitID.Text != string.Empty)
            {
                // --Income--
                int coaIncome = (serviceUnit.ChartOfAccountIdIncome.HasValue ? serviceUnit.ChartOfAccountIdIncome.Value : 0);
                int slIncome = (serviceUnit.SubledgerIdIncome.HasValue ? serviceUnit.SubledgerIdIncome.Value : 0);
                if (coaIncome > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdIncome, coaIncome);
                    if (slIncome > 0)
                        PopulateCboSubLedger(cboSubledgerIdIncome, slIncome);
                    else
                        ClearCombobox(cboSubledgerIdIncome);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdIncome);
                    ClearCombobox(cboSubledgerIdIncome);
                }

                // --Acrual--
                int coaAcrual = (serviceUnit.ChartOfAccountIdAcrual.HasValue ? serviceUnit.ChartOfAccountIdAcrual.Value : 0);
                int slAcrual = (serviceUnit.SubledgerIdAcrual.HasValue ? serviceUnit.SubledgerIdAcrual.Value : 0);
                if (coaAcrual > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAcrual, coaAcrual);
                    if (slAcrual > 0)
                        PopulateCboSubLedger(cboSubledgerIdAcrual, slAcrual);
                    else
                        ClearCombobox(cboSubledgerIdAcrual);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAcrual);
                    ClearCombobox(cboSubledgerIdAcrual);
                }

                // --Discount--
                int coaDiscount = (serviceUnit.ChartOfAccountIdDiscount.HasValue ? serviceUnit.ChartOfAccountIdDiscount.Value : 0);
                int slDiscount = (serviceUnit.SubledgerIdDiscount.HasValue ? serviceUnit.SubledgerIdDiscount.Value : 0);
                if (coaDiscount > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdDiscount, coaDiscount);
                    if (slDiscount > 0)
                        PopulateCboSubLedger(cboSubledgerIdDiscount, slDiscount);
                    else
                        ClearCombobox(cboSubledgerIdDiscount);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdDiscount);
                    ClearCombobox(cboSubledgerIdDiscount);
                }

                // --Cost--
                int coaCost = (serviceUnit.ChartOfAccountIdCost.HasValue ? serviceUnit.ChartOfAccountIdCost.Value : 0);
                int slCost = (serviceUnit.SubledgerIdCost.HasValue ? serviceUnit.SubledgerIdCost.Value : 0);
                if (coaCost > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCost, coaCost);
                    if (slCost > 0)
                        PopulateCboSubLedger(cboSubledgerIdCost, slCost);
                    else
                        ClearCombobox(cboSubledgerIdCost);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCost);
                    ClearCombobox(cboSubledgerIdCost);
                }

                // --CostNonMedic--
                int coaCostNonMedic = (serviceUnit.ChartOfAccountIdCostNonMedic.HasValue ? serviceUnit.ChartOfAccountIdCostNonMedic.Value : 0);
                int slCostNonMedic = (serviceUnit.SubledgerIdCostNonMedic.HasValue ? serviceUnit.SubledgerIdCostNonMedic.Value : 0);
                if (coaCostNonMedic > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCostNonMedic, coaCostNonMedic);
                    if (slCostNonMedic > 0)
                        PopulateCboSubLedger(cboSubledgerIdCostNonMedic, slCostNonMedic);
                    else
                        ClearCombobox(cboSubledgerIdCostNonMedic);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCostNonMedic);
                    ClearCombobox(cboSubledgerIdCostNonMedic);
                }

                // --Cost Paramedic Fee--
                int coaCostParaFee = (serviceUnit.ChartOfAccountIdCostParamedicFee.HasValue ? serviceUnit.ChartOfAccountIdCostParamedicFee.Value : 0);
                int slCostParaFee = (serviceUnit.SubledgerIdCostParamedicFee.HasValue ? serviceUnit.SubledgerIdCostParamedicFee.Value : 0);
                if (coaCostParaFee > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCostParamedicFee, coaCostParaFee);
                    if (slCostParaFee > 0)
                        PopulateCboSubLedger(cboSubledgerIdCostParamedicFee, slCostParaFee);
                    else
                        ClearCombobox(cboSubledgerIdCostParamedicFee);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCostParamedicFee);
                    ClearCombobox(cboSubledgerIdCostParamedicFee);
                }

                // --Ppn In--
                int coaPpnIn = (serviceUnit.ChartOfAccountIdPpnIn.HasValue ? serviceUnit.ChartOfAccountIdPpnIn.Value : 0);
                int slPpnIn = (serviceUnit.SubledgerIdPpnIn.HasValue ? serviceUnit.SubledgerIdPpnIn.Value : 0);
                if (coaPpnIn > 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPpnIn, coaPpnIn);
                    if (slPpnIn > 0)
                        PopulateCboSubLedger(cboSubledgerIdPpnIn, slPpnIn);
                    else
                        ClearCombobox(cboSubledgerIdPpnIn);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPpnIn);
                    ClearCombobox(cboSubledgerIdPpnIn);
                }

                // hide coa ppn in
                var sutc = new ServiceUnitTransactionCode();
                if (sutc.LoadByPrimaryKey(serviceUnit.ServiceUnitID, "040"))
                {
                    trCoaPpnIn.Visible = true;
                    trSlPpnIn.Visible = true;
                }
                else
                {
                    trCoaPpnIn.Visible = false;
                    trSlPpnIn.Visible = false;
                }
            }
            else
            {
                ClearCombobox(cboChartOfAccountIdIncome);
                ClearCombobox(cboSubledgerIdIncome);
                ClearCombobox(cboChartOfAccountIdAcrual);
                ClearCombobox(cboSubledgerIdAcrual);
                ClearCombobox(cboChartOfAccountIdDiscount);
                ClearCombobox(cboSubledgerIdDiscount);
                ClearCombobox(cboChartOfAccountIdCost);
                ClearCombobox(cboSubledgerIdCost);
                ClearCombobox(cboChartOfAccountIdCostNonMedic);
                ClearCombobox(cboSubledgerIdCostNonMedic);
                ClearCombobox(cboChartOfAccountIdCostParamedicFee);
                ClearCombobox(cboSubledgerIdCostParamedicFee);
                ClearCombobox(cboChartOfAccountIdPpnIn);
                ClearCombobox(cboSubledgerIdPpnIn);
            }

            //Display Data Detail
            PopulateServiceUnitVisitTypeGrid();
            PopulateServiceUnitAutoBillItemGrid();
            PopulateServiceUnitParamedicGrid();
            PopulateServiceUnitItemServiceGrid();
            PopulateServiceUnitTransactionCodeGrid();
            PopulateBodyDiagramServiceUnitGrid();
            PopulateServiceUnitImageTemplateGrid();
            PopulateServiceUnitVitalSignGrid();
            PopulateServiceUnitBridgingGrid();
            PopulateServiceUnitLocationGrid();
            PopulateServiceUnitAssessmentTypeGrid();
            PopulateServiceUnitScheduleGrid();

            var comp = ServiceUnitItemServiceCompMappings;
            var classComp = ServiceUnitItemServiceClasses;
            var prod = ServiceUnitProductAccountMappings;

            // Pcare Map
            pcareReference.Populate(serviceUnit.ServiceUnitID);
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

        #region Private Method Standard

        private void SetEntityValue(ServiceUnit entity)
        {
            entity.ServiceUnitID = txtServiceUnitID.Text;
            entity.DepartmentID = txtDepartmentID.Text;
            entity.ServiceUnitName = txtServiceUnitName.Text;
            entity.ShortName = txtShortName.Text;
            entity.ServiceUnitOfficer = txtServiceUnitOfficer.Text;
            entity.Email = txtEmail.Text;
            //entity.LocationID = txtLocationID.Text;
            entity.SRRegistrationType = cboSRRegistrationType.SelectedValue;
            entity.SRGenderType = cboSRGenderType.SelectedValue;
            entity.IsUsingJobOrder = chkIsUsingJobOrder.Checked;
            entity.IsPatientTransaction = chkIsPatientTransaction.Checked;
            entity.IsTransactionEntry = chkIsTransactionEntry.Checked;
            entity.IsDispensaryUnit = chkIsDispensaryUnit.Checked;
            entity.IsPurchasingUnit = chkIsPurchasingUnit.Checked;
            entity.IsGenerateMedicalNo = chkIsGenerateMedicalNo.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.IsDirectPayment = chkIsDirectPayment.Checked;
            entity.IsNeedConfirmationOfAttendance = chkIsNeedConfirmationOfAttendance.Checked;
            entity.IsShowOnKiosk = chkShoOnKiosk.Checked;
            entity.IsAllowAccessPatientWithServiceUnitParamedic = chkIsAllowAccessPatientWithServiceUnitParamedic.Checked;
            entity.SRInpatientClassificationUnit = cboInpatientType.SelectedValue;

            int chartOfAccountIdIncome = 0;
            int subLegderIdIncome = 0;
            int.TryParse(cboChartOfAccountIdIncome.SelectedValue, out chartOfAccountIdIncome);
            int.TryParse(cboSubledgerIdIncome.SelectedValue, out subLegderIdIncome);
            entity.ChartOfAccountIdIncome = chartOfAccountIdIncome;
            entity.SubledgerIdIncome = subLegderIdIncome;

            int chartOfAccountIdAcrual = 0;
            int subLegderIdAcrual = 0;
            int.TryParse(cboChartOfAccountIdAcrual.SelectedValue, out chartOfAccountIdAcrual);
            int.TryParse(cboSubledgerIdAcrual.SelectedValue, out subLegderIdAcrual);
            entity.ChartOfAccountIdAcrual = chartOfAccountIdAcrual;
            entity.SubledgerIdAcrual = subLegderIdAcrual;

            int chartOfAccountIdDiscount = 0;
            int subLegderIdDiscount = 0;
            int.TryParse(cboChartOfAccountIdDiscount.SelectedValue, out chartOfAccountIdDiscount);
            int.TryParse(cboSubledgerIdDiscount.SelectedValue, out subLegderIdDiscount);
            entity.ChartOfAccountIdDiscount = chartOfAccountIdDiscount;
            entity.SubledgerIdDiscount = subLegderIdDiscount;

            int chartOfAccountIdCost = 0;
            int subLegderIdCost = 0;
            int.TryParse(cboChartOfAccountIdCost.SelectedValue, out chartOfAccountIdCost);
            int.TryParse(cboSubledgerIdCost.SelectedValue, out subLegderIdCost);
            entity.ChartOfAccountIdCost = chartOfAccountIdCost;
            entity.SubledgerIdCost = subLegderIdCost;

            int chartOfAccountIdCostNonMedic = 0;
            int subLegderIdCostNonMedic = 0;
            int.TryParse(cboChartOfAccountIdCostNonMedic.SelectedValue, out chartOfAccountIdCostNonMedic);
            int.TryParse(cboSubledgerIdCostNonMedic.SelectedValue, out subLegderIdCostNonMedic);
            entity.ChartOfAccountIdCostNonMedic = chartOfAccountIdCostNonMedic;
            entity.SubledgerIdCostNonMedic = subLegderIdCostNonMedic;

            int chartOfAccountIdCostParaFee = 0;
            int subLegderIdCostParaFee = 0;
            int.TryParse(cboChartOfAccountIdCostParamedicFee.SelectedValue, out chartOfAccountIdCostParaFee);
            int.TryParse(cboSubledgerIdCostParamedicFee.SelectedValue, out subLegderIdCostParaFee);
            entity.ChartOfAccountIdCostParamedicFee = chartOfAccountIdCostParaFee;
            entity.SubledgerIdCostParamedicFee = subLegderIdCostParaFee;

            int chartOfAccountIdPpnIn = 0;
            int subLegderIdPpnIn = 0;
            int.TryParse(cboChartOfAccountIdPpnIn.SelectedValue, out chartOfAccountIdPpnIn);
            int.TryParse(cboSubledgerIdPpnIn.SelectedValue, out subLegderIdPpnIn);
            entity.ChartOfAccountIdPpnIn = chartOfAccountIdPpnIn;
            entity.SubledgerIdPpnIn = subLegderIdPpnIn;

            entity.ServiceUnitPharmacyID = cboServiceUnitPharmacy.SelectedValue;
            entity.LocationPharmacyID = cboLocationPharmacy.SelectedValue;
            entity.SRAssessmentType = cboSRAssessmentType.SelectedValue;
            entity.DefaultChargeClassID = cboDefaultChargeClassID.SelectedValue;
            entity.SRServiceUnitGroup = cboSRServiceUnitGroup.SelectedValue;
            entity.SRBuilding = cboSRBuilding.SelectedValue;
            entity.ServiceUnitPorID = cboServiceUnitPorID.SelectedValue;
            entity.LocationPorID = cboLocationPorID.SelectedValue;
            entity.MedicalFileFolderColor = ColorTranslator.ToHtml(txtMedicalFileFolderColor.SelectedColor);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Visit Type
            foreach (ServiceUnitVisitType visitType in ServiceUnitVisitTypes)
            {
                visitType.ServiceUnitID = txtServiceUnitID.Text;

                if (visitType.es.IsModified || visitType.es.IsAdded)
                {
                    visitType.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    visitType.LastUpdateDateTime = DateTime.Now;
                }
            }

            //Auto Bill Item
            foreach (ServiceUnitAutoBillItem bill in ServiceUnitAutoBillItems)
            {
                bill.ServiceUnitID = txtServiceUnitID.Text;

                if (bill.es.IsModified || bill.es.IsAdded)
                {
                    bill.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bill.LastUpdateDateTime = DateTime.Now;
                }
            }

            //Visit Type
            foreach (ServiceUnitParamedic paramedic in ServiceUnitParamedics)
            {
                paramedic.ServiceUnitID = txtServiceUnitID.Text;

                if (paramedic.es.IsModified || paramedic.es.IsAdded)
                {
                    paramedic.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    paramedic.LastUpdateDateTime = DateTime.Now;
                }
            }

            //ServiceUnitItemServices
            foreach (ServiceUnitItemService itemService in ServiceUnitItemServices)
            {
                itemService.ServiceUnitID = txtServiceUnitID.Text;

                if (itemService.es.IsModified || itemService.es.IsAdded)
                {
                    itemService.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    itemService.LastUpdateDateTime = DateTime.Now;
                }
            }


            ////ServiceUnitItemServiceCompMapping
            //foreach (ServiceUnitItemServiceCompMapping itemServiceComp in ServiceUnitItemServiceCompMappings)
            //{
            //    itemServiceComp.ServiceUnitID = txtServiceUnitID.Text;

            //    if (itemServiceComp.es.IsModified || itemServiceComp.es.IsAdded)
            //    {
            //        itemServiceComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        itemServiceComp.LastUpdateDateTime = DateTime.Now;
            //    }
            //}

            //ServiceUnitProductAccountMapping
            foreach (ServiceUnitProductAccountMapping ProductAccountComp in ServiceUnitProductAccountMappings)
            {
                ProductAccountComp.LocationId = txtLocationID.Text;
                ProductAccountComp.ServiceUnitId = txtServiceUnitID.Text;
                //ProductAccountComp.ProductAccountId = "a";

                if (ProductAccountComp.es.IsModified || ProductAccountComp.es.IsAdded)
                {
                    ProductAccountComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ProductAccountComp.LastUpdateDateTime = DateTime.Now;
                }
            }

            //ServiceUnitItemServiceClass
            foreach (BusinessObject.ServiceUnitItemServiceClass ClassComp in ServiceUnitItemServiceClasses)
            {
                ClassComp.ServiceUnitID = txtServiceUnitID.Text;

                if (ClassComp.es.IsModified || ClassComp.es.IsAdded)
                {
                    ClassComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ClassComp.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (BusinessObject.ServiceUnitVitalSign ClassComp in ServiceUnitVitalSigns)
            {
                ClassComp.ServiceUnitID = txtServiceUnitID.Text;

                if (ClassComp.es.IsModified || ClassComp.es.IsAdded)
                {
                    ClassComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ClassComp.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (var alias in ServiceUnitBridgings)
            {
                alias.ServiceUnitID = txtServiceUnitID.Text;

                if (alias.es.IsModified || alias.es.IsAdded)
                {
                    alias.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    alias.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (var location in ServiceUnitLocations)
            {
                location.ServiceUnitID = txtServiceUnitID.Text;

                if (location.es.IsModified || location.es.IsAdded)
                {
                    location.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    location.LastUpdateDateTime = DateTime.Now;
                }
            }

            foreach (var alias in ServiceUnitSchedules)
            {
                alias.ServiceUnitID = txtServiceUnitID.Text;

                if (alias.es.IsModified || alias.es.IsAdded)
                {
                    alias.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    alias.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(ServiceUnit entity)
        {
            //TransactionCode
            var serviceUnitTransactionCodes = new ServiceUnitTransactionCodeCollection();
            serviceUnitTransactionCodes.Query.Where(serviceUnitTransactionCodes.Query.ServiceUnitID == entity.ServiceUnitID);
            serviceUnitTransactionCodes.LoadAll();

            foreach (GridDataItem dataItem in grdServiceUnitTransactionCode.MasterTableView.Items)
            {
                string tranCode = dataItem.GetDataKeyValue("SRTransactionCode").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                bool isItemProductMedic = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsItemProductMedic")).Checked;
                bool isItemProductNonMedic = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsItemProductNonMedic")).Checked;
                bool isItemKitchen = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsItemKitchen")).Checked;

                bool isExist = false;
                foreach (ServiceUnitTransactionCode row in serviceUnitTransactionCodes)
                {
                    if (row.SRTransactionCode.Equals(tranCode))
                    {
                        isExist = true;
                        row.IsItemProductMedic = isItemProductMedic;
                        row.IsItemProductNonMedic = isItemProductNonMedic;
                        row.IsItemKitchen = isItemKitchen;
                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    ServiceUnitTransactionCode row = serviceUnitTransactionCodes.AddNew();
                    row.ServiceUnitID = entity.ServiceUnitID;
                    row.SRTransactionCode = tranCode;
                    row.IsItemProductMedic = isItemProductMedic;
                    row.IsItemProductNonMedic = isItemProductNonMedic;
                    row.IsItemKitchen = isItemKitchen;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = DateTime.Now;
                }
            }

            //BodyDiagramServiceUnit
            var bodyDiagramServiceUnits = new BodyDiagramServiceUnitCollection();
            bodyDiagramServiceUnits.Query.Where(bodyDiagramServiceUnits.Query.ServiceUnitID == entity.ServiceUnitID);
            bodyDiagramServiceUnits.LoadAll();
            foreach (GridDataItem dataItem in grdBodyDiagram.MasterTableView.Items)
            {
                string bodyID = dataItem.GetDataKeyValue("BodyID").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;

                bool isExist = false;
                foreach (BodyDiagramServiceUnit row in bodyDiagramServiceUnits)
                {
                    if (row.BodyID.Equals(bodyID))
                    {
                        isExist = true;
                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    BodyDiagramServiceUnit row = bodyDiagramServiceUnits.AddNew();
                    row.ServiceUnitID = entity.ServiceUnitID;
                    row.BodyID = bodyID;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = DateTime.Now;
                }
            }

            //ServiceUnitImageTemplate
            var serviceUnitImageTemplates = new ServiceUnitImageTemplateCollection();
            serviceUnitImageTemplates.Query.Where(serviceUnitImageTemplates.Query.ServiceUnitID == entity.ServiceUnitID);
            serviceUnitImageTemplates.LoadAll();
            foreach (GridDataItem dataItem in grdImageTemplate.MasterTableView.Items)
            {
                string id = dataItem.GetDataKeyValue("ImageTemplateID").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;

                bool isExist = false;
                foreach (ServiceUnitImageTemplate row in serviceUnitImageTemplates)
                {
                    if (row.ImageTemplateID.Equals(id))
                    {
                        isExist = true;
                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    ServiceUnitImageTemplate row = serviceUnitImageTemplates.AddNew();
                    row.ServiceUnitID = entity.ServiceUnitID;
                    row.ImageTemplateID = id;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = DateTime.Now;
                }
            }

            //ServiceUnitAssessmentType
            var serviceUnitAssessmentTypes = new ServiceUnitAssessmentTypeCollection();
            serviceUnitAssessmentTypes.Query.Where(serviceUnitAssessmentTypes.Query.ServiceUnitID == entity.ServiceUnitID);
            serviceUnitAssessmentTypes.LoadAll();
            foreach (GridDataItem dataItem in grdServiceUnitAssessmentType.MasterTableView.Items)
            {
                string itemID = dataItem.GetDataKeyValue("ItemID").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;

                bool isExist = false;
                foreach (ServiceUnitAssessmentType row in serviceUnitAssessmentTypes)
                {
                    if (row.SRAssessmentType.Equals(itemID))
                    {
                        isExist = true;
                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    ServiceUnitAssessmentType row = serviceUnitAssessmentTypes.AddNew();
                    row.ServiceUnitID = entity.ServiceUnitID;
                    row.SRAssessmentType = itemID;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = DateTime.Now;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                ServiceUnitVisitTypes.Save();
                ServiceUnitAutoBillItems.Save();
                ServiceUnitParamedics.Save();
                serviceUnitTransactionCodes.Save();
                bodyDiagramServiceUnits.Save();
                serviceUnitImageTemplates.Save();
                ServiceUnitItemServiceClasses.Save();
                //ServiceUnitProductAccountMappings.Save();
                ServiceUnitItemServices.Save();
                ServiceUnitVitalSigns.Save();
                ServiceUnitBridgings.Save();
                ServiceUnitLocations.Save();
                serviceUnitAssessmentTypes.Save();
                ServiceUnitSchedules.Save();

                //subledger
                var subledgerGroupId = AppSession.Parameter.SubLedgerGroupIdServiceUnit;
                if (subledgerGroupId != "")
                {
                    var sub = new BusinessObject.SubLedgers()
                    {
                        GroupId = subledgerGroupId.ToInt(),
                        SubLedgerName = entity.ServiceUnitID,
                        Description = entity.ServiceUnitName,
                        IsDirectCost = true,
                        DateCreated = DateTime.Now,
                        LastUpdateDateTime = DateTime.Now,
                        CreatedBy = AppSession.UserLogin.UserID,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };

                    sub.Query.Where(sub.Query.SubLedgerName == entity.ServiceUnitID);
                    if (sub.Query.Load())
                    {
                        sub.Description = entity.ServiceUnitName;
                        sub.LastUpdateDateTime = DateTime.Now;
                        sub.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        sub = new BusinessObject.SubLedgers()
                        {
                            GroupId = subledgerGroupId.ToInt(),
                            SubLedgerName = entity.ServiceUnitID,
                            Description = entity.ServiceUnitName,
                            IsDirectCost = true,
                            DateCreated = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            CreatedBy = AppSession.UserLogin.UserID,
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };
                    }

                    sub.Save();
                }

                //PCareReferenceItemMapping
                pcareReference.Save(entity.ServiceUnitID);

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ServiceUnitQuery que = new ServiceUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID > txtServiceUnitID.Text);
                que.OrderBy(que.ServiceUnitID.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID < txtServiceUnitID.Text);
                que.OrderBy(que.ServiceUnitID.Descending);
            }
            ServiceUnit entity = new ServiceUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        protected void txtDepartmentID_TextChanged(object sender, EventArgs e)
        {
            PopulateDepartmentName(true);
        }

        private void PopulateDepartmentName(bool isResetIdIfNotExist)
        {
            //TODO: Fix generated Code
            if (txtDepartmentID.Text == string.Empty)
            {
                lblDepartmentName.Text = string.Empty;
                return;
            }
            Department entity = new Department();
            if (entity.LoadByPrimaryKey(txtDepartmentID.Text))
            {
                txtDepartmentID.Text = entity.DepartmentID;
                lblDepartmentName.Text = entity.DepartmentName;
            }
            else
            {
                lblDepartmentName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtDepartmentID.Text = string.Empty;
            }
        }

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            PopulateLocationName(true);
        }

        private void PopulateLocationName(bool isResetIdIfNotExist)
        {
            //TODO: Fix generated Code
            if (txtLocationID.Text == string.Empty)
            {
                lblLocationName.Text = string.Empty;
                return;
            }
            Location entity = new Location();
            if (entity.LoadByPrimaryKey(txtLocationID.Text))
            {
                txtLocationID.Text = entity.LocationID;
                lblLocationName.Text = entity.LocationName;
            }
            else
            {
                lblLocationName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtLocationID.Text = string.Empty;
            }
        }

        protected void cboChartOfAccountIdIncome_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdIncome.Items.Clear();
            cboSubledgerIdIncome.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdIncome.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdIncome.Items.Clear();
                cboChartOfAccountIdIncome.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAcrual_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdAcrual.Items.Clear();
            cboSubledgerIdAcrual.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAcrual.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdAcrual.Items.Clear();
                cboChartOfAccountIdAcrual.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdDiscount_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdDiscount.Items.Clear();
            cboSubledgerIdDiscount.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdDiscount.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdDiscount.Items.Clear();
                cboChartOfAccountIdDiscount.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCost_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedSubledgerid = cboSubledgerIdCost.SelectedValue;
            var selectedSubledgerText = cboSubledgerIdCost.Text;

            cboSubledgerIdCost.Items.Clear();
            cboSubledgerIdCost.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCost.Text = string.Empty;
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectedSubledgerid))
                    {
                        // select subledger
                        var ev = new RadComboBoxItemsRequestedEventArgs();
                        char[] c = { ' ' };
                        string[] sText = selectedSubledgerText.Split(c);
                        if (sText.Length > 0)
                        {
                            ev.Text = sText[0];
                        }
                        cboSubledgerIdCost_ItemsRequested(o, ev);
                        cboSubledgerIdCost.SelectedValue = selectedSubledgerid;
                    }
                }
            }
            else
            {
                cboChartOfAccountIdCost.Items.Clear();
                cboChartOfAccountIdCost.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCostNonMedic_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedSubledgerid = cboSubledgerIdCostNonMedic.SelectedValue;
            var selectedSubledgerText = cboSubledgerIdCostNonMedic.Text;

            cboSubledgerIdCostNonMedic.Items.Clear();
            cboSubledgerIdCostNonMedic.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCostNonMedic.Text = string.Empty;
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectedSubledgerid))
                    {
                        // select subledger
                        var ev = new RadComboBoxItemsRequestedEventArgs();
                        char[] c = { ' ' };
                        string[] sText = selectedSubledgerText.Split(c);
                        if (sText.Length > 0)
                        {
                            ev.Text = sText[0];
                        }
                        cboSubledgerIdCostNonMedic_ItemsRequested(o, ev);
                        cboSubledgerIdCostNonMedic.SelectedValue = selectedSubledgerid;
                    }
                }
            }
            else
            {
                cboChartOfAccountIdCostNonMedic.Items.Clear();
                cboChartOfAccountIdCostNonMedic.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCostParamedicFee_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedSubledgerid = cboSubledgerIdCostParamedicFee.SelectedValue;
            var selectedSubledgerText = cboSubledgerIdCostParamedicFee.Text;

            cboSubledgerIdCostParamedicFee.Items.Clear();
            cboSubledgerIdCostParamedicFee.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCostParamedicFee.Text = string.Empty;
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectedSubledgerid))
                    {
                        // select subledger
                        var ev = new RadComboBoxItemsRequestedEventArgs();
                        char[] c = { ' ' };
                        string[] sText = selectedSubledgerText.Split(c);
                        if (sText.Length > 0)
                        {
                            ev.Text = sText[0];
                        }
                        cboSubledgerIdCostParamedicFee_ItemsRequested(o, ev);
                        cboSubledgerIdCostParamedicFee.SelectedValue = selectedSubledgerid;
                    }
                }
            }
            else
            {
                cboChartOfAccountIdCostParamedicFee.Items.Clear();
                cboChartOfAccountIdCostParamedicFee.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdPpnIn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var selectedSubledgerid = cboSubledgerIdPpnIn.SelectedValue;
            var selectedSubledgerText = cboSubledgerIdPpnIn.Text;

            cboSubledgerIdPpnIn.Items.Clear();
            cboSubledgerIdPpnIn.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdPpnIn.Text = string.Empty;
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectedSubledgerid))
                    {
                        // select subledger
                        var ev = new RadComboBoxItemsRequestedEventArgs();
                        char[] c = { ' ' };
                        string[] sText = selectedSubledgerText.Split(c);
                        if (sText.Length > 0)
                        {
                            ev.Text = sText[0];
                        }
                        cboSubledgerIdPpnIn_ItemsRequested(o, ev);
                        cboSubledgerIdPpnIn.SelectedValue = selectedSubledgerid;
                    }
                }
            }
            else
            {
                cboChartOfAccountIdPpnIn.Items.Clear();
                cboChartOfAccountIdPpnIn.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitVisitType

        private ServiceUnitVisitTypeCollection ServiceUnitVisitTypes
        {
            get
            {
                string sessName = string.Format("collServiceUnitVisitType_{0}", this.PageID);
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null)
                    {
                        return ((ServiceUnitVisitTypeCollection)(obj));
                    }
                }

                ServiceUnitVisitTypeCollection coll = new ServiceUnitVisitTypeCollection();
                ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
                VisitTypeQuery vq = new VisitTypeQuery("b");
                query.InnerJoin(vq).On(query.VisitTypeID == vq.VisitTypeID);
                query.Select(query.SelectAllExcept(), vq.VisitTypeName.As("refToVisitType_VisitTypeName"));
                string serviceUnitID = txtServiceUnitID.Text;
                query.Where(query.ServiceUnitID == serviceUnitID);
                coll.Load(query);

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = string.Format("collServiceUnitVisitType_{0}", this.PageID);
                Session[sessName] = value;
            }
        }

        private void RefreshCommandItemServiceUnitVisitType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitVisitType.Columns[0].Visible = isVisible;
            grdServiceUnitVisitType.Columns[grdServiceUnitVisitType.Columns.Count - 1].Visible = isVisible;

            grdServiceUnitVisitType.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnitVisitType.Rebind();
        }

        private void PopulateServiceUnitVisitTypeGrid()
        {
            //Display Data Detail
            ServiceUnitVisitTypes = null; //Reset Record Detail
            grdServiceUnitVisitType.DataSource = ServiceUnitVisitTypes; //Requery
            grdServiceUnitVisitType.MasterTableView.IsItemInserted = false;
            grdServiceUnitVisitType.MasterTableView.ClearEditItems();
            grdServiceUnitVisitType.DataBind();
        }

        protected void grdServiceUnitVisitType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitVisitType.DataSource = ServiceUnitVisitTypes;
        }

        protected void grdServiceUnitVisitType_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String visitTypeID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID]);
            ServiceUnitVisitType entity = FindServiceUnitVisitType(visitTypeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdServiceUnitVisitType_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String visitTypeID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID]);
            ServiceUnitVisitType entity = FindServiceUnitVisitType(visitTypeID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnitVisitType_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitVisitType entity = ServiceUnitVisitTypes.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitVisitType.Rebind();
        }

        private ServiceUnitVisitType FindServiceUnitVisitType(String visitTypeID)
        {
            ServiceUnitVisitTypeCollection coll = ServiceUnitVisitTypes;
            return coll.FirstOrDefault(rec => rec.VisitTypeID.Equals(visitTypeID));
        }

        private void SetEntityValue(ServiceUnitVisitType entity, GridCommandEventArgs e)
        {
            ServiceUnitVisitTypeDetail userControl = (ServiceUnitVisitTypeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.VisitTypeID = userControl.VisitTypeID;
                entity.VisitTypeName = userControl.VisitTypeName;
                entity.VisitDuration = userControl.VisitDuration;
            }
        }

        #endregion

        #region Record Detail Method Function of ServiceUnitAutoBillItem

        private ServiceUnitAutoBillItemCollection ServiceUnitAutoBillItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitAutoBillItem"];
                    if (obj != null)
                        return ((ServiceUnitAutoBillItemCollection)(obj));
                }

                ServiceUnitAutoBillItemCollection coll = new ServiceUnitAutoBillItemCollection();

                ServiceUnitAutoBillItemQuery query = new ServiceUnitAutoBillItemQuery("a");

                ItemQuery iq = new ItemQuery("b");
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

                AppStandardReferenceItemQuery asriq = new AppStandardReferenceItemQuery("c");
                query.LeftJoin(asriq).On(query.SRItemUnit == asriq.ItemID && asriq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);

                query.Where
                    (
                    query.ServiceUnitID == txtServiceUnitID.Text,
                    asriq.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );
                query.OrderBy(query.ServiceUnitID.Ascending);

                query.Select
                    (
                    query,
                    iq.ItemName.As("refToItem_ItemName"),
                    asriq.ItemName.As("refToAppStandardReferenceItem_SRItemUnit")
                    );
                coll.Load(query);

                Session["collServiceUnitAutoBillItem"] = coll;
                return coll;
            }
            set { Session["collServiceUnitAutoBillItem"] = value; }
        }

        private void RefreshCommandItemServiceUnitAutoBillItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitAutoBillItem.Columns[0].Visible = isVisible;
            grdServiceUnitAutoBillItem.Columns[grdServiceUnitAutoBillItem.Columns.Count - 1].Visible = isVisible;
            grdServiceUnitAutoBillItem.Columns[10].Visible = !AppSession.Application.IsHisMode;

            grdServiceUnitAutoBillItem.MasterTableView.CommandItemDisplay = isVisible
                                                                                ? GridCommandItemDisplay.Top
                                                                                : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnitAutoBillItem.Rebind();
        }

        private void PopulateServiceUnitAutoBillItemGrid()
        {
            //Display Data Detail
            ServiceUnitAutoBillItems = null; //Reset Record Detail
            grdServiceUnitAutoBillItem.DataSource = ServiceUnitAutoBillItems;
            grdServiceUnitAutoBillItem.MasterTableView.IsItemInserted = false;
            grdServiceUnitAutoBillItem.MasterTableView.ClearEditItems();
            grdServiceUnitAutoBillItem.DataBind();
        }

        protected void grdServiceUnitAutoBillItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitAutoBillItem.DataSource = ServiceUnitAutoBillItems;
        }

        protected void grdServiceUnitAutoBillItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String itemID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID]);
            ServiceUnitAutoBillItem entity = FindItemGridOfServiceUnitAutoBillItem(itemID);
            if (entity != null)
                SetEntityValueOfServiceUnitAutoBillItem(entity, e);
        }

        protected void grdServiceUnitAutoBillItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID
                        ]);
            ServiceUnitAutoBillItem entity = FindItemGridOfServiceUnitAutoBillItem(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnitAutoBillItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitAutoBillItem entity = ServiceUnitAutoBillItems.AddNew();
            SetEntityValueOfServiceUnitAutoBillItem(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitAutoBillItem.Rebind();
        }

        private void SetEntityValueOfServiceUnitAutoBillItem(ServiceUnitAutoBillItem entity, GridCommandEventArgs e)
        {
            ServiceUnitAutoBillItemDetail userControl =
                (ServiceUnitAutoBillItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.ItemName = userControl.ItemName;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnit;
                entity.IsAutoPayment = userControl.IsAutoPayment;
                entity.IsActive = userControl.IsActive;
                entity.IsGenerateOnRegistration = userControl.IsGenerateOnRegistration;
                entity.IsGenerateOnNewRegistration = userControl.IsGenerateOnNewRegistration;
                entity.IsGenerateOnReferral = userControl.IsGenerateOnReferral;
                entity.IsGenerateOnFirstRegistration = userControl.IsGenerateOnFirstRegistration;
                entity.IsGenerateOnSchedule = userControl.IsGenerateOnSchedule;
                entity.GenerateOnDayStart = userControl.GenerateOnDayStart;
                entity.GenerateOnDayEnd = userControl.GenerateOnDayEnd;
                entity.GenerateOnClassIDs = userControl.GenerateOnClassIDs;
            }
        }

        private ServiceUnitAutoBillItem FindItemGridOfServiceUnitAutoBillItem(string itemID)
        {
            ServiceUnitAutoBillItemCollection coll = ServiceUnitAutoBillItems;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        #endregion

        #region Record Detail Method Function ServiceUnitParamedic

        private class NewServiceUnitParamedicColl : ServiceUnitParamedic
        {
            public string RoomName { get; set; }
        }

        private ServiceUnitParamedicCollection ServiceUnitParamedics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitParamedic"];
                    if (obj != null)
                    {
                        return ((ServiceUnitParamedicCollection)(obj));
                    }
                }

                ServiceUnitParamedicCollection coll = new ServiceUnitParamedicCollection();
                ServiceUnitParamedicQuery query = new ServiceUnitParamedicQuery("a");
                ParamedicQuery pq = new ParamedicQuery("b");
                ServiceRoomQuery srq = new ServiceRoomQuery("c");
                query.InnerJoin(pq).On(query.ParamedicID == pq.ParamedicID);
                query.LeftJoin(srq).On(query.DefaultRoomID == srq.RoomID);
                query.Select(query.SelectAllExcept(), pq.ParamedicName.As("refToParamedic_ParamedicName"), srq.RoomName.As("refToServiceRoom_RoomName"));

                string serviceUnitID = txtServiceUnitID.Text;
                query.Where(query.ServiceUnitID == serviceUnitID);
                coll.Load(query);

                Session["collServiceUnitParamedic"] = coll;
                return coll;
            }
            set { Session["collServiceUnitParamedic"] = value; }
        }

        private void RefreshCommandItemServiceUnitParamedic(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitParamedic.Columns[0].Visible = isVisible;
            grdServiceUnitParamedic.Columns[grdServiceUnitParamedic.Columns.Count - 1].Visible = isVisible;
            grdServiceUnitParamedic.Columns[grdServiceUnitParamedic.Columns.Count - 2].Visible = !isVisible; //ScheduleTemplate

            grdServiceUnitParamedic.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnitParamedic.Rebind();
        }

        private void PopulateServiceUnitParamedicGrid()
        {
            //Display Data Detail
            ServiceUnitParamedics = null; //Reset Record Detail
            grdServiceUnitParamedic.DataSource = ServiceUnitParamedics;
            grdServiceUnitParamedic.MasterTableView.IsItemInserted = false;
            grdServiceUnitParamedic.MasterTableView.ClearEditItems();
            grdServiceUnitParamedic.DataBind();
        }

        protected void grdServiceUnitParamedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitParamedic.DataSource = ServiceUnitParamedics;
        }

        protected void grdServiceUnitParamedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String visitTypeID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitParamedicMetadata.ColumnNames.ParamedicID]);
            ServiceUnitParamedic entity = FindServiceUnitParamedic(visitTypeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdServiceUnitParamedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String visitTypeID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ServiceUnitParamedicMetadata.ColumnNames.ParamedicID]);
            ServiceUnitParamedic entity = FindServiceUnitParamedic(visitTypeID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnitParamedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitParamedic entity = ServiceUnitParamedics.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitParamedic.Rebind();
        }

        private ServiceUnitParamedic FindServiceUnitParamedic(String visitTypeID)
        {
            ServiceUnitParamedicCollection coll = ServiceUnitParamedics;
            return coll.FirstOrDefault(rec => rec.ParamedicID.Equals(visitTypeID));
        }

        private void SetEntityValue(ServiceUnitParamedic entity, GridCommandEventArgs e)
        {
            ServiceUnitParamedicDetail userControl = (ServiceUnitParamedicDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.DefaultRoomID = userControl.DefaultRoomID;
                entity.RoomName = userControl.DefaultRoomName;
                entity.IsUsingQue = userControl.IsUsingQue;
                entity.IsAcceptBPJS = userControl.IsAcceptBPJS;
                entity.IsAcceptNonBPJS = userControl.IsAcceptNonBPJS;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitItemService

        private ServiceUnitItemServiceCollection ServiceUnitItemServices
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitItemService"];
                    if (obj != null)
                    {
                        return ((ServiceUnitItemServiceCollection)(obj));
                    }
                }


                ServiceUnitItemServiceCollection coll = new ServiceUnitItemServiceCollection();
                ServiceUnitItemServiceQuery query = new ServiceUnitItemServiceQuery("a");
                ItemQuery pq = new ItemQuery("b");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("c");
                SubLedgersQuery sl = new SubLedgersQuery("d");
                var idi = new ItemIdiQuery("e");

                query.InnerJoin(pq).On(query.ItemID == pq.ItemID);
                query.LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(sl).On(query.SubledgerId == sl.SubLedgerId);
                query.LeftJoin(idi).On(query.IdiCode == idi.IdiCode);
                query.Select
                    (
                        query,
                        pq.ItemName.As("refToItem_ItemName"),
                        coa.ChartOfAccountCode.As("refToChartOfAccount_ChartOfAccountCode"),
                        sl.SubLedgerName.As("refToSubLedger_SubLedgerName"),
                        idi.IdiName.As("refToItemIdi_IdiName")
                    );

                string serviceUnitID = txtServiceUnitID.Text;
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collServiceUnitItemService"] = coll;
                return coll;
            }
            set { Session["collServiceUnitItemService"] = value; }
        }

        private void RefreshCommandItemServiceUnitItemService(AppEnum.DataMode newVal)
        {

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitItemService.Columns[0].Visible = isVisible;
            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 3].Visible = isVisible;


            if (!isVisible)
            {
                grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = false;
                grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = false;

            }
            else
            {
                if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.CoaUsingClass) == "1")
                    {

                        if (txtDepartmentID.Text == AppSession.Parameter.InPatientDepartmentID)
                        {
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = true;
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = false;

                        }
                        else if (txtDepartmentID.Text == AppSession.Parameter.OutPatientDepartmentID || txtDepartmentID.Text == AppSession.Parameter.EmergencyDepartmentID)
                        {
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = false;
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = true;

                        }
                        else
                        {
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = true;
                            grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = true;
                            //lbPickList.Visible = true;
                            //lbPickListOp.Visible = true;
                        }
                    }
                    // kl tidak pakai class maka table yang digunakan hanya serviceunitcompmapping
                    else
                    {
                        grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = false;
                        grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = true;
                        //lbPickList.Visible = false;
                        //lbPickListOp.Visible = true;
                    }
                }
                else
                {
                    grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 2].Visible = false;
                    grdServiceUnitItemService.Columns[grdServiceUnitItemService.Columns.Count - 1].Visible = true;
                    //lbPickList.Visible = false;
                    //lbPickListOp.Visible = true;
                }
            }

            grdServiceUnitItemService.Columns[3].Visible = !AppSession.Application.IsHisMode; //IDI code
            grdServiceUnitItemService.Columns[4].Visible = !AppSession.Application.IsHisMode; // IDI Name

            grdServiceUnitItemService.MasterTableView.CommandItemDisplay = isVisible
                                                                               ? GridCommandItemDisplay.Top
                                                                               : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdServiceUnitItemService.Rebind();

            // harus setelah rebind
            //---------- start remark by deby : error index out of range
            //if (this.DataModeCurrent == AppEnum.DataMode.Edit)
            //{
            //    var lbPickList = grdServiceUnitItemService.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickList");
            //    var lbPickListOp = grdServiceUnitItemService.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickListOp");

            //    if (!isVisible)
            //    {
            //        lbPickList.Visible = false;
            //        lbPickListOp.Visible = false;

            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
            //        {
            //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.CoaUsingClass) == "1")
            //            {

            //                if (txtDepartmentID.Text == AppSession.Parameter.InPatientDepartmentID)
            //                {
            //                    lbPickList.Visible = true;
            //                    lbPickListOp.Visible = false;

            //                }
            //                else if (txtDepartmentID.Text == AppSession.Parameter.OutPatientDepartmentID || txtDepartmentID.Text == AppSession.Parameter.EmergencyDepartmentID)
            //                {
            //                    lbPickList.Visible = false;
            //                    lbPickListOp.Visible = true;

            //                }
            //                else
            //                {

            //                    lbPickList.Visible = true;
            //                    lbPickListOp.Visible = true;
            //                }
            //            }
            //            // kl tidak pakai class maka table yang digunakan hanya serviceunitcompmapping
            //            else
            //            {

            //                lbPickList.Visible = false;
            //                lbPickListOp.Visible = true;
            //            }
            //        }
            //        else
            //        {

            //            lbPickList.Visible = false;
            //            lbPickListOp.Visible = true;
            //        }
            //    }
            //}
            //---------- end remark by deby : error index out of range

        }

        private void PopulateServiceUnitItemServiceGrid()
        {
            //Display Data Detail
            ServiceUnitItemServices = null; //Reset Record Detail
            grdServiceUnitItemService.DataSource = ServiceUnitItemServices; //Requery
            grdServiceUnitItemService.MasterTableView.IsItemInserted = false;
            grdServiceUnitItemService.MasterTableView.ClearEditItems();
            grdServiceUnitItemService.DataBind();
        }

        protected void grdServiceUnitItemService_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemService.Text.Trim() != string.Empty)
            {
                var ds = from d in ServiceUnitItemServices
                         where d.ItemName.ToLower().Contains(txtFilterItemService.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemService.Text.ToLower())
                         select d;
                grdServiceUnitItemService.DataSource = ds;
            }
            else
            {
                grdServiceUnitItemService.DataSource = ServiceUnitItemServices;
            }
        }

        protected void grdServiceUnitItemService_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String visitTypeID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitItemServiceMetadata.ColumnNames.ItemID]);
            ServiceUnitItemService entity = FindServiceUnitItemService(visitTypeID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdServiceUnitItemService_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String visitTypeID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitItemServiceMetadata.ColumnNames.ItemID]);
            ServiceUnitItemService entity = FindServiceUnitItemService(visitTypeID);

            if (entity != null)
            {
                entity.MarkAsDeleted();

                var ip = new ItemPackageCollection();
                ip.Query.Where(ip.Query.ServiceUnitID == txtServiceUnitID.Text, ip.Query.DetailItemID == visitTypeID);
                ip.LoadAll();
                //kl item ini tidak dipakai di itempackage baru boleh dihapus
                if (ip.Count == 0)
                {

                    //ServiceUnitItemServiceCompMappingCollection compColl = ServiceUnitItemServiceCompMappings;
                    foreach (ServiceUnitItemServiceCompMapping comp in ServiceUnitItemServiceCompMappings.Where(comp => comp.ItemID.Equals(visitTypeID)))
                    {
                        comp.MarkAsDeleted();
                    }

                    //ServiceUnitItemServiceClassCollection ClassCompColl = ServiceUnitItemServiceClasses;
                    foreach (BusinessObject.ServiceUnitItemServiceClass ClassComp in ServiceUnitItemServiceClasses.Where(ClassComp => ClassComp.ItemID.Equals(visitTypeID)))
                    {
                        ClassComp.MarkAsDeleted();
                    }
                }
            }

        }

        protected void grdServiceUnitItemService_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitItemService entity = ServiceUnitItemServices.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode

            SetEntityValueForItemComp(e);
            SetEntityValueForClassComp(e);

            e.Canceled = true;
            grdServiceUnitItemService.Rebind();
        }

        private ServiceUnitItemService FindServiceUnitItemService(String visitTypeID)
        {
            ServiceUnitItemServiceCollection coll = ServiceUnitItemServices;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(visitTypeID));
        }

        //private ServiceUnitItemServiceCompMappingCollection FindServiceUnitItemServiceComp(String visitTypeID)
        //{
        //    ServiceUnitItemServiceCompMappingCollection coll = ServiceUnitItemServiceCompMappings ;
        //    ServiceUnitItemServiceCompMapping retEntity = null;
        //    foreach (ServiceUnitItemServiceCompMapping rec in coll)
        //    {
        //        if (rec.ItemID.Equals(visitTypeID))
        //        {
        //            retEntity = rec;
        //            break;
        //        }
        //    }
        //    return retEntity;
        //}

        private void SetEntityValue(ServiceUnitItemService entity, GridCommandEventArgs e)
        {
            ServiceUnitItemServiceDetail userControl = (ServiceUnitItemServiceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IdiCode = userControl.IdiCode;
                entity.IdiName = userControl.IdiName;
                int chartOfAccountId = 0;
                int subLedgerId = 0;
                int.TryParse(userControl.ChartOfAccountId, out chartOfAccountId);
                int.TryParse(userControl.SubLedgerId, out subLedgerId);
                entity.ChartOfAccountId = chartOfAccountId;
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(chartOfAccountId);
                entity.ChartOfAccountCode = coa.ChartOfAccountCode;
                entity.SubledgerId = subLedgerId;
                entity.IsAllowEditByUserVerificated = userControl.IsAllowEditByUserVerificated;
                entity.IsVisible = userControl.IsVisible;
            }
        }

        private void SetEntityValueForItemComp(GridCommandEventArgs e)
        {
            ServiceUnitItemServiceDetail userControl = (ServiceUnitItemServiceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                var suiscm = new ServiceUnitItemServiceCompMappingCollection();
                suiscm.Query.Where(suiscm.Query.ServiceUnitID == txtServiceUnitID.Text,
                    suiscm.Query.ItemID == userControl.ItemID);
                suiscm.LoadAll();
                if (suiscm.Count > 0)
                    return;

                TariffComponentCollection coll = new TariffComponentCollection();
                coll.LoadAll();

                foreach (TariffComponent rec in coll)
                {
                    var entity = ServiceUnitItemServiceCompMappings.AddNew();
                    entity.ServiceUnitID = txtServiceUnitID.Text;
                    entity.ItemID = userControl.ItemID;
                    entity.TariffComponentID = rec.TariffComponentID;
                    entity.TariffComponentName = rec.TariffComponentName;
                }
            }
        }

        private void SetEntityValueForClassComp(GridCommandEventArgs e)
        {
            ServiceUnitItemServiceDetail userControl = (ServiceUnitItemServiceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                var suisc = new ServiceUnitItemServiceClassCollection();
                suisc.Query.Where(suisc.Query.ServiceUnitID == txtServiceUnitID.Text,
                    suisc.Query.ItemID == userControl.ItemID);
                suisc.LoadAll();
                if (suisc.Count > 0)
                    return;

                TariffComponentCollection coll = new TariffComponentCollection();
                coll.LoadAll();

                ClassCollection coll2 = new ClassCollection();
                coll2.LoadAll();

                foreach (TariffComponent rec in coll)
                {
                    foreach (Class rec2 in coll2)
                    {
                        var entity = ServiceUnitItemServiceClasses.AddNew();
                        entity.ServiceUnitID = txtServiceUnitID.Text;
                        entity.ItemID = userControl.ItemID;
                        entity.ClassID = rec2.ClassID;
                        entity.ClassName = rec2.ClassName;
                        entity.TariffComponentID = rec.TariffComponentID;
                        entity.TariffComponentName = rec.TariffComponentName;
                    }
                }
            }
        }


        #endregion

        #region Record Detail Method Function ServiceUnitTransactionCode

        private void PopulateServiceUnitTransactionCodeGrid()
        {
            //Display Data Detail
            grdServiceUnitTransactionCode.DataSource = GetServiceUnitTransactionCodes();
            grdServiceUnitTransactionCode.DataBind();
        }

        protected void grdServiceUnitTransactionCode_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitTransactionCode.DataSource = GetServiceUnitTransactionCodes();
        }

        private DataTable GetServiceUnitTransactionCodes()
        {
            ServiceUnitTransactionCodeQuery query = new ServiceUnitTransactionCodeQuery("a");
            AppStandardReferenceItemQuery qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.SRTransactionCode == qrRef.ItemID);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.SRTransactionCode == qrRef.ItemID & query.ServiceUnitID == txtServiceUnitID.Text);
            }
            query.Where(qrRef.StandardReferenceID == "TransactionCode", qrRef.IsActive == true);
            query.OrderBy(qrRef.ItemName.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.SRTransactionCode,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.ItemID.As("SRTransactionCode"),
                    qrRef.ItemName.As("TransactionName"),
                    "<ISNULL(a.IsItemProductMedic, 0) as IsItemProductMedic>",
                    "<ISNULL(a.IsItemProductNonMedic, 0) as IsItemProductNonMedic>",
                    "<ISNULL(a.IsItemKitchen, 0) as IsItemKitchen>",
                    "<CONVERT(Bit,CASE WHEN ISNULL(b.ReferenceID,'')='Inventory' THEN 1 ELSE 0 END) as IsVisible>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemServiceUnitTransactionCode(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitTransactionCode.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdServiceUnitTransactionCode.Rebind();
        }
        #endregion

        #region Record Detail Method Function ServiceUnitItemServiceCompMapping

        private ServiceUnitItemServiceCompMappingCollection ServiceUnitItemServiceCompMappings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitItemServiceCompMappingCollection"];
                    if (obj != null)
                        return ((ServiceUnitItemServiceCompMappingCollection)(obj));
                }
                var coll = new ServiceUnitItemServiceCompMappingCollection();

                var query = new ServiceUnitItemServiceCompMappingQuery("a");
                var c = new TariffComponentQuery("b");

                var rev = new ChartOfAccountsQuery("c");
                var srev = new SubLedgersQuery("d");

                var disc = new ChartOfAccountsQuery("e");
                var sdisc = new SubLedgersQuery("f");

                var cost = new ChartOfAccountsQuery("g");
                var scost = new SubLedgersQuery("h");

                var rtype = new AppStandardReferenceItemQuery("i");

                query.Select
                    (
                        query,

                        c.TariffComponentName.As("refToTariffComponent_TariffComponentName"),

                        rev.ChartOfAccountName.As("refToChartOfAccounts_COARevenueName"),
                        srev.SubLedgerName.As("refToSubledgers_SubledgerRevenueName"),

                        disc.ChartOfAccountName.As("refToChartOfAccounts_COADiscountName"),
                        sdisc.SubLedgerName.As("refToSubledgers_SubledgerDiscountName"),

                        cost.ChartOfAccountName.As("refToChartOfAccounts_COACostName"),
                        scost.SubLedgerName.As("refToSubledgers_SubledgerCostName"),

                        rtype.ItemName.As("refToAppStandardReferenceItem_RegistrationType")
                    );
                query.InnerJoin(c).On(query.TariffComponentID == c.TariffComponentID);

                query.LeftJoin(rev).On(query.ChartOfAccountIdIncome == rev.ChartOfAccountId);
                query.LeftJoin(srev).On(query.SubledgerIdIncome == srev.SubLedgerId);

                query.LeftJoin(disc).On(query.ChartOfAccountIdDiscount == disc.ChartOfAccountId);
                query.LeftJoin(sdisc).On(query.SubledgerIdDiscount == sdisc.SubLedgerId);

                query.LeftJoin(cost).On(query.ChartOfAccountIdCost == cost.ChartOfAccountId);
                query.LeftJoin(scost).On(query.SubledgerIdCost == scost.SubLedgerId);

                query.LeftJoin(rtype).On(query.SRRegistrationType == rtype.ItemID &&
                                          rtype.StandardReferenceID == AppEnum.StandardReference.RegistrationType);

                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);

                coll.Load(query);

                Session["ServiceUnitItemServiceCompMappingCollection"] = coll;
                return coll;
            }
            set
            {
                Session["ServiceUnitItemServiceCompMappingCollection"] = value;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitProductAccountMapping

        private ServiceUnitProductAccountMappingCollection ServiceUnitProductAccountMappings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitProductAccountMappingCollection"];
                    if (obj != null)
                        return ((ServiceUnitProductAccountMappingCollection)(obj));
                }
                var coll = new ServiceUnitProductAccountMappingCollection();

                var query = new ServiceUnitProductAccountMappingQuery("a");
                query.Where(query.ServiceUnitId == txtServiceUnitID.Text, query.SRRegistrationType == (string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue) ? "OPR" : (cboSRRegistrationType.SelectedValue == "MCU" ? "OPR" : cboSRRegistrationType.SelectedValue)));

                coll.Load(query);

                Session["ServiceUnitProductAccountMappingCollection"] = coll;
                return coll;
            }
            set
            {
                Session["ServiceUnitProductAccountMappingCollection"] = value;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitItemServiceClass

        private ServiceUnitItemServiceClassCollection ServiceUnitItemServiceClasses
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitItemServiceClassCollection"];
                    if (obj != null)
                        return ((ServiceUnitItemServiceClassCollection)(obj));
                }
                var coll = new ServiceUnitItemServiceClassCollection();

                var query = new ServiceUnitItemServiceClassQuery("a");
                var c = new TariffComponentQuery("b");

                var rev = new ChartOfAccountsQuery("c");
                var srev = new SubLedgersQuery("d");

                var disc = new ChartOfAccountsQuery("e");
                var sdisc = new SubLedgersQuery("f");

                var cost = new ChartOfAccountsQuery("g");
                var scost = new SubLedgersQuery("h");

                var cls = new ClassQuery("i");

                query.Select
                    (
                        query,

                        c.TariffComponentName.As("refToTariffComponent_TariffComponentName"),

                        rev.ChartOfAccountName.As("refToChartOfAccounts_COARevenueName"),
                        srev.SubLedgerName.As("refToSubledgers_SubledgerRevenueName"),

                        disc.ChartOfAccountName.As("refToChartOfAccounts_COADiscountName"),
                        sdisc.SubLedgerName.As("refToSubledgers_SubledgerDiscountName"),

                        cost.ChartOfAccountName.As("refToChartOfAccounts_COACostName"),
                        scost.SubLedgerName.As("refToSubledgers_SubledgerCostName"),

                        cls.ClassName.As("refToClass_ClassName")
                    );
                query.InnerJoin(c).On(query.TariffComponentID == c.TariffComponentID);

                query.LeftJoin(rev).On(query.ChartOfAccountIdIncome == rev.ChartOfAccountId);
                query.LeftJoin(srev).On(query.SubledgerIdIncome == srev.SubLedgerId);

                query.LeftJoin(disc).On(query.ChartOfAccountIdDiscount == disc.ChartOfAccountId);
                query.LeftJoin(sdisc).On(query.SubledgerIdDiscount == sdisc.SubLedgerId);

                query.LeftJoin(cost).On(query.ChartOfAccountIdCost == cost.ChartOfAccountId);
                query.LeftJoin(scost).On(query.SubledgerIdCost == scost.SubLedgerId);

                query.InnerJoin(cls).On(query.ClassID == cls.ClassID);

                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);

                coll.Load(query);

                Session["ServiceUnitItemServiceClassCollection"] = coll;
                return coll;
            }
            set
            {
                ViewState["ServiceUnitItemServiceClassCollection"] = value;
            }
        }

        #endregion

        #region Combobox

        #region ComboBox ChartOfAccountIdIncome
        protected void cboChartOfAccountIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdIncome.DataSource = dtb;
            cboChartOfAccountIdIncome.DataBind();
        }

        protected void cboChartOfAccountIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdIncome
        protected void cboSubledgerIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdIncome.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncome.SelectedValue));
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
            cboSubledgerIdIncome.DataSource = dtb;
            cboSubledgerIdIncome.DataBind();
        }

        protected void cboSubledgerIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdAcrual
        protected void cboChartOfAccountIdAcrual_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAcrual.DataSource = dtb;
            cboChartOfAccountIdAcrual.DataBind();
        }

        protected void cboChartOfAccountIdAcrual_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdAcrual
        protected void cboSubledgerIdAcrual_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAcrual.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAcrual.SelectedValue));
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
            cboSubledgerIdAcrual.DataSource = dtb;
            cboSubledgerIdAcrual.DataBind();
        }

        protected void cboSubledgerIdAcrual_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdDiscount
        protected void cboChartOfAccountIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdDiscount.DataSource = dtb;
            cboChartOfAccountIdDiscount.DataBind();
        }

        protected void cboChartOfAccountIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdDiscount
        protected void cboSubledgerIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdDiscount.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDiscount.SelectedValue));
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
            cboSubledgerIdDiscount.DataSource = dtb;
            cboSubledgerIdDiscount.DataBind();
        }

        protected void cboSubledgerIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCost
        protected void cboChartOfAccountIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCost.DataSource = dtb;
            cboChartOfAccountIdCost.DataBind();
        }

        protected void cboChartOfAccountIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCost
        protected void cboSubledgerIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCost.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCost.SelectedValue));
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
            cboSubledgerIdCost.DataSource = dtb;
            cboSubledgerIdCost.DataBind();
        }

        protected void cboSubledgerIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCostNonMedic
        protected void cboChartOfAccountIdCostNonMedic_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCostNonMedic.DataSource = dtb;
            cboChartOfAccountIdCostNonMedic.DataBind();
        }

        protected void cboChartOfAccountIdCostNonMedic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCostNonMedic
        protected void cboSubledgerIdCostNonMedic_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCost.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCost.SelectedValue));
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
            cboSubledgerIdCostNonMedic.DataSource = dtb;
            cboSubledgerIdCostNonMedic.DataBind();
        }

        protected void cboSubledgerIdCostNonMedic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCostParamedicFee
        protected void cboChartOfAccountIdCostParamedicFee_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCostParamedicFee.DataSource = dtb;
            cboChartOfAccountIdCostParamedicFee.DataBind();
        }

        protected void cboChartOfAccountIdCostParamedicFee_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCostParamedicFee
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

        #region ComboBox ChartOfAccountIdPpnIn
        protected void cboChartOfAccountIdPpnIn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdPpnIn.DataSource = dtb;
            cboChartOfAccountIdPpnIn.DataBind();
        }

        protected void cboChartOfAccountIdPpnIn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdPpnIn
        protected void cboSubledgerIdPpnIn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdPpnIn.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdPpnIn.SelectedValue));
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
            cboSubledgerIdPpnIn.DataSource = dtb;
            cboSubledgerIdPpnIn.DataBind();
        }

        protected void cboSubledgerIdPpnIn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ServiceUnitPharmacy
        protected void cboServiceUnitPharmacy_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            var lColl = new LocationCollection();
            var l = new LocationQuery("a");
            var sl = new ServiceUnitLocationQuery("b");
            l.InnerJoin(sl).On(l.LocationID.Equal(sl.LocationID))
                .Where(sl.ServiceUnitID.Equal(cbo.SelectedValue));
            lColl.Load(l);
            cboLocationPharmacy.Items.Clear();
            cboLocationPharmacy.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var loc in lColl)
            {
                cboLocationPharmacy.Items.Add(new RadComboBoxItem(loc.LocationName, loc.LocationID));
            }
        }
        #endregion

        #region ComboBox ServiceUnitPorID
        protected void cboServiceUnitPorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboServiceUnitPorID.Items.Clear();

            DataTable tbl = LoadServiceUnit(e.Text);
            cboServiceUnitPorID.DataSource = tbl;
            cboServiceUnitPorID.DataBind();
        }

        private DataTable LoadServiceUnit(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ServiceUnitQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.Where(
                query.IsActive == true,
                query.ServiceUnitName.Like(searchTextContain)
                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboServiceUnitPorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitPorID_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            var lColl = new LocationCollection();
            var l = new LocationQuery("a");
            var sl = new ServiceUnitLocationQuery("b");
            l.InnerJoin(sl).On(l.LocationID.Equal(sl.LocationID))
                .Where(sl.ServiceUnitID.Equal(cbo.SelectedValue));
            lColl.Load(l);
            cboLocationPorID.Items.Clear();
            cboLocationPorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var loc in lColl)
            {
                cboLocationPorID.Items.Add(new RadComboBoxItem(loc.LocationName, loc.LocationID));
            }
        }
        #endregion
        #endregion

        protected void grdServiceUnitItemService_DataBound(object sender, EventArgs e)
        {
            //if (this.DataModeCurrent == AppEnum.DataMode.Edit)
            //{
            //    var lbPickList = grdServiceUnitItemService.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickList");
            //    var lbPickListOp = grdServiceUnitItemService.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickListOp");
            //    var lbPickListPA = grdServiceUnitItemService.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lbPickListPA");

            //    if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
            //    {
            //        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.CoaUsingClass) == "1")
            //        {

            //            if (txtDepartmentID.Text == AppSession.Parameter.InPatientDepartmentID)
            //            {
            //                lbPickList.Visible = true;
            //                lbPickListOp.Visible = false;

            //            }
            //            else if (txtDepartmentID.Text == AppSession.Parameter.OutPatientDepartmentID || txtDepartmentID.Text == AppSession.Parameter.EmergencyDepartmentID)
            //            {
            //                lbPickList.Visible = false;
            //                lbPickListOp.Visible = true;

            //            }
            //            else
            //            {

            //                lbPickList.Visible = true;
            //                lbPickListOp.Visible = true;
            //            }
            //        }
            //        // kl tidak pakai class maka table yang digunakan hanya serviceunitcompmapping
            //        else
            //        {

            //            lbPickList.Visible = false;
            //            lbPickListOp.Visible = true;
            //        }
            //    }
            //    else
            //    {

            //        lbPickList.Visible = false;
            //        lbPickListOp.Visible = true;
            //    }


            //    //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsUnitBasedProductAccount) == "Yes")
            //    //    lbPickListPA.Visible = true;
            //    //else
            //    //    lbPickListPA.Visible = false;
            //}
        }

        #region Record Detail Method Function BodyDiagram
        protected void grdBodyDiagram_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdBodyDiagram.DataSource = GetBodyDiagramServiceUnits();
        }


        private DataTable GetBodyDiagramServiceUnits()
        {
            BodyDiagramServiceUnitQuery query = new BodyDiagramServiceUnitQuery("a");
            BodyDiagramQuery qrRef = new BodyDiagramQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.BodyID == qrRef.BodyID);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.BodyID == qrRef.BodyID & query.ServiceUnitID == txtServiceUnitID.Text);
            }
            query.OrderBy(qrRef.BodyName.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.BodyID,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.BodyID,
                    qrRef.BodyName,
                    qrRef.Description,
                    qrRef.BodyImage
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
        private void PopulateBodyDiagramServiceUnitGrid()
        {
            //Display Data Detail
            grdBodyDiagram.DataSource = GetBodyDiagramServiceUnits();
            grdBodyDiagram.DataBind();
        }
        private void RefreshCommandItemBodyDiagramServiceUnit(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdBodyDiagram.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdBodyDiagram.Rebind();
        }
        #endregion

        #region Record Detail Method Function ImageTemplate
        protected void grdImageTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdImageTemplate.DataSource = GetImageTemplateServiceUnits();
        }


        private DataTable GetImageTemplateServiceUnits()
        {
            ServiceUnitImageTemplateQuery query = new ServiceUnitImageTemplateQuery("a");
            ImageTemplateQuery itempl = new ImageTemplateQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(itempl).On(query.ImageTemplateID == itempl.ImageTemplateID);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
            }
            else
            {
                query.RightJoin(itempl).On(query.ImageTemplateID == itempl.ImageTemplateID & query.ServiceUnitID == txtServiceUnitID.Text);
            }

            var sr = new AppStandardReferenceItemQuery("sri");
            query.LeftJoin(sr)
                .On(itempl.SRImageTemplateType == sr.ItemID && sr.StandardReferenceID == AppEnum.StandardReference.ImageTemplateType);

            query.OrderBy(itempl.ImageTemplateName.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.ImageTemplateID,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    itempl.ImageTemplateID,
                    itempl.ImageTemplateName,
                    itempl.Description,
                    itempl.Image,
                    sr.ItemName.As("ImageTemplateType")
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
        private void PopulateServiceUnitImageTemplateGrid()
        {
            //Display Data Detail
            grdImageTemplate.DataSource = GetImageTemplateServiceUnits();
            grdImageTemplate.DataBind();
        }
        private void RefreshCommandItemServiceUnitImageTemplate(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdImageTemplate.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdImageTemplate.Rebind();
        }
        #endregion

        #region Record Detail Method Function of ServiceUnitAutoBillItem

        private ServiceUnitVitalSignCollection ServiceUnitVitalSigns
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitAutoVitalSign"];
                    if (obj != null)
                        return ((ServiceUnitVitalSignCollection)(obj));
                }

                ServiceUnitVitalSignCollection coll = new ServiceUnitVitalSignCollection();

                ServiceUnitVitalSignQuery query = new ServiceUnitVitalSignQuery("a");

                VitalSignQuery iq = new VitalSignQuery("b");
                query.InnerJoin(iq).On(query.VitalSignID == iq.VitalSignID);

                query.Where
                    (
                    query.ServiceUnitID == txtServiceUnitID.Text
                    );
                query.OrderBy(query.ServiceUnitID.Ascending, query.VitalSignID.Ascending);

                query.Select
                    (
                    query,
                    iq.VitalSignName.As("refToVitalSign_VitalSignName")
                    );
                coll.Load(query);

                Session["collServiceUnitAutoVitalSign"] = coll;
                return coll;
            }
            set { Session["collServiceUnitAutoVitalSign"] = value; }
        }

        private void RefreshCommandItemServiceUnitVitalSign(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdVitalSign.Columns[0].Visible = isVisible;
            grdVitalSign.Columns[grdVitalSign.Columns.Count - 1].Visible = isVisible;
            grdVitalSign.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdVitalSign.Rebind();
        }

        private void PopulateServiceUnitVitalSignGrid()
        {
            //Display Data Detail
            ServiceUnitVitalSigns = null; //Reset Record Detail
            grdVitalSign.DataSource = ServiceUnitVitalSigns;
            grdVitalSign.MasterTableView.IsItemInserted = false;
            grdVitalSign.MasterTableView.ClearEditItems();
            grdVitalSign.DataBind();
        }

        protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVitalSign.DataSource = ServiceUnitVitalSigns;
        }

        protected void grdVitalSign_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID]);
            ServiceUnitVitalSign entity = FindItemGridOfServiceUnitVitalSign(itemID);
            if (entity != null) SetEntityValueOfServiceUnitVitalSign(entity, e);
        }

        protected void grdVitalSign_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID]);
            ServiceUnitVitalSign entity = FindItemGridOfServiceUnitVitalSign(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdVitalSign_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitVitalSign entity = ServiceUnitVitalSigns.AddNew();
            SetEntityValueOfServiceUnitVitalSign(entity, e);
            //Stay in insert mode
            e.Canceled = true;
            grdVitalSign.Rebind();
        }

        private void SetEntityValueOfServiceUnitVitalSign(ServiceUnitVitalSign entity, GridCommandEventArgs e)
        {
            ServiceUnitVitalSignDetail userControl = (ServiceUnitVitalSignDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.VitalSignID = userControl.VitalSignID;
                entity.VitalSignName = userControl.VitalSignName;
            }
        }

        private ServiceUnitVitalSign FindItemGridOfServiceUnitVitalSign(string itemID)
        {
            ServiceUnitVitalSignCollection coll = ServiceUnitVitalSigns;
            return coll.FirstOrDefault(rec => rec.VitalSignID.Equals(itemID));
        }

        #endregion

        #region Record Detail Method Function ServiceUnitBridging

        private ServiceUnitBridgingCollection ServiceUnitBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitBridging"];
                    if (obj != null) return ((ServiceUnitBridgingCollection)(obj));
                }

                ServiceUnitBridgingCollection coll = new ServiceUnitBridgingCollection();

                ServiceUnitBridgingQuery query = new ServiceUnitBridgingQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                coll.Load(query);

                Session["collServiceUnitBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collServiceUnitBridging"] = value;
            }
        }

        private void RefreshCommandItemServiceUnitBridging(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAliasName.Rebind();
        }

        private void PopulateServiceUnitBridgingGrid()
        {
            //Display Data Detail
            ServiceUnitBridgings = null; //Reset Record Detail
            grdAliasName.DataSource = ServiceUnitBridgings; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = ServiceUnitBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindServiceUnitBridging(type, id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindServiceUnitBridging(type, id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ServiceUnitBridgings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private ServiceUnitBridging FindServiceUnitBridging(String type, string id)
        {
            var coll = ServiceUnitBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type) && rec.BridgingID.Equals(id));
        }

        private void SetEntityValue(ServiceUnitBridging entity, GridCommandEventArgs e)
        {
            ServiceUnitAliasDetail userControl = (ServiceUnitAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingName = string.IsNullOrEmpty(userControl.BridgingName) ? txtServiceUnitName.Text : userControl.BridgingName;
                entity.IsActive = userControl.IsActive;
            }
        }

        #endregion

        #region Record Detail Method Function ServiceUnitLocation

        private ServiceUnitLocationCollection ServiceUnitLocations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitLocation"];
                    if (obj != null) return ((ServiceUnitLocationCollection)(obj));
                }

                var coll = new ServiceUnitLocationCollection();
                var query = new ServiceUnitLocationQuery("a");
                var locq = new LocationQuery("b");
                query.InnerJoin(locq).On(query.LocationID == locq.LocationID);

                query.Select(query, locq.LocationName.As("refToLocation_LocationName"));
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                query.OrderBy(query.IsLocationMain.Descending, query.LocationID.Ascending);
                coll.Load(query);

                Session["collServiceUnitLocation"] = coll;
                return coll;
            }
            set
            {
                Session["collServiceUnitLocation"] = value;
            }
        }

        private void RefreshCommandItemServiceUnitLocation(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdLocation.Columns[0].Visible = isVisible;
            grdLocation.Columns[grdLocation.Columns.Count - 2].Visible = isVisible;
            grdLocation.Columns[grdLocation.Columns.Count - 1].Visible = !isVisible;

            grdLocation.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdLocation.Rebind();
        }

        private void PopulateServiceUnitLocationGrid()
        {
            //Display Data Detail
            ServiceUnitLocations = null; //Reset Record Detail
            grdLocation.DataSource = ServiceUnitLocations; //Requery
            grdLocation.MasterTableView.IsItemInserted = false;
            grdLocation.MasterTableView.ClearEditItems();
            grdLocation.DataBind();
        }

        protected void grdLocation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLocation.DataSource = ServiceUnitLocations;
        }

        protected void grdLocation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitLocationMetadata.ColumnNames.LocationID]);
            var entity = FindServiceUnitLocation(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdLocation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitLocationMetadata.ColumnNames.LocationID]);
            var entity = FindServiceUnitLocation(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdLocation_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ServiceUnitLocations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLocation.Rebind();
        }

        private ServiceUnitLocation FindServiceUnitLocation(String id)
        {
            var coll = ServiceUnitLocations;
            return coll.FirstOrDefault(rec => rec.LocationID.Equals(id));
        }

        private void SetEntityValue(ServiceUnitLocation entity, GridCommandEventArgs e)
        {
            var userControl = (ServiceUnitLocationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.LocationID = userControl.LocationID;
                entity.LocationName = userControl.LocationName;
                entity.IsLocationMain = userControl.IsLocationMain;
            }
        }

        #endregion

        #region Record Detail Method Function AssessmentType
        protected void grdServiceUnitAssessmentType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitAssessmentType.DataSource = GetServiceUnitAssessmentTypes();
        }


        private DataTable GetServiceUnitAssessmentTypes()
        {
            ServiceUnitAssessmentTypeQuery query = new ServiceUnitAssessmentTypeQuery("a");
            AppStandardReferenceItemQuery qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.SRAssessmentType == qrRef.ItemID);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text && qrRef.StandardReferenceID == "AssessmentType");
            }
            else
            {
                query.RightJoin(qrRef).On(query.SRAssessmentType == qrRef.ItemID & query.ServiceUnitID == txtServiceUnitID.Text);
                query.Where(qrRef.StandardReferenceID == "AssessmentType");

            }
            query.OrderBy(qrRef.ItemName.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.SRAssessmentType,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.ItemID,
                    qrRef.ItemName
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
        private void PopulateServiceUnitAssessmentTypeGrid()
        {
            //Display Data Detail
            grdServiceUnitAssessmentType.DataSource = GetServiceUnitAssessmentTypes();
            grdServiceUnitAssessmentType.DataBind();
        }
        private void RefreshCommandItemServiceUnitAssessmentType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitAssessmentType.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdServiceUnitAssessmentType.Rebind();
        }
        #endregion

        #region Record Detail Method Function ServiceUnitSchedule

        private ServiceUnitScheduleCollection ServiceUnitSchedules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitSchedule"];
                    if (obj != null) return ((ServiceUnitScheduleCollection)(obj));
                }

                ServiceUnitScheduleCollection coll = new ServiceUnitScheduleCollection();

                ServiceUnitScheduleQuery query = new ServiceUnitScheduleQuery("a");

                query.Select(query, "<'' AS refToServiceUnitSchedule_DayOfWeekName>");
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                coll.Load(query);

                var culture = new CultureInfo(AppSession.UserLogin.SRLanguage);
                var days = culture.DateTimeFormat.DayNames;

                foreach (var entity in coll)
                {
                    entity.DayOfWeekName = days[(entity.DayOfWeek ?? 0) - 1];
                }

                Session["collServiceUnitSchedule"] = coll;

                return coll;
            }
            set
            {
                Session["collServiceUnitSchedule"] = value;
            }
        }

        private void RefreshCommandItemServiceUnitSchedule(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSchedule.Columns[0].Visible = isVisible;
            grdSchedule.Columns[grdSchedule.Columns.Count - 1].Visible = isVisible;

            grdSchedule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdSchedule.Rebind();
        }

        private void PopulateServiceUnitScheduleGrid()
        {
            //Display Data Detail
            ServiceUnitBridgings = null; //Reset Record Detail
            grdAliasName.DataSource = ServiceUnitBridgings; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdSchedule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSchedule.DataSource = ServiceUnitSchedules;
        }

        protected void grdSchedule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek]);

            var entity = FindServiceUnitSchedule(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdSchedule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek]);

            var entity = FindServiceUnitSchedule(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdSchedule_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ServiceUnitSchedules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSchedule.Rebind();
        }

        private ServiceUnitSchedule FindServiceUnitSchedule(int id)
        {
            var coll = ServiceUnitSchedules;
            return coll.FirstOrDefault(rec => rec.DayOfWeek.Equals(id));
        }

        private void SetEntityValue(ServiceUnitSchedule entity, GridCommandEventArgs e)
        {
            ServiceUnitScheduleDetail userControl = (ServiceUnitScheduleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.DayOfWeek = userControl.DayOfWeek;
                entity.DayOfWeekName = userControl.DayOfWeekName;
                entity.StartTime = userControl.StartTime;
                entity.EndTime = userControl.EndTime;
            }
        }

        #endregion

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            switch (mpgDetail.SelectedIndex)
            {
                case 2:
                    {
                        grdServiceUnitItemService.CurrentPageIndex = 0;
                        grdServiceUnitItemService.Rebind();
                        break;
                    }
            }
        }

        protected void cboMappingItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboMappingItemGroup.Items.Clear();
            cboMappingItemGroup.Text = string.Empty;
        }
        protected void cboMappingItemGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboMappingItemType.SelectedValue);
        }
        protected void cboMappingItemGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
        }

        protected void btnMappingProcess_Click(object sender, EventArgs e)
        {
            pnlInfo.Visible = false;
            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                if (string.IsNullOrEmpty(cboMappingItemGroup.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Group required.";
                    return;
                }

                var coll = new ItemCollection();
                coll.Query.Where(coll.Query.ItemGroupID == cboMappingItemGroup.SelectedValue, coll.Query.IsActive == true);
                coll.LoadAll();

                foreach (var c in coll)
                {
                    var suis = new ServiceUnitItemService();
                    if (!suis.LoadByPrimaryKey(txtServiceUnitID.Text, c.ItemID))
                    {
                        suis.AddNew();
                        suis.ServiceUnitID = txtServiceUnitID.Text;
                        suis.ItemID = c.ItemID;
                        var item = new Item();
                        item.LoadByPrimaryKey(c.ItemID);
                        suis.ItemName = item.ItemName;
                        suis.ChartOfAccountId = 0;
                        suis.ChartOfAccountCode = "";
                        suis.SubledgerId = 0;
                        suis.SubLedgerName = "";
                        suis.LastUpdateDateTime = DateTime.Now;
                        suis.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        suis.IsAllowEditByUserVerificated = true;
                        suis.IsVisible = true;
                        suis.Save();
                    }
                }

                ServiceUnitItemServices = null;
                grdServiceUnitItemService.Rebind();

                pnlInfo.Visible = true;
                lblInfo.Text = "Mapping process has been completed.";
            }
            else
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit has no exist.";
            }
        }
    }
}