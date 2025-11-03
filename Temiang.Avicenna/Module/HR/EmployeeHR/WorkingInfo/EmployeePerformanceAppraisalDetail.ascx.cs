using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeePerformanceAppraisalDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtPerformanceAppraisalID.Text = "1";
                txtParticipantItemID.Text = "-1";
                rfvSRQuarterPeriod.Visible = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPerformanceAppraisalID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID));
            txtParticipantItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.ParticipantItemID));
            txtYearPeriod.Text = (String)DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.YearPeriod);
            cboSRQuarterPeriod.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.SRQuarterPeriod);
            txtScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.Score));
            txtScoreText.Text = (String)DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.ScoreText);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, EmployeePerformanceAppraisalMetadata.ColumnNames.Notes);

            rfvSRQuarterPeriod.Visible = txtParticipantItemID.Text == "-1";
            if (txtParticipantItemID.Text != "-1")
            {
                txtYearPeriod.ReadOnly = true;
                cboSRQuarterPeriod.Enabled = false;
                txtScore.ReadOnly = true;
                txtScoreText.ReadOnly = true;
                txtNotes.ReadOnly = true;
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EmployeePerformanceAppraisalCollection)Session["collEmployeePerformanceAppraisal" + Request.UserHostName + PageId];

                bool isExist = false;
                foreach (EmployeePerformanceAppraisal item in coll)
                {
                    if (item.ParticipantItemID != -1 && item.YearPeriod.Equals(txtYearPeriod.Text) && item.SRQuarterPeriod.Equals(cboSRQuarterPeriod.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Period: {0} has exist", cboSRQuarterPeriod.Text + " " + txtYearPeriod.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PerformanceAppraisalID
        {
            get { return Convert.ToInt32(txtPerformanceAppraisalID.Text); }
        }
        public Int32 ParticipantItemID
        {
            get { return Convert.ToInt32(txtParticipantItemID.Text); }
        }
        public String YearPeriod
        {
            get { return txtYearPeriod.Text; }
        }
        public String SRQuarterPeriod
        {
            get { return cboSRQuarterPeriod.SelectedValue; }
        }
        public String QuarterPeriodName
        {
            get { return cboSRQuarterPeriod.Text; }
        }
        public Decimal Score
        {
            get { return Convert.ToDecimal(txtScore.Value); }
        }
        public String ScoreText
        {
            get { return txtScoreText.Text; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion
    }
}