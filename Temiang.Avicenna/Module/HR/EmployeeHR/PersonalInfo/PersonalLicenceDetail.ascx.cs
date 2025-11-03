using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalLicenceDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRLicenceType, AppEnum.StandardReference.LicenseType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalLicenceID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalLicenceID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.PersonalLicenceID));
            cboSRLicenceType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.SRLicenceType);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.ValidTo);
            txtNote.Text = (String)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.Note);
            txtVerificationLetterNo.Text = (String)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.VerificationLetterNo);
            object verificationDate = DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.VerificationDate);
            if (verificationDate != null)
                txtVerificationDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalLicenceMetadata.ColumnNames.VerificationDate);
            else
                txtVerificationDate.Clear();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalLicenceCollection coll =
                    (PersonalLicenceCollection)Session["collPersonalLicence" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string type = cboSRLicenceType.SelectedValue;
                string note = txtNote.Text;
                bool isExist = false;
                foreach (PersonalLicence item in coll)
                {
                    if (item.SRLicenceType.Equals(type) & item.Note.Equals(note))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} with number {1} has exist", cboSRLicenceType.Text, note);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PersonalLicenceID
        {
            get { return Convert.ToInt32(txtPersonalLicenceID.Text); }
        }
        public String SRLicenceType
        {
            get { return cboSRLicenceType.SelectedValue; }
        }
        public String LicenceTypeName
        {
            get { return cboSRLicenceType.Text; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        public String VerificationLetterNo
        {
            get { return txtVerificationLetterNo.Text; }
        }
        public DateTime? VerificationDate
        {
            get { return txtVerificationDate.SelectedDate; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}