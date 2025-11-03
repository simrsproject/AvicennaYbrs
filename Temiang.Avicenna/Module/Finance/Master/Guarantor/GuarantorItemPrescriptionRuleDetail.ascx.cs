using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemPrescriptionRuleDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemProductMedical, txtItemID);
            StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtAmountValue.Value = 0D;
                txtAmountOPR.Value = 0D;
                txtAmountEMR.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID);
            PopulateItemName(false);

            rblInclude.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsInclude) ? 0 : 1;
            cboSRGuarantorRuleType.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.SRGuarantorRuleType);
            txtAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.AmountValue));
            chkIsValueInPercent.Checked = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsValueInPercent);
            rblToGuarantor.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsToGuarantor) ? 0 : 1;

            txtAmountOPR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.OutpatientAmountValue) ?? 0);
            txtAmountEMR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionRuleMetadata.ColumnNames.EmergencyAmountValue) ?? 0);

            tblRuleType.Visible = rblInclude.SelectedIndex != 1;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                GuarantorItemPrescriptionRuleCollection coll = (GuarantorItemPrescriptionRuleCollection)Session["collGuarantorItemPrescriptionRule"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (GuarantorItemPrescriptionRule item in coll)
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

            if (rblInclude.SelectedIndex == 0)
            {
                if (cboSRGuarantorRuleType.SelectedIndex == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Rule Type Name is required";
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return lblItemName.Text; }
        }

        public String SRGuarantorRuleType
        {
            get { return cboSRGuarantorRuleType.SelectedValue; }
        }

        public String GuarantorRuleTypeName
        {
            get { return cboSRGuarantorRuleType.Text; }
        }

        public Decimal AmountValue
        {
            get { return Convert.ToDecimal(txtAmountValue.Value); }
        }

        public Boolean IsValueInPercent
        {
            get { return chkIsValueInPercent.Checked; }
        }

        public Boolean IsInclude
        {
            get { return rblInclude.SelectedIndex == 0 ? true : false; }
        }

        public Boolean IsToGuarantor
        {
            get
            {
                return rblToGuarantor.SelectedIndex == 0 ? true : false;
            }
        }

        public Decimal OPRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountOPR.Value); }
        }

        public Decimal EMRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountEMR.Value); }
        }

        #endregion

        #region Method & Event TextChanged

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemName(true);
        }

        private void PopulateItemName(bool isResetIdIfNotExist)
        {
            if (txtItemID.Text == string.Empty)
            {
                lblItemName.Text = string.Empty;
                return;
            }
            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = entity.ItemName;
            else
            {
                lblItemName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtItemID.Text = string.Empty;
            }
        }

        #endregion

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblRuleType.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }
    }
}