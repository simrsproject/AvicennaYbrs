using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierContractItemKitchenDetail : BaseUserControl
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
                txtPurchaseDiscount1.Value = 0;
                txtPurchaseDiscount2.Value = 0;
                txtPriceInPurchaseUnit.Value = 0;
                chkIsActive.Checked = true;

                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new ItemQuery("a");
            var prodmedQ = new ItemKitchenQuery("b");

            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.ItemID));

            DataTable tbl = query.LoadDataTable();
            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
            ComboBox.SelectedValue(cboItemID, (String)DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.ItemID));
            txtSRPurchaseUnit.Text = (String)DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.SRPurchaseUnit);
            txtPurchaseDiscount1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.PurchaseDiscount1));
            txtPurchaseDiscount2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.PurchaseDiscount2));
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.PriceInPurchaseUnit));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierContractItemMetadata.ColumnNames.IsActive);
        }

        #region cboItemID

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var prodmedQ = new ItemKitchenQuery("b");

            query.es.Top = 20;
            query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
            query.Where(query.ItemName.Like(searchTextContain));
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(e.Value))
            {
                cboItemID.Text = string.Empty;
                return;
            }

            var med = new ItemKitchen();
            if (med.LoadByPrimaryKey(cboItemID.SelectedValue))
                txtSRPurchaseUnit.Text = med.SRPurchaseUnit;

        }

        #endregion

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public Decimal PurchaseDiscount1
        {
            get { return Convert.ToDecimal(txtPurchaseDiscount1.Value); }
        }

        public Decimal PurchaseDiscount2
        {
            get { return Convert.ToDecimal(txtPurchaseDiscount2.Value); }
        }

        public String SRPurchaseUnit
        {
            get { return txtSRPurchaseUnit.Text; }
        }

        public Decimal PriceInPurchaseUnit
        {
            get { return Convert.ToDecimal(txtPriceInPurchaseUnit.Value); }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (SupplierContractItemCollection)Session["collSupplierContractItemKitchen"];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (SupplierContractItem item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
                }
            }
        }
    }
}