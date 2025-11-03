using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientFormDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.EpisodeAndHistory;
            var regType = Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.ClusterPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializedQuestion(Request.QueryString["fid"]);
            PopulateQuestionValue();
            Button btnOK = ((Button)Helper.FindControlRecursive(Page, "btnOk"));
            btnOK.ValidationGroup = "entry";
        }

        private string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }

        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        private void PopulateQuestionValue()
        {
            var query = new HealthRecordLineQuery("a");
            var qQuest = new QuestionQuery("b");
            query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);
            query.Select
                (
                    query.QuestionID,
                    query.QuestionAnswerSelectionLineID,
                    query.QuestionAnswerNum,
                    query.QuestionAnswerText,
                    qQuest.SRAnswerType
                );
            query.Where
                (
                    query.PatientID == Request.QueryString["patid"],
                    query.QuestionFormID == Request.QueryString["fid"]
                );
            query.OrderBy(qQuest.IndexNo.Ascending);

            DataTable dtbValue = query.LoadDataTable();

            foreach (DataRow dataRow in dtbValue.Rows)
            {
                string controlID = QuestionControlID(dataRow["QuestionID"].ToString());
                string answerType = dataRow[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
                object obj;


                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID);

                switch (answerType)
                {
                    case "NUM":
                        var numAnswerValue = (obj as RadNumericTextBox);
                        if (!dataRow["QuestionAnswerNum"].Equals("&nbsp;") &&
                            dataRow["QuestionAnswerNum"] != DBNull.Value)
                            numAnswerValue.Value = Convert.ToDouble(dataRow["QuestionAnswerNum"]);
                        break;
                    case "TXT":
                    case "MEM":
                        var txtAnswerValue = (obj as RadTextBox);
                        txtAnswerValue.Text = dataRow["QuestionAnswerText"].ToStringDefaultEmpty();
                        break;
                    case "CBO":
                        var cboAnswerValue = (obj as RadComboBox);
                        cboAnswerValue.SelectedValue = dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty();
                        break;
                    case "CHK":
                        var chk = (obj as CheckBox);
                        chk.Checked = "1".Equals(dataRow["QuestionAnswerText"]);
                        break;
                    case "CTX":
                        var ctxValue = dataRow["QuestionAnswerText"];
                        if (ctxValue != DBNull.Value)
                        {
                            var ctxValues = ctxValue.ToString().Split('|');
                            if (ctxValues.Length > 0 && ctxValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                ctxChk.Checked = "1".Equals(ctxValues[0]);
                            }
                            if (ctxValues.Length > 1 && ctxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                                var ctxTxt = (obj as RadTextBox);
                                ctxTxt.Text = ctxValues[1];
                            }
                        }
                        break;
                    case "CNM":
                        var cnmValue = dataRow["QuestionAnswerText"];
                        if (cnmValue != DBNull.Value)
                        {
                            var cnmValues = cnmValue.ToString().Split('|');
                            if (cnmValues.Length > 0 && cnmValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                ctxChk.Checked = "1".Equals(cnmValues[0]);
                            }
                            if (cnmValues.Length > 1 && cnmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                                var cnmNum = (obj as RadNumericTextBox);
                                if (!string.IsNullOrEmpty(cnmValues[1]) && !cnmValues[1].Equals("&nbsp;"))
                                    cnmNum.Value = Convert.ToDouble(cnmValues[1]);
                            }
                        }
                        break;
                    case "CBT":
                        var cbtValue = dataRow["QuestionAnswerText"];
                        if (cbtValue != DBNull.Value)
                        {
                            var cbtValues = cbtValue.ToString().Split('|');
                            if (cbtValues.Length > 0 && cbtValues[0] != null)
                            {
                                var cbtCbo = (obj as RadComboBox);
                                cbtCbo.SelectedValue = dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty();
                            }
                            if (cbtValues.Length > 1 && cbtValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                                var cbtTxt = (obj as RadTextBox);
                                cbtTxt.Text = cbtValues[1];
                            }
                        }
                        break;
                    case "CB2":
                        var cb2Value = dataRow["QuestionAnswerSelectionLineID"];
                        if (cb2Value != DBNull.Value)
                        {
                            var cb2Values = cb2Value.ToStringDefaultEmpty().Split('|');
                            if (cb2Values.Length > 0 && cb2Values[0] != null)
                            {
                                var cbo1 = (obj as RadComboBox);
                                cbo1.SelectedValue = cb2Values[0]; ;
                            }
                            if (cb2Values.Length > 1 && cb2Values[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                                var cbo2 = (obj as RadComboBox);
                                cbo2.SelectedValue = cb2Values[1];
                            }
                        }
                        break;
                    case "TTX":
                        var ttxValue = dataRow["QuestionAnswerText"];
                        if (ttxValue != DBNull.Value)
                        {
                            var ttxValues = ttxValue.ToString().Split('|');
                            if (ttxValues.Length > 0 && ttxValues[0] != null)
                            {
                                var txt = (obj as RadTextBox);
                                txt.Text = ttxValues[0];
                            }
                            if (ttxValues.Length > 1 && ttxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                                var txt2 = (obj as RadTextBox);
                                txt2.Text = ttxValues[1];
                            }
                        }
                        break;
                }

            }
        }

        private IEnumerable<QuestionGroup> LoadQuestionGroup(string formID)
        {
            var query = new QuestionGroupQuery("a");
            var qrQGroupInForm = new QuestionGroupInFormQuery("d");
            query.InnerJoin(qrQGroupInForm).On(query.QuestionGroupID == qrQGroupInForm.QuestionGroupID);
            query.Where(qrQGroupInForm.QuestionFormID == formID);
            query.SelectAll();
            query.OrderBy(qrQGroupInForm.RowIndex.Ascending);

            var coll = new QuestionGroupCollection();
            coll.Load(query);
            return coll;
        }

        private DataTable QuestionDataTable(string formID)
        {
            var questionQuery = new QuestionQuery("a");
            var qrQInGroup = new QuestionInGroupQuery("c");
            var qrQGInForm = new QuestionGroupInFormQuery("d");
            questionQuery.InnerJoin(qrQInGroup).On(questionQuery.QuestionID == qrQInGroup.QuestionID);
            questionQuery.InnerJoin(qrQGInForm).On(qrQInGroup.QuestionGroupID == qrQGInForm.QuestionGroupID);
            questionQuery.OrderBy(qrQInGroup.RowIndex.Ascending, questionQuery.IndexNo.Ascending);
            questionQuery.Where(qrQGInForm.QuestionFormID == formID);
            questionQuery.Select
                (
                    questionQuery,
                    qrQInGroup.QuestionGroupID,
                    qrQInGroup.RowIndex
                );

            return questionQuery.LoadDataTable();
        }

        private void InitializedQuestion(string formID)
        {
            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            pnlPatientHealthRecordLine.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (QuestionGroup questionGroup in questionGroups)
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
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                foreach (DataRow rowChild in questionRows)
                {
                    if (!rowChild["Formula"].ToString().Equals(string.Empty))
                        formulas.Add(rowChild["QuestionID"].ToString().Replace('.', '_'), rowChild["Formula"].ToString());
                    if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                    {
                        row = InitializedRowQuestion(rowChild);
                        groupTable.Rows.Add(row);
                    }
                    var quest = new QuestionQuery();
                    quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                    quest.OrderBy(quest.IndexNo.Ascending);
                    var dtbSubQuestion = quest.LoadDataTable();

                    foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                    {
                        if (!rowSubQuestion["Formula"].ToString().Equals(string.Empty))
                            formulas.Add(rowSubQuestion["QuestionID"].ToString().Replace('.', '_'), rowSubQuestion["Formula"].ToString());
                        row = InitializedRowQuestion(rowSubQuestion);
                        groupTable.Rows.Add(row);
                    }
                }
                pnlPatientHealthRecordLine.Controls.Add(groupTable);
            }

            //Generate Formula Script
            if (!IsPostBack)
            {
                var script = new StringBuilder();
                script.AppendLine("<script type='text/javascript' language='javascript'>");
                script.AppendLine("function fillFormulaField(){");

                foreach (DictionaryEntry dictionaryEntry in formulas)
                {

                    string id = dictionaryEntry.Key.ToString();

                    // [200.020]/(([200.030]/100)*([200.030]/100))
                    string formula = dictionaryEntry.Value.ToString();
                    formula = formula.Replace('.', '_');
                    formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
                    formula = formula.Replace("]", "').get_value()");
                    script.AppendFormat("var value{0}={1};", id, formula);
                    script.AppendLine();
                    script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
                }
                script.AppendLine();
                script.AppendLine("}</script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            }

            //Asign Formula
            foreach (DictionaryEntry dictionaryEntry in formulas)
            {
                var inputFormula = (RadInputControl)Helper.FindControlRecursive(pnlPatientHealthRecordLine, "quest" + dictionaryEntry.Key.ToString().Replace('.', '_'));
                inputFormula.ClientEvents.OnValueChanged = "fillFormulaField";

                var regex = new Regex("(?<=\\[)(.*?)(?=\\])"); // Ambil string yg hanya diapit oleh [ dan ]
                var results = regex.Matches(dictionaryEntry.Value.ToString());
                foreach (Match result in results)
                {
                    var input = (RadInputControl)Helper.FindControlRecursive(pnlPatientHealthRecordLine, "quest" + result.Value.Replace('.', '_'));
                    if (input != null) input.ClientEvents.OnValueChanged = "fillFormulaField";
                }
            }
        }

        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc) 
        {
            //if (isMandatory)
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionID"].ToString() + unique);
                rfv.ControlToValidate = ctlToValidate;
                rfv.ErrorMessage = string.Format("Field {0} Required!", rowQuestion["QuestionText"]);
                rfv.SetFocusOnError = true;
                //rfv.ForeColor = System.Drawing.Color.Red;
                //rfv.Display = ValidatorDisplay.Dynamic;
                //rfv.IsValid = false;
                //rfv.EnableViewState = true;
                System.Web.UI.WebControls.Image myImg = new System.Web.UI.WebControls.Image();
                myImg.Visible = true;
                myImg.SkinID = "rfvImage";
                rfv.Controls.Add(myImg);

                tc.Controls.Add(rfv);
                //Page.Controls.Add(rfv);
            }
        }

        private TableRow InitializedRowQuestion(DataRow rowQuestion)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());

            bool isMandatory = (bool)rowQuestion["IsMandatory"];

            //Create 2 Cell
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());
            // add new cell for validation
            tblRow.Cells.Add(new TableCell());

            tblRow.Cells[0].Attributes["class"] = "label";

            var litSep = new Literal();
            switch (answerType)
            {
                case "LBL":
                    AddCaptionLabel(tblRow, rowQuestion);
                    break;
                case "NUM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var num = RadNumericTextBoxControl(controlID, int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(num);
                    CreateValidationControl(num.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "MEM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var mem = MemoControl(controlID, rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(mem);
                    CreateValidationControl(mem.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TXT":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(txt);
                    CreateValidationControl(txt.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBO":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cbo);
                    CreateValidationControl(cbo.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CHK":
                    var chk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(chk);
                    break;
                case "TTX":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt1 = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(txt1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var txt2 = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt());
                    tblRow.Cells[1].Controls.Add(txt2);
                    CreateValidationControl(txt1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(txt2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CTX":
                    var ctxChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctxChk);
                    var ctxTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctxTxt);
                    break;
                case "CNM":
                    var cnmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cnmChk);
                    var cnmNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt());
                    tblRow.Cells[1].Controls.Add(cnmNum);
                    break;
                case "CB2":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo1 = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cbo1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbo2 = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID2"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString(), rowQuestion["AnswerWidth2"].ToInt());
                    tblRow.Cells[1].Controls.Add(cbo2);
                    CreateValidationControl(cbo1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbo2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBT":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cbCbo);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt());
                    tblRow.Cells[1].Controls.Add(cbTxt);
                    CreateValidationControl(cbCbo.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbTxt.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
            }
            return tblRow;
        }

        private void AddCaptionLabel(TableRow tblRow, DataRow rowQuestion)
        {
            if (rowQuestion["QuestionLevel"].ToInt() == 0)
                tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            else
                tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
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

        private RadTextBox TextBoxControl(string id, int width)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }

        private RadTextBox MemoControl(string id, int width)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            textBox.Height = Unit.Pixel(100);
            textBox.TextMode = InputMode.MultiLine;
            return textBox;
        }

        private RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;") ? string.Empty : string.Format("n {0}", suffix);
            textBox.Value = 0;
            return textBox;
        }

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width)
        {
            var comboBox = new RadComboBox();
            comboBox.ID = id;
            comboBox.Width = Unit.Pixel(width == 0 ? 304 : width);

            var query = new QuestionAnswerSelectionLineQuery();
            query.Select(
                query.QuestionAnswerSelectionLineID,
                query.QuestionAnswerSelectionLineText
                );
            query.Where(query.QuestionAnswerSelectionID == selectionID);
            var dtb = query.LoadDataTable();

            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(), row["QuestionAnswerSelectionLineID"].ToString()));
            }

            if (!string.IsNullOrEmpty(defaultSelectionID))
                comboBox.SelectedValue = defaultSelectionID;

            return comboBox;
        }

        private void AddSpacerCell(TableCellCollection cells)
        {
            var cell = new TableCell { Text = "&nbsp;&nbsp;", Wrap = false };
            cells.Add(cell);
        }

        public override bool OnButtonOkClicked()
        {
            //Page.Validate("entry");
            if (!Page.IsValid)
                return false;

            var hr = new HealthRecord();
            if (!hr.LoadByPrimaryKey(Request.QueryString["patid"], Request.QueryString["fid"]))
            {
                hr.AddNew();
                hr.PatientID = Request.QueryString["patid"];
                hr.QuestionFormID = Request.QueryString["fid"];
                hr.RecordDate = DateTime.Now.Date;
                hr.RecordTime = DateTime.Now.ToString("HH:mm");
                hr.EmployeeID = AppSession.UserLogin.UserID;
                hr.IsComplete = false;
            }
            hr.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 
            hr.LastUpdateByUserID = AppSession.UserLogin.UserID;

            var dtbQuestion = QuestionDataTable(Request.QueryString["fid"]);

            var coll = new HealthRecordLineCollection();
            coll.Query.Where(
                coll.Query.PatientID == Request.QueryString["patid"],
                coll.Query.QuestionFormID == Request.QueryString["fid"]
                );
            coll.LoadAll();

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                SetPatientHealthRecordLine(coll, rowQuestion, rowQuestion["QuestionGroupID"].ToString());

                var quest = new QuestionQuery();
                quest.Where(
                    quest.ParentQuestionID == rowQuestion["QuestionID"] &&
                    quest.SRAnswerType != string.Empty
                    );
                var dtbSubQuestion = quest.LoadDataTable();

                foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                {
                    SetPatientHealthRecordLine(coll, rowSubQuestion, rowQuestion["QuestionGroupID"].ToString());
                }
            }

            using (var trans = new esTransactionScope())
            {
                hr.Save();
                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        private void SetPatientHealthRecordLine(HealthRecordLineCollection collValue, DataRow rowQuestion, string questionGroupID)
        {
            string questionID = rowQuestion[HealthRecordLineMetadata.ColumnNames.QuestionID].ToString();

            var hrLine = collValue.FindByPrimaryKey(Request.QueryString["patid"], Request.QueryString["fid"], questionGroupID, questionID) ?? collValue.AddNew();

            hrLine.PatientID = Request.QueryString["patid"];
            hrLine.QuestionFormID = Request.QueryString["fid"];
            hrLine.QuestionGroupID = questionGroupID;
            hrLine.QuestionID = questionID;
            hrLine.QuestionAnswerPrefix = rowQuestion["AnswerPrefix"].ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = rowQuestion["AnswerSuffix"].ToStringDefaultEmpty();

            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj;

            obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID);
            switch (answerType)
            {
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    break;
                case "MEM":
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = txtAnswerValue.Text;
                    break;
                case "CBO":
                    var cboAnswerValue = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = cboAnswerValue.SelectedValue;
                    hrLine.QuestionAnswerText = cboAnswerValue.Text;
                    break;
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", hrLine.QuestionAnswerText, ctxTxt.Text);
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", hrLine.QuestionAnswerText, cnmNum.Text);
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = cbtCbo.SelectedValue;
                    hrLine.QuestionAnswerText = cbtCbo.Text;

                    obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", hrLine.QuestionAnswerText, cbtTxt.Text);
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = cbo1.SelectedValue;
                    hrLine.QuestionAnswerText = cbo1.Text;

                    obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", hrLine.QuestionAnswerText, cbo2.Text);

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", cbo1.SelectedValue, cbo2.SelectedValue);
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = txt1.Text;

                    obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", hrLine.QuestionAnswerText, txt2.Text);
                    break;
            }
            if (hrLine.es.IsAdded || hrLine.es.IsModified)
            {
                hrLine.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hrLine.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.form = 'form'";
        }
    }
}
