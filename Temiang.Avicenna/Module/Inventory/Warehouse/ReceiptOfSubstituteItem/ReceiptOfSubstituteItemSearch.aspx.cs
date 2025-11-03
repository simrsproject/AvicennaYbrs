using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ReceiptOfSubstituteItemSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PurchaseOrderReturn;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.ReceiptOfSubstitute, true);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "4"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("c");
            var sup = new SupplierQuery("b");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var qusr = new AppUserServiceUnitQuery("u");

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                    qusr.UserID == AppSession.UserLogin.UserID);

            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ReceiptOfSubstitute);

            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   query.ReferenceNo,
                   qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
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
                    query.Where(sup.SupplierName == txtBusinessPartnerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtBusinessPartnerName.Text);
                    query.Where(sup.SupplierName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRItemType.Text))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }

            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
