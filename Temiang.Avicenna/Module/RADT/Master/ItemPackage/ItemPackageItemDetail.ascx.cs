using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemPackageItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Item, txtDetailItemID);
            //PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemServices, txtDetailItemID);
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ServiceUnit, txtServiceUnitID);

            trDiscount.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH";
            trChkAutoApprove.Visible = AppSession.Parameter.IsAutoApprovePackage;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtQuantity.Value = 1;
                chkIsActive.Checked = true;
                txtDiscountValue.Value = 0;
                chkIsDiscountInPercent.Checked = true;
                txtDiscountValue.Enabled = true;
                SetEnableChkAutoApprove();
                return;
            }

            ViewState["IsNewRecord"] = false;
            txtDetailItemID.ReadOnly = true;
            txtDetailItemID.ShowButton = false;
            txtServiceUnitID.ReadOnly = true;
            txtServiceUnitID.ShowButton = false;
            txtDetailItemID.Text = (String)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.DetailItemID);
            txtServiceUnitID.Text = (String)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.ServiceUnitID);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.Quantity));
            txtSRItemUnit.Text = (String)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.SRItemUnit);
            chkIsStockControl.Checked = (bool)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsStockControl);
            chkExtraItem.Checked = (bool)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsExtraItem);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsActive);
            chkAutoApprove.Checked = (bool)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsAutoApprove);

            txtDiscountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.DiscountValue)); //DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.DiscountValue) as double? ?? 0;
            chkIsDiscountInPercent.Checked = (bool)DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsDiscountInPercent); //DataBinder.Eval(DataItem, ItemPackageMetadata.ColumnNames.IsDiscountInPercent) as bool? ?? true;

            PopulateDetailItemName(false);
            PopulateServiceUnitName(false);
            SetEnabledDiscount();
            SetEnableChkAutoApprove();
        }

        private void SetEnableChkAutoApprove() {
            
            chkAutoApprove.Enabled = AppSession.Parameter.IsAutoApprovePackage && 
                !string.IsNullOrEmpty(txtDetailItemID.Text) && 
                !string.IsNullOrEmpty(txtServiceUnitID.Text);
            if(chkAutoApprove.Enabled){
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(txtServiceUnitID.Text))
                {
                    chkAutoApprove.Enabled = !(su.IsUsingJobOrder ?? false);
                }
                else {
                    chkAutoApprove.Enabled = false;
                }
            }

            if (!chkAutoApprove.Enabled) chkAutoApprove.Checked = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemPackageCollection)Session["collItemPackage"];

                string detailItemID = txtDetailItemID.Text;
                string unitId = txtServiceUnitID.Text;
                bool isExist = false;
                bool isExistExtra = false;
                
                foreach (ItemPackage item in coll)
                {
                    if (item.DetailItemID.Equals(detailItemID) && item.ServiceUnitID.Equals(unitId))
                    {
                        isExist = true;
                        break;
                    }

                    if (item.DetailItemID.Equals(detailItemID) && item.IsExtraItem == true)
                    {
                        isExistExtra = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} on Service Unit: {1} cannot be double.", detailItemID, lblServiceUnitName.Text);
                    return;
                }
                
                if (isExistExtra)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Extra Item ID: {0} cannot be double.", detailItemID); // item extra tidak boleh double
                }
            }
        }

        private RadTextBox txtItemID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemID"); }
        }

        public String DetailItemID
        {
            get { return txtDetailItemID.Text; }
        }

        public String DetailItemName
        {
            get { return lblDetailItemName.Text; }
        }

        public String ServiceUnitID
        {
            get { return txtServiceUnitID.Text; }
        }

        public String ServiceUnitName
        {
            get { return lblServiceUnitName.Text; }
        }

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public String SRItemUnit
        {
            get { return txtSRItemUnit.Text; }
        }

        public Boolean IsStockControl
        {
            get { return chkIsStockControl.Checked; }
        }

        public Boolean IsExtraItem
        {
            get { return chkExtraItem.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public Boolean IsAutoApprove
        {
            get { return chkAutoApprove.Checked; }
        }

        public Decimal DiscountValue
        {
            get { return Convert.ToDecimal(txtDiscountValue.Value); }
        }

        public Boolean IsDiscountInPercent
        {
            get { return chkIsDiscountInPercent.Checked; }
        }

        protected void txtDetailItemID_TextChanged(object sender, EventArgs e)
        {
            PopulateDetailItemName(true);
        }

        private void PopulateDetailItemName(bool isResetIdIfNotExist)
        {
            if (txtDetailItemID.Text == string.Empty)
            {
                lblDetailItemName.Text = string.Empty;
                txtSRItemUnit.Text = string.Empty;
                return;
            }

            var entity = new Item();
            if (entity.LoadByPrimaryKey(txtDetailItemID.Text))
            {
                lblDetailItemName.Text = entity.ItemName;
                switch (entity.SRItemType)
                {
                    case BusinessObject.Reference.ItemType.Service:
                        var det1 = new ItemService();
                        det1.LoadByPrimaryKey(txtDetailItemID.Text);
                        txtSRItemUnit.Text = det1.SRItemUnit;
                        break;
                    case BusinessObject.Reference.ItemType.Medical:
                        var det2 = new ItemProductMedic();
                        det2.LoadByPrimaryKey(txtDetailItemID.Text);
                        txtSRItemUnit.Text = det2.SRItemUnit;
                        chkIsStockControl.Enabled = true;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        var det3 = new ItemProductNonMedic();
                        det3.LoadByPrimaryKey(txtDetailItemID.Text);
                        txtSRItemUnit.Text = det3.SRItemUnit;
                        chkIsStockControl.Enabled = true;
                        break;
                    case BusinessObject.Reference.ItemType.Diagnostic:
                    case BusinessObject.Reference.ItemType.Laboratory:
                    case BusinessObject.Reference.ItemType.Radiology:
                        txtSRItemUnit.Text = "X";
                        break;
                }
            }
            else
            {
                lblDetailItemName.Text = string.Empty;
                txtSRItemUnit.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtDetailItemID.Text = string.Empty;
            }
        }

        protected void txtServiceUnitID_TextChanged(object sender, EventArgs e)
        {
            PopulateServiceUnitName(true);
            SetEnableChkAutoApprove();
        }

        private void PopulateServiceUnitName(bool isResetIdIfNotExist)
        {
            if (txtServiceUnitID.Text == string.Empty)
            {
                lblServiceUnitName.Text = string.Empty;
                return;
            }
            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
                lblServiceUnitName.Text = entity.ServiceUnitName;
            else
            {
                lblServiceUnitName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtServiceUnitID.Text = string.Empty;
            }
        }

        protected void chkIsDiscountInPercent_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledDiscount();
            txtDiscountValue.Value = 0.00;
        }

        private void SetEnabledDiscount()
        {
            txtDiscountValue.Enabled = chkIsDiscountInPercent.Checked;
        }
    }
}
