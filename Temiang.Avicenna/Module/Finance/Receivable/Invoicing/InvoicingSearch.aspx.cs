using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string InvoiceNo;
            public DateTime? InvoiceDate;
            public DateTime? InvoiceDateTo;
            public DateTime? InvoiceDueDate;
            public string GuarantorName;
            public string PaymentNo;
            public string RegistrationNo;
            public string MedicalNo;
            public string PatientName;
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_INVOICING;
            
            if (Session[SessionNameForQuery] != null)
            {
                InvoicingSearch.SearchValue sv = (InvoicingSearch.SearchValue)Session[SessionNameForQuery];
                txtInvoiceNo.Text = sv.InvoiceNo;
                txtInvoiceDateFrom.SelectedDate = sv.InvoiceDate;
                txtInvoiceDateTo.SelectedDate = sv.InvoiceDateTo;
                txtInvoiceDueDate.SelectedDate = sv.InvoiceDueDate;
                txtGuarantorName.Text = sv.GuarantorName;
                txtPaymentNo.Text = sv.PaymentNo;
                txtRegistrationNo.Text = sv.RegistrationNo;
                txtMedicalNo.Text = sv.MedicalNo;
                txtPatientName.Text = sv.PatientName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue sv = new SearchValue();

            sv.InvoiceNo = txtInvoiceNo.Text;
            sv.InvoiceDate = txtInvoiceDateFrom.SelectedDate;
            sv.InvoiceDateTo = txtInvoiceDateTo.SelectedDate;
            sv.InvoiceDueDate = txtInvoiceDueDate.SelectedDate;
            sv.GuarantorName = txtGuarantorName.Text;
            sv.PaymentNo = txtPaymentNo.Text;
            sv.RegistrationNo = txtRegistrationNo.Text;
            sv.MedicalNo = txtMedicalNo.Text;
            sv.PatientName = txtPatientName.Text;
            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;


            //var query = new InvoicesQuery("a");
            //var guar = new GuarantorQuery("b");
            //var sr = new AppStandardReferenceItemQuery("c");
            //var itm = new InvoicesItemQuery("d");

            //query.LeftJoin(itm).On(itm.InvoiceNo == query.InvoiceNo);
            //query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            //query.LeftJoin(sr).On(
            //    query.SRReceivableStatus == sr.ItemID &&
            //    sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
            //    );

            //query.Select(
            //       query.InvoiceNo,
            //       query.InvoiceDate,
            //       query.InvoiceDueDate,
            //       guar.GuarantorName,
            //       query.IsApproved,
            //       query.IsVoid,
            //       sr.ItemName.As("ReceivableStatusName"),
            //       "<SUM(ISNULL(d.Amount,0)) AS TotalTransaction>",
            //       "<ISNULL(a.DiscountAmount, 0) AS DiscountAmount>",
            //       "<SUM(ISNULL(d.Amount,0)) - ISNULL(a.DiscountAmount, 0) AS TotalAmount>",
            //       @"<DATEDIFF(DAY, GETDATE(), a.InvoiceDueDate) AS Aging>" 
            //   );

            //query.Where(query.IsInvoicePayment == false, query.InvoiceReferenceNo.IsNull(), query.Or(query.IsAdditionalInvoice.IsNull(), query.IsAdditionalInvoice == false));
            //query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, guar.GuarantorName,
            //              query.IsApproved, query.IsVoid, sr.ItemName, query.DiscountAmount);

            //query.OrderBy(query.InvoiceNo.Descending);

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
            //if (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty)
            //{
            //    query.Where(query.InvoiceDate >= txtInvoiceDateFrom.SelectedDate.Value.Date, query.InvoiceDate < txtInvoiceDateTo.SelectedDate.Value.AddDays(1));
            //}
            //if (!txtInvoiceDueDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            //{
            //    query.Where(query.InvoiceDueDate >= txtInvoiceDueDate.SelectedDate, query.InvoiceDueDate < txtInvoiceDueDate.SelectedDate.Value.AddDays(1));
            //}
            //if (!string.IsNullOrEmpty(txtGuarantorName.Text))
            //{
            //    if (cboFilterGuarantorName.SelectedIndex == 1)
            //        query.Where(guar.GuarantorName == txtGuarantorName.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtGuarantorName.Text);
            //        query.Where(guar.GuarantorName.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtPaymentNo.Text))
            //{
            //    if (cboFilterPaymentNo.SelectedIndex == 1)
            //        query.Where(itm.PaymentNo == txtPaymentNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtPaymentNo.Text);
            //        query.Where(itm.PaymentNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            //{
            //    if (cboFilterRegistrationNo.SelectedIndex == 1)
            //        query.Where(itm.RegistrationNo == txtRegistrationNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
            //        query.Where(itm.RegistrationNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            //{
            //    var pat = new PatientQuery("pat");
            //    query.InnerJoin(pat).On(itm.PatientID == pat.PatientID);
            //    if (cboFilterMedicalNo.SelectedIndex == 1)
            //        query.Where(pat.MedicalNo == txtMedicalNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtMedicalNo.Text);
            //        query.Where(pat.MedicalNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtPatientName.Text))
            //{
            //    if (cboFilterPatientName.SelectedIndex == 1)
            //        query.Where(itm.PatientName == txtPatientName.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
            //        query.Where(itm.PatientName.Like(searchTextContain));
            //    }
            //}

            //Session[SessionNameForQuery] = query;
            //Session.Remove(SessionNameForList); //reset

            //return true;
        }
    }
}
