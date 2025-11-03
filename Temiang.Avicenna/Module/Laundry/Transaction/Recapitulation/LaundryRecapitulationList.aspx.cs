using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryRecapitulationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaundryRecapitulationSearch.aspx";
            UrlPageDetail = "LaundryRecapitulationDetail.aspx";

            ProgramID = AppConstant.Program.LaundryRecapitulation;

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
            string id = dataItem.GetDataKeyValue(LaundryRecapitulationProcessMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("LaundryRecapitulationDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundryRecapitulationProcesss;
        }

        private DataTable LaundryRecapitulationProcesss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryRecapitulationProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundryRecapitulationProcessQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryRecapitulationProcessQuery("a");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid,
                            "<'LaundryRecapitulationDetail.aspx?md=view&id='+a.TransactionNo AS TxUrl>"
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
            var query = new LaundryRecapitulationProcessItemQuery("a");
            var iq = new ItemQuery("d");
            var unitq = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),
                    query.Qty,
                    query.QtyRewashing,
                    query.SRItemUnit,
                    unitq.ItemName.As("ItemUnit")
                );
            
            query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}