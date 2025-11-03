using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class InvoiceCustomerCollection
    {
        public DataTable ItemTransactionOutstanding(string customerId)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[TransactionNo], a.[TransactionDate], b.[CustomerName], a.[Notes], " +
                                    "a.[ChargesAmount], a.[TaxAmount], (a.[ChargesAmount] + a.[TaxAmount]) AS 'Amount' " +
                                "FROM [ItemTransaction] a " +
                                "INNER JOIN [Customer] b ON a.[CustomerID] = b.[CustomerID] " +
                                "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 AND a.[TransactionCode] IN ('101', '102') " +
                                    "AND a.[CustomerID] = '" + customerId + "' " +
	                                "AND a.[TransactionNo] NOT IN (SELECT ii.[TransactionNo] FROM InvoiceCustomerItem ii " +
								            "INNER JOIN InvoiceCustomer i ON i.[InvoiceNo] = ii.[InvoiceNo] " +
                                            "WHERE i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 AND i.[CustomerID] = '" + customerId + "') ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemTransactionOutstandingWithParameter(string customerId, DateTime? transactionFromDate, DateTime? transactionToDate)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[TransactionNo], a.[TransactionDate], b.[CustomerName], a.[Notes], c.[ItemName] AS 'PaymentTypeName', " +
                                    "a.[ChargesAmount], a.[TaxAmount], (a.[ChargesAmount] + a.[TaxAmount]) AS 'Amount' " +
                                "FROM [ItemTransaction] a " +
                                "INNER JOIN [Customer] b ON a.[CustomerID] = b.[CustomerID] " +
                                "LEFT JOIN (SELECT z.ItemID, z.ItemName FROM AppStandardReferenceItem AS z WHERE z.StandardReferenceID = 'STBPaymentType') c ON c.ItemID = a.SRPaymentType " +
                                "WHERE a.[IsApproved] = 1 AND a.[IsVoid] = 0 AND a.[TransactionCode] IN ('101', '102') " +
                                    "AND a.[CustomerID] = '" + customerId + "' " +
                                    "AND a.[TransactionNo] NOT IN (SELECT ii.[TransactionNo] FROM InvoiceCustomerItem ii " +
                                            "INNER JOIN InvoiceCustomer i ON i.[InvoiceNo] = ii.[InvoiceNo] " +
                                            "WHERE i.[IsVoid] = 0 AND i.[IsInvoicePayment] = 0 AND i.[CustomerID] = '" + customerId + "') ";

            if (transactionFromDate != null && transactionToDate != null)
                commandText += "AND a.[TransactionDate] >= '" + transactionFromDate + "' AND a.[TransactionDate] <= '" + transactionToDate + "' ";

            commandText += "ORDER BY a.[TransactionDate], a.[TransactionNo] ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
