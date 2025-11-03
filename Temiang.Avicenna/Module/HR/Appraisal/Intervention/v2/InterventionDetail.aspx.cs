using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Appraisal.Intervention.v2
{
    public partial class InterventionDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalIntervention;

            UrlPageSearch = "##";
            UrlPageList = "InterventionList.aspx";

            txtScoresheetID.Text = string.IsNullOrEmpty(Request.QueryString["id"]) ? "0" : Request.QueryString["id"].ToString();
            txtReferenceID.Text = string.IsNullOrEmpty(Request.QueryString["rid"]) ? "0" : Request.QueryString["rid"].ToString();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppraisalScoresheet());

            txtScoringDate.SelectedDate = DateTime.Now.Date;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (entity.Query.Load())
            {
                entity.MarkAsDeleted();

                var coll = new AppraisalScoresheetItemCollection();
                coll.Query.Where(coll.Query.ScoresheetID == entity.ScoresheetID);
                coll.Query.Load();
                coll.MarkAllAsDeleted();

                entity.Save();
                coll.Save();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (entity.Query.Load())
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ScoresheetID='{0}'", txtScoresheetID.Value.ToInt());
            auditLogFilter.TableName = "AppraisalScoresheet";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Refresh Grid Detail
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdList.Columns[5].Visible = !isVisible;
            grdList.Columns[6].Visible = !isVisible;
            grdList.Columns[7].Visible = isVisible;
            grdList.Columns[8].Visible = isVisible;

            RefreshGrid();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppraisalScoresheet();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                {
                    entity.Query.Where(entity.Query.ScoresheetID == id.ToInt());
                    entity.Query.Load();
                }
            }
            else
            {
                entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
                entity.Query.Load();
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var aq = (AppraisalScoresheet)entity;

            txtScoresheetID.Value = aq.ScoresheetID ?? 0;
            txtReferenceID.Value = aq.ReferenceID ?? txtReferenceID.Value;
            ViewState["pid"] = aq.ParticipantItemID ?? Request.QueryString["pid"].ToInt();
            ViewState["eid"] = aq.EvaluatorID ?? Request.QueryString["eid"].ToInt();

            var api = new AppraisalParticipantItem();
            api.Query.Where(api.Query.ParticipantItemID == ViewState["pid"].ToInt());
            api.Query.Load();

            var ap = new AppraisalParticipant();
            ap.LoadByPrimaryKey(api.ParticipantID ?? 0);

            txtScoringDate.SelectedDate = aq.ScoringDate ?? DateTime.Now.Date;

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == ViewState["eid"].ToInt());
            emp.Query.Load();

            var ape = new AppraisalParticipantEvaluator();
            ape.Query.Where(ape.Query.ParticipantItemID == api.ParticipantItemID, ape.Query.EvaluatorID == ViewState["eid"].ToInt());
            ape.Query.Load();

            var asri = new AppStandardReferenceItem();
            asri.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), ape.SREvaluatorType);

            txtEvaluatorTypeName.Text = string.Format("{0} - {1}", asri.ItemID, asri.ItemName);
            txtEvaluatorName.Text = emp.EmployeeName;
            txtEvaluatorEmployeeNo.Text = emp.EmployeeNumber;

            string organizationUnit = string.Empty;
            var ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(ape.OrganizationUnitID.ToInt()))
                organizationUnit = ou.OrganizationUnitName;

            ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(ape.ServiceUnitID.ToInt()))
            {
                if (organizationUnit == string.Empty)
                    organizationUnit = ou.OrganizationUnitName;
                else
                    organizationUnit += " - " + ou.OrganizationUnitName;
            }
            txtEvaluatorOrganizationUnitName.Text = organizationUnit;

            var pos = new Position();
            if (pos.LoadByPrimaryKey(ape.PositionID.ToInt()))
                txtEvaluatorPositionName.Text = pos.PositionName;
            else txtEvaluatorPositionName.Text = string.Empty;

            emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == (api.EmployeeID ?? 0));
            emp.Query.Load();

            txtEmployeeName.Text = emp.EmployeeName;
            txtEmployeeNo.Text = emp.EmployeeNumber;

            organizationUnit = string.Empty;
            ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(api.OrganizationUnitID.ToInt()))
                organizationUnit = ou.OrganizationUnitName;

            ou = new OrganizationUnit();
            if (ou.LoadByPrimaryKey(api.ServiceUnitID.ToInt()))
            {
                if (organizationUnit == string.Empty)
                    organizationUnit = ou.OrganizationUnitName;
                else
                    organizationUnit += " - " + ou.OrganizationUnitName;
            }
            txtOrganizationUnitName.Text = organizationUnit;

            pos = new Position();
            if (pos.LoadByPrimaryKey(api.PositionID.ToInt()))
                txtPositionName.Text = pos.PositionName;
            else txtPositionName.Text = string.Empty;

            txtPeriodYear.Text = ap.PeriodYear;

            ViewState["IsApproved"] = aq.IsApproved ?? false;
            ViewState["IsVoid"] = aq.IsVoid ?? false;

            if (IsPostBack)
            {
                RefreshGrid();
            }
        }

        private void SetEntityValue(AppraisalScoresheet entity)
        {
            entity.ReferenceID = txtReferenceID.Value.ToInt();
            entity.ParticipantItemID = ViewState["pid"].ToInt();
            entity.EvaluatorID = ViewState["eid"].ToInt();
            entity.PeriodYear = txtPeriodYear.Text;
            entity.ScoringDate = txtScoringDate.SelectedDate;
            var str = txtEvaluatorTypeName.Text.Substring(0, 3);
            entity.SREvaluatorType = str;

            entity.IsApproved = Convert.ToBoolean(ViewState["IsApproved"]);
            entity.IsVoid = Convert.ToBoolean(ViewState["IsVoid"]);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppraisalScoresheet entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                txtScoresheetID.Value = entity.ScoresheetID;

                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    string questionerItemId = dataItem.GetDataKeyValue("QuestionerItemID").ToString();
                    string questionerId = dataItem.GetDataKeyValue("QuestionerID").ToString();

                    var item = new AppraisalScoresheetItem();
                    item.Query.Where(item.Query.ScoresheetID == txtScoresheetID.Value.ToInt(), item.Query.QuestionerItemID == questionerItemId);
                    if (!item.Query.Load())
                        item = new AppraisalScoresheetItem();

                    item.ScoresheetID = txtScoresheetID.Value.ToInt();
                    item.QuestionerItemID = questionerItemId.ToInt();
                    item.MarkValue = string.Empty;

                    string notes = ((RadTextBox)dataItem.FindControl("txtInterventionNotes")).Text;
                    double score = ((RadNumericTextBox)dataItem.FindControl("txtInterventionScore")).Value ?? 0;

                    item.Notes = notes;
                    item.Score = Convert.ToDecimal(score);

                    var appraisalQuestionItem = new AppraisalQuestionItem();
                    if (appraisalQuestionItem.LoadByPrimaryKey(questionerItemId.ToInt()))
                    {
                        if (appraisalQuestionItem.Benchmark > 0)
                        {
                            item.RealizationValue = (item.Score / appraisalQuestionItem.Benchmark ?? 0) * 100;

                            var rating = new AppraisalQuestionRating();
                            var ratingq = new AppraisalQuestionRatingQuery();
                            ratingq.Where(ratingq.QuestionerID == questionerId, ratingq.RatingValueMin <= item.RealizationValue, ratingq.RatingValueMax >= item.RealizationValue);
                            rating.Load(ratingq);
                            if (rating != null)
                            {
                                item.RatingValue = rating.RatingValue;
                                item.TotalScore = appraisalQuestionItem.Rating * rating.RatingValue;
                            }
                            else
                            {
                                item.RatingValue = 0;
                                item.TotalScore = item.Score;
                            }
                        }
                        else
                        {
                            item.RealizationValue = 0;
                            item.RatingValue = 0;
                            item.TotalScore = item.Score * appraisalQuestionItem.Rating;
                        }
                    }
                    else
                    {
                        item.RealizationValue = 0;
                        item.RatingValue = 0;
                        item.TotalScore = item.Score;
                    }

                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;

                    item.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppraisalScoresheetQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ReferenceID.IsNotNull(), que.ScoresheetID > txtScoresheetID.Value.ToInt());
                que.OrderBy(que.ScoresheetID.Ascending);
            }
            else
            {
                que.Where(que.ReferenceID.IsNotNull(), que.ScoresheetID < txtScoresheetID.Value.ToInt());
                que.OrderBy(que.ScoresheetID.Descending);
            }
            var entity = new AppraisalScoresheet();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (!entity.Query.Load())
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = true;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDateTime = DateTime.Now;

            entity.Save();
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (!entity.Query.Load())
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = false;
            entity.str.ApprovedByUserID = string.Empty;
            entity.str.ApprovedDateTime = string.Empty;
            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (!entity.Query.Load())
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = true;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = DateTime.Now;

            entity.Save();
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == txtScoresheetID.Value.ToInt());
            if (!entity.Query.Load())
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            entity.IsVoid = false;
            entity.str.VoidByUserID = string.Empty;
            entity.str.VoidDateTime = string.Empty;

            entity.Save();
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtScoresheetID.Value.ToInt() > 0;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        #region AppraisalQuestionerItem

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalScoresheetItems;
        }

        private DataTable AppraisalScoresheetItems
        {
            get
            {
                object obj = this.Session["AppraisalScoresheetItemInterventions" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var id = txtScoresheetID.Value.ToString();
                var rid = txtReferenceID.Value.ToString();

                AppraisalScoresheetItemCollection coll = new AppraisalScoresheetItemCollection();
                DataTable dtb = coll.GetReferenceJoin(id, rid);

                Session["AppraisalScoresheetItemInterventions" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private void RefreshGrid()
        {
            Session["AppraisalScoresheetItemInterventions" + Request.UserHostName] = null;
            grdList.Rebind();
        }

        private string _questionName = string.Empty;
        private string _questionerId = string.Empty;
        protected void grdList_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                _questionName = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["QuestionName"]);
                _questionerId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["QuestionerID"]);
            }

            if (e.Item is GridNestedViewItem)
            {
                // Populate
                var grd2 = (RadGrid)e.Item.FindControl("grdQuestionerList");
                grd2.DataSource = AppraisalQuestionItems(_questionName);
                grd2.Rebind();

                var grd3 = (RadGrid)e.Item.FindControl("grdRatingList");
                grd3.DataSource = AppraisalQuestionRatings(_questionerId);
                grd3.Rebind();

                _questionName = string.Empty;
                _questionerId = string.Empty;
            }
        }

        private DataTable AppraisalQuestionItems(string questionGroupName)
        {
            var query = new AppraisalQuestionItemQuery("a");
            query.Select(query.QuestionerItemID, query.QuestionCode, query.QuestionName, query.Notes);
            query.Where(query.QuestionGroupName == questionGroupName);
            query.OrderBy(query.QuestionCode.Ascending);

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private DataTable AppraisalQuestionRatings(string questionerId)
        {
            var query = new AppraisalQuestionRatingQuery("a");
            query.Select(query.RatingID, query.RatingCode, query.RatingName, query.RatingValue);
            query.Where(query.QuestionerID == questionerId.ToInt());
            query.OrderBy(query.RatingCode.Ascending);

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        #endregion
    }
}