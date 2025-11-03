namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPrescriptionOrder
    {
        public string FromUnit
        {
            get { return GetColumn("refToServiceUnit_FromUnit").ToString(); }
            set { SetColumn("refToServiceUnit_FromUnit", value); }
        }

        public string ToUnit
        {
            get { return GetColumn("refToServiceUnit_ToUnit").ToString(); }
            set { SetColumn("refToServiceUnit_ToUnit", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }
    }
}
