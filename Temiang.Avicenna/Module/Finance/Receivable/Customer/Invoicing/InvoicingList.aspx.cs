using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicingList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "InvoicingSearch.aspx";
            UrlPageDetail = "InvoicingDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.AR_CUSTOMER_INVOICING;

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
            string id = dataItem.GetDataKeyValue(InvoiceCustomerMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("InvoicingDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            if (!e.IsFromDetailTable)
                grdList.DataSource = Invoicess;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
            //Load record
            var query = new InvoiceCustomerItemQuery("a");
            var txQ = new ItemTransactionQuery("b");
            var csQ = new CustomerQuery("c");
            query.InnerJoin(txQ).On(query.TransactionNo == txQ.TransactionNo);
            query.InnerJoin(csQ).On(txQ.CustomerID == csQ.CustomerID);

            query.Where(query.InvoiceNo == invoiceNo);
            query.OrderBy(query.TransactionNo.Ascending);

            query.Select
                (
                    query.InvoiceNo,
                    query.TransactionNo,
                    query.TransactionDate,
                    query.Amount,
                    query.VerifyAmount,
                    query.PaymentAmount,
                    query.OtherAmount,
                    query.Notes,
                    csQ.CustomerName
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable Invoicess
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                InvoiceCustomerQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (InvoiceCustomerQuery)Session[SessionNameForQuery];
                else
                {
                    query = new InvoiceCustomerQuery("a");
                    var csQ = new CustomerQuery("b");
                    var srQ = new AppStandardReferenceItemQuery("c");
                    var itmQ = new InvoiceCustomerItemQuery("d");

                    query.InnerJoin(itmQ).On(itmQ.InvoiceNo == query.InvoiceNo);
                    query.LeftJoin(csQ).On(query.CustomerID == csQ.CustomerID);
                    query.LeftJoin(srQ).On(
                        query.SRReceivableStatus == srQ.ItemID && 
                        srQ.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                        );

                    query.Select(
                           query.InvoiceNo,
                           query.InvoiceDate,
                           query.InvoiceDueDate,
                           csQ.CustomerName,
                           query.IsApproved,
                           query.IsVoid,
                           srQ.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                           "<SUM(d.Amount) AS TotalAmount>"
                       );

                    query.Where(query.IsInvoicePayment == false, query.InvoiceReferenceNo.IsNull());
                    query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, csQ.CustomerName,
                                  query.IsApproved, query.IsVoid, srQ.ItemName);
                    query.OrderBy(query.InvoiceNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
