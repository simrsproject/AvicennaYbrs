namespace Temiang.Avicenna.BusinessObject
{
    public partial class AssetWorkOrderItem
    {
        public bool IsInventoryItem
        {
            get { return (bool)GetColumn("refToItemProductNonMedic_IsInventoryItem"); }
            set { SetColumn("refToItemProductNonMedic_IsInventoryItem", value); }
        }

        public bool IsEnabledGeneratePrDr
        {
            get { return (bool)GetColumn("refTo_IsEnabledGeneratePrDr"); }
            set { SetColumn("refTo_IsEnabledGeneratePrDr", value); }
        }

        public bool IsChecklistGeneratePrDr
        {
            get { return (bool)GetColumn("refTo_IsChecklistGeneratePrDr"); }
            set { SetColumn("refTo_IsChecklistGeneratePrDr", value); }
        }

        public string TransactionNoPrDr
        {
            get { return GetColumn("refToItemTransactionItem_TransactionNo").ToString(); }
            set { SetColumn("refToItemTransactionItem_TransactionNo", value); }
        }

        public string ProductAccountId
        {
            get { return GetColumn("refToItem_ProductAccountId").ToString(); }
            set { SetColumn("refToItem_ProductAccountId", value); }
        }
    }
}
