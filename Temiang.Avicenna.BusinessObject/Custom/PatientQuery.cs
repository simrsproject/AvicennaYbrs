using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientQuery
    {
        public esQueryItem PatientName
        {
            get
            {
                esQueryItem patientName = ((((FirstName + " " + MiddleName).LTrim()).RTrim() + " " + LastName).LTrim()).RTrim();
                patientName = patientName.As("PatientName");
                return patientName;
            }
        }

        public esQueryItem Address
        {
            get
            {
                esQueryItem address = (StreetName.RTrim() + (" " + City).RTrim() + " " + County).RTrim() + (" " + ZipCode.Coalesce("''")).RTrim();
                address = address.As("Address");
                return address;
            }
        }
    }
}