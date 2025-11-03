using System.Data;
using System.Collections;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SupplierItemCollection
    {
        public DataTable GetLastData(string SupplierID)
        {
            esParameters par = new esParameters();
            par.Add("SupplierID", SupplierID);

            string commandText =
                @"SELECT SupplierID, ItemID FROM(
	                SELECT SupplierID, ItemID, ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY si.LastUpdateDateTime DESC) AS rn
	                FROM SupplierItem AS si
                    WHERE si.SupplierID = @SupplierID
                ) x WHERE x.rn = 1 /*UNION ALL select 'xxxxx','xxxxx'*/ ";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
