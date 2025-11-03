using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "rec" ? AppConstant.Program.SanitationWasteReceipt : AppConstant.Program.SanitationWasteDisposal;

            if (!IsPostBack)
            {
                var unitColl = new ServiceUnitCollection();
                unitColl.Query.Where(unitColl.Query.IsActive == true);
                unitColl.LoadAll();

                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var unit in unitColl)
                {
                    cboFromServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }

                trFromServiceUnitID.Visible = getPageID == "rec";
                trSupplierID.Visible = getPageID == "dis";
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SanitationWasteTransQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var supp = new SupplierQuery("c");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    fromunit.ServiceUnitName.As("FromServiceUnitName"),
                    supp.SupplierName,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid
                );

            query.LeftJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
            query.LeftJoin(supp).On(supp.SupplierID == query.SupplierID);

            if (getPageID == "rec")
            {
                query.Select(@"<'SanitationWasteDetail.aspx?md=view&id='+a.TransactionNo+'&type=rec' AS RUrl>");
                query.Where(query.TransactionCode == "R");
            }
            else
            {
                query.Select(@"<'SanitationWasteDetail.aspx?md=view&id='+a.TransactionNo+'&type=dis' AS RUrl>");
                query.Where(query.TransactionCode == "D");
            }

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

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
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                query.Where(query.SupplierID == cboSupplierID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text,
                query.SupplierName.Like(searchTextContain)),
                query.IsActive == true
                );
           
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

    }
}