namespace Temiang.Avicenna.BusinessObject
{
    public partial class DietMenu
    {
        public string FormOfFoodName
        {
            get { return GetColumn("refToStdReff_FormOfFoodName").ToString(); }
            set { SetColumn("refToStdReff_FormOfFoodName", value); }
        }

        public string MenuName
        {
            get { return GetColumn("refToMenu_MenuName").ToString(); }
            set { SetColumn("refToMenu_MenuName", value); }
        }
    }
}
