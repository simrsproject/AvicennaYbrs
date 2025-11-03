using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentPatientCollection
    {
        public DataTable TransPaymentPatientHistory(string patientID, string paymentNo)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[PatientID],a.[PaymentNo],a.[PaymentDate],a.[PaymentTime],
                    a.[IsApproved],a.[IsVoid],a.[Notes],
                    CASE WHEN transactioncode = '019' THEN -1 else 1 end * d.Amount as Amount, a.LastUpdateByUserID
                    FROM [TransPaymentPatient] a                 
                    INNER JOIN (SELECT x.PaymentNo, SUM(x.Amount) AS Amount FROM TransPaymentPatientItem x GROUP BY x.PaymentNo) d ON a.PaymentNo = d.paymentNo
                    WHERE ((a.[PatientID] = '" + patientID + "' OR a.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "'))) " +
                                 "AND a.[PaymentNo] LIKE '%" + paymentNo + "%' " +
                                 "ORDER BY a.[PaymentDate] ASC,a.[PaymentTime] ASC, a.[PaymentNo] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
