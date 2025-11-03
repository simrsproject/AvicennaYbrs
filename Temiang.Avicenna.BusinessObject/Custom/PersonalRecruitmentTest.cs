using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalRecruitmentTest
    {
        public string RecruitmentTestName
        {
            get { return GetColumn("refTo_RecruitmentTestName").ToString(); }
            set { SetColumn("refTo_RecruitmentTestName", value); }
        }
        public string RecruitmentTestType
        {
            get { return GetColumn("refTo_RecruitmentTestType").ToString(); }
            set { SetColumn("refTo_RecruitmentTestType", value); }
        }
        public string RecruitmentTestConclusionName
        {
            get { return GetColumn("refTo_RecruitmentTestConclusionName").ToString(); }
            set { SetColumn("refTo_RecruitmentTestConclusionName", value); }
        }
    }
}
