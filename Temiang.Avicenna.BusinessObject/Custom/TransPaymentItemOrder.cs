using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentItemOrder
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public decimal? TotalToDisplay
        {
            get { return (decimal?)GetColumn("refToTransPaymentItemOrder_Total"); }
            set { SetColumn("refToTransPaymentItemOrder_Total", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public DateTime? TransactionDate
        {
            get { return (DateTime?)GetColumn("refTransCharges_TransactionDate"); }
            set { SetColumn("refTransCharges_TransactionDate", value); }
        }
    }
}
