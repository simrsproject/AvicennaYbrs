using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueRequestItemDetail : BaseUserControl
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

        private RadComboBox cboToServiceUnitID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID");
            }
        }

        private RadComboBox cboLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboToLocationID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboServiceUnitID.Enabled = false;
            cboToServiceUnitID.Enabled = false;
            cboLocationID.Enabled = false;
            cboSRItemType.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName];
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
            PopulateItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ItemID);
            txtSequenceNo.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SequenceNo);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.Quantity));
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

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemTransactionItemCollection coll = (ItemTransactionItemCollection)Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName];

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
            set { txtSequenceNo.Text = value; }
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

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["InventoryIssueRequest:collItemTransactionItem" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboServiceUnitID.Enabled = true;
                cboToServiceUnitID.Enabled = true;
                cboLocationID.Enabled = true;
                cboSRItemType.Enabled = true;
            }
        }

        #region ComboBox ItemID

        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateItemID((RadComboBox)sender, e.Text, true);
        }

        private void PopulateItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string locationID = cboLocationID.SelectedValue;
            string searchTextContain = string.Format("%{0}%", textSearch);
            ItemQuery query = new ItemQuery("a");
            ItemBalanceQuery bal = new ItemBalanceQuery("b");
            query.InnerJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationID
                );

            query.Where(query.SRItemType == cboSRItemType.SelectedItem.Value);

            if (isNew)
            {
                query.Where
               (
                   query.IsActive == true,
                   query.Or(query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)));
            }
            else
                query.Where(query.ItemID == textSearch);

            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
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
                query.Where(prod.IsInventoryItem == true, query.IsActive == true);
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
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
                query.Where(prod.IsInventoryItem == true, query.IsActive == true);
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    if (Request.QueryString["type"] == "PurchaseCat-001")
                        query.Where(prod.SRPurchaseCategorization == "PurchaseCat-001");
                    else
                        query.Where(prod.SRPurchaseCategorization != "PurchaseCat-001");
                }
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
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
                query.Where(prod.IsInventoryItem == true, query.IsActive == true);
            }
            query.OrderBy(query.ItemName.Ascending);

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