using System.Data;
using System.Linq;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeTransChargesItemComp
    {
        public string ItemID
        {
            get { return GetColumn("refToTransChargesItem_ItemID").ToString(); }
            set { SetColumn("refToTransChargesItem_ItemID", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string RegistrationNo
        {
            get { return GetColumn("refToTransCharges_RegistrationNo").ToString(); }
            set { SetColumn("refToTransCharges_RegistrationNo", value); }
        }

        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }

        public string PatientName
        {
            get { return GetColumn("refToPatient_PatientName").ToString(); }
            set { SetColumn("refToPatient_PatientName", value); }
        }

        public string KeyField
        {
            get { return GetColumn("refTransChargesItemComp_KeyField").ToString(); }
            set { SetColumn("refTransChargesItemComp_KeyField", value); }
        }

        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }

        public string PaymentMethod
        {
            get { return GetColumn("refToVwClosedRegistration_PaymentMethod").ToString(); }
            set { SetColumn("refToVwClosedRegistration_PaymentMethod", value); }
        }
    }

    public partial class ParamedicFeeTransChargesItemCompCollection
    {
         public DataTable GetPaymentType(string registrationNo, string transactionNo, string sequenceNo)
         {
             string cmd = @"SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName FROM TransPayment tp
                            INNER JOIN TransPaymentItem tpi ON tpi.PaymentNo = tp.PaymentNo
                            INNER JOIN TransPaymentItemOrder tpio ON tpio.PaymentNo = tp.PaymentNo
                            INNER JOIN PaymentType pt ON tpi.SRPaymentType = pt.SRPaymentTypeID
                            LEFT JOIN PaymentMethod pm ON tpi.SRPaymentMethod = pm.SRPaymentMethodID
                                AND pm.SRPaymentTypeID = pt.SRPaymentTypeID
                            WHERE tp.RegistrationNo = '" + registrationNo + @"'
                                AND tpio.TransactionNo = '" + transactionNo + @"'
                                AND tpio.SequenceNo = '" + sequenceNo + @"'
                                AND tp.IsApproved = 1";

             return FillDataTable(esQueryType.Text, cmd);
         }

         public DataTable GetPaymentType(string[] registrationNo)
         {
             var regs = (registrationNo.Select(t => t)).Distinct()
                                                       .Aggregate(string.Empty, (current, reg) => current + (reg + "','"));

             string cmd = @"SELECT ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) AS PaymentMethodName FROM TransPayment tp
                            INNER JOIN TransPaymentItem tpi ON tpi.PaymentNo = tp.PaymentNo
                            INNER JOIN PaymentType pt ON tpi.SRPaymentType = pt.SRPaymentTypeID
                            LEFT JOIN PaymentMethod pm ON tpi.SRPaymentMethod = pm.SRPaymentMethodID
                                AND pm.SRPaymentTypeID = pt.SRPaymentTypeID
                            WHERE tp.RegistrationNo IN ('" + regs + @"')
                                AND tp.IsApproved = 1";

             return FillDataTable(esQueryType.Text, cmd);
         }
    }
}
