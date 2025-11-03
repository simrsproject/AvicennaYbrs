using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockOpnameSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.StockOpname;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.StockTaking, true);
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
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
            var user = new AppUserServiceUnitQuery("e");
            var Location = new LocationQuery("f");
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName.As("FromServiceUnitID"),
                    type.ItemName.As("SRItemType"),
                    Location.LocationName.As("FromLocationID"),
                    query.Notes,
                    "<CAST((CASE WHEN (SELECT COUNT(TransactionNo) FROM ItemStockOpnameApproval WHERE TransactionNo = a.TransactionNo AND IsApproved = 0) = 0 THEN 1 ELSE 0 END) AS BIT) AS IsApproved>",
                    query.IsVoid
                );
            query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(type).On
                (
                    query.SRItemType == type.ItemID &
                    type.StandardReferenceID == "ItemType"
                );
            query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(Location).On(Location.LocationID == query.FromLocationID);
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.StockTaking,
                        user.UserID == AppSession.UserLogin.UserID);

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
            if (!txtTransactionDateFrom.IsEmpty && !txtTransactionDateTo.IsEmpty)
                query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate.Value.Date, txtTransactionDateTo.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboLocationID.SelectedValue))
                query.Where(query.FromLocationID == cboLocationID.SelectedValue);
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);

            query.OrderBy(query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboLocationID.Items.Clear();
            cboLocationID.SelectedValue = string.Empty;
            cboLocationID.Text = string.Empty;
        }

        protected void cboLocationID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LocationQuery("l");
            var sul = new ServiceUnitLocationQuery("sul");
            var tcode = new ServiceUnitTransactionCodeQuery("tcode");
            var qusr = new AppUserServiceUnitQuery("u");
            query.InnerJoin(sul).On(query.LocationID == sul.LocationID);
            query.InnerJoin(tcode).On(tcode.ServiceUnitID == sul.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.StockTaking);
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

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(sul.ServiceUnitID == cboServiceUnitID.SelectedValue);

            cboLocationID.DataSource = query.LoadDataTable();
            cboLocationID.DataBind();
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }
    }
}
