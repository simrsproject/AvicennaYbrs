using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemLaboratoryDetail : BasePageDetail
    {
        private void SetEntityValue(Item entity, ItemLaboratory itemLaboratory)
        {
            //Item
            entity.ItemID = txtItemID.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;
            entity.SRItemSubGroup = cboSRItemSubGroup.SelectedValue;
            entity.Barcode = txtBarcode.Text;
            entity.SRItemType = BusinessObject.Reference.ItemType.Laboratory;
            entity.SRBillingGroup = cboBillingGroup.SelectedValue;
            entity.SRBpjsItemGroup = cboSRBpjsItemGroup.SelectedValue;
            entity.ItemName = txtItemName.Text;
            entity.IsActive = chkIsActive.Checked;
            entity.IsDelegationToNurse = chkIsDelegationToNurse.Checked;
            entity.Notes = txtNotes.Text;
            entity.ItemIDExternal = txtItemIDExternal.Text;
            entity.IsHasTestResults = chkIsHasTestResults.Checked;
            entity.IsNeedToBeSterilized = false;
            entity.SREklaimTariffGroup = cboSREklaimGroup.SelectedValue;
            entity.SREklaimFactorGroup = cboSREklaimFactorGroup.SelectedValue;
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

            //ItemLaboratory
            itemLaboratory.ItemID = entity.ItemID;
            itemLaboratory.ReportRLID = cboReportRLID.SelectedValue;
            itemLaboratory.RlMasterReportItemID = string.IsNullOrEmpty(cboRlMasterReportItemID.SelectedValue) ? 0 : Convert.ToInt32(cboRlMasterReportItemID.SelectedValue);
            itemLaboratory.IsAdminCalculation = chkIsAdminCalculation.Checked;
            itemLaboratory.IsAllowVariable = chkIsAllowVariable.Checked;
            itemLaboratory.IsAllowCito = chkIsAllowCito.Checked;
            itemLaboratory.IsAllowDiscount = chkIsAllowDiscount.Checked;
            itemLaboratory.IsAssetUtilization = chkIsAssetUtilization.Checked;
            itemLaboratory.IsDisplayInOrderList = chkIsDisplayInOrderList.Checked;
            itemLaboratory.SRExaminationClass = cboSRExaminationClass.SelectedValue;
            itemLaboratory.SRLaboratoryUnit = cboSRLabUnit.SelectedValue;
            itemLaboratory.IsConfidential = chkIsConfidential.Checked;
            itemLaboratory.IsResultOnSepataredPage = chkIsResultOnSepataredPage.Checked;
            itemLaboratory.IsCitoFromStandardReference = chkIsCitoFromStandardReference.Checked;
            itemLaboratory.WaitingTimeForResults = Convert.ToInt16(txtWaitingTimeForResults.Value);
            itemLaboratory.SRIntervalTime = cboSRIntervalTime.SelectedValue;
            itemLaboratory.SRSpecimenType = cboSRSpecimenType.SelectedValue;
            itemLaboratory.IsCulture = chkIsCulture.Checked;

            //Last Update Status
            if (itemLaboratory.es.IsAdded || itemLaboratory.es.IsModified)
            {
                itemLaboratory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                itemLaboratory.LastUpdateDateTime = DateTime.Now;
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

            ItemLaboratoryDetailCollection collResult = ItemResults;
            foreach (var unit in ItemResults)
            {
                unit.ItemID = txtItemID.Text;

                //Last Update Status
                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }

            ItemLaboratoryProfileCollection collProfile = ItemProfiles;
            foreach (var unit in ItemProfiles)
            {
                if (rblProfileType.SelectedValue == "0") unit.ParentItemID = txtItemID.Text;
                else unit.DetailItemID = txtItemID.Text;
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

                if (unit.es.IsAdded || unit.es.IsModified)
                {
                    unit.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    unit.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ItemQuery que = new ItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Laboratory);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text, que.SRItemType == BusinessObject.Reference.ItemType.Laboratory);
                que.OrderBy(que.ItemID.Descending);
            }
            Item entity = new Item();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnMenuEditClick()
        {
            chkIsCitoFromStandardReference.Enabled = chkIsAllowCito.Checked;
        }
        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("ItemID='{0}'", txtItemID.Text.Trim());
            auditLogFilter.TableName = "ItemLaboratory";
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
            Item item = (Item)entity;
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

            ItemLaboratory itemLaboratory = new ItemLaboratory();
            if (item.ItemID != null) itemLaboratory.LoadByPrimaryKey(item.ItemID);
            cboReportRLID.SelectedValue = itemLaboratory.ReportRLID;

            cboReportRLID_SelectedIndexChanged(cboReportRLID, new RadComboBoxSelectedIndexChangedEventArgs(
                                                                      cboReportRLID.Text, string.Empty,
                                                                      cboReportRLID.SelectedValue, string.Empty));

            if (itemLaboratory.RlMasterReportItemID != null & itemLaboratory.RlMasterReportItemID != 0)
            {
                cboRlMasterReportItemID.SelectedValue = itemLaboratory.RlMasterReportItemID.ToString();

                var rl = new RlMasterReportItem();
                if (rl.LoadByPrimaryKey(Convert.ToInt32(itemLaboratory.RlMasterReportItemID)))
                    cboRlMasterReportItemID.Text = rl.RlMasterReportItemCode + " - " + rl.RlMasterReportItemName;
            }

            cboSREklaimGroup.SelectedValue = item.SREklaimTariffGroup;
            cboSREklaimFactorGroup.SelectedValue = item.SREklaimFactorGroup;
            txtIntervalOrderWarning.Value = Convert.ToDouble(item.IntervalOrderWarning);

            chkIsAdminCalculation.Checked = itemLaboratory.IsAdminCalculation ?? false;
            chkIsAllowVariable.Checked = itemLaboratory.IsAllowVariable ?? false;
            chkIsAllowCito.Checked = itemLaboratory.IsAllowCito ?? false;
            chkIsAllowDiscount.Checked = itemLaboratory.IsAllowDiscount ?? false;
            chkIsAssetUtilization.Checked = itemLaboratory.IsAssetUtilization ?? false;
            txtItemIDExternal.Text = item.ItemIDExternal;
            chkIsDisplayInOrderList.Checked = itemLaboratory.IsDisplayInOrderList ?? false;
            cboSRExaminationClass.SelectedValue = itemLaboratory.SRExaminationClass;
            cboSRSpecimenType.SelectedValue = itemLaboratory.SRSpecimenType;
            cboSRLabUnit.SelectedValue = itemLaboratory.SRLaboratoryUnit;
            chkIsConfidential.Checked = itemLaboratory.IsConfidential ?? false;
            chkIsCulture.Checked = itemLaboratory.IsCulture ?? false;
            chkIsResultOnSepataredPage.Checked = itemLaboratory.IsResultOnSepataredPage ?? false;
            chkIsCitoFromStandardReference.Checked = itemLaboratory.IsCitoFromStandardReference ?? false;
            txtWaitingTimeForResults.Value = Convert.ToDouble(itemLaboratory.WaitingTimeForResults);

            if (!string.IsNullOrEmpty(itemLaboratory.SRIntervalTime))
            {
                var IntervalTime = new AppStandardReferenceItemQuery();
                IntervalTime.Where(IntervalTime.ItemID == itemLaboratory.str.SRIntervalTime,
                                  IntervalTime.StandardReferenceID == AppEnum.StandardReference.IntervalTime);
                cboSRIntervalTime.DataSource = IntervalTime.LoadDataTable();
                cboSRIntervalTime.DataBind();
                cboSRIntervalTime.SelectedValue = itemLaboratory.SRIntervalTime;
            }
            else
            {
                cboSRIntervalTime.Items.Clear();
                cboSRIntervalTime.Text = string.Empty;
            }


            //var profile = new ItemLaboratoryProfile();
            //profile.Query.Where(profile.Query.DetailItemID == txtItemID.Text);
            //if (!string.IsNullOrEmpty(Request.QueryString["parentid"]))
            //{
            //    profile.Query.Where(profile.Query.ParentItemID == Request.QueryString["parentid"]);
            //}
            //if (profile.Query.Load())
            //{
            //    var query = new ItemQuery();
            //    query.Select(query.ItemID, query.ItemName);
            //    query.Where(
            //            query.SRItemType == ItemType.Laboratory,
            //            query.ItemID == profile.ParentItemID
            //        );
            //    cboParentTestName.DataSource = query.LoadDataTable();
            //    cboParentTestName.DataBind();
            //    cboParentTestName.SelectedValue = profile.ParentItemID;
            //}

            var profile = new ItemLaboratoryProfileCollection();
            profile.Query.Where(profile.Query.ParentItemID == txtItemID.Text);
            rblProfileType.SelectedValue = profile.Query.Load() ? "0" : "1";

            PopulateItemConsumptionGrid();
            grdPriceHistory.Rebind();
            PopulateServiceUnitItemServiceGrid();
            PopulateItemResultGrid();
            PopulateItemProfileGrid();
            PopulateItemBirdgingGrid();
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

            cboSREklaimGroup.SelectedValue = "08";
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
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // Url Search & List
            UrlPageSearch = "ItemLaboratorySearch.aspx";
            UrlPageList = "ItemLaboratoryList.aspx";

            ProgramID = AppConstant.Program.LaboratoryItem;

            if (!IsCallback)
            {
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductInventory, this.Page);
            }

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true,
                                  group.Query.SRItemType == ItemType.Laboratory);
                group.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup entity in group)
                {
                    var itemGroupName = string.IsNullOrEmpty(entity.Initial) ? entity.ItemGroupName : entity.ItemGroupName + " [" + entity.Initial + "]";
                    cboItemGroupID.Items.Add(new RadComboBoxItem(itemGroupName, entity.ItemGroupID));
                }

                StandardReference.InitializeIncludeSpace(cboBillingGroup, AppEnum.StandardReference.BillingGroup);
                StandardReference.InitializeIncludeSpace(cboSRBpjsItemGroup, AppEnum.StandardReference.BpjsItemGroup);
                StandardReference.InitializeIncludeSpace(cboSREklaimGroup, AppEnum.StandardReference.EklaimTariffGroup);
                StandardReference.InitializeIncludeSpace(cboSREklaimFactorGroup, AppEnum.StandardReference.EklaimFactorGroup);

                var rl = new RlMasterReportCollection();
                rl.Query.Where(rl.Query.IsActive == true, rl.Query.RlMasterReportID == 11);
                rl.LoadAll();
                cboReportRLID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (RlMasterReport entity in rl)
                {
                    cboReportRLID.Items.Add(new RadComboBoxItem(entity.RlMasterReportNo + " - " + entity.RlMasterReportName, entity.RlMasterReportID.ToString()));
                }

                StandardReference.InitializeIncludeSpace(cboSRExaminationClass, AppEnum.StandardReference.ExaminationClass);
                StandardReference.InitializeIncludeSpace(cboSRLabUnit, AppEnum.StandardReference.LaboratoryUnit);
                StandardReference.InitializeIncludeSpace(cboSRSpecimenType, AppEnum.StandardReference.SpecimenType);

                trBpjsItemGroup.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                trSRItemSubGroup.Visible = AppSession.Parameter.IsUsingItemSubGroup;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                // cek apakah master item sudah ada transaksi
                var cek = new TransChargesItemCollection();//VwItemsAlreadyUsedCollection();
                cek.Query.Where(cek.Query.ItemID == txtItemID.Text);
                cek.LoadAll();
                if (cek.Count > 0)
                {
                    args.MessageText = "Item already used in transaction.";
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                ItemLaboratory itemLaboratory = new ItemLaboratory();
                if (itemLaboratory.LoadByPrimaryKey(txtItemID.Text))
                    itemLaboratory.MarkAsDeleted();
                else
                    itemLaboratory = null;

                string itemID = txtItemID.Text;
                ItemConsumptionCollection itemConsumptionCollection = new ItemConsumptionCollection();
                itemConsumptionCollection.Query.Where(itemConsumptionCollection.Query.ItemID == itemID);
                itemConsumptionCollection.LoadAll();
                itemConsumptionCollection.MarkAllAsDeleted();

                var unitColl = new ServiceUnitItemServiceCollection();
                unitColl.Query.Where(unitColl.Query.ItemID == itemID);
                unitColl.LoadAll();
                unitColl.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    itemConsumptionCollection.Save();
                    unitColl.Save();

                    if (itemLaboratory != null)
                        itemLaboratory.Save();

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

            if (IsBarcodeUsedByOtherItem(args, txtItemID.Text, txtBarcode.Text))
            {
                return;
            }

            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Item();
            entity.AddNew();
            ItemLaboratory itemLaboratory = new ItemLaboratory();
            itemLaboratory.AddNew();
            SetEntityValue(entity, itemLaboratory);
            SaveEntity(entity, itemLaboratory);
        }

        private void SaveEntity(Item entity, ItemLaboratory itemLaboratory)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                itemLaboratory.Save();
                ItemConsumptions.Save();
                ServiceUnitItemServices.Save();
                ItemResults.Save();

                //if (!string.IsNullOrEmpty(cboParentTestName.SelectedValue))
                //{
                //    //var profile = new ItemLaboratoryProfile();
                //    //if (profile.LoadByPrimaryKey(cboParentTestName.SelectedValue, txtItemID.Text))
                //    //{
                //    //    profile.MarkAsDeleted();
                //    //    profile.Save();
                //    //};
                //    /*----------- 1 detail hanya mengacu ke 1 parent ------------ */
                //    var displaySequence = 1;
                //    var ilp = new BusinessObject.ItemLaboratoryProfile();
                //    ilp.Query.es.Top = 1;
                //    ilp.Query.Select(ilp.Query.DisplaySequence);
                //    ilp.Query.Where(ilp.Query.ParentItemID == cboParentTestName.SelectedValue);
                //    ilp.Query.OrderBy(ilp.Query.DisplaySequence.Descending);
                //    if (ilp.Query.Load()) displaySequence = (ilp.DisplaySequence ?? 0) + 1;

                //    //var profile = new ItemLaboratoryProfileCollection();
                //    //profile.Query.Where(profile.Query.DetailItemID == txtItemID.Text);
                //    //profile.LoadAll();
                //    //profile.MarkAllAsDeleted();
                //    //profile.Save();

                //    //ilp = new BusinessObject.ItemLaboratoryProfile
                //    //{
                //    //    ParentItemID = cboParentTestName.SelectedValue,
                //    //    DetailItemID = txtItemID.Text,
                //    //    DisplaySequence = displaySequence,
                //    //    IsDisplayInResult = true,
                //    //    LastUpdateByUserID = AppSession.UserLogin.UserID,
                //    //    LastUpdateDateTime = DateTime.Now
                //    //};
                //    //ilp.Save();
                //}

                ItemProfiles.Save();
                ItemBridgings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
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
            ItemLaboratory itemLaboratory = new ItemLaboratory();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
            {
                if (!itemLaboratory.LoadByPrimaryKey(txtItemID.Text))
                {
                    itemLaboratory = new ItemLaboratory();
                    itemLaboratory.AddNew();
                }
                SetEntityValue(entity, itemLaboratory);
                SaveEntity(entity, itemLaboratory);
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
            grdItemConsumption.Columns[grdItemConsumption.Columns.Count - 2].Visible = isVisible;

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

            grdLabResult.Columns[0].Visible = isVisible;
            grdLabResult.Columns[grdLabResult.Columns.Count - 1].Visible = isVisible;

            grdLabResult.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdLabResult.Rebind();


            grdLabProfile.Columns[0].Visible = false;
            grdLabProfile.Columns[grdLabProfile.Columns.Count - 1].Visible = isVisible;

            grdLabProfile.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdLabProfile.Rebind();
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

        #region ItemResult
        private ItemLaboratoryDetailCollection ItemResults
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemLaboratoryDetail"];
                    if (obj != null)
                    {
                        return ((ItemLaboratoryDetailCollection)(obj));
                    }
                }

                ItemLaboratoryDetailCollection coll = new ItemLaboratoryDetailCollection();

                ItemLaboratoryDetailQuery query = new ItemLaboratoryDetailQuery("a");
                AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("b");
                AppStandardReferenceItemQuery item2 = new AppStandardReferenceItemQuery("c");
                QuestionAnswerSelectionQuery line = new QuestionAnswerSelectionQuery("d");

                string itemID = txtItemID.Text;
                query.Where(query.ItemID == itemID);
                query.Select(query, item.ItemName.As("refToAppStandardReferenceItem_SRAgeUnit"),
                    item2.ItemName.As("refToAppStandardReferenceItem_SRAnswerType"),
                    line.QuestionAnswerSelectionText.As("refToQuestionAnswerSelection_AnswerTypeReferenceID"));
                query.InnerJoin(item).On(query.SRAgeUnit == item.ItemID && item.StandardReferenceID == AppEnum.StandardReference.AgeUnit);
                query.LeftJoin(item2).On(query.SRAnswerType == item2.ItemID && item2.StandardReferenceID == AppEnum.StandardReference.AnswerType);
                query.LeftJoin(line).On(query.AnswerTypeReferenceID == line.QuestionAnswerSelectionID);
                query.OrderBy(query.Sex.Ascending, query.SRAgeUnit.Ascending, query.AgeMin.Ascending);
                coll.Load(query);

                Session["collItemLaboratoryDetail"] = coll;
                return coll;
            }
            set { Session["collItemLaboratoryDetail"] = value; }
        }

        private void PopulateItemResultGrid()
        {
            //Display Data Detail
            ItemResults = null; //Reset Record Detail
            grdLabResult.DataSource = ItemResults; //Requery
            grdLabResult.MasterTableView.IsItemInserted = false;
            grdLabResult.MasterTableView.ClearEditItems();
            grdLabResult.DataBind();
        }

        protected void grdLabResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLabResult.DataSource = ItemResults;
        }

        protected void grdLabResult_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo]);
            BusinessObject.ItemLaboratoryDetail entity = FindItemResult(itemID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdLabResult_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo]);
            BusinessObject.ItemLaboratoryDetail entity = FindItemResult(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdLabResult_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.ItemLaboratoryDetail entity = ItemResults.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLabResult.Rebind();
        }

        private BusinessObject.ItemLaboratoryDetail FindItemResult(String itemID)
        {
            ItemLaboratoryDetailCollection coll = ItemResults;
            BusinessObject.ItemLaboratoryDetail retEntity = null;
            foreach (BusinessObject.ItemLaboratoryDetail rec in coll)
            {
                if (rec.SequenceNo.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BusinessObject.ItemLaboratoryDetail entity, GridCommandEventArgs e)
        {
            ItemLaboratoryResult userControl = (ItemLaboratoryResult)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.Sex = userControl.Sex;
                entity.SRAgeUnit = userControl.SRAgeUnit;
                entity.AgeUnitName = userControl.AgeUnit;
                entity.AgeMin = userControl.AgeMin;
                entity.TotalAgeMin = BusinessObject.ItemLaboratoryDetail.CalculateTotalAge(userControl.SRAgeUnit, entity.AgeMin ?? 0);
                entity.AgeMax = userControl.AgeMax;
                entity.TotalAgeMax = BusinessObject.ItemLaboratoryDetail.CalculateTotalAge(userControl.SRAgeUnit, entity.AgeMax ?? 0);
                entity.SRAnswerType = userControl.SRAnswerType;
                entity.AnswerTypeName = userControl.AnswerTypeName;
                entity.NormalValueMin = userControl.NormalValueMin;
                entity.NormalValueMax = userControl.NormalValueMax;
                entity.Notes = userControl.Notes;
                entity.AnswerTypeReferenceID = userControl.AnswerTypeReferenceID;
                entity.AnswerTypeReferenceName = userControl.AnswerTypeReferenceName;
            }
        }
        #endregion

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

        protected void cboReportRLID_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRlMasterReportItemID.Items.Clear();
            cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(string.Empty, "0"));
            cboRlMasterReportItemID.Text = string.Empty;

            if (!string.IsNullOrEmpty(e.Value))
            {
                var coll = new RlMasterReportItemCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == Convert.ToInt32(e.Value), coll.Query.IsActive == true);
                coll.LoadAll();

                foreach (RlMasterReportItem entity in coll)
                {
                    cboRlMasterReportItemID.Items.Add(new RadComboBoxItem(entity.RlMasterReportItemCode + " - " + entity.RlMasterReportItemName, entity.RlMasterReportItemID.ToString()));
                }
            }
        }

        protected void chkIsAllowCito_CheckedChanged(object sender, EventArgs e)
        {
            chkIsCitoFromStandardReference.Enabled = chkIsAllowCito.Checked;
            chkIsCitoFromStandardReference.Checked = false;
        }

        protected void cboParentTestName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            /*hanya item lab yg tidak ada parent yg muncul*/
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var profile = new ItemLaboratoryProfileQuery("b");
            query.LeftJoin(profile).On(query.ItemID == profile.DetailItemID);
            query.es.Top = 10;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                    query.SRItemType == ItemType.Laboratory,
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    profile.ParentItemID.IsNull()
                );
            cboParentTestName.DataSource = query.LoadDataTable();
            cboParentTestName.DataBind();
        }

        protected void cboParentTestName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRIntervalTime_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                        query.StandardReferenceID == AppEnum.StandardReference.IntervalTime.ToString(),
                        query.IsActive == true
                );

            cboSRIntervalTime.DataSource = query.LoadDataTable();
            cboSRIntervalTime.DataBind();
        }

        protected void cboSRIntervalTime_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
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

        #region ItemProfile

        private ItemLaboratoryProfileCollection ItemProfiles
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemLaboratoryProfile"];
                    if (obj != null)
                    {
                        return ((ItemLaboratoryProfileCollection)(obj));
                    }
                }

                ItemLaboratoryProfileCollection coll = new ItemLaboratoryProfileCollection();

                ItemLaboratoryProfileQuery query = new ItemLaboratoryProfileQuery("a");
                ItemQuery item = new ItemQuery("b");

                string itemID = txtItemID.Text;
                query.Select(query, item.ItemName.As("refToItem_ItemName"));

                if (rblProfileType.SelectedValue == "0")
                {
                    query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                    query.Where(query.ParentItemID == itemID);
                }
                else
                {
                    query.InnerJoin(item).On(query.ParentItemID == item.ItemID);
                    query.Where(query.DetailItemID == itemID);
                }

                query.OrderBy(query.DisplaySequence.Ascending);
                coll.Load(query);

                Session["collItemLaboratoryProfile"] = coll;
                return coll;
            }
            set { Session["collItemLaboratoryProfile"] = value; }
        }

        private void PopulateItemProfileGrid()
        {
            //Display Data Detail
            ItemProfiles = null; //Reset Record Detail
            grdLabProfile.DataSource = ItemProfiles; //Requery
            grdLabProfile.MasterTableView.IsItemInserted = false;
            grdLabProfile.MasterTableView.ClearEditItems();
            grdLabProfile.DataBind();
        }

        protected void grdLabProfile_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLabProfile.DataSource = ItemProfiles;
        }

        protected void grdLabProfile_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID]);
            BusinessObject.ItemLaboratoryProfile entity = FindItemProfile(itemID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdLabProfile_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID]);
            BusinessObject.ItemLaboratoryProfile entity = FindItemProfile(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdLabProfile_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.ItemLaboratoryProfile entity = ItemProfiles.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLabProfile.Rebind();
        }

        private BusinessObject.ItemLaboratoryProfile FindItemProfile(string itemID)
        {
            ItemLaboratoryProfileCollection coll = ItemProfiles;
            BusinessObject.ItemLaboratoryProfile retEntity = null;
            foreach (BusinessObject.ItemLaboratoryProfile rec in coll)
            {
                if (rec.DetailItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BusinessObject.ItemLaboratoryProfile entity, GridCommandEventArgs e)
        {
            ItemLaboratoryProfile userControl = (ItemLaboratoryProfile)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                if (rblProfileType.SelectedValue == "0")
                {
                    entity.ParentItemID = txtItemID.Text;
                    entity.DetailItemID = userControl.ProfileItemID;

                }
                else
                {
                    entity.ParentItemID = userControl.ProfileItemID; ;
                    entity.DetailItemID = txtItemID.Text;
                }

                entity.ItemName = userControl.ProfileItemName;

                var displaySequence = 1;
                var ilp = new BusinessObject.ItemLaboratoryProfile();
                ilp.Query.es.Top = 1;
                ilp.Query.Select(ilp.Query.DisplaySequence);
                ilp.Query.Where(ilp.Query.ParentItemID == entity.ParentItemID);
                ilp.Query.OrderBy(ilp.Query.DisplaySequence.Descending);
                if (ilp.Query.Load()) displaySequence = (ilp.DisplaySequence ?? 0) + 1;

                entity.DisplaySequence = displaySequence;
                entity.IsDisplayInResult = true;
            }
        }

        #endregion

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
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

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
                entity.BridgingGroupID = userControl.BridgingGroupID;
                entity.BridgingGroupName = userControl.BridgingGroupName;
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
    }
}