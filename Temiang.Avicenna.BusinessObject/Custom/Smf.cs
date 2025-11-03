namespace Temiang.Avicenna.BusinessObject
{
    public partial class Smf
    {
        public string ParamedicFeeCaseTypeName
        {
            get { return GetColumn("refTo_ParamedicFeeCaseTypeName").ToString(); }
            set { SetColumn("refTo_ParamedicFeeCaseTypeName", value); }
        }
    }
}
