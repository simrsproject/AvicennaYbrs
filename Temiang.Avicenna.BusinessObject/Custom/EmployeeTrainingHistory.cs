using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeTrainingHistory
    {
        public string EmployeeName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public Boolean? IsProposal
        {
            get { return Convert.ToBoolean(GetColumn("refToEmployeeTraining_IsProposal")); }
            set { SetColumn("refToEmployeeTraining_IsProposal", value); }
        }

        public string ActivityTypeName
        {
            get { return GetColumn("refToStdRef_ActivityTypeName").ToString(); }
            set { SetColumn("refToStdRef_ActivityTypeName", value); }
        }

        public string ActivitySubTypeName
        {
            get { return GetColumn("refToStdRef_ActivitySubTypeName").ToString(); }
            set { SetColumn("refToStdRef_ActivitySubTypeName", value); }
        }

        public string TrainingFinancingSourcesName
        {
            get { return GetColumn("refToStdRef_TrainingFinancingSourcesName").ToString(); }
            set { SetColumn("refToStdRef_TrainingFinancingSourcesName", value); }
        }

        public string EmployeeTrainingRoleName
        {
            get { return GetColumn("refToStdRef_EmployeeTrainingRole").ToString(); }
            set { SetColumn("refToStdRef_EmployeeTrainingRole", value); }
        }
    }
}
