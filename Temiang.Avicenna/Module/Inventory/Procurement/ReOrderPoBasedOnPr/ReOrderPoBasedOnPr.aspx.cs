using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class ReOrderPoBasedOnPr : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ReOrderPoBasedOnPr;
            if (!IsPostBack)
            {
                // Reset datasource grid
                ItemBalances = null;

                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.PurchaseRequest, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.PurchaseOrder, true);

                if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboToServiceUnitID.SelectedValue = Request.QueryString["su"];
                else
                    cboToServiceUnitID.Text = string.Empty;

                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);

                if (!string.IsNullOrEmpty(Request.QueryString["it"]))
                    cboSRItemType.SelectedValue = Request.QueryString["it"];
                else
                    cboSRItemType.Text = string.Empty;

                cboFromServiceUnitID.Text = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StoreEntryValue();
            }
        }
        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRItemType.Items.Clear();
            cboSRItemType.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);
        }

        private DataTable PopulateSupplier()
        {
            if (Session["supplier"] != null)
                return Session["supplier"] as DataTable;

            var supp = new SupplierQuery("b");
            supp.Select(
                "<b.SupplierID + ';' + CASE WHEN b.IsPKP = 1 THEN 'true' ELSE 'false' END + ';' + CAST(b.TaxPercentage AS VARCHAR(MAX)) AS SupplierID>",
                supp.SupplierID.As("SupplierIDFilter"),
                supp.SupplierName
                );
            supp.Where(supp.IsActive == true);
            var dtb = supp.LoadDataTable();
            dtb.PrimaryKey = new[] { dtb.Columns["SupplierIDFilter"] };
            Session["supplier"] = dtb;
            return dtb;
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.DataItem != null)
            {
                var dataRow = ((System.Data.DataRowView)(e.Item.DataItem)).Row;

                if (dataRow == null) return;

                // Populate ItemUnit
                var cboItemUnitSelected = (e.Item.FindControl("cboItemUnitSelected") as RadComboBox);
                if (cboItemUnitSelected != null)
                {
                    cboItemUnitSelected.Items.Add(new RadComboBoxItem(dataRow["SRPurchaseUnit"].ToString(), dataRow["SRPurchaseUnit"].ToString()));
                    cboItemUnitSelected.Items.Add(new RadComboBoxItem(dataRow["SRItemUnit"].ToString(), dataRow["SRItemUnit"].ToString()));

                    var itemUnitSelected = dataRow["SRItemUnitSelected"].ToString();
                    if (!string.IsNullOrEmpty(itemUnitSelected) && itemUnitSelected != "&nbsp;")
                        cboItemUnitSelected.SelectedValue = itemUnitSelected;
                    else
                        cboItemUnitSelected.SelectedIndex = 0;
                }


                // Populate SupplierID
                var suppId = dataRow["SupplierID"].ToString();
                if (!string.IsNullOrEmpty(suppId) && suppId != "&nbsp;")
                {
                    var cboSupplierId = (e.Item.FindControl("cboSupplierID") as RadComboBox);
                    var findRow = PopulateSupplier().Rows.Find(suppId);
                    if (findRow != null)
                    {
                        if (cboSupplierId != null)
                        {
                            cboSupplierId.Items.Add(new RadComboBoxItem(findRow["SupplierName"].ToString(), findRow["SupplierID"].ToString()));
                            cboSupplierId.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                if (ItemBalances == null)
                {
                    ItemBalances = PopulateItemBalances();
                }
                grdList.DataSource = ItemBalances;
            }
            else
                grdList.DataSource = new string[] { };
        }

        private void StoreEntryValue()
        {
            var dtb = ItemBalances;
            if (dtb == null) return;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                var rowID = dataItem["RowID"].Text;
                var row = dtb.Rows.Find(rowID);
                var supplierSelected = ((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue;
                var itemUnitSelected = ((RadComboBox)dataItem.FindControl("cboItemUnitSelected")).SelectedValue;
                var supplierID = string.Empty;
                var isPkp = false;
                decimal taxPercentage = 0;
                if (!string.IsNullOrEmpty(supplierSelected))
                {
                    var values = supplierSelected.Split(';');
                    supplierID = values[0];
                    isPkp = Convert.ToBoolean(values[1]);
                    taxPercentage = Convert.ToDecimal(values[2]);
                }
                if (!string.IsNullOrEmpty(itemUnitSelected) && itemUnitSelected != "&nbsp;")
                    row["SRItemUnitSelected"] = itemUnitSelected;
                else
                    row["SRItemUnitSelected"] = string.Empty;

                row["IsSelect"] = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                row["IsPKP"] = isPkp;
                row["SupplierID"] = supplierID;
                row["TaxPercentage"] = taxPercentage;
                row["QtyOrder"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0);
            }

        }

        private DataTable ItemBalances
        {
            get
            {
                var result = Session["ib"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["ib"] = value;
            }
        }

        private DataTable PopulateItemBalances()
        {
            var su = new ServiceUnit();
            var fromLocationId = su.GetMainLocationId(cboFromServiceUnitID.SelectedValue);
            var toLocationId = su.GetMainLocationId(cboToServiceUnitID.SelectedValue);

            var itemType = cboSRItemType.SelectedValue;
            var query = new ItemTransactionItemQuery("a");
            var qhd = new ItemTransactionQuery("b");
            var qfunit = new ServiceUnitQuery("c");
            var qtunit = new ServiceUnitQuery("d");
            var qfib = new ItemBalanceQuery("e");
            var qtib = new ItemBalanceQuery("f");
            var qi = new ItemQuery("g");

            // QtyOrder dalam satuan PurchaseUnit yg dibulatkan keatas dari Outstanding PR
            query.Select(
                @"<1000000 AS RowID, 0 AS IsSelect,  0 AS IsPKP, 000.00 AS TaxPercentage>",
                qhd.TransactionNo,
                qfunit.ServiceUnitName.As("FromUnit"),
                query.SequenceNo,
                query.ItemID,
                qi.ItemName,
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                @"<(a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit AS QtyOutstanding>",
                @"<CEILING(((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit)/x.ConversionFactor) AS QtyOrder>",
                @"<ISNULL(e.Balance, 0) AS FromBalance>",
                @"<ISNULL(f.Balance, 0) AS ToBalance>",
                @"<x.SRPurchaseUnit + '/' + CAST(x.ConversionFactor AS VARCHAR(MAX)) AS Unit>",
                @"<ISNULL((SELECT TOP 1 si.SupplierID FROM SupplierItem si WHERE si.ItemID = a.ItemID ORDER BY si.LastUpdateDateTime DESC), '') AS SupplierID>"
                );
            query.InnerJoin(qhd).On(query.TransactionNo == qhd.TransactionNo &&
                                    qhd.TransactionCode == TransactionCode.PurchaseRequest && qhd.IsApproved == true &&
                                    qhd.SRItemType == itemType && qhd.IsClosed == false && query.IsClosed == false);
            query.InnerJoin(qfunit).On(qhd.FromServiceUnitID == qfunit.ServiceUnitID);
            query.InnerJoin(qtunit).On(qhd.ToServiceUnitID == qtunit.ServiceUnitID);
            query.LeftJoin(qfib).On(query.ItemID == qfib.ItemID && qfib.LocationID == fromLocationId);
            query.LeftJoin(qtib).On(query.ItemID == qtib.ItemID && qtib.LocationID == toLocationId);
            query.InnerJoin(qi).On(query.ItemID == qi.ItemID);

            if (itemType == ItemType.Medical)
            {
                var qipm = new ItemProductMedicQuery("x");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID && qipm.IsConsignment == false);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.SRPurchaseUnit.As("SRItemUnitSelected"), qipm.ConversionFactor);
            }
            else if (itemType == ItemType.NonMedical)
            {
                var qipm = new ItemProductNonMedicQuery("x");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID && qipm.IsConsignment == false);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.SRPurchaseUnit.As("SRItemUnitSelected"), qipm.ConversionFactor);
            }
            else
            {
                var qipm = new ItemKitchenQuery("x");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit, qipm.SRPurchaseUnit, qipm.SRPurchaseUnit.As("SRItemUnitSelected"), qipm.ConversionFactor);
            }

            query.Where("<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit) > 0>");

            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                query.Where(qhd.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            }

            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(qhd.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }

            query.OrderBy(query.ItemID.Ascending);

            var tbl = query.LoadDataTable();
            var i = 0;
            foreach (DataRow row in tbl.Rows)
            {
                i++;
                row[0] = i;
            }
            // Add index
            tbl.PrimaryKey = new[] { tbl.Columns["RowID"] };
            return tbl;
        }


        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            //StoreEntryValue();
            var dtb = ItemBalances;
            var value = ((CheckBox)sender).Checked;
            foreach (DataRow row in dtb.Rows)
            {
                row["IsSelect"] = value;
            }
            grdList.Rebind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (o as RadComboBox);
            if (cbo != null)
            {
                cbo.DataValueField = "SupplierID";
                cbo.DataTextField = "SupplierName";
                cbo.DataSource = PopulateSupplier(e.Text);
                cbo.DataBind();
            }
        }

        private DataTable PopulateSupplier(string supplierName)
        {
            if (!string.IsNullOrEmpty(supplierName))
            {
                DataView dv = PopulateSupplier().DefaultView;
                dv.RowFilter = "SupplierName LIKE '%" + supplierName + "%'";
                return dv.ToTable();
            }

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("SupplierID"));
            dtb.Columns.Add(new DataColumn("SupplierName"));
            dtb.PrimaryKey = new[] { dtb.Columns["SupplierID"] };
            return dtb;
        }

        private bool CheckRequiredEntry()
        {
            var msg = string.Empty;
            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                msg = "Purchasing Unit";
            }
            if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                if (string.IsNullOrEmpty(msg))
                    msg = "Item Type";
                else
                    msg = string.Concat(msg, " & ", "Item Type required");
            }
            if (!string.IsNullOrEmpty(msg))
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", string.Format("alert('{0} required.');", msg), true);

            return string.IsNullOrEmpty(msg);
        }
        protected void lnkCalculate_OnClick(object sender, EventArgs e)
        {
            this.Validate("query");
            if (!CheckRequiredEntry()) return;
            if (!IsValid) return;

            // Populate Record
            ItemBalances = null;
            grdList.Rebind();
        }

        protected void lnkConfirmPo_OnClick(object sender, EventArgs e)
        {
            if (!CheckRequiredEntry()) return;

            // Reset selected item
            Session["ibs"] = null;

            // Show Confirm PO
            var fromSu = cboFromServiceUnitID.SelectedValue;
            var toSu = cboToServiceUnitID.SelectedValue;
            var it = cboSRItemType.SelectedValue;
            var url = "ReOrderPoBasedOnPrConfirm.aspx?fromSu=" + fromSu + "&toSu=" + toSu + "&it=" + it;
            OpenRadWindow(winConfirmPo, url, "confirmPo", true);
        }

        private void OpenRadWindow(RadWindow radWindow, string url, string scriptKey, bool isMaximize)
        {
            string script = string.Concat("function f(){var oWnd = $find(\"" + radWindow.ClientID + "\");oWnd.setUrl('", url, "'); oWnd.show();",isMaximize? "oWnd.Maximize();": string.Empty, "Sys.Application.remove_load(f);}Sys.Application.add_load(f);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), scriptKey, script, true);
        }
    }
}
