namespace Temiang.Avicenna.BusinessObject
{
    public partial class ClassMealSetMenuSetting
    {
        public string MealSetName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}
