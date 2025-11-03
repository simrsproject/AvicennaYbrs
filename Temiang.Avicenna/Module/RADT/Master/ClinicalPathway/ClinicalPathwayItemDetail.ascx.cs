using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClinicalPathwayItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemAndZatActive, txtAssesmentID);

            txtClass1.Value = 0;
            txtClass2.Value = 0;
            txtClass3.Value = 0;

            var txtAlos = Helper.FindControlRecursive(this.Page, "txtALOS") as RadNumericTextBox;
            if (2 > txtAlos.Value) cboDay2.Enabled = false;
            if (3 > txtAlos.Value) cboDay3.Enabled = false;
            if (4 > txtAlos.Value) cboDay4.Enabled = false;
            if (5 > txtAlos.Value) cboDay5.Enabled = false;
            if (6 > txtAlos.Value) cboDay6.Enabled = false;
            if (7 > txtAlos.Value) cboDay7.Enabled = false;

            var index = 0;
            var pathwayItemExecutions = Session["collPathwayItemCollection"] as PathwayItemCollection;
            foreach (var item in pathwayItemExecutions.Select(s => new { AssesmentHeaderName = Regex.Replace(s.AssesmentHeaderName.Trim(), @"\t|\n|\r", " ") }).Distinct())
            {
                if (index == 0)
                {
                    cboHeaderName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    //cboItemGroupName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    index++;
                }
                if (!cboHeaderName.Items.Any(h => h.Text == item.AssesmentHeaderName.Trim())) cboHeaderName.Items.Add(new RadComboBoxItem(item.AssesmentHeaderName.Trim(), string.Empty));
                //if (!cboItemGroupName.Items.Any(h => h.Text == item.AssesmentGroupName)) cboItemGroupName.Items.Add(new RadComboBoxItem(item.AssesmentGroupName, string.Empty));
            }
            index = 0;
            foreach (var item in pathwayItemExecutions.Select(s => new { AssesmentGroupName = Regex.Replace(s.AssesmentGroupName.Trim(), @"\t|\n|\r", " ") }))
            {
                if (index == 0)
                {
                    //cboHeaderName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    cboItemGroupName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    index++;
                }
                //if (!cboHeaderName.Items.Any(h => h.Text == item.AssesmentHeaderName)) cboHeaderName.Items.Add(new RadComboBoxItem(item.AssesmentHeaderName, string.Empty));
                if (!cboItemGroupName.Items.Any(h => h.Text == item.AssesmentGroupName.Trim())) cboItemGroupName.Items.Add(new RadComboBoxItem(item.AssesmentGroupName.Trim(), string.Empty));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            txtAssesmentID.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.ItemID);
            txtAssesmentName.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.AssesmentName);
            txtItemGroupName.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.AssesmentGroupName);
            txtClass1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.CoverageValue1));
            txtClass2.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.CoverageValue2));
            txtClass3.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.CoverageValue3));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.Notes);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.IsActive));
            txtHeaderName.Text = (String)DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.AssesmentHeaderName);

            string sessName = "collPathwayItemExecutionCollection";
            var coll = (PathwayItemExecutionCollection)Session[sessName];
            foreach (var exec in coll.Where(c => c.PathwayItemSeqNo == Convert.ToInt32(DataBinder.Eval(DataItem, PathwayItemMetadata.ColumnNames.PathwayItemSeqNo))))
            {
                if (exec.DayNo == 1) cboDay1.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 2) cboDay2.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 3) cboDay3.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 4) cboDay4.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 5) cboDay5.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 6) cboDay6.SelectedValue = exec.SRPathwayExecutionType;
                if (exec.DayNo == 7) cboDay7.SelectedValue = exec.SRPathwayExecutionType;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string sessName = "collPathwayItemCollection";
                var coll = (PathwayItemCollection)Session[sessName];

                bool isExist = coll.Any(c => c.AssesmentName == txtAssesmentName.Text);
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Assesment Name : {0} already exist", txtAssesmentName.Text);
                }
            }
        }

        public String ItemID
        {
            get { return txtAssesmentID.Text; }
        }

        public String AssesmentName
        {
            get { return txtAssesmentName.Text; }
        }

        public String AssesmentGroupName
        {
            get { return string.IsNullOrEmpty(cboItemGroupName.Text) ? txtItemGroupName.Text : cboItemGroupName.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public string Day1
        {
            get { return cboDay1.SelectedValue; }
        }

        public string Day2
        {
            get { return cboDay2.SelectedValue; }
        }

        public string Day3
        {
            get { return cboDay3.SelectedValue; }
        }

        public string Day4
        {
            get { return cboDay4.SelectedValue; }
        }

        public string Day5
        {
            get { return cboDay5.SelectedValue; }
        }

        public string Day6
        {
            get { return cboDay6.SelectedValue; }
        }

        public string Day7
        {
            get { return cboDay7.SelectedValue; }
        }

        public decimal CoverageValue1
        {
            get { return Convert.ToDecimal(txtClass1.Value); }
        }

        public decimal CoverageValue2
        {
            get { return Convert.ToDecimal(txtClass2.Value); }
        }

        public decimal CoverageValue3
        {
            get { return Convert.ToDecimal(txtClass3.Value); }
        }

        public string AssesmentHeaderName
        {
            get { return string.IsNullOrEmpty(cboHeaderName.Text) ? txtHeaderName.Text : cboHeaderName.Text; }
        }

        protected void txtAssesmentID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAssesmentID.Text)) txtAssesmentName.Text = string.Empty;
            else
            {
                var item = new Item();
                if (item.LoadByPrimaryKey(txtAssesmentID.Text))
                    txtAssesmentName.Text = item.ItemName;
                else
                {
                    var zat = new ZatActive();
                    zat.LoadByPrimaryKey(txtAssesmentID.Text);
                    txtAssesmentName.Text = zat.ZatActiveName;

                }
            }
        }
    }
}