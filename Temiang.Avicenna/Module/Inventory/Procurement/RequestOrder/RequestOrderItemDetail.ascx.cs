using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderItemDetail : BaseUserControl
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

        private RadComboBox cboLocationID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboLocationID"); }
        }

        private RadComboBox cboToServiceUnit
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID"); }
        }

        private RadComboBox cboBusinessPartnerID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID"); }
        }

        private RadComboBox cboSRProductAccountID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRProductAccountID"); }
        }

        private RadComboBox cboCategorization
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboCategorization"); }
        }

        private CheckBox chkIsNonMasterOrder
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsNonMasterOrder"); }
        }

        private CheckBox ChkIsInventoryItem
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsInventoryItem"); }
        }

        private CheckBox ChkIsConsignment
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsConsignment"); }
        }

        private CheckBox ChkIsAssets
        {
            get { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsAssets"); }
        }

        private RadDatePicker txtTransactionDate
        {
            get
            {
                return (RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate");
            }
        }
        private RadTextBox txtTransactionNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtTransactionNo");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            pnlBudgetPlan.Visible = false;
            tblPriceInfo.Visible = false;
            if (!string.IsNullOrEmpty(Session["PurchaseRequestForWorkOrder" + Request.UserHostName].ToString()))
                tblPriceInfo.Visible = true;

            if (AppSession.Parameter.IsShowPriceInPurchaseRequest)
            {
                tblPriceInfo.Visible = true;
            }

            if (chkIsNonMasterOrder.Checked)
            {
                cboItemID.Visible = false;
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
                txtQtyUnitConversion.Value = 1;
            }
            else
                txtDescription.Visible = false;

            cboSRItemType.Enabled = false;
            cboSRItemCategory.Enabled = false;
            chkIsNonMasterOrder.Enabled = false;
            ChkIsInventoryItem.Enabled = false;
            ChkIsConsignment.Enabled = false;
            //if (AppSession.Application.IsModuleAssetActive)
            //    ChkIsAssets.Enabled = false;

            txtQuantity.Value = 0;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["SequenceNo"] = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }

                return;
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);


            var isPorByStockGroup = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup);

            ComboBox.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue,
                            cboLocationID.SelectedValue, string.Empty,
                            string.Empty, false, ChkIsInventoryItem.Checked,
                            ChkIsConsignment.Checked, isPorByStockGroup, string.Empty, ChkIsInventoryItem.Checked ? AppSession.Parameter.IsPurchaseRequestBasedOnItemsPerLocation : false, 
                            AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory ? cboSRItemCategory.SelectedValue : string.Empty, ChkIsAssets.Checked, false);
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            txtQtyUnitConversion.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));
            txtDescription.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Description);
            txtSpecification.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Specification);
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Price));
            txtDiscount1Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage));
            txtDiscount2Percentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage));

            string unitConversion = string.Empty;
            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(cboItemID.SelectedValue);
                unitConversion = medic.SRItemUnit;
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(cboItemID.SelectedValue);
                unitConversion = medic.SRItemUnit;
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(cboItemID.SelectedValue);
                unitConversion = medic.SRItemUnit;
            }

            if (!chkIsNonMasterOrder.Checked)
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, cboSRItemType.SelectedValue);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
            txtItemUnitConversion.Text = unitConversion;

            PopulateBudgetPlan(cboItemID.SelectedValue);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            //Check Qty
            if (txtQuantity.Value == null || txtQuantity.Value < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Qty must greather than 0";
            }

            if (string.IsNullOrEmpty(cboSRItemUnit.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Unit Item";
            }

            if (txtDiscount1Percentage.Value != null && txtDiscount1Percentage.Value > 100.00)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Discount 1 (%) cannot greather than 100";
            }

            if (txtDiscount2Percentage.Value != null && txtDiscount2Percentage.Value > 100.00)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Discount 2 (%) cannot greather than 100";
            }

            //Check duplicate key, hanya dicek kalo item inventory
            //if (ViewState["IsNewRecord"].Equals(true) && ChkIsInventoryItem.Checked)
            //{
            //    ItemTransactionItemCollection coll =
            //        (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];

            //    string itemID = cboItemID.SelectedValue;
            //    bool isExist = false;
            //    if (chkIsNonMasterOrder.Checked == false)
            //    {
            //        if (coll.Any(row => row.ItemID.Equals(itemID)))
            //        {
            //            isExist = true;
            //        }
            //    }

            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
            //    }
            //}

            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (ChkIsInventoryItem.Checked)
                {
                    var coll = (ItemTransactionItemCollection) Session["RequestOrderItems" + Request.UserHostName];
                    var isExist =
                        coll.Any(
                            entity =>
                            entity.ItemID.Equals(cboItemID.SelectedValue));
                    if (isExist)
                    {
                        args.IsValid = false;
                        ((CustomValidator) source).ErrorMessage = string.Format("ID: {0} has exist",
                                                                                cboItemID.SelectedValue);
                        return;
                    }
                }
                if (!chkIsNonMasterOrder.Checked)
                {
                    if (string.IsNullOrEmpty(cboItemID.SelectedValue))
                    {
                        args.IsValid = false;
                        ((CustomValidator) source).ErrorMessage = string.Format("The selected item is invalid");
                        return;
                    }

                    var item = new Item();
                    if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        args.IsValid = false;
                        ((CustomValidator) source).ErrorMessage = string.Format("The selected item is invalid");
                        return;
                    }
                }
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
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
            {
                return Convert.ToDecimal(txtQtyUnitConversion.Value);
            }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }

        public String Specification
        {
            get { return txtSpecification.Text; }
        }

        public Decimal Price
        {
            get
            {
                return Convert.ToDecimal(txtPrice.Value);
            }
        }
        public Decimal Discount1Percentage
        {
            get
            {
                return Convert.ToDecimal(txtDiscount1Percentage.Value);
            }
        }
        public Decimal Discount2Percentage
        {
            get
            {
                return Convert.ToDecimal(txtDiscount2Percentage.Value);
            }
        }

        public String SRMasterBaseUnit
        {
            get { return ViewState["SRMasterBaseUnit"].ToString(); }
            set { ViewState["SRMasterBaseUnit"] = value; }
        }

        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboSRItemType.Enabled = true;
                cboSRItemCategory.Enabled = true;
                ChkIsInventoryItem.Enabled = true;
                chkIsNonMasterOrder.Enabled = AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !ChkIsInventoryItem.Checked;
                ChkIsConsignment.Enabled = true;
                //ChkIsAssets.Enabled = true;
            }
        }

        #region ComboBox ItemID

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var isPorByStockGroup = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup);

            var supplierID = string.Empty;
            var productAccountID = string.Empty;

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem))
            {
                supplierID = cboBusinessPartnerID.SelectedValue;
            }
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount))
                productAccountID = cboSRProductAccountID.SelectedValue;

            var isSeparationOfItemPurchaseCategorization = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsSeparationOfItemPurchaseCategorization);

            ComboBox.ItemItemsRequested((RadComboBox) sender, e.Text, cboSRItemType.SelectedValue,
                                        cboLocationID.SelectedValue, supplierID,
                                        productAccountID, false, ChkIsInventoryItem.Checked,
                                        ChkIsConsignment.Checked, isPorByStockGroup,
                                        isSeparationOfItemPurchaseCategorization
                                            ? cboCategorization.SelectedValue
                                            : string.Empty, ChkIsInventoryItem.Checked ? AppSession.Parameter.IsPurchaseRequestBasedOnItemsPerLocation : false, 
                                        AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory ? cboSRItemCategory.SelectedValue : string.Empty, ChkIsAssets.Checked, true);

        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var itemType = cboSRItemType.SelectedValue ?? string.Empty;
            // Satuan unit yg dipakai selalu dalam Purchase Unit
            var selectedValue = e.Value ?? string.Empty;
            if (!chkIsNonMasterOrder.Checked)
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, selectedValue, itemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            txtDescription.Text = e.Text;


            var entity = new ItemTransactionItem();
            entity.ItemID = selectedValue;

            string purchaseUnit = string.Empty;
            string baseUnit = string.Empty;
            decimal convertionPurchaseUnitToBaseUnit = 1;
            var supplierID = cboBusinessPartnerID.SelectedValue ?? string.Empty;
            ProcurementUtils.PopulateWithHistPrice(entity, itemType, supplierID, ref  purchaseUnit, ref  baseUnit, ref  convertionPurchaseUnitToBaseUnit);

            SRMasterBaseUnit = baseUnit;
            txtDiscount1Percentage.Value = (double)(entity.Discount1Percentage ?? 0);
            txtDiscount2Percentage.Value = (double)(entity.Discount2Percentage ?? 0);

            ComboBox.SelectedValue(cboSRItemUnit, string.IsNullOrEmpty(purchaseUnit) ? "PCS" : purchaseUnit);
            txtPrice.Value = (double)(entity.Price ?? 0);
            txtQtyUnitConversion.Value = (double)convertionPurchaseUnitToBaseUnit;
            txtItemUnitConversion.Text = baseUnit;



            //string purcUnitId = string.Empty;
            //SRMasterBaseUnit = string.Empty;
            //decimal? factor = 0;
            //double priceInPurchaseUnit = 0;
            //string unitConversion = string.Empty;

            //if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            //{
            //    var medic = new ItemProductMedic();
            //    medic.LoadByPrimaryKey(selectedValue);
            //    purcUnitId = medic.SRPurchaseUnit;
            //    factor = medic.ConversionFactor;
            //    unitConversion = medic.SRItemUnit;
            //    SRMasterBaseUnit = medic.SRItemUnit;

            //    txtDiscount1Percentage.Value = (double)(medic.PurchaseDiscount1 ?? 0);
            //    txtDiscount2Percentage.Value = (double)(medic.PurchaseDiscount2 ?? 0);
            //    priceInPurchaseUnit = (double)(medic.PriceInPurchaseUnit ?? 0);
            //}
            //else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            //{
            //    var medic = new ItemProductNonMedic();
            //    medic.LoadByPrimaryKey(selectedValue);
            //    purcUnitId = medic.SRPurchaseUnit;
            //    factor = medic.ConversionFactor;
            //    unitConversion = medic.SRItemUnit;
            //    SRMasterBaseUnit = medic.SRItemUnit;

            //    txtDiscount1Percentage.Value = (double)(medic.PurchaseDiscount1 ?? 0);
            //    txtDiscount2Percentage.Value = (double)(medic.PurchaseDiscount2 ?? 0);
            //    priceInPurchaseUnit = (double)(medic.PriceInPurchaseUnit ?? 0);
            //}
            //else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
            //{
            //    var k = new ItemKitchen();
            //    k.LoadByPrimaryKey(selectedValue);
            //    purcUnitId = k.SRPurchaseUnit;
            //    factor = k.ConversionFactor;
            //    unitConversion = k.SRItemUnit;
            //    SRMasterBaseUnit = k.SRItemUnit;

            //    txtDiscount1Percentage.Value = (double)(k.PurchaseDiscount1 ?? 0);
            //    txtDiscount2Percentage.Value = (double)(k.PurchaseDiscount2 ?? 0);
            //    priceInPurchaseUnit = (double)(k.PriceInPurchaseUnit ?? 0);
            //}


            //// Override bila ada price history di supplier
            //var suppItem = new SupplierItem();
            //var suppID = ((RadComboBox)Helper.FindControlRecursive(Page, "cboBusinessPartnerID")).SelectedValue ?? string.Empty;
            //if (suppItem.LoadByPrimaryKey(suppID, selectedValue))
            //{
            //    txtDiscount1Percentage.Value = (double)(suppItem.PurchaseDiscount1 ?? 0);
            //    txtDiscount2Percentage.Value = (double)(suppItem.PurchaseDiscount2 ?? 0);
            //    priceInPurchaseUnit = (double)(suppItem.PriceInPurchaseUnit ?? 0);
            //}

            //txtDescription.Text = e.Text;
            //ComboBox.SelectedValue(cboSRItemUnit, string.IsNullOrEmpty(purcUnitId) ? "PCS" : purcUnitId);
            //txtPrice.Value = SRMasterBaseUnit == cboSRItemUnit.SelectedValue
            //    ? priceInPurchaseUnit / (factor ?? 0).ToDouble()
            //    : priceInPurchaseUnit;

            //txtQtyUnitConversion.Value = Convert.ToDouble(factor == 0 ? 1 : factor);
            //txtItemUnitConversion.Text = unitConversion;

            PopulateBudgetPlan(selectedValue);
        }

        private void PopulateBudgetPlan(string ItemID)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            if (itemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                if (AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan)
                {
                    // budget plan validation is applied for request to MainDistributionLocation only
                    var svcUnit = new ServiceUnit();
                    if (svcUnit.LoadByPrimaryKey(cboToServiceUnit.SelectedValue))
                    {
                        if (svcUnit.ServiceUnitID == AppSession.Parameter.MainPurchasingUnitIDForNonMedical)
                        {
                            pnlBudgetPlan.Visible = true;
                            // lihat budget plan approval
                            var iti = new ItemTransactionItem();
                            decimal qtyBp = iti.GetCountBudgetPlan(cboServiceUnit.SelectedValue, cboToServiceUnit.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                "");
                            txtQuota.Value = (double)qtyBp;

                            // lihat jumlah yang sudah pernah diajukan
                            decimal qtyOffered = iti.GetCountBudgetPlanRealization(
                                cboToServiceUnit.SelectedValue,
                                cboServiceUnit.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                (bool)ViewState["IsNewRecord"] ? "" : txtTransactionNo.Text, false);
                            txtQtyOffered.Value = (double)qtyOffered;

                            txtBalace.Value = (double)qtyBp - (double)qtyOffered;

                            txtQuantity.MaxValue = (txtBalace.Value ?? 0) / (txtQtyUnitConversion.Value ?? 1);
                            if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
                        }
                    }
                }
                else
                    PopulateMaxValue(ItemID);
            }
            else
                PopulateMaxValue(ItemID);
        }
        
        private void PopulateMaxValue(string itemId)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            bool needValidateMax = false;
            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboLocationID.SelectedValue))
            {
                switch (itemType)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        needValidateMax = loc.IsValidateMaxValueOnPurcReqForIpm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        needValidateMax = loc.IsValidateMaxValueOnPurcReqForIpnm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        needValidateMax = loc.IsValidateMaxValueOnPurcReqForIk ?? false;
                        break;
                }
            }

            if (needValidateMax)
            {
                var ib = new ItemBalance();
                if (ib.LoadByPrimaryKey(cboLocationID.SelectedValue, itemId))
                {
                    txtQuantity.MaxValue = (Convert.ToDouble(ib.Maximum) - Convert.ToDouble(ib.Balance)) / (txtQtyUnitConversion.Value ?? 1);
                    if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
                }
            }
        }
        #endregion

        #region ComboBox SRItemUnit
        protected void cboSRItemUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                if (!chkIsNonMasterOrder.Checked)
                {
                    var entity = new ItemTransactionItem();
                    entity.ItemID = cboItemID.SelectedValue ?? string.Empty;

                    var priceInItemUnit = cboSRItemUnit.SelectedValue;
                    var itemType = cboSRItemType.SelectedItem.Value ?? string.Empty;
                    var supplierID = cboBusinessPartnerID.SelectedValue ?? string.Empty;
                    ProcurementUtils.PopulateWithHistPrice(entity, itemType, supplierID, priceInItemUnit);

                    txtDiscount1Percentage.Value = (double) (entity.Discount1Percentage ?? 0);
                    txtDiscount2Percentage.Value = (double) (entity.Discount2Percentage ?? 0);

                    txtPrice.Value = (double) (entity.Price ?? 0);

                    txtQtyUnitConversion.Value = (double) (entity.ConversionFactor ?? 1);
                }
                else
                {
                    txtQtyUnitConversion.Value = 1;
                    txtItemUnitConversion.Text = cboSRItemUnit.SelectedValue;

                }
            }

        }
        #endregion
    }
}