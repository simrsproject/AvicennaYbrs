using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeTrainingHistoryDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRActivityType, AppEnum.StandardReference.ActivityType);
            StandardReference.InitializeIncludeSpace(cboSRTrainingFinancingSources, AppEnum.StandardReference.TrainingFinancingSources);
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingPointType, AppEnum.StandardReference.EmployeeTrainingPointType);
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingDateSeparator, AppEnum.StandardReference.EmployeeTrainingDateSeparator);
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingRole, AppEnum.StandardReference.EmployeeTrainingRole);
            
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeTrainingHistoryID.Text = "1";
                chkIsAttending.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeTrainingHistoryID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID));
            chkIsAttending.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending));
            txtEventName.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EventName);
            txtTrainingLocation.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.TrainingLocation);
            txtTrainingInstitution.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.TrainingInstitution);
            txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.StartDate);
            txtEndDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EndDate);
            txtTotalHour.Value= Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.TotalHour));
            txtCreditPoint.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.CreditPoint));
            txtPlanningCosts.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.PlanningCosts));
            txtFee.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.Fee));
            txtSponsorFee.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SponsorFee));
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.Note);
            chkIsInHouseTraining.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.IsInHouseTraining));
            chkIsScheduledTraining.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.IsScheduledTraining));

            cboSRActivityType.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SRActivityType);
            string activitySubType = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SRActivitySubType);
            if (!string.IsNullOrEmpty(activitySubType))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                        query.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(),
                        query.ItemID == activitySubType
                    );

                cboSRActivitySubType.DataSource = query.LoadDataTable();
                cboSRActivitySubType.DataBind();
                cboSRActivitySubType.SelectedValue = activitySubType;
            }
            else
            {
                cboSRActivitySubType.Items.Clear();
                cboSRActivitySubType.SelectedValue = string.Empty;
                cboSRActivitySubType.Text = string.Empty;
            }
            object certificateValidityPeriod = DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod);
            if (certificateValidityPeriod != null)
                txtCertificateValidityPeriod.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.CertificateValidityPeriod);
            else
                txtCertificateValidityPeriod.Clear();
            chkIsCommitmentToWork.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.IsCommitmentToWork));
            txtLengthOfService.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.LengthOfService));

            object startServiceDate = DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate);
            if (startServiceDate != null)
                txtStartServiceDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.StartServiceDate);
            else
                txtStartServiceDate.Clear();

            object endServiceDate = DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate);
            if (endServiceDate != null)
                txtEndServiceDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EndServiceDate);
            else
                txtEndServiceDate.Clear();

            cboSRTrainingFinancingSources.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SRTrainingFinancingSources);

            object evaluationDate = DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate);
            if (evaluationDate != null)
                txtEvaluationDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EvaluationDate);
            else
                txtEvaluationDate.Clear();

            txtDurationHour.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.DurationHour));
            txtDurationMinutes.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.DurationMinutes));
            cboSREmployeeTrainingPointType.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingPointType);

            string separator = Convert.ToString(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingDateSeparator));
            if (!string.IsNullOrEmpty(separator))
                separator = "-";
            cboSREmployeeTrainingDateSeparator.SelectedValue = separator;
            cboSREmployeeTrainingRole.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole));

            object supervisorEvaluationDate = DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SupervisorEvaluationDateTime);
            if (supervisorEvaluationDate != null)
                txtEvaluationDate.Enabled = false;
            else
                txtEvaluationDate.Enabled = true;
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeTrainingHistoryCollection coll =
                    (EmployeeTrainingHistoryCollection)Session["collEmployeeTrainingHistory" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtEmployeeTrainingHistoryID.Text;
                bool isExist = false;
                foreach (EmployeeTrainingHistory item in coll)
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
        public Int32 EmployeeTrainingHistoryID
        {
            get { return Convert.ToInt32(txtEmployeeTrainingHistoryID.Text); }
        }
        public String EventName
        {
            get { return txtEventName.Text; }
        }
        public String TrainingLocation
        {
            get { return txtTrainingLocation.Text; }
        }
        public String TrainingInstitution
        {
            get { return txtTrainingInstitution.Text; }
        }
        public DateTime StartDate
        {
            get { return Convert.ToDateTime(txtStartDate.SelectedDate); }
        }
        public DateTime EndDate
        {
            get { return Convert.ToDateTime(txtEndDate.SelectedDate); }
        }
        public Int32 TotalHour
        {
            get { return Convert.ToInt32(txtTotalHour.Value); }
        }
        public Decimal CreditPoint
        {
            get { return Convert.ToDecimal(txtCreditPoint.Value); }
        }
        public Decimal PlanningCosts
        {
            get { return Convert.ToDecimal(txtPlanningCosts.Value); }
        }
        public Decimal Fee
        {
            get { return Convert.ToDecimal(txtFee.Value); }
        }
        public Decimal SponsorFee
        {
            get { return Convert.ToDecimal(txtSponsorFee.Value); }
        }
        public bool IsAttending
        {
            get { return chkIsAttending.Checked; }
        }
        public String Note
        {
            get { return txtNote.Text; }
        }
        public bool IsInHouseTraining
        {
            get { return chkIsInHouseTraining.Checked; }
        }
        public bool IsScheduledTraining
        {
            get { return chkIsScheduledTraining.Checked; }
        }
        public String SRActivityType
        {
            get { return cboSRActivityType.SelectedValue; }
        }
        public String ActivityTypeName
        {
            get { return cboSRActivityType.Text; }
        }
        public String SRActivitySubType
        {
            get { return cboSRActivitySubType.SelectedValue; }
        }
        public String ActivitySubTypeName
        {
            get { return cboSRActivitySubType.Text; }
        }
        public DateTime? CertificateValidityPeriod
        {
            get { return txtCertificateValidityPeriod.SelectedDate; }
        }
        public bool IsCommitmentToWork
        {
            get { return chkIsCommitmentToWork.Checked; }
        }
        public Int16 LengthOfService
        {
            get { return Convert.ToInt16(txtLengthOfService.Value); }
        }
        public DateTime? StartServiceDate
        {
            get { return txtStartServiceDate.SelectedDate; }
        }
        public DateTime? EndServiceDate
        {
            get { return txtEndServiceDate.SelectedDate; }
        }
        public String SRTrainingFinancingSources
        {
            get { return cboSRTrainingFinancingSources.SelectedValue; }
        }
        public String TrainingFinancingSourcesName
        {
            get { return cboSRTrainingFinancingSources.Text; }
        }
        public DateTime? EvaluationDate
        {
            get { return txtEvaluationDate.SelectedDate; }
        }
        public String SREmployeeTrainingPointType
        {
            get { return cboSREmployeeTrainingPointType.SelectedValue; }
        }
        public Decimal DurationHour
        {
            get { return Convert.ToDecimal(txtDurationHour.Value); }
        }
        public Decimal DurationMinutes
        {
            get { return Convert.ToDecimal(txtDurationMinutes.Value); }
        }
        public String SREmployeeTrainingDateSeparator
        {
            get { return cboSREmployeeTrainingDateSeparator.SelectedValue; }
        }
        public String SREmployeeTrainingRole
        {
            get { return cboSREmployeeTrainingRole.SelectedValue; }
        }
        public String EmployeeTrainingRoleName
        {
            get { return cboSREmployeeTrainingRole.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        protected void txtStartDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (txtEvaluationDate.Enabled)
                txtEvaluationDate.SelectedDate = txtStartDate.SelectedDate.Value.AddMonths(AppSession.Parameter.IntervalTrainingEvaluationSchedule);
        }

        protected void cboSRActivitySubType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ActivitySubType.ToString(),
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true,
                    query.ReferenceID == cboSRActivityType.SelectedValue
                );

            cboSRActivitySubType.DataSource = query.LoadDataTable();
            cboSRActivitySubType.DataBind();
        }

        protected void cboSRActivitySubType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRActivityType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRActivitySubType.Items.Clear();
            cboSRActivitySubType.SelectedValue = string.Empty;
            cboSRActivitySubType.Text = string.Empty;
        }
        #endregion
    }
}
