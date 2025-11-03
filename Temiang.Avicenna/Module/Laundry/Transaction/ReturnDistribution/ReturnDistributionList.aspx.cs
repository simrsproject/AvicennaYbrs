using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class ReturnDistributionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ReturnDistributionSearch.aspx";
            UrlPageDetail = "ReturnDistributionDetail.aspx";

            ProgramID = AppConstant.Program.LaundryReturnDistribution;

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
            string id = dataItem.GetDataKeyValue(LaundryReturnDistributionMetadata.ColumnNames.ReturnNo).ToString();
            Page.Response.Redirect("ReturnDistributionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundryReturnDistributions;
        }

        private DataTable LaundryReturnDistributions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryReturnDistributionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundryReturnDistributionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryReturnDistributionQuery("a");
                    var fromUnit = new ServiceUnitQuery("b");
                    var usr = new AppUserQuery("c");

                    query.Select
                        (
                            query.ReturnNo,
                            query.ReturnDate,
                            query.ReturnTime,
                            fromUnit.ServiceUnitName.As("FromServiceUnitName"),
                            usr.UserName.As("HandedByUserName"),
                            query.ReceivedBy,
                            query.IsApproved,
                            query.IsVoid,
                            @"<'ReturnDistributionDetail.aspx?md=view&id='+a.ReturnNo AS RUrl>"
                        );

                    query.InnerJoin(fromUnit).On(fromUnit.ServiceUnitID == query.FromServiceUnitID);
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
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string distNo = dataItem.GetDataKeyValue("ReturnNo").ToString();

            var query = new LaundryReturnDistributionItemQuery("a");
            var iq = new ItemQuery("b");
            var unitq = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                    query.ReturnNo,
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
            query.Where(query.ReturnNo == distNo);
            query.OrderBy(query.SeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}