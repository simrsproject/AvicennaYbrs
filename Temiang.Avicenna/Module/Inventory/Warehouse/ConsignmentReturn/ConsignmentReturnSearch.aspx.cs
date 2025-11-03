using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentReturnSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ConsignmentReturn;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("c");
            var sup = new SupplierQuery("b");

            var itemtype = new AppStandardReferenceItemQuery("d");
            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
            query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentReturn);

            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   query.ReferenceNo,
                   qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                   sup.SupplierName,
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
                    string searchText = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchText));
                }
            }
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtBusinessPartnerName.Text))
            {
                if (cboFilterBusinessPartnerID.SelectedIndex == 1)
                    query.Where(sup.SupplierName == txtBusinessPartnerName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtBusinessPartnerName.Text);
                    query.Where(sup.SupplierName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtFromServiceUnitName.Text))
            {
                if (cboFilterFromServiceUnitName.SelectedIndex == 1)
                    query.Where(qryserviceunit.ServiceUnitName == txtFromServiceUnitName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtFromServiceUnitName.Text);
                    query.Where(qryserviceunit.ServiceUnitName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
