using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System;
using System.Data;
using System.Linq;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItemCollection
    {
        public bool HasPackage
        {
            get
            {
                return (this.Where(t => (t.IsPackage ?? false)).Count() > 0);
            }
        }

        public void SetParentNoByTransactionNo()
        {
            foreach (var tci in this)
            {
                var prn = this.Where(x => x.TransactionNo == tci.TransactionNo && tci.ParentNo == x.SequenceNo);
                var val = prn.Any() ? prn.First().SequenceNo : "";
                tci.ParentNoByTransactionNo = val;
            }
        }

        public void SetPrevOrder(string RegistrationNo, int iHourInterval) {
            if (this.Count == 0) return;
            if (iHourInterval == 0) return;

            var TransNos = this.Select(tn => tn.TransactionNo).ToArray().Distinct();
            var tciCollPrev = new TransChargesItemCollection();
            var tciPrev = new TransChargesItemQuery("tci");
            var tcPrev = new TransChargesQuery("tc");
            tciPrev.InnerJoin(tcPrev).On(tciPrev.TransactionNo == tcPrev.TransactionNo)
                .Where(tcPrev.RegistrationNo == RegistrationNo,
                    /*tcPrev.IsOrder == true, */tcPrev.IsVoid == false,
                    tciPrev.IsVoid == false,
                    tcPrev.TransactionNo.NotIn(TransNos)
                );
            if (tciCollPrev.Load(tciPrev))
            {
                foreach (var tci in this)
                {
                    tci.SetPrevOrder(tciCollPrev, iHourInterval);

                    //var tciLastPrev = tciCollPrev.Where(t => t.ItemID == tci.ItemID).OrderBy(t => t.CreatedDateTime)
                    //    .FirstOrDefault();
                    //if (tciLastPrev != null) {
                    //    var hr = (tci.CreatedDateTime - tciLastPrev.CreatedDateTime).Value.Hours;
                    //    if (hr < 3) {
                    //        tci.PrevOrder = "Last trans < 3 hours";
                    //    }
                    //}
                }
            }
        }

        //        public DataTable LaboratoryResultByTransactionNo(string transactionNo)
        //        {
        //            esParameters par = new esParameters();

        //            string commandText = @"SELECT a.[TransactionNo],
        //       a.[SequenceNo],
        //       a.[ItemID],
        //       CASE 
        //            WHEN a.ParentNo != '' THEN REPLICATE('-', 3) + ' ' + e.ItemName
        //            ELSE e.ItemName
        //       END + ' [' + a.ItemID + ']' AS TestName,
        //       CASE 
        //            WHEN c.AgeInYear > 0 THEN c.AgeInYear
        //            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN c.AgeInMonth
        //            ELSE c.AgeInDay
        //       END AS Age,
        //       CASE 
        //            WHEN c.AgeInYear > 0 THEN 'Year'
        //            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN 'Month'
        //            ELSE 'Day'
        //       END AS AgeType,
        //       d.[Sex],
        //       a.[IsCito],
        //       COALESCE(a.[ResultValue], '') AS 'ResultValue',
        //       COALESCE(h.[ItemName], '') AS 'ItemUnit',
        //       COALESCE(a.[IsDuplo], CAST(0 AS BIT)) AS 'IsDuplo',
        //       COALESCE(g.[NormalValueMin], '') AS 'NormalValueMin',
        //       COALESCE(g.[NormalValueMax], '') AS 'NormalValueMax',
        //       --CAST(CASE WHEN a.ParentNo = '' THEN 0 ELSE 1 END AS BIT) AS IsResultInput,
        //       CAST(1 AS BIT) AS IsResultInput,
        //       i.[ItemGroupName],
        //       a.Notes, ISNULL(a.IsDescriptionResult, 0) AS IsDescriptionResult
        //FROM   [TransChargesItem] a
        //       INNER JOIN [TransCharges] b ON  b.[TransactionNo] = a.[TransactionNo]
        //       INNER JOIN [Registration] c ON  b.[RegistrationNo] = c.[RegistrationNo]
        //       INNER JOIN [Patient] d ON  c.[PatientID] = d.[PatientID]
        //       INNER JOIN [Item] e ON  a.[ItemID] = e.[ItemID]
        //       INNER JOIN [ItemLaboratory] f ON  f.[ItemID] = e.[ItemID]
        //       LEFT JOIN [ItemLaboratoryDetail] g ON  g.[ItemID] = e.[ItemID] 
        //			AND g.Sex IN (d.Sex, 'A')
        //            --AND g.SRAgeUnit = CASE WHEN c.AgeInYear > 0 THEN 'AgeUnit-004' 
        //			--					   WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN 'AgeUnit-003' 
        //			--					   ELSE 'AgeUnit-001' END 
        //			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) <= g.TotalAgeMax
        //			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) >= g.TotalAgeMin
        //       LEFT JOIN [AppStandardReferenceItem] h ON f.[SRLaboratoryUnit] = h.[ItemID] AND h.[StandardReferenceID] = 'LaboratoryUnit'
        //       LEFT JOIN [AppStandardReferenceItem] j ON g.SRAgeUnit = j.ItemID AND j.[StandardReferenceID] = 'AgeUnit'
        //       INNER JOIN [ItemGroup] i ON  e.[ItemGroupID] = i.[ItemGroupID]
        //WHERE  a.[TransactionNo] = '" + transactionNo + @"' AND a.IsVoid = 0
        //ORDER BY a.[SequenceNo] ASC";

        //            return FillDataTable(esQueryType.Text, commandText, par);
        //        }

        //        public DataTable LaboratoryResultByRegistrationNo(string registrationNo)
        //        {
        //            esParameters par = new esParameters();

        //            string commandText = @"SELECT a.[TransactionNo] AS OrderLabNo,
        //       a.[ItemID] AS LabOrderCode,
        //       CASE 
        //            WHEN a.ParentNo != '' THEN REPLICATE('-', 3) + ' ' + e.ItemName
        //            ELSE e.ItemName
        //       END + ' [' + a.ItemID + ']' AS LabOrderSummary,
        //       COALESCE(a.[ResultValue], '') AS 'Result',
        //       COALESCE(g.[NormalValueMin], '') + CASE WHEN COALESCE(g.[NormalValueMax], '') != '' THEN ' - ' + COALESCE(g.[NormalValueMax], '') ELSE '' END + ' ' + COALESCE(h.[ItemName], '') AS 'StandarValue',
        //       b.TransactionDate AS OrderLabTglOrder
        //FROM   [TransChargesItem] a
        //       INNER JOIN [TransCharges] b ON  b.[TransactionNo] = a.[TransactionNo]
        //       INNER JOIN [Registration] c ON  b.[RegistrationNo] = c.[RegistrationNo]
        //       INNER JOIN [Patient] d ON  c.[PatientID] = d.[PatientID]
        //       INNER JOIN [Item] e ON  a.[ItemID] = e.[ItemID]
        //       INNER JOIN [ItemLaboratory] f ON  f.[ItemID] = e.[ItemID]
        //       LEFT JOIN [ItemLaboratoryDetail] g ON  g.[ItemID] = e.[ItemID] 
        //			AND g.Sex IN (d.Sex, 'A')
        //            --AND g.SRAgeUnit = CASE WHEN c.AgeInYear > 0 THEN 'AgeUnit-004' 
        //			--					   WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN 'AgeUnit-003' 
        //			--					   ELSE 'AgeUnit-001' END 
        //			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) <= g.TotalAgeMax
        //			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) >= g.TotalAgeMin
        //       LEFT JOIN [AppStandardReferenceItem] h ON f.[SRLaboratoryUnit] = h.[ItemID] AND h.[StandardReferenceID] = 'LaboratoryUnit'
        //       LEFT JOIN [AppStandardReferenceItem] j ON g.SRAgeUnit = j.ItemID AND j.[StandardReferenceID] = 'AgeUnit'
        //       INNER JOIN [ItemGroup] i ON  e.[ItemGroupID] = i.[ItemGroupID]
        //WHERE  b.RegistrationNo = '" + registrationNo + @"' AND a.IsVoid = 0
        //ORDER BY a.[SequenceNo] ASC";

        //            return FillDataTable(esQueryType.Text, commandText, par);
        //        }

        public DataTable LaboratoryResultByTransactionNoRSSMCB(string transactionNo)
        {
            esParameters par = new esParameters();

            string commandText = @" IF OBJECT_ID('tempdb..#tmp_lab1') IS NOT NULL
    DROP TABLE #tmp_lab1
IF OBJECT_ID('tempdb..#tmp_lab2') IS NOT NULL
    DROP TABLE #tmp_lab2
IF OBJECT_ID('tempdb..#tmp_lab3') IS NOT NULL
    DROP TABLE #tmp_lab3
IF OBJECT_ID('tempdb..#tmp_lab4') IS NOT NULL
    DROP TABLE #tmp_lab4
IF OBJECT_ID('tempdb..#tmp_lab5') IS NOT NULL
    DROP TABLE #tmp_lab5
IF OBJECT_ID('tempdb..#tmp_trans1') IS NOT NULL
    DROP TABLE #tmp_trans1

SELECT  (SELECT tci2.ItemID 
        FROM TransChargesItem AS tci2 
        WHERE tci2.TransactionNo = tci.TransactionNo AND tci2.SequenceNo = tci.ParentNo) ParentItemID, 
        tci.* 
INTO ##tmp_lab1
FROM TransChargesItem AS tci
WHERE tci.TransactionNo IN (SELECT DISTINCT tci2.TransactionNo
                            FROM TransChargesItem AS tci2 
                            WHERE tci2.TransactionNo = '" + transactionNo + @"' AND tci2.IsApprove = 1 AND tci2.IsBillProceed = 1 AND tci2.IsOrderRealization = 1 AND tci2.ChargeQuantity > 0)

DECLARE @sequenceNo VARCHAR(MAX)
DECLARE c CURSOR FOR SELECT SequenceNo 
				 FROM ##tmp_lab1 tl1
                 WHERE ParentItemID IS NOT NULL
OPEN c
FETCH NEXT FROM c INTO @sequenceNo
WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE ##tmp_lab1
	SET IsApprove = (SELECT tl.IsApprove FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo),
		IsBillProceed = (SELECT tl.IsBillProceed FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo),
		IsOrderRealization = (SELECT tl.IsOrderRealization FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo)
	FROM ##tmp_lab1 tl1
	WHERE SequenceNo = @sequenceNo
	
	FETCH NEXT FROM c INTO @sequenceNo
END
CLOSE c
DEALLOCATE c

SELECT CASE WHEN (SELECT t1.ParentNo 
                  FROM ##tmp_lab1 t1 
                  WHERE t1.ItemID = t.ParentItemID) = '' THEN t.ParentNo
	ELSE (SELECT t1.ParentNo 
	      FROM ##tmp_lab1 t1 
	      WHERE t1.ItemID = t.ParentItemID) END SuperParentNo,
	t.* 
INTO ##tmp_lab2
FROM ##tmp_lab1 t
WHERE t.IsApprove = 1 AND t.IsBillProceed = 1 AND t.IsOrderRealization = 1

SELECT REPLICATE('0', 3 - LEN(CAST(ilp.DisplaySequence AS VARCHAR(MAX)))) + CAST(ilp.DisplaySequence AS VARCHAR(MAX)) AS DisplaySequence, 
	t.*
INTO ##tmp_lab3 
FROM ##tmp_lab2 t
LEFT JOIN ItemLaboratoryProfile AS ilp ON ilp.ParentItemID = t.ParentItemID AND ilp.DetailItemID = t.ItemID

SELECT (SELECT REPLICATE('0', 3 - LEN(CAST(t1.DisplaySequence AS VARCHAR(MAX)))) + CAST(t1.DisplaySequence AS VARCHAR(MAX)) 
        FROM ##tmp_lab3 AS t1 
        WHERE t1.TransactionNo = t.TransactionNo AND t.ParentNo = t1.SequenceNo) ParentDisplaySequence, t.*
INTO ##tmp_lab4
FROM ##tmp_lab3 t

SELECT CASE WHEN t.ParentDisplaySequence IS NOT NULL THEN t.SuperParentNo + t.ParentDisplaySequence + t.DisplaySequence
	ELSE ISNULL(t.SuperParentNo + t.DisplaySequence, t.SequenceNo) END SuperDisplaySequence, t.*
INTO ##tmp_lab5
FROM ##tmp_lab4 t

SELECT tci.TransactionNo, 
	tci.SequenceNo, 
	tci.ParentNo,
	tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) AS ChargeQuantity
INTO ##tmp_trans1
FROM Registration r 
INNER JOIN TransCharges tc ON r.RegistrationNo = tc.RegistrationNo AND tc.IsApproved = 1
INNER JOIN TransChargesItem tci ON tci.TransactionNo = tc.TransactionNo AND tci.ReferenceNo = ''
WHERE  tc.TransactionNo = '" + transactionNo + @"'
ORDER BY tci.SequenceNo

SET @sequenceNo = ''

DECLARE @transactionNo VARCHAR(MAX), @parentNo VARCHAR(MAX),
		@chargeQuantity NUMERIC(10, 2)
DECLARE c CURSOR FOR SELECT * 
                     FROM ##tmp_trans1
OPEN c
FETCH NEXT FROM c INTO @transactionNo, @sequenceNo, @parentNo, @chargeQuantity
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @parentNo != ''
	BEGIN
		UPDATE ##tmp_trans1
		SET ChargeQuantity = (SELECT b.ChargeQuantity 
		                      FROM ##tmp_trans1 b
		                      WHERE b.SequenceNo = @parentNo)
		WHERE SequenceNo = @sequenceNo		
	END
	FETCH NEXT FROM c INTO @transactionNo, @sequenceNo, @parentNo, @chargeQuantity
END
CLOSE c
DEALLOCATE c

SELECT 
       a.SuperDisplaySequence,
	   a.[TransactionNo],
       a.[SequenceNo],
       a.[ItemID],
       LEN(a.SequenceNo) / 3 AS [LEVEL], a.ParentNo,
       CASE 
            WHEN a.ParentNo != '' THEN REPLICATE('-', LEN(a.SequenceNo) - 3) + ' ' + e.ItemName
            ELSE e.ItemName
       END + ' [' + a.ItemID + ']' AS TestName,
       CASE 
            WHEN c.AgeInYear > 0 THEN c.AgeInYear
            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN c.AgeInMonth
            ELSE c.AgeInDay
       END AS Age,
       CASE 
            WHEN c.AgeInYear > 0 THEN 'Year'
            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN 'Month'
            ELSE 'Day'
       END AS AgeType,
       d.[Sex],
       a.[IsCito],
       COALESCE(a.[ResultValue], '') AS 'ResultValue',
       COALESCE(h.[ItemName], '') AS 'ItemUnit',
       COALESCE(a.[IsDuplo], CAST(0 AS BIT)) AS 'IsDuplo',
       COALESCE(g.[NormalValueMin], '') AS 'NormalValueMin',
       COALESCE(g.[NormalValueMax], '') AS 'NormalValueMax',
       CAST(1 AS BIT) AS IsResultInput,
       i.[ItemGroupName],
       a.Notes, ISNULL(a.IsDescriptionResult, 0) AS IsDescriptionResult
FROM   ##tmp_lab5 a
       INNER JOIN [TransCharges] b ON  b.[TransactionNo] = a.[TransactionNo]
       INNER JOIN [Registration] c ON  b.[RegistrationNo] = c.[RegistrationNo]
       INNER JOIN [Patient] d ON  c.[PatientID] = d.[PatientID]
       INNER JOIN [Item] e ON  a.[ItemID] = e.[ItemID]
       INNER JOIN [ItemLaboratory] f ON  f.[ItemID] = e.[ItemID]
       LEFT JOIN [ItemLaboratoryDetail] g ON  g.[ItemID] = e.[ItemID] 
			AND g.Sex IN (d.Sex, 'A')
            AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) >= g.TotalAgeMin
			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) <= g.TotalAgeMax
       LEFT JOIN [AppStandardReferenceItem] h ON f.[SRLaboratoryUnit] = h.[ItemID] AND h.[StandardReferenceID] = 'LaboratoryUnit'
       LEFT JOIN [AppStandardReferenceItem] j ON g.SRAgeUnit = j.ItemID AND j.[StandardReferenceID] = 'AgeUnit'
       INNER JOIN [ItemGroup] i ON  e.[ItemGroupID] = i.[ItemGroupID]
