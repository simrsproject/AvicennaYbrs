using System;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalRecruitmentTestEvaluator
    {
        public string EvaluatorName
        {
            get { return GetColumn("refTo_EvaluatorName").ToString(); }
            set { SetColumn("refTo_EvaluatorName", value); }
        }

        public string PositionName
        {
            get { return GetColumn("refTo_PositionName").ToString(); }
            set { SetColumn("refTo_PositionName", value); }
        }
    }
}
