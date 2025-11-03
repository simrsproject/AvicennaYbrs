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

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class QuestionnaireDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalQuestionnaire;

            UrlPageSearch = "QuestionnaireSearch.aspx";
            UrlPageList = "QuestionnaireList.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ClinicalPerformanceAppraisalQuestionnaire());

            ViewState["id"] = 0;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaire();
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
            var entity = new ClinicalPerformanceAppraisalQuestionnaire();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaire();
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
            auditLogFilter.PrimaryKeyData = string.Format("QuestionnaireID='{0}'", ViewState["id"].ToInt());
            auditLogFilter.TableName = "ClinicalPerformanceAppraisalQuestionnaire";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItem(newVal);
            RefreshCommandItemConclusion(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaire();
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
            var aq = (ClinicalPerformanceAppraisalQuestionnaire)entity;
            if (aq != null && aq.QuestionnaireID != null) ViewState["id"] = aq.QuestionnaireID.ToString();
            else ViewState["id"] = 0;
            txtQuestionnaireCode.Text = aq.QuestionnaireCode;
            txtQuestionnaireName.Text = aq.QuestionnaireName;
            txtMinValue.Value = Convert.ToDouble(aq.MinValue);
            txtMaxValue.Value = Convert.ToDouble(aq.MaxValue);

            PopulateItemGrid();
            PopulateItemConclusionGrid();
        }

        private void SetEntityValue(ClinicalPerformanceAppraisalQuestionnaire entity)
        {
            entity.QuestionnaireCode = txtQuestionnaireCode.Text;
            entity.QuestionnaireName = txtQuestionnaireName.Text;
            entity.MinValue = Convert.ToInt16(txtMinValue.Value);
            entity.MaxValue = Convert.ToInt16(txtMaxValue.Value);

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ClinicalPerformanceAppraisalQuestionnaire entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ViewState["id"] = entity.QuestionnaireID;

                foreach (var item in ClinicalPerformanceAppraisalQuestionnaireItems)
                {
                    item.QuestionnaireID = ViewState["id"].ToInt();
                    item.LastUpdateDateTime = DateTime.Now;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                ClinicalPerformanceAppraisalQuestionnaireItems.Save();

                foreach (var item in ClinicalPerformanceAppraisalQuestionnaireConclusions)
                {
                    item.QuestionnaireID = ViewState["id"].ToInt();
                    item.LastUpdateDateTime = DateTime.Now;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                ClinicalPerformanceAppraisalQuestionnaireConclusions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ClinicalPerformanceAppraisalQuestionnaireQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.QuestionnaireID > ViewState["id"].ToInt());
                que.OrderBy(que.QuestionnaireID.Ascending);
            }
            else
            {
                que.Where(que.QuestionnaireID < ViewState["id"].ToInt());
                que.OrderBy(que.QuestionnaireID.Descending);
            }
            var entity = new ClinicalPerformanceAppraisalQuestionnaire();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function ClinicalPerformanceAppraisalQuestionnaireItem
        private ClinicalPerformanceAppraisalQuestionnaireItemCollection ClinicalPerformanceAppraisalQuestionnaireItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collClinicalPerformanceAppraisalQuestionnaireItem" + Request.UserHostName];
                    if (obj != null) return ((ClinicalPerformanceAppraisalQuestionnaireItemCollection)(obj));
                }

                var coll = new ClinicalPerformanceAppraisalQuestionnaireItemCollection();

                var query = new ClinicalPerformanceAppraisalQuestionnaireItemQuery("a");

                query.Select(query);
                query.Where(query.QuestionnaireID == ViewState["id"].ToInt());
                query.OrderBy(query.QuestionCode.Ascending);

                coll.Load(query);

                Session["collClinicalPerformanceAppraisalQuestionnaireItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collClinicalPerformanceAppraisalQuestionnaireItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdList.Columns[0].Visible = isVisible;
            grdList.Columns[grdList.Columns.Count - 1].Visible = isVisible;

            grdList.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdList.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            ClinicalPerformanceAppraisalQuestionnaireItems = null; //Reset Record Detail
            grdList.DataSource = ClinicalPerformanceAppraisalQuestionnaireItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ClinicalPerformanceAppraisalQuestionnaireItems;
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID]);
            var entity = FindItem(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID]);
            var entity = FindItem(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ClinicalPerformanceAppraisalQuestionnaireItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdList.Rebind();
        }

        private ClinicalPerformanceAppraisalQuestionnaireItem FindItem(String id)
        {
            return ClinicalPerformanceAppraisalQuestionnaireItems.FirstOrDefault(rec => rec.QuestionnaireItemID.Equals(id.ToInt()));
        }

        private void SetEntityValue(ClinicalPerformanceAppraisalQuestionnaireItem entity, GridCommandEventArgs e)
        {
            var userControl = (QuestionnaireItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.QuestionnaireItemID = userControl.QuestionnaireItemID;
                entity.QuestionCode = userControl.QuestionCode;
                entity.QuestionNo = userControl.QuestionNo;
                entity.QuestionName = userControl.QuestionName;
                entity.QuestionGroupName = userControl.QuestionGroupName;
                entity.IsDetail = userControl.IsDetail;
                entity.LoadScore = userControl.LoadScore;
            }
        }
        #endregion

        #region Record Detail Method Function ClinicalPerformanceAppraisalQuestionnaireConclusion
        private ClinicalPerformanceAppraisalQuestionnaireConclusionCollection ClinicalPerformanceAppraisalQuestionnaireConclusions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collClinicalPerformanceAppraisalQuestionnaireConclusion" + Request.UserHostName];
                    if (obj != null) return ((ClinicalPerformanceAppraisalQuestionnaireConclusionCollection)(obj));
                }

                var coll = new ClinicalPerformanceAppraisalQuestionnaireConclusionCollection();

                var query = new ClinicalPerformanceAppraisalQuestionnaireConclusionQuery("a");
                query.Select(query);
                query.Where(query.QuestionnaireID == ViewState["id"].ToInt());
                query.OrderBy(query.ConclusionGrade.Ascending);

                coll.Load(query);

                Session["collClinicalPerformanceAppraisalQuestionnaireConclusion" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collClinicalPerformanceAppraisalQuestionnaireConclusion" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemConclusion(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdConclusion.Columns[0].Visible = isVisible;
            grdConclusion.Columns[grdConclusion.Columns.Count - 1].Visible = isVisible;

            grdConclusion.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdConclusion.Rebind();
        }

        private void PopulateItemConclusionGrid()
        {
            //Display Data Detail
            ClinicalPerformanceAppraisalQuestionnaireConclusions = null; //Reset Record Detail
            grdConclusion.DataSource = ClinicalPerformanceAppraisalQuestionnaireConclusions; //Requery
            grdConclusion.MasterTableView.IsItemInserted = false;
            grdConclusion.MasterTableView.ClearEditItems();
            grdConclusion.DataBind();
        }

        protected void grdConclusion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdConclusion.DataSource = ClinicalPerformanceAppraisalQuestionnaireConclusions;
        }

        protected void grdConclusion_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID]);
            var entity = FindItemConclusion(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdConclusion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID]);
            var entity = FindItemConclusion(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdConclusion_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ClinicalPerformanceAppraisalQuestionnaireConclusions.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdConclusion.Rebind();
        }

        private ClinicalPerformanceAppraisalQuestionnaireConclusion FindItemConclusion(String id)
        {
            return ClinicalPerformanceAppraisalQuestionnaireConclusions.FirstOrDefault(rec => rec.ConclusionID.Equals(id.ToInt()));
        }

        private void SetEntityValue(ClinicalPerformanceAppraisalQuestionnaireConclusion entity, GridCommandEventArgs e)
        {
            var userControl = (QuestionnaireConclusionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ConclusionID = userControl.ConclusionID;
                entity.ConclusionGrade = userControl.ConclusionGrade;
                entity.ConclusionGradeName = userControl.ConclusionGradeName;
                entity.MinValue = userControl.MinValue;
                entity.MaxValue = userControl.MaxValue;
            }
        }
        #endregion
    }
}