WHERE  a.[TransactionNo] = '" + transactionNo + @"' 
    AND a.TransactionNo + a.SequenceNo IN (SELECT tci.TransactionNo + tci.SequenceNo
	                                       FROM ##tmp_trans1 tci
	                                       WHERE ChargeQuantity > 0)
ORDER BY a.SuperDisplaySequence

DROP TABLE ##tmp_trans1
         
DROP TABLE ##tmp_lab1
DROP TABLE ##tmp_lab2
DROP TABLE ##tmp_lab3
DROP TABLE ##tmp_lab4
DROP TABLE ##tmp_lab5";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable LaboratoryResultByTransactionNo(string transactionNo)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT  (SELECT tci2.ItemID 
        FROM TransChargesItem AS tci2 
        WHERE tci2.TransactionNo = tci.TransactionNo AND tci2.SequenceNo = tci.ParentNo) ParentItemID, 
        tci.*, 
        (SELECT i.ItemGroupID 
         FROM Item i 
         WHERE i.ItemID = CASE WHEN (SELECT tci2.ItemID 
        	 						 FROM TransChargesItem AS tci2 
        	 						 WHERE tci2.TransactionNo = tci.TransactionNo 
        	 						 	 AND tci2.SequenceNo = tci.ParentNo) IS NULL THEN tci.ItemID 
        					ELSE (SELECT tci2.ItemID 
        							FROM TransChargesItem AS tci2 
        							WHERE tci2.TransactionNo = tci.TransactionNo 
        								AND tci2.SequenceNo = tci.ParentNo) 
                          END) AS ItemGroupID  
