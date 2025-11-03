using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingAssessmentDiagnosaDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        public RadComboBox cboSRAnswerType
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRAnswerType");
            }
        }

        private void ToggleEnableInputCheckbox(bool val) {
            chkCheck.Enabled = val;
        }

        private void ToggleEnableInputFreeText(bool val)
        {
            cboOperand.Enabled = val;
            txtAcceptedValues.Enabled = val;
            txtNumAceptedValue.ReadOnly = true;
            txtNumAceptedValue2.ReadOnly = true;
        }

        private void ToggleEnableInputNumeric(bool val)
        {
            cboOperand.Enabled = val;
            txtAcceptedValues.Enabled = false;
            txtNumAceptedValue.ReadOnly = !val;
            txtNumAceptedValue2.ReadOnly = !val || !chkUsingRange.Checked;
        }

        private void SetupInputFreeText(bool val) {
            cboOperand.Items.Clear();
            cboOperand.Items.Add(new RadComboBoxItem("", ""));
            if (val)
            {
                cboOperand.Items.Add(new RadComboBoxItem("=", "="));
                cboOperand.Items.Add(new RadComboBoxItem("Like", "Like"));
                cboOperand.Items.Add(new RadComboBoxItem("NotLike", "Not Like"));
            }
            ToggleEnableInputFreeText(val);
        }

        protected void chkUsingRange_OnCheckedChanged(object sender, EventArgs e)
        {
            FormatInterface();
        }

        private void SetupInputNumeric(bool val)
        {
            cboOperand.Items.Clear();
            cboOperand.Items.Add(new RadComboBoxItem("", ""));
            if (val)
            {
                if (chkUsingRange.Checked)
                {
                    cboOperand.Items.Add(new RadComboBoxItem("> Val1 & < Val2", ">&<"));
                    cboOperand.Items.Add(new RadComboBoxItem(">= Val1 & <= Val2", ">=&<="));
                    cboOperand.Items.Add(new RadComboBoxItem("< Val1 | > Val2", "<|>"));
                    cboOperand.Items.Add(new RadComboBoxItem("<= Val1 | >= Val2", "<=|>="));
                }
                else
                {
                    cboOperand.Items.Add(new RadComboBoxItem("=", "="));
                    cboOperand.Items.Add(new RadComboBoxItem(">", ">"));
                    cboOperand.Items.Add(new RadComboBoxItem("<", "<"));
                    cboOperand.Items.Add(new RadComboBoxItem(">=", ">="));
                    cboOperand.Items.Add(new RadComboBoxItem("<=", "<="));
                }
            }
            
            ToggleEnableInputNumeric(val);
        }

        private void SetupInputCheckbox(bool val)
        {
            ToggleEnableInputCheckbox(val);
        }

        private void FormatInterface() {
            string AnswerType = cboSRAnswerType.SelectedValue;

            ToggleEnableInputFreeText(false);
            ToggleEnableInputNumeric(false);
            ToggleEnableInputCheckbox(false);

            switch (AnswerType)
            {
                case "MSK":
                case "TXT":
                case "MEM":
                case "CBO":
                case "RBT":
                case "CBT": // pake nilai option saja
                case "CBN":
                case "CBM":
                case "CB2":
                case "TTX":
                    // enable text input
                    SetupInputFreeText(true);
                    SetupInputCheckbox(false);
                    break;
                case "DAT":
                case "NUM":
                    SetupInputNumeric(true);
                    SetupInputCheckbox(false);
                    break;
                case "CHK":
                case "CTX":
                    SetupInputFreeText(false);
                    SetupInputNumeric(false);
                    SetupInputCheckbox(true);
                    break;
                case "CNM":
                    SetupInputFreeText(false);
                    SetupInputNumeric(true);
                    SetupInputCheckbox(true);
                    break;
                case "CDT":
                    SetupInputFreeText(false);
                    SetupInputNumeric(false);
                    SetupInputCheckbox(true);
                    break;
                default:
                    ToggleEnableInputFreeText(false);
                    ToggleEnableInputNumeric(false);
                    ToggleEnableInputCheckbox(false);
                    break;
            }
        }

        private void initRB() { 
            var std = new AppStandardReferenceItemCollection();
            std.Query.Where(std.Query.StandardReferenceID == "NsMandatoryLevel");
            std.LoadAll();

            rbAssLevel.DataSource = std;
            rbAssLevel.DataValueField = "ItemID";
            rbAssLevel.DataTextField = "ItemName";
        }

        protected override void OnDataBinding(EventArgs e)
        {
            initRB();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                //ComboBox.PopulateWithOneNursingDiagnosa(cboNursingDiagnosaID, "", "10");

                FormatInterface();
                return;
            }
            ViewState["IsNewRecord"] = false;

            var diag = new NursingDiagnosa();
            var diagID = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID);
            if (diag.LoadByPrimaryKey(diagID)) { 
                var arghh = new RadComboBoxItemsRequestedEventArgs();
                arghh.Text = diag.NursingDiagnosaName;
                cboNursingDiagnosaID_ItemsRequested(cboNursingDiagnosaID, arghh);
                cboNursingDiagnosaID.SelectedValue = diagID;
            }
            ComboBox.PopulateWithOneNursingDiagnosa(cboNursingDiagnosaID, (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.NursingDiagnosaID), "10");

            txtAgeStart.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthStart) ?? 0);
            if ((txtAgeStart.Value % 12) > 0)
            {
                cboAgeStartConversion.SelectedValue = "1";
            }
            else {
                cboAgeStartConversion.SelectedValue = "12";
                txtAgeStart.Value = txtAgeStart.Value / 12;
            }
            txtAgeEnd.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.AgeInMonthEnd) ?? 0);
            if ((txtAgeEnd.Value % 12) > 0)
            {
                cboAgeEndConversion.SelectedValue = "1";
            }
            else
            {
                cboAgeEndConversion.SelectedValue = "12";
                txtAgeEnd.Value = txtAgeEnd.Value / 12;
            }

            /**/
            cboSex.SelectedValue = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.Sex);
            
            txtAcceptedValues.Text = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedText);
            txtNumAceptedValue.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum) ?? 0);
            chkCheck.Checked = System.Convert.ToBoolean(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.CheckValue));
            txtNumAceptedValue2.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.AcceptedNum2) ?? 0);
            chkUsingRange.Checked = System.Convert.ToBoolean(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.IsUsingRange) ?? false);

            FormatInterface();
            var sv = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.Operand);
            var svo = cboOperand.Items.FindItemByValue(sv);
            if (svo != null)
            {
                cboOperand.SelectedValue = sv;
            }

            chkShowInPrefix.Checked = System.Convert.ToBoolean(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsPrefix));
            chkShowInSuffix.Checked = System.Convert.ToBoolean(DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.ShowAssessmetAsSuffix));
            
            var srPre = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaPrefix); 
            if(!string.IsNullOrEmpty(srPre)){
                var args = new RadComboBoxItemsRequestedEventArgs();
                args.Text = srPre;
                cboSRDiagPrefix_ItemsRequested(cboSRDiagPrefix, args);
                cboSRDiagPrefix.SelectedValue = srPre; 
            }
            var srSuf = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsDiagnosaSuffix);
            if (!string.IsNullOrEmpty(srSuf))
            {
                var args = new RadComboBoxItemsRequestedEventArgs();
                args.Text = srSuf;
                cboSRDiagSuffix_ItemsRequested(cboSRDiagSuffix, args);
                cboSRDiagSuffix.SelectedValue = srSuf;
            }

            var assLevel = (String)DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.SRNsMandatoryLevel);
            if(string.IsNullOrEmpty(assLevel)) assLevel = "02"; // default optional
            rbAssLevel.SelectedValue = assLevel;
            
            //hfID.Value = DataBinder.Eval(DataItem, NursingAssessmentDiagnosaMetadata.ColumnNames.ID).ToString();
        }

        protected void cboNursingDiagnosaID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["NursingDiagnosaName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["NursingDiagnosaID"].ToString();
        }

        protected void cboNursingDiagnosaID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboNursingDiagnosaID.DataSource = tbl;
            cboNursingDiagnosaID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new NursingDiagnosaQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.NursingDiagnosaID,
                    query.NursingDiagnosaName
                );
            query.Where(
                query.IsActive == true,
                query.SRNursingDiagnosaLevel == "10",
                query.Or(
                    query.NursingDiagnosaName.Like(searchTextContain),
                    query.NursingDiagnosaID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.NursingDiagnosaName.Ascending);

            return query.LoadDataTable();
        }

        protected void cboSRDiagPrefix_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRDiagPrefix_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "NsDiagnosaPrefix", e.Text, true);
        }

        protected void cboSRDiagSuffix_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "NsDiagnosaSuffix", e.Text, true);
        }

        #region Properties for return entry value

        public String NursingDiagnosaID
        {
            get { return cboNursingDiagnosaID.SelectedValue; }
        }

        public String NursingDiagnosaName
        {
            get { return cboNursingDiagnosaID.Text; }
        }

        public int AgeInMonthStart
        {
            get { return System.Convert.ToInt32(txtAgeStart.Value) * System.Convert.ToInt32(cboAgeStartConversion.SelectedValue); }
        }
        public int AgeInMonthEnd
        {
            get { return System.Convert.ToInt32(txtAgeEnd.Value) * System.Convert.ToInt32(cboAgeEndConversion.SelectedValue); }
        }

        public String Sex
        {
            get { return cboSex.SelectedValue; }
        }

        public String Operand
        {
            get { return cboOperand.SelectedValue; }
        }

        public String AcceptedText
        {
            get { return txtAcceptedValues.Text; }
        }

        public double? AcceptedNum
        {
            get { return txtNumAceptedValue.Value; }
        }

        public bool IsChecked
        {
            get { return chkCheck.Checked; }
        }

        public double? AcceptedNum2
        {
            get { return txtNumAceptedValue2.Value; }
        }

        public bool IsUsingRange
        {
            get { return chkUsingRange.Checked; }
        }

        public bool ShowInPrefix {
            get { return chkShowInPrefix.Checked; }
        }

        public bool ShowInSuffix
        {
            get { return chkShowInSuffix.Checked; }
        }

        public string SRDiagnosisPrefix
        {
            get { return cboSRDiagPrefix.SelectedValue; }
        }

        public string SRDiagnosisPrefixName
        {
            get { return cboSRDiagPrefix.Text; }
        }

        public string SRDiagnosisSuffix
        {
            get { return cboSRDiagSuffix.SelectedValue; }
        }

        public string SRDiagnosisSuffixName
        {
            get { return cboSRDiagSuffix.Text; }
        }

        public string SRNsMandatoryLevel
        {
            get { return rbAssLevel.SelectedValue; }
        }

        public string SRNsMandatoryLevelName
        {
            get { 
                return rbAssLevel.SelectedItem == null ? string.Empty : rbAssLevel.SelectedItem.Text; 
            }
        }
        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    NursingAssessmentDiagnosaCollection coll = (NursingAssessmentDiagnosaCollection)Session["collNursingAssessmentDiagnosaCollection"];

            //    string NursingDiagnosaID = cboNursingDiagnosaID.SelectedValue;
            //    bool isExist = false;
            //    foreach (NursingAssessmentDiagnosa item in coll)
            //    {
            //        if (item.NursingDiagnosaID.Equals(NursingDiagnosaID) && 
            //            item.AgeInMonthStart == AgeInMonthStart && 
            //            item.AgeInMonthEnd == AgeInMonthEnd)
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", NursingDiagnosaID);
            //    }
            //}
        }

        protected void cboNursingDiagnosaID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cboNursingDiagnosaID.SelectedValue)) {
                var ns = new NursingDiagnosa();
                if (ns.LoadByPrimaryKey(cboNursingDiagnosaID.SelectedValue)) {
                    var isAsbid = ns.SRNsDiagnosaType == "02";
                    trPrefSuf.Visible = isAsbid;
                    trAdditional.Visible = isAsbid;
                    trMandatoryType.Visible = isAsbid;
                    trAddSuff.Visible = isAsbid;
                }
            }
        }
    }
}
