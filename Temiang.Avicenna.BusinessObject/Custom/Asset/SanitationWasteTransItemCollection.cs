using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SanitationWasteTransItemCollection
    {
        public DataTable GetSanitationWasteTransItemReceived(DateTime? sDate, DateTime? eDate)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT swt.TransactionDate, swti.TransactionNo, swti.SRWasteType, asri.ItemName AS WasteTypeName, swti.Qty, (swti.Qty - ISNULL(aa.Qty, 0)) AS QtyDisposal
                FROM SanitationWasteTransItem AS swti
                INNER JOIN SanitationWasteTrans AS swt ON swt.TransactionNo = swti.TransactionNo
                LEFT JOIN 
                (
	                SELECT swti2.ReferenceNo AS TransactionNo, swti2.SRWasteType, SUM(swti2.Qty) AS Qty
	                FROM SanitationWasteTransItem AS swti2
	                INNER JOIN SanitationWasteTrans AS swt2 ON swt2.TransactionNo = swti2.TransactionNo
	                WHERE swt2.TransactionCode = 'D' AND swt2.IsVoid = 0
	                GROUP BY swti2.ReferenceNo, swti2.SRWasteType
                ) aa ON aa.TransactionNo = swti.TransactionNo AND aa.SRWasteType = swti.SRWasteType
                INNER JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'WasteType') asri ON asri.ItemID = swti.SRWasteType
                WHERE swt.TransactionDate >= '" + sDate.Value.ToString("MM/dd/yyyy") + "' AND swt.TransactionDate <= '" + eDate.Value.ToString("MM/dd/yyyy") + "' ";
            commandText += @"AND swt.TransactionCode = 'R' AND swt.IsApproved = 1
	            AND swti.Qty > ISNULL(aa.Qty, 0) ";

            commandText += "ORDER BY swti.SRWasteType, swti.TransactionNo ";

            this.es.Connection.CommandTimeout = 300;
            var dtb = FillDataTable(esQueryType.Text, commandText, par);

            return dtb;
        }
    }
}
