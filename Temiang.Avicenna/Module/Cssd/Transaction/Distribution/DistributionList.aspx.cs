using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DistributionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DistributionSearch.aspx";
            UrlPageDetail = "DistributionDetail.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsDistribution;

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
            string id = dataItem.GetDataKeyValue(CssdDistributionMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("DistributionDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdDistributions;
        }

        private DataTable CssdDistributions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdDistributionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdDistributionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdDistributionQuery("a");
                    var tounit = new ServiceUnitQuery("b");
                    var usr = new AppUserQuery("c");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.TransactionTime,
                            tounit.ServiceUnitName.As("ToServiceUnitName"),
                            query.HandedByUserID,
                            usr.UserName.As("HandedBy"),
                            query.ReceivedBy,
                            query.IsApproved,
                            query.IsVoid
                        );

                    query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

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
            var query = new CssdDistributionItemQuery("a");
            var iq = new ItemQuery("b");
            var vwipq = new VwItemProductMedicNonMedicQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),
                    query.Qty,
                    unitq.ItemName.As("CssdItemUnit")
                );
            query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
            query.InnerJoin(vwipq).On(vwipq.ItemID == query.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == vwipq.SRItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(iq.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}