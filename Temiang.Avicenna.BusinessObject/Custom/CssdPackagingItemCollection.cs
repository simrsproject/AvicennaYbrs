using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdPackagingItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDate, string serviceUnitId, string phase)
        {
            esParameters par = new esParameters();

            var commandText = string.Empty;
            commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[Qty] AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound],ISNULL(a.[IsFeasibilityTestPassed], 1) AS 'IsFeasibilityTestPassed',
                ISNULL(a.[IsBrokenInstrument], 0) AS 'IsBrokenInstrument',ISNULL(a.[QtyReplacements], 0) AS 'QtyReplacements',ISNULL(a.[IsPackaging], 0) AS 'IsPackaging',a.[ExpiredDate],a.[ReuseTo],a.[IsNeedUltrasound],
                a.[IsDtt],CASE WHEN a.[IsDtt] = 1 THEN 'High' ELSE 'Low' END AS 'DttDescription' 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (phase == "fts")
                commandText += "AND ISNULL(a.[IsFeasibilityTest], 0) = 1 AND ISNULL(a.[IsFeasibilityTestPassed], 0) = 1 ";
            else if (phase == "dec")
                commandText += "AND ((b.[SRInstrumentType] = 'D' AND ISNULL(a.[IsDecontamination], 0) = 1 AND ISNULL(a.[SRDecontaminationPhase], '') = '3') OR (b.[SRInstrumentType] = 'C')) ";

            commandText += "AND ISNULL(a.[IsPackaging], 0) = 0 ";
            commandText += "AND b.[ReceivedDate] = '" + receiveDate + "' ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string phase)
        {
            esParameters par = new esParameters();

            var commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[Qty] AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound],ISNULL(a.[IsFeasibilityTestPassed], 1) AS 'IsFeasibilityTestPassed',
                ISNULL(a.[IsBrokenInstrument], 0) AS 'IsBrokenInstrument',ISNULL(a.[QtyReplacements], 0) AS 'QtyReplacements',ISNULL(a.[IsPackaging], 0) AS 'IsPackaging',a.[ExpiredDate],a.[ReuseTo],a.[IsNeedUltrasound],
                a.[IsDtt],CASE WHEN a.[IsDtt] = 1 THEN 'High' ELSE 'Low' END AS 'DttDescription' 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (phase == "fts")
                commandText += "AND ISNULL(a.[IsFeasibilityTest], 0) = 1 AND ISNULL(a.[IsFeasibilityTestPassed], 0) = 1 ";
            else if (phase == "dec")
                commandText += "AND ((b.[SRInstrumentType] = 'D' AND ISNULL(a.[IsDecontamination], 0) = 1 AND ISNULL(a.[SRDecontaminationPhase], '') = '3') OR (b.[SRInstrumentType] = 'C')) ";

            commandText += "AND ISNULL(a.[IsPackaging], 0) = 0 ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetItemIsPackagingItemStatus(string transactionNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.ReceivedNo, a.ReceivedSeqNo, b.ItemID, i.ItemName, ISNULL(b.IsDecontamination, 0) AS IsDecontamination, ISNULL(b.SRDecontaminationPhase, '') AS SRDecontaminationPhase, ISNULL(b.IsFeasibilityTest, 0) AS IsFeasibilityTest, 
                ISNULL(b.IsFeasibilityTestPassed, 0) AS IsFeasibilityTestPassed, ISNULL(b.IsPackaging, 0) AS IsPackaging, ISNULL(b.IsSterilization, 0) AS IsSterilization 
                FROM CssdPackagingItem a
                INNER JOIN CssdSterileItemsReceivedItem AS b ON b.ReceivedNo = a.ReceivedNo AND b.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN Item AS i ON i.ItemID = b.ItemID ";

            commandText += "WHERE a.TransactionNo = '" + transactionNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemProceed(string receivedNo, string receivedSeqNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.TransactionNo, xx.SeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, aa.Qty, aa.ItemID, i.ItemName 
	            FROM [CssdPackagingItem] xx
	            INNER JOIN [CssdPackaging] yy ON yy.TransactionNo = xx.TransactionNo AND yy.IsVoid = 0 
                INNER JOIN [CssdSterileItemsReceivedItem] aa ON aa.ReceivedNo = xx.ReceivedNo AND aa.ReceivedSeqNo = xx.ReceivedSeqNo 
	            INNER JOIN [Item] i ON i.ItemID = aa.ItemID ";

            commandText += "WHERE xx.[ReceivedNo] = '" + receivedNo + "' ";
            if (!string.IsNullOrEmpty(receivedSeqNo))
                commandText += "AND xx.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
