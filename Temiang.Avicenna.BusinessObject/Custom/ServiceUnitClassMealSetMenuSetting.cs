namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitClassMealSetMenuSetting
    {
        public string MealSet
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}
