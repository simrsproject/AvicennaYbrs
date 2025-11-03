using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class RecruitmentTestDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboTestName, AppEnum.StandardReference.RecruitmentTest);
            StandardReference.InitializeIncludeSpace(cboSRRecruitmentTestConclusion, AppEnum.StandardReference.RecruitmentTestConclusion);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                ViewState["PersonalRecruitmentTestID"] = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;

            ViewState["PersonalRecruitmentTestID"] = Convert.ToInt32(DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID));
            txtTestDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.TestDate));
            cboTestName.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTest);
            txtTestResult.Text = (string)DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.TestResult);
            txtNotes.Text = (string)DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.Notes);
            cboSRRecruitmentTestConclusion.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTestConclusion);
        }

        public int PersonalRecruitmentTestID
        {
            get { return Convert.ToInt32(ViewState["PersonalRecruitmentTestID"]); }
        }

        public DateTime TestDate
        {
            get { return txtTestDate.SelectedDate.Value.Date; }
        }

        public string SRTest
        {
            get { return cboTestName.SelectedValue; }
        }

        public string TestName
        {
            get { return cboTestName.Text; }
        }

        public string TestResult
        {
            get { return txtTestResult.Text; }
        }

        public string Notes
        {
            get { return txtNotes.Text; }
        }

        public string SRRecruitmentTestConclusion
        {
            get { return cboSRRecruitmentTestConclusion.SelectedValue; }
        }

        public string RecruitmentTestConclusionName
        {
            get { return cboSRRecruitmentTestConclusion.Text; }
        }
    }
}