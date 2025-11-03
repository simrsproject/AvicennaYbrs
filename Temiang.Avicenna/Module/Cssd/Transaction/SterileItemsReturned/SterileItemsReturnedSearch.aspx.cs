using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReturnedSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdSterileItemsReturned;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdSterileItemsReturnedQuery("a");
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
                  "<'SterileItemsReturnedDetail.aspx?md=view&id='+a.ReturnNo AS RetUrl>"
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

            if (txtReturnDate.SelectedDate.HasValue)
            {
                // Ambil tanggal tanpa waktu dari SelectedDate
                DateTime selectedDate = txtReturnDate.SelectedDate.Value.Date;

                // Lakukan pencarian dengan menggunakan rentang waktu dalam satu hari
                DateTime nextDay = selectedDate.AddDays(1);

                query.Where(query.ReturnDate >= selectedDate && query.ReturnDate < nextDay);
            }

            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                if (cboFilterToServiceUnitID.SelectedIndex == 1)
                {
                    //query.Where(query.InvoiceReferenceNo == txtInvoiceRefNo.Text);
                    query.Where(tounit.ServiceUnitID == cboToServiceUnitID.SelectedValue);
                }
                else
                {
                    //query.Where(query.InvoiceReferenceNo.Like(string.Format("%.{0}%", txtInvoiceRefNo.Text)));
                    string searchTextContain = string.Format("%{0}%", cboToServiceUnitID.SelectedValue);
                    query.Where(tounit.ServiceUnitID.Like(searchTextContain));
                }
            }

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
