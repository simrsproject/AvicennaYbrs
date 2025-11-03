using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetMovementApprovalList : BasePage
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["md"]))
            {
                // Redirect to entry page
                Response.Redirect(string.Format("AssetMovementDetail.aspx?{0}", Request.QueryString));
                return;
            }

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

            ProgramID = AppConstant.Program.ASSET_MOVEMENT;

            if (!IsPostBack)
            {
                
            }
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
            var dataSource = AssetMovements;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable AssetMovements
        {
            get
            {
                var isEmptyFilter = txtFromMovementDate.IsEmpty && txtToMovementDate.IsEmpty && string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue) 
                    && string.IsNullOrEmpty(cboAssetID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Movement")) return null;

                var query = new AssetMovementQuery("a");
                var asset = new AssetQuery("b");
                var fsu = new ServiceUnitQuery("c");
                var floc = new LocationQuery("d");
                var tsu = new ServiceUnitQuery("e");
                var tloc = new LocationQuery("f");

                query.InnerJoin(asset).On(asset.AssetID == query.AssetID);
                query.InnerJoin(fsu).On(fsu.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(floc).On(floc.LocationID == query.FromAssetLocationID);
                query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(tloc).On(tloc.LocationID == query.ToAssetLocationID);

                query.OrderBy
                    (
                        query.AssetMovementNo.Descending
                    );

                query.Select(
                    query.AssetMovementNo,
                    query.MovementDate,
                    query.AssetID,
                    asset.AssetName,
                    query.FromServiceUnitID,
                    fsu.ServiceUnitName.As("FromServiceUnitName"),
                    query.FromAssetLocationID,
                    floc.LocationName.As("FromLocationName"),
                    query.ToServiceUnitID,
                    tsu.ServiceUnitName.As("ToServiceUnitName"),
                    query.ToAssetLocationID,
                    tloc.LocationName.As("ToLocationName"),
                    query.Notes,
                    query.IsPosted,
                    query.IsApproved,
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID
                    );

                query.Where(query.IsPosted == true);

                if (!txtFromMovementDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToMovementDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.MovementDate.Between(txtFromMovementDate.SelectedDate, txtToMovementDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                    query.Where(query.AssetID == cboAssetID.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }
        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetMovementOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable AssetMovementOutstandings
        {
            get
            {
                var isEmptyFilter = txtFromMovementDate.IsEmpty && txtToMovementDate.IsEmpty && string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue)
                    && string.IsNullOrEmpty(cboAssetID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Movement")) return null;

                var query = new AssetMovementQuery("a");
                var asset = new AssetQuery("b");
                var fsu = new ServiceUnitQuery("c");
                var floc = new LocationQuery("d");
                var tsu = new ServiceUnitQuery("e");
                var tloc = new LocationQuery("f");

                query.InnerJoin(asset).On(asset.AssetID == query.AssetID);
                query.InnerJoin(fsu).On(fsu.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(floc).On(floc.LocationID == query.FromAssetLocationID);
                query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(tloc).On(tloc.LocationID == query.ToAssetLocationID);

                query.OrderBy
                    (
                        query.AssetMovementNo.Descending
                    );

                query.Select(
                    query.AssetMovementNo,
                    query.MovementDate,
                    query.AssetID,
                    asset.AssetName,
                    query.FromServiceUnitID,
                    fsu.ServiceUnitName.As("FromServiceUnitName"),
                    query.FromAssetLocationID,
                    floc.LocationName.As("FromLocationName"),
                    query.ToServiceUnitID,
                    tsu.ServiceUnitName.As("ToServiceUnitName"),
                    query.ToAssetLocationID,
                    tloc.LocationName.As("ToLocationName"),
                    query.Notes,
                    query.IsPosted,
                    query.IsApproved,
                    query.LastUpdateDateTime,
                    query.LastUpdateByUserID
                    );

                query.Where(query.IsApproved == true, query.IsPosted == false);

                if (!txtFromMovementDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToMovementDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(query.MovementDate.Between(txtFromMovementDate.SelectedDate, txtToMovementDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboAssetID.SelectedValue))
                    query.Where(query.AssetID == cboAssetID.SelectedValue);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListOutstanding.Rebind();
        }

        protected void cboFromServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );

            query.Where
                (
                    query.IsActive == true, 
                    query.ServiceUnitName.Like(searchText)
                );

            cboFromServiceUnit.DataSource = query.LoadDataTable();
            cboFromServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFromServiceUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboAssetID.Items.Clear();
            cboAssetID.Text = string.Empty;
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
                    query.Or(query.AssetID.Like(searchText),
                            query.AssetName.Like(searchText),
                            query.SerialNumber.Like(searchText))
                );
            if (!string.IsNullOrEmpty(cboFromServiceUnit.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboFromServiceUnit.SelectedValue);
            }
            query.OrderBy(query.AssetName.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboAssetID.DataSource = dtb;
            cboAssetID.DataBind();
        }

        protected void cboAssetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AssetName"] + " (" + ((DataRowView)e.Item.DataItem)["AssetID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AssetID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}