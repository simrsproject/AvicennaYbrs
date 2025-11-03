using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeEducationDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSREducationStatus, AppEnum.StandardReference.EducationStatus);
            StandardReference.InitializeIncludeSpace(cboSREducationFinancingSources, AppEnum.StandardReference.EducationFinancingSources);
            StandardReference.InitializeIncludeSpace(cboSRStudyPeriodStatus, AppEnum.StandardReference.StudyPeriodStatus);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtEmployeeEducationID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeEducationID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.EmployeeEducationID));
            cboSREducationStatus.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.SREducationStatus);
            cboSREducationFinancingSources.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.SREducationFinancingSources);
            chkIsTuitionAssistance.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.IsTuitionAssistance));
            txtAssistanceAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.AssistanceAmount));
            txtInstitutionName.Text = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.InstitutionName);
            txtStudyProgram.Text = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.StudyProgram);
            txtStartYearPeriod.Text = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.StartYearPeriod);
            txtEndYearPeriod.Text = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.EndYearPeriod);
            cboSRStudyPeriodStatus.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.SRStudyPeriodStatus);
            chkIsCommitmentToWork.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.IsCommitmentToWork));
            txtDurationOfService.Text = (String)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.DurationOfService);
            object startServiceDate = DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.StartServiceDate);
            if (startServiceDate != null)
                txtStartServiceDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.StartServiceDate);
            else
                txtStartServiceDate.Clear();
            object endServiceDate = DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.EndServiceDate);
            if (endServiceDate != null)
                txtEndServiceDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeEducationMetadata.ColumnNames.EndServiceDate);
            else
                txtEndServiceDate.Clear();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        public Int32 EmployeeEducationID
        {
            get { return Convert.ToInt32(txtEmployeeEducationID.Text); }
        }
        public String SREducationStatus
        {
            get { return cboSREducationStatus.SelectedValue; }
        }
        public String EducationStatusName
        {
            get { return cboSREducationStatus.Text; }
        }
        public String SREducationFinancingSources
        {
            get { return cboSREducationFinancingSources.SelectedValue; }
        }
        public String EducationFinancingSourcesName
        {
            get { return cboSREducationFinancingSources.Text; }
        }
        public bool IsTuitionAssistance
        {
            get { return chkIsTuitionAssistance.Checked; }
        }
        public Decimal AssistanceAmount
        {
            get { return Convert.ToDecimal(txtAssistanceAmount.Value); }
        }
        public String InstitutionName
        {
            get { return txtInstitutionName.Text; }
        }
        public String StudyProgram
        {
            get { return txtStudyProgram.Text; }
        }
        public String StartYearPeriod
        {
            get { return txtStartYearPeriod.Text; }
        }
        public String EndYearPeriod
        {
            get { return txtEndYearPeriod.Text; }
        }
        public String SRStudyPeriodStatus
        {
            get { return cboSRStudyPeriodStatus.SelectedValue; }
        }
        public String StudyPeriodStatusName
        {
            get { return cboSRStudyPeriodStatus.Text; }
        }
        public bool IsCommitmentToWork
        {
            get { return chkIsCommitmentToWork.Checked; }
        }
        public String DurationOfService
        {
            get { return txtDurationOfService.Text; }
        }
        public DateTime? StartServiceDate
        {
            get { return txtStartServiceDate.SelectedDate; }
        }
        public DateTime? EndServiceDate
        {
            get { return txtEndServiceDate.SelectedDate; }
        }
        #endregion
    }
}