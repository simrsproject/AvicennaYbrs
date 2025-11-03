using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorAliasDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboBridgingType, AppEnum.StandardReference.BridgingType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtCoverageValue.Value = 0;
                txtMarginValue.Value = 0;
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBridgingType.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.SRBridgingType);
            cboBridgingType_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, (String)DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.SRBridgingType), string.Empty));
            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString()) cboServiceUnitAliasID.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.BridgingID);
            else
            {
                txtBridgingCode.Text = (String)DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.BridgingID);
                txtBridgingInitial.Text = (String)DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.BridgingCode);
            }
            txtCoverageValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.CoverageValue));
            txtMarginValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.MarginValue));
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, GuarantorBridgingMetadata.ColumnNames.IsActive));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (GuarantorBridgingCollection)Session["collGuarantorBridging"];

                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.SRBridgingType.Equals(cboBridgingType.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Bridging ID : {0} already exist", cboBridgingType.Text);
                }
            }
        }

        public String BridgingType
        {
            get { return cboBridgingType.SelectedValue; }
        }

        public String BridgingTypeName
        {
            get { return cboBridgingType.Text; }
        }

        public String BridgingID
        {
            get { return cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.SelectedValue : txtBridgingCode.Text; }
        }

        public String BridgingCode
        {
            get { return cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.Text : txtBridgingInitial.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public decimal CoverageValue
        {
            get { return Convert.ToDecimal(txtCoverageValue.Value); }
        }

        public decimal MarginValue
        {
            get { return Convert.ToDecimal(txtMarginValue.Value); }
        }

        protected void cboBridgingType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString())
            {
                trCombo.Visible = true;
                trText1.Visible = false;
                trText2.Visible = false;

                var svc = new Common.LinkLis.Service();
                var poli = svc.GetStatus();
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in poli.Data)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.NamaStatus, item.IdStatus));
                }
            }
            else
            {
                trCombo.Visible = false;
                trText1.Visible = true;
                trText2.Visible = true;
            }
        }
    }
}