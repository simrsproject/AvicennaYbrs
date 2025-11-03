using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormDetail : BasePageDetail
    {
        private void SetEntityValue(QuestionForm entity)
        {
            entity.QuestionFormID = txtQuestionFormID.Text;
            entity.QuestionFormName = txtQuestionFormName.Text;
            entity.RmNO = txtRMNo.Text;
            entity.SRAutoNumber = txtSRAutoNumber.Text;
            entity.IsSingleEntry = chkIsSingleEntry.Checked;
            entity.IsSharingEdit = chkIsSharingEdit.Checked;
            entity.IsUsingApproval = chkIsUsingApproval.Checked;
            entity.IsAskepForm = chkIsAskepForm.Checked;
            entity.IsModeMapping = chkIsModeMapping.Checked;

            var restrictionUserType = string.Empty;

            foreach (GridDataItem dataItem in grdUserType.MasterTableView.Items)
            {
                string itemId = dataItem.GetDataKeyValue("ItemID").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                if (isSelect)
                {
                    restrictionUserType += itemId + ";";
                }
            }

            entity.RestrictionUserType = restrictionUserType;

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new QuestionFormQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.IsActive == true, que.QuestionFormID > txtQuestionFormID.Text);
                que.OrderBy(que.QuestionFormID.Ascending);
            }
            else
            {
                que.Where(que.IsActive == true, que.QuestionFormID < txtQuestionFormID.Text);
                que.OrderBy(que.QuestionFormID.Descending);
            }
            var entity = new QuestionForm();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            QuestionForm entity = new QuestionForm();
            if (parameters.Length > 0)
            {
                String formId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(formId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtQuestionFormID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var questionForm = (QuestionForm)entity;
            txtQuestionFormID.Text = questionForm.QuestionFormID;
            txtQuestionFormName.Text = questionForm.QuestionFormName;
            txtRMNo.Text = questionForm.RmNO;
            txtSRAutoNumber.Text = questionForm.SRAutoNumber;
            chkIsSingleEntry.Checked = questionForm.IsSingleEntry ?? false;
            chkIsSharingEdit.Checked = questionForm.IsSharingEdit ?? false;
            chkIsUsingApproval.Checked = questionForm.IsUsingApproval ?? false;
            chkIsAskepForm.Checked = questionForm.IsAskepForm ?? false;
            chkIsModeMapping.Checked = questionForm.IsModeMapping ?? false;

            PopulateUserTypeGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new QuestionForm());
        }

        protected override void OnMenuEditClick()
        {
            PopulateUserTypeGrid();
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
            auditLogFilter.PrimaryKeyData = string.Format("QuestionFormID='{0}'", txtQuestionFormID.Text.Trim());
            auditLogFilter.TableName = "QuestionForm";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtQuestionFormID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandUserType(newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "QuestionFormSearch.aspx";
            UrlPageList = "QuestionFormList.aspx";

            ProgramID = AppConstant.Program.QuestionForm;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new QuestionForm();
            //entity.LoadByPrimaryKey(txtQuestionFormID.Text);
            //entity.MarkAsDeleted();
            //SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new QuestionForm();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(QuestionForm entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            QuestionForm entity = new QuestionForm();
            if (entity.LoadByPrimaryKey(txtQuestionFormID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region UserTypeGrid
#endregion

        private void PopulateUserTypeGrid()
        {
            //Display Data Detail
            grdUserType.DataSource = GetUserType();
            grdUserType.DataBind();
        }

        protected void grdUserType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUserType.DataSource = GetUserType();
        }

        private DataTable GetUserType()
        {
            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("IsSelect", typeof(bool)));
            dtb.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dtb.Columns.Add(new DataColumn("ItemName", typeof(string)));

            var qf = new QuestionForm();
            if (qf.LoadByPrimaryKey(txtQuestionFormID.Text))
            {
                if (!string.IsNullOrEmpty(qf.RestrictionUserType))
                {
                    string[] restrictionUserType = qf.RestrictionUserType.Split(';');
                    if (this.DataModeCurrent == AppEnum.DataMode.Read)
                    {
                        var query = new AppStandardReferenceItemQuery();
                        query.Where(query.StandardReferenceID == AppEnum.StandardReference.UserType.ToString(), query.ItemID.In(restrictionUserType));
                        query.OrderBy(query.ItemID.Ascending);
                        query.Select(@"<CAST(1 AS BIT) AS IsSelect>", query.ItemID, query.ItemName);

                        dtb = query.LoadDataTable();
                    }
                    else
                    {
                        int ttl = string.IsNullOrEmpty(qf.RestrictionUserType) ? 0 : qf.RestrictionUserType.Length;
                        int idx = 0;
                        while (idx < ttl)
                        {
                            string parseChar = qf.RestrictionUserType.Substring(idx, 3);
                            if (parseChar != ";")
                            {
                                var usrType = dtb.NewRow();
                                usrType["IsSelect"] = true;
                                usrType["ItemID"] = parseChar;
                                var std = new AppStandardReferenceItem();
                                if (std.LoadByPrimaryKey(AppEnum.StandardReference.UserType.ToString(), usrType["ItemID"].ToString()))
                                    usrType["ItemName"] = std.ItemName;
                                else
                                    usrType["ItemName"] = "";

                                dtb.Rows.Add(usrType);
                            }
                            idx += 4;
                        }

                        var query = new AppStandardReferenceItemQuery();
                        query.Where(query.StandardReferenceID == AppEnum.StandardReference.UserType.ToString(), query.ItemID.NotIn(restrictionUserType));
                        query.OrderBy(query.ItemID.Ascending);
                        query.Select(@"<CAST(0 AS BIT) AS IsSelect>", query.ItemID, query.ItemName);
                        var dtb2 = query.LoadDataTable();

                        dtb.Merge(dtb2);
                    }
                }
                else
                {
                    if (this.DataModeCurrent == AppEnum.DataMode.Read)
                    {
                        var usrType = dtb.NewRow();
                        usrType["IsSelect"] = false;
                        usrType["ItemID"] = string.Empty;
                        usrType["ItemName"] = "--- no record to display ---";

                        dtb.Rows.Add(usrType);
                    }
                    else
                    {
                        var query = new AppStandardReferenceItemQuery();
                        query.Where(query.StandardReferenceID == AppEnum.StandardReference.UserType.ToString());
                        query.OrderBy(query.ItemID.Ascending);
                        query.Select(@"<CAST(0 AS BIT) AS IsSelect>", query.ItemID, query.ItemName);
                        dtb = query.LoadDataTable();
                    }
                }
            }
            else
            {
                var usrType = dtb.NewRow();
                usrType["IsSelect"] = true;
                usrType["ItemID"] = string.Empty;
                usrType["ItemName"] = "--- no record to display ---";

                dtb.Rows.Add(usrType);
            }

            return dtb;
        }

        private void RefreshCommandUserType(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdUserType.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdUserType.Rebind();
        }
    }
}