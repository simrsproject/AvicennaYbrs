namespace Temiang.Avicenna.BusinessObject
{
    public partial class FoodPackage
    {
        public string FoodDetailName
        {
            get { return GetColumn("refToFood_FoodName").ToString(); }
            set { SetColumn("refToFood_FoodName", value); }
        }
    }
}
