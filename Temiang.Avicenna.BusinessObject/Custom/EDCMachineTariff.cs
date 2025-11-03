namespace Temiang.Avicenna.BusinessObject
{
    public partial class EDCMachineTariff
    {
        public string CardTypeName
        {
            get { return GetColumn("refToSRItem_ItemName").ToString(); }
            set { SetColumn("refToSRItem_ItemName", value); }
        }
    }
}
