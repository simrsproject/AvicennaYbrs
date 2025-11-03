using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingAdditionalSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_INVOICING_ADDITIONAL;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new InvoicesQuery("a");
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

            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                if (cboFilterInvoiceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtInvoiceNo.Text);
                    query.Where(query.InvoiceNo.Like(searchTextContain));
                }
            }
            if (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty)
            {
                query.Where(query.InvoiceDate >= txtInvoiceDateFrom.SelectedDate, query.InvoiceDate < txtInvoiceDateTo.SelectedDate.Value.AddDays(1));
            }
            if (!txtInvoiceDueDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.InvoiceDueDate == txtInvoiceDueDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtGuarantorName.Text))
            {
                if (cboFilterGuarantorName.SelectedIndex == 1)
                    query.Where(guar.GuarantorName == txtGuarantorName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtGuarantorName.Text);
                    query.Where(guar.GuarantorName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
