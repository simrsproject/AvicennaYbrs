CREATE PROCEDURE [dbo].[spxml_RL3_13V2025] 
 @RlTxReportNo VARCHAR(20)  
AS  
  
--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250120-0003'
  	
SET NOCOUNT ON  
  
SELECT 'RL 3.13 REKAPITULASI KEGIATAN PELAYANAN REHAB MEDIK'                ReportName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
                 asri.ItemName + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
                 b.PeriodYear
       END                            AS Periode,
       h.HospitalCode,
       h.HealthcareName,
       h.HospitalCode,
       h.ProvincesCode,
       h.City,
       b.PeriodYear,
       c.RlMasterReportItemCode       AS 'No',
       CASE WHEN LEN(c.RlMasterReportItemCode) = 1 THEN '01' ELSE '02' END AS HeaderDetail,
       c.RlMasterReportItemName       AS 'JenisKegiatan',
       a.Jumlah
FROM   RlTxReport3_13v2025                  AS a
       INNER JOIN RlTxReportV2025          AS b
            ON  b.RlTxReportNo = a.RlTxReportNo
       INNER JOIN RlMasterReportItemV2025  AS c
            ON  c.RlMasterReportItemID = a.RlMasterReportItemID
       INNER JOIN AppStandardReferenceItem asri
            ON  asri.StandardReferenceID = 'MonthID'
            AND asri.ReferenceID = b.PeriodMonthStart
       INNER JOIN AppStandardReferenceItem asri2
            ON  asri2.StandardReferenceID = 'MonthID'
            AND asri2.ReferenceID = b.PeriodMonthEnd
       CROSS JOIN (
                SELECT *
                FROM   Healthcare AS h
                WHERE  h.HealthcareID = (
                           SELECT ap.ParameterValue
                           FROM   AppParameter AS ap
                           WHERE  ap.ParameterID = 'HealthcareID'
                       )
            )                            h
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY
       c.RlMasterReportItemNo  
  