using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class CredentialingLicenseItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRLicenseType, AppEnum.StandardReference.LicenseType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRLicenseType.SelectedValue = (String)DataBinder.Eval(DataItem, CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType);
            txtLicenseNo.Text = (String)DataBinder.Eval(DataItem, CredentialProcessLicenseMetadata.ColumnNames.LicenseNo);
            txtDateOfIssue.SelectedDate = (DateTime)DataBinder.Eval(DataItem, CredentialProcessLicenseMetadata.ColumnNames.DateOfIssue);
            txtValidUntil.SelectedDate = (DateTime)DataBinder.Eval(DataItem, CredentialProcessLicenseMetadata.ColumnNames.ValidUntil);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                CredentialProcessLicenseCollection coll =
                    (CredentialProcessLicenseCollection)Session["collCredentialProcessLicense" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string id = cboSRLicenseType.SelectedValue;
                bool isExist = false;
                foreach (CredentialProcessLicense item in coll)
                {
                    if (item.SRLicenseType.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("License Type: {0} has exist", cboSRLicenseType.Text);
                }
            }
        }

        #region Properties for return entry value
        public String SRLicenseType
        {
            get { return cboSRLicenseType.SelectedValue; }
        }
        public String LicenseTypeName
        {
            get { return cboSRLicenseType.Text; }
        }
        public String LicenseNo
        {
            get { return txtLicenseNo.Text; }
        }
        public DateTime DateOfIssue
        {
            get { return Convert.ToDateTime(txtDateOfIssue.SelectedDate); }
        }
        public DateTime ValidUntil
        {
            get { return Convert.ToDateTime(txtValidUntil.SelectedDate); }
        }
        #endregion
    }
}