using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaunderedProcessItemInfectiousCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDateFrom, DateTime receiveDateTo, string serviceUnitId)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],(a.Qty - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRItemUnit],f.[ItemName] AS 'ItemUnit',a.[Notes] 
                FROM [LaundryReceivedItemInfectious] a 
                INNER JOIN [LaundryReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty 
	                FROM [LaunderedProcessItemInfectious] xx
	                INNER JOIN [LaunderedProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [ItemLinen] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            commandText += "AND b.[ReceivedDate] >= '" + receiveDateFrom + "' ";
            commandText += "AND b.[ReceivedDate] <= '" + receiveDateTo + "' ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "GROUP BY b.[ReceivedDate],g.[ServiceUnitName],h.[RoomName],a.[ReceivedNo],a.[ReceivedSeqNo],a.[ItemID],e.[ItemName],a.[Qty],a.[SRItemUnit],f.[ItemName],a.[Notes] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC, a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],(a.Qty - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRItemUnit],f.[ItemName] AS 'ItemUnit',a.[Notes] 
                FROM [LaundryReceivedItemInfectious] a 
                INNER JOIN [LaundryReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty 
	                FROM [LaunderedProcessItemInfectious] xx
	                INNER JOIN [LaunderedProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [ItemLinen] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "GROUP BY b.[ReceivedDate],g.[ServiceUnitName],h.[RoomName],a.[ReceivedNo],a.[ReceivedSeqNo],a.[ItemID],e.[ItemName],a.[Qty],a.[SRItemUnit],f.[ItemName],a.[Notes] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[ReceivedNo] ASC, a.[ReceivedSeqNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemProceed(string receivedNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty 
	            FROM [LaunderedProcessItemInfectious] xx
	            INNER JOIN [LaunderedProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0 ";

            commandText += "WHERE xx.[ReceivedNo] = '" + receivedNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetQtyProcessed(string processNo, string processSeqNo, string receivedNo, string receivedSeqNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT x.[ReceivedNo], x.[ReceivedSeqNo], SUM(x.[Qty]) AS Qty
                FROM [LaunderedProcessItemInfectious] x
                INNER JOIN [LaunderedProcess] y ON y.ProcessNo = x.ProcessNo AND y.IsVoid = 0 ";

            commandText += "WHERE x.[ProcessNo] + x.[ProcessSeqNo] <> '" + processNo + processSeqNo + "' ";
            commandText += "AND x.[ReceivedNo] = '" + receivedNo + "' AND x.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";
            commandText += "GROUP BY x.[ReceivedNo], x.[ReceivedSeqNo] ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
