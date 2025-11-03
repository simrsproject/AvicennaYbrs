using System.Data;
using System.Linq;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class VwPatientsPaidOffCollection
    {
        public DataTable GetPatientsPaidOff(string[] registrationNo)
        {
            var regs = (registrationNo.Select(t => t))
                                      .Distinct()
                                      .Aggregate(string.Empty, (current, reg) => current + (reg + "','"));

            string cmd = @" SELECT a.RegistrationNo,
                                   CAST(
                                       CASE WHEN (a.PaymentAmount - a.TxAmount) >= 0 THEN 1
                                            ELSE 0
                                       END AS BIT
                                   ) AS IsPaidOff
                            FROM   (
                                       SELECT r.RegistrationNo,
                                              (
                                                  SELECT SUM(p1.PaymentAmount) AS PaymentAmount
                                                  FROM   (
                                                             SELECT SUM(tpi.Amount) AS PaymentAmount
                                                             FROM   TransPaymentItem tpi
                                                                    INNER JOIN dbo.TransPayment tp
                                                                         ON  tpi.PaymentNo = tp.PaymentNo
                                                                         AND tp.IsApproved = 1
                                                                         AND tpi.SRPaymentType NOT IN ('PaymentType-003', 'PaymentType-004')
                                                                         AND (tp.RegistrationNo = r.RegistrationNo
                                                                              OR tp.RegistrationNo IN ('" + regs + @"'))
                                                                         AND tp.TransactionCode = '016'
                                                             UNION ALL
                                                             SELECT SUM(ISNULL(ii.PaymentAmount, 0) + ISNULL(ii.OtherAmount, 0)) AS ArPaymentAmount
                                                             FROM   dbo.InvoicesItem ii
                                                                    INNER JOIN dbo.Invoices i
                                                                         ON  ii.InvoiceNo = i.InvoiceNo
                                                                         AND i.IsApproved = 1
                                                                         AND i.IsInvoicePayment = 1
                                                                         AND (ii.RegistrationNo = r.RegistrationNo
                                                                              OR ii.RegistrationNo IN ('" + regs + @"'))
                                                         ) p1
                                              ) AS PaymentAmount,
                                              ISNULL(cc.TxAmount, 0) AS TxAmount
                                       FROM   Registration r
                                              LEFT OUTER JOIN (
                                                       SELECT RegistrationNo,
                                                              SUM(PatientAmount + GuarantorAmount) AS TxAmount
                                                       FROM   CostCalculation cc
                                                       GROUP BY
                                                              RegistrationNo
                                                   ) cc
                                                   ON  r.RegistrationNo = cc.RegistrationNo
                                       WHERE  r.RegistrationNo IN ('" + regs + @"')
                                              AND r.IsVoid = 0
                                              AND r.IsClosed = 1
                                   ) a";

            return FillDataTable(esQueryType.Text, cmd);
        }
    }
}