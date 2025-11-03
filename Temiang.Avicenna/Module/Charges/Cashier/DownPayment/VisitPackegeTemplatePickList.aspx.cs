using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class VisitPackegeTemplatePickList : BasePageDialog
    {
        public int GetInt(object o)
        {
            return o.ToInt();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DownPayment;

            if (!IsPostBack)
            { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StoreEntryValue();
            }
            else
            {
                Session.Remove("DownPaymentItemVisite:Selection");
                Session.Remove("DownPaymentItemVisite:ItemSelected");
            }
        }

        private void StoreEntryValue()
        {
            var dtb = RequestItemSelectionDataTable;
            if (dtb == null) return;
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                var itemId = dataItem["ItemID"].Text;
                var row = dtb.Rows.Find(itemId);
                row["IsSelect"] = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                row["Qty"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0);
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack)
            {
                RequestItemSelectionDataTable = LoadRequestItemSelectionDataTable();
            }
            grdDetail.DataSource = RequestItemSelectionDataTable;
        }

        private DataTable LoadRequestItemSelectionDataTable()
        {
            var query = new VisitPackageItemQuery("a");
            var qrHd = new VisitPackageQuery("b");
            var qrUnit = new ServiceUnitQuery("c");
            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrHd).On(qrHd.VisitPackageID == query.VisitPackageID);
            query.InnerJoin(qrUnit).On(qrUnit.ServiceUnitID == qrHd.ServiceUnitID);
            query.InnerJoin(qrItem).On(qrItem.ItemID == query.ItemID);

            if (string.IsNullOrEmpty(cboVisitPackageID.SelectedValue))
                query.Where(
                    query.VisitPackageID == "XXX"
                );
            else
                query.Where(
                    query.VisitPackageID == cboVisitPackageID.SelectedValue
                );

            var itemGroupID = Page.Request.QueryString["igid"];
            if (!string.IsNullOrEmpty(itemGroupID))
                query.Where(qrItem.ItemGroupID == itemGroupID);

            query.Select(
                @"<1 AS IsSelect>",
                query.VisitPackageID,
                qrHd.ServiceUnitID,
                qrUnit.ServiceUnitName,
                query.ItemID,
                qrItem.ItemName,
                query.Qty
                );

            query.OrderBy(qrItem.ItemName.Ascending);

            var dtb = query.LoadDataTable();
            dtb.PrimaryKey = new[] { dtb.Columns["ItemID"] };
            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            RequestItemSelectedDataTable =
                RequestItemSelectionDataTable.Select().Where(
                    row => (1.Equals(row["IsSelect"]) && Convert.ToDecimal(row["Qty"]) > 0)).CopyToDataTable();
            return true;
        }
        private DataTable RequestItemSelectedDataTable
        {
            set
            {
                Session["DownPaymentItemVisite:ItemSelected"] = value;
            }
        }
        private DataTable RequestItemSelectionDataTable
        {
            get
            {
                var result = Session["DownPaymentItemVisite:Selection"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["DownPaymentItemVisite:Selection"] = value;
            }
        }
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            //StoreEntryValue();
            var dtb = RequestItemSelectionDataTable;
            var value = ((CheckBox)sender).Checked;
            foreach (DataRow row in dtb.Rows)
            {
                row["IsSelect"] = value;
            }
            grdDetail.Rebind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            RequestItemSelectionDataTable = LoadRequestItemSelectionDataTable();
            grdDetail.Rebind();
        }

        protected void cboVisitPackageID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["VisitPackageName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["VisitPackageID"].ToString();
        }

        protected void cboVisitPackageID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new  VisitPackageQuery("a");
            query.es.Top = 20;
            query.Where(
                    query.VisitPackageName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.VisitPackageName.Ascending);

            cboVisitPackageID.DataSource = query.LoadDataTable();
            cboVisitPackageID.DataBind();
        }
    }
}