using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PhysicianLeaveList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "PhysicianLeaveSearch.aspx";
            UrlPageDetail = "PhysicianLeaveDetail.aspx";

            ProgramID = AppConstant.Program.PhysicianLeave;

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(ParamedicLeaveMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("PhysicianLeaveDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ParamedicLeave;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new ParamedicLeaveQuery("a");
            query.Select
                (
                    query.TransactionNo,
                    query.Notes
                );

            query.Where(query.TransactionNo == transNo);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ParamedicLeave
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicLeaveQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ParamedicLeaveQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ParamedicLeaveQuery("a");
                    var item = new AppStandardReferenceItemQuery("b");
                    var par = new ParamedicQuery("c");
                    var parIp = new ParamedicQuery("d");
                    var parOp = new ParamedicQuery("e");
                    var parEr = new ParamedicQuery("f");

                    query.LeftJoin(item).On(query.SRPhysicianLeaveReason == item.ItemID &&
                                             item.StandardReferenceID == AppEnum.StandardReference.PhysicianLeaveReason);
                    query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                    query.LeftJoin(parIp).On(query.SubsParamedicIP == parIp.ParamedicID);
                    query.LeftJoin(parOp).On(query.SubsParamedicOP == parOp.ParamedicID);
                    query.LeftJoin(parEr).On(query.SubsParamedicEMR == parEr.ParamedicID);
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            par.ParamedicName,
                            item.ItemName,
                            query.StartDate,
                            query.EndDate,
                            query.Notes,
                            parIp.ParamedicName.As("SubsParamedicIP"),
                            parOp.ParamedicName.As("SubsParamedicOP"),
                            parEr.ParamedicName.As("SubsParamedicEMR"),
                            query.IsApproved,
                            query.LastUpdateByUserID,
                            query.LastUpdateDateTime
                        );
                    query.OrderBy(query.TransactionNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
