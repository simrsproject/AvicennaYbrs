using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentTransferSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ConsignmentTransfer;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.ConsignmentTransfer, false);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var itype = new AppStandardReferenceItemQuery("d");
            var supp = new SupplierQuery("s");
            var tsu = new ServiceUnitQuery("c");
            var floc = new LocationQuery("floc");
            var tloc = new LocationQuery("tloc");

            query.LeftJoin(itype).On(itype.ItemID == query.SRItemType && itype.StandardReferenceID == "ItemType");
            query.InnerJoin(supp).On(query.BusinessPartnerID == supp.SupplierID);
            query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(floc).On(query.FromLocationID == floc.LocationID);
            query.InnerJoin(tloc).On(query.ToLocationID == tloc.LocationID);

            query.Select(
                   query.TransactionNo,
                   query.TransactionDate,
                   itype.ItemName.As("ItemType"),
                   supp.SupplierName,
                   floc.LocationName.As("FromLocationName"),
                   tsu.ServiceUnitName.As("ToServiceUnitName"),
                   tloc.LocationName.As("ToLocationName"),
                   query.Notes,
                   query.IsApproved,
                   query.IsVoid
               );

            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.ConsignmentTransfer);

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
            if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
            {
                query.Where(query.BusinessPartnerID == cboSupplierID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSupplierID((RadComboBox)sender, e.Text);
        }

        private static void PopulateCboSupplierID(BaseDataBoundControl comboBox, string textSearch)
        {
            string searchText = string.Format("%{0}%", textSearch);
            var query = new SupplierQuery();

            query.Select(
                query.SupplierID,
                query.SupplierName,
                (query.StreetName + " " + query.City + " " + query.ZipCode).Trim().As("Address")
                );
            query.Where(
                query.Or(
                    query.SupplierID == textSearch,
                    query.SupplierName.Like(searchText)
                    ), query.IsActive == true
                );

            query.es.Top = 20;

            comboBox.DataSource = query.LoadDataTable();
            comboBox.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }
    }
}
