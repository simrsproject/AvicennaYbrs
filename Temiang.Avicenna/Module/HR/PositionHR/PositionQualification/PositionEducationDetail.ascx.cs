using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionEducationDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);
            StandardReference.InitializeIncludeSpace(cboSREducationField, AppEnum.StandardReference.EducationField);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionEducationID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionEducationID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionEducationMetadata.ColumnNames.PositionEducationID));
            cboSRRequirement.SelectedValue = (String)DataBinder.Eval(DataItem, PositionEducationMetadata.ColumnNames.SRRequirement);
            cboSREducationLevel.SelectedValue = (String)DataBinder.Eval(DataItem, PositionEducationMetadata.ColumnNames.SREducationLevel);
            cboSREducationField.SelectedValue = (String)DataBinder.Eval(DataItem, PositionEducationMetadata.ColumnNames.SREducationField);
            txtEducationNotes.Text = (String)DataBinder.Eval(DataItem, PositionEducationMetadata.ColumnNames.EducationNotes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionEducationCollection coll =
                    (PositionEducationCollection)Session["collPositionEducation"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionEducationID.Text;
                bool isExist = false;
                foreach (PositionEducation item in coll)
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
        public Int32 PositionEducationID
        {
            get { return Convert.ToInt32(txtPositionEducationID.Text); }
        }
        public String SRRequirement
        {
            get { return cboSRRequirement.SelectedValue; }
        }
        public String HRRequirementName
        {
            get { return cboSRRequirement.Text; }
        }
        public String SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public String EducationLevelName
        {
            get { return cboSREducationLevel.Text; }
        }
        public String SREducationField
        {
            get { return cboSREducationField.SelectedValue; }
        }
        public String EducationFieldName
        {
            get { return cboSREducationField.Text; }
        }
        public String EducationNotes
        {
            get { return txtEducationNotes.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
