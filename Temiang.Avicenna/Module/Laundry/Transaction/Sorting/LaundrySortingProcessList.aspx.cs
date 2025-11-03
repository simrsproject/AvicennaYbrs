using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundrySortingProcessList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaundrySortingProcessSearch.aspx";
            UrlPageDetail = "LaundrySortingProcessDetail.aspx";

            ProgramID = AppConstant.Program.LaundrySortingProcess;

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
            string id = dataItem.GetDataKeyValue(LaundrySortingProcessMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("LaundrySortingProcessDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundrySortingProcesses;
        }

        private DataTable LaundrySortingProcesses
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundrySortingProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundrySortingProcessQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundrySortingProcessQuery("a");
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.ProcessNo,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid,
                            @"<'LaundrySortingProcessDetail.aspx?md=view&id='+a.TransactionNo AS PUrl>"
                        );

                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            var query = new LaundrySortingProcessItemQuery("a");
            var iq = new ItemQuery("b");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),
                    query.Qty,
                    query.SRItemUnit,
                    unitq.ItemName.As("ItemUnit")
                );
            query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(unitq.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}