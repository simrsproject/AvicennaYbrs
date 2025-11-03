using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class PaymentList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_PAYMENT;

            UrlPageSearch = "PaymentSearch.aspx";
            UrlPageDetail = "PaymentDetail.aspx";

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
            string id = dataItem.GetDataKeyValue(InvoiceCustomerMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("PaymentDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            grdList.DataSource = InvoiceCustomers;
        }

        private DataTable InvoiceCustomers
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
                    var cust = new CustomerQuery("b");
                    var sr = new AppStandardReferenceItemQuery("c");
                    var itm = new InvoiceCustomerItemQuery("d");

                    query.InnerJoin(itm).On(query.InvoiceNo == itm.InvoiceNo);
                    query.LeftJoin(cust).On(query.CustomerID == cust.CustomerID);
                    query.LeftJoin(sr).On(
                        query.SRReceivableStatus == sr.ItemID &&
                        sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                        );

                    query.Select(
                           query.InvoiceNo,
                           query.InvoiceReferenceNo,
                           query.PaymentDate,
                           cust.CustomerName,
                           query.IsApproved,
                           query.IsVoid,
                           sr.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                           "<SUM(d.PaymentAmount) AS Total>"
                       );

                    query.Where(query.IsInvoicePayment == true);
                    query.GroupBy(query.InvoiceNo, query.InvoiceReferenceNo, query.PaymentDate, cust.CustomerName,
                                  query.IsApproved, query.IsVoid, sr.ItemName);
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
