using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingEvaluationDetail : BasePageDetail
    {
        private string FromPageId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pageId"]) ? "" : Request.QueryString["pageId"];
            }
        }

        private string PersonId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["pid"]) ? "-1" : Request.QueryString["pid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "##";

            switch (FromPageId)
            {
                case "ewi":
                    UrlPageList = "../../../HR/EmployeeHR/WorkingInfo/EmployeeWorkingInfoDetail.aspx?md=view&id=" + PersonId + "&status=";
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;

                case "gen":
                    UrlPageList = "../../../HR/EmployeeHR/Logbook/LogbookDetail.aspx?id=" + PersonId + "&type=gen";
                    ProgramID = AppConstant.Program.EmployeeLogbook;
                    break;
                case "c01":
                    UrlPageList = "../../../HR/EmployeeHR/Logbook/LogbookDetail.aspx?id=" + PersonId + "&type=c01";
                    ProgramID = AppConstant.Program.EmployeeLogbookMedicalCommitte;
                    break;
                case "c02":
                    UrlPageList = "../../../HR/EmployeeHR/Logbook/LogbookDetail.aspx?id=" + PersonId + "&type=c02";
                    ProgramID = AppConstant.Program.EmployeeLogbookNursingCommitte;
                    break;
                case "c03":
                    UrlPageList = "../../../HR/EmployeeHR/Logbook/LogbookDetail.aspx?id=" + PersonId + "&type=c03";
                    ProgramID = AppConstant.Program.EmployeeLogbookKtkl;
                    break;
                default:
                    UrlPageList = "EmployeeTrainingEvaluationList.aspx";
                    ProgramID = AppConstant.Program.EmployeeTrainingEvaluation;
                    break;
            }

            if (IsPostBack) return;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuMoveNext.Visible = false;
            ToolBarMenuMovePrev.Visible = false;

            if (FromPageId != "" && FromPageId != "gen")
                ToolBarMenuEdit.Enabled = false;
            else if (FromPageId == "gen")
            {
                var ewi = new EmployeeWorkingInfo();
                if (!(ewi.LoadByPrimaryKey(PersonId.ToInt()) && ewi.SupervisorId == AppSession.UserLogin.PersonID))
                {
                    ToolBarMenuEdit.Enabled = false;
                }
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingHistory();
            if (entity.LoadByPrimaryKey(txtEmployeeTrainingHistoryID.Text.ToInt()))
            {
                if (!string.IsNullOrEmpty(entity.SupervisorEvaluationNoteByUserID) && entity.SupervisorEvaluationNoteByUserID != AppSession.UserLogin.UserID)
                {
                    var usr = new AppUser();
                    usr.LoadByPrimaryKey(entity.SupervisorEvaluationNoteByUserID);

                    args.MessageText = string.Format("This data belong to {0}. You do not have authorization to make edits.", usr.UserName);
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingHistory();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeTrainingHistoryID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeTrainingHistory();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeTrainingHistoryID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //auditLogFilter.PrimaryKeyData = string.Format("EmployeeTrainingHistoryID='{0}'", txtEmployeeTrainingHistoryID.Text.Trim());
            //auditLogFilter.TableName = "EmployeeTrainingHistory";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeTrainingHistory();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeTrainingHistoryID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var training = (EmployeeTrainingHistory)entity;
            txtEmployeeTrainingHistoryID.Value = training.EmployeeTrainingHistoryID;
            txtPersonID.Value = training.PersonID;

            var ewiQ = new VwEmployeeTableQuery();
            ewiQ.Where(ewiQ.PersonID == training.PersonID);
            var ewi = new VwEmployeeTable();
            ewi.Load(ewiQ);
            txtEmployeeNumber.Text = ewi.EmployeeNumber;
            txtEmployeeName.Text = ewi.EmployeeName;

            var std = new AppStandardReferenceItem();
            if (std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeStatus.ToString(), ewi.SREmployeeStatus))
                txtSREmployeeStatusName.Text = std.ItemName;
            else
                txtSREmployeeStatusName.Text = string.Empty;

            var pos = new Position();
            if (pos.LoadByPrimaryKey(ewi.PositionID.ToInt()))
                txtPositionName.Text = pos.PositionName;
            else txtPositionName.Text = string.Empty;

            txtDateStart.SelectedDate = training.StartDate ?? DateTime.Now.Date;
            lblSREmployeeTrainingDateSeparator.Text = string.IsNullOrEmpty(training.SREmployeeTrainingDateSeparator) ? "-" : training.SREmployeeTrainingDateSeparator;
            txtDateEnd.SelectedDate = training.EndDate ?? DateTime.Now.Date;

            txtTrainingLocation.Text = training.TrainingLocation;
            txtTrainingName.Text = training.EventName;

            std = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(training.SRActivityType) && std.LoadByPrimaryKey(AppEnum.StandardReference.ActivityType.ToString(), training.SRActivityType))
                txtSRActivityTypeName.Text = std.ItemName;
            else
                txtSRActivityTypeName.Text = string.Empty;

            if (!string.IsNullOrEmpty(training.SRActivitySubType))
            {
                std = new AppStandardReferenceItem();
                if (!string.IsNullOrEmpty(training.SRActivitySubType) && std.LoadByPrimaryKey(AppEnum.StandardReference.ActivitySubType.ToString(), training.SRActivitySubType))
                    txtSRActivitySubTypeName.Text = std.ItemName;
                else
                    txtSRActivitySubTypeName.Text = string.Empty;
            }
            else
                txtSRActivitySubTypeName.Text = string.Empty;

            if (training.EvaluationDate != null)
                txtEvaluationDate.SelectedDate = training.EvaluationDate ?? DateTime.Now.Date;
            else
                txtEvaluationDate.SelectedDate = training.StartDate.Value.AddMonths(6);
            txtEvaluationNote.Text = training.SupervisorEvaluationNote;
            txtEvaluationScore.Value = Convert.ToDouble(training.EvaluationScore);
            txtRecommendation.Text = training.Recommendation;

            var usr = new AppUser();
            if (!string.IsNullOrEmpty(training.SupervisorEvaluationNoteByUserID) && usr.LoadByPrimaryKey(training.SupervisorEvaluationNoteByUserID))
                txtEvaluationBy.Text = usr.UserName;
            else
            {
                if (FromPageId == "")
                    txtEvaluationBy.Text = AppSession.UserLogin.UserName;
                else
                {
                    if (FromPageId != "gen")
                        txtEvaluationBy.Text = string.Empty;
                    else
                    {
                        if (ewi.SupervisorId == AppSession.UserLogin.PersonID.ToInt())
                            txtEvaluationBy.Text = AppSession.UserLogin.UserName;
                        else
                            txtEvaluationBy.Text = string.Empty;
                    }
                }
            }

            var acq = new EmployeeTrainingAssessmentCriteriaQuery();
            acq.Where(acq.MinValue <= txtEvaluationScore.Value, acq.MaxValue >= txtEvaluationScore.Value);
            acq.es.Top = 1;
            var ac = new EmployeeTrainingAssessmentCriteria();
            ac.Load(acq);

            lblResult.Text = ac.AssessmentCriteriaName;

            PopulateEvaluationGrid(string.IsNullOrEmpty(training.SupervisorEvaluationNoteByUserID));
        }

        private void SetEntityValue(EmployeeTrainingHistory training)
        {
            training.EmployeeTrainingID = Convert.ToInt32(txtEmployeeTrainingHistoryID.Value);

            training.EvaluationDate = txtEvaluationDate.SelectedDate;
            training.SupervisorEvaluationNote = txtEvaluationNote.Text;
            training.SupervisorEvaluationNoteByUserID = AppSession.UserLogin.UserID;
            training.SupervisorEvaluationDateTime = DateTime.Now;

            //Last Update Status
            if (training.es.IsAdded || training.es.IsModified)
            {
                training.LastUpdateByUserID = AppSession.UserLogin.UserID;
                training.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeTrainingHistory entity)
        {
            using (var trans = new esTransactionScope())
            {
                var evals = new EmployeeTrainingEvaluationCollection();
                evals.Query.Where(evals.Query.EmployeeTrainingHistoryID == entity.EmployeeTrainingHistoryID);
                evals.LoadAll();

                decimal totalScore = 0;
                int i = 0;
                foreach (GridDataItem dataItem in grdEvaluation.MasterTableView.Items)
                {
                    string id = dataItem.GetDataKeyValue("AssessmentAspectID").ToString();
                    decimal result = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtRatingResult")).Value);

                    bool isExist = false;
                    foreach (EmployeeTrainingEvaluation row in evals)
                    {
                        if (row.AssessmentAspectID.Equals(id))
                        {
                            isExist = true;
                            row.RatingResult = result;

                            break;
                        }
                    }
                    //Add
                    if (!isExist)
                    {
                        EmployeeTrainingEvaluation row = evals.AddNew();
                        row.EmployeeTrainingHistoryID = entity.EmployeeTrainingHistoryID;
                        row.AssessmentAspectID = id;
                        row.RatingResult = result;
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        row.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    totalScore += result;
                    i++;
                }

                entity.EvaluationScore = totalScore / i;
                var acq = new EmployeeTrainingAssessmentCriteriaQuery();
                acq.Where(acq.MinValue <= entity.EvaluationScore, acq.MaxValue >= entity.EvaluationScore);
                acq.es.Top = 1;
                var ac = new EmployeeTrainingAssessmentCriteria();
                ac.Load(acq);

                entity.Recommendation = ac.Recommendation;

                entity.Save();
                evals.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeTrainingHistoryQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeTrainingHistoryID > txtEmployeeTrainingHistoryID.Value);
                que.OrderBy(que.EmployeeTrainingHistoryID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeTrainingHistoryID < txtEmployeeTrainingHistoryID.Text);
                que.OrderBy(que.EmployeeTrainingHistoryID.Descending);
            }

            var entity = new EmployeeTrainingHistory();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function EmployeeTrainingEvaluation

        private void PopulateEvaluationGrid(bool isNew)
        {
            //Display Data Detail
            grdEvaluation.DataSource = GetEvaluationLists(isNew);
            grdEvaluation.DataBind();
        }

        protected void grdEvaluation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEvaluation.DataSource = GetEvaluationLists(false);
        }

        private DataTable GetEvaluationLists(bool isNew)
        {
            var query = new EmployeeTrainingEvaluationQuery("a");
            var qrRef = new EmployeeTrainingAssessmentAspectQuery("b");
            if (this.DataModeCurrent != AppEnum.DataMode.Read || isNew)
            {
                query.RightJoin(qrRef).On(query.AssessmentAspectID == qrRef.AssessmentAspectID & query.EmployeeTrainingHistoryID == txtEmployeeTrainingHistoryID.Text.ToInt());
                query.Where(qrRef.IsActive == true);
            }
            else
            {
                query.InnerJoin(qrRef).On(query.AssessmentAspectID == qrRef.AssessmentAspectID);
                query.Where(query.EmployeeTrainingHistoryID == txtEmployeeTrainingHistoryID.Text.ToInt());
            }
            
            query.OrderBy(qrRef.AssessmentAspectID.Ascending);
            query.Select
                (
                    qrRef.AssessmentAspectID,
                    qrRef.AssessmentAspectName,
                    "<ISNULL(a.RatingResult, 0) AS RatingResult>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            //grdEvaluation.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdEvaluation.Rebind();
        }
        #endregion
    }
}