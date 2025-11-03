namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicAutoBillItem
    {
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ItemUnit
        {
            get { return GetColumn("refToAppStandardReferenceItem_SRItemUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SRItemUnit", value); }
        }
    }
}