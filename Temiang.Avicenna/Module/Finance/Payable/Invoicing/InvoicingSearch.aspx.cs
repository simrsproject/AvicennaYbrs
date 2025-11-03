using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string InvoiceNo;
            public DateTime? InvoiceDate;
            public DateTime? InvoiceDueDate;
            public string SupplierName;
            public string InvoiceSuppNo;
            public string ReceivedNo;
            public string PurchaseOrderNo;
        }

        private string Type
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Type == "1" ? AppConstant.Program.AP_INVOICING : AppConstant.Program.AP_INVOICING2;

            if (Session[SessionNameForQuery] != null)
            {
                InvoicingSearch.SearchValue sv = (InvoicingSearch.SearchValue)Session[SessionNameForQuery];
                txtInvoiceNo.Text = sv.InvoiceNo;
                txtInvoiceDate.SelectedDate = sv.InvoiceDate;
                txtInvoiceDueDate.SelectedDate = sv.InvoiceDueDate;
                txtSupplierName.Text = sv.SupplierName;
                txtInvoiceSuppNo.Text = sv.InvoiceSuppNo;
                txtReceivedNo.Text = sv.ReceivedNo;
                txtPurchaseOrderNo.Text = sv.PurchaseOrderNo;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!IsPostBack)
            //    RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {

            SearchValue sv = new SearchValue();

            sv.InvoiceNo = txtInvoiceNo.Text;
            sv.InvoiceDate = txtInvoiceDate.SelectedDate;
            sv.InvoiceDueDate = txtInvoiceDueDate.SelectedDate;
            sv.SupplierName = txtSupplierName.Text;
            sv.InvoiceSuppNo = txtInvoiceSuppNo.Text;
            sv.ReceivedNo = txtReceivedNo.Text;
            sv.PurchaseOrderNo = txtPurchaseOrderNo.Text;

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;

            //var query = new InvoiceSupplierQuery("a");
            //var supp = new SupplierQuery("b");
            //var sr = new AppStandardReferenceItemQuery("c");
            //var det = new InvoiceSupplierItemQuery("d");
            //var por = new ItemTransactionQuery("por");

            //query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);
            //query.LeftJoin(sr).On(query.SRPayableStatus == sr.ItemID && sr.StandardReferenceID == AppEnum.StandardReference.PayableStatus);
            //query.LeftJoin(det).On(query.InvoiceNo == det.InvoiceNo);
            //query.LeftJoin(por).On(det.TransactionNo == por.TransactionNo);

            //query.Select(
            //       query.InvoiceNo,
            //       query.InvoiceDate,
            //       query.InvoiceDueDate,
            //       supp.SupplierName,
            //       query.InvoiceSuppNo,
            //       query.IsApproved,
            //       query.IsVoid,
            //       sr.ItemName.As("refToAppStandardReference_PayableStatusName"),
            //       "<SUM(ISNULL(d.Amount, 0) + ISNULL(d.PPnAmount, 0) - ISNULL(d.PPh22Amount, 0) - ISNULL(d.PPh23Amount, 0) + ISNULL(d.StampAmount, 0)- ISNULL(d.DownPaymentAmount, 0)- ISNULL(d.OtherDeduction, 0)- ISNULL(d.PphAmount, 0)) AS Total>"
            //   );
            //query.Where(query.IsInvoicePayment == false, query.IsConsignment == false);

            //if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            //{
            //    if (cboFilterInvoiceNo.SelectedIndex == 1)
            //        query.Where(query.InvoiceNo == txtInvoiceNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceNo.Text);
            //        query.Where(query.InvoiceNo.Like(searchTextContain));
            //    }
            //}
            //if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            //{
            //    query.Where(query.InvoiceDate == txtInvoiceDate.SelectedDate);
            //}
            //if (!txtInvoiceDueDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            //{
            //    query.Where(query.InvoiceDueDate == txtInvoiceDueDate.SelectedDate);
            //}
            //if (!string.IsNullOrEmpty(txtInvoiceSuppNo.Text))
            //{
            //    if (cboFilterInvoiceSuppNo.SelectedIndex == 1)
            //        query.Where(por.InvoiceNo == txtInvoiceSuppNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceSuppNo.Text);
            //        query.Where(por.InvoiceNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtSupplierName.Text))
            //{
            //    if (cboFilterSupplierName.SelectedIndex == 1)
            //        query.Where(supp.SupplierName == txtSupplierName.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtSupplierName.Text);
            //        query.Where(supp.SupplierName.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtReceivedNo.Text))
            //{
            //    if (cboFilterReceivedNo.SelectedIndex == 1)
            //        query.Where(det.TransactionNo == txtReceivedNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtReceivedNo.Text);
            //        query.Where(det.TransactionNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtPurchaseOrderNo.Text))
            //{
            //    query.Where(por.ReferenceNo == txtPurchaseOrderNo.Text);
            //}

            //query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, supp.SupplierName, query.InvoiceSuppNo, query.IsApproved, query.IsVoid, sr.ItemName);

            //query.OrderBy(query.InvoiceNo.Descending);

            //Session[SessionNameForQuery] = query;
            //Session.Remove(SessionNameForList); //reset

            //SaveValueToCookie();

            //return true;
        }
    }
}
