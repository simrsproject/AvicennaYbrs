using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MembershipMember
    {
        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }
        public string PatientName
        {
            get { return GetColumn("refToPatient_PatientName").ToString(); }
            set { SetColumn("refToPatient_PatientName", value); }
        }
        public string Sex
        {
            get { return GetColumn("refToPatient_Sex").ToString(); }
            set { SetColumn("refToPatient_Sex", value); }
        }
        public string CityOfBirth
        {
            get { return GetColumn("refToPatient_CityOfBirth").ToString(); }
            set { SetColumn("refToPatient_CityOfBirth", value); }
        }
        public DateTime DateOfBirth
        {
            get { return Convert.ToDateTime(GetColumn("refToPatient_DateOfBirth")); }
            set { SetColumn("refToPatient_DateOfBirth", value); }
        }
        public string Address
        {
            get { return GetColumn("refToPatient_Address").ToString(); }
            set { SetColumn("refToPatient_Address", value); }
        }
        public string PhoneNo
        {
            get { return GetColumn("refToPatient_PhoneNo").ToString(); }
            set { SetColumn("refToPatient_PhoneNo", value); }
        }
        public string MobilePhoneNo
        {
            get { return GetColumn("refToPatient_MobilePhoneNo").ToString(); }
            set { SetColumn("refToPatient_MobilePhoneNo", value); }
        }
        public string Email
        {
            get { return GetColumn("refToPatient_Email").ToString(); }
            set { SetColumn("refToPatient_Email", value); }
        }
    }
}
