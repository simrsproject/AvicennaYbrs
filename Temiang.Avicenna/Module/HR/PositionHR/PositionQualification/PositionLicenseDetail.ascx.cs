using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionLicenseDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRRequirement, AppEnum.StandardReference.HRLevelRequirement);
            StandardReference.InitializeIncludeSpace(cboSRLicenseType, AppEnum.StandardReference.LicenseType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionLicenseID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionLicenseID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionLicenseMetadata.ColumnNames.PositionLicenseID));
            cboSRRequirement.SelectedValue = (String)DataBinder.Eval(DataItem, PositionLicenseMetadata.ColumnNames.SRRequirement);
            cboSRLicenseType.SelectedValue = (String)DataBinder.Eval(DataItem, PositionLicenseMetadata.ColumnNames.SRLicenseType);
            txtLicenseNotes.Text = (String)DataBinder.Eval(DataItem, PositionLicenseMetadata.ColumnNames.LicenseNotes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionLicenseCollection coll =
                    (PositionLicenseCollection)Session["collPositionLicense"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionLicenseID.Text;
                bool isExist = false;
                foreach (PositionLicense item in coll)
                {
                    if (item.PositionID.Equals(id))
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
        public Int32 PositionLicenseID
        {
            get { return Convert.ToInt32(txtPositionLicenseID.Text); }
        }
        public String SRRequirement
        {
            get { return cboSRRequirement.SelectedValue; }
        }
        public String HRRequirementName
        {
            get { return cboSRRequirement.Text; }
        }
        public String SRLicenseType
        {
            get { return cboSRLicenseType.SelectedValue; }
        }
        public String LicenseTypeName
        {
            get { return cboSRLicenseType.Text; }
        }
        public String LicenseNotes
        {
            get { return txtLicenseNotes.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
