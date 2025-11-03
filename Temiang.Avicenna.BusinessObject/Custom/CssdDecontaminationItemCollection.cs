using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdDecontaminationItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDate, string serviceUnitId, string phase)
        {
            esParameters par = new esParameters();

            var commandText = string.Empty;
            commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[Qty] AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (phase == "1")
                commandText += "AND ISNULL(a.[IsDecontamination], 0) = 0 ";
            else if (phase == "2")
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '1' ";
            else if (phase == "3")
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '2' ";

            commandText += "AND b.[ReceivedDate] = '" + receiveDate + "' ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "AND b.[SRInstrumentType] = 'D' ";

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string phase)
        {
            esParameters par = new esParameters();

            var commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo  AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[Qty] AS 'QtyProcessed',a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',0 AS 'Weight',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 ";

            if (phase == "1")
                commandText += "AND ISNULL(a.[IsDecontamination], 0) = 0 ";
            else if (phase == "2")
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '1' ";
            else if (phase == "3")
                commandText += "AND a.[IsDecontamination] = 1 AND a.[SRDecontaminationPhase] = '2' ";

            if (!string.IsNullOrEmpty(serviceUnitId))
                commandText += "AND b.[FromServiceUnitID] = '" + serviceUnitId + "' ";

            commandText += "AND b.[SRInstrumentType] = 'D' ";

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemDecontaminationStatus(string decontaminationNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT a.ReceivedNo, a.ReceivedSeqNo, b.ItemID, i.ItemName, ISNULL(b.IsDecontamination, 0) AS IsDecontamination, ISNULL(b.SRDecontaminationPhase, '') AS SRDecontaminationPhase, ISNULL(b.IsFeasibilityTest, 0) AS IsFeasibilityTest 
                FROM CssdDecontaminationItem a
                INNER JOIN CssdSterileItemsReceivedItem AS b ON b.ReceivedNo = a.ReceivedNo AND b.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN Item AS i ON i.ItemID = b.ItemID ";

            commandText += "WHERE a.DecontaminationNo = '" + decontaminationNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemProceed(string receivedNo, string receivedSeqNo, string phase)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.DecontaminationNo, xx.DecontaminationSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, aa.Qty, aa.ItemID, i.ItemName 
	            FROM [CssdDecontaminationItem] xx
	            INNER JOIN [CssdDecontamination] yy ON yy.DecontaminationNo = xx.DecontaminationNo AND yy.IsVoid = 0 
                INNER JOIN [CssdSterileItemsReceivedItem] aa ON aa.ReceivedNo = xx.ReceivedNo AND aa.ReceivedSeqNo = xx.ReceivedSeqNo 
	            INNER JOIN [Item] i ON i.ItemID = aa.ItemID ";

            commandText += "WHERE xx.[ReceivedNo] = '" + receivedNo + "' ";
            if (!string.IsNullOrEmpty(receivedSeqNo))
                commandText += "AND xx.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";
            commandText += "AND yy.[SRDecontaminationPhase] = '" + phase + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
