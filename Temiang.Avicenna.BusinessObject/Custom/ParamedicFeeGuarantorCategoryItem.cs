namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeGuarantorCategoryItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}
