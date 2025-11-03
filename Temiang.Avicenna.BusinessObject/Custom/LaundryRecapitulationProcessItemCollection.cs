using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryRecapitulationProcessItemCollection
    {
        public DataTable GetLaundryRecapitulationItem(DateTime? date)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT i.ItemID, i.ItemName, ISNULL(p1.Qty, 0) + ISNULL(p2.Qty, 0) AS Qty, ISNULL(p3.Qty, 0) AS QtyRewashing, ipnm.SRItemUnit, asri.ItemName AS ItemUnit
                FROM Item AS i WITH (NOLOCK)
                INNER JOIN ItemProductNonMedic AS ipnm WITH (NOLOCK) ON ipnm.ItemID = i.ItemID
                INNER JOIN AppStandardReferenceItem AS asri ON asri.StandardReferenceID = 'ItemUnit' AND asri.ItemID = ipnm.SRItemUnit
                LEFT JOIN (
	                SELECT pr.ItemID, SUM(pr.Qty) AS Qty 
	                FROM LaunderedProcessItemCentralization AS pr WITH (NOLOCK)
	                INNER JOIN LaunderedProcess AS lp WITH (NOLOCK) ON lp.ProcessNo = pr.ProcessNo
	                WHERE lp.ProcessDate = '" + date.Value.ToString("MM/dd/yyyy") + "' ";
            commandText += @"AND lp.IsApproved = 1
                    GROUP BY pr.ItemID) p1 ON p1.ItemID = i.ItemID
                LEFT JOIN (
	                SELECT pr.ItemID, SUM(pr.Qty) AS Qty  
	                FROM LaundrySortingProcessItem AS pr WITH (NOLOCK)
	                INNER JOIN LaundrySortingProcess AS lsp WITH (NOLOCK) ON lsp.TransactionNo = pr.TransactionNo
	                WHERE lsp.TransactionDate = '" + date.Value.ToString("MM/dd/yyyy") + "' ";
            commandText += @"AND lsp.IsApproved = 1
	                GROUP BY pr.ItemID) p2 ON p2.ItemID = i.ItemID
                LEFT JOIN (
	                SELECT pr.ItemID, SUM(pr.Qty) AS Qty  
	                FROM LaunderedProcessItemRewashing AS pr WITH (NOLOCK)
	                INNER JOIN LaunderedProcess AS lp WITH (NOLOCK) ON lp.ProcessNo = pr.ProcessNo
	                WHERE lp.ProcessDate = '" + date.Value.ToString("MM/dd/yyyy") + "' ";
            commandText += @"AND lp.IsApproved = 1
	                GROUP BY pr.ItemID) p3 ON p3.ItemID = i.ItemID
                WHERE i.IsActive = 1 AND ipnm.IsNeedToBeLaundered = 1 ";

            commandText += "ORDER BY i.ItemID ";

            this.es.Connection.CommandTimeout = 300;
            var dtb = FillDataTable(esQueryType.Text, commandText, par);

            return dtb;
        }
    }
}
