using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class UltrasoundProcessList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "UltrasoundProcessSearch.aspx";
            UrlPageDetail = "UltrasoundProcessDetail.aspx";

            ProgramID = AppConstant.Program.CssdUltrasoundProcess;

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
            string id = dataItem.GetDataKeyValue(CssdSterileItemsUltrasoundMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("UltrasoundProcessDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdSterileItemsUltrasounds;
        }

        private DataTable CssdSterileItemsUltrasounds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdSterileItemsUltrasoundQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdSterileItemsUltrasoundQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdSterileItemsUltrasoundQuery("a");
                    var usr = new AppUserQuery("b");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.TransactionTime,
                            query.TransactionByUserID,
                            usr.UserName.As("TransactionBy"),
                            query.IsApproved,
                            query.IsVoid,
                            "<'UltrasoundProcessDetail.aspx?md=view&id='+a.TransactionNo AS PUrl>"
                        );

                    query.InnerJoin(usr).On(usr.UserID == query.TransactionByUserID);

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
            var query = new CssdSterileItemsUltrasoundItemQuery("a");
            var received = new CssdSterileItemsReceivedItemQuery("b");
            var iq = new ItemQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionSeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,
                    received.Qty,
                    received.CssdItemNo,
                    @"<CAST((CAST(b.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",
                    received.ItemID,
                    iq.ItemName.As("ItemName"),
                    received.SRCssdItemUnit,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes
                );
            query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(received.CssdItemNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
