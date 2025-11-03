using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryRecapitulationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaundryRecapitulation;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new LaundryRecapitulationProcessQuery("a");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    "<'LaundryRecapitulationDetail.aspx?md=view&id='+a.TransactionNo AS TxUrl>"
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
            if (!txtTransctionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransctionDate.SelectedDate);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}