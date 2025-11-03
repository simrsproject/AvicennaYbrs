namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientDischargeAppointment
    {
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
        public string RoomName
        {
            get { return GetColumn("refToServiceRoom_RoomName").ToString(); }
            set { SetColumn("refToServiceRoom_RoomName", value); }
        }
    }
}
