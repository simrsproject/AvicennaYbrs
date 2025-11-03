namespace Temiang.Avicenna.BusinessObject
{
    public partial class LiquidFoodDietTimeGender
    {
        public string GenderName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string FoodName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }

        public string ChildrenFoodName
        {
            get { return GetColumn("refToFood_ChildrenFoodName").ToString(); }
            set { SetColumn("refToFood_ChildrenFoodName", value); }
        }
    }
}