INTO ##tmp_lab1
FROM TransChargesItem AS tci
WHERE tci.TransactionNo IN (SELECT DISTINCT tci2.TransactionNo
                            FROM TransChargesItem AS tci2 
                            WHERE tci2.TransactionNo = '" + transactionNo + @"' AND tci2.IsApprove = 1 AND tci2.IsBillProceed = 1 AND tci2.IsOrderRealization = 1 AND tci2.ChargeQuantity > 0)

DECLARE @sequenceNo VARCHAR(MAX)
DECLARE c CURSOR FOR SELECT SequenceNo 
				 FROM ##tmp_lab1 tl1
                 WHERE ParentItemID IS NOT NULL
OPEN c
FETCH NEXT FROM c INTO @sequenceNo
WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE ##tmp_lab1
	SET IsApprove = (SELECT tl.IsApprove FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo),
		IsBillProceed = (SELECT tl.IsBillProceed FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo),
		IsOrderRealization = (SELECT tl.IsOrderRealization FROM ##tmp_lab1 tl WHERE tl.SequenceNo = tl1.ParentNo)
	FROM ##tmp_lab1 tl1
	WHERE SequenceNo = @sequenceNo
	
	FETCH NEXT FROM c INTO @sequenceNo
END
CLOSE c
DEALLOCATE c

SELECT CASE WHEN (SELECT t1.ParentNo 
                  FROM ##tmp_lab1 t1 
                  WHERE t1.ItemID = t.ParentItemID) = '' THEN t.ParentNo
	ELSE (SELECT t1.ParentNo 
	      FROM ##tmp_lab1 t1 
	      WHERE t1.ItemID = t.ParentItemID) END SuperParentNo,
	t.* 
INTO ##tmp_lab2
FROM ##tmp_lab1 t
WHERE t.IsApprove = 1 AND t.IsBillProceed = 1 AND t.IsOrderRealization = 1

SELECT REPLICATE('0', 3 - LEN(CAST(ilp.DisplaySequence AS VARCHAR(MAX)))) + CAST(ilp.DisplaySequence AS VARCHAR(MAX)) AS DisplaySequence, 
	t.*
INTO ##tmp_lab3 
FROM ##tmp_lab2 t
LEFT JOIN ItemLaboratoryProfile AS ilp ON ilp.ParentItemID = t.ParentItemID AND ilp.DetailItemID = t.ItemID

SELECT (SELECT REPLICATE('0', 3 - LEN(CAST(t1.DisplaySequence AS VARCHAR(MAX)))) + CAST(t1.DisplaySequence AS VARCHAR(MAX)) 
        FROM ##tmp_lab3 AS t1 
        WHERE t1.TransactionNo = t.TransactionNo AND t.ParentNo = t1.SequenceNo) ParentDisplaySequence, t.*
INTO ##tmp_lab4
FROM ##tmp_lab3 t

SELECT CASE WHEN t.ParentDisplaySequence IS NOT NULL THEN t.SuperParentNo + t.ParentDisplaySequence + t.DisplaySequence
	ELSE ISNULL(t.SuperParentNo + t.DisplaySequence, t.SequenceNo) END SuperDisplaySequence, t.*
INTO ##tmp_lab5
FROM ##tmp_lab4 t

SELECT tci.TransactionNo, 
	tci.SequenceNo, 
	tci.ParentNo,
	tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) AS ChargeQuantity
