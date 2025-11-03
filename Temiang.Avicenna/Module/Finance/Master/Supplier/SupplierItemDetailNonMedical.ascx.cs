using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierItemDetailNonMedical : BaseUserControl
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
                txtConversionFactor.Value = 1;
                chkIsActive.Checked = true;

                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new ItemQuery("a");
            var prodmedQ = new ItemProductNonMedicQuery("b");

            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
            query.Where(query.SRItemType == ItemType.NonMedical, 
                query.ItemID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));

            DataTable tbl = query.LoadDataTable();
            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
            ComboBox.SelectedValue(cboItemID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));
            txtSRPurchaseUnit.Text = (String)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.SRPurchaseUnit);
            txtPurchaseDiscount1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount1));
            txtPurchaseDiscount2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount2));
            txtConversionFactor.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.ConversionFactor));
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.IsActive);
        }

        #region cboItemID

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var prodmedQ = new ItemProductNonMedicQuery("b");

            query.es.Top = 20;
            query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
            query.Where(query.SRItemType == ItemType.NonMedical, 
                query.Or(query.ItemID.Like(searchTextContain),
                                 query.ItemName.Like(searchTextContain)));
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

            switch (item.SRItemType)
            {
                case ItemType.Medical:
                    var med = new ItemProductMedic();
                    if (med.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        txtSRPurchaseUnit.Text = med.SRPurchaseUnit;
                        txtPriceInPurchaseUnit.Value = Convert.ToDouble(med.PriceInPurchaseUnit);
                        txtConversionFactor.Value = Convert.ToDouble(med.ConversionFactor);
                        txtPurchaseDiscount1.Value = Convert.ToDouble(med.PurchaseDiscount1);
                        txtPurchaseDiscount2.Value = Convert.ToDouble(med.PurchaseDiscount2);
                    }
                    break;
                case ItemType.NonMedical:
                    var nmed = new ItemProductNonMedic();
                    if (nmed.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        txtSRPurchaseUnit.Text = nmed.SRPurchaseUnit;
                        txtPriceInPurchaseUnit.Value = Convert.ToDouble(nmed.PriceInPurchaseUnit);
                        txtConversionFactor.Value = Convert.ToDouble(nmed.ConversionFactor);
                        txtPurchaseDiscount1.Value = Convert.ToDouble(nmed.PurchaseDiscount1);
                        txtPurchaseDiscount2.Value = Convert.ToDouble(nmed.PurchaseDiscount2);
                    }
                    break;
                case ItemType.Kitchen:
                    var k = new ItemKitchen();
                    if (k.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        txtSRPurchaseUnit.Text = k.SRPurchaseUnit;
                        txtPriceInPurchaseUnit.Value = Convert.ToDouble(k.PriceInPurchaseUnit);
                        txtConversionFactor.Value = Convert.ToDouble(k.ConversionFactor);
                        txtPurchaseDiscount1.Value = Convert.ToDouble(k.PurchaseDiscount1);
                        txtPurchaseDiscount2.Value = Convert.ToDouble(k.PurchaseDiscount2);
                    }
                    break;
            }
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

        public Decimal ConversionFactor
        {
            get { return Convert.ToDecimal(txtConversionFactor.Value); }
        }

        public Decimal PriceInPurchaseUnit
        {
            get { return Convert.ToDecimal(txtPriceInPurchaseUnit.Value); }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public String DrugDistributionLicenseNo
        {
            get { return string.Empty; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (SupplierItemCollection)Session["collSupplierItemNonMedical"];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (SupplierItem item in coll)
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