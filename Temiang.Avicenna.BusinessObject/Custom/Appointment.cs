using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.DynamicQuery;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Appointment
    {
        public string PatientName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName))
                    return string.Empty;
                return FirstName + " " + MiddleName.Trim() + " " + LastName.Trim();
            }
        }

        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }

        public string AppointmentStatusName
        {
            get { return GetColumn("refToAppStandardReferenceItem_AppointmentStatus").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_AppointmentStatus", value); }
        }
    }

    public partial class AppointmentCollection
    {
        public DataTable BpjsOpenAppointment(string bridgingType, string guarantorId, string appointmentStatus, DateTime minDate)
        {
            var par = new esParameters();

            var commandText = $@"select a.AppointmentNo,
	bs.NoSEP,
	pb.BridgingID as pb,
	sub.BridgingID as sub,
	a.AppointmentDate,
	a.LastCreateByUserID
from Appointment a
inner join ServiceUnitBridging sub on a.ServiceUnitID = sub.ServiceUnitID and sub.SRBridgingType = '{bridgingType}'
inner join ParamedicBridging pb on a.ParamedicID = pb.ParamedicID and pb.SRBridgingType = '{bridgingType}'
inner join BpjsSEP bs on a.Notes = bs.NoSEP
where a.GuarantorID = '{guarantorId}' and
	a.SRAppointmentStatus = '{appointmentStatus}' and
	a.AppointmentDate > '{minDate:yyyyMMdd}' and
	ISNULL(a.ReferenceNumber, '') = ''";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable BpjsOpenAppointment(string bridgingType, string guarantorId, string appointmentStatus, string appointmentNo)
        {
            var par = new esParameters();

            var commandText = $@"select a.AppointmentNo,
	bs.NoSEP,
	pb.BridgingID as pb,
	sub.BridgingID as sub,
	a.AppointmentDate,
	a.LastCreateByUserID
from Appointment a
inner join ServiceUnitBridging sub on a.ServiceUnitID = sub.ServiceUnitID and sub.SRBridgingType = '{bridgingType}'
inner join ParamedicBridging pb on a.ParamedicID = pb.ParamedicID and pb.SRBridgingType = '{bridgingType}'
inner join BpjsSEP bs on a.Notes = bs.NoSEP
where a.GuarantorID = '{guarantorId}' and
	a.SRAppointmentStatus = '{appointmentStatus}' and
	a.AppointmentNo = '{appointmentNo}' and
	ISNULL(a.ReferenceNumber, '') = ''";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
