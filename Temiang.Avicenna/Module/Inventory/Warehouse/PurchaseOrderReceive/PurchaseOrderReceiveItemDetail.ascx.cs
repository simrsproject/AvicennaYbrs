using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox cboSRItemType
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            }
        }

        private RadComboBox cboToLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboToLocationID");
            }
        }

        private CheckBox ChkIsConsignment
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsConsignment"); }
        }

        private RadTextBox txtReferenceNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtReferenceNo");
            }
        }

        private CheckBox ChkIsInventoryItem
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsInventoryItem"); }
        }

        private CheckBox ChkIsAssets
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsAssets"); }
        }

        private bool IsGrantsReceiving
        {
            get
            {
                return (Request.QueryString["grants"] == "1");
            }
        }

        private bool IsDirectPurchase
        {
            get
            {
                return (Request.QueryString["grants"] == "2");
            }
        }

        private void InitGrantsReceive() {
            trIsDiscInPerc.Visible = !IsGrantsReceiving;
            trDisc1.Visible = !IsGrantsReceiving;
            trDisc2.Visible = !IsGrantsReceiving;
            trDiscAmt.Visible = !IsGrantsReceiving;
            trBonus.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trTaxable.Visible = !IsGrantsReceiving;
            chkIsTaxable.Enabled = IsDirectPurchase;
            trIsDiscInPerc.Visible = !IsGrantsReceiving;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSRItemType.Enabled = false;
            pnlEd.Visible = !AppSession.Parameter.IsTxUsingEdDetail;

            InitGrantsReceive();

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUSKY")
                txtConversionFactor.ReadOnly = false;

            trFabricID.Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["POR_W3TOT:collItemTransactionItem" + Request.UserHostName];
                if (collitem.Count == 0)
                    txtSequenceNo.Text = "001";
                else
                {
                    int seqNo = 0;
                    foreach (ItemTransactionItem item in collitem)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    txtSequenceNo.Text = string.Format("{0:000}", seqNo + 1);
                }
                chkIsBonusItem.Checked = (IsGrantsReceiving || IsDirectPurchase) ? false: true;
                chkIsBonusItem.Enabled = false;

                txtPrice.Value = 0;
                chkIsDiscountInPercent.Checked = false;
                txtDiscount1Percentage.Value = 0;
                txtDiscount2Percentage.Value = 0;
                txtDiscountAmount.Value = 0;
                chkIsTaxable.Checked = IsGrantsReceiving ? false : true;

                SetEnabled_Price(!chkIsBonusItem.Checked);
                SetEnabled_Discount(!chkIsBonusItem.Checked);

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtReferenceSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo);

            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"));
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue);
            ComboBox.SelectedValue(cboSRItemUnit, (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit));

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Price));
            txtDiscount1Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage));
            txtDiscount2Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage));
            txtDiscountAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount));
            chkIsDiscountInPercent.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent);
            chkIsBonusItem.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsBonusItem);
            try
            {
                chkIsTaxable.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsTaxable);
            }
            catch (Exception)
            {
                chkIsTaxable.Checked = true;
            }
            
            txtBatchNumber.Text = (string)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.BatchNumber);

            object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();

            if (!IsGrantsReceiving && !IsDirectPurchase)
            {
                var reff = new ItemTransactionItemQuery();
                reff.Where(
                    reff.TransactionNo ==
                    (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo),
                    reff.SequenceNo ==
                    (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo));
                DataTable dtreff = reff.LoadDataTable();
                if (dtreff.Rows.Count > 0)
                {
                    txtQtyPending.Value = ((Convert.ToDouble(dtreff.Rows[0]["Quantity"]) *
                                            Convert.ToDouble(dtreff.Rows[0]["ConversionFactor"])) -
                                           Convert.ToDouble(dtreff.Rows[0]["QuantityFinishInBaseUnit"]));
                }
                else
                    txtQtyPending.Value = 0;
            }
            else
                txtQtyPending.Value = 0;

            var fabricId = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.FabricID);
            if (!string.IsNullOrEmpty(fabricId))
            {
                var fq = new FabricQuery();
                fq.Where(fq.FabricID == fabricId);
                cboFabricID.DataSource = fq.LoadDataTable();
                cboFabricID.DataBind();
                cboFabricID.SelectedValue = fabricId;
            }

            if (chkIsBonusItem.Checked)
            {
                SetEnabled_Price(!chkIsBonusItem.Checked);
                SetEnabled_Discount(!chkIsBonusItem.Checked);
            }
            else
            {
                SetEnabledPrice(cboItemID.SelectedValue);
                cboItemID.Enabled = false;
                cboFabricID.Enabled = false;
            }

            //if (ChkIsConsignment.Checked && !chkIsBonusItem.Checked)
            //{
            //    txtQuantity.ReadOnly = true;
            //    cboSRItemUnit.Enabled = false;
            //    txtConversionFactor.ReadOnly = true;
            //}
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity must greather than 0.");
                return;
            }

            if (!chkIsBonusItem.Checked && (!IsGrantsReceiving && !IsDirectPurchase))
            {
                if (txtPrice.Value == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Price must greather than 0.");
                    return;
                }
                if ((txtQuantity.Value * txtConversionFactor.Value) > txtQtyPending.Value)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Quantity can not be greather than " + (txtQtyPending.Value / txtConversionFactor.Value).ToString() + ".");
                    return;
                }
            }

            if (IsDirectPurchase && txtPrice.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Price must greather than 0.");
                return;
            }

            if (pnlEd.Visible)
            {
                bool isControlExpired = false;
                var item = new Item();
                item.LoadByPrimaryKey(cboItemID.SelectedValue);
                if (item.SRItemType == ItemType.Medical)
                {
                    var product = new ItemProductMedic();
                    product.LoadByPrimaryKey(item.ItemID);
                    isControlExpired = product.IsControlExpired ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical)
                {
                    var product = new ItemProductNonMedic();
                    product.LoadByPrimaryKey(item.ItemID);
                    isControlExpired = product.IsControlExpired ?? false;
                }
                else if (item.SRItemType == ItemType.Kitchen)
                {
                    var product = new ItemKitchen();
                    product.LoadByPrimaryKey(item.ItemID);
                    isControlExpired = product.IsControlExpired ?? false;
                }
                if (isControlExpired)
                {
                    if (txtExpiredDate.SelectedDate != null)
                    {
                        if (txtExpiredDate.SelectedDate < (new DateTime()).NowAtSqlServer())
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Expired date must greather than transaction date.");
                            return;
                        }
                    }
                    else
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Expired date must be fill.");
                        return;
                    }
                }
            }
        }

        private void SetEnabledPrice(string itemId)
        {
            bool enabled = AppSession.Parameter.IsPorCanChangeThePrice || IsDirectPurchase;
            var reff = new ItemTransaction();
            if (reff.LoadByPrimaryKey(txtReferenceNo.Text))
            {
                if (!string.IsNullOrEmpty(reff.ContractNo))
                {
                    var scItem = new SupplierContractItem();
                    if (scItem.LoadByPrimaryKey(reff.ContractNo, itemId))
                        enabled = false;
                }
            }

            txtPrice.ReadOnly = !enabled;
            chkIsDiscountInPercent.Enabled = enabled;
            if (enabled == false)
            {
                txtDiscount1Percentage.ReadOnly = true;
                txtDiscount2Percentage.ReadOnly = true;
                txtDiscountAmount.ReadOnly = true;
            }
            else
            {
                txtDiscount1Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
                txtDiscount2Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
                txtDiscountAmount.ReadOnly = chkIsDiscountInPercent.Checked;
            }
        }

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (IsGrantsReceiving)
            {
                string filterBySupplier = AppSession.Parameter.PurcOrderItemTypeRestrictionForItemSupplier.Contains(cboSRItemType.SelectedValue) ? "y" : "n";

                ComboBox.ItemItemsRequested(
                    (RadComboBox)sender,
                    e.Text,
                    cboSRItemType.SelectedValue,
                    cboToLocationID.SelectedValue, //cboServiceUnit.SelectedValue,
                    ((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue,
                    string.Empty, ChkIsInventoryItem.Checked,
                    false, filterBySupplier == "y" ? "y" : "",
                    string.Empty, ChkIsAssets.Checked, true
                    );
                //ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue, cboToLocationID.SelectedValue, )
            }
            else
            {
                ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue, cboToLocationID.SelectedValue, string.Empty, IsDirectPurchase, ChkIsConsignment.Checked, string.Empty, true);
            }
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            //ComboBox.PopulateWithItemPurchaseUnit(cboSRItemUnit, e.Value, itemType);
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, e.Value, itemType);

            string unitID = string.Empty;
            decimal conversion = 1;
            if (itemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(e.Value);
                unitID = medic.SRPurchaseUnit;
                conversion = medic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(e.Value);
                unitID = medic.SRPurchaseUnit;
                conversion = medic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(e.Value);
                unitID = medic.SRPurchaseUnit;
                conversion = medic.ConversionFactor ?? 1;
            }
            ComboBox.SelectedValue(cboSRItemUnit, unitID);
            txtConversionFactor.Value = Convert.ToDouble(conversion);

            var item = new Item();
            item.LoadByPrimaryKey(e.Value);
            txtBarcode.Text = item.Barcode;

            cboFabricID.Items.Clear();
            cboFabricID.Text = string.Empty;
        }

        #endregion

        #region ComboBox SRItemUnit
        protected void cboSRItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                string itemType = cboSRItemType.SelectedItem.Value;
                string itemId = cboItemID.SelectedValue;
                decimal conversionFactor = 1;
                string baseUnitId = string.Empty;
                decimal priceInBaseUnit = 0;

                var po = new ItemTransactionItem();
                if (po.LoadByPrimaryKey(txtReferenceNo.Text, txtReferenceSequenceNo.Text))
                    priceInBaseUnit = (po.Price ?? 0) / (po.ConversionFactor ?? 0);

                if (itemType == ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemId);
                    baseUnitId = medic.SRItemUnit;
                    conversionFactor = medic.ConversionFactor ?? 1;
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemId);
                    baseUnitId = nonMedic.SRItemUnit;
                    conversionFactor = nonMedic.ConversionFactor ?? 1;
                }
                else if (itemType == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(itemId);
                    baseUnitId = kitchen.SRItemUnit;
                    conversionFactor = kitchen.ConversionFactor ?? 1;
                }

                txtConversionFactor.Value = e.Value.Equals(baseUnitId) ? 1 : Convert.ToDouble(conversionFactor);

                if (!chkIsBonusItem.Checked)
                    txtPrice.Value = e.Value.Equals(baseUnitId)
                                         ? Convert.ToDouble(priceInBaseUnit)
                                         : Convert.ToDouble(priceInBaseUnit * conversionFactor);

            }
        }
        #endregion

        protected void chkIsDiscountInPercent_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledDiscount();
            txtDiscount1Percentage.Value = 0.00;
            txtDiscount2Percentage.Value = 0.00;
            txtDiscountAmount.Value = 0.00;
        }

        protected void txtConversionFactor_TextChanged(object sender, EventArgs e)
        {
            if (!chkIsBonusItem.Checked)
            {
                decimal priceInBaseUnit = 0;

                var po = new ItemTransactionItem();
                if (po.LoadByPrimaryKey(txtReferenceNo.Text, txtReferenceSequenceNo.Text))
                    priceInBaseUnit = (po.Price ?? 0) / (po.ConversionFactor ?? 0);

                txtPrice.Value = Convert.ToDouble(priceInBaseUnit) * txtConversionFactor.Value;
            }
        }

        private void SetEnabledDiscount()
        {
            txtDiscount1Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
            txtDiscountAmount.ReadOnly = chkIsDiscountInPercent.Checked;
        }

        #region Properties for return entry value
        public String SequenceNo
        {
            get { return txtSequenceNo.Text; }
        }
        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }
        public String ItemName
        {
            get { return cboItemID.Text; }
        }
        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }
        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }
        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }
        public Decimal Discount1Percentage
        {
            get { return Convert.ToDecimal(txtDiscount1Percentage.Value); }
        }
        public Decimal Discount2Percentage
        {
            get { return Convert.ToDecimal(txtDiscount2Percentage.Value); }
        }
        public Decimal Discount
        {
            get { return Convert.ToDecimal(txtDiscountAmount.Value); }
        }
        public Boolean IsDiscountInPercent
        {
            get { return chkIsDiscountInPercent.Checked; }
        }
        public String BatchNumber
        {
            get { return txtBatchNumber.Text; }
        }
        public DateTime? ExpiredDate
        {
            get { return txtExpiredDate.SelectedDate; }
        }
        public Boolean IsBonusItem
        {
            get { return chkIsBonusItem.Checked; }
        }
        public Boolean IsTaxable
        {
            get { return chkIsTaxable.Checked; }
        }
        public Decimal QtyPending
        {
            get { return Convert.ToDecimal(txtQtyPending.Value); }
        }
        public String Barcode
        {
            get { return txtBarcode.Text; }
        }
        public String FabricID
        {
            get { return cboFabricID.SelectedValue; }
        }
        public String FabricName
        {
            get { return cboFabricID.Text; }
        }
        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (ItemTransactionItemCollection)Session["POR_W3TOT:collItemTransactionItem" + Request.UserHostName];
            if (collitem != null && collitem.Count == 0)
                cboSRItemType.Enabled = true;
        }

        private void SetEnabled_Price(bool isEnabled)
        {
            txtPrice.ReadOnly = !isEnabled;
        }
        private void SetEnabled_Discount(bool isEnabled)
        {
            chkIsDiscountInPercent.Enabled = isEnabled;
            txtDiscount1Percentage.ReadOnly = !isEnabled;
            txtDiscount2Percentage.ReadOnly = !isEnabled;
            txtDiscountAmount.ReadOnly = !isEnabled;
        }

        protected void cboFabricID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new FabricQuery("a");
            var imq = new VwItemProductFabricQuery("b");
            query.InnerJoin(imq).On(imq.FabricID == query.FabricID);
            query.Select
                (
                    query.FabricID,
                    query.FabricName
                );
            query.Where
                (
                    query.FabricName.Like(searchTextContain),
                    query.IsActive == true,
                    imq.ItemID == cboItemID.SelectedValue
                );

            cboFabricID.DataSource = query.LoadDataTable();
            cboFabricID.DataBind();
        }

        protected void cboFabricID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FabricName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FabricID"].ToString();
        }
    }
}