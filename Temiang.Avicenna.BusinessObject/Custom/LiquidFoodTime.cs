namespace Temiang.Avicenna.BusinessObject
{
    public partial class LiquidFoodTime
    {
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
