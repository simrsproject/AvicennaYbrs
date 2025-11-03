using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class RecruitmentTestScoring : BasePageDialog
    {
        private string personId
        {
            get { return string.IsNullOrEmpty(Request.QueryString["pid"]) ? "-1" : Request.QueryString["pid"]; }
        }

        private string recruitmentTestId
        {
            get { return string.IsNullOrEmpty(Request.QueryString["tid"]) ? "-1" : Request.QueryString["tid"]; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRecruitmentTestConclusion, AppEnum.StandardReference.RecruitmentTestConclusion);

                hdnSRRecruitmentTest.Value = string.Empty;
                var emp = new PersonalInfo();
                if (emp.LoadByPrimaryKey(personId.ToInt()))
                {
                    txtEmployeeNumber.Text = emp.EmployeeNumber;
                    txtEmployeeName.Text = emp.EmployeeName;

                    var rt = new PersonalRecruitmentTest();
                    if (rt.LoadByPrimaryKey(recruitmentTestId.ToInt()))
                    {
                        txtTestDate.SelectedDate = rt.TestDate;
                        hdnSRRecruitmentTest.Value = rt.SRRecruitmentTest;

                        double testresult = -1;
                        double.TryParse(rt.TestResult, out testresult);
                        txtTestResult.Value = testresult;

                        txtNotes.Text = rt.Notes;

                        var std = new AppStandardReferenceItem();
                        if (std.LoadByPrimaryKey(AppEnum.StandardReference.RecruitmentTest.ToString(), rt.SRRecruitmentTest))
                        {
                            txtRecruitmentTestName.Text = std.ItemName;
                        }
                        cboSRRecruitmentTestConclusion.SelectedValue = rt.SRRecruitmentTestConclusion;
                    }
                }

                PopulateEvalGrid();
            }
        }

        #region detail PersonalRecruitmentTestEvaluator
        private PersonalRecruitmentTestEvaluatorCollection PersonalRecruitmentTestEvaluators
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPersonalRecruitmentTestEvaluator" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((PersonalRecruitmentTestEvaluatorCollection)(obj));
                    }
                }

                var coll = new PersonalRecruitmentTestEvaluatorCollection();
                var query = new PersonalRecruitmentTestEvaluatorQuery("a");
                var pinfo = new PersonalInfoQuery("b");
                var position = new PositionQuery("c");

                query.Select
                    (
                    query,
                    pinfo.EmployeeName.As("refTo_EvaluatorName"),
                    position.PositionName.As("refTo_PositionName")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.EvaluatorID);
                query.InnerJoin(position).On(position.PositionID == query.PositionID);

                query.Where(query.PersonalRecruitmentTestID == recruitmentTestId);

                coll.Load(query);

                Session["collPersonalRecruitmentTestEvaluator" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collPersonalRecruitmentTestEvaluator" + Request.UserHostName] = value; }
        }

        private void PopulateEvalGrid()
        {
            //Display Data Detail
            PersonalRecruitmentTestEvaluators = null; //Reset Record Detail
            grdEvaluator.DataSource = PersonalRecruitmentTestEvaluators; //Requery
            grdEvaluator.MasterTableView.IsItemInserted = false;
            grdEvaluator.MasterTableView.ClearEditItems();
            grdEvaluator.DataBind();
        }

        protected void grdEvaluator_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEvaluator.DataSource = PersonalRecruitmentTestEvaluators;
        }

        protected void grdEvaluator_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            PersonalRecruitmentTestEvaluator entity = FindEvalItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID].ToInt());

            if (entity != null)
                SetEntityValue(entity, e);

            CalculateTestResult();
        }

        protected void grdEvaluator_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            Int32 id = item.OwnerTableView.DataKeyValues[item.ItemIndex][PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID].ToInt();
            PersonalRecruitmentTestEvaluator entity = FindEvalItem(id);

            if (entity != null)
                entity.MarkAsDeleted();

            CalculateTestResult();
        }

        protected void grdEvaluator_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalRecruitmentTestEvaluator entity = PersonalRecruitmentTestEvaluators.AddNew();

            SetEntityValue(entity, e);

            CalculateTestResult();

            //Stay in insert mode
            e.Canceled = true;
            grdEvaluator.Rebind();
        }

        private PersonalRecruitmentTestEvaluator FindEvalItem(Int32 id)
        {
            PersonalRecruitmentTestEvaluatorCollection coll = PersonalRecruitmentTestEvaluators;
            PersonalRecruitmentTestEvaluator retEntity = null;
            foreach (PersonalRecruitmentTestEvaluator rec in coll)
            {
                if (rec.EvaluatorID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PersonalRecruitmentTestEvaluator entity, GridCommandEventArgs e)
        {
            var userControl = (RecruitmentTestScoringEvaluatorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PersonalRecruitmentTestID = recruitmentTestId.ToInt();
                entity.EvaluatorID = userControl.EvaluatorID;
                entity.EvaluatorName = userControl.EvaluatorName;
                entity.PositionID = userControl.PositionID;
                entity.PositionName = userControl.PositionName;
                entity.Score = userControl.Score;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }
        #endregion
        private void CalculateTestResult()
        {
            if (PersonalRecruitmentTestEvaluators.Count > 0)
            {
                decimal? total = 0;

                foreach (PersonalRecruitmentTestEvaluator item in PersonalRecruitmentTestEvaluators)
                {
                    total += item.Score;
                }

                txtTestResult.Value = Convert.ToDouble(total);
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            HideInformationHeader();

            if (PersonalRecruitmentTestEvaluators.Count == 0)
            {
                ShowInformationHeader("Evaluator required.");
                return false;
            }

            var rt = new PersonalRecruitmentTest();
            if (rt.LoadByPrimaryKey(recruitmentTestId.ToInt()))
            {
                using (var trans = new esTransactionScope())
                {
                    PersonalRecruitmentTestEvaluators.Save();

                    decimal total = Convert.ToDecimal(txtTestResult.Value);
                    
                    if (total > 0)
                        rt.TestResult = string.Format("{0:n2}", total);

                    rt.Notes = txtNotes.Text;
                    rt.SRRecruitmentTestConclusion = cboSRRecruitmentTestConclusion.SelectedValue;
                    rt.LastUpdateDateTime = DateTime.Now;
                    rt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    rt.Save();

                    trans.Complete();
                }
            }

            return true;
        }
    }
}