using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceRoomAutoBillItemDetail : BaseUserControl
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
                txtQuantity.Value = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;
            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, ServiceRoomAutoBillItemMetadata.ColumnNames.ItemID));
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ServiceRoomAutoBillItemMetadata.ColumnNames.Quantity));
            txtSRItemUnit.Text = LoadItemUnitId(cboItemID.SelectedValue);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ItemQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(
                query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen),
                query.IsActive == true,
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    query.ItemID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ItemName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtSRItemUnit.Text = LoadItemUnitId(cboItemID.SelectedValue);
        }

        private string LoadItemUnitId(string itemId)
        {
            string itemUnit = string.Empty;
            var i = new Item();
            if (i.LoadByPrimaryKey(itemId))
            {
                switch (i.SRItemType)
                {
                    case ItemType.Service:
                        var iservice = new ItemService();
                        if (iservice.LoadByPrimaryKey(i.ItemID))
                            itemUnit = iservice.SRItemUnit;

                        break;
                    case ItemType.Laboratory:
                    case ItemType.Radiology:
                        itemUnit = "X";
                        break;
                    case ItemType.Medical:
                        var imedic = new ItemProductMedic();
                        if (imedic.LoadByPrimaryKey(i.ItemID))
                            itemUnit = imedic.SRItemUnit;
                        break;
                    case ItemType.NonMedical:
                        var inonmedic = new ItemProductNonMedic();
                        if (inonmedic.LoadByPrimaryKey(i.ItemID))
                            itemUnit = inonmedic.SRItemUnit;
                        break;
                    case ItemType.Kitchen:
                        var ikitchen = new ItemKitchen();
                        if (ikitchen.LoadByPrimaryKey(i.ItemID))
                            itemUnit = ikitchen.SRItemUnit;
                        break;
                }
            }
            return itemUnit;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ServiceRoomAutoBillItemCollection coll;

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                coll = (ServiceRoomAutoBillItemCollection)Session["collServiceRoomAutoBillItem"];

                string id = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ServiceRoomAutoBillItem item in coll)
                {
                    if (item.ItemID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", id);
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

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public String ItemUnit
        {
            get
            {
                return LoadItemUnitId(ItemID);
            }
        }

        #endregion
    }
}