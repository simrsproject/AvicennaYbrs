/************************************************************  
 * Code formatted by SoftTree SQL Assistant © v11.0.35  
 * Time: 9/20/2023 15:55:54  
 ************************************************************/  
  
ALTER VIEW [dbo].[vw_Transaction]   
AS                        
                        
SELECT tc.TransactionNo                   AS TransactionNo,  
       tc.TransactionDate                 AS TransactionDate,  
       tc.RegistrationNo,  
       tc.ToServiceUnitID                 AS ServiceUnitID,  
       tc.ReferenceNo,  
       tc.IsCorrection,  
       ISNULL(tcReff.ExecutionDate, tc.ExecutionDate) AS FilterDate,  
       ISNULL(tcReff.TransactionNo, tc.TransactionNo) AS OrderTransNo,  
       ISNULL(tcReff.TransactionDate, tc.TransactionDate) AS OrderDate,  
       tc.IsPackage,  
       ISNULL(tc.PackageReferenceNo, '')  AS PackageReferenceNo,  
       CAST(0 AS BIT)                     AS IsPrescription,  
       tc.ClassID  
FROM   TransCharges tc WITH(NOLOCK)  
       LEFT JOIN TransCharges tcReff WITH(NOLOCK)  
            ON  tc.ReferenceNo = tcReff.TransactionNo  
WHERE  tc.IsVoid = 0  
       AND tc.IsApproved = 1 /*AND tc.IsBillProceed = 1      */ -- di remark teguhs 171127 karena bermasalah di payment direct untuk transaksi yang belum realisasi      
                      
UNION ALL                        
                  
SELECT tp.PrescriptionNo        AS TransactionNo,  
       tp.PrescriptionDate      AS TransactionDate,  
       tp.RegistrationNo,  
       tp.ServiceUnitID         AS ServiceUnitID,  
       CASE   
            WHEN tpReff.PrescriptionNo IS NULL THEN ''  
            ELSE tp.ReferenceNo  
       END                      AS ReferenceNo,  
       tp.IsPrescriptionReturn  AS IsCorrection,  
       ISNULL(tp.ExecutionDate, tp.PrescriptionDate) AS FilterDate,  
       ISNULL(tpReff.PrescriptionNo, tp.PrescriptionNo) AS OrderTransNo,  
       ISNULL(tpReff.PrescriptionDate, tp.PrescriptionDate) AS OrderDate,  
       0                        AS IsPackage,  
       ''                       AS PackageReferenceNo,  
       CAST(1 AS BIT)           AS IsPrescription,  
       tp.ClassID  
FROM   TransPrescription tp WITH(NOLOCK)  
       LEFT JOIN (  
                SELECT ret.PrescriptionNo, ret.PrescriptionDate   
                FROM   TransPrescription ret WITH(NOLOCK)  
                WHERE  ret.IsPrescriptionReturn = 1  
            ) tpReff  
            ON  tp.ReferenceNo = tpReff.PrescriptionNo  
WHERE  tp.IsVoid = 0  
       AND tp.IsApproval = 1  
       AND tp.IsBillProceed = 1