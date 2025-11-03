namespace Temiang.Avicenna.BusinessObject
{
    public partial class ThrScheduleItem
    {
        public string ReligionName
        {
            get { return GetColumn("refToReligionName").ToString(); }
            set { SetColumn("refToReligionName", value); }
        }
    }
}
