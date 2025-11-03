using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class PlanActionTherapyCtl : BaseAssessmentCtl
    {
        private bool? _isPlanningQuestionExist = null;
        private bool IsPlanningQuestionExist
        {
            get
            {
                if (_isPlanningQuestionExist == null)
                {
                    var qg = new QuestionGroup();
                    _isPlanningQuestionExist = (qg.LoadByPrimaryKey("RHB.PLN") && qg.IsActive == true);
                }
                return _isPlanningQuestionExist ?? false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var astp = Request.QueryString["astp"];
            var qgID = string.Empty;

            switch (astp)
            {
                case "REHAB":
                    qgID = "RHB.PLN";
                    break;
            }

            if (!string.IsNullOrEmpty(qgID))
            {
                // Check QuestionGroup untuk REHAB Planning
                if (IsPlanningQuestionExist)
                {
                    questPlanning.QuestionGroupID = qgID;
                    tblTherapyFreeText.Visible = false;
                }
                else
                    questPlanning.Visible = false;
            }
            else
            {
                questPlanning.Visible = false;
                tblTherapyFreeText.Visible = true;
            }

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var astp = Request.QueryString["astp"];
            if (IsPlanningQuestionExist && astp == "REHAB")
            {
                var therapy = JsonConvert.DeserializeObject<QuestionGroupAnswerValue>(assessment.Therapy);
                questPlanning.PopulateValue(therapy);
            }
            else
                txtTherapy.Text = assessment.Therapy;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            //assessment.Therapy = txtTherapy.Text;
            var plan = string.Empty;
            var astp = Request.QueryString["astp"];
            if (IsPlanningQuestionExist && astp == "REHAB")
            {
                var answerVal = questPlanning.GetQuestionAnswerValue();
                var therapy = JsonConvert.SerializeObject(answerVal);
                var strBuilder = new StringBuilder();

                foreach (var answer in answerVal.QuestionAnswerValues)
                {
                    if (answer.SRAnswerType == "LBL")
                    {
                        var ques = new Question();
                        ques.LoadByPrimaryKey(answer.QuestionID);
                        strBuilder.AppendLine(string.Empty);
                        strBuilder.Append(ques.QuestionText + " : ");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(answer.QuestionAnswerText))
                            strBuilder.Append(answer.QuestionAnswerText + " | ");
                    }

                }

                plan = strBuilder.ToString();

                assessment.Therapy = therapy;
            }
            else
            {
                assessment.Therapy = txtTherapy.Text;
                plan = txtTherapy.Text;
            }

            // Planning (P) gabung
            if (rim.es.IsAdded || rim.Info4.Equals(rim.GetOriginalColumnValue("Info4")))
                rim.Info4 = plan;
            else
                rim.Info4 = string.Concat(rim.Info4, Environment.NewLine, plan);

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        #endregion
    }
}