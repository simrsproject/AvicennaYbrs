using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppointmentLokadok
    {
        public DateTime AppointmentDate
        {
            get
            {
                //return DateTime.Now;
                return AppointmentLokadokCollection.UnixTimeToDateTime(this.StartDate.Value);
            }
        }

        public string PatientName
        {
            get {
                return Patient.GetFullName(
                    GetColumn("refToPatient_FirstName").ToString(),
                    GetColumn("refToPatient_MiddleName").ToString(),
                    GetColumn("refToPatient_LastName").ToString()
                    );
            }
            //set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            //set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string RegistrationNoRef
        {
            get { return GetColumn("refToRegistration_RegistrationNo").ToString(); }
        }

        public bool PatientNotFound
        {
            get { return (bool)GetColumn("refToPatient_PatientNotFound"); }
        }

        public string RegistrationQue
        {
            get { return GetColumn("refToRegistration_RegistrationQue").ToString(); }
        }
    }
}
