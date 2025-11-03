using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalEducationHistoryDetail : System.Web.UI.UserControl
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
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalEducationHistoryID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalEducationHistoryID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID));
            cboSREducationLevel.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.SREducationLevel);
            txtInstitutionName.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.InstitutionName);
            txtLocation.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.Location);
            txtStartYear.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.StartYear);
            txtEndYear.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.EndYear);
            txtGpa.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.Gpa));
            txtAchievement.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.Achievement);
            txtNote.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.Note);

            txtMajors.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.Majors);
            object graduateDate = DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.GraduateDate);
            if (graduateDate != null)
                txtGraduateDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.GraduateDate);
            else
                txtGraduateDate.Clear();
            txtDiplomaNo.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.DiplomaNo);
            txtDiplomaVerificationNo.Text = (String)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.DiplomaVerificationNo);
            object educationalAdjustmentDate = DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate);
            if (educationalAdjustmentDate != null)
                txtEducationalAdjustmentDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate);
            else
                txtEducationalAdjustmentDate.Clear();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalEducationHistoryCollection coll =
                    (PersonalEducationHistoryCollection)Session["collPersonalEducationHistory" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtPersonalEducationHistoryID.Text;
                bool isExist = false;
                foreach (PersonalEducationHistory item in coll)
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
        public Int32 PersonalEducationHistoryID
        {
            get { return Convert.ToInt32(txtPersonalEducationHistoryID.Text); }
        }
        public String SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public String EducationLevelName
        {
            get { return cboSREducationLevel.Text; }
        }
        public String InstitutionName
        {
            get { return txtInstitutionName.Text; }
        }
        public String Location
        {
            get { return txtLocation.Text; }
        }
        public String StartYear
        {
            get { return txtStartYear.Text; }
        }
        public String EndYear
        {
            get { return txtEndYear.Text; }
        }
        public Decimal Gpa
        {
            get { return Convert.ToDecimal(txtGpa.Value); }
        }
        public String Achievement
        {
            get { return txtAchievement.Text; }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        public String Majors
        {
            get { return txtMajors.Text; }
        }
        public DateTime? GraduateDate
        {
            get { return txtGraduateDate.SelectedDate; }
        }
        public String DiplomaNo
        {
            get { return txtDiplomaNo.Text; }
        }
        public String DiplomaVerificationNo
        {
            get { return txtDiplomaVerificationNo.Text; }
        }
        public DateTime? EducationalAdjustmentDate
        {
            get { return txtEducationalAdjustmentDate.SelectedDate; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion
    }
}