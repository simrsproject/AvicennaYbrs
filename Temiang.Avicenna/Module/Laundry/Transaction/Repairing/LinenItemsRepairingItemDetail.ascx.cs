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
    public partial class LinenItemsRepairingItemDetail : BaseUserControl
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
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryRepairingProcessItemMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaundryRepairingProcessItemMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryRepairingProcessItemMetadata.ColumnNames.SRItemUnit);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value <= 0)
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
                var coll = (LaundryRepairingProcessItemCollection)Session["collLaundryRepairingProcessItem" + Request.UserHostName];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (LaundryRepairingProcessItem row in coll)
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
        public String ItemUnit
        {
            get { return cboSRItemUnit.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboItemID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var nmedic = new ItemProductNonMedicQuery("b");
            query.InnerJoin(nmedic).On(query.ItemID == nmedic.ItemID);
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.ItemName.Like(searchTextContain),
                query.IsActive == true,
                nmedic.IsNeedToBeLaundered == true);

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
                    cboSRItemUnit.SelectedValue = nm.SRItemUnit;
            }

            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
        }
        #endregion
    }
}