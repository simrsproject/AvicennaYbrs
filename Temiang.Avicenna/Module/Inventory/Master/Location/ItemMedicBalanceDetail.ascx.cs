using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemMedicBalanceDetail : BaseUserControl
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

        protected override void OnDataBinding(EventArgs e)
        {
            trItemSubBin.Visible = AppSession.Parameter.IsUsingItemSubBin;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            PopulateCboSRItemBin(cboSRItemBin, (String)DataBinder.Eval(DataItem, "SRItemBin"));

            ItemProductMedic medic = new ItemProductMedic();
            medic.LoadByPrimaryKey(cboItemID.SelectedValue);
            txtSRItemUnitName.Text = medic.SRItemUnit;

            txtMinimum.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Minimum));
            txtMaximum.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Maximum));
            txtBalance.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Balance));
            txtItemSubBin.Text = (String)DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.ItemSubBin);
        }
        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text);
        }

        protected void cboSRItemBin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRItemBin((RadComboBox)sender, e.Text);
        }

        private void PopulateCboSRItemBin(RadComboBox comboBox, string textSearch)
        {
            StandardReference.PopulateCboSRItemBin(comboBox, textSearch, 
                ((RadTextBox)Helper.FindControlRecursive(Page, "txtLocationID")).Text);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch)
        {
            string locationID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtLocationID")).Text;
            string searchTextContain = string.Format("%{0}%", textSearch);
            ItemQuery query = new ItemQuery("a");
            ItemBalanceQuery bal = new ItemBalanceQuery("b");
            query.LeftJoin(bal).On(query.ItemID == bal.ItemID & bal.LocationID ==  locationID);

            query.Where(
                query.SRItemType == BusinessObject.Reference.ItemType.Medical,
                query.Or(query.ItemName.Like(searchTextContain), query.ItemID.Like(searchTextContain)));
            ItemProductMedicQuery prod = new ItemProductMedicQuery("c");
            query.LeftJoin(prod).On(query.ItemID == prod.ItemID);
            AppStandardReferenceItemQuery std = new AppStandardReferenceItemQuery("d");
            query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");

            query.Select(query.ItemID, query.ItemName, bal.Balance, std.ItemName.As("Unit"));

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRItemBin_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ItemProductMedic medic = new ItemProductMedic();
            medic.LoadByPrimaryKey(e.Value);
            txtSRItemUnitName.Text = medic.SRItemUnit;
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check Entry ItemID
            ItemQuery qrItem = new ItemQuery();
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemName == cboItemID.Text);
            Item item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
                return;
            }

            if ((txtMinimum.Value ?? 0) < 0) {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid qty minimum";
                return;
            }
            if ((txtMaximum.Value ?? 0) < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid qty maximum";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemBalanceCollection coll =
                    (ItemBalanceCollection)Session["collItemMedic" + PageId];

                string id = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ItemBalance row in coll)
                {
                    if (row.ItemID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }
        public String ItemName
        {
            get { return cboItemID.Text; }
        }
        public String SRItemBin
        {
            get { return cboSRItemBin.SelectedValue; }
        }
        public String SRItemBinName
        {
            get { return cboSRItemBin.Text; }
        }

        public String SRItemUnitName
        {
            get { return txtSRItemUnitName.Text; }
        }
        public Decimal Minimum
        {
            get { return Convert.ToDecimal(txtMinimum.Value); }
        }
        public Decimal Maximum
        {
            get { return Convert.ToDecimal(txtMaximum.Value); }
        }
        public String ItemSubBin
        {
            get { return txtItemSubBin.Text; }
        }
        #endregion
    }
}
