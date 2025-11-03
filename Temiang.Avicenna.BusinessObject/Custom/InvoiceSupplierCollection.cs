using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceSupplierCollection
    {
        public DataTable ItemTransactionOutstandingWithParameter(string supplierId, string receivedCode, string returnCode, bool AllowCash, string healthcareInitial, string srPurchaseCategorization)
        {
            esParameters par = new esParameters();

            // Add DISTINCT (Handono 2023-11-25)
            string commandText =
                @"SELECT DISTINCT a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.Pph22 AS PPH22Amount, a.Pph23 AS PPH23Amount, ISNULL(a.[PphAmount], 0) AS [PphAmount], " +
                "a.[TaxAmount], a.InvoiceNo AS InvoiceSuppNo, a.[InvoiceSupplierDate], (a.[DownPaymentAmount] + a.[StampAmount]) AS [StampAmount], " +
                "ISNULL(a.[AdvanceAmount], 0) + ISNULL((SELECT SUM(ctd.Amount) FROM CashTransactionDetail AS ctd " +
                "INNER JOIN CashTransaction AS ct ON ct.TransactionId = ctd.TransactionId AND ct.IsVoid = 0 " +
                "WHERE ctd.TransactionId IN (SELECT satt.[VALUE] FROM dbo.SplitArrayToTable(it.CashTransactionReconcileId, ',') AS satt)), 0) AS [DownPaymentAmount], 0 AS [OtherDeduction], NULL AS InvoiceSN, NULL AS TaxInvoiceDate, " +
                "CAST(0 AS BIT) AS IsEdit " +
                "FROM [ItemTransaction] a " +
                "INNER JOIN [ItemTransactionItem] b ON a.[TransactionNo] = b.[TransactionNo] " +
                "INNER JOIN ItemTransaction AS it ON it.TransactionNo = a.ReferenceNo " +
                "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 " +
                "AND a.[BusinessPartnerID] = '" + supplierId + "' ";

            // Tambah ii.[TransactionNo] = a.[TransactionNo] pada filter NOT IN sub query (Handono 2023-11-25)
            commandText += "AND a.[TransactionNo] NOT IN (SELECT ii.[TransactionNo] FROM InvoiceSupplierItem ii INNER JOIN InvoiceSupplier i ON i.[InvoiceNo] = ii.[InvoiceNo] WHERE ii.[TransactionNo] = a.[TransactionNo] AND i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 AND i.[SupplierID] = '" + supplierId + "') ";

            if (AllowCash)
                //commandText += "AND a.[TransactionCode] IN ('" + receivedCode + "', '" + returnCode + "') ";
                commandText += "AND ((a.[TransactionCode] = '" + receivedCode + "') OR (a.[TransactionCode] = '" + returnCode + "' AND a.[SRPurchaseReturnType] = 'CR')) ";
            else
                commandText += "AND ((a.[TransactionCode] = '" + receivedCode + "' AND a.[SRPurchaseOrderType] = 'CR') OR (a.[TransactionCode] = '" + returnCode + "' AND a.[SRPurchaseReturnType] = 'CR')) ";

            if (!string.IsNullOrEmpty(srPurchaseCategorization)) commandText += "AND a.[SRPurchaseCategorization] = '" + srPurchaseCategorization + "' ";

            // Diganti DISTINCT hasilnya jauh lebih cepat (Handono 2023-11-25)
            //commandText += "GROUP BY a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.[Pph22], a.[Pph23], a.[TaxAmount], a.[InvoiceNo], a.[InvoiceSupplierDate], a.[StampAmount], a.[DownPaymentAmount], a.[AdvanceAmount], a.[PphAmount], it.CashTransactionReconcileId ";

            if (healthcareInitial == "RSSA")
                commandText += "ORDER BY a.[TransactionDate], a.[TransactionNo] ";
            else
                commandText += "ORDER BY a.[TransactionDate] DESC, a.[TransactionNo] DESC ";
            this.es.Connection.CommandTimeout = 300;
            return FillDataTable(esQueryType.Text, commandText, par);
        }


        public DataTable ItemTransactionOutstandingWithParameter(DateTime? returnDate, string transactionNo, string supplierId, string returnCode, string healthcareInitial)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.Pph22 AS PPH22Amount, a.Pph23 AS PPH23Amount, ISNULL(a.[PphAmount], 0) AS [PphAmount], " +
                "a.[TaxAmount], a.InvoiceNo AS InvoiceSuppNo, (a.[DownPaymentAmount] + a.[StampAmount]) AS [StampAmount], ISNULL(a.[AdvanceAmount], 0) AS [DownPaymentAmount], 0 AS [OtherDeduction], NULL AS InvoiceSN, NULL AS TaxInvoiceDate, " +
                "CAST(0 AS BIT) AS IsEdit " +
                "FROM [ItemTransaction] a " +
                "INNER JOIN [ItemTransactionItem] b ON a.[TransactionNo] = b.[TransactionNo] ";
            commandText += "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 ";

            if (returnDate != null) commandText += "AND a.[TransactionDate] = '" + returnDate.Value.ToString("MM/dd/yyyy") + "' ";
            if (!string.IsNullOrEmpty(transactionNo)) commandText += "AND a.[TransactionNo] = '" + transactionNo + "' ";
            if (!string.IsNullOrEmpty(supplierId)) commandText += "AND a.[BusinessPartnerID] = '" + supplierId + "' ";

            if (returnCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReturn)
                commandText += "AND a.[TransactionNo] NOT IN (SELECT ii.[TransactionNo] FROM InvoiceSupplierItem ii INNER JOIN InvoiceSupplier i ON i.[InvoiceNo] = ii.[InvoiceNo] WHERE i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 AND i.[SupplierID] = '" + supplierId + "') ";

            if (returnCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReturn)
                commandText += "AND a.[TransactionCode] = '" + returnCode + "' AND a.[SRPurchaseReturnType] = 'CS' ";
            else
                commandText += "AND a.[TransactionCode] = '" + returnCode + "' AND a.[SRPurchaseOrderType] = 'CR' ";

            commandText += "GROUP BY a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.[Pph22], a.[Pph23], a.[TaxAmount], a.[InvoiceNo], a.[StampAmount], a.[DownPaymentAmount], a.[AdvanceAmount], a.[PphAmount]";

            if (healthcareInitial == "RSSA")
                commandText += "ORDER BY a.[TransactionDate], a.[TransactionNo] ";
            else
                commandText += "ORDER BY a.[TransactionDate] DESC, a.[TransactionNo] DESC ";
            this.es.Connection.CommandTimeout = 300;
            var dtb = FillDataTable(esQueryType.Text, commandText, par);

            if (returnCode == BusinessObject.Reference.TransactionCode.PurchaseOrder)
            {
                foreach (var row in dtb.AsEnumerable())
                {
                    var it = new ItemTransactionQuery("it");
                    var ivi = new InvoiceSupplierItemQuery("ivi");
                    var iv = new InvoiceSupplierQuery("iv");
                    it.InnerJoin(ivi).On(it.TransactionNo == ivi.TransactionNo)
                        .InnerJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsVoid == false)
                        .Where(it.IsApproved == true, it.IsVoid == false, it.ReferenceNo == row["TransactionNo"].ToString())
                        .Select(iv.InvoiceNo);
                    var dtRef = it.LoadDataTable();
                    if (dtRef.Rows.Count > 0)
                    {
                        row.Delete();
                    }
                }
                dtb.AcceptChanges();
            }

            return dtb;
        }

        public DataTable ItemTransactionOutstandingForEditWithParameter(string transNo, string invNo, string srPurchaseCategorization)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.Pph22 AS PPH22Amount, a.Pph23 AS PPH23Amount, ISNULL(a.[PphAmount], 0) AS [PphAmount], " +
                "a.[TaxAmount], a.InvoiceNo AS InvoiceSuppNo, a.[InvoiceSupplierDate], (a.[DownPaymentAmount] + a.[StampAmount]) AS [StampAmount], ISNULL(a.[AdvanceAmount], 0) AS [DownPaymentAmount], ISNULL(c.[OtherDeduction], 0) AS [OtherDeduction], NULL AS InvoiceSN, NULL AS TaxInvoiceDate, " +
                "CAST(1 AS BIT) AS IsEdit " +
                "FROM [ItemTransaction] a " +
                "INNER JOIN [ItemTransactionItem] b ON a.[TransactionNo] = b.[TransactionNo] " +
                "LEFT JOIN [InvoiceSupplierItem] c ON a.[TransactionNo] = c.[TransactionNo] AND c.[InvoiceNo] = '" + invNo + "' " +
                "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 " +
                "AND a.[TransactionNo] = '" + transNo + "' ";

            if (!string.IsNullOrEmpty(srPurchaseCategorization)) commandText += "AND a.[SRPurchaseCategorization] = '" + srPurchaseCategorization + "' ";

            commandText += "GROUP BY a.[TransactionNo], a.[TransactionDate], a.[ReferenceNo], a.[ChargesAmount], a.[Pph22], a.[Pph23], a.[TaxAmount], a.[InvoiceNo], a.[InvoiceSupplierDate], a.[StampAmount], a.[DownPaymentAmount], a.[AdvanceAmount], a.[PphAmount], c.[OtherDeduction]";
            this.es.Connection.CommandTimeout = 300;
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetPaymentWithPaging(int pageNumber, int pageSize, string InvoicePaymentNo, string InvoiceNo, string SupplierID, DateTime? InvoicePaymentDateFrom, DateTime? InvoicePaymentDateTo)
        {
            var ip = new InvoiceSupplierQuery("ip");
            var iip = new InvoiceSupplierItemQuery("iip");
            var s = new SupplierQuery("s");
            var i = new InvoiceSupplierQuery("i");
            var pm = new PaymentMethodQuery("pm");

            ip.InnerJoin(iip).On(ip.InvoiceNo.Equal(iip.InvoiceNo))
                .InnerJoin(s).On(ip.SupplierID.Equal(s.SupplierID))
                .InnerJoin(i).On(ip.InvoiceReferenceNo.Equal(i.InvoiceNo))
                .LeftJoin(pm).On(ip.SRInvoicePayment.Equal(pm.SRPaymentMethodID) && pm.SRPaymentTypeID.Equal("PaymentType-006"))
                .Select(
                    ip.InvoiceNo.As("InvoicePaymentNo"), ip.PaymentApprovedDateTime.As("PaymentDate"),
                    ip.SupplierID, s.SupplierName,
                    iip.PaymentAmount.Sum().As("PaymentAmount"),
                    iip.OtherDeduction.Sum().As("OtherDeduction"),
                    ip.InvoiceReferenceNo.As("InvoiceNo"),
                    pm.PaymentMethodName
                )
                .Where(ip.IsInvoicePayment.Equal(true), ip.CashTransactionReconcileId.IsNull())
                .GroupBy(ip.InvoiceNo, ip.PaymentApprovedDateTime,
                    ip.SupplierID, s.SupplierName,
                    ip.InvoiceReferenceNo, pm.PaymentMethodName
                );

            // additional params
            if (!string.IsNullOrEmpty(InvoicePaymentNo))
                ip.Where(ip.InvoiceNo.Like(InvoicePaymentNo + '%'));
            if (!string.IsNullOrEmpty(InvoiceNo))
                ip.Where(ip.InvoiceReferenceNo.Like(InvoiceNo + '%'));
            if (!string.IsNullOrEmpty(SupplierID))
                ip.Where(ip.SupplierID.Like(SupplierID));
            if (InvoicePaymentDateFrom.HasValue && InvoicePaymentDateTo.HasValue)
            {
                ip.Where(ip.PaymentApprovedDateTime.Between(InvoicePaymentDateFrom.Value, InvoicePaymentDateTo.Value));
            }

            ip.OrderBy(ip.InvoiceNo.Ascending, ip.PaymentApprovedDateTime.Ascending,
                    ip.SupplierID.Ascending, s.SupplierName.Ascending,
                    ip.InvoiceReferenceNo.Ascending, pm.PaymentMethodName.Ascending
                );

            //h.es.WithNoLock = true;
            ip.es.PageSize = pageSize;
            ip.es.PageNumber = pageNumber + 1;

            var dttbl = ip.LoadDataTable();
            return dttbl;
        }

        public int GetPaymentWithPagingCount(string InvoicePaymentNo, string InvoiceNo, string SupplierID, DateTime? InvoicePaymentDateFrom, DateTime? InvoicePaymentDateTo)
        {
            var ip = new InvoiceSupplierQuery("ip");
            var s = new SupplierQuery("s");
            var i = new InvoiceSupplierQuery("i");
            var pm = new PaymentMethodQuery("pm");

            ip.InnerJoin(s).On(ip.SupplierID.Equal(s.SupplierID))
                .InnerJoin(i).On(ip.InvoiceReferenceNo.Equal(i.InvoiceNo))
                .LeftJoin(pm).On(ip.SRInvoicePayment.Equal(pm.SRPaymentMethodID) && pm.SRPaymentTypeID.Equal("PaymentType-006"))
                .Select(
                    ip.InvoiceNo.Count().As("iCount")
                )
                .Where(ip.IsInvoicePayment.Equal(true), ip.CashTransactionReconcileId.IsNull());

            // additional params
            if (!string.IsNullOrEmpty(InvoicePaymentNo))
                ip.Where(ip.InvoiceNo.Like(InvoicePaymentNo + '%'));
            if (!string.IsNullOrEmpty(InvoiceNo))
                ip.Where(ip.InvoiceReferenceNo.Like(InvoiceNo + '%'));
            if (!string.IsNullOrEmpty(SupplierID))
                ip.Where(ip.SupplierID.Like(SupplierID));
            if (InvoicePaymentDateFrom.HasValue && InvoicePaymentDateTo.HasValue)
            {
                ip.Where(ip.PaymentApprovedDateTime.Between(InvoicePaymentDateFrom.Value, InvoicePaymentDateTo.Value));
            }


            int iCount = 0;
            var dttbl = ip.LoadDataTable();
            if (dttbl.Rows.Count > 0)
            {
                iCount = (int)dttbl.Rows[0]["iCount"];
            }
            return iCount;
        }

        public DataTable ItemTransactionOutstandingByInvoiceNo(string invNo)
        {
            esParameters par = new esParameters();

            string commandText = string.Format(@"SELECT InvoiceNo, COUNT(a.InvoiceNo) AS [RowCount]
FROM
(
    SELECT isi.InvoiceNo,
        (ISNULL(isi.Amount, 0) + ISNULL(isi.PPnAmount, 0) - ISNULL(isi.PPh22Amount, 0) - ISNULL(isi.PPh23Amount, 0) + ISNULL(isi.StampAmount, 0) - ISNULL(isi.DownPaymentAmount, 0) - ISNULL(isi.OtherDeduction, 0) - ISNULL(isi.PphAmount, 0)) - 
        ISNULL((SELECT SUM(isi2.PaymentAmount)
         FROM InvoiceSupplierItem AS isi2
         INNER JOIN InvoiceSupplier AS is1 ON isi2.InvoiceNo = is1.InvoiceNo AND is1.IsInvoicePayment = 1 AND is1.IsApproved = 1 AND is1.IsVoid = 0
         WHERE isi2.TransactionNo = isi.TransactionNo AND isi2.InvoiceReferenceNo = isi.InvoiceNo), 0) AS TotalPayment
    FROM InvoiceSupplierItem AS isi
    WHERE isi.InvoiceNo = '{0}'
) a
WHERE a.TotalPayment > 0
GROUP BY a.InvoiceNo", invNo);

            this.es.Connection.CommandTimeout = 300;
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
