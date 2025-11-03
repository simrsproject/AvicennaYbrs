using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentQuery
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
    }
}