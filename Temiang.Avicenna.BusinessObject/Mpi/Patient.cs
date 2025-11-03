using System.Collections;
using System.Xml.Serialization;

namespace Temiang.Avicenna.BusinessObject.Mpi
{
    [XmlRoot("PatientSearch", Namespace = "", IsNullable = false)]
    public class PatientSearch
    {
        [XmlArray("patients"), XmlArrayItem("patient", typeof(Patient))]
        public ArrayList PatientList = new ArrayList();
    }

    [XmlRoot("patient", Namespace = "", IsNullable = false)]
    public class Patient
    {
        [XmlElement("patient_id")]
        public string MpiPatientID;

        [XmlElement("mrn")]
        public string MedicalNo;

        [XmlElement("person_nm")]
        public string PatientName;

        [XmlElement("gender_cd")]
        public string Sex;

        [XmlElement("address_txt")]
        public string Address;


    }
}