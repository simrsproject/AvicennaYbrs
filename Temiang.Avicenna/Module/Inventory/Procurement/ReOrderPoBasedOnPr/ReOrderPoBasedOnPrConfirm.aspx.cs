using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class ReOrderPoBasedOnPrConfirm : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                StoreEntryValue();
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
            return dtb;

        }
        private DataTable PopulateSupplier()
        {
            if (Session["supplier"] != null)
                return Session["supplier"] as DataTable;

            var supp = new SupplierQuery("b");
            supp.Select(
                "<b.SupplierID + ';' + CASE WHEN b.IsPKP = 1 THEN 'true' ELSE 'false' END + ';' + CAST(b.TaxPercentage AS VARCHAR(MAX)) AS SupplierID>",
                supp.SupplierID.As("SuppliedIDFilter"),
                supp.SupplierName
                );
            supp.Where(supp.IsActive == true);
            var dtb = supp.LoadDataTable();
            Session["supplier"] = dtb;
            return dtb;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ItemBalanceSelectedDataTable == null)
            {
                ItemBalanceSelectedDataTable = PopulateItemBalanceSelectedDataTable();
            }
            grdList.DataSource = ItemBalanceSelectedDataTable;
        }

        private DataTable PopulateItemBalanceSelectedDataTable()
        {
            var dtb = (DataTable)Session["ib"];
            var selectedRows = dtb.Select().Where(row => (1.Equals(row["IsSelect"]) &&
                                                   row["SupplierID"] != null &&
                                                   !string.IsNullOrEmpty(row["SupplierID"].ToString()))).CopyToDataTable();

            selectedRows.PrimaryKey = new[] { selectedRows.Columns["RowID"] };
            return selectedRows;
        }

        private DataTable ItemBalanceSelectedDataTable
        {
            get
            {
                var result = Session["ibs"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["ibs"] = value;
            }
        }
        private void StoreEntryValue()
        {
            DataTable dtb = ItemBalanceSelectedDataTable;
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
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
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
        public override bool OnButtonOkClicked()
        {
            var poGenerated = GeneratePo();
            ScriptManager.RegisterStartupScript(this, GetType(), "close",
                    string.Format("GetRadWindow().close(\"{0}\");", poGenerated), true);

            // return false supaya tidak menjalankan Close pada masterpage
            return false;
        }

        private string GeneratePo()
        {
            var poGenerated = string.Empty;
            var fromServiceUnitID = Request.QueryString["fromSu"];
            var toServiceUnitID = Request.QueryString["toSu"];
            var itemType = Request.QueryString["it"];
            var dateTransaction = DateTime.Now.Date;

            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(fromServiceUnitID))
            {
                using (var trans = new esTransactionScope())
                {
                    var itemTransactionItemCollection = new ItemTransactionItemCollection();

                    var items = ItemBalanceSelectedDataTable.Select().Where(row => (1.Equals(row["IsSelect"]) &&
                                           row["SupplierID"] != null && !string.IsNullOrEmpty(row["SupplierID"].ToString())))
                        .Select(row => new
                        {
                            SupplierID = row["SupplierID"].ToString(),
                            IsPkp = Convert.ToBoolean(row["IsPkp"]),
                            TaxPercentage = Convert.ToDecimal(row["TaxPercentage"]),
                            TransactionNo = row["TransactionNo"].ToString(),
                            SequenceNo = row["SequenceNo"].ToString(),
                            ItemID = row["ItemID"].ToString(),
                            QtyOrder = Convert.ToDecimal(row["QtyOrder"] ?? 0),
                            SRItemUnitSelected = row["SRItemUnitSelected"].ToString(),
                            SRPurchaseUnit = row["SRPurchaseUnit"].ToString(),
                            ConversionFactor = Convert.ToDecimal(row["ConversionFactor"] ?? 0)
                        });

                    // Update PR QuantityFinishInBaseUnit
                    foreach (var item in items)
                    {
                        var prOutStanding = new ItemTransactionItem();
                        if (prOutStanding.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                        {
                            if (item.SRItemUnitSelected == item.SRPurchaseUnit)
                            {
                                // QuantityFinishInBaseUnit dalam satuan Base Unit
                                // Jika dalam satuan Purchase Unit maka kalikan dgn konversi ke base unitnya
                                prOutStanding.QuantityFinishInBaseUnit += (item.QtyOrder * item.ConversionFactor);
                            }
                            else
                            {
                                prOutStanding.QuantityFinishInBaseUnit += item.QtyOrder;
                            }

                            // Update status Closed
                            if (prOutStanding.QuantityFinishInBaseUnit >= prOutStanding.Quantity * prOutStanding.ConversionFactor)
                                prOutStanding.IsClosed = true;

                            prOutStanding.Save();
                        }
                    }

                    // Grouping
                    var itemGroups = items.GroupBy(ig => new
                    {
                        ig.SupplierID,
                        ig.IsPkp,
                        ig.TaxPercentage,
                        ig.ItemID,
                        ig.SRItemUnitSelected
                    }).Select(q => new
                    {
                        q.Key.SupplierID,
                        q.Key.IsPkp,
                        q.Key.TaxPercentage,
                        q.Key.ItemID,
                        q.Key.SRItemUnitSelected,
                        QtyOrder = q.Sum(p => (p.QtyOrder))
                    });

                    foreach (var itemGrouped in (from g in itemGroups
                                                 group g by new
                                                 {
                                                     g.SupplierID,
                                                     g.IsPkp,
                                                     g.TaxPercentage
                                                 }
                                                     into grp
                                                     orderby grp.Key.SupplierID
                                                     select new
                                                     {
                                                         SupplierID = grp.Key.SupplierID,
                                                         IsPkp = grp.Key.IsPkp,
                                                         TaxPercentage = grp.Key.TaxPercentage
                                                     }))
                    {
                        var autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                        decimal chargeAmt = 0;

                        foreach (var poEntry in itemGroups.Where(poEntry => poEntry.SupplierID == itemGrouped.SupplierID))
                        {
                            var transItem = itemTransactionItemCollection.AddNew();

                            #region detail

                            transItem.TransactionNo = autoNumber.LastCompleteNumber;
                            transItem.ItemID = poEntry.ItemID;
                            transItem.SequenceNo = string.Format("{0:000}", itemTransactionItemCollection.Count + 1);
                            transItem.Quantity = poEntry.QtyOrder;
                            transItem.SRItemUnit = poEntry.SRItemUnitSelected;

                            var item = new Item();
                            item.LoadByPrimaryKey(transItem.ItemID);
                            transItem.Description = item.ItemName;

                            if (itemType == ItemType.Medical)
                            {
                                var ipm = new ItemProductMedic();
                                ipm.LoadByPrimaryKey(transItem.ItemID);
                                transItem.ConversionFactor = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.ConversionFactor : 1);
                                transItem.Price = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.PriceInPurchaseUnit : ipm.PriceInBaseUnit) ?? 0;
                                transItem.IsDiscountInPercent = true;
                                transItem.Discount1Percentage = transItem.Discount1Percentage ?? 0;
                                transItem.Discount2Percentage = transItem.Discount2Percentage ?? 0;
                            }
                            else if (itemType == ItemType.NonMedical)
                            {
                                var ipm = new ItemProductNonMedic();
                                ipm.LoadByPrimaryKey(transItem.ItemID);
                                transItem.ConversionFactor = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.ConversionFactor : 1);
                                transItem.Price = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.PriceInPurchaseUnit : ipm.PriceInBaseUnit) ?? 0;

                                transItem.IsDiscountInPercent = true;
                                transItem.Discount1Percentage = transItem.Discount1Percentage ?? 0;
                                transItem.Discount2Percentage = transItem.Discount2Percentage ?? 0;
                            }
                            else if (itemType == ItemType.Kitchen)
                            {
                                var ipm = new ItemKitchen();
                                ipm.LoadByPrimaryKey(transItem.ItemID);
                                transItem.ConversionFactor = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.ConversionFactor : 1);
                                transItem.Price = (poEntry.SRItemUnitSelected == ipm.SRPurchaseUnit ? ipm.PriceInPurchaseUnit : ipm.PriceInBaseUnit) ?? 0;
                                transItem.IsDiscountInPercent = true;
                                transItem.Discount1Percentage = transItem.Discount1Percentage ?? 0;
                                transItem.Discount2Percentage = transItem.Discount2Percentage ?? 0;
                            }

                            // Update Price & Diskon jika ada pada setingan vendor untuk Purchase Unit
                            var suppItem = new SupplierItem();
                            if (suppItem.LoadByPrimaryKey(poEntry.SupplierID, transItem.ItemID))
                            {
                                if (poEntry.SRItemUnitSelected == suppItem.SRPurchaseUnit)
                                {
                                    transItem.Price = suppItem.PriceInPurchaseUnit ?? 0;
                                    transItem.IsDiscountInPercent = true;
                                    transItem.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                    transItem.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                }
                            }

                            transItem.PriceInCurrency = transItem.Price;

                            decimal disc1 = Convert.ToDecimal(transItem.Price * transItem.Discount1Percentage / 100);
                            decimal disc2 = Convert.ToDecimal((transItem.Price - disc1) * transItem.Discount2Percentage / 100);
                            transItem.Discount = disc1 + disc2;
                            transItem.DiscountInCurrency = transItem.Discount;
                            transItem.IsBonusItem = false;
                            transItem.IsTaxable = true;
                            transItem.IsClosed = false;
                            transItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            transItem.LastUpdateDateTime = DateTime.Now;

                            #endregion

                            chargeAmt += (transItem.Quantity ?? 0) * ((transItem.Price ?? 0) - (transItem.Discount ?? 0));
                        }


                        #region header
                        var entity = new ItemTransaction();
                        entity.TransactionNo = autoNumber.LastCompleteNumber;
                        entity.TransactionCode = TransactionCode.PurchaseOrder;
                        entity.TransactionDate = dateTransaction;
                        entity.BusinessPartnerID = @itemGrouped.SupplierID;
                        entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                        entity.CurrencyRate = 1;
                        entity.ReferenceNo = string.Empty;
                        entity.FromServiceUnitID = fromServiceUnitID;
                        entity.ToServiceUnitID = toServiceUnitID;
                        entity.ServiceUnitCostID = fromServiceUnitID;
                        entity.TermID = AppSession.Parameter.DefaultTerm;
                        entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                        entity.SRItemType = itemType;
                        entity.DiscountAmount = 0;
                        entity.ChargesAmount = chargeAmt;
                        entity.AmountTaxed = chargeAmt;
                        entity.DownPaymentAmount = 0;
                        entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                        entity.IsTaxable = Convert.ToInt16(@itemGrouped.IsPkp ? 1 : 0);
                        entity.TaxPercentage = @itemGrouped.IsPkp ? @itemGrouped.TaxPercentage : 0;
                        entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;

                        entity.Notes = string.Empty;
                        entity.IsNonMasterOrder = false;
                        entity.LeadTime = string.Empty;
                        entity.ContractNo = string.Empty;
                        entity.IsBySystem = true;
                        entity.IsInventoryItem = true;
                        entity.IsConsignment = false;

                        var supp = new Supplier();
                        supp.LoadByPrimaryKey(@itemGrouped.SupplierID);

                        entity.TermOfPayment = supp.TermOfPayment;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;

                        #endregion

                        autoNumber.Save();
                        entity.Save();
                        itemTransactionItemCollection.Save();

                        var linkPo =
                                string.Format(
                                    "<a href='../PurchaseOrder/PurchaseOrderDetail.aspx?md=view&id={0}'>{0}</a>",
                                    entity.TransactionNo);

                        poGenerated = string.IsNullOrEmpty(poGenerated)
                            ? linkPo
                            : poGenerated + ", " + linkPo;
                    }

                    trans.Complete();
                }

            }

            return poGenerated;
        }

    }
}
