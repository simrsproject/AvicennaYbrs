using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DistributionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdSterileItemsDistribution;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdDistributionQuery("a");
            var tounit = new ServiceUnitQuery("b");
            var usr = new AppUserQuery("c");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.TransactionTime,
                    tounit.ServiceUnitName.As("ToServiceUnitName"),
                    query.HandedByUserID,
                    usr.UserName.As("HandedBy"),
                    query.ReceivedBy,
                    query.IsApproved,
                    query.IsVoid
                );

            query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

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
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboToServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboToServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
    }
}