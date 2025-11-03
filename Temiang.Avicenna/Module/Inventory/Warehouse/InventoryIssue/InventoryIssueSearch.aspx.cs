using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.InventoryIssue;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.InventoryIssueOut, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.InventoryIssueOut, false);
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
            var itemtype = new AppStandardReferenceItemQuery("d");
            var user = new AppUserServiceUnitQuery("e");
            var tounit = new ServiceUnitQuery("f");

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                     user.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(itemtype).On
                (
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString()
                );
            query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueOut);
            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            query.Select
                (
                   query.TransactionNo,
                   query.TransactionDate,
                   qryserviceunit.ServiceUnitName,
                   tounit.ServiceUnitName.As("ToUnit"),
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
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
