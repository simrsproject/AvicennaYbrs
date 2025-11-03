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
    public partial class NursingCarePickQuestions : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildCustomEntryAssessment();
            if (!IsPostBack)
            {
                grdListAssessment.DataBind();
            }
        }
        private string GetAns(GridDataItem row) {
            var Ass = string.Empty;
            var ansList = NursingDiagnosaTransDT.GetAssessmentValueList(
                            row["SRAnswerType"].Text, row["QuestionAnswerText"].Text,
                            System.Convert.ToDecimal((row["QuestionAnswerNum"].Text == "") ? "0" : row["QuestionAnswerNum"].Text),
                            row["QuestionText"].Text,
                            System.Convert.ToInt32(row["AnswerDecimalDigit"].Text),
                            row["AnswerSuffix"].Text
                            );
            foreach (var r in ansList)
            {
                Ass += ((Ass.Length == 0) ? "" : ", ") + r.AssessmentName;
            }
            return Ass;
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string sRetDS = string.Empty;
            string sRetDO = string.Empty;
            foreach (GridDataItem row in grdListAssessment.MasterTableView.Items)
            {
                CheckBox chkDS = row.FindControl("chkDS") as CheckBox;
                if (chkDS != null)
                {
                    if (chkDS.Checked)
                    {
                        sRetDS = sRetDS + (sRetDS.Length > 0 ? ", " : "") + GetAns(row);
                    }
                }

                CheckBox chkDO = row.FindControl("chkDO") as CheckBox;
                if (chkDO != null)
                {
                    if (chkDO.Checked)
                    {
                        sRetDO = sRetDO + (sRetDO.Length > 0 ? ", " : "") + GetAns(row);
                    }
                }
            }

            do
            {
                sRetDS = sRetDS.Replace("  ", " ");
            } while (sRetDS.IndexOf("  ") >= 0);
            do
            {
                sRetDO = sRetDO.Replace("  ", " ");
            } while (sRetDO.IndexOf("  ") >= 0);

            sRetDS = sRetDS.Replace("\r\n", "").Replace("\t", "").Trim();
            sRetDO = sRetDO.Replace("\r\n", "").Replace("\t", "").Trim();

            sRetDS = Helper.StripHTML(sRetDS);
            sRetDO = Helper.StripHTML(sRetDO);
            sRetDS = System.Uri.EscapeUriString(sRetDS);
            sRetDO = System.Uri.EscapeUriString(sRetDO);
            sRetDS = sRetDS.Replace("'", "\\'");//HttpUtility.JavaScriptStringEncode(sRetDS);
            sRetDO = sRetDO.Replace("'", "\\'");// HttpUtility.JavaScriptStringEncode(sRetDO);

            if (string.IsNullOrEmpty(sRetDS) && string.IsNullOrEmpty(sRetDO))
            {
                return "oWnd.argument.result = 'Gak OK'";
            }

            return string.Format("oWnd.argument.result = 'OK'; oWnd.argument.dataDS = '{0}'; oWnd.argument.dataDO = '{1}';",
                sRetDS, sRetDO);
        }
        public override bool OnButtonOkClicked()
        {

            return true;
        }

        private string NSDiagnosaType
        {
            get
            {
                return Request.QueryString["diagtype"];
            }
        }

        public DataTable AssessmentViewState
        {
            get
            {
                if (ViewState["AssessmentViewState"] != null)
                {
                    return (DataTable)ViewState["AssessmentViewState"];
                }

                return null;
            }
            set
            {
                ViewState["AssessmentViewState"] = value;
            }
        }

        private void BuildCustomEntryAssessment()
        {
            // get saved value
            foreach (GridDataItem item in grdListAssessment.MasterTableView.Items)
            {
                var id = item.GetDataKeyValue("QuestionID").ToString();
                DataRow[] questionRows = AssessmentViewState.Select(string.Format("QuestionID='{0}'", id));

                item["AnswerObj"].Controls.Clear();
                if (questionRows.Count() > 0)
                {
                    var question = questionRows[0];
                    if (!string.IsNullOrEmpty(question["SRAnswerType"].ToString()))
                    {

                        NursingCareStandardViewImportedAssessment.InitializedQuestion(
                            question["QuestionID"].ToString(), question["QuestionText"].ToString(), question["SRAnswerType"].ToString(),
                            question["AnswerWidth"] is DBNull ? 0 : question["AnswerWidth"].ToInt(),
                            question["AnswerDecimalDigit"] is DBNull ? 0 : int.Parse(question["AnswerDecimalDigit"].ToString()),
                            question["AnswerPrefix"].ToString(), question["AnswerSuffix"].ToString(),
                            question["AnswerWidth2"] is DBNull ? 0 : question["AnswerWidth2"].ToInt(),
                            question["QuestionAnswerSelectionID"].ToStringDefaultEmpty(),
                            question["QuestionAnswerDefaultSelectionID"].ToString(),
                            question["QuestionAnswerSelectionID2"].ToStringDefaultEmpty(),
                            question["QuestionAnswerDefaultSelectionID2"].ToString(),
                            question["QuestionAnswerText"].ToString(),
                            question["QuestionAnswerNum"] is DBNull ? 0 : System.Convert.ToDouble(question["QuestionAnswerNum"]),
                            question["QuestionAnswerSelectionLineID"].ToString(), 
                            question["Formula"].ToString(),
                            item["AnswerObj"], true);
                    }
                }
            }
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

        private static RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width)
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

        private static RadioButtonList RadioButtonControl(string id, string selectionID, string defaultSelectionID)
        {
            var rbl = new RadioButtonList();
            rbl.ID = id;
            //opt.Width = Unit.Pixel(width == 0 ? 1000 : width);
            var query = new QuestionAnswerSelectionLineQuery();
            query.Where(query.QuestionAnswerSelectionID == selectionID);
            query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
            var dtb = query.LoadDataTable();
            rbl.Items.Clear();
            foreach (DataRow row in dtb.Rows)
            {
                rbl.Items.Add(new ListItem(row["QuestionAnswerSelectionLineText"].ToString(),
                                                       row["QuestionAnswerSelectionLineID"].ToString()));
            }

            //Nurul - Kondisi jika pilihan nya lebih dari 5 maka direction menjadi Vertikal
            if (dtb.Rows.Count > 5)
            {
                rbl.RepeatDirection = RepeatDirection.Vertical;
            }
            else
            {
                rbl.RepeatDirection = RepeatDirection.Horizontal;
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
            var dttbl = (new PatientHealthRecordLineCollection()).GetPHRWithQuestion(RegistrationNo, NSDiagnosaType);
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
