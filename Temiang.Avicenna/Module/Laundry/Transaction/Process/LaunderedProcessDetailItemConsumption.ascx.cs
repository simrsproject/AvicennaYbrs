using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessDetailItemConsumption : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaunderedProcessItemConsumptionMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, LaunderedProcessItemConsumptionMetadata.ColumnNames.SRItemUnit);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (LaunderedProcessItemConsumptionCollection)Session["collLaunderedProcessItemConsumption" + Request.UserHostName];

                bool isExist = coll.Any(cons => (cons.ItemID.Equals(cboItemID.SelectedValue)));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
                    return;
                }
            }

            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            //Check Entry ItemID
            var qrItem = new ItemQuery("a");
            var qrProduct = new ItemProductNonMedicQuery("b");
            qrItem.InnerJoin(qrProduct).On(qrItem.ItemID == qrProduct.ItemID);
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemID == cboItemID.SelectedValue);
            var item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
                return;
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
        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }
        public String SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public String ItemUnitName
        {
            get { return cboSRItemUnit.Text; }
        }
        public Decimal CostPrice
        {
            get { return Convert.ToDecimal(txtCostPrice.Value); }
        }
        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }
        public Boolean IsInventoryItem
        {
            get { return chkIsInventoryItem.Checked; }
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var nmedic = new ItemProductNonMedicQuery("b");
            query.InnerJoin(nmedic).On(query.ItemID == nmedic.ItemID);
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.ItemName.Like(searchTextContain)
                );
            if (isNew)
                query.Where(query.IsActive == true);

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
            var i = new Item();
            if (i.LoadByPrimaryKey(e.Value))
            {
                ComboBox.PopulateWithItemBaseUnit(cboSRItemUnit, e.Value, i.SRItemType);
                var nm = new ItemProductNonMedic();
                if (nm.LoadByPrimaryKey(e.Value))
                {
                    cboSRItemUnit.SelectedValue = nm.SRItemUnit;
                    txtPrice.Value = Convert.ToDouble(nm.PriceInBaseUnit);
                    txtCostPrice.Value = Convert.ToDouble(nm.CostPrice);
                    chkIsInventoryItem.Checked = nm.IsInventoryItem ?? false;
                }
                else
                {
                    txtPrice.Value = 0;
                    txtCostPrice.Value = 0;
                    chkIsInventoryItem.Checked = false;
                }
            }
            else
            {
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
                txtPrice.Value = 0;
                txtCostPrice.Value = 0;
                chkIsInventoryItem.Checked = false;
            }
        }
        #endregion
    }
}