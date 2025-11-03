using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTransactionCollection
    {
        public DataTable ItemConsumptionMedic(DateTime startDate, DateTime endDate, string productType, string serviceUnitId, bool isCentralWarehouse, string unitPharmacyId, string unitPharmacyId2, string startPrefix, string endPrefix)
        {
            esParameters par = new esParameters();

            string commandText;

            if (isCentralWarehouse)
            {
                commandText =
                @"SELECT tx.ItemID, i.ItemName, SUM(tx.Qty) AS Qty, SUM(tx.OtherQty) AS OtherQty, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                FROM 
                (
	                SELECT tci.ItemID, 0 AS Qty, SUM(tci.ChargeQuantity) AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.IsPackage, 0) = 0 
		                AND ISNULL(tc.PackageReferenceNo, '') = '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductMedic ipm ON ipm.ItemID = tci.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tc.ToServiceUnitID NOT IN ('" + unitPharmacyId + @"', '" + unitPharmacyId2 + @"')
                        AND tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.ItemID, 0 AS Qty, SUM(tci.ChargeQuantity) AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.PackageReferenceNo, '') <> '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductMedic ipm ON  ipm.ItemID = tci.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tc.ToServiceUnitID NOT IN ('" + unitPharmacyId + @"', '" + unitPharmacyId2 + @"') 
                        AND tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.DetailItemID, 0 AS Qty, SUM(tci.QtyRealization) AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItemConsumption tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductMedic ipm ON ipm.ItemID = tci.DetailItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tc.ToServiceUnitID NOT IN ('" + unitPharmacyId + @"', '" + unitPharmacyId2 + @"') 
                        AND tci.DetailItemID >= '" + startPrefix + @"' AND tci.DetailItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.DetailItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tpi.ItemID, SUM(tpi.ResultQty) AS Qty, 0 AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransPrescriptionItem tpi 
	                INNER JOIN TransPrescription tp ON tp.PrescriptionNo = tpi.PrescriptionNo 
		                AND tp.IsApproval = 1 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductMedic ipm ON  ipm.ItemID = tpi.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tp.ServiceUnitID = '" + unitPharmacyId + @"' 
                        AND tpi.ItemID >= '" + startPrefix + @"' AND tpi.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tpi.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT iti.ItemID, 0 AS Qty, SUM(iti.Quantity * iti.ConversionFactor) AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM ItemTransactionItem iti 
	                INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
		                AND it.TransactionCode = '082' 
		                AND it.IsApproved = 1 
	                INNER JOIN ItemProductMedic ipm ON ipm.ItemID = iti.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND it.FromServiceUnitID NOT IN ('" + unitPharmacyId + @"', '" + unitPharmacyId2 + @"') 
                        AND iti.ItemID >= '" + startPrefix + @"' AND iti.ItemID <= '" + endPrefix + @"' 
	                GROUP BY iti.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
                    UNION ALL 
	                SELECT iti.ItemID, SUM(iti.Quantity * iti.ConversionFactor) AS Qty, 0 AS OtherQty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM ItemTransactionItem iti 
	                INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
		                AND it.TransactionCode = '082' 
		                AND it.IsApproved = 1 
	                INNER JOIN ItemProductMedic ipm ON ipm.ItemID = iti.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND it.FromServiceUnitID = '" + unitPharmacyId + @"' 
                        AND iti.ItemID >= '" + startPrefix + @"' AND iti.ItemID <= '" + endPrefix + @"' 
	                GROUP BY iti.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
                ) tx 
                INNER JOIN Item AS i ON tx.ItemID = i.ItemID 
                GROUP BY tx.ItemID, i.ItemName, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                HAVING SUM(tx.Qty) + SUM(tx.OtherQty) > 0";
            }
            else
            {
                commandText =
                @"SELECT tx.ItemID, i.ItemName, SUM(tx.Qty) AS Qty, 0 AS OtherQty, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                FROM 
                (
	                SELECT tpi.ItemID, SUM(tpi.ResultQty) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransPrescriptionItem tpi 
	                INNER JOIN TransPrescription tp ON tp.PrescriptionNo = tpi.PrescriptionNo 
		                AND tp.IsApproval = 1 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductMedic ipm ON  ipm.ItemID = tpi.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tp.ServiceUnitID = '" + serviceUnitId + @"' 
                        AND tpi.ItemID >= '" + startPrefix + @"' AND tpi.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tpi.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT iti.ItemID, SUM(iti.Quantity * iti.ConversionFactor) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM ItemTransactionItem iti 
	                INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
		                AND it.TransactionCode = '082' 
		                AND it.IsApproved = 1 
	                INNER JOIN ItemProductMedic ipm ON ipm.ItemID = iti.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND it.FromServiceUnitID = '" + serviceUnitId + @"'  
                        AND iti.ItemID >= '" + startPrefix + @"' AND iti.ItemID <= '" + endPrefix + @"' 
	                GROUP BY iti.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
                ) tx 
                INNER JOIN Item AS i ON tx.ItemID = i.ItemID 
                GROUP BY tx.ItemID, i.ItemName, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                HAVING SUM(tx.Qty) > 0";
            }
            
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemConsumptionNonMedic(DateTime startDate, DateTime endDate, string productType, string startPrefix, string endPrefix)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT tx.ItemID, i.ItemName, SUM(tx.Qty) AS Qty, 0 AS OtherQty, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                FROM 
                (
	                SELECT tci.ItemID, SUM(tci.ChargeQuantity) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.IsPackage, 0) = 0 
		                AND ISNULL(tc.PackageReferenceNo, '') = '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductNonMedic ipm ON ipm.ItemID = tci.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.ItemID, SUM(tci.ChargeQuantity) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.PackageReferenceNo, '') <> '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductNonMedic ipm ON  ipm.ItemID = tci.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.DetailItemID, SUM(tci.QtyRealization) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItemConsumption tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductNonMedic ipm ON ipm.ItemID = tci.DetailItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tci.DetailItemID >= '" + startPrefix + @"' AND tci.DetailItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.DetailItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tpi.ItemID, SUM(tpi.ResultQty) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransPrescriptionItem tpi 
	                INNER JOIN TransPrescription tp ON tp.PrescriptionNo = tpi.PrescriptionNo 
		                AND tp.IsApproval = 1 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemProductNonMedic ipm ON  ipm.ItemID = tpi.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND tpi.ItemID >= '" + startPrefix + @"' AND tpi.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tpi.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT iti.ItemID, SUM(iti.Quantity * iti.ConversionFactor) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM ItemTransactionItem iti 
	                INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
		                AND it.TransactionCode = '082' 
		                AND it.IsApproved = 1 
	                INNER JOIN ItemProductNonMedic ipm ON ipm.ItemID = iti.ItemID 
	                WHERE ipm.SRProductType LIKE '" + productType + @"' + '%' 
                        AND iti.ItemID >= '" + startPrefix + @"' AND iti.ItemID <= '" + endPrefix + @"' 
	                GROUP BY iti.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
                ) tx 
                INNER JOIN Item AS i ON tx.ItemID = i.ItemID 
                GROUP BY tx.ItemID, i.ItemName, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                HAVING SUM(tx.Qty) > 0";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable ItemConsumptionKitchen(DateTime startDate, DateTime endDate, string productType, string startPrefix, string endPrefix)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT tx.ItemID, i.ItemName, SUM(tx.Qty) AS Qty, 0 AS OtherQty, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                FROM 
                (
	                SELECT tci.ItemID, SUM(tci.ChargeQuantity) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.IsPackage, 0) = 0 
		                AND ISNULL(tc.PackageReferenceNo, '') = '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemKitchen ipm ON ipm.ItemID = tci.ItemID 
                    WHERE tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.ItemID, SUM(tci.ChargeQuantity) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItem tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND ISNULL(tc.PackageReferenceNo, '') <> '' 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemKitchen ipm ON  ipm.ItemID = tci.ItemID 
                    WHERE tci.ItemID >= '" + startPrefix + @"' AND tci.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tci.DetailItemID, SUM(tci.QtyRealization) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransChargesItemConsumption tci 
	                INNER JOIN TransCharges tc ON tc.TransactionNo = tci.TransactionNo 
		                AND tc.IsBillProceed = 1 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tci.LastUpdateDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemKitchen ipm ON ipm.ItemID = tci.DetailItemID 
                    WHERE tci.DetailItemID >= '" + startPrefix + @"' AND tci.DetailItemID <= '" + endPrefix + @"' 
	                GROUP BY tci.DetailItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT tpi.ItemID, SUM(tpi.ResultQty) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM TransPrescriptionItem tpi 
	                INNER JOIN TransPrescription tp ON tp.PrescriptionNo = tpi.PrescriptionNo 
		                AND tp.IsApproval = 1 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), tp.ApprovalDateTime, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
	                INNER JOIN ItemKitchen ipm ON  ipm.ItemID = tpi.ItemID 
                    WHERE tpi.ItemID >= '" + startPrefix + @"' AND tpi.ItemID <= '" + endPrefix + @"' 
	                GROUP BY tpi.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                UNION ALL 
	                SELECT iti.ItemID, SUM(iti.Quantity * iti.ConversionFactor) AS Qty, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
	                FROM ItemTransactionItem iti 
	                INNER JOIN ItemTransaction it ON it.TransactionNo = iti.TransactionNo 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) >= CONVERT(VARCHAR(10), CAST('" + startDate + @"' AS DATETIME), 112) 
		                AND CONVERT(VARCHAR(10), it.TransactionDate, 112) <= CONVERT(VARCHAR(10), CAST('" + endDate + @"' AS DATETIME), 112) 
		                AND it.TransactionCode = '082' 
		                AND it.IsApproved = 1 
	                INNER JOIN ItemKitchen ipm ON ipm.ItemID = iti.ItemID 
                    WHERE iti.ItemID >= '" + startPrefix + @"' AND iti.ItemID <= '" + endPrefix + @"' 
	                GROUP BY iti.ItemID, ipm.SRItemUnit, ipm.SRPurchaseUnit, ipm.ConversionFactor 
                ) tx 
                INNER JOIN Item AS i ON tx.ItemID = i.ItemID 
                GROUP BY tx.ItemID, i.ItemName, tx.SRItemUnit, tx.SRPurchaseUnit, tx.ConversionFactor 
                HAVING SUM(tx.Qty) > 0";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