INTO ##tmp_trans1
FROM Registration r 
INNER JOIN TransCharges tc ON r.RegistrationNo = tc.RegistrationNo AND tc.IsApproved = 1
INNER JOIN TransChargesItem tci ON tci.TransactionNo = tc.TransactionNo AND tci.ReferenceNo = ''
WHERE  tc.TransactionNo = '" + transactionNo + @"'
ORDER BY tci.SequenceNo

SET @sequenceNo = ''

DECLARE @transactionNo VARCHAR(MAX), @parentNo VARCHAR(MAX),
		@chargeQuantity NUMERIC(10, 2)
DECLARE c CURSOR FOR SELECT * 
                     FROM ##tmp_trans1
OPEN c
FETCH NEXT FROM c INTO @transactionNo, @sequenceNo, @parentNo, @chargeQuantity
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @parentNo != ''
	BEGIN
		UPDATE ##tmp_trans1
		SET ChargeQuantity = (SELECT b.ChargeQuantity 
		                      FROM ##tmp_trans1 b
		                      WHERE b.SequenceNo = @parentNo)
		WHERE SequenceNo = @sequenceNo		
	END
	FETCH NEXT FROM c INTO @transactionNo, @sequenceNo, @parentNo, @chargeQuantity
END
CLOSE c
DEALLOCATE c

