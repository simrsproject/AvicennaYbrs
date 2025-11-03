using System;
using System.Configuration;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemServiceDetail : BasePageDetail
    {
        private void SetEntityValue(Item entity, ItemService itemService)
        {
            //Item
            entity.ItemID = txtItemID.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.SRItemSubGroup = cboSRItemSubGroup.SelectedValue;
            entity.Barcode = txtBarcode.Text;
            entity.SRItemType = BusinessObject.Reference.ItemType.Service;
            entity.SRBillingGroup = cboBillingGroup.SelectedValue;
            entity.SRBpjsItemGroup = cboSRBpjsItemGroup.SelectedValue;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.IsDelegationToNurse = chkIsDelegationToNurse.Checked;
            entity.Notes = txtNotes.Text;
            entity.IsHasTestResults = chkIsHasTestResults.Checked;
            entity.IsNeedToBeSterilized = false;
            entity.SREklaimTariffGroup = cboSREklaimGroup.SelectedValue;
            entity.IntervalOrderWarning = Convert.ToInt16(txtIntervalOrderWarning.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Created
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            //ItemService
            itemService.ItemID = entity.ItemID;
            itemService.ReportRLID = cboReportRLID.SelectedValue;
            itemService.RlMasterReportItemID = string.IsNullOrEmpty(cboRlMasterReportItemID.SelectedValue) ? 0 : Convert.ToInt32(cboRlMasterReportItemID.SelectedValue);
            itemService.SRItemUnit = cboSRItemUnit.SelectedValue;
            itemService.IsPrimaryService = chkIsPrimaryService.Checked;
            itemService.IsAdminCalculation = chkIsAdminCalculation.Checked;
            itemService.IsAllowVariable = chkIsAllowVariable.Checked;
            itemService.IsAllowCito = chkIsAllowCito.Checked;
            itemService.IsAllowDiscount = chkIsAllowDiscount.Checked;
            itemService.IsPrintWithDoctorName = chkIsPrintWithDoctorName.Checked;
            itemService.IsAssetUtilization = chkIsAssetUtilization.Checked;
            itemService.PremiAmount = Convert.ToDecimal(txtPremiAmount.Value);
            itemService.Premi2Amount = Convert.ToDecimal(txtPremi2Amount.Value);
            itemService.ProductionServicesPercentage = Convert.ToDecimal(txtProductionServicesPercentage1.Value);
            itemService.ProductionServicesPercentage2 = Convert.ToDecimal(txtProductionServicesPercentage2.Value);
            itemService.TogethernessPercentage = Convert.ToDecimal(txtSavingPercentage.Value);
            itemService.ItemRelatedID = cboItemRelatedID.SelectedValue;
            itemService.QtyDivider = Convert.ToDecimal(txtQtyDivider.Value);
            itemService.IsCitoFromStandardReference = chkIsCitoFromStandardReference.Checked;

            //Last Update Status
            if (itemService.es.IsAdded || itemService.es.IsModified)
            {
                itemService.LastUpdateByUserID = AppSession.UserLogin.UserID;
                itemService.LastUpdateDateTime = DateTime.Now;
            }

            //Detail
            ItemConsumptionCollection coll = ItemConsumptions;
            foreach (ItemConsumption itemConsumption in coll)
            {
                itemConsumption.ItemID = txtItemID.Text;

                //Last Update Status
                if (itemConsumption.es.IsAdded || itemConsumption.es.IsModified)
                {
                    itemConsumption.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    itemConsumption.LastUpdateDateTime = DateTime.Now;
                }
            }

            ServiceUnitItemServiceCollection collUnit = ServiceUnitItemServices;
            foreach (ServiceUnitItemService unit in collUnit)
            {
                unit.ItemID = txtItemID.Text;

                //Last Update Status
                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }

            ItemBridgingCollection collBridging = ItemBridgings;
            foreach (ItemBridging unit in collBridging)
            {
                unit.ItemID = txtItemID.Text;

                //Last Update Status
                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }

            ItemServiceProcedureCollection collProc = ItemServiceProcedures;
            foreach (ItemServiceProcedure proc in collProc)
            {
                proc.ItemID = txtItemID.Text;

                //Last Update Status
                if (proc.es.IsAdded || proc.es.IsModified)
                {
                    proc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    proc.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ItemQuery que = new ItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Service);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Service);
                que.OrderBy(que.ItemID.Descending);
            }

            Item entity = new Item();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnMenuEditClick()
        {
            chkIsCitoFromStandardReference.Enabled = chkIsAllowCito.Checked;
            PopulateServiceUnitItemServiceCheckListGrid();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Item entity = new Item();
            if (parameters.Length > 0)
            {
                String itemID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(itemID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var item = (Item)entity;
            txtItemID.Text = item.ItemID;

            cboItemGroupID.SelectedValue = item.ItemGroupID;
            if (!string.IsNullOrEmpty(item.SRItemSubGroup))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Select(query.ItemID, query.ItemName);
                query.Where(
                        query.StandardReferenceID == AppEnum.StandardReference.ItemSubGroup.ToString(),
                        query.ItemID == item.SRItemSubGroup
                    );
                cboSRItemSubGroup.DataSource = query.LoadDataTable();
                cboSRItemSubGroup.DataBind();
                cboSRItemSubGroup.SelectedValue = item.SRItemSubGroup;
            }
            else
            {
                cboSRItemSubGroup.Items.Clear();
                cboSRItemSubGroup.SelectedValue = string.Empty;
                cboSRItemSubGroup.Text = string.Empty;
            }
            txtBarcode.Text = item.Barcode;
            cboBillingGroup.SelectedValue = item.SRBillingGroup;
            cboSRBpjsItemGroup.SelectedValue = item.SRBpjsItemGroup;
            txtItemName.Text = item.ItemName;
            chkIsActive.Checked = item.IsActive ?? false;
            txtNotes.Text = item.Notes;
            chkIsHasTestResults.Checked = item.IsHasTestResults ?? false;
            chkIsDelegationToNurse.Checked = item.IsDelegationToNurse ?? false;

            var itemService = new ItemService();
            if (item.ItemID != null)
                itemService.LoadByPrimaryKey(item.ItemID);
            cboReportRLID.SelectedValue = itemService.ReportRLID;

            cboReportRLID_SelectedIndexChanged(cboReportRLID, new RadComboBoxSelectedIndexChangedEventArgs(
                                                                      cboReportRLID.Text, string.Empty,
                                                                      cboReportRLID.SelectedValue, string.Empty));

            if (itemService.RlMasterReportItemID != null & itemService.RlMasterReportItemID != 0)
            {
                cboRlMasterReportItemID.SelectedValue = itemService.RlMasterReportItemID.ToString();

                var rl = new RlMasterReportItem();
                if (rl.LoadByPrimaryKey(Convert.ToInt32(itemService.RlMasterReportItemID)))
                    cboRlMasterReportItemID.Text = rl.RlMasterReportItemCode + " - " + rl.RlMasterReportItemName;
            }

            cboSRItemUnit.SelectedValue = itemService.SRItemUnit;
            chkIsPrimaryService.Checked = itemService.IsPrimaryService ?? false;
            chkIsAdminCalculation.Checked = itemService.IsAdminCalculation ?? false;
            chkIsAllowVariable.Checked = itemService.IsAllowVariable ?? false;
            chkIsAllowCito.Checked = itemService.IsAllowCito ?? false;
            chkIsAllowDiscount.Checked = itemService.IsAllowDiscount ?? false;
            chkIsPrintWithDoctorName.Checked = itemService.IsPrintWithDoctorName ?? false;
            chkIsAssetUtilization.Checked = itemService.IsAssetUtilization ?? false;
            txtPremiAmount.Value = Convert.ToDouble(itemService.PremiAmount ?? 0);
            txtPremi2Amount.Value = Convert.ToDouble(itemService.Premi2Amount ?? 0);
            txtProductionServicesPercentage1.Value = Convert.ToDouble(itemService.ProductionServicesPercentage ?? 0);
            txtProductionServicesPercentage2.Value = Convert.ToDouble(itemService.ProductionServicesPercentage2 ?? 0);
            txtSavingPercentage.Value = Convert.ToDouble(itemService.TogethernessPercentage ?? 0);
            chkIsCitoFromStandardReference.Checked = itemService.IsCitoFromStandardReference ?? false;

            if (!string.IsNullOrEmpty(itemService.ItemRelatedID))
            {
                var ipQ = new ItemQuery();
                ipQ.Select(ipQ.ItemID, ipQ.ItemName);
                ipQ.Where(ipQ.SRItemType.In(ItemType.Medical, ItemType.NonMedical),
                          ipQ.ItemID == itemService.ItemRelatedID);

                DataTable dtb = ipQ.LoadDataTable();
                cboItemRelatedID.DataSource = dtb;
                cboItemRelatedID.DataBind();
                cboItemRelatedID.SelectedValue = itemService.ItemRelatedID;
                cboItemRelatedID.Text = dtb.Rows[0]["ItemName"] + " (" + dtb.Rows[0]["ItemID"] + ")";
            }
            else
            {
                cboItemRelatedID.Items.Clear();
                cboItemRelatedID.Text = string.Empty;
            }
            txtQtyDivider.Value = Convert.ToDouble(itemService.QtyDivider ?? 0);

            cboSREklaimGroup.SelectedValue = item.SREklaimTariffGroup;
            txtIntervalOrderWarning.Value = Convert.ToDouble(item.IntervalOrderWarning);

            PopulateItemConsumptionGrid();
            grdPriceHistory.Rebind();

            if (!AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService)
                PopulateServiceUnitItemServiceGrid();
            else
                PopulateServiceUnitItemServiceCheckListGrid();
            PopulateItemBirdgingGrid();
            PopulateItemProcedureGrid();

            // Pcare Map
            pcareReference.Populate(item.ItemID);

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboReportRLID, cboRlMasterReportItemID);
            ajax.AddAjaxSetting(cboReportRLID, cboReportRLID);
            ajax.AddAjaxSetting(chkIsAllowCito, chkIsAllowCito);
            ajax.AddAjaxSetting(chkIsAllowCito, chkIsCitoFromStandardReference);

            if (AppSession.Parameter.IsUsingItemSubGroup)
            {
                ajax.AddAjaxSetting(cboItemGroupID, cboSRItemSubGroup);
                ajax.AddAjaxSetting(cboItemGroupID, cboItemGroupID);
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Item());
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
                txtItemID.ReadOnly = true;

            cboItemGroupID.SelectedValue = string.Empty;
            cboItemGroupID.Text = string.Empty;
            cboBillingGroup.SelectedValue = string.Empty;
            cboBillingGroup.Text = string.Empty;
            cboSRBpjsItemGroup.SelectedValue = string.Empty;
            cboSRBpjsItemGroup.Text = string.Empty;
            chkIsActive.Checked = true;
            chkIsHasTestResults.Checked = false;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New);
            //if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            //{
            //    cboItemGroupID.Enabled = (newVal == DataMode.New);
            //}
            RefreshCommandItemGrid(newVal);
            RefreshCommandItemItemBridging(newVal);
            RefreshCommandServiceUnitItemServiceCheckList(newVal);
            RefreshCommandItemProcedure(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // Url Search & List
            UrlPageSearch = "ItemServiceSearch.aspx";
            UrlPageList = "ItemServiceList.aspx";

            ProgramID = AppConstant.Program.ServiceItem;

            trPcare.Visible = pcareReference.IsPCareValidation;

            if (!IsCallback)
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductInventory, this.Page);

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true,
                                  group.Query.SRItemType == ItemType.Service);
                group.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup entity in group)
                {
                    var itemGroupName = string.IsNullOrEmpty(entity.Initial) ? entity.ItemGroupName : entity.ItemGroupName + " [" + entity.Initial + "]";
                    cboItemGroupID.Items.Add(new RadComboBoxItem(itemGroupName, entity.ItemGroupID));
                }

                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);
                StandardReference.InitializeIncludeSpace(cboBillingGroup, AppEnum.StandardReference.BillingGroup);
                StandardReference.InitializeIncludeSpace(cboSRBpjsItemGroup, AppEnum.StandardReference.BpjsItemGroup);
                StandardReference.InitializeIncludeSpace(cboSREklaimGroup, AppEnum.StandardReference.EklaimTariffGroup);

                var ApRl = new AppProgramCollection();
                ApRl.Query.Where(ApRl.Query.ProgramID == AppConstant.Program.RlReportV2025, ApRl.Query.IsVisible == true);
                ApRl.LoadAll();
                if (ApRl.Count > 0 || !string.IsNullOrEmpty(ApRl.Query.ProgramID))
                {
                    var rl = new RlMasterReportV2025Collection();
                    rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID.In(11,13,14,15));
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
                    rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID.In(6, 12, 13, 14));
                    rl.LoadAll();
                    cboReportRLID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (RlMasterReport entity in rl)
                    {
                        cboReportRLID.Items.Add(new RadComboBoxItem(entity.RlMasterReportNo + " - " + entity.RlMasterReportName, entity.RlMasterReportID.ToString()));
                    }
                }

                trBpjsItemGroup.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                trSRItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;

                tabStrip.Tabs[2].Visible = !AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService;
                tabStrip.Tabs[3].Visible = AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService;
                tabStrip.Tabs[5].Visible = (AppSession.Parameter.IsDisplayServiceUnitBookingNoOnTransactionEntry &&
                    AppSession.Parameter.IsVisibleTrProcedureOnBookingRealization && AppSession.Parameter.IsUsingMappingServiceUnitProcedure);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Item();

            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                // cek apakah master item sudah ada transaksi
                var cek = new TransChargesItemCollection(); //new VwItemsAlreadyUsedCollection();
                cek.Query.Where(cek.Query.ItemID == txtItemID.Text);
                cek.LoadAll();
                if (cek.Count > 0)
                {
                    args.MessageText = "Item already used in transaction.";
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                var itemService = new ItemService();
                if (itemService.LoadByPrimaryKey(txtItemID.Text))
                    itemService.MarkAsDeleted();
                else
                    itemService = null;

                string itemID = txtItemID.Text;
                var itemConsumptionCollection = new ItemConsumptionCollection();
                itemConsumptionCollection.Query.Where(itemConsumptionCollection.Query.ItemID == itemID);
                itemConsumptionCollection.LoadAll();
                itemConsumptionCollection.MarkAllAsDeleted();

                var unitColl = new ServiceUnitItemServiceCollection();
                unitColl.Query.Where(unitColl.Query.ItemID == itemID);
                unitColl.LoadAll();
                unitColl.MarkAllAsDeleted();

                var procColl = new ItemServiceProcedureCollection();
                procColl.Query.Where(procColl.Query.ItemID == itemID);
                procColl.LoadAll();
                procColl.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    itemConsumptionCollection.Save();
                    unitColl.Save();
                    procColl.Save();

                    if (itemService != null)
                        itemService.Save();

                    //PCareReferenceItemMapping
                    pcareReference.Delete(itemID);

                    entity.Save();

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
            if (string.IsNullOrEmpty(cboItemGroupID.SelectedValue) && AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            {
                args.MessageText = "Item Group must selected first";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(cboItemRelatedID.SelectedValue) && Convert.ToDecimal(txtQtyDivider.Value) < 1)
            {
                args.MessageText = "Qty Divider must greater than zero.";
                args.IsCancel = true;
                return;
            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateItemIdServiceAutomaticUseGroupInitial))
            {
                txtItemID.Text = Helper.GetItemProductIDUseGroupInitial(cboItemGroupID.SelectedValue);
            }
            else if (string.IsNullOrEmpty(txtItemID.Text))
            {
                args.MessageText = "Item ID required.";
                args.IsCancel = true;
                return;
            }

            if (Helper.IsInacbgIntegration)
            {
                if (string.IsNullOrEmpty(cboSREklaimGroup.SelectedValue))
                {
                    args.MessageText = "Eklaim group required.";
                    args.IsCancel = true;
                    return;
                }
            }

            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            if (IsBarcodeUsedByOtherItem(args, txtItemID.Text, txtBarcode.Text))
            {
                return;
            }

            entity = new Item();
            entity.AddNew();
            var itemService = new ItemService();
            itemService.AddNew();
            SetEntityValue(entity, itemService);
            SaveEntity(entity, itemService);
        }

        private void SaveEntity(Item entity, ItemService itemService)
        {
            var serviceUnitItemServiceCheckListColl = new ServiceUnitItemServiceCollection();

            if (AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService)
            {
                serviceUnitItemServiceCheckListColl.Query.Where(serviceUnitItemServiceCheckListColl.Query.ItemID == entity.ItemID);
                serviceUnitItemServiceCheckListColl.LoadAll();

                foreach (GridDataItem dataItem in grdServiceUnitCheckList.MasterTableView.Items)
                {
                    string unitId = dataItem.GetDataKeyValue("ServiceUnitID").ToString();
                    bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                    bool isAllowEditByUserVerificated = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsAllowEditByUserVerificated")).Checked;
                    bool isVisible = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsVisible")).Checked;

                    bool isExist = false;
                    foreach (ServiceUnitItemService row in serviceUnitItemServiceCheckListColl)
                    {
                        if (row.ServiceUnitID.Equals(unitId))
                        {
                            isExist = true;
                            row.IsAllowEditByUserVerificated = isAllowEditByUserVerificated;
                            row.IsVisible = isVisible;
                            if (!isSelect)
                                row.MarkAsDeleted();
                            break;
                        }
                    }
                    //Add
                    if (!isExist && isSelect)
                    {
                        ServiceUnitItemService row = serviceUnitItemServiceCheckListColl.AddNew();
                        row.ItemID = entity.ItemID;
                        row.ServiceUnitID = unitId;
                        row.ChartOfAccountId = 0;
                        row.SubledgerId = 0;
                        row.IsAllowEditByUserVerificated = isAllowEditByUserVerificated;
                        row.IsVisible = isVisible;
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        row.LastUpdateDateTime = DateTime.Now;
                    }
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                itemService.Save();
                if (AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService)
                    serviceUnitItemServiceCheckListColl.Save();
                else
                    ServiceUnitItemServices.Save();

                ItemConsumptions.Save();
                ItemBridgings.Save();
                ItemServiceProcedures.Save();

                //PCareReferenceItemMapping
                pcareReference.Save(entity.ItemID);

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (!string.IsNullOrEmpty(cboItemRelatedID.SelectedValue) && Convert.ToDecimal(txtQtyDivider.Value) < 1)
            {
                args.MessageText = "Qty Divider must greater than zero.";
                args.IsCancel = true;
                return;
            }

            if (Helper.IsInacbgIntegration)
            {
                if (string.IsNullOrEmpty(cboSREklaimGroup.SelectedValue))
                {
                    args.MessageText = "Eklaim group required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (IsBarcodeUsedByOtherItem(args, txtItemID.Text, txtBarcode.Text))
            {
                return;
            }

            Item entity = new Item();
            ItemService itemService = new ItemService();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                if (!itemService.LoadByPrimaryKey(txtItemID.Text))
                {
                    itemService = new ItemService();
                    itemService.AddNew();
                }
                SetEntityValue(entity, itemService);
                SaveEntity(entity, itemService);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemConsumption.Columns[0].Visible = isVisible;
            grdItemConsumption.Columns[grdItemConsumption.Columns.Count - 1].Visible = isVisible;

            grdItemConsumption.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemConsumption.Rebind();

            grdServiceUnit.Columns[0].Visible = isVisible;
            grdServiceUnit.Columns[grdServiceUnit.Columns.Count - 1].Visible = isVisible;

            grdServiceUnit.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdServiceUnit.Rebind();
        }

        #region ItemConsumptions
        private ItemConsumptionCollection ItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemConsumption"];
                    if (obj != null)
                    {
                        return ((ItemConsumptionCollection)(obj));
                    }
                }

                ItemConsumptionCollection coll = new ItemConsumptionCollection();
                ItemConsumptionQuery query = new ItemConsumptionQuery("a");
                ItemQuery item = new ItemQuery("b");

                string itemID = txtItemID.Text;
                query.Where(query.ItemID == itemID);
                query.Select(query.SelectAllExcept(), item.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                coll.Load(query);

                Session["collItemConsumption"] = coll;
                return coll;
            }
            set { Session["collItemConsumption"] = value; }
        }

        private void PopulateItemConsumptionGrid()
        {
            //Display Data Detail
            ItemConsumptions = null; //Reset Record Detail
            grdItemConsumption.DataSource = ItemConsumptions; //Requery
            grdItemConsumption.MasterTableView.IsItemInserted = false;
            grdItemConsumption.MasterTableView.ClearEditItems();
            grdItemConsumption.DataBind();
        }

        protected void grdItemConsumption_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemConsumption.DataSource = ItemConsumptions;
        }

        protected void grdItemConsumption_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemConsumptionMetadata.ColumnNames.DetailItemID]);
            ItemConsumption entity = FindItemConsumption(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemConsumption_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemConsumptionMetadata.ColumnNames.DetailItemID]);
            ItemConsumption entity = FindItemConsumption(itemID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemConsumption_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemConsumption entity = ItemConsumptions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemConsumption.Rebind();
        }

        private ItemConsumption FindItemConsumption(String itemID)
        {
            ItemConsumptionCollection coll = ItemConsumptions;
            ItemConsumption retEntity = null;
            foreach (ItemConsumption rec in coll)
            {
                if (rec.DetailItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ItemConsumption entity, GridCommandEventArgs e)
        {
            ItemConsumptionDetail userControl = (ItemConsumptionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DetailItemName = userControl.DetailItemName;
                entity.DetailItemID = userControl.DetailItemID;
                entity.Qty = userControl.Qty ?? 0;
                entity.SRItemUnit = userControl.SRItemUnit;
            }
        }
        #endregion

        #region ServiceUnitItemServices
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

                var coll = new ServiceUnitItemServiceCollection();
                var query = new ServiceUnitItemServiceQuery("a");
                var unit = new ServiceUnitQuery("b");

                string itemId = txtItemID.Text;
                query.Where(query.ItemID == itemId);
                query.Select(query.SelectAllExcept(), unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"));
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                coll.Load(query);

                Session["collServiceUnitItemService"] = coll;
                return coll;
            }
            set { Session["collServiceUnitItemService"] = value; }
        }

        private void PopulateServiceUnitItemServiceGrid()
        {
            //Display Data Detail
            ServiceUnitItemServices = null; //Reset Record Detail
            grdServiceUnit.DataSource = ServiceUnitItemServices; //Requery
            grdServiceUnit.MasterTableView.IsItemInserted = false;
            grdServiceUnit.MasterTableView.ClearEditItems();
            grdServiceUnit.DataBind();
        }

        protected void grdServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnit.DataSource = ServiceUnitItemServices;
        }

        protected void grdServiceUnit_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String unitID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitItemService entity = FindItemServiceUnit(unitID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdServiceUnit_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitItemService entity = ServiceUnitItemServices.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnit.Rebind();
        }

        protected void grdServiceUnit_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String unitID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID]);
            ServiceUnitItemService entity = FindItemServiceUnit(unitID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private ServiceUnitItemService FindItemServiceUnit(String unitId)
        {
            ServiceUnitItemServiceCollection coll = ServiceUnitItemServices;
            ServiceUnitItemService retEntity = null;
            foreach (ServiceUnitItemService rec in coll)
            {
                if (rec.ServiceUnitID.Equals(unitId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ServiceUnitItemService entity, GridCommandEventArgs e)
        {
            var userControl = (ItemServiceUnitDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ChartOfAccountId = 0;
                entity.SubledgerId = 0;
                entity.IsAllowEditByUserVerificated = userControl.IsAllowEditByUserVerificated;
                entity.IsVisible = userControl.IsVisible;
            }
        }
        #endregion


        #endregion

        #region Record Detail Method Function ServiceUnitItemServices_CheckList

        private void PopulateServiceUnitItemServiceCheckListGrid()
        {
            //Display Data Detail
            grdServiceUnitCheckList.DataSource = GetServiceUnitItemServiceCheckList();
            grdServiceUnitCheckList.DataBind();
        }

        protected void grdServiceUnitCheckList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitCheckList.DataSource = GetServiceUnitItemServiceCheckList();
        }

        private DataTable GetServiceUnitItemServiceCheckList()
        {
            if (!AppSession.Parameter.IsUsingCheckListForMatrixServiceUnitItemService)
                return null;

            var query = new ServiceUnitItemServiceQuery("a");
            var suq = new ServiceUnitQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID);
                query.Where(query.ItemID == txtItemID.Text);
            }
            else
            {
                query.RightJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID & query.ItemID == txtItemID.Text);
            }
            query.OrderBy(suq.ServiceUnitID.Ascending);
            query.Select
                (
                    "<CONVERT(BIT,CASE WHEN COALESCE(a.ServiceUnitID,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    suq.ServiceUnitID,
                    suq.ServiceUnitName,
                    "<ISNULL(a.IsAllowEditByUserVerificated, 1) as IsAllowEditByUserVerificated>",
                    "<ISNULL(a.IsVisible, 1) as IsVisible>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandServiceUnitItemServiceCheckList(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdServiceUnitCheckList.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdServiceUnitCheckList.Rebind();
        }
        #endregion

        #region Item Tariff

        protected void grdPriceHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdPriceHistory.DataSource = GetItemTariffHistory();
            }
        }

        private DataTable GetItemTariffHistory()
        {
            ItemTariff itemTariff = new ItemTariff();
            return itemTariff.GetHistory(txtItemID.Text);
        }

        protected void grdPriceHistory_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tariffType = dataItem.GetDataKeyValue("SRTariffType").ToString();
            string itemID = dataItem.GetDataKeyValue("ItemID").ToString();
            string classID = dataItem.GetDataKeyValue("ClassID").ToString();
            DateTime startingDate = Convert.ToDateTime(dataItem.GetDataKeyValue("StartingDate"));

            DataTable dtb = (new ItemTariff()).GetItemTariffComponent(tariffType, itemID, classID, startingDate);
            e.DetailTableView.DataSource = dtb;
        }

        #endregion

        #region Item Related

        protected void cboItemRelatedID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemRelatedID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.es.Top = 15;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.SRItemType.In
                        (
                            ItemType.Medical,
                            ItemType.NonMedical
                        ),
                    query.Or
                        (
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);
            cboItemRelatedID.DataSource = query.LoadDataTable();
            cboItemRelatedID.DataBind();
        }
        #endregion

        //protected void cboReportRLID_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    cboRlMasterReportItemID.Items.Clear();
        //    cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(string.Empty, "0"));
        //    cboRlMasterReportItemID.Text = string.Empty;

        //    if (!string.IsNullOrEmpty(e.Value))
        //    {
        //        var coll = new RlMasterReportItemCollection();
        //        coll.Query.Where(coll.Query.RlMasterReportID == Convert.ToInt32(e.Value), coll.Query.IsActive == true);
        //        coll.LoadAll();

        //        foreach (RlMasterReportItem entity in coll)
        //        {
        //            cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(entity.RlMasterReportItemCode + " - " + entity.RlMasterReportItemName, entity.RlMasterReportItemID.ToString()));
        //        }
        //    }
        //}

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
                if (ApRl.Count > 0 || !string.IsNullOrEmpty(ApRl.Query.ProgramID))
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

        protected void cboItemGroupID_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRItemSubGroup.Items.Clear();
            cboSRItemSubGroup.Text = string.Empty;
        }

        protected void cboSRItemSubGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.ItemSubGroup.ToString(),
                    query.ReferenceID == cboItemGroupID.SelectedValue,
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            cboSRItemSubGroup.DataSource = query.LoadDataTable();
            cboSRItemSubGroup.DataBind();
        }

        protected void cboSRItemSubGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void chkIsAllowCito_CheckedChanged(object sender, EventArgs e)
        {
            chkIsCitoFromStandardReference.Enabled = chkIsAllowCito.Checked;
            chkIsCitoFromStandardReference.Checked = false;
        }

        #region Record Detail Method Function ParamedicBridging

        private ItemBridgingCollection ItemBridgings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemBridging"];
                    if (obj != null) return ((ItemBridgingCollection)(obj));
                }

                ItemBridgingCollection coll = new ItemBridgingCollection();

                ItemBridgingQuery query = new ItemBridgingQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToAppStandardReferenceItem_ItemName"));
                query.InnerJoin(asri).On(query.SRBridgingType == asri.ItemID && asri.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString());
                query.Where(query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collItemBridging"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicBridging"] = value;
            }
        }

        private void RefreshCommandItemItemBridging(AppEnum.DataMode newVal)
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
            ItemBridgings = null;
            grdAliasName.DataSource = ItemBridgings;
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = ItemBridgings;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String type = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindItemBridging(type, id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String type = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBridgingMetadata.ColumnNames.SRBridgingType]);
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBridgingMetadata.ColumnNames.BridgingID]);

            var entity = FindItemBridging(type, id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemBridgings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private ItemBridging FindItemBridging(String type, string id)
        {
            var coll = ItemBridgings;
            return coll.FirstOrDefault(rec => rec.SRBridgingType.Equals(type) && rec.BridgingID.Equals(id));
        }

        private void SetEntityValue(ItemBridging entity, GridCommandEventArgs e)
        {
            ItemAliasDetail userControl = (ItemAliasDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.SRBridgingType = userControl.BridgingType;
                entity.BridgingTypeName = userControl.BridgingTypeName;
                entity.BridgingID = userControl.BridgingID;
                entity.BridgingName = string.IsNullOrEmpty(userControl.BridgingName) ? txtItemName.Text : userControl.BridgingName;
                entity.ItemIdExternal = userControl.ItemIdExternal;
                entity.IsActive = userControl.IsActive;
            }
        }

        #endregion

        #region Record Detail Method Function ItemServiceProcedure

        private ItemServiceProcedureCollection ItemServiceProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemServiceProcedure"];
                    if (obj != null) return ((ItemServiceProcedureCollection)(obj));
                }

                ItemServiceProcedureCollection coll = new ItemServiceProcedureCollection();

                ItemServiceProcedureQuery query = new ItemServiceProcedureQuery("a");
                AppStandardReferenceItemQuery asri = new AppStandardReferenceItemQuery("b");

                query.Select(query, asri.ItemName.As("refToStdRef_Procedure"));
                query.InnerJoin(asri).On(asri.StandardReferenceID == AppEnum.StandardReference.Procedure.ToString() && query.SRProcedure == asri.ItemID);
                query.Where(query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collItemServiceProcedure"] = coll;
                return coll;
            }
            set
            {
                Session["collItemServiceProcedure"] = value;
            }
        }

        private void RefreshCommandItemProcedure(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdProcedure.Columns[grdProcedure.Columns.Count - 1].Visible = isVisible;

            grdProcedure.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdProcedure.Rebind();
        }

        private void PopulateItemProcedureGrid()
        {
            ItemServiceProcedures = null;
            grdProcedure.DataSource = ItemServiceProcedures;
            grdProcedure.MasterTableView.IsItemInserted = false;
            grdProcedure.MasterTableView.ClearEditItems();
            grdProcedure.DataBind();
        }

        protected void grdProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdProcedure.DataSource = ItemServiceProcedures;
        }

        protected void grdProcedure_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemServiceProcedureMetadata.ColumnNames.SRProcedure]);

            var entity = FindItemProcedure(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdProcedure_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ItemServiceProcedures.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdProcedure.Rebind();
        }

        private ItemServiceProcedure FindItemProcedure(string id)
        {
            var coll = ItemServiceProcedures;
            return coll.FirstOrDefault(rec => rec.SRProcedure.Equals(id));
        }

        private void SetEntityValue(ItemServiceProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (ItemProcedureDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = txtItemID.Text;
                entity.SRProcedure = userControl.SRProcedure;
                entity.ProcedureName = userControl.ProcedureName;
            }
        }

        #endregion

        private bool IsBarcodeUsedByOtherItem(ValidateArgs args, string itemID, string bc)
        {
            args.IsCancel = false;
            var barcodeUsedBy = BarcodeUsed(itemID, bc);
            if (barcodeUsedBy != null)
            {
                args.MessageText = string.Format("Barcode has used by item: {0} {1}", barcodeUsedBy.ItemID, barcodeUsedBy.ItemName);
                args.IsCancel = true;
            }
            return args.IsCancel;
        }
        private Item BarcodeUsed(string itemID, string bc)
        {
            if (string.IsNullOrEmpty(bc))
                return null;

            var item = new Item();
            if (item.LoadByBarcode(bc) && item.ItemID != itemID)
            {
                return item;
            }
            return null;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdServiceUnitCheckList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked = selected;
            }
        }
    }
}