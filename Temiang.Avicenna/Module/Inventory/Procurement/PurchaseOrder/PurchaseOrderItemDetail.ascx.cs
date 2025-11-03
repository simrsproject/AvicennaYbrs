using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class PurchaseOrderItemDetail : BaseUserControl
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
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType"); }
        }

        private RadComboBox cboSRItemCategory
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemCategory"); }
        }

        private RadComboBox cboServiceUnit
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID"); }
        }

        private CheckBox chkIsNonMasterOrder
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsNonMasterOrder"); }
        }

        private CheckBox ChkIsInventoryItem
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsInventoryItem"); }
        }

        private CheckBox ChkIsConsignment
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsConsignment"); }
        }

        private CheckBox ChkIsAssets
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsAssets"); }
        }

        private CheckBox ChkIsConsignmentAlreadyReceived
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsConsignmentAlreadyReceived"); }
        }

        private RadTextBox TxtReferenceNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtReferenceNo"); }
        }

        private RadTextBox TxtTransactionNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtTransactionNo"); }
        }

        private RadComboBox cboBusinessPartnerID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSRItemType.Enabled = false;
            cboSRItemCategory.Enabled = false;
            chkIsNonMasterOrder.Enabled = false;
            ChkIsInventoryItem.Enabled = false;
            ChkIsConsignment.Enabled = false;
            //if (AppSession.Application.IsModuleAssetActive)
            //    ChkIsAssets.Enabled = false;

            //var enableCF = true;
            //txtConversionFactor.ReadOnly = !enableCF;
            txtConversionFactor.ReadOnly = !AppSession.Parameter.IsPOCanChangeConversion;

            if (chkIsNonMasterOrder.Checked)
            {
                cboItemID.Visible = false;
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
                //cboSRItemUnit.SelectedValue = "PCS";
                //txtConversionFactor.Value = 1;
            }
            else
                txtDescription.Visible = false;

            if (!string.IsNullOrEmpty(TxtReferenceNo.Text))
            {
                //txtQuantity.ReadOnly = !AppSession.Parameter.IsPOCanChangeQty;
                cboSRItemUnit.Enabled = AppSession.Parameter.IsPOCanChangePurchaseUnit;
            }

            trFabricID.Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtReferenceSequenceNo.Text = string.Empty;

                var collitem = (ItemTransactionItemCollection)Session["PurchaseOrderItems" + Request.UserHostName];
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

                if (!string.IsNullOrEmpty(TxtReferenceNo.Text))
                {
                    chkIsBonusItem.Checked = true;
                    chkIsBonusItem.Enabled = false;
                    ChangeBonusState(chkIsBonusItem.Checked);

                    cboItemID.Enabled = chkIsBonusItem.Checked;
                    txtQuantity.ReadOnly = false;
                    cboSRItemUnit.Enabled = true;
                }

                txtDiscount1Percentage.Value = 0.00;
                txtDiscount2Percentage.Value = 0.00;
                txtDiscountAmount.Value = 0.00;
                txtDiscountAmount.Enabled = false;
                chkIsTaxable.Checked = true;

                return;
            }

            ViewState["IsNewRecord"] = false;
            txtReferenceSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo);

            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtSpecs.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Specification);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Description);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Price));
            txtDiscount1Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage));
            txtDiscount2Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage));
            txtDiscountAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount));
            chkIsBonusItem.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsBonusItem);
            chkIsClosed.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsClosed);
            chkIsDiscountInPercent.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent);
            try
            {
                chkIsTaxable.Checked = (bool)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.IsTaxable);
            }
            catch (Exception)
            {
                chkIsTaxable.Checked = true;
            }

            if (!chkIsNonMasterOrder.Checked)
            {
                this.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue, cboServiceUnit.SelectedValue, 
                    ((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, 
                    AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory ? cboSRItemCategory.SelectedValue : string.Empty);

                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, cboSRItemType.SelectedValue);
            }
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));

            var trno = ((DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo) == null) ? "" : (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo));
            var seqno = ((DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceNo) == null) ? "" : (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo));

            txtQtyPending.Value = 0;
            var reff = new ItemTransactionItem();
            if (reff.LoadByPrimaryKey(trno, seqno))
            {
                double qtyPending = 0;
                if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                {
                    var finDt = new ItemTransactionItemQuery("dt");
                    var finHd = new ItemTransactionQuery("hd");
                    finDt.InnerJoin(finHd).On(finHd.TransactionNo == finDt.TransactionNo && finHd.TransactionCode == TransactionCode.PurchaseOrder && finHd.IsVoid == false);
                    finDt.Where(finDt.TransactionNo != TxtTransactionNo.Text, finDt.ReferenceNo == trno, finDt.ReferenceSequenceNo == seqno);
                    finDt.Select(finDt.ReferenceNo, finDt.ReferenceSequenceNo, @"<SUM(dt.Quantity*dt.ConversionFactor) AS QuantityFinishInBaseUnit>");
                    finDt.GroupBy(finDt.ReferenceNo, finDt.ReferenceSequenceNo);
                    DataTable finDtb = finDt.LoadDataTable();
                    if (finDtb.Rows.Count > 0)
                    {
                        qtyPending = Convert.ToDouble((reff.Quantity ?? 0 * reff.ConversionFactor ?? 0)) - Convert.ToDouble(finDtb.Rows[0]["QuantityFinishInBaseUnit"]);
                    }
                    else
                        qtyPending = Convert.ToDouble((reff.Quantity ?? 0 * reff.ConversionFactor ?? 0));
                }
                else
                {
                    qtyPending = Convert.ToDouble((reff.Quantity ?? 0 * reff.ConversionFactor ?? 0) - reff.QuantityFinishInBaseUnit ?? 0);
                }
                txtQtyPending.Value = qtyPending > 0 ? qtyPending : 0;
            }

            var fabricId = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.FabricID);
            if (!string.IsNullOrEmpty(fabricId))
            {
                var fq = new FabricQuery();
                fq.Where(fq.FabricID == fabricId);
                cboFabricID.DataSource = fq.LoadDataTable();
                cboFabricID.DataBind();
                cboFabricID.SelectedValue = fabricId;
            }

            if (!string.IsNullOrEmpty(txtReferenceSequenceNo.Text))
            {
                if (ChkIsConsignmentAlreadyReceived.Checked)
                {
                    cboItemID.Enabled = false;
                    txtQuantity.ReadOnly = true;
                    cboSRItemUnit.Enabled = false;
                    txtConversionFactor.ReadOnly = true;
                }
                else
                {
                    cboItemID.Enabled = chkIsBonusItem.Checked;
                    //txtQuantity.ReadOnly = !chkIsBonusItem.Checked && !AppSession.Parameter.IsPOCanChangeQty;
                    cboSRItemUnit.Enabled = chkIsBonusItem.Checked ? chkIsBonusItem.Checked : AppSession.Parameter.IsPOCanChangePurchaseUnit;
                }
            }

            SetEnabledPrice(cboItemID.SelectedValue, chkIsBonusItem.Checked);
            SetEnabledDiscount();

            if (!chkIsNonMasterOrder.Checked)
            {
                grdItemBalance.DataSource = ItemBalances;
                grdItemBalance.DataBind();
                grdSupplierItem.DataSource = SupplierItems;
                grdSupplierItem.DataBind();
            }
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //string poType = ((RadComboBox)Helper.FindControlRecursive(Page, "cboSRPurchaseOrderType")).SelectedValue;

            var LocationID = string.Empty;
            var slColl = new ServiceUnitLocationCollection();
            slColl.Query.Where(slColl.Query.ServiceUnitID.Equal(cboServiceUnit.SelectedValue),
                slColl.Query.IsLocationMain.Equal(true));
            slColl.Query.es.Top = 1;
            if (slColl.LoadAll())
            {
                LocationID = slColl.First().LocationID;
            }

            string filterBySupplier = AppSession.Parameter.PurcOrderItemTypeRestrictionForItemSupplier.Contains(cboSRItemType.SelectedValue) ? "y" : "n";

            ComboBox.ItemItemsRequested(
                (RadComboBox)sender,
                e.Text,
                cboSRItemType.SelectedValue,
                LocationID, //cboServiceUnit.SelectedValue,
                ((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue,
                ((RadTextBox)Helper.FindControlRecursive(Page, "txtContractNo")).Text, ChkIsInventoryItem.Checked,
                ChkIsConsignment.Checked, filterBySupplier == "y" ? "y" : Request.QueryString["suptype"],
                AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory ? cboSRItemCategory.SelectedValue : string.Empty, ChkIsAssets.Checked, true
                );
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // Satuan unit yg dipakai selalu dalam Purchase Unit
            bool isContract = false;

            string itemType = cboSRItemType.SelectedItem.Value;
            string purcUnitId = string.Empty;
            double factor = 0;
            if (!chkIsNonMasterOrder.Checked)
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, e.Value, itemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            if (itemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(e.Value);
                purcUnitId = medic.str.SRPurchaseUnit;
                factor = Convert.ToDouble(medic.ConversionFactor);

                txtDiscount1Percentage.Value = (double)(medic.PurchaseDiscount1 ?? 0);
                txtDiscount2Percentage.Value = (double)(medic.PurchaseDiscount2 ?? 0);
                txtPrice.Value = (double)(medic.PriceInPurchaseUnit ?? 0);
            }
            else if (itemType == ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(e.Value);
                purcUnitId = medic.str.SRPurchaseUnit;
                factor = Convert.ToDouble(medic.ConversionFactor);

                txtDiscount1Percentage.Value = (double)(medic.PurchaseDiscount1 ?? 0);
                txtDiscount2Percentage.Value = (double)(medic.PurchaseDiscount2 ?? 0);
                txtPrice.Value = (double)(medic.PriceInPurchaseUnit ?? 0);
            }
            else if (itemType == ItemType.Kitchen)
            {
                var k = new ItemKitchen();
                k.LoadByPrimaryKey(e.Value);
                purcUnitId = k.str.SRPurchaseUnit;
                factor = Convert.ToDouble(k.ConversionFactor);

                txtDiscount1Percentage.Value = (double)(k.PurchaseDiscount1 ?? 0);
                txtDiscount2Percentage.Value = (double)(k.PurchaseDiscount2 ?? 0);
                txtPrice.Value = (double)(k.PriceInPurchaseUnit ?? 0);
            }

            txtDescription.Text = e.Text;
            ComboBox.SelectedValue(cboSRItemUnit, string.IsNullOrEmpty(purcUnitId) ? "PCS" : purcUnitId);
            txtConversionFactor.Value = Convert.ToDouble(factor == 0 ? 1 : factor);

            if (!chkIsBonusItem.Checked)
            {
                string transNo = ((RadTextBox)Helper.FindControlRecursive(Page, "txtContractNo")).Text;
                if (transNo != string.Empty)
                {
                    var scItem = new SupplierContractItem();
                    if (scItem.LoadByPrimaryKey(transNo, e.Value))
                    {
                        txtDiscount1Percentage.Value = (double)(scItem.PurchaseDiscount1 ?? 0);
                        txtDiscount2Percentage.Value = (double)(scItem.PurchaseDiscount2 ?? 0);
                        txtPrice.Value = (double)(scItem.PriceInPurchaseUnit ?? 0);
                        txtDiscountAmount.Value = (txtPrice.Value * txtDiscount1Percentage.Value / 100) +
                                                ((txtPrice.Value - (txtPrice.Value * txtDiscount1Percentage.Value / 100)) *
                                                 txtDiscount2Percentage.Value / 100);
                        isContract = true;
                    }
                    else
                    {
                        var suppItem = new SupplierItem();
                        if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, e.Value))
                        {
                            txtDiscount1Percentage.Value = (double)(suppItem.PurchaseDiscount1 ?? 0);
                            txtDiscount2Percentage.Value = (double)(suppItem.PurchaseDiscount2 ?? 0);
                            txtPrice.Value = (double)(suppItem.PriceInPurchaseUnit ?? 0);
                            txtDiscountAmount.Value = (txtPrice.Value * txtDiscount1Percentage.Value / 100) +
                                                ((txtPrice.Value - (txtPrice.Value * txtDiscount1Percentage.Value / 100)) *
                                                 txtDiscount2Percentage.Value / 100);
                        }
                    }
                }
                else
                {
                    var suppItem = new SupplierItem();
                    if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, e.Value))
                    {
                        txtDiscount1Percentage.Value = (double)(suppItem.PurchaseDiscount1 ?? 0);
                        txtDiscount2Percentage.Value = (double)(suppItem.PurchaseDiscount2 ?? 0);
                        txtPrice.Value = (double)(suppItem.PriceInPurchaseUnit ?? 0);
                    }
                }

                txtPrice.Enabled = !isContract;
                chkIsDiscountInPercent.Enabled = !isContract;
                txtDiscount1Percentage.Enabled = !isContract && chkIsDiscountInPercent.Checked;
                txtDiscount2Percentage.Enabled = !isContract && chkIsDiscountInPercent.Checked;
                txtDiscountAmount.Enabled = !isContract && !chkIsDiscountInPercent.Checked;
            }
            else
                ChangeBonusState(true);

            cboFabricID.Items.Clear();
            cboFabricID.Text = string.Empty;

            grdItemBalance.Rebind();
            grdSupplierItem.Rebind();
            grdSupplierPrice.Rebind();
        }

        protected void txtConversionFactor_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReferenceSequenceNo.Text) && AppSession.Parameter.IsPOCanChangeConversion)
            {
                var po = new ItemTransactionItem();
                po.SetQtyPricePO(TxtReferenceNo.Text, cboItemID.SelectedValue,
                    txtConversionFactor.Value.ToDecimal(), cboBusinessPartnerID.SelectedValue, true);
                if (!AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                    txtQuantity.Value = po.Quantity.ToDouble();
                else 
                    txtQuantity.Value = txtQtyPending.Value / txtConversionFactor.Value;
                txtPrice.Value = po.Price.ToDouble();
                txtDiscount1Percentage.Value = po.Discount1Percentage.ToDouble();
                txtDiscount2Percentage.Value = po.Discount2Percentage.ToDouble();
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

            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (ChkIsInventoryItem.Checked)
                {
                    var coll = (ItemTransactionItemCollection)Session["PurchaseOrderItems" + Request.UserHostName];
                    var isExist =
                        coll.Any(
                            entity =>
                            entity.ItemID.Equals(cboItemID.SelectedValue) &&
                            entity.IsBonusItem.Equals(chkIsBonusItem.Checked));
                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", cboItemID.SelectedValue);
                        return;
                    }
                }
                if (!chkIsNonMasterOrder.Checked)
                {
                    var coll = (ItemTransactionItemCollection)Session["PurchaseOrderItems" + Request.UserHostName];
                    var isExist =
                        coll.Any(
                            entity =>
                            entity.ItemID.Equals(cboItemID.SelectedValue) &&
                            entity.IsBonusItem.Equals(chkIsBonusItem.Checked) &&
                            entity.Specification.Equals(txtSpecs.Text));
                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} with same spesification has exist", cboItemID.SelectedValue);
                        return;
                    }

                    if (string.IsNullOrEmpty(cboItemID.SelectedValue))
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("The selected item is invalid");
                        return;
                    }

                    var item = new Item();
                    if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("The selected item is invalid");
                        return;
                    }
                }
            }
            if (chkIsNonMasterOrder.Checked && string.IsNullOrEmpty(txtDescription.Text))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Item required");
                return;
            }
            if (!chkIsBonusItem.Checked)
            {
                if (txtPrice.Value == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Price must greather than 0.");
                    return;
                }

                if (!AppSession.Parameter.IsPOCanChangeQty && !string.IsNullOrEmpty(txtReferenceSequenceNo.Text) && txtReferenceSequenceNo.Text != "000")
                {
                    if (Math.Round((txtQuantity.Value ?? 0 * txtConversionFactor.Value ?? 0)) > Math.Round(txtQtyPending.Value ?? 0))
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Quantity can not be greather than " + (txtQtyPending.Value / txtConversionFactor.Value).ToString() + ".");
                        return;
                    }
                }
            }
            if (txtDiscountAmount.Value > txtPrice.Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Discount Amount can not be greather than " + string.Format("{0:n2}", txtPrice.Value));
                return;
            }
        }

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

        public Boolean IsBonusItem
        {
            get { return chkIsBonusItem.Checked; }
        }

        public Boolean IsTaxable
        {
            get { return chkIsTaxable.Checked; }
        }

        public Boolean IsClosed
        {
            get { return chkIsClosed.Checked; }
        }

        public String Specs
        {
            get { return txtSpecs.Text; }
        }

        public String FabricID
        {
            get { return cboFabricID.SelectedValue; }
        }

        public String FabricName
        {
            get { return cboFabricID.Text; }
        }

        protected void chkisBonus_Changed(object sender, EventArgs e)
        {
            ChangeBonusState(chkIsBonusItem.Checked);
        }

        protected void chkIsDiscountInPercent_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledDiscount();
            txtDiscount1Percentage.Value = 0.00;
            txtDiscount2Percentage.Value = 0.00;
            txtDiscountAmount.Value = 0.00;
        }

        private void PopulateItemPrice()
        {
            string baseUnitId = string.Empty;
            decimal priceInBaseUnit = 0;
            decimal priceInPurchaseUnit = 0;
            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var item = new ItemProductMedic();
                item.LoadByPrimaryKey(cboItemID.SelectedValue);
                baseUnitId = item.SRItemUnit;
                priceInBaseUnit = item.PriceInBaseUnit ?? 0;
                priceInPurchaseUnit = item.PriceInPurchaseUnit ?? 0;
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var item = new ItemProductMedic();
                item.LoadByPrimaryKey(cboItemID.SelectedValue);
                baseUnitId = item.SRItemUnit;
                priceInBaseUnit = item.PriceInBaseUnit ?? 0;
                priceInPurchaseUnit = item.PriceInPurchaseUnit ?? 0;
            }
            else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
            {
                var item = new ItemKitchen();
                item.LoadByPrimaryKey(cboItemID.SelectedValue);
                baseUnitId = item.SRItemUnit;
                priceInBaseUnit = item.PriceInBaseUnit ?? 0;
                priceInPurchaseUnit = item.PriceInPurchaseUnit ?? 0;
            }

            txtPrice.Value = cboSRItemUnit.SelectedValue.Equals(baseUnitId)
                                    ? Convert.ToDouble(priceInBaseUnit)
                                    : Convert.ToDouble(priceInPurchaseUnit);

            string transNo = ((RadTextBox)Helper.FindControlRecursive(Page, "txtContractNo")).Text;
            if (transNo != string.Empty)
            {
                var scItem = new SupplierContractItem();
                if (scItem.LoadByPrimaryKey(transNo, cboItemID.SelectedValue))
                {
                    txtDiscount1Percentage.Value = (double)(scItem.PurchaseDiscount1 ?? 0);
                    txtDiscount2Percentage.Value = (double)(scItem.PurchaseDiscount2 ?? 0);
                    txtPrice.Value = (double)(scItem.PriceInPurchaseUnit ?? 0);
                    txtDiscountAmount.Value = (txtPrice.Value * txtDiscount1Percentage.Value / 100) +
                                            ((txtPrice.Value - (txtPrice.Value * txtDiscount1Percentage.Value / 100)) *
                                             txtDiscount2Percentage.Value / 100);
                }
                else
                {
                    var suppItem = new SupplierItem();
                    if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, cboItemID.SelectedValue))
                    {
                        txtDiscount1Percentage.Value = (double)(suppItem.PurchaseDiscount1 ?? 0);
                        txtDiscount2Percentage.Value = (double)(suppItem.PurchaseDiscount2 ?? 0);
                        txtPrice.Value = (double)(suppItem.PriceInPurchaseUnit ?? 0);
                        txtDiscountAmount.Value = (txtPrice.Value * txtDiscount1Percentage.Value / 100) +
                                            ((txtPrice.Value - (txtPrice.Value * txtDiscount1Percentage.Value / 100)) *
                                             txtDiscount2Percentage.Value / 100);
                    }
                }
            }
            else
            {
                var suppItem = new SupplierItem();
                if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, cboItemID.SelectedValue))
                {
                    txtDiscount1Percentage.Value = (double)(suppItem.PurchaseDiscount1 ?? 0);
                    txtDiscount2Percentage.Value = (double)(suppItem.PurchaseDiscount2 ?? 0);
                    txtPrice.Value = (double)(suppItem.PriceInPurchaseUnit ?? 0);
                }
            }
        }

        private void ChangeBonusState(bool status)
        {
            txtPrice.Enabled = !status;
            chkIsDiscountInPercent.Enabled = !status;
            txtDiscount1Percentage.Enabled = !status && chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.Enabled = !status && chkIsDiscountInPercent.Checked;
            txtDiscountAmount.Enabled = !status && !chkIsDiscountInPercent.Checked;

            if (status)
            {
                txtDiscount1Percentage.Value = 0.00;
                txtDiscount2Percentage.Value = 0.00;
                txtDiscountAmount.Value = 0.00;
                txtPrice.Value = 0.00;
            }
            else
                PopulateItemPrice();
        }

        private void SetEnabledPrice(string itemId, bool isItemBonus)
        {
            bool enabled = isItemBonus;
            if (!isItemBonus)
            {
                var scItem = new SupplierContractItem();
                enabled = !scItem.LoadByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtContractNo")).Text, itemId);
            }

            txtPrice.Enabled = enabled;
            chkIsDiscountInPercent.Enabled = enabled;
            txtDiscount1Percentage.Enabled = enabled && chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.Enabled = enabled && chkIsDiscountInPercent.Checked;
            txtDiscountAmount.Enabled = enabled && !chkIsDiscountInPercent.Checked;
        }

        private void SetEnabledDiscount()
        {
            txtDiscount1Percentage.Enabled = chkIsDiscountInPercent.Checked;
            txtDiscount2Percentage.Enabled = chkIsDiscountInPercent.Checked;
            txtDiscountAmount.Enabled = !chkIsDiscountInPercent.Checked;
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (ItemTransactionItemCollection)Session["PurchaseOrderItems" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboSRItemType.Enabled = true;
                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !ChkIsInventoryItem.Checked)
                    chkIsNonMasterOrder.Enabled = true;
                ChkIsInventoryItem.Enabled = true;
                ChkIsConsignment.Enabled = true;
                //if (AppSession.Application.IsModuleAssetActive)
                //    ChkIsAssets.Enabled = true;
            }
        }

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
                decimal priceInPurchaseUnit = 0;

                if (itemType == ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemId);
                    baseUnitId = medic.SRItemUnit;
                    conversionFactor = medic.ConversionFactor ?? 1;
                    priceInBaseUnit = medic.PriceInBaseUnit ?? 0;
                    priceInPurchaseUnit = medic.PriceInPurchaseUnit ?? 0;
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemId);
                    baseUnitId = nonMedic.SRItemUnit;
                    conversionFactor = nonMedic.ConversionFactor ?? 1;
                    priceInBaseUnit = nonMedic.PriceInBaseUnit ?? 0;
                    priceInPurchaseUnit = nonMedic.PriceInPurchaseUnit ?? 0;
                }
                else if (itemType == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(itemId);
                    baseUnitId = kitchen.SRItemUnit;
                    conversionFactor = kitchen.ConversionFactor ?? 1;
                    priceInBaseUnit = kitchen.PriceInBaseUnit ?? 0;
                    priceInPurchaseUnit = kitchen.PriceInPurchaseUnit ?? 0;
                }

                txtConversionFactor.Value = e.Value.Equals(baseUnitId) ? 1 : Convert.ToDouble(conversionFactor);
                txtPrice.Value = e.Value.Equals(baseUnitId)
                                     ? Convert.ToDouble(priceInBaseUnit)
                                     : Convert.ToDouble(priceInPurchaseUnit);

                if (!chkIsBonusItem.Checked)
                {
                    string transNo = ((RadTextBox)Helper.FindControlRecursive(Page, "txtContractNo")).Text;
                    if (transNo != string.Empty)
                    {
                        var scItem = new SupplierContractItem();
                        if (scItem.LoadByPrimaryKey(transNo, cboItemID.SelectedValue))
                            txtPrice.Value = e.Value.Equals(baseUnitId)
                                         ? (double)(scItem.PriceInPurchaseUnit ?? 0) / Convert.ToDouble(conversionFactor)
                                         : (double)(scItem.PriceInPurchaseUnit ?? 0);


                        else
                        {
                            var suppItem = new SupplierItem();
                            if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, cboItemID.SelectedValue))
                                txtPrice.Value = e.Value.Equals(baseUnitId)
                                         ? (double)(suppItem.PriceInPurchaseUnit ?? 0) / Convert.ToDouble(conversionFactor)
                                         : (double)(suppItem.PriceInPurchaseUnit ?? 0);

                        }
                    }
                    else
                    {
                        var suppItem = new SupplierItem();
                        if (suppItem.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue, cboItemID.SelectedValue))
                            txtPrice.Value = e.Value.Equals(baseUnitId)
                                         ? (double)(suppItem.PriceInPurchaseUnit ?? 0) / Convert.ToDouble(conversionFactor)
                                         : (double)(suppItem.PriceInPurchaseUnit ?? 0);

                    }

                }
                else
                    ChangeBonusState(true);

                if (!string.IsNullOrEmpty(TxtReferenceNo.Text) && !string.IsNullOrEmpty(txtReferenceSequenceNo.Text) && txtReferenceSequenceNo.Text != "000" && AppSession.Parameter.IsPOCanChangeConversion)
                {
                    var po = new ItemTransactionItem();
                    po.SetQtyPricePO(TxtReferenceNo.Text, cboItemID.SelectedValue,
                                     txtConversionFactor.Value.ToDecimal(), cboBusinessPartnerID.SelectedValue, true);
                    if (!AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
                        txtQuantity.Value = po.Quantity.ToDouble();
                    else
                        txtQuantity.Value = txtQtyPending.Value / txtConversionFactor.Value;
                    txtPrice.Value = po.Price.ToDouble();
                    txtDiscount1Percentage.Value = po.Discount1Percentage.ToDouble();
                    txtDiscount2Percentage.Value = po.Discount2Percentage.ToDouble();
                }
            }
            else
            {
                txtConversionFactor.Value = 1;
                txtPrice.Value = 0;
            }
        }
        #endregion

        private void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType, string serviceUnitID, string supplierID, string srItemCategory)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var su = new ServiceUnit();
            var locationId = su.GetMainLocationId(serviceUnitID);

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");

            query.LeftJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationId
                );
            query.Where
                (
                    query.SRItemType == itemType,
                    query.ItemID == textSearch,
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(srItemCategory))
                query.Where(query.SRItemCategory == srItemCategory);

            var std = new AppStandardReferenceItemQuery("d");
            var suppItem = new SupplierItemQuery("e");

            if (itemType == ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(prod.IsDirectPurchase == false);
            }
            else if (itemType == ItemType.NonMedical)
            {
                var nmed = new ItemProductNonMedicQuery("f");

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID);
                query.LeftJoin(std).On
                    (
                        nmed.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
            }
            else if (itemType == ItemType.Kitchen)
            {
                var kitchen = new ItemKitchenQuery("f");

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                query.LeftJoin(std).On
                    (
                        kitchen.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
            }

            if (supplierID != string.Empty)
                query.LeftJoin(suppItem).On(
                    query.ItemID == suppItem.ItemID &&
                    suppItem.SupplierID == supplierID
                    );

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        private DataTable ItemBalances
        {
            get
            {
                var balanceQ = new ItemBalanceQuery("a");
                var itemQ = new ItemQuery("b");
                balanceQ.InnerJoin(itemQ).On(balanceQ.ItemID == itemQ.ItemID);

                var locationQ = new LocationQuery("c");
                balanceQ.InnerJoin(locationQ).On(balanceQ.LocationID == locationQ.LocationID);

                var itemProductMedicQ = new ItemProductMedicQuery("d");
                balanceQ.LeftJoin(itemProductMedicQ).On(balanceQ.ItemID == itemProductMedicQ.ItemID);

                var itemProductNonMedicQ = new ItemProductNonMedicQuery("e");
                balanceQ.LeftJoin(itemProductNonMedicQ).On(balanceQ.ItemID == itemProductNonMedicQ.ItemID);

                var itemKitchenQ = new ItemKitchenQuery("f");
                balanceQ.LeftJoin(itemKitchenQ).On(balanceQ.ItemID == itemKitchenQ.ItemID);

                balanceQ.Where(
                    itemQ.SRItemType == cboSRItemType.SelectedValue,
                    balanceQ.ItemID == cboItemID.SelectedValue,
                    balanceQ.Balance > 0
                    );

                balanceQ.Select
                (
                    balanceQ.ItemID,
                    itemQ.ItemName,
                    locationQ.LocationName,
                    balanceQ.Balance,
                    balanceQ.Booking,
                    balanceQ.Minimum,
                    balanceQ.Maximum
                );

                switch (cboSRItemType.SelectedValue)
                {
                    case ItemType.Medical:
                        balanceQ.Select(itemProductMedicQ.SRItemUnit);
                        break;
                    case ItemType.NonMedical:
                        balanceQ.Select(itemProductNonMedicQ.SRItemUnit);
                        break;
                    case ItemType.Kitchen:
                        balanceQ.Select(itemKitchenQ.SRItemUnit);
                        break;
                }

                balanceQ.es.Top = AppSession.Parameter.MaxResultRecord;

                return balanceQ.LoadDataTable();
            }
        }

        private DataTable SupplierItems
        {
            get
            {
                var query = new SupplierItemQuery("a");
                var supp = new SupplierQuery("b");
                var item = new VwItemProductMedicNonMedicQuery("c");
                query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);

                query.Where(query.ItemID == cboItemID.SelectedValue);

                query.Select
                    (
                        query.SupplierID,
                        query.ItemID,
                        supp.SupplierName,
                        query.PurchaseDiscount1,
                        query.PurchaseDiscount2,
                        query.SRPurchaseUnit,
                        query.PriceInPurchaseUnit,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        query.DrugDistributionLicenseNo,
                        @"<ISNULL(a.ConversionFactor, c.ConversionFactor) AS ConversionFactor>"
                    );

                query.OrderBy(query.LastUpdateDateTime.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                return query.LoadDataTable();
            }
        }

        private DataTable SupplierPrices
        {
            get
            {
                var itq = new ItemTransactionQuery("a");
                var itdq = new ItemTransactionItemQuery("b");
                var supp = new SupplierQuery("c");

                itq.es.Top = AppSession.Parameter.MaxResultRecord;
                itq.Select
                    (
                        itq.BusinessPartnerID.As("SupplierID"),
                        supp.SupplierName,
                        itdq.Discount1Percentage.As("PurchaseDiscount1"),
                        itdq.Discount2Percentage.As("PurchaseDiscount2"),
                        itdq.SRItemUnit.As("SRPurchaseUnit"),
                        itdq.PriceInCurrency.As("PriceInPurchaseUnit"),
                        itdq.LastUpdateDateTime,
                        itdq.LastUpdateByUserID,
                        itdq.ConversionFactor
                    );
                itq.InnerJoin(itdq).On(itq.TransactionNo == itdq.TransactionNo && itdq.ItemID == cboItemID.SelectedValue && itdq.PriceInCurrency < (txtPrice.Value ?? 0));
                itq.InnerJoin(supp).On(itq.BusinessPartnerID == supp.SupplierID);
                itq.Where(itq.TransactionCode.In(TransactionCode.PurchaseOrder.ToString(), TransactionCode.PurchaseOrderReceive.ToString()), itq.BusinessPartnerID != cboBusinessPartnerID.SelectedValue, itq.IsApproved == true);
                itq.OrderBy(itq.LastUpdateDateTime.Descending);

                return itq.LoadDataTable();
            }
        }

        protected void grdItemBalance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = ItemBalances;
        }

        protected void grdSupplierItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = SupplierItems;
        }

        protected void grdSupplierPrice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = SupplierPrices;
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