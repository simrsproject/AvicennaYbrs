using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public partial class InterventionDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalIntervention;

            UrlPageSearch = "InterventionSearch.aspx";
            UrlPageList = "InterventionList.aspx";

            if (!IsPostBack)
            {
                var as1 = new AppraisalScoresheetQuery();
                as1.es.Distinct = true;
                as1.Select(as1.PeriodYear);
                as1.OrderBy(as1.PeriodYear.Descending);
                var tbl = as1.LoadDataTable();

                bool isAdded = false;
                foreach (DataRow row in tbl.Rows)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem(row["PeriodYear"].ToString(), row["PeriodYear"].ToString()));
                    isAdded = true;
                }

                if (isAdded) cboPeriodYear.SelectedIndex = 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["pid"] != null) InitializedQuestion(ViewState["pid"].ToInt(), pnlAppraisalIntervention, true);
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
            if ((aq.ScoresheetID ?? 0) == 0) return;

            ViewState["id"] = aq.ScoresheetID ?? 0;
            ViewState["pid"] = aq.ParticipantItemID;
            ViewState["eid"] = aq.EvaluatorID;

            var as1 = new AppraisalScoresheetQuery("b");
            var api = new AppraisalParticipantItemQuery("c");
            var ape = new AppraisalParticipantEvaluatorQuery("d");
            var emp = new VwEmployeeTableQuery("e");
            var eval = new VwEmployeeTableQuery("f");

            as1.Select(as1.ScoresheetID, emp.EmployeeNumber, emp.EmployeeName, eval.EmployeeNumber.As("EvaluatorNumber"), eval.EmployeeName.As("EvaluatorName"));
            as1.InnerJoin(api).On(as1.ParticipantItemID == api.ParticipantItemID);
            as1.InnerJoin(ape).On(as1.ParticipantItemID == ape.ParticipantItemID && as1.EvaluatorID == ape.EvaluatorID);
            as1.InnerJoin(emp).On(api.EmployeeID == emp.PersonID);
            as1.InnerJoin(eval).On(as1.EvaluatorID == eval.PersonID);
            as1.Where(as1.ScoresheetID == ViewState["id"].ToInt(), as1.ParticipantItemID == ViewState["pid"].ToInt());

            cboEmployeeID.DataSource = as1.LoadDataTable();
            cboEmployeeID.DataBind();
            cboEmployeeID.SelectedValue = ViewState["id"].ToString();

            cboEmployeeID_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, ViewState["id"].ToString(), string.Empty));

            cboPeriodYear.SelectedValue = aq.PeriodYear;
            txtScoringDate.SelectedDate = aq.ScoringDate;

            ViewState["IsApproved"] = aq.IsApproved ?? false;
            ViewState["IsVoid"] = aq.IsVoid ?? false;
        }

        private void SetEntityValue(AppraisalScoresheet entity, AppraisalScoresheetItemCollection coll)
        {
            entity.ParticipantItemID = ViewState["pid"].ToInt();
            entity.EvaluatorID = ViewState["eid"].ToInt();
            entity.ReferenceID = cboEmployeeID.SelectedValue.ToInt();
            entity.PeriodYear = cboPeriodYear.SelectedValue;
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

                    string controlID = QuestionControlID(row["QuestionerItemID"].ToString(), true);
                    var combo = Helper.FindControlRecursive(pnlAppraisalIntervention, controlID) as RadComboBox;
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
                que.Where(que.ScoresheetID > ViewState["id"].ToInt(), que.ReferenceID.IsNotNull());
                que.OrderBy(que.ScoresheetID.Ascending);
            }
            else
            {
                que.Where(que.ScoresheetID < ViewState["id"].ToInt(), que.ReferenceID.IsNotNull());
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

        private string QuestionControlID(string questionID, bool isIntervention)
        {
            return (isIntervention ? "quest" : "oquest") + questionID.Replace('.', '_');
        }

        private string RfvControlID(string questionID, bool isIntervention)
        {
            return (isIntervention ? "rfv" : "orfv") + questionID.Replace('.', '_');
        }

        private void PopulateQuestionValue(int scoresheetId, bool isIntervention)
        {
            var asi = new AppraisalScoresheetItemQuery("a");
            var aqi = new AppraisalQuestionItemQuery("b");
            var as1 = new AppraisalScoresheetQuery("c");

            asi.Select(aqi.QuestionerItemID, asi.MarkValue, string.Format("<ISNULL(c.ReferenceID, {0}) AS ReferenceID>", scoresheetId.ToString()));
            asi.InnerJoin(aqi).On(asi.QuestionerItemID == aqi.QuestionerItemID);
            asi.InnerJoin(as1).On(asi.ScoresheetID == as1.ScoresheetID);
            asi.Where(asi.ScoresheetID == scoresheetId);

            var dtbValue = asi.LoadDataTable();

            if (!isIntervention)
            {
                asi = new AppraisalScoresheetItemQuery("a");
                aqi = new AppraisalQuestionItemQuery("b");
                as1 = new AppraisalScoresheetQuery("c");

                asi.Select(aqi.QuestionerItemID, asi.MarkValue, as1.ReferenceID);
                asi.InnerJoin(aqi).On(asi.QuestionerItemID == aqi.QuestionerItemID);
                asi.InnerJoin(as1).On(asi.ScoresheetID == as1.ScoresheetID);
                asi.Where(asi.ScoresheetID == dtbValue.AsEnumerable().Select(d => d.Field<int>("ReferenceID")).Distinct().Take(1).Single());

                dtbValue = asi.LoadDataTable();
            }

            foreach (DataRow dataRow in dtbValue.Rows)
            {
                string controlID = QuestionControlID(dataRow["QuestionerItemID"].ToString(), isIntervention);
                //string answerType = dataRow[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
                object obj = Helper.FindControlRecursive(isIntervention ? pnlAppraisalIntervention : pnlAppraisalQuestionItem, controlID);

                //switch (answerType)
                //{
                //   case "CBO":
                var cboAnswerValue = (obj as RadComboBox);
                if (cboAnswerValue != null) cboAnswerValue.SelectedValue = HtmlTagHelper.Devalidate(dataRow["MarkValue"].ToStringDefaultEmpty());
                //        break;
                //}
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

        private void InitializedQuestion(int participantItemId, Panel panel, bool isIntervention)
        {
            //  Get List Question Group
            IEnumerable<AppraisalQuestionItem> questionGroups = LoadQuestionGroup(participantItemId);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(participantItemId);

            //  Generate Question Entry
            panel.Controls.Clear();
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

                InitializedQuestion(questionRows, groupTable, row, formulas, isIntervention);
                panel.Controls.Add(groupTable);
            }
        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, TableRow row, Hashtable formulas, bool isIntervention)
        {
            foreach (DataRow rowChild in questionRows)
            {
                groupTable.Rows.Add(InitializedRowQuestion(rowChild, isIntervention));
            }
        }

        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc, bool isIntervention)
        {
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionerItemID"].ToString() + unique, isIntervention);
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

        private TableRow InitializedRowQuestion(DataRow rowQuestion, bool isIntervention)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionerItemID"].ToString(), isIntervention);

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
                    CreateValidationControl(cbo.ID, string.Empty, rowQuestion, tblRow.Cells[1], isIntervention);
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

        private CheckBox CheckBoxControl(string id, string text, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            chk.Width = Unit.Pixel(width == 0 ? 300 : width);
            chk.Text = text;
            return chk;
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

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ScoresheetID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var as1 = new AppraisalScoresheetQuery("b");
            var api = new AppraisalParticipantItemQuery("c");
            var ape = new AppraisalParticipantEvaluatorQuery("d");
            var emp = new VwEmployeeTableQuery("e");
            var eval = new VwEmployeeTableQuery("f");

            as1.es.Top = 20;
            as1.Select(as1.ScoresheetID, emp.EmployeeNumber, emp.EmployeeName, eval.EmployeeNumber.As("EvaluatorNumber"), eval.EmployeeName.As("EvaluatorName"));
            as1.InnerJoin(api).On(as1.ParticipantItemID == api.ParticipantItemID);
            as1.InnerJoin(ape).On(as1.ParticipantItemID == ape.ParticipantItemID && as1.EvaluatorID == ape.EvaluatorID);
            as1.InnerJoin(emp).On(api.EmployeeID == emp.PersonID);
            as1.InnerJoin(eval).On(as1.EvaluatorID == eval.PersonID);
            as1.Where(as1.PeriodYear == cboPeriodYear.SelectedValue);
            as1.Where(emp.EmployeeName.Like(searchTextContain));

            cboEmployeeID.DataSource = as1.LoadDataTable();
            cboEmployeeID.DataBind();
        }

        protected void cboEmployeeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var scrId = e.Value.ToInt();

            var as1 = new AppraisalScoresheet();
            as1.Query.Where(as1.Query.ScoresheetID == scrId);
            as1.Query.Load();

            ViewState["pid"] = as1.ParticipantItemID ?? 0;

            var api = new AppraisalParticipantItem();
            api.Query.Where(api.Query.ParticipantItemID == as1.ParticipantItemID);
            api.Query.Load();

            //var ap = new AppraisalParticipant();
            //ap.LoadByPrimaryKey(api.ParticipantID ?? 0);
            //txtPeriodYear.Text = ap.PeriodYear;

            var eval = new VwEmployeeTable();
            eval.Query.Where(eval.Query.PersonID == (as1.EvaluatorID ?? -1));
            eval.Query.Load();

            var ape = new AppraisalParticipantEvaluator();
            ape.Query.Where(ape.Query.ParticipantItemID == as1.ParticipantItemID, ape.Query.EvaluatorID == as1.EvaluatorID);
            ape.Query.Load();

            var asri = new AppStandardReferenceItem();
            asri.LoadByPrimaryKey(AppEnum.StandardReference.EvaluatorType.ToString(), ape.SREvaluatorType);

            txtEvaluatorName.Text = string.Format("{0} - {1} ({2} - {3})", eval.EmployeeNumber, eval.EmployeeName, asri.ItemID, asri.ItemName);

            //var apq = new AppraisalParticipantQuestionerCollection();
            //apq.Query.Where(apq.Query.ParticipantItemID == as1.ParticipantItemID);
            //apq.Query.Load();

            InitializedQuestion(as1.ParticipantItemID ?? 0, pnlAppraisalQuestionItem, false);
            PopulateQuestionValue(scrId, false);
            InitializedQuestion(as1.ParticipantItemID ?? 0, pnlAppraisalIntervention, true);
            PopulateQuestionValue(ViewState["id"] == null ? 0 : ViewState["id"].ToInt(), true);
        }
    }
}