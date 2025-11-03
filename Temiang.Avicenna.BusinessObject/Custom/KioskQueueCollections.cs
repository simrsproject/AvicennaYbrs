using System.Data;
using System.Collections;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class KioskQueueCollection
    {
        public DataTable GetFirstLastData(string[] KioskQueueCodes)
        {
            string codes = string.Empty;
            foreach (var s in KioskQueueCodes) {
                codes += (codes.Length == 0 ? "" : ",") + "'" + s + "'";
            }
            esParameters par = new esParameters();

            string commandText =
                @"SELECT * FROM(
                    SELECT 
	                    *, ROW_NUMBER() OVER (PARTITION BY kq.KioskQueueCode ORDER BY kq.KioskQueueID asc) oasc,
	                    ROW_NUMBER() OVER (PARTITION BY kq.KioskQueueCode ORDER BY kq.KioskQueueID desc) odesc
                    FROM KioskQueue AS kq
                    WHERE kq.SRKioskQueueStatus = '01'
	                    AND kq.KioskQueueCode IN (" + codes + @") 
                    ) a WHERE a.oasc = 1 OR a.odesc = 1";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
