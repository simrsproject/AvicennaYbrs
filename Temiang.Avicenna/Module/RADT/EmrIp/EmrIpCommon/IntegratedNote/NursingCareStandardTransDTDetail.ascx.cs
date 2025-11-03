using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Module.RADT.Cpoe;
using System.Web.UI.HtmlControls;
using Temiang.Avicenna.Module.Emr.Phr;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NursingCareStandardTransDTDetail : BaseUserControl
    {
        private object _dataItem;

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string NursingTransNo
        {
            get
            {
                if (Session["NursingTransNo" + RegistrationNo] != null)
                {
                    return Session["NursingTransNo" + RegistrationNo].ToString();
                }

                return string.Empty;
            }
            set
            {
                Session["NursingTransNo" + RegistrationNo] = value;
            }
        }

        private NursingDiagnosaTransDTCollection NewOrEditedNursingImplementations
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["collNursingImplementation" + RegistrationNo];
                if (obj != null)
                {
                    return ((NursingDiagnosaTransDTCollection)(obj));
                }
                //}

                //var coll = NursingDiagnosaTransDT.Implementation(NursingTransNo, 500);
                var coll = new NursingDiagnosaTransDTCollection();
                Session["collNursingImplementation" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNursingImplementation" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        public string SelectedParentID
        {
            get
            {
                var grid = (RadGrid)Helper.FindControlRecursive(Page, "gridListImplementasiDiagnosa");
                string parentID = string.Empty;
                if (grid.SelectedItems.Count > 0)
                {
                    GridDataItem item = (GridDataItem)grid.MasterTableView.Items[grid.SelectedItems[0].ItemIndex];
                    parentID = item.GetDataKeyValue(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID).ToString();
                }
                return parentID;
            }
        }
        public string SelectedParentText
        {
            get
            {
                var grid = (RadGrid)Helper.FindControlRecursive(Page, "gridListImplementasiDiagnosa");
                string parentID = string.Empty;
                if (grid.SelectedItems.Count > 0)
                {
                    GridDataItem item = (GridDataItem)grid.MasterTableView.Items[grid.SelectedItems[0].ItemIndex];
                    return item.Cells[5].Text;
                }
                //NursingDiagnosaNameEdited
                return string.Empty;
            }
        }

        public GridTableView GridListImplementasiDiagnosaDS
        {
            get
            {
                var grid = (RadGrid)Helper.FindControlRecursive(Page, "gridListImplementasiDiagnosa");
                return grid.MasterTableView;
            }
        }

        public string SessionName
        {
            get
            {
                return "collNursingImplementation" + RegistrationNo; //_" + SelectedParentID;
            }
        }

        private string OldData
        {
            get
            {
                return (Session["OldTemplateData" + RegistrationNo] ?? string.Empty).ToString();
            }
            set
            {
                Session["OldTemplateData" + RegistrationNo] = value;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            LoadInputTemplate();
            OldData = string.Empty;

            //NewOrEditedPhrLine = null;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtCustomRespond.Text = string.Empty;
                hfID.Value = string.Empty;
                hfTmpNursingDiagnosaID.Value = NursingDiagnosaTransDT.GetTmpNursingDiagnosaID(NewOrEditedNursingImplementations, NursingTransNo);

                txtS.Text = string.Empty; txtO.Text = string.Empty;
                txtA.Text = string.Empty; txtP.Text = string.Empty;
                txtPpaInstruction.Text = string.Empty;
                txtSubmitBy.Text = string.Empty;
                txtReceiveBy.Text = string.Empty;
                EnableRegularInput();

                // load default if nic is selected
                if (!string.IsNullOrEmpty(SelectedParentID))
                {
                    cboNursingDiagnosa_ItemsRequested(cboNursingDiagnosa, new RadComboBoxItemsRequestedEventArgs());
                    if (cboNursingDiagnosa.Items.Count > 0)
                    {
                        cboNursingDiagnosa.SelectedIndex = cboNursingDiagnosa.Items.Count - 1;
                    }
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");

            query.Select
                (
                    query.NursingDiagnosaID,
                    query.NursingDiagnosaName
                );
            query.Where(query.NursingDiagnosaID == DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID).ToString());

            DataTable tbl = query.LoadDataTable();
            tbl = AddDefault(tbl);
            cboNursingDiagnosa.DataSource = tbl;
            cboNursingDiagnosa.DataBind();
            //ComboBox.SelectedValue(cboNursingDiagnosa, (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID));
            //cboNursingDiagnosa.SelectedValue = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID);
            //if (string.IsNullOrEmpty(cboNursingDiagnosa.SelectedValue)) {
            cboNursingDiagnosa.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName);
            //}
            txtDateTimeImplementation.SelectedDate = (DateTime)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime);
            txtCustomRespond.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.Respond);
            hfID.Value = (DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ID) ?? string.Empty).ToString();
            hfTmpNursingDiagnosaID.Value = (DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID)).ToString();

            txtS.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.S);
            txtO.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.O);
            txtA.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.A);
            txtP.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.P);
            txtInfo5.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.Info5);
            txtPpaInstruction.Text = Convert.ToString(DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction));
            txtSubmitBy.Text = Convert.ToString(DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.SubmitBy));
            txtReceiveBy.Text = Convert.ToString(DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ReceiveBy));

            if (Equals((String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName), "S B A R"))
            {
                EnableSbarInput();
            }
            else if (Equals((String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName), "S O A P"))
            {
                EnableSoapInput();
            }
            else if (Equals((String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName), "ADIME"))
            {
                EnableAdimeInput();
            }
            else if (Equals((String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName), "Handover Patient"))
            {
                EnableHandoverPatientInput();
            }
            else
            {
                EnableRegularInput();
            }

            hrRelatedPHRNo.Value = DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ReferenceToPhrNo).ToString();
            var phrlColl = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrEmpty(hrRelatedPHRNo.Value))
            {
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == hrRelatedPHRNo.Value);
                if (phrlColl.LoadAll())
                {
                    foreach (var phrl in phrlColl)
                    {
                        NewOrEditedPhrLine.AttachEntity(phrl);
                    }
                }

                var phrC = GetCurrentPHRLines(NewOrEditedPhrLine);
                var cColl = new QuestionCollection();
                if (phrC.Count() > 0)
                {
                    cColl.Query.Where(cColl.Query.QuestionID.In(phrC.Select(x => x.QuestionID)));
                    cColl.LoadAll();
                }

                grdQuestionRespond.DataSource = cColl;
                grdQuestionRespond.DataBind();
            }

            string tid = DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.TemplateID).ToString();
            var cboItem = cboTemplate.Items.FindItemByValue(tid);
            if (cboItem != null)
            {
                cboTemplate.SelectedValue = tid;
                //cboTemplate_SelectedIndexChanged(cboTemplate, new RadComboBoxSelectedIndexChangedEventArgs(tid, tid, tid, tid));
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // input SBAR
            if (pnlSBAR.Visible)
            {
                if (string.IsNullOrEmpty(txtS.Text) &&
                    string.IsNullOrEmpty(txtO.Text) &&
                    string.IsNullOrEmpty(txtA.Text) &&
                    string.IsNullOrEmpty(txtP.Text))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("At least one of SBAR / SOAP input must be filled", NursingDiagnosaID);
                }
            }
        }

        private void VisiblePanel(string type)
        {
            pnlRespond.Visible = false;
            pnlSBAR.Visible = true;
            pnlSBAR.Visible = true;
            trInfo3.Visible = true;
            trInfo4.Visible = true;

            trtPpaInstruction.Visible = false;

            trInfo5.Visible = false;
            trReceiveBy.Visible = false;
            trSubmitBy.Visible = false;

            if (type == "ADIME")
            {
                trInfo5.Visible = true;
                trReceiveBy.Visible = false;
                trSubmitBy.Visible = false;
            }
            else if (type == "SOAP")
            {
                trtPpaInstruction.Visible = true;
            }
        }
        private void EnableSbarInput()
        {
            VisiblePanel("SBAR");
            lblS.Text = "Situation (S)"; lblO.Text = "Background (B)";
            lblA.Text = "Assessment (A)"; lblP.Text = "Recommendation (R)";
        }
        private void EnableSoapInput()
        {
            VisiblePanel("SOAP");
            lblS.Text = "Subjective (S)"; lblO.Text = "Objective (O)";
            lblA.Text = "Assessment / Diagnosis (A)"; lblP.Text = "Planning (P)";

        }
        private void EnableAdimeInput()
        {
            VisiblePanel("ADIME");
            lblS.Text = "Assessment (A)";
            lblO.Text = "Diagnosis (D)";
            lblA.Text = "Intervention (I)";
            lblP.Text = "Monitoring (M)";
            lblInfo5.Text = "Evaluation (E)";

        }
        private void EnableHandoverPatientInput()
        {
            EnableSoapInput();
            trReceiveBy.Visible = true;
            trSubmitBy.Visible = true;
        }
        private void EnableRegularInput()
        {
            pnlRespond.Visible = true;
            pnlSBAR.Visible = false;
        }
        private DataTable AddDefault(DataTable dt)
        {
            var r3 = dt.NewRow(); r3["NursingDiagnosaID"] = ""; r3["NursingDiagnosaName"] = "ADIME";
            dt.Rows.InsertAt(r3, 0);
            var r1 = dt.NewRow(); r1["NursingDiagnosaID"] = ""; r1["NursingDiagnosaName"] = "S B A R";
            dt.Rows.InsertAt(r1, 0);
            var r4 = dt.NewRow(); r4["NursingDiagnosaID"] = ""; r4["NursingDiagnosaName"] = "Handover Patient";
            dt.Rows.InsertAt(r4, 0);
            var r2 = dt.NewRow(); r2["NursingDiagnosaID"] = ""; r2["NursingDiagnosaName"] = "S O A P";
            dt.Rows.InsertAt(r2, 0);

            var re = dt.NewRow(); re["NursingDiagnosaID"] = ""; re["NursingDiagnosaName"] = "";
            dt.Rows.InsertAt(re, 0);

            dt.AcceptChanges();
            return dt;
        }
        #region Properties for return entry value
        public String NursingDiagnosaID
        {
            get { return string.Empty; /* cboNursingDiagnosa.SelectedValue;*/ }
        }

        public String NursingDiagnosaCustomText
        {
            get { return cboNursingDiagnosa.Text; }
        }

        public DateTime? ImplementationDateTime
        {
            get { return txtDateTimeImplementation.SelectedDate; }
        }

        public String Respond
        {
            get { return txtCustomRespond.Text; }
        }

        public String RecordID
        {
            get { return hfID.Value; }
        }

        public String TmpNursingDiagnosaID
        {
            get { return hfTmpNursingDiagnosaID.Value; }
        }
        public string S
        {
            get { return txtS.Text; }
        }
        public string O
        {
            get { return txtO.Text; }
        }
        public string A
        {
            get { return txtA.Text; }
        }
        public string P
        {
            get { return txtP.Text; }
        }
        public string Info5
        {
            get { return txtInfo5.Text; }
        }
        public string PpaInstruction
        {
            get { return txtPpaInstruction.Text; }
        }
        public string SubmitBy
        {
            get { return txtSubmitBy.Text; }
        }
        public string ReceiveBy
        {
            get { return txtReceiveBy.Text; }
        }

        public string TemplateID
        {
            get { return cboTemplate.SelectedValue; }
        }

        private PatientHealthRecordLineCollection NewOrEditedPhrLine
        {
            get
            {
                object obj = Session["collNewOrEditedPhrLine" + RegistrationNo];
                if (obj != null)
                {
                    return ((PatientHealthRecordLineCollection)(obj));
                }

                var coll = new PatientHealthRecordLineCollection();
                coll.Query.Select(coll.Query, "<GETDATE() as refToPatientHealthRecord_RecordDate>");
                Session["collNewOrEditedPhrLine" + RegistrationNo] = coll;
                return coll;
            }
        }

        private IEnumerable<PatientHealthRecordLine> GetCurrentPHRLines(PatientHealthRecordLineCollection hrLines)
        {
            IEnumerable<PatientHealthRecordLine> currentHrLines;
            if (!string.IsNullOrEmpty(hrRelatedPHRNo.Value))
            {
                currentHrLines = NewOrEditedPhrLine.Where(x => x.TransactionNo == hrRelatedPHRNo.Value);
            }
            else if (!string.IsNullOrEmpty(hfID.Value))
            {
                currentHrLines = NewOrEditedPhrLine.Where(x => x.QuestionFormID == hfID.Value);
            }
            else
            {
                currentHrLines = NewOrEditedPhrLine.Where(x => x.QuestionGroupID == hfTmpNursingDiagnosaID.Value);
            }
            return currentHrLines;
        }

        public void UpdatePHRLine()
        {
            var hrLines = GetCurrentPHRLines(NewOrEditedPhrLine);

            foreach (var hrl in hrLines)
            {
                hrl.MarkAsDeleted();
            }

            List<string> questionIDs = new List<string>();
            foreach (GridDataItem row in grdQuestionRespond.MasterTableView.Items)
            {
                SetPatientHealthRecordLine(NewOrEditedPhrLine, row);
                questionIDs.Add(row.GetDataKeyValue("QuestionID").ToString());
            }
        }


        private void SetPatientHealthRecordLine(/*bool isNewRecord, string transactionNo, */
            PatientHealthRecordLineCollection collValue, GridDataItem gdi/*, string questionGroupID*/)
        {
            PatientHealthRecordLine hrLine;
            string questionID = gdi.GetDataKeyValue("QuestionID").ToString();

            var q = new Question();
            q.LoadByPrimaryKey(questionID);

            IEnumerable<PatientHealthRecordLine> hrLines = GetCurrentPHRLines(collValue).Where(x => x.QuestionID == questionID);

            if (hrLines.Count() > 1) throw new Exception("Multiple question in one form");
            hrLine = hrLines.Count() == 1 ? hrLines.First() : collValue.AddNew();

            hrLine.TransactionNo = hrRelatedPHRNo.Value;
            hrLine.RegistrationNo = RegistrationNo;
            hrLine.QuestionFormID = hfID.Value;
            hrLine.QuestionGroupID = hfTmpNursingDiagnosaID.Value;
            hrLine.QuestionID = questionID;

            hrLine.QuestionAnswerPrefix = q.AnswerPrefix.ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = q.AnswerSuffix.ToStringDefaultEmpty();

            hrLine.LastUpdateDateTime = txtDateTimeImplementation.SelectedDate; // Utk setting PHR Date

            string controlID = EmrIp.MainContent.NursingCare
                .NursingCareStandardViewImportedAssessment.QuestionControlID(questionID);
            string answerType = q.SRAnswerType;
            object obj = null;

            if (answerType != "DNT") //Dental Control
            {
                if (string.IsNullOrEmpty(q.ReferenceQuestionID.ToStringDefaultEmpty()))
                    obj = Helper.FindControlRecursive(gdi, controlID);
                else
                    obj = Helper.FindControlRecursive(gdi, EmrIp.MainContent.NursingCare
                .NursingCareStandardViewImportedAssessment.QuestionControlID(q.ReferenceQuestionID.ToString()));

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
                    hrLine.QuestionAnswerText = (dat.SelectedDate ?? DateTime.Now).ToShortDateString();
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    hrLine.QuestionAnswerText = (tim.SelectedDate ?? DateTime.Now).ToString("HH:mm");
                    break;
                case "DTM":
                    var dattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = (dattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    break;
                case "MEM":
                    var memAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(memAnswerValue.Text);
                    break;
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txtAnswerValue.Text);
                    break;
                case "CBO":
                    var cboAnswerValue = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cboAnswerValue.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cboAnswerValue.Text);
                    break;
                case "RBT":
                    var rbl = (obj as RadioButtonList);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(rbl.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(rbl.SelectedItem.Text);
                    break;
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctxTxt.Text));
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctmTxt.Text));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxt.Text));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxm.Text));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txt1.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(txt2.Text));
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
                                    gdi,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                var objCellText = (objCell as RadTextBox);
                                ansText += (ansText.Equals(string.Empty) ? "" : "|") + HtmlTagHelper.Validate(objCellText.Text);
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
                        var txt = Helper.FindControlRecursive(gdi, "txtLU" + i.ToString()) as RadTextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtLU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(gdi, "txtLD" + i.ToString()) as RadTextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtLD" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(gdi, "txtRU" + i.ToString()) as RadTextBox;
                        if (txt != null && !string.IsNullOrEmpty(txt.Text.Trim()))
                        {
                            str += "txtRU" + i.ToString() + "|" + txt.Text.Trim() + ";";
                        }

                        txt = Helper.FindControlRecursive(gdi, "txtRD" + i.ToString()) as RadTextBox;
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
            }
        }

        #endregion
        #region Method & Event
        protected void cboNursingDiagnosa_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //EnableSBARInput(Equals(e.Text, "S B A R"));

            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            var t = e.Text.Replace("S B A R", "").Replace("S O A P", "");

            string searchTextContain = string.Format("%{0}%", t);

            query.es.Top = 20;
            if (!string.IsNullOrEmpty(SelectedParentID))
            {
                query.Where(query.NursingDiagnosaParentID == SelectedParentID);
                query.Where(
                query.Or(
                    query.NursingDiagnosaName.Like(searchTextContain)
                ));
            }
            else
            {
                query.Where(query.SRNursingDiagnosaLevel == "32");
                query.Where(
                query.Or(
                    query.NursingDiagnosaName.Like(searchTextContain),
                    query.NursingDiagnosaID.Like(searchTextContain)
                ));
            }

            query.Select
                (
                    query.NursingDiagnosaID,
                    query.NursingDiagnosaName
                );
            var dt = query.LoadDataTable();
            if (!string.IsNullOrEmpty(SelectedParentID) && dt.Rows.Count < 1)
            {
                // create kata kerja
                var verb = dt.NewRow(); verb["NursingDiagnosaID"] = "";
                verb["NursingDiagnosaName"] = Helper.FirstLetterToUpper(Helper.NounToVerb(SelectedParentText));
                dt.Rows.InsertAt(verb, 0);
            }

            // tambah default
            dt = AddDefault(dt);

            cboNursingDiagnosa.DataSource = dt;
            cboNursingDiagnosa.DataBind();
        }
        protected void cboNursingDiagnosa_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID].ToString();
        }
        protected void cboNursingDiagnosa_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Equals(e.Text, "S B A R"))
            {
                EnableSbarInput();
            }
            else if (Equals(e.Text, "S O A P"))
            {
                EnableSoapInput();
            }
            else if (Equals(e.Text, "ADIME"))
            {
                EnableAdimeInput();
            }
            else if (Equals(e.Text, "Handover Patient"))
            {
                EnableHandoverPatientInput();
            }
            else
            {
                pnlRespond.Visible = true;
                pnlSBAR.Visible = false;

                var t = new NursingDiagnosa();
                if (t.LoadByPrimaryKey(e.Value))
                {
                    txtCustomRespond.Text = t.RespondTemplate;
                }
            }
        }

        private void LoadInputTemplate()
        {
            var query = new NursingDiagnosaTemplateQuery("a");
            query.Where(query.IsActive == true)
                .Select(query.TemplateID,
                    query.TemplateName,
                    query.TemplateText);

            var dt = query.LoadDataTable();

            var dta = dt.Clone();
            dta.Rows.Clear();
            var dr = dta.NewRow();
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateID] = 0;
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateName] = string.Empty;
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateText] = string.Empty;
            dta.Rows.Add(dr);
            dta.Merge(dt);

            cboTemplate.DataSource = dta;
            cboTemplate.DataBind();
        }

        protected void cboTemplate_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateID].ToString();
        }

        protected void cboTemplate_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value.Equals("0"))
            {
                txtCustomRespond.Text = OldData;
                //grdQuestionRespond.DataSource = null;
            }
            else
            {
                OldData = txtCustomRespond.Text;
                var t = new NursingDiagnosaTemplate();
                if (t.LoadByPrimaryKey(int.Parse(e.Value)))
                {
                    txtCustomRespond.Text = t.TemplateText;
                }
            }
            grdQuestionRespond.Rebind();
            //BuildGridDataSourceFromTemplate();
        }
        #endregion

        private void BuildGridDataSourceFromTemplate()
        {
            QuestionCollection cColl = new QuestionCollection();
            if (cboTemplate.SelectedValue.Equals("0") || cboTemplate.SelectedValue.Equals(string.Empty) ||
                cboTemplate.Text.Trim().Equals("-"))
            {

            }
            else
            {
                int TemplateID = System.Convert.ToInt32(cboTemplate.SelectedValue);
                var rQuestion = new NursingDiagnosaTemplateDetailCollection();
                rQuestion.Query.Where(rQuestion.Query.TemplateID == TemplateID);
                if (rQuestion.LoadAll())
                {
                    cColl = NursingDiagnosaTransDT.GetQuestionsByTemplateID(TemplateID);
                }
            }
            grdQuestionRespond.DataSource = null;
            grdQuestionRespond.DataSource = cColl;
            grdQuestionRespond.DataBind();
        }

        private void BuildGridDataSourceFromSavedOrEdited()
        {
            QuestionCollection cColl = new QuestionCollection(); ;
            if (cboTemplate.SelectedValue.Equals("0") || cboTemplate.SelectedValue.Equals(string.Empty))
            {
                txtCustomRespond.Text = OldData;
            }
            else
            {

            }

        }

        protected void grdQuestionRespond_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            QuestionCollection cColl = new QuestionCollection();

            var templateID = cboTemplate.SelectedValue;
            if (templateID.Equals("0") || templateID.Equals(string.Empty))
            {
                //grdQuestionRespond.DataSource = null;
            }
            else
            {
                int templateId = System.Convert.ToInt32(cboTemplate.SelectedValue);
                var rQuestion = new NursingDiagnosaTemplateDetailCollection();
                rQuestion.Query.Where(rQuestion.Query.TemplateID == templateId);
                if (rQuestion.LoadAll())
                {
                    cColl = NursingDiagnosaTransDT.GetQuestionsByTemplateID(templateId);
                }
            }

            grdQuestionRespond.DataSource = cColl;
        }

        protected void grdQuestionRespond_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //BuildCustomEntryAssessment(e.Item as GridDataItem);

                var item = e.Item as GridDataItem;

                var id = item.GetDataKeyValue("QuestionID").ToString();

                Question q = new Question();
                q.LoadByPrimaryKey(id);

                var qAns = GetCurrentPHRLines(NewOrEditedPhrLine).Where(x => x.QuestionID == id).FirstOrDefault();

                item["AnswerObj"].Controls.Clear();
                if (!string.IsNullOrEmpty(q.SRAnswerType))
                {
                    EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment
                        .InitializedQuestion(q, item["AnswerObj"],
                        qAns == null ? string.Empty : qAns.QuestionAnswerText,
                        qAns == null ? new double() : System.Convert.ToDouble(qAns.QuestionAnswerNum),
                        qAns == null ? string.Empty : qAns.QuestionAnswerSelectionLineID, false);
                }
            }
        }
        protected void grdQuestionRespond_ItemDataBound(object source, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //BuildCustomEntryAssessment(e.Item as GridDataItem);

                var item = e.Item as GridDataItem;

                var id = item.GetDataKeyValue("QuestionID").ToString();

                Question q = item.DataItem as Question;
                if (q == null)
                    return;

                var qAns = GetCurrentPHRLines(NewOrEditedPhrLine).Where(x => x.QuestionID == id).FirstOrDefault();

                item["AnswerObj"].Controls.Clear();
                if (!string.IsNullOrEmpty(q.SRAnswerType))
                {
                    EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment
                        .InitializedQuestion(q, item["AnswerObj"],
                        qAns == null ? string.Empty : qAns.QuestionAnswerText,
                        qAns == null ? new double() : System.Convert.ToDouble(qAns.QuestionAnswerNum),
                        qAns == null ? string.Empty : qAns.QuestionAnswerSelectionLineID, false);
                }
            }
        }

        private void BuildCustomEntryAssessment(GridDataItem item)
        {
            var id = item.GetDataKeyValue("QuestionID").ToString();

            Question q = item.DataItem as Question;
            if (q == null)
                return;

            var qAns = NewOrEditedPhrLine.Where(n => n.QuestionID == id).FirstOrDefault();

            item["AnswerObj"].Controls.Clear();
            if (!string.IsNullOrEmpty(q.SRAnswerType))
            {
                EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment
                    .InitializedQuestion(q, item["AnswerObj"],
                    qAns == null ? string.Empty : qAns.QuestionAnswerText,
                    qAns == null ? new double() : System.Convert.ToDouble(qAns.QuestionAnswerNum),
                    qAns == null ? string.Empty : qAns.QuestionAnswerSelectionLineID, false);
            }
        }
    }
}
