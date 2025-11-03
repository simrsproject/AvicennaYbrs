namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitItemService
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string ChartOfAccountCode
        {
            get { return GetColumn("refToChartOfAccount_ChartOfAccountCode").ToString(); }
            set { SetColumn("refToChartOfAccount_ChartOfAccountCode", value); }
        }
        public string SubLedgerName
        {
            get { return GetColumn("refToSubLedger_SubLedgerName").ToString(); }
            set { SetColumn("refToSubLedger_SubLedgerName", value); }
        }
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
        public string IdiName
        {
            get { return GetColumn("refToItemIdi_IdiName").ToString(); }
            set { SetColumn("refToItemIdi_IdiName", value); }
        }
    }
}
