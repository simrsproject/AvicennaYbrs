using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaunderedProcessItemRewashingCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDate)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT b.[TransactionDate],a.[TransactionNo],a.[ItemID],e.[ItemName] AS 'ItemName',
                    a.[QtyRewashing],(a.QtyRewashing - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRItemUnit],f.[ItemName] AS 'ItemUnit'  
                FROM [LaundryRecapitulationProcessItem] a 
                INNER JOIN [LaundryRecapitulationProcess] b ON a.[TransactionNo] = b.[TransactionNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReferenceNo, xx.ItemID, xx.Qty 
	                FROM [LaunderedProcessItemRewashing] xx
	                INNER JOIN [LaunderedProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReferenceNo = b.TransactionNo AND c.ItemID = a.ItemID
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                WHERE  a.[QtyRewashing] > 0 AND b.[IsApproved] = 1  ";

            commandText += "AND b.[TransactionDate] == '" + receiveDate + "' ";

            commandText += "GROUP BY b.[TransactionDate],a.[TransactionNo],a.[ItemID],e.[ItemName],a.[QtyRewashing],a.[SRItemUnit],f.[ItemName] ";
            commandText += "HAVING (a.QtyRewashing - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[TransactionNo] ASC, a.[ItemID] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate()
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT b.[TransactionDate],a.[TransactionNo],a.[ItemID],e.[ItemName] AS 'ItemName',
                    a.[QtyRewashing],(a.QtyRewashing - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRItemUnit],f.[ItemName] AS 'ItemUnit' 
                FROM [LaundryRecapitulationProcessItem] a 
                INNER JOIN [LaundryRecapitulationProcess] b ON a.[TransactionNo] = b.[TransactionNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReferenceNo, xx.ItemID, xx.Qty 
	                FROM [LaunderedProcessItemRewashing] xx
	                INNER JOIN [LaunderedProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReferenceNo = b.TransactionNo AND c.ItemID = a.ItemID
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                WHERE a.[QtyRewashing] > 0 AND b.[IsApproved] = 1  ";

            commandText += "GROUP BY b.[TransactionDate],a.[TransactionNo],a.[ItemID],e.[ItemName],a.[QtyRewashing],a.[SRItemUnit],f.[ItemName] ";
            commandText += "HAVING (a.QtyRewashing - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[TransactionNo] ASC, a.[ItemID] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
