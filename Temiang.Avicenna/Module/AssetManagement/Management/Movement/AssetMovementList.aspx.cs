using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetMovementList : BasePageList
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AssetMovementSearch.aspx?type=" + FormType;
            UrlPageDetail = "AssetMovementDetail.aspx?type=" + FormType;

            WindowSearch.Height = 400;

            if (FormType == "req")
                ProgramID = AppConstant.Program.ASSET_MOVEMENT_REQUEST;
            else
                ProgramID = AppConstant.Program.ASSET_MOVEMENT;

            if (!IsPostBack)
            {
                grdList.Columns[grdList.Columns.Count - 2].Visible = FormType == "req";
                grdList.Columns[grdList.Columns.Count - 1].Visible = FormType == "";
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
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
            string id = dataItem.GetDataKeyValue(AssetMovementMetadata.ColumnNames.AssetMovementNo).ToString();
            string url = string.Format("AssetMovementDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AssetMovements;
        }

        private DataTable AssetMovements
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AssetMovementQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AssetMovementQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AssetMovementQuery("a");
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

                    if (FormType == "req")
                    {
                        var usr = new AppUserServiceUnitQuery("usr");
                        query.InnerJoin(usr).On(usr.ServiceUnitID == query.FromServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
                    }

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
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        
    }
}