using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryReturnedItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime processDate, string serviceUnitId, string roomId)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRItemUnit],f.[ItemName] AS 'ItemUnit',c.[Notes], CAST(0 AS BIT) AS 'IsInfectious' 
                FROM [LaunderedProcessItem] a INNER JOIN [LaunderedProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [LaundryReceivedItem] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [LaundryReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [LaundryReturnedItem] xx
	                INNER JOIN [LaundryReturned] yy ON yy.ReturnNo = xx.ReturnNo AND xx.IsInfectious = 0 AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[SRLaundryProcessType] = '00' AND b.[IsApproved] = 1 ";
            commandText += "AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";
            commandText += "AND b.[ProcessDate] = '" + processDate + "' ";

            if (!string.IsNullOrEmpty(roomId))
                commandText += "AND d.[FromRoomID] = '" + roomId + "' ";
            
            commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRItemUnit],f.[ItemName],c.[Notes] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC,a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingInfectiousWithReceivedDate(DateTime processDate, string serviceUnitId, string roomId, string type)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],
	                si.[ItemID],e.[ItemName] AS 'ItemName',si.[Qty],(si.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',si.[SRItemUnit],f.[ItemName] AS 'ItemUnit','' [Notes], CAST(1 AS BIT) AS 'IsInfectious' 
                FROM [LaunderedProcessItemInfectious] a 
                INNER JOIN [LaunderedProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [LaundryReceivedItemInfectious] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [LaundryReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [LaundrySortingProcess] s ON s.[ProcessNo] = a.[ProcessNo] AND s.[IsApproved] = 1 
                INNER JOIN [LaundrySortingProcessItem] si ON si.[TransactionNo] = s.[TransactionNo]
                INNER JOIN [Item] e ON e.[ItemID] = si.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = si.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [LaundryReturnedItem] xx
	                INNER JOIN [LaundryReturned] yy ON yy.ReturnNo = xx.ReturnNo AND xx.IsInfectious = 1 AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[SRLaundryProcessType] = '" + type + "' ";
            commandText += "AND b.[IsApproved] = 1 AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";
            commandText += "AND b.[ProcessDate] = '" + processDate + "' ";

            if (!string.IsNullOrEmpty(roomId))
                commandText += "AND d.[FromRoomID] = '" + roomId + "' ";

            commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],si.[ItemID],e.[ItemName],si.[Qty],si.[SRItemUnit],f.[ItemName] ";
            commandText += "HAVING (si.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC,a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string roomId)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRItemUnit],f.[ItemName] AS 'ItemUnit',c.[Notes], CAST(0 AS BIT) AS 'IsInfectious' 
                FROM [LaunderedProcessItem] a INNER JOIN [LaunderedProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [LaundryReceivedItem] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [LaundryReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [LaundryReturnedItem] xx
	                INNER JOIN [LaundryReturned] yy ON yy.ReturnNo = xx.ReturnNo AND xx.IsInfectious = 0 AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[SRLaundryProcessType] = '00' AND b.[IsApproved] = 1 ";
            commandText += "AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            if (!string.IsNullOrEmpty(roomId))
                commandText += "AND d.[FromRoomID] = '" + roomId + "' ";
            commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRItemUnit],f.[ItemName],c.[Notes] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC,a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingInfectiousWithoutReceivedDate(string serviceUnitId, string roomId, string type)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],
	                si.[ItemID],e.[ItemName] AS 'ItemName',si.[Qty],(si.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',si.[SRItemUnit],f.[ItemName] AS 'ItemUnit','' [Notes], CAST(1 AS BIT) AS 'IsInfectious' 
                FROM [LaunderedProcessItemInfectious] a 
                INNER JOIN [LaunderedProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [LaundryReceivedItemInfectious] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [LaundryReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [LaundrySortingProcess] s ON s.[ProcessNo] = a.[ProcessNo] AND s.[IsApproved] = 1 
                INNER JOIN [LaundrySortingProcessItem] si ON si.[TransactionNo] = s.[TransactionNo]
                INNER JOIN [Item] e ON e.[ItemID] = si.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = si.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [LaundryReturnedItem] xx
	                INNER JOIN [LaundryReturned] yy ON yy.ReturnNo = xx.ReturnNo AND xx.IsInfectious = 1 AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[SRLaundryProcessType] = '" + type + "' ";
            commandText += "AND b.[IsApproved] = 1 AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            if (!string.IsNullOrEmpty(roomId))
                commandText += "AND d.[FromRoomID] = '" + roomId + "' ";
            commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],si.[ItemID],e.[ItemName],si.[Qty],si.[SRItemUnit],f.[ItemName] ";
            commandText += "HAVING (si.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC,a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemReturned(string processNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.ItemID, xx.Qty, xx.IsInfectious 
	            FROM [LaundryReturnedItem] xx
	            INNER JOIN [LaundryReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0 ";

            commandText += "WHERE xx.[ProcessNo] = '" + processNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
