using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetStatusChangeList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.AssetStatusChange;
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetStatusHistorys;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable AssetStatusHistorys
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnit.SelectedValue) && string.IsNullOrEmpty(cboLocation.SelectedValue) && string.IsNullOrEmpty(cboAssetID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Status")) return null;

                var query = new AssetStatusHistoryQuery("a");
                var asset = new AssetQuery("b");
                var fr = new AppStandardReferenceItemQuery("c");
                var to = new AppStandardReferenceItemQuery("d");
                var unit = new ServiceUnitQuery("e");

                query.Select
                    (
                        query.SeqNo,
                        query.TransactionDate,
                        query.AssetID,
                        asset.AssetName,
                        asset.BrandName,
                        asset.SerialNumber,
                        unit.ServiceUnitName,
                        fr.ItemName.As("AssetsStatusFrom"),
                        to.ItemName.As("AssetsStatusTo"),
                        @"<a.AssetID + ' - ' + b.AssetName + ' (SN: ' + b.SerialNumber + ' | ' + b.BrandName + ')' AS AssetGroup>",
                        query.Notes,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );
                query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
                query.InnerJoin(unit).On(asset.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(fr).On(query.SRAssetsStatusFrom == fr.ItemID && fr.StandardReferenceID == "AssetsStatus");
                query.LeftJoin(to).On(query.SRAssetsStatusTo == to.ItemID && to.StandardReferenceID == "AssetsStatus");

                query.Where(query.SRAssetsStatusTo != AppSession.Parameter.AssetsStatusSold);
                
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

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
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
                    query.AssetName.Like(searchText),
                    query.ServiceUnitID == cboServiceUnit.SelectedValue
                );

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}
