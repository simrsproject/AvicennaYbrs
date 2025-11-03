using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderList : BasePageList
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty( Request.QueryString["type"])?string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkOrderSearch.aspx?type=" + getPageID;
            UrlPageDetail = "WorkOrderDetail.aspx?type=" + getPageID;

            ProgramID = getPageID == "" ? AppConstant.Program.AssetWorkOrder : AppConstant.Program.SanitationActivityWorkOrder;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                grdList.Columns[7].Visible = getPageID == "";
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
            string id = dataItem.GetDataKeyValue(AssetWorkOrderMetadata.ColumnNames.OrderNo).ToString();
            Page.Response.Redirect("WorkOrderDetail.aspx?md=" + mode + "&id=" + id + "&type=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AssetWorkOrders;
        }

        private DataTable AssetWorkOrders
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AssetWorkOrderQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AssetWorkOrderQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AssetWorkOrderQuery("a");
                    var fromunit = new ServiceUnitQuery("b");
                    var tounit = new ServiceUnitQuery("c");
                    var wtype = new AppStandardReferenceItemQuery("d");
                    var wstatus = new AppStandardReferenceItemQuery("e");
                    var asset = new AssetQuery("f");
                    var user = new AppUserServiceUnitQuery("g");

                    query.Select
                        (
                            query.OrderNo,
                            query.OrderDate,
                            query.RequiredDate,
                            fromunit.ServiceUnitName.As("FromServiceUnit"),
                            tounit.ServiceUnitName.As("ToServiceUnit"),
                            wtype.ItemName.As("WorkType"),
                            wstatus.ItemName.As("WorkStatus"),
                            asset.AssetName,
                            query.ProblemDescription,
                            query.IsPreventiveMaintenance,
                            query.IsApproved,
                            query.IsVoid
                        );

                    if (getPageID == "")
                        query.Select(@"<'WorkOrderDetail.aspx?md=view&id='+a.OrderNo+'&type=' as WoUrl>");
                    else
                        query.Select(@"<'WorkOrderDetail.aspx?md=view&id='+a.OrderNo+'&type=sa' as WoUrl>");

                    query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(wtype).On
                        (
                            wtype.ItemID == query.SRWorkType &&
                            wtype.StandardReferenceID == "WorkType"
                        );
                    query.InnerJoin(wstatus).On
                        (
                            wstatus.ItemID == query.SRWorkStatus &&
                            wstatus.StandardReferenceID == "WorkStatus"
                        );
                    query.LeftJoin(asset).On(asset.AssetID == query.AssetID);
                    query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);

                    if (getPageID == "")
                        query.Where(query.IsSanitation == false);
                    else
                        query.Where(query.IsSanitation == true);

                    query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
