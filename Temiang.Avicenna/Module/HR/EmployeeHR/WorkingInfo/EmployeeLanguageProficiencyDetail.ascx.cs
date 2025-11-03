using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeLanguageProficiencyDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRLanguage, AppEnum.StandardReference.LanguageProficiency);
            StandardReference.InitializeIncludeSpace(cboSRConversation, AppEnum.StandardReference.LanguageCapable);
            StandardReference.InitializeIncludeSpace(cboSRTranslation, AppEnum.StandardReference.LanguageCapable);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeLanguageProficiencyID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeLanguageProficiencyID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.EmployeeLanguageProficiencyID));
            txtEvaluationDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.EvaluationDate);
            cboSRLanguage.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.SRLanguage);
            cboSRConversation.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.SRConversation);
            cboSRTranslation.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.SRTranslation);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, EmployeeLanguageProficiencyMetadata.ColumnNames.Notes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeLanguageProficiencyCollection coll =
                    (EmployeeLanguageProficiencyCollection)Session["collEmployeeLanguageProficiency" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtEmployeeLanguageProficiencyID.Text;
                bool isExist = false;
                foreach (EmployeeLanguageProficiency item in coll)
                {
                    if (item.EmployeeLanguageProficiencyID.Equals(id))
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
        public Int32 EmployeeLanguageProficiencyID
        {
            get { return Convert.ToInt32(txtEmployeeLanguageProficiencyID.Text); }
        }
        public DateTime EvaluationDate
        {
            get { return Convert.ToDateTime(txtEvaluationDate.SelectedDate); }
        }
        public String SRLanguage
        {
            get { return cboSRLanguage.SelectedValue; }
        }
        public String LanguageName
        {
            get { return cboSRLanguage.Text; }
        }
        public String SRConversation
        {
            get { return cboSRConversation.SelectedValue; }
        }
        public String ConversationName
        {
            get { return cboSRConversation.Text; }
        }
        public String SRTranslation
        {
            get { return cboSRTranslation.SelectedValue; }
        }
        public String TranslationName
        {
            get { return cboSRTranslation.Text; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
