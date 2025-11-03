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
    public partial class ReceiptOfSubstituteItemDetailItem : BaseUserControl
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

        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            lblItemName.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Description);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue);
            ComboBox.SelectedValue(cboSRItemUnit, (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));
            
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
            {
                txtQtyPending.Value = 0;
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
        
        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
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
            }
        }
        #endregion

        #region Method & Event TextChanged

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            RadComboBox cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["POSubstitute:collItemTransactionItem" + Request.UserHostName];
            if (collitem.Count == 0)
                cbo.Enabled = true;
        }

        #endregion	
    }
}