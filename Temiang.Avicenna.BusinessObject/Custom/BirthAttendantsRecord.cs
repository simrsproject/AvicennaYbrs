namespace Temiang.Avicenna.BusinessObject
{
    public partial class BirthAttendantsRecord
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string MidwivesType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemNameMidwivesType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemNameMidwivesType", value); }
        }

        public string ParamedicType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemNameParamedicType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemNameParamedicType", value); }
        }
    }
}
