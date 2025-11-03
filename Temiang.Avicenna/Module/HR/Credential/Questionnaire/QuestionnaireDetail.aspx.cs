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

namespace Temiang.Avicenna.Module.HR.Credential.Questionnaire
{
    public partial class QuestionnaireDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CredentialQuestionnaire;

            UrlPageSearch = "QuestionnaireSearch.aspx";
            UrlPageList = "QuestionnaireList.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRProfessionGroup);
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRClinicalWorkArea);
            ajax.AddAjaxSetting(cboSRProfessionGroup, cboSRClinicalAuthorityLevel);

            ajax.AddAjaxSetting(cboSRClinicalWorkArea, cboSRClinicalWorkArea);
            ajax.AddAjaxSetting(cboSRClinicalWorkArea, cboSRClinicalAuthorityLevel);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CredentialQuestionnaire());

            ViewState["id"] = 0;
            cboSRProfessionGroup.SelectedValue = string.Empty;
            cboSRProfessionGroup.Text = string.Empty;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new CredentialQuestionnaire();
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
            if (string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
            {
                args.MessageText = "Invalid Profession Group";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
            {
                args.MessageText = "Invalid Work Area";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboSRClinicalAuthorityLevel.SelectedValue))
            {
                args.MessageText = "Invalid Clinical Authority Level / Qualification";
                args.IsCancel = true;
                return;
            }
            var entity = new CredentialQuestionnaire();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRProfessionGroup.SelectedValue))
            {
                args.MessageText = "Invalid Profession Group";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
            {
                args.MessageText = "Invalid Work Area";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboSRClinicalAuthorityLevel.SelectedValue))
            {
                args.MessageText = "Invalid Clinical Authority Level / Qualification";
                args.IsCancel = true;
                return;
            }

            var entity = new CredentialQuestionnaire();
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
            auditLogFilter.TableName = "CredentialQuestionnaire";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CredentialQuestionnaire();
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
            var aq = (CredentialQuestionnaire)entity;
            if (aq != null && aq.QuestionnaireID != null) ViewState["id"] = aq.QuestionnaireID.ToString();
            else ViewState["id"] = 0;
            txtQuestionnaireCode.Text = aq.QuestionnaireCode;
            txtQuestionnaireName.Text = aq.QuestionnaireName;
            cboSRProfessionGroup.SelectedValue = aq.SRProfessionGroup;
            if (!string.IsNullOrEmpty(aq.SRClinicalWorkArea))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                        query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString(),
                        query.ItemID == aq.SRClinicalWorkArea
                    );
                cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
                cboSRClinicalWorkArea.DataBind();
                cboSRClinicalWorkArea.SelectedValue = aq.SRClinicalWorkArea;
            }
            else
            {
                cboSRClinicalWorkArea.Items.Clear();
                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(aq.SRClinicalAuthorityLevel))
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                        query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString(),
                        query.ItemID == aq.SRClinicalAuthorityLevel
                    );
                cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
                cboSRClinicalAuthorityLevel.DataBind();
                cboSRClinicalAuthorityLevel.SelectedValue = aq.SRClinicalAuthorityLevel;
            }
            else
            {
                cboSRClinicalAuthorityLevel.Items.Clear();
                cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
                cboSRClinicalAuthorityLevel.Text = string.Empty;
            }
            txtInfo1.Text = aq.Info1;
            txtInfo2.Text = aq.Info2;
            txtInfo3.Text = aq.Info3;
            txtInfo4.Text = aq.Info4;
            chkIsActive.Checked = aq.IsActive ?? false;

            PopulateItemGrid();
        }

        private void SetEntityValue(CredentialQuestionnaire entity)
        {
            entity.QuestionnaireCode = txtQuestionnaireCode.Text;
            entity.QuestionnaireName = txtQuestionnaireName.Text;
            entity.SRProfessionGroup = cboSRProfessionGroup.SelectedValue;
            entity.SRClinicalWorkArea = cboSRClinicalWorkArea.SelectedValue;
            entity.SRClinicalAuthorityLevel = cboSRClinicalAuthorityLevel.SelectedValue;
            entity.Info1 = txtInfo1.Text;
            entity.Info2 = txtInfo2.Text;
            entity.Info3 = txtInfo3.Text;
            entity.Info4 = txtInfo4.Text;
            entity.IsActive = chkIsActive.Checked;

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

        private void SaveEntity(CredentialQuestionnaire entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ViewState["id"] = entity.QuestionnaireID;

                foreach (var item in CredentialQuestionnaireItems)
                {
                    item.QuestionnaireID = ViewState["id"].ToInt();
                    item.LastUpdateDateTime = DateTime.Now;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                CredentialQuestionnaireItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CredentialQuestionnaireQuery();
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
            var entity = new CredentialQuestionnaire();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function CredentialQuestionnaireItem
        private CredentialQuestionnaireItemCollection CredentialQuestionnaireItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collCredentialQuestionnaireItem" + Request.UserHostName];
                    if (obj != null) return ((CredentialQuestionnaireItemCollection)(obj));
                }

                var coll = new CredentialQuestionnaireItemCollection();

                var query = new CredentialQuestionnaireItemQuery("a");
                var level = new AppStandardReferenceItemQuery("b");
                var type = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.CredentialQuestionLevel && level.ItemID == query.SRCredentialQuestionLevel);
                query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.CredentialActionType && type.ItemID == query.SRCredentialActionType);

                query.Select(query, level.ItemName.As("refToAppStdRefItem_CredentialQuestionLevel"), type.ItemName.As("refToAppStdRefItem_CredentialActionType"));
                query.Where(query.QuestionnaireID == ViewState["id"].ToInt());
                query.OrderBy(query.QuestionCode.Ascending);

                coll.Load(query);

                Session["collCredentialQuestionnaireItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collCredentialQuestionnaireItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
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

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CredentialQuestionnaireItems = null; //Reset Record Detail
            grdList.DataSource = CredentialQuestionnaireItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CredentialQuestionnaireItems;
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID]);
            var entity = FindItem(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID]);
            var entity = FindItem(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = CredentialQuestionnaireItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdList.Rebind();
        }

        private CredentialQuestionnaireItem FindItem(String id)
        {
            return CredentialQuestionnaireItems.FirstOrDefault(rec => rec.QuestionnaireItemID.Equals(id.ToInt()));
        }

        private void SetEntityValue(CredentialQuestionnaireItem entity, GridCommandEventArgs e)
        {
            var userControl = (QuestionnaireItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.QuestionnaireItemID = userControl.QuestionnaireItemID;
                entity.QuestionCode = userControl.QuestionCode;
                entity.QuestionNo = userControl.QuestionNo;
                entity.QuestionName = userControl.QuestionName;
                entity.SRCredentialQuestionLevel = userControl.SRCredentialQuestionLevel;
                entity.CredentialQuestionLevelName = userControl.CredentialQuestionLevelName;
                entity.SRCredentialActionType = userControl.SRCredentialActionType;
                entity.CredentialActionTypeName = userControl.CredentialActionTypeName;
                entity.IsDetail = userControl.IsDetail;
            }
        }
        #endregion

        #region Combobox
        protected void cboSRProfessionGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalWorkArea.Items.Clear();
            cboSRClinicalWorkArea.SelectedValue = string.Empty;
            cboSRClinicalWorkArea.Text = string.Empty;

            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalWorkArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.Where(query.ReferenceID == cboSRProfessionGroup.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
            cboSRClinicalWorkArea.DataBind();
        }

        protected void cboSRClinicalWorkArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.Where(query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
            cboSRClinicalAuthorityLevel.DataBind();
        }
        #endregion
    }
}