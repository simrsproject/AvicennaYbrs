using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemLaboratoryDetail
    {
        public string AgeUnitName
        {
            get { return GetColumn("refToAppStandardReferenceItem_SRAgeUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SRAgeUnit", value); }
        }

        public string AnswerTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_SRAnswerType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SRAnswerType", value); }
        }

        public string AnswerTypeReferenceName
        {
            get { return GetColumn("refToQuestionAnswerSelection_AnswerTypeReferenceID").ToString(); }
            set { SetColumn("refToQuestionAnswerSelection_AnswerTypeReferenceID", value); }
        }

        public static decimal CalculateTotalAge(string srAgeUnit, decimal age)
        {
            switch (srAgeUnit)
            {
                case "AgeUnit-001": //day
                    return Convert.ToDecimal(age);
                case "AgeUnit-002": //week
                    if (age % 4 == 0) return Convert.ToDecimal((age / 4) * 30);
                    return Convert.ToDecimal(age * 7);
                case "AgeUnit-003": //month
                    if (age % 12 == 0) return Convert.ToDecimal((age / 12) * 365);
                    return Convert.ToDecimal(age * 30);
                case "AgeUnit-004": //year
                    return Convert.ToDecimal(age * 365);
                default:
                    return 0;
            }
        }
    }
}
