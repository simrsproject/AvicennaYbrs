using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class FeasibilityTestList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "FeasibilityTestSearch.aspx";
            UrlPageDetail = "FeasibilityTestDetail.aspx";

            ProgramID = AppConstant.Program.CssdFeasibilityTest;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
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
            string id = dataItem.GetDataKeyValue(CssdFeasibilityTestMetadata.ColumnNames.FeasibilityTestNo).ToString();
            string url = string.Format("FeasibilityTestDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdFeasibilityTests;
        }

        private DataTable CssdFeasibilityTests
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdFeasibilityTestQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdFeasibilityTestQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdFeasibilityTestQuery("a");
                    query.Select
                        (
                            query.FeasibilityTestNo,
                            query.FeasibilityTestDate,
                            query.FeasibilityTestTime,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.OrderBy(query.FeasibilityTestDate.Descending, query.FeasibilityTestNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new CssdFeasibilityTestItemQuery("a");
            var received = new CssdSterileItemsReceivedItemQuery("b");
            var iq = new ItemQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.FeasibilityTestNo,
                    query.FeasibilityTestSeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,

                    received.CssdItemNo,
                    @"<CAST(b.CssdItemNo  AS VARCHAR) AS 'ItemNo'>",
                    received.ItemID,
                    iq.ItemName,
                    received.Qty,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes,

                    query.IsFeasibilityTestPassed,
                    query.IsBrokenInstrument,
                    query.QtyReplacements,

                    @"<ISNULL((SELECT SUM(x.QtyReplacements) AS QtyReplacementsDetail FROM CssdSterileItemsReceivedItemDetail AS x WHERE x.ReceivedNo = a.ReceivedNo AND x.ReceivedSeqNo = a.ReceivedSeqNo), 0) AS 'QtyReplacementsDetail'>",
                    @"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) AS IsBrokenInstrumentDetail FROM CssdSterileItemsReceivedItemDetail AS x WHERE x.ReceivedNo = a.ReceivedNo AND x.ReceivedSeqNo = a.ReceivedSeqNo AND x.IsBrokenInstrument = 1), CAST(0 AS BIT)) AS 'IsBrokenInstrumentDetail'>"

                );
            query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                     received.ReceivedSeqNo == query.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.FeasibilityTestNo == e.DetailTableView.ParentItem.GetDataKeyValue("FeasibilityTestNo").ToString());
            query.OrderBy(query.FeasibilityTestSeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}