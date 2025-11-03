/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 22/01/2025 14:02:31
 ************************************************************/

CREATE PROCEDURE spxml_RL3_6V2025(
	@RlTxReportNo VARCHAR(20)
)

AS

--DECLARE @RlTxReportNo VARCHAR(20) = 'RL/250122-0002'

SELECT 'RL 3.6 Rekapitulasi Kegiatan Pelayanan Kebidanan' RptName,
       CASE 
            WHEN b.PeriodMonthStart = b.PeriodMonthEnd THEN 'Periode : ' + asri.Note + ' ' + b.PeriodYear
            ELSE 'Periode : ' + asri.Note + ' s/d ' + asri2.ItemName + ' ' + b.PeriodYear
       END                           AS Periode,
       asri.Note                     AS Bulan,
       c.RlMasterReportItemName,
       c.RlMasterReportItemCode,
       CASE WHEN c.RlMasterReportItemCode LIKE '%.%' THEN 1 ELSE 0 END flagbold,
       a.*
FROM   RlTxReport3_6V2025            AS a
       JOIN RlTxReportV2025          AS b
            ON  b.RlTxReportNo = a.RlTxReportNo
       JOIN RlMasterReportItemV2025  AS c
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
            )                           h
WHERE  a.RlTxReportNo = @RlTxReportNo