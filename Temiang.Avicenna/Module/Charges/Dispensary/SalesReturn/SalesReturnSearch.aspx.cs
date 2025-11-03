using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesReturnSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SalesReturn;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("c");
            var cust = new CustomerQuery("b");
            var itemtype = new AppStandardReferenceItemQuery("d");
            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(cust).On(query.CustomerID == cust.CustomerID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.SalesReturn);

            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   query.ReferenceNo,
                   qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                   cust.CustomerName,
                   itemtype.ItemName,
                   query.IsApproved,
                   query.ReferenceNo,
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
            if (!string.IsNullOrEmpty(txtReferenceNo.Text))
            {
                if (cboFilterReferenceNo.SelectedIndex == 1)
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReferenceNo.Text);
                    query.Where(query.ReferenceNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboFilterCustomerID.SelectedIndex == 1)
                    cust.Where(cust.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerName.Text);
                    cust.Where(cust.CustomerName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtToServiceUnitID.Text))
            {
                if (cboFilterToServiceUnitID.SelectedIndex == 1)
                    query.Where(query.ToServiceUnitID == txtToServiceUnitID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtToServiceUnitID.Text);
                    query.Where(query.ToServiceUnitID.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
