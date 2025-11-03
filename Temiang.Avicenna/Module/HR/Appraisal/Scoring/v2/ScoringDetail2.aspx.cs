using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Appraisal.Scoring.v2
{
    public partial class ScoringDetail2 : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
            {
                case "sheet":
                    ProgramID = AppConstant.Program.AppraisalScoring;
                    break;
                case "eval":
                    ProgramID = AppConstant.Program.AppraisalEvaluation;
                    break;
            }

            UrlPageSearch = "##";
            UrlPageList = "ScoringList.aspx?type=" + Request.QueryString["type"];

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRQuarterPeriod, AppEnum.StandardReference.QuarterPeriod);
                var pid = Request.QueryString["pid"].ToInt();
                var eid = Request.QueryString["eid"].ToInt();

                var ap = new AppraisalParticipant();
                if (ap.LoadByPrimaryKey(Request.QueryString["pd"].ToInt()))
                    chkIsScoringRecapitulation.Checked = ap.IsScoringRecapitulation ?? false;

                if (!chkIsScoringRecapitulation.Checked)
                {
                    var apq = new AppraisalParticipantQuestioner();
                    var apqQ = new AppraisalParticipantQuestionerQuery();
                    apqQ.Where(apqQ.ParticipantItemID == pid);
                    apqQ.es.Top = 1;
                    apq.Load(apqQ);

                    hdnQuestionerID.Value = apq.QuestionerID.ToString();
                }
                else
                    hdnQuestionerID.Value = "-1";

                tabDetail.Tabs[1].Visible = false;

                //var ape = new AppraisalParticipantEvaluator();
                //ape.Query.Where(ape.Query.ParticipantItemID == pid, ape.Query.EvaluatorID == eid);
                //if (ape.Query.Load())
                //{
                //    tabDetail.Tabs[1].Visible = ape.SREvaluatorType == "001" && chkIsScoringRecapitulation.Checked;
                //}

                grdList.EnableGroupsExpandAll = false;
            }
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
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            auditLogFilter.PrimaryKeyData = string.Format("ScoresheetID='{0}'", ViewState["id"].ToInt());
            auditLogFilter.TableName = "AppraisalScoresheet";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Refresh Grid Detail
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdList.Columns.FindByUniqueName("Notes").Visible = false; //!isVisible;
            grdList.Columns.FindByUniqueName("txtNotes").Visible = false; //isVisible;
            if (chkIsScoringRecapitulation.Checked)
            {
                grdList.Columns.FindByUniqueName("ScoreX").Visible = !isVisible;
                grdList.Columns.FindByUniqueName("txtScore").Visible = isVisible;
                grdList.Columns.FindByUniqueName("TotalScoreX").Visible = false; //Request.QueryString["type"] != "sheet";

                grdList.Columns.FindByUniqueName("RatingX").Visible = false;
                grdList.Columns.FindByUniqueName("cboRatingID").Visible = false;
            }
            else
            {
                grdList.Columns.FindByUniqueName("ScoreX").Visible = false;
                grdList.Columns.FindByUniqueName("txtScore").Visible = false;
                grdList.Columns.FindByUniqueName("TotalScoreX").Visible = false;

                grdList.Columns.FindByUniqueName("RatingX").Visible = !isVisible;
                grdList.Columns.FindByUniqueName("cboRatingID").Visible = isVisible;
            }

            grdListNotes.Columns.FindByUniqueName("Capacity").Visible = !isVisible;
            grdListNotes.Columns.FindByUniqueName("NeedsToBeDeveloped").Visible = !isVisible;
            grdListNotes.Columns.FindByUniqueName("txtCapacity").Visible = isVisible;
            grdListNotes.Columns.FindByUniqueName("txtNeedsToBeDeveloped").Visible = isVisible;

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
                entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
                entity.Query.Load();
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var aq = (AppraisalScoresheet)entity;

            ViewState["id"] = aq.ScoresheetID ?? 0;
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

            txtParticipantName.Text = ap.ParticipantName;
            txtPeriodYear.Text = ap.PeriodYear;
            cboSRQuarterPeriod.SelectedValue = ap.SRQuarterPeriod;

            ViewState["IsApproved"] = aq.IsApproved ?? false;
            ViewState["IsVoid"] = aq.IsVoid ?? false;

            if (IsPostBack)
            {
                RefreshGrid();
            }
        }

        private void SetEntityValue(AppraisalScoresheet entity)
        {
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
                ViewState["id"] = entity.ScoresheetID;

                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    string questionerItemId = dataItem.GetDataKeyValue("QuestionerItemID").ToString();
                    string questionerId = dataItem.GetDataKeyValue("QuestionerID").ToString();

                    var item = new AppraisalScoresheetItem();
                    item.Query.Where(item.Query.ScoresheetID == ViewState["id"].ToInt(), item.Query.QuestionerItemID == questionerItemId);
                    if (!item.Query.Load())
                        item = new AppraisalScoresheetItem();

                    item.ScoresheetID = ViewState["id"].ToInt();
                    item.QuestionerItemID = questionerItemId.ToInt();
                    item.MarkValue = string.Empty;

                    string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;
                    double score = ((RadNumericTextBox)dataItem.FindControl("txtScore")).Value ?? 0;
                    string ratingid = ((RadComboBox)dataItem.FindControl("cboRatingID")).SelectedValue;

                    item.Notes = notes;
                    item.Score = Convert.ToDecimal(score);
                    item.RatingID = Convert.ToInt32(string.IsNullOrEmpty(ratingid) ? "-1" : ratingid);

                    if (AppSession.Parameter.AppraisalVersionNo == "3")
                    {
                        item.RealizationValue = 0;
                        item.RatingValue = 0;
                        item.TotalScore = item.Score;
                    }
                    else
                    {
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
                    }

                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;

                    item.Save();
                }

                if (tabDetail.Tabs[1].Visible)
                {
                    foreach (GridDataItem dataItem in grdListNotes.MasterTableView.Items)
                    {
                        string questionerId = dataItem.GetDataKeyValue("QuestionerID").ToString();

                        var recap = new AppraisalScoringRecapitulation();
                        recap.Query.Where(recap.Query.ParticipantItemID == ViewState["pid"].ToInt(), recap.Query.QuestionerID == questionerId);
                        if (!recap.Query.Load())
                            recap = new AppraisalScoringRecapitulation();

                        recap.ParticipantItemID = ViewState["pid"].ToInt();
                        recap.QuestionerID = questionerId.ToInt();

                        string capacity = ((RadTextBox)dataItem.FindControl("txtCapacity")).Text;
                        string developed = ((RadTextBox)dataItem.FindControl("txtNeedsToBeDeveloped")).Text;

                        recap.Capacity = capacity;
                        recap.NeedsToBeDeveloped = developed;

                        recap.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        recap.LastUpdateDateTime = DateTime.Now;

                        recap.Save();
                    }
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
                que.Where(que.ReferenceID.IsNull(), que.ScoresheetID > ViewState["id"].ToInt());
                que.OrderBy(que.ScoresheetID.Ascending);
            }
            else
            {
                que.Where(que.ReferenceID.IsNull(), que.ScoresheetID < ViewState["id"].ToInt());
                que.OrderBy(que.ScoresheetID.Descending);
            }
            var entity = new AppraisalScoresheet();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            var api = new AppraisalParticipantItem();
            api.Query.Where(api.Query.ParticipantItemID == entity.ParticipantItemID.ToInt(), api.Query.IsClosed == true);
            if (api.Query.Load())
            {
                args.MessageText = "Appraisal recapitulation has been done for this participant.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            var api = new AppraisalParticipantItem();
            api.Query.Where(api.Query.ParticipantItemID == entity.ParticipantItemID.ToInt(), api.Query.IsClosed == true);
            if (api.Query.Load())
            {
                args.MessageText = "Appraisal recapitulation has been done for this participant.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false);
        }

        private void SetApproval(AppraisalScoresheet entity, bool isApprove)
        {
            entity.IsApproved = isApprove;
            entity.ApprovedByUserID = isApprove ? AppSession.UserLogin.UserID : string.Empty;
            if (isApprove)
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
            else
                entity.str.ApprovedDateTime = string.Empty;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
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
            return ViewState["id"].ToInt() > 0;
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
                object obj = this.Session["AppraisalScoresheetItems" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var id = DataModeCurrent == AppEnum.DataMode.New ? "0" : ViewState["id"].ToString();
                var pid = Request.QueryString["pid"].ToString();
                var eid = Request.QueryString["eid"].ToString();

                AppraisalScoresheetItemCollection coll = new AppraisalScoresheetItemCollection();
                DataTable dtb = coll.GetJoin(id, pid, eid, AppSession.Parameter.AppraisalVersionNo);

                Session["AppraisalScoresheetItems" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["IsScoringEnabled"].Text == "False")
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

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var ratingId = dataItem["RatingID"].Text;
            if (!string.IsNullOrEmpty(ratingId) && ratingId != "&nbsp;" && ratingId != "-1")
            {
                var result = (dataItem["QuestionCode"].FindControl("cboRatingID") as RadComboBox);
                
                DataView dv = PopulateRating().DefaultView;
                dv.RowFilter = "RatingID = '" + ratingId + "'";

                result.DataSource = dv.ToTable();
                result.DataValueField = "RatingID";
                result.DataTextField = "RatingName";
                result.DataBind();
                ComboBox.SelectedValue(result, ratingId);
            }
        }

        private DataTable PopulateRating()
        {
            if (ViewState["rating"] != null)
                return ViewState["rating"] as DataTable;

            var query = new AppraisalQuestionRatingQuery("a");
            query.Where(
                query.QuestionerID == hdnQuestionerID.Value.ToInt());
            query.Select(query.RatingID, query.RatingCode, query.RatingName);

            ViewState["rating"] = query.LoadDataTable();
            return ViewState["rating"] as DataTable;
        }

        private void RefreshGrid()
        {
            Session["AppraisalScoresheetItems" + Request.UserHostName] = null;
            grdList.Rebind();

            Session["AppraisalScoringRecapitulations" + Request.UserHostName] = null;
            grdListNotes.Rebind();

            Session["AppraisalQuestionRatings" + Request.UserHostName] = null;
            grdGuidelines.Rebind();
        }

        #endregion

        #region AppraisalScoringRecapitulation
        protected void grdListNotes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListNotes.DataSource = AppraisalScoringRecapitulations;
        }

        private DataTable AppraisalScoringRecapitulations
        {
            get
            {
                object obj = this.Session["AppraisalScoringRecapitulations" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                AppraisalParticipantItemCollection coll = new AppraisalParticipantItemCollection();
                DataTable dtb = coll.GetScoringRecapitulations(Request.QueryString["pid"].ToString());

                Session["AppraisalScoringRecapitulations" + Request.UserHostName] = dtb;
                return dtb;
            }
        }
        #endregion

        #region AppraisalQuestionRating

        protected void grdGuidelines_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuidelines.DataSource = AppraisalQuestionRatings;
        }

        private DataTable AppraisalQuestionRatings
        {
            get
            {
                object obj = this.Session["AppraisalQuestionRatings" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var id = DataModeCurrent == AppEnum.DataMode.New ? "0" : ViewState["id"].ToString();
                var pid = Request.QueryString["pid"].ToString();
                var eid = Request.QueryString["eid"].ToString();

                AppraisalScoresheetItemCollection coll = new AppraisalScoresheetItemCollection();
                DataTable dtb = coll.GetQuestionRating(id, pid, eid);

                Session["AppraisalQuestionRatings" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        #endregion

        protected string GetQuestionName(object questionGroupName, object questionName)
        {
            if (questionGroupName.ToString().Equals(string.Empty))
                return questionName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + questionName.ToString();
        }

        protected void cboRatingID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RatingCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["RatingName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RatingID"].ToString();
        }

        protected void cboRatingID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppraisalQuestionRatingQuery("a");
            query.Where(
                query.QuestionerID == hdnQuestionerID.Value.ToInt());
            query.Select(query.RatingID, query.RatingCode, query.RatingName);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }
    }
}