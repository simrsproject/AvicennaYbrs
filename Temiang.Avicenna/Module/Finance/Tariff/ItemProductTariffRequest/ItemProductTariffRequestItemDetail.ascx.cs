using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemProductTariffRequestItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSrItemType
        {
            get
            {
                return ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboSRItemType"));
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            tblSalesDisocunt.Visible = CboSrItemType.SelectedValue != ItemType.Kitchen;

            if (DataItem is GridInsertionObject)
            {
                //Tariff Component
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new ItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                    );
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.ItemID);
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.PriceInPurchaseUnit));
            txtPriceInBaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnit));
            txtPriceInBasedUnitWVat.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.PriceInBaseUnitWVat));
            txtCostPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.CostPrice));
            txtDiscPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemTariffRequestItemMetadata.ColumnNames.DiscPercentage));

            PopulateItem(false, cboItemID.SelectedValue);
            
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemTariffRequestItemCollection)Session["collItemTariffRequestItem" + Request.UserHostName];

                bool isExist = false;
                foreach (ItemTariffRequestItem item in coll)
                {
                    if (item.ItemID.Equals(cboItemID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", cboItemID.SelectedValue);
                    return;
                }
            }

            if (txtPriceInBaseUnit.Value > 0.00 && txtCostPrice.Value == 0.00)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Cost Price must be greater than zero.");
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

        public Decimal PriceInPurchaseUnit
        {
            get { return Convert.ToDecimal(txtPriceInPurchaseUnit.Value); }
        }

        public Decimal PriceInBaseUnit
        {
            get { return Convert.ToDecimal(txtPriceInBaseUnit.Value); }
        }

        public Decimal PriceInBasedUnitWVat
        {
            get { return Convert.ToDecimal(txtPriceInBasedUnitWVat.Value); }
        }

        public Decimal CostPrice
        {
            get { return Convert.ToDecimal(txtCostPrice.Value); }
        }

        public Decimal DiscPercentage
        {
            get { return Convert.ToDecimal(txtDiscPercentage.Value); }
        }
        #endregion

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.SRItemType == ((RadComboBox)Helper.FindControlRecursive(Page, "cboSRItemType")).SelectedValue,
                    query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);

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
            PopulateItem(true, e.Value);
        }

        private void PopulateItem(bool isNew, string itemID)
        {
            if (isNew)
            {
                if (CboSrItemType.SelectedValue == ItemType.Medical)
                {
                    var itemProduct = new ItemProductMedic();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);

                    txtPriceInPurchaseUnit.Value = Convert.ToDouble(itemProduct.PriceInPurchaseUnit);
                    txtPriceInBaseUnit.Value = Convert.ToDouble(itemProduct.PriceInBaseUnit);
                    txtPriceInBasedUnitWVat.Value = txtPriceInBaseUnit.Value + (txtPriceInBaseUnit.Value * AppSession.Parameter.Ppn / 100.00);//Convert.ToDouble(itemProduct.PriceInBasedUnitWVat);
                    txtCostPrice.Value = Convert.ToDouble(itemProduct.CostPrice);
                }
                else if (CboSrItemType.SelectedValue == ItemType.NonMedical)
                {
                    var itemProduct = new ItemProductNonMedic();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);

                    txtPriceInPurchaseUnit.Value = Convert.ToDouble(itemProduct.PriceInPurchaseUnit);
                    txtPriceInBaseUnit.Value = Convert.ToDouble(itemProduct.PriceInBaseUnit);
                    txtPriceInBasedUnitWVat.Value = txtPriceInBaseUnit.Value + (txtPriceInBaseUnit.Value * AppSession.Parameter.Ppn / 100.00);//Convert.ToDouble(itemProduct.PriceInBasedUnitWVat);
                    txtCostPrice.Value = Convert.ToDouble(itemProduct.CostPrice);
                }
                else if (CboSrItemType.SelectedValue == ItemType.Kitchen)
                {
                    var itemProduct = new ItemKitchen();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);

                    txtPriceInPurchaseUnit.Value = Convert.ToDouble(itemProduct.PriceInPurchaseUnit);
                    txtPriceInBaseUnit.Value = Convert.ToDouble(itemProduct.PriceInBaseUnit);
                    txtPriceInBasedUnitWVat.Value = txtPriceInBaseUnit.Value + (txtPriceInBaseUnit.Value * AppSession.Parameter.Ppn / 100.00);//Convert.ToDouble(itemProduct.PriceInBasedUnitWVat);
                    txtCostPrice.Value = Convert.ToDouble(itemProduct.CostPrice);
                }
                txtDiscPercentage.Value = 0;
            }
            else
            {
                if (CboSrItemType.SelectedValue == ItemType.Medical)
                {
                    var itemProduct = new ItemProductMedic();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);
                }
                else if (CboSrItemType.SelectedValue == ItemType.NonMedical)
                {
                    var itemProduct = new ItemProductNonMedic();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);
                }
                else if (CboSrItemType.SelectedValue == ItemType.Kitchen)
                {
                    var itemProduct = new ItemKitchen();
                    itemProduct.LoadByPrimaryKey(itemID);
                    txtSRItemUnit.Text = itemProduct.SRItemUnit;
                    txtSRPurchaseUnit.Text = itemProduct.SRPurchaseUnit;
                    txtConversionFactor.Value = Convert.ToDouble(itemProduct.ConversionFactor);
                }
            }

            // BELUM JADI DIPAKE, masih ragu-ragu
            //txtCostPrice.ReadOnly = (txtCostPrice.Value ?? 0) > 0;
        }

        protected void txtPriceInPurchaseUnit_TextChanged(object sender, EventArgs e)
        {
            PopulatePrice("1");
        }

        protected void txtPriceInBaseUnit_TextChanged(object sender, EventArgs e)
        {
            PopulatePrice("2");
        }

        protected void txtPriceInBasedUnitWVat_TextChanged(object sender, EventArgs e)
        {
            PopulatePrice("3");
        }

        private void PopulatePrice(string a)
        {
            decimal price;
            decimal factor = Convert.ToDecimal(txtConversionFactor.Value);
            double tax = AppSession.Parameter.Ppn/100.00;

            switch (a)
            {
                case "1":
                    price = Convert.ToDecimal(txtPriceInPurchaseUnit.Value);

                    txtPriceInBaseUnit.Value = Convert.ToDouble(price / factor);
                    //txtPriceInBasedUnitWVat.Value = Convert.ToDouble(price / factor) * 1.1;
                    txtPriceInBasedUnitWVat.Value = Convert.ToDouble(price / factor) + (Convert.ToDouble(price / factor) * tax);

                    break;
                case "2":
                    price = Convert.ToDecimal(txtPriceInBaseUnit.Value);

                    txtPriceInPurchaseUnit.Value = Convert.ToDouble(price * factor);
                    //txtPriceInBasedUnitWVat.Value = Convert.ToDouble(price) * 1.1;
                    txtPriceInBasedUnitWVat.Value = Convert.ToDouble(price) + (Convert.ToDouble(price) * tax);

                    break;
                case "3":
                    price = Convert.ToDecimal(txtPriceInBasedUnitWVat.Value);

                    //txtPriceInBaseUnit.Value = Convert.ToDouble(price) / 1.1;
                    //txtPriceInPurchaseUnit.Value = Convert.ToDouble(price * factor) / 1.1;

                    txtPriceInBaseUnit.Value = Convert.ToDouble(price) / (Convert.ToDouble(price) + (Convert.ToDouble(price) * tax));
                    txtPriceInPurchaseUnit.Value = Convert.ToDouble(price * factor) / (Convert.ToDouble(price * factor) + (Convert.ToDouble(price * factor) * tax));

                    break;
            }
        }
    }
}