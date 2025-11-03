using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationQuery
    {
        public esQueryItem RegistrationDateTime
        {
            get
            {
                esQueryItem RegistrationDateTime = RegistrationDate + " " + RegistrationTime;
                RegistrationDateTime = RegistrationDateTime.As("RegistrationDateTime");
                return RegistrationDateTime;
            }
        }

        public esQueryItem DischargeDateTime
        {
            get
            {
                esQueryItem DischargeDateTime = DischargeDate + " " + DischargeTime;
                DischargeDateTime.Cast(esCastType.DateTime);
                DischargeDateTime = DischargeDateTime.As("DischargeDateTime");
                return DischargeDateTime;
            }
        }
    }
}