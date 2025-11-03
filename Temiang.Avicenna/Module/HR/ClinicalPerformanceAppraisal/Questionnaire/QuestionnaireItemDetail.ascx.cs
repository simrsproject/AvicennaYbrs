using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class QuestionnaireItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            var coll = (ClinicalPerformanceAppraisalQuestionnaireItemCollection)Session["collClinicalPerformanceAppraisalQuestionnaireItem" + Request.UserHostName];
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
                    var questionId = (coll.OrderByDescending(c => c.QuestionnaireItemID).Select(c => c.QuestionnaireItemID)).Take(1);
                    int id = Convert.ToInt32(questionId.Single()) + 1;

                    ViewState["id"] = id.ToString();
                }
                txtLoadScore.Value = 0;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["id"] = DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID).ToInt();

            txtQuestionCode.Text = (String)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionCode);
            txtQuestionNo.Text = (String)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionNo);
            txtQuestionName.Text = (String)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionName);
            cboQuestionGroupName.SelectedValue = DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.QuestionGroupName).ToString();
            txtLoadScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.LoadScore));
            chkIsDetail.Checked = (bool)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireItemMetadata.ColumnNames.IsDetail);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ClinicalPerformanceAppraisalQuestionnaireItemCollection)Session["collClinicalPerformanceAppraisalQuestionnaireItem" + Request.UserHostName];
                var isExist = coll.Any(entity => entity.QuestionCode.Equals(txtQuestionCode.Text));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Question Code {0} has exist.", txtQuestionCode.Text);
                    return;
                }
            }
            if (chkIsDetail.Checked && (txtLoadScore.Value == 0))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Invalid Load Score.");
                return;
            }
        }

        public int QuestionnaireItemID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public String QuestionCode
        {
            get { return txtQuestionCode.Text; }
        }

        public String QuestionNo
        {
            get { return txtQuestionNo.Text; }
        }

        public string QuestionName
        {
            get { return txtQuestionName.Text; }
        }

        public string QuestionGroupName
        {
            get { return cboQuestionGroupName.Text; }
        }

        public Int16 LoadScore
        {
            get { return Convert.ToInt16(txtLoadScore.Value); }
        }

        public bool IsDetail
        {
            get { return chkIsDetail.Checked; }
        }
    }
}