using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppraisalParticipantItem
    {
        public string EmployeeName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_EmployeeName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_EmployeeName", value); }
        }

        public string SupervisorName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_SupervisorName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_SupervisorName", value); }
        }

        public string PartnerName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_PartnerName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_PartnerName", value); }
        }

        public string SubordinateName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_SubordinateName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_SubordinateName", value); }
        }
        public string Evaluators
        {
            get { return GetColumn("refToAppraisalParticipantQuestionerQuery_Evaluators").ToString(); }
            set { SetColumn("refToAppraisalParticipantQuestionerQuery_Evaluators", value); }
        }

        public string Questioners
        {
            get { return GetColumn("refToAppraisalParticipantQuestionerQuery_Questioners").ToString(); }
            set { SetColumn("refToAppraisalParticipantQuestionerQuery_Questioners", value); }
        }
    }
}
