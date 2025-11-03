namespace Temiang.Avicenna.BusinessObject
{
    public partial class MenuItemExtraFood
    {
        public string DayName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
        public string FoodName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }
        public string FoodGroup
        {
            get { return GetColumn("refToFood_FoodGroup").ToString(); }
            set { SetColumn("refToFood_FoodGroup", value); }
        }
    }
}
