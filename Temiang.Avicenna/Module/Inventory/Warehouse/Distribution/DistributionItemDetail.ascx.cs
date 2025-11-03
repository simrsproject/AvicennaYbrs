using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionItemDetail : BaseUserControl
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

            cboSRItemType.Enabled = false;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;

            cboItemID.Enabled = true;
            trBooking.Visible= (!AppSession.Parameter.IsDistributionAutoConfirm);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collitem = (ItemTransactionItemCollection)Session["DistributionItems" + Request.UserHostName];
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
            
            cboItemID.Enabled = false;

            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            ComboBox.SelectedValue(cboItemID,(String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID));
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, cboSRItemType.SelectedValue);
            ComboBox.SelectedValue(cboSRItemUnit, (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ConversionFactor));

            GetStockInfo(cboItemID.SelectedValue);
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
                    (ItemTransactionItemCollection)Session["DistributionItems" + Request.UserHostName];

                bool isExist = false;

                if (coll != null)
                {
                    string itemID = cboItemID.SelectedValue;
                    foreach (ItemTransactionItem row in coll)
                    {
                        if (row.ItemID.Equals(itemID))
                        {
                            isExist = true;
                            break;
                        }
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

        public Decimal Balance
        {
            get { return Convert.ToDecimal(txtBalance.Value); }
        }

        public Decimal Booking
        {
            get { return Convert.ToDecimal(txtBooking.Value); }
        }

        public Decimal Minimum
        {
            get { return Convert.ToDecimal(txtMinimum.Value); }
        }

        public Decimal Maximum
        {
            get { return Convert.ToDecimal(txtMaximum.Value); }
        }

        #endregion

        #region Method & Event TextChanged
        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (ItemTransactionItemCollection)Session["DistributionItems" + Request.UserHostName];
            if (collitem.Count == 0)
                cboSRItemType.Enabled = true;
        }
        #endregion

        #region ComboBox ItemID
        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, true);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            query.InnerJoin(bal).On(query.ItemID == bal.ItemID & bal.LocationID == cboFromLocationID.SelectedValue);

            query.Where(
                query.SRItemType == cboSRItemType.SelectedItem.Value, query.IsActive == true);

            if (AppSession.Parameter.IsDistributionOnlyInStock)
                query.Where(bal.Balance > 0);

            string itemGroupID = cboItemGroupID.SelectedValue;
            if (!string.IsNullOrEmpty(itemGroupID))
            {
                query.Where(query.ItemGroupID == itemGroupID);
            }

            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");

                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum,
                    std.ItemName.As("Unit"));
                if (isNew)
                {
                    query.Where(prod.IsInventoryItem == true);
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    if (cboItemID.Enabled == true)
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                    else
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                }
                else
                    query.Where(query.ItemID == textSearch);
                
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                var prod = new ItemProductNonMedicQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");

                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum,
                    std.ItemName.As("Unit"));
                if (isNew)
                {
                    query.Where(prod.IsInventoryItem == true);
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    if (cboItemID.Enabled == true)
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                    else
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                }
                else
                    query.Where(query.ItemID == textSearch);

            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
            {
                var prod = new ItemKitchenQuery("c");
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");

                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum,
                    std.ItemName.As("Unit"));
                query.Where(prod.IsInventoryItem == true);
                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    if (cboItemID.Enabled == true)
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                    else
                    {
                        query.Where(
                            query.Or
                                (
                                    prod.Barcode == textSearch,
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                            );
                    }
                }
                else
                    query.Where(query.ItemID == textSearch);

            }
            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count == 1)
            {
                comboBox.SelectedValue = (dtb.Rows[0]["ItemID"]).ToString();
                comboBox.Text = (dtb.Rows[0]["ItemName"]).ToString();
                txtQuantity.Focus();
            }
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            var itemID = e.Value;
            PopulateWithItemID(itemID, itemType);
        }

        public void PopulateWithItemID(string itemID, string itemType)
        {
            ComboBox.PopulateWithItemUnit(cboSRItemUnit, itemID, itemType);

            string baseUnitId = string.Empty;
            txtQuantity.MaxLength = 10;
            txtQuantity.MinValue = 0;
            txtQuantity.MaxValue = 99999999.99;

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

            GetStockInfo(itemID);
            PopulateBudgetPlan(itemID);
        }

        protected void GetStockInfo(string itemId)
        {
            txtBalance.Value = 0;
            txtBooking.Value = 0;
            txtMinimum.Value = 0;
            txtMaximum.Value = 0;

            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(cboToLocationID.SelectedValue, itemId))
            {
                txtBalance.Value = Convert.ToDouble(ib.Balance);
                txtBooking.Value = Convert.ToDouble(ib.Booking);
                txtMinimum.Value = Convert.ToDouble(ib.Minimum);
                txtMaximum.Value = Convert.ToDouble(ib.Maximum);
            }
        }

        private void PopulateBudgetPlan(string ItemID)
        {
            string itemType = cboSRItemType.SelectedItem.Value;
            if (itemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                if (AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan)
                {
                    // budget plan validation is applied for ditribution from MainDistributionLocation only
                    var svcUnit = new ServiceUnit();
                    if (svcUnit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
                    {
                        if (cboFromLocationID.SelectedValue == AppSession.Parameter.MainDistributionLocationIDForNonMedical)
                        {
                            pnlBudgetPlan.Visible = true;
                            // lihat budget plan approval
                            var iti = new ItemTransactionItem();
                            decimal qtyBp = iti.GetCountBudgetPlan(cboToServiceUnitID.SelectedValue, cboFromServiceUnitID.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                "");
                            txtBpQuota.Value = (double)qtyBp;

                            // lihat jumlah yang sudah pernah distribusi
                            decimal qtyOffered = iti.GetCountBudgetPlanRealization(cboFromServiceUnitID.SelectedValue, 
                                cboToServiceUnitID.SelectedValue, ItemID,
                                txtTransactionDate.SelectedDate.Value.Year,
                                (bool)ViewState["IsNewRecord"] ? "" : txtTransactionNo.Text, false);
                            txtBpQtyOffered.Value = (double)qtyOffered;

                            txtBpQtySaldo.Value = (double)qtyBp - (double)qtyOffered;

                            txtQuantity.MaxValue = (txtBpQtySaldo.Value ?? 0) / (txtConversionFactor.Value ?? 1);
                            if (txtQuantity.MaxValue < 0) txtQuantity.MaxValue = 0;
                        }
                    }
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
        }
        #endregion
    }
}