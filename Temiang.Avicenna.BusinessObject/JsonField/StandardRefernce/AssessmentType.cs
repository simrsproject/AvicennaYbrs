using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class AssessmentType : BaseJsonField
    {
        public bool? IsSoapEntryTypeAssessment { get; set; }
        public bool? IsUseMedicalHist { get; set; }
        public string PastMedicalHistRefId { get; set; }
        public string FamilyMedHistRefId { get; set; }
        public bool? IsUseLocalist { get; set; }
        public bool? IsUseEducation { get; set; }
        public bool? IsUseDiagnoseTherapy { get; set; }
        public bool? IsUseOdontogram { get; set; }
        public bool? IsUseChildBirtHist { get; set; }
        public bool? IsUseBirthFoodGrowthHist { get; set; }
        public bool? IsUseImunizationHist { get; set; }
        public bool? IsUseAncillaryExam { get; set; }
        public bool? IsFollowUpPlan { get; set; }
        public string PhsycalExamInitialCtlUrl { get; set; }
        public string PhsycalExamContinuedCtlUrl { get; set; }
        public string PageCaption { get; set; }
    }
}
