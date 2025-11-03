namespace Temiang.Avicenna.BusinessObject
{
    public partial class MenuItemFood
    {
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

        public string FoodType
        {
            get { return GetColumn("refToFood_FoodGroup2").ToString(); }
            set { SetColumn("refToFood_FoodGroup2", value); }
        }

        public string MenuItemFoodGroup
        {
            get { return GetColumn("refToStdRef_MenuItemFoodGroup").ToString(); }
            set { SetColumn("refToStdRef_MenuItemFoodGroup", value); }
        }
    }
}
