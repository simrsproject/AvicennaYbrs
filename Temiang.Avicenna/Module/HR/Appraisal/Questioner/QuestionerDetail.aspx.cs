using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class QuestionerDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalQuestioner;

            UrlPageSearch = "QuestionerSearch.aspx";
            UrlPageList = "QuestionerList.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                trScoringRecapitulation.Visible = false;
                trAppraisalType.Visible = false;

                grdList.Columns.FindByUniqueName("Target").Visible = AppSession.Parameter.AppraisalVersionNo == "3";
                grdList.Columns.FindByUniqueName("Achievements").Visible = AppSession.Parameter.AppraisalVersionNo == "3";
                grdList.Columns.FindByUniqueName("Rating").Visible = AppSession.Parameter.AppraisalVersionNo == "2";
                grdList.Columns.FindByUniqueName("Benchmark").Visible = AppSession.Parameter.AppraisalVersionNo == "2";

                grdRating.Columns.FindByUniqueName("RatingValue").Visible = AppSession.Parameter.AppraisalVersionNo == "2";

                if (AppSession.Parameter.AppraisalVersionNo == "3")
                {
                    grdRating.Columns.FindByUniqueName("RatingValueMin").HeaderText = "Min Value";
                    grdRating.Columns.FindByUniqueName("RatingValueMax").HeaderText = "Max Value";
                }

                StandardReference.InitializeIncludeSpace(cboSRAppraisalType, AppEnum.StandardReference.AppraisalType);
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppraisalQuestion());

            ViewState["id"] = 0;
            txtLoad.Value = 0;
            chkIsScoringRecapitulation.Checked = true;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppraisalQuestion();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalQuestion();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppraisalQuestion();
            if (entity.LoadByPrimaryKey(ViewState["id"].ToInt()))
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
            auditLogFilter.PrimaryKeyData = string.Format("QuestionerID='{0}'", ViewState["id"].ToInt());
            auditLogFilter.TableName = "AppraisalQuestion";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItemAppraisalQuestionItem(newVal);
            RefreshCommandItemAppraisalQuestionRating(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppraisalQuestion();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(id.ToInt());
            }
            else
            {
                entity.LoadByPrimaryKey(ViewState["id"].ToInt());
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var aq = (AppraisalQuestion)entity;
            if (aq != null && aq.QuestionerID != null) ViewState["id"] = aq.QuestionerID.ToString();
            else ViewState["id"] = 0;
            txtQuestionerNo.Text = aq.QuestionerNo;
            txtQuestionerName.Text = aq.QuestionerName;
            cboSRAppraisalType.SelectedValue = aq.SRAppraisalType;
            GetAppraisalTypeNote("A", false);
            txtPeriodYear.Text = aq.PeriodYear;
            txtLoad.Value = Convert.ToDouble(aq.LoadScore);
            txtNotes.Text = aq.Notes;
            chkIsScoringRecapitulation.Checked = aq.IsScoringRecapitulation ?? false;
            chkIsActive.Checked = aq.IsActive ?? false;

            PopulateAppraisalQuestionItemGrid();
            PopulateAppraisalQuestionRatingGrid();
        }

        private void SetEntityValue(AppraisalQuestion entity)
        {
            entity.QuestionerNo = txtQuestionerNo.Text;
            entity.QuestionerName = txtQuestionerName.Text;
            entity.SRAppraisalType = "A";
            entity.PeriodYear = txtPeriodYear.Text;
            entity.LoadScore = Convert.ToDecimal(txtLoad.Value);
            entity.Notes = txtNotes.Text;
            entity.IsScoringRecapitulation = chkIsScoringRecapitulation.Checked;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppraisalQuestion entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ViewState["id"] = entity.QuestionerID;

                foreach (var item in AppraisalQuestionItems)
                {
                    item.QuestionerID = ViewState["id"].ToInt();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var item in AppraisalQuestionRatings)
                {
                    item.QuestionerID = ViewState["id"].ToInt();
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                AppraisalQuestionItems.Save();
                AppraisalQuestionRatings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppraisalQuestionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.QuestionerID > ViewState["id"].ToInt());
                que.OrderBy(que.QuestionerID.Ascending);
            }
            else
            {
                que.Where(que.QuestionerID < ViewState["id"].ToInt());
                que.OrderBy(que.QuestionerID.Descending);
            }
            var entity = new AppraisalQuestion();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function AppraisalQuestionItem
        private AppraisalQuestionItemCollection AppraisalQuestionItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAppraisalQuestionItem" + Request.UserHostName];
                    if (obj != null) return ((AppraisalQuestionItemCollection)(obj));
                }

                var coll = new AppraisalQuestionItemCollection();

                var query = new AppraisalQuestionItemQuery("a");
                query.Select(query);
                query.Where(query.QuestionerID == ViewState["id"].ToInt());
                query.OrderBy(query.QuestionCode.Ascending);

                coll.Load(query);

                Session["collAppraisalQuestionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAppraisalQuestionItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemAppraisalQuestionItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdList.Columns[0].Visible = isVisible;
            grdList.Columns[grdList.Columns.Count - 1].Visible = isVisible;

            grdList.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdList.Rebind();
        }

        private void PopulateAppraisalQuestionItemGrid()
        {
            //Display Data Detail
            AppraisalQuestionItems = null; //Reset Record Detail
            grdList.DataSource = AppraisalQuestionItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppraisalQuestionItems;
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID]);
            var entity = FindAppraisalQuestionItem(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID]);
            var entity = FindAppraisalQuestionItem(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = AppraisalQuestionItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdList.Rebind();
        }

        private AppraisalQuestionItem FindAppraisalQuestionItem(String id)
        {
            return AppraisalQuestionItems.FirstOrDefault(rec => rec.QuestionerItemID.Equals(id.ToInt()));
        }

        private void SetEntityValue(AppraisalQuestionItem entity, GridCommandEventArgs e)
        {
            var userControl = (QuestionerItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.QuestionerItemID = userControl.QuestionerItemID;
                entity.QuestionCode = userControl.QuestionCode;
                entity.QuestionGroupName = userControl.QuestionGroupName;
                entity.QuestionName = userControl.QuestionName;
                entity.Notes = userControl.Notes;
                entity.Target = userControl.Target;
                entity.Achievements = userControl.Achievements;
                entity.Rating = userControl.Rating;
                entity.Benchmark = userControl.Benchmark;
                entity.MinValue = userControl.MinValue;
                entity.MaxValue = userControl.MaxValue;
            }
        }
        #endregion

        #region Record Detail Method Function AppraisalQuestionRating
        private AppraisalQuestionRatingCollection AppraisalQuestionRatings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAppraisalQuestionRating" + Request.UserHostName];
                    if (obj != null) return ((AppraisalQuestionRatingCollection)(obj));
                }

                var coll = new AppraisalQuestionRatingCollection();

                var query = new AppraisalQuestionRatingQuery("a");

                query.Select(query);
                query.Where(query.QuestionerID == ViewState["id"].ToInt());
                query.OrderBy(query.RatingCode.Ascending);

                coll.Load(query);

                Session["collAppraisalQuestionRating" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAppraisalQuestionRating" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemAppraisalQuestionRating(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRating.Columns[0].Visible = isVisible;
            grdRating.Columns[grdRating.Columns.Count - 1].Visible = isVisible;

            grdRating.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdRating.Rebind();
        }

        private void PopulateAppraisalQuestionRatingGrid()
        {
            //Display Data Detail
            AppraisalQuestionRatings = null; //Reset Record Detail
            grdRating.DataSource = AppraisalQuestionRatings; //Requery
            grdRating.MasterTableView.IsItemInserted = false;
            grdRating.MasterTableView.ClearEditItems();
            grdRating.DataBind();
        }

        protected void grdRating_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRating.DataSource = AppraisalQuestionRatings;
        }

        protected void grdRating_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppraisalQuestionRatingMetadata.ColumnNames.RatingID]);
            var entity = FindAppraisalQuestionRating(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdRating_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalQuestionRatingMetadata.ColumnNames.RatingID]);
            var entity = FindAppraisalQuestionRating(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdRating_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = AppraisalQuestionRatings.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdRating.Rebind();
        }

        private AppraisalQuestionRating FindAppraisalQuestionRating(String id)
        {
            return AppraisalQuestionRatings.FirstOrDefault(rec => rec.RatingID.Equals(id.ToInt()));
        }

        private void SetEntityValue(AppraisalQuestionRating entity, GridCommandEventArgs e)
        {
            var userControl = (QuestionRatingDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RatingCode = userControl.RatingCode;
                entity.RatingName = userControl.RatingName;
                entity.RatingValue = userControl.RatingValue;
                entity.RatingValueMin = userControl.RatingValueMin;
                entity.RatingValueMax = userControl.RatingValueMax;
            }
        }
        #endregion

        protected void cboSRAppraisalType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetAppraisalTypeNote(e.Value, true);
        }

        private void GetAppraisalTypeNote(string itemId, bool isNew)
        {
            var appraisaltype = new AppStandardReferenceItem();
            if (appraisaltype.LoadByPrimaryKey(AppEnum.StandardReference.AppraisalType.ToString(), itemId))
            {
                lblAppraisalTypeNote.Text = appraisaltype.CustomField;
            }
            else 
                lblAppraisalTypeNote.Text = string.Empty;
        }
    }
}