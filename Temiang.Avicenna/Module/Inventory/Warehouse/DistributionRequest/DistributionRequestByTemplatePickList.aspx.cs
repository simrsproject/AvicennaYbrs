using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionRequestByTemplatePickList : BasePageDialog
    {
        public int GetInt(object o)
        {
            return o.ToInt();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DistributionRequest;

            var coll = new LocationTemplateCollection();
            coll.Query.Where(coll.Query.LocationID == Request.QueryString["lid"],
                coll.Query.IsActive == true);
            coll.LoadAll();
            cboTemplateNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var c in coll)
            {
                cboTemplateNo.Items.Add(new RadComboBoxItem(c.TemplateName, c.TemplateNo));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StoreEntryValue();
            }
            else
            {
                Session.Remove("DistRequest:Selection");
                Session.Remove("DistRequest:ItemSelected");
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
                row["QtyInput"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0);
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
            var query = new LocationTemplateItemQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("c");

            if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );

            }
            else if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            if (string.IsNullOrEmpty(cboTemplateNo.SelectedValue))
                query.Where(
                    query.TemplateNo == "XXX"
                );
            else
                query.Where(
                    query.TemplateNo == cboTemplateNo.SelectedValue
                );

            var itemGroupID = Page.Request.QueryString["igid"];
            if (!string.IsNullOrEmpty(itemGroupID))
                query.Where(qrItem.ItemGroupID == itemGroupID);

            query.Select(
                @"<0 AS IsSelect>",
                query.ItemID,
                @"<0 AS QtyInput>",
                qrItem.ItemName
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
                    row => (1.Equals(row["IsSelect"]) && Convert.ToDecimal(row["QtyInput"]) > 0)).CopyToDataTable();
            return true;
        }
        private DataTable RequestItemSelectedDataTable
        {
            set
            {
                Session["DistRequest:ItemSelected"] = value;
            }
        }
        private DataTable RequestItemSelectionDataTable
        {
            get
            {
                var result = Session["DistRequest:Selection"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["DistRequest:Selection"] = value;
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
    }
}