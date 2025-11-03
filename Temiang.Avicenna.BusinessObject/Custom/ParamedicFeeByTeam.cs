namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeByTeam
    {
        public string ParamedicMemberName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }
    }
}
