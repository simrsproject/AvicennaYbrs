using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierContractSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SupplierContract;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SupplierContractQuery("a");
            var supp = new SupplierQuery("b");
            query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);
            query.Select(query, supp.SupplierName);
            query.OrderBy(query.ContractStart.Descending);
            
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
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                if (cboFilterSupplier.SelectedIndex == 1)
                    query.Where(supp.SupplierName == txtSupplier.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSupplier.Text);
                    query.Where(supp.SupplierName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
