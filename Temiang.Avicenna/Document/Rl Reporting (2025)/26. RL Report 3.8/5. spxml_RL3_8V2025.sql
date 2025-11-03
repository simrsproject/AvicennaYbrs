CREATE PROCEDURE spxml_RL3_8V2025
	@RlTxReportNo VARCHAR(20)
AS
  
SET NOCOUNT ON  
IF 1 = 0
BEGIN
    SET FMTONLY OFF
END  
  
--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250506-0008'
  
DECLARE @ReportName VARCHAR(50) = 'Formulir RL 3.8 Rekapitulasi Kegiatan Pelayanan Laboratorium'  
  
---- Config ----  
DECLARE @QtyCharge      INT = 1,
        @QtyPatient     INT;  
  
WITH CTEPatient AS (
         SELECT COUNT(p.PatientID)       AS Patient
         FROM   RlTxReportV2025          AS a WITH (NOLOCK)
                JOIN RlTxReport3_8V2025  AS b WITH (NOLOCK)
                     ON  b.RlTxReportNo = a.RlTxReportNo
                JOIN ItemLaboratory      AS il WITH (NOLOCK)
                     ON  il.ReportRLID = a.RlMasterReportID
                     AND il.RlMasterReportItemID = b.RlMasterReportItemID
                JOIN TransChargesItem    AS tci WITH (NOLOCK)
                     ON  tci.ItemID = il.ItemID
                JOIN TransCharges        AS tc WITH (NOLOCK)
                     ON  tc.TransactionNo = tci.TransactionNo
                     AND YEAR(tc.TransactionDate) = a.PeriodYear
                     AND MONTH(tc.TransactionDate) BETWEEN a.PeriodMonthStart AND a.PeriodMonthEnd
                JOIN Registration        AS r WITH (NOLOCK)
                     ON  r.RegistrationNo = tc.RegistrationNo
                JOIN Patient             AS p WITH (NOLOCK)
                     ON  p.PatientID = r.PatientID
         WHERE  a.RlTxReportNo = @RlTxReportNo
                AND tci.ChargeQuantity >= @QtyCharge
     )  
  
SELECT @QtyPatient = c.Patient
FROM   CTEPatient AS c  
  
--SELECT SUM(a.JumlahPemeriksaanL), SUM(a.JumlahPemeriksaanP), (SUM(a.JumlahPemeriksaanL) + SUM(a.JumlahPemeriksaanP))  
--FROM   Healthcare                     AS h,  
--       RlTxReport3_8V2025             AS a WITH (NOLOCK)  
--       JOIN RlTxReportV2025           AS b WITH (NOLOCK)  
--            ON  b.RlTxReportNo = a.RlTxReportNo  
--       JOIN RlMasterReportItemV2025   AS c WITH (NOLOCK)  
--            ON  c.RlMasterReportItemID = a.RlMasterReportItemID  
--       JOIN AppStandardReferenceItem  AS asri WITH (NOLOCK)  
--            ON  asri.StandardReferenceID = 'MonthID'  
--            AND asri.ReferenceID = b.PeriodMonthStart  
--       JOIN AppStandardReferenceItem  AS asri2 WITH (NOLOCK)  
--            ON  asri2.StandardReferenceID = 'MonthID'  
--            AND asri2.ReferenceID = b.PeriodMonthEnd  
--WHERE  a.RlTxReportNo = @RlTxReportNo  
  
  
---- Display ----  
SELECT @ReportName                    AS ReportName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
                 asri.Note + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.Note + ' ' +
                 b.PeriodYear
       END                            AS Periode,
       h.HospitalCode,
       h.HealthcareName,
       h.ProvincesCode,
       h.City,
       b.PeriodYear,
       c.RlMasterReportItemCode       AS [No],
       c.RlMasterReportItemName       AS [JenisKegiatan],
       a.JumlahPemeriksaanL,
       a.JumlahPemeriksaanP,
       @QtyPatient                    AS QtyPatient,
       CASE 
            WHEN a.JumlahPemeriksaanL > 0 AND @QtyPatient > 0 THEN CONVERT(
                     DECIMAL(10, 2),
                     CAST(a.JumlahPemeriksaanL AS DECIMAL(10, 6)) / @QtyPatient
                 )
            ELSE 0
       END                            AS RataRataL,
       CASE 
            WHEN a.JumlahPemeriksaanP > 0 AND @QtyPatient > 0 THEN CONVERT(
                     DECIMAL(10, 2),
                     CAST(a.JumlahPemeriksaanP AS DECIMAL(10, 6)) / @QtyPatient
                 )
            ELSE 0
       END                            AS RataRataP
FROM   Healthcare                     AS h,
       RlTxReport3_8V2025             AS a WITH (NOLOCK)
       JOIN RlTxReportV2025           AS b WITH (NOLOCK)
            ON  b.RlTxReportNo = a.RlTxReportNo
       JOIN RlMasterReportItemV2025   AS c WITH (NOLOCK)
            ON  c.RlMasterReportItemID = a.RlMasterReportItemID
       JOIN AppStandardReferenceItem  AS asri WITH (NOLOCK)
            ON  asri.StandardReferenceID = 'MonthID'
            AND asri.ReferenceID = b.PeriodMonthStart
       JOIN AppStandardReferenceItem  AS asri2 WITH (NOLOCK)
            ON  asri2.StandardReferenceID = 'MonthID'
            AND asri2.ReferenceID = b.PeriodMonthEnd
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY
       a.RlMasterReportItemID,
       c.RlMasterReportItemNo