using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReturnedList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaundryReturnedSearch.aspx";
            UrlPageDetail = "LaundryReturnedDetail.aspx";

            ProgramID = AppConstant.Program.LaundryReturned;

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
            string id = dataItem.GetDataKeyValue(LaundryReturnedMetadata.ColumnNames.ReturnNo).ToString();
            string url = string.Format("LaundryReturnedDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundryReturneds;
        }

        private DataTable LaundryReturneds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryReturnedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundryReturnedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryReturnedQuery("a");
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
                            "<'LaundryReturnedDetail.aspx?md=view&id='+a.ReturnNo AS RetUrl>"
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
            var query = new LaundryReturnedItemQuery("a");
            var iq = new ItemQuery("d");
            var inm = new ItemProductNonMedicQuery("f");
            var unitq = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query,
                    iq.ItemName.As("ItemName"),
                    unitq.ItemName.As("ItemUnit")
                );
            query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
            query.InnerJoin(inm).On(inm.ItemID == query.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == inm.SRItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.ReturnNo == e.DetailTableView.ParentItem.GetDataKeyValue("ReturnNo").ToString());
            query.OrderBy(query.ReturnSeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
