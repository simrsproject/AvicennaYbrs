namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorAutoBillItem
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
            get { return GetColumn("refToItem_ItemUnit").ToString(); }
            set { SetColumn("refToItem_ItemUnit", value); }
        }
    }
}
