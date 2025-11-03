using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class AssetAuctionList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.AssetAuction;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdDisposed_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdDisposed.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = InactiveAssets;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
            //grdDisposed.DataSource = InactiveAssets;
        }

        private DataTable InactiveAssets
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnit.SelectedValue) && string.IsNullOrEmpty(cboLocation.SelectedValue) && string.IsNullOrEmpty(cboAssetID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Auction")) return null;

                var query = new AssetQuery("a");
                var unit = new ServiceUnitQuery("e");
                var auc = new AssetStatusHistoryQuery("f");
                var assetgroup = new AssetGroupQuery("g");

                query.Select
                    (
                        @"<(SELECT TOP 1 x.SeqNo FROM AssetStatusHistory x WHERE x.AssetID = a.AssetID AND x.SRAssetsStatusTo = a.SRAssetsStatus ORDER BY x.ApprovedDateTime DESC) AS SeqNo>",
                        @"<(SELECT TOP 1 x.TransactionDate FROM AssetStatusHistory x WHERE x.AssetID = a.AssetID AND x.SRAssetsStatusTo = a.SRAssetsStatus ORDER BY x.ApprovedDateTime DESC) AS TransactionDate>",
                        query.AssetID,
                        query.AssetName,
                        query.BrandName,
                        query.SerialNumber,
                        assetgroup.GroupName.As("AssetGroupName"),
                        unit.ServiceUnitName,
                        @"<a.AssetID + ' - ' + a.AssetName + ' (SN: ' + a.BrandName + ' - ' + a.SerialNumber + ' | Location : ' + e.ServiceUnitName + ')' AS AssetGroup>",
                        @"<(SELECT TOP 1 x.Notes FROM AssetStatusHistory x WHERE x.AssetID = a.AssetID AND x.SRAssetsStatusTo = a.SRAssetsStatus ORDER BY x.ApprovedDateTime DESC) AS Notes>",
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );
                query.InnerJoin(assetgroup).On(assetgroup.AssetGroupId == query.AssetGroupID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(auc).On(auc.AssetID == query.AssetID && auc.SRAssetsStatusTo == AppSession.Parameter.AssetsStatusSold);

                query.Where(query.SRAssetsStatus.In(AppSession.Parameter.AssetsStatusInActive, AppSession.Parameter.AssetsStatusDamaged), auc.SeqNo.IsNull());

                if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboLocation.SelectedValue))
                    query.Where(query.AssetLocationID == cboLocation.SelectedValue);
                if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                    query.Where(query.AssetID == cboAssetID.SelectedValue);

                query.OrderBy(query.LastUpdateDateTime.Ascending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetAuctioneds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
            //grdList.DataSource = AssetAuctioneds;
        }

        private DataTable AssetAuctioneds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnit.SelectedValue) && string.IsNullOrEmpty(cboLocation.SelectedValue) && string.IsNullOrEmpty(cboAssetID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Auction")) return null;

                var query = new AssetStatusHistoryQuery("a");
                var asset = new AssetQuery("b");
                var fr = new AppStandardReferenceItemQuery("c");
                var to = new AppStandardReferenceItemQuery("d");
                var unit = new ServiceUnitQuery("e");
                var assetgroup = new AssetGroupQuery("g");

                query.Select
                    (
                        query.SeqNo,
                        query.TransactionDate,
                        query.AssetID,
                        asset.AssetName,
                        asset.BrandName,
                        asset.SerialNumber,
                        assetgroup.GroupName.As("AssetGroupName"),
                        unit.ServiceUnitName,
                        fr.ItemName.As("AssetsStatusFrom"),
                        to.ItemName.As("AssetsStatusTo"),
                        @"<a.AssetID + ' - ' + b.AssetName + ' (SN: ' + b.BrandName + ' - ' + b.SerialNumber + ' | Location : ' + e.ServiceUnitName + ')' AS AssetGroup>",
                        query.Notes,
                        query.IsApproved,
                        query.IsVoid,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );
                query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
                query.InnerJoin(assetgroup).On(assetgroup.AssetGroupId == asset.AssetGroupID);
                query.InnerJoin(unit).On(asset.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(fr).On(query.SRAssetsStatusFrom == fr.ItemID && fr.StandardReferenceID == "AssetsStatus");
                query.LeftJoin(to).On(query.SRAssetsStatusTo == to.ItemID && to.StandardReferenceID == "AssetsStatus");

                query.Where(query.SRAssetsStatusTo == AppSession.Parameter.AssetsStatusSold);

                if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
                    query.Where(asset.ServiceUnitID == cboServiceUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboLocation.SelectedValue))
                    query.Where(asset.AssetLocationID == cboLocation.SelectedValue);
                if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                    query.Where(query.AssetID == cboAssetID.SelectedValue);

                query.OrderBy(query.TransactionDate.Descending, query.SeqNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdDisposed.Rebind();
            grdList.Rebind();
        }

        #region Combobox
        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
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
        protected void cboServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboLocation.Items.Clear();
            cboLocation.Text = string.Empty;
            PopulateRoomList(cboServiceUnit.SelectedValue, true, cboLocation);
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
        }

        protected void cboLocation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
                    query.AssetName.Like(searchText)
                );
            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnit.SelectedValue);
            if (!string.IsNullOrEmpty(cboLocation.SelectedValue))
            {
                query.Where(query.AssetLocationID == cboLocation.SelectedValue);
            }
            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
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