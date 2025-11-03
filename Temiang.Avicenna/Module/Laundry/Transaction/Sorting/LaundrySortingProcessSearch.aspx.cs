using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundrySortingProcessSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaundrySortingProcess;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new LaundrySortingProcessQuery("a");
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.ProcessNo,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    @"<'LaundrySortingProcessDetail.aspx?md=view&id='+a.TransactionNo AS PUrl>"
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtProcessNo.Text))
            {
                if (cboFilterProcessNo.SelectedIndex == 1)
                    query.Where(query.ProcessNo == txtProcessNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProcessNo.Text);
                    query.Where(query.ProcessNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}