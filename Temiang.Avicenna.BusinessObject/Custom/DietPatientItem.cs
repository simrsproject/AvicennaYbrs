namespace Temiang.Avicenna.BusinessObject
{
    public partial class DietPatientItem
    {
        public string DietName
        {
            get { return GetColumn("refToDiet_DietName").ToString(); }
            set { SetColumn("refToDiet_DietName", value); }
        }

        public string MenuName
        {
            get { return GetColumn("refToMenu_MenuName").ToString(); }
            set { SetColumn("refToMenu_MenuName", value); }
        }

        public string FormOfFoodName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}
