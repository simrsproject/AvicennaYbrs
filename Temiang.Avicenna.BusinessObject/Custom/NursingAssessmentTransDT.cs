using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingAssessmentTransDT
    {
        public string RefToNursingAssessmentQuestionAnswerType
        {
            get { return GetColumn("refTo_NursingAssessmentQuestionAnswerType").ToString(); }
            set { SetColumn("refTo_NursingAssessmentQuestionAnswerType", value); }
        }
    }
}
