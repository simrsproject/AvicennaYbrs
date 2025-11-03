using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingAdditionalList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "InvoicingAdditionalSearch.aspx";
            UrlPageDetail = "InvoicingAdditionalDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.AR_INVOICING_ADDITIONAL;

            if (!IsPostBack)
            {
                if (!AppSession.Parameter.IsAllowDiscountInvoice)
                {
                    grdList.Columns[4].Visible = false; // kolom total transaksi
                    grdList.Columns[5].Visible = false; // kolom diskon
                }
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
            string id = dataItem.GetDataKeyValue(InvoiceSupplierMetadata.ColumnNames.InvoiceNo).ToString();
            string url = string.Format("{0}?md={1}&id={2}",UrlPageDetail, mode, id);
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
            var query = new InvoicesItemQuery("a");
            var coa = new ChartOfAccountsQuery("coa");
            var pat = new PatientQuery("d");
            query.LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
            query.LeftJoin(pat).On(query.PatientID == pat.PatientID);

            query.Where(query.InvoiceNo == invoiceNo, query.InvoiceReferenceNo == string.Empty);
            query.OrderBy(query.PaymentNo.Ascending);

            query.Select
                (
                    query.InvoiceNo,
                    query.PaymentNo,
                    query.Amount,
                    query.PpnAmount,
                    query.PphAmount,
                    query.VerifyAmount,
                    query.PaymentAmount,
                    query.OtherAmount,
                    query.Notes,
                    query.PatientID,
                    query.PatientName,
                     pat.MedicalNo.As("refPatientID_MedicalNo"),
                    coa.ChartOfAccountCode.As("refToChartOfAccounts_ChartOfAccountCode"),
                    coa.ChartOfAccountName.As("refToChartOfAccounts_ChartOfAccountName")
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

                InvoicesQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (InvoicesQuery)Session[SessionNameForQuery];
                else
                {
                    query = new InvoicesQuery("a");
                    var guar = new GuarantorQuery("b");
                    var sr = new AppStandardReferenceItemQuery("c");
                    var itm = new InvoicesItemQuery("d");

                    query.InnerJoin(itm).On(itm.InvoiceNo == query.InvoiceNo);
                    query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
                    query.LeftJoin(sr).On(
                        query.SRReceivableStatus == sr.ItemID &&
                        sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                        );

                    query.Select(
                           query.InvoiceNo,
                           query.InvoiceDate,
                           query.InvoiceDueDate,
                           guar.GuarantorName,
                           query.IsApproved,
                           query.IsVoid,
                           sr.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                           "<SUM(d.VerifyAmount) AS Total>",
                           "<ISNULL(a.DiscountAmount, 0) AS Discount>",
                           "<SUM(d.VerifyAmount) - ISNULL(a.DiscountAmount, 0) AS TotalAmount>"
                       );

                    query.Where(query.IsInvoicePayment == false, query.InvoiceReferenceNo.IsNull(), query.IsAdditionalInvoice == true);
                    query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, guar.GuarantorName,
                                  query.IsApproved, query.IsVoid, sr.ItemName, query.DiscountAmount);
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
