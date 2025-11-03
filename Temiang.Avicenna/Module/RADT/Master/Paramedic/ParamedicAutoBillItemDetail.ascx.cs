using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicAutoBillItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSRRegistrationType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

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
                cboSRRegistrationType.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
            }

            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ServiceUnitAutoBillItem, txtItemID);

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

            cboSRRegistrationType.SelectedValue = (string)DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.ServiceUnitID);

            txtItemID.Text = (String)DataBinder.Eval(DataItem, ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID);
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = i.ItemName;

            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.Quantity));
            cboSRItemUnit.SelectedValue = (string)DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.SRItemUnit);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.IsActive);
            chkIsGenerateOnRegistration.Checked = (bool)DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
            chkIsGenerateOnReferral.Checked = (bool)DataBinder.Eval(DataItem, ParamedicAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicAutoBillItemCollection)Session["collParamedicAutoBillItem"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ServiceUnitID.Equals(cboSRRegistrationType.SelectedValue) && item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} already exist", itemID);
                }
            }
        }

        #region Properties for return entry value

        public String ServiceUnitID
        {
            get { return cboSRRegistrationType.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboSRRegistrationType.Text; }
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

        public string SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
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

        public Boolean IsGenerateOnReferral
        {
            get { return chkIsGenerateOnReferral.Checked; }
        }

        #endregion

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            Item i = new Item();
            if (i.LoadByPrimaryKey(txtItemID.Text))
            {
                lblItemName.Text = i.ItemName;
                if (i.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    ItemProductMedic ipmd = new ItemProductMedic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    ItemProductNonMedic ipmd = new ItemProductNonMedic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.Service)
                {
                    ItemService ipmd = new ItemService();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = ipmd.SRItemUnit;
                }
                else if (i.SRItemType == BusinessObject.Reference.ItemType.Diagnostic)
                {
                    ItemDiagnostic ipmd = new ItemDiagnostic();
                    if (ipmd.LoadByPrimaryKey(txtItemID.Text))
                        cboSRItemUnit.SelectedValue = "";
                }
            }
            else
            {
                txtItemID.Text = string.Empty;
                lblItemName.Text = string.Empty;
            }
        }
    }
}
