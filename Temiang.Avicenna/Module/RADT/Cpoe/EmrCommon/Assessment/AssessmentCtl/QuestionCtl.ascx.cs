using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class QuestionCtl : System.Web.UI.UserControl
    {
        bool _isInitialized = false;
        private bool? _isUseQuestionGroupCaption;
        private const int WidthOneLevel = 18;

        public bool IsUseQuestionGroupCaption
        {
            get { return _isUseQuestionGroupCaption ?? true; }
            set { _isUseQuestionGroupCaption = value; }
        }

        public string QuestionGroupID
        {
            get
            {
                var qgid = ViewState["qgid"];
                if (qgid == null) return string.Empty;
                return ViewState["qgid"].ToString();
            }
            set { ViewState["qgid"] = value; }
        }

        public int LabelWidth
        {
            get
            {
                var lbw = ViewState["lbw"];
                if (lbw == null) return 0;
                return ViewState["lbw"].ToInt();
            }
            set { ViewState["lbw"] = value; }
        }

        public int Width
        {
            get
            {
                var lbw = ViewState["tbw"];
                if (lbw == null) return 0;
                return ViewState["tbw"].ToInt();
            }
            set { ViewState["tbw"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!_isInitialized)
                InitializedQuestion(QuestionGroupID);
        }
        private string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }
        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        #region QuestionGroupAnswerValue
        public QuestionGroupAnswerValue GetQuestionAnswerValue()
        {
            var strbSummary = new StringBuilder();
            var retval = new QuestionGroupAnswerValue();
            retval.QuestionGroupID = QuestionGroupID;

            var dtbQuestion = QuestionDataTable(QuestionGroupID);
            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                var answerVal = GetQuestionAnswerValue(rowQuestion);
                if (answerVal != null)
                {
                    retval.QuestionAnswerValues.Add(answerVal);
                }

                var normalVal = rowQuestion["QuestionAnswerDefaultSelectionID"];

                // Summary
                AppendSummary(answerVal, normalVal, rowQuestion["QuestionText"].ToString(), rowQuestion["SRAnswerType"].ToString(), strbSummary);

                // Check Child Question
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);
                var dtbSubQuestion = quest.LoadDataTable();

                if (dtbSubQuestion != null && dtbSubQuestion.Rows.Count > 0)
                {
                    var strbSummaryChild = new StringBuilder();

                    foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                    {
                        var childAnswerVal = GetQuestionAnswerValue(rowSubQuestion);
                        if (childAnswerVal == null) continue;

                        retval.QuestionAnswerValues.Add(childAnswerVal);
                        normalVal = rowSubQuestion["QuestionAnswerDefaultSelectionID"];

                        // Summary
                        AppendSummary(childAnswerVal, normalVal, rowSubQuestion["QuestionText"].ToString(),
                            rowSubQuestion["SRAnswerType"].ToString(), strbSummaryChild);

                    }

                    var sumChild = strbSummaryChild.ToString();
                    if (!string.IsNullOrWhiteSpace(sumChild))
                    {
                        strbSummary.AppendFormat("{0} [ ", rowQuestion["QuestionText"]);
                        strbSummary.Append(sumChild);
                        strbSummary.Append("] | ");
                    }
                }
            }

            retval.Summary = strbSummary.ToString();
            return retval;
        }

        private void AppendSummary(QuestionAnswerValue answerVal, object normalVal, string questionText, string answerType,
            StringBuilder strbSummary)
        {
            if (answerVal == null) return;

            if (answerType == "TXT" || answerType == "MEM")
            {
                strbSummary.AppendFormat("{0}: {1} | ", questionText, answerVal.QuestionAnswerText);
                return;
            }

            if (!((answerVal.QuestionAnswerSelectionLineID != null && answerVal.QuestionAnswerSelectionLineID.Equals(normalVal))
                  || (answerVal.QuestionAnswerText != null && answerVal.QuestionAnswerText.Equals(normalVal))))
            {
                if (answerVal.QuestionAnswerText != null)
                {
                    if (answerVal.QuestionAnswerText.Equals(questionText) || answerType == "YSN") // Ambil captionnya saja
                    {
                        strbSummary.AppendFormat("{0} | ", questionText);
                    }
                    else
                    {
                        if (answerVal.QuestionAnswerText.Contains('|'))
                        {
                            var vals = answerVal.QuestionAnswerText.Split('|');
                            if (!string.IsNullOrWhiteSpace(vals[0]))
                            {
                                if (vals[0].Equals(questionText))
                                {
                                    strbSummary.AppendFormat("{0}: {1} | ", questionText, vals[1]);
                                }
                                else
                                {
                                    strbSummary.AppendFormat("{0}: {1} | ", questionText, answerVal.QuestionAnswerText.Replace('|', ' '));
                                }
                            }
                        }
                        else
                        {
                            strbSummary.AppendFormat("{0}: {1} | ", questionText,
                                answerVal.QuestionAnswerText);
                        }
                    }
                }
            }
        }
        private QuestionAnswerValue GetQuestionAnswerValue(DataRow rowQuestion)
        {
            var questionID = rowQuestion["QuestionID"].ToString();
            var answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            string controlID = QuestionControlID(questionID);

            object obj;
            if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                obj = Helper.FindControlRecursive(pnlQuestion, controlID);
            else
                obj = Helper.FindControlRecursive(pnlQuestion, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

            bool? isAnswerExist = null;

            var answerValue = new QuestionAnswerValue();
            switch (answerType)
            {
                case "YSN":
                    {
                        var ddl = (obj as RadioButtonList);
                        if (ddl.SelectedIndex > -1)
                        {
                            answerValue.QuestionAnswerSelectionLineID = ddl.SelectedValue;
                            answerValue.QuestionAnswerText = ddl.Text;
                        }

                        break;
                    }
                case "YNT":
                    {
                        var ddl = (obj as RadioButtonList);
                        answerValue.QuestionAnswerSelectionLineID = ddl.SelectedValue;
                        answerValue.QuestionAnswerText = ddl.Text;

                        obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                        var yntTxt = (obj as RadTextBox);
                        answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, yntTxt.Text);

                        // Reset jika kosong
                        if (answerValue.QuestionAnswerText.Trim().Equals("|"))
                            answerValue.QuestionAnswerText = string.Empty;
                        break;
                    }
                case "MSK":
                    var mskAnswerValue = (obj as RadMaskedTextBox);
                    if (!String.IsNullOrWhiteSpace(mskAnswerValue.Text))
                        answerValue.QuestionAnswerText = mskAnswerValue.TextWithLiterals;

                    break;
                case "DAT":
                    var dat = (obj as RadDatePicker);
                    if (!dat.IsEmpty)
                        answerValue.QuestionAnswerText =
                            dat.SelectedDate.Value.ToString("MM/dd/yyyy"); // Harcode Format
                    break;
                case "DTM":
                    var dtm = (obj as RadDateTimePicker);
                    if (!dtm.IsEmpty)
                        answerValue.QuestionAnswerText =
                            dtm.SelectedDate.Value.ToString("MM/dd/yyyy HH:mm"); // Harcode Format
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    if (!tim.IsEmpty)
                        answerValue.QuestionAnswerText = tim.SelectedDate.Value.ToString("HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    answerValue.QuestionAnswerNum = numAnswerValue.Value ?? 0;
                    answerValue.QuestionAnswerText = numAnswerValue.Text;
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "MEM":
                    var memAnswerValue = (obj as RadTextBox);
                    if (!string.IsNullOrWhiteSpace(memAnswerValue.Text))
                        answerValue.QuestionAnswerText = memAnswerValue.Text.Replace("\n", "<br />");
                    break;
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    if (!string.IsNullOrWhiteSpace(txtAnswerValue.Text))
                        answerValue.QuestionAnswerText = txtAnswerValue.Text;
                    break;
                case "CBO":
                    var cboAnswerValue = (obj as RadComboBox);
                    if (!string.IsNullOrWhiteSpace(cboAnswerValue.Text))
                    {
                        answerValue.QuestionAnswerSelectionLineID = cboAnswerValue.SelectedValue;
                        answerValue.QuestionAnswerText = cboAnswerValue.Text;
                    }
                    break;
                case "CHK":
                    var chk = (obj as CheckBox);
                    answerValue.QuestionAnswerSelectionLineID = chk != null && chk.Checked ? "1" : "0";
                    answerValue.QuestionAnswerText = chk != null && chk.Checked ? chk.Text : string.Empty;
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CCB":
                    var ccb1 = (obj as CheckBox);
                    answerValue.QuestionAnswerSelectionLineID = ccb1 != null && ccb1.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var ccb2 = (obj as RadComboBox);
                    answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, ccb2.Text);
                    answerValue.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", ccb2.SelectedValue, ccb2.SelectedValue);
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    answerValue.QuestionAnswerSelectionLineID = ctxChk != null && ctxChk.Checked ? "1" : "0";
                    answerValue.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? ctxChk.Text : string.Empty;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, ctxTxt.Text);
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    answerValue.QuestionAnswerSelectionLineID = ctmChk != null && ctmChk.Checked ? "1" : "0";
                    answerValue.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? ctmChk.Text : string.Empty;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, ctmTxt.Text.Replace("\n", "<br />"));
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    answerValue.QuestionAnswerSelectionLineID = cnmChk != null && cnmChk.Checked ? "1" : "0";
                    answerValue.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? cnmChk.Text : string.Empty;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, cnmNum.Text);
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    answerValue.QuestionAnswerSelectionLineID = cbtCbo.SelectedValue;
                    answerValue.QuestionAnswerText = cbtCbo.Text;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);

                    if (!string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText) || !string.IsNullOrWhiteSpace(cbtTxt.Text))
                        answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, cbtTxt.Text);
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    answerValue.QuestionAnswerSelectionLineID = cbnCbo.SelectedValue;
                    answerValue.QuestionAnswerText = cbnCbo.Text;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, cbnNum.Text);
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    answerValue.QuestionAnswerSelectionLineID = cbtCbm.SelectedValue;
                    answerValue.QuestionAnswerText = cbtCbm.Text;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);

                    if (!string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText) || !string.IsNullOrWhiteSpace(cbtTxm.Text))
                        answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, cbtTxm.Text.Replace("\n", "<br />"));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    answerValue.QuestionAnswerSelectionLineID = cbo1.SelectedValue;
                    answerValue.QuestionAnswerText = cbo1.Text;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);

                    if (!string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText) ||
                        !string.IsNullOrWhiteSpace(cbo2.Text))
                    {
                        answerValue.QuestionAnswerText =
                            string.Format("{0}|{1}", answerValue.QuestionAnswerText, cbo2.Text);

                        answerValue.QuestionAnswerSelectionLineID =
                            string.Format("{0}|{1}", cbo1.SelectedValue, cbo2.SelectedValue);
                    }

                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    answerValue.QuestionAnswerText = txt1.Text;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    if (!string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText) ||
                        !string.IsNullOrWhiteSpace(txt2.Text))
                        answerValue.QuestionAnswerText = string.Format("{0}|{1}", answerValue.QuestionAnswerText, txt2.Text);
                    break;
                case "TBL":
                    var tbl = (obj as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var objCell = Helper.FindControlRecursive(
                                    pnlQuestion,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                var objCellText = (objCell as RadTextBox);
                                ansText += (ansText.Equals(string.Empty) ? "" : "|") + objCellText.Text;
                            }
                        }
                    }
                    answerValue.QuestionAnswerText = ansText;
                    isAnswerExist = true; // Dianggap selalu ada
                    break;
                case "DNT": //Dental Control

                    break;
            }

            if ((isAnswerExist ?? false) || !string.IsNullOrWhiteSpace(answerValue.QuestionAnswerText))
            {
                answerValue.QuestionID = questionID;
                answerValue.SRAnswerType = answerType;

                var val = rowQuestion[QuestionMetadata.ColumnNames.AnswerPrefix];
                if (val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()))
                    answerValue.QuestionAnswerPrefix = val.ToString();

                val = rowQuestion[QuestionMetadata.ColumnNames.AnswerSuffix];
                if (val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()))
                    answerValue.QuestionAnswerSuffix = val.ToString();
                return answerValue;
            }

            return null;
        }

        #endregion
        public void PopulateValue(QuestionGroupAnswerValue questionGroupAnswerValue)
        {
            if (!_isInitialized)
                InitializedQuestion(QuestionGroupID);

            if (questionGroupAnswerValue != null && questionGroupAnswerValue.QuestionAnswerValues != null)
            {
                var pExam = questionGroupAnswerValue.QuestionAnswerValues;
                foreach (QuestionAnswerValue answerValue in pExam)
                {
                    string controlID = QuestionControlID(answerValue.QuestionID);
                    object obj;

                    obj = Helper.FindControlRecursive(pnlQuestion, controlID);
                    if (obj == null) continue;

                    switch (answerValue.SRAnswerType)
                    {
                        case "YSN":
                        {
                            var rbl = (obj as RadioButtonList);
                            if (rbl != null) rbl.SelectedValue = answerValue.QuestionAnswerSelectionLineID;
                            break;
                        }
                        case "YNT":
                        {
                            var yntValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(yntValue))
                            {
                                var rbl = (obj as RadioButtonList);
                                if (rbl != null) rbl.SelectedValue = answerValue.QuestionAnswerSelectionLineID;

                                if (yntValue.Contains("|"))
                                {
                                    var vals = yntValue.Split('|');
                                    if (vals.Length > 1 && vals[1] != null)
                                    {
                                        obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                        var ctxTxt = (obj as RadTextBox);
                                        if (ctxTxt != null) ctxTxt.Text = vals[1].Replace("<br />", "\n");
                                    }
                                }
                            }
                            break;
                        }
                        case "MSK":
                            var mskAnswerValue = (obj as RadMaskedTextBox);
                            if (mskAnswerValue != null) mskAnswerValue.Text = answerValue.QuestionAnswerText;
                            break;
                        case "DAT":
                            var dtAnswerValue = (obj as RadDatePicker);
                            if (dtAnswerValue != null)
                                dtAnswerValue.SelectedDate = Convert.ToDateTime(answerValue.QuestionAnswerText);
                            break;
                        case "DTM":
                            var dtmAnswerValue = (obj as RadDateTimePicker);
                            if (dtmAnswerValue != null)
                                dtmAnswerValue.SelectedDate = Convert.ToDateTime(answerValue.QuestionAnswerText);
                            break;
                        case "TIM":
                            var tpAnswerValue = (obj as RadTimePicker);
                            if (tpAnswerValue != null)
                                tpAnswerValue.SelectedDate = Convert.ToDateTime(answerValue.QuestionAnswerText);
                            break;
                        case "NUM":
                            var numAnswerValue = (obj as RadNumericTextBox);
                            if (numAnswerValue != null) numAnswerValue.Value = answerValue.QuestionAnswerNum;
                            break;
                        case "TXT":
                            var txtAnswerValue = (obj as RadTextBox);
                            if (txtAnswerValue != null) txtAnswerValue.Text = answerValue.QuestionAnswerText;
                            break;
                        case "MEM":
                            var memAnswerValue = (obj as RadTextBox);
                            if (memAnswerValue != null)
                                memAnswerValue.Text = answerValue.QuestionAnswerText.Replace("<br />", "\n");
                            break;
                        case "CBO":
                            var cboAnswerValue = (obj as RadComboBox);
                            if (cboAnswerValue != null)
                                cboAnswerValue.SelectedValue = answerValue.QuestionAnswerSelectionLineID;
                            break;
                        case "CHK":
                            var chk = (obj as CheckBox);
                            if (chk != null) chk.Checked = "1".Equals(answerValue.QuestionAnswerSelectionLineID);
                            break;
                        case "CTX":
                            var ctxValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ctxValue))
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null) ctxChk.Checked = "1".Equals(answerValue.QuestionAnswerSelectionLineID);

                                var ctxValues = ctxValue.Split('|');
                                if (ctxValues.Length > 1 && ctxValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var ctxTxt = (obj as RadTextBox);
                                    if (ctxTxt != null) ctxTxt.Text = ctxValues[1];
                                }
                            }
                            break;
                        case "CTM":
                        {
                            var ctmValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ctmValue))
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(answerValue.QuestionAnswerSelectionLineID);

                                var ctmValues = ctmValue.ToString().Split('|');
                                if (ctmValues.Length > 1 && ctmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var ctxTxt = (obj as RadTextBox);
                                    if (ctxTxt != null) ctxTxt.Text = ctmValues[1].Replace("<br />", "\n");
                                }
                            }

                            break;
                        }
                        case "CNM":
                            var cnmValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cnmValue))
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null) ctxChk.Checked = "1".Equals(answerValue.QuestionAnswerSelectionLineID);

                                var cnmValues = cnmValue.ToString().Split('|');
                                if (cnmValues.Length > 1 && cnmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var cnmNum = (obj as RadNumericTextBox);
                                    if (!string.IsNullOrEmpty(cnmValues[1]) && !cnmValues[1].Equals("&nbsp;"))
                                        if (cnmNum != null) cnmNum.Value = Convert.ToDouble(cnmValues[1]);
                                }
                            }
                            break;
                        case "CBT":
                            var cbtValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbtValue))
                            {
                                var cbtValues = cbtValue.ToString().Split('|');
                                if (cbtValues.Length > 0 && cbtValues[0] != null)
                                {
                                    var cbtCbo = (obj as RadComboBox);
                                    if (cbtCbo != null) cbtCbo.SelectedValue = answerValue.QuestionAnswerSelectionLineID;
                                }
                                if (cbtValues.Length > 1 && cbtValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var cbtTxt = (obj as RadTextBox);
                                    if (cbtTxt != null) cbtTxt.Text = cbtValues[1];
                                }
                            }
                            break;
                        case "CBN":
                            var cbnValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbnValue))
                            {
                                var cbnValues = cbnValue.ToString().Split('|');
                                if (cbnValues[0] != null)
                                {
                                    var cbnCbo = (obj as RadComboBox);
                                    if (cbnCbo != null) cbnCbo.SelectedValue = answerValue.QuestionAnswerSelectionLineID;
                                }
                                if (cbnValues.Length > 1 && cbnValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var cbnNum = (obj as RadNumericTextBox);
                                    if (!string.IsNullOrEmpty(cbnValues[1]) && !cbnValues[1].Equals("&nbsp;"))
                                        cbnNum.Value = Convert.ToDouble(cbnValues[1]);
                                }
                            }
                            break;
                        case "CBM":
                            var cbmValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbmValue))
                            {
                                var cbmValues = cbmValue.ToString().Split('|');
                                if (cbmValues.Length > 0 && cbmValues[0] != null)
                                {
                                    var cbtCbo = (obj as RadComboBox);
                                    if (cbtCbo != null) cbtCbo.SelectedValue = answerValue.QuestionAnswerSelectionLineID;
                                }
                                if (cbmValues.Length > 1 && cbmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var cbtTxt = (obj as RadTextBox);
                                    if (cbtTxt != null) cbtTxt.Text = cbmValues[1].Replace("<br />", "\n");
                                }
                            }
                            break;
                        case "CB2":
                            var cb2Value = answerValue.QuestionAnswerSelectionLineID;
                            if (!string.IsNullOrEmpty(cb2Value))
                            {
                                var cb2Values = cb2Value.ToStringDefaultEmpty().Split('|');
                                if (cb2Values.Length > 0 && cb2Values[0] != null)
                                {
                                    var cbo1 = (obj as RadComboBox);
                                    if (cbo1 != null) cbo1.SelectedValue = cb2Values[0];
                                }
                                if (cb2Values.Length > 1 && cb2Values[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var cbo2 = (obj as RadComboBox);
                                    if (cbo2 != null) cbo2.SelectedValue = cb2Values[1];
                                }
                            }
                            break;
                        case "TTX":
                            var ttxValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ttxValue))
                            {
                                var ttxValues = ttxValue.ToString().Split('|');
                                if (ttxValues.Length > 0 && ttxValues[0] != null)
                                {
                                    var txt = (obj as RadTextBox);
                                    if (txt != null) txt.Text = ttxValues[0];
                                }
                                if (ttxValues.Length > 1 && ttxValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(pnlQuestion, controlID + "_2");
                                    var txt2 = (obj as RadTextBox);
                                    if (txt2 != null) txt2.Text = ttxValues[1];
                                }
                            }
                            break;
                        case "TBL":
                            var tblValue = answerValue.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(tblValue))
                            {
                                var tblValues = tblValue.ToString().Split('|');

                                // find table
                                var tbl = (HtmlTable)Helper.FindControlRecursive(
                                                pnlQuestion,
                                                controlID);
                                // get row length and col length
                                var rCount = tbl.Rows.Count;
                                string ansText = string.Empty;
                                if (rCount > 0)
                                {
                                    var cCount = tbl.Rows[0].Cells.Count;
                                    for (var r = 1; r < rCount; r++)
                                    {
                                        for (var c = 0; c < cCount; c++)
                                        {
                                            var objCell = Helper.FindControlRecursive(
                                                pnlQuestion,
                                                controlID + "_" + r.ToString() + "_" + c.ToString());
                                            var objCellText = (objCell as RadTextBox);
                                            var cellValIndex = c % cCount;
                                            if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                            {
                                                objCellText.Text = tblValues[cellValIndex + (cCount * (r - 1))];
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "DNT": //Dental Control
                            var dnt = answerValue.QuestionAnswerText.Split(';');

                            foreach (var str in dnt.Where(d => !string.IsNullOrEmpty(d))
                                                   .Select(d => new
                                                   {
                                                       controlID = d.Split('|')[0],
                                                       value = d.Split('|')[1]
                                                   }))
                            {
                                obj = Helper.FindControlRecursive(pnlQuestion, str.controlID);
                                var txt2 = (obj as RadTextBox);
                                if (txt2 != null) txt2.Text = str.value;
                            }

                            break;
                    }

                }
            }
        }

        private DataTable QuestionDataTable(string questionGroupID)
        {
            var questionQuery = new QuestionQuery("a");
            var qrQInGroup = new QuestionInGroupQuery("c");
            questionQuery.InnerJoin(qrQInGroup).On(questionQuery.QuestionID == qrQInGroup.QuestionID);
            questionQuery.OrderBy(qrQInGroup.RowIndex.Ascending, questionQuery.QuestionID.Ascending);
            questionQuery.Where(qrQInGroup.QuestionGroupID == questionGroupID);
            questionQuery.Select
                (
                    questionQuery,
                    qrQInGroup.QuestionGroupID,
                    qrQInGroup.RowIndex
                );

            var dtb = questionQuery.LoadDataTable();
            return dtb;
        }


        private void InitializedQuestion(string questionGroupID)
        {
            if (string.IsNullOrWhiteSpace(questionGroupID)) return;

            _isInitialized = true;

            //  Generate Question Entry
            pnlQuestion.Controls.Clear();
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula

            var qrQg = new QuestionGroup();
            qrQg.LoadByPrimaryKey(questionGroupID);

            var table = new Table();

            // Set Width table
            if (qrQg.Width > 0 || Width > 0)
            {
                if (Width > 0)
                    table.Width = Unit.Pixel(Width);
                else if (qrQg.Width > 0)
                    table.Width = Unit.Pixel(qrQg.Width ?? 1000);
                else
                    table.Width = Unit.Percentage(100);
            }
            else
                table.Width = Unit.Percentage(100);

            // Jika diseting lebar label / kolom pertama maka diset cukup di row pertama saja
            var row = new TableRow();
            if (qrQg.LabelWidth > 0 || LabelWidth > 0)
            {
                var tblRow = new TableRow();

                //Create 2 Cell
                tblRow.Cells.Add(new TableCell());
                tblRow.Cells.Add(new TableCell());

                var cellLabel = tblRow.Cells[0];
                int labelWidth = 150;
                if (LabelWidth > 0)
                    labelWidth = LabelWidth;
                else if (qrQg.LabelWidth > 0)
                    labelWidth = qrQg.LabelWidth ?? 150;

                cellLabel.Attributes.Add("style", string.Format("height:1px;width:{0}px;", labelWidth));
                table.Rows.Add(tblRow);
            }

            // Caption
            if (IsUseQuestionGroupCaption)
            {
                // Add Group Label
                table.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = qrQg.QuestionGroupName
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }

            var questionRows = QuestionDataTable(questionGroupID).Select();

            InitializedQuestion(questionRows, table, formulas);
            pnlQuestion.Controls.Add(table);

            ////Generate Formula Script
            //if (!IsPostBack)
            //{
            //    var script = new StringBuilder();
            //    script.AppendLine("<script type='text/javascript' language='javascript'>");
            //    script.AppendLine("function fillFormulaField(){");

            //    foreach (DictionaryEntry dictionaryEntry in formulas)
            //    {

            //        string id = dictionaryEntry.Key.ToString();

            //        // [200.020]/(([200.030]/100)*([200.030]/100))
            //        string formula = dictionaryEntry.Value.ToString();
            //        formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
            //        formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
            //        formula = formula.Replace("]", "').get_value()");
            //        script.AppendFormat("var value{0}={1};", id, formula);
            //        script.AppendLine();
            //        script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
            //    }
            //    script.AppendLine();
            //    script.AppendLine("}</script>");
            //    Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            //}
        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, Hashtable formulas)
        {
            foreach (DataRow dataRow in questionRows)
            {
                if (!dataRow["Formula"].ToString().Equals(string.Empty))
                {
                    formulas.Add(dataRow["QuestionID"].ToString().Replace('.', '_'), dataRow["Formula"].ToString());
                }
                if (!string.IsNullOrEmpty(dataRow["SRAnswerType"].ToString()))
                {
                    var tblRow = new TableRow();

                    //Create 2 Cell
                    tblRow.Cells.Add(new TableCell());
                    tblRow.Cells.Add(new TableCell());

                    var cellLabel = tblRow.Cells[0];
                    var cellEntry = tblRow.Cells[1];

                    cellLabel.Attributes.Add("class", "label");

                    InitializedRowCellQuestion(dataRow, cellLabel, cellEntry);

                    groupTable.Rows.Add(tblRow);
                }
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == dataRow["QuestionID"], quest.SRAnswerType != string.Empty);
                quest.OrderBy(quest.IndexNo.Ascending, quest.QuestionID.Ascending);
                var dtbSubQuestion = quest.LoadDataTable();

                if (dtbSubQuestion.Rows.Count > 0)
                {
                    InitializedQuestion(dtbSubQuestion.Select(), groupTable, formulas);
                }
            }
        }

        private void InitializedRowCellQuestion(DataRow dtrQuestion, TableCell cellLabel, TableCell cellEntry)
        {
            string answerType = dtrQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(dtrQuestion["QuestionID"].ToString());


            var litSep = new Literal();
            switch (answerType)
            {
                case "YSN":
                    {
                        // Yes No Option
                        AddLabel(cellLabel, dtrQuestion);
                        var rbl = RadioButtonListYesNo(controlID, dtrQuestion["AnswerWidth"].ToInt(),
                            dtrQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToStringDefaultEmpty());
                        cellEntry.Controls.Add(rbl);
                        break;
                    }
                case "YNT":
                    {
                        // Yes No Option + Text
                        AddLabel(cellLabel, dtrQuestion);
                        var rbl = RadioButtonListYesNo(controlID, dtrQuestion["AnswerWidth"].ToInt(),
                            dtrQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToStringDefaultEmpty());
                        cellEntry.Controls.Add(rbl);

                        litSep = new Literal();
                        litSep.Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]);
                        cellEntry.Controls.Add(litSep);
                        var txt = TextBoxControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt(), string.Empty);
                        cellEntry.Controls.Add(txt);

                        break;
                    }
                case "MSK":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var msk = MaskedTextBoxControl(controlID, dtrQuestion["AnswerWidth"].ToInt(),
                            dtrQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty());
                        cellEntry.Controls.Add(msk);
                        CreateValidationControl(msk.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "DAT":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var dat = DatePickerControl(controlID, dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(dat);
                        break;
                    }
                case "DTM":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var dtm = DateTimePickerControl(controlID, dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(dtm);
                        break;
                    }
                case "TIM":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var tim = TimePickerControl(controlID, dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(tim);
                        break;
                    }
                case "LBL":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        break;
                    }
                case "NUM":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var num = RadNumericTextBoxControl(controlID,
                            int.Parse(dtrQuestion["AnswerDecimalDigit"].ToString()),
                            dtrQuestion["AnswerSuffix"].ToString(), dtrQuestion["AnswerWidth"].ToInt(),
                            dtrQuestion["Formula"].ToString());
                        cellEntry.Controls.Add(num);
                        CreateValidationControl(num.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "MEM":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var mem = MemoControl(controlID, dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(mem);
                        CreateValidationControl(mem.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "TXT":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var txt = TextBoxControl(controlID, dtrQuestion["AnswerWidth"].ToInt(),
                            dtrQuestion["Formula"].ToString());

                        if (!string.IsNullOrEmpty(dtrQuestion["AnswerSuffix"].ToString()))
                        {
                            litSep = new Literal();
                            litSep.Text = "&nbsp;&nbsp;" + dtrQuestion["AnswerSuffix"].ToString();
                            cellEntry.Controls.Add(litSep);

                            var tab = new HtmlTable() { ID = "tab_" + controlID, CellSpacing = 0, CellPadding = 0 };

                            var row = new HtmlTableRow();
                            row.Cells.Add(new HtmlTableCell());
                            row.Cells.Add(new HtmlTableCell());

                            row.Cells[0].Controls.Add(txt);
                            row.Cells[1].Controls.Add(litSep);

                            tab.Rows.Add(row);

                            cellEntry.Controls.Add(tab);
                        }
                        else
                            cellEntry.Controls.Add(txt);

                        CreateValidationControl(txt.ID, "", dtrQuestion, cellEntry);

                        break;
                    }
                case "CBO":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var cbo = ComboBoxControl(controlID, dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cbo);
                        CreateValidationControl(cbo.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "CHK":
                    {
                        var chk = CheckBoxControl(controlID, dtrQuestion["QuestionText"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        var level = dtrQuestion["QuestionLevel"].ToInt();
                        if (level > 0)
                            chk.Style.Add("margin-left", string.Format("{0}px", level * WidthOneLevel));
                        cellEntry.Controls.Add(chk);
                        CreateValidationControl(chk.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "TTX":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var txt1 = TextBoxControl(controlID, dtrQuestion["AnswerWidth"].ToInt(), string.Empty);
                        cellEntry.Controls.Add(txt1);
                        litSep = new Literal { Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]) };
                        cellEntry.Controls.Add(litSep);
                        var txt2 = TextBoxControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt(), string.Empty);
                        cellEntry.Controls.Add(txt2);
                        CreateValidationControl(txt1.ID, "1", dtrQuestion, cellEntry);
                        CreateValidationControl(txt2.ID, "2", dtrQuestion, cellEntry);
                        break;
                    }
                case "CCB":
                    {
                        var ccb1 = CheckBoxControl(controlID, dtrQuestion["QuestionText"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        var level = dtrQuestion["QuestionLevel"].ToInt();
                        if (level > 0)
                            ccb1.Style.Add("margin-left", string.Format("{0}px", level * WidthOneLevel));
                        cellLabel.Controls.Add(ccb1);

                        var ccb2 = ComboBoxControl(controlID + "_2", dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(ccb2);
                        CreateValidationControl(ccb2.ID, "", dtrQuestion, cellEntry);
                        break;
                    }
                case "CTX":
                    {
                        var ctxChk = CheckBoxControl(controlID, dtrQuestion["QuestionText"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellLabel.Controls.Add(ctxChk);
                        litSep = new Literal { Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]) };
                        cellEntry.Controls.Add(litSep);
                        var ctxTxt = TextBoxControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt(), string.Empty);
                        cellEntry.Controls.Add(ctxTxt);
                        break;
                    }
                case "CTM":
                    {
                        var ctmChk = CheckBoxControl(controlID, dtrQuestion["QuestionText"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(ctmChk);
                        litSep = new Literal();
                        litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]);
                        cellEntry.Controls.Add(litSep);
                        var ctmTxt = MemoControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt());
                        cellEntry.Controls.Add(ctmTxt);
                        break;
                    }
                case "CNM":
                    {
                        var cnmChk = CheckBoxControl(controlID, dtrQuestion["QuestionText"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cnmChk);
                        var cnmNum = RadNumericTextBoxControl(controlID + "_2",
                            int.Parse(dtrQuestion["AnswerDecimalDigit"].ToString()),
                            dtrQuestion["AnswerSuffix"].ToString(), dtrQuestion["AnswerWidth2"].ToInt(),
                            string.Empty);
                        cellEntry.Controls.Add(cnmNum);
                        break;
                    }
                case "CB2":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var cbo1 = ComboBoxControl(controlID, dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cbo1);
                        litSep = new Literal();
                        litSep.Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]);
                        cellEntry.Controls.Add(litSep);
                        var cbo2 = ComboBoxControl(controlID + "_2", dtrQuestion["QuestionAnswerSelectionID2"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID2"].ToString(),
                            dtrQuestion["AnswerWidth2"].ToInt());
                        cellEntry.Controls.Add(cbo2);
                        CreateValidationControl(cbo1.ID, "1", dtrQuestion, cellEntry);
                        CreateValidationControl(cbo2.ID, "2", dtrQuestion, cellEntry);
                        break;
                    }
                case "CBT":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var cbCbo = ComboBoxControl(controlID, dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cbCbo);
                        litSep = new Literal();
                        litSep.Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]);
                        cellEntry.Controls.Add(litSep);
                        var cbTxt = TextBoxControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt(), string.Empty);
                        cellEntry.Controls.Add(cbTxt);
                        CreateValidationControl(cbCbo.ID, "1", dtrQuestion, cellEntry);
                        CreateValidationControl(cbTxt.ID, "2", dtrQuestion, cellEntry);
                        break;
                    }
                case "CBN":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var cbCbn = ComboBoxControl(controlID, dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cbCbn);
                        litSep = new Literal { Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]) };
                        cellEntry.Controls.Add(litSep);
                        var cbnNum = RadNumericTextBoxControl(controlID + "_2",
                            int.Parse(dtrQuestion["AnswerDecimalDigit"].ToString()),
                            dtrQuestion["AnswerSuffix"].ToString(), dtrQuestion["AnswerWidth2"].ToInt(),
                            string.Empty);
                        cellEntry.Controls.Add(cbnNum);
                        break;
                    }
                case "CBM":
                    {
                        AddLabel(cellLabel, dtrQuestion);
                        var cbCbm = ComboBoxControl(controlID, dtrQuestion["QuestionAnswerSelectionID"].ToString(),
                            dtrQuestion["QuestionAnswerDefaultSelectionID"].ToString(), dtrQuestion["AnswerWidth"].ToInt());
                        cellEntry.Controls.Add(cbCbm);
                        litSep = new Literal();
                        litSep.Text = string.Format("&nbsp;{0}&nbsp;", dtrQuestion["AnswerPrefix"]);
                        cellEntry.Controls.Add(litSep);
                        var cbTxm = MemoControl(controlID + "_2", dtrQuestion["AnswerWidth2"].ToInt());
                        cellEntry.Controls.Add(cbTxm);
                        CreateValidationControl(cbCbm.ID, "1", dtrQuestion, cellEntry);
                        CreateValidationControl(cbTxm.ID, "2", dtrQuestion, cellEntry);
                        break;
                    }
            }
            // TBL dan DENT tidak disupport saat ini
        }


        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc)
        {
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionID"].ToString() + unique);
                rfv.ControlToValidate = ctlToValidate;
                rfv.ErrorMessage = string.Format("Field {0} Required!", rowQuestion["QuestionText"]);
                rfv.SetFocusOnError = true;
                System.Web.UI.WebControls.Image myImg = new System.Web.UI.WebControls.Image();
                myImg.Visible = true;
                myImg.SkinID = "rfvImage";
                rfv.Controls.Add(myImg);

                tc.Controls.Add(rfv);
            }
        }

        private HtmlTableCell AddTableCell(string controlID)
        {
            var txt = new RadTextBox() { ID = controlID, Width = Unit.Pixel(35), MaxLength = 2 };
            txt.Style["text-align"] = "center";
            txt.Style["font-weight"] = "bold";

            var cell = new HtmlTableCell();
            cell.Controls.Add(txt);
            return cell;
        }
        private HtmlTableCell AddTableCellStandard(string text)
        {
            var cell = new HtmlTableCell();
            cell.Style["border-bottom-style"] = "solid";
            cell.InnerText = text;
            return cell;
        }

        private void AddLabel(TableCell cellLabel, DataRow dtrQuestion)
        {
            if (dtrQuestion["QuestionLevel"].ToInt() > 0)
            {
                cellLabel.Attributes.Add("style", string.Format("padding-left:{0}px;", 10 * dtrQuestion["QuestionLevel"].ToInt()));
            }

            cellLabel.Text = dtrQuestion["QuestionText"].ToString();
        }

        #region Entry Control
        private RadioButtonList RadioButtonListYesNo(string id, int width, string selectedValue, string defaultValue)
        {
            var opt = new RadioButtonList();
            opt.ID = id;
            opt.Width = Unit.Pixel(width == 0 ? 90 : width);
            opt.RepeatDirection = RepeatDirection.Horizontal;
            opt.Items.Add(new ListItem() { Text = "Yes", Value = "Y" });
            opt.Items.Add(new ListItem() { Text = "No", Value = "N" });

            if (!string.IsNullOrWhiteSpace(selectedValue))
            {
                opt.SelectedValue = selectedValue;
            }

            // defaultValue jangan dipakai supaya isian adalah benar2 user yg memilih
            //else if (!string.IsNullOrWhiteSpace(defaultValue))
            //{
            //    opt.SelectedValue = defaultValue;
            //}
            return opt;
        }
        private RadDatePicker DatePickerControl(string id, int width)
        {
            var obj = new RadDatePicker();
            obj.ID = id;
            obj.SelectedDate = (new DateTime()).NowAtSqlServer();
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private RadDatePicker DateTimePickerControl(string id, int width)
        {
            var obj = new RadDateTimePicker();
            obj.ID = id;
            obj.SelectedDate = (new DateTime()).NowAtSqlServer();
            obj.Width = Unit.Pixel(width == 0 ? 150 : width);
            return obj;
        }
        private RadTimePicker TimePickerControl(string id, int width)
        {
            var obj = new RadTimePicker();
            obj.ID = id;
            obj.SelectedDate = (new DateTime()).NowAtSqlServer();
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }

        private CheckBox CheckBoxControl(string id, string text, string defaultSelectionID, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            if (width > 0)
                chk.Width = Unit.Pixel(width);
            chk.Text = text;
            if (!string.IsNullOrEmpty(defaultSelectionID))
                chk.Checked = true;
            return chk;
        }
        private RadTextBox TextBoxControl(string id, int width, string formula)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnValueChanged = "fillFormulaField";
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            return textBox;
        }
        private RadTextBox MemoControl(string id, int width)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            //textBox.Height = Unit.Pixel(100);
            textBox.TextMode = InputMode.MultiLine;
            return textBox;
        }
        private RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width, string formula)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;")
                                                       ? string.Empty
                                                       : string.Format("n {0}", suffix);
            textBox.Value = 0;
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            return textBox;
        }
        private RadMaskedTextBox MaskedTextBoxControl(string id, int width, string mask)
        {
            var textBox = new RadMaskedTextBox();
            textBox.ID = id;
            textBox.Mask = mask;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width)
        {
            var comboBox = new RadComboBox();
            comboBox.ID = id;
            comboBox.Width = Unit.Pixel(width == 0 ? 304 : width);
            var query = new QuestionAnswerSelectionLineQuery();
            query.Where(query.QuestionAnswerSelectionID == selectionID);
            query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
            DataTable dtb = query.LoadDataTable();
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(),
                                                       row["QuestionAnswerSelectionLineID"].ToString()));
            }

            if (!string.IsNullOrEmpty(defaultSelectionID))
                comboBox.SelectedValue = defaultSelectionID;

            return comboBox;
        }
        #endregion
    }
}