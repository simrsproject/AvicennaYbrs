using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetItemList : BasePageList
    {
        private string getPageID
        {
            get
            {
                return Request.QueryString["fa"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
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
            
            UrlPageDetail = "AssetItemDetail.aspx?fa=" + getPageID;

            this.WindowSearch.Height = 400;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
                grdList.Columns.FindByUniqueName("IsFixedAsset").Visible = (getPageID == "2");
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", getPageID);
            return script;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(AssetMetadata.ColumnNames.AssetID).ToString();
            Page.Response.Redirect("AssetItemDetail.aspx?md=" + mode + "&id=" + id + "&fa=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Assets;
        }

        private DataTable Assets
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AssetQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AssetQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AssetQuery("a");
                    var unit = new ServiceUnitQuery("b");
                    var room = new ServiceRoomQuery("c");
                    var agroup = new AssetGroupQuery("d");
                    var astatus = new AppStandardReferenceItemQuery("e");

                    query.Select
                        (
                            query.AssetID,
                            query.AssetName,
                            query.BrandName,
                            query.SerialNumber,
                            agroup.GroupName.As("AssetGroupName"),
                            unit.ServiceUnitName,
                            room.RoomName.As("LocationName"),
                            query.Notes,
                            astatus.ItemName.As("AssetStatus"), 
                            query.IsFixedAsset,
                            query.PurchaseOrderNumber
                        );

                    query.LeftJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
                    query.LeftJoin(room).On(room.RoomID == query.AssetLocationID);
                    query.InnerJoin(agroup).On(agroup.AssetGroupId == query.AssetGroupID);
                    query.InnerJoin(astatus).On
                        (
                            astatus.ItemID == query.SRAssetsStatus &&
                            astatus.StandardReferenceID == "AssetsStatus"
                        );

                    if (getPageID == "0")
                        query.Where(query.IsFixedAsset == false);
                    else if (getPageID == "1" || getPageID == "d")
                        query.Where(query.IsFixedAsset == true);

                    query.OrderBy(query.AssetID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "AssetName", "AssetID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
