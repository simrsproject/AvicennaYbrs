namespace Temiang.Avicenna.BusinessObject
{
    public partial class MealOrderItem
    {
        public string FoodName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }

        public string FoodGroupId
        {
            get { return GetColumn("refToFood_FoodGroupID").ToString(); }
            set { SetColumn("refToFood_FoodGroupID", value); }
        }

        public string FoodGroupName
        {
            get { return GetColumn("refToFood_FoodGroupName").ToString(); }
            set { SetColumn("refToFood_FoodGroupName", value); }
        }
    }
}
