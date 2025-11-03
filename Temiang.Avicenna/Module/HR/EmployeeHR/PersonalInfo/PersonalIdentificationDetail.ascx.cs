using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalIdentificationDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRIdentificationType, AppEnum.StandardReference.IdentificationType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalIdentificationID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalIdentificationID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID));
            cboSRIdentificationType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.SRIdentificationType);
            txtIdentificationValue.Text = (String)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.IdentificationValue);
            txtIdentificationName.Text = (String)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.IdentificationName);
            txtPlaceOfIssue.Text = (String)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.PlaceOfIssue);

            object validFrom = DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.ValidFrom);
            if (validFrom != null)
                txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.ValidFrom);
            else
                txtValidFrom.Clear();

            object validTo = DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.ValidFrom);
            if (validFrom != null)
                txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalIdentificationMetadata.ColumnNames.ValidTo);
            else
                txtValidTo.Clear();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalIdentificationCollection coll =
                    (PersonalIdentificationCollection)Session["collPersonalIdentification" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string type = cboSRIdentificationType.SelectedValue;
                bool isExist = false;
                foreach (PersonalIdentification item in coll)
                {
                    if (item.SRIdentificationType.Equals(type))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Identification Type: {0} has exist", cboSRIdentificationType.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PersonalIdentificationID
        {
            get { return Convert.ToInt32(txtPersonalIdentificationID.Text); }
        }
        public String SRIdentificationType
        {
            get { return cboSRIdentificationType.SelectedValue; }
        }
        public String IdentificationTypeName
        {
            get { return cboSRIdentificationType.Text; }
        }
        public String IdentificationValue
        {
            get { return txtIdentificationValue.Text; }
        }
        public String IdentificationName
        {
            get { return txtIdentificationName.Text; }
        }
        public String PlaceOfIssue
        {
            get { return txtPlaceOfIssue.Text; }
        }
        public DateTime? ValidFrom
        {
            get { return txtValidFrom.SelectedDate; }
        }
        public DateTime? ValidTo
        {
            get { return txtValidTo.SelectedDate; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}
