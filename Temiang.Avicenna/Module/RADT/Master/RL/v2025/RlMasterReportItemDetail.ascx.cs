using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master.v2025
{
    public partial class RlMasterReportItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadNumericTextBox txtRlMasterReportID
        {
            get
            {
                return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtRlMasterReportID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ComboBox.PopulateWithSmf(cboSRParamedicRL1);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtRlMasterReportItemID.Text = "1";
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRlMasterReportItemNo.ReadOnly = true;
            txtRlMasterReportItemCode.ReadOnly = true;
            txtRlMasterReportItemName.ReadOnly = true;

            switch (txtRlMasterReportID.Value.ToString())
            {
                case "2":
                case "4":
                case "18":
                    txtParameterValue.ReadOnly = true;
                    break;
                case "1":
                case "5":
                case "9":
                case "12":
                    txtParameterValue.ReadOnly = false;
                    break;
                case "25":
                    cboSRParamedicRL1.Enabled = false;
                    break;
                default:
                    txtParameterValue.ReadOnly = true;
                    cboSRParamedicRL1.Enabled = false;
                    break;
            }

            txtRlMasterReportItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID));
            txtRlMasterReportItemNo.Text = (String)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemNo);
            txtRlMasterReportItemCode.Text = (String)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemCode);
            txtRlMasterReportItemName.Text = (String)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemName);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.IsActive);
            cboSRParamedicRL1.SelectedValue = (String)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.SRParamedicRL1);
            txtParameterValue.Text = (String)DataBinder.Eval(DataItem, RlMasterReportItemV2025Metadata.ColumnNames.ParameterValue);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                RlMasterReportItemV2025Collection coll =
                    (RlMasterReportItemV2025Collection)Session["collRlMasterReportItemV2025"];

                //TODO: Betulkan cara pengecekannya
                string id = txtRlMasterReportItemID.Text;
                bool isExist = false;
                foreach (RlMasterReportItemV2025 item in coll)
                {
                    if (item.RlMasterReportID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 RlMasterReportItemID
        {
            get { return Convert.ToInt32(txtRlMasterReportItemID.Text); }
        }
        public String RlMasterReportItemNo
        {
            get { return txtRlMasterReportItemNo.Text; }
        }
        public String RlMasterReportItemCode
        {
            get { return txtRlMasterReportItemCode.Text; }
        }
        public String RlMasterReportItemName
        {
            get { return txtRlMasterReportItemName.Text; }
        }
        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
        public String SRParamedicRL1
        {
            get { return cboSRParamedicRL1.SelectedValue; }
        }
        public String SRParamedicRL1Name
        {
            get { return cboSRParamedicRL1.Text; }
        }
        public String ParameterValue
        {
            get { return txtParameterValue.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