SELECT DISTINCT
       a.SuperDisplaySequence,
	   a.[TransactionNo],
       a.[SequenceNo],
       a.[ItemID],
       LEN(a.SequenceNo) / 3 AS [LEVEL], a.ParentNo,
       CASE 
            WHEN a.ParentNo != '' THEN REPLICATE('-', LEN(a.SequenceNo) - 3) + ' ' + e.ItemName
            ELSE e.ItemName
       END + ' [' + a.ItemID + ']' AS TestName,
       CASE 
            WHEN c.AgeInYear > 0 THEN c.AgeInYear
            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN c.AgeInMonth
            ELSE c.AgeInDay
       END AS Age,
       CASE 
            WHEN c.AgeInYear > 0 THEN 'Year'
            WHEN c.AgeInYear = 0 AND c.AgeInMonth > 0 THEN 'Month'
            ELSE 'Day'
       END AS AgeType,
       d.[Sex],
       a.[IsCito],
       COALESCE(a.[ResultValue], '') AS 'ResultValue',
       COALESCE(h.[ItemName], '') AS 'ItemUnit',
       COALESCE(a.[IsDuplo], CAST(0 AS BIT)) AS 'IsDuplo',
       COALESCE(g.[NormalValueMin], '') AS 'NormalValueMin',
       COALESCE(g.[NormalValueMax], '') AS 'NormalValueMax',
       CAST(1 AS BIT) AS IsResultInput,
       i.[ItemGroupName],
       a.Notes, ISNULL(a.IsDescriptionResult, 0) AS IsDescriptionResult,
       CAST(LEFT(a.[SequenceNo], 3) AS INT) LV1,
       CASE WHEN LEN(a.SequenceNo) / 3 = 1 THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 4, 3) AS INT) END LV2,
       CASE WHEN LEN(a.SequenceNo) / 3 IN (1, 2) THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 7, 3) AS INT) END LV3,
       CASE WHEN LEN(a.SequenceNo) / 3 IN (1, 2, 3) THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 11, 3) AS INT) END LV4,
       ilp.DisplaySequence
