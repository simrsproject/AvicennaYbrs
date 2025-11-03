using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Sales;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryCustomer = new CustomerQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var user = new AppUserServiceUnitQuery("e");

            query.InnerJoin(qryCustomer).On(qryCustomer.CustomerID == query.CustomerID);
            query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                     user.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(itemtype).On
                (
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                );
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.Sales);
            query.OrderBy(query.TransactionDate.Descending);

            query.Select
                (
                   query.TransactionNo,
                   query.TransactionDate,
                   qryCustomer.CustomerName,
                   itemtype.ItemName,
                   query.IsApproved,
                   query.Notes,
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
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboCustomerName.SelectedIndex == 1)
                    query.Where(qryCustomer.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerName.Text);
                    query.Where(qryCustomer.CustomerName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
