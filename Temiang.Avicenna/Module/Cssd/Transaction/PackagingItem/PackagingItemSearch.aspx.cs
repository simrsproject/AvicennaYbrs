using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class PackagingItemSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdPackagingItem;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdPackagingQuery("a");
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
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
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}