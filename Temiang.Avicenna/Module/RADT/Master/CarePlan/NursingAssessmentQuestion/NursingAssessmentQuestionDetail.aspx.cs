using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingAssessmentQuestionDetail : BasePageDetail
    {
        private string GetEquivalentAssessmentID() {
            return (string.IsNullOrEmpty(cboEquivalentAssessmentID.SelectedValue) ? txtQuestionID.Text : cboEquivalentAssessmentID.SelectedValue);
        }

        private void SetEntityValue(esQuestion entity)
        {
            entity.NursingDisplayAs = txtQuestionText.Text;
            entity.EquivalentQuestionID = chkIsCopyTemplate.Checked ? string.Empty : cboEquivalentAssessmentID.SelectedValue;

            //Last Update Status
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            var existingDiagnosa = new NursingAssessmentDiagnosaCollection();
            existingDiagnosa.Query.Where(existingDiagnosa.Query.QuestionID == txtQuestionID.Text);
            existingDiagnosa.LoadAll();
            existingDiagnosa.MarkAllAsDeleted();
            existingDiagnosa.Save();

            // matrix NursingAssessment vs NursingDiagnosa
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            foreach (NursingAssessmentDiagnosa l in NursingAssessmentDiagnosas)
            {
                if (entity.es.IsAdded)
                {
                    l.QuestionID = GetEquivalentAssessmentID();
                }

                if (l.es.IsAdded)
                {
                    l.CreateByUserID = AppSession.UserLogin.UserID;
                    l.CreateDateTime = DateTime.Now;
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }
                else if (l.es.IsModified)
                {
                    l.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    l.LastUpdateDateTime = DateTime.Now;
                }

                //if (string.IsNullOrEmpty(entity.EquivalentQuestionID))
                //{
                //    l.QuestionID = txtQuestionID.Text;
                //}
            }

            if (chkIsCopyTemplate.Checked)
            {
                object obj = Session["collNADCollection"];
                if (obj == null)
                    return;

                DataTable dtbSelectedItem = (DataTable)obj;

                foreach (DataRow row in dtbSelectedItem.Rows)
                {
                    NursingAssessmentDiagnosa nad = new NursingAssessmentDiagnosa();
                    nad.AddNew();
                    nad.QuestionID = txtQuestionID.Text;
                    nad.NursingDiagnosaID = row["NursingDiagnosaID"].ToString();
                    nad.SRAnswerType = row["SRAnswerType"].ToString();
                    nad.Operand = row["Operand"].ToString();
                    nad.AcceptedText = row["AcceptedText"].ToString();
                    nad.AcceptedNum = row["AcceptedNum"].ToDecimal();
                    nad.CheckValue = row["CheckValue"].ToBoolean();
                    nad.CreateByUserID = AppSession.UserLogin.UserID;
                    nad.CreateDateTime = DateTime.Now;
                    nad.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    nad.LastUpdateDateTime = DateTime.Now;
                    nad.IsUsingRange = row["IsUsingRange"].ToBoolean();
                    nad.AcceptedNum2 = row["AcceptedNum2"].ToDecimal();
                    nad.AgeInMonthStart = row["AgeInMonthStart"].ToInt();
                    nad.AgeInMonthEnd = row["AgeInMonthStart"].ToInt();
                    nad.SRNsDiagnosaPrefix = row["SRNsDiagnosaPrefix"].ToString();
                    nad.SRNsDiagnosaSuffix = row["SRNsDiagnosaSuffix"].ToString();
                    nad.ShowAssessmetAsPrefix = row["ShowAssessmetAsPrefix"].ToBoolean();
                    nad.ShowAssessmetAsSuffix = row["ShowAssessmetAsSuffix"].ToBoolean();
                    nad.SRNsMandatoryLevel = row["SRNsMandatoryLevel"].ToString();
                    nad.Sex = row["Sex"].ToString();
                    nad.Save();
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new QuestionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.QuestionID > txtQuestionID.Text, que.NursingDisplayAs.Coalesce("''") != string.Empty);
                que.OrderBy(que.QuestionID.Ascending);
            }
            else
            {
                que.Where(que.QuestionID < txtQuestionID.Text, que.NursingDisplayAs.Coalesce("''") != string.Empty);
                que.OrderBy(que.QuestionID.Descending);
            }
            var entity = new Question();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Question();
            if (parameters.Length > 0)
            {
                var classID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(classID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtQuestionID.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var question = (Question)entity;
            txtQuestionID.Text = question.QuestionID;
            chkIsCopyTemplate.Checked = false;

            if (!string.IsNullOrEmpty(txtQuestionID.Text))
            {
                var arghhh = new RadComboBoxItemsRequestedEventArgs();
                arghhh.Text = question.QuestionText;
                cboQuestion_ItemsRequested(cboQuestion, arghhh);
                cboQuestion.SelectedValue = question.QuestionID;
                var args = new RadComboBoxSelectedIndexChangedEventArgs(
                    cboQuestion.Text, cboQuestion.Text, cboQuestion.SelectedValue, cboQuestion.SelectedValue);
                args.Value = cboQuestion.SelectedValue;
                cboQuestion_SelectedIndexChanged(cboQuestion, args);
            }
            else
            {
                cboQuestion.SelectedValue = "";
                cboQuestion.Text = "";
                txtAnswerDecimalDigit.Text = "";
                txtAnswerPrefix.Text = "";
                txtAnswerSuffix.Text = "";
                txtFormula.Text = "";
                txtQuestionText.Text = "";
                cboSRAnswerType.SelectedValue = "";
                cboSRAnswerType.Text = "";
                cboEquivalentAssessmentID.SelectedValue = "";
                cboEquivalentAssessmentID.Text = "";
            }

            PopulateGridNursingAssessmentDiagnosa();
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Question());
            chkIsActive.Checked = true;
            //chkIsCopyTemplate.Enabled = false;
            SetNursingAssessmentDiagnosas(null);
            grdNursingAssessmentDiagnosa.DataSource = null;
            grdNursingAssessmentDiagnosa.MasterTableView.ClearEditItems();
            grdNursingAssessmentDiagnosa.Rebind();

            gridQuestionForm.DataSource = null;
            gridQuestionForm.MasterTableView.ClearEditItems();
            gridQuestionForm.Rebind();
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
            auditLogFilter.PrimaryKeyData = string.Format("QuestionID='{0}'", txtQuestionID.Text.Trim());
            auditLogFilter.TableName = "NursingAssessmentQuestion";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //txtQuestionID.Enabled = (newVal == DataMode.New);

            // jika ada related lihat ke relatednya
            if (DataModeCurrent == AppEnum.DataMode.Edit)
            {
                if (!string.IsNullOrEmpty(cboQuestion.SelectedValue))
                {
                    DisableInputByQuestion(true);
                }
            }else if(DataModeCurrent == AppEnum.DataMode.New){
                DisableInputByQuestion(false);
            }

            RefreshCommandItemGridNursingDiagnosa(oldVal, newVal);
            gridQuestionForm.Rebind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "NursingAssessmentQuestionSearch.aspx";
            UrlPageList = "NursingAssessmentQuestionList.aspx";

            ProgramID = AppConstant.Program.NursingAssessmentQuestion;

            StandardReference.InitializeIncludeSpace(cboSRAnswerType, AppEnum.StandardReference.AnswerType);

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            var nadColl = NursingAssessmentDiagnosas.Where(x => x.QuestionID == txtQuestionID.Text);
            foreach (var nad in nadColl) {
                nad.MarkAsDeleted();
            }

            var entity = new Question();
            entity.LoadByPrimaryKey(txtQuestionID.Text);
            entity.NursingDisplayAs = string.Empty;
            SaveEntity(entity);

            grdNursingAssessmentDiagnosa.Rebind();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            OnMenuSaveEditClick(args);
        }

        private void SaveEntity(Question entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
                NursingAssessmentDiagnosas.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Question();
            if (entity.LoadByPrimaryKey(txtQuestionID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
        private IQueryable<Question> LoadQuestionToCombo(string filterQuestionText)
        {
            var na1 = new Question();
            if (na1.LoadByPrimaryKey(txtQuestionID.Text))
            {

            }

            var nd = new QuestionCollection();
            var query = new QuestionQuery("a");
            var qig = new QuestionInGroupQuery("b");
            var qgif = new QuestionGroupInFormQuery("c");
            var qf = new QuestionFormQuery("d");

            query.InnerJoin(qig).On(query.QuestionID == qig.QuestionID);
            query.InnerJoin(qgif).On(qig.QuestionGroupID == qgif.QuestionGroupID);
            query.InnerJoin(qf).On(qgif.QuestionFormID == qf.QuestionFormID);

            string searchTextContain = string.Format("%{0}%", filterQuestionText);

            query.Where(
                qf.IsAskepForm == true,
                query.SRAnswerType != "LBL",
                //query.ReferenceQuestionID.Coalesce("''") == string.Empty,
                query.Or(query.QuestionText.Like(searchTextContain),
                query.QuestionID.Like(searchTextContain))
            );

            query.OrderBy(string.Format("<CHARINDEX('{0}', a.QuestionText)>", filterQuestionText), query.QuestionText.Ascending);
            query.Select(query, string.Format("<CHARINDEX('{0}', a.QuestionText)>", filterQuestionText));

            query.es.Distinct = true;
            query.es.Top = 30;

            nd.Load(query);

            nd.AddNew();

            return nd.OrderBy(x => x.QuestionText).AsQueryable<Question>();
        }

        protected void cboQuestion_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboQuestion.DataSource = LoadQuestionToCombo(e.Text);
            cboQuestion.DataBind();
        }

        protected void cboQuestion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                DisableInputByQuestion(false);
            }
            else
            {
                var q = new Question();
                //TODO: Apakah efektif?
                var qId = Request.QueryString["of"] == "phr" ? Request.QueryString["id"] : e.Value;
                if (q.LoadByPrimaryKey(qId))
                {
                    txtQuestionID.Text = q.QuestionID;
                    txtQuestionText.Text = string.IsNullOrEmpty(q.NursingDisplayAs) ? q.QuestionText : q.NursingDisplayAs;
                    cboQuestion.Text = string.IsNullOrEmpty(q.NursingDisplayAs) ? q.QuestionText : q.NursingDisplayAs;
                    cboSRAnswerType.SelectedValue = q.SRAnswerType;
                    txtAnswerDecimalDigit.Value = q.AnswerDecimalDigit;
                    txtAnswerPrefix.Text = q.AnswerPrefix;
                    txtAnswerSuffix.Text = q.AnswerSuffix;
                    txtFormula.Text = q.Formula;
                    chkIsActive.Checked = q.IsActive ?? false;
                    chkIsMandatory.Checked = q.IsMandatory ?? false;
                    if (!string.IsNullOrEmpty(q.EquivalentQuestionID))
                    {
                        var args = new RadComboBoxItemsRequestedEventArgs();
                        args.Text = q.EquivalentQuestionID;
                        cboEquivalentAssessmentID_ItemsRequested(cboEquivalentAssessmentID, args);
                        cboEquivalentAssessmentID.SelectedValue = q.EquivalentQuestionID;
                    }
                    else
                    {
                        cboEquivalentAssessmentID.SelectedValue = "";
                        cboEquivalentAssessmentID.Text = "";
                    }

                    // disabled input
                    DisableInputByQuestion(true);
                }
                else
                {
                    DisableInputByQuestion(false);
                }
            }
            //gridQuestionForm.DataSource = null;
            //gridQuestionForm.DataSource = QuestionFormRelated();
            //gridQuestionForm.DataBind();
            gridQuestionForm.Rebind();

            //NursingAssessmentDiagnosas = null;
            SetNursingAssessmentDiagnosas(null);
            //grdNursingAssessmentDiagnosa.DataSource = null;
            //grdNursingAssessmentDiagnosa.DataSource = GetNursingAssessmentDiagnosas(); //NursingAssessmentDiagnosas;
            //grdNursingAssessmentDiagnosa.DataBind();
            grdNursingAssessmentDiagnosa.Rebind();
        }

        protected void cboQuestion_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Question)e.Item.DataItem).QuestionText;
            e.Item.Value = ((Question)e.Item.DataItem).QuestionID;
        }

        private IQueryable<Question> LoadEquivalentAssessmentToCombo(string Filter)
        {
            var qColl = new QuestionCollection();
            if (string.IsNullOrEmpty(txtQuestionID.Text) && string.IsNullOrEmpty(cboQuestion.SelectedValue)) {
                var n = qColl.AddNew();
                return qColl.AsQueryable<Question>();
            }
            string[] txtType = { "TXT", "MEM" };

            string searchTextContain = string.Format("%{0}%", Filter);

            var qs2 = new QuestionQuery("qs");
            var nad = new NursingAssessmentDiagnosaQuery("nadd");
            qs2.InnerJoin(nad).On(qs2.QuestionID == nad.QuestionID);

            qs2.Where(
                qs2.Or(qs2.QuestionID.Like(searchTextContain),
                qs2.QuestionText.Like(searchTextContain)));
            qs2.Where(
                qs2.EquivalentQuestionID.Coalesce("''") == string.Empty
                );



            if (!string.IsNullOrEmpty(txtQuestionID.Text))
            {
                var qs = new Question();
                if (qs.LoadByPrimaryKey(cboQuestion.SelectedValue))
                {
                    if (txtType.Contains(qs.SRAnswerType))
                    {
                        qs2.Where(qs2.SRAnswerType.In(txtType));
                    }
                    else
                    {
                        qs2.Where(qs2.SRAnswerType == qs.SRAnswerType);
                    }
                }
            }

            qColl.Load(qs2);
            //var n2 = qColl.AddNew();
            return qColl.GroupBy(o => o.QuestionText).Select(p => p.FirstOrDefault()).OrderBy(x => x.QuestionText).AsQueryable<Question>();
        }

        protected void cboEquivalentAssessmentID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboEquivalentAssessmentID.DataSource = LoadEquivalentAssessmentToCombo(e.Text);
            cboEquivalentAssessmentID.DataBind();
        }

        protected void cboEquivalentAssessmentID_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //NursingAssessmentDiagnosas = null;
            Session["collNursingAssessmentDiagnosaCollection"] = null;
            grdNursingAssessmentDiagnosa.Rebind();
        }

        protected void cboEquivalentAssessmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Question)e.Item.DataItem).QuestionText + " - " + ((Question)e.Item.DataItem).QuestionID;
            e.Item.Value = ((Question)e.Item.DataItem).QuestionID;
        }


        private void DisableInputByQuestion(bool val)
        {
            txtAnswerPrefix.Enabled = !val;
            txtAnswerSuffix.Enabled = !val;
            cboSRAnswerType.Enabled = !val;
            txtAnswerDecimalDigit.Enabled = !val;
            txtFormula.Enabled = !val;
            chkIsActive.Enabled = !val;
        }

        protected void gridQuestionForm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gridQuestionForm.DataSource = QuestionFormRelated();
        }

        private DataTable QuestionFormRelated() {
            var sqi = cboQuestion.SelectedValue;

            var query = new QuestionFormQuery("a");
            var qgif = new QuestionGroupInFormQuery("b");
            var qig = new QuestionInGroupQuery("c");
            var q = new QuestionQuery("d");

            query.InnerJoin(qgif).On(query.QuestionFormID == qgif.QuestionFormID)
                .InnerJoin(qig).On(qgif.QuestionGroupID == qig.QuestionGroupID)
                .InnerJoin(q).On(qig.QuestionID == q.QuestionID)
                .Where(query.IsAskepForm == true)
                .Where(query.Or(q.QuestionID == sqi, q.ReferenceQuestionID == sqi))
                .Select(query.QuestionFormID, query.QuestionFormName, qig.ParentQuestionID,
                    q.QuestionID,
                    q.QuestionText,
                    q.SRAnswerType,
                    q.QuestionText.As("RelatedQuestion"))
                .OrderBy(q.QuestionID.Ascending, query.QuestionFormID.Ascending);

            var dt = query.LoadDataTable();

            foreach (System.Data.DataRow r in dt.Rows)
            {
                r["RelatedQuestion"] = FindAllRelatedParentQuestion(
                    r["ParentQuestionID"].ToString(),
                    r["QuestionFormID"].ToString(),
                    r["RelatedQuestion"].ToString(), 0);
                if (r["RelatedQuestion"].ToString() == r["QuestionText"].ToString())
                {
                    // try to display parent question by level and 
                    r["RelatedQuestion"] = FindAllRelatedParentQuestionByLevel(
                        sqi, r["QuestionFormID"].ToString(), r["RelatedQuestion"].ToString(), 0);
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        private string FindAllRelatedParentQuestion(string ParentQuestionID, string FormID, string QuestionText,
            int loopCounter)
        {
            loopCounter++;
            // prevent too many loop
            if (loopCounter > 10) return QuestionText;

            var query = new QuestionFormQuery("a");
            var qgif = new QuestionGroupInFormQuery("b");
            var qig = new QuestionInGroupQuery("c");
            var qs = new QuestionQuery("d");

            query.InnerJoin(qgif).On(query.QuestionFormID == qgif.QuestionFormID)
                .InnerJoin(qig).On(qgif.QuestionGroupID == qig.QuestionGroupID)
                .InnerJoin(qs).On(qig.QuestionID == qs.QuestionID)
                .Where(
                    qig.QuestionID == ParentQuestionID,
                    query.QuestionFormID == FormID,
                    query.IsAskepForm == true)
                .Select(qig.QuestionID, qig.ParentQuestionID, qs.QuestionText);

            var dt = query.LoadDataTable();
            if (dt.Rows.Count == 0)
            {
                return QuestionText;
            }
            else {
                foreach(System.Data.DataRow r in dt.Rows){
                    QuestionText = (r["QuestionText"].ToString().Trim() + ": " + QuestionText.Trim())
                        .Replace("::"," :").Replace("  "," ");

                    QuestionText = FindAllRelatedParentQuestion(
                        r["ParentQuestionID"].ToString(), FormID, QuestionText, loopCounter);
                }
                return QuestionText;
            }
        }

        private string FindAllRelatedParentQuestionByLevel(string QuestionID, string FormID, string QuestionText,
           int loopCounter)
        {
            loopCounter++;
            // prevent too many loop
            if (loopCounter > 10) return QuestionText;

            var q = new QuestionQuery("q");
            var qig = new QuestionInGroupQuery("qig");
            var qgif = new QuestionGroupInFormQuery("qgif");
            var qigP = new QuestionInGroupQuery("qigP");
            var qP = new QuestionQuery("qP");

            q.InnerJoin(qig).On(q.QuestionID == qig.QuestionID)
                .InnerJoin(qgif).On(qig.QuestionGroupID == qgif.QuestionGroupID)
                .InnerJoin(qigP).On(qig.QuestionGroupID == qigP.QuestionGroupID &&
                    qigP.QuestionLevel.Coalesce("0") < qig.QuestionLevel.Coalesce("0") &&
                    qigP.RowIndex < qig.RowIndex)
                .InnerJoin(qP).On(qigP.QuestionID == qP.QuestionID)
                .Where(q.QuestionID == QuestionID, qgif.QuestionFormID == FormID)
                .Select(qP.QuestionID, qP.QuestionText)
                .OrderBy(qigP.RowIndex.Descending);
            q.es.Top = 1;

            var dt = q.LoadDataTable();
            if (dt.Rows.Count == 0)
            {
                return QuestionText;
            }
            else
            {
                foreach (System.Data.DataRow r in dt.Rows)
                {
                    QuestionText = (r["QuestionText"].ToString().Trim() + ": " + QuestionText.Trim())
                        .Replace("::", " :").Replace("  ", " ");

                    QuestionText = FindAllRelatedParentQuestionByLevel(
                        r["QuestionID"].ToString(), FormID, QuestionText, loopCounter);
                }
                return QuestionText;
            }
        }

        #region NursingAssessmentDiagnosa

        private void PopulateGridNursingAssessmentDiagnosa()
        {
            //Display Data Detail
            SetNursingAssessmentDiagnosas(null);
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            grdNursingAssessmentDiagnosa.DataSource = NursingAssessmentDiagnosas;
            grdNursingAssessmentDiagnosa.DataBind();
        }

        protected void grdNursingAssessmentDiagnosa_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var x = ((NursingAssessmentDiagnosa)item.DataItem).SRAnswerType.ToString();
                var x2 = cboSRAnswerType.SelectedValue;

                //CheckBox chkCorrected = item["Corrected"].Controls[0] as CheckBox;
                //if (chkCorrected.Checked)
                if (!x.Equals(x2) && !x2.Equals(string.Empty))
                {
                    item.ForeColor = System.Drawing.Color.Red;
                    item.Font.Bold = true;
                }
            }
        }
        protected void grdNursingAssessmentDiagnosa_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            grdNursingAssessmentDiagnosa.DataSource = NursingAssessmentDiagnosas;
        }

        protected void grdNursingAssessmentDiagnosa_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //GridEditableItem item = e.Item as GridEditableItem;
            //String NursingDiagnosaID =
            //    Convert.ToString(
            //        item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID]);
            //int ageInMonthStart = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart]);
            //int ageInMonthEnd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd]);
            //NursingAssessmentDiagnosa entity = FindItemGridNursingDiagnosa(
            //    NursingDiagnosaID, ageInMonthStart, ageInMonthEnd);
            //if (entity != null)
            //    SetEntityValueNursingDiagnosa(entity, e);

            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            NursingAssessmentDiagnosa entity = NursingAssessmentDiagnosas[e.Item.ItemIndex];
            if (entity != null)
                SetEntityValueNursingDiagnosa(entity, e);
        }

        protected void grdNursingAssessmentDiagnosa_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //GridDataItem item = e.Item as GridDataItem;
            //String NursingDiagnosaID =
            //    Convert.ToString(
            //        item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID]);
            //int ageInMonthStart = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart]);
            //int ageInMonthEnd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][
            //            NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd]);
            //NursingAssessmentDiagnosa entity = FindItemGridNursingDiagnosa(
            //    NursingDiagnosaID, ageInMonthStart, ageInMonthEnd);
            //if (entity != null)
            //    entity.MarkAsDeleted();
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            NursingAssessmentDiagnosa entity = NursingAssessmentDiagnosas[e.Item.ItemIndex];
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdNursingAssessmentDiagnosa_InsertCommand(object source, GridCommandEventArgs e)
        {
            var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
            NursingAssessmentDiagnosa entity = NursingAssessmentDiagnosas.AddNew();
            SetEntityValueNursingDiagnosa(entity, e);
        }

        //private NursingAssessmentDiagnosa FindItemGridNursingDiagnosa(string NursingDiagnosaID, 
        //    int AgeInMonthStart, int AgeInMonthEnd)
        //{
        //    //NursingAssessmentDiagnosaCollection coll = NursingAssessmentDiagnosas;
        //    //NursingAssessmentDiagnosa retval = null;
        //    //foreach (NursingAssessmentDiagnosa rec in coll)
        //    //{
        //    //    if (rec.NursingDiagnosaID.Equals(NursingDiagnosaID))
        //    //    {
        //    //        retval = rec;
        //    //        break;
        //    //    }
        //    //}
        //    //return retval;
        //    var NursingAssessmentDiagnosas = GetNursingAssessmentDiagnosas();
        //    return NursingAssessmentDiagnosas.Where(x => x.NursingDiagnosaID == NursingDiagnosaID &&
        //        x.AgeInMonthStart == AgeInMonthStart && x.AgeInMonthEnd == AgeInMonthEnd).FirstOrDefault();
        //}

        private void SetEntityValueNursingDiagnosa(NursingAssessmentDiagnosa entity, GridCommandEventArgs e)
        {
            NursingAssessmentDiagnosaDetail userControl =
                (NursingAssessmentDiagnosaDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.NursingDiagnosaID = userControl.NursingDiagnosaID;
                entity.NursingDiagnosaName = userControl.NursingDiagnosaName;
                entity.QuestionID = chkIsCopyTemplate.Checked ? txtQuestionID.Text : GetEquivalentAssessmentID(); //((RadTextBox)Helper.FindControlRecursive(Page, "txtQuestionID")).Text;
                entity.Operand = userControl.Operand;
                entity.AcceptedText = userControl.AcceptedText;
                entity.AcceptedNum = System.Convert.ToDecimal(userControl.AcceptedNum);
                entity.CheckValue = userControl.IsChecked;
                entity.SRAnswerType = cboSRAnswerType.SelectedValue;
                entity.IsUsingRange = userControl.IsUsingRange;
                entity.AcceptedNum2 = System.Convert.ToDecimal(userControl.AcceptedNum2);

                entity.AgeInMonthStart = userControl.AgeInMonthStart;
                entity.AgeInMonthEnd = userControl.AgeInMonthEnd;

                entity.Sex = userControl.Sex;

                entity.ShowAssessmetAsPrefix = userControl.ShowInPrefix;
                entity.ShowAssessmetAsSuffix = userControl.ShowInSuffix;

                entity.SRNsDiagnosaPrefix = userControl.SRDiagnosisPrefix;
                entity.SRNsDiagnosaSuffix = userControl.SRDiagnosisSuffix;

                entity.NsDiagnosaPrefixName = userControl.SRDiagnosisPrefixName;
                entity.NsDiagnosaSuffixName = userControl.SRDiagnosisSuffixName;

                entity.SRNsMandatoryLevel = userControl.SRNsMandatoryLevel;
                entity.NsMandatoryLevelName = userControl.SRNsMandatoryLevelName;

            }
        }

        //private NursingAssessmentDiagnosaCollection NursingAssessmentDiagnosas
        //{
        //    get
        //    {
        //        if (IsPostBack)
        //        {
        //            var obj = Session["collNursingAssessmentDiagnosaCollection"];
        //            if (obj != null)
        //                return ((NursingAssessmentDiagnosaCollection)(obj));
        //        }

        //        var mrg = new NursingAssessmentDiagnosaQuery("a");
        //        var clsq = new NursingDiagnosaQuery("b");

        //        mrg.Select(
        //            mrg,
        //            clsq.NursingDiagnosaName.As("refToNursingDiagnosa_NursingDiagnosaName")
        //            );
        //        mrg.InnerJoin(clsq).On(mrg.NursingDiagnosaID == clsq.NursingDiagnosaID);
        //        mrg.Where(
        //            mrg.QuestionID == GetEquivalentAssessmentID(),
        //            clsq.IsActive == true
        //            );

        //        var margins = new NursingAssessmentDiagnosaCollection();
        //        margins.Load(mrg);

        //        Session["collNursingAssessmentDiagnosaCollection"] = margins;
        //        return margins;
        //    }
        //    set { Session["collNursingAssessmentDiagnosaCollection"] = value; }
        //}

        private NursingAssessmentDiagnosaCollection GetNursingAssessmentDiagnosas()
        {
            if (IsPostBack)
            {
                var obj = Session["collNursingAssessmentDiagnosaCollection"];
                if (obj != null)
                    return ((NursingAssessmentDiagnosaCollection)(obj));
            }

            var questionId = chkIsCopyTemplate.Checked ? txtQuestionID.Text : GetEquivalentAssessmentID();

            var mrg = new NursingAssessmentDiagnosaQuery("a");
            var clsq = new NursingDiagnosaQuery("b");
            var stdPref = new AppStandardReferenceItemQuery("stdPref");
            var stdSuff = new AppStandardReferenceItemQuery("stdSuff");
            var stdAssLev = new AppStandardReferenceItemQuery("stdAssLevel");

            mrg.Select(
                mrg,
                clsq.NursingDiagnosaName.As("refToNursingDiagnosa_NursingDiagnosaName"),
                stdPref.ItemName.As("refToAppStdRef_NsDiagnosaPrefixName"),
                stdSuff.ItemName.As("refToAppStdRef_NsDiagnosaSuffixName"),
                stdAssLev.ItemName.As("refToAppStdRef_NsMandatoryLevelName")
                );
            mrg.InnerJoin(clsq).On(mrg.NursingDiagnosaID == clsq.NursingDiagnosaID)
                .LeftJoin(stdPref).On(stdPref.StandardReferenceID == AppEnum.StandardReference.NsDiagnosaPrefix &&
                    mrg.SRNsDiagnosaPrefix == stdPref.ItemID)
                .LeftJoin(stdSuff).On(stdSuff.StandardReferenceID == AppEnum.StandardReference.NsDiagnosaSuffix &&
                    mrg.SRNsDiagnosaSuffix== stdSuff.ItemID)
                .LeftJoin(stdAssLev).On(stdAssLev.StandardReferenceID == AppEnum.StandardReference.NsMandatoryLevel &&
                    mrg.SRNsMandatoryLevel == stdAssLev.ItemID)
                .Where(
                    mrg.QuestionID == questionId, //GetEquivalentAssessmentID(),
                    clsq.IsActive == true
                );

            var dtb = mrg.LoadDataTable();

            Session["collNADCollection"] = dtb;

            var margins = new NursingAssessmentDiagnosaCollection();
            margins.Load(mrg);

            Session["collNursingAssessmentDiagnosaCollection"] = margins;
            return margins;
        }

        private void SetNursingAssessmentDiagnosas(object val){
            Session["collNursingAssessmentDiagnosaCollection"] = val;
        }


        private void RefreshCommandItemGridNursingDiagnosa(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdNursingAssessmentDiagnosa.Columns[0].Visible = isVisible;
            grdNursingAssessmentDiagnosa.Columns[grdNursingAssessmentDiagnosa.Columns.Count - 1].Visible = isVisible;

            grdNursingAssessmentDiagnosa.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                //NursingAssessmentDiagnosas = null;
                SetNursingAssessmentDiagnosas(null);
            }

            //Perbaharui tampilan dan data
            grdNursingAssessmentDiagnosa.Rebind();
        }
        protected void chkIsCopyTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsCopyTemplate.Checked)
            {
                grdNursingAssessmentDiagnosa.Columns[0].Visible = false;
                grdNursingAssessmentDiagnosa.Columns[grdNursingAssessmentDiagnosa.Columns.Count - 1].Visible = false;
            }
        }
        #endregion

    }
}