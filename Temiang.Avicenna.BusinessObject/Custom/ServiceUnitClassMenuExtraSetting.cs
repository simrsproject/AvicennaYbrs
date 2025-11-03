namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitClassMenuExtraSetting
    {
        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }

        public string MenuName
        {
            get { return GetColumn("refToMenu_MenuName").ToString(); }
            set { SetColumn("refToMenu_MenuName", value); }
        }
    }
}
