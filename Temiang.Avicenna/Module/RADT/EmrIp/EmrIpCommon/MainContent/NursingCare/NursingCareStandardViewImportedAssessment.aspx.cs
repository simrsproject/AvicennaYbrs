using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardViewImportedAssessment : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildCustomEntryAssessment();
            if (!IsPostBack)
            {
                grdListAssessment.DataBind();
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            //if (grdListActivities.SelectedValue != null)
            //{
            //    return "oWnd.argument.result = 'OK'";
            //    //return "oWnd.argument.mode = 'reg|'";
            //}
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {

            return true;
        }

        private string ID
        {
            get
            {
                return Request.QueryString["assid"];
            }
        }

        public DataTable AssessmentViewState
        {
            get
            {
                if (ViewState["AssessmentViewState" + ID] != null)
                {
                    return (DataTable)ViewState["AssessmentViewState" + ID];
                }

                return null;
            }
            set
            {
                ViewState["AssessmentViewState" + ID] = value;
            }
        }

        private void BuildCustomEntryAssessment()
        {
            // get saved value

            var nat = new NursingAssessmentTransDTCollection();
            if (!string.IsNullOrEmpty(ID))
            {
                nat.Query.Where(nat.Query.Hdid == Int64.Parse(ID));
                nat.LoadAll();
            }

            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                DataRow[] questionRows = AssessmentViewState.Select(string.Format("QuestionID='{0}'", id));

                var natItem = nat.Where(n => n.QuestionID == id).FirstOrDefault() ??
                    nat.Where(n => n.QuestionID == id).FirstOrDefault();

                item["AnswerObj"].Controls.Clear();
                if (questionRows.Count() > 0)
                {
                    var question = questionRows[0];
                    if (!string.IsNullOrEmpty(question["SRAnswerType"].ToString()))
                    {
                        InitializedQuestion(question, item, natItem);
                        if (natItem != null)
                        {
                            if (nat.Where(n => n.QuestionID == id).FirstOrDefault() != null)
                            {
                                var chkS = item.FindControl("defaultChkBoxS");
                                if (chkS != null)
                                {
                                    ((CheckBox)chkS).Checked = (bool)question["IsSubjective"];
                                }

                                var chkO = item.FindControl("defaultChkBoxO");
                                if (chkO != null)
                                {
                                    ((CheckBox)chkO).Checked = (bool)question["IsObjective"];
                                }
                            }
                        }
                    }
                }
            }
        }

        public static System.Web.UI.Control InitializedQuestion(Question q, System.Web.UI.Control ControlContainer,
            string AnswerText, double? AnswerNum, string AnswerSelectionLineID, bool ReadOnly)
        {
            return InitializedQuestion(q.QuestionID, q.QuestionText, q.SRAnswerType, q.AnswerWidth ?? 0,
                q.AnswerDecimalDigit ?? 0, q.AnswerPrefix, q.AnswerSuffix, q.AnswerWidth2 ?? 0,
                q.QuestionAnswerSelectionID, q.QuestionAnswerDefaultSelectionID,
                q.QuestionAnswerSelectionID2, q.QuestionAnswerDefaultSelectionID2,
                AnswerText, AnswerNum, AnswerSelectionLineID, q.Formula, ControlContainer, ReadOnly);
        }

        public static System.Web.UI.Control InitializedQuestion(DataRow question, GridDataItem cell, NursingAssessmentTransDT nat)
        {
            double? AnsNum = new double();
            if (nat != null)
            {
                if (!nat.AnswerNum.Equals("&nbsp;"))
                {
                    AnsNum = Convert.ToDouble(nat.AnswerNum);
                }
            }

            return InitializedQuestion(question["QuestionID"].ToString(), question["QuestionText"].ToString(), question["SRAnswerType"].ToString(),
                 question["AnswerWidth"] is DBNull ? 0 : question["AnswerWidth"].ToInt(),
                 question["AnswerDecimalDigit"] is DBNull ? 0 : int.Parse(question["AnswerDecimalDigit"].ToString()),
                 question["AnswerPrefix"].ToString(), question["AnswerSuffix"].ToString(),
                 question["AnswerWidth2"] is DBNull ? 0 : question["AnswerWidth2"].ToInt(),
                 question["QuestionAnswerSelectionID"].ToStringDefaultEmpty(),
                 question["QuestionAnswerDefaultSelectionID"].ToString(),
                 question["QuestionAnswerSelectionID2"].ToStringDefaultEmpty(),
                 question["QuestionAnswerDefaultSelectionID2"].ToString(),
                 (nat == null) ? string.Empty : nat.AnswerText,
                 AnsNum,
                 (nat == null) ? string.Empty : nat.AnswerSelectionLineID,
                 question["Formula"].ToString(),
                 cell["AnswerObj"], false);
        }
        public static System.Web.UI.Control InitializedQuestion(string QuestionID, string QuestionText, string SRAnswerType,
            int AnswerWidth, int AnswerDecimalDigit, string AnswerPrefix, string AnswerSuffix, int AnswerWidth2,
            string QuestionAnswerSelectionID, string QuestionAnswerDefaultSelectionID,
            string QuestionAnswerSelectionID2, string QuestionAnswerDefaultSelectionID2,
            string AnswerText, double? AnswerNum, string AnswerSelectionLineID, string Formula,
            System.Web.UI.Control ControlContainer, bool ReadOnly)
        {
            string answerType = SRAnswerType;
            string controlID = QuestionControlID(QuestionID);

            var eControl = ControlContainer.FindControl(controlID);
            if (eControl == null)
            {

                var litSep = new Literal();
                switch (answerType)
                {
                    case "MSK":
                        {
                            var msk = MaskedTextBoxControl(controlID, AnswerWidth, QuestionAnswerSelectionID);
                            msk.Text = AnswerText;
                            msk.ReadOnly = ReadOnly;
                            //msk.AutoPostBack = true;
                            //msk.TextChanged += new EventHandler(msk_TextChanged);
                            ControlContainer.Controls.Add(msk);
                            break;
                        }
                    case "DAT":
                        {
                            var dat = DatePickerControl(controlID, AnswerWidth);
                            dat.Enabled = !ReadOnly;
                            //dat.AutoPostBack = true;
                            //dat.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(dat_SelectedDateChanged);
                            ControlContainer.Controls.Add(dat);
                            break;
                        }
                    case "TIM":
                        {
                            var tim = TimePickerControl(controlID, AnswerWidth);
                            tim.Enabled = !ReadOnly;
                            //tim.AutoPostBack = true;
                            //tim.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(tim_SelectedDateChanged);
                            ControlContainer.Controls.Add(tim);
                            break;
                        }
                    case "LBL":
                        //AddRowLabel(tblRow, question);
                        break;
                    case "NUM":
                        {
                            var num = RadNumericTextBoxControl(controlID, AnswerDecimalDigit, AnswerSuffix, AnswerWidth);
                            if (AnswerNum.HasValue)
                            {
                                num.Value = AnswerNum.Value;
                            }


                            num.ReadOnly = ReadOnly;
                            //num.AutoPostBack = true;
                            //num.TextChanged += new EventHandler(num_TextChanged);

                            if (!string.IsNullOrEmpty(Formula) && !num.ReadOnly)
                            {
                                num.ShowButton = true;
                                num.ClientEvents.OnButtonClick = "fillFormulaField";
                            }

                            ControlContainer.Controls.Add(num);
                            break;
                        }
                    case "MEM":
                        {
                            var mem = MemoControl(controlID, AnswerWidth);
                            mem.Text = AnswerText.Replace("<br />", "\n");
                            mem.ReadOnly = ReadOnly;
                            //mem.AutoPostBack = true;
                            //mem.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(mem);
                            break;
                        }
                    case "TXT":
                        {
                            var txt = TextBoxControl(controlID, AnswerWidth);
                            txt.Text = AnswerText;
                            txt.ReadOnly = ReadOnly;
                            //txt.AutoPostBack = true;
                            //txt.TextChanged += new EventHandler(txt_TextChanged);

                            if (!string.IsNullOrEmpty(Formula) && !txt.ReadOnly)
                            {
                                txt.ShowButton = true;
                                txt.ClientEvents.OnButtonClick = "fillFormulaField";
                            }

                            ControlContainer.Controls.Add(txt);
                            break;
                        }
                    case "CBL": // Combobox dg list item dari web service ComboBoxDataService
                    case "CBR": // Combobox dg list item dari AppStandardReference
                    case "CBO": // Combobox dg list item dari QuestionAnswerSelectionLine
                        {
                            var cbo = ComboBoxControl(controlID, QuestionAnswerSelectionID,
                                            QuestionAnswerDefaultSelectionID, AnswerWidth, answerType);
                            cbo.SelectedValue = AnswerSelectionLineID;
                            cbo.Enabled = !ReadOnly;
                            //cbo.AutoPostBack = true;
                            //cbo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbo);
                            break;
                        }
                    case "RBT":
                        {
                            var rbl = RadioButtonControl(controlID, QuestionAnswerSelectionID, QuestionAnswerDefaultSelectionID);

                            //nurul, ketika klik edit pilihan masih tersimpan
                            if (!string.IsNullOrEmpty(AnswerSelectionLineID)) // error jika tidak ada dalam pilihan
                                rbl.SelectedValue = AnswerSelectionLineID;

                            rbl.Enabled = !ReadOnly;
                            ControlContainer.Controls.Add(rbl);
                            break;
                        }
                    case "CHK":
                        {
                            var chk = CheckBoxControl(controlID, QuestionText, AnswerWidth);
                            chk.Checked = AnswerText.Equals("1");
                            chk.Enabled = !ReadOnly;
                            //chk.AutoPostBack = true;
                            //chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                            ControlContainer.Controls.Add(chk);
                            break;
                        }
                    case "TTX":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var txt1 = TextBoxControl(controlID, AnswerWidth);
                            txt1.Text = (txtNat.Count() > 0) ? txtNat[0] : string.Empty;
                            txt1.ReadOnly = ReadOnly;
                            //txt1.AutoPostBack = true;
                            //txt1.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(txt1);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var txt2 = TextBoxControl(controlID + "_2", AnswerWidth2);
                            txt2.Text = (txtNat.Count() > 1) ? txtNat[1] : string.Empty;
                            txt2.ReadOnly = ReadOnly;
                            //txt2.AutoPostBack = true;
                            //txt2.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(txt2);
                            break;
                        }
                    case "CTX":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var ctxChk = CheckBoxControl(controlID, QuestionText, AnswerWidth);
                            ctxChk.Checked = (txtNat.Count() > 0) ? txtNat[0].Equals("1") : false;
                            ctxChk.Enabled = !ReadOnly;
                            //ctxChk.AutoPostBack = true;
                            //ctxChk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                            ControlContainer.Controls.Add(ctxChk);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var ctxTxt = TextBoxControl(controlID + "_2", AnswerWidth2);
                            ctxTxt.Text = (txtNat.Count() > 1) ? txtNat[1] : string.Empty;
                            ctxTxt.ReadOnly = ReadOnly;
                            //ctxTxt.AutoPostBack = true;
                            //ctxTxt.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(ctxTxt);
                            break;
                        }
                    case "CTM":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var ctmChk = CheckBoxControl(controlID, QuestionText, AnswerWidth);
                            ctmChk.Checked = (txtNat.Count() > 0) ? txtNat[0].Equals("1") : false;
                            ctmChk.Enabled = !ReadOnly;
                            //ctmChk.AutoPostBack = true;
                            //ctmChk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                            ControlContainer.Controls.Add(ctmChk);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var ctmTxt = MemoControl(controlID + "_2", AnswerWidth2);
                            ctmTxt.Text = (txtNat.Count() > 1) ? txtNat[1] : string.Empty;
                            ctmTxt.ReadOnly = ReadOnly;
                            //ctmTxt.AutoPostBack = true;
                            //ctmTxt.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(ctmTxt);
                            break;
                        }
                    case "CNM":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var cnmChk = CheckBoxControl(controlID, QuestionText, AnswerWidth);
                            cnmChk.Checked = (txtNat.Count() > 0) ? txtNat[0].Equals("1") : false;
                            cnmChk.Enabled = !ReadOnly;
                            //cnmChk.AutoPostBack = true;
                            //cnmChk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                            ControlContainer.Controls.Add(cnmChk);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cnmNum = RadNumericTextBoxControl(controlID + "_2", AnswerDecimalDigit,
                                                     AnswerSuffix, AnswerWidth2);
                            Double vDouble = 0;
                            try
                            { vDouble = System.Convert.ToDouble(txtNat[1]); }
                            catch (Exception e) { }
                            cnmNum.Value = (txtNat.Count() > 1) ? vDouble : 0;
                            cnmNum.ReadOnly = ReadOnly;
                            //cnmNum.AutoPostBack = true;
                            //cnmNum.TextChanged += new EventHandler(num_TextChanged);
                            ControlContainer.Controls.Add(cnmNum);
                            break;
                        }
                    case "CDT":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var cnmChk = CheckBoxControl(controlID, QuestionText, AnswerWidth);
                            cnmChk.Checked = (txtNat.Count() > 0) ? txtNat[0].Equals("1") : false;
                            cnmChk.Enabled = !ReadOnly;
                            //cnmChk.AutoPostBack = true;
                            //cnmChk.CheckedChanged += new EventHandler(chk_CheckedChanged);
                            ControlContainer.Controls.Add(cnmChk);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cnmNum = DateTimePickerControl(controlID + "_2", AnswerWidth2);

                            try
                            {
                                cnmNum.SelectedDate = System.Convert.ToDateTime(txtNat[1]);
                            }
                            catch (Exception e) { }

                            cnmNum.DatePopupButton.Enabled = false;
                            cnmNum.TimePopupButton.Enabled = false;
                            cnmNum.DateInput.ReadOnly = true;

                            //cnmNum.AutoPostBack = true;
                            //cnmNum.TextChanged += new EventHandler(num_TextChanged);
                            ControlContainer.Controls.Add(cnmNum);
                            break;
                        }
                    case "CB2":
                        {
                            var cbo1 = ComboBoxControl(controlID, QuestionAnswerSelectionID,
                                            QuestionAnswerDefaultSelectionID, AnswerWidth, "CBO");
                            cbo1.Enabled = !ReadOnly;
                            //cbo1.AutoPostBack = true;
                            //cbo1.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbo1);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cbo2 = ComboBoxControl(controlID + "_2", QuestionAnswerSelectionID2,
                                            QuestionAnswerDefaultSelectionID2, AnswerWidth2, "CBO");
                            cbo2.Enabled = !ReadOnly;
                            //cbo2.AutoPostBack = true;
                            //cbo2.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbo2);
                            break;
                        }
                    case "CBT":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var cbCbo = ComboBoxControl(controlID, QuestionAnswerSelectionID,
                                            QuestionAnswerDefaultSelectionID, AnswerWidth, "CBO");
                            cbCbo.SelectedValue = AnswerSelectionLineID;
                            cbCbo.Enabled = !ReadOnly;
                            //cbCbo.AutoPostBack = true;
                            //cbCbo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbCbo);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cbTxt = TextBoxControl(controlID + "_2", AnswerWidth2);
                            cbTxt.Text = (txtNat.Count() > 1) ? txtNat[1] : string.Empty;
                            cbTxt.ReadOnly = ReadOnly;
                            //cbTxt.AutoPostBack = true;
                            //cbTxt.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(cbTxt);
                            break;
                        }
                    case "CBN":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var cbCbo = ComboBoxControl(controlID, QuestionAnswerSelectionID,
                                            QuestionAnswerDefaultSelectionID, AnswerWidth, "CBO");
                            cbCbo.SelectedValue = AnswerSelectionLineID;
                            cbCbo.Enabled = !ReadOnly;
                            //cbCbo.AutoPostBack = true;
                            //cbCbo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbCbo);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cnmNum = RadNumericTextBoxControl(controlID + "_2", AnswerDecimalDigit,
                                                     AnswerSuffix, AnswerWidth2);
                            Double vDouble = 0;
                            try
                            { vDouble = System.Convert.ToDouble(txtNat[1]); }
                            catch (Exception e) { }
                            cnmNum.Value = (txtNat.Count() > 1) ? vDouble : 0;
                            cnmNum.ReadOnly = ReadOnly;
                            //cnmNum.AutoPostBack = true;
                            //cnmNum.TextChanged += new EventHandler(num_TextChanged);
                            ControlContainer.Controls.Add(cnmNum);
                            break;
                        }
                    case "CBM":
                        {
                            string[] txtNat = (AnswerText == string.Empty) ? (new string[] { string.Empty }) : AnswerText.Split(new char[] { '|' });
                            var cbCbm = ComboBoxControl(controlID, QuestionAnswerSelectionID,
                                            QuestionAnswerDefaultSelectionID, AnswerWidth, "CBO");
                            cbCbm.SelectedValue = AnswerSelectionLineID;
                            cbCbm.Enabled = !ReadOnly;
                            //cbCbm.AutoPostBack = true;
                            //cbCbm.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbo_SelectedIndexChanged);
                            ControlContainer.Controls.Add(cbCbm);
                            litSep = new Literal();
                            litSep.Text = string.Format("&nbsp;{0}&nbsp;", AnswerPrefix);
                            ControlContainer.Controls.Add(litSep);
                            var cbTxm = MemoControl(controlID + "_2", AnswerWidth2);
                            cbTxm.Text = (txtNat.Count() > 1) ? txtNat[1] : string.Empty;
                            cbTxm.ReadOnly = ReadOnly;
                            //cbTxm.AutoPostBack = true;
                            //cbTxm.TextChanged += new EventHandler(txt_TextChanged);
                            ControlContainer.Controls.Add(cbTxm);
                            break;
                        }
                        //case "DNT": //Dental Control
                        //    ControlContainer.Controls.Add(ctlDental);
                        //    break;
                }
            }
            return ControlContainer;
        }

        public static string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }
        #region control builder
        private static RadDatePicker DatePickerControl(string id, int width)
        {
            var obj = new RadDatePicker();
            obj.ID = id;
            obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private static RadTimePicker TimePickerControl(string id, int width)
        {
            var obj = new RadTimePicker();
            obj.ID = id;
            obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }

        private static CheckBox CheckBoxControl(string id, string text, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            chk.Width = Unit.Pixel(width == 0 ? 300 : width);
            chk.Text = text;
            return chk;
        }
        private static RadTextBox TextBoxControl(string id, int width)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }
        private static RadTextBox MemoControl(string id, int width)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            //textBox.Height = Unit.Pixel(100);
            textBox.TextMode = InputMode.MultiLine;
            return textBox;
        }
        private static RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            if (!string.IsNullOrEmpty(suffix))
                textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;")
                                                           ? string.Empty
                                                           : string.Format("n {0}", suffix);
            textBox.Value = 0;
            return textBox;
        }
        private static RadDateTimePicker DateTimePickerControl(string id, int width)
        {
            var obj = new RadDateTimePicker();
            obj.ID = id;
            obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 170 : width);
            //obj.TimeView.TimeFormat = "HH:mm tt";
            return obj;
        }
        private static RadMaskedTextBox MaskedTextBoxControl(string id, int width, string mask)
        {
            var textBox = new RadMaskedTextBox();
            textBox.ID = id;
            textBox.Mask = mask;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }

        // Diganti function yg dibawahnya untuk tambahan tipe CBR dan CBL (Handono 230331)
        //private static RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width)
        //{
        //    var cbo = new RadComboBox();
        //    cbo.ID = id;
        //    cbo.Width = Unit.Pixel(width == 0 ? 304 : width);


        //    if (selectionID.Contains("[RANGE_"))
        //    {
        //        // Add range selection ex. [RANGE_1_TO_10], [RANGE_0_TO_100_STEP_10] (Handono 230308)
        //        var ranges = selectionID.Substring(0, selectionID.Length - 1).Split('_');
        //        var from = ranges[1].ToInt();
        //        var to = ranges[3].ToInt();

        //        var step = 1;
        //        if (selectionID.Contains("_STEP_"))
        //        {
        //            step = ranges[5].ToInt();
        //        }
        //        to = to + step;
        //        cbo.Items.Clear();
        //        cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //        for (int i = from; i < to; i = i + step)
        //        {
        //            cbo.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
        //        }
        //    }
        //    else
        //    {
        //        var query = new QuestionAnswerSelectionLineQuery();
        //        query.Where(query.QuestionAnswerSelectionID == selectionID);
        //        query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
        //        DataTable dtb = query.LoadDataTable();
        //        cbo.Items.Clear();
        //        cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //        foreach (DataRow row in dtb.Rows)
        //        {
        //            cbo.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(),
        //                                                   row["QuestionAnswerSelectionLineID"].ToString()));
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(defaultSelectionID))
        //        cbo.SelectedValue = defaultSelectionID;

        //    return cbo;
        //}

        private static RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width, string cboType)
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
                        coll.Query.OrderBy(coll.Query.LineNumber.Ascending, coll.Query.ItemName.Ascending);
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


            // Set Default value
            if (!string.IsNullOrEmpty(defaultSelectionID))
                ComboBox.SelectedValue(cbo, defaultSelectionID);

            return cbo;
        }

        private static RadioButtonList RadioButtonControl(string id, string selectionID, string defaultSelectionID)
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
                //rbl.Items.Clear();
                var isVertical = false;
                foreach (DataRow row in dtb.Rows)
                {
                    var text = row["QuestionAnswerSelectionLineText"].ToString();
                    rbl.Items.Add(new ListItem(text, row["QuestionAnswerSelectionLineID"].ToString()));
                    if (text.Length > 40)
                        isVertical = true;
                }

                //Nurul - Kondisi jika pilihan nya lebih dari 5 maka direction menjadi Vertikal
                // Jika lebar pilihan ada >40 char maka dibuat vertical (Handono 2023 nov 30)
                if (dtb.Rows.Count > 5 || isVertical)
                {
                    rbl.RepeatDirection = RepeatDirection.Vertical;
                }
                else
                {
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                }
            }
            return rbl;
        }

        #endregion
        #region AutoPostBack Event Handler
        private void msk_TextChanged(object sender, EventArgs e)
        {
            var obj = ((RadMaskedTextBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadMaskedTextBox)eControl).Text = obj.Text;

                    AutoCheckSO(item);
                }
            }
        }
        private void dat_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            var obj = ((RadDatePicker)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadDatePicker)eControl).SelectedDate = obj.SelectedDate;

                    AutoCheckSO(item);
                }
            }
        }
        private void tim_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            var obj = ((RadTimePicker)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadTimePicker)eControl).SelectedDate = obj.SelectedDate;

                    AutoCheckSO(item);
                }
            }
        }
        private void num_TextChanged(object sender, EventArgs e)
        {
            var obj = ((RadNumericTextBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadNumericTextBox)eControl).Text = obj.Text;

                    AutoCheckSO(item);
                }
            }
        }
        private void txt_TextChanged(object sender, EventArgs e)
        {
            var obj = ((RadTextBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadTextBox)eControl).Text = obj.Text;

                    AutoCheckSO(item);
                }
            }
        }
        private void cbo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var obj = ((RadComboBox)o);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((RadComboBox)eControl).SelectedIndex = obj.SelectedIndex;

                    AutoCheckSO(item);
                }
            }
        }
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            var obj = ((CheckBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                string controlID = QuestionControlID(id);
                if (controlID == obj.ID)
                {
                    var eControl = item.FindControl(controlID);
                    ((CheckBox)eControl).Checked = obj.Checked;

                    AutoCheckSO(item);
                }
            }
        }
        protected void chkS_CheckedChanged(object sender, EventArgs e)
        {
            var obj = ((CheckBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                if (id == obj.ToolTip)
                {
                    var eControl = item.FindControl("defaultChkBoxS");
                    if (eControl != null)
                    {
                        ((CheckBox)eControl).Checked = obj.Checked;
                    }
                }
            }
        }
        protected void chkO_CheckedChanged(object sender, EventArgs e)
        {
            var obj = ((CheckBox)sender);
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                if (id == obj.ToolTip)
                {
                    var eControl = item.FindControl("defaultChkBoxO");
                    if (eControl != null)
                    {
                        ((CheckBox)eControl).Checked = obj.Checked;
                    }
                }
            }
        }
        #endregion
        private void AutoCheckSO(GridDataItem item)
        {
            // autocheck subjective dan objective
            var chkS = item.FindControl("defaultChkBoxS");
            if (chkS != null)
            {
                ((CheckBox)chkS).Checked = true && ((CheckBox)chkS).Visible;
            }
            var chkO = item.FindControl("defaultChkBoxO");
            if (chkO != null)
            {
                ((CheckBox)chkO).Checked = true && ((CheckBox)chkO).Visible;
            }
        }
        protected void grdListAssessment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dttbl = NursingDiagnosaTransDT.NursingAssessment(ID, false);
            AssessmentViewState = dttbl;
            grdListAssessment.DataSource = dttbl;

            //string rowFilter = string.Format("[{0}] = {1} OR [{2}] = {3}", "IsSub", true, "IsObj", true);
            //(grdListAssessment.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        protected void grdListAssessment_DataBound(object sender, EventArgs e)
        {
            BuildCustomEntryAssessment();
        }

        protected void grdListAssessment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    var item = ((GridDataItem)e.Item);

            //    var id = item.GetDataKeyValue("QuestionID").ToString();

            //    DataRow[] questionRows = AssessmentViewState.Select(string.Format("QuestionID='{0}'", id));

            //    item["AnswerObj"].Controls.Clear();
            //    if (questionRows.Count() > 0) {
            //        var question = questionRows[0];
            //        if (!string.IsNullOrEmpty(question["SRAnswerType"].ToString()))
            //        {
            //            InitializedQuestion(question, item);
            //        }
            //    }
            //    //item["AnswerObj"].Controls.Add(new TextBox());

            //    //CheckBox chkCorrected = item["Corrected"].Controls[0] as CheckBox;
            //    //if (chkCorrected.Checked)
            //    //{
            //    //    item.BackColor = Color.Yellow;
            //    //    //celltoVerify1.Font.Bold = true;
            //    //    //celltoVerify1.BackColor = Color.Yellow;
            //    //}

            //    //CheckBox chk = item["IsModified"].Controls[0] as CheckBox;
            //    //if (chk.Checked)
            //    //{
            //    //    item.BackColor = Color.Red;
            //    //    //celltoVerify1.Font.Bold = true;
            //    //    //celltoVerify1.BackColor = Color.Yellow;
            //    //}
            //}
        }
    }
}
