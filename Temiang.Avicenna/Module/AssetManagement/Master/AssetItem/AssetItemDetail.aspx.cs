using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetItemDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string getPageID
        {
            get
            {
                return Request.QueryString["fa"];
            }
        }

        private void PopulateNewAssetId()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;
            if (cboAssetGroup.SelectedValue == string.Empty)
                return;

            if (AppSession.Parameter.IsUsingAssetIdNewNumberingFormat)
            {
                var ag = new AssetGroup();
                if (ag.LoadByPrimaryKey(cboAssetGroup.SelectedValue))
                {
                    var initial = string.Empty;
                    if (getPageID == "2")
                        initial = chkIsFixedAsset.Checked ? "A." : "I.";
                    else
                        initial = (getPageID == "0" ? "I." : "A.");

                    var type = AppSession.Parameter.IsUsingAssetIdNumberingFormatWithSplitCategory ? initial : string.Empty;
                    var group = !string.IsNullOrEmpty(ag.Initial) ? ag.Initial : ag.AssetGroupId;
                    var subgroup = string.Empty;

                    if (!string.IsNullOrEmpty(cboAssetSubGroupId.SelectedValue))
                    {
                        var asg = new AssetSubGroup();
                        if (asg.LoadByPrimaryKey(cboAssetGroup.SelectedValue, cboAssetSubGroupId.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(asg.Initial))
                                subgroup = asg.Initial;
                        }
                    }

                    var department = type + group + subgroup;
                    _autoNumber = Helper.GetNewAutoNumber(txtPurchaseDate.SelectedDate.Value.Date,
                                                       AppEnum.AutoNumber.AssetID, department, AppSession.UserLogin.UserID);

                }
            }
            else
            {
                var ag = new AssetGroup();
                if (ag.LoadByPrimaryKey(cboAssetGroup.SelectedValue))
                {
                    var initial = string.Empty;
                    if (getPageID == "2")
                        initial = chkIsFixedAsset.Checked ? "A" : "I";
                    else
                        initial = (getPageID == "0" ? "I" : "A");

                    var type = initial;
                    var department = string.Empty;

                    if (!string.IsNullOrEmpty(cboAssetSubGroupId.SelectedValue))
                    {
                        var asg = new AssetSubGroup();
                        if (asg.LoadByPrimaryKey(cboAssetGroup.SelectedValue, cboAssetSubGroupId.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(asg.Initial))
                                department = asg.Initial;
                        }
                    }

                    _autoNumber = Helper.GetNewAssetId(txtPurchaseDate.SelectedDate.Value.Date,
                                                       AppEnum.AutoNumber.AssetID,
                                                       !string.IsNullOrEmpty(ag.Initial) ? ag.Initial : ag.AssetGroupId,
                                                       department, type, AppSession.UserLogin.UserID);
                }
            }
            txtAssetId.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboAssetType, AppEnum.StandardReference.AssetType);
                StandardReference.InitializeIncludeSpace(cboManufacture, AppEnum.StandardReference.Manufacturer);
                StandardReference.InitializeIncludeSpace(cboInsurance, AppEnum.StandardReference.AssetInsurance);

                //StandardReference.InitializeIncludeSpace(cboSRAssetsStatus, AppEnum.StandardReference.AssetsStatus);
                // AssetsStatus, dimunculkan semua listnya, include yg non aktif
                var collAssetsStatus = new AppStandardReferenceItemCollection();
                collAssetsStatus.Query.Where(
                    collAssetsStatus.Query.StandardReferenceID == AppEnum.StandardReference.AssetsStatus.ToString()
                );
                collAssetsStatus.Query.OrderBy(collAssetsStatus.Query.ItemID.Ascending);
                collAssetsStatus.LoadAll();
                cboSRAssetsStatus.Items.Clear();
                cboSRAssetsStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in collAssetsStatus)
                {
                    cboSRAssetsStatus.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }

                StandardReference.InitializeIncludeSpace(cboSRAssetsCriticality, AppEnum.StandardReference.AssetsCriticality);
                StandardReference.InitializeIncludeSpace(cboSRAssetsWarrantyContract, AppEnum.StandardReference.AssetsWarrantyContract);
                StandardReference.InitializeIncludeSpace(cboSRAssetUsageTimeEstimation, AppEnum.StandardReference.AssetUsageTimeEstimation);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            switch (getPageID)
            {
                case "0":
                    ProgramID = AppConstant.Program.ASSET_ITEM_NONFIXEDASSETS;
                    UrlPageSearch = "AssetItemNonFixedSearch.aspx?fa=0";
                    break;
                case "1":
                    ProgramID = AppConstant.Program.ASSET_ITEM;
                    UrlPageSearch = "AssetItemSearch.aspx?fa=1";
                    break;
                case "2":
                    ProgramID = AppConstant.Program.ASSET_ITEM;
                    UrlPageSearch = "AssetItemSearch.aspx?fa=2";
                    break;
                case "d":
                    ProgramID = AppConstant.Program.ASSET_DEPRECIATION;
                    UrlPageSearch = "AssetItemSearch.aspx?fa=d";
                    break;
            }

            UrlPageList = "AssetItemList.aspx?fa=" + getPageID;

            WindowSearch.Height = 400;

            Helper.SetupGrid(grdMovementHistory);
            Helper.SetupGrid(grdMaintenanceHistory);
            Helper.SetupGrid(grdAssetDepreciation);

            //grid
            grdMovementHistory.NeedDataSource += (delegate { GenerateGridMovementHistory(); });
            grdMaintenanceHistory.NeedDataSource += (delegate { GenerateGridMaintenanceHistory(); });
            grdAssetDepreciation.NeedDataSource += grdAssetDepreciation_NeedDataSource;
            grdAssetDepreciation.ItemCommand += grdAssetDepreciation_OnItemCommand;

            if (!IsPostBack)
            {
                if (getPageID == "0")
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.Tabs[1].Visible = false;
                    tabStrip.Tabs[2].Selected = true;
                    multiPage.SelectedIndex = 2;
                }
                chkIsFixedAsset.Visible = (getPageID == "2");
                rfvSRAssetUsageTimeEstimation.Visible = getPageID == "d" || (getPageID != "0" && !AppSession.Parameter.IsAssetDepreciationCreateByAccounting);
            }
        }

        //protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        //{
        //    ajax.AddAjaxSetting(cboSRAssetUsageTimeEstimation, cboSRAssetUsageTimeEstimation);
        //    ajax.AddAjaxSetting(cboSRAssetUsageTimeEstimation, txtUsageTimeEstimation);
        //}

        void grdAssetDepreciation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            GenerateGridAssetDepreciation();
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Asset());

            txtAssetId.ReadOnly = AppSession.Parameter.IsCreateAssetIdAutomatic;

            cboAssetGroup.Text = string.Empty;
            cboAssetGroup.Items.Clear();

            cboAssetSubGroupId.Text = string.Empty;
            cboAssetSubGroupId.ClearSelection();

            cboItemID.Text = string.Empty;
            cboItemID.Items.Clear();

            cboServiceUnit.Text = string.Empty;
            cboServiceUnit.Items.Clear();

            cboLocation.Text = string.Empty;
            cboLocation.ClearSelection();

            cboInsurance.Text = string.Empty;
            txtGuaranteeExpiredDate.SelectedDate = DateTime.Now;
            txtPurchaseDate.SelectedDate = DateTime.Now;
            txtStartUsingDate.SelectedDate = DateTime.Now;
            chkIsActive.Checked = true;

            cboMaintenanceServiceUnitID.Text = string.Empty;
            cboMaintenanceServiceUnitID.Items.Clear();

            cboAssetType.SelectedValue = string.Empty;
            cboAssetType.Text = string.Empty;

            cboSRAssetsStatus.SelectedValue = AppSession.Parameter.AssetsStatusActive;

            cboSRAssetsCriticality.SelectedValue = string.Empty;
            cboSRAssetsCriticality.Text = string.Empty;

            rbtMaintenanceIntervalIn.SelectedValue = "d";

            grdAssetDepreciation.Rebind();
            grdMaintenanceHistory.Rebind();
            grdMovementHistory.Rebind();

            cboSRAssetUsageTimeEstimation.SelectedValue = string.Empty;
            cboSRAssetUsageTimeEstimation.Text = string.Empty;

            cboSupplierID.Items.Clear();
            cboSupplierID.SelectedValue = string.Empty;
            cboSupplierID.Text = string.Empty;

            if ((getPageID == "1" || getPageID == "2") && AppSession.Parameter.IsAssetDepreciationCreateByAccounting)
            {
                txtStartDepreciationDate.Enabled = false;
                txtUsageTimeEstimation.ReadOnly = true;
                cboSRAssetUsageTimeEstimation.Enabled = false;
                txtResidualValue.ReadOnly = true;
                txtCurrentPrice.ReadOnly = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (!AppSession.Parameter.IsAllowEditAssetGroup)
            {
                cboAssetGroup.Enabled = false;
                cboAssetSubGroupId.Enabled = false;
            }
            else
            {
                if (chkIsFixedAsset.Checked)
                {
                    var depre = new AssetDepreciationPostCollection();
                    depre.Query.Where(depre.Query.AssetID == txtAssetId.Text, depre.Query.PostingId.IsNotNull());
                    depre.LoadAll();
                    if (depre.Count > 0)
                    {
                        cboAssetGroup.Enabled = false;
                        cboAssetSubGroupId.Enabled = false;
                    }
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var movements = new AssetMovementCollection();
            movements.Query.Where(movements.Query.AssetID == txtAssetId.Text);
            movements.LoadAll();
            if (movements.Count > 0)
            {
                args.MessageText = "The asset has been moved. Data can not be deleted.";
                args.IsCancel = true;
                return;
            }

            var maintenances = new AssetWorkOrderCollection();
            maintenances.Query.Where(maintenances.Query.AssetID == txtAssetId.Text, maintenances.Query.IsVoid == false);
            maintenances.LoadAll();
            if (maintenances.Count > 0)
            {
                args.MessageText = "The asset has been maintained. Data can not be deleted.";
                args.IsCancel = true;
                return;
            }

            if (getPageID != "0")
            {
                var d = new AssetDepreciationPostCollection();
                d.Query.Where(d.Query.AssetID == txtAssetId.Text);
                d.LoadAll();

                if (d.Count > 0)
                {
                    args.MessageText = "Asset depreciation has been made. Data can not be deleted.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new Asset();
            entity.LoadByPrimaryKey(txtAssetId.Text);
            entity.MarkAsDeleted();

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (((getPageID == "1" || getPageID == "2") && !AppSession.Parameter.IsAssetDepreciationCreateByAccounting) || getPageID == "d")
            {
                if (!txtStartDepreciationDate.IsEmpty)
                {
                    if (txtUsageTimeEstimation.Text == string.Empty || txtUsageTimeEstimation.Value == 0)
                    {
                        args.MessageText = "Usage Time Estimation should not be equal to zero or empty.";
                        args.IsCancel = true;
                        return;
                    }
                    if (txtCurrentPrice.Text == string.Empty || txtCurrentPrice.Value == 0)
                    {
                        args.MessageText = "Current Price should not be equal to zero or empty.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            if (getPageID == "2")
            {
                if (chkIsFixedAsset.Checked)
                {
                    if (Convert.ToDecimal(txtPurchasePrice.Value) < AppSession.Parameter.acc_AssetDepreciationAmountLimit)
                    {
                        args.MessageText = string.Format("Asset master should not be marked With Depreciation (Purchase Price less then Asset Depreciation Amount Limit - {0:N2}).", AppSession.Parameter.acc_AssetDepreciationAmountLimit);
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtPurchasePrice.Value) >= AppSession.Parameter.acc_AssetDepreciationAmountLimit)
                    {
                        args.MessageText = string.Format("Asset master should be marked With Depreciation (Purchase Price greater then Asset Depreciation Amount Limit - {0:N2}).", AppSession.Parameter.acc_AssetDepreciationAmountLimit);
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            
            if (AppSession.Parameter.IsCreateAssetIdAutomatic)
            {
                PopulateNewAssetId();
                _autoNumber.Save();
            }
            else if (string.IsNullOrEmpty(txtAssetId.Text))
            {
                args.MessageText = "Asset ID required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Asset();
            if (entity.LoadByPrimaryKey(txtAssetId.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Asset();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (((getPageID == "1" || getPageID == "2") && !AppSession.Parameter.IsAssetDepreciationCreateByAccounting) || getPageID == "d")
            {
                if (!txtStartDepreciationDate.IsEmpty)
                {
                    if (txtUsageTimeEstimation.Text == string.Empty || txtUsageTimeEstimation.Value == 0)
                    {
                        args.MessageText = "Usage Time Estimation should not be equal to zero or empty.";
                        args.IsCancel = true;
                        return;
                    }
                    if (txtCurrentPrice.Text == string.Empty || txtCurrentPrice.Value == 0)
                    {
                        args.MessageText = "Current Price should not be equal to zero or empty.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var entity = new Asset();
            if (entity.LoadByPrimaryKey(txtAssetId.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("AssetId='{0}'", txtAssetId.Text.Trim());
            auditLogFilter.TableName = "Asset";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_AssetID", txtAssetId.Text);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Asset entity)
        {
            if (getPageID != "d")
            {
                entity.AssetName = txtAssetName.Text;
                entity.ItemID = cboItemID.SelectedValue;
                entity.SerialNumber = txtSerialNumber.Text;
                entity.AssetGroupID = cboAssetGroup.SelectedValue;
                entity.AssetSubGroupId = cboAssetSubGroupId.SelectedValue;
                entity.SRAssetType = cboAssetType.SelectedValue;
                entity.ServiceUnitID = cboServiceUnit.SelectedValue;
                entity.AssetLocationID = cboLocation.SelectedValue;
                entity.PurchaseOrderNumber = txtPONumber.Text;
                entity.SRManufacturer = cboManufacture.SelectedValue;

                entity.PurchasedPrice = Convert.ToDecimal(txtPurchasePrice.Value);
                entity.PurchasedDate = txtPurchaseDate.SelectedDate;
                if (txtStartUsingDate.IsEmpty)
                    entity.str.StartUsingDate = string.Empty;
                else
                    entity.StartUsingDate = txtStartUsingDate.SelectedDate;

                entity.GuaranteeExpiredDate = txtGuaranteeExpiredDate.SelectedDate;
                entity.InsuranceID = cboInsurance.SelectedValue;
                entity.InsurancePolicyNo = txtInsurancePolisNo.Text;
                entity.InsuranceAmount = Convert.ToDecimal(txtInsuranceAmount.Value);
                entity.Notes = txtDescription.Text;
                entity.BrandName = txtBrandName.Text;
                entity.IsActive = chkIsActive.Checked;

                if (txtLastMaintenanceDate.IsEmpty)
                    entity.str.LastMaintenanceDate = string.Empty;
                else
                    entity.LastMaintenanceDate = txtLastMaintenanceDate.SelectedDate;
                entity.str.NextMaintenanceDate = string.Empty;
                entity.MaintenanceInterval = Convert.ToByte(txtMaintenanceInterval.Value);
                entity.MaintenanceIntervalIn = rbtMaintenanceIntervalIn.SelectedValue;

                //if (!txtLastMaintenanceDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                //{
                //    entity.LastMaintenanceDate = txtLastMaintenanceDate.SelectedDate;
                //    entity.MaintenanceInterval = Convert.ToByte(txtMaintenanceInterval.Value);
                //    entity.MaintenanceIntervalIn = rbtMaintenanceIntervalIn.SelectedValue;
                //    switch (entity.MaintenanceIntervalIn)
                //    {
                //        case "d":
                //            entity.NextMaintenanceDate = txtLastMaintenanceDate.SelectedDate.Value.AddDays(Convert.ToInt32(txtMaintenanceInterval.Value));
                //            break;
                //        case "m":
                //            entity.NextMaintenanceDate = txtLastMaintenanceDate.SelectedDate.Value.AddMonths(Convert.ToInt32(txtMaintenanceInterval.Value));
                //            break;
                //        case "y":
                //            entity.NextMaintenanceDate = txtLastMaintenanceDate.SelectedDate.Value.AddYears(Convert.ToInt32(txtMaintenanceInterval.Value));
                //            break;
                //    }
                //}
                //else
                //{
                //    entity.str.LastMaintenanceDate = string.Empty;
                //    entity.str.NextMaintenanceDate = string.Empty;
                //    entity.MaintenanceInterval = 0;
                //    entity.MaintenanceIntervalIn = "d";
                //}
                entity.IsContinuousMaintenanceSchedule = chkIsContinuousMaintenanceSchedule.Checked;
                entity.MaintenanceServiceUnitID = cboMaintenanceServiceUnitID.SelectedValue;
                //-------------
                entity.SRAssetsStatus = cboSRAssetsStatus.SelectedValue;
                entity.SRAssetsCriticality = cboSRAssetsCriticality.SelectedValue;
                entity.NotesToTechnician = txtNotesToTechnician.Text;
                entity.SupplierID = cboSupplierID.SelectedValue;
                entity.SRAssetsWarrantyContract = cboSRAssetsWarrantyContract.SelectedValue;
                entity.WarrantyContractNotes = txtWarrantyContractNotes.Text;

                //-batas depresiasi
                if (txtStartDepreciationDate.IsEmpty)
                    entity.str.StartDepreciationDate = "1/1/1900";
                else
                    entity.StartDepreciationDate = txtStartDepreciationDate.SelectedDate;
                entity.UsageTimeEstimation = Convert.ToInt16(txtUsageTimeEstimation.Value);
                entity.SRAssetUsageTimeEstimation = cboSRAssetUsageTimeEstimation.SelectedValue;
                entity.AgeOfDepreciation = entity.UsageTimeEstimation;
                entity.CurrentValue = Convert.ToDecimal(txtCurrentPrice.Value);
                entity.ResidualValue = Convert.ToDecimal(txtResidualValue.Value);
                //-batas depresiasi

                if (entity.es.IsAdded)
                {
                    entity.IsFixedAsset = getPageID == "2" ? chkIsFixedAsset.Checked : (getPageID != "0");
                }
            }
            else if (getPageID == "d")
            {
                //-batas depresiasi
                if (txtStartDepreciationDate.IsEmpty)
                    entity.str.StartDepreciationDate = "1/1/1900";
                else
                    entity.StartDepreciationDate = txtStartDepreciationDate.SelectedDate;
                entity.UsageTimeEstimation = Convert.ToInt16(txtUsageTimeEstimation.Value);
                entity.SRAssetUsageTimeEstimation = cboSRAssetUsageTimeEstimation.SelectedValue;
                entity.AgeOfDepreciation = entity.UsageTimeEstimation;
                entity.CurrentValue = Convert.ToDecimal(txtCurrentPrice.Value);
                entity.ResidualValue = Convert.ToDecimal(txtResidualValue.Value);
                //-batas depresiasi
            }

            //not used
            if (entity.es.IsAdded)
            {
                entity.DepartmentID = string.Empty;
                entity.DepreciationMethodID = "DM001";
                entity.ItemUnit = "Unit";
                entity.SalesPrice = decimal.Zero;
                entity.CurrentCondition = decimal.Zero;
                entity.LastInventoriedDate = new DateTime(1900, 1, 1);
                entity.LastInventoriedBy = string.Empty;
                entity.IssuedDate = new DateTime(1900, 1, 1);
                entity.CurrentUsageTimeEstimation = 0;
                entity.IssuedBy = string.Empty;
                entity.SRIssuedReason = string.Empty;
                entity.IntervalUnit = string.Empty;
                entity.ReferenceNo = string.Empty;
                entity.IsAudiometer = false;
                entity.IsSpirometer = false;
            }

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.AssetID = txtAssetId.Text;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Asset entity)
        {
            bool generateDepreciation = false;
            if (entity.IsFixedAsset == true)
            {
                if (((getPageID == "1" || getPageID == "2") && !AppSession.Parameter.IsAssetDepreciationCreateByAccounting) || getPageID == "d")
                {
                    var d = new AssetDepreciationPostCollection();
                    d.Query.Where(d.Query.AssetID == txtAssetId.Text);
                    d.LoadAll();

                    if (d.Count == 0)
                    {
                        generateDepreciation = true;
                    }
                    else
                    {
                        var e = Asset.Get(txtAssetId.Text);
                        if (e.UsageTimeEstimation != entity.UsageTimeEstimation ||
                            e.CurrentValue != entity.CurrentValue ||
                            e.ResidualValue != entity.ResidualValue ||
                            e.StartDepreciationDate != entity.StartDepreciationDate)
                        {
                            generateDepreciation = true;
                        }
                    }
                }

            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed

                if (generateDepreciation)
                {
                    var result = AssetDepreciationPost.GenerateAssetDepreciation(txtAssetId.Text, AppSession.UserLogin.UserID);
                    //if (result == 0)
                    //{
                    //}
                    // result = 0 sukses
                    // result = -1 asset not found
                    // result = -2 already processed
                    // result = -3 invalid usage est
                    // result = 500 unknown error
                }

                trans.Complete();
            }

            grdAssetDepreciation.Rebind();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                if (getPageID == "0")
                    que.Where(que.IsFixedAsset == false, que.AssetID > txtAssetId.Text);
                else if (getPageID == "2")
                    que.Where(que.AssetID > txtAssetId.Text);
                else
                    que.Where(que.IsFixedAsset == true, que.AssetID > txtAssetId.Text);

                que.OrderBy(que.AssetID.Ascending);
            }
            else
            {
                if (getPageID == "0")
                    que.Where(que.IsFixedAsset == false, que.AssetID < txtAssetId.Text);
                else if (getPageID == "2")
                    que.Where(que.AssetID < txtAssetId.Text);
                else
                    que.Where(que.IsFixedAsset == true, que.AssetID < txtAssetId.Text);

                que.OrderBy(que.AssetID.Descending);
            }

            var entity = new Asset();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //RefreshCommandItemGrid(grdAssetDepreciation, oldVal, newVal);

            txtAssetId.ReadOnly = !((newVal == AppEnum.DataMode.New) && !AppSession.Parameter.IsCreateAssetIdAutomatic);
            chkIsFixedAsset.Enabled = (newVal == AppEnum.DataMode.New) && (getPageID != "d");

            if (AppSession.Parameter.IsAssetDepreciationCreateByAccounting)
            {
                if (getPageID == "1" || getPageID == "2")
                {
                    txtStartDepreciationDate.Enabled = false;
                    txtUsageTimeEstimation.ReadOnly = true;
                    cboSRAssetUsageTimeEstimation.Enabled = false;
                    txtResidualValue.ReadOnly = true;
                    txtCurrentPrice.ReadOnly = true;
                }
            }
            
            if (getPageID == "d")
            {
                txtAssetName.ReadOnly = true;
                cboItemID.Enabled = false;
                cboServiceUnit.Enabled = false;
                cboLocation.Enabled = false;
                cboAssetGroup.Enabled = false;
                cboAssetSubGroupId.Enabled = false;
                cboAssetType.Enabled = false;
                cboSRAssetsStatus.Enabled = false;
                cboSRAssetsCriticality.Enabled = false;
                txtNotesToTechnician.ReadOnly = true;
                txtAssetIdCopy.ReadOnly = true;
                txtAssetIdCopy.ShowButton = false;
                cboInsurance.Enabled = false;
                txtInsurancePolisNo.ReadOnly = true;
                txtInsuranceAmount.ReadOnly = true;
                cboSRAssetsWarrantyContract.Enabled = false;
                txtGuaranteeExpiredDate.Enabled = false;
                txtWarrantyContractNotes.ReadOnly = true;
                txtDescription.ReadOnly = true;
                cboManufacture.Enabled = false;
                txtBrandName.ReadOnly = true;
                txtSerialNumber.ReadOnly = true;
                cboMaintenanceServiceUnitID.Enabled = false;
                txtMaintenanceInterval.ReadOnly = true;
                rbtMaintenanceIntervalIn.Enabled = false;
                txtLastMaintenanceDate.Enabled = false;
                txtPONumber.ReadOnly = true;
                txtPONumber.ShowButton = false;
                cboSupplierID.Enabled = false;
                txtPurchasePrice.ReadOnly = true;
                txtPurchaseDate.Enabled = false;
                txtStartUsingDate.Enabled = false;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Asset();
            if (parameters.Length > 0)
            {
                var id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtAssetId.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var e = (Asset)entity;
            txtAssetId.Text = e.AssetID;
            txtAssetName.Text = e.AssetName;
            txtSerialNumber.Text = e.SerialNumber;

            if (!string.IsNullOrEmpty(e.AssetGroupID))
            {
                var assetGroup = new AssetGroupQuery();
                assetGroup.Where(assetGroup.AssetGroupId == e.AssetGroupID);
                cboAssetGroup.DataSource = assetGroup.LoadDataTable();
                cboAssetGroup.DataBind();
                cboAssetGroup.SelectedValue = e.AssetGroupID;

                cboAssetSubGroupId.Items.Clear();
                cboAssetSubGroupId.Text = string.Empty;
                PopulateSubGroupList(cboAssetGroup.SelectedValue, false);
                cboAssetSubGroupId.SelectedValue = e.AssetSubGroupId;
            }

            if (!string.IsNullOrEmpty(e.ItemID))
            {
                var item = new ItemQuery();
                item.Where(item.ItemID == e.ItemID);
                cboItemID.DataSource = item.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = e.ItemID;
            }

            if (!string.IsNullOrEmpty(e.ServiceUnitID))
            {
                var serviceUnit = new ServiceUnitQuery();
                serviceUnit.Where(serviceUnit.ServiceUnitID == e.ServiceUnitID);
                cboServiceUnit.DataSource = serviceUnit.LoadDataTable();
                cboServiceUnit.DataBind();
                cboServiceUnit.SelectedValue = e.ServiceUnitID;

                cboLocation.Items.Clear();
                cboLocation.Text = string.Empty;
                PopulateRoomList(cboServiceUnit.SelectedValue, false);
                cboLocation.SelectedValue = e.AssetLocationID;
            }

            cboAssetType.SelectedValue = e.SRAssetType;
            txtPONumber.Text = e.PurchaseOrderNumber;
            cboManufacture.SelectedValue = e.SRManufacturer;
            if (e.IsFixedAsset == true && e.UsageTimeEstimation != 0)
                txtStartDepreciationDate.SelectedDate = e.StartDepreciationDate;
            else
                txtStartDepreciationDate.SelectedDate = null;
            txtUsageTimeEstimation.Value = e.UsageTimeEstimation;
            cboSRAssetUsageTimeEstimation.SelectedValue = e.SRAssetUsageTimeEstimation;
            txtPurchasePrice.Value = (double?)e.PurchasedPrice;
            txtCurrentPrice.Value = (double?)e.CurrentValue;
            txtResidualValue.Value = (double?)e.ResidualValue;
            txtPurchaseDate.SelectedDate = e.PurchasedDate;
            txtStartUsingDate.SelectedDate = e.StartUsingDate ?? null;
            if (e.SRAssetsStatus == AppSession.Parameter.AssetsStatusDisposed)
                txtDateDisposed.SelectedDate = e.DateDisposed ?? null;
            else txtDateDisposed.SelectedDate = null;
            txtGuaranteeExpiredDate.SelectedDate = e.GuaranteeExpiredDate;
            cboInsurance.SelectedValue = e.InsuranceID;
            txtInsurancePolisNo.Text = e.InsurancePolicyNo;
            txtInsuranceAmount.Value = (double?)e.InsuranceAmount;
            txtDescription.Text = e.Notes;
            chkIsActive.Checked = e.IsActive ?? false;
            txtBrandName.Text = e.BrandName;
            txtLastMaintenanceDate.SelectedDate = e.LastMaintenanceDate;
            txtNextMaintenanceDate.SelectedDate = e.NextMaintenanceDate;
            txtMaintenanceInterval.Value = e.MaintenanceInterval;
            rbtMaintenanceIntervalIn.SelectedValue = e.MaintenanceIntervalIn;
            chkIsContinuousMaintenanceSchedule.Checked = e.IsContinuousMaintenanceSchedule ?? false;
            if (!string.IsNullOrEmpty(e.MaintenanceServiceUnitID))
            {
                var serviceUnit = new ServiceUnitQuery();
                serviceUnit.Where(serviceUnit.ServiceUnitID == e.MaintenanceServiceUnitID);
                cboMaintenanceServiceUnitID.DataSource = serviceUnit.LoadDataTable();
                cboMaintenanceServiceUnitID.DataBind();
                cboMaintenanceServiceUnitID.SelectedValue = e.MaintenanceServiceUnitID;
            }

            //-------------
            cboSRAssetsStatus.SelectedValue = e.SRAssetsStatus;
            cboSRAssetsCriticality.SelectedValue = e.SRAssetsCriticality;
            txtNotesToTechnician.Text = e.NotesToTechnician;

            if (!string.IsNullOrEmpty(e.SupplierID))
            {
                var supp = new SupplierQuery();
                supp.Where(supp.SupplierID == e.SupplierID);
                cboSupplierID.DataSource = supp.LoadDataTable();
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = e.SupplierID;
            }

            cboSRAssetsWarrantyContract.SelectedValue = e.SRAssetsWarrantyContract;
            txtWarrantyContractNotes.Text = e.WarrantyContractNotes;
            chkIsFixedAsset.Checked = e.IsFixedAsset ?? false;

            grdAssetDepreciation.Rebind();
            grdMaintenanceHistory.Rebind();
            grdMovementHistory.Rebind();
        }

        #endregion

        #region Grid
        private void GenerateGridMaintenanceHistory()
        {
            grdMaintenanceHistory.VirtualItemCount = AssetWorkOrder.TotalCountByAssetId(txtAssetId.Text);
            var en = AssetWorkOrder.GetAllWithPagingByAssetId(txtAssetId.Text, grdMaintenanceHistory.CurrentPageIndex, grdMaintenanceHistory.PageSize);
            grdMaintenanceHistory.DataSource = en.Select(entity => new MaintenanceHistoryDataSource(entity));
        }

        private void GenerateGridMovementHistory()
        {
            grdMovementHistory.VirtualItemCount = AssetMovement.TotalCountByAssetId(txtAssetId.Text);
            var en = AssetMovement.GetAllWithPagingByAssetId(txtAssetId.Text, grdMovementHistory.CurrentPageIndex, grdMovementHistory.PageSize);
            grdMovementHistory.DataSource = en.Select(entity => new MovementHistoryDataSource(entity));
        }

        private void GenerateGridAssetDepreciation()
        {
            grdAssetDepreciation.VirtualItemCount = AssetDepreciationPost.TotalCount(txtAssetId.Text);
            var en = AssetDepreciationPost.GetAllWithPaging(txtAssetId.Text, grdAssetDepreciation.CurrentPageIndex, grdAssetDepreciation.PageSize);
            grdAssetDepreciation.DataSource = en.Select(entity => new AssetDepreciationPostDataSource(entity));
        }

        private void grdAssetDepreciation_OnItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PerformInsert":
                    Validate();
                    if (!IsValid)
                    {
                        e.Canceled = true;
                        return;
                    }

                    var result = AssetDepreciationPost.GenerateAssetDepreciation(txtAssetId.Text, AppSession.UserLogin.UserID);
                    if (result != 0)
                    {
                        e.Canceled = true;
                        return;
                    }

                    e.Canceled = false;
                    grdAssetDepreciation.CurrentPageIndex = 0;

                    var entity = new Asset();
                    entity.LoadByPrimaryKey(txtAssetId.Text);
                    OnPopulateEntryControl(entity);

                    GenerateGridAssetDepreciation();
                    break;
            }
        }
        #endregion

        #region Combobox

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var ipnm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(ipnm).On(query.ItemID == ipnm.ItemID);
            query.es.Top = 20;
            query.Where
                (query.Or(query.ItemName.Like(searchText),
                          query.ItemID.Like(searchText)),
                 query.IsActive == true);
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemID"] + " - " +
                          ((DataRowView)e.Item.DataItem)["ItemName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboServiceUnit.DataSource = query.LoadDataTable();
            cboServiceUnit.DataBind();
        }
        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        protected void cboServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboLocation.Items.Clear();
            cboLocation.Text = string.Empty;
            PopulateRoomList(cboServiceUnit.SelectedValue, true);
        }

        protected void cboAssetGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AssetGroupQuery();
            query.Select(query.AssetGroupId, query.GroupName);
            query.es.Top = 20;
            query.Where
                (
                    query.GroupName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.AssetGroupId.Ascending);

            cboAssetGroup.DataSource = query.LoadDataTable();
            cboAssetGroup.DataBind();
        }
        protected void cboAssetGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetGroupId"] + @" - " + ((DataRowView)e.Item.DataItem)["GroupName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetGroupId"].ToString();
        }
        protected void cboAssetGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetSubGroupId.Items.Clear();
            cboAssetSubGroupId.Text = string.Empty;
            PopulateSubGroupList(cboAssetGroup.SelectedValue, true);
        }

        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();
            query.Select(query.SupplierID, query.SupplierName);
            query.es.Top = 20;
            query.Where
                (
                    query.SupplierName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.SupplierName.Ascending);

            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }
        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboPoNumber_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery();
            query.Select(query.SupplierID, query.SupplierName);
            query.es.Top = 20;
            query.Where
                (
                    query.SupplierName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.SupplierName.Ascending);

            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }
        protected void cboPoNumber_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TransactionNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TransactionNo"].ToString();
        }
        protected void txtPONumber_TextChanged(object sender, EventArgs e)
        {
            if (txtPONumber.Text == string.Empty)
            {
                txtAssetName.Text = string.Empty;

                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
                cboItemID.Enabled = true;

                cboServiceUnit.Items.Clear();
                cboServiceUnit.Text = string.Empty;

                cboSupplierID.Items.Clear();
                cboSupplierID.Text = string.Empty;

                txtPurchasePrice.Value = 0;
                chkIsFixedAsset.Checked = false;

                return;
            }
            var val = txtPONumber.Text.Split('|');
            txtPONumber.Text = val[0];

            var it = new ItemTransaction();
            it.LoadByPrimaryKey(txtPONumber.Text);

            var query = new SupplierQuery();
            query.Where(query.SupplierID == it.BusinessPartnerID);
            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
            cboSupplierID.SelectedValue = it.BusinessPartnerID;

            var itiq = new ItemTransactionItemQuery();
            itiq.Where(itiq.TransactionNo == val[0].ToString(), itiq.ItemID == val[1].ToString());
            itiq.es.Top = 1;

            var iti = new ItemTransactionItem();
            iti.Load(itiq);
            //iti.LoadByPrimaryKey(val[0], val[1]);
            //txtPurchasePrice.Value = (it.IsTaxable == 1)
            //                             ? (Convert.ToDouble(iti.PriceInCurrency) -
            //                               Convert.ToDouble(iti.DiscountInCurrency)) * Convert.ToDouble(1 + ((it.TaxPercentage ?? 0) /100))
            //                             : Convert.ToDouble(iti.PriceInCurrency) -
            //                               Convert.ToDouble(iti.DiscountInCurrency);
            txtPurchasePrice.Value = (it.IsTaxable == 2)
                                         ? (Convert.ToDouble(iti.PriceInCurrency) - Convert.ToDouble(iti.DiscountInCurrency))
                                         : (Convert.ToDouble(iti.PriceInCurrency) - Convert.ToDouble(iti.DiscountInCurrency)) * Convert.ToDouble(1 + ((it.TaxPercentage ?? 0) / 100));
            if (chkIsFixedAsset.Visible == true && chkIsFixedAsset.Enabled == true)
                chkIsFixedAsset.Checked = Convert.ToDecimal(txtPurchasePrice.Value) >= AppSession.Parameter.acc_AssetDepreciationAmountLimit;
            txtPurchaseDate.SelectedDate = it.TransactionDate;
            var itm = new ItemQuery();
            itm.Where(itm.ItemID == iti.ItemID);
            cboItemID.DataSource = itm.LoadDataTable();
            cboItemID.DataBind();
            cboItemID.SelectedValue = iti.ItemID;

            cboItemID.Enabled = false;

            var i = new Item();
            if (i.LoadByPrimaryKey(iti.ItemID))
                txtAssetName.Text = i.ItemName;

            if (!string.IsNullOrEmpty(it.ServiceUnitCostID))
            {
                var serviceUnit = new ServiceUnitQuery();
                serviceUnit.Where(serviceUnit.ServiceUnitID == it.ServiceUnitCostID);
                cboServiceUnit.DataSource = serviceUnit.LoadDataTable();
                cboServiceUnit.DataBind();
                cboServiceUnit.SelectedValue = it.ServiceUnitCostID;
            }
            else
            {
                var po = new ItemTransaction();
                if (po.LoadByPrimaryKey(it.ReferenceNo) & !string.IsNullOrEmpty(po.ServiceUnitCostID))
                {
                    var serviceUnit = new ServiceUnitQuery();
                    serviceUnit.Where(serviceUnit.ServiceUnitID == po.ServiceUnitCostID);
                    cboServiceUnit.DataSource = serviceUnit.LoadDataTable();
                    cboServiceUnit.DataBind();
                    cboServiceUnit.SelectedValue = po.ServiceUnitCostID;
                }
                else
                {
                    cboServiceUnit.Items.Clear();
                    cboServiceUnit.Text = string.Empty;
                }
            }
        }

        protected void txtAssetIdCopy_TextChanged(object sender, EventArgs e)
        {
            if (txtAssetIdCopy.Text != string.Empty)
            {
                var a = new Asset();
                if (a.LoadByPrimaryKey(txtAssetIdCopy.Text))
                {
                    txtAssetName.Text = a.AssetName;
                    txtSerialNumber.Text = a.SerialNumber;

                    if (!string.IsNullOrEmpty(a.AssetGroupID))
                    {
                        var assetGroup = new AssetGroupQuery();
                        assetGroup.Where(assetGroup.AssetGroupId == a.AssetGroupID);
                        cboAssetGroup.DataSource = assetGroup.LoadDataTable();
                        cboAssetGroup.DataBind();
                        cboAssetGroup.SelectedValue = a.AssetGroupID;

                        cboAssetSubGroupId.Items.Clear();
                        cboAssetSubGroupId.Text = string.Empty;
                        PopulateSubGroupList(cboAssetGroup.SelectedValue, false);
                        cboAssetSubGroupId.SelectedValue = a.AssetSubGroupId;
                    }

                    if (!string.IsNullOrEmpty(a.ItemID))
                    {
                        var itm = new ItemQuery();
                        itm.Where(itm.ItemID == a.ItemID);
                        cboItemID.DataSource = itm.LoadDataTable();
                        cboItemID.DataBind();
                        cboItemID.SelectedValue = a.ItemID;
                    }

                    cboAssetType.SelectedValue = a.SRAssetType;
                    txtPONumber.Text = a.PurchaseOrderNumber;
                    cboManufacture.SelectedValue = a.SRManufacturer;
                    txtUsageTimeEstimation.Value = a.UsageTimeEstimation;
                    cboSRAssetUsageTimeEstimation.SelectedValue = a.SRAssetUsageTimeEstimation;
                    txtPurchasePrice.Value = (double?)a.PurchasedPrice;
                    chkIsFixedAsset.Checked = a.IsFixedAsset ?? false;
                    txtCurrentPrice.Value = (double?)a.CurrentValue;
                    txtResidualValue.Value = (double?)a.ResidualValue;
                    txtPurchaseDate.SelectedDate = a.PurchasedDate;
                    txtStartUsingDate.SelectedDate = a.StartUsingDate;
                    txtGuaranteeExpiredDate.SelectedDate = a.GuaranteeExpiredDate;
                    cboInsurance.SelectedValue = a.InsuranceID;
                    txtInsurancePolisNo.Text = a.InsurancePolicyNo;
                    txtInsuranceAmount.Value = (double?)a.InsuranceAmount;
                    txtDescription.Text = a.Notes;
                    chkIsActive.Checked = a.IsActive ?? false;

                    txtBrandName.Text = a.BrandName;
                    txtMaintenanceInterval.Value = a.MaintenanceInterval;
                    rbtMaintenanceIntervalIn.SelectedValue = a.MaintenanceIntervalIn;
                    if (!string.IsNullOrEmpty(a.MaintenanceServiceUnitID))
                    {
                        var su = new ServiceUnitQuery();
                        su.Where(su.ServiceUnitID == a.MaintenanceServiceUnitID);
                        cboMaintenanceServiceUnitID.DataSource = su.LoadDataTable();
                        cboMaintenanceServiceUnitID.DataBind();
                        cboMaintenanceServiceUnitID.SelectedValue = a.MaintenanceServiceUnitID;
                    }

                    //-------------
                    cboSRAssetsStatus.SelectedValue = a.SRAssetsStatus;
                    cboSRAssetsCriticality.SelectedValue = a.SRAssetsCriticality;
                    txtNotesToTechnician.Text = a.NotesToTechnician;

                    if (!string.IsNullOrEmpty(a.SupplierID))
                    {
                        var supp = new SupplierQuery();
                        supp.Where(supp.SupplierID == a.SupplierID);
                        cboSupplierID.DataSource = supp.LoadDataTable();
                        cboSupplierID.DataBind();
                        cboSupplierID.SelectedValue = a.SupplierID;
                    }

                    cboSRAssetsWarrantyContract.SelectedValue = a.SRAssetsWarrantyContract;
                    txtWarrantyContractNotes.Text = a.WarrantyContractNotes;
                }
            }
        }

        protected void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            if (chkIsFixedAsset.Visible == true && chkIsFixedAsset.Enabled == true)
                chkIsFixedAsset.Checked = Convert.ToDecimal(txtPurchasePrice.Value) >= AppSession.Parameter.acc_AssetDepreciationAmountLimit;
        }

        protected void cboMaintenanceServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var unittc = new ServiceUnitTransactionCodeQuery("b");
            query.InnerJoin(unittc).On(query.ServiceUnitID == unittc.ServiceUnitID &&
                                       unittc.SRTransactionCode == TransactionCode.AssetWorkOrderRealization);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboMaintenanceServiceUnitID.DataSource = query.LoadDataTable();
            cboMaintenanceServiceUnitID.DataBind();
        }
        protected void cboMaintenanceServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboSRAssetUsageTimeEstimation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                //txtUsageTimeEstimation.Value = 0;
                return;
            }

            var ute = new AppStandardReferenceItem();
            if (ute.LoadByPrimaryKey(AppEnum.StandardReference.AssetUsageTimeEstimation.ToString(), e.Value))
            {
                txtUsageTimeEstimation.Value = Convert.ToDouble(ute.NumericValue);
            }
            else
                txtUsageTimeEstimation.Value = 0;
        }
        #endregion

        #region Populate List
        private void PopulateRoomList(string serviceUnitId, bool isNew)
        {
            if (serviceUnitId != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                sr.Query.Where(sr.Query.ServiceUnitID == serviceUnitId);

                if (isNew)
                    sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                cboLocation.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    cboLocation.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }

        private void PopulateSubGroupList(string groupId, bool isNew)
        {
            if (groupId != string.Empty)
            {
                var sg = new AssetSubGroupCollection();
                sg.Query.Where(sg.Query.AssetGroupId == groupId);

                if (isNew)
                    sg.Query.Where(sg.Query.IsActive == true);

                sg.LoadAll();

                cboAssetSubGroupId.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AssetSubGroup entity in sg)
                {
                    cboAssetSubGroupId.Items.Add(new RadComboBoxItem(entity.AssetSubGroupName, entity.AssetSubGroupId));
                }
            }
        }
        #endregion
    }
}
