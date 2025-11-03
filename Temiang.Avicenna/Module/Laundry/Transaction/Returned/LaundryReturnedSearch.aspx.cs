using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReturnedSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaundryReturned;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new LaundryReturnedQuery("a");
            var tounit = new ServiceUnitQuery("b");
            var usr = new AppUserQuery("c");

            query.Select
                (
                    query.ReturnNo,
                    query.ReturnDate,
                    query.ReturnTime,
                    tounit.ServiceUnitName.As("ToServiceUnitName"),
                    query.HandedByUserID,
                    usr.UserName.As("HandedBy"),
                    query.ReceivedBy,
                    query.IsApproved,
                    query.IsVoid,
                    "<'LaundryReturnedDetail.aspx?md=view&id='+a.ReturnNo AS RetUrl>"
                );

            query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

            if (!string.IsNullOrEmpty(txtReturnNo.Text))
            {
                if (cboFilterReturnNo.SelectedIndex == 1)
                    query.Where(query.ReturnNo == txtReturnNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReturnNo.Text);
                    query.Where(query.ReturnNo.Like(searchTextContain));
                }
            }
            if (!txtReturnDate.IsEmpty)
                query.Where(query.ReturnDate == txtReturnDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

            query.OrderBy(query.ReturnDate.Descending, query.ReturnNo.Descending);

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
