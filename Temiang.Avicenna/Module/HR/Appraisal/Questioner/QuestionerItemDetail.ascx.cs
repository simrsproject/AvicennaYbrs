using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class QuestionerItemDetail : BaseUserControl
    {
        private CheckBox ChkIsScoringRecapitulation
        {
            get
            {
                return (CheckBox)Helper.FindControlRecursive(Page, "chkIsScoringRecapitulation");
            }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            pnlRating.Visible = AppSession.Parameter.AppraisalVersionNo == "2";
            pnlTarget.Visible = AppSession.Parameter.AppraisalVersionNo == "3";
            trMinMaxValue.Visible = ChkIsScoringRecapitulation.Checked;

            var coll = (AppraisalQuestionItemCollection)Session["collAppraisalQuestionItem" + Request.UserHostName];
            cboQuestionGroupName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var c in coll.Where(g => g.QuestionGroupName == string.Empty).Distinct())
            {
                cboQuestionGroupName.Items.Add(new RadComboBoxItem(c.QuestionName, c.QuestionName));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                if (coll.Count == 0)
                    ViewState["id"] = "1";
                else
                {
                    var questionId = (coll.OrderByDescending(c => c.QuestionerItemID).Select(c => c.QuestionerItemID)).Take(1);
                    int id = Convert.ToInt32(questionId.Single()) + 1;

                    ViewState["id"] = id.ToString();
                }

                txtRating.Value = 0;
                txtBenchmark.Value = 0;
                txtMinValue.Value = 0;
                txtMaxValue.Value = 0;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["id"] = DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID).ToInt();

            txtQuestionCode.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.QuestionCode);
            cboQuestionGroupName.SelectedValue = DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.QuestionGroupName).ToString();
            txtQuestionName.Text = (string)DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.QuestionName);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.Notes);
            txtTarget.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.Target);
            txtAchievements.Text = (String)DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.Achievements);
            txtRating.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.Rating));
            txtBenchmark.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.Benchmark));
            txtMinValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.MinValue));
            txtMaxValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, AppraisalQuestionItemMetadata.ColumnNames.MaxValue));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppraisalQuestionItemCollection)Session["collAppraisalQuestionItem" + Request.UserHostName];
                var isExist = coll.Any(entity => entity.QuestionGroupName.Equals(cboQuestionGroupName.SelectedValue) && entity.QuestionCode.Equals(txtQuestionCode.Text));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Question Group : {0}, Question Code {1} has exist", cboQuestionGroupName.Text, txtQuestionCode.Text);
                }
            }
        }

        public int QuestionerItemID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public String QuestionCode
        {
            get { return txtQuestionCode.Text; }
        }

        public string QuestionGroupName
        {
            get { return cboQuestionGroupName.Text; }
        }

        public string QuestionName
        {
            get { return txtQuestionName.Text; }
        }

        public string Notes
        {
            get { return txtNotes.Text; }
        }

        public string Target
        {
            get { return txtTarget.Text; }
        }

        public string Achievements
        {
            get { return txtAchievements.Text; }
        }

        public Decimal Rating
        {
            get { return Convert.ToDecimal(txtRating.Value); }
        }

        public Decimal Benchmark
        {
            get { return Convert.ToDecimal(txtBenchmark.Value); }
        }

        public Decimal MinValue
        {
            get { return Convert.ToDecimal(txtMinValue.Value); }
        }

        public Decimal MaxValue
        {
            get { return Convert.ToDecimal(txtMaxValue.Value); }
        }
    }
}