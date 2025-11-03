using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CustomerSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.CUSTOMER;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CustomerQuery();
            if (!string.IsNullOrEmpty(txtCustomerID.Text))
            {
                if (cboFilterCustomerID.SelectedIndex == 1)
                    query.Where(query.CustomerID == txtCustomerID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerID.Text);
                    query.Where(query.CustomerID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboFilterCustomerName.SelectedIndex == 1)
                    query.Where(query.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerName.Text);
                    query.Where(query.CustomerName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtContactPerson.Text))
            {
                if (cboFilterContactPerson.SelectedIndex == 1)
                    query.Where(query.ContactPerson == txtContactPerson.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtContactPerson.Text);
                    query.Where(query.ContactPerson.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCity.Text))
            {
                if (cboFilterCity.SelectedIndex == 1)
                    query.Where(query.City == txtCity.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCity.Text);
                    query.Where(query.City.Like(searchTextContain));
                }
            }
            query.OrderBy(query.CustomerID.Descending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
