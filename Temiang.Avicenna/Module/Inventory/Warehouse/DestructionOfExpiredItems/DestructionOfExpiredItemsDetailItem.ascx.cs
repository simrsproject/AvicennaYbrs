using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DestructionOfExpiredItemsDetailItem : BaseUserControl
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

        private RadComboBox cboLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromLocationID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboServiceUnitID.Enabled = false;
            cboSRItemType.Enabled = false;
            cboLocationID.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collitem = (ItemTransactionItemCollection)Session["collDestructionOfExpiredItems" + Request.UserHostName];
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
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
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

            var balance = new ItemBalance();
            if (!balance.LoadByPrimaryKey(cboLocationID.SelectedValue, cboItemID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item is not have balance quantity";
                return;
            }
            if (balance.Balance <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item is not have sufficient balance quantity";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemTransactionItemCollection)Session["collDestructionOfExpiredItems" + Request.UserHostName];

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
            get { return txtSRItemUnit.Text; }
        }

        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (ItemTransactionItemCollection)Session["collDestructionOfExpiredItems" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboSRItemType.Enabled = true;
                cboServiceUnitID.Enabled = true;
                cboLocationID.Enabled = true;
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
            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
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
                    bal.Balance > 0,
                    query.Or(query.ItemName.Like(searchTextContain),
                             query.ItemID.Like(searchTextContain)));
            }
            else
                query.Where(query.ItemID == textSearch);

            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);

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
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                var prod = new ItemProductNonMedicQuery("c");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);

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
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
            {
                var prod = new ItemKitchenQuery("c");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);

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
            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                ItemProductMedic medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                ItemProductNonMedic medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Kitchen)
            {
                var medic = new ItemKitchen();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            txtQuantity.Focus();
        }

        #endregion
    }
}