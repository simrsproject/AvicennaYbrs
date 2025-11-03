using DevExpress.Web.Internal.XmlProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.CustomControl.Phr.InputControl;
using Temiang.Avicenna.Module.Emr.Phr;
using Temiang.Avicenna.Module.RADT.Cpoe;
using Temiang.Avicenna.WebService;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl.Phr
{
    /// <summary>
    /// Patient Health Record Control
    /// </summary>
    /// 18 Sept 2023 Handono
    /// - Autosize all textarea (TextBox Multiline)
    /// - Replace RadTextBox with TextBox because autosize function conflict with RadTextBox focus, blur, & mouse event
    /// - Add webcam control
    /// 
    /// 02 Des 2023 Handono
    /// - Add fitur Load template from NursingDiagnosaTemplateDetail
    /// - AnswerType RBT -> penerapan selectionID.Contains("[RANGE_")
    /// - TuneUp load Question dengan cara menghilangkan looping query child question 
    public partial class PhrCtl : System.Web.UI.UserControl
    {
        private const int _spacer = 10;
        private CultureInfo _dateCultureInfo = AppConstant.DisplayFormat.DateCultureInfo;
        private CultureInfo _numericCultureInfo = AppConstant.DisplayFormat.NumericCultureInfo;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region PatientHealthRecordLine
        private string TransactionNo
        {
            get { return ViewState["trno"].ToString(); }
            set { ViewState["trno"] = value; }
        }
        private string QuestionFormID
        {
            get { return ViewState["fid"].ToString(); }
            set { ViewState["fid"] = value; }
        }
        private bool IsFromNursingDiagTemplate
        {
            get
            {
                if (ViewState["indt"] == null) return false;
                return Convert.ToBoolean(ViewState["indt"]);
            }
            set { ViewState["indt"] = value; }
        }
        protected string RegistrationNo
        {
            get { return hdnRegistrationNo.Value; }
            set { hdnRegistrationNo.Value = value; }
        }
        protected string FromRegistrationNo
        {
            get { return hdnFromRegistrationNo.Value; }
            set { hdnFromRegistrationNo.Value = value; }
        }
        protected string PatientID
        {
            get { return hdnPatientID.Value; }
            set { hdnPatientID.Value = value; }
        }

        private string QuestionControlID(string questionGroupID, string questionID)
        {
            //return string.Format("q_{0}_{1}", questionGroupID.Replace('.', '_'), questionID.Replace('.', '_')); // Jangan dulu krn di askep belum ditambah field questionGroupID (BangTe)
            return string.Format("q_{0}", questionID.Replace('.', '_'));
        }
        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        public void PopulateValue(Patient pat, Registration reg,
            Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr,
            string lastRegistrationNo)
        {
            TransactionNo = phr.TransactionNo;
            RegistrationNo = string.IsNullOrWhiteSpace(phr.RegistrationNo) ? reg.RegistrationNo : phr.RegistrationNo;
            QuestionFormID = phr.QuestionFormID;
            FromRegistrationNo = reg.FromRegistrationNo;
            PatientID = pat.PatientID;

            var lineValues = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrWhiteSpace(TransactionNo))
            {
                // Edit
                lineValues = LoadHistoryValue(TransactionNo, RegistrationNo, QuestionFormID);
            }
            else
            {
                // New
                lineValues = LoadDefaultValue(pat, reg, othRelatedEntities, phr);
            }

            PopulateValue(pat, reg, phr, lastRegistrationNo, lineValues);
        }

        public void PopulateValue(Patient pat, Registration reg, PatientHealthRecord phr, string lastRegistrationNo, PatientHealthRecordLineCollection lineValues)
        {
            TransactionNo = phr.TransactionNo;
            RegistrationNo = string.IsNullOrWhiteSpace(phr.RegistrationNo) ? reg.RegistrationNo : phr.RegistrationNo;
            QuestionFormID = phr.QuestionFormID;
            FromRegistrationNo = reg.FromRegistrationNo;
            PatientID = pat.PatientID;

            foreach (var line in lineValues)
            {
                string controlID = QuestionControlID(line.QuestionGroupID, line.QuestionID);
                string answerType = line.SRAnswerType;
                object obj;

                obj = Helper.FindControlRecursive(this, controlID);
                if (obj == null)
                    if (answerType != "DNT") continue;

                switch (answerType)
                {
                    case "MSK":
                        {
                            var msk = (obj as RadMaskedTextBox);
                            if (msk != null)
                                msk.Text = HtmlTagHelper.Devalidate(line.QuestionAnswerText.ToStringDefaultEmpty());
                            break;
                        }
                    case "DAT":
                        {
                            var dt = (obj as RadDatePicker);
                            if (dt != null)
                                if (string.IsNullOrEmpty(line.QuestionAnswerText))
                                    dt.Clear();
                                else
                                    dt.SelectedDate = Convert.ToDateTime(line.QuestionAnswerText);
                            break;
                        }
                    case "DTM":
                    case "ADT":
                        {
                            var dtm = (obj as RadDateTimePicker);
                            if (dtm != null)
                                if (string.IsNullOrEmpty(line.QuestionAnswerText))
                                    dtm.Clear();
                                else
                                    dtm.SelectedDate = Convert.ToDateTime(line.QuestionAnswerText);
                            break;
                        }
                    case "TIM":
                        {
                            var tm = (obj as RadTimePicker);
                            if (tm != null)
                                if (string.IsNullOrEmpty(line.QuestionAnswerText))
                                    tm.Clear();
                                else
                                    tm.SelectedDate = Convert.ToDateTime(line.QuestionAnswerText);
                            break;
                        }
                    case "NUM":
                        {
                            var num = (obj as RadNumericTextBox);
                            if (num != null)
                                num.Value = line.QuestionAnswerNum.ToDouble();
                            break;
                        }
                    case "TXT":
                    case "ABY":
                    case "MEM":
                        TextBoxTextSet(obj, line.QuestionAnswerText);
                        break;
                    case "CBR": // Combobox dg list item dari AppStandardReference
                    case "CBO": // Combobox dg list item dari QuestionAnswerSelectionLine
                        {
                            var cbo = (obj as RadComboBox);
                            if (cbo != null)
                                ComboBox.SelectedValue(cbo, HtmlTagHelper.Devalidate(line.QuestionAnswerSelectionLineID));
                            break;
                        }
                    case "RBT":
                        {
                            var rbl = (obj as RadioButtonList);
                            if (rbl != null && !string.IsNullOrEmpty(line.QuestionAnswerSelectionLineID)) rbl.SelectedValue = line.QuestionAnswerSelectionLineID;
                            break;
                        }
                    case "CBL": // Combobox dg list item dari web service ComboBoxDataService
                        {
                            var cbo = (obj as RadComboBox);
                            if (cbo != null)
                            {
                                cbo.Items.Add(new RadComboBoxItem(HtmlTagHelper.Devalidate(line.QuestionAnswerText),
                                    HtmlTagHelper.Devalidate(line.QuestionAnswerSelectionLineID)));
                            }
                            break;
                        }
                    case "CHK":
                        {
                            var chk = (obj as CheckBox);
                            if (chk != null)
                                chk.Checked = "1".Equals(line.QuestionAnswerText);
                            break;
                        }
                    case "CTX":
                        {
                            var ctxValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ctxValue))
                            {
                                var ctxValues = ctxValue.ToString().Split('|');
                                if (ctxValues.Length > 0 && ctxValues[0] != null)
                                {
                                    var ctxChk = (obj as CheckBox);
                                    if (ctxChk != null)
                                        ctxChk.Checked = "1".Equals(ctxValues[0]);
                                }

                                if (ctxValues.Length > 1 && ctxValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    TextBoxTextSet(obj, ctxValues[1]);
                                }
                            }

                            break;
                        }
                    case "CTM":
                        {
                            var ctmValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ctmValue))
                            {
                                var ctmValues = ctmValue.ToString().Split('|');
                                if (ctmValues.Length > 0 && ctmValues[0] != null)
                                {
                                    var ctxChk = (obj as CheckBox);
                                    if (ctxChk != null)
                                        ctxChk.Checked = "1".Equals(ctmValues[0]);
                                }

                                if (ctmValues.Length > 1 && ctmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    //var ctxTxt = (obj as RadTextBox);
                                    //if (ctxTxt != null)
                                    //    ctxTxt.Text = HtmlTagHelper.Devalidate(ctmValues[1]);

                                    TextBoxTextSet(obj, ctmValues[1]);
                                }
                            }

                            break;
                        }
                    case "CNM":
                        {
                            var cnmValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cnmValue))
                            {
                                var cnmValues = cnmValue.ToString().Split('|');
                                if (cnmValues.Length > 0 && cnmValues[0] != null)
                                {
                                    var ctxChk = (obj as CheckBox);
                                    if (ctxChk != null)
                                        ctxChk.Checked = "1".Equals(cnmValues[0]);
                                }

                                if (cnmValues.Length > 1 && cnmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    var cnmNum = (obj as RadNumericTextBox);
                                    if (cnmNum != null)
                                        if (!string.IsNullOrEmpty(cnmValues[1]) && !cnmValues[1].Equals("&nbsp;"))
                                            cnmNum.Value = Convert.ToDouble(cnmValues[1]);
                                }
                            }

                            break;
                        }
                    case "CDT":
                        {
                            var cdtValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cdtValue))
                            {
                                var cdtValues = cdtValue.ToString().Split('|');
                                if (cdtValues.Length > 0 && cdtValues[0] != null)
                                {
                                    var ctxChk = (obj as CheckBox);
                                    if (ctxChk != null)
                                        ctxChk.Checked = "1".Equals(cdtValues[0]);
                                }

                                if (cdtValues.Length > 1 && cdtValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    var cdtDattim = (obj as RadDateTimePicker);
                                    if (cdtDattim != null)
                                        if (!string.IsNullOrEmpty(cdtValues[1]) && !cdtValues[1].Equals("&nbsp;"))
                                            cdtDattim.SelectedDate = Convert.ToDateTime(cdtValues[1]);
                                }
                            }

                            break;
                        }
                    case "CBT":
                        {
                            var cbtValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbtValue))
                            {
                                var cbtValues = cbtValue.ToString().Split('|');
                                if (cbtValues.Length > 0 && cbtValues[0] != null)
                                {
                                    var cbtCbo = (obj as RadComboBox);
                                    if (cbtCbo != null)
                                        cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(line.QuestionAnswerSelectionLineID);
                                }

                                if (cbtValues.Length > 1 && cbtValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    //var cbtTxt = (obj as RadTextBox);
                                    //if (cbtTxt != null)
                                    //    cbtTxt.Text = HtmlTagHelper.Devalidate(cbtValues[1]);
                                    TextBoxTextSet(obj, cbtValues[1]);
                                }
                            }

                            break;
                        }
                    case "CBN":
                        {
                            var cbnValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbnValue))
                            {
                                var cbnValues = cbnValue.ToString().Split('|');
                                if (cbnValues.Length != null && cbnValues[0] != null)
                                {
                                    var cbnCbo = (obj as RadComboBox);
                                    if (cbnCbo != null)
                                        cbnCbo.SelectedValue = HtmlTagHelper.Devalidate(line.QuestionAnswerSelectionLineID);
                                }

                                if (cbnValues.Length > 1 && cbnValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    var cbnNum = (obj as RadNumericTextBox);
                                    if (cbnNum != null)
                                        if (!string.IsNullOrEmpty(cbnValues[1]) && !cbnValues[1].Equals("&nbsp;"))
                                            cbnNum.Value = Convert.ToDouble(cbnValues[1]);
                                }
                            }

                            break;
                        }
                    case "CBM":
                        {
                            var cbmValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(cbmValue))
                            {
                                var cbmValues = cbmValue.ToString().Split('|');
                                if (cbmValues.Length > 0 && cbmValues[0] != null)
                                {
                                    var cbtCbo = (obj as RadComboBox);
                                    if (cbtCbo != null)
                                        cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(line.QuestionAnswerSelectionLineID);
                                }

                                if (cbmValues.Length > 1 && cbmValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    //var cbtTxt = (obj as RadTextBox);
                                    //if (cbtTxt != null)
                                    //    cbtTxt.Text = HtmlTagHelper.Devalidate(cbmValues[1]);

                                    TextBoxTextSet(obj, cbmValues[1]);
                                }
                            }

                            break;
                        }
                    case "CB2":
                        {
                            var cb2Value = line.QuestionAnswerSelectionLineID;
                            if (!string.IsNullOrEmpty(cb2Value))
                            {
                                var cb2Values = cb2Value.ToStringDefaultEmpty().Split('|');
                                if (cb2Values.Length > 0 && cb2Values[0] != null)
                                {
                                    var cbo1 = (obj as RadComboBox);
                                    if (cbo1 != null)
                                        cbo1.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[0]);
                                    ;
                                }

                                if (cb2Values.Length > 1 && cb2Values[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    var cbo2 = (obj as RadComboBox);
                                    if (cbo2 != null)
                                        cbo2.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[1]);
                                }
                            }

                            break;
                        }
                    case "TTX":
                        {
                            var ttxValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ttxValue))
                            {
                                var ttxValues = ttxValue.ToString().Split('|');
                                if (ttxValues.Length > 0 && ttxValues[0] != null)
                                {
                                    //var txt = (obj as RadTextBox);
                                    //if (txt != null)
                                    //    txt.Text = HtmlTagHelper.Devalidate(ttxValues[0]);

                                    TextBoxTextSet(obj, ttxValues[0]);
                                }

                                if (ttxValues.Length > 1 && ttxValues[1] != null)
                                {
                                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                                    //var txt2 = (obj as RadTextBox);
                                    //if (txt2 != null)
                                    //    txt2.Text = HtmlTagHelper.Devalidate(ttxValues[1]);

                                    TextBoxTextSet(obj, ttxValues[1]);
                                }
                            }

                            break;
                        }
                    case "TBL":
                        {
                            var tblValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(tblValue))
                            {
                                var tblValues = tblValue.ToString().Split('|');

                                // find table
                                var tbl = (HtmlTable)Helper.FindControlRecursive(
                                    this,
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
                                                this,
                                                controlID + "_" + r.ToString() + "_" + c.ToString());

                                            if (objCell is RadNumericTextBox)
                                            {
                                                var objCellText = (objCell as RadNumericTextBox);
                                                var cellValIndex = c % cCount;
                                                if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                                {
                                                    objCellText.Value = System.Convert.ToDouble(
                                                        HtmlTagHelper.Devalidate(
                                                            tblValues[cellValIndex + (cCount * (r - 1))]));
                                                }
                                            }
                                            else if (objCell is TextBox)
                                            {
                                                var objCellText = (objCell as TextBox);
                                                var cellValIndex = c % cCount;
                                                if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                                {
                                                    objCellText.Text =
                                                        HtmlTagHelper.Devalidate(
                                                            tblValues[cellValIndex + (cCount * (r - 1))]);
                                                }
                                            }
                                            else if (objCell is RadTextBox)
                                            {
                                                var objCellText = (objCell as RadTextBox);
                                                var cellValIndex = c % cCount;
                                                if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                                {
                                                    objCellText.Text =
                                                        HtmlTagHelper.Devalidate(
                                                            tblValues[cellValIndex + (cCount * (r - 1))]);
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                            break;
                        }
                    case "DNT": //Dental Control
                        {
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText) && line.QuestionAnswerText.Contains(";") && line.QuestionAnswerText.Contains("|"))
                            {
                                var dnt = line.QuestionAnswerText.ToString().Split(';');

                                foreach (var str in dnt.Where(d => !string.IsNullOrEmpty(d))
                                    .Select(d => new
                                    {
                                        controlID = d.Split('|')[0],
                                        value = d.Split('|')[1]
                                    }))
                                {
                                    obj = Helper.FindControlRecursive(this, str.controlID);
                                    //var txt2 = (obj as RadTextBox);
                                    //txt2.Text = str.value;
                                    TextBoxTextSet(obj, str.value);
                                }
                            }

                            break;
                        }
                    default:
                        {
                            var appControl = new AppControl();
                            if (appControl.LoadByPrimaryKey(answerType))
                            {
                                var phrCtl = (obj as BasePhrCtl);
                                phrCtl.PopulateEntryControl(pat, reg, phr, line, lastRegistrationNo);
                            }

                            break;
                        }
                }
            }
        }

        #region Load Default Value
        private PatientHealthRecordLineCollection LoadDefaultValue(Patient patient, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr)
        {
            // Ambil default value
            var hrLineColl = new PatientHealthRecordLineCollection();

            // Load virtual field
            var qrLine = new PatientHealthRecordLineQuery("a");
            qrLine.Select
            (
                qrLine,
                "<'12345' AS refToQuestion_QuestionText>",
                "<'12345' as refToQuestion_SRAnswerType>"
            );
            qrLine.es.Top = 0;
            hrLineColl.Load(qrLine);


            var qdvColl = new QuestionDefaultValueCollection();
            qdvColl.Query.Where(qdvColl.Query.QuestionFormID == QuestionFormID);
            qdvColl.LoadAll();
            foreach (QuestionDefaultValue dv in qdvColl)
            {
                var lastVal = new PatientHealthRecordLine();
                var qr = new PatientHealthRecordLineQuery("l");
                var qrReg = new RegistrationQuery("a");
                qr.InnerJoin(qrReg).On(qr.RegistrationNo == qrReg.RegistrationNo);
                qr.Where(qrReg.PatientID == patient.PatientID);

                if (!string.IsNullOrWhiteSpace(dv.FromQuestionFormID))
                    qr.Where(qr.QuestionFormID == dv.FromQuestionFormID);

                if (!string.IsNullOrWhiteSpace(dv.FromQuestionGroupID))
                    qr.Where(qr.QuestionGroupID == dv.FromQuestionGroupID);

                if (!string.IsNullOrWhiteSpace(dv.FromQuestionID))
                    qr.Where(qr.QuestionID == dv.FromQuestionID);

                if (true.Equals(dv.IsFromCurrentRegistration))
                    qr.Where(qr.Or(qr.RegistrationNo == reg.RegistrationNo, qr.RegistrationNo == reg.FromRegistrationNo));

                qr.es.Top = 1;
                qr.OrderBy(qr.TransactionNo.Descending);

                if (lastVal.Load(qr))
                {
                    var quest = new Question();
                    quest.LoadByPrimaryKey(dv.QuestionID);
                    var phrLine = hrLineColl.AddNew();
                    phrLine.QuestionGroupID = dv.QuestionGroupID;
                    phrLine.QuestionID = dv.QuestionID;
                    phrLine.SRAnswerType = quest.SRAnswerType;
                    phrLine.QuestionAnswerSelectionLineID = lastVal.QuestionAnswerSelectionLineID;
                    phrLine.QuestionAnswerNum = lastVal.QuestionAnswerNum;
                    phrLine.QuestionAnswerText = lastVal.QuestionAnswerText;
                    phrLine.QuestionAnswerText2 = lastVal.QuestionAnswerText2;
                }
            }


            // Default value form Related Table
            if (_questionDtb != null)
            {
                var relatedFields = _questionDtb.Select("RelatedEntityName>'' AND RelatedColumnName>''");
                foreach (DataRow row in relatedFields)
                {
                    var value = GetRelatedFielValue(patient, reg, othRelatedEntities, phr, row);

                    if (value.Value != DBNull.Value)
                    {
                        var phrLine = hrLineColl.AddNew();
                        phrLine.QuestionGroupID = row["QuestionGroupID"].ToString();
                        phrLine.QuestionID = row["QuestionID"].ToString();
                        phrLine.SRAnswerType = row["SRAnswerType"].ToString();

                        if (value.Value is string || value.Value is DateTime)
                        {
                            phrLine.QuestionAnswerText = value.Text.ToString();
                            phrLine.QuestionAnswerSelectionLineID = value.Value.ToString();
                        }
                        else
                            phrLine.QuestionAnswerNum = value.Value.ToDecimal();
                    }
                }
            }

            return hrLineColl;
        }

        internal PatientHealthRecordLineCollection LoadHistoryValue(string transactionNo, string registrationNo, string questionFormID)
        {
            var hrLineColl = new PatientHealthRecordLineCollection();
            var query = new PatientHealthRecordLineQuery("a");
            var qQuest = new QuestionQuery("b");

            query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);

            query.Select
            (
                query,
                "<CASE WHEN b.SRAnswerType = 'DNT' THEN a.QuestionAnswerText2 ELSE a.QuestionAnswerText END AS refToQuestion_QuestionText>",
                qQuest.SRAnswerType.As("refToQuestion_SRAnswerType")
            );
            query.Where
            (
                query.TransactionNo == transactionNo,
                query.RegistrationNo == registrationNo,
                query.QuestionFormID == questionFormID,
                qQuest.SRAnswerType != "LBL"
            );
            query.OrderBy(qQuest.IndexNo.Ascending);

            hrLineColl.Load(query);
            return hrLineColl;
        }
        #endregion

        /// <summary>
        /// Jalankan di OnDataModeChanged
        /// </summary>
        /// <param name="isReadOnly"></param>
        public void SetReadOnlyPatientHealthRecordLine(bool isReadOnly, Patient pat, Registration reg)
        {
            // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                SetReadOnlyPatientHealthRecordLine(isReadOnly, rowQuestion,
                    rowQuestion["QuestionGroupID"].ToString(), pat, reg);
            }
        }

        private void SetReadOnlyPatientHealthRecordLine(bool isReadOnly, DataRow rowQuestion, string questionGroupID, Patient pat, Registration reg)
        {
            // Override isReadOnly jika di Question diset IsReadOnly = true
            if (rowQuestion["IsReadOnly"] != DBNull.Value && Convert.ToBoolean(rowQuestion["IsReadOnly"]))
                isReadOnly = Convert.ToBoolean(rowQuestion["IsReadOnly"]);

            string controlID = QuestionControlID(questionGroupID, rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            Control ctl;

            if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                ctl = Helper.FindControlRecursive(this, controlID);
            else
                ctl = Helper.FindControlRecursive(this, QuestionControlID(rowQuestion["QuestionGroupID"].ToString(),
                    rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

            switch (answerType)
            {
                case "MSK":
                    (ctl as RadMaskedTextBox).ReadOnly = isReadOnly;
                    break;
                case "DAT":
                    var dp = (ctl as RadDatePicker);
                    dp.DatePopupButton.Enabled = !isReadOnly;
                    dp.DateInput.ReadOnly = isReadOnly;
                    break;
                case "DTM":
                case "ADT":
                    var dtp = (ctl as RadDateTimePicker);
                    dtp.DatePopupButton.Enabled = !isReadOnly;
                    dtp.DateInput.ReadOnly = isReadOnly;
                    break;
                case "TIM":
                    var tm = (ctl as RadTimePicker);
                    tm.DatePopupButton.Enabled = !isReadOnly;
                    tm.DateInput.ReadOnly = isReadOnly;
                    break;
                case "NUM":
                    (ctl as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "MEM":
                case "TXT":
                case "ABY":
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "CBR":
                case "CBL":
                case "CBO":
                    (ctl as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "RBT":
                    (ctl as RadioButtonList).Enabled = !isReadOnly;
                    break;
                case "CHK":
                    (ctl as CheckBox).Enabled = !isReadOnly;
                    break;
                case "CTX":
                    (ctl as CheckBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "CTM":
                    (ctl as CheckBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "CNM":
                    (ctl as CheckBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    (ctl as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDT":
                    (ctl as CheckBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    var dt = ctl as RadDateTimePicker;
                    dt.DatePopupButton.Enabled = !isReadOnly;
                    dt.TimePopupButton.Enabled = !isReadOnly;
                    dt.DateInput.ReadOnly = isReadOnly;
                    break;
                case "CBT":
                    (ctl as RadComboBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "CBN":
                    (ctl as RadComboBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    (ctl as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBM":
                    (ctl as RadComboBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "CB2":
                    (ctl as RadComboBox).Enabled = !isReadOnly;

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    (ctl as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "TTX":
                    TextBoxReadOnlySet(isReadOnly, ctl);

                    ctl = Helper.FindControlRecursive(this, controlID + "_2");
                    TextBoxReadOnlySet(isReadOnly, ctl);
                    break;
                case "TBL":
                    var tbl = (ctl as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cList = rowQuestion["QuestionAnswerSelectionID"].ToString().Split('|');
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var cDef = cList[c].Split(',');
                                var isRN = isReadOnly;
                                if (cDef.Length > 4)
                                {
                                    if (cDef[4].ToLower() == "readonly") isRN = true;
                                }
                                var objCell = Helper.FindControlRecursive(
                                    this,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());

                                if (objCell is TextBox)
                                {
                                    (objCell as TextBox).ReadOnly = isRN;
                                }
                                else if (objCell is RadTextBox)
                                {
                                    (objCell as RadTextBox).ReadOnly = isRN;
                                }
                                if (objCell is RadNumericTextBox)
                                {
                                    (objCell as RadNumericTextBox).ReadOnly = isRN;
                                }
                            }
                        }
                    }
                    break;
                case "DNT": //Dental Control
                    for (int i = 8; i >= 1; i--)
                    {
                        //var txt = Helper.FindControlRecursive(this, "txtLU" + i.ToString()) as RadTextBox;
                        //if (txt != null) (txt as RadTextBox).ReadOnly = isReadOnly;

                        //txt = Helper.FindControlRecursive(this, "txtLD" + i.ToString()) as RadTextBox;
                        //if (txt != null) (txt as RadTextBox).ReadOnly = isReadOnly;

                        //txt = Helper.FindControlRecursive(this, "txtRU" + i.ToString()) as RadTextBox;
                        //if (txt != null) (txt as RadTextBox).ReadOnly = isReadOnly;

                        //txt = Helper.FindControlRecursive(this, "txtRD" + i.ToString()) as RadTextBox;
                        //if (txt != null) (txt as RadTextBox).ReadOnly = isReadOnly;

                        var ctlDtl = Helper.FindControlRecursive(this, "txtLU" + i.ToString());
                        TextBoxReadOnlySet(isReadOnly, ctlDtl);

                        ctlDtl = Helper.FindControlRecursive(this, "txtLD" + i.ToString());
                        TextBoxReadOnlySet(isReadOnly, ctlDtl);

                        ctlDtl = Helper.FindControlRecursive(this, "txtRU" + i.ToString());
                        TextBoxReadOnlySet(isReadOnly, ctlDtl);

                        ctlDtl = Helper.FindControlRecursive(this, "txtRD" + i.ToString());
                        TextBoxReadOnlySet(isReadOnly, ctlDtl);
                    }
                    break;
                default:
                    if (ctl is BasePhrCtl)
                    {
                        ((BasePhrCtl)ctl).SetReadOnly(isReadOnly, pat, reg);
                    }
                    break;
            }
        }

        private IEnumerable<QuestionGroup> LoadQuestionGroup(string formID)
        {
            if (ViewState["questionGroup"] != null)
                return ViewState["questionGroup"] as QuestionGroupCollection;
            else
            {
                var query = new QuestionGroupQuery("a");
                var qrQGroupInForm = new QuestionGroupInFormQuery("d");
                query.InnerJoin(qrQGroupInForm).On(query.QuestionGroupID == qrQGroupInForm.QuestionGroupID);
                query.Where(qrQGroupInForm.QuestionFormID == formID);
                query.SelectAll();
                query.OrderBy(qrQGroupInForm.RowIndex.Ascending);

                var coll = new QuestionGroupCollection();
                coll.Load(query);

                ViewState["questionGroup"] = coll;

                return coll;
            }
        }

        private DataTable _questionDtb = null;
        private DataTable QuestionDataTable(string formID)
        {
            if (_questionDtb != null)
                return _questionDtb;

            if (IsFromNursingDiagTemplate)
            {
                return QuestionFromNursingDiagDataTable(formID.ToInt());
            }

            var quest = new QuestionQuery("a");
            var qrQInGroup = new QuestionInGroupQuery("c");
            var qrQGInForm = new QuestionGroupInFormQuery("d");
            quest.InnerJoin(qrQInGroup).On(quest.QuestionID == qrQInGroup.QuestionID);
            quest.InnerJoin(qrQGInForm).On(qrQInGroup.QuestionGroupID == qrQGInForm.QuestionGroupID);
            quest.OrderBy(qrQGInForm.RowIndex.Ascending, qrQInGroup.QuestionGroupID.Ascending, qrQInGroup.RowIndex.Ascending);
            quest.Where(qrQGInForm.QuestionFormID == formID, quest.IsActive == true);

            // Check has child
            var questChild = new QuestionQuery("qc");
            questChild.es.Top = 1;
            questChild.Where(questChild.ParentQuestionID == quest.QuestionID);
            questChild.Select("<CAST('1' as BIT) as IsHasChild>");

            quest.Select
                    (
                    quest.QuestionID, quest.ParentQuestionID, quest.IndexNo,
                    "<ISNULL(c.QuestionLevel,a.QuestionLevel) QuestionLevel>",
                    quest.QuestionText, quest.QuestionShortText,
                    quest.SRAnswerType, quest.AnswerDecimalDigit, quest.AnswerPrefix,
                    quest.AnswerSuffix, quest.IsActive, quest.AnswerWidth,
                    quest.AnswerWidth2, quest.QuestionAnswerSelectionID,
                    quest.QuestionAnswerDefaultSelectionID, quest.QuestionAnswerSelectionID2,
                    quest.QuestionAnswerDefaultSelectionID2, quest.Formula, quest.IsAlwaysPrint,
                    quest.LastUpdateDateTime, quest.LastUpdateByUserID, quest.IsMandatory,
                    quest.ReferenceQuestionID,
                    quest.BodyID,
                    qrQInGroup.QuestionGroupID,
                    qrQInGroup.RowIndex,
                    quest.RelatedEntityName,
                    quest.RelatedColumnName,
                    quest.LookUpID,
                    quest.IsReadOnly,
                    quest.IsUpdateRelatedEntity,
                    "<COALESCE(a.IsEmptyDefault, 0) IsEmptyDefault>",
                    quest.IsNotOverWriteRelatedEntity,
                    quest.EquivalentQuestionID,
                    string.Format("<IsHasChild = COALESCE( ({0}),CAST('0' as BIT))>", questChild.Parse())
                    );

            var dtb = quest.LoadDataTable();
            _questionDtb = dtb;
            return _questionDtb;
        }

        private DataTable QuestionFromNursingDiagDataTable(int templateID)
        {
            if (_questionDtb != null)
                return _questionDtb;

            var quest = new QuestionQuery("a");
            var ndtpl = new NursingDiagnosaTemplateDetailQuery("c");
            quest.InnerJoin(ndtpl).On(quest.QuestionID == ndtpl.QuestionID);
            quest.Where(ndtpl.TemplateID == templateID, quest.IsActive == true);

            // Check has child
            var questChild = new QuestionQuery("qc");
            questChild.es.Top = 1;
            questChild.Where(questChild.ParentQuestionID == quest.QuestionID);
            questChild.Select("<CAST('1' as BIT) as IsHasChild>");

            quest.Select
                    (
                    quest.QuestionID, quest.ParentQuestionID, quest.IndexNo,
                    "<COALESCE(a.QuestionLevel,1) QuestionLevel>",
                    quest.QuestionText, quest.QuestionShortText,
                    quest.SRAnswerType, quest.AnswerDecimalDigit, quest.AnswerPrefix,
                    quest.AnswerSuffix, quest.IsActive, quest.AnswerWidth,
                    quest.AnswerWidth2, quest.QuestionAnswerSelectionID,
                    quest.QuestionAnswerDefaultSelectionID, quest.QuestionAnswerSelectionID2,
                    quest.QuestionAnswerDefaultSelectionID2, quest.Formula, quest.IsAlwaysPrint,
                    quest.LastUpdateDateTime, quest.LastUpdateByUserID, quest.IsMandatory,
                    quest.ReferenceQuestionID,
                    quest.BodyID,
                    "<'' QuestionGroupID>",
                    ndtpl.RowIndex,
                    quest.RelatedEntityName,
                    quest.RelatedColumnName,
                    quest.LookUpID,
                    quest.IsReadOnly,
                    quest.IsUpdateRelatedEntity,
                    "<COALESCE(a.IsEmptyDefault, 0) IsEmptyDefault>",
                    quest.IsNotOverWriteRelatedEntity,
                    quest.EquivalentQuestionID,
                    string.Format("<IsHasChild = COALESCE( ({0}),CAST('0' as BIT))>", questChild.Parse())
                    );
            quest.OrderBy(ndtpl.RowIndex.Ascending);
            var dtb = quest.LoadDataTable();
            _questionDtb = dtb;
            return _questionDtb;
        }


        /// <summary>
        /// Jalankan setiap page load
        /// </summary>
        /// <param name="formID"></param>
        public void InitializedQuestion(string formID, bool isForNursingDiagTemplate = false)
        {
            if (isForNursingDiagTemplate)
            {
                InitializedQuestionFromNursingDiagTemplate(formID.ToInt());
                return;
            }
            QuestionFormID = formID;
            var qf = new QuestionForm();
            qf.LoadByPrimaryKey(formID);

            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            // this.Controls.Clear(); // Jangan dijalankan krn script dan hidden field yg didefinisikan di PhrCtl.ascx akan hilang
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            var isShowNo = questionGroups.Count() > 1;
            var dateTimeNow = (new DateTime()).NowAtSqlServer();
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
                    Text = isShowNo ? string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName) : string.Format("{0}", questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["padding-left"] = "2px";
                cell.Style["height"] = "24px";
                cell.Style["color"] = "white";
                cell.Style["background-color"] = AppSession.Application.Skin == "Default" ? "#a8a8a8" : "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                InitializedQuestion(questionRows, groupTable, row, formulas, questionGroup.QuestionGroupID, dateTimeNow, qf.IsModeMapping ?? false);
                this.Controls.Add(groupTable);
                this.Controls.Add(new Literal() { Text = "<br/>" });
            }

            //Generate Formula Script
            //if (!IsPostBack)
            //{

            // Selalu digenerate krn kalau tidak doi akan error ...bapak mana bapak mana
            var baseClientID = string.Format("{0}_q_", this.ClientID);
            var script = GenerateFormulaScript(baseClientID, formulas);

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            //}
        }

        public void InitializedQuestionFromNursingDiagTemplate(int templateID)
        {
            QuestionFormID = templateID.ToString();
            IsFromNursingDiagTemplate = true;

            // Masih belum ketemu cara ambil urutan group nya
            //var query = new QuestionGroupQuery("a");
            //var qrQGroupInForm = new NursingDiagnosaTemplateDetailQuery("g");
            //query.RightJoin(qrQGroupInForm).On(query.QuestionGroupID == qrQGroupInForm.QuestionGroupID & qrQGroupInForm.TemplateID == templateID);
            //query.Select("<COALESCE(a.QuestionGroupID,'') as QuestionGroupID>", "<COALESCE(a.QuestionGroupName,'') as QuestionGroupName>", "<COALESCE(a.RowIndex,0) as RowIndex>");
            //query.OrderBy("<1,2,3>");
            //query.es.Distinct = true;

            //var questionGroups = new QuestionGroupCollection();
            //questionGroups.Load(query);

            var nd = new NursingDiagnosaTemplate();
            nd.LoadByPrimaryKey(templateID);

            //  Get List Question
            var dtbQuestion = QuestionFromNursingDiagDataTable(templateID);
            ////  Generate Question Entry
            //// this.Controls.Clear(); // Jangan dijalankan krn script dan hidden field yg didefinisikan di PhrCtl.ascx akan hilang
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            var isShowNo = false; // questionGroups.Count() > 1;
            var dateTimeNow = (new DateTime()).NowAtSqlServer();
            //foreach (QuestionGroup questionGroup in questionGroups)
            //{
            //    rowNo++;
            var groupTable = new Table { Width = Unit.Percentage(100) };
            // Add Group Label
            var row = new TableRow();
            groupTable.Rows.Add(row);
            var cell = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Center,
                //Text = isShowNo ? string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName) : string.Format("{0}", questionGroup.QuestionGroupName)
                Text = !string.IsNullOrEmpty(nd.TemplateText) ? nd.TemplateText : nd.TemplateName
            };
            cell.Font.Bold = true;
            cell.Style["padding-left"] = "2px";
            cell.Style["height"] = "24px";
            cell.Style["color"] = "white";
            cell.Style["background-color"] = AppSession.Application.Skin == "Default" ? "#a8a8a8" : "#758DA6";
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);

            groupTable.Rows.Add(row);
            DataRow[] questionRows = dtbQuestion.Select(); //dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

            InitializedQuestion(questionRows, groupTable, row, formulas, string.Empty, dateTimeNow, false);
            this.Controls.Add(groupTable);
            this.Controls.Add(new Literal() { Text = "<br/>" });
            //}

            //Generate Formula Script
            //if (!IsPostBack)
            //{

            // Selalu digenerate krn kalau tidak doi akan error ...bapak mana bapak mana
            var baseClientID = string.Format("{0}_q_", this.ClientID);
            var script = GenerateFormulaScript(baseClientID, formulas);

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            //}

        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, TableRow row, Hashtable formulas, string questionGroupID, DateTime defaultSelecteDateTime, bool isMappingMode)
        {
            foreach (DataRow rowChild in questionRows)
            {
                //-----------------------
                // diperlukan untuk form askep multi hirarki
                var ctlID = QuestionControlID(questionGroupID, rowChild["QuestionID"].ToString());
                var ctlAlreadyCreated = Helper.FindControlRecursive(groupTable, ctlID);
                if (ctlAlreadyCreated != null) continue;
                //-----------------------

                if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                {
                    row = InitializedRowQuestion(rowChild, questionGroupID, defaultSelecteDateTime, formulas, isMappingMode);
                    groupTable.Rows.Add(row);
                }

                // Cek jika memiliki child (Handono 231203)
                if (true.Equals(rowChild["IsHasChild"]))
                {

                    var quest = new QuestionQuery("q");
                    quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                    quest.OrderBy(quest.IndexNo.Ascending);

                    // Check has child
                    var questChild = new QuestionQuery("qc");
                    questChild.es.Top = 1;
                    questChild.Where(questChild.ParentQuestionID == quest.QuestionID);
                    questChild.Select("<CAST('1' as BIT) as IsHasChild>");

                    quest.Select(quest, string.Format("<IsHasChild = COALESCE( ({0}),CAST('0' as BIT))>", questChild.Parse()));
                    var dtbSubQuestion = quest.LoadDataTable();

                    if (dtbSubQuestion.Rows.Count > 0)
                    {
                        InitializedQuestion(dtbSubQuestion.Select(), groupTable, row, formulas, questionGroupID, defaultSelecteDateTime, isMappingMode);
                    }
                }
            }
        }

        public static StringBuilder GenerateFormulaScript(string baseClientID, Hashtable formulas)
        {
            var script = new StringBuilder();
            script.AppendLine("<script type='text/javascript' language='javascript'>");
            script.AppendLine("function fillFormulaField(){");

            foreach (DictionaryEntry dictionaryEntry in formulas)
            {

                string id = dictionaryEntry.Key.ToString();
                if (id == "PureJS") continue;

                // [200.020]/(([200.030]/100)*([200.030]/100))
                string formula = dictionaryEntry.Value.ToString();

                // JS murni
                if (formula.Length > 3)
                {
                    var ident = formula.Substring(0, 3);
                    if (ident == "js:")
                    {
                        formula = Helper.EncodeQuestionID(formula.Substring(3), baseClientID);
                        script.Append(formula);
                        continue;
                    }
                }

                formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
                formula = formula.Replace("[", "$find('" + baseClientID);
                formula = formula.Replace("]", "').get_value()");

                // combobox
                if (formula.Length > 3)
                {
                    var answerType = formula.Substring(0, 3);
                    if (answerType == "CBO" || answerType == "CBR" || answerType == "CBL")
                    {
                        formula = formula.Substring(3);
                        script.AppendFormat("var dd{0} = $find('{1}{0}');", id, baseClientID);
                        script.AppendLine();
                        script.AppendFormat("var value{0}={1};", id, formula);
                        script.AppendLine();
                        script.AppendFormat("var item{0} = dd{0}.findItemByValue(value{0});", id);
                        script.AppendLine();
                        script.AppendFormat("item{0}.select();", id);
                        continue;
                    }
                }

                script.AppendFormat("var value{0}={1};", id, formula);
                script.AppendLine();
                script.AppendFormat("$find('{0}{1}').set_value(value{1});", baseClientID, id);
            }
            script.AppendLine();
            script.AppendLine("}");

            if (formulas.ContainsKey("PureJS"))
            {
                script.AppendLine(formulas["PureJS"].ToString());
            }

            script.AppendLine("</script>");

            return script;
        }

        private void InitializedValidationCtl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc)
        {
            if (rowQuestion["IsMandatory"] != DBNull.Value && (bool)rowQuestion["IsMandatory"])
            {
                if (rowQuestion["SRAnswerType"].ToString().ToUpper() == "CHK")
                {
                    var cv = new CustomValidator();
                    cv.ID = RfvControlID(rowQuestion["QuestionID"].ToString() + unique);
                    cv.ErrorMessage = $"Field {rowQuestion["QuestionText"]} Required!";
                    cv.ValidationGroup = "entry";
                    cv.Attributes["TargetCheckBox"] = ctlToValidate;
                    System.Web.UI.WebControls.Image myImg = new System.Web.UI.WebControls.Image();
                    myImg.Visible = true;
                    myImg.SkinID = "rfvImage";
                    cv.ServerValidate += new ServerValidateEventHandler(CheckBoxRequired_ServerValidate);
                    cv.Controls.Add(myImg);

                    tc.Controls.Add(cv);
                }
                else
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
                }
            }
        }
        protected void CheckBoxRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var cv = (CustomValidator)source;
            string chkID = cv.Attributes["TargetCheckBox"];
            var chk = (CheckBox)cv.NamingContainer.FindControl(chkID);
            args.IsValid = chk != null && chk.Checked;
        }

        private TableRow InitializedRowQuestion(DataRow rowQuestion, string questionGroupID, DateTime defaultSelectedDateTime, Hashtable formulas, bool isModeMapping)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(questionGroupID, rowQuestion["QuestionID"].ToString());

            //Create 3 Cell
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells[0].Attributes["class"] = "label";

            if (isModeMapping)
            {
                if (answerType.ToLower().Trim() != "lbl")
                {
                    //TuneUp by Handono 230327
                    //var qs = new Question();
                    //if (qs.LoadByPrimaryKey(rowQuestion["QuestionID"].ToString()))
                    //{
                    //    var check = (rowQuestion["EquivalentQuestionID"]==DBNull.Value || string.IsNullOrWhiteSpace(rowQuestion["EquivalentQuestionID"].ToString()) ? rowQuestion["QuestionID"] : rowQuestion["EquivalentQuestionID"]).ToString();

                    //    var qID = string.IsNullOrEmpty(qs.EquivalentQuestionID) ? qs.QuestionID : qs.EquivalentQuestionID;

                    //    var isBeda = check.Equals(qID);

                    //    var nadColl = new NursingAssessmentDiagnosaCollection();
                    //    nadColl.Query.Where(nadColl.Query.QuestionID == qID, nadColl.Query.NursingDiagnosaID != string.Empty);
                    //    nadColl.LoadAll();
                    //    tblRow.Cells[2].Text = string.Format("<a href=\"../../../../../Module/RADT/Master/CarePlan/NursingAssessmentQuestion/NursingAssessmentQuestionDetail.aspx?md=view&id={0}\" target=\"_blank\" title=\"{1}\">{2}</a>",
                    //        rowQuestion["QuestionID"].ToString(), rowQuestion["QuestionText"].ToString(), nadColl.Count.ToString());
                    //}


                    var qID = (rowQuestion["EquivalentQuestionID"] == DBNull.Value || string.IsNullOrWhiteSpace(rowQuestion["EquivalentQuestionID"].ToString()) ? rowQuestion["QuestionID"] : rowQuestion["EquivalentQuestionID"]).ToString();
                    var qrNad = new NursingAssessmentDiagnosaQuery("nad");
                    qrNad.Select("<COUNT(1) as RecCount>");
                    qrNad.Where(qrNad.QuestionID == qID, qrNad.NursingDiagnosaID != string.Empty);
                    var dtbCount = qrNad.LoadDataTable();
                    tblRow.Cells[2].Text = string.Format("<a class=\"noti_Container\" href=\"{3}/Module/RADT/Master/CarePlan/NursingAssessmentQuestion/NursingAssessmentQuestionDetail.aspx?md=view&id={0}&of=phr\" target=\"_blank\" title=\"{1}\"><span  class=\"noti_bubble\">{2}</span></a>",
                        rowQuestion["QuestionID"].ToString(), rowQuestion["QuestionText"].ToString(), string.Format("{0}", dtbCount.Rows[0][0]), Helper.UrlRoot());
                }
            }
            else
            {
                tblRow.Cells[2].Visible = false;
            }

            var questionLevel = rowQuestion["QuestionLevel"].ToInt();
            var litSep = new Literal();

            // formulas
            if (!rowQuestion["Formula"].ToString().Equals(string.Empty))
            {
                if (rowQuestion["SRAnswerType"].ToString().ToUpper() == "TBL")
                {
                    // formula for table goes somewhere else vrohh!!
                }
                else
                {
                    formulas.Add(rowQuestion["QuestionID"].ToString().Replace('.', '_'), rowQuestion["Formula"].ToString());
                }
            }

            switch (answerType)
            {
                case "MSK":
                    AddText(rowQuestion, tblRow.Cells[0], questionLevel);
                    var msk = MaskedTextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty());
                    tblRow.Cells[1].Controls.Add(msk);
                    InitializedValidationCtl(msk.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "DAT":
                    AddText(rowQuestion, tblRow.Cells[0], questionLevel);

                    var dat = DatePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), defaultSelectedDateTime, Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dat);

                    if (!string.IsNullOrEmpty(rowQuestion["Formula"].ToString()))
                    {
                        tblRow.Cells[1].Controls.Add(Helper.ActionButtonInput());
                    }

                    break;
                case "DTM":
                case "ADT":
                    AddText(rowQuestion, tblRow.Cells[0], questionLevel);
                    var dtm = DateTimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), defaultSelectedDateTime, Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dtm);
                    break;
                case "TIM":
                    AddText(rowQuestion, tblRow.Cells[0], questionLevel);

                    var tim = TimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), defaultSelectedDateTime, Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(tim);
                    break;
                case "LBL":
                    AddLabel(controlID, tblRow, rowQuestion);
                    break;
                case "NUM":
                    InitializedRowNumeric(rowQuestion, tblRow, controlID);
                    break;
                case "MEM":
                    InitializedRowMemo(rowQuestion, tblRow, controlID);
                    break;
                case "TXT":
                case "ABY":
                    InitializedRowText(rowQuestion, tblRow, controlID);
                    break;
                case "CBL": // Combobox dg list item dari web service ComboBoxDataService
                case "CBR": // Combobox dg list item dari AppStandardReference
                case "CBO": // Combobox dg list item dari QuestionAnswerSelectionLine
                    {
                        AddCaptionLabel(tblRow, rowQuestion);
                        var cbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                            rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                            rowQuestion["Formula"].ToString(), answerType);
                        if (answerType == "CBO")
                        {
                            cbo.AllowCustomText = true;
                            cbo.Filter = RadComboBoxFilter.Contains;
                        }
                        tblRow.Cells[1].Controls.Add(cbo);
                        if (!string.IsNullOrEmpty(rowQuestion["Formula"].ToString()))
                        {
                            tblRow.Cells[1].Controls.Add(Helper.ActionButtonInput());
                        }
                        InitializedValidationCtl(cbo.ID, "", rowQuestion, tblRow.Cells[1]);
                        break;
                    }
                case "RBT":
                    {
                        AddCaptionLabel(tblRow, rowQuestion);

                        var rbl = RadioButtonListControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                            rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());

                        tblRow.Cells[1].Controls.Add(rbl);
                        InitializedValidationCtl(rbl.ID, "", rowQuestion, tblRow.Cells[1]);
                        break;
                    }
                case "CHK":
                    var chk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(chk);
                    InitializedValidationCtl(chk.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TTX":
                    AddText(rowQuestion, tblRow.Cells[0], questionLevel);
                    var txt1 = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(txt1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var txt2 = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(txt2);
                    InitializedValidationCtl(txt1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    InitializedValidationCtl(txt2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CTX":
                    var ctxChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctxChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctxTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctxTxt);
                    break;
                case "CTM":
                    var ctmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctmChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctmTxt = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctmTxt);
                    break;
                case "CNM":
                    var cnmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cnmChk);
                    var cnmNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                             string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cnmNum);
                    break;
                case "CDT":
                    InitializedRowChkDate(rowQuestion, tblRow, controlID, defaultSelectedDateTime);
                    break;
                case "CB2":
                    InitializedRowCboCbo(rowQuestion, tblRow, controlID);
                    break;
                case "CBT":
                    InitializedRowCboText(rowQuestion, tblRow, controlID);
                    break;
                case "CBN":
                    InitializedRowCboNum(rowQuestion, tblRow, controlID);
                    break;
                case "CBM":
                    InitializedRowCboMemo(rowQuestion, tblRow, controlID);
                    break;
                case "TBL":
                    InitializedRowTable(rowQuestion, tblRow, controlID, formulas);
                    break;
                case "DNT": //Dental Control
                    InitializedRowDental(rowQuestion, tblRow);
                    break;
                default:
                    InitializedCustomControl(rowQuestion, tblRow, controlID, answerType);
                    break;
            }
            return tblRow;
        }

        private static void AddText(DataRow rowQuestion, TableCell tblCell, int questionLevel)
        {
            if (questionLevel > 0)
                tblCell.Style.Add("padding-left", string.Format("{0}px", questionLevel * _spacer));
            tblCell.Text = rowQuestion["QuestionText"].ToString();
        }

        private void InitializedRowNumeric(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            AddCaptionLabel(tblRow, rowQuestion);
            var num = RadNumericTextBoxControl(controlID, rowQuestion["AnswerDecimalDigit"].ToInt(),
                rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
            tblRow.Cells[1].Controls.Add(num);
            InitializedValidationCtl(num.ID, "", rowQuestion, tblRow.Cells[1]);
        }

        //private void InitializedRowCheckbox(DataRow rowQuestion, TableRow tblRow, string controlID)
        //{
        //    AddCaptionLabel(tblRow, rowQuestion);
        //    var chk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
        //    tblRow.Cells[1].Controls.Add(chk);
        //    InitializedValidationCtl(chk.ID, "", rowQuestion, tblRow.Cells[1]);
        //}

        private void InitializedRowMemo(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            AddCaptionLabel(tblRow, rowQuestion);

            var mem = MemoControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());

            // Check LookUp Define
            InitializedLookUpLink(rowQuestion, tblRow.Cells[0], mem.ClientID);

            //if (rowQuestion["LookUpID"] != null && !string.IsNullOrWhiteSpace(rowQuestion["LookUpID"].ToString()))
            //{
            //    var lup = string.Format(
            //         "<a style=\"cursor: pointer;\" onclick=\"javascript:ResetValue('{0}', '{1}', '{2}');\"><img src=\"{3}/Images/Toolbar/refresh16.png\" alt=\"\"/></a>",
            //         mem.ClientID, rowQuestion["LookUpID"], rowQuestion["QuestionText"], Helper.UrlRoot());

            //    tblRow.Cells[0].Text = string.Format("{0}&nbsp;&nbsp;{1}", tblRow.Cells[0].Text, lup);
            //}

            tblRow.Cells[1].Controls.Add(mem);
            InitializedValidationCtl(mem.ID, "", rowQuestion, tblRow.Cells[1]);
        }

        private void InitializedCustomControl(DataRow rowQuestion, TableRow tblRow, string controlID, string answerType)
        {
            var appCtl = new AppControl();
            if (!appCtl.LoadByPrimaryKey(answerType)) return;

            var ctl = (BasePhrCtl)LoadControl(appCtl.ControlUrl);
            ctl.ID = controlID;

            if (!string.IsNullOrWhiteSpace(rowQuestion["QuestionText"].ToString()) && !ctl.IsLocateAtFirstColumn)
            {
                AddCaptionLabel(tblRow, rowQuestion);
                tblRow.Cells[1].Controls.Add(ctl);
            }
            else
            {
                tblRow.Cells[0].ColumnSpan = 2;
                tblRow.Cells[0].Attributes["class"] = string.Empty;
                tblRow.Cells[0].Controls.Add(ctl);
            }


            if (answerType == "IMG")
            {
                if (rowQuestion["BodyID"] != DBNull.Value && !string.IsNullOrEmpty(rowQuestion["BodyID"].ToString()))
                {
                    var body = new BodyDiagram();
                    if (body.LoadByPrimaryKey(rowQuestion["BodyID"].ToString()))
                        ctl.Value = body.BodyImage;
                }
            }
        }

        private void InitializedRowText(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            //tblRow.Cells[0].Text =
            //    string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            var questionLevel = rowQuestion["QuestionLevel"].ToInt();
            if (questionLevel > 0)
                tblRow.Cells[0].Style.Add("padding-left", string.Format("{0}px", questionLevel * _spacer));
            tblRow.Cells[0].Text = rowQuestion["QuestionText"].ToString();


            var txt = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());

            if (!string.IsNullOrEmpty(rowQuestion["AnswerSuffix"].ToString()))
            {
                var litSep = new Literal();
                litSep.Text = "&nbsp;&nbsp;" + rowQuestion["AnswerSuffix"].ToString();
                tblRow.Cells[1].Controls.Add(litSep);

                var tab = new HtmlTable() { ID = "tab_" + controlID, CellSpacing = 0, CellPadding = 0 };

                var row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(new HtmlTableCell());

                row.Cells[0].Controls.Add(txt);
                row.Cells[1].Controls.Add(litSep);

                tab.Rows.Add(row);

                tblRow.Cells[1].Controls.Add(tab);
            }
            else
                tblRow.Cells[1].Controls.Add(txt);

            InitializedValidationCtl(txt.ID, "", rowQuestion, tblRow.Cells[1]);

            InitializedLookUpLink(rowQuestion, tblRow.Cells[0], txt.ClientID);
        }

        private static void InitializedLookUpLink(DataRow rowQuestion, TableCell cellCaption, string clientID)
        {
            // Check LookUp Define
            if (rowQuestion["LookUpID"] != null && !string.IsNullOrWhiteSpace(rowQuestion["LookUpID"].ToString()))
            {
                var lup = string.Format(
                    "<a style=\"cursor: pointer;\" onclick=\"javascript:ResetValue('{0}', '{1}', '{2}');\"><img src=\"{3}/Images/Toolbar/refresh16.png\" alt=\"\"/></a>",
                    clientID, rowQuestion["LookUpID"], rowQuestion["QuestionText"], Helper.UrlRoot());

                cellCaption.Text = string.Format("{0}&nbsp;&nbsp;{1}", cellCaption.Text, lup);
            }
        }

        private void InitializedRowChkDate(DataRow rowQuestion, TableRow tblRow, string controlID, DateTime defaultSelectedDateTime)
        {
            //Literal litSep;
            //var cdtChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
            //tblRow.Cells[1].Controls.Add(cdtChk);
            //litSep = new Literal();
            //litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
            //tblRow.Cells[1].Controls.Add(litSep);
            //var cdtDattim = DateTimePickerControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt());
            //tblRow.Cells[1].Controls.Add(cdtDattim);

            Literal litSep;
            var cdtChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
            tblRow.Cells[1].Controls.Add(cdtChk);
            litSep = new Literal();
            litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
            tblRow.Cells[1].Controls.Add(litSep);
            var cdtDattim = DateTimePickerControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), defaultSelectedDateTime, Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
            tblRow.Cells[1].Controls.Add(cdtDattim);
        }

        private void InitializedRowCboCbo(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            Literal litSep;
            AddCaptionLabel(tblRow, rowQuestion);
            var cbo1 = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                string.Empty, "CBO");
            tblRow.Cells[1].Controls.Add(cbo1);
            litSep = new Literal();
            litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
            tblRow.Cells[1].Controls.Add(litSep);
            var cbo2 = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID2"].ToString(),
                rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                string.Empty, "CBO");
            tblRow.Cells[1].Controls.Add(cbo2);
            InitializedValidationCtl(cbo1.ID, "1", rowQuestion, tblRow.Cells[1]);
            InitializedValidationCtl(cbo2.ID, "2", rowQuestion, tblRow.Cells[1]);
        }

        private void InitializedRowCboMemo(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            AddCaptionLabel(tblRow, rowQuestion);

            var cbCbm = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                string.Empty, "CBO");
            tblRow.Cells[1].Controls.Add(cbCbm);

            var litSep = new Literal();
            litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
            tblRow.Cells[1].Controls.Add(litSep);

            var cbTxm = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
            tblRow.Cells[1].Controls.Add(cbTxm);
            InitializedValidationCtl(cbCbm.ID, "1", rowQuestion, tblRow.Cells[1]);
            InitializedValidationCtl(cbTxm.ID, "2", rowQuestion, tblRow.Cells[1]);
        }

        private void InitializedRowCboNum(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            Literal litSep;
            AddCaptionLabel(tblRow, rowQuestion);
            var cbCbn = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                string.Empty, "CBO");
            tblRow.Cells[1].Controls.Add(cbCbn);
            litSep = new Literal();
            litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
            tblRow.Cells[1].Controls.Add(litSep);
            var cbnNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
            tblRow.Cells[1].Controls.Add(cbnNum);
        }

        private void InitializedRowCboText(DataRow rowQuestion, TableRow tblRow, string controlID)
        {
            AddCaptionLabel(tblRow, rowQuestion);
            var cbCbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                string.Empty, "CBO");
            tblRow.Cells[1].Controls.Add(cbCbo);
            var litSep = new Literal { Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]) };
            tblRow.Cells[1].Controls.Add(litSep);
            var cbTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
            tblRow.Cells[1].Controls.Add(cbTxt);
            InitializedValidationCtl(cbCbo.ID, "1", rowQuestion, tblRow.Cells[1]);
            InitializedValidationCtl(cbTxt.ID, "2", rowQuestion, tblRow.Cells[1]);
        }

        private void InitializedRowTable(DataRow rowQuestion, TableRow tblRow, string controlID, Hashtable formulas)
        {
            AddCaptionLabel(tblRow, rowQuestion);
            //tblRow.Cells[0].Text =
            //    string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);


            var tbl = new HtmlTable() { ID = controlID, CellSpacing = 0, CellPadding = 0 };
            // ambil answer width sebagai jumlah kolom
            // ambil answer width 2 sebagai jumalh row
            var cList = rowQuestion["QuestionAnswerSelectionID"].ToString().Split('|');
            var defaultAnsList = rowQuestion["QuestionAnswerDefaultSelectionID"].ToString().Split('|');
            var cCount = cList.Length;
            var rCount = rowQuestion["AnswerWidth"].ToInt();

            var formula = rowQuestion["Formula"].ToString();
            var lFormula = new List<string>();
            if (!string.IsNullOrEmpty(formula))
            {
                lFormula = formula.Split('|').ToList();
            }
            var jsFormula = string.Empty;

            //create header table
            for (int iR = 0; iR <= rCount; iR++)
            {
                // row
                var rowTbl = new HtmlTableRow();
                for (int iC = 0; iC < cCount; iC++)
                {
                    var rHeader = cList[iC].Split(':');
                    rowTbl.Cells.Add(new HtmlTableCell());
                    if (iR == 0)
                    {
                        // table header
                        rowTbl.Cells[iC].InnerText = rHeader[0]; // print header
                        rowTbl.Cells[iC].Width = rHeader.Length == 1 ? "50" : rHeader[1];
                        rowTbl.Align = "Center";
                    }
                    else
                    {
                        var isNum = false;
                        var ctlID = controlID + "_" + iR.ToString() + "_" + iC.ToString();

                        var inputType = "txt";
                        var decDigit = "0";
                        var suffix = "";
                        var fnToAttach = "";
                        var isReadonly = false;

                        if (rHeader.Length > 2)
                        {
                            // numeric or text
                            var numPart = rHeader[2].Split(',');
                            inputType = numPart[0];
                            decDigit = numPart[1];
                            suffix = numPart[2];
                            fnToAttach = numPart[3];
                            isReadonly = (numPart.Length > 4) ? (numPart[4].ToLower() == "readonly") : false;
                            if (inputType.ToLower() == "num")
                            {
                                isNum = true;
                                var numCell = RadNumericTextBoxControl(ctlID,
                                    System.Convert.ToInt32(decDigit), suffix,
                                    System.Convert.ToInt32(rHeader[1]), "", "");
                                // set defaul value if any
                                if ((iC % cCount) + (iR - 1) * cCount < defaultAnsList.Length)
                                {
                                    var defaultVal = defaultAnsList[(iC % cCount) + (iR - 1) * cCount];
                                    if (Helper.IsNumeric(defaultVal))
                                    {
                                        numCell.Value = System.Convert.ToDouble(defaultVal);
                                    }
                                }
                                // client event
                                var lFnToAttach = fnToAttach.Split(';');
                                var jsFnToAttach = string.Empty;
                                foreach (string fta in lFnToAttach)
                                {
                                    if (string.IsNullOrEmpty(fta)) continue;
                                    jsFnToAttach += string.Format("{0}{1}();", ctlID, fta);
                                }
                                if (!string.IsNullOrEmpty(jsFnToAttach))
                                {
                                    // add to formulas
                                    string jsFormulaItem = "function " + ctlID + "_OnValChanged(){";
                                    jsFormulaItem += jsFnToAttach;
                                    jsFormulaItem += "}";
                                    FormulasAddJS(formulas, jsFormulaItem);

                                    numCell.ClientEvents.OnBlur = ctlID + "_OnValChanged";
                                }

                                // js formula
                                foreach (var lf in lFormula)
                                {
                                    if (string.IsNullOrEmpty(lf)) continue;
                                    var lfPart = lf.Split(':');
                                    if (lfPart[1].ToLower().Contains("row"))
                                    {
                                        string jsFormulaItem = "function " + ctlID + lfPart[0] + "() {";
                                        for (int iCl = 0; iCl < cCount; iCl++)
                                        {
                                            var iRl = iR;
                                            if (!string.IsNullOrEmpty(lfPart[2]))
                                            {
                                                iRl = System.Convert.ToInt32(lfPart[2]);
                                            }
                                            lfPart[3] = lfPart[3].Replace(string.Format("[{0}]", iCl),
                                                string.Format("$find('{0}_{1}')", this.ClientID, controlID + "_" + iRl.ToString() + "_" + iCl.ToString()));

                                        }
                                        jsFormulaItem += lfPart[3];
                                        jsFormulaItem += "}";
                                        FormulasAddJS(formulas, jsFormulaItem);
                                    }
                                    else if (lfPart[1].ToLower().Contains("col"))
                                    {
                                        string jsFormulaItem = "function " + ctlID + lfPart[0] + "() {";
                                        for (int iRl = 1; iRl <= rCount; iRl++)
                                        {
                                            var iCl = iC;
                                            if (!string.IsNullOrEmpty(lfPart[2]))
                                            {
                                                iCl = System.Convert.ToInt32(lfPart[2]);
                                            }
                                            lfPart[3] = lfPart[3].Replace(string.Format("[{0}]", iRl),
                                                string.Format("$find('{0}_{1}')", this.ClientID, controlID + "_" + iRl.ToString() + "_" + iCl.ToString()));
                                        }
                                        jsFormulaItem += lfPart[3];
                                        jsFormulaItem += "}";
                                        FormulasAddJS(formulas, jsFormulaItem);
                                    }
                                }
                                numCell.ReadOnly = isReadonly;
                                rowTbl.Cells[iC].Controls.Add(numCell);
                            }
                        }
                        // table content input
                        if (!isNum)
                        {
                            //var txtCell = TextBoxControl(ctlID,
                            //    System.Convert.ToInt32((rHeader.Length == 1 ? "50" : rHeader[1])),
                            //    string.Empty, string.Empty);

                            var txtCell = new TextBox();
                            txtCell.ID = ctlID;
                            txtCell.Width = Unit.Pixel(System.Convert.ToInt32((rHeader.Length == 1 ? "50" : rHeader[1])));
                            txtCell.CssClass = "riTextBox";

                            // set defaul value if any
                            if ((iC % cCount) + (iR - 1) * cCount < defaultAnsList.Length)
                            {
                                txtCell.Text = defaultAnsList[(iC % cCount) + (iR - 1) * cCount];
                            }
                            txtCell.ReadOnly = isReadonly;
                            rowTbl.Cells[iC].Controls.Add(txtCell);
                        }
                    }
                }

                tbl.Rows.Add(rowTbl);
            }

            tblRow.Cells[1].Controls.Add(tbl);
        }

        private static void TextBoxTextSet(object txtCtl, string value)
        {
            if (txtCtl == null) return;

            if (txtCtl is TextBox)
                (txtCtl as TextBox).Text = HtmlTagHelper.Devalidate(value);
            else if (txtCtl is RadTextBox)
                (txtCtl as RadTextBox).Text = HtmlTagHelper.Devalidate(value);
        }

        private void FormulasAddJS(Hashtable formulas, string jsFormula)
        {
            if (formulas.ContainsKey("PureJS"))
            {
                formulas["PureJS"] = formulas["PureJS"] + jsFormula;
            }
            else
            {
                formulas.Add("PureJS", jsFormula);
            }
        }

        private void InitializedRowDental(DataRow rowQuestion, TableRow tblRow)
        {
            AddCaptionLabel(tblRow, rowQuestion);

            // main table
            var tab0 = new HtmlTable() { CellPadding = 0, CellSpacing = 0 };
            var row1 = new HtmlTableRow();

            // table left
            var tabLeft = new HtmlTable() { CellPadding = 0, CellSpacing = 0 };

            //left up
            var row0 = new HtmlTableRow();

            for (int i = 8; i >= 1; i--)
            {
                row0.Cells.Add(AddTableCell("txtLU" + i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            //left center
            row0 = new HtmlTableRow();

            for (int i = 8; i >= 1; i--)
            {
                row0.Cells.Add(AddTableCellStandard(i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            row0 = new HtmlTableRow();

            for (int i = 8; i >= 1; i--)
            {
                row0.Cells.Add(new HtmlTableCell() { InnerText = i.ToString() });
            }

            tabLeft.Rows.Add(row0);

            //left bottom
            row0 = new HtmlTableRow();

            for (int i = 8; i >= 1; i--)
            {
                row0.Cells.Add(AddTableCell("txtLD" + i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            var cell0 = new HtmlTableCell();
            cell0.Style["border-right-style"] = "solid";
            cell0.Style["text-align"] = "center";
            cell0.Controls.Add(tabLeft);

            row1.Cells.Add(cell0);

            // table right
            tabLeft = new HtmlTable() { CellPadding = 0, CellSpacing = 0 };

            //right up
            row0 = new HtmlTableRow();

            for (int i = 1; i <= 8; i++)
            {
                row0.Cells.Add(AddTableCell("txtRU" + i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            //right center
            row0 = new HtmlTableRow();

            for (int i = 1; i <= 8; i++)
            {
                row0.Cells.Add(AddTableCellStandard(i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            row0 = new HtmlTableRow();

            for (int i = 1; i <= 8; i++)
            {
                row0.Cells.Add(new HtmlTableCell() { InnerText = i.ToString() });
            }

            tabLeft.Rows.Add(row0);

            //right bottom
            row0 = new HtmlTableRow();

            for (int i = 1; i <= 8; i++)
            {
                row0.Cells.Add(AddTableCell("txtRD" + i.ToString()));
            }

            tabLeft.Rows.Add(row0);

            cell0 = new HtmlTableCell();
            cell0.Style["text-align"] = "center";
            cell0.Controls.Add(tabLeft);

            row1.Cells.Add(cell0);

            tab0.Rows.Add(row1);

            // row bottom
            tabLeft = new HtmlTable();

            row0 = new HtmlTableRow();
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>G</b> = Gangrene (Terinfeksi)" });
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>R</b> = Radix (Akar)" });
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>F</b> = Filling (Tumpatan)" });

            tabLeft.Rows.Add(row0);

            row0 = new HtmlTableRow();
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>C</b> = Caries (Berlubang)" });
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>M</b> = Missing (Hilang)" });
            row0.Cells.Add(new HtmlTableCell() { InnerHtml = "<b>P</b> = Prothese (Gigi Palsu)" });

            tabLeft.Rows.Add(row0);

            cell0 = new HtmlTableCell();
            cell0.ColSpan = 2;
            cell0.Controls.Add(tabLeft);

            row1 = new HtmlTableRow();
            row1.Cells.Add(cell0);

            tab0.Rows.Add(row1);


            // add to main control
            tblRow.Cells[1].Controls.Add(tab0);
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
        private void AddLabel(string id, TableRow tblRow, DataRow rowQuestion)
        {
            tblRow.Cells[0].ColumnSpan = 2;
            var questionLevel = rowQuestion["QuestionLevel"].ToInt();
            if (questionLevel > 0)
                tblRow.Cells[0].Style.Add("padding-left", string.Format("{0}px", questionLevel * _spacer));
            tblRow.Cells[0].Text = rowQuestion["QuestionText"].ToString();

        }

        private void AddCaptionLabel(TableRow tblRow, DataRow rowQuestion)
        {
            var questionLevel = rowQuestion["QuestionLevel"].ToInt();

            if (questionLevel == 0)
            {
                var tblCell = tblRow.Cells[0];
                //tblCell.Style.Add("padding-left", string.Format("{0}px", questionLevel * _spacer)); // hasilnya akan 0px
                tblCell.Style.Add("padding-top", "10px");
                tblCell.Text = rowQuestion["QuestionText"].ToString();
            }
            else
            {
                AddText(rowQuestion, tblRow.Cells[0], questionLevel);
            }
        }
        //private string Spacer(int questionLevel)
        //{
        //    var retval = string.Empty;
        //    for (int i = 0; i < questionLevel; i++)
        //    {
        //        retval = string.Concat(retval, "&nbsp;&nbsp;");
        //    }
        //    return retval;
        //}

        private RadDatePicker DatePickerControl(string id, int width, DateTime defaultSelecteDateTime, bool isEmptyDefault)
        {

            var obj = new RadDatePicker();
            obj.ID = id;
            obj.Culture = _dateCultureInfo;
            if (!isEmptyDefault)
                obj.SelectedDate = defaultSelecteDateTime;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            obj.MinDate = new DateTime(1900, 1, 1);
            return obj;
        }
        private RadDatePicker DateTimePickerControl(string id, int width, DateTime defaultSelecteDateTime, bool isEmptyDefault)
        {
            var obj = new RadDateTimePicker();
            obj.ID = id;
            obj.Culture = _dateCultureInfo;
            if (!isEmptyDefault)
                obj.SelectedDate = defaultSelecteDateTime;
            obj.Width = Unit.Pixel(width == 0 ? 180 : width);
            obj.MinDate = new DateTime(1900, 1, 1);
            return obj;
        }
        private RadTimePicker TimePickerControl(string id, int width, DateTime defaultSelecteDateTime, bool isEmptyDefault)
        {
            var obj = new RadTimePicker();
            obj.ID = id;
            obj.Culture = _dateCultureInfo;
            if (!isEmptyDefault)
                obj.SelectedDate = defaultSelecteDateTime;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }

        private CheckBox CheckBoxControl(string id, string text, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            if (width > 0)
                chk.Width = Unit.Pixel(width);
            chk.Text = text;
            return chk;
        }
        private Control TextBoxControl(string id, int width, string formula, string defaultValue)
        {
            if (!string.IsNullOrEmpty(formula))
            {
                var radTxt = new RadTextBox();
                radTxt.ID = id;
                radTxt.Width = Unit.Pixel(width == 0 ? 300 : width);
                radTxt.ShowButton = true;
                radTxt.ClientEvents.OnValueChanged = "fillFormulaField";
                radTxt.ClientEvents.OnButtonClick = "fillFormulaField";

                // default value
                switch (defaultValue)
                {
                    case "[USERNAME]":
                        radTxt.Text = AppSession.UserLogin.UserName;
                        break;
                    default:
                        radTxt.Text = defaultValue;
                        break;
                }

                return radTxt;
            }


            // Supaya support autosize
            var textBox = new TextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            textBox.TextMode = TextBoxMode.MultiLine;
            textBox.Rows = 1;
            textBox.CssClass = "riTextBox";

            // default value
            switch (defaultValue)
            {
                case "[USERNAME]":
                    textBox.Text = AppSession.UserLogin.UserName;
                    break;
                default:
                    textBox.Text = defaultValue;
                    break;
            }

            return textBox;


        }
        //private RadTextBox MemoControl(string id, int width, string defaultValue)
        //{
        //    var textBox = new RadTextBox();
        //    textBox.ID = id;
        //    textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
        //    textBox.Height = Unit.Pixel(100); //sebelumnya di remark
        //    textBox.TextMode = InputMode.MultiLine;
        //    textBox.Resize = ResizeMode.Vertical;
        //    textBox.Text = defaultValue;
        //    return textBox;
        //}
        private TextBox MemoControl(string id, int width, string defaultValue)
        {
            var textBox = new TextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            textBox.TextMode = TextBoxMode.MultiLine;

            textBox.Height = Unit.Pixel(100);
            textBox.CssClass = "riTextBox";

            // Default value
            textBox.Text = defaultValue;
            return textBox;
        }
        private RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width, string formula, string defaultVal)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Culture = _numericCultureInfo;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;")
                                                       ? string.Empty
                                                       : string.Format("n {0}", suffix);

            defaultVal = defaultVal.Trim();
            if (Helper.IsNumeric(defaultVal))
            {
                textBox.Value = System.Convert.ToDouble(defaultVal);
            }
            else
            {
                textBox.Value = 0;
            }
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

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width, string formula, string cboType)
        {
            var cbo = new RadComboBox();
            cbo.ID = id;
            cbo.Width = Unit.Pixel(width == 0 ? 304 : width);
            // Populate Items
            switch (cboType)
            {
                case "CBO":
                    {
                        if (selectionID.Contains("[RANGE_"))
                        {
                            // Add range selection ex. RANGE_1_TO_10, RANGE_0_TO_100_STEP_10 (Handono 230308)
                            var ranges = selectionID.Substring(0, selectionID.Length - 1).Split('_');
                            var from = ranges[1].ToInt();
                            var to = ranges[3].ToInt();

                            var step = 1;
                            if (selectionID.Contains("_STEP_"))
                            {
                                step = ranges[5].ToInt();
                            }
                            to = to + step;
                            cbo.Items.Clear();
                            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            for (int i = from; i < to; i = i + step)
                            {
                                cbo.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                            }
                        }
                        else
                        {
                            var query = new QuestionAnswerSelectionLineQuery();
                            query.Where(query.QuestionAnswerSelectionID == selectionID);
                            query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
                            var dtb = query.LoadDataTable();
                            cbo.Items.Clear();
                            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (DataRow row in dtb.Rows)
                            {
                                cbo.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(),
                                                                       row["QuestionAnswerSelectionLineID"].ToString()));
                            }
                        }
                        break;
                    }
                case "CBR":
                    {
                        var coll = new AppStandardReferenceItemCollection();
                        coll.Query.Where(
                            coll.Query.StandardReferenceID == selectionID,
                            coll.Query.IsActive == true
                        );
                        coll.Query.OrderBy(coll.Query.ItemName.Ascending);
                        coll.LoadAll();

                        var val = cbo.SelectedValue;
                        cbo.Items.Clear();

                        cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        foreach (var item in coll)
                        {
                            cbo.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                        }

                        if (!string.IsNullOrWhiteSpace(val))
                            ComboBox.SelectedValue(cbo, val);
                        break;
                    }
                case "CBL":
                    {
                        cbo.EnableLoadOnDemand = true;
                        cbo.ShowMoreResultsBox = true;
                        cbo.EnableVirtualScrolling = true;

                        cbo.WebServiceSettings.Method = selectionID;
                        cbo.WebServiceSettings.Path = "~/WebService/ComboBoxDataService.asmx";
                        break;
                    }
            }


            if (!string.IsNullOrEmpty(formula))
            {
                //comboBox.ShowButton = true;
                //comboBox.ClientEvents.OnValueChanged = "fillFormulaField";
                //comboBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }


            // Set Default value
            if (!string.IsNullOrEmpty(defaultSelectionID))
                ComboBox.SelectedValue(cbo, defaultSelectionID);

            return cbo;
        }

        private RadioButtonList RadioButtonListControl(string id, string selectionID, string defaultSelectionID)
        {
            var rbl = new RadioButtonList();
            rbl.ID = id;

            if (selectionID.Contains("[RANGE_"))
            {
                // Add range selection ex. RANGE_1_TO_10, RANGE_0_TO_100_STEP_10 (Handono 2023 Des 03)
                var ranges = selectionID.Substring(0, selectionID.Length - 1).Split('_');
                var from = ranges[1].ToInt();
                var to = ranges[3].ToInt();

                var step = 1;
                if (selectionID.Contains("_STEP_"))
                {
                    step = ranges[5].ToInt();
                }
                to = to + step;
                rbl.Items.Clear();
                for (int i = from; i < to; i = i + step)
                {
                    rbl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                rbl.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                var query = new QuestionAnswerSelectionLineQuery();
                query.Where(query.QuestionAnswerSelectionID == selectionID);
                query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
                var dtb = query.LoadDataTable();
                rbl.Items.Clear();
                var isVertical = false;
                foreach (DataRow row in dtb.Rows)
                {
                    var text = row["QuestionAnswerSelectionLineText"].ToString();
                    rbl.Items.Add(new ListItem(text, row["QuestionAnswerSelectionLineID"].ToString()));

                    if (text.Length > 40)
                        isVertical = true;
                }

                //Nurul - Kondisi jika pilihan nya lebih dari 5 maka direction menjadi Vertikal
                if (dtb.Rows.Count > 5 || isVertical) // + jika ada piilihannya yg panjang (Handono 2023 des 2)
                    rbl.RepeatDirection = RepeatDirection.Vertical;
                else
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
            }
            if (!string.IsNullOrWhiteSpace(selectionID))
            {
                rbl.SelectedValue = selectionID;
            }

            return rbl;
        }

        private void AddSpacerCell(TableCellCollection cells)
        {
            var cell = new TableCell { Text = "&nbsp;&nbsp;", Wrap = false };
            cells.Add(cell);
        }

        #endregion


        #region Save
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PatientHealthRecord);
            return _autoNumber.LastCompleteNumber;
        }
        public void SetEntityValue(Patient pat, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr,
            PatientHealthRecordLineCollection collValue, string lastRegistrationNo)
        {
            if (string.IsNullOrEmpty(phr.TransactionNo))
            {
                phr.TransactionNo = GetNewTransactionNo();
                _autoNumber.Save();
            }

            //PatientHealthRecordLine
            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                SetPhrLine(phr.es.IsAdded, pat, reg, othRelatedEntities, phr, collValue, rowQuestion, rowQuestion["QuestionGroupID"].ToString(), lastRegistrationNo);

                // Cek jika memiliki child (Handono 231203)
                if (true.Equals(rowQuestion["IsHasChild"]))
                {
                    var quest = new QuestionQuery("q");
                    quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);

                    // Check has child
                    var questChild = new QuestionQuery("qc");
                    questChild.es.Top = 1;
                    questChild.Where(questChild.ParentQuestionID == quest.QuestionID);
                    questChild.Select("<CAST('1' as BIT) as IsHasChild>");

                    quest.Select(quest, string.Format("<IsHasChild = COALESCE( ({0}),CAST('0' as BIT))>", questChild.Parse()));
                    var dtbSubQuestion = quest.LoadDataTable();

                    foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                    {
                        SetPhrLine(phr.es.IsAdded, pat, reg, othRelatedEntities, phr, collValue, rowSubQuestion, rowQuestion["QuestionGroupID"].ToString(), lastRegistrationNo);
                    }
                }
            }
        }

        private void SetPhrLine(bool isNewRecord, Patient pat, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr, PatientHealthRecordLineCollection collValue, DataRow rowQuestion, string questionGroupID, string lastRegistrationNo)
        {
            var args = new ValidateArgs();
            string questionID = rowQuestion[PatientHealthRecordLineMetadata.ColumnNames.QuestionID].ToString();
            var hrLine = collValue.FindByPrimaryKey(phr.TransactionNo, phr.RegistrationNo, phr.QuestionFormID, questionGroupID, questionID) ?? collValue.AddNew();

            hrLine.TransactionNo = phr.TransactionNo;
            hrLine.RegistrationNo = phr.RegistrationNo;
            hrLine.QuestionFormID = phr.QuestionFormID;
            hrLine.QuestionGroupID = questionGroupID;
            hrLine.QuestionID = questionID;
            hrLine.QuestionAnswerPrefix = rowQuestion["AnswerPrefix"].ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = rowQuestion["AnswerSuffix"].ToStringDefaultEmpty();

            string controlID = QuestionControlID(questionGroupID, rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            Control obj = null;

            if (answerType != "DNT") //Dental Control
            {
                if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                    obj = Helper.FindControlRecursive(this, controlID);
                else
                    obj = Helper.FindControlRecursive(this, QuestionControlID(questionGroupID, rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

                if (obj == null)
                {
                    hrLine.MarkAsDeleted();
                    return;
                }
            }

            switch (answerType)
            {
                case "MSK":
                    var mskAnswerValue = (obj as RadMaskedTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(mskAnswerValue.TextWithLiterals);
                    break;
                case "DAT":
                    var dat = (obj as RadDatePicker);
                    hrLine.QuestionAnswerText = (dat.SelectedDate ?? DateTime.Now).ToString("MM/dd/yyyy"); // Hardcode jangan dirubah krn data sudah terekam (Handono)
                    break;
                case "DTM":
                case "ADT":
                    var dtm = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = (dtm.SelectedDate ?? DateTime.Now).ToString("MM/dd/yyyy HH:mm"); // Hardcode  jangan dirubah  krn data sudah terekam (Handono)
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    hrLine.QuestionAnswerText = (tim.SelectedDate ?? DateTime.Now).ToString("HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    hrLine.QuestionAnswerText = numAnswerValue.Text;
                    break;
                case "MEM":
                case "TXT":
                case "ABY":
                    hrLine.QuestionAnswerText = TextBoxText(obj);
                    break;
                case "CBR":
                case "CBL":
                case "CBO":
                    {
                        var cbo = (obj as RadComboBox);
                        hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo.SelectedValue);
                        hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo.Text);

                        // Simpan juga di QuestionAnswerNum jika Selection berasal dari RANGE int yg berarti adalah int (Handono 230308)
                        if (int.TryParse(cbo.Text, out int numValue))
                            hrLine.QuestionAnswerNum = Convert.ToInt32(numValue);
                        break;
                    }
                case "RBT":
                    {
                        var rbl = (obj as RadioButtonList);
                        //if (rbl.SelectedValue != null)
                        //{
                        //    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(rbl.SelectedValue);
                        //    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(rbl.Text); // <- isinya sama dengan SelectedValue
                        //}
                        if (rbl.SelectedItem != null)
                        {
                            hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(rbl.SelectedItem.Value);
                            hrLine.QuestionAnswerText = HtmlTagHelper.Validate(rbl.SelectedItem.Text);
                        }
                        else
                        {
                            hrLine.str.QuestionAnswerSelectionLineID = string.Empty;
                            hrLine.str.QuestionAnswerText = string.Empty;
                        }

                        // Simpan juga di QuestionAnswerNum jika Selection berasal dari RANGE int yg berarti adalah int (Handono 231208)
                        if (rbl.SelectedItem != null)
                        {
                            if (int.TryParse(rbl.SelectedItem.Value, out int numValue))
                                hrLine.QuestionAnswerNum = Convert.ToInt32(numValue);
                        }
                        else
                            hrLine.str.QuestionAnswerNum = string.Empty;

                        break;
                    }
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), TextBoxText(obj));
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), TextBoxText(obj));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), TextBoxText(obj));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), TextBoxText(obj));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    hrLine.QuestionAnswerText = TextBoxText(obj);

                    obj = Helper.FindControlRecursive(this, controlID + "_2");
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), TextBoxText(obj));
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
                                    this,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());

                                if (objCell is TextBox)
                                {
                                    var objCellText = (objCell as TextBox);
                                    ansText += (ansText.Equals(string.Empty) ? "" : "|") + HtmlTagHelper.Validate(objCellText.Text);
                                }
                                else if (objCell is RadTextBox)
                                {
                                    var objCellText = (objCell as RadTextBox);
                                    ansText += (ansText.Equals(string.Empty) ? " " : "|") + HtmlTagHelper.Validate(objCellText.Text);
                                }
                                else if (objCell is RadNumericTextBox)
                                {
                                    var objCellText = (objCell as RadNumericTextBox);
                                    ansText += (ansText.Equals(string.Empty) ? " " : "|") + HtmlTagHelper.Validate(objCellText.Text);
                                }
                            }
                        }
                    }
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(ansText);
                    break;
                case "DNT": //Dental Control
                    var dnt = new DentalHelper();
                    var str = string.Empty;

                    for (int i = 8; i >= 1; i--)
                    {
                        //var txt = Helper.FindControlRecursive(this, "txtLU" + i.ToString()) as RadTextBox;
                        //if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        //{
                        //    str += "txtLU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        //}

                        //txt = Helper.FindControlRecursive(this, "txtLD" + i.ToString()) as RadTextBox;
                        //if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        //{
                        //    str += "txtLD" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        //}

                        //txt = Helper.FindControlRecursive(this, "txtRU" + i.ToString()) as RadTextBox;
                        //if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        //{
                        //    str += "txtRU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        //}

                        //txt = Helper.FindControlRecursive(this, "txtRD" + i.ToString()) as RadTextBox;
                        //if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        //{
                        //    str += "txtRD" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        //}

                        var txt = Helper.FindControlRecursive(this, "txtLU" + i.ToString()) as TextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtLU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(this, "txtLD" + i.ToString()) as TextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtLD" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(this, "txtRU" + i.ToString()) as TextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtRU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(this, "txtRD" + i.ToString()) as TextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtRD" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }
                    }

                    foreach (var ctl in str.Split(';').Where(d => !string.IsNullOrEmpty(d))
                                                      .Select(d => new
                                                      {
                                                          controlID = d.Split('|')[0],
                                                          value = d.Split('|')[1]
                                                      }))
                    {
                        if (ctl.controlID == "txtLU8") dnt.LU8 = ctl.value;
                        if (ctl.controlID == "txtLU7") dnt.LU7 = ctl.value;
                        if (ctl.controlID == "txtLU6") dnt.LU6 = ctl.value;
                        if (ctl.controlID == "txtLU5") dnt.LU5 = ctl.value;
                        if (ctl.controlID == "txtLU4") dnt.LU4 = ctl.value;
                        if (ctl.controlID == "txtLU3") dnt.LU3 = ctl.value;
                        if (ctl.controlID == "txtLU2") dnt.LU2 = ctl.value;
                        if (ctl.controlID == "txtLU1") dnt.LU1 = ctl.value;

                        if (ctl.controlID == "txtLD8") dnt.LD8 = ctl.value;
                        if (ctl.controlID == "txtLD7") dnt.LD7 = ctl.value;
                        if (ctl.controlID == "txtLD6") dnt.LD6 = ctl.value;
                        if (ctl.controlID == "txtLD5") dnt.LD5 = ctl.value;
                        if (ctl.controlID == "txtLD4") dnt.LD4 = ctl.value;
                        if (ctl.controlID == "txtLD3") dnt.LD3 = ctl.value;
                        if (ctl.controlID == "txtLD2") dnt.LD2 = ctl.value;
                        if (ctl.controlID == "txtLD1") dnt.LD1 = ctl.value;

                        if (ctl.controlID == "txtRU8") dnt.RU8 = ctl.value;
                        if (ctl.controlID == "txtRU7") dnt.RU7 = ctl.value;
                        if (ctl.controlID == "txtRU6") dnt.RU6 = ctl.value;
                        if (ctl.controlID == "txtRU5") dnt.RU5 = ctl.value;
                        if (ctl.controlID == "txtRU4") dnt.RU4 = ctl.value;
                        if (ctl.controlID == "txtRU3") dnt.RU3 = ctl.value;
                        if (ctl.controlID == "txtRU2") dnt.RU2 = ctl.value;
                        if (ctl.controlID == "txtRU1") dnt.RU1 = ctl.value;

                        if (ctl.controlID == "txtRD8") dnt.RD8 = ctl.value;
                        if (ctl.controlID == "txtRD7") dnt.RD7 = ctl.value;
                        if (ctl.controlID == "txtRD6") dnt.RD6 = ctl.value;
                        if (ctl.controlID == "txtRD5") dnt.RD5 = ctl.value;
                        if (ctl.controlID == "txtRD4") dnt.RD4 = ctl.value;
                        if (ctl.controlID == "txtRD3") dnt.RD3 = ctl.value;
                        if (ctl.controlID == "txtRD2") dnt.RD2 = ctl.value;
                        if (ctl.controlID == "txtRD1") dnt.RD1 = ctl.value;
                    }

                    hrLine.QuestionAnswerText = dnt.MarkupResult;
                    hrLine.QuestionAnswerText2 = str;
                    break;
                default:
                    var phrCtl = (obj as BasePhrCtl);
                    if (phrCtl != null) phrCtl.SetEntityValue(args, pat, reg, phr, hrLine, lastRegistrationNo);
                    break;

            }

            // Set related Field
            SetRelatedFieldValue(pat, reg, othRelatedEntities, phr, rowQuestion, lastRegistrationNo, hrLine);
        }

        private static string TextBoxText(object txtCtl)
        {
            var txtVal = string.Empty;

            if (txtCtl is TextBox)
                txtVal = (txtCtl as TextBox).Text;
            else if (txtCtl is RadTextBox)
                txtVal = (txtCtl as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace(txtVal))
                return HtmlTagHelper.Validate(txtVal);
            return string.Empty;
        }

        private static void TextBoxReadOnlySet(bool isReadOnly, Control ctl)
        {
            if (ctl == null) return;

            if (ctl is TextBox)
                (ctl as TextBox).ReadOnly = isReadOnly;
            else if (ctl is RadTextBox)
            {
                var txt = (ctl as RadTextBox);
                txt.ReadOnly = isReadOnly;
                if (isReadOnly && txt.ShowButton)
                {
                    txt.ShowButton = false;
                    txt.ClientEvents.OnValueChanged = "";
                    txt.ClientEvents.OnButtonClick = "";
                }
            }

        }
        private static void SetRelatedFieldValueIfFromLastReg(string patientID, string registrationNo, Dictionary<string, esEntityWAuditLog> othRelatedEntities,
            DataRow rowQuestion, string lastRegistrationNo, string entName, string value)
        {
            if (string.IsNullOrWhiteSpace(lastRegistrationNo) || registrationNo == lastRegistrationNo)
            {
                if (!othRelatedEntities.ContainsKey(entName))
                {
                    othRelatedEntities.Add(entName, Utils.GetEntity(entName));
                }

                var ent = othRelatedEntities[entName];
                if (!patientID.Equals(ent.GetColumn("PatientID")))
                {
                    //Load
                    if (!ent.Load(string.Format("PatientID='{0}'", patientID)))
                    {
                        // New Entity
                        ent = Utils.GetEntity(entName);
                        othRelatedEntities[entName] = ent;
                        ent.SetColumn("PatientID", patientID);
                    }
                }

                ent.SetColumn(rowQuestion["RelatedColumnName"].ToString(), value);
            }
        }
        private static void SetRelatedFieldValueIfFromLastReg2(string patientID, string registrationNo, Dictionary<string, esEntityWAuditLog> othRelatedEntities,
            DataRow rowQuestion, string lastRegistrationNo, string entName, string value)
        {
            if (string.IsNullOrWhiteSpace(lastRegistrationNo) || registrationNo == lastRegistrationNo)
            {
                if (!othRelatedEntities.ContainsKey(entName))
                {
                    othRelatedEntities.Add(entName, Utils.GetEntity(entName));
                }

                var ent = othRelatedEntities[entName];
                if (!registrationNo.Equals(ent.GetColumn("RegistrationNo")))
                {
                    //Load
                    if (!ent.Load(string.Format("RegistrationNo='{0}'", registrationNo)))
                    {
                        // New Entity
                        ent = Utils.GetEntity(entName);
                        othRelatedEntities[entName] = ent;
                        ent.SetColumn("RegistrationNo", registrationNo);
                    }
                }

                ent.SetColumn(rowQuestion["RelatedColumnName"].ToString(), value);
            }
        }

        private class FielValue
        {
            public object Value { get; set; }
            public string Text { get; set; }
        }
        private static FielValue GetRelatedFielValue(Patient pat, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr, DataRow rowQuestion)
        {
            var retval = new FielValue();
            var relEntName = rowQuestion["RelatedEntityName"].ToString();
            var relColumnName = rowQuestion["RelatedColumnName"].ToString();
            switch (relEntName.ToLower())
            {
                case "registration":
                    {
                        switch (relColumnName.ToLower())
                        {
                            case "serviceunitname":
                                {
                                    var su = new ServiceUnit();
                                    if (su.LoadByPrimaryKey(reg.GetColumn("ServiceUnitID").ToString()))
                                        retval.Value = su.ServiceUnitName;
                                    break;
                                }
                            case "guarantorname":
                                {
                                    var gr = new Guarantor();
                                    if (gr.LoadByPrimaryKey(reg.GetColumn("GuarantorID").ToString()))
                                        retval.Value = gr.GuarantorName;
                                    break;
                                }
                            case "referralname":
                                {
                                    var ent = new Referral();
                                    if (ent.LoadByPrimaryKey(reg.GetColumn("ReferralID").ToString()))
                                        retval.Value = ent.ReferralName;
                                    break;
                                }
                            case "patientname":
                                {
                                    var ent = new Patient();
                                    if (ent.LoadByPrimaryKey(reg.GetColumn("PatientID").ToString()))
                                        retval.Value = ent.PatientName;
                                    break;
                                }
                            default:
                                retval.Value = reg.GetColumn(relColumnName);
                                break;
                        }
                        break;
                    }
                case "patient":
                    retval.Value = pat.GetColumn(rowQuestion["RelatedColumnName"].ToString());
                    break;
                case "patientbirthrecord":
                case "birthrecord":
                case "pastsurgicalhistory":
                case "patienttransferhistory":
                    {
                        if (!othRelatedEntities.ContainsKey(relEntName)) // Cek jika belum ada maka create
                        {
                            var ent = Utils.GetEntity(relEntName);
                            switch (relEntName.ToLower())
                            {
                                case "patientbirthrecord":                                
                                case "pastsurgicalhistory":
                                    {
                                        if (!ent.Load(string.Format("PatientID='{0}'", pat.PatientID)))
                                            ent = Utils.GetEntity(relEntName);

                                        othRelatedEntities.Add(relEntName, ent);

                                        break;
                                    }                                
                                case "patienttransferhistory":
                                    {
                                        if (!ent.Load(string.Format("RegistrationNo = '{0}' AND TransferNo = '{1}'",
                                            reg.RegistrationNo, phr.ReferenceNo ?? string.Empty)))
                                            ent = Utils.GetEntity(relEntName);

                                        othRelatedEntities.Add(relEntName, ent);
                                        break;
                                    }
                                case "birthrecord":
                                    {
                                        if (!ent.Load(string.Format("RegistrationNo='{0}'", reg.RegistrationNo)))
                                            ent = Utils.GetEntity(relEntName);

                                        othRelatedEntities.Add(relEntName, ent);

                                        break;
                                    }
                            }
                        }

                        var entUpd = othRelatedEntities[relEntName];
                        if (entUpd != null)
                        {
                            retval.Value = entUpd.GetColumn(rowQuestion["RelatedColumnName"].ToString());
                        }

                        break;
                    }
                case "patientfield":
                    {
                        var af = new AppField();
                        af.Query.Where(af.Query.FieldName == rowQuestion["RelatedColumnName"].ToString());
                        if (af.Query.Load())
                        {
                            var patField = new PatientField();
                            if (patField.LoadByPrimaryKey(reg.PatientID, af.FieldID ?? 0))
                            {
                                switch (af.FieldType)
                                {
                                    case "S": // String
                                        retval.Value = patField.ValueInString != null ? patField.ValueInString : null;
                                        break;
                                    case "D": // DateTime "MM/dd/yyyy HH:mm"
                                        retval.Value = patField.ValueInDatetime != null ? patField.ValueInDatetime.Value.ToString("MM/dd/yyyy HH:mm") : null;
                                        break;
                                    case "B": // Bool
                                        retval.Value = patField.ValueInBool == null ? null : (patField.ValueInBool == true ? "1" : "0"); //Checkbox Value
                                        break;
                                    case "N": // Numeric
                                    case "I": // Integer
                                        retval.Value = patField.ValueInNumeric != null ? patField.ValueInNumeric : null;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                    }

            }

            // Koreksi text jika tipe CBL Combobox Web service
            if (retval.Value != null)
            {
                retval.Text = retval.Value.ToString();

                string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
                if (answerType == "CBL")
                {
                    // Lengkapi Text nya
                    switch (rowQuestion["QuestionAnswerSelectionID"].ToString().ToLower())
                    {
                        case "users":
                            var user = new AppUser();
                            if (user.LoadByPrimaryKey(retval.Value.ToString()))
                                retval.Text = user.UserName;
                            break;
                        case "servicerooms":
                            var sr = new ServiceRoom();
                            if (sr.LoadByPrimaryKey(retval.Value.ToString()))
                                retval.Text = sr.RoomName;
                            break;
                        case "serviceunitcares":
                            var su = new ServiceUnit();
                            if (su.LoadByPrimaryKey(retval.Value.ToString()))
                                retval.Text = su.ServiceUnitName;
                            break;
                        case "classes":
                            var cls = new Class();
                            if (cls.LoadByPrimaryKey(retval.Value.ToString()))
                                retval.Text = cls.ClassName;
                            break;
                    }
                }
            }

            return retval;
        }

        private static void SetRelatedFieldValue(Patient pat, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord phr, DataRow rowQuestion, string lastRegistrationNo,
        PatientHealthRecordLine hrLine)
        {
            if (rowQuestion["IsUpdateRelatedEntity"] != DBNull.Value
                && Convert.ToBoolean(rowQuestion["IsUpdateRelatedEntity"])
                && rowQuestion["RelatedEntityName"] != DBNull.Value
                && rowQuestion["RelatedColumnName"] != DBNull.Value)
            {
                string value = string.IsNullOrWhiteSpace(hrLine.QuestionAnswerSelectionLineID)
                    ? HtmlTagHelper.Devalidate(hrLine.QuestionAnswerText)
                    : hrLine.QuestionAnswerSelectionLineID;

                var entName = rowQuestion["RelatedEntityName"].ToString();

                var isNotOverWriteRelatedEntity = rowQuestion["IsNotOverWriteRelatedEntity"] != DBNull.Value && Convert.ToBoolean(rowQuestion["IsNotOverWriteRelatedEntity"]);
                var isUpdate = !isNotOverWriteRelatedEntity;

                switch (entName.ToLower())
                {
                    case "registration":
                        {
                            if (isNotOverWriteRelatedEntity)
                            {
                                // Update hanya jika valuenya masih kosong
                                var curVal = reg.GetColumn(rowQuestion["RelatedColumnName"].ToString());
                                if (curVal != null && string.IsNullOrWhiteSpace(curVal.ToString()))
                                {
                                    isUpdate = true;
                                }
                            }

                            if (isUpdate)
                                reg.SetColumn(rowQuestion["RelatedColumnName"].ToString(), value);

                            break;
                        }
                    case "patient":
                        {
                            if (isNotOverWriteRelatedEntity)
                            {
                                // Update hanya jika valuenya masih kosong
                                var curVal = pat.GetColumn(rowQuestion["RelatedColumnName"].ToString());
                                if (curVal != null && string.IsNullOrWhiteSpace(curVal.ToString()))
                                {
                                    isUpdate = true;
                                }
                            }

                            if (isUpdate)
                            {
                                // update hanya jika dari reg terakhir
                                if (string.IsNullOrWhiteSpace(lastRegistrationNo) || reg.RegistrationNo == lastRegistrationNo)
                                    pat.SetColumn(rowQuestion["RelatedColumnName"].ToString(), value);
                            }
                            break;
                        }
                    case "pastsurgicalhistory":
                        {
                            // Histori pembedahan pasien yg tidak terdata di ServiceUnitBooking dan PatientEpisode
                            //TODO: Masih rancu harusnya histori pembedahan ya semua
                            // update hanya jika dari reg terakhir
                            SetRelatedFieldValueIfFromLastReg(pat.PatientID, reg.RegistrationNo, othRelatedEntities, rowQuestion, lastRegistrationNo, entName, value);
                            break;
                        }
                    case "patienttransferhistory":
                        {
                            if (!othRelatedEntities.ContainsKey(entName))
                            {
                                var ent = Utils.GetEntity(entName);
                                if (ent.Load(string.Format("RegistrationNo = '{0}' AND TransferNo = '{1}'", reg.RegistrationNo, phr.ReferenceNo ?? string.Empty)))
                                {
                                    othRelatedEntities.Add(entName, ent);
                                }
                            }
                            var entUpd = othRelatedEntities[entName];
                            if (entUpd != null)
                                entUpd.SetColumn(rowQuestion["RelatedColumnName"].ToString(), value);
                            break;
                        }

                    case "patientbirthrecord":
                        {
                            // Update hanya jika dari reg terakhir
                            SetRelatedFieldValueIfFromLastReg(pat.PatientID, reg.RegistrationNo, othRelatedEntities, rowQuestion, lastRegistrationNo, entName, value);

                            break;
                        }
                    case "birthrecord":
                        {
                            SetRelatedFieldValueIfFromLastReg2(pat.PatientID, reg.RegistrationNo, othRelatedEntities, rowQuestion, lastRegistrationNo, entName, value);

                            break;                            
                        }
                    case "patientfield":
                        {
                            var relEntKey = string.Format("{0}{1}", entName, rowQuestion["RelatedColumnName"]);
                            var af = new AppField();
                            af.Query.Where(af.Query.FieldName == rowQuestion["RelatedColumnName"].ToString());
                            if (af.Query.Load())
                            {
                                if (!othRelatedEntities.ContainsKey(relEntKey))
                                {
                                    var ent = new PatientField();
                                    if (!ent.LoadByPrimaryKey(reg.PatientID, af.FieldID ?? 0))
                                        ent = new PatientField();

                                    othRelatedEntities.Add(relEntKey, ent);

                                }
                                var dict = othRelatedEntities[relEntKey];
                                if (dict != null)
                                {
                                    // Update jika tgl datanya lebih baru
                                    var recordDate = phr.RecordDate.Value;
                                    var recordTimes = (string.IsNullOrWhiteSpace(phr.RecordTime) ? "00:00" : phr.RecordTime).Split(':');
                                    recordDate = new DateTime(recordDate.Year, recordDate.Month, recordDate.Day, recordTimes[0].ToInt(),
                                        recordTimes[1].ToInt(), 59);

                                    var patField = (PatientField)dict;
                                    if (patField.DataDateTime == null || patField.DataDateTime <= recordDate)
                                    {
                                        patField.PatientID = reg.PatientID;
                                        patField.FieldID = af.FieldID;
                                        patField.DataDateTime = recordDate;
                                        switch (af.FieldType)
                                        {
                                            case "S": // String
                                                patField.ValueInString = value;
                                                break;
                                            case "D": // DateTime
                                                      // "MM/dd/yyyy HH:mm"
                                                patField.ValueInDatetime = Convert.ToDateTime(value);
                                                break;
                                            case "B": // Bool
                                                patField.ValueInBool = (value == "1"); //Checkbox Value
                                                break;
                                            case "N": // Numeric
                                                patField.ValueInNumeric = value.ToDecimal();
                                                break;
                                            case "I": // Integer
                                                patField.ValueInNumeric = value.ToInt();
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        }

                }
            }
        }


        public void Save(Patient pat, Registration reg, Dictionary<string, esEntityWAuditLog> othRelatedEntities, PatientHealthRecord entity, PatientHealthRecordLineCollection collValue, string lastRegistrationNo)
        {
            using (var trans = new esTransactionScope())
            {
                SetEntityValue(pat, reg, othRelatedEntities, entity, collValue, lastRegistrationNo);

                entity.Save();
                collValue.Save();

                if (pat.es.IsModified)
                    pat.Save();

                if (reg.es.IsModified)
                    reg.Save();

                // othRelatedEntities
                foreach (var othRelatedEntity in othRelatedEntities.Values)
                {
                    if (othRelatedEntity.es.IsModified || othRelatedEntity.es.IsAdded)
                        othRelatedEntity.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region Export to SOAP
        public void PopulateSoapValue(PatientHealthRecord phr, ref string subjective, ref string objective, ref string assessment, ref string planning, ref string instruction)
        {
            var sbS = new StringBuilder();
            var sbO = new StringBuilder();
            var sbA = new StringBuilder();
            var sbP = new StringBuilder();
            var sbI = new StringBuilder();


            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(phr.QuestionFormID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(phr.QuestionFormID);
            int rowNo = 0;
            foreach (QuestionGroup qg in questionGroups)
            {
                PopulateSoapValue(qg.QuestionGroupName, qg.SoapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);

                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", qg.QuestionGroupID), "RowIndex");

                PopulateSoapValue(phr, questionRows, qg.SoapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
            }

            // Result
            subjective = sbS.ToString();
            objective = sbO.ToString();
            assessment = sbA.ToString();
            planning = sbP.ToString();
            instruction = sbI.ToString();
        }
        private void PopulateSoapValue(string value, string soapType, ref StringBuilder subjective, ref StringBuilder objective,
            ref StringBuilder assessment, ref StringBuilder planning, ref StringBuilder instruction)
        {
            switch (soapType)
            {
                case "S":
                    subjective.AppendLine(value);
                    break;
                case "O":
                    objective.AppendLine(value);
                    break;
                case "A":
                    assessment.AppendLine(value);
                    break;
                case "P":
                    planning.AppendLine(value);
                    break;
                case "I":
                    instruction.AppendLine(value);
                    break;
            }
        }
        private void PopulateSoapValue(PatientHealthRecord phr, DataRow[] questionRows, string soapType, ref StringBuilder sbS, ref StringBuilder sbO,
            ref StringBuilder sbA, ref StringBuilder sbP, ref StringBuilder sbI)
        {
            foreach (DataRow rowChild in questionRows)
            {

                if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                {
                    var qr = new PatientHealthRecordLineQuery("a");
                    var qQuest = new QuestionQuery("b");
                    qr.InnerJoin(qQuest).On(qr.QuestionID == qQuest.QuestionID);

                    qr.Where(qr.RegistrationNo == phr.RegistrationNo, qr.TransactionNo == phr.TransactionNo,
                        qr.QuestionGroupID == rowChild["QuestionGroupID"], qr.QuestionID == rowChild["QuestionID"]);
                    qr.Select
                    (
                        qr, qQuest.QuestionText.As("refToQuestion_QuestionText"),
                        qQuest.SRAnswerType.As("refToQuestion_SRAnswerType"), qQuest.QuestionAnswerSelectionID.As("refToQuestion_QuestionAnswerSelectionID")
                    );

                    var line = new PatientHealthRecordLine();
                    if (!line.Load(qr)) continue;

                    var answerType = line.SRAnswerType;
                    var answerVal = string.Empty;

                    switch (answerType)
                    {
                        case "LBL":
                            PopulateSoapValue(line.QuestionText, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            break;
                        case "DAT":
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText))
                            {
                                answerVal = string.Format("{0}: {1}", line.QuestionText, Convert.ToDateTime(line.QuestionAnswerText).ToString("dd/MM/yyyy")); // Hardcode indonesia format
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }
                            break;
                        case "DTM":
                        case "ADT":
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText))
                            {
                                var dtm = Convert.ToDateTime(line.QuestionAnswerText);
                                answerVal = string.Format("{0}: {1} Jam: {2}", line.QuestionText, dtm.ToString("dd/MM/yyyy"), dtm.ToString("HH:mm")); // Hardcode indonesia format
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }
                            break;
                        case "MSK":
                        case "TIM":
                        case "TXT":
                        case "ABY":
                        case "MEM":
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText))
                            {
                                answerVal = string.Format("{0}: {1}", line.QuestionText, HtmlTagHelper.Validate(line.QuestionAnswerText));
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }
                            break;
                        case "NUM":
                            if (line.QuestionAnswerNum != 0)
                            {
                                answerVal = string.Format("{0}: {1} {2}", line.QuestionText, HtmlTagHelper.Validate(line.QuestionAnswerText), line.QuestionAnswerSuffix);
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }

                            break;
                        case "CBR":
                        case "CBL":
                        case "CBO":
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerSelectionLineID))
                            {
                                answerVal = string.Format("{0}: {1}", line.QuestionText, line.QuestionAnswerText);
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }

                            break;
                        case "RBT":
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerSelectionLineID))
                            {
                                answerVal = string.Format("{0}: {1}", line.QuestionText, line.QuestionAnswerText);
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }

                            break;
                        case "CHK":
                            // TODO: Sementara hanya yg jawabannya Ya dianggap abnormal nantinya harus ada field nilai normalnya
                            if (!string.IsNullOrWhiteSpace(line.QuestionAnswerText) && "1".Equals(line.QuestionAnswerText))
                            {
                                answerVal = string.Format("{0}: {1}", line.QuestionText, "1".Equals(line.QuestionAnswerText) ? "Ya" : "Tidak");
                                PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                            }
                            break;
                        case "CTX":
                        case "CTM":
                            var ctxValue = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(ctxValue) && !ctxValue.Equals("|"))
                            {
                                var ctxValues = ctxValue.ToString().Split('|');
                                if (ctxValues.Length > 0 && ctxValues[0] != null)
                                {
                                    if (ctxValues.Length > 1 && ctxValues[1] != null && (!string.IsNullOrEmpty(ctxValues[1]) || ctxValues[0] != "0"))
                                    {
                                        answerVal = string.Format("{0}: {1} - {2}", line.QuestionText, ctxValues[0], HtmlTagHelper.Validate(ctxValues[1]));
                                        PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                                    }
                                }
                            }
                            break;
                        case "CNM":
                        case "CDT":
                        case "CBT":
                        case "CBN":
                        case "CBM":
                        case "CB2":
                        case "TTX":
                            var doubleVal = line.QuestionAnswerText;
                            if (!string.IsNullOrEmpty(doubleVal) && !doubleVal.Equals("|"))
                            {
                                var doubleVals = doubleVal.ToString().Split('|');
                                if (doubleVals.Length > 0 && doubleVals[0] != null)
                                {
                                    if (doubleVals.Length > 1 && doubleVals[1] != null)
                                    {
                                        //if (answerType=="CNM")
                                        //    answerVal = string.Format("{0}: {1} - {2}", line.QuestionText, doubleVals[0], HtmlTagHelper.Validate(doubleVals[1]));
                                        //else
                                        answerVal = string.Format("{0}: {1} - {2}", line.QuestionText, doubleVals[0], HtmlTagHelper.Validate(doubleVals[1]));
                                        PopulateSoapValue(answerVal, soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                                    }
                                }
                            }
                            break;
                    }
                }

                // Cek jika memiliki child (Handono 231203)
                if (true.Equals(rowChild["IsHasChild"]))
                {
                    var quest = new QuestionQuery("q");
                    quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                    quest.OrderBy(quest.IndexNo.Ascending);

                    // Check has child
                    var questChild = new QuestionQuery("qc");
                    questChild.es.Top = 1;
                    questChild.Where(questChild.ParentQuestionID == quest.QuestionID);
                    questChild.Select("<CAST('1' as BIT) as IsHasChild>");

                    quest.Select(quest, string.Format("<IsHasChild = COALESCE( ({0}),CAST('0' as BIT))>", questChild.Parse()));
                    var dtbSubQuestion = quest.LoadDataTable();

                    if (dtbSubQuestion.Rows.Count > 0)
                    {
                        PopulateSoapValue(phr, dtbSubQuestion.Select(), soapType, ref sbS, ref sbO, ref sbA, ref sbP, ref sbI);
                    }
                }
            }


        }

        #endregion

    }
}