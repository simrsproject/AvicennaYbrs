namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlMasterReportItem
    {
        public string ParamedicRL1Name
        {
            get { return GetColumn("refToSmf_SmfName").ToString(); }
            set { SetColumn("refToSmf_SmfName", value); }
        }
    }
}