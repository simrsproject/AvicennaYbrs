using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class SalesItemDetail : BaseUserControl
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

        private RadComboBox cboServiceUnitID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID");
            }
        }
        private RadComboBox cboCustomerID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboCustomerID");
            }
        }
        private RadNumericTextBox txtSalesMarginPercentage
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtSalesMarginPercentage");
            }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            cboServiceUnitID.Enabled = false;
            cboSRItemType.Enabled = false;
            cboCustomerID.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["Sales:collItemTransactionItem" + Request.UserHostName];
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

                txtDiscount1Percentage.Value = 0.00;
                txtDiscount2Percentage.Value = 0.00;
                txtDiscountAmount.Value = 0.00;
                txtDiscountAmount.Enabled = false;

                return;
            }

            ViewState["IsNewRecord"] = false;
            PopulateItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Price));
            txtDiscount1Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage));
            txtDiscount2Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage));
            txtDiscountAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount));
            chkIsDiscountInPercent.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent);

            ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, cboSRItemType.SelectedValue);
            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity must greater than 0");
                return;
            }

            //Check Entry ItemID
            ItemQuery qrItem = new ItemQuery();
            qrItem.Where(qrItem.ItemID == cboItemID.SelectedValue);

            Item item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select existing item";
                return;
            }

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            var balance = new ItemBalance();
            if (!balance.LoadByPrimaryKey(unit.GetMainLocationId(), cboItemID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item is not have balance quantity";
                return;
            }
            else
            {
                if (balance.Balance <= 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Selected item is not have sufficient balance quantity";
                    return;
                }
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemTransactionItemCollection coll = (ItemTransactionItemCollection)Session["Sales:collItemTransactionItem" + Request.UserHostName];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ItemTransactionItem row in coll)
                {
                    if (row.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
                }
            }
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

        //public String ItemName
        //{
        //    get { return cboItemID.Text; }
        //}

        public String ItemName
        {
            get { return txtDescription.Text; }
        }


        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }

        public Decimal ConversionFactor
        {
            get
            { return Convert.ToDecimal(txtConversionFactor.Value); }
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

        public Decimal DiscountAmount
        {
            get { return Convert.ToDecimal(txtDiscountAmount.Value); }
        }

        public Boolean IsDiscountInPercent
        {
            get { return chkIsDiscountInPercent.Checked; }
        }
        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["Sales:collItemTransactionItem" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboSRItemType.Enabled = true;
                cboServiceUnitID.Enabled = true;
            }
        }

        #region ComboBox ItemID

        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateItemID((RadComboBox)sender, e.Text);
        }

        private void PopulateItemID(RadComboBox comboBox, string textSearch)
        {
            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(this.Page, "cboFromServiceUnitID")).SelectedValue))
                return;

            var cboSRItemType = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            query.InnerJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == unit.GetMainLocationId()
                );

            query.Where
                (
                    query.SRItemType == cboSRItemType.SelectedItem.Value,
                    query.ItemName.Like(searchTextContain)
                );

            if (cboSRItemType.SelectedItem.Value == ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);

                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString()
                    );

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.Where(prod.IsInventoryItem == true);
            }
            else if (cboSRItemType.SelectedItem.Value == ItemType.NonMedical)
            {
                var prod = new ItemProductNonMedicQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);

                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On
                    (
                         prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString()
                    );

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.Where(prod.IsInventoryItem == true);
            }
            else if (cboSRItemType.SelectedItem.Value == ItemType.Kitchen)
            {
                var prod = new ItemKitchenQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);

                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == AppEnum.StandardReference.ItemUnit.ToString()
                    );

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.Where(prod.IsInventoryItem == true);
            }

            query.es.Top = 20;

            comboBox.DataSource = query.LoadDataTable(); ;
            comboBox.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, e.Value, itemType);

            decimal conversionFactor;
            string realUnitUsed = string.Empty;
            PopulatePrice(e.Value, out conversionFactor, string.Empty, out realUnitUsed);

            ComboBox.SelectedValue(cboSRItemUnit, string.IsNullOrEmpty(realUnitUsed) ? "PCS" : realUnitUsed);
            txtConversionFactor.Value = Convert.ToDouble(conversionFactor);
            txtDescription.Text = e.Text;
        }

        private void PopulatePrice(string itemID, out decimal conversionFactor, string priceBaseOnUnitID, out string realUnitUsed)
        {
            // Diskon Purchase untuk keuntungan klinik

            decimal price = 0;
            decimal disc1 = 0;
            decimal disc2 = 0;
            string purcUnitId = string.Empty;
            string baseUnitId = string.Empty;

            conversionFactor = 1;
            if (cboSRItemType.SelectedItem.Value == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemID);
                purcUnitId = medic.SRPurchaseUnit;
                baseUnitId = medic.SRItemUnit;

                price = medic.PriceInBaseUnit ?? 0;
                conversionFactor = medic.ConversionFactor ?? 1;
                disc1 = medic.PurchaseDiscount1 ?? 0;
                disc2 = medic.PurchaseDiscount2 ?? 0;
            }
            else if (cboSRItemType.SelectedItem.Value == ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(itemID);
                purcUnitId = medic.SRPurchaseUnit;
                baseUnitId = medic.SRItemUnit;

                price = medic.PriceInBaseUnit ?? 0;
                conversionFactor = medic.ConversionFactor ?? 1;
                disc1 = medic.PurchaseDiscount1 ?? 0;
                disc2 = medic.PurchaseDiscount2 ?? 0;
            }
            else if (cboSRItemType.SelectedItem.Value == ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(itemID);
                purcUnitId = medic.SRPurchaseUnit;
                baseUnitId = medic.SRItemUnit;

                price = medic.PriceInBaseUnit ?? 0;
                conversionFactor = medic.ConversionFactor ?? 1;
                disc1 = medic.PurchaseDiscount1 ?? 0;
                disc2 = medic.PurchaseDiscount2 ?? 0;
            }

            // Cek conversionFactor and set baseUnitId as realUnitUsed if priceBaseOnUnitID enpty or not equal purcUnitId
            if (!purcUnitId.Equals(priceBaseOnUnitID))
            {
                conversionFactor = 1;
                realUnitUsed = baseUnitId;
            }
            else
                realUnitUsed = purcUnitId;

            txtPrice.Value = (double)(price * conversionFactor) *
                                       ((100 + (txtSalesMarginPercentage.Value ?? 0)) / 100);

            if (AppSession.Parameter.IsSharePurchaseDiscToCustomer)
            {
                txtDiscount1Percentage.Value = (double)disc1;
                txtDiscount2Percentage.Value = (double)disc2;
            }
        }

        protected void cboSRItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                string itemId = cboItemID.SelectedValue;
                decimal conversionFactor;
                string realUnitUsed = string.Empty;
                PopulatePrice(itemId, out conversionFactor, e.Value, out realUnitUsed);
                txtConversionFactor.Value = Convert.ToDouble(conversionFactor);
            }
        }

        protected void chkIsDiscountInPercent_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledDiscount();
            txtDiscount1Percentage.Value = 0.00;
            txtDiscount2Percentage.Value = 0.00;
            txtDiscountAmount.Value = 0.00;
        }

        private void SetEnabledDiscount()
        {
            txtDiscount1Percentage.Enabled = chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.Enabled = chkIsDiscountInPercent.Checked;
            txtDiscountAmount.Enabled = !chkIsDiscountInPercent.Checked;
        }

        #endregion
    }
}