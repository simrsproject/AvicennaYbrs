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
    public partial class QuestionnaireConclusionDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                var coll = (ClinicalPerformanceAppraisalQuestionnaireConclusionCollection)Session["collClinicalPerformanceAppraisalQuestionnaireConclusion" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["id"] = "1";
                else
                {
                    var questionId = (coll.OrderByDescending(c => c.ConclusionID).Select(c => c.ConclusionID)).Take(1);
                    int id = Convert.ToInt32(questionId.Single()) + 1;

                    ViewState["id"] = id.ToString();
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["id"] = DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID).ToInt();

            txtConclusionGrade.Text = (String)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGrade);
            txtConclusionGradeName.Text = (String)DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGradeName);
            txtMinValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MinValue));
            txtMaxValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MaxValue));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ClinicalPerformanceAppraisalQuestionnaireConclusionCollection)Session["collClinicalPerformanceAppraisalQuestionnaireConclusion" + Request.UserHostName];
                var isExist = coll.Any(entity => entity.ConclusionGrade.Equals(txtConclusionGrade.Text));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Grade {0} has exist.", txtConclusionGrade.Text);
                    return;
                }
            }
        }

        public int ConclusionID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public String ConclusionGrade
        {
            get { return txtConclusionGrade.Text; }
        }

        public String ConclusionGradeName
        {
            get { return txtConclusionGradeName.Text; }
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