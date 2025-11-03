using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeVerificationRentalRooms
    {
        public DateTime ? PhysicianFeePeriod
        {
            get { return Convert.ToDateTime(GetColumn("refFeeVerification_PhysicianFeePeriod")); }
            set { SetColumn("refFeeVerification_PhysicianFeePeriod", value); }
        }

        public string ParamedicID
        {
            get { return GetColumn("refFeeVerification_ParamedicID").ToString(); }
            set { SetColumn("refFeeVerification_ParamedicID", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refParamedic_ParamedicName").ToString(); }
            set { SetColumn("refParamedic_ParamedicName", value); }
        }

    }
}
