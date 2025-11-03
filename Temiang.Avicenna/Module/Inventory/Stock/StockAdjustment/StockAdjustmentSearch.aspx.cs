using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockAdjustmentSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "p" ? AppConstant.Program.StockAdjustmentPlus : AppConstant.Program.StockAdjustment; 

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                StandardReference.InitializeIncludeSpace(cboSRAdjustmentType, AppEnum.StandardReference.AdjustmentType);
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.StockAdjustment, true);
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
            var adjustment = new AppStandardReferenceItemQuery("e");
            var loc = new LocationQuery("f");
            var usr = new AppUserServiceUnitQuery("u");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName.As("FromServiceUnitID"),
                    loc.LocationName.As("FromLocationID"),
                    type.ItemName.As("SRItemType"),
                    adjustment.ItemName.As("SRAdjustmentType"),
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
            query.InnerJoin(adjustment).On
                (
                    query.SRAdjustmentType == adjustment.ItemID &
                    adjustment.StandardReferenceID == "AdjustmentType"
                );
            query.InnerJoin(usr).On(query.FromServiceUnitID == usr.ServiceUnitID &
                                    usr.UserID == AppSession.UserLogin.UserID);

            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.StockAdjustment);
            if (!string.IsNullOrEmpty(FormType))
            {
                var iti = new ItemTransactionItemQuery("iti");
                query.InnerJoin(iti).On(query.TransactionNo == iti.TransactionNo);
                if (FormType == "p")
                    query.Where(iti.Quantity > 0);
                else query.Where(iti.Quantity < 0);
                query.GroupBy(
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    loc.LocationName,
                    type.ItemName,
                    adjustment.ItemName,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid);
            }

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!txtTransactionDateFrom.IsEmpty && !txtTransactionDateTo.IsEmpty)
                query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate.Value.Date, txtTransactionDateTo.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRAdjustmentType.SelectedValue))
                query.Where(query.SRAdjustmentType == cboSRAdjustmentType.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
                query.Where(query.FromLocationID == cboFromLocationID.SelectedValue);
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

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromLocationID.Items.Clear();
            cboFromLocationID.SelectedValue = string.Empty;
            cboFromLocationID.Text = string.Empty;
        }

        protected void cboFromLocationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery("l");
            var sul = new ServiceUnitLocationQuery("sul");
            var tcode = new ServiceUnitTransactionCodeQuery("tcode");
            var qusr = new AppUserServiceUnitQuery("u");
            query.InnerJoin(sul).On(query.LocationID == sul.LocationID);
            query.InnerJoin(tcode).On(tcode.ServiceUnitID == sul.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.StockAdjustment);
            query.InnerJoin(qusr).On(sul.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);

            query.es.Distinct = true;
            query.es.Top = 10;
            query.Select
                (
                    query.LocationID,
                    query.LocationName
                );
            query.Where
                (
                    query.LocationName.Like(searchTextContain),
                    query.IsActive == true
                );

            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(sul.ServiceUnitID == cboFromServiceUnitID.SelectedValue);

            cboFromLocationID.DataSource = query.LoadDataTable();
            cboFromLocationID.DataBind();
        }

        protected void cboFromLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }
    }
}
