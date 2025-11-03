namespace Temiang.Avicenna.BusinessObject
{
    public partial class EpisodeSOAPE
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }
    }
}
