using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsReturnedItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime processDate, string serviceUnitId, string roomId, string senderId, bool isUsingPackaging)
        {
            esParameters par = new esParameters();

            var commandText = string.Empty;
            if (!isUsingPackaging)
            {
                commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],CAST(c.CssdItemNo AS VARCHAR) AS 'ItemNo',
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',a.[Weight],c.[Notes] 
                FROM [CssdSterilizationProcessItem] a INNER JOIN [CssdSterilizationProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [CssdSterileItemsReceivedItem] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [CssdSterileItemsReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRCssdItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [CssdSterileItemsReturnedItem] xx
	                INNER JOIN [CssdSterileItemsReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[IsApproved] = 1 ";
                commandText += "AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";
                commandText += "AND b.[ProcessDate] = '" + processDate + "' ";

                if (!string.IsNullOrEmpty(roomId))
                    commandText += "AND d.[FromRoomID] = '" + roomId + "' ";
                if (!string.IsNullOrEmpty(senderId))
                    commandText += "AND d.[SenderByID] = '" + senderId + "' ";
                commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRCssdItemUnit],f.[ItemName],a.[Weight],c.[Notes] ";
                commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY c.[CssdItemNo] ASC ";
            }
            else
            {
                commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],CAST(c.CssdItemNo AS VARCHAR) AS 'ItemNo',
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRItemUnit] AS 'SRCssdItemUnit',f.[ItemName] AS 'CssdItemUnit',a.[Weight],c.[Notes] 
                FROM [CssdSterilizationProcessItem] a INNER JOIN [CssdSterilizationProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [CssdPackagingItem] c ON (c.[TransactionNo] = a.[ReceivedNo] AND c.[SeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [CssdPackaging] d ON d.[TransactionNo] = c.[TransactionNo] 
                INNER JOIN [CssdSterileItemsReceived] AS i ON i.[ReceivedNo] = d.[ReceivedNo]
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [CssdSterileItemsReturnedItem] xx
	                INNER JOIN [CssdSterileItemsReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[IsApproved] = 1 ";
                commandText += "AND i.[FromServiceUnitID] = '" + serviceUnitId + "' ";
                commandText += "AND b.[ProcessDate] = '" + processDate + "' ";

                if (!string.IsNullOrEmpty(roomId))
                    commandText += "AND i.[FromRoomID] = '" + roomId + "' ";
                if (!string.IsNullOrEmpty(senderId))
                    commandText += "AND i.[SenderByID] = '" + senderId + "' ";
                commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRItemUnit],f.[ItemName],a.[Weight],c.[Notes] ";
                commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY c.[CssdItemNo] ASC ";
            }

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string roomId, string senderId, bool isUsingPackaging)
        {
            esParameters par = new esParameters();

            var commandText = string.Empty;
            if (!isUsingPackaging)
            {
                commandText =
                  @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],CAST(c.CssdItemNo AS VARCHAR) AS 'ItemNo',
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',a.[Weight],c.[Notes] 
                FROM [CssdSterilizationProcessItem] a INNER JOIN [CssdSterilizationProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [CssdSterileItemsReceivedItem] c ON (c.[ReceivedNo] = a.[ReceivedNo] AND c.[ReceivedSeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [CssdSterileItemsReceived] d ON d.[ReceivedNo] = c.[ReceivedNo] 
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRCssdItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [CssdSterileItemsReturnedItem] xx
	                INNER JOIN [CssdSterileItemsReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[IsApproved] = 1 ";
                commandText += "AND d.[FromServiceUnitID] = '" + serviceUnitId + "' ";

                if (!string.IsNullOrEmpty(roomId))
                    commandText += "AND d.[FromRoomID] = '" + roomId + "' ";
                if (!string.IsNullOrEmpty(senderId))
                    commandText += "AND d.[SenderByID] = '" + senderId + "' ";
                commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRCssdItemUnit],f.[ItemName],a.[Weight],c.[Notes] ";
                commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY c.[CssdItemNo] ASC ";
            }
            else
            {
                commandText =
                @"SELECT a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],CAST(c.CssdItemNo AS VARCHAR) AS 'ItemNo',
                c.[ItemID],e.[ItemName] AS 'ItemName',a.[Qty],(a.Qty - SUM(ISNULL(g.Qty, 0))) AS 'QtyReturned',c.[SRItemUnit] AS 'SRCssdItemUnit',f.[ItemName] AS 'CssdItemUnit',a.[Weight],c.[Notes] 
                FROM [CssdSterilizationProcessItem] a INNER JOIN [CssdSterilizationProcess] b ON b.[ProcessNo] = a.[ProcessNo] 
                INNER JOIN [CssdPackagingItem] c ON (c.[TransactionNo] = a.[ReceivedNo] AND c.[SeqNo] = a.[ReceivedSeqNo]) 
                INNER JOIN [CssdPackaging] d ON d.[TransactionNo] = c.[TransactionNo] 
                INNER JOIN [CssdSterileItemsReceived] AS i ON i.[ReceivedNo] = d.[ReceivedNo]
                INNER JOIN [Item] e ON e.[ItemID] = c.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = c.[SRItemUnit] AND f.[StandardReferenceID] = 'ItemUnit') 
                LEFT JOIN 
                (
	                SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	                FROM [CssdSterileItemsReturnedItem] xx
	                INNER JOIN [CssdSterileItemsReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0
	                ) g ON (a.[ProcessNo] = g.[ProcessNo] AND a.[ProcessSeqNo] = g.[ProcessSeqNo]) 
                WHERE b.[IsApproved] = 1 ";
                commandText += "AND i.[FromServiceUnitID] = '" + serviceUnitId + "' ";

                if (!string.IsNullOrEmpty(roomId))
                    commandText += "AND i.[FromRoomID] = '" + roomId + "' ";
                if (!string.IsNullOrEmpty(senderId))
                    commandText += "AND i.[SenderByID] = '" + senderId + "' ";
                commandText += "GROUP BY a.[ProcessNo],a.[ProcessSeqNo],a.[ReceivedNo],a.[ReceivedSeqNo],c.[CssdItemNo],c.[ItemID],e.[ItemName],a.[Qty],c.[SRItemUnit],f.[ItemName],a.[Weight],c.[Notes] ";
                commandText += "HAVING (a.Qty - SUM(ISNULL(g.Qty, 0)) > 0) ORDER BY c.[CssdItemNo] ASC ";
            }
            
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemReturned(string processNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.ReturnNo, xx.ReturnSeqNo, xx.ProcessNo, xx.ProcessSeqNo, xx.Qty
	            FROM [CssdSterileItemsReturnedItem] xx
	            INNER JOIN [CssdSterileItemsReturned] yy ON yy.ReturnNo = xx.ReturnNo AND yy.IsVoid = 0 ";

            commandText += "WHERE xx.[ProcessNo] = '" + processNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
