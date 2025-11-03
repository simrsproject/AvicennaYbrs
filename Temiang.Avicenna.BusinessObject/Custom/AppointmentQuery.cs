using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppointmentQuery
    {
        public esQueryItem PatientName
        {
            get
            {
                esQueryItem patientName = FirstName + " " + MiddleName.RTrim() + " " + LastName.RTrim();
                patientName = patientName.As("PatientName");
                return patientName;
            }
        }

        public esQueryItem Address
        {
            get
            {
                esQueryItem address = StreetName + " " + City.RTrim() + " " + County.RTrim() + " " + ZipCode.RTrim();
                address = address.As("Address");
                return address;
            }
        }
    }
}