namespace Temiang.Avicenna.BusinessObject
{
    public partial class DietComplication
    {
        public string DietComplicationName
        {
            get { return GetColumn("refToDiet_DietName").ToString(); }
            set { SetColumn("refToDiet_DietName", value); }
        }
    }
}
