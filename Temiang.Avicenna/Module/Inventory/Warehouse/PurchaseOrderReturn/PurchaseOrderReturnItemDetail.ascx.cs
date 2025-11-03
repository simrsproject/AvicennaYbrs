using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReturnItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private RadComboBox cboSRItemType
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            pnlPrice.Visible = (AppSession.Parameter.IsPurcReturnWithPrice);
            if (AppSession.Parameter.IsPorCanChangeThePrice)
            {
                txtPrice.ReadOnly = false;
                chkIsDiscountInPercent.Enabled = true;
            }

            txtItemID.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            lblItemName.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Description);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue);
            ComboBox.SelectedValue(cboSRItemUnit, (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Price));
            txtPriceInCurrency.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.PriceInCurrency));
            txtDiscount1Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage));
            txtDiscount2Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage));
            txtDiscountAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount));
            txtDiscountAmountInCurrency.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.DiscountInCurrency));
            chkIsDiscountInPercent.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent);
            txtBatchNumber.Text = (string)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.BatchNumber);

            object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();

            SetEnabledDiscount();

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
                txtPurchasePrice.Value = Convert.ToDouble(dtreff.Rows[0]["Price"]);
                txtPurchasePriceInCurrency.Value = Convert.ToDouble(dtreff.Rows[0]["PriceInCurrency"]);
                txtPurchaseDiscount.Value = Convert.ToDouble(dtreff.Rows[0]["Discount"]);
                txtPurchaseDiscountInCurrency.Value = Convert.ToDouble(dtreff.Rows[0]["DiscountInCurrency"]);
                txtPurchaseConversionFactor.Value = Convert.ToDouble(dtreff.Rows[0]["ConversionFactor"]);

                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    var reffed = new ItemTransactionItemEdQuery();
                    reffed.Where(
                        reffed.TransactionNo ==
                        (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo),
                        reffed.SequenceNo ==
                        (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo),
                        reffed.BatchNumber ==
                        (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.BatchNumber));
                    DataTable dtreffed = reffed.LoadDataTable();
                    if (dtreffed.Rows.Count > 0)
                    {
                        txtQtyPending.Value = ((Convert.ToDouble(dtreffed.Rows[0]["Quantity"]) *
                                                Convert.ToDouble(dtreffed.Rows[0]["ConversionFactor"])) -
                                               Convert.ToDouble(dtreffed.Rows[0]["QuantityFinishInBaseUnit"]));
                    }
                }
            }
            else
            {
                txtQtyPending.Value = 0;
                txtPurchasePrice.Value = 0;
                txtPurchasePriceInCurrency.Value = 0;
                txtPurchaseDiscount.Value = 0;
                txtPurchaseDiscountInCurrency.Value = 0;
                txtPurchaseConversionFactor.Value = 1;
            }
        }
        
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            if (txtQuantity.Value > txtQtyPending.Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity can not be greather than " + txtQtyPending.Value.ToString());
                return;
            }
        }

        #region Properties for return entry value
        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }
        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }
        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }
        public Decimal PriceInCurrency
        {
            get { return Convert.ToDecimal(txtPriceInCurrency.Value); }
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
        public Decimal DiscountInCurrency
        {
            get { return Convert.ToDecimal(txtDiscountAmountInCurrency.Value); }
        }
        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public Boolean IsDiscountInPercent
        {
            get { return chkIsDiscountInPercent.Checked; }
        }
        
        #endregion

        #region ComboBox SRItemUnit
        protected void cboSRItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                string itemType = cboSRItemType.SelectedItem.Value;
                string itemId = txtItemID.Text;
                decimal conversionFactor = 1;
                string baseUnitId = string.Empty;
                decimal priceInBaseUnit = Convert.ToDecimal(txtPurchasePrice.Value/txtConversionFactor.Value);
                decimal priceInCurrencyInBaseUnit = Convert.ToDecimal(txtPurchasePriceInCurrency.Value / txtConversionFactor.Value);
                decimal discInBaseUnit = Convert.ToDecimal(txtDiscountAmount.Value / txtConversionFactor.Value);
                decimal discInCurrencyInBaseUnit = Convert.ToDecimal(txtDiscountAmountInCurrency.Value / txtConversionFactor.Value);
                
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
                txtPrice.Value = e.Value.Equals(baseUnitId) ? Convert.ToDouble(priceInBaseUnit) : txtPurchasePrice.Value;
                txtPriceInCurrency.Value = e.Value.Equals(baseUnitId) ? Convert.ToDouble(priceInCurrencyInBaseUnit) : txtPurchasePriceInCurrency.Value;
                txtDiscountAmount.Value = e.Value.Equals(baseUnitId) ? Convert.ToDouble(discInBaseUnit) : txtPurchaseDiscount.Value;
                txtDiscountAmountInCurrency.Value = e.Value.Equals(baseUnitId) ? Convert.ToDouble(discInCurrencyInBaseUnit) : txtPurchaseDiscountInCurrency.Value;
            }
        }
        #endregion

        #region Method & Event TextChanged
        protected void chkIsDiscountInPercent_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledDiscount();
            txtDiscount1Percentage.Value = 0.00;
            txtDiscount2Percentage.Value = 0.00;
            txtDiscountAmount.Value = 0.00;
        }

        private void SetEnabledDiscount()
        {
            txtDiscount1Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.ReadOnly = !chkIsDiscountInPercent.Checked;
            txtDiscountAmount.ReadOnly = chkIsDiscountInPercent.Checked;
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            RadComboBox cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["POReturn:collItemTransactionItem" + Request.UserHostName + PageId];
            if (collitem.Count == 0)
                cbo.Enabled = true;
        }

        #endregion	

    }
}