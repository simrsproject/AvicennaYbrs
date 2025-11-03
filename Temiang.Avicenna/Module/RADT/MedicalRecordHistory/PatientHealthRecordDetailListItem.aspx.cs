// Remark by Handono, 
// Ada perubahan design Questionaire, tunggu entrian selesai dulu
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientHealthRecordDetailListItem : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);   
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //InitializedQuestion();

            if (!IsPostBack)
                PopulateEntryControl(Request.QueryString["regno"], Request.QueryString["sno"]);

        }

        private void PopulateEntryControl(string registrationNo, string sequenceNo)
        {
            PatientHealthRecord entity = new PatientHealthRecord();
            entity.Query.Where(entity.Query.RegistrationNo == registrationNo, entity.Query.QuestionFormID == sequenceNo);
            entity.Query.Load();

            OnPopulateEntryControl(entity);

        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
            //PatientHealthRecord patientHealthRecord = (PatientHealthRecord)entity;

            ////Rubah modus disini bila recorrd tidak ada
            //if (patientHealthRecord.RegistrationNo == string.Empty)
            //{
            //    return;
            //}

            //txtRegistrationNo.Text = patientHealthRecord.RegistrationNo;
            //hdnSequenceNo.Value = patientHealthRecord.SequenceNo;
            //txtRecordDate.SelectedDate = patientHealthRecord.RecordDate;
            //txtRecordTime.Text = patientHealthRecord.RecordTime;
            //PopulateEmployeeItem(patientHealthRecord.EmployeeID);
            //PopulateRegistrationInformation();

            //PopulateQuestionValue();
        }

        private void PopulateRegistrationInformation()
        {
            Registration registration = new Registration();
            if (registration.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                Patient patient = new Patient();
                patient.LoadByPrimaryKey(registration.PatientID);

                txtPatientID.Text = patient.PatientID;
                txtPatientName.Text = patient.PatientName;

                optSexFemale.Checked = (patient.Sex == "F");
                optSexFemale.Enabled = (patient.Sex == "F");
                optSexMale.Checked = (patient.Sex == "M");
                optSexMale.Enabled = (patient.Sex == "M");

                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                txtParamedicID.Text = registration.ParamedicID;

                Paramedic paramedic = new Paramedic();
                paramedic.LoadByPrimaryKey(txtParamedicID.Text);
                lblParamedicName.Text = paramedic.ParamedicName;

                txtAnamnesis.Text = registration.Anamnesis;
                txtComplaint.Text = registration.Complaint;
            }
        }

        //private void PopulateQuestionValue()
        //{
        //    PatientHealthRecordLineQuery query = new PatientHealthRecordLineQuery("a");
        //    QuestionQuery qQuest = new QuestionQuery("b");
        //    query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);
        //    query.Select
        //        (
        //            query.QuestionID,
        //            query.QuestionAnswerSelectionLineID,
        //            query.QuestionAnswerNum,
        //            query.QuestionAnswerText,
        //            qQuest.AnswerType
        //        );
        //    query.Where
        //        (
        //            query.RegistrationNo == txtRegistrationNo.Text,
        //            query.SequenceNo == hdnSequenceNo.Value.ToString()
        //        );

        //    DataTable dtbValue = query.LoadDataTable();

        //    foreach (DataRow dataRow in dtbValue.Rows)
        //    {
        //        string controlID = "quest" + dataRow["QuestionID"].ToString().Replace('.', '_');
        //        string answerType = dataRow[QuestionMetadata.ColumnNames.AnswerType].ToString();
        //        object obj = Helper.FindControlRecursive(pnlPatientHealthRecordLine, controlID);

        //        switch (answerType)
        //        {
        //            case "NUM":
        //                RadNumericTextBox numAnswerValue = (obj as RadNumericTextBox);
        //                if (!dataRow["QuestionAnswerNum"].Equals("&nbsp;") && dataRow["QuestionAnswerNum"] != DBNull.Value)
        //                    numAnswerValue.Value = Convert.ToDouble(dataRow["QuestionAnswerNum"]);
        //                break;
        //            case "TXT":
        //                RadTextBox txtAnswerValue = (obj as RadTextBox);
        //                txtAnswerValue.Text = dataRow["QuestionAnswerText"].ToString();
        //                break;
        //            case "CBO":
        //                RadComboBox cboAnswerValue = (obj as RadComboBox);
        //                cboAnswerValue.SelectedValue = dataRow["QuestionAnswerSelectionLineID"].ToString();
        //                break;
        //        }
        //    }
        //}

        //private void InitializedQuestion()
        //{
        //    Table table = new Table();
        //    table.Width = Unit.Percentage(100);
        //    table.BorderStyle = BorderStyle.Solid;

        //    pnlPatientHealthRecordLine.Controls.Clear();
        //    pnlPatientHealthRecordLine.Controls.Add(table);

        //    QuestionQuery query = new QuestionQuery("b");
        //    AppStandardReferenceItemQuery qSr = new AppStandardReferenceItemQuery("s");
        //    query.InnerJoin(qSr).On
        //        (
        //            query.SRQuestionGroup == qSr.ItemID &
        //            qSr.StandardReferenceID == "QuestionGroup"
        //        );
        //    query.OrderBy
        //        (
        //            query.SRQuestionGroup.Ascending,
        //            query.RowIndex.Ascending
        //        );

        //    query.Select
        //        (
        //            qSr.ItemName.As("QuestionGroupName"),
        //            query.QuestionID,
        //            query.QuestionText,
        //            query.AnswerType,
        //            query.QuestionAnswerSelectionID,
        //            query.AnswerSuffix,
        //            query.AnswerDecimalDigit,
        //            query.Formula
        //        );

        //    DataTable dtbQuestion = query.LoadDataTable();

        //    foreach (DataRow dataRow in dtbQuestion.Rows)
        //    {
        //        TableRow row = new TableRow();
        //        table.Rows.Add(row);
        //        string answerType = dataRow["AnswerType"].ToString();
        //        string id = "quest" + dataRow["QuestionID"].ToString().Replace('.', '_');

        //        if (answerType.Equals("LBL"))
        //        {
        //            table.Rows.Add(row);
        //            AddGroupCell(row.Cells, dataRow["QuestionText"].ToString());
        //        }
        //        else
        //            AddLabelCell(row.Cells, dataRow["QuestionText"].ToString());

        //        switch (answerType)
        //        {
        //            case "NUM":
        //                AddRadNumericTextBoxCell(row.Cells, id, int.Parse(dataRow["AnswerDecimalDigit"].ToString()), dataRow["AnswerSuffix"].ToString());
        //                AddSpacerCell(row.Cells);
        //                break;
        //            case "TXT":
        //                AddTextBoxCell(row.Cells, id);
        //                AddSpacerCell(row.Cells);
        //                break;
        //            case "CBO":
        //                AddComboBoxCell(row.Cells, id, dataRow["QuestionAnswerSelectionID"].ToString());
        //                AddSpacerCell(row.Cells);
        //                break;
        //        }

        //    }

        //    //Generate Formula Script
        //    if (!IsPostBack)
        //    {
        //        StringBuilder script = new StringBuilder();
        //        script.AppendLine("<script type='text/javascript' language='javascript'>");
        //        script.AppendLine("function fillFormulaField(){");

        //        foreach (DataRow dataRow in dtbQuestion.Rows)
        //        {
        //            if (!dataRow["Formula"].ToString().Equals(string.Empty))
        //            {
        //                string id = dataRow["QuestionID"].ToString().Replace('.', '_');

        //                // [200.020]/(([200.030]/100)*([200.030]/100))
        //                string formula = dataRow["Formula"].ToString();
        //                formula = formula.Replace('.', '_');
        //                formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
        //                formula = formula.Replace("]", "').get_value()");
        //                script.AppendFormat("var value{0}={1};", id, formula);
        //                script.AppendLine();
        //                script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
        //                script.AppendLine();
        //            }
        //        }
        //        script.AppendLine();
        //        script.AppendLine("}</script>");
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "formula", script.ToString());
        //    }

        //    //Asign Formula
        //    foreach (DataRow dataRow in dtbQuestion.Rows)
        //    {
        //        if (!dataRow["Formula"].ToString().Equals(string.Empty))
        //        {
        //            RadInputControl inputFormula = (RadInputControl)Helper.FindControlRecursive(pnlPatientHealthRecordLine, "quest" + dataRow["QuestionID"].ToString().Replace('.', '_'));
        //            inputFormula.Enabled = false;

        //            string formula = dataRow["Formula"].ToString();
        //            foreach (DataRow member in dtbQuestion.Rows)
        //            {
        //                if (formula.Contains(member["QuestionID"].ToString()))
        //                {
        //                    RadInputControl input = (RadInputControl)Helper.FindControlRecursive(pnlPatientHealthRecordLine, "quest" + member["QuestionID"].ToString().Replace('.', '_'));
        //                    input.ClientEvents.OnValueChanged = "fillFormulaField";
        //                }
        //            }
        //        }
        //    }
        //}

        //void AddTextBoxCell(TableCellCollection cells, string id)
        //{
        //    TableCell cell = new TableCell();
        //    cells.Add(cell);
        //    RadTextBox textBox = new RadTextBox();
        //    textBox.ID = id;
        //    textBox.Width = Unit.Pixel(300);
        //    cell.Controls.Add(textBox);
        //}

        //void AddRadNumericTextBoxCell(TableCellCollection cells, string id, int decimalDigit, string suffix)
        //{
        //    TableCell cell = new TableCell();
        //    cell.Attributes["class"] = "entry";
        //    cells.Add(cell);
        //    RadNumericTextBox textBox = new RadNumericTextBox();
        //    textBox.ID = id;
        //    textBox.Width = 100;
        //    textBox.NumberFormat.DecimalDigits = decimalDigit;
        //    textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;") ? string.Empty : string.Format("n {0}", suffix);
        //    cell.Controls.Add(textBox);

        //}

        //void AddComboBoxCell(TableCellCollection cells, string id, string selectionID)
        //{
        //    TableCell cell = new TableCell();
        //    cell.Attributes["class"] = "entry";
        //    cells.Add(cell);
        //    RadComboBox comboBox = new RadComboBox();
        //    comboBox.ID = id;
        //    comboBox.Width = Unit.Pixel(300);
        //    cell.Controls.Add(comboBox);
        //    QuestionAnswerSelectionLineQuery query = new QuestionAnswerSelectionLineQuery();
        //    query.Where(query.QuestionAnswerSelectionID == selectionID);
        //    query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(), row["QuestionAnswerSelectionLineID"].ToString()));
        //    }
        //}

        //void AddLabelCell(TableCellCollection cells, string text)
        //{
        //    TableCell cell = new TableCell();
        //    cell.Text = text;
        //    cell.Wrap = false;
        //    cell.Attributes["class"] = "label";
        //    cells.Add(cell);

        //}

        //void AddGroupCell(TableCellCollection cells, string text)
        //{
        //    TableCell cell = new TableCell();
        //    cells.Add(cell);
        //    cell.Text = text;
        //    cell.Attributes["class"] = "labelcaption";
        //    cell.ColumnSpan = 3;

        //}

        //void AddSpacerCell(TableCellCollection cells)
        //{
        //    TableCell cell = new TableCell();
        //    cells.Add(cell);
        //}

        #region ComboBox EmployeeID

        protected void cboEmployeeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateEmployeeItem(e.Text);
        }

        private void PopulateEmployeeItem(string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;

            //EmployeeQuery query = new EmployeeQuery("a");
            //query.es.Top = 20;
            //query.Where
            //    (
            //        query.Or
            //            (
            //                query.EmployeeID == textSearch,
            //                query.EmployeeName.Like(string.Format("%.{0}%", textSearch))
            //            ),
            //        query.IsActive == true
            //    );


            //DataTable dtb = query.LoadDataTable();

            //cboEmployeeID.DataSource = dtb;
            //cboEmployeeID.DataBind();
           
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmployeeID"].ToString();
        }

        #endregion

    }
}
