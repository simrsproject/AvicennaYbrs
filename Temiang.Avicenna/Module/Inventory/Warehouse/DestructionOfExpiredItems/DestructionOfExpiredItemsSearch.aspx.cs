using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DestructionOfExpiredItemsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DestructionOfExpiredItems;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DestructionOfExpiredItems, true);
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "4"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var unit = new ServiceUnitQuery("b");
            var type = new AppStandardReferenceItemQuery("d");
            var loc = new LocationQuery("e");
            var usr = new AppUserServiceUnitQuery("u");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName.As("FromServiceUnitID"),
                    loc.LocationName,
                    type.ItemName.As("SRItemType"),
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
                );
            query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(loc).On(query.FromLocationID == loc.LocationID);
            query.InnerJoin(type).On
                (
                    query.SRItemType == type.ItemID &
                    type.StandardReferenceID == "ItemType"
                );
            query.InnerJoin(usr).On(query.FromServiceUnitID == usr.ServiceUnitID &
                            usr.UserID == AppSession.UserLogin.UserID);

            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.DestructionOfExpiredItems);
            
            if (!txtTransactionDateFrom.IsEmpty && !txtTransactionDateTo.IsEmpty)
                query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate.Value.Date, txtTransactionDateTo.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            query.OrderBy(query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
