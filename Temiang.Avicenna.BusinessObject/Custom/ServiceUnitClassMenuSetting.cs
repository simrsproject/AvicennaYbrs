namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitClassMenuSetting
    {
        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }
    }
}
