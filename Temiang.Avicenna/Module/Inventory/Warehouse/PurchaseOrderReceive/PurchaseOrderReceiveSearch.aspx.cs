using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveSearch : BasePageDialog
    {
        private string Cons
        {
            get
            {
                return Request.QueryString["cons"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Cons == "0"
                            ? AppConstant.Program.ReceivingOrder
                            : AppConstant.Program.ReceivingOrderConsignment;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var supp = new SupplierQuery("b");
            var qryserviceunit = new ServiceUnitQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");

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
            if (!string.IsNullOrEmpty(txtBusinessPartnerName.Text))
            {
                if (cboFilterBusinessPartnerID.SelectedIndex == 1)
                    query.Where(supp.SupplierName == txtBusinessPartnerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtBusinessPartnerName.Text);
                    query.Where(supp.SupplierName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFromServiceUnitID.Text))
            {
                if (cboFilterFromServiceUnitID.SelectedIndex == 1)
                    query.Where(qryserviceunit.ServiceUnitName == txtFromServiceUnitID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFromServiceUnitID.Text);
                    query.Where(qryserviceunit.ServiceUnitName.Like(searchTextContain));
                }
            }

            if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(supp).On(query.BusinessPartnerID == supp.SupplierID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReceive);
            if (Cons == "1")
                query.Where(query.IsConsignment == true);
                   
            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                query.ReferenceNo,
                supp.SupplierName,
                qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                itemtype.ItemName,
                query.IsApproved,
                query.ReferenceNo,
                query.Notes,
                query.IsVoid
                );

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
