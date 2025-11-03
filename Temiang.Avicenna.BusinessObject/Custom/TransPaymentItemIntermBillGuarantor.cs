using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentItemIntermBillGuarantor
    {
        public string RegistrationNo
        {
            get { return GetColumn("refIntermBill_RegistrationNo").ToString(); }
            set { SetColumn("refIntermBill_RegistrationNo", value); }
        }

        public DateTime? IntermBillDate
        {
            get { return (DateTime?)GetColumn("refIntermBill_IntermBillDate"); }
            set { SetColumn("refIntermBill_IntermBillDate", value); }
        }
        public DateTime? StartDate
        {
            get { return (DateTime?)GetColumn("refIntermBill_StartDate"); }
            set { SetColumn("refIntermBill_StartDate", value); }
        }
        public DateTime? EndDate
        {
            get { return (DateTime?)GetColumn("refIntermBill_EndDate"); }
            set { SetColumn("refIntermBill_EndDate", value); }
        }
        public decimal? PatientAmount
        {
            get { return (decimal?)GetColumn("refToIntermBill_PatientAmount"); }
            set { SetColumn("refToIntermBill_PatientAmount", value); }
        }
        public decimal? GuarantorAmount
        {
            get { return (decimal?)GetColumn("refToIntermBill_GuarantorAmount"); }
            set { SetColumn("refToIntermBill_GuarantorAmount", value); }
        }
        public string AskesCoveredSeqNo
        {
            get { return GetColumn("refIntermBill_AskesCoveredSeqNo").ToString(); }
            set { SetColumn("refIntermBill_AskesCoveredSeqNo", value); }
        }
    }
}
