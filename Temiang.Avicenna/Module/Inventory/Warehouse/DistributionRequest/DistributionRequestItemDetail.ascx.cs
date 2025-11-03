using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionRequestItemDetail : BaseUserControl
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
        private RadComboBox cboFromServiceUnitID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID");
            }
        }
        private RadComboBox cboFromLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromLocationID");
            }
        }
        private RadComboBox cboItemGroupID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboItemGroupID");
            }
        }
        private RadComboBox cboToServiceUnitID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID");
            }
        }
        private RadComboBox cboToLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboToLocationID");
            }
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
            pnlStockInfo.Visible = AppSession.Parameter.IsShowBalanceInfoInDistributionRequest;

            cboSRItemType.Enabled = false;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;

            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            cboFromServiceUnitID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboFromLocationID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToLocationID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["DistRequest:collItemTransactionItem" + Request.UserHostName];
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

                return;
            }
            ViewState["IsNewRecord"] = false;

            string locId;
            if (isListItemBaseOnLocationTo)
                locId = cboToLocationID.SelectedValue;
            else locId = cboFromLocationID.SelectedValue;

            if (AppSession.Parameter.IsDistributionMenuIsUsedAsItemRequestMenu)
            {
                ComboBox.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue,
                                locId, string.Empty, false, true, false, string.Empty, cboItemGroupID.SelectedValue, string.Empty, false);
            }
            else
            {
                ComboBox.ItemItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), cboSRItemType.SelectedValue, locId, cboItemGroupID.SelectedValue, false);

            }

            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, cboSRItemType.SelectedValue);
            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));

            PopulateBudgetPlan(cboItemID.SelectedValue);
            PopulateStockInfo(cboItemID.SelectedValue);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            //Check Entry ItemID
            var qrItem = new ItemQuery();
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemName == cboItemID.Text);
            var item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemTransactionItemCollection coll =
                    (ItemTransactionItemCollection)Session["DistRequest:collItemTransactionItem" + Request.UserHostName];

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

            //Cek budget plan

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
        public String SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }
        #endregion

        #region Method & Event TextChanged

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            RadComboBox cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["DistRequest:collItemTransactionItem" + Request.UserHostName];
            if (collitem != null && collitem.Count == 0)
                cbo.Enabled = true;
        }


        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string itemGroupID = cboItemGroupID.SelectedValue;
            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            if (AppSession.Parameter.IsDistributionMenuIsUsedAsItemRequestMenu)
            {
                ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue,
                                isListItemBaseOnLocationTo ? cboToLocationID.SelectedValue : cboFromLocationID.SelectedValue,
                                string.Empty, false, true, null, string.Empty, itemGroupID, string.Empty, true);
            }
            else
            {
                ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue,
                    isListItemBaseOnLocationTo ? cboToLocationID.SelectedValue : cboFromLocationID.SelectedValue,
                    itemGroupID, true);
            }
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        protected void cboItemID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            var itemID = e.Value;
            PopulateWithItemID(itemID, itemType);
        }

        public void PopulateWithItemID(string itemID, string itemType)
        {
            txtQuantity.MaxLength = 10;
            txtQuantity.MinValue = 0;
            txtQuantity.MaxValue = 99999999.99;

            string baseUnitId = string.Empty;
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, itemID, itemType);

            if (itemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemID);
                baseUnitId = medic.SRItemUnit;
            }
            else if (itemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(itemID);
                baseUnitId = medic.SRItemUnit;
            }
            else if (itemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(itemID);
                baseUnitId = medic.SRItemUnit;
            }
            ComboBox.SelectedValue(cboSRItemUnit, baseUnitId);
            txtConversionFactor.Value = 1;
            PopulateBudgetPlan(itemID);
            PopulateStockInfo(itemID);
        }

        private void PopulateStockInfo(string itemId)
        {
            var bal = new ItemBalance();
            if (bal.LoadByPrimaryKey(cboFromLocationID.SelectedValue, itemId))
                txtBalanceFrom.Value = Convert.ToDouble(bal.Balance);
            else txtBalanceFrom.Value = 0;

            bal = new ItemBalance();
            if (bal.LoadByPrimaryKey(cboToLocationID.SelectedValue, itemId))
                txtBalanceTo.Value = Convert.ToDouble(bal.Balance);
            else txtBalanceTo.Value = 0;
        }

        private void PopulateBudgetPlan(string itemId)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            if (itemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                if (AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan)
                {
                    // budget plan validation is applied for request to MainDistributionLocation only
                    var svcUnit = new ServiceUnit();
                    if (svcUnit.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                    {
                        if (cboToLocationID.SelectedValue == AppSession.Parameter.MainDistributionLocationIDForNonMedical)
                        {
                            pnlBudgetPlan.Visible = true;
                            // lihat budget plan approval
                            var iti = new ItemTransactionItem();
                            decimal qtyBp = iti.GetCountBudgetPlan(cboFromServiceUnitID.SelectedValue, cboToServiceUnitID.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                "");
                            txtQuota.Value = (double)qtyBp;

                            // lihat jumlah yang sudah pernah diajukan
                            decimal qtyOffered = iti.GetCountBudgetPlanRealization(
                                cboToServiceUnitID.SelectedValue, cboFromServiceUnitID.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                (bool)ViewState["IsNewRecord"] ? "" : txtTransactionNo.Text, false);
                            txtQtyOffered.Value = (double)qtyOffered;

                            txtBalace.Value = (double)qtyBp - (double)qtyOffered;

                            txtQuantity.MaxValue = (txtBalace.Value ?? 0) / (txtConversionFactor.Value ?? 1);
                            if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
                        }
                    }
                }
                else
                    PopulateMaxValue(itemId);
            }
            else
                PopulateMaxValue(itemId);
        }

        private void PopulateMaxValue(string itemId)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            bool needValidateMax = false;
            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue))
            {
                switch (itemType)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        needValidateMax = loc.IsValidateMaxValueOnDistReqForIpm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        needValidateMax = loc.IsValidateMaxValueOnDistReqForIpnm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        needValidateMax = loc.IsValidateMaxValueOnDistReqForIk ?? false;
                        break;
                }
            }

            if (needValidateMax)
            {
                var ib = new ItemBalance();
                if (ib.LoadByPrimaryKey(cboFromLocationID.SelectedValue, itemId))
                {
                    txtQuantity.MaxValue = (Convert.ToDouble(ib.Maximum) - Convert.ToDouble(ib.Balance)) / (txtConversionFactor.Value ?? 1);
                    if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
                }

                if (AppSession.Parameter.IsDistributionRequestMustNotExceedCWStock)
                {
                    var ib2 = new ItemBalance();
                    if (ib2.LoadByPrimaryKey(cboToLocationID.SelectedValue, itemId))
                    {
                        if (txtQuantity.MaxValue > Convert.ToDouble(ib2.Balance) / (txtConversionFactor.Value ?? 1))
                            txtQuantity.MaxValue = Convert.ToDouble(ib2.Balance) / (txtConversionFactor.Value ?? 1);
                    }
                    else
                        txtQuantity.MaxValue = 0;
                }
            }

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

                if (itemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(itemId);
                    baseUnitId = medic.SRItemUnit;
                    conversionFactor = medic.ConversionFactor ?? 1;
                }
                else if (itemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var nonMedic = new ItemProductNonMedic();
                    nonMedic.LoadByPrimaryKey(itemId);
                    baseUnitId = nonMedic.SRItemUnit;
                    conversionFactor = nonMedic.ConversionFactor ?? 1;
                }
                else if (itemType == BusinessObject.Reference.ItemType.Kitchen)
                {
                    var k = new ItemKitchen();
                    k.LoadByPrimaryKey(itemId);
                    baseUnitId = k.SRItemUnit;
                    conversionFactor = k.ConversionFactor ?? 1;
                }
                txtConversionFactor.Value = e.Value.Equals(baseUnitId) ? 1 : Convert.ToDouble(conversionFactor);
            }
            else
                txtConversionFactor.Value = 1;

            PopulateBudgetPlan(cboItemID.SelectedValue);
        }
        #endregion
    }
}