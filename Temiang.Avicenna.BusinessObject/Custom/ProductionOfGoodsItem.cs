namespace Temiang.Avicenna.BusinessObject
{
    public partial class ProductionOfGoodsItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public decimal? Balance
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Balance");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Balance", value); }
        }
    }
}
