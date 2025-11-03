using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentReceiveSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ConsignmentReceive;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var supplierQuery = new SupplierQuery("s");

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.InnerJoin(supplierQuery).On(query.BusinessPartnerID == supplierQuery.SupplierID);

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchText));
                }
            }
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtToServiceUnitName.Text))
            {
                if (cboFilterToServiceUnitName.SelectedIndex == 1)
                    query.Where(qryserviceunit.ServiceUnitName == txtToServiceUnitName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtToServiceUnitName.Text);
                    query.Where(qryserviceunit.ServiceUnitName.Like(searchText));
                }
            }

            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentReceive);

            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   query.ReferenceNo,
                   qryserviceunit.ServiceUnitName,
                   itemtype.ItemName.As("ItemType"),
                   query.IsApproved,
                   supplierQuery.SupplierName,
                   query.Notes,
                   query.IsVoid
               );

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