FROM   ##tmp_lab5 a
       INNER JOIN [TransCharges] b ON  b.[TransactionNo] = a.[TransactionNo]
       INNER JOIN [Registration] c ON  b.[RegistrationNo] = c.[RegistrationNo]
       INNER JOIN [Patient] d ON  c.[PatientID] = d.[PatientID]
       INNER JOIN [Item] e ON  a.[ItemID] = e.[ItemID]
       INNER JOIN [ItemLaboratory] f ON  f.[ItemID] = e.[ItemID]
       LEFT JOIN [ItemLaboratoryDetail] g ON  g.[ItemID] = e.[ItemID] 
			AND g.Sex IN (d.Sex, 'A')
            AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) >= g.TotalAgeMin
			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) <= g.TotalAgeMax
       LEFT JOIN [AppStandardReferenceItem] h ON f.[SRLaboratoryUnit] = h.[ItemID] AND h.[StandardReferenceID] = 'LaboratoryUnit'
       LEFT JOIN [AppStandardReferenceItem] j ON g.SRAgeUnit = j.ItemID AND j.[StandardReferenceID] = 'AgeUnit'
       INNER JOIN [ItemGroup] i ON  a.[ItemGroupID] = i.[ItemGroupID]
       LEFT JOIN ItemLaboratoryProfile AS ilp
       		ON ilp.ParentItemID = CASE WHEN a.ParentNo = '' THEN a.[ItemID] ELSE  
										(SELECT tci.ItemID FROM TransChargesItem AS tci
										 WHERE tci.TransactionNo = '" + transactionNo + @"' AND tci.SequenceNo = a.ParentNo)
								   END AND
				ilp.DetailItemID = CASE WHEN a.ParentNo != '' THEN a.[ItemID] ELSE  
										(SELECT tci.ItemID FROM TransChargesItem AS tci
										 WHERE tci.TransactionNo = '" + transactionNo + @"' AND tci.SequenceNo = a.ParentNo)
								   END 
