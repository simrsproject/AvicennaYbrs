using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetStatusChangeDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAssetsStatusFrom, AppEnum.StandardReference.AssetsStatus);

                var exclude = AppSession.Parameter.AssetsStatusSold.Split(',');
                StandardReference.InitializeIncludeSpace(cboSRAssetsStatusTo, AppEnum.StandardReference.AssetsStatus, exclude);

                if (Request.QueryString["seqNo"] == "-1")
                {
                    this.Title = "New Asset Status Change";

                    txtDate.SelectedDate = DateTime.Now;

                    EnabledText(true);
                }
                else
                {
                    this.Title = "View Asset Status Change";

                    var sh = new AssetStatusHistory();
                    sh.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["seqNo"]));
                    txtSeqNo.Text = sh.SeqNo.ToString();

                    var assetq = new AssetQuery("a");
                    var funitq = new ServiceUnitQuery("b");
                    var tunitq = new ServiceUnitQuery("c");
                    assetq.InnerJoin(funitq).On(assetq.ServiceUnitID == funitq.ServiceUnitID);
                    assetq.LeftJoin(tunitq).On(assetq.MaintenanceServiceUnitID == tunitq.ServiceUnitID);
                    assetq.Select(assetq.AssetID, assetq.AssetName, assetq.SerialNumber, funitq.ServiceUnitName,
                                  tunitq.ServiceUnitName.As("MaintenanceServiceUnitName"));
                    assetq.Where(assetq.AssetID == sh.AssetID);
                    cboAssetID.DataSource = assetq.LoadDataTable();
                    cboAssetID.DataBind();
                    cboAssetID.SelectedValue = sh.AssetID;

                    chkIsFixedAssetFrom.Checked = sh.IsFixedAssetFrom ?? false;
                    cboSRAssetsStatusFrom.SelectedValue = sh.SRAssetsStatusFrom;
                    cboSRAssetsStatusTo.SelectedValue = sh.SRAssetsStatusTo;
                    txtDate.SelectedDate = sh.LastUpdateDateTime;
                    txtNotes.Text = sh.Notes;
                    chkIsFixedAssetTo.Checked = sh.IsFixedAssetTo ?? false;
                    txtCurrentValue.Value = Convert.ToDouble(sh.CurrentValue);
                    txtDepreciationAccValue.Value = Convert.ToDouble(sh.DepreciationAccValue);

                    #region Asset Info
                    var asset = new Asset();
                    asset.LoadByPrimaryKey(sh.AssetID);

                    var unitq = new ServiceUnitQuery();
                    unitq.Where(unitq.ServiceUnitID == asset.ServiceUnitID);
                    cboFromServiceUnit.DataSource = unitq.LoadDataTable();
                    cboFromServiceUnit.DataBind();
                    cboFromServiceUnit.SelectedValue = asset.ServiceUnitID;

                    if (string.IsNullOrEmpty(asset.AssetLocationID))
                    {
                        PopulateRoomList(asset.ServiceUnitID, false, cboFromLocation);
                        cboFromLocation.SelectedValue = asset.AssetLocationID;
                    }
                    else
                    {
                        cboFromLocation.Items.Clear();
                        cboFromLocation.Text = string.Empty;
                    }

                    txtBrandName.Text = asset.BrandName;
                    txtSerialNumber.Text = asset.SerialNumber;
                    var g = AssetGroup.Get(asset.AssetGroupID);
                    txtAssetGroup.Text = string.Format("{0} - {1}", asset.AssetGroupID, g.GroupName);
                    txtPurchaseDate2.Text = asset.PurchasedDate.Value.ToString("dd-MMMM-yyyy");
                    #endregion

                    EnabledText(false);

                    Button btnOK = ((Button)Helper.FindControlRecursive(Page, "btnOk"));
                    btnOK.Visible = false;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (Request.QueryString["seqNo"] == "-1")
            {
                if (string.IsNullOrEmpty(cboAssetID.SelectedValue))
                {
                    ShowInformationHeader("Asset is required.");
                    return false;
                }
                if (string.IsNullOrEmpty(cboSRAssetsStatusTo.SelectedValue))
                {
                    ShowInformationHeader("To Status is required.");
                    return false;
                }
                if (cboSRAssetsStatusFrom.SelectedValue == cboSRAssetsStatusTo.SelectedValue && chkIsFixedAssetFrom.Checked == chkIsFixedAssetTo.Checked)
                {
                    ShowInformationHeader("There is no change in asset status.");
                    return false;
                }
                
                if (txtCurrentValue.Enabled && txtCurrentValue.Text == string.Empty)
                {
                    ShowInformationHeader("Current Value is required.");
                    return false;
                }
                if (chkIsFixedAssetFrom.Checked && !chkIsFixedAssetTo.Checked)
                {
                    var adpQ = new AssetDepreciationPostQuery();
                    adpQ.Where(adpQ.AssetID == cboAssetID.SelectedValue, adpQ.IsPosted == true);
                    DataTable dtb = adpQ.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        ShowInformationHeader("Asset depreciation already processed.");
                        return false;
                    }
                }

                bool isAutoJournal = false;
                if (cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusDisposed || cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusLost)
                {
                    //- AssetsStatusInActive & AssetsStatusDamaged --> ada kelanjutan di asset auction
                    if (AppSession.Parameter.acc_IsAutoJournalAssetDestruction)
                    {
                        
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(txtDate.SelectedDate ?? DateTime.Now);
                        if (isClosingPeriod)
                        {
                            ShowInformationHeader("Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", txtDate.SelectedDate ?? DateTime.Now) +
                                               " have been closed. Please contact the authorities.");
                            return false;
                        }
                        isAutoJournal = true;
                    }
                }

                using (var trans = new esTransactionScope())
                {
                    var sh = new AssetStatusHistory();
                    sh.AddNew();
                    sh.TransactionDate = txtDate.SelectedDate;
                    sh.AssetID = cboAssetID.SelectedValue;
                    sh.SRAssetsStatusFrom = cboSRAssetsStatusFrom.SelectedValue;
                    sh.SRAssetsStatusTo = cboSRAssetsStatusTo.SelectedValue;
                    sh.IsFixedAssetFrom = chkIsFixedAssetFrom.Checked;
                    sh.IsFixedAssetTo = chkIsFixedAssetTo.Checked;
                    sh.CurrentValue = Convert.ToDecimal(txtCurrentValue.Value);
                    sh.DepreciationAccValue = Convert.ToDecimal(txtDepreciationAccValue.Value);
                    sh.Notes = txtNotes.Text;
                    sh.IsApproved = true;
                    sh.ApprovedDateTime = DateTime.Now;
                    sh.ApprovedByUserID = AppSession.UserLogin.UserID;
                    sh.IsVoid = false;
                    sh.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    sh.LastUpdateDateTime = DateTime.Now;
                    sh.Save();

                    var asset = new Asset();
                    asset.LoadByPrimaryKey(cboAssetID.SelectedValue);
                    asset.SRAssetsStatus = cboSRAssetsStatusTo.SelectedValue;
                    if (cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusDisposed || cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusLost)
                    {
                        asset.DateDisposed = txtDate.SelectedDate;
                        asset.ValueDisposed = Convert.ToDecimal(txtCurrentValue.Value);
                    }
                    if (chkIsFixedAssetFrom.Checked != chkIsFixedAssetTo.Checked)
                        asset.IsFixedAsset = chkIsFixedAssetTo.Checked;

                    asset.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    asset.LastUpdateDateTime = DateTime.Now;
                    asset.Save();

                    if (chkIsFixedAssetFrom.Checked && !chkIsFixedAssetTo.Checked)
                    {
                        var adpC = new AssetDepreciationPostCollection();
                        adpC.Query.Where(adpC.Query.AssetID == cboAssetID.SelectedValue);
                        adpC.LoadAll();
                        adpC.MarkAllAsDeleted();
                        adpC.Save();
                    }

                    /* Automatic Journal Testing Start */

                    if (isAutoJournal && (asset.IsFixedAsset ?? false))
                    {
                        int? journalId = JournalTransactions.AddNewAssetDestructionJournal(sh, asset, AppSession.UserLogin.UserID, 0);
                    }

                    /* Automatic Journal Testing End */

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            return true;
        }

        private void EnabledText(bool isNew)
        {
            cboFromServiceUnit.Enabled = isNew;
            cboFromLocation.Enabled = isNew;
            cboAssetID.Enabled = isNew;
            cboSRAssetsStatusTo.Enabled = isNew;

            txtNotes.ReadOnly = !isNew;
            if (!isNew)
            {
                txtCurrentValue.Enabled = false;
                chkIsFixedAssetTo.Enabled = false;
            }
        }

        private void PopulateAssetInfo(string assetId)
        {
            var asset = new Asset();
            if (asset.LoadByPrimaryKey(assetId))
            {
                txtBrandName.Text = asset.BrandName;
                txtSerialNumber.Text = asset.SerialNumber;
                var g = AssetGroup.Get(asset.AssetGroupID);
                txtAssetGroup.Text = string.Format("{0} - {1}", asset.AssetGroupID, g.GroupName);
                txtPurchaseDate2.Text = asset.PurchasedDate.Value.ToString("dd-MMMM-yyyy");
                cboSRAssetsStatusFrom.SelectedValue = asset.SRAssetsStatus;
                chkIsFixedAssetFrom.Checked = asset.IsFixedAsset ?? false;
                chkIsFixedAssetTo.Checked = asset.IsFixedAsset ?? false;
                if (asset.IsFixedAsset == true)
                {
                    var adpQ = new AssetDepreciationPostQuery("adp");
                    adpQ.Where(adpQ.AssetID == assetId);
                    adpQ.Where(@"<CAST(adp.[Month] AS INT) = MONTH(GETDATE()) AND CAST(adp.[Year] AS INT) = YEAR(GETDATE())>");
                    DataTable dtb = adpQ.LoadDataTable();
                    txtCurrentValue.Value = dtb.Rows.Count > 0 ? (Convert.ToDouble(dtb.Rows[0]["CurrentAmount"]) - Convert.ToDouble(dtb.Rows[0]["DepreciationAmount"])) : 0;
                    txtDepreciationAccValue.Value = dtb.Rows.Count > 0 ? Convert.ToDouble(dtb.Rows[0]["AccumulationAmount"]) : 0;
                }
                else
                {
                    txtCurrentValue.Value = 0;
                    txtDepreciationAccValue.Value = 0;
                }
            }
            else
            {
                txtBrandName.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtAssetGroup.Text = string.Empty;
                txtPurchaseDate2.Text = string.Empty;
                cboSRAssetsStatusFrom.SelectedValue = string.Empty;
                cboSRAssetsStatusFrom.Text = string.Empty;
                chkIsFixedAssetFrom.Checked = false;
                chkIsFixedAssetTo.Checked = false;
                txtCurrentValue.Value = 0;
                txtDepreciationAccValue.Value = 0;
            }
        }

        #region Combobox
        protected void cboFromServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            //var usr = new AppUserServiceUnitQuery("b");
            //query.InnerJoin(usr).On(query.ServiceUnitID == usr.ServiceUnitID &&
            //                        usr.UserID == AppSession.UserLogin.UserID);
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.es.Top = 20;
            query.Where
                (
                    query.ServiceUnitName.Like(searchText),
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitID.Ascending);

            cboFromServiceUnit.DataSource = query.LoadDataTable();
            cboFromServiceUnit.DataBind();
        }

        protected void cboFromServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromLocation.Items.Clear();
            cboFromLocation.Text = string.Empty;
            PopulateRoomList(cboFromServiceUnit.SelectedValue, true, cboFromLocation);
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
        }

        protected void cboFromLocation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
        }

        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        protected void cboAssetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AssetQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var tounit = new ServiceUnitQuery("c");
            query.InnerJoin(fromunit).On(query.ServiceUnitID == fromunit.ServiceUnitID);
            query.LeftJoin(tounit).On(query.MaintenanceServiceUnitID == tounit.ServiceUnitID);
            query.es.Top = 20;
            query.Select(query.AssetID, query.AssetName, query.SerialNumber, fromunit.ServiceUnitName,
                         tounit.ServiceUnitName.As("MaintenanceServiceUnitName"));
            query.Where
                (
                    query.Or(query.AssetID == e.Text, query.AssetName.Like(searchText), query.SerialNumber.Like(searchText)),
                    query.ServiceUnitID == cboFromServiceUnit.SelectedValue
                );

            query.Where(query.SRAssetsStatus.In(AppSession.Parameter.AssetsStatusActive, AppSession.Parameter.AssetsStatusInActive, AppSession.Parameter.AssetsStatusDamaged));

            if (!string.IsNullOrEmpty(cboFromLocation.SelectedValue))
            {
                query.Where(query.AssetLocationID == cboFromLocation.SelectedValue);
            }
            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboAssetID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateAssetInfo(e.Value);
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboSRAssetsStatusTo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusDisposed || cboSRAssetsStatusTo.SelectedValue == AppSession.Parameter.AssetsStatusLost)
            {
                txtCurrentValue.Enabled = false;//chkIsFixedAssetFrom.Checked;
                chkIsFixedAssetTo.Enabled = false;
            }
            else
            {
                txtCurrentValue.Enabled = false;
                chkIsFixedAssetTo.Enabled = cboSRAssetsStatusFrom.SelectedValue == e.Value;
            }
            chkIsFixedAssetTo.Checked = chkIsFixedAssetFrom.Checked;
        }

        #endregion

        #region Populate List
        private void PopulateRoomList(string serviceUnitId, bool isNew, RadComboBox comboBox)
        {
            if (serviceUnitId != string.Empty)
            {
                var sr = new ServiceRoomCollection();
                sr.Query.Where(sr.Query.ServiceUnitID == serviceUnitId);

                if (isNew)
                    sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in sr)
                {
                    comboBox.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }
        #endregion
    }
}
