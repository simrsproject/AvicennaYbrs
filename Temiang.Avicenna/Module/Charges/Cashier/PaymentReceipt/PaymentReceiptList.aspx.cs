using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiptList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PaymentReceiptSearch.aspx";
            UrlPageDetail = "PaymentReceiptDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.PaymentReceipt;
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
            string id = dataItem.GetDataKeyValue(TransPaymentReceiptMetadata.ColumnNames.PaymentReceiptNo).ToString();
            string url = string.Format("PaymentReceiptDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = TransPaymentReceipts;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string paymentReceiptNo = dataItem.GetDataKeyValue("PaymentReceiptNo").ToString();
            //Load record

            var query = new TransPaymentReceiptItemQuery("a");
            var py = new TransPaymentQuery("b");

            query.InnerJoin(py).On(query.PaymentNo == py.PaymentNo);
            query.Where(query.PaymentReceiptNo == paymentReceiptNo);
            query.OrderBy(query.PaymentNo.Ascending);

            query.Select
                (
                    query.PaymentReceiptNo,
                    query.PaymentNo,
                    py.PaymentDate,
                    py.PaymentTime,
                    py.PrintReceiptAsName,
                    py.Notes,
                    query.Amount
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable TransPaymentReceipts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                TransPaymentReceiptQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (TransPaymentReceiptQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new TransPaymentReceiptQuery("a");
                    var reg = new RegistrationQuery("b");
                    var pat = new PatientQuery("c");
                    var su = new ServiceUnitQuery("d");
                    
                    query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                    query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                    query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
                    
                    query.Select(
                           query.PaymentReceiptNo,
                           query.PaymentReceiptDate,
                           query.PaymentReceiptTime,
                           query.RegistrationNo,
                           pat.PatientName,
                           su.ServiceUnitName,
                           query.PrintReceiptAsName,
                           query.IsApproved,
                           query.IsVoid,
                           query.ApprovedByUserID
                       );
                    query.OrderBy(query.PaymentReceiptNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

    }
}
