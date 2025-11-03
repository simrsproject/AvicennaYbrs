using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionRequestSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DistributionRequest;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.Distribution, false);
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
            var qryserviceunit = new ServiceUnitQuery("b");
            var qryserviceunitto = new ServiceUnitQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var qusr = new AppUserServiceUnitQuery("u");

            query.Select(
                    query.TransactionNo,
                    query.TransactionDate,
                    qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                    qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                    itemtype.ItemName,
                    query.IsApproved,
                    query.Notes,
                    query.IsVoid,
                   query.LastUpdateDateTime,
                   query.LastUpdateByUserID

                );

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.InnerJoin(qusr).On(query.FromServiceUnitID == qusr.ServiceUnitID &&
                                    qusr.UserID == AppSession.UserLogin.UserID);
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionRequest);

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
            if (!string.IsNullOrEmpty(cboSRItemType.Text))
            {
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            }
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "2")
                query.Where(query.IsClosed == false);
            if (cboStatus.SelectedValue == "3")
                query.Where(query.IsClosed == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            query.OrderBy(query.FromServiceUnitID.Ascending, query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
