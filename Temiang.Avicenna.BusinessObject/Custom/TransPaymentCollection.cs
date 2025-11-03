using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentCollection
    {
        public DataTable TransPaymentHistory(string patientID, string registrationNo, string paymentNo)
        {
            esParameters par = new esParameters();

            //string commandText = @"SELECT FORMAT(a.[RegistrationDate], 'yyyy-MM-dd') + a.[RegistrationNo] RegDateRegNo, a.[PatientID],a.[RegistrationNo],b.[PaymentNo],b.[PaymentDate],b.[PaymentTime],
            //     CASE WHEN transactioncode IN ('017','019') THEN -1 ELSE 1 END * b.[TotalPaymentAmount] TotalPaymentAmount,
            //     b.[IsApproved],b.[IsPrinted],b.[IsVoid],b.[Notes],g.[ItemName] AS 'RegistrationTypeName',
            //     b.CreatedBy, c.[GuarantorName] AS 'ChargeToGuarantor', c.[GuarantorID] AS 'ChargeToGuarantorID',
            //     CASE WHEN transactioncode IN ('017','019') THEN -1 ELSE 1 END * d.Amount AS Amount,
            //     b.LastUpdateByUserID 
            //    FROM [Registration] a WITH(NOLOCK) 
            //    INNER JOIN [Guarantor] c WITH(NOLOCK) ON c.[GuarantorID] = a.[GuarantorID] 
            //    INNER JOIN [TransPayment] b WITH(NOLOCK) ON b.[RegistrationNo] = a.[RegistrationNo] 
            //    INNER JOIN (SELECT x.PaymentNo, SUM(x.Amount) AS Amount FROM TransPaymentItem x WITH(NOLOCK) GROUP BY x.PaymentNo) d ON d.PaymentNo = b.paymentNo 
            //    LEFT JOIN [AppStandardReferenceItem] g WITH(NOLOCK) ON (g.[ItemID] = a.[SRRegistrationType] AND g.[StandardReferenceID] = 'RegistrationType') 
            //    WHERE ((a.[PatientID] = '" + patientID + "' OR a.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WITH(NOLOCK) WHERE x.PatientID = '" + patientID + "'))) " +
            //        "AND (a.[RegistrationNo] = '" + registrationNo + "' OR '" + registrationNo + "' = '') " +
            //        "AND a.[IsVoid] = 0 " +
            //        "AND (b.[PaymentNo] = '" + paymentNo + "' OR '" + paymentNo + "' = '') " +
            //    "ORDER BY a.[RegistrationNo] ASC, b.[PaymentDate] ASC, b.[PaymentTime] ASC, b.[PaymentNo] ASC";

            string commandText = @"SELECT FORMAT(a.[RegistrationDate], 'yyyy-MM-dd') + a.[RegistrationNo] RegDateRegNo, a.[PatientID],a.[RegistrationNo],b.[PaymentNo],b.[PaymentDate],b.[PaymentTime],
	                CASE WHEN b.TransactionCode IN ('017','019') THEN -1 ELSE 1 END * b.[TotalPaymentAmount] TotalPaymentAmount,
	                b.[IsApproved],b.[IsPrinted],b.[IsVoid],b.[Notes],g.[ItemName] AS 'RegistrationTypeName',
	                b.CreatedBy, c.[GuarantorName] AS 'ChargeToGuarantor', c.[GuarantorID] AS 'ChargeToGuarantorID',
	                CASE WHEN b.TransactionCode IN ('017','019') THEN -1 ELSE 1 END * SUM(d.Amount) AS Amount,
	                b.LastUpdateByUserID 
                FROM [Registration] a WITH(NOLOCK) 
                INNER JOIN [Guarantor] c WITH(NOLOCK) ON c.[GuarantorID] = a.[GuarantorID] 
                INNER JOIN [TransPayment] b WITH(NOLOCK) ON b.[RegistrationNo] = a.[RegistrationNo] 
                INNER JOIN [TransPaymentItem] AS d WITH (NOLOCK) ON d.PaymentNo = b.PaymentNo
                LEFT JOIN [AppStandardReferenceItem] g WITH(NOLOCK) ON (g.[ItemID] = a.[SRRegistrationType] AND g.[StandardReferenceID] = 'RegistrationType') 
                WHERE ((a.[PatientID] = '" + patientID + "' OR a.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WITH(NOLOCK) WHERE x.PatientID = '" + patientID + "'))) " +
                    "AND (a.[RegistrationNo] = '" + registrationNo + "' OR '" + registrationNo + "' = '') " +
                    "AND a.[IsVoid] = 0 " +
                    "AND (b.[PaymentNo] = '" + paymentNo + "' OR '" + paymentNo + "' = '') " +
                "GROUP BY a.[RegistrationDate], a.[RegistrationNo], a.[PatientID],a.[RegistrationNo],b.[PaymentNo],b.[PaymentDate],b.[PaymentTime],b.TransactionCode," +
                    "b.[TotalPaymentAmount],b.[IsApproved],b.[IsPrinted],b.[IsVoid],b.[Notes],g.[ItemName],b.CreatedBy, c.[GuarantorName], c.[GuarantorID], b.LastUpdateByUserID " +
                "ORDER BY a.[RegistrationNo] ASC, b.[PaymentDate] ASC, b.[PaymentTime] ASC, b.[PaymentNo] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetPaymentNoByTransactionNo(string[] TransactionNos)
        {
            esParameters par = new esParameters();

            string TransNos = string.Empty;
            foreach (var tn in TransactionNos) {
                TransNos += (TransNos.Length == 0 ? "" : ",") + "'" + tn + "'";
            }

            string commandText = @"SELECT 
	                    tpio.TransactionNo, tpio.SequenceNo, tpio.PaymentNo
                    FROM TransPaymentItemOrder AS tpio WITH(NOLOCK) 
                    WHERE tpio.TransactionNo IN (" + TransNos + @") AND tpio.IsPaymentProceed = 1 AND tpio.IsPaymentReturned = 0

                    UNION ALL 

                    SELECT
	                    cc.TransactionNo, cc.SequenceNo, tpiib.PaymentNo
                    FROM CostCalculation AS cc WITH(NOLOCK) 
	                    INNER JOIN TransPaymentItemIntermBill AS tpiib WITH(NOLOCK) ON cc.IntermBillNo = tpiib.IntermBillNo
                    WHERE cc.TransactionNo IN (" + TransNos + @") AND tpiib.IsPaymentProceed = 1 AND tpiib.IsPaymentReturned = 0

                    UNION ALL 

                    SELECT
	                    cc.TransactionNo, cc.SequenceNo, tpiibg.PaymentNo
                    FROM CostCalculation AS cc WITH(NOLOCK) 
	                    INNER JOIN TransPaymentItemIntermBillGuarantor AS tpiibg WITH(NOLOCK) ON cc.IntermBillNo = tpiibg.IntermBillNo
                    WHERE cc.TransactionNo IN (" + TransNos + @") AND tpiibg.IsPaymentProceed = 1 AND tpiibg.IsPaymentReturned = 0";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public static decimal GetTotalPaymentV2(string[] RegistrationNos, string[] PaymentTypes) {

            var tp = new TransPaymentQuery("tp"); 
            var tpi = new TransPaymentItemQuery("tpi");

            tp.InnerJoin(tpi).On(tpi.PaymentNo == tp.PaymentNo)
                .Where(
                    tp.RegistrationNo.In(RegistrationNos), tp.IsVoid == false, tp.IsApproved == true,
                    tp.TransactionCode.In("016","017")
                ).Select(tp.PaymentNo, tp.TransactionCode, tpi.Amount, tpi.Balance, tpi.RoundingAmount);
            if (PaymentTypes.Count() > 0) {
                tp.Where(tpi.SRPaymentType.In(PaymentTypes));
            }
            var dtb = tp.LoadDataTable();
            var tPay = dtb.AsEnumerable().Select(x =>
                ((new string[] { "017", "019" }).Contains(x.Field<string>("TransactionCode")) ? -1 : 1) *
                (x.Field<decimal>("Amount") - x.Field<decimal>("RoundingAmount"))).Sum();

            return tPay;
        }

        public static decimal GetTotalPayment(string[] registrationNo, bool ExcludeDiscount, bool IntermbilledOnly)
        {
            {
                var headerColl = new TransPaymentCollection();
                var headerQuery = new TransPaymentQuery("tp");
                var tpiib = new TransPaymentItemIntermBillQuery("tpiib");
                var tpiibg = new TransPaymentItemIntermBillGuarantorQuery("tpiibg");
                var tpio = new TransPaymentItemOrderQuery("tpio");

                headerQuery.Where
                    (
                    //headerQuery.Or
                    //    (
                    //    headerQuery.TransactionCode == "016",
                    //    headerQuery.TransactionCode == "017"
                    //    ),
                    headerQuery.TransactionCode == "016",
                    headerQuery.RegistrationNo.In(registrationNo),
                    headerQuery.IsVoid == false,
                    headerQuery.IsApproved == true
                    );

                if (IntermbilledOnly)
                {
                    headerQuery.LeftJoin(tpiib).On(headerQuery.PaymentNo == tpiib.PaymentNo && tpiib.IsPaymentReturned == false);
                    headerQuery.LeftJoin(tpiibg).On(headerQuery.PaymentNo == tpiibg.PaymentNo && tpiibg.IsPaymentReturned == false);
                    headerQuery.Where(
                        headerQuery.Or(
                            tpiib.PaymentNo.IsNotNull(),
                            tpiibg.PaymentNo.IsNotNull()
                            )
                        );
                    headerQuery.LeftJoin(tpio).On(headerQuery.PaymentNo == tpio.PaymentNo);
                    headerQuery.Where(tpio.PaymentNo.IsNull());
                }

                headerColl.Load(headerQuery);

                decimal total = 0;

                foreach (var header in headerColl)
                {
                    var detailColl = new TransPaymentItemCollection();
                    var detailQuery = new TransPaymentItemQuery();

                    detailQuery.Where(detailQuery.PaymentNo == header.PaymentNo);
                    if (ExcludeDiscount)
                    {
                        detailQuery.Where(detailQuery.SRPaymentType != "PaymentType-005"/*discount*/);
                    }

                    detailColl.Load(detailQuery);

                    total += detailColl.Sum(detail => (detail.Amount ?? 0) - (detail.RoundingAmount ?? 0));
                }

                return total;
            }
        }
    }
}

