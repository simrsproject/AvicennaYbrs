using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationItemRuleDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

            rblInclude.Items[1].Enabled = (((RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID")).SelectedValue != 
                AppSession.Parameter.SelfGuarantor);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtAmountValue.Value = 0D;
                txtOutpatientAmountValue.Value = 0D;
                txtEmergencyAmountValue.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;

            ItemQuery item = new ItemQuery();
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where(item.ItemID == (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.ItemID));

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.ItemID);

            cboSRGuarantorRuleType.SelectedValue = (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType);
            txtAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.AmountValue));
            txtOutpatientAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue));
            txtEmergencyAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue));
            chkIsValueInPercent.Checked = (bool)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent);
            rblInclude.SelectedIndex = (bool)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.IsInclude) ? 0 : 1;
            rblToGuarantor.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor) ? 0 : 1;

            tblInclude.Visible = rblInclude.SelectedIndex != 1;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                RegistrationItemRuleCollection coll = (RegistrationItemRuleCollection)Session["collRegistrationItemRule" + Request.UserHostName];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (BusinessObject.RegistrationItemRule item in coll)
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
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
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

        public Decimal OutpatientAmountValue
        {
            get { return Convert.ToDecimal(txtOutpatientAmountValue.Value); }
        }

        public Decimal EmergencyAmountValue
        {
            get { return Convert.ToDecimal(txtEmergencyAmountValue.Value); }
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

        #endregion

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemQuery item = new ItemQuery();
            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        ),
                    item.IsActive == true
                );
            item.OrderBy(item.ItemID.Ascending);

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblInclude.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0D;
            txtOutpatientAmountValue.Value = 0D;
            txtEmergencyAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }
    }
}