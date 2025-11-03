namespace Temiang.Avicenna.BusinessObject
{
    public partial class AbRestrictionItem
    {
        public string ZatActiveName
        {
            get { return GetColumn("refToZatActive_ZatActiveName").ToString(); }
            set { SetColumn("refToZatActive_ZatActiveName", value); }
        }
    }
}
