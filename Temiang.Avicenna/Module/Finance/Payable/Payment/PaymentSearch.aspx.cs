using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class PaymentSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string InvoiceNo;
            public DateTime? InvoiceDate;
            public DateTime? InvoiceDateTo;
            public string InvoiceReferenceNo;
            public string SupplierName;
            public string InvoiceSuppNo;
            public string Status;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_PAYMENT;

            if (Session[SessionNameForQuery] != null)
            {
                PaymentSearch.SearchValue sv = (PaymentSearch.SearchValue)Session[SessionNameForQuery];
                txtInvoiceNo.Text = sv.InvoiceNo;
                txtInvoiceDate.SelectedDate = sv.InvoiceDate;
                txtInvoiceDateTo.SelectedDate = sv.InvoiceDateTo;
                txtInvoiceRefNo.Text = sv.InvoiceReferenceNo;
                txtInvoiceSupplierNo.Text = sv.InvoiceSuppNo;
                txtSupplierName.Text = sv.SupplierName;
                cboStatus.SelectedValue = sv.Status;
            }

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Non Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "2"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue sv = new SearchValue();

            sv.InvoiceNo = txtInvoiceNo.Text;
            sv.InvoiceDate = txtInvoiceDate.SelectedDate;
            sv.InvoiceDateTo = txtInvoiceDateTo.SelectedDate;
            sv.InvoiceReferenceNo = txtInvoiceRefNo.Text;
            sv.InvoiceSuppNo = txtInvoiceSupplierNo.Text;
            sv.SupplierName = txtSupplierName.Text;
            sv.Status = cboStatus.SelectedValue;

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;


            //var query = new InvoiceSupplierQuery("a");
            //var isi = new InvoiceSupplierItemQuery("isi");
            //var supp = new SupplierQuery("b");

            //query.InnerJoin(isi).On(query.InvoiceNo == isi.InvoiceNo);

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
            //if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtInvoiceDateTo.SelectedDate.ToString().Trim().Equals(string.Empty))
            //{
            //    query.Where(query.InvoiceDate >= txtInvoiceDate.SelectedDate, query.InvoiceDate <= txtInvoiceDateTo.SelectedDate);
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
            //if (!string.IsNullOrEmpty(txtInvoiceRefNo.Text))
            //{
            //    if (cboFilterInvoiceRefNo.SelectedIndex == 1)
            //    {
            //        //query.Where(query.InvoiceReferenceNo == txtInvoiceRefNo.Text);
            //        query.Where(isi.InvoiceReferenceNo == txtInvoiceRefNo.Text);
            //    }
            //    else
            //    {
            //        //query.Where(query.InvoiceReferenceNo.Like(string.Format("%.{0}%", txtInvoiceRefNo.Text)));
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceRefNo.Text);
            //        query.Where(isi.InvoiceReferenceNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtInvoiceSupplierNo.Text))
            //{
            //    var porq = new ItemTransactionQuery("por");
            //    query.InnerJoin(porq).On(isi.TransactionNo == porq.TransactionNo);

            //    if (cboFilterInvoiceSupplierNo.SelectedIndex == 1)
            //    {
            //        //query.Where(query.InvoiceReferenceNo == txtInvoiceRefNo.Text);
            //        query.Where(porq.InvoiceNo == txtInvoiceSupplierNo.Text);
            //    }
            //    else
            //    {
            //        //query.Where(query.InvoiceReferenceNo.Like(string.Format(".%{0}%", txtInvoiceRefNo.Text)));
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceSupplierNo.Text);
            //        query.Where(porq.InvoiceNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            //{
            //    switch (cboStatus.SelectedValue)
            //    {
            //        case "0":
            //            query.Where(query.IsApproved == true);
            //            break;
            //        case "1":
            //            query.Where(query.Or(query.IsApproved.IsNull(), query.IsApproved == false), query.Or(query.IsVoid.IsNull(), query.IsVoid == false));
            //            break;
            //        case "2":
            //            query.Where(query.IsVoid == true);
            //            break;
            //    }
            //}

            //query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);

            //query.Select(
            //       query.InvoiceNo,
            //       query.InvoiceReferenceNo,
            //       query.InvoiceDate,
            //       supp.SupplierName,
            //       query.IsApproved,
            //       query.IsVoid,
            //       "<SUM(isi.PaymentAmount) AS Total>"
            //   );

            //query.Where(query.IsInvoicePayment == true, query.IsWriteOff == false, query.IsAddPayment.IsNull());
            //query.GroupBy(query.InvoiceNo,
            //               query.InvoiceReferenceNo,
            //               query.InvoiceDate,
            //               supp.SupplierName,
            //               query.IsApproved,
            //               query.IsVoid);
            //query.OrderBy(query.InvoiceNo.Descending);

            //query.es.Distinct = true;

            //Session[SessionNameForQuery] = query;
            //Session.Remove(SessionNameForList); //reset

            //return true;
        }
    }
}
