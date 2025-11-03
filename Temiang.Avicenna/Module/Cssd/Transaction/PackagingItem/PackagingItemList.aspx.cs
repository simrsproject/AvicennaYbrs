using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class PackagingItemList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PackagingItemSearch.aspx";
            UrlPageDetail = "PackagingItemDetail.aspx";

            ProgramID = AppConstant.Program.CssdPackagingItem;

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
            string id = dataItem.GetDataKeyValue(CssdPackagingMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("PackagingItemDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdPackagings;
        }

        private DataTable CssdPackagings
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdPackagingQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdPackagingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdPackagingQuery("a");
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
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
            var query = new CssdPackagingItemQuery("a");
            var received = new CssdSterileItemsReceivedItemQuery("b");
            var iq = new ItemQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.SeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,

                    received.CssdItemNo,
                    @"<CAST(b.CssdItemNo  AS VARCHAR) AS 'ItemNo'>",
                    received.ItemID,
                    iq.ItemName,
                    received.Qty,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes

                );
            query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                     received.ReceivedSeqNo == query.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.SeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}