using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentTransferItemDetail : BaseUserControl
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
        private RadComboBox cboFromLocationID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboFromLocationID");
            }
        }
        protected override void OnDataBinding(EventArgs e)
        {
            cboSRItemType.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collitem = (ItemTransactionItemCollection)Session["ConsignmentTransferItems" + Request.UserHostName];
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
            PopulateCboItemId(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"), false);
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

            var balance = new ItemBalance();
            if (!balance.LoadByPrimaryKey(cboFromLocationID.SelectedValue, cboItemID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item is not have balance quantity";
                return;
            }
            if (balance.Balance <= 0 || balance.Balance < (decimal)txtQuantity.Value)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item is not have sufficient balance quantity";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemTransactionItemCollection)Session["ConsignmentTransferItems" + Request.UserHostName];

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
        #region Method & Event TextChanged


        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var cbo = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            var collitem = (ItemTransactionItemCollection)Session["ConsignmentTransferItems" + Request.UserHostName];
            if (collitem.Count == 0)
                cbo.Enabled = true;
        }

        #endregion

        #region ComboBox ItemID
        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemId((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboItemId(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var cboSRItemType = (RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType");
            var cboFromLocationID = (RadComboBox)Helper.FindControlRecursive(Page, "cboFromLocationID");

            string locationID = cboFromLocationID.SelectedValue;
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            query.LeftJoin(bal).On(query.ItemID == bal.ItemID & bal.LocationID == locationID);

            query.Where(query.SRItemType == cboSRItemType.SelectedItem.Value);
            if (isNew)
            {
                query.Where(query.Or(query.ItemName.Like(searchTextContain), 
                    query.ItemID == textSearch));
            }
            else
                query.Where(query.ItemID == textSearch);

            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");
                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum, std.ItemName.As("Unit"));
                query.Where(prod.IsConsignment == true);
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                var prod = new ItemProductNonMedicQuery("c");
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");
                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum, std.ItemName.As("Unit"));
                query.Where(prod.IsConsignment == true);
            }
            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtQuantity.MaxLength = 10;
            txtQuantity.MinValue = 0;
            txtQuantity.MaxValue = 99999999.99;

            if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (cboSRItemType.SelectedItem.Value == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(e.Value);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
        }
        #endregion
    }
}