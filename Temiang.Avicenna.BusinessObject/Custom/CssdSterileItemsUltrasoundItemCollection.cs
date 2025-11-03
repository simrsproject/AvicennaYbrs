using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CssdSterileItemsUltrasoundItemCollection
    {
        public DataTable ItemOutstandingWithReceivedDate(DateTime receiveDate, string serviceUnitId, string senderId, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination)
        {
            esParameters par = new esParameters();

            var commandText = 
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.TransactionNo, xx.TransactionSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo
	                FROM [CssdSterileItemsUltrasoundItem] xx
	                INNER JOIN  [CssdSterileItemsUltrasound] yy ON yy.TransactionNo = xx.TransactionNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 AND a.[IsNeedUltrasound] = 1 AND c.[TransactionSeqNo] IS NULL ";

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

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemOutstandingWithoutReceivedDate(string serviceUnitId, string senderId, bool isUsingPackaging, bool isUsingFeasibilityTest, bool isUsingDecontamination)
        {
            esParameters par = new esParameters();

            var commandText =
                @"SELECT b.[ReceivedDate],g.ServiceUnitName + (CASE WHEN h.RoomName IS NULL THEN '' ELSE ' [' + h.RoomName + ']' END) AS 'ServiceUnitName',
                a.[ReceivedNo],a.[ReceivedSeqNo],a.[CssdItemNo],CAST(a.CssdItemNo AS VARCHAR) AS 'ItemNo',a.[ItemID],e.[ItemName] AS 'ItemName',
                a.[Qty],a.[SRCssdItemUnit],f.[ItemName] AS 'CssdItemUnit',a.[Notes],a.[IsNeedUltrasound] 
                FROM [CssdSterileItemsReceivedItem] a 
                INNER JOIN [CssdSterileItemsReceived] b ON a.[ReceivedNo] = b.[ReceivedNo] 
                LEFT JOIN 
                (
	                SELECT xx.TransactionNo, xx.TransactionSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo
	                FROM [CssdSterileItemsUltrasoundItem] xx
	                INNER JOIN  [CssdSterileItemsUltrasound] yy ON yy.TransactionNo = xx.TransactionNo AND yy.IsVoid = 0
                ) c ON c.ReceivedNo = b.ReceivedNo AND c.ReceivedSeqNo = a.ReceivedSeqNo
                INNER JOIN [Item] e ON a.[ItemID] = e.[ItemID] 
                INNER JOIN [AppStandardReferenceItem] f ON (a.[SRCssdItemUnit] = f.[ItemID] AND f.[StandardReferenceID] = 'ItemUnit') 
                INNER JOIN [ServiceUnit] g ON b.[FromServiceUnitID] = g.[ServiceUnitID] 
                LEFT JOIN [ServiceRoom] h ON b.[FromRoomID] = h.[RoomID] 
                WHERE b.[IsApproved] = 1 AND a.[IsNeedUltrasound] = 1 AND c.[TransactionSeqNo] IS NULL ";

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

            commandText += "ORDER BY a.[CssdItemNo] ASC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetItemProceed(string receivedNo, string receivedSeqNo)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT xx.TransactionNo, xx.TransactionSeqNo, xx.ReceivedNo, xx.ReceivedSeqNo, aa.ItemID, i.ItemName 
                FROM [CssdSterileItemsUltrasoundItem] xx 
                INNER JOIN [CssdSterileItemsUltrasound] yy ON yy.TransactionNo = xx.TransactionNo AND yy.IsVoid = 0 
                INNER JOIN [CssdSterileItemsReceivedItem] aa ON aa.ReceivedNo = xx.ReceivedNo AND aa.ReceivedSeqNo = xx.ReceivedSeqNo 
	            INNER JOIN [Item] i ON i.ItemID = aa.ItemID ";

            commandText += "WHERE xx.[ReceivedNo] = '" + receivedNo + "' ";
            if (!string.IsNullOrEmpty(receivedSeqNo))
                commandText += "AND xx.[ReceivedSeqNo] = '" + receivedSeqNo + "' ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
