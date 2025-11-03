using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionConfirmSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DistributionConfirm;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionConfirm, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromUnit, BusinessObject.Reference.TransactionCode.Distribution, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                cboCloseStatus.Items.Add(new RadComboBoxItem("", ""));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Void", "4"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var user = new AppUserServiceUnitQuery("e");
            var refq = new ItemTransactionQuery("f");
            var refunitq = new ServiceUnitQuery("g");

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(refq).On(query.ReferenceNo == refq.TransactionNo);
            query.InnerJoin(refunitq).On(refq.FromServiceUnitID == refunitq.ServiceUnitID);
            query.Where
                (
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionConfirm,
                    user.UserID == AppSession.UserLogin.UserID
                );
            
            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   query.ReferenceNo,
                   qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                   refunitq.ServiceUnitName.As("FromUnit"),
                   itemtype.ItemName,
                   query.IsApproved,
                   query.Notes,
                   query.IsVoid,
                   query.LastUpdateDateTime,
                   query.LastUpdateByUserID
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
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboFromUnit.SelectedValue))
            {
                query.Where(refq.FromServiceUnitID == cboFromUnit.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            if (cboCloseStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboCloseStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboCloseStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            query.OrderBy(query.FromServiceUnitID.Ascending, query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
