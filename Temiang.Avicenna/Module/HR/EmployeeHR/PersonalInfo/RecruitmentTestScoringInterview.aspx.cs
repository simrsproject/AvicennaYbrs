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
    public partial class RecruitmentTestScoringInterview : BasePageDialog
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

                        var std = new AppStandardReferenceItem();
                        if (std.LoadByPrimaryKey(AppEnum.StandardReference.RecruitmentTest.ToString(), rt.SRRecruitmentTest))
                        {
                            txtRecruitmentTestName.Text = std.ItemName;
                        }

                        txtAdvantages.Text = rt.Advantages;
                        txtDeficiency.Text = rt.Deficiency;
                        txtSuggestion.Text = rt.Suggestion;
                        cboSRRecruitmentTestConclusion.SelectedValue = rt.SRRecruitmentTestConclusion;
                    }
                }

                PopulateEvalGrid();
                RefreshGridInterviewItems();
                txtSumAverageScore.Value = CalculateInterviewScore();
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
        }

        protected void grdEvaluator_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalRecruitmentTestEvaluator entity = PersonalRecruitmentTestEvaluators.AddNew();

            SetEntityValue(entity, e);

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

        #region detail PersonalRecruitmentTestInterview
        protected void grdInterview_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInterview.DataSource = PersonalRecruitmentTestInterviews;
        }

        private DataTable PersonalRecruitmentTestInterviews
        {
            get
            {
                object obj = this.Session["PersonalRecruitmentTestInterviews" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));


                PersonalRecruitmentTestInterviewCollection coll = new PersonalRecruitmentTestInterviewCollection();
                DataTable dtb = coll.GetJoin(recruitmentTestId);

                Session["PersonalRecruitmentTestInterviews" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        protected void grdInterview_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["IsDetail"].Text == "False")
                    {
                        //dataItem.ForeColor = Color.DarkBlue;
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                    }
                }
            }
            catch
            { }
        }

        protected void grdInterview_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdInterview_ItemPreRender;
        }

        private void grdInterview_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;
        }

        private void RefreshGridInterviewItems()
        {
            this.Session["PersonalRecruitmentTestInterviews" + Request.UserHostName] = null;
            grdInterview.DataSource = PersonalRecruitmentTestInterviews; //Requery
            grdInterview.DataBind();

            //Session["ClinicalPerformanceAppraisalQuestionnaireScoresheetItems" + Request.UserHostName] = null;
            //grdInterview.Rebind();
        }
        #endregion

        private double CalculateInterviewScore()
        {
            decimal total = 0;
            var i = PersonalRecruitmentTestEvaluators.Count == 0 ? 1 : PersonalRecruitmentTestEvaluators.Count;
            foreach (GridDataItem dataItem in grdInterview.MasterTableView.Items)
            {
                string questionId = dataItem.GetDataKeyValue("QuestionID").ToString();
                decimal score1 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore1")).Value);
                decimal score2 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore2")).Value);
                decimal score3 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore3")).Value);
                decimal score4 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore4")).Value);
                decimal score5 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore5")).Value);
                decimal score6 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore6")).Value);

                var averageScore = (score1 + score2 + score3 + score4 + score5 + score6) / i;

                total += averageScore;
            }

            return Convert.ToDouble(total);
        }

        protected void txtScore_TextChanged(object sender, EventArgs e)
        {
            var i = PersonalRecruitmentTestEvaluators.Count == 0 ? 1 : PersonalRecruitmentTestEvaluators.Count;

            var txt = (RadNumericTextBox)sender;
            var gdi = (GridDataItem)txt.NamingContainer;

            var txtScore1 = gdi.FindControl("txtScore1") as RadNumericTextBox;
            var txtScore2 = gdi.FindControl("txtScore2") as RadNumericTextBox;
            var txtScore3 = gdi.FindControl("txtScore3") as RadNumericTextBox;
            var txtScore4 = gdi.FindControl("txtScore4") as RadNumericTextBox;
            var txtScore5 = gdi.FindControl("txtScore5") as RadNumericTextBox;
            var txtScore6 = gdi.FindControl("txtScore6") as RadNumericTextBox;

            var txtAverageScore = gdi.FindControl("txtAverageScore") as RadNumericTextBox;

            txtAverageScore.Value = (txtScore1.Value + txtScore2.Value + txtScore3.Value + txtScore4.Value + txtScore5.Value + txtScore6.Value) / i;

            txtSumAverageScore.Value = CalculateInterviewScore();
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

            if (PersonalRecruitmentTestEvaluators.Count > 6)
            {
                ShowInformationHeader("The number of evaluator should not be more than 6.");
                return false;
            }

            var rt = new PersonalRecruitmentTest();
            if (rt.LoadByPrimaryKey(recruitmentTestId.ToInt()))
            {
                rt.Advantages = txtAdvantages.Text;
                rt.Deficiency = txtDeficiency.Text;
                rt.Suggestion = txtSuggestion.Text;
                rt.SRRecruitmentTestConclusion = cboSRRecruitmentTestConclusion.SelectedValue;

                using (var trans = new esTransactionScope())
                {
                    PersonalRecruitmentTestEvaluators.Save();

                    decimal total = 0;

                    int i = PersonalRecruitmentTestEvaluators.Count();
                    foreach (GridDataItem dataItem in grdInterview.MasterTableView.Items)
                    {
                        string questionId = dataItem.GetDataKeyValue("QuestionID").ToString();
                        decimal score1 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore1")).Value);
                        decimal score2 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore2")).Value);
                        decimal score3 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore3")).Value);
                        decimal score4 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore4")).Value);
                        decimal score5 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore5")).Value);
                        decimal score6 = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtScore6")).Value);
                        decimal average = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAverageScore")).Value);

                        var item = new PersonalRecruitmentTestInterview();
                        item.Query.Where(item.Query.PersonalRecruitmentTestID == recruitmentTestId.ToInt(), item.Query.SRRecruitmentTestQuestion == questionId);
                        if (!item.Query.Load())
                        {
                            item = new PersonalRecruitmentTestInterview();
                            item.AddNew();
                        }

                        item.PersonalRecruitmentTestID = recruitmentTestId.ToInt();
                        item.SRRecruitmentTestQuestion = questionId;
                        item.Score1 = score1;
                        item.Score2 = score2;
                        item.Score3 = score3;
                        item.Score4 = score4;
                        item.Score5 = score5;
                        item.Score6 = score6;
                        item.AverageScore = average;//(score1 + score2 + score3 + score4 + score5 + score6) / i;

                        total += item.AverageScore ?? 0;

                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;

                        item.Save();
                    }

                    if (total > 0)
                        rt.TestResult = string.Format("{0:n2}", total);

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