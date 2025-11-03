using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class ItemLinenItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, ItemLinenItemMetadata.ColumnNames.ItemDetailID), false);
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemLinenItemMetadata.ColumnNames.ItemDetailID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemLinenItemMetadata.ColumnNames.Qty));
            txtQtyDetail.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemLinenItemMetadata.ColumnNames.QtyDetail));
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemLinenItemMetadata.ColumnNames.SRItemUnit);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemLinenItemCollection coll =
                    (ItemLinenItemCollection)Session["collItemLinenItem"];

                bool isExist = false;
                foreach (ItemLinenItem item in coll)
                {
                    if (item.ItemDetailID.Equals(cboItemID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item Detail: {0} has exist", cboItemID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemDetailID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemDetailName
        {
            get { return cboItemID.Text; }
        }

        public Decimal? Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }

        public Decimal? QtyDetail
        {
            get { return Convert.ToDecimal(txtQtyDetail.Value); }
        }

        public String SRItemUnit
        {
            get { return txtSRItemUnit.Text; }
        }
        #endregion

        #region Combobox
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, true);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateItemUnit(e.Value);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var inm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(inm).On(inm.ItemID == query.ItemID);
            query.Where(query.Or(
                query.ItemID == textSearch, 
                query.ItemName.Like(searchTextContain))
                );
            if (isNew)
                query.Where(query.IsActive == true, inm.IsNeedToBeLaundered == true);

            query.Select(query.ItemID, query.ItemName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        private void PopulateItemUnit(string itemId)
        {
            var item = new ItemProductNonMedic();
            if (item.LoadByPrimaryKey(itemId))
                txtSRItemUnit.Text = item.SRItemUnit;
            else txtSRItemUnit.Text = string.Empty;
        }
        #endregion
    }
}