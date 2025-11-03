using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionWorkExperienceDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRLineBusiness, AppEnum.StandardReference.LineBusiness);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionWorkExperienceID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID));
            cboSRRequirement.SelectedValue = (String)DataBinder.Eval(DataItem, PositionWorkExperienceMetadata.ColumnNames.SRRequirement);
            cboSRLineBusiness.SelectedValue = (String)DataBinder.Eval(DataItem, PositionWorkExperienceMetadata.ColumnNames.SRLineBusiness);
            txtYearExperience.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionWorkExperienceMetadata.ColumnNames.YearExperience));
            txtWorkExperienceNotes.Text = (String)DataBinder.Eval(DataItem, PositionWorkExperienceMetadata.ColumnNames.WorkExperienceNotes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionWorkExperienceCollection coll =
                    (PositionWorkExperienceCollection)Session["collPositionWorkExperience"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionWorkExperienceID.Text;
                bool isExist = false;
                foreach (PositionWorkExperience item in coll)
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
        public Int32 PositionWorkExperienceID
        {
            get { return Convert.ToInt32(txtPositionWorkExperienceID.Text); }
        }
        public String SRRequirement
        {
            get { return cboSRRequirement.SelectedValue; }
        }
        public String HRRequirementName
        {
            get { return cboSRRequirement.Text; }
        }
        public String SRLineBusiness
        {
            get { return cboSRLineBusiness.SelectedValue; }
        }
        public String LineBusinessName
        {
            get { return cboSRLineBusiness.Text; }
        }
        public Int32 YearExperience
        {
            get { return Convert.ToInt32(txtYearExperience.Text); }
        }
        public String WorkExperienceNotes
        {
            get { return txtWorkExperienceNotes.Text; }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