WHERE  a.[TransactionNo] = '" + transactionNo + @"' 
    AND a.TransactionNo + a.SequenceNo IN (SELECT tci.TransactionNo + tci.SequenceNo
	                                       FROM ##tmp_trans1 tci
	                                       WHERE ChargeQuantity > 0) 
ORDER BY CAST(LEFT(a.[SequenceNo], 3) AS INT),
       CASE WHEN LEN(a.SequenceNo) / 3 = 1 THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 4, 3) AS INT) END,
       CASE WHEN LEN(a.SequenceNo) / 3 IN (1, 2) THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 7, 3) AS INT) END,
       CASE WHEN LEN(a.SequenceNo) / 3 IN (1, 2, 3) THEN NULL ELSE CAST(SUBSTRING(a.[SequenceNo], 11, 3) AS INT) END,
       ilp.DisplaySequence

DROP TABLE ##tmp_trans1
         
DROP TABLE ##tmp_lab1
DROP TABLE ##tmp_lab2
DROP TABLE ##tmp_lab3
DROP TABLE ##tmp_lab4
DROP TABLE ##tmp_lab5";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable LaboratoryResultByRegistrationNo(string registrationNo)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT a.[TransactionNo] AS OrderLabNo,
       a.[ItemID] AS LabOrderCode,
       CASE 
            WHEN a.ParentNo != '' THEN REPLICATE('-', 3) + ' ' + e.ItemName
            ELSE e.ItemName
       END + ' [' + a.ItemID + ']' AS LabOrderSummary,
       COALESCE(a.[ResultValue], '') AS 'Result',
       COALESCE(g.[NormalValueMin], '') + CASE WHEN COALESCE(g.[NormalValueMax], '') != '' THEN ' - ' + COALESCE(g.[NormalValueMax], '') ELSE '' END + ' ' + COALESCE(h.[ItemName], '') AS 'StandarValue',
       b.TransactionDate AS OrderLabTglOrder
FROM   [TransChargesItem] a
       INNER JOIN [TransCharges] b ON  b.[TransactionNo] = a.[TransactionNo]
       INNER JOIN [Registration] c ON  b.[RegistrationNo] = c.[RegistrationNo]
       INNER JOIN [Patient] d ON  c.[PatientID] = d.[PatientID]
       INNER JOIN [Item] e ON  a.[ItemID] = e.[ItemID]
       INNER JOIN [ItemLaboratory] f ON  f.[ItemID] = e.[ItemID]
       LEFT JOIN [ItemLaboratoryDetail] g ON  g.[ItemID] = e.[ItemID] 
			AND g.Sex IN (d.Sex, 'A')
            AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) >= g.TotalAgeMin
			AND DATEDIFF(DD, d.DateOfBirth, c.RegistrationDate) <= g.TotalAgeMax
       LEFT JOIN [AppStandardReferenceItem] h ON f.[SRLaboratoryUnit] = h.[ItemID] AND h.[StandardReferenceID] = 'LaboratoryUnit'
       LEFT JOIN [AppStandardReferenceItem] j ON g.SRAgeUnit = j.ItemID AND j.[StandardReferenceID] = 'AgeUnit'
       INNER JOIN [ItemGroup] i ON  e.[ItemGroupID] = i.[ItemGroupID]
