using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorAutoBillItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var units = new ServiceUnitCollection();
            units.Query.Where(
                units.Query.SRRegistrationType.In(
                    AppConstant.RegistrationType.InPatient,
                    AppConstant.RegistrationType.OutPatient,
                    AppConstant.RegistrationType.EmergencyPatient,
                    AppConstant.RegistrationType.MedicalCheckUp
                    ),
                units.Query.IsActive == true
                );
            units.Query.Load();

            foreach (var unit in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
            }

            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemService, txtItemID);
            
            if (cboSRItemUnit.Items.Count == 0)
                StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtQuantity.Value = 1.00;
                chkIsActive.Checked = true;
                
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboServiceUnitID.Enabled = false;
            txtItemID.Enabled = false;

            cboServiceUnitID.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID);
            txtItemID.Text = (String)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.ItemID);
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = i.ItemName;

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.Quantity));
            var itmService = new ItemService();
            if (itmService.LoadByPrimaryKey(txtItemID.Text))
                cboSRItemUnit.SelectedValue = itmService.SRItemUnit;
            else
            {
                cboSRItemUnit.SelectedValue = string.Empty;
                cboSRItemUnit.Text = string.Empty;
            }

            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.IsActive);
            chkIsGenerateOnRegistration.Checked = (bool)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
            chkIsGenerateOnNewRegistration.Checked = (bool)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration);
            chkIsGenerateOnReferral.Checked = (bool)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
            chkIsGenerateOnFirstRegistration.Checked = (bool)DataBinder.Eval(DataItem, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                GuarantorAutoBillItemCollection coll = (GuarantorAutoBillItemCollection)Session["collGuarantorAutoBillItem"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (GuarantorAutoBillItem item in coll)
                {
                    if (item.ServiceUnitID.Equals(cboServiceUnitID.SelectedValue) && item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Service Unit : {1} with Item ID : {0} already exist", itemID, cboServiceUnitID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return lblItemName.Text; }
        }

        public Decimal Quantity
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public string ItemUnit
        {
            get { return cboSRItemUnit.SelectedItem.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public Boolean IsGenerateOnRegistration
        {
            get { return chkIsGenerateOnRegistration.Checked; }
        }

        public Boolean IsGenerateOnNewRegistration
        {
            get { return chkIsGenerateOnNewRegistration.Checked; }
        }

        public Boolean IsGenerateOnReferral
        {
            get { return chkIsGenerateOnReferral.Checked; }
        }

        public Boolean IsGenerateOnFirstRegistration
        {
            get { return chkIsGenerateOnFirstRegistration.Checked; }
        }

        #endregion

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
            {
                lblItemName.Text = i.ItemName;
                ItemService ipmd = new ItemService();
                if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                    cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                else
                {
                    cboSRItemUnit.SelectedValue = string.Empty;
                    cboSRItemUnit.Text = string.Empty;
                }
            }
            else
            {
                txtItemID.Text = string.Empty;
                lblItemName.Text = string.Empty;
                cboSRItemUnit.SelectedValue = string.Empty;
                cboSRItemUnit.Text = string.Empty;
            }
        }
    }
}