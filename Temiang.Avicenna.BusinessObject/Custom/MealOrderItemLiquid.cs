namespace Temiang.Avicenna.BusinessObject
{
    public partial class MealOrderItemLiquid
    {
        public string FoodName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }
    }
}
