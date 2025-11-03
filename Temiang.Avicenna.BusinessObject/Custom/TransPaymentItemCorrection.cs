using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentItemCorrection
    {
        public string RegistrationNo
        {
            get { return GetColumn("refToRegistration_RegistrationNo").ToString(); }
            set { SetColumn("refToRegistration_RegistrationNo", value); }
        }
        public string PatientName
        {
            get { return GetColumn("refToPatient_PatientName").ToString(); }
            set { SetColumn("refToPatient_PatientName", value); }
        }
        public string PaymentMethodOName
        {
            get { return GetColumn("refToPM_PaymentMethodOName").ToString(); }
            set { SetColumn("refToPM_PaymentMethodOName", value); }
        }

        public string CardProviderOName
        {
            get { return GetColumn("refToCP_CardProviderOName").ToString(); }
            set { SetColumn("refToCP_CardProviderOName", value); }
        }
        public string CardTypeOName
        {
            get { return GetColumn("refToCT_CardTypeOName").ToString(); }
            set { SetColumn("refToCT_CardTypeOName", value); }
        }
        public string EDCMachineOName
        {
            get { return GetColumn("refToEDC_EDCMachineOName").ToString(); }
            set { SetColumn("refToEDC_EDCMachineOName", value); }
        }

        public string CardProviderCName
        {
            get { return GetColumn("refToCP_CardProviderCName").ToString(); }
            set { SetColumn("refToCP_CardProviderCName", value); }
        }
        public string CardTypeCName
        {
            get { return GetColumn("refToCT_CardTypeCName").ToString(); }
            set { SetColumn("refToCT_CardTypeCName", value); }
        }
        public string EDCMachineCName
        {
            get { return GetColumn("refToEDC_EDCMachineCName").ToString(); }
            set { SetColumn("refToEDC_EDCMachineCName", value); }
        }
        
        public decimal? Amount
        {
            get { return (decimal?)GetColumn("refToPayment_Amount"); }
            set { SetColumn("refToPayment_Amount", value); }
        }
    }
}
