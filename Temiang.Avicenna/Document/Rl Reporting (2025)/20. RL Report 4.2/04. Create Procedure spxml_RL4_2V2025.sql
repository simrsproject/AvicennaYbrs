/************************************************************
 * Code formatted by SoftTree SQL Assistant © v12.0.191
 * Time: 20/01/2025 10:59:07
 ************************************************************/

CREATE PROCEDURE spxml_RL4_2V2025      
(       
 @RlTxReportNo VARCHAR(20)       
)      
AS      
      
SET NOCOUNT ON      

    
--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250116-0005'
      
SELECT TOP 10                    'RL4.2 10 Besar Penyakit Rawat Inap' ReportName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' + asri.Note + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.ItemName + ' ' + b.PeriodYear
       END                    AS Periode,
       asri.Note              AS Bulan,
       h.HealthcareName,
       h.HospitalCode,
       h.ProvincesCode,
       h.City,
       b.PeriodYear,
       a.DiagnosaID,
       d.DiagnoseName         AS 'JenisKegiatan',
       a.KeluarHidupL,
       a.KeluarHidupP,
       a.KeluarMatiL,
       a.KeluarMatiP,
       a.Total
FROM RlTxReport4_2V2025          AS a
       INNER JOIN RlTxReportV2025  AS b
            ON  b.RlTxReportNo = a.RlTxReportNo
       INNER JOIN Diagnose    AS d
            ON  d.DiagnoseID = a.DiagnosaID
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
            )                    h
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY
       a.Total DESC 
       
       