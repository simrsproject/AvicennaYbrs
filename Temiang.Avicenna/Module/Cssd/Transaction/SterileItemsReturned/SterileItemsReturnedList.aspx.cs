using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReturnedList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SterileItemsReturnedSearch.aspx";
            UrlPageDetail = "SterileItemsReturnedDetail.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsReturned;

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
            string id = dataItem.GetDataKeyValue(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnNo).ToString();
            string url = string.Format("SterileItemsReturnedDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdSterileItemsReturneds;
        }

        private DataTable CssdSterileItemsReturneds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdSterileItemsReturnedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdSterileItemsReturnedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdSterileItemsReturnedQuery("a");
                    var tounit = new ServiceUnitQuery("b");
                    var usr = new AppUserQuery("c");

                    query.Select
                        (
                            query.ReturnNo,
                            query.ReturnDate,
                            query.ReturnTime,
                            tounit.ServiceUnitName.As("ToServiceUnitName"),
                            query.HandedByUserID,
                            usr.UserName.As("HandedBy"),
                            query.ReceivedBy,
                            query.IsApproved,
                            query.IsVoid,
                            "<'SterileItemsReturnedDetail.aspx?md=view&id='+a.ReturnNo AS RetUrl>"
                        );

                    query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

                    query.OrderBy(query.ReturnDate.Descending, query.ReturnNo.Descending);
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
            var query = new CssdSterileItemsReturnedItemQuery("a");
            var proceed = new CssdSterilizationProcessItemQuery("b");
            var received = new CssdSterileItemsReceivedItemQuery("c");
            var iq = new ItemQuery("d");
            var unitq = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query.ReturnNo,
                    query.ReturnSeqNo,
                    query.ProcessNo,
                    query.ProcessSeqNo,
                    proceed.ReceivedNo,
                    proceed.ReceivedSeqNo,
                    proceed.Qty,
                    proceed.Weight,

                    received.CssdItemNo,
                    @"<CAST((CAST(c.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

                    received.ItemID,
                    iq.ItemName.As("ItemName"),

                    received.SRCssdItemUnit,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes
                );
            query.InnerJoin(proceed).On(proceed.ProcessNo == query.ProcessNo &&
                                        proceed.ProcessSeqNo == query.ProcessSeqNo);
            query.InnerJoin(received).On(received.ReceivedNo == proceed.ReceivedNo &&
                                         received.ReceivedSeqNo == proceed.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.ReturnNo == e.DetailTableView.ParentItem.GetDataKeyValue("ReturnNo").ToString());
            query.OrderBy(received.CssdItemNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
