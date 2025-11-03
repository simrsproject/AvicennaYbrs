using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterilizationProcessItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDate, string serviceUnitId, string senderId, bool isDtt, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination)
        {
            esParameters par = new esParameters();

            var commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],(a.Qty - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty 
	                FROM [CssdSterilizationProcessItem] xx
	                INNER JOIN [CssdSterilizationProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (isDtt)
                commandText += "AND a.[IsDtt] = 1 ";
            else
                commandText += "AND a.[IsDtt] = 0 ";

            commandText += "AND b.[ReceivedDate] = '" + receiveDate + "' ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";
            if (!string.IsNullOrEmpty(senderId))
                commandText += "AND b.[SenderByID] = '" + senderId + "' ";
            if (isUsingPackaging)
                commandText += "AND a.[IsPackaging] = 1 ";
            else if (isUsingFeasibilityTest)
                commandText += "AND a.[IsFeasibilityTest] = 1 AND a.[IsFeasibilityTestPassed] = 1 ";
            else if (isUsingDecontamination)
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '3' ";

            commandText += "GROUP BY b.[ReceivedDate],g.[ServiceUnitName],h.[RoomName],a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],a.[ItemID],e.[ItemName],a.[Qty],a.[SRCssdItemUnit],f.[ItemName],a.[Notes],a.[IsNeedUltrasound] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string senderId, bool isDtt, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination)
        {
            esParameters par = new esParameters();

            var commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],(a.Qty - SUM(ISNULL(c.Qty, 0))) AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty 
	                FROM [CssdSterilizationProcessItem] xx
	                INNER JOIN [CssdSterilizationProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (isDtt)
                commandText += "AND a.[IsDtt] = 1 ";
            else
                commandText += "AND a.[IsDtt] = 0 ";
            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";
            if (!string.IsNullOrEmpty(senderId))
                commandText += "AND b.[SenderByID] = '" + senderId + "' ";
            if (isUsingPackaging)
                commandText += "AND a.[IsPackaging] = 1 ";
            else if (isUsingFeasibilityTest)
                commandText += "AND a.[IsFeasibilityTest] = 1 AND a.[IsFeasibilityTestPassed] = 1 ";
            else if (isUsingDecontamination)
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '3' ";

            commandText += "GROUP BY b.[ReceivedDate],g.[ServiceUnitName],h.[RoomName],a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],a.[ItemID],e.[ItemName],a.[Qty],a.[SRCssdItemUnit],f.[ItemName],a.[Notes],a.[IsNeedUltrasound] ";
            commandText += "HAVING (a.Qty - SUM(ISNULL(c.Qty, 0)) > 0) ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemProceed(string receivedNo, string receivedSeqNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.ProcessNo, xx.ProcessSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, xx.Qty, aa.ItemID, i.ItemName 
	            FROM [CssdSterilizationProcessItem] xx
	            INNER JOIN [CssdSterilizationProcess] yy ON yy.ProcessNo = xx.ProcessNo AND yy.IsVoid = 0 
                INNER JOIN [CssdSterileItemsReceivedItem] aa ON aa.ReceivedNo = xx.ReceivedNo AND aa.ReceivedSeqNo = xx.ReceivedSeqNo 
	            INNER JOIN [Item] i ON i.ItemID = aa.ItemID ";

            commandText += "WHERE xx.[ReceivedNo] = '" + receivedNo + "' ";
            if (!string.IsNullOrEmpty(receivedSeqNo))
                commandText += "AND xx.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetQtyProcessed(string processNo, string processSeqNo, string receivedNo, string receivedSeqNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT x.[ReceivedNo], x.[ReceivedSeqNo], SUM(x.[Qty]) AS Qty
                FROM [CssdSterilizationProcessItem] x
                INNER JOIN [CssdSterilizationProcess] y ON y.ProcessNo = x.ProcessNo AND y.IsVoid = 0 ";

            commandText += "WHERE x.[ProcessNo] + x.[ProcessSeqNo] <> '" + processNo + processSeqNo + "' ";
            commandText += "AND x.[ReceivedNo] = '" + receivedNo + "' AND x.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";
            commandText += "GROUP BY x.[ReceivedNo], x.[ReceivedSeqNo] ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
