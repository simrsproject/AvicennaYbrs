using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalSupplierDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        //protected override void OnDataBinding(EventArgs e)
        //{
        //    if (DataItem is GridInsertionObject)
        //    {
        //        txtPurchaseDiscount1.Value = 0;
        //        txtPurchaseDiscount2.Value = 0;
        //        txtPriceInPurchaseUnit.Value = 0;
        //        chkIsActive.Checked = true;

        //        ViewState["IsNewRecord"] = true;
        //        return;
        //    }
        //    ViewState["IsNewRecord"] = false;

        //    ItemQuery query = new ItemQuery("a");
        //    ItemProductMedicQuery prodmedQ = new ItemProductMedicQuery("b");

        //    query.Select
        //        (
        //            query.ItemID,
        //            query.ItemName
        //        );
        //    query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
        //    query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));

        //    DataTable tbl = query.LoadDataTable();
        //    cboItemID.DataSource = tbl;
        //    cboItemID.DataBind();
        //    ComboBox.SelectedValue(cboItemID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));
        //    txtSRPurchaseUnit.Text = (String) DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.SRPurchaseUnit);
        //    txtPurchaseDiscount1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount1));
        //    txtPurchaseDiscount2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount2));
        //    txtPriceInPurchaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit));
        //    chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.IsActive);
        //}
        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                txtPurchaseDiscount1.Value = 0;
                txtPurchaseDiscount2.Value = 0;
                txtConversionFactor.Value = 1;
                txtPriceInPurchaseUnit.Value = 0;
                chkIsActive.Checked = true;

                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            SupplierQuery query = new SupplierQuery("a");

            query.Select
                (
                    query.SupplierID,
                    query.SupplierName
                );
            query.Where(query.SupplierID == (String)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.SupplierID));

            DataTable tbl = query.LoadDataTable();
            cboSupplierID.DataSource = tbl;
            cboSupplierID.DataBind();
            ComboBox.SelectedValue(cboSupplierID, (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID));
            txtSRPurchaseUnit.Text = (String)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.SRPurchaseUnit);
            txtPurchaseDiscount1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount1));
            txtPurchaseDiscount2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PurchaseDiscount2));

            var conversionFactor = DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.ConversionFactor).ToDouble();
            txtConversionFactor.Value = conversionFactor == 0 ? 1 : conversionFactor;
            txtPriceInPurchaseUnit.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.IsActive);
            txtDrugDistributionLicenseNo.Text = (String)DataBinder.Eval(DataItem, SupplierItemMetadata.ColumnNames.DrugDistributionLicenseNo);
        }

        #region cboSupplierID

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            SupplierQuery query = new SupplierQuery("a");

            query.es.Top = 20;
            query.Where(query.SupplierName.Like(searchTextContain));
            query.Select
                (
                    query.SupplierID,
                    query.SupplierName
                );
            cboSupplierID.DataSource = query.LoadDataTable();
            cboSupplierID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Supplier sup = new Supplier();
            if (!sup.LoadByPrimaryKey(e.Value))
            {
                cboSupplierID.Text = string.Empty;
                return;
            }

            var SrPUnit = ((RadComboBox)Common.Helper.FindControlRecursive(Page, "cboSRPurchaseUnit")).SelectedValue;
            txtSRPurchaseUnit.Text = SrPUnit;

            // cf
            var cf = ((RadNumericTextBox)Common.Helper.FindControlRecursive(Page, "txtConversionFactor")).Value;
            txtConversionFactor.Value = cf;
        }

        #endregion

        #region Properties for return entry value

        public String SupplierID
        {
            get { return cboSupplierID.SelectedValue; }
        }

        public String SupplierName
        {
            get { return cboSupplierID.Text; }
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
            get { return txtDrugDistributionLicenseNo.Text; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                SupplierItemCollection coll = (SupplierItemCollection)Session["collSupplierItem"];

                string supplierID = cboSupplierID.SelectedValue;
                bool isExist = false;
                foreach (SupplierItem sup in coll)
                {
                    if (sup.SupplierID.Equals(supplierID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Supplier ID: {0} has exist", supplierID);
                }
            }
        }
    }
}