namespace Temiang.Avicenna.BusinessObject
{
    public partial class ZatActiveInteraction
    {
        public string InteractionZatActiveName
        {
            get { return GetColumn("refZatActive_ZatActiveName").ToString(); }
            set { SetColumn("refZatActive_ZatActiveName", value); }
        }
    }
}