WHERE  b.RegistrationNo = '" + registrationNo + @"' AND a.IsVoid = 0
ORDER BY a.[SequenceNo] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable TransChargesItemWithCorrection(EnumerableRowCollection<string> listTransactionNo)
        {
            esParameters par = new esParameters();

            string list = string.Empty;

            foreach (string transactionNo in listTransactionNo)
            {
                list += transactionNo + "','";
            }

            string commandText = @"SELECT tci.TransactionNo, 
	tci.SequenceNo,
	tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) AS ChargeQuantity
FROM Registration r 
INNER JOIN TransCharges tc ON r.RegistrationNo = tc.RegistrationNo AND tc.IsApproved = 1
INNER JOIN TransChargesItem tci ON tci.TransactionNo = tc.TransactionNo AND tci.ReferenceNo = ''
WHERE  tc.TransactionNo IN ('" + list + @"')
	AND tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) = 0
ORDER BY tci.TransactionNo, tci.SequenceNo";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable TransChargesItemWithCorrection(string transactionNo)
        {
            esParameters par = new esParameters();
            string commandText = @"SELECT tci.TransactionNo, 
	tci.SequenceNo, 
	tci.ParentNo,
	tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) AS ChargeQuantity
FROM Registration r 
INNER JOIN TransCharges tc ON r.RegistrationNo = tc.RegistrationNo AND tc.IsApproved = 1
INNER JOIN TransChargesItem tci ON tci.TransactionNo = tc.TransactionNo AND tci.ReferenceNo = ''
WHERE  tc.TransactionNo = '" + transactionNo + @"'
	AND tci.ChargeQuantity + ISNULL( 
	( 
		  SELECT ISNULL(SUM(tci1.ChargeQuantity), 0) 
		  FROM Registration r1
		  INNER JOIN TransCharges tc1 ON r1.RegistrationNo = tc1.RegistrationNo
		  INNER JOIN TransChargesItem tci1 ON tci1.TransactionNo = tc1.TransactionNo 
		  WHERE  tc1.IsApproved = 1 AND tci1.IsBillProceed = 1 AND tci1.ReferenceNo = tci.TransactionNo AND tci1.SequenceNo = tci.SequenceNo 
		  GROUP BY tci1.ReferenceNo, tci1.ReferenceSequenceNo 
	), 0) > 0
ORDER BY tci.SequenceNo";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public void GetTciByNoRegWithMergeBilling(string RegistrationNo)
        {
            var registrationNoList = MergeBilling.GetMergeRegistration(RegistrationNo);
            GetTciByNoRegWithMergeBilling(registrationNoList, false);
        }

        public void GetTciByNoRegWithMergeBilling(string[] RegistrationNoList, bool IntermbilledOnly)
        {
            var tci = new TransChargesItemQuery("tci");
            var tc = new TransChargesQuery("tc");
            tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(RegistrationNoList))
                .Select(tci);
            if (IntermbilledOnly) {
                var cc = new CostCalculationQuery("cc");
                tci.InnerJoin(cc).On(tci.TransactionNo == cc.TransactionNo && tci.SequenceNo == cc.SequenceNo);
                tci.Where(cc.IntermBillNo.IsNotNull());
            }
            this.Load(tci);
        }

        public DataTable GetTotalMinutes(DateTime startDateTime, DateTime endDateTime)
        {
            esParameters par = new esParameters();

            string commandText = @"SELECT DATEDIFF(MINUTE, '" + startDateTime + "' " + ", '" + endDateTime + "') AS TotMinutes";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetRadiologyExamRsts()
        {
            esParameters par = new esParameters();

            string commandText = @"select tci.TransactionNo, tci.SequenceNo
from TransChargesItem tci
inner join Item i on tci.ItemID = i.ItemID and i.SRItemType = '41'
where tci.IsCorrection = 0 and tci.IsSendToLIS = 1
	and tci.TransactionNo + '-' + tci.ItemID not in (select tr.TransactionNo + '-' + tr.ItemID from TestResult tr)";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}

