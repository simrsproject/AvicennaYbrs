namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorItemGroupProductMargin
    {
        public string ItemTypeName
        {
            get { return GetColumn("refToStdRef_ItemType").ToString(); }
            set { SetColumn("refToStdRef_ItemType", value); }
        }
        public string ItemGroupName
        {
            get { return GetColumn("refToItemGroup_ItemGroupName").ToString(); }
            set { SetColumn("refToItemGroup_ItemGroupName", value); }
        }
        public string MarginName
        {
            get { return GetColumn("refToItemProductMargin_MarginName").ToString(); }
            set { SetColumn("refToItemProductMargin_MarginName", value); }
        }
    }
}
