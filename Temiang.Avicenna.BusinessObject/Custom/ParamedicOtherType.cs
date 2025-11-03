namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicOtherType
    {
        public string ParamedicTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ParamedicType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ParamedicType", value); }
        }
    }
}
