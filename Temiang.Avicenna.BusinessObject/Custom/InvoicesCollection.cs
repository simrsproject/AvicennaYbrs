using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoicesCollection
    {
        public DataTable TransPaymentOutstanding(string guarantorId, string paymentType)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[PaymentNo], " +
                                   "a.[PaymentDate], " +
                                   "a.[RegistrationNo], " +
                                   "e.[PatientID], " +
                                   "e.[MedicalNo], " +
                                   "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))) AS 'PatientName', " +
                                   "f.[GuarantorName], " +
                                   "SUM(b.[Amount]) AS 'Amount' " +
                            "FROM   [TransPayment] a " +
                                   "INNER JOIN [Registration] d " +
                                        "ON  a.[RegistrationNo] = d.[RegistrationNo] " +
                                   "INNER JOIN [Guarantor] f " +
                                        "ON  a.[GuarantorID] = f.[GuarantorID] " +
                                   "INNER JOIN [Patient] e " +
                                        "ON  d.[PatientID] = e.[PatientID] " +
                                   "INNER JOIN [TransPaymentItem] b " +
                                        "ON  a.[PaymentNo] = b.[PaymentNo] " +
                            "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 " +
                                "AND f.[GuarantorHeaderID] = '" + guarantorId + "' " +
                                "AND b.[SRPaymentType] = '" + paymentType + "' " +
                                "AND a.[PaymentNo] NOT IN (SELECT ii.[PaymentNo] FROM InvoicesItem ii INNER JOIN Invoices i ON i.[InvoiceNo] = ii.[InvoiceNo] WHERE i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 AND i.[GuarantorID] = '" + guarantorId + "') " +
                            "GROUP BY a.[PaymentNo], a.[PaymentDate], a.[RegistrationNo], e.[PatientID], e.[MedicalNo], " +
                                "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))), f.[GuarantorName]";


            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable TransPaymentOutstandingWithParameter_deactivated(string guarantorId, string paymentType, DateTime? paymentFromDate, DateTime? paymentToDate, string registrationNo, string patientName, string guarDetailId, string healthcareInitial)
        {
            /*
             * fungsi ini memiliki kelemahan jika header id guarantor berubah maka invoice bisa dibikin 2x
             * atas payment yang sama, sebagai gantinya pakai fungsi TransPaymentOutstandingWithParameter
             */
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[PaymentNo], a.[PaymentDate], a.[RegistrationNo], e.[PatientID], e.[MedicalNo], " +
                                     "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))) AS 'PatientName', " +
                                     "f.[GuarantorName], SUM(b.[Amount]) AS 'Amount' " +
                                 "FROM   [TransPayment] a " +
                                 "INNER JOIN [Registration] d ON a.[RegistrationNo] = d.[RegistrationNo] " +
                                 "INNER JOIN [Guarantor] f ON a.[GuarantorID] = f.[GuarantorID] " +
                                 "INNER JOIN [Patient] e ON d.[PatientID] = e.[PatientID] " +
                                 "INNER JOIN [TransPaymentItem] b ON a.[PaymentNo] = b.[PaymentNo] " +
                                 "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 " +
                                    "AND f.[GuarantorHeaderID] = '" + guarantorId + "' " +
                                    "AND b.[SRPaymentType] = '" + paymentType + "' " +
                                    "AND a.[PaymentNo] NOT IN (" +
                                        "SELECT ii.[PaymentNo] " +
                                        "FROM InvoicesItem ii " +
                                        "INNER JOIN Invoices i ON i.[InvoiceNo] = ii.[InvoiceNo] " +
                                        "INNER JOIN Guarantor AS g ON i.GuarantorID = g.GuarantorID " +
                                        "WHERE i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 " +
                                            "AND g.[GuarantorHeaderID] = '" + guarantorId + "') ";
            if (paymentFromDate != null && paymentToDate != null)
                commandText += "AND a.[PaymentDate] >= '" + paymentFromDate + "' AND a.[PaymentDate] <= '" + paymentToDate + "' ";
            if (!string.IsNullOrEmpty(registrationNo))
                commandText += "AND a.[RegistrationNo] = '" + registrationNo + "' ";
            if (!string.IsNullOrEmpty(patientName))
                commandText += "AND (e.[MedicalNo] = '" + patientName + "' OR e.[OldMedicalNo] = '" + patientName + "' OR e.[FirstName] LIKE '%" + patientName + "%' OR e.[MiddleName] LIKE '%" + patientName + "%' OR e.[LastName] LIKE '%" + patientName + "%') ";
            if (!string.IsNullOrEmpty(guarDetailId))
                commandText += "AND a.[GuarantorID] = '" + guarDetailId + "' ";

            commandText += "GROUP BY a.[PaymentNo], a.[PaymentDate], a.[RegistrationNo], e.[PatientID], e.[MedicalNo], " +
                                "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))), f.[GuarantorName]";

            if (healthcareInitial == "RSSA")
                commandText += "ORDER BY a.[PaymentNo] DESC ";
            else if (healthcareInitial == "RSUI" || healthcareInitial == "RSPM")
                commandText += "ORDER BY a.[RegistrationNo] ";
            else
                commandText += "ORDER BY a.[PaymentNo] ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable TransPaymentOutstandingWithParameter(string guarantorId, string paymentType,
            DateTime? paymentFromDate, DateTime? paymentToDate, string registrationNo,
            string patientName, string guarDetailId, string healthcareInitial,
            DateTime? dischargeDateFrom, DateTime? dischargeDateTo, string srRegistrationType,
            string rtInpatient, string ServiceUnitID)
        {
            esParameters par = new esParameters();

            string commandText = "SELECT a.[PaymentNo], a.[PaymentDate], a.[RegistrationNo], " +
                                     "CASE d.SRRegistrationType WHEN 'IPR' THEN d.[DischargeDate] ELSE d.RegistrationDate END DischargeDate, " +
                                     "e.[PatientID], e.[MedicalNo], " +
                                     "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))) AS 'PatientName', " +
                                     "f.[GuarantorName], SUM(b.[Amount]) AS 'Amount' " +
                                 "FROM   [TransPayment] a " +
                                 "INNER JOIN [Registration] d ON a.[RegistrationNo] = d.[RegistrationNo] " +
                                 "INNER JOIN [Guarantor] f ON a.[GuarantorID] = f.[GuarantorID] " +
                                 "INNER JOIN [Patient] e ON d.[PatientID] = e.[PatientID] " +
                                 "INNER JOIN [TransPaymentItem] b ON a.[PaymentNo] = b.[PaymentNo] " +
                                 "LEFT JOIN ( " +
                                    "SELECT ii.InvoiceNo, ii.PaymentNo " +
                                    "FROM Invoices AS i " +
                                    "INNER JOIN InvoicesItem AS ii ON i.InvoiceNo = ii.InvoiceNo " +
                                    "WHERE i.IsVoid = 0 " +
                                        "AND i.IsInvoicePayment = 0 " +
                                 ") r ON a.PaymentNo = r.PaymentNo " +
                                 "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 " +
                                    "AND f.[GuarantorHeaderID] = '" + guarantorId + "' " +
                                    "AND b.[SRPaymentType] = '" + paymentType + "' " +
                                    "AND r.PaymentNo IS NULL ";
            if (paymentFromDate != null && paymentToDate != null)
                commandText += "AND a.[PaymentDate] >= '" + paymentFromDate + "' AND a.[PaymentDate] <= '" + paymentToDate + "' ";
            if (!string.IsNullOrEmpty(registrationNo))
                commandText += "AND a.[RegistrationNo] = '" + registrationNo + "' ";
            if (!string.IsNullOrEmpty(patientName))
                commandText += "AND (e.[MedicalNo] = '" + patientName + "' OR e.[OldMedicalNo] = '" + patientName + "' OR e.[FirstName] LIKE '%" + patientName + "%' OR e.[MiddleName] LIKE '%" + patientName + "%' OR e.[LastName] LIKE '%" + patientName + "%') ";
            if (!string.IsNullOrEmpty(guarDetailId))
                commandText += "AND a.[GuarantorID] = '" + guarDetailId + "' ";
            if (dischargeDateFrom != null && dischargeDateTo != null)
                commandText += "AND (CASE d.SRRegistrationType WHEN 'IPR' THEN d.[DischargeDate] ELSE d.RegistrationDate END) >= '" +
                    dischargeDateFrom +
                    "' AND (CASE d.SRRegistrationType WHEN 'IPR' THEN d.[DischargeDate] ELSE d.RegistrationDate END) <= '" + dischargeDateTo + "' ";
            if (!string.IsNullOrEmpty(srRegistrationType))
            {
                if (srRegistrationType == rtInpatient)
                    commandText += "AND d.[SRRegistrationType] = '" + rtInpatient + "' ";
                else
                    commandText += "AND d.[SRRegistrationType] <> '" + rtInpatient + "' ";
            }

            if (!string.IsNullOrEmpty(ServiceUnitID))
                commandText += "AND d.[ServiceUnitID] = '" + ServiceUnitID + "' ";

            commandText += "GROUP BY a.[PaymentNo], a.[PaymentDate], a.[RegistrationNo], " +
                                "CASE d.SRRegistrationType WHEN 'IPR' THEN d.[DischargeDate] ELSE d.RegistrationDate END, " +
                                "e.[PatientID], e.[MedicalNo], " +
                                "RTRIM(LTRIM(((RTRIM(LTRIM(((e.[FirstName] + ' ') + e.[MiddleName]))) + ' ') + e.[LastName]))), f.[GuarantorName]";

            if (healthcareInitial == "RSSA")
                commandText += "ORDER BY a.[PaymentNo] DESC ";
            else if (healthcareInitial == "RSUI" || healthcareInitial == "RSPM")
                commandText += "ORDER BY a.[RegistrationNo] ";
            else
                commandText += "ORDER BY a.[PaymentNo] ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetPaymentWithPaging(int pageNumber, int pageSize,
           string InvoicePaymentNo, string InvoiceNo, string GuarantorID,
            DateTime? InvoicePaymentDateFrom, DateTime? InvoicePaymentDateTo)
        {
            var ip = new InvoicesQuery("ip");
            var iip = new InvoicesItemQuery("iip");
            var g = new GuarantorQuery("g");
            var i = new InvoicesQuery("i");
            var pm = new PaymentMethodQuery("pm");

            ip.InnerJoin(iip).On(ip.InvoiceNo.Equal(iip.InvoiceNo))
                .InnerJoin(g).On(ip.GuarantorID.Equal(g.GuarantorID))
                .InnerJoin(i).On(ip.InvoiceReferenceNo.Equal(i.InvoiceNo))
                .LeftJoin(pm).On(ip.SRPaymentMethod.Equal(pm.SRPaymentMethodID) && ip.SRPaymentType.Equal(pm.SRPaymentTypeID))
                .Select(
                    ip.InvoiceNo.As("InvoicePaymentNo"), ip.PaymentDate,
                    ip.GuarantorID, g.GuarantorName,
                    iip.PaymentAmount.Sum().As("PaymentAmount"),
                    iip.OtherAmount.Sum().As("OtherAmount"),
                    iip.BankCost.Sum().As("BankCost"),
                    ip.InvoiceReferenceNo.As("InvoiceNo"),
                    pm.PaymentMethodName
                )
                .Where(ip.IsInvoicePayment.Equal(true), ip.CashTransactionReconcileId.IsNull())
                .GroupBy(ip.InvoiceNo, ip.PaymentDate,
                    ip.GuarantorID, g.GuarantorName,
                    ip.InvoiceReferenceNo, pm.PaymentMethodName
                );

            // additional params
            if (!string.IsNullOrEmpty(InvoicePaymentNo))
                ip.Where(ip.InvoiceNo.Like(InvoicePaymentNo + '%'));
            if (!string.IsNullOrEmpty(InvoiceNo))
                ip.Where(ip.InvoiceReferenceNo.Like(InvoiceNo + '%'));
            if (!string.IsNullOrEmpty(GuarantorID))
                ip.Where(ip.GuarantorID.Like(GuarantorID));
            if (InvoicePaymentDateFrom.HasValue && InvoicePaymentDateTo.HasValue)
            {
                ip.Where(ip.PaymentDate.Between(InvoicePaymentDateFrom.Value, InvoicePaymentDateTo.Value));
            }

            ip.OrderBy(ip.InvoiceNo.Ascending, ip.PaymentDate.Ascending,
                    ip.GuarantorID.Ascending, g.GuarantorName.Ascending,
                    ip.InvoiceReferenceNo.Ascending, pm.PaymentMethodName.Ascending
                );

            //h.es.WithNoLock = true;
            ip.es.PageSize = pageSize;
            ip.es.PageNumber = pageNumber + 1;

            var dttbl = ip.LoadDataTable();
            return dttbl;
        }

        public int GetPaymentWithPagingCount(string InvoicePaymentNo, string InvoiceNo, string GuarantorID,
            DateTime? InvoicePaymentDateFrom, DateTime? InvoicePaymentDateTo)
        {
            var ip = new InvoicesQuery("ip");
            var g = new GuarantorQuery("g");
            var i = new InvoicesQuery("i");
            var pm = new PaymentMethodQuery("pm");

            ip.InnerJoin(g).On(ip.GuarantorID.Equal(g.GuarantorID))
                .InnerJoin(i).On(ip.InvoiceReferenceNo.Equal(i.InvoiceNo))
                .LeftJoin(pm).On(ip.SRPaymentMethod.Equal(pm.SRPaymentMethodID) && ip.SRPaymentType.Equal(pm.SRPaymentTypeID))
                .Select(
                    ip.InvoiceNo.Count().As("iCount")
                )
                .Where(ip.IsInvoicePayment.Equal(true), ip.CashTransactionReconcileId.IsNull());

            // additional params
            if (!string.IsNullOrEmpty(InvoicePaymentNo))
                ip.Where(ip.InvoiceNo.Like(InvoicePaymentNo + '%'));
            if (!string.IsNullOrEmpty(InvoiceNo))
                ip.Where(ip.InvoiceReferenceNo.Like(InvoiceNo + '%'));
            if (!string.IsNullOrEmpty(GuarantorID))
                ip.Where(ip.GuarantorID.Like(GuarantorID));
            if (InvoicePaymentDateFrom.HasValue && InvoicePaymentDateTo.HasValue)
            {
                ip.Where(ip.PaymentDate.Between(InvoicePaymentDateFrom.Value, InvoicePaymentDateTo.Value));
            }


            int iCount = 0;
            var dttbl = ip.LoadDataTable();
            if (dttbl.Rows.Count > 0)
            {
                iCount = (int)dttbl.Rows[0]["iCount"];
            }
            return iCount;
        }

        public static decimal GetTotalPaymentByRegistrationNo(string[] RegistrationNo) {
            var iviColl = new InvoicesItemCollection();
            var ivi = new InvoicesItemQuery("ivi");
            var iv = new InvoicesQuery("iv");
            ivi.InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo)
                .Where(ivi.RegistrationNo.In(RegistrationNo), iv.IsInvoicePayment == true, iv.IsApproved == true, iv.IsVoid == false)
                .Select(ivi.PaymentAmount);
            if (iviColl.Load(ivi)) {
                return iviColl.Sum(x => x.PaymentAmount ?? 0);
            }
            return 0;
        }
        public static decimal GetTotalInvoicedByRegistrationNo(string[] RegistrationNo)
        {
            var iviColl = new InvoicesItemCollection();
            var ivi = new InvoicesItemQuery("ivi");
            var iv = new InvoicesQuery("iv");
            ivi.InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo)
                .Where(ivi.RegistrationNo.In(RegistrationNo), iv.IsInvoicePayment == false, iv.IsApproved == true, iv.IsVoid == false)
                .Select(ivi.VerifyAmount);
            if (iviColl.Load(ivi))
            {
                return iviColl.Sum(x => x.VerifyAmount ?? 0);
            }
            return 0;
        }
        
    }
}
