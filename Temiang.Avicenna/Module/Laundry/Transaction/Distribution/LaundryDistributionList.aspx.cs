using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryDistributionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaundryDistributionSearch.aspx";
            UrlPageDetail = "LaundryDistributionDetail.aspx";

            ProgramID = AppConstant.Program.LaundryDistribution;

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
            string id = dataItem.GetDataKeyValue(LaundryDistributionMetadata.ColumnNames.DistributionNo).ToString();
            Page.Response.Redirect("LaundryDistributionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundryDistributions;
        }

        private DataTable LaundryDistributions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryDistributionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundryDistributionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryDistributionQuery("a");
                    var tounit = new ServiceUnitQuery("b");
                    var usr = new AppUserQuery("c");

                    query.Select
                        (
                            query.DistributionNo,
                            query.DistributionDate,
                            query.DistributionTime,
                            tounit.ServiceUnitName.As("ToServiceUnitName"),
                            usr.UserName.As("HandedByUserName"),
                            query.ReceivedBy,
                            query.IsApproved,
                            query.IsVoid,
                            @"<'LaundryDistributionDetail.aspx?md=view&id='+a.DistributionNo AS RUrl>"
                        );

                    query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                    query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

                    query.OrderBy(query.DistributionDate.Descending, query.DistributionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string distNo = dataItem.GetDataKeyValue("DistributionNo").ToString();

            var query = new LaundryDistributionItemQuery("a");
            var iq = new ItemQuery("b");
            var unitq = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                    query.DistributionNo,
                    query.SeqNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),

                    query.Qty,
                    query.SRItemUnit,
                    unitq.ItemName.As("ItemUnit")
                );
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.DistributionNo == distNo);
            query.OrderBy(query.SeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}