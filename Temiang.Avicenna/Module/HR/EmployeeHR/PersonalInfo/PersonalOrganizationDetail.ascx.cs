using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalOrganizationDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSROrganizationType, AppEnum.StandardReference.OrganizationType);
            StandardReference.InitializeIncludeSpace(cboSROrganizationRole, AppEnum.StandardReference.OrganizationRole);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalOrganizationID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalOrganizationID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID));
            txtOrganizationName.Text = (String)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.OrganizationName);
            txtLocation.Text = (String)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.Location);
            cboSROrganizationType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.SROrganizationType);
            cboSROrganizationRole.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.SROrganizationRole);
           
            object validFrom = DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.ValidFrom);
            if (validFrom != null)
                txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.ValidFrom);
            else
                txtValidFrom.Clear();

            object validTo = DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.ValidFrom);
            if (validFrom != null)
                txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.ValidTo);
            else
                txtValidTo.Clear();

            txtNote.Text = (String)DataBinder.Eval(DataItem, PersonalOrganizationMetadata.ColumnNames.Note);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalOrganizationCollection coll =
                    (PersonalOrganizationCollection)Session["collPersonalOrganization" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtOrganizationName.Text;
                bool isExist = false;
                foreach (PersonalOrganization item in coll)
                {
                    if (item.PersonID.Equals(id))
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
        public Int32 PersonalOrganizationID
        {
            get { return Convert.ToInt32(txtPersonalOrganizationID.Text); }
        }
        public String OrganizationName
        {
            get { return txtOrganizationName.Text; }
        }
        public String Location
        {
            get { return txtLocation.Text; }
        }
        public String SROrganizationType
        {
            get { return cboSROrganizationType.SelectedValue; }
        }
        public String SROrganizationRole
        {
            get { return cboSROrganizationRole.SelectedValue; }
        }
        public String OrganizationRoleName
        {
            get { return cboSROrganizationRole.Text; }
        }
        public DateTime? ValidFrom
        {
            get { return txtValidFrom.SelectedDate; }
        }
        public DateTime? ValidTo
        {
            get { return txtValidTo.SelectedDate; }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
