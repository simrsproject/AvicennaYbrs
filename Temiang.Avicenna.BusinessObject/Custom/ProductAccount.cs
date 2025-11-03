using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ProductAccountCollection
    {
        public DataTable GetProductAccountServiceUnitMapping(string serviceUnitID)
        {
            var par = new esParameters();

            string commandText = @" SELECT pa.ProductAccountID, 
	                                    pa.ProductAccountName, 
	                                    asri.ItemID AS SRRegistrationType,
	                                    asri.ItemName AS RegistrationTypeName,
	                                    NULL AS ChartOfAccountIdIncome,
	                                    NULL AS SubledgerIdIncome,
	                                    NULL AS ChartOfAccountIdAccrual,
	                                    NULL AS SubledgerIdAccrual,
	                                    NULL AS ChartOfAccountIdDiscount,
	                                    NULL AS SubledgerIdDiscount,
	                                    NULL AS ChartOfAccountIdInventory,
	                                    NULL AS SubledgerIdInventory,
	                                    NULL AS ChartOfAccountIdCOGS,
	                                    NULL AS SubledgerIdCOGS,
	                                    NULL AS ChartOfAccountIdExpense,
	                                    NULL AS SubledgerIdExpense
                                    FROM ProductAccount AS pa
                                    CROSS JOIN (SELECT asri.* 
                                                FROM AppStandardReferenceItem AS asri 
                                                INNER JOIN ServiceUnit AS su
                                                    ON CASE WHEN ISNULL(su.SRRegistrationType, '') = '' THEN 'OPR' WHEN ISNULL(su.SRRegistrationType, '') = 'MCU' THEN 'OPR' ELSE su.SRRegistrationType  END = asri.ItemID 
                                                    AND su.ServiceUnitID = '" + serviceUnitID + @"'
                                                WHERE asri.StandardReferenceID = 'RegistrationType' 
				                                    AND asri.IsActive = 1) asri
                                    WHERE pa.IsActive = 1
                                    ORDER BY pa.ProductAccountID, asri.ItemID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}