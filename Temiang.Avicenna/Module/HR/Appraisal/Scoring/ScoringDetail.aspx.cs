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

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ScoringDetail : BasePageDetail
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

            UrlPageSearch = "ScoringSearch.aspx";
            UrlPageList = "ScoringList.aspx?type=" + Request.QueryString["type"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["pid"] != null) InitializedQuestion(ViewState["pid"].ToInt());
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

                SaveEntity(entity, coll);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            var coll = new AppraisalScoresheetItemCollection();

            SetEntityValue(entity, coll);
            SaveEntity(entity, coll);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppraisalScoresheet();
            entity.Query.Where(entity.Query.ScoresheetID == ViewState["id"].ToInt());
            if (entity.Query.Load())
            {
                var coll = new AppraisalScoresheetItemCollection();
                coll.Query.Where(coll.Query.ScoresheetID == entity.ScoresheetID);
                coll.Query.Load();

                SetEntityValue(entity, coll);
                SaveEntity(entity, coll);
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
            //TODO: Set status entry control
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

            txtEvaluatorName.Text = string.Format("{0} - {1} ({2} - {3})", emp.EmployeeNumber, emp.EmployeeName, asri.ItemID, asri.ItemName);

            emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == (api.EmployeeID ?? 0));
            emp.Query.Load();

            txtEmployeeName.Text = string.Format("{0} - {1}", emp.EmployeeNumber, emp.EmployeeName);
            txtPeriodYear.Text = ap.PeriodYear;

            ViewState["IsApproved"] = aq.IsApproved ?? false;
            ViewState["IsVoid"] = aq.IsVoid ?? false;

            InitializedQuestion(ViewState["pid"].ToInt());
            PopulateQuestionValue(ViewState["id"].ToInt());
        }

        private void SetEntityValue(AppraisalScoresheet entity, AppraisalScoresheetItemCollection coll)
        {
            entity.ParticipantItemID = ViewState["pid"].ToInt();
            entity.EvaluatorID = ViewState["eid"].ToInt();
            entity.PeriodYear = txtPeriodYear.Text;
            entity.ScoringDate = txtScoringDate.SelectedDate;
            var str = txtEvaluatorName.Text.Substring(txtEvaluatorName.Text.IndexOf('(') + 1, 3);
            entity.SREvaluatorType = str;

            if (coll != null)
            {
                foreach (DataRow row in (ViewState["questionDataTable"] as DataTable).Rows)
                {
                    var c = coll.SingleOrDefault(l => l.QuestionerItemID == row["QuestionerItemID"].ToInt() && l.ScoresheetID == ViewState["id"].ToInt());
                    if (c == null) c = coll.AddNew();
                    c.QuestionerItemID = row["QuestionerItemID"].ToInt();

                    string controlID = QuestionControlID(row["QuestionerItemID"].ToString());
                    var combo = Helper.FindControlRecursive(pnlAppraisalQuestionItem, controlID) as RadComboBox;
                    c.MarkValue = combo.SelectedValue;

                    c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    c.LastUpdateDateTime = DateTime.Now;
                }
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(AppraisalScoresheet entity, AppraisalScoresheetItemCollection coll)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ViewState["id"] = entity.ScoresheetID;

                if (coll != null)
                {
                    foreach (var c in coll)
                    {
                        c.ScoresheetID = ViewState["id"].ToInt();
                    }
                    coll.Save();
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
                que.Where(que.ScoresheetID > ViewState["id"].ToInt());
                que.OrderBy(que.ScoresheetID.Ascending);
            }
            else
            {
                que.Where(que.ScoresheetID < ViewState["id"].ToInt());
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
            entity.IsApproved = true;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDateTime = DateTime.Now;

            SaveEntity(entity, null);
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
            entity.IsApproved = false;
            entity.str.ApprovedByUserID = string.Empty;
            entity.str.ApprovedDateTime = string.Empty;
            SaveEntity(entity, null);
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

            SaveEntity(entity, null);
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

            SaveEntity(entity, null);
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

        private string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }

        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        private void PopulateQuestionValue(int scoresheetId)
        {
            var asi = new AppraisalScoresheetItemQuery("a");
            var aqi = new AppraisalQuestionItemQuery("b");

            asi.Select(aqi.QuestionerItemID, asi.MarkValue);
            asi.InnerJoin(aqi).On(asi.QuestionerItemID == aqi.QuestionerItemID);
            asi.Where(asi.ScoresheetID == scoresheetId);

            var dtbValue = asi.LoadDataTable();

            foreach (DataRow dataRow in dtbValue.Rows)
            {
                string controlID = QuestionControlID(dataRow["QuestionerItemID"].ToString());
                object obj = Helper.FindControlRecursive(pnlAppraisalQuestionItem, controlID);

                var cboAnswerValue = (obj as RadComboBox);
                if (cboAnswerValue != null) cboAnswerValue.SelectedValue = HtmlTagHelper.Devalidate(dataRow["MarkValue"].ToStringDefaultEmpty());
            }
        }

        private IEnumerable<AppraisalQuestionItem> LoadQuestionGroup(int participantItemID)
        {
            if (ViewState["questionGroup"] != null) return ViewState["questionGroup"] as AppraisalQuestionItemCollection;
            else
            {
                var aqi = new AppraisalQuestionItemQuery("a");
                var apq = new AppraisalParticipantQuestionerQuery("b");

                aqi.es.Distinct = true;
                aqi.Select(aqi.QuestionGroupName);
                aqi.InnerJoin(apq).On(apq.QuestionerID == aqi.QuestionerID);
                aqi.Where(apq.ParticipantItemID == participantItemID);

                var coll = new AppraisalQuestionItemCollection();
                coll.Load(aqi);

                ViewState["questionGroup"] = coll;

                return coll;
            }
        }

        private DataTable QuestionDataTable(int participantItemID)
        {
            if (ViewState["questionDataTable"] != null) return ViewState["questionDataTable"] as DataTable;
            else
            {
                var aqi = new AppraisalQuestionItemQuery("c");
                var apq = new AppraisalParticipantQuestionerQuery("d");

                aqi.Select(
                    aqi, "<1 AS QuestionLevel>", "<CAST(1 AS BIT) AS IsMandatory>", "<'' AS Formula>", "<'CBO' AS SRAnswerType>"
                );
                aqi.InnerJoin(apq).On(apq.QuestionerID == aqi.QuestionerID);
                aqi.Where(apq.ParticipantItemID == participantItemID);
                aqi.OrderBy(apq.ParticipantQuestionerID.Ascending, aqi.QuestionerItemID.Ascending);

                var dtb = aqi.LoadDataTable();
                ViewState["questionDataTable"] = dtb;
                return dtb;
            }
        }

        private void InitializedQuestion(int participantItemID)
        {
            //  Get List Question Group
            IEnumerable<AppraisalQuestionItem> questionGroups = LoadQuestionGroup(participantItemID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(participantItemID);

            //  Generate Question Entry
            pnlAppraisalQuestionItem.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (var questionGroup in questionGroups)
            {
                rowNo++;
                var groupTable = new Table { Width = Unit.Percentage(100) };

                // Add Group Label
                var row = new TableRow();
                groupTable.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupName='{0}'", questionGroup.QuestionGroupName), "QuestionerItemID");

                InitializedQuestion(questionRows, groupTable, row, formulas);
                pnlAppraisalQuestionItem.Controls.Add(groupTable);
            }
        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, TableRow row, Hashtable formulas)
        {
            foreach (DataRow rowChild in questionRows)
            {
                groupTable.Rows.Add(InitializedRowQuestion(rowChild));
            }
        }

        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc)
        {
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionerItemID"].ToString() + unique);
                rfv.ControlToValidate = ctlToValidate;
                rfv.ErrorMessage = string.Format("Field {0} Required!", rowQuestion["QuestionName"]);
                rfv.SetFocusOnError = true;

                var myImg = new System.Web.UI.WebControls.Image();
                myImg.Visible = true;
                myImg.SkinID = "rfvImage";
                rfv.Controls.Add(myImg);

                tc.Controls.Add(rfv);
            }
        }

        private TableRow InitializedRowQuestion(DataRow rowQuestion)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionerItemID"].ToString());

            //Create 2 Cell
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());

            tblRow.Cells[0].Attributes["class"] = "label";

            switch (answerType)
            {
                case "LBL":
                    AddLabel(controlID, tblRow, rowQuestion);
                    break;
                case "CBO":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo = ComboBoxControl(controlID, "", string.Empty, 304, rowQuestion["Formula"].ToString(), true);
                    tblRow.Cells[1].Controls.Add(cbo);
                    CreateValidationControl(cbo.ID, string.Empty, rowQuestion, tblRow.Cells[1]);
                    break;
            }
            return tblRow;
        }

        private void AddLabel(string id, TableRow tblRow, DataRow rowQuestion)
        {
            var lbl = new Label();
            lbl.ID = id;
            lbl.Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionName"]);

            tblRow.Cells[0].ColumnSpan = 3;
            tblRow.Cells[0].Controls.Add(lbl);
        }

        private void AddCaptionLabel(TableRow tblRow, DataRow rowQuestion)
        {
            if (rowQuestion["QuestionLevel"].ToInt() == 0)
            {
                tblRow.Cells[0].ColumnSpan = 3;
                tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionName"]);
            }
            else
                tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionName"]);
        }

        private string Spacer(int questionLevel)
        {
            var retval = string.Empty;
            for (int i = 0; i < questionLevel; i++)
            {
                retval = string.Concat(retval, "&nbsp;&nbsp;");
            }
            return retval;
        }

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width, string formula, bool isEnabled)
        {
            var comboBox = new RadComboBox
            {
                ID = id,
                Width = Unit.Pixel(width == 0 ? 304 : width),
                Enabled = isEnabled
            };

            //comboBox.Items.Clear();
            //comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (string row in selectionID.Split(';'))
            //{
            //    comboBox.Items.Add(new RadComboBoxItem(row, row));
            //}

            StandardReference.InitializeIncludeSpace(comboBox, AppEnum.StandardReference.AppraisalAnswer);
            if (!string.IsNullOrEmpty(defaultSelectionID)) comboBox.SelectedValue = defaultSelectionID;

            return comboBox;
        }

        #endregion
    }

    public class HtmlTagHelper
    {
        public static string Validate(string value)
        {
            value = value.Replace("&", "&amp;");
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");
            value = value.Replace("\n", "<br />");

            return value;
        }

        public static string Devalidate(string value)
        {
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("<br />", "\n");
            value = value.Replace("&amp;", "&");

            return value;
        }
    }
}