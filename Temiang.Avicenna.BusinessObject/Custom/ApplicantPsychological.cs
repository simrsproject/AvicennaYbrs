using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantPsychological
    {
        public string PsychologicalName
        {
            get { return GetColumn("refToSRPsychological_PositionPsychological").ToString(); }
            set { SetColumn("refToSRPsychological_PositionPsychological", value); }
        }

        public string OperandTypeName
        {
            get { return GetColumn("refToOperandTypeName_PositionPhysical").ToString(); }
            set { SetColumn("refToOperandTypeName_PositionPhysical", value); }
        }
    }
}
