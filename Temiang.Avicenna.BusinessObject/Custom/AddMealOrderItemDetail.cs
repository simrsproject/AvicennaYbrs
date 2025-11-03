namespace Temiang.Avicenna.BusinessObject
{
    public partial class AddMealOrderItemDetail
    {
        public string FoodName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }
    }
}
