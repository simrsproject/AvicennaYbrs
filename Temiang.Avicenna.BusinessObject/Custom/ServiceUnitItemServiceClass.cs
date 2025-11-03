namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitItemServiceClass
    {
        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }

        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public string COARevenueName
        {
            get { return GetColumn("refToChartOfAccounts_COARevenueName").ToString(); }
            set { SetColumn("refToChartOfAccounts_COARevenueName", value); }
        }

        public string SubledgerRevenueName
        {
            get { return GetColumn("refToSubledgers_SubledgerRevenueName").ToString(); }
            set { SetColumn("refToSubledgers_SubledgerRevenueName", value); }
        }

        public string COADiscountName
        {
            get { return GetColumn("refToChartOfAccounts_COADiscountName").ToString(); }
            set { SetColumn("refToChartOfAccounts_COADiscountName", value); }
        }

        public string SubledgerDiscountName
        {
            get { return GetColumn("refToSubledgers_SubledgerDiscountName").ToString(); }
            set { SetColumn("refToSubledgers_SubledgerDiscountName", value); }
        }

        public string COACostName
        {
            get { return GetColumn("refToChartOfAccounts_COACostName").ToString(); }
            set { SetColumn("refToChartOfAccounts_COACostName", value); }
        }

        public string SubledgerCostName
        {
            get { return GetColumn("refToSubledgers_SubledgerCostName").ToString(); }
            set { SetColumn("refToSubledgers_SubledgerCostName", value); }
        }
        //public string ChartOfAccountName
        //{
        //    get { return GetColumn("refToChartOfAccounts_ChartOfAccountName").ToString(); }
        //    set { SetColumn("refToChartOfAccounts_ChartOfAccountName", value); }
        //}

        //public string SubledgerName
        //{
        //    get { return GetColumn("refToSubledgers_SubledgerName").ToString(); }
        //    set { SetColumn("refToSubledgers_SubledgerName", value); }
        //}

    }
}
