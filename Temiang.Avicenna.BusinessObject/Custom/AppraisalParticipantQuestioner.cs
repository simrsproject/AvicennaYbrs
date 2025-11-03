using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppraisalParticipantQuestioner
    {
        public string EmployeeName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_EmployeeName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_EmployeeName", value); }
        }

        public string QuestionerNo
        {
            get { return GetColumn("refToAppraisalQuestion_QuestionerNo").ToString(); }
            set { SetColumn("refToAppraisalQuestion_QuestionerNo", value); }
        }

        public string QuestionerName
        {
            get { return GetColumn("refToAppraisalQuestion_QuestionerName").ToString(); }
            set { SetColumn("refToAppraisalQuestion_QuestionerName", value); }
        }

        public string QuestionerEvaluatorName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_QuestionerEvaluatorName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_QuestionerEvaluatorName", value); }
        }
    }
}
