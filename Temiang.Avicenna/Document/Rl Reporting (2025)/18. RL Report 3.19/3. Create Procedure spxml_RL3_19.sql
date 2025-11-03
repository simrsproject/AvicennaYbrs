/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 08/01/2025 10:27:23
 ************************************************************/

CREATE PROCEDURE spxml_RL3_19
 @RlTxReportNo VARCHAR(20)  
AS


  --DECLARE @RlTxReportNo VARCHAR(20)   ='RL/250108-0001'
  	
  
SET NOCOUNT ON  
  
SELECT 'RL 3.19 CARA BAYAR'              ReportName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' +
                 asri.ItemName + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.ItemName + ' s/d ' + asri2.ItemName + ' ' +
                 b.PeriodYear
       END                            AS Periode,
       h.HospitalCode,
       h.HealthcareName,
       h.ProvincesCode,
       h.City,
       b.PeriodYear,
       c.RlMasterReportItemCode       AS 'No',
       c.RlMasterReportItemName       AS 'CaraPembayaran',
       a.RiJpk,
       a.RiJld,
       a.Rj,
       a.RjLab,
       a.RjRad,
       a.RjLl
FROM   RlTxReport3_19V2025                 AS a WITH(NOLOCK)
       INNER JOIN RlTxReportV2025          AS b WITH(NOLOCK)
            ON  b.RlTxReportNo = a.RlTxReportNo
       INNER JOIN RlMasterReportItemV2025  AS c WITH(NOLOCK)
            ON  c.RlMasterReportItemID = a.RlMasterReportItemID
       INNER JOIN AppStandardReferenceItem asri WITH(NOLOCK)
            ON  asri.StandardReferenceID = 'MonthID'
            AND asri.ReferenceID = b.PeriodMonthStart
       INNER JOIN AppStandardReferenceItem asri2 WITH(NOLOCK)
            ON  asri2.StandardReferenceID = 'MonthID'
            AND asri2.ReferenceID = b.PeriodMonthEnd
       CROSS JOIN (
                SELECT *
                FROM   Healthcare AS h WITH(NOLOCK)
                WHERE  h.HealthcareID = (
                           SELECT ap.ParameterValue
                           FROM   AppParameter AS ap WITH(NOLOCK)
                           WHERE  ap.ParameterID = 'HealthcareID'
                       )
            )                            h
WHERE  a.RlTxReportNo = @RlTxReportNo
ORDER BY
       c.RlMasterReportItemNo  
  
  